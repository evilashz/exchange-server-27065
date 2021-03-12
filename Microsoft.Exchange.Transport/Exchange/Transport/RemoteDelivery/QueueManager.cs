using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.QueueViewer;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003A2 RID: 930
	internal sealed class QueueManager : ITransportComponent
	{
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x000A45FC File Offset: 0x000A27FC
		public static ExEventLog EventLogger
		{
			get
			{
				return QueueManager.eventLogger;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06002985 RID: 10629 RVA: 0x000A4603 File Offset: 0x000A2803
		public static int InstanceCountersLength
		{
			get
			{
				if (QueueManager.instanceCountersLength == -1)
				{
					QueueManager.instanceCountersLength = QueueManager.priorityBasedInstanceCounterNames.Length + (QueueManager.includeRiskBasedCounters ? QueueManager.riskBasedInstanceCounterNames.Length : 0);
				}
				return QueueManager.instanceCountersLength;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x000A4630 File Offset: 0x000A2830
		public PoisonMessageQueue PoisonMessageQueue
		{
			get
			{
				return this.poisonMessageQueue;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000A4638 File Offset: 0x000A2838
		public QueuingPerfCountersInstance PerfCountersTotal
		{
			get
			{
				return this.queuingPerfCountersTotalInstance;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x000A4640 File Offset: 0x000A2840
		internal static Dictionary<DeliveryPriority, int> PriorityToInstanceIndexMap
		{
			get
			{
				return QueueManager.priorityToInstanceIndexMap;
			}
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000A4648 File Offset: 0x000A2848
		public static void StartUpdateAllQueues()
		{
			TimeSpan timeSpan = DateTime.UtcNow - QueueManager.lastQueueUpdate;
			if (timeSpan > Components.Configuration.AppConfig.QueueConfiguration.MaxUpdateQueueBlockedInterval)
			{
				throw new QueueManager.QueueUpdateBlockedException("Queue is not updated for " + timeSpan);
			}
			if (!QueueManager.updateAllQueuesPendingOrBusy)
			{
				QueueManager.updateAllQueuesPendingOrBusy = true;
				ThreadPool.QueueUserWorkItem(new WaitCallback(QueueManager.UpdateAllQueuesCallback));
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000A46B8 File Offset: 0x000A28B8
		public static void UpdateAllQueuesCallback(object state)
		{
			if (Interlocked.CompareExchange(ref QueueManager.busyUpdateAllQueues, 1, 0) == 0)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3708169533U);
				try
				{
					QueueManager.lastQueueUpdate = DateTime.UtcNow;
					Components.CategorizerComponent.UpdateSubmitQueue();
					Components.RemoteDeliveryComponent.UpdateQueues();
					Components.ShadowRedundancyComponent.ShadowRedundancyManager.UpdateQueues();
				}
				finally
				{
					Interlocked.Exchange(ref QueueManager.busyUpdateAllQueues, 0);
					QueueManager.updateAllQueuesPendingOrBusy = false;
				}
			}
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000A4734 File Offset: 0x000A2934
		public static bool ShouldDehydrateMessage(RoutedMessageQueue routedMessageQueue, RoutedMailItem routedMailItem, out bool shouldAttemptConnection)
		{
			shouldAttemptConnection = true;
			if (routedMessageQueue.Suspended)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<long, Guid, string>(0L, "Message {0} may be committed and dehydrated because queue {1} ({2}) is frozen", routedMailItem.RecordId, routedMessageQueue.Key.NextHopConnector, routedMessageQueue.Key.NextHopDomain);
				shouldAttemptConnection = false;
				return true;
			}
			if (routedMessageQueue.RetryConnectionScheduled)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<long, Guid, string>(0L, "Message {0} may be committed and dehydrated because queue {1} ({2}) is in retry", routedMailItem.RecordId, routedMessageQueue.Key.NextHopConnector, routedMessageQueue.Key.NextHopDomain);
				shouldAttemptConnection = false;
				return true;
			}
			if (Components.ResourceManager.ShouldShrinkDownMemoryCaches)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<long>(0L, "Message {0} may be committed and dehydrated because of the high memory pressure.", routedMailItem.RecordId);
				shouldAttemptConnection = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000A47F0 File Offset: 0x000A29F0
		public static bool FreezeQueue(QueueIdentity queueIdentity)
		{
			if (!queueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (queueIdentity.Type == QueueType.Delivery)
			{
				lock (Components.RemoteDeliveryComponent.SyncQueues)
				{
					List<RoutedMessageQueue> list = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
					if (list.Count == 0)
					{
						return false;
					}
					if (list.Count > 1)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
					}
					RoutedMessageQueue routedMessageQueue = list[0];
					routedMessageQueue.Suspended = true;
					routedMessageQueue.ResetScheduledCallback();
					goto IL_EE;
				}
			}
			if (queueIdentity.Type == QueueType.Submission)
			{
				if (Components.MessageDepotComponent.Enabled)
				{
					Components.ProcessingSchedulerComponent.ProcessingSchedulerAdmin.Pause();
				}
				else
				{
					Components.CategorizerComponent.SubmitMessageQueue.Suspended = true;
				}
			}
			else if (queueIdentity.Type == QueueType.Unreachable)
			{
				RemoteDeliveryComponent.UnreachableMessageQueue.Suspended = true;
			}
			else
			{
				if (queueIdentity.Type != QueueType.Shadow)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				QueueManager.SuspendShadowQueue(queueIdentity);
			}
			IL_EE:
			ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Queue {0} frozen by the admin", queueIdentity);
			return true;
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000A4910 File Offset: 0x000A2B10
		public static bool UnfreezeQueue(QueueIdentity queueIdentity)
		{
			if (!queueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (queueIdentity.Type == QueueType.Delivery)
			{
				RoutedMessageQueue routedMessageQueue;
				lock (Components.RemoteDeliveryComponent.SyncQueues)
				{
					List<RoutedMessageQueue> list = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
					if (list.Count == 0)
					{
						return false;
					}
					if (list.Count > 1)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
					}
					routedMessageQueue = list[0];
					routedMessageQueue.Suspended = false;
				}
				Components.RemoteDeliveryComponent.ConnectionManager.CreateConnectionIfNecessary(routedMessageQueue);
			}
			else if (queueIdentity.Type == QueueType.Submission)
			{
				if (Components.MessageDepotComponent.Enabled)
				{
					Components.ProcessingSchedulerComponent.ProcessingSchedulerAdmin.Resume();
				}
				else
				{
					Components.CategorizerComponent.SubmitMessageQueue.Suspended = false;
				}
			}
			else if (queueIdentity.Type == QueueType.Unreachable)
			{
				RemoteDeliveryComponent.UnreachableMessageQueue.Suspended = false;
			}
			else
			{
				if (queueIdentity.Type != QueueType.Shadow)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				QueueManager.ResumeShadowQueue(queueIdentity);
			}
			ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Queue {0} unfrozen by the admin", queueIdentity);
			return true;
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000A4A3C File Offset: 0x000A2C3C
		public static bool RetryQueue(QueueIdentity queueIdentity, bool resubmit)
		{
			return QueueManager.RetryQueueAsync(queueIdentity, resubmit).Result;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000A4D50 File Offset: 0x000A2F50
		public static async Task<bool> RetryQueueAsync(QueueIdentity queueIdentity, bool resubmit)
		{
			if (!queueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (queueIdentity.Type == QueueType.Delivery)
			{
				RoutedMessageQueue routedMessageQueue;
				lock (Components.RemoteDeliveryComponent.SyncQueues)
				{
					List<RoutedMessageQueue> list = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
					if (list.Count == 0)
					{
						return false;
					}
					if (list.Count > 1)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
					}
					routedMessageQueue = list[0];
					if (routedMessageQueue.Suspended)
					{
						return true;
					}
					routedMessageQueue.ResetScheduledCallback();
				}
				if (resubmit)
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Queue {0} resubmitted by the admin", queueIdentity);
					await routedMessageQueue.ResubmitAsync(ResubmitReason.Admin, null);
				}
				else
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Queue {0} forced into immediate retry by the admin", queueIdentity);
					Components.RemoteDeliveryComponent.ConnectionManager.CreateConnectionIfNecessary(routedMessageQueue);
				}
			}
			else if (queueIdentity.Type == QueueType.Unreachable)
			{
				if (!resubmit)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity>(0L, "Queue {0} resubmitted by the admin", queueIdentity);
				RemoteDeliveryComponent.UnreachableMessageQueue.Resubmit(ResubmitReason.Admin, null);
			}
			else
			{
				if (queueIdentity.Type != QueueType.Shadow)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity, bool>(0L, "Retry-Queue {0} -Resubmit:${1}", queueIdentity, resubmit);
				List<ShadowMessageQueue> list2 = Components.ShadowRedundancyComponent.ShadowRedundancyManager.FindByQueueIdentity(queueIdentity);
				if (list2.Count == 0)
				{
					return false;
				}
				if (list2.Count > 1)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
				}
				if (resubmit)
				{
					list2[0].Resubmit(ResubmitReason.Admin);
				}
				else
				{
					list2[0].ScheduleImmediateHeartbeat();
				}
			}
			return true;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000A4DA0 File Offset: 0x000A2FA0
		public static bool FreezeMessage(MessageIdentity mailItemId)
		{
			if (!mailItemId.QueueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Submission)
			{
				bool flag = Components.MessageDepotComponent.Enabled ? QueueManager.SuspendMessageInMessageDepot(mailItemId.InternalId) : Components.CategorizerComponent.SubmitMessageQueue.SuspendMailItem(mailItemId.InternalId);
				if (!flag && Components.CategorizerComponent.CatContainsMailItem(mailItemId.InternalId))
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				return flag;
			}
			else
			{
				if (mailItemId.QueueIdentity.Type != QueueType.Delivery && mailItemId.QueueIdentity.Type != QueueType.Unreachable && mailItemId.QueueIdentity.Type != QueueType.Shadow)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				TransportMailItem mailItem = Components.RemoteDeliveryComponent.GetMailItem(mailItemId);
				if (mailItem == null)
				{
					mailItem = Components.ShadowRedundancyComponent.ShadowRedundancyManager.GetMailItem(mailItemId);
					if (mailItem == null)
					{
						return false;
					}
				}
				bool flag2 = false;
				NextHopSolution nextHopSolution = null;
				lock (mailItem)
				{
					if (mailItemId.QueueIdentity.Type != QueueType.Shadow)
					{
						if (!mailItem.IsActive)
						{
							return false;
						}
						if (!mailItem.QueuedForDelivery)
						{
							throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
						}
					}
					else if (mailItem.IsRowDeleted)
					{
						return false;
					}
					if (!QueueManager.TryGetMailItemSolution(mailItem, mailItemId.QueueIdentity, out nextHopSolution))
					{
						return false;
					}
					if (nextHopSolution.IsInactive)
					{
						return false;
					}
					if (nextHopSolution.DeliveryStatus == DeliveryStatus.InDelivery && nextHopSolution.IsDeletedByAdmin)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
					}
					if (nextHopSolution.AdminActionStatus == AdminActionStatus.None)
					{
						nextHopSolution.AdminActionStatus = AdminActionStatus.Suspended;
						mailItem.CommitLazy();
						flag2 = true;
						QueueManager.RemoveItemFromDeliveryConditionManager(nextHopSolution, mailItem.Priority);
					}
				}
				if (flag2)
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Message {0} frozen by the admin", mailItemId);
				}
				return true;
			}
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000A4F74 File Offset: 0x000A3174
		public static QueuingPerfCountersInstance GetTotalPerfCounters()
		{
			return QueuingPerfCounters.GetInstance("_Total");
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000A4F80 File Offset: 0x000A3180
		public static IEnumerable<int> GetInstanceCounterIndex(RiskLevel riskLevel, DeliveryPriority priority)
		{
			List<int> list = new List<int>();
			list.Add(QueueManager.priorityToInstanceIndexMap[priority]);
			int item;
			if (QueueManager.priorityToTotalExcludingPriorityNoneInstanceIndexMap.TryGetValue(priority, out item))
			{
				list.Add(item);
			}
			if (QueueManager.includeRiskBasedCounters)
			{
				list.Add(QueueManager.riskToInstanceIndexMap[riskLevel]);
				if (QueueManager.riskToHighAndBulkRiskTotalInstanceIndexMap.TryGetValue(riskLevel, out item))
				{
					list.Add(item);
				}
				if (QueueManager.riskToNormalAndLowRiskTotalInstanceIndexMap.TryGetValue(riskLevel, out item))
				{
					list.Add(item);
				}
				Tuple<RiskLevel, DeliveryPriority> key = new Tuple<RiskLevel, DeliveryPriority>(riskLevel, priority);
				if (QueueManager.riskAndPriorityInstanceIndexMap.TryGetValue(key, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000A5020 File Offset: 0x000A3220
		public static void PreAsyncRetryQueueValidate(QueueIdentity queueIdentity, bool resubmit)
		{
			if (!queueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (queueIdentity.Type == QueueType.Delivery)
			{
				lock (Components.RemoteDeliveryComponent.SyncQueues)
				{
					List<RoutedMessageQueue> list = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
					if (list.Count == 0)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_OBJECT_NOT_FOUND);
					}
					if (list.Count > 1)
					{
						throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
					}
					return;
				}
			}
			if (queueIdentity.Type == QueueType.Unreachable)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<bool, bool>(0L, "Unreachable Queue can only be resubmitted while not being suspended: Resubmit = {0}, Suspended = {1}", resubmit, RemoteDeliveryComponent.UnreachableMessageQueue.Suspended);
				if (!resubmit)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
			}
			else
			{
				if (queueIdentity.Type != QueueType.Shadow)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				ExTraceGlobals.QueuingTracer.TraceDebug<QueueIdentity, bool>(0L, "Retry-Queue {0} -Resubmit:${1}", queueIdentity, resubmit);
				List<ShadowMessageQueue> list2 = Components.ShadowRedundancyComponent.ShadowRedundancyManager.FindByQueueIdentity(queueIdentity);
				if (list2.Count == 0)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_OBJECT_NOT_FOUND);
				}
				if (list2.Count > 1)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
				}
			}
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000A5144 File Offset: 0x000A3344
		public void Load()
		{
			if (this.queuingPerfCountersTotalInstance == null)
			{
				this.queuingPerfCountersTotalInstance = QueueManager.GetTotalPerfCounters();
				int num = QueueManager.includeRiskBasedCounters ? (QueueManager.riskBasedInstanceCounterNames.Length + QueueManager.priorityBasedInstanceCounterNames.Length) : QueueManager.priorityBasedInstanceCounterNames.Length;
				this.queuingPerfCountersInstances = new QueuingPerfCountersInstance[num];
				for (int i = 0; i < QueueManager.priorityBasedInstanceCounterNames.Length; i++)
				{
					this.queuingPerfCountersInstances[i] = QueuingPerfCounters.GetInstance(QueueManager.priorityBasedInstanceCounterNames[i]);
				}
				if (QueueManager.includeRiskBasedCounters)
				{
					for (int j = 0; j < QueueManager.riskBasedInstanceCounterNames.Length; j++)
					{
						this.queuingPerfCountersInstances[j + QueueManager.priorityBasedInstanceCounterNames.Length] = QueuingPerfCounters.GetInstance(QueueManager.riskBasedInstanceCounterNames[j]);
					}
				}
				Components.RemoteDeliveryComponent.SetPerfCounters(this.queuingPerfCountersTotalInstance);
			}
			if (this.poisonMessageQueue == null)
			{
				this.poisonMessageQueue = PoisonMessageQueue.Instance;
			}
			this.submissionPerfCounterWrapper = new QueueManager.SubmissionPerfCounterWrapper(this.queuingPerfCountersTotalInstance, Components.TransportAppConfig.QueueConfiguration.RecentPerfCounterTrackingInterval, Components.TransportAppConfig.QueueConfiguration.RecentPerfCounterTrackingBucketSize);
			this.LoadMessageQueues();
			Components.RoutingComponent.MailRouter.RoutingTablesChanged += UnreachableMessageQueue.Instance.RoutingTablesChangedHandler;
			this.queuedRecipientsByAge = new QueuedRecipientsByAgePerfCountersWrapper(Components.TransportAppConfig.QueueConfiguration.QueuedRecipientsByAgeTrackingEnabled);
			SubmitMessageQueue.Instance.OnAcquire += this.GetQueuedRecipientsByAge().TrackEnteringSubmissionQueue;
			SubmitMessageQueue.Instance.OnRelease += this.GetQueuedRecipientsByAge().TrackExitingSubmissionQueue;
			SubmitMessageQueue.Instance.OnAcquire += new Action<TransportMailItem>(this.UpdatePerfCountersOnEnterSubmissionQueue);
			SubmitMessageQueue.Instance.OnRelease += new Action<TransportMailItem>(this.UpdatePerfCountersOnExitSubmissionQueue);
			Components.RemoteDeliveryComponent.OnAcquireRoutedMailItem += this.GetQueuedRecipientsByAge().TrackEnteringDeliveryQueue;
			Components.RemoteDeliveryComponent.OnReleaseRoutedMailItem += this.GetQueuedRecipientsByAge().TrackExitingDeliveryQueue;
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x000A5317 File Offset: 0x000A3517
		public void Unload()
		{
			if (UnreachableMessageQueue.Instance != null)
			{
				Components.RoutingComponent.MailRouter.RoutingTablesChanged -= UnreachableMessageQueue.Instance.RoutingTablesChangedHandler;
			}
			this.queuedRecipientsByAge.Reset();
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x000A534A File Offset: 0x000A354A
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x000A5350 File Offset: 0x000A3550
		public bool UnfreezeMessage(MessageIdentity mailItemId)
		{
			if (!mailItemId.QueueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			switch (mailItemId.QueueIdentity.Type)
			{
			case QueueType.Delivery:
			case QueueType.Unreachable:
				return QueueManager.UnfreezeRemoteDeliveryMessage(mailItemId);
			case QueueType.Poison:
				return this.UnfreezePoisonMessage(mailItemId);
			case QueueType.Submission:
				return (Components.MessageDepotComponent.Enabled ? QueueManager.ResumeMessageInMessageDepot(mailItemId.InternalId) : Components.CategorizerComponent.SubmitMessageQueue.ResumeMailItem(mailItemId.InternalId)) || Components.CategorizerComponent.CatContainsMailItem(mailItemId.InternalId);
			case QueueType.Shadow:
				return QueueManager.UnfreezeShadowMessage(mailItemId);
			default:
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
			}
		}

		// Token: 0x06002998 RID: 10648 RVA: 0x000A5404 File Offset: 0x000A3604
		public bool DeleteMessage(MessageIdentity mailItemId, bool withNDR)
		{
			if (!mailItemId.QueueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Delivery || mailItemId.QueueIdentity.Type == QueueType.Unreachable)
			{
				return QueueManager.DeleteRemoteDeliveryMessage(mailItemId, withNDR);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Shadow)
			{
				return QueueManager.DeleteShadowMessage(mailItemId);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Poison)
			{
				return this.DeletePoisonMessage(mailItemId);
			}
			if (mailItemId.QueueIdentity.Type != QueueType.Submission)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
			}
			bool flag = Components.MessageDepotComponent.Enabled ? QueueManager.DeleteMessageFromMessageDepot(mailItemId.InternalId, withNDR) : Components.CategorizerComponent.SubmitMessageQueue.DeleteMailItem(mailItemId.InternalId, withNDR);
			if (!flag && Components.CategorizerComponent.CatContainsMailItem(mailItemId.InternalId))
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
			}
			return flag;
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x000A5600 File Offset: 0x000A3800
		public void RedirectMessage(MultiValuedProperty<Fqdn> targetServers)
		{
			List<string> targetHosts = (from server in targetServers
			select server.Domain).ToList<string>();
			if (!targetHosts.All(new Func<string, bool>(Components.RoutingComponent.MailRouter.IsHubTransportServer)))
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_COLLECTION);
			}
			if (this.pendingRedirectMessageTask != null && !this.pendingRedirectMessageTask.IsCompleted)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_REDIRECT_MESSAGE_IN_PROGRESS);
			}
			List<string> resubmitQueueNames = new List<string>();
			List<RoutedMessageQueue> resubmitQueues = new List<RoutedMessageQueue>();
			long resubmitCount = 0L;
			QueueManager.FreezeQueue(QueueIdentity.SubmissionQueueIdentity);
			Task<int>[] resubmitTasks;
			try
			{
				RemoteDeliveryComponent.UnreachableMessageQueue.Resubmit(ResubmitReason.Redirect, null);
				resubmitQueueNames.Add(Strings.LatencyComponentSubmissionQueue);
				resubmitQueueNames.Add(Strings.LatencyComponentUnreachableQueue);
				resubmitCount = (Components.MessageDepotComponent.Enabled ? QueueManager.RedirectMessagesInMessageDepot(targetHosts) : QueueManager.RedirectMessagesInSubmissionQueue(targetHosts));
				lock (Components.RemoteDeliveryComponent.SyncQueues)
				{
					foreach (RoutedMessageQueue routedMessageQueue in from queue in Components.RemoteDeliveryComponent.GetQueueArray()
					where !queue.IsEmpty
					select queue)
					{
						resubmitQueues.Add(routedMessageQueue);
						routedMessageQueue.AddReference();
						resubmitQueueNames.Add(string.Format("'{0}:{1}:{2}'", routedMessageQueue.Key.NextHopType, routedMessageQueue.Key.NextHopDomain, routedMessageQueue.Key.NextHopConnector));
					}
				}
				resubmitTasks = (from q in resubmitQueues
				select q.ResubmitAsync(ResubmitReason.Redirect, delegate(TransportMailItem tmi)
				{
					QueueManager.RedirectMessage(tmi, targetHosts);
				})).ToArray<Task<int>>();
			}
			catch
			{
				QueueManager.UnfreezeQueue(QueueIdentity.SubmissionQueueIdentity);
				throw;
			}
			this.pendingRedirectMessageTask = Task.WhenAll<int>(resubmitTasks).ContinueWith(delegate(Task<int[]> t)
			{
				QueueManager.UnfreezeQueue(QueueIdentity.SubmissionQueueIdentity);
				resubmitQueues.ForEach(delegate(RoutedMessageQueue queue)
				{
					queue.ReleaseReference();
				});
				resubmitCount = resubmitTasks.Aggregate(resubmitCount, (long current, Task<int> resubmitTask) => current + (long)resubmitTask.Result);
				QueueManager.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RedirectMessageStarted, null, new object[]
				{
					string.Join(",", resubmitQueueNames.ToArray()),
					string.Join(",", targetHosts.ToArray()),
					resubmitCount
				});
			});
		}

		// Token: 0x0600299A RID: 10650 RVA: 0x000A5898 File Offset: 0x000A3A98
		public bool SetMessage(MessageIdentity mailItemId, ExtensibleMessageInfo properties, bool resubmit)
		{
			if (!mailItemId.QueueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Shadow || mailItemId.QueueIdentity.Type == QueueType.Poison)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Delivery || mailItemId.QueueIdentity.Type == QueueType.Unreachable)
			{
				return QueueManager.SetDeliveryMessage(mailItemId, properties, resubmit);
			}
			bool flag;
			if (mailItemId.QueueIdentity.Type != QueueType.Submission || Components.CategorizerComponent.SubmitMessageQueue.UpdateMailItem(mailItemId.InternalId, properties, out flag))
			{
				return true;
			}
			if (flag)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_MESSAGE_NOT_SUSPENDED);
			}
			throw new QueueViewerException(QVErrorCode.QV_E_OBJECT_NOT_FOUND);
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x000A594C File Offset: 0x000A3B4C
		public bool ReadMessageBody(MessageIdentity mailItemId, byte[] buffer, int position, int count, out int bytesRead)
		{
			if (!mailItemId.QueueIdentity.IsFullySpecified)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Delivery || mailItemId.QueueIdentity.Type == QueueType.Unreachable)
			{
				return QueueManager.ReadRemoteDeliveryMessageBody(mailItemId, buffer, position, count, out bytesRead);
			}
			if (mailItemId.QueueIdentity.Type == QueueType.Submission)
			{
				bool flag2;
				bool flag;
				if (Components.MessageDepotComponent.Enabled)
				{
					flag = QueueManager.ReadMessageBodyFromMessageDepot(mailItemId.InternalId, buffer, position, count, out bytesRead, out flag2);
				}
				else
				{
					flag = Components.CategorizerComponent.SubmitMessageQueue.ReadMessageBody(mailItemId.InternalId, buffer, position, count, out bytesRead, out flag2);
				}
				if (flag2)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MESSAGE_NOT_SUSPENDED);
				}
				if (!flag && Components.CategorizerComponent.CatContainsMailItem(mailItemId.InternalId))
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				return flag;
			}
			else
			{
				if (mailItemId.QueueIdentity.Type == QueueType.Poison)
				{
					return this.ReadPoisonMessageBody(mailItemId, buffer, position, count, out bytesRead);
				}
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000A5A40 File Offset: 0x000A3C40
		public ExtensibleMessageInfo[] GetMessageInfoPage(PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine, out int matchCount, out int pageOffset)
		{
			MessageInfoFactory messageInfoFactory = new MessageInfoFactory(pagingEngine.IncludeDetails, pagingEngine.IncludeComponentLatencyInfo);
			matchCount = 0;
			pageOffset = 0;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IMessageDepotQueueViewer messageDepotQueueViewer = null;
			bool enabled = Components.MessageDepotQueueViewerComponent.Enabled;
			if (enabled)
			{
				messageDepotQueueViewer = Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer;
			}
			TransportMailItem transportMailItem = null;
			TransportMailItem transportMailItem2 = null;
			TransportMailItem transportMailItem3 = null;
			CategorizerItem categorizerItem = null;
			IMessageDepotItemWrapper messageDepotItemWrapper = null;
			ICollection<NextHopSolution> collection = null;
			if (pagingEngine.IdentitySearch)
			{
				MessageIdentity messageIdentity = (MessageIdentity)pagingEngine.IdentitySearchValue;
				if (messageIdentity.QueueIdentity.IsEmpty)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_IDENTITY_FOR_EQUALITY);
				}
				transportMailItem2 = Components.ShadowRedundancyComponent.ShadowRedundancyManager.GetMailItem(messageIdentity);
				if (transportMailItem2 != null)
				{
					collection = QueueManager.GetMailItemSolutions(transportMailItem2, messageIdentity.QueueIdentity);
				}
				transportMailItem = Components.RemoteDeliveryComponent.GetMailItem(messageIdentity);
				if (transportMailItem != null && collection == null)
				{
					collection = QueueManager.GetMailItemSolutions(transportMailItem, messageIdentity.QueueIdentity);
				}
				if (transportMailItem2 == null && transportMailItem == null)
				{
					if (enabled)
					{
						if (QueueManager.TryGetMailItemByIdFromMessageDepot(messageIdentity, out messageDepotItemWrapper) && messageDepotItemWrapper.State == MessageDepotItemState.Poisoned)
						{
							transportMailItem3 = (TransportMailItem)messageDepotItemWrapper.Item.MessageObject;
							messageDepotItemWrapper = null;
						}
					}
					else
					{
						categorizerItem = Components.CategorizerComponent.GetCategorizerItemById(messageIdentity);
						if (categorizerItem == null)
						{
							transportMailItem3 = this.poisonMessageQueue[messageIdentity];
						}
					}
				}
				if (transportMailItem == null && transportMailItem3 == null && categorizerItem == null && transportMailItem2 == null && messageDepotItemWrapper == null)
				{
					stopwatch.Stop();
					ExTraceGlobals.QueuingTracer.TraceDebug<long, long>(0L, "Return 0 ExtensibleMessageInfo elements in {0}ms [{1} ticks]", stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);
					return QueueManager.emptyMessageInfoResult;
				}
			}
			int num;
			bool flag;
			ExtensibleMessageInfo[] page;
			do
			{
				matchCount = 0;
				num = 0;
				pagingEngine.ResetResultSet();
				QueueManager.MultiSolutionMessageFilter multiSolutionMessageFilter = new QueueManager.MultiSolutionMessageFilter(messageInfoFactory, pagingEngine, collection, true);
				if (pagingEngine.IdentitySearch)
				{
					multiSolutionMessageFilter.Visit(transportMailItem2);
				}
				else
				{
					Components.ShadowRedundancyComponent.ShadowRedundancyManager.VisitMailItems(new Func<TransportMailItem, bool>(multiSolutionMessageFilter.Visit));
				}
				matchCount += multiSolutionMessageFilter.MatchCount;
				num += multiSolutionMessageFilter.TotalCount;
				if (matchCount < pagingEngine.PageSize)
				{
					QueueManager.MultiSolutionMessageFilter multiSolutionMessageFilter2 = new QueueManager.MultiSolutionMessageFilter(messageInfoFactory, pagingEngine, collection, false);
					if (pagingEngine.IdentitySearch)
					{
						multiSolutionMessageFilter2.Visit(transportMailItem);
					}
					else
					{
						Components.RemoteDeliveryComponent.VisitMailItems(new Func<TransportMailItem, bool>(multiSolutionMessageFilter2.Visit));
					}
					matchCount += multiSolutionMessageFilter2.MatchCount;
					num += multiSolutionMessageFilter2.TotalCount;
				}
				if (matchCount < pagingEngine.PageSize)
				{
					QueueManager.PoisonMessageFilter poisonMessageFilter = new QueueManager.PoisonMessageFilter(messageInfoFactory, pagingEngine);
					if (pagingEngine.IdentitySearch)
					{
						poisonMessageFilter.Visit(transportMailItem3);
						matchCount += poisonMessageFilter.MatchCount;
						num += poisonMessageFilter.TotalCount;
					}
					else if (enabled)
					{
						QueueManager.PoisonMessageDepotItemFilter poisonMessageDepotItemFilter = new QueueManager.PoisonMessageDepotItemFilter(messageInfoFactory, pagingEngine);
						messageDepotQueueViewer.VisitMailItems(new Func<IMessageDepotItemWrapper, bool>(poisonMessageDepotItemFilter.Visit));
						matchCount += poisonMessageDepotItemFilter.MatchCount;
						num += poisonMessageDepotItemFilter.TotalCount;
					}
					else
					{
						this.poisonMessageQueue.VisitMailItems(new Func<TransportMailItem, bool>(poisonMessageFilter.Visit));
						matchCount += poisonMessageFilter.MatchCount;
						num += poisonMessageFilter.TotalCount;
					}
				}
				if (matchCount < pagingEngine.PageSize)
				{
					if (enabled)
					{
						QueueManager.SubmissionMessageDepotItemFilter submissionMessageDepotItemFilter = new QueueManager.SubmissionMessageDepotItemFilter(messageInfoFactory, pagingEngine);
						if (pagingEngine.IdentitySearch)
						{
							submissionMessageDepotItemFilter.Visit(messageDepotItemWrapper);
						}
						else
						{
							messageDepotQueueViewer.VisitMailItems(new Func<IMessageDepotItemWrapper, bool>(submissionMessageDepotItemFilter.Visit));
						}
						matchCount += submissionMessageDepotItemFilter.MatchCount;
						num += submissionMessageDepotItemFilter.TotalCount;
					}
					else
					{
						QueueManager.CatogorizerMessageFilter catogorizerMessageFilter = new QueueManager.CatogorizerMessageFilter(messageInfoFactory, pagingEngine);
						if (pagingEngine.IdentitySearch)
						{
							catogorizerMessageFilter.Visit(categorizerItem);
						}
						else
						{
							Components.CategorizerComponent.VisitCategorizerItems(new Func<CategorizerItem, bool>(catogorizerMessageFilter.Visit), true);
						}
						matchCount += catogorizerMessageFilter.MatchCount;
						num += catogorizerMessageFilter.TotalCount;
					}
				}
				page = pagingEngine.GetPage(matchCount, out pageOffset, out flag);
			}
			while (flag);
			stopwatch.Stop();
			ExTraceGlobals.QueuingTracer.TraceDebug<int, long, long>(0L, "Processed {0} TransportMailItem elements in {1} ms [{2} ticks]", num, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);
			ExTraceGlobals.QueuingTracer.TraceDebug<int>(0L, "Return {0} ExtensibleMessageInfo elements", page.Length);
			return page;
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x000A5E30 File Offset: 0x000A4030
		public ExtensibleQueueInfo[] GetQueueInfoPage(PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema> pagingEngine, out int totalCount, out int pageOffset)
		{
			totalCount = 0;
			pageOffset = 0;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			int num = 0;
			RoutedMessageQueue[] array = null;
			ShadowMessageQueue[] array2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			long num2;
			if (Components.MessageDepotQueueViewerComponent.Enabled)
			{
				IMessageDepotQueueViewer messageDepotQueueViewer = Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer;
				num2 = messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Poisoned);
			}
			else
			{
				num2 = (long)this.PoisonMessageQueue.Count;
			}
			if (pagingEngine.IdentitySearch)
			{
				QueueIdentity queueIdentity = (QueueIdentity)pagingEngine.IdentitySearchValue;
				if (queueIdentity == QueueIdentity.PoisonQueueIdentity)
				{
					if (num2 > 0L)
					{
						flag = true;
						num = 1;
					}
				}
				else if (queueIdentity == QueueIdentity.SubmissionQueueIdentity)
				{
					flag2 = true;
					num = 1;
				}
				else if (queueIdentity == QueueIdentity.UnreachableQueueIdentity)
				{
					if (RemoteDeliveryComponent.UnreachableMessageQueue.CountNotDeleted > 0 || RemoteDeliveryComponent.UnreachableMessageQueue.Suspended)
					{
						flag3 = true;
						num = 1;
					}
				}
				else if (queueIdentity.Type == QueueType.Shadow)
				{
					List<ShadowMessageQueue> list = Components.ShadowRedundancyComponent.ShadowRedundancyManager.FindByQueueIdentity(queueIdentity);
					if (list.Count > 0)
					{
						array2 = list.ToArray();
						num += array2.Length;
					}
				}
				else
				{
					List<RoutedMessageQueue> list2 = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
					if (list2.Count > 0)
					{
						array = list2.ToArray();
						num += array.Length;
					}
				}
				if (num == 0)
				{
					return new List<ExtensibleQueueInfo>(0).ToArray();
				}
			}
			else
			{
				array = Components.RemoteDeliveryComponent.GetQueueArray();
				array2 = Components.ShadowRedundancyComponent.ShadowRedundancyManager.GetQueueArray();
				num = array.Length + array2.Length;
				flag2 = true;
				num++;
				if (num2 > 0L)
				{
					flag = true;
					num++;
				}
				UnreachableMessageQueue unreachableMessageQueue = RemoteDeliveryComponent.UnreachableMessageQueue;
				if (unreachableMessageQueue.CountNotDeleted > 0 || unreachableMessageQueue.Suspended)
				{
					flag3 = true;
					num++;
				}
			}
			bool flag4 = false;
			ExtensibleQueueInfo[] page;
			do
			{
				totalCount = 0;
				pagingEngine.ResetResultSet();
				if (array != null)
				{
					foreach (RoutedMessageQueue routedMessageQueue in array)
					{
						if (routedMessageQueue.IsAdminVisible)
						{
							ExtensibleQueueInfo queueInfo = QueueInfoFactory.NewDeliveryQueueInfo(routedMessageQueue);
							bool flag5 = QueueManager.ProcessQueueInfo(pagingEngine, queueInfo, ref totalCount);
							if (!flag5)
							{
								break;
							}
						}
					}
				}
				if (array2 != null)
				{
					foreach (ShadowMessageQueue shadowMessageQueue in array2)
					{
						ExtensibleQueueInfo queueInfo2 = QueueInfoFactory.NewShadowQueueInfo(shadowMessageQueue);
						bool flag6 = QueueManager.ProcessQueueInfo(pagingEngine, queueInfo2, ref totalCount);
						if (!flag6)
						{
							break;
						}
					}
				}
				if (flag)
				{
					ExtensibleQueueInfo queueInfo3 = QueueInfoFactory.NewPoisonQueueInfo();
					QueueManager.ProcessQueueInfo(pagingEngine, queueInfo3, ref totalCount);
				}
				if (flag2)
				{
					ExtensibleQueueInfo queueInfo4 = QueueInfoFactory.NewSubmissionQueueInfo();
					QueueManager.ProcessQueueInfo(pagingEngine, queueInfo4, ref totalCount);
				}
				if (flag3)
				{
					ExtensibleQueueInfo queueInfo5 = QueueInfoFactory.NewUnreachableQueueInfo();
					QueueManager.ProcessQueueInfo(pagingEngine, queueInfo5, ref totalCount);
				}
				page = pagingEngine.GetPage(totalCount, out pageOffset, out flag4);
			}
			while (flag4);
			stopwatch.Stop();
			ExTraceGlobals.QueuingTracer.TraceDebug<int, long, long>(0L, "Processed {0} Queue objects in {1} ms [{2} ticks]", num, stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks);
			ExTraceGlobals.QueuingTracer.TraceDebug<int>(0L, "Return {0} ExtensibleQueueInfo elements", page.Length);
			return page;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x000A60EC File Offset: 0x000A42EC
		public void LoadMessageQueues()
		{
			foreach (RoutedQueueBase routedQueueBase in Components.MessagingDatabase.Queues)
			{
				if (routedQueueBase.NextHopType.DeliveryType == DeliveryType.Undefined)
				{
					SubmitMessageQueue.LoadInstance(routedQueueBase);
				}
				else if (routedQueueBase.NextHopType.DeliveryType == DeliveryType.Unreachable)
				{
					UnreachableMessageQueue.LoadInstance(routedQueueBase);
				}
				else if (routedQueueBase.NextHopType.DeliveryType == DeliveryType.ShadowRedundancy)
				{
					Components.ShadowRedundancyComponent.ShadowRedundancyManager.LoadQueue(routedQueueBase);
				}
				else
				{
					Components.RemoteDeliveryComponent.LoadQueue(routedQueueBase);
				}
			}
			if (SubmitMessageQueue.Instance == null)
			{
				SubmitMessageQueue.CreateInstance();
			}
			if (UnreachableMessageQueue.Instance == null)
			{
				UnreachableMessageQueue.CreateInstance();
			}
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x000A61B0 File Offset: 0x000A43B0
		public void UpateInstanceCounter(RiskLevel riskLevel, DeliveryPriority priority, Action<QueuingPerfCountersInstance> updateCounter)
		{
			IEnumerable<int> instanceCounterIndex = QueueManager.GetInstanceCounterIndex(riskLevel, priority);
			foreach (int num in instanceCounterIndex)
			{
				updateCounter(this.queuingPerfCountersInstances[num]);
			}
			updateCounter(this.queuingPerfCountersTotalInstance);
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000A6214 File Offset: 0x000A4414
		public void UpdateAllInstanceCounters(int[] instanceValues, Action<QueuingPerfCountersInstance, int> updateCounterWithValue)
		{
			this.UpdateAllInstanceCounters(instanceValues, updateCounterWithValue, true);
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x000A6220 File Offset: 0x000A4420
		public void UpdateAllInstanceCounters(int[] instanceValues, Action<QueuingPerfCountersInstance, int> updateCounterWithValue, bool updateTotal)
		{
			if (instanceValues == null || instanceValues.Length != QueueManager.InstanceCountersLength)
			{
				throw new InvalidOperationException(string.Format("instanceValues does not have the right array length. Expected: {0}, Actual: {1}", QueueManager.InstanceCountersLength, (instanceValues != null) ? instanceValues.Length : 0));
			}
			for (int i = 0; i < QueueManager.InstanceCountersLength; i++)
			{
				updateCounterWithValue(this.queuingPerfCountersInstances[i], instanceValues[i]);
			}
			if (updateTotal)
			{
				updateCounterWithValue(this.queuingPerfCountersTotalInstance, this.GetTotalFromInstance(instanceValues));
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000A62B8 File Offset: 0x000A44B8
		public int GetTotalFromInstance(IList<int> instanceValues)
		{
			if (instanceValues == null || instanceValues.Count != QueueManager.InstanceCountersLength)
			{
				throw new InvalidOperationException(string.Format("instanceValues does not have the right array length. Expected: {0}, Actual: {1}", QueueManager.InstanceCountersLength, (instanceValues != null) ? instanceValues.Count : 0));
			}
			return QueueManager.priorityToInstanceIndexMap.Sum((KeyValuePair<DeliveryPriority, int> priorityIndex) => instanceValues[priorityIndex.Value]);
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000A6337 File Offset: 0x000A4537
		public void UpdatePerfCountersOnEnterSubmissionQueue(IQueueItem item)
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnEnterSubmissionQueue(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000A6352 File Offset: 0x000A4552
		public void UpdatePerfCountersOnExpireFromSubmissionQueue(IQueueItem item)
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnExpireFromSubmissionQueue(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000A636D File Offset: 0x000A456D
		public void UpdatePerfCountersOnLockExpiredInSubmissionQueue()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.queuingPerfCountersTotalInstance.SubmissionQueueLocksExpiredTotal.Increment();
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000A6388 File Offset: 0x000A4588
		public void UpdatePerfCountersOnLockExpiredInDeliveryQueue()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.queuingPerfCountersTotalInstance.LocksExpiredInDeliveryTotal.Increment();
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000A63A3 File Offset: 0x000A45A3
		public void UpdatePerfCountersOnExitSubmissionQueue(IQueueItem item)
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnExitSubmissionQueue(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000A63BE File Offset: 0x000A45BE
		public void UpdatePerfCountersOnMessageBifurcatedInCategorizer()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnMessageBifurcatedInCategorizer(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000A63D9 File Offset: 0x000A45D9
		public void UpdatePerfCountersOnLeavingCategorizer()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnLeavingCategorizer(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x000A63F4 File Offset: 0x000A45F4
		public void UpdatePerfCountersOnMessageDeferredFromCategorizer()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnMessageDeferredFromCategorizer(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x000A640F File Offset: 0x000A460F
		public QueuedRecipientsByAgePerfCountersWrapper GetQueuedRecipientsByAge()
		{
			if (this.queuedRecipientsByAge == null)
			{
				throw new InvalidOperationException("cannot acquire queuedRecipientsByAge before loading the component");
			}
			return this.queuedRecipientsByAge;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000A642A File Offset: 0x000A462A
		public void UpdatePerfCountersOnMessagesResubmittedFromCategorizer()
		{
			if (this.queuingPerfCountersTotalInstance != null)
			{
				this.submissionPerfCounterWrapper.OnMessagesResubmittedFromCategorizer(this.queuingPerfCountersTotalInstance);
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000A6445 File Offset: 0x000A4645
		public void TimeUpdatePerfCounters()
		{
			this.submissionPerfCounterWrapper.OnTimedUpdate(this.queuingPerfCountersTotalInstance);
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000A6458 File Offset: 0x000A4658
		internal static bool TryGetMailItemByIdFromMessageDepot(long mailItemId, out IMessageDepotItemWrapper item)
		{
			TransportMessageId messageId = new TransportMessageId(mailItemId.ToString(CultureInfo.CurrentCulture));
			return Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer.TryGet(messageId, out item);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x000A6488 File Offset: 0x000A4688
		private static bool SuspendMessageInMessageDepot(long internalMessageId)
		{
			IMessageDepotItemWrapper messageDepotItemWrapper;
			if (!QueueManager.TryGetMailItemByIdFromMessageDepot(internalMessageId, out messageDepotItemWrapper))
			{
				return false;
			}
			IMessageDepotItem item = messageDepotItemWrapper.Item;
			TransportMailItem transportMailItem = (TransportMailItem)item.MessageObject;
			if (transportMailItem == null)
			{
				return false;
			}
			try
			{
				Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer.Suspend(item.Id);
				transportMailItem.Suspend();
			}
			catch (MessageDepotPermanentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000A64F0 File Offset: 0x000A46F0
		private static bool ResumeMessageInMessageDepot(long internalMessageId)
		{
			IMessageDepotItemWrapper messageDepotItemWrapper;
			if (!QueueManager.TryGetMailItemByIdFromMessageDepot(internalMessageId, out messageDepotItemWrapper))
			{
				return false;
			}
			IMessageDepotItem item = messageDepotItemWrapper.Item;
			TransportMailItem transportMailItem = (TransportMailItem)item.MessageObject;
			if (transportMailItem == null)
			{
				return false;
			}
			try
			{
				Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer.Resume(item.Id);
				transportMailItem.Resume();
			}
			catch (MessageDepotPermanentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x000A6558 File Offset: 0x000A4758
		private static bool DeleteMessageFromMessageDepot(long internalMessageId, bool withNdr)
		{
			IMessageDepotItemWrapper messageDepotItemWrapper;
			if (!QueueManager.TryGetMailItemByIdFromMessageDepot(internalMessageId, out messageDepotItemWrapper))
			{
				return false;
			}
			IMessageDepotQueueViewer messageDepotQueueViewer = Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer;
			try
			{
				messageDepotQueueViewer.Remove(messageDepotItemWrapper.Item.Id, withNdr);
			}
			catch (MessageDepotPermanentException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x000A65A8 File Offset: 0x000A47A8
		private static bool ReadMessageBodyFromMessageDepot(long internalMessageId, byte[] buffer, int position, int count, out int bytesRead, out bool foundNotSuspended)
		{
			bytesRead = 0;
			foundNotSuspended = false;
			IMessageDepotItemWrapper messageDepotItemWrapper;
			if (!QueueManager.TryGetMailItemByIdFromMessageDepot(internalMessageId, out messageDepotItemWrapper))
			{
				return false;
			}
			foundNotSuspended = (messageDepotItemWrapper.State == MessageDepotItemState.Suspended);
			if (foundNotSuspended)
			{
				return false;
			}
			TransportMailItem transportMailItem = (TransportMailItem)messageDepotItemWrapper.Item.MessageObject;
			Stream stream;
			if (ExportStream.TryCreate(transportMailItem, transportMailItem.Recipients, false, out stream))
			{
				using (stream)
				{
					stream.Position = (long)position;
					bytesRead = stream.Read(buffer, 0, count);
				}
			}
			return true;
		}

		// Token: 0x060029B3 RID: 10675 RVA: 0x000A6634 File Offset: 0x000A4834
		private static bool UnfreezeRemoteDeliveryMessage(MessageIdentity mailItemId)
		{
			bool flag = false;
			DateTime utcNow = DateTime.UtcNow;
			bool flag2 = false;
			NextHopSolution nextHopSolution = null;
			TransportMailItem mailItem = Components.RemoteDeliveryComponent.GetMailItem(mailItemId);
			if (mailItem == null)
			{
				return false;
			}
			lock (mailItem)
			{
				if (!mailItem.IsActive)
				{
					return false;
				}
				if (!mailItem.QueuedForDelivery)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				if (!QueueManager.TryGetMailItemSolution(mailItem, mailItemId.QueueIdentity, out nextHopSolution))
				{
					return false;
				}
				if (nextHopSolution.IsInactive)
				{
					return false;
				}
				if (nextHopSolution.DeliveryStatus == DeliveryStatus.InDelivery && nextHopSolution.IsDeletedByAdmin)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				if (nextHopSolution.AdminActionStatus == AdminActionStatus.Suspended)
				{
					nextHopSolution.AdminActionStatus = AdminActionStatus.None;
					flag2 = true;
					flag = (nextHopSolution.DeliveryStatus == DeliveryStatus.DequeuedAndDeferred);
					if (flag)
					{
						nextHopSolution.DeliveryStatus = DeliveryStatus.Enqueued;
						nextHopSolution.DeferUntil = utcNow;
					}
					else
					{
						QueueManager.AddItemToDeliveryConditionManager(nextHopSolution);
					}
					mailItem.CommitLazy();
				}
			}
			if (flag2)
			{
				if (flag)
				{
					RoutedMessageQueue queue = Components.RemoteDeliveryComponent.GetQueue(nextHopSolution.NextHopSolutionKey);
					queue.UpdateNextActivationTime(utcNow);
					Components.RemoteDeliveryComponent.ConnectionManager.CreateConnectionIfNecessary(queue);
				}
				ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Message {0} has been unfrozen by the admin", mailItemId);
			}
			return true;
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000A6784 File Offset: 0x000A4984
		private static bool UnfreezeShadowMessage(MessageIdentity mailItemId)
		{
			bool flag = false;
			NextHopSolution nextHopSolution = null;
			TransportMailItem mailItem = Components.ShadowRedundancyComponent.ShadowRedundancyManager.GetMailItem(mailItemId);
			if (mailItem == null)
			{
				return false;
			}
			lock (mailItem)
			{
				if (mailItem.IsRowDeleted)
				{
					return false;
				}
				if (!QueueManager.TryGetMailItemSolution(mailItem, mailItemId.QueueIdentity, out nextHopSolution))
				{
					return false;
				}
				if (nextHopSolution.AdminActionStatus == AdminActionStatus.Suspended)
				{
					nextHopSolution.AdminActionStatus = AdminActionStatus.None;
					flag = true;
					mailItem.CommitLazy();
				}
			}
			if (flag)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Shadow Message '{0}' has been unfrozen by the admin", mailItemId);
			}
			return true;
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000A6900 File Offset: 0x000A4B00
		private static bool DeleteRemoteDeliveryMessage(MessageIdentity mailItemId, bool withNDR)
		{
			TransportMailItem item = null;
			if (!QueueManager.RunActionOnSolutionInDelivery(mailItemId, delegate(TransportMailItem mailItem, NextHopSolution nextHopSolution)
			{
				RemoteMessageQueue queue = QueueManager.GetQueue(mailItemId);
				RoutedMailItem routedMailItem = null;
				try
				{
					routedMailItem = QueueManager.FindMailItem(queue, mailItemId, true);
					item = mailItem;
					nextHopSolution.AdminActionStatus = (withNDR ? AdminActionStatus.PendingDeleteWithNDR : AdminActionStatus.PendingDeleteWithOutNDR);
					IList<MailRecipient> recipientsToBeResubmitted = new List<MailRecipient>();
					bool flag;
					bool flag2;
					mailItem.Ack(AckStatus.Fail, AckReason.MessageDeletedByAdmin, null, nextHopSolution.Recipients, nextHopSolution.AdminActionStatus, null, null, recipientsToBeResubmitted, out flag, out flag2);
					QueueManager.PostProcessDeletedItem(nextHopSolution);
					nextHopSolution.DeliveryStatus = DeliveryStatus.Complete;
					if (withNDR)
					{
						Components.DsnGenerator.GenerateDSNs(mailItem, nextHopSolution.Recipients);
					}
					MessageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.ADMIN, mailItem, nextHopSolution.Recipients, null);
				}
				catch (Exception)
				{
					if (routedMailItem != null && nextHopSolution.DeliveryStatus != DeliveryStatus.Complete)
					{
						queue.Enqueue(routedMailItem);
					}
					throw;
				}
			}))
			{
				return false;
			}
			if (item.Status == Status.Complete)
			{
				item.ReleaseFromRemoteDelivery();
				Components.RemoteDeliveryComponent.ReleaseMailItem(item);
			}
			ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity, bool>(0L, "Message {0} deleted by the admin, NDR={1}", mailItemId, withNDR);
			return true;
		}

		// Token: 0x060029B6 RID: 10678 RVA: 0x000A698C File Offset: 0x000A4B8C
		private static bool RunActionOnSolutionInDelivery(MessageIdentity mailItemId, Action<TransportMailItem, NextHopSolution> action)
		{
			TransportMailItem mailItem = Components.RemoteDeliveryComponent.GetMailItem(mailItemId);
			if (mailItem == null)
			{
				return false;
			}
			lock (mailItem)
			{
				if (!mailItem.IsActive)
				{
					return false;
				}
				if (!mailItem.QueuedForDelivery)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				NextHopSolution nextHopSolution;
				if (!QueueManager.TryGetMailItemSolution(mailItem, mailItemId.QueueIdentity, out nextHopSolution))
				{
					return false;
				}
				if (nextHopSolution.IsInactive || nextHopSolution.IsDeletedByAdmin)
				{
					return false;
				}
				if (nextHopSolution.DeliveryStatus == DeliveryStatus.InDelivery)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				action(mailItem, nextHopSolution);
			}
			return true;
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x000A6A40 File Offset: 0x000A4C40
		private static bool DeleteShadowMessage(MessageIdentity mailItemId)
		{
			ShadowRedundancyManager shadowRedundancyManager = Components.ShadowRedundancyComponent.ShadowRedundancyManager;
			List<ShadowMessageQueue> list = shadowRedundancyManager.FindByQueueIdentity(mailItemId.QueueIdentity);
			if (list.Count > 1)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
			}
			if (list.Count == 0 || list[0].IsEmpty)
			{
				return false;
			}
			TransportMailItem mailItem = shadowRedundancyManager.GetMailItem(mailItemId.InternalId);
			if (mailItem == null)
			{
				return false;
			}
			bool flag = list[0].Discard(mailItem.ShadowMessageId, DiscardReason.DeletedByAdmin);
			if (flag)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Shadow Message '{0}' deleted by the admin.", mailItemId);
			}
			return flag;
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x000A6AD0 File Offset: 0x000A4CD0
		private static void PostProcessDeletedItem(NextHopSolution nextHopSolution)
		{
			RemoteMessageQueue remoteMessageQueue;
			if (nextHopSolution.NextHopSolutionKey.Equals(NextHopSolutionKey.Unreachable))
			{
				remoteMessageQueue = RemoteDeliveryComponent.UnreachableMessageQueue;
			}
			else
			{
				remoteMessageQueue = Components.RemoteDeliveryComponent.GetQueue(nextHopSolution.NextHopSolutionKey);
				if (remoteMessageQueue == null)
				{
					return;
				}
			}
			if (nextHopSolution.DeferUntil != DateTime.MinValue)
			{
				nextHopSolution.DeferUntil = DateTime.MinValue;
				remoteMessageQueue.UpdateNextActivationTime(DateTime.UtcNow);
			}
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x000A6B3C File Offset: 0x000A4D3C
		private static bool ReadRemoteDeliveryMessageBody(MessageIdentity mailItemId, byte[] buffer, int position, int count, out int bytesRead)
		{
			bytesRead = 0;
			TransportMailItem mailItem = Components.RemoteDeliveryComponent.GetMailItem(mailItemId);
			if (mailItem == null)
			{
				return false;
			}
			lock (mailItem)
			{
				if (!mailItem.IsActive)
				{
					return false;
				}
				if (!mailItem.QueuedForDelivery)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				NextHopSolution nextHopSolution;
				if (!QueueManager.TryGetMailItemSolution(mailItem, mailItemId.QueueIdentity, out nextHopSolution))
				{
					return false;
				}
				if (nextHopSolution.IsInactive)
				{
					return false;
				}
				if (nextHopSolution.AdminActionStatus != AdminActionStatus.Suspended)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MESSAGE_NOT_SUSPENDED);
				}
				if (nextHopSolution.DeliveryStatus == DeliveryStatus.InDelivery)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_INVALID_OPERATION);
				}
				Stream stream;
				if (ExportStream.TryCreate(mailItem, nextHopSolution.Recipients, false, out stream))
				{
					using (stream)
					{
						stream.Position = (long)position;
						bytesRead = stream.Read(buffer, 0, count);
					}
				}
			}
			if (bytesRead > 0)
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<int, MessageIdentity>(0L, "{0} bytes of message {1} read by the admin", bytesRead, mailItemId);
			}
			return true;
		}

		// Token: 0x060029BA RID: 10682 RVA: 0x000A6C64 File Offset: 0x000A4E64
		private static List<NextHopSolution> GetMailItemSolutions(TransportMailItem mailItem, QueueIdentity queueIdentity)
		{
			List<NextHopSolution> list = new List<NextHopSolution>();
			Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutions = mailItem.NextHopSolutions;
			if (queueIdentity.Type == QueueType.Unreachable)
			{
				NextHopSolution nextHopSolution;
				if (nextHopSolutions.TryGetValue(NextHopSolutionKey.Unreachable, out nextHopSolution))
				{
					list.Add(nextHopSolution);
				}
			}
			else
			{
				if (queueIdentity.Type == QueueType.Shadow)
				{
					List<ShadowMessageQueue> list2 = Components.ShadowRedundancyComponent.ShadowRedundancyManager.FindByQueueIdentity(queueIdentity);
					using (List<ShadowMessageQueue>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ShadowMessageQueue shadowMessageQueue = enumerator.Current;
							NextHopSolution nextHopSolution;
							if (nextHopSolutions.TryGetValue(shadowMessageQueue.Key, out nextHopSolution))
							{
								list.Add(nextHopSolution);
							}
						}
						return list;
					}
				}
				List<RoutedMessageQueue> list3 = Components.RemoteDeliveryComponent.FindByQueueIdentity(queueIdentity);
				foreach (RoutedMessageQueue routedMessageQueue in list3)
				{
					NextHopSolution nextHopSolution;
					if (nextHopSolutions.TryGetValue(routedMessageQueue.Key, out nextHopSolution) && nextHopSolution.DeliveryStatus != DeliveryStatus.Complete)
					{
						list.Add(nextHopSolution);
					}
				}
			}
			return list;
		}

		// Token: 0x060029BB RID: 10683 RVA: 0x000A6D7C File Offset: 0x000A4F7C
		private static bool TryGetMailItemSolution(TransportMailItem transportMailItem, QueueIdentity queueIdentity, out NextHopSolution solution)
		{
			solution = null;
			List<NextHopSolution> mailItemSolutions = QueueManager.GetMailItemSolutions(transportMailItem, queueIdentity);
			if (mailItemSolutions.Count == 0)
			{
				return false;
			}
			if (mailItemSolutions.Count > 1)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
			}
			solution = mailItemSolutions[0];
			return true;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000A6DBC File Offset: 0x000A4FBC
		private static bool ProcessQueueInfo(PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema> pagingEngine, ExtensibleQueueInfo queueInfo, ref int totalCount)
		{
			if (pagingEngine.ApplyFilterConditions(queueInfo))
			{
				totalCount++;
				if (pagingEngine.ApplyBookmarkConditions(queueInfo))
				{
					return pagingEngine.AddToResultSet(queueInfo);
				}
			}
			return true;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000A6DDF File Offset: 0x000A4FDF
		private static bool SuspendShadowQueue(QueueIdentity queueIdentity)
		{
			return QueueManager.InternalSuspendResumeShadowQueue(queueIdentity, true);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000A6DE8 File Offset: 0x000A4FE8
		private static bool ResumeShadowQueue(QueueIdentity queueIdentity)
		{
			return QueueManager.InternalSuspendResumeShadowQueue(queueIdentity, false);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000A6DF4 File Offset: 0x000A4FF4
		private static bool InternalSuspendResumeShadowQueue(QueueIdentity queueIdentity, bool suspend)
		{
			lock (Components.ShadowRedundancyComponent.ShadowRedundancyManager.SyncQueues)
			{
				List<ShadowMessageQueue> list = Components.ShadowRedundancyComponent.ShadowRedundancyManager.FindByQueueIdentity(queueIdentity);
				if (list.Count == 0)
				{
					return false;
				}
				if (list.Count > 1)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MULTIPLE_IDENTITY_MATCH);
				}
				list[0].Suspended = suspend;
			}
			return true;
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000A6ECC File Offset: 0x000A50CC
		private static long RedirectMessagesInMessageDepot(List<string> targetHosts)
		{
			long count = 0L;
			Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer.VisitMailItems(delegate(IMessageDepotItemWrapper item)
			{
				if (item.State != MessageDepotItemState.Poisoned && item.State != MessageDepotItemState.Expiring)
				{
					TransportMailItem transportMailItem = (TransportMailItem)item.Item.MessageObject;
					transportMailItem.MoveToHosts = targetHosts;
					count += 1L;
				}
				return true;
			});
			return count;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000A6F3C File Offset: 0x000A513C
		private static long RedirectMessagesInSubmissionQueue(List<string> targetHosts)
		{
			long count = 0L;
			Components.CategorizerComponent.SubmitMessageQueue.ForEach(delegate(IQueueItem item)
			{
				QueueManager.RedirectMessage(item as TransportMailItem, targetHosts);
				count += 1L;
			}, true);
			return count;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000A6F80 File Offset: 0x000A5180
		private static void RedirectMessage(TransportMailItem mailItem, List<string> targetHosts)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			mailItem.MoveToHosts = targetHosts;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000A6F94 File Offset: 0x000A5194
		private static void RemoveItemFromDeliveryConditionManager(NextHopSolution nextHopSolution, DeliveryPriority priority)
		{
			if (nextHopSolution != null && nextHopSolution.AccessToken != null && nextHopSolution.AccessToken.Validate(nextHopSolution.AccessToken.Condition))
			{
				nextHopSolution.AccessToken.Return(true);
				return;
			}
			if (nextHopSolution != null && nextHopSolution.DeliveryStatus != DeliveryStatus.InDelivery && nextHopSolution.CurrentCondition != null)
			{
				Components.RemoteDeliveryComponent.ConditionManager.MoveLockedToDisabled(nextHopSolution.CurrentCondition, nextHopSolution.NextHopSolutionKey);
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000A7001 File Offset: 0x000A5201
		private static void AddItemToDeliveryConditionManager(NextHopSolution nextHopSolution)
		{
			if (nextHopSolution.CurrentCondition != null)
			{
				Components.RemoteDeliveryComponent.ConditionManager.AddToLocked(nextHopSolution.CurrentCondition, nextHopSolution.NextHopSolutionKey);
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000A70CC File Offset: 0x000A52CC
		private static bool SetDeliveryMessage(MessageIdentity mailItemId, ExtensibleMessageInfo properties, bool resubmit)
		{
			bool result = false;
			RoutedMailItem routedMailItem = null;
			QueueManager.RunActionOnSolutionInDelivery(mailItemId, delegate(TransportMailItem mailItem, NextHopSolution solution)
			{
				RemoteMessageQueue queue = QueueManager.GetQueue(mailItemId);
				if (!queue.Suspended && solution.AdminActionStatus != AdminActionStatus.Suspended)
				{
					throw new QueueViewerException(QVErrorCode.QV_E_MESSAGE_NOT_SUSPENDED);
				}
				try
				{
					routedMailItem = QueueManager.FindMailItem(queue, mailItemId, resubmit);
					if (routedMailItem.UpdateProperties(properties, resubmit))
					{
						result = true;
					}
				}
				finally
				{
					if (routedMailItem != null && !result)
					{
						queue.Enqueue(routedMailItem);
					}
				}
			});
			return result;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000A7120 File Offset: 0x000A5320
		private static RemoteMessageQueue GetQueue(MessageIdentity mailItemId)
		{
			if (mailItemId.QueueIdentity.Type == QueueType.Unreachable)
			{
				return RemoteDeliveryComponent.UnreachableMessageQueue;
			}
			List<RoutedMessageQueue> list = Components.RemoteDeliveryComponent.FindByQueueIdentity(mailItemId.QueueIdentity);
			if (list.Count != 1)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INCOMPLETE_IDENTITY);
			}
			return list[0];
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000A71AC File Offset: 0x000A53AC
		private static RoutedMailItem FindMailItem(RemoteMessageQueue queue, MessageIdentity mailItemId, bool dequeue)
		{
			RoutedMailItem routedMailItem = null;
			queue.DequeueItem(delegate(IQueueItem item)
			{
				if (((RoutedMailItem)item).RecordId != mailItemId.InternalId)
				{
					return DequeueMatchResult.Continue;
				}
				routedMailItem = (RoutedMailItem)item;
				if (dequeue)
				{
					return DequeueMatchResult.DequeueAndBreak;
				}
				return DequeueMatchResult.Break;
			});
			if (routedMailItem == null)
			{
				throw new QueueViewerException(QVErrorCode.QV_E_INVALID_SERVER_DATA);
			}
			return routedMailItem;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000A7200 File Offset: 0x000A5400
		private bool UnfreezePoisonMessage(MessageIdentity mailItemId)
		{
			TransportMailItem transportMailItem = null;
			if (Components.MessageDepotComponent.Enabled)
			{
				IMessageDepotItemWrapper messageDepotItemWrapper = null;
				if (QueueManager.TryGetMailItemByIdFromMessageDepot(mailItemId.InternalId, out messageDepotItemWrapper))
				{
					IMessageDepotItem item = messageDepotItemWrapper.Item;
					transportMailItem = (TransportMailItem)item.MessageObject;
					if (!QueueManager.DeleteMessageFromMessageDepot(mailItemId.InternalId, false))
					{
						return false;
					}
				}
			}
			else
			{
				transportMailItem = this.poisonMessageQueue.Extract(mailItemId);
			}
			if (transportMailItem != null)
			{
				transportMailItem.BumpExpirationTime();
				transportMailItem.PoisonCount = 0;
				MessageTrackingLog.TrackResubmit(MessageTrackingSource.QUEUE, transportMailItem, transportMailItem, "resubmitting from poison");
				Components.CategorizerComponent.EnqueueSubmittedMessage(transportMailItem);
				ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Message {0} was removed from the poison queue and submitted to the Categorizer", mailItemId);
				return true;
			}
			return false;
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x000A72A0 File Offset: 0x000A54A0
		private bool DeletePoisonMessage(MessageIdentity mailItemId)
		{
			TransportMailItem transportMailItem = this.poisonMessageQueue.Extract(mailItemId);
			if (transportMailItem != null)
			{
				transportMailItem.Ack(AckStatus.Fail, AckReason.PoisonMessageDeletedByAdmin, transportMailItem.Recipients, null);
				MessageTrackingLog.TrackPoisonMessageDeleted(MessageTrackingSource.ADMIN, null, transportMailItem);
				transportMailItem.ReleaseFromActiveMaterializedLazy();
				ExTraceGlobals.QueuingTracer.TraceDebug<MessageIdentity>(0L, "Message {0} was deleted from the poison queue", mailItemId);
				return true;
			}
			return false;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x000A72F8 File Offset: 0x000A54F8
		private bool ReadPoisonMessageBody(MessageIdentity mailItemId, byte[] buffer, int position, int count, out int bytesRead)
		{
			bytesRead = 0;
			TransportMailItem transportMailItem = this.poisonMessageQueue[mailItemId];
			if (transportMailItem != null)
			{
				Stream stream;
				if (transportMailItem.TryCreateExportStream(out stream))
				{
					using (stream)
					{
						stream.Position = (long)position;
						bytesRead = stream.Read(buffer, 0, count);
					}
				}
				if (bytesRead > 0)
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<int, MessageIdentity>(0L, "{0} bytes of message {1} read by the admin", bytesRead, mailItemId);
				}
				return true;
			}
			return false;
		}

		// Token: 0x04001548 RID: 5448
		private const string TotalPerfCounterInstanceName = "_Total";

		// Token: 0x04001549 RID: 5449
		private static readonly ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.QueuingTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400154A RID: 5450
		private static readonly Dictionary<DeliveryPriority, int> priorityToInstanceIndexMap = new Dictionary<DeliveryPriority, int>
		{
			{
				DeliveryPriority.High,
				0
			},
			{
				DeliveryPriority.Normal,
				1
			},
			{
				DeliveryPriority.Low,
				2
			},
			{
				DeliveryPriority.None,
				3
			}
		};

		// Token: 0x0400154B RID: 5451
		private static readonly Dictionary<DeliveryPriority, int> priorityToTotalExcludingPriorityNoneInstanceIndexMap = new Dictionary<DeliveryPriority, int>
		{
			{
				DeliveryPriority.High,
				4
			},
			{
				DeliveryPriority.Normal,
				4
			},
			{
				DeliveryPriority.Low,
				4
			}
		};

		// Token: 0x0400154C RID: 5452
		private static readonly Dictionary<RiskLevel, int> riskToInstanceIndexMap = new Dictionary<RiskLevel, int>
		{
			{
				RiskLevel.High,
				5
			},
			{
				RiskLevel.Bulk,
				6
			},
			{
				RiskLevel.Normal,
				7
			},
			{
				RiskLevel.Low,
				8
			}
		};

		// Token: 0x0400154D RID: 5453
		private static readonly Dictionary<RiskLevel, int> riskToHighAndBulkRiskTotalInstanceIndexMap = new Dictionary<RiskLevel, int>
		{
			{
				RiskLevel.High,
				9
			},
			{
				RiskLevel.Bulk,
				9
			}
		};

		// Token: 0x0400154E RID: 5454
		private static readonly Dictionary<RiskLevel, int> riskToNormalAndLowRiskTotalInstanceIndexMap = new Dictionary<RiskLevel, int>
		{
			{
				RiskLevel.Normal,
				10
			},
			{
				RiskLevel.Low,
				10
			}
		};

		// Token: 0x0400154F RID: 5455
		private static readonly Dictionary<Tuple<RiskLevel, DeliveryPriority>, int> riskAndPriorityInstanceIndexMap = new Dictionary<Tuple<RiskLevel, DeliveryPriority>, int>
		{
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Normal, DeliveryPriority.Normal),
				11
			},
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Normal, DeliveryPriority.Low),
				12
			},
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Normal, DeliveryPriority.None),
				13
			},
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Low, DeliveryPriority.Normal),
				14
			},
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Low, DeliveryPriority.Low),
				15
			},
			{
				new Tuple<RiskLevel, DeliveryPriority>(RiskLevel.Low, DeliveryPriority.None),
				16
			}
		};

		// Token: 0x04001550 RID: 5456
		private static readonly string[] priorityBasedInstanceCounterNames = new string[]
		{
			Strings.HighPriority,
			Strings.NormalPriority,
			Strings.LowPriority,
			Strings.NonePriority,
			Strings.TotalExcludingPriorityNone
		};

		// Token: 0x04001551 RID: 5457
		private static readonly string[] riskBasedInstanceCounterNames = new string[]
		{
			Strings.HighRisk,
			Strings.BulkRisk,
			Strings.NormalRisk,
			Strings.LowRisk,
			Strings.HighAndBulkRisk,
			Strings.NormalAndLowRisk,
			Strings.NormalRiskNormalPriority,
			Strings.NormalRiskLowPriority,
			Strings.NormalRiskNonePriority,
			Strings.LowRiskNormalPriority,
			Strings.LowRiskLowPriority,
			Strings.LowRiskNonePriority
		};

		// Token: 0x04001552 RID: 5458
		private static ExtensibleMessageInfo[] emptyMessageInfoResult = new ExtensibleMessageInfo[0];

		// Token: 0x04001553 RID: 5459
		private static int busyUpdateAllQueues;

		// Token: 0x04001554 RID: 5460
		private static bool updateAllQueuesPendingOrBusy;

		// Token: 0x04001555 RID: 5461
		private static DateTime lastQueueUpdate = DateTime.UtcNow;

		// Token: 0x04001556 RID: 5462
		private static bool includeRiskBasedCounters = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.RiskBasedCounters.Enabled;

		// Token: 0x04001557 RID: 5463
		private static int instanceCountersLength = -1;

		// Token: 0x04001558 RID: 5464
		private PoisonMessageQueue poisonMessageQueue;

		// Token: 0x04001559 RID: 5465
		private QueuingPerfCountersInstance queuingPerfCountersTotalInstance;

		// Token: 0x0400155A RID: 5466
		private QueuingPerfCountersInstance[] queuingPerfCountersInstances;

		// Token: 0x0400155B RID: 5467
		private QueueManager.SubmissionPerfCounterWrapper submissionPerfCounterWrapper;

		// Token: 0x0400155C RID: 5468
		private QueuedRecipientsByAgePerfCountersWrapper queuedRecipientsByAge;

		// Token: 0x0400155D RID: 5469
		private Task pendingRedirectMessageTask;

		// Token: 0x020003A3 RID: 931
		private class SubmissionPerfCounterWrapper
		{
			// Token: 0x060029CF RID: 10703 RVA: 0x000A761C File Offset: 0x000A581C
			public SubmissionPerfCounterWrapper(QueuingPerfCountersInstance queuingPerfCounter, TimeSpan recentInterval, TimeSpan recentBucketSize)
			{
				this.percentMessageCompleting = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(1.0));
				this.percentMessageDeferred = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(1.0));
				this.percentMessageResubmitted = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(1.0));
				queuingPerfCounter.MessagesCompletingCategorization.RawValue = (long)((int)this.percentMessageCompleting.GetSlidingPercentage());
				this.messagesSubmittedRecently = new SlidingTotalCounter(recentInterval, recentBucketSize);
			}

			// Token: 0x060029D0 RID: 10704 RVA: 0x000A76CC File Offset: 0x000A58CC
			public void OnEnterSubmissionQueue(QueuingPerfCountersInstance queuingPerfCounter)
			{
				queuingPerfCounter.SubmissionQueueLength.Increment();
				queuingPerfCounter.MessagesSubmittedTotal.Increment();
				this.IncrementDenominator(this.percentMessageCompleting, queuingPerfCounter.MessagesCompletingCategorization);
				this.messagesSubmittedRecently.AddValue(1L);
				queuingPerfCounter.MessagesSubmittedRecently.RawValue = this.messagesSubmittedRecently.Sum;
			}

			// Token: 0x060029D1 RID: 10705 RVA: 0x000A7727 File Offset: 0x000A5927
			public void OnExpireFromSubmissionQueue(QueuingPerfCountersInstance queuingPerfCounter)
			{
				queuingPerfCounter.SubmissionQueueItemsExpiredTotal.Increment();
				this.OnLeavingCategorizer(queuingPerfCounter);
			}

			// Token: 0x060029D2 RID: 10706 RVA: 0x000A773C File Offset: 0x000A593C
			public void OnExitSubmissionQueue(QueuingPerfCountersInstance queuingPerfCounter)
			{
				queuingPerfCounter.SubmissionQueueLength.Decrement();
				this.IncrementDenominator(this.percentMessageDeferred, queuingPerfCounter.MessagesDeferredDuringCategorization);
				this.IncrementDenominator(this.percentMessageResubmitted, queuingPerfCounter.MessagesResubmittedDuringCategorization);
			}

			// Token: 0x060029D3 RID: 10707 RVA: 0x000A776E File Offset: 0x000A596E
			public void OnMessageBifurcatedInCategorizer(QueuingPerfCountersInstance queuingPerfCounter)
			{
				this.IncrementDenominator(this.percentMessageCompleting, queuingPerfCounter.MessagesCompletingCategorization);
				this.IncrementDenominator(this.percentMessageDeferred, queuingPerfCounter.MessagesDeferredDuringCategorization);
				this.IncrementDenominator(this.percentMessageResubmitted, queuingPerfCounter.MessagesResubmittedDuringCategorization);
			}

			// Token: 0x060029D4 RID: 10708 RVA: 0x000A77A6 File Offset: 0x000A59A6
			public void OnLeavingCategorizer(QueuingPerfCountersInstance queuingPerfCounter)
			{
				this.IncrementNumerator(this.percentMessageCompleting, queuingPerfCounter.MessagesCompletingCategorization);
			}

			// Token: 0x060029D5 RID: 10709 RVA: 0x000A77BA File Offset: 0x000A59BA
			public void OnMessageDeferredFromCategorizer(QueuingPerfCountersInstance queuingPerfCounter)
			{
				this.IncrementNumerator(this.percentMessageDeferred, queuingPerfCounter.MessagesDeferredDuringCategorization);
			}

			// Token: 0x060029D6 RID: 10710 RVA: 0x000A77CE File Offset: 0x000A59CE
			public void OnMessagesResubmittedFromCategorizer(QueuingPerfCountersInstance queuingPerfCounter)
			{
				this.IncrementNumerator(this.percentMessageResubmitted, queuingPerfCounter.MessagesResubmittedDuringCategorization);
			}

			// Token: 0x060029D7 RID: 10711 RVA: 0x000A77E2 File Offset: 0x000A59E2
			public void OnTimedUpdate(QueuingPerfCountersInstance queuingPerfCounter)
			{
				queuingPerfCounter.MessagesSubmittedRecently.RawValue = this.messagesSubmittedRecently.Sum;
			}

			// Token: 0x060029D8 RID: 10712 RVA: 0x000A77FC File Offset: 0x000A59FC
			private void IncrementDenominator(SlidingPercentageCounter slidingCounter, ExPerformanceCounter perfCounter)
			{
				int val = (int)slidingCounter.AddDenominator(1L);
				perfCounter.RawValue = (long)Math.Min(val, 1000);
			}

			// Token: 0x060029D9 RID: 10713 RVA: 0x000A7828 File Offset: 0x000A5A28
			private void IncrementNumerator(SlidingPercentageCounter slidingCounter, ExPerformanceCounter perfCounter)
			{
				int val = (int)slidingCounter.AddNumerator(1L);
				perfCounter.RawValue = (long)Math.Min(val, 1000);
			}

			// Token: 0x04001560 RID: 5472
			private const int Infinity = 1000;

			// Token: 0x04001561 RID: 5473
			private SlidingPercentageCounter percentMessageCompleting;

			// Token: 0x04001562 RID: 5474
			private SlidingPercentageCounter percentMessageDeferred;

			// Token: 0x04001563 RID: 5475
			private SlidingPercentageCounter percentMessageResubmitted;

			// Token: 0x04001564 RID: 5476
			private SlidingTotalCounter messagesSubmittedRecently;
		}

		// Token: 0x020003A4 RID: 932
		private abstract class MessageFilter<T>
		{
			// Token: 0x060029DA RID: 10714 RVA: 0x000A7851 File Offset: 0x000A5A51
			protected MessageFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine)
			{
				if (messageInfoFactory == null)
				{
					throw new ArgumentNullException("messageInfoFactory");
				}
				if (pagingEngine == null)
				{
					throw new ArgumentNullException("pagingEngine");
				}
				this.messageInfoFactory = messageInfoFactory;
				this.pagingEngine = pagingEngine;
			}

			// Token: 0x17000C9F RID: 3231
			// (get) Token: 0x060029DB RID: 10715 RVA: 0x000A7883 File Offset: 0x000A5A83
			public PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> PagingEngine
			{
				get
				{
					return this.pagingEngine;
				}
			}

			// Token: 0x17000CA0 RID: 3232
			// (get) Token: 0x060029DC RID: 10716 RVA: 0x000A788B File Offset: 0x000A5A8B
			// (set) Token: 0x060029DD RID: 10717 RVA: 0x000A7893 File Offset: 0x000A5A93
			public int MatchCount
			{
				get
				{
					return this.matchCount;
				}
				protected set
				{
					this.matchCount = value;
				}
			}

			// Token: 0x17000CA1 RID: 3233
			// (get) Token: 0x060029DE RID: 10718 RVA: 0x000A789C File Offset: 0x000A5A9C
			// (set) Token: 0x060029DF RID: 10719 RVA: 0x000A78A4 File Offset: 0x000A5AA4
			public int TotalCount
			{
				get
				{
					return this.totalCount;
				}
				protected set
				{
					this.totalCount = value;
				}
			}

			// Token: 0x17000CA2 RID: 3234
			// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000A78AD File Offset: 0x000A5AAD
			protected MessageInfoFactory MessageInfoFactory
			{
				get
				{
					return this.messageInfoFactory;
				}
			}

			// Token: 0x17000CA3 RID: 3235
			// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000A78B5 File Offset: 0x000A5AB5
			// (set) Token: 0x060029E2 RID: 10722 RVA: 0x000A78BD File Offset: 0x000A5ABD
			protected ExtensibleMessageInfo MessageInfoToRecycle
			{
				get
				{
					return this.messageInfoToRecycle;
				}
				set
				{
					this.messageInfoToRecycle = value;
				}
			}

			// Token: 0x060029E3 RID: 10723 RVA: 0x000A78C8 File Offset: 0x000A5AC8
			public virtual bool Visit(T item)
			{
				if (item == null)
				{
					return true;
				}
				this.TotalCount++;
				ExtensibleMessageInfo extensibleMessageInfo = this.CreateMessageInfo(item);
				if (extensibleMessageInfo == null)
				{
					return true;
				}
				this.RegisterForRecycling(extensibleMessageInfo);
				if (this.PagingEngine.ApplyFilterConditions(extensibleMessageInfo))
				{
					this.MatchCount++;
					if (this.PagingEngine.ApplyBookmarkConditions(extensibleMessageInfo))
					{
						return this.AddToResultSetAndBlockRecycling(extensibleMessageInfo);
					}
				}
				return true;
			}

			// Token: 0x060029E4 RID: 10724 RVA: 0x000A7934 File Offset: 0x000A5B34
			public void Visit(IEnumerable<T> items)
			{
				if (items == null)
				{
					return;
				}
				foreach (T item in items)
				{
					if (!this.Visit(item))
					{
						break;
					}
				}
			}

			// Token: 0x060029E5 RID: 10725
			protected abstract ExtensibleMessageInfo CreateMessageInfo(T item);

			// Token: 0x060029E6 RID: 10726 RVA: 0x000A7984 File Offset: 0x000A5B84
			protected bool AddToResultSetAndBlockRecycling(ExtensibleMessageInfo messageInfo)
			{
				bool result = this.PagingEngine.AddToResultSet(messageInfo);
				this.messageInfoToRecycle = null;
				return result;
			}

			// Token: 0x060029E7 RID: 10727 RVA: 0x000A79A6 File Offset: 0x000A5BA6
			protected void RegisterForRecycling(ExtensibleMessageInfo messageInfo)
			{
				if (messageInfo != null)
				{
					if (this.messageInfoToRecycle == null)
					{
						this.messageInfoToRecycle = messageInfo;
						return;
					}
					if (!object.ReferenceEquals(messageInfo, this.messageInfoToRecycle))
					{
						throw new InvalidOperationException("Available message info instance was not recycled.");
					}
				}
			}

			// Token: 0x04001565 RID: 5477
			private MessageInfoFactory messageInfoFactory;

			// Token: 0x04001566 RID: 5478
			private PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine;

			// Token: 0x04001567 RID: 5479
			private ExtensibleMessageInfo messageInfoToRecycle;

			// Token: 0x04001568 RID: 5480
			private int totalCount;

			// Token: 0x04001569 RID: 5481
			private int matchCount;
		}

		// Token: 0x020003A5 RID: 933
		private class MessageDepotItemFilter : QueueManager.MessageFilter<IMessageDepotItemWrapper>
		{
			// Token: 0x060029E8 RID: 10728 RVA: 0x000A79D4 File Offset: 0x000A5BD4
			public MessageDepotItemFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine) : base(messageInfoFactory, pagingEngine)
			{
			}

			// Token: 0x060029E9 RID: 10729 RVA: 0x000A79DE File Offset: 0x000A5BDE
			public override bool Visit(IMessageDepotItemWrapper item)
			{
				return item == null || base.Visit(item);
			}

			// Token: 0x060029EA RID: 10730 RVA: 0x000A79EC File Offset: 0x000A5BEC
			protected override ExtensibleMessageInfo CreateMessageInfo(IMessageDepotItemWrapper item)
			{
				return base.MessageInfoFactory.NewMessageDepotItemMessageInfo(item, base.MessageInfoToRecycle);
			}
		}

		// Token: 0x020003A6 RID: 934
		private sealed class PoisonMessageDepotItemFilter : QueueManager.MessageDepotItemFilter
		{
			// Token: 0x060029EB RID: 10731 RVA: 0x000A7A00 File Offset: 0x000A5C00
			public PoisonMessageDepotItemFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine) : base(messageInfoFactory, pagingEngine)
			{
			}

			// Token: 0x060029EC RID: 10732 RVA: 0x000A7A0A File Offset: 0x000A5C0A
			public override bool Visit(IMessageDepotItemWrapper item)
			{
				return item == null || item.Item == null || item.State != MessageDepotItemState.Poisoned || item.Item.Stage != MessageDepotItemStage.Submission || base.Visit(item);
			}
		}

		// Token: 0x020003A7 RID: 935
		private sealed class SubmissionMessageDepotItemFilter : QueueManager.MessageDepotItemFilter
		{
			// Token: 0x060029ED RID: 10733 RVA: 0x000A7A36 File Offset: 0x000A5C36
			public SubmissionMessageDepotItemFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine) : base(messageInfoFactory, pagingEngine)
			{
			}

			// Token: 0x060029EE RID: 10734 RVA: 0x000A7A40 File Offset: 0x000A5C40
			public override bool Visit(IMessageDepotItemWrapper itemWrapper)
			{
				return itemWrapper == null || itemWrapper.Item == null || itemWrapper.Item.Stage != MessageDepotItemStage.Submission || (itemWrapper.State != MessageDepotItemState.Ready && itemWrapper.State != MessageDepotItemState.Deferred && itemWrapper.State != MessageDepotItemState.Expiring && itemWrapper.State != MessageDepotItemState.Processing && itemWrapper.State != MessageDepotItemState.Suspended) || base.Visit(itemWrapper);
			}
		}

		// Token: 0x020003A8 RID: 936
		private sealed class CatogorizerMessageFilter : QueueManager.MessageFilter<CategorizerItem>
		{
			// Token: 0x060029EF RID: 10735 RVA: 0x000A7A9A File Offset: 0x000A5C9A
			public CatogorizerMessageFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine) : base(messageInfoFactory, pagingEngine)
			{
			}

			// Token: 0x060029F0 RID: 10736 RVA: 0x000A7AA4 File Offset: 0x000A5CA4
			public override bool Visit(CategorizerItem item)
			{
				return item == null || item.TransportMailItem == null || item.TransportMailItem.RecordId == 0L || base.Visit(item);
			}

			// Token: 0x060029F1 RID: 10737 RVA: 0x000A7AC9 File Offset: 0x000A5CC9
			protected override ExtensibleMessageInfo CreateMessageInfo(CategorizerItem item)
			{
				return base.MessageInfoFactory.NewCategorizerMessageInfo(item, base.MessageInfoToRecycle);
			}
		}

		// Token: 0x020003A9 RID: 937
		private sealed class PoisonMessageFilter : QueueManager.MessageFilter<TransportMailItem>
		{
			// Token: 0x060029F2 RID: 10738 RVA: 0x000A7ADD File Offset: 0x000A5CDD
			public PoisonMessageFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine) : base(messageInfoFactory, pagingEngine)
			{
			}

			// Token: 0x060029F3 RID: 10739 RVA: 0x000A7AE7 File Offset: 0x000A5CE7
			protected override ExtensibleMessageInfo CreateMessageInfo(TransportMailItem item)
			{
				return base.MessageInfoFactory.NewPoisonMessageInfo(item, base.MessageInfoToRecycle);
			}
		}

		// Token: 0x020003AA RID: 938
		private sealed class MultiSolutionMessageFilter : QueueManager.MessageFilter<TransportMailItem>
		{
			// Token: 0x060029F4 RID: 10740 RVA: 0x000A7AFB File Offset: 0x000A5CFB
			public MultiSolutionMessageFilter(MessageInfoFactory messageInfoFactory, PagingEngine<ExtensibleMessageInfo, ExtensibleMessageInfoSchema> pagingEngine, IEnumerable<NextHopSolution> nextHopSolutions, bool processShadowSolutions) : base(messageInfoFactory, pagingEngine)
			{
				this.nextHopSolutions = nextHopSolutions;
				this.processShadowSolutions = processShadowSolutions;
			}

			// Token: 0x060029F5 RID: 10741 RVA: 0x000A7B14 File Offset: 0x000A5D14
			public override bool Visit(TransportMailItem mailItem)
			{
				if (mailItem == null)
				{
					return true;
				}
				if (mailItem.IsHeartbeat)
				{
					return true;
				}
				base.TotalCount++;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				IEnumerable<NextHopSolution> enumerable = this.nextHopSolutions ?? mailItem.NextHopSolutions.Values;
				foreach (NextHopSolution nextHopSolution in enumerable)
				{
					bool flag4 = false;
					DeliveryType deliveryType = nextHopSolution.NextHopSolutionKey.NextHopType.DeliveryType;
					ExtensibleMessageInfo extensibleMessageInfo;
					if (deliveryType == DeliveryType.ShadowRedundancy)
					{
						if (!this.processShadowSolutions)
						{
							continue;
						}
						extensibleMessageInfo = base.MessageInfoFactory.NewShadowMessageInfo(mailItem, nextHopSolution, base.MessageInfoToRecycle);
					}
					else
					{
						if (this.processShadowSolutions)
						{
							continue;
						}
						extensibleMessageInfo = base.MessageInfoFactory.NewMessageInfo(mailItem, nextHopSolution, base.MessageInfoToRecycle);
					}
					if (extensibleMessageInfo != null)
					{
						base.RegisterForRecycling(extensibleMessageInfo);
						if (flag || base.PagingEngine.ApplyFilterConditions(extensibleMessageInfo, out flag4))
						{
							base.MatchCount++;
							flag = base.PagingEngine.FilterUsesOnlyBasicFields;
							bool flag5 = true;
							if (!flag3)
							{
								if (flag2 || base.PagingEngine.ApplyBookmarkConditions(extensibleMessageInfo, out flag5))
								{
									flag2 = flag5;
									if (!base.AddToResultSetAndBlockRecycling(extensibleMessageInfo))
									{
										return false;
									}
								}
								else
								{
									flag3 = flag5;
								}
							}
						}
						else if (flag4)
						{
							break;
						}
					}
				}
				return true;
			}

			// Token: 0x060029F6 RID: 10742 RVA: 0x000A7C78 File Offset: 0x000A5E78
			protected override ExtensibleMessageInfo CreateMessageInfo(TransportMailItem item)
			{
				throw new NotSupportedException("This method should not be called.");
			}

			// Token: 0x0400156A RID: 5482
			private readonly bool processShadowSolutions;

			// Token: 0x0400156B RID: 5483
			private IEnumerable<NextHopSolution> nextHopSolutions;
		}

		// Token: 0x020003AB RID: 939
		private class QueueUpdateBlockedException : Exception
		{
			// Token: 0x060029F7 RID: 10743 RVA: 0x000A7C84 File Offset: 0x000A5E84
			public QueueUpdateBlockedException(string message) : base(message)
			{
			}
		}
	}
}
