using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000377 RID: 887
	internal sealed class ShadowMessageQueue : IQueueVisitor
	{
		// Token: 0x06002648 RID: 9800 RVA: 0x0009479C File Offset: 0x0009299C
		public ShadowMessageQueue(RoutedQueueBase queueStorage, NextHopSolutionKey key, ShadowMessageQueue.ItemExpiredHandler itemExpiredHandler, IShadowRedundancyConfigurationSource configuration, ShouldSuppressResubmission shouldSuppressResubmission, ShadowRedundancyEventLogger shadowRedundancyEventLogger, FindRelatedBridgeHeads findRelatedBridgeHeads, GetRoutedMessageQueueStatus getRoutedMessageQueueStatus)
		{
			if (itemExpiredHandler == null)
			{
				throw new ArgumentNullException("itemExpiredHandler");
			}
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			if (shouldSuppressResubmission == null)
			{
				throw new ArgumentNullException("shouldSuppressResubmission");
			}
			if (shadowRedundancyEventLogger == null)
			{
				throw new ArgumentNullException("shadowRedundancyEventLogger");
			}
			if (!string.Equals(queueStorage.NextHopDomain, key.NextHopDomain, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "queueStorage.NextHopDomain = '{0}' and key.NextHopDomain = '{1}' where they should be consistent.", new object[]
				{
					queueStorage.NextHopDomain,
					key.NextHopDomain
				}));
			}
			if (findRelatedBridgeHeads == null)
			{
				this.findRelatedBridgeHeads = new FindRelatedBridgeHeads(ShadowMessageQueue.GetRelatedBridgeHeads);
			}
			else
			{
				this.findRelatedBridgeHeads = findRelatedBridgeHeads;
			}
			if (getRoutedMessageQueueStatus == null)
			{
				this.getRoutedMessageQueueStatus = new GetRoutedMessageQueueStatus(ShadowMessageQueue.GetRoutedMessageQueueStatus);
			}
			else
			{
				this.getRoutedMessageQueueStatus = getRoutedMessageQueueStatus;
			}
			this.queueStorage = queueStorage;
			this.itemExpiredHandler = itemExpiredHandler;
			this.key = key;
			this.configuration = configuration;
			this.shouldSuppressResubmission = shouldSuppressResubmission;
			this.shadowRedundancyEventLogger = shadowRedundancyEventLogger;
			if (ShadowMessageQueue.heartbeatMonitoringInterval > this.configuration.HeartbeatFrequency)
			{
				ShadowMessageQueue.heartbeatMonitoringInterval = this.configuration.HeartbeatFrequency;
			}
			this.heartbeatHelper = new ShadowRedundancyHeartbeatHelper(key, configuration, shadowRedundancyEventLogger);
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x000948FF File Offset: 0x00092AFF
		public long Id
		{
			get
			{
				return this.queueStorage.Id;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x0009490C File Offset: 0x00092B0C
		public NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x0600264B RID: 9803 RVA: 0x00094914 File Offset: 0x00092B14
		public string NextHopDomain
		{
			get
			{
				return this.key.NextHopDomain;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x00094921 File Offset: 0x00092B21
		public bool IsEmpty
		{
			get
			{
				return this.Count == 0;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600264D RID: 9805 RVA: 0x0009492C File Offset: 0x00092B2C
		// (set) Token: 0x0600264E RID: 9806 RVA: 0x00094939 File Offset: 0x00092B39
		public bool Suspended
		{
			get
			{
				return this.queueStorage.Suspended;
			}
			set
			{
				this.queueStorage.Suspended = value;
				this.queueStorage.Commit();
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600264F RID: 9807 RVA: 0x00094952 File Offset: 0x00092B52
		public bool HasHeartbeatFailure
		{
			get
			{
				return this.heartbeatHelper.HasHeartbeatFailure;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x0009495F File Offset: 0x00092B5F
		public bool IsResubmissionSuppressed
		{
			get
			{
				return this.suppressed;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002651 RID: 9809 RVA: 0x00094967 File Offset: 0x00092B67
		public int Count
		{
			get
			{
				return this.shadowMailItems.Count;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00094974 File Offset: 0x00092B74
		public DateTime LastHeartbeatTime
		{
			get
			{
				return this.heartbeatHelper.LastHeartbeatTime;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002653 RID: 9811 RVA: 0x00094981 File Offset: 0x00092B81
		public DateTime LastExpiryCheck
		{
			get
			{
				return this.lastExpiryCheck;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00094989 File Offset: 0x00092B89
		public long IgnoredDiscardIdCount
		{
			get
			{
				return this.ignoredDiscardIdCount;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002655 RID: 9813 RVA: 0x00094991 File Offset: 0x00092B91
		public long ValidDiscardIdCount
		{
			get
			{
				return this.validDiscardIdCount;
			}
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0009499C File Offset: 0x00092B9C
		public static ShadowMessageQueue NewQueue(NextHopSolutionKey key, ShadowMessageQueue.ItemExpiredHandler itemExpiredHandler, IShadowRedundancyConfigurationSource configuration, ShouldSuppressResubmission shouldSuppressResubmission, ShadowRedundancyEventLogger shadowRedundancyEventLogger, FindRelatedBridgeHeads findRelatedBridgeHeads, GetRoutedMessageQueueStatus getRoutedMessageQueueStatus)
		{
			RoutedQueueBase routedQueueBase = Components.MessagingDatabase.CreateQueue(key, false);
			return new ShadowMessageQueue(routedQueueBase, key, itemExpiredHandler, configuration, shouldSuppressResubmission, shadowRedundancyEventLogger, findRelatedBridgeHeads, getRoutedMessageQueueStatus);
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000949C8 File Offset: 0x00092BC8
		public static void EnsureValidResubmitReason(ResubmitReason resubmitReason)
		{
			if (resubmitReason != ResubmitReason.Admin && resubmitReason != ResubmitReason.ShadowHeartbeatFailure && resubmitReason != ResubmitReason.ShadowStateChange)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Resubmit reason '{0}' is not applicable to Shadow Queues.", new object[]
				{
					resubmitReason
				}));
			}
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00094A08 File Offset: 0x00092C08
		public void Enqueue(ShadowMailItem shadowMailItem)
		{
			if (shadowMailItem == null)
			{
				throw new ArgumentNullException("shadowMailItem");
			}
			lock (this.shadowMailItems)
			{
				this.shadowMailItems[shadowMailItem.TransportMailItem.ShadowMessageId] = shadowMailItem;
			}
			ShadowRedundancyManager.PerfCounters.UpdateShadowQueueLength(this.key.NextHopDomain, 1);
			this.lastActivityTime = DateTime.UtcNow;
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00094A88 File Offset: 0x00092C88
		public ShadowMailItem FindShadowMailItem(Guid shadowMessageId)
		{
			if (shadowMessageId == Guid.Empty)
			{
				throw new ArgumentException("shadowMessageId can't be Guid.Empty.");
			}
			ShadowMailItem shadowMailItem = null;
			ShadowMailItem result;
			lock (this.shadowMailItems)
			{
				if (this.shadowMailItems.TryGetValue(shadowMessageId, out shadowMailItem))
				{
					result = shadowMailItem;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00094AF4 File Offset: 0x00092CF4
		public void ForEach(Action<IQueueItem> action, bool includeDeferred)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			lock (this.shadowMailItems)
			{
				foreach (KeyValuePair<Guid, ShadowMailItem> keyValuePair in this.shadowMailItems)
				{
					action(keyValuePair.Value.TransportMailItem);
				}
			}
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00094B8C File Offset: 0x00092D8C
		public bool Discard(Guid shadowMessageId, DiscardReason discardReason)
		{
			if (shadowMessageId == Guid.Empty)
			{
				throw new ArgumentException("shadowMessageId can't be Guid.Empty.");
			}
			ShadowMailItem shadowMailItem = null;
			lock (this.shadowMailItems)
			{
				if (this.shadowMailItems.TryGetValue(shadowMessageId, out shadowMailItem))
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, Guid, DiscardReason>((long)this.GetHashCode(), "ShadowMessageQueue.Discard: queue {0} removing shadowMessageId={1} for reason={2}", this.key, shadowMessageId, discardReason);
					this.shadowMailItems.Remove(shadowMessageId);
					this.validDiscardIdCount += 1L;
				}
			}
			if (shadowMailItem == null)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, Guid>((long)this.GetHashCode(), "ShadowMessageQueue.Discard queue {0} could not find shadowMessageId={1}", this.key, shadowMessageId);
				this.ignoredDiscardIdCount += 1L;
				return false;
			}
			shadowMailItem.Discard(discardReason);
			this.itemExpiredHandler(this, shadowMailItem);
			ShadowRedundancyManager.PerfCounters.UpdateShadowQueueLength(this.key.NextHopDomain, -1);
			this.lastActivityTime = DateTime.UtcNow;
			return true;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00094C94 File Offset: 0x00092E94
		public void DiscardAll()
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Shadow queue '{0}' is being discarded", this.key);
			List<ShadowMailItem> list;
			lock (this.shadowMailItems)
			{
				list = new List<ShadowMailItem>(this.shadowMailItems.Values);
				this.shadowMailItems.Clear();
				this.lastActivityTime = DateTime.MinValue;
			}
			if (list.Count > 0)
			{
				foreach (ShadowMailItem shadowMailItem in list)
				{
					shadowMailItem.Discard(DiscardReason.DiscardAll);
				}
				this.NotifyExpiryHandlerAsync(list);
			}
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x00094D60 File Offset: 0x00092F60
		public bool CanBeDeleted(TimeSpan idleTime)
		{
			return this.referenceCount == 0 && this.IsEmpty && !this.Suspended && this.lastActivityTime + idleTime < DateTime.UtcNow;
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00094D92 File Offset: 0x00092F92
		public void Delete()
		{
			this.queueStorage.MarkToDelete();
			this.queueStorage.Commit();
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00094DAA File Offset: 0x00092FAA
		public void AddReference()
		{
			Interlocked.Increment(ref this.referenceCount);
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00094DB8 File Offset: 0x00092FB8
		public void ReleaseReference()
		{
			Interlocked.Decrement(ref this.referenceCount);
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x00094DC8 File Offset: 0x00092FC8
		public void UpdateQueue(bool heartbeatEnabled, bool shadowRedundancyPaused)
		{
			DateTime utcNow = DateTime.UtcNow;
			List<ShadowMailItem> list = null;
			bool flag = false;
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, bool>((long)this.GetHashCode(), "ShadowMessageQueue.UpdateQueue for queue {0} heartbeatEnabled={1}", this.key, heartbeatEnabled);
			lock (this.shadowMailItems)
			{
				if (utcNow - this.lastExpiryCheck >= this.configuration.ShadowQueueCheckExpiryInterval)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, DateTime>((long)this.GetHashCode(), "ShadowMessageQueue.UpdateQueue for queue {0} needs to check for expired items lastExpiryCheck={1}", this.key, this.lastExpiryCheck);
					foreach (ShadowMailItem shadowMailItem in this.shadowMailItems.Values)
					{
						if (utcNow >= ((IQueueItem)shadowMailItem).Expiry)
						{
							if (list == null)
							{
								list = new List<ShadowMailItem>();
							}
							if (shadowMailItem.DiscardReason == null)
							{
								shadowMailItem.Discard(DiscardReason.Expired);
							}
							list.Add(shadowMailItem);
						}
					}
					this.RemoveShadowMailItems(list);
					this.lastExpiryCheck = utcNow;
				}
				if (heartbeatEnabled)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, DateTime, DateTime>((long)this.GetHashCode(), "ShadowMessageQueue.UpdateQueue for queue {0} checking time now {1} against last heartbeat check at {2}", this.key, utcNow, this.lastHeartbeatCheck);
					if (utcNow - this.lastHeartbeatCheck >= ShadowMessageQueue.heartbeatMonitoringInterval)
					{
						this.lastHeartbeatCheck = utcNow;
						flag = true;
					}
				}
				else
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "ShadowMessageQueue.UpdateQueue for queue {0} skipping heartbeat because it was told to", this.key);
					this.suppressed = false;
					this.lastHeartbeatCheck = utcNow;
					this.heartbeatHelper.ResetHeartbeat();
				}
			}
			this.NotifyExpiryHandlerAsync(list);
			if (flag)
			{
				if (!this.ResubmitIfNecessary(shadowRedundancyPaused))
				{
					this.CreateHeartbeatIfNecessary();
					return;
				}
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "ShadowMessageQueue.UpdateQueue for queue {0} skipping heartbeat because ResubmitIfNecessary returned true", this.key);
			}
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x00094FC4 File Offset: 0x000931C4
		public int Resubmit(ResubmitReason resubmitReason)
		{
			this.suppressed = false;
			ShadowMessageQueue.EnsureValidResubmitReason(resubmitReason);
			QueueIdentity queueIdentity = new QueueIdentity(QueueType.Shadow, this.Id, this.NextHopDomain);
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<QueueIdentity, ResubmitReason>((long)this.GetHashCode(), "Resubmit request for the queue '{0}' due to reason '{1}'.", queueIdentity, resubmitReason);
			int result;
			try
			{
				if (Interlocked.Increment(ref this.resubmitGuard) != 1)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<QueueIdentity>((long)this.GetHashCode(), "Another thread is currently resubmitting for queue '{0}'.", queueIdentity);
					result = 0;
				}
				else if (this.Suspended)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<QueueIdentity, ResubmitReason>((long)this.GetHashCode(), "Resubmit request for the queue {0} due to reason '{1}' was not performed because the queue is frozen.", queueIdentity, resubmitReason);
					result = 0;
				}
				else
				{
					List<ShadowMailItem> list = null;
					lock (this.shadowMailItems)
					{
						foreach (ShadowMailItem shadowMailItem in this.shadowMailItems.Values)
						{
							if (shadowMailItem.NextHopSolution.AdminActionStatus != AdminActionStatus.Suspended)
							{
								if (list == null)
								{
									list = new List<ShadowMailItem>();
								}
								list.Add(shadowMailItem);
							}
						}
						this.RemoveShadowMailItems(list);
					}
					int num = 0;
					if (list != null)
					{
						List<TransportMailItem> list2 = new List<TransportMailItem>(list.Count);
						foreach (ShadowMailItem shadowMailItem2 in list)
						{
							list2.Add(shadowMailItem2.TransportMailItem);
						}
						ShadowRedundancyResubmitHelper shadowRedundancyResubmitHelper = new ShadowRedundancyResubmitHelper(resubmitReason, this.Key);
						shadowRedundancyResubmitHelper.Resubmit(list2);
						ShadowRedundancyManager.PerfCounters.SubmitMessagesFromShadowQueue(this.key.NextHopDomain, list2.Count);
						foreach (ShadowMailItem shadowMailItem3 in list)
						{
							shadowMailItem3.Discard(DiscardReason.Resubmitted);
						}
						this.NotifyExpiryHandler(list);
						num = list.Count;
					}
					this.lastActivityTime = DateTime.UtcNow;
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int, QueueIdentity>((long)this.GetHashCode(), "Resubmitted {0} items from queue '{1}'.", num, queueIdentity);
					if (num > 0)
					{
						this.shadowRedundancyEventLogger.LogShadowRedundancyMessagesResubmitted(num, this.NextHopDomain, resubmitReason);
					}
					result = num;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.resubmitGuard);
			}
			return result;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0009526C File Offset: 0x0009346C
		public void ScheduleImmediateHeartbeat()
		{
			QueueIdentity arg = new QueueIdentity(QueueType.Shadow, this.Id, this.NextHopDomain);
			ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Immediate heartbeat scheduled for queue {0}", arg);
			this.heartbeatHelper.ScheduleImmediateHeartbeat();
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000952AC File Offset: 0x000934AC
		public void CreateHeartbeatIfNecessary()
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, bool, bool>((long)this.GetHashCode(), "ShadowMessageQueue.CreateHeartbeatIfNecessary for queue {0}: IsEmpty={1} Suspended={2}", this.key, this.IsEmpty, this.Suspended);
			if (this.IsEmpty || this.Suspended)
			{
				this.suppressed = false;
				this.heartbeatHelper.ResetHeartbeat();
				return;
			}
			this.heartbeatHelper.CreateHeartbeatIfNecessary();
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0009530F File Offset: 0x0009350F
		public bool CanResubmit()
		{
			return this.heartbeatHelper.CanResubmit();
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0009531C File Offset: 0x0009351C
		public void UpdateHeartbeat(DateTime heartbeatTime, NextHopSolutionKey key, bool successfulHeartbeat)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowMessageQueue.UpdateHeartbeat for queue {0} heartbeatTime={1} key={2} successfulHeartbeat={3}", new object[]
			{
				this.key,
				heartbeatTime,
				key,
				successfulHeartbeat
			});
			if (this.IsEmpty || this.Suspended)
			{
				this.suppressed = false;
				this.heartbeatHelper.ResetHeartbeat();
				return;
			}
			this.heartbeatHelper.UpdateHeartbeat(heartbeatTime, key, successfulHeartbeat);
			if (successfulHeartbeat)
			{
				this.suppressed = false;
				return;
			}
			this.shadowRedundancyEventLogger.LogPrimaryServerHeartbeatFailed(this.NextHopDomain);
			ShadowRedundancyManager.PerfCounters.HeartbeatFailure(this.NextHopDomain);
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x000953CD File Offset: 0x000935CD
		public void EvaluateHeartbeatAttempt(out bool sendHeartbeat, out bool abortHeartbeat)
		{
			sendHeartbeat = false;
			abortHeartbeat = (this.IsEmpty || this.Suspended);
			if (abortHeartbeat)
			{
				this.suppressed = false;
				this.heartbeatHelper.ResetHeartbeat();
				return;
			}
			this.heartbeatHelper.EvaluateHeartbeatAttempt(out sendHeartbeat, out abortHeartbeat);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x0009540C File Offset: 0x0009360C
		public void NotifyHeartbeatConfigChanged(NextHopSolutionKey key, out bool abortHeartbeat)
		{
			abortHeartbeat = false;
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Heartbeat queue corresponding to shadow queue '{0}' has config change", this.key);
			abortHeartbeat = (this.IsEmpty || this.Suspended);
			if (abortHeartbeat)
			{
				this.suppressed = false;
				this.heartbeatHelper.ResetHeartbeat();
			}
			else
			{
				this.heartbeatHelper.UpdateHeartbeat(DateTime.UtcNow, key, false);
			}
			if (this.heartbeatHelper.CanResubmit())
			{
				this.DiscardAll();
				abortHeartbeat = true;
				this.suppressed = false;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00095492 File Offset: 0x00093692
		public void NotifyConfigUpdated(IShadowRedundancyConfigurationSource oldConfiguration)
		{
			if (oldConfiguration == null)
			{
				throw new ArgumentNullException("ShadowMessageConfigChange: oldConfiguration");
			}
			this.heartbeatHelper.NotifyConfigUpdated(oldConfiguration);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x000954B0 File Offset: 0x000936B0
		private static IEnumerable<INextHopServer> GetRelatedBridgeHeads(NextHopSolutionKey nextHopSolutionKey)
		{
			IEnumerable<INextHopServer> result;
			if (Components.RoutingComponent.MailRouter.TryGetRelatedServersForShadowQueue(nextHopSolutionKey, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600266B RID: 9835 RVA: 0x000954D4 File Offset: 0x000936D4
		private static QueueStatus GetRoutedMessageQueueStatus(NextHopSolutionKey key)
		{
			SmtpSendConnectorConfig smtpSendConnectorConfig;
			if (!Components.RoutingComponent.MailRouter.TryGetLocalSendConnector<SmtpSendConnectorConfig>(key.NextHopConnector, out smtpSendConnectorConfig) || smtpSendConnectorConfig.DNSRoutingEnabled)
			{
				return QueueStatus.None;
			}
			NextHopSolutionKey nextHopSolutionKey = new NextHopSolutionKey(DeliveryType.SmartHostConnectorDelivery, smtpSendConnectorConfig.SmartHostsString, key.NextHopConnector);
			RoutedMessageQueue queue = Components.RemoteDeliveryComponent.GetQueue(nextHopSolutionKey);
			if (queue == null)
			{
				return QueueStatus.None;
			}
			if (queue.Suspended)
			{
				return QueueStatus.Suspended;
			}
			if (queue.ActiveConnections > 0)
			{
				return QueueStatus.Active;
			}
			if (queue.AttemptingConnections > 0)
			{
				return QueueStatus.Connecting;
			}
			DateTime dateTime;
			if (queue.GetRetryConnectionSchedule(out dateTime))
			{
				return QueueStatus.Retry;
			}
			return QueueStatus.Ready;
		}

		// Token: 0x0600266C RID: 9836 RVA: 0x00095558 File Offset: 0x00093758
		private void RemoveShadowMailItems(IEnumerable<ShadowMailItem> shadowMailItemsToRemove)
		{
			if (shadowMailItemsToRemove != null)
			{
				foreach (ShadowMailItem shadowMailItem in shadowMailItemsToRemove)
				{
					this.shadowMailItems.Remove(shadowMailItem.TransportMailItem.ShadowMessageId);
				}
			}
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x000955B4 File Offset: 0x000937B4
		private bool ResubmitIfNecessary(bool shadowRedundancyPaused)
		{
			bool result = false;
			if (this.Suspended)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} was not resubmitted because it is frozen.", this.key);
			}
			else if (Components.RemoteDeliveryComponent.IsPaused)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} was not resubmitted because Remote Delivery is paused.", this.key);
			}
			else if (shadowRedundancyPaused)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey>((long)this.GetHashCode(), "Queue {0} was not resubmitted because Shadow Redundancy is paused.", this.key);
			}
			else if (this.CanResubmit())
			{
				QueueStatus queueStatus = this.getRoutedMessageQueueStatus(this.key);
				bool flag;
				if (queueStatus == QueueStatus.Ready || queueStatus == QueueStatus.Active)
				{
					flag = false;
				}
				else
				{
					IEnumerable<INextHopServer> relatedBridgeheads = this.findRelatedBridgeHeads(this.key);
					flag = this.shouldSuppressResubmission(relatedBridgeheads);
				}
				if (!flag)
				{
					this.Resubmit(ResubmitReason.ShadowHeartbeatFailure);
					result = true;
				}
				else if (!this.suppressed)
				{
					this.suppressed = true;
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<NextHopSolutionKey, string>((long)this.GetHashCode(), "Queue {0} was not resubmitted because resubmission for the {1} is suppressed.", this.key, this.key.NextHopDomain);
					this.shadowRedundancyEventLogger.LogShadowRedundancyMessageResubmitSuppressed(this.shadowMailItems.Count, this.NextHopDomain, Strings.ShadowRedundancyNoActiveServerInNexthopSolution);
				}
				this.heartbeatHelper.ResetHeartbeat();
			}
			return result;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x00095700 File Offset: 0x00093900
		private void NotifyExpiryHandlerCallback(object state)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			IEnumerable<ShadowMailItem> enumerable = state as IEnumerable<ShadowMailItem>;
			if (enumerable == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "State is of type '{0}' rather than 'IEnumerable<ShadowMailItem>'.", new object[]
				{
					state.GetType()
				}));
			}
			this.NotifyExpiryHandler(enumerable);
		}

		// Token: 0x0600266F RID: 9839 RVA: 0x00095752 File Offset: 0x00093952
		private void NotifyExpiryHandlerAsync(ICollection<ShadowMailItem> expiredItems)
		{
			if (expiredItems != null && expiredItems.Count > 0)
			{
				ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int>((long)this.GetHashCode(), "Scheduling a call to NotifyExpiryHandlerCallback() to expire '{0}' items.", expiredItems.Count);
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.NotifyExpiryHandlerCallback), expiredItems);
			}
		}

		// Token: 0x06002670 RID: 9840 RVA: 0x00095790 File Offset: 0x00093990
		private void NotifyExpiryHandler(IEnumerable<ShadowMailItem> expiredItems)
		{
			foreach (ShadowMailItem shadowMailItem in expiredItems)
			{
				this.itemExpiredHandler(this, shadowMailItem);
				ShadowRedundancyManager.PerfCounters.UpdateShadowQueueLength(this.key.NextHopDomain, -1);
			}
		}

		// Token: 0x040013A8 RID: 5032
		private static TimeSpan heartbeatMonitoringInterval = TimeSpan.FromSeconds(1.0);

		// Token: 0x040013A9 RID: 5033
		private Dictionary<Guid, ShadowMailItem> shadowMailItems = new Dictionary<Guid, ShadowMailItem>();

		// Token: 0x040013AA RID: 5034
		private NextHopSolutionKey key;

		// Token: 0x040013AB RID: 5035
		private DateTime lastActivityTime = DateTime.UtcNow;

		// Token: 0x040013AC RID: 5036
		private DateTime lastExpiryCheck = DateTime.MinValue;

		// Token: 0x040013AD RID: 5037
		private ShadowRedundancyHeartbeatHelper heartbeatHelper;

		// Token: 0x040013AE RID: 5038
		private DateTime lastHeartbeatCheck = DateTime.UtcNow;

		// Token: 0x040013AF RID: 5039
		private int referenceCount;

		// Token: 0x040013B0 RID: 5040
		private ShadowMessageQueue.ItemExpiredHandler itemExpiredHandler;

		// Token: 0x040013B1 RID: 5041
		private IShadowRedundancyConfigurationSource configuration;

		// Token: 0x040013B2 RID: 5042
		private RoutedQueueBase queueStorage;

		// Token: 0x040013B3 RID: 5043
		private int resubmitGuard;

		// Token: 0x040013B4 RID: 5044
		private ShouldSuppressResubmission shouldSuppressResubmission;

		// Token: 0x040013B5 RID: 5045
		private ShadowRedundancyEventLogger shadowRedundancyEventLogger;

		// Token: 0x040013B6 RID: 5046
		private FindRelatedBridgeHeads findRelatedBridgeHeads;

		// Token: 0x040013B7 RID: 5047
		private GetRoutedMessageQueueStatus getRoutedMessageQueueStatus;

		// Token: 0x040013B8 RID: 5048
		private bool suppressed;

		// Token: 0x040013B9 RID: 5049
		private long ignoredDiscardIdCount;

		// Token: 0x040013BA RID: 5050
		private long validDiscardIdCount;

		// Token: 0x02000378 RID: 888
		// (Invoke) Token: 0x06002673 RID: 9843
		internal delegate void ItemExpiredHandler(ShadowMessageQueue shadowMessageQueue, ShadowMailItem shadowMailItem);
	}
}
