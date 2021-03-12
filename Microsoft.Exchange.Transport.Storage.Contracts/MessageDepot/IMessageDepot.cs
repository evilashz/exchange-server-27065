using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x0200000F RID: 15
	internal interface IMessageDepot
	{
		// Token: 0x06000022 RID: 34
		void SubscribeToAddEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x06000023 RID: 35
		void UnsubscribeFromAddEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x06000024 RID: 36
		void SubscribeToActivatedEvent(MessageDepotItemStage targetStage, MessageActivatedEventHandler eventHandler);

		// Token: 0x06000025 RID: 37
		void UnsubscribeFromActivatedEvent(MessageDepotItemStage targetStage, MessageActivatedEventHandler eventHandler);

		// Token: 0x06000026 RID: 38
		void SubscribeToDeactivatedEvent(MessageDepotItemStage targetStage, MessageDeactivatedEventHandler eventHandler);

		// Token: 0x06000027 RID: 39
		void UnsubscribeFromDeactivatedEvent(MessageDepotItemStage targetStage, MessageDeactivatedEventHandler eventHandler);

		// Token: 0x06000028 RID: 40
		void SubscribeToRemovedEvent(MessageDepotItemStage targetStage, MessageRemovedEventHandler eventHandler);

		// Token: 0x06000029 RID: 41
		void UnsubscribeFromRemovedEvent(MessageDepotItemStage targetStage, MessageRemovedEventHandler eventHandler);

		// Token: 0x0600002A RID: 42
		void SubscribeToExpiredEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x0600002B RID: 43
		void UnsubscribeFromExpiredEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x0600002C RID: 44
		void SubscribeToDelayedEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x0600002D RID: 45
		void UnsubscribeFromDelayedEvent(MessageDepotItemStage targetStage, MessageEventHandler eventHandler);

		// Token: 0x0600002E RID: 46
		void Add(IMessageDepotItem item);

		// Token: 0x0600002F RID: 47
		void DeferMessage(TransportMessageId messageId, TimeSpan deferTimeSpan, AcquireToken acquireToken);

		// Token: 0x06000030 RID: 48
		AcquireResult Acquire(TransportMessageId messageId);

		// Token: 0x06000031 RID: 49
		bool TryAcquire(TransportMessageId messageId, out AcquireResult result);

		// Token: 0x06000032 RID: 50
		void Release(TransportMessageId messageId, AcquireToken token);

		// Token: 0x06000033 RID: 51
		bool TryGet(TransportMessageId messageId, out IMessageDepotItemWrapper item);

		// Token: 0x06000034 RID: 52
		IMessageDepotItemWrapper Get(TransportMessageId messageId);

		// Token: 0x06000035 RID: 53
		void DehydrateAll();
	}
}
