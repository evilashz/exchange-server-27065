using System;
using System.Collections.Generic;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000016 RID: 22
	internal interface IContentSynchronizationScope : IDisposable
	{
		// Token: 0x060000E8 RID: 232
		ExchangeId GetExchangeId(long shortTermId);

		// Token: 0x060000E9 RID: 233
		ReplId GuidToReplid(Guid guid);

		// Token: 0x060000EA RID: 234
		IdSet GetServerCnsetSeen(MapiContext operationContext, bool conversations);

		// Token: 0x060000EB RID: 235
		IEnumerable<Properties> GetChangedMessages(MapiContext operationContext, IcsState icsState);

		// Token: 0x060000EC RID: 236
		IdSet GetDeletes(MapiContext operationContext, IcsState icsState);

		// Token: 0x060000ED RID: 237
		IdSet GetSoftDeletes(MapiContext operationContext, IcsState icsState);

		// Token: 0x060000EE RID: 238
		void GetNewReadsUnreads(MapiContext operationContext, IcsState icsState, out IdSet midsetNewReads, out IdSet midsetNewUnreads, out IdSet finalCnsetRead);

		// Token: 0x060000EF RID: 239
		FastTransferMessage OpenMessage(ExchangeId mid);

		// Token: 0x060000F0 RID: 240
		PropertyGroupMapping GetPropertyGroupMapping();
	}
}
