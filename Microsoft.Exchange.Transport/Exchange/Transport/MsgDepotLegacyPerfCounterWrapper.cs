using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000032 RID: 50
	internal sealed class MsgDepotLegacyPerfCounterWrapper
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00004E58 File Offset: 0x00003058
		public MsgDepotLegacyPerfCounterWrapper(IMessageDepot messageDepot, IMessageDepotQueueViewer messageDepotQueueViewer, TransportAppConfig.ILegacyQueueConfig queueConfig)
		{
			ArgumentValidator.ThrowIfNull("messageDepot", messageDepot);
			ArgumentValidator.ThrowIfNull("messageDepotQueueViewer", messageDepotQueueViewer);
			this.messageDepotQueueViewer = messageDepotQueueViewer;
			this.perfCountersInstance = QueuingPerfCounters.GetInstance("_Total");
			this.messagesSubmittedRecently = new SlidingTotalCounter(queueConfig.RecentPerfCounterTrackingInterval, queueConfig.RecentPerfCounterTrackingBucketSize);
			this.queuedRecipientsByAge = new QueuedRecipientsByAgePerfCountersWrapper(queueConfig.QueuedRecipientsByAgeTrackingEnabled);
			messageDepot.SubscribeToAddEvent(MessageDepotItemStage.Submission, new MessageEventHandler(this.OnMessageAdded));
			messageDepot.SubscribeToActivatedEvent(MessageDepotItemStage.Submission, new MessageActivatedEventHandler(this.OnMessageActivated));
			messageDepot.SubscribeToDeactivatedEvent(MessageDepotItemStage.Submission, new MessageDeactivatedEventHandler(this.OnMessageDeactivated));
			messageDepot.SubscribeToRemovedEvent(MessageDepotItemStage.Submission, new MessageRemovedEventHandler(this.OnMessageRemoved));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004F0C File Offset: 0x0000310C
		public void TimedUpdate()
		{
			this.perfCountersInstance.MessagesSubmittedRecently.RawValue = this.messagesSubmittedRecently.Sum;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004F29 File Offset: 0x00003129
		private void OnMessageAdded(MessageEventArgs args)
		{
			ArgumentValidator.ThrowIfNull("args", args);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper", args.ItemWrapper);
			if (args.ItemWrapper.State == MessageDepotItemState.Poisoned)
			{
				this.perfCountersInstance.PoisonQueueLength.Increment();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F68 File Offset: 0x00003168
		private void OnMessageRemoved(MessageRemovedEventArgs args)
		{
			ArgumentValidator.ThrowIfNull("args", args);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper", args.ItemWrapper);
			if (args.ItemWrapper.State == MessageDepotItemState.Poisoned)
			{
				this.perfCountersInstance.PoisonQueueLength.Decrement();
			}
			if (args.Reason == MessageRemovalReason.Expired)
			{
				this.perfCountersInstance.SubmissionQueueItemsExpiredTotal.Increment();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004FC8 File Offset: 0x000031C8
		private void OnMessageActivated(MessageActivatedEventArgs args)
		{
			ArgumentValidator.ThrowIfNull("args", args);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper", args.ItemWrapper);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper.Item", args.ItemWrapper.Item);
			IMessageDepotItem item = args.ItemWrapper.Item;
			this.perfCountersInstance.MessagesSubmittedTotal.Increment();
			this.perfCountersInstance.SubmissionQueueLength.Increment();
			this.perfCountersInstance.PoisonQueueLength.RawValue = this.messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Poisoned);
			this.messagesSubmittedRecently.AddValue(1L);
			this.perfCountersInstance.MessagesSubmittedRecently.RawValue = this.messagesSubmittedRecently.Sum;
			this.queuedRecipientsByAge.TrackEnteringSubmissionQueue((TransportMailItem)item.MessageObject);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005090 File Offset: 0x00003290
		private void OnMessageDeactivated(MessageDeactivatedEventArgs args)
		{
			ArgumentValidator.ThrowIfNull("args", args);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper", args.ItemWrapper);
			ArgumentValidator.ThrowIfNull("args.ItemWrapper.Item", args.ItemWrapper.Item);
			IMessageDepotItem item = args.ItemWrapper.Item;
			this.perfCountersInstance.SubmissionQueueLength.Decrement();
			this.perfCountersInstance.PoisonQueueLength.RawValue = this.messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Poisoned);
			TransportMailItem transportMailItem = (TransportMailItem)item.MessageObject;
			if (transportMailItem.QueuedRecipientsByAgeToken != null)
			{
				this.queuedRecipientsByAge.TrackExitingSubmissionQueue(transportMailItem);
			}
		}

		// Token: 0x04000081 RID: 129
		private const string TotalPerfCounterInstanceName = "_Total";

		// Token: 0x04000082 RID: 130
		private readonly IMessageDepotQueueViewer messageDepotQueueViewer;

		// Token: 0x04000083 RID: 131
		private readonly QueuingPerfCountersInstance perfCountersInstance;

		// Token: 0x04000084 RID: 132
		private readonly SlidingTotalCounter messagesSubmittedRecently;

		// Token: 0x04000085 RID: 133
		private readonly QueuedRecipientsByAgePerfCountersWrapper queuedRecipientsByAge;
	}
}
