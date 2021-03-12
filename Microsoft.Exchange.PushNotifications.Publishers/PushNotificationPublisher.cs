using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.CrimsonEvents;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000011 RID: 17
	internal abstract class PushNotificationPublisher<TNotif, TChannel> : PushNotificationPublisherBase where TNotif : PushNotification where TChannel : PushNotificationChannel<TNotif>
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003014 File Offset: 0x00001214
		protected PushNotificationPublisher(PushNotificationPublisherSettings settings, ITracer tracer, IThrottlingManager throttlingManager = null, List<IPushNotificationMapping<TNotif>> mappings = null, PushNotificationQueue<TNotif> notificationQueue = null, IPushNotificationOptics optics = null) : base(settings, tracer)
		{
			this.Counters = PublisherCounters.GetInstance(base.AppId);
			this.QueueSizeCounter = new ItemCounter(this.Counters.QueueSize, this.Counters.QueueSizePeak, this.Counters.QueueSizeTotal);
			this.throttlingManager = throttlingManager;
			this.mappings = new Dictionary<Type, IPushNotificationMapping<TNotif>>();
			if (mappings != null)
			{
				foreach (IPushNotificationMapping<TNotif> pushNotificationMapping in mappings)
				{
					this.mappings.Add(pushNotificationMapping.InputType, pushNotificationMapping);
				}
			}
			this.notificationQueue = (notificationQueue ?? new PushNotificationQueue<TNotif>(base.Settings.QueueSize));
			this.optics = (optics ?? PushNotificationOptics.Default);
			this.cancellationTokenSource = new CancellationTokenSource();
			this.channelThreads = new Thread[base.Settings.NumberOfChannels];
			for (int i = 0; i < base.Settings.NumberOfChannels; i++)
			{
				this.channelThreads[i] = new Thread(new ThreadStart(this.ConsumeNotifications));
				this.channelThreads[i].Start();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000315C File Offset: 0x0000135C
		private PublisherCountersInstance Counters { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003165 File Offset: 0x00001365
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000316D File Offset: 0x0000136D
		private ItemCounter QueueSizeCounter { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003176 File Offset: 0x00001376
		private CancellationToken CancelToken
		{
			get
			{
				return this.cancellationTokenSource.Token;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003184 File Offset: 0x00001384
		public override void Publish(Notification notification, PushNotificationPublishingContext context)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			ArgumentValidator.ThrowIfNull("context", context);
			base.CheckDisposed();
			if (!notification.IsValid)
			{
				this.optics.ReportDiscardedByValidation(notification);
				return;
			}
			IPushNotificationMapping<TNotif> pushNotificationMapping;
			if (!this.mappings.TryGetValue(notification.GetType(), out pushNotificationMapping))
			{
				this.optics.ReportDiscardedByUnknownMapping(notification);
				return;
			}
			TNotif tnotif;
			if (pushNotificationMapping.TryMap(notification, context, out tnotif))
			{
				this.optics.ReportProcessed(notification, tnotif, context);
				this.Publish(tnotif);
				return;
			}
			this.optics.ReportDiscardedByFailedMapping(notification);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003218 File Offset: 0x00001418
		public override void Publish(PushNotification notification)
		{
			TNotif tnotif = notification as TNotif;
			if (tnotif == null)
			{
				this.optics.ReportDiscardedByMissmatchingType(notification);
				return;
			}
			this.Publish(notification as TNotif);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003258 File Offset: 0x00001458
		public void Publish(TNotif notification)
		{
			ArgumentValidator.ThrowIfNull("notification", notification);
			base.CheckDisposed();
			if (PushNotificationsCrimsonEvents.NotificationToPublish.IsEnabled(PushNotificationsCrimsonEvent.Provider))
			{
				PushNotificationsCrimsonEvents.NotificationToPublish.Log<string, string>(notification.AppId, notification.ToFullString());
			}
			PushNotificationQueueItem<TNotif> pushNotificationQueueItem = this.PreprocessNotification(notification);
			if (pushNotificationQueueItem != null)
			{
				this.AddToNotificationQueue(pushNotificationQueueItem);
			}
		}

		// Token: 0x06000093 RID: 147
		protected abstract TChannel CreateNotificationChannel();

		// Token: 0x06000094 RID: 148 RVA: 0x000032C4 File Offset: 0x000014C4
		protected virtual bool TryPreprocess(TNotif notification)
		{
			if (notification.IsValid)
			{
				OverBudgetException obe;
				if (this.throttlingManager == null || this.throttlingManager.TryApproveNotification(notification, out obe))
				{
					return true;
				}
				this.optics.ReportDiscardedByThrottling(notification, obe);
			}
			else
			{
				this.OnInvalidNotificationFound(notification, new InvalidPushNotificationException(notification.ValidationErrors[0]));
			}
			return false;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003333 File Offset: 0x00001533
		protected void OnInvalidNotificationFound(TNotif notification, Exception ex)
		{
			this.optics.ReportDiscardedByValidation(notification, ex);
			if (this.throttlingManager != null)
			{
				this.throttlingManager.ReportInvalidNotifications(notification);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003360 File Offset: 0x00001560
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.notificationQueue.CompleteAdding();
				this.cancellationTokenSource.Cancel();
				for (int i = 0; i < this.channelThreads.Length; i++)
				{
					try
					{
						this.channelThreads[i].Join();
						this.channelThreads[i] = null;
					}
					catch (ThreadStateException exception)
					{
						base.Tracer.TraceError<int, string>((long)this.GetHashCode(), "[InternalDispose] ThreadStateException for channel {0}: '{1}'", i, exception.ToTraceString());
					}
					catch (ThreadInterruptedException exception2)
					{
						base.Tracer.TraceError<int, string>((long)this.GetHashCode(), "[InternalDispose] ThreadStateException for channel {0}: '{1}'", i, exception2.ToTraceString());
					}
				}
				this.notificationQueue.Dispose();
				this.cancellationTokenSource.Dispose();
				this.notificationQueue = null;
				this.cancellationTokenSource = null;
				this.channelThreads = null;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003440 File Offset: 0x00001640
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<PushNotificationPublisher<TNotif, TChannel>>(this);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003448 File Offset: 0x00001648
		private PushNotificationQueueItem<TNotif> PreprocessNotification(TNotif notification)
		{
			AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(this.Counters.AveragePreprocessTime, this.Counters.AveragePreprocessTimeBase, true);
			if (this.TryPreprocess(notification))
			{
				averageTimeCounterBase.Stop();
				base.Tracer.TraceDebug<TNotif>((long)this.GetHashCode(), "[PushNotificationPublisher] Preprocess notification succeeded: '{0}'.", notification);
				return new PushNotificationQueueItem<TNotif>
				{
					Notification = notification,
					QueueTimeCounter = new AverageTimeCounterBase(this.Counters.AverageQueueItemTime, this.Counters.AverageQueueItemTimeBase)
				};
			}
			this.Counters.PreprocessError.Increment();
			this.Counters.TotalNotificationsDiscarded.Increment();
			return null;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034F0 File Offset: 0x000016F0
		private void AddToNotificationQueue(PushNotificationQueueItem<TNotif> prepNotification)
		{
			bool flag = false;
			try
			{
				prepNotification.QueueTimeCounter.Start();
				flag = this.notificationQueue.TryAdd(prepNotification, base.Settings.AddTimeout, this.CancelToken);
				if (flag)
				{
					this.QueueSizeCounter.Increment();
					base.Tracer.TraceDebug<TNotif>((long)this.GetHashCode(), "[PushNotificationPublisher] Notification added to the queue. '{0}'", prepNotification.Notification);
				}
				else
				{
					base.Tracer.TraceWarning<TNotif>((long)this.GetHashCode(), "[PushNotificationPublisher] Unable to add notification to the queue.'{0}'", prepNotification.Notification);
				}
			}
			catch (OperationCanceledException ex)
			{
				throw new ObjectDisposedException(ex.Message, ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new ObjectDisposedException(ex2.Message, ex2);
			}
			finally
			{
				if (!flag)
				{
					this.Counters.TotalNotificationsDiscarded.Increment();
					this.Counters.EnqueueError.Increment();
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000035E0 File Offset: 0x000017E0
		private void ConsumeNotifications()
		{
			bool flag = false;
			while (!flag)
			{
				flag = true;
				try
				{
					PushNotificationQueueItem<TNotif> pushNotificationQueueItem;
					if (this.notificationQueue.TryTake(out pushNotificationQueueItem, -1, this.CancelToken))
					{
						pushNotificationQueueItem.QueueTimeCounter.Stop();
						this.QueueSizeCounter.Decrement();
						using (TChannel tchannel = this.CreateNotificationChannel())
						{
							tchannel.InvalidNotificationDetected += this.ChannelFoundInvalidNotification;
							this.SendNotification(tchannel, pushNotificationQueueItem.Notification);
							foreach (PushNotificationQueueItem<TNotif> pushNotificationQueueItem2 in this.notificationQueue.GetConsumingEnumerable(this.CancelToken))
							{
								pushNotificationQueueItem2.QueueTimeCounter.Stop();
								this.QueueSizeCounter.Decrement();
								this.SendNotification(tchannel, pushNotificationQueueItem2.Notification);
							}
							continue;
						}
					}
					base.Tracer.TraceError((long)this.GetHashCode(), "[PushNotificationPublisher] TryTake returned false");
					flag = false;
				}
				catch (OperationCanceledException)
				{
					base.Tracer.TraceDebug((long)this.GetHashCode(), "[PushNotificationPublisher] Done by OperationCanceledException");
				}
				catch (ObjectDisposedException)
				{
					base.Tracer.TraceDebug((long)this.GetHashCode(), "[PushNotificationPublisher] Done by ObjectDisposedException");
				}
				catch (Exception exception)
				{
					base.Tracer.TraceError<string>((long)this.GetHashCode(), "[PushNotificationPublisher] Unexpected exception '{0}'", exception.ToTraceString());
					PushNotificationsCrimsonEvents.PushNotificationPublisherConsumeError.Log<string, string, string>(base.AppId, string.Empty, exception.ToTraceString());
					flag = base.IsDisposed;
				}
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037A0 File Offset: 0x000019A0
		private void SendNotification(TChannel channel, TNotif notification)
		{
			base.Tracer.TraceDebug<TNotif>((long)this.GetHashCode(), "[PushNotificationPublisher] Sending notification. '{0}'", notification);
			using (new AverageTimeCounter(this.Counters.AveragePublisherSendTime, this.Counters.AveragePublisherSendTimeBase))
			{
				channel.Send(notification, this.CancelToken);
			}
			this.Counters.TotalNotificationsSent.Increment();
			base.Tracer.TraceDebug<TNotif>((long)this.GetHashCode(), "[PushNotificationPublisher] Notification sent. '{0}'", notification);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000383C File Offset: 0x00001A3C
		private void ChannelFoundInvalidNotification(object sender, InvalidNotificationEventArgs e)
		{
			this.OnInvalidNotificationFound((TNotif)((object)e.Notification), e.Exception);
		}

		// Token: 0x04000027 RID: 39
		private PushNotificationQueue<TNotif> notificationQueue;

		// Token: 0x04000028 RID: 40
		private Dictionary<Type, IPushNotificationMapping<TNotif>> mappings;

		// Token: 0x04000029 RID: 41
		private IThrottlingManager throttlingManager;

		// Token: 0x0400002A RID: 42
		private IPushNotificationOptics optics;

		// Token: 0x0400002B RID: 43
		private CancellationTokenSource cancellationTokenSource;

		// Token: 0x0400002C RID: 44
		private Thread[] channelThreads;
	}
}
