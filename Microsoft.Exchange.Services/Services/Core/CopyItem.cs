using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002B8 RID: 696
	internal sealed class CopyItem : MoveCopyItemCommandBase
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x0005B7FC File Offset: 0x000599FC
		public CopyItem(CallContext callContext, CopyItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0005B806 File Offset: 0x00059A06
		protected override BaseInfoResponse CreateResponse()
		{
			return new CopyItemResponse();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0005B810 File Offset: 0x00059A10
		protected override AggregateOperationResult DoOperation(StoreSession destinationSession, StoreSession sourceSession, StoreId sourceId)
		{
			return sourceSession.Copy(destinationSession, this.destinationFolder.Id, sourceSession == destinationSession, new StoreId[]
			{
				sourceId
			});
		}
	}
}
