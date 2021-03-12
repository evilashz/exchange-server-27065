using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000045 RID: 69
	internal sealed class MessageIdConverter : IdConverter
	{
		// Token: 0x060002BC RID: 700 RVA: 0x00018048 File Offset: 0x00016248
		internal MessageIdConverter() : base(PropertyTag.Mid, CoreItemSchema.Id)
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001805A File Offset: 0x0001625A
		protected override long CreateClientId(StoreSession session, StoreId id)
		{
			return session.IdConverter.GetMidFromMessageId(StoreId.GetStoreObjectId(id));
		}
	}
}
