using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003BA RID: 954
	internal class RoutedMessageQueue : RemoteMessageQueue, ILockableQueue, IRoutedMessageQueue, IDisposeTrackable, IDisposable
	{
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000AD700 File Offset: 0x000AB900
		public virtual int ActiveQueueLength
		{
			get
			{
				return base.ActiveCount + ((base.ConditionManager != null && base.ConditionManager.MapStateChanged) ? base.LockedCount : 0);
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000AD728 File Offset: 0x000AB928
		protected override LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyTracker.GetDeliveryQueueLatencyComponent(this.key.NextHopType.DeliveryType);
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000AD750 File Offset: 0x000AB950
		public virtual int GetActiveQueueLength(DeliveryPriority priority)
		{
			if (base.SupportsFixedPriority)
			{
				return this.subQueues[(int)priority].ActiveCount + ((base.ConditionManager != null && base.ConditionManager.MapStateChanged) ? this.subQueues[(int)priority].LockedCount : 0);
			}
			return this.ActiveQueueLength;
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000AD79F File Offset: 0x000AB99F
		public virtual int TotalQueueLength
		{
			get
			{
				return base.TotalCount;
			}
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x000AD7A8 File Offset: 0x000AB9A8
		public int ActiveConnections
		{
			get
			{
				int result;
				lock (this.syncObject)
				{
					result = this.activeConnections[0] + this.activeConnections[1] + this.activeConnections[2];
				}
				return result;
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x000AD800 File Offset: 0x000ABA00
		public int AttemptingConnections
		{
			get
			{
				int result;
				lock (this.syncObject)
				{
					result = this.attemptingConnections[0].Count + this.attemptingConnections[1].Count + this.attemptingConnections[2].Count;
				}
				return result;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000AD868 File Offset: 0x000ABA68
		public int TotalConnections
		{
			get
			{
				int result;
				lock (this.syncObject)
				{
					result = this.ActiveConnections + this.AttemptingConnections;
				}
				return result;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000AD8B4 File Offset: 0x000ABAB4
		public int InFlightMessages
		{
			get
			{
				return this.inFlightMessages;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x000AD8BC File Offset: 0x000ABABC
		public override NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
		public bool RetryConnectionScheduled
		{
			get
			{
				return this.retryTimerInfo != null;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x000AD8D2 File Offset: 0x000ABAD2
		// (set) Token: 0x06002B8D RID: 11149 RVA: 0x000AD8E5 File Offset: 0x000ABAE5
		public DateTime LastRetryTime
		{
			get
			{
				return new DateTime(Interlocked.Read(ref this.lastRetryTime), DateTimeKind.Utc);
			}
			internal set
			{
				Interlocked.Exchange(ref this.lastRetryTime, value.Ticks);
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x000AD8FA File Offset: 0x000ABAFA
		public DateTime FirstRetryTime
		{
			get
			{
				return this.firstRetryTime;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000AD902 File Offset: 0x000ABB02
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000AD90F File Offset: 0x000ABB0F
		public long LastDeliveryTime
		{
			get
			{
				return Interlocked.Read(ref this.lastDeliveryTime);
			}
			set
			{
				Interlocked.Exchange(ref this.lastDeliveryTime, value);
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000AD91E File Offset: 0x000ABB1E
		public int ConnectionRetryCount
		{
			get
			{
				return this.connectionRetryCount;
			}
		}

		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x000AD926 File Offset: 0x000ABB26
		public long Id
		{
			get
			{
				return this.queueStorage.Id;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000AD933 File Offset: 0x000ABB33
		public string NextHopDomain
		{
			get
			{
				return this.queueStorage.NextHopDomain;
			}
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x000AD940 File Offset: 0x000ABB40
		public bool GetRetryConnectionSchedule(out DateTime nextRetryTime)
		{
			RoutedMessageQueue.RetryTimerInfo retryTimerInfo = (RoutedMessageQueue.RetryTimerInfo)this.retryTimerInfo;
			if (retryTimerInfo != null)
			{
				nextRetryTime = retryTimerInfo.NextRetryTime;
				return true;
			}
			nextRetryTime = DateTime.MaxValue;
			return false;
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000AD976 File Offset: 0x000ABB76
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x000AD97E File Offset: 0x000ABB7E
		public SmtpResponse LastError
		{
			get
			{
				return this.lastError;
			}
			set
			{
				this.lastError = value;
			}
		}

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000AD987 File Offset: 0x000ABB87
		public bool IsEmpty
		{
			get
			{
				return this.inFlightMessages == 0 && base.TotalCount == 0;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000AD99C File Offset: 0x000ABB9C
		public bool IsAdminVisible
		{
			get
			{
				return this.Key.NextHopType != NextHopType.Heartbeat;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (set) Token: 0x06002B99 RID: 11161 RVA: 0x000AD9C1 File Offset: 0x000ABBC1
		public override bool Suspended
		{
			set
			{
				base.Suspended = value;
				if (this.Suspended)
				{
					this.RelockAllItems("Queue suspended");
				}
			}
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000AD9E0 File Offset: 0x000ABBE0
		public bool CanBeDeleted(TimeSpan idleTime)
		{
			long num = Math.Max(base.LastDequeueTime, base.LastResubmitTime);
			return this.referenceCount == 0 && this.IsEmpty && !this.Suspended && num + idleTime.Ticks < DateTime.UtcNow.Ticks;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000ADA34 File Offset: 0x000ABC34
		public bool CanResubmit(TimeSpan idleTime)
		{
			if (this.key.NextHopType.IsConnectorDeliveryType || this.key.NextHopType == NextHopType.Heartbeat)
			{
				return false;
			}
			if (!this.RetryConnectionScheduled)
			{
				return false;
			}
			long ticks = DateTime.UtcNow.Ticks;
			long val = ticks - this.LastDeliveryTime;
			long val2 = ticks - base.LastResubmitTime;
			long num = Math.Min(val, val2);
			return num > idleTime.Ticks;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000ADAB0 File Offset: 0x000ABCB0
		public bool EvaluateConnectionAttempt(DeliveryPriority priority)
		{
			if (this.RetryConnectionScheduled)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} is in retry, no connection will be created.", this.key);
				return false;
			}
			if (this.Suspended)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} is frozen, no connection will be created.", this.key);
				return false;
			}
			if (this.GetActiveQueueLength(priority) == 0)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} has no active messages, no connection will be created.", this.key);
				return false;
			}
			if (this.key.NextHopType != NextHopType.Heartbeat)
			{
				return true;
			}
			bool flag;
			bool flag2;
			Components.ShadowRedundancyComponent.ShadowRedundancyManager.EvaluateHeartbeatAttempt(this.key, out flag, out flag2);
			if (flag2)
			{
				this.AbortHeartbeat();
			}
			if (!flag)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat connection not created for queue {0}.", this.key);
			}
			return flag;
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000ADB9C File Offset: 0x000ABD9C
		public void NDRAllMessages(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, SmtpResponse smtpResponse, AckDetails details)
		{
			if (this.key.NextHopType == NextHopType.Heartbeat)
			{
				throw new InvalidOperationException("Should never attempt to NDR heartbeat messages");
			}
			if (base.DeferredCount > 0)
			{
				return;
			}
			if (this.TotalConnections != 0)
			{
				return;
			}
			ICollection<IQueueItem> collection = base.DequeueAll((IQueueItem item) => true, false);
			foreach (IQueueItem queueItem in collection)
			{
				RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
				bool flag;
				RiskLevel riskLevel;
				DeliveryPriority priority;
				routedMailItem.Ack(AckStatus.Fail, smtpResponse, messageTrackingSource, messageTrackingSourceContext, details, false, out flag, out riskLevel, out priority);
				if (!flag)
				{
					Components.QueueManager.UpateInstanceCounter(riskLevel, priority, delegate(QueuingPerfCountersInstance c)
					{
						c.ItemsCompletedDeliveryTotal.Increment();
					});
				}
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000ADC84 File Offset: 0x000ABE84
		public override void Enqueue(IQueueItem item)
		{
			RoutedMailItem routedMailItem = item as RoutedMailItem;
			if (routedMailItem == null)
			{
				throw new InvalidOperationException("Attempt to enqueue a non-routedmailitem (or null) to the RoutedMessageQueue");
			}
			routedMailItem.EnqueuedTime = DateTime.UtcNow;
			if (this.orgId == null && !string.IsNullOrEmpty(this.key.OverrideSource))
			{
				this.orgId = routedMailItem.OrganizationId;
			}
			base.Enqueue(item);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000ADCE4 File Offset: 0x000ABEE4
		public void UpdateQueue()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > this.lastDeferEventLogTime.Add(Components.TransportAppConfig.QueueConfiguration.MessageDeferEventCheckInterval))
			{
				this.HandleDeferEventLogging();
				this.lastDeferEventLogTime = utcNow;
			}
			base.UpdateQueueRates();
			base.TimedUpdate();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000ADD34 File Offset: 0x000ABF34
		private void HandleDeferEventLogging()
		{
			bool retryConnectionScheduled = this.RetryConnectionScheduled;
			AckDetails ackDetails = new AckDetails(new IPEndPoint(IPAddress.None, 0), this.Key.NextHopDomain, null, this.Key.NextHopConnector.ToString(), IPAddress.None);
			RoutedMessageQueue.DeferLoggingState state = new RoutedMessageQueue.DeferLoggingState(DateTime.UtcNow, retryConnectionScheduled, this.Suspended, ackDetails);
			if (RoutedMessageQueue.RetryDeferLoggingEnabled)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "HandleDeferEventLogging: ForEach message call HandleLogDeferRetryOrSuspended. Start.");
				base.ForEach<RoutedMessageQueue.DeferLoggingState>(new Action<IQueueItem, RoutedMessageQueue.DeferLoggingState>(this.HandleLogDeferRetryOrSuspended), state, true);
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "HandleDeferEventLogging: ForEach message call HandleLogDeferRetryOrSuspended. Done.");
			}
			if (RoutedMessageQueue.DelayDeferLoggingEnabled && !this.Suspended && !retryConnectionScheduled)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "HandleDeferEventLogging: ForEach message (except deferred queue) call HandleLogDeferDelay. Start.");
				base.ForEach<RoutedMessageQueue.DeferLoggingState>(new Action<IQueueItem, RoutedMessageQueue.DeferLoggingState>(this.HandleLogDeferDelay), state, false);
				ExTraceGlobals.QueuingTracer.TraceDebug((long)this.GetHashCode(), "HandleDeferEventLogging: ForEach message (except deferred queue) call HandleLogDeferDelay. Done.");
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000ADE38 File Offset: 0x000AC038
		public void HandleLogDeferRetryOrSuspended(IQueueItem item, RoutedMessageQueue.DeferLoggingState state)
		{
			RoutedMailItem routedMailItem = item as RoutedMailItem;
			if (!routedMailItem.IsDeletedByAdmin && !routedMailItem.RetryDeferLogged && state.ScanStartTime.Ticks > routedMailItem.OriginalEnqueuedTime.Add(RoutedMessageQueue.RetryDeferLoggingInterval).Ticks)
			{
				if (state.QueueSuspended || routedMailItem.IsSuspendedByAdmin)
				{
					routedMailItem.TrackDeferRetryOrSuspended(SmtpResponse.QueueSuspended, state.AckDetails);
					return;
				}
				if (state.QueueInRetry)
				{
					routedMailItem.TrackDeferRetryOrSuspended(this.LastError, state.AckDetails);
					return;
				}
				routedMailItem.TrackDeferIfRecipientsInRetry(this.LastError, state.AckDetails);
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000ADEE4 File Offset: 0x000AC0E4
		public void HandleLogDeferDelay(IQueueItem item, RoutedMessageQueue.DeferLoggingState state)
		{
			RoutedMailItem routedMailItem = item as RoutedMailItem;
			if (!routedMailItem.IsDeletedByAdmin && !routedMailItem.RetryDeferLogged && !routedMailItem.DelayDeferLogged && !routedMailItem.IsInactive && state.ScanStartTime.Ticks > routedMailItem.EnqueuedTime.Add(RoutedMessageQueue.DelayDeferLoggingInterval).Ticks)
			{
				routedMailItem.TrackDeferDelay(SmtpResponse.QueueLarge, state.AckDetails, base.ActiveCount.ToString());
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000ADF68 File Offset: 0x000AC168
		public RoutedMailItem GetNextMailItem(DeliveryPriority priority)
		{
			if (this.Suspended)
			{
				return null;
			}
			RoutedMailItem routedMailItem;
			if (base.ConditionManager != null)
			{
				routedMailItem = (RoutedMailItem)base.ConditionManager.DequeueNext(this, priority);
				if (routedMailItem != null)
				{
					routedMailItem.ThrottlingContext.AddMemoryCost(ByteQuantifiedSize.FromBytes((ulong)routedMailItem.GetCurrentMimeSize()));
				}
			}
			else
			{
				routedMailItem = (RoutedMailItem)((ILockableQueue)this).DequeueInternal(priority);
			}
			if (routedMailItem != null)
			{
				routedMailItem.InitializeDeliveryLatencyTracking();
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2865114429U, routedMailItem.Subject);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3938856253U, routedMailItem.Subject);
			}
			return routedMailItem;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000ADFF8 File Offset: 0x000AC1F8
		ILockableItem ILockableQueue.DequeueInternal(DeliveryPriority priority)
		{
			while (!this.Suspended && !base.IsResubmitting)
			{
				Interlocked.Increment(ref this.inFlightMessages);
				RoutedMailItem routedMailItem = (RoutedMailItem)this.Dequeue(priority);
				if (routedMailItem == null)
				{
					Interlocked.Decrement(ref this.inFlightMessages);
					return null;
				}
				PoisonMessage.Context = ((IQueueItem)routedMailItem).GetMessageContext(MessageProcessingSource.Queue);
				switch (routedMailItem.PrepareForDelivery())
				{
				case RoutedMailItem.PrepareForDeliveryResult.Deliver:
					return routedMailItem;
				case RoutedMailItem.PrepareForDeliveryResult.IgnoreDeleted:
					Interlocked.Decrement(ref this.inFlightMessages);
					break;
				case RoutedMailItem.PrepareForDeliveryResult.Requeue:
					this.Enqueue(routedMailItem);
					Interlocked.Decrement(ref this.inFlightMessages);
					break;
				default:
					throw new InvalidOperationException("Unexpected PrepareForDeliveryResult");
				}
			}
			return null;
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000AE09B File Offset: 0x000AC29B
		ILockableItem ILockableQueue.DequeueInternal()
		{
			throw new InvalidOperationException("RoutedMessageQueue does not support dequeue without priority");
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000AE0A7 File Offset: 0x000AC2A7
		void ILockableQueue.Lock(ILockableItem item, WaitCondition condition, string lockReason, int dehydrateThreshold)
		{
			base.Lock(item, condition, lockReason, dehydrateThreshold);
			Interlocked.Decrement(ref this.inFlightMessages);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
		internal void AckMessage(RoutedMailItem routedMailItem, Queue<AckStatusAndResponse> recipientResponses, AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails ackDetails, DeferReason resubmitDeferReason, TimeSpan? resubmitDeferInterval, TimeSpan? retryInterval, MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, IEnumerable<MailRecipient> readyRecipients, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			routedMailItem.FinalizeDeliveryLatencyTracking(deliveryComponent);
			DeliveryPriority priority = routedMailItem.Priority;
			RiskLevel riskLevel = routedMailItem.RiskLevel;
			WaitCondition currentCondition = routedMailItem.CurrentCondition;
			bool flag;
			routedMailItem.Ack(ackStatus, smtpResponse, recipientResponses, readyRecipients, messageTrackingSource, messageTrackingSourceContext, ackDetails, reportEndToEndLatencies, resubmitDeferReason, resubmitDeferInterval, retryInterval, remoteMta, shadowed, primaryServer, false, out flag);
			if (flag)
			{
				LatencyTracker.BeginTrackLatency(LatencyTracker.GetDeliveryQueueLatencyComponent(routedMailItem.DeliveryType), routedMailItem.LatencyTracker);
				this.Enqueue(routedMailItem);
			}
			else
			{
				Components.QueueManager.UpateInstanceCounter(riskLevel, priority, delegate(QueuingPerfCountersInstance c)
				{
					c.ItemsCompletedDeliveryTotal.Increment();
				});
			}
			if (ackStatus == AckStatus.Success)
			{
				this.LastDeliveryTime = DateTime.UtcNow.Ticks;
				this.ResetConnectionRetryCount();
				base.LastTransientError = null;
				this.firstRetryTime = DateTime.MinValue;
			}
			Interlocked.Decrement(ref this.inFlightMessages);
			if (base.ConditionManager != null && currentCondition != null)
			{
				base.ConditionManager.MessageCompleted(currentCondition, this.Key);
				routedMailItem.CurrentCondition = null;
			}
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000AE1C8 File Offset: 0x000AC3C8
		public void Retry(TimerCallback callBackDelegate, TimeSpan? interval, SmtpResponse response, AckDetails ackDetails)
		{
			lock (this.syncObject)
			{
				if (this.ActiveConnections == 0 && !this.Suspended)
				{
					if (this.key.NextHopType == NextHopType.Heartbeat)
					{
						bool flag2;
						Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyHeartbeatRetry(this.key, out flag2);
						if (flag2)
						{
							this.AbortHeartbeat();
							return;
						}
					}
					if (this.firstRetryTime == DateTime.MinValue)
					{
						this.firstRetryTime = DateTime.UtcNow;
					}
					if (this.ShouldResubmitQueueDueToOutboundConnectorChange())
					{
						this.Resubmit(ResubmitReason.OutboundConnectorChange, null);
					}
					else
					{
						this.RelockAllItems("Queue in retry");
						this.SetScheduledCallback(callBackDelegate, interval);
						this.LastError = response;
						if (response.SmtpResponseType == SmtpResponseType.TransientError && ackDetails != null)
						{
							base.LastTransientError = new LastError(ackDetails.RemoteHostName, ackDetails.RemoteEndPoint, new DateTime?(this.LastRetryTime), response);
						}
					}
				}
			}
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000AE2D4 File Offset: 0x000AC4D4
		private bool ShouldResubmitQueueDueToOutboundConnectorChange()
		{
			if (this.orgId != null && !string.IsNullOrEmpty(this.key.OverrideSource) && this.key.OverrideSource.StartsWith("Connector"))
			{
				long num = Math.Max(this.firstRetryTime.Ticks, base.LastResubmitTime);
				if (DateTime.UtcNow.Ticks - num > Components.Configuration.AppConfig.RemoteDelivery.ResubmitDueToOutboundConnectorChangeInterval.Ticks)
				{
					try
					{
						PerTenantOutboundConnectors perTenantOutboundConnectors;
						if (Components.Configuration.TryGetTenantOutboundConnectors(this.orgId, out perTenantOutboundConnectors))
						{
							TimeSpan timeSpan = Components.Configuration.AppConfig.PerTenantCache.OutboundConnectorsCacheExpirationInterval + Components.Configuration.AppConfig.RemoteDelivery.OutboundConnectorLookbackBufferInterval;
							foreach (TenantOutboundConnector tenantOutboundConnector in perTenantOutboundConnectors.TenantOutboundConnectors)
							{
								if (tenantOutboundConnector.WhenChangedUTC != null && tenantOutboundConnector.WhenChangedUTC.Value.Ticks > num - timeSpan.Ticks)
								{
									return true;
								}
							}
							return perTenantOutboundConnectors.TenantOutboundConnectors.Length == 0;
						}
					}
					catch (TenantOutboundConnectorsRetrievalException ex)
					{
						Exception exception = ex.Result.Exception;
						QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RetryQueueOutboundConnectorLookupFailed, this.key.OverrideSource, new object[]
						{
							this.GetQueueName(),
							(exception != null) ? exception.Message : "<NULL>",
							(exception != null) ? exception.StackTrace : "<NULL>"
						});
						return false;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000AE4A4 File Offset: 0x000AC6A4
		public bool EvaluateResubmitDueToConfigUpdate(TimerCallback callBackDelegate)
		{
			if (this.key.NextHopType == NextHopType.Heartbeat)
			{
				bool flag;
				Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyHeartbeatConfigChanged(this.key, out flag);
				if (flag)
				{
					this.AbortHeartbeat();
				}
				else
				{
					this.SetScheduledCallback(callBackDelegate, null);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000AE4FD File Offset: 0x000AC6FD
		public override int Resubmit(ResubmitReason resubmitReason, Action<TransportMailItem> updateBeforeResubmit = null)
		{
			return this.ResubmitAsync(resubmitReason, updateBeforeResubmit).Result;
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000AE918 File Offset: 0x000ACB18
		public async Task<int> ResubmitAsync(ResubmitReason resubmitReason, Action<TransportMailItem> updateBeforeResubmit = null)
		{
			DateTime endTime = DateTime.UtcNow;
			bool firstResubmit = true;
			int resubmitCount = 0;
			int result;
			if (!this.resubmittingLock.Wait(0))
			{
				result = 0;
			}
			else
			{
				try
				{
					await Task.Run(async delegate()
					{
						while (!this.Suspended)
						{
							resubmitCount += this.<>n__FabricatedMethode(resubmitReason, updateBeforeResubmit);
							if (firstResubmit)
							{
								firstResubmit = false;
								endTime = DateTime.UtcNow + RoutedMessageQueue.QueueResubmitRetryTimeout;
							}
							if (!(DateTime.UtcNow < endTime) || this.InFlightMessages <= 0)
							{
								return;
							}
							await Task.Delay(RoutedMessageQueue.QueueResubmitRetryInterval);
						}
						ExTraceGlobals.QueuingTracer.TraceDebug<long, ResubmitReason>((long)this.GetHashCode(), "A resubmit request for the queue {0} due to reason '{1}' was not performed because the queue is frozen.", this.Id, resubmitReason);
					});
				}
				finally
				{
					this.resubmittingLock.Release();
				}
				this.LogResubmitEvent(resubmitReason, resubmitCount);
				result = resubmitCount;
			}
			return result;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000AE970 File Offset: 0x000ACB70
		public int CreateAttemptingConnection(DeliveryPriority priority, out long connectionId)
		{
			connectionId = Interlocked.Increment(ref this.nextConnectionId);
			int totalConnectionsCount;
			lock (this.syncObject)
			{
				this.attemptingConnections[(int)priority].Add(connectionId);
				totalConnectionsCount = this.GetTotalConnectionsCount(priority);
			}
			return totalConnectionsCount;
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000AE9D0 File Offset: 0x000ACBD0
		public void ConnectionAttemptSucceeded(DeliveryPriority priority, long connectionId)
		{
			lock (this.syncObject)
			{
				if (!this.attemptingConnections[(int)priority].Contains(connectionId))
				{
					throw new InvalidOperationException("Next Hop Connection Id: {0} with priority: {1} does not exist in attempting connections collection.");
				}
				this.attemptingConnections[(int)priority].Remove(connectionId);
				this.activeConnections[(int)priority]++;
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000AEA50 File Offset: 0x000ACC50
		public int CloseConnection(DeliveryPriority priority, long connectionId)
		{
			int totalConnectionsCount;
			lock (this.syncObject)
			{
				if (this.attemptingConnections[(int)priority].Contains(connectionId))
				{
					this.attemptingConnections[(int)priority].Remove(connectionId);
				}
				else
				{
					if (this.activeConnections[(int)priority] <= 0)
					{
						throw new InvalidOperationException("The active connection count is zero, while trying to close the connection.");
					}
					this.activeConnections[(int)priority]--;
				}
				totalConnectionsCount = this.GetTotalConnectionsCount(priority);
			}
			return totalConnectionsCount;
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000AEAE8 File Offset: 0x000ACCE8
		private int GetTotalConnectionsCount(DeliveryPriority priority)
		{
			return this.activeConnections[(int)priority] + this.attemptingConnections[(int)priority].Count;
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000AEB00 File Offset: 0x000ACD00
		public void ResetScheduledCallback()
		{
			RoutedMessageQueue.RetryTimerInfo retryTimerInfo = (RoutedMessageQueue.RetryTimerInfo)Interlocked.Exchange(ref this.retryTimerInfo, null);
			if (retryTimerInfo != null)
			{
				retryTimerInfo.RetryTimer.Dispose();
			}
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000AEB30 File Offset: 0x000ACD30
		public void SetScheduledCallback(TimerCallback callBackDelegate, TimeSpan? interval)
		{
			if (this.retryTimerInfo != null)
			{
				return;
			}
			int num = this.connectionRetryCount + 1;
			bool flag = false;
			TimeSpan timeSpan;
			if (interval != null)
			{
				timeSpan = interval.Value;
				if (num > Components.Configuration.LocalServer.TransportServer.TransientFailureRetryCount)
				{
					flag = true;
				}
			}
			else if (this.key.NextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox)
			{
				timeSpan = Components.Configuration.AppConfig.RemoteDelivery.MailboxDeliveryQueueRetryInterval;
				if (num > Components.Configuration.LocalServer.TransportServer.TransientFailureRetryCount)
				{
					flag = true;
				}
			}
			else if (this.key.NextHopType == NextHopType.Heartbeat)
			{
				timeSpan = Components.ShadowRedundancyComponent.ShadowRedundancyManager.Configuration.HeartbeatFrequency;
			}
			else if (num <= Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryCount)
			{
				timeSpan = Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryInterval;
			}
			else if (num <= Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryCount + Components.Configuration.LocalServer.TransportServer.TransientFailureRetryCount)
			{
				timeSpan = Components.Configuration.LocalServer.TransportServer.TransientFailureRetryInterval;
			}
			else
			{
				timeSpan = Components.Configuration.LocalServer.TransportServer.OutboundConnectionFailureRetryInterval;
				flag = true;
			}
			RoutedMessageQueue.RetryTimerInfo retryTimerInfo = new RoutedMessageQueue.RetryTimerInfo(new Timer(callBackDelegate, this, timeSpan, TimeSpan.Zero), DateTime.UtcNow + timeSpan);
			if (Interlocked.CompareExchange(ref this.retryTimerInfo, retryTimerInfo, null) != null)
			{
				retryTimerInfo.RetryTimer.Dispose();
				return;
			}
			if (flag)
			{
				base.AttemptToGenerateDelayDSNAndDehydrateAll();
			}
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000AECD6 File Offset: 0x000ACED6
		public int IncrementConnectionRetryCount()
		{
			return Interlocked.Increment(ref this.connectionRetryCount);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000AECE3 File Offset: 0x000ACEE3
		public void ResetConnectionRetryCount()
		{
			Interlocked.Exchange(ref this.connectionRetryCount, 0);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000AECF8 File Offset: 0x000ACEF8
		internal void GetQueueCounts(out int[] activeCount, out int[] retryCount)
		{
			lock (this.syncObject)
			{
				IEnumerable<int> instanceCounterIndex = QueueManager.GetInstanceCounterIndex(RiskLevel.Normal, DeliveryPriority.Normal);
				if (this.RetryConnectionScheduled || (this.ActiveConnections == 0 && this.AttemptingConnections != 0))
				{
					activeCount = new int[QueueManager.InstanceCountersLength];
					retryCount = base.ActiveMessageCounts.Zip(base.DeferredMessageCounts, (int a, int b) => a + b).ToArray<int>();
					using (IEnumerator<int> enumerator = instanceCounterIndex.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							int num = enumerator.Current;
							retryCount[num] += this.inFlightMessages;
						}
						goto IL_119;
					}
				}
				activeCount = base.ActiveMessageCounts.ToArray<int>();
				foreach (int num2 in instanceCounterIndex)
				{
					activeCount[num2] += this.inFlightMessages;
				}
				retryCount = base.DeferredMessageCounts.ToArray<int>();
				IL_119:;
			}
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000AEE78 File Offset: 0x000AD078
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000AEE88 File Offset: 0x000AD088
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.resubmittingLock != null)
			{
				this.resubmittingLock.Dispose();
				this.resubmittingLock = null;
			}
			this.disposed = true;
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000AEEDC File Offset: 0x000AD0DC
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RoutedMessageQueue>(this);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000AEEF9 File Offset: 0x000AD0F9
		internal void GetQueueCountsOnlyForIndividualPriorities(out int[] activeCount, out int[] retryCount)
		{
			this.GetQueueCounts(out activeCount, out retryCount);
			activeCount = activeCount.Take(QueueManager.PriorityToInstanceIndexMap.Count).ToArray<int>();
			retryCount = retryCount.Take(QueueManager.PriorityToInstanceIndexMap.Count).ToArray<int>();
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000AEF33 File Offset: 0x000AD133
		private RoutedMessageQueue(RoutedQueueBase queueStorage, PriorityBehaviour priorityBehaviour) : this(queueStorage, priorityBehaviour, null, null)
		{
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000AEF40 File Offset: 0x000AD140
		private RoutedMessageQueue(RoutedQueueBase queueStorage, PriorityBehaviour priorityBehaviour, MultiQueueWaitConditionManager conditionManager, OrganizationId orgId)
		{
			this.activeConnections = new int[3];
			this.attemptingConnections = new HashSet<long>[]
			{
				new HashSet<long>(),
				new HashSet<long>(),
				new HashSet<long>()
			};
			this.syncObject = new object();
			this.lastError = SmtpResponse.Empty;
			this.lastRetryTime = DateTime.MinValue.Ticks;
			this.lastDeliveryTime = DateTime.MinValue.Ticks;
			this.lastDeferEventLogTime = DateTime.MinValue;
			this.firstRetryTime = DateTime.MinValue;
			this.resubmittingLock = new SemaphoreSlim(1);
			base..ctor(queueStorage, priorityBehaviour, conditionManager);
			this.orgId = orgId;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000AEFFC File Offset: 0x000AD1FC
		protected RoutedMessageQueue(PriorityBehaviour priorityBehavior)
		{
			this.activeConnections = new int[3];
			this.attemptingConnections = new HashSet<long>[]
			{
				new HashSet<long>(),
				new HashSet<long>(),
				new HashSet<long>()
			};
			this.syncObject = new object();
			this.lastError = SmtpResponse.Empty;
			this.lastRetryTime = DateTime.MinValue.Ticks;
			this.lastDeliveryTime = DateTime.MinValue.Ticks;
			this.lastDeferEventLogTime = DateTime.MinValue;
			this.firstRetryTime = DateTime.MinValue;
			this.resubmittingLock = new SemaphoreSlim(1);
			base..ctor(priorityBehavior);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000AF0AC File Offset: 0x000AD2AC
		private static PriorityBehaviour NewQueueBehaviour(DeliveryType deliveryType)
		{
			PriorityBehaviour result = PriorityBehaviour.IgnorePriority;
			if (Components.IsBridgehead)
			{
				if (Components.Configuration.AppConfig.RemoteDelivery.LocalDeliveryPriorityQueuingEnabled && deliveryType == DeliveryType.MapiDelivery)
				{
					result = PriorityBehaviour.QueuePriority;
				}
				else if (deliveryType != DeliveryType.MapiDelivery && deliveryType != DeliveryType.NonSmtpGatewayDelivery && deliveryType != DeliveryType.DeliveryAgent)
				{
					if (Components.Configuration.AppConfig.RemoteDelivery.PriorityQueuingEnabled)
					{
						result = PriorityBehaviour.Fixed;
					}
					else if (Components.Configuration.AppConfig.RemoteDelivery.RemoteDeliveryPriorityQueuingEnabled)
					{
						result = PriorityBehaviour.QueuePriority;
					}
				}
			}
			return result;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000AF120 File Offset: 0x000AD320
		public static RoutedMessageQueue NewQueue(NextHopSolutionKey key)
		{
			return RoutedMessageQueue.NewQueue(key, null, null);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000AF12C File Offset: 0x000AD32C
		public static RoutedMessageQueue NewQueue(NextHopSolutionKey key, MultiQueueWaitConditionManager conditionManager, OrganizationId orgId)
		{
			RoutedQueueBase queueStorage = Components.MessagingDatabase.CreateQueue(key, false);
			PriorityBehaviour priorityBehaviour = RoutedMessageQueue.NewQueueBehaviour(key.NextHopType.DeliveryType);
			return new RoutedMessageQueue(queueStorage, priorityBehaviour, conditionManager, orgId)
			{
				key = key
			};
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000AF170 File Offset: 0x000AD370
		public static RoutedMessageQueue LoadQueue(RoutedQueueBase queueStorage, MultiQueueWaitConditionManager conditionManager)
		{
			if (queueStorage.NextHopType == NextHopType.Heartbeat)
			{
				RoutedMessageQueue.Delete(queueStorage);
				return null;
			}
			PriorityBehaviour priorityBehaviour = RoutedMessageQueue.NewQueueBehaviour(queueStorage.NextHopType.DeliveryType);
			RoutedMessageQueue routedMessageQueue = new RoutedMessageQueue(queueStorage, priorityBehaviour, conditionManager, null);
			routedMessageQueue.ComputeKey();
			return routedMessageQueue;
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000AF1BC File Offset: 0x000AD3BC
		public override void Delete()
		{
			RoutedMessageQueue.Delete(this.queueStorage);
			base.Delete();
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000AF1CF File Offset: 0x000AD3CF
		public void AddReference()
		{
			Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000AF1DD File Offset: 0x000AD3DD
		public void ReleaseReference()
		{
			Interlocked.Decrement(ref this.referenceCount);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000AF1EB File Offset: 0x000AD3EB
		private static void Delete(RoutedQueueBase queueStorage)
		{
			queueStorage.MarkToDelete();
			queueStorage.Commit();
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000AF1FC File Offset: 0x000AD3FC
		protected override bool ItemDeferred(IQueueItem item)
		{
			RoutedMailItem mailItem = (RoutedMailItem)item;
			Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemDeferred(mailItem, this, item.DeferUntil);
			return base.ItemDeferred(item);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000AF240 File Offset: 0x000AD440
		private void AbortHeartbeat()
		{
			ICollection<IQueueItem> collection = base.DequeueAll((IQueueItem item) => true, false);
			foreach (IQueueItem queueItem in collection)
			{
				RoutedMailItem routedMailItem = (RoutedMailItem)queueItem;
				routedMailItem.AbortHeartbeat();
				Components.QueueManager.UpateInstanceCounter(routedMailItem.RiskLevel, routedMailItem.Priority, delegate(QueuingPerfCountersInstance c)
				{
					c.ItemsCompletedDeliveryTotal.Increment();
				});
			}
			ExTraceGlobals.QueuingTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat aborted for queue {0}.", this.key);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000AF300 File Offset: 0x000AD500
		private void ComputeKey()
		{
			this.key = new NextHopSolutionKey(this.queueStorage.NextHopType, this.queueStorage.NextHopDomain, this.queueStorage.NextHopConnector, this.queueStorage.NextHopTlsDomain);
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000AF35C File Offset: 0x000AD55C
		private void RelockAllItems(string lockReason)
		{
			base.RelockAll(lockReason, delegate(IQueueItem item)
			{
				RoutedMailItem routedMailItem = (RoutedMailItem)item;
				return routedMailItem.AccessToken != null;
			});
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000AF382 File Offset: 0x000AD582
		private string GetQueueName()
		{
			return string.Format("'{0}':'{1}':'{2}'", this.key.NextHopType, this.key.NextHopDomain, this.key.NextHopConnector);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000AF3BC File Offset: 0x000AD5BC
		private void LogResubmitEvent(ResubmitReason reason, int resubmitCount)
		{
			if (resubmitCount <= 0 || reason == ResubmitReason.Admin)
			{
				return;
			}
			string queueName = this.GetQueueName();
			if (reason == ResubmitReason.ConfigUpdate)
			{
				QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResubmitDueToConfigUpdate, null, new object[]
				{
					resubmitCount,
					queueName
				});
				return;
			}
			if (reason == ResubmitReason.UnreachableSameVersionHubs)
			{
				QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResubmitDueToUnavailabilityOfSameVersionHubs, null, new object[]
				{
					resubmitCount,
					queueName
				});
				return;
			}
			if (reason != ResubmitReason.OutboundConnectorChange)
			{
				QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResubmitDueToInactivityTimeout, null, new object[]
				{
					resubmitCount,
					queueName,
					Components.Configuration.AppConfig.RemoteDelivery.MaxIdleTimeBeforeResubmit
				});
				return;
			}
			QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_ResubmitDueToOutboundConnectorChange, null, new object[]
			{
				resubmitCount,
				queueName
			});
		}

		// Token: 0x040015E2 RID: 5602
		private static readonly TimeSpan RetryDeferLoggingInterval = Components.TransportAppConfig.QueueConfiguration.MinLargeQueueDeferEventInterval;

		// Token: 0x040015E3 RID: 5603
		private static readonly bool RetryDeferLoggingEnabled = RoutedMessageQueue.RetryDeferLoggingInterval.CompareTo(TimeSpan.Zero) > 0;

		// Token: 0x040015E4 RID: 5604
		private static readonly TimeSpan DelayDeferLoggingInterval = Components.TransportAppConfig.QueueConfiguration.MinQueueRetryOrSuspendDeferEventInterval;

		// Token: 0x040015E5 RID: 5605
		private static readonly bool DelayDeferLoggingEnabled = RoutedMessageQueue.DelayDeferLoggingInterval.CompareTo(TimeSpan.Zero) > 0;

		// Token: 0x040015E6 RID: 5606
		private static readonly TimeSpan QueueResubmitRetryTimeout = Components.TransportAppConfig.QueueConfiguration.QueueResubmitRetryTimeout;

		// Token: 0x040015E7 RID: 5607
		private static readonly TimeSpan QueueResubmitRetryInterval = Components.TransportAppConfig.QueueConfiguration.QueueResubmitRetryInterval;

		// Token: 0x040015E8 RID: 5608
		private NextHopSolutionKey key;

		// Token: 0x040015E9 RID: 5609
		private long nextConnectionId;

		// Token: 0x040015EA RID: 5610
		private int[] activeConnections;

		// Token: 0x040015EB RID: 5611
		private HashSet<long>[] attemptingConnections;

		// Token: 0x040015EC RID: 5612
		private object syncObject;

		// Token: 0x040015ED RID: 5613
		private int referenceCount;

		// Token: 0x040015EE RID: 5614
		private int inFlightMessages;

		// Token: 0x040015EF RID: 5615
		private object retryTimerInfo;

		// Token: 0x040015F0 RID: 5616
		private int connectionRetryCount;

		// Token: 0x040015F1 RID: 5617
		private SmtpResponse lastError;

		// Token: 0x040015F2 RID: 5618
		private long lastRetryTime;

		// Token: 0x040015F3 RID: 5619
		private long lastDeliveryTime;

		// Token: 0x040015F4 RID: 5620
		private DateTime lastDeferEventLogTime;

		// Token: 0x040015F5 RID: 5621
		private DateTime firstRetryTime;

		// Token: 0x040015F6 RID: 5622
		private OrganizationId orgId;

		// Token: 0x040015F7 RID: 5623
		private SemaphoreSlim resubmittingLock;

		// Token: 0x040015F8 RID: 5624
		private bool disposed;

		// Token: 0x040015F9 RID: 5625
		private DisposeTracker disposeTracker;

		// Token: 0x020003BB RID: 955
		private class RetryTimerInfo
		{
			// Token: 0x06002BD5 RID: 11221 RVA: 0x000AF53D File Offset: 0x000AD73D
			public RetryTimerInfo(Timer retryTimer, DateTime nextRetryTime)
			{
				this.RetryTimer = retryTimer;
				this.NextRetryTime = nextRetryTime;
			}

			// Token: 0x04001601 RID: 5633
			public Timer RetryTimer;

			// Token: 0x04001602 RID: 5634
			public DateTime NextRetryTime;
		}

		// Token: 0x020003BC RID: 956
		internal struct DeferLoggingState
		{
			// Token: 0x06002BD6 RID: 11222 RVA: 0x000AF553 File Offset: 0x000AD753
			public DeferLoggingState(DateTime scanStartTime, bool queueInRetry, bool queueSuspended, AckDetails ackDetails)
			{
				this.ScanStartTime = scanStartTime;
				this.QueueInRetry = queueInRetry;
				this.QueueSuspended = queueSuspended;
				this.AckDetails = ackDetails;
			}

			// Token: 0x04001603 RID: 5635
			public readonly DateTime ScanStartTime;

			// Token: 0x04001604 RID: 5636
			public readonly bool QueueInRetry;

			// Token: 0x04001605 RID: 5637
			public readonly bool QueueSuspended;

			// Token: 0x04001606 RID: 5638
			public readonly AckDetails AckDetails;
		}
	}
}
