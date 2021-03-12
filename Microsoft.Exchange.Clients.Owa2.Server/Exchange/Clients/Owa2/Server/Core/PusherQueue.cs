using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Hosting;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C9 RID: 457
	internal class PusherQueue
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x0003E0E1 File Offset: 0x0003C2E1
		static PusherQueue()
		{
			if (Globals.IsPreCheckinApp)
			{
				PusherQueue.ListenerVdir = HostingEnvironment.ApplicationVirtualPath;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0003E100 File Offset: 0x0003C300
		public int TotalPayloads
		{
			get
			{
				int result;
				lock (this.syncRoot)
				{
					result = this.payloadQueue.Count + ((this.inTransitQueue == null) ? 0 : this.inTransitQueue.Count);
				}
				return result;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0003E160 File Offset: 0x0003C360
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x0003E168 File Offset: 0x0003C368
		public string DestinationUrl { get; private set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x0003E171 File Offset: 0x0003C371
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x0003E179 File Offset: 0x0003C379
		public int FailureCount { get; private set; }

		// Token: 0x06001032 RID: 4146 RVA: 0x0003E184 File Offset: 0x0003C384
		public PusherQueue(Uri destination, Action<PusherQueue> readyCallback)
		{
			this.readyCallback = readyCallback;
			this.DestinationUrl = string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}/remotenotification.ashx", new object[]
			{
				destination.Scheme,
				destination.Authority,
				PusherQueue.ListenerVdir
			});
			this.payloadQueue = new Queue<PusherQueuePayload>();
			this.remoteNotificationManager = this.GetRemoteNotificationManager();
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003E218 File Offset: 0x0003C418
		public virtual void Enqueue(List<NotificationPayloadBase> payloads, IEnumerable<string> channelIds)
		{
			bool flag = false;
			lock (this.syncRoot)
			{
				if (this.TotalPayloads + payloads.Count <= 200)
				{
					flag = (this.TotalPayloads == 0);
					foreach (NotificationPayloadBase payload2 in payloads)
					{
						this.payloadQueue.Enqueue(new PusherQueuePayload(payload2, channelIds));
					}
					NotificationStatisticsManager.Instance.NotificationQueued(payloads);
				}
				else
				{
					HashSet<string> hashSet = new HashSet<string>();
					foreach (PusherQueuePayload pusherQueuePayload in this.payloadQueue.Concat(this.inTransitQueue ?? Array<PusherQueuePayload>.Empty).Concat(from payload in payloads
					select new PusherQueuePayload(payload, channelIds)))
					{
						hashSet.UnionWith(pusherQueuePayload.ChannelIds);
					}
					IEnumerable<NotificationPayloadBase> payloads2 = from p in this.payloadQueue
					select p.Payload;
					NotificationStatisticsManager.Instance.NotificationDropped(payloads2, NotificationState.Queued);
					this.payloadQueue.Clear();
					this.inTransitQueue = null;
					ReloadAllNotificationPayload reloadAllNotificationPayload = new ReloadAllNotificationPayload();
					reloadAllNotificationPayload.Source = new TypeLocation(base.GetType());
					this.payloadQueue.Enqueue(new PusherQueuePayload(reloadAllNotificationPayload, hashSet));
					NotificationStatisticsManager.Instance.NotificationCreated(reloadAllNotificationPayload);
					NotificationStatisticsManager.Instance.NotificationQueued(reloadAllNotificationPayload);
				}
			}
			if (flag)
			{
				this.readyCallback(this);
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003E428 File Offset: 0x0003C628
		public virtual PusherQueuePayload[] GetPayloads()
		{
			PusherQueuePayload[] result;
			lock (this.syncRoot)
			{
				if (this.inTransitQueue == null)
				{
					this.inTransitQueue = this.payloadQueue;
					this.payloadQueue = new Queue<PusherQueuePayload>();
				}
				else
				{
					while (this.payloadQueue.Count > 0)
					{
						this.inTransitQueue.Enqueue(this.payloadQueue.Dequeue());
					}
				}
				result = this.inTransitQueue.ToArray();
			}
			return result;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003E4C8 File Offset: 0x0003C6C8
		public void SendComplete(bool success)
		{
			bool flag = false;
			string[] array = null;
			lock (this.syncRoot)
			{
				if (success)
				{
					this.inTransitQueue = null;
					this.FailureCount = 0;
				}
				else if (++this.FailureCount >= 5)
				{
					array = (from payload in this.payloadQueue.Concat(this.inTransitQueue ?? Array<PusherQueuePayload>.Empty)
					select payload.ChannelIds).Aggregate((IEnumerable<string> workingset, IEnumerable<string> next) => workingset.Union(next)).ToArray<string>();
					this.payloadQueue.Clear();
					this.inTransitQueue = null;
				}
				flag = (this.TotalPayloads > 0);
			}
			if (flag)
			{
				this.readyCallback(this);
			}
			if (array != null)
			{
				foreach (string channelId in array)
				{
					this.remoteNotificationManager.CleanUpChannel(channelId);
				}
				ExTraceGlobals.NotificationsCallTracer.TraceDebug<string>(0L, "Pusher removed implicity lost channels. LostChannelIds: {0}", string.Join(",", array));
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003E60C File Offset: 0x0003C80C
		public virtual RemoteNotificationManager GetRemoteNotificationManager()
		{
			return RemoteNotificationManager.Instance;
		}

		// Token: 0x040009A1 RID: 2465
		public const int MaxQueueSize = 200;

		// Token: 0x040009A2 RID: 2466
		private const int MaxFailureCount = 5;

		// Token: 0x040009A3 RID: 2467
		private static readonly string ListenerVdir = "/owa";

		// Token: 0x040009A4 RID: 2468
		private readonly Action<PusherQueue> readyCallback;

		// Token: 0x040009A5 RID: 2469
		private readonly object syncRoot = new object();

		// Token: 0x040009A6 RID: 2470
		private readonly RemoteNotificationManager remoteNotificationManager;

		// Token: 0x040009A7 RID: 2471
		protected Queue<PusherQueuePayload> payloadQueue;

		// Token: 0x040009A8 RID: 2472
		private Queue<PusherQueuePayload> inTransitQueue;
	}
}
