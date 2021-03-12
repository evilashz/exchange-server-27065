using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Clients.Owa2.Server.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C6 RID: 454
	internal class Pusher
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0003D89B File Offset: 0x0003BA9B
		public static Pusher Instance
		{
			get
			{
				return Pusher.instance;
			}
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003D8A4 File Offset: 0x0003BAA4
		protected Pusher()
		{
			this.remoteNotificationManager = this.GetRemoteNotificationManager();
			this.remoteNotificationRequester = this.GetRemoteNotificationRequester();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003D914 File Offset: 0x0003BB14
		public void Distribute(List<NotificationPayloadBase> payloads, string subscriptionContextKey, string subscriptionId)
		{
			if (payloads != null && payloads.Count > 0)
			{
				IEnumerable<IDestinationInfo> destinations = this.remoteNotificationManager.GetDestinations(subscriptionContextKey, subscriptionId);
				int num = 0;
				lock (this.syncRoot)
				{
					foreach (IDestinationInfo destinationInfo in destinations)
					{
						num++;
						this.AddToQueue(destinationInfo, payloads);
					}
				}
				OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.Distribute)
				{
					OriginationUserContextKey = subscriptionContextKey,
					DestinationCount = num,
					PayloadCount = payloads.Count
				});
				if (ExTraceGlobals.NotificationsCallTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					IEnumerable<string> values = from destination in destinations
					select string.Format("[Destination:{0}; ChannelId:{1}]", destination.Destination, string.Join(",", destination.ChannelIds));
					ExTraceGlobals.NotificationsCallTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Pusher - Distribute notification from mailbox referred by user context key {0} to destinations {1}.", subscriptionContextKey, string.Join(",", values));
				}
			}
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003DA34 File Offset: 0x0003BC34
		public virtual RemoteNotificationManager GetRemoteNotificationManager()
		{
			return RemoteNotificationManager.Instance;
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003DA3B File Offset: 0x0003BC3B
		public virtual RemoteNotificationRequester GetRemoteNotificationRequester()
		{
			return RemoteNotificationRequester.Instance;
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003DA42 File Offset: 0x0003BC42
		public virtual PusherQueue CreatePusherQueue(IDestinationInfo destinationInfo)
		{
			return new PusherQueue(destinationInfo.Destination, new Action<PusherQueue>(this.QueueReady));
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003DA5C File Offset: 0x0003BC5C
		private void AddToQueue(IDestinationInfo destinationInfo, List<NotificationPayloadBase> payloads)
		{
			PusherQueue pusherQueue;
			if (!this.pusherQueues.TryGetValue(destinationInfo.Destination.Authority, out pusherQueue))
			{
				pusherQueue = (this.pusherQueues[destinationInfo.Destination.Authority] = this.CreatePusherQueue(destinationInfo));
			}
			pusherQueue.Enqueue(payloads, destinationInfo.ChannelIds);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
		internal void QueueReady(PusherQueue pusherQueue)
		{
			lock (this.syncRoot)
			{
				this.readyQueue.Enqueue(pusherQueue);
				this.StartPusherThread();
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0003DB00 File Offset: 0x0003BD00
		private void StartPusherThread()
		{
			if (this.isPusherThreadActive == 0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessQueues));
				return;
			}
			ExTraceGlobals.NotificationsCallTracer.TraceDebug((long)this.GetHashCode(), "Pusher Thread is already active. Not starting one.");
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0003DB34 File Offset: 0x0003BD34
		private void ProcessQueues(object state)
		{
			int num = -1;
			bool flag = false;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			Exception ex = null;
			try
			{
				num = Interlocked.CompareExchange(ref this.isPusherThreadActive, 1, 0);
				if (num == 0)
				{
					OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.PusherThreadStart)
					{
						ThreadId = managedThreadId
					});
					List<Task> list = new List<Task>();
					PusherQueue nextQueue;
					while ((nextQueue = this.GetNextQueue(list.Count, out flag)) != null)
					{
						ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>((long)this.GetHashCode(), "Processing Pusher Queue. Destination: {0}", nextQueue.DestinationUrl);
						list.Add(this.remoteNotificationRequester.SendNotificationsAsync(nextQueue));
						this.remoteNotificationRequester.UnderRequestLimitEvent.Wait();
					}
					OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.PusherThreadCleanup)
					{
						ThreadId = managedThreadId,
						TaskCount = list.Count
					});
					lock (this.syncRoot)
					{
						foreach (string key in this.pusherQueues.Keys.ToArray<string>())
						{
							if (this.pusherQueues[key].TotalPayloads == 0)
							{
								this.pusherQueues.Remove(key);
							}
						}
					}
					Task.WaitAll(list.ToArray());
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				ExWatson.SendReport(ex, ReportOptions.None, null);
			}
			finally
			{
				if (num == 0)
				{
					OwaServerTraceLogger.AppendToLog(new PusherLogEvent(PusherEventType.PusherThreadEnd)
					{
						ThreadId = managedThreadId,
						HandledException = ex
					});
					if (!flag)
					{
						Interlocked.CompareExchange(ref this.isPusherThreadActive, 0, 1);
						this.StartPusherThread();
					}
					this.EndOfPusherThreadTestIndicator();
				}
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003DD1C File Offset: 0x0003BF1C
		protected virtual void EndOfPusherThreadTestIndicator()
		{
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003DD20 File Offset: 0x0003BF20
		private PusherQueue GetNextQueue(int taskCount, out bool wasPusherThreadActiveFlagReset)
		{
			PusherQueue result;
			lock (this.syncRoot)
			{
				PusherQueue pusherQueue = null;
				wasPusherThreadActiveFlagReset = false;
				if (taskCount < 10000 && this.readyQueue.Count > 0)
				{
					pusherQueue = this.readyQueue.Dequeue();
				}
				else
				{
					this.isPusherThreadActive = 0;
					wasPusherThreadActiveFlagReset = true;
				}
				result = pusherQueue;
			}
			return result;
		}

		// Token: 0x04000986 RID: 2438
		private const int MaxTasksForPusherThread = 10000;

		// Token: 0x04000987 RID: 2439
		private static readonly Pusher instance = new Pusher();

		// Token: 0x04000988 RID: 2440
		private readonly Dictionary<string, PusherQueue> pusherQueues = new Dictionary<string, PusherQueue>();

		// Token: 0x04000989 RID: 2441
		private readonly Queue<PusherQueue> readyQueue = new Queue<PusherQueue>();

		// Token: 0x0400098A RID: 2442
		private readonly object syncRoot = new object();

		// Token: 0x0400098B RID: 2443
		private readonly RemoteNotificationManager remoteNotificationManager;

		// Token: 0x0400098C RID: 2444
		private readonly RemoteNotificationRequester remoteNotificationRequester;

		// Token: 0x0400098D RID: 2445
		private int isPusherThreadActive;
	}
}
