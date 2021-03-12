using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000326 RID: 806
	internal sealed class SubmitMessageQueue : TransportMessageQueue, ILockableQueue, IQueueQuotaObservableComponent
	{
		// Token: 0x060022C9 RID: 8905 RVA: 0x0008398C File Offset: 0x00081B8C
		private SubmitMessageQueue(RoutedQueueBase queueStorage) : base(queueStorage, PriorityBehaviour.RoundRobin)
		{
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x00083996 File Offset: 0x00081B96
		public static SubmitMessageQueue Instance
		{
			get
			{
				return SubmitMessageQueue.instance;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x0008399D File Offset: 0x00081B9D
		public NextHopSolutionKey Key
		{
			get
			{
				return NextHopSolutionKey.Submission;
			}
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000839A4 File Offset: 0x00081BA4
		public static void CreateInstance()
		{
			if (SubmitMessageQueue.instance != null)
			{
				throw new InvalidOperationException("Submission queue already created");
			}
			RoutedQueueBase orAddQueue = Components.MessagingDatabase.GetOrAddQueue(NextHopSolutionKey.Submission);
			SubmitMessageQueue.instance = new SubmitMessageQueue(orAddQueue);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000839DE File Offset: 0x00081BDE
		public static void LoadInstance(RoutedQueueBase queueStorage)
		{
			if (SubmitMessageQueue.instance != null)
			{
				throw new InvalidOperationException("Submission queue already created");
			}
			SubmitMessageQueue.instance = new SubmitMessageQueue(queueStorage);
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060022CE RID: 8910 RVA: 0x00083A00 File Offset: 0x00081C00
		// (remove) Token: 0x060022CF RID: 8911 RVA: 0x00083A38 File Offset: 0x00081C38
		public event Action<TransportMailItem> OnAcquire;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060022D0 RID: 8912 RVA: 0x00083A70 File Offset: 0x00081C70
		// (remove) Token: 0x060022D1 RID: 8913 RVA: 0x00083AA8 File Offset: 0x00081CA8
		public event Action<TransportMailItem> OnRelease;

		// Token: 0x17000B03 RID: 2819
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x00083B00 File Offset: 0x00081D00
		public override bool Suspended
		{
			set
			{
				base.Suspended = value;
				if (this.Suspended)
				{
					base.RelockAll("Queue suspended", delegate(IQueueItem item)
					{
						TransportMailItem transportMailItem = (TransportMailItem)item;
						return transportMailItem.AccessToken != null;
					});
					return;
				}
				if (base.ActiveCount > 0 || base.LockedCount > 0)
				{
					this.DataAvailable();
				}
			}
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x00083B60 File Offset: 0x00081D60
		public new void Enqueue(IQueueItem item)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			if (item.DeferUntil == DateTime.MinValue)
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.SubmissionQueue, transportMailItem.LatencyTracker);
			}
			base.Enqueue(item);
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00083B9C File Offset: 0x00081D9C
		public new IQueueItem Dequeue()
		{
			IQueueItem queueItem;
			if (this.conditionManager != null)
			{
				queueItem = this.conditionManager.DequeueNext();
				if (queueItem != null)
				{
					TransportMailItem transportMailItem = (TransportMailItem)queueItem;
					transportMailItem.ThrottlingContext.AddMemoryCost(new ByteQuantifiedSize((ulong)transportMailItem.MimeSize));
				}
			}
			else
			{
				queueItem = this.DequeueInternal();
			}
			return queueItem;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00083BE8 File Offset: 0x00081DE8
		public ILockableItem DequeueInternal()
		{
			TransportMailItem transportMailItem = (TransportMailItem)base.Dequeue();
			if (transportMailItem != null)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.SubmissionQueue, transportMailItem.LatencyTracker);
				this.InternalOnDequeue(transportMailItem, true);
			}
			return transportMailItem;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00083C1B File Offset: 0x00081E1B
		public ILockableItem DequeueInternal(DeliveryPriority priority)
		{
			throw new InvalidOperationException("Submission queue does not support dequeuing by specifying priority.");
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00083C27 File Offset: 0x00081E27
		public void Lock(ILockableItem item, WaitCondition condition, string lockReason, int dehydrateThreshold)
		{
			item.LockExpirationTime = DateTime.UtcNow + Components.TransportAppConfig.ThrottlingConfig.LockExpirationInterval;
			base.Lock(item, condition, lockReason, dehydrateThreshold);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00083C90 File Offset: 0x00081E90
		public TransportMailItem GetMailItemById(long mailItemId)
		{
			TransportMailItem mailItem = null;
			base.DequeueItem(delegate(IQueueItem item)
			{
				TransportMailItem transportMailItem = (TransportMailItem)item;
				if (transportMailItem != null && transportMailItem.RecordId == mailItemId)
				{
					mailItem = transportMailItem;
					return DequeueMatchResult.Break;
				}
				return DequeueMatchResult.Continue;
			}, false);
			return mailItem;
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00083CCC File Offset: 0x00081ECC
		public bool SuspendMailItem(long internalMessageId)
		{
			TransportMailItem transportMailItem = this.DequeueTransportMailItem(internalMessageId, false);
			if (transportMailItem != null)
			{
				transportMailItem.Suspend();
				SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
				base.Enqueue(transportMailItem);
				ExTraceGlobals.QueuingTracer.TraceDebug<long>(0L, "Submission queue message {0} has been frozen by the admin.", internalMessageId);
			}
			return transportMailItem != null;
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x00083D14 File Offset: 0x00081F14
		public bool ResumeMailItem(long internalMessageId)
		{
			TransportMailItem transportMailItem = this.DequeueTransportMailItem(internalMessageId, true);
			if (transportMailItem != null)
			{
				transportMailItem.Resume();
				base.Enqueue(transportMailItem);
				ExTraceGlobals.QueuingTracer.TraceDebug<long>(0L, "Submission queue message {0} has been unfrozen by the admin.", internalMessageId);
			}
			return transportMailItem != null;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00083D54 File Offset: 0x00081F54
		public bool DeleteMailItem(long internalMessageId, bool withNDR)
		{
			TransportMailItem transportMailItem = this.DequeueTransportMailItem(internalMessageId, false);
			if (transportMailItem != null)
			{
				if (withNDR)
				{
					if (transportMailItem.ADRecipientCache == null)
					{
						ADOperationResult adoperationResult = MultiTenantTransport.TryCreateADRecipientCache(transportMailItem);
						if (!adoperationResult.Succeeded)
						{
							MultiTenantTransport.TraceAttributionError(string.Format("Error {0} when creating recipient cache for message {1}. Falling back to first org", adoperationResult.Exception, MultiTenantTransport.ToString(transportMailItem)), new object[0]);
							MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(transportMailItem, OrganizationId.ForestWideOrgId);
						}
					}
					MailRecipient mailRecipient = transportMailItem.Recipients[0];
					mailRecipient.DsnNeeded = DsnFlags.Failure;
					Components.DsnGenerator.GenerateDSNs(transportMailItem, transportMailItem.Recipients);
				}
				transportMailItem.Ack(AckStatus.Fail, AckReason.MessageDeletedByAdmin, transportMailItem.Recipients, null);
				MessageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.ADMIN, transportMailItem, transportMailItem.Recipients, null);
				SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
				transportMailItem.ReleaseFromActive();
				transportMailItem.CommitLazy();
				ExTraceGlobals.QueuingTracer.TraceDebug<long, bool>(0L, "Submission queue message {0} has been deleted by the admin, NDR={1}", internalMessageId, withNDR);
			}
			return transportMailItem != null;
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00083E28 File Offset: 0x00082028
		public bool ReadMessageBody(long internalMessageId, byte[] buffer, int position, int count, out int bytesRead, out bool foundNotSuspended)
		{
			bytesRead = 0;
			TransportMailItem transportMailItem = this.DequeueTransportMailItem(internalMessageId, true, true, out foundNotSuspended);
			if (foundNotSuspended)
			{
				return false;
			}
			if (transportMailItem != null)
			{
				Stream stream;
				if (ExportStream.TryCreate(transportMailItem, transportMailItem.Recipients, false, out stream))
				{
					using (stream)
					{
						stream.Position = (long)position;
						bytesRead = stream.Read(buffer, 0, count);
					}
				}
				base.Enqueue(transportMailItem);
				if (bytesRead > 0)
				{
					ExTraceGlobals.QueuingTracer.TraceDebug<int, long>(0L, "Exported {0} bytes of message {1} read by the admin.", bytesRead, internalMessageId);
				}
			}
			return transportMailItem != null;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x00083EBC File Offset: 0x000820BC
		public bool UpdateMailItem(long internalMessageId, ExtensibleMessageInfo properties, out bool errorNotSuspended)
		{
			errorNotSuspended = false;
			TransportMailItem transportMailItem = this.DequeueTransportMailItem(internalMessageId, false);
			if (transportMailItem == null)
			{
				return false;
			}
			bool result;
			try
			{
				if (transportMailItem.Recipients[0].AdminActionStatus != AdminActionStatus.SuspendedInSubmissionQueue && !this.Suspended)
				{
					errorNotSuspended = true;
					result = false;
				}
				else
				{
					if (properties.OutboundIPPool > 0)
					{
						foreach (MailRecipient mailRecipient in transportMailItem.Recipients.AllUnprocessed)
						{
							if (properties.OutboundIPPool != mailRecipient.OutboundIPPool)
							{
								mailRecipient.OutboundIPPool = properties.OutboundIPPool;
							}
						}
						ExTraceGlobals.QueuingTracer.TraceDebug<long>(0L, "Submission queue: properties of message {0} have been updated by the admin.", internalMessageId);
					}
					result = true;
				}
			}
			finally
			{
				base.Enqueue(transportMailItem);
			}
			return result;
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00083FB4 File Offset: 0x000821B4
		public bool VisitMailItems(Func<TransportMailItem, bool> visitor)
		{
			bool visitedAll = true;
			base.DequeueItem(delegate(IQueueItem item)
			{
				if (!visitor((TransportMailItem)item))
				{
					visitedAll = false;
					return DequeueMatchResult.Break;
				}
				return DequeueMatchResult.Continue;
			}, false);
			return visitedAll;
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00083FEF File Offset: 0x000821EF
		public new void TimedUpdate()
		{
			base.UpdateQueueRates();
			base.TimedUpdate();
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00083FFD File Offset: 0x000821FD
		public override bool IsInterestingQueueToLog()
		{
			return base.IsInterestingQueueToLog() || base.TotalCount > 0;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00084012 File Offset: 0x00082212
		internal void SetConditionManager(SingleQueueWaitConditionManager conditionManager)
		{
			if (this.conditionManager != null)
			{
				throw new InvalidOperationException("Overwriting existing condition map");
			}
			this.conditionManager = conditionManager;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0008402E File Offset: 0x0008222E
		protected override void DataAvailable()
		{
			if (Components.IsActive)
			{
				Components.CategorizerComponent.DataAvail();
			}
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x00084041 File Offset: 0x00082241
		protected override void ItemEnqueued(IQueueItem item)
		{
			this.InternalOnEnqueue((TransportMailItem)item);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x00084050 File Offset: 0x00082250
		protected override void ItemExpired(IQueueItem item, bool wasEnqueued)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			ExTraceGlobals.SchedulerTracer.TraceDebug<long>((long)this.GetHashCode(), "Message with ID {0} has expired in the submission queue.", transportMailItem.RecordId);
			if (transportMailItem.ADRecipientCache == null)
			{
				ADOperationResult adoperationResult = MultiTenantTransport.TryCreateADRecipientCache(transportMailItem);
				if (!adoperationResult.Succeeded)
				{
					MultiTenantTransport.TraceAttributionError(string.Format("Error {0} when creating recipient cache for message {1}. Falling back to first org", adoperationResult.Exception, MultiTenantTransport.ToString(transportMailItem)), new object[0]);
					MultiTenantTransport.UpdateADRecipientCacheAndOrganizationScope(transportMailItem, OrganizationId.ForestWideOrgId);
				}
			}
			this.ItemRemoved(item, wasEnqueued);
			CategorizerComponent.AckAllRecipients(transportMailItem, AckStatus.Fail, AckReason.MessageExpired);
			Components.OrarGenerator.GenerateOrarMessage(transportMailItem, true);
			Components.DsnGenerator.GenerateDSNs(transportMailItem);
			LatencyFormatter latencyFormatter = new LatencyFormatter(transportMailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, true);
			MessageTrackingLog.TrackRelayedAndFailed(MessageTrackingSource.QUEUE, "Queue=Submission", transportMailItem, transportMailItem.Recipients, null, SmtpResponse.Empty, latencyFormatter);
			transportMailItem.ReleaseFromActiveMaterializedLazy();
			Components.QueueManager.UpdatePerfCountersOnExpireFromSubmissionQueue(transportMailItem);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00084135 File Offset: 0x00082335
		protected override void ItemLockExpired(IQueueItem item)
		{
			this.ItemRemoved(item);
			Components.QueueManager.UpdatePerfCountersOnLockExpiredInSubmissionQueue();
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00084148 File Offset: 0x00082348
		protected override bool ItemDeferred(IQueueItem item)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemDeferred(transportMailItem, this, item.DeferUntil);
			SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
			this.InternalOnEnqueue(transportMailItem);
			item.Update();
			return true;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00084188 File Offset: 0x00082388
		protected override bool ItemActivated(IQueueItem item)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			ExTraceGlobals.SchedulerTracer.TraceDebug<long>((long)this.GetHashCode(), "Message with ID {0} has been activated in the submission queue.", transportMailItem.RecordId);
			TransportMailItem transportMailItem2 = item as TransportMailItem;
			if (transportMailItem2 != null && transportMailItem2.DeferReason != DeferReason.None)
			{
				LatencyTracker.EndTrackLatency(TransportMailItem.GetDeferLatencyComponent(transportMailItem2.DeferReason), transportMailItem2.LatencyTracker);
			}
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Retry)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
			LatencyTracker.BeginTrackLatency(LatencyComponent.SubmissionQueue, transportMailItem.LatencyTracker);
			this.InternalOnDequeue(item, true);
			return true;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00084240 File Offset: 0x00082440
		protected override bool ItemLocked(IQueueItem item, WaitCondition condition, string lockReason)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			LatencyTracker.BeginTrackLatency(LatencyComponent.CategorizerLocking, transportMailItem.LatencyTracker);
			transportMailItem.LockReason = lockReason;
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Ready)
				{
					mailRecipient.Status = Status.Locked;
				}
			}
			SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
			this.InternalOnEnqueue(transportMailItem);
			return true;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000842C0 File Offset: 0x000824C0
		protected override bool ItemUnlocked(IQueueItem item, AccessToken token)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			transportMailItem.AccessToken = token;
			transportMailItem.LockReason = null;
			transportMailItem.LockExpirationTime = DateTimeOffset.MinValue;
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Locked)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
			LatencyTracker.EndTrackLatency(LatencyComponent.CategorizerLocking, transportMailItem.LatencyTracker);
			return true;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00084348 File Offset: 0x00082548
		protected override void ItemRelocked(IQueueItem item, string lockReason, out WaitCondition condition)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			LatencyTracker.BeginTrackLatency(LatencyComponent.CategorizerLocking, transportMailItem.LatencyTracker);
			SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
			transportMailItem.LockReason = lockReason;
			transportMailItem.LockExpirationTime = DateTime.UtcNow + Components.TransportAppConfig.ThrottlingConfig.LockExpirationInterval;
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Ready)
				{
					mailRecipient.Status = Status.Locked;
				}
			}
			this.conditionManager.AddToWaitlist(transportMailItem.CurrentCondition);
			condition = transportMailItem.CurrentCondition;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000843FC File Offset: 0x000825FC
		protected override void ItemRemoved(IQueueItem item)
		{
			this.ItemRemoved(item, true);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00084408 File Offset: 0x00082608
		protected override void ItemDehydrated(IQueueItem item)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			transportMailItem.CommitLazyAndDehydrateMessageIfPossible(Breadcrumb.DehydrateOnMailItemLocked);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00084428 File Offset: 0x00082628
		private void ItemRemoved(IQueueItem item, bool wasEnqueued)
		{
			TransportMailItem transportMailItem = (TransportMailItem)item;
			if (transportMailItem.AccessToken != null)
			{
				SubmitMessageQueue.ReturnTokenIfPresent(transportMailItem);
			}
			else if (transportMailItem.CurrentCondition != null)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.CategorizerLocking, transportMailItem.LatencyTracker);
				this.conditionManager.CleanupItem(transportMailItem.CurrentCondition);
			}
			else
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.SubmissionQueue, transportMailItem.LatencyTracker);
			}
			foreach (MailRecipient mailRecipient in transportMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Locked)
				{
					mailRecipient.Status = Status.Ready;
				}
			}
			if (wasEnqueued)
			{
				this.InternalOnDequeue(transportMailItem, false);
			}
			transportMailItem.CurrentCondition = null;
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000844E0 File Offset: 0x000826E0
		private void InternalOnEnqueue(TransportMailItem item)
		{
			Interlocked.Increment(ref this.lastIncomingMessageCount);
			item.ThrottlingContext = null;
			if (this.OnAcquire != null)
			{
				this.OnAcquire(item);
			}
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0008450C File Offset: 0x0008270C
		private void InternalOnDequeue(IQueueItem item, bool clearDeferReason = true)
		{
			Interlocked.Increment(ref this.lastOutgoingMessageCount);
			TransportMailItem transportMailItem = (TransportMailItem)item;
			if (clearDeferReason)
			{
				transportMailItem.DeferReason = DeferReason.None;
			}
			if (this.OnRelease != null)
			{
				this.OnRelease(transportMailItem);
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0008454A File Offset: 0x0008274A
		private static void ReturnTokenIfPresent(TransportMailItem mailItem)
		{
			if (mailItem.AccessToken != null)
			{
				mailItem.AccessToken.Return(true);
				mailItem.AccessToken = null;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00084568 File Offset: 0x00082768
		private TransportMailItem DequeueTransportMailItem(long internalMessageId, bool deferredQueueFirst)
		{
			bool flag;
			return this.DequeueTransportMailItem(internalMessageId, false, deferredQueueFirst, out flag);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000845D8 File Offset: 0x000827D8
		private TransportMailItem DequeueTransportMailItem(long internalMessageId, bool dequeueSuspendedOnly, bool deferredQueueFirst, out bool foundNotSuspended)
		{
			bool found = false;
			IQueueItem queueItem = base.DequeueItem(delegate(IQueueItem item)
			{
				TransportMailItem transportMailItem = (TransportMailItem)item;
				if (transportMailItem == null || transportMailItem.RecordId != internalMessageId)
				{
					return DequeueMatchResult.Continue;
				}
				if (dequeueSuspendedOnly && transportMailItem.Recipients[0].AdminActionStatus != AdminActionStatus.SuspendedInSubmissionQueue)
				{
					found = true;
					return DequeueMatchResult.Break;
				}
				return DequeueMatchResult.DequeueAndBreak;
			}, deferredQueueFirst);
			foundNotSuspended = found;
			return (TransportMailItem)queueItem;
		}

		// Token: 0x0400122B RID: 4651
		private static SubmitMessageQueue instance;

		// Token: 0x0400122C RID: 4652
		private SingleQueueWaitConditionManager conditionManager;
	}
}
