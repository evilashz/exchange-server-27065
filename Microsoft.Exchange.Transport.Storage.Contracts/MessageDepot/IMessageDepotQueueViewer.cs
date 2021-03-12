using System;

namespace Microsoft.Exchange.Transport.MessageDepot
{
	// Token: 0x02000012 RID: 18
	internal interface IMessageDepotQueueViewer
	{
		// Token: 0x06000046 RID: 70
		void Remove(TransportMessageId messageId, bool withNdr);

		// Token: 0x06000047 RID: 71
		void Suspend(TransportMessageId messageId);

		// Token: 0x06000048 RID: 72
		void Resume(TransportMessageId messageId);

		// Token: 0x06000049 RID: 73
		bool TryGet(TransportMessageId messageId, out IMessageDepotItemWrapper item);

		// Token: 0x0600004A RID: 74
		IMessageDepotItemWrapper Get(TransportMessageId messageId);

		// Token: 0x0600004B RID: 75
		long GetCount(MessageDepotItemStage stage, MessageDepotItemState state);

		// Token: 0x0600004C RID: 76
		void VisitMailItems(Func<IMessageDepotItemWrapper, bool> visitor);
	}
}
