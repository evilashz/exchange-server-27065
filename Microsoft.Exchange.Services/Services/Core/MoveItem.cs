using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200034B RID: 843
	internal sealed class MoveItem : MoveCopyItemCommandBase
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x0007F844 File Offset: 0x0007DA44
		public MoveItem(CallContext callContext, MoveItemRequest request) : base(callContext, request)
		{
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0007F84E File Offset: 0x0007DA4E
		internal override TimeSpan? MaxExecutionTime
		{
			get
			{
				return new TimeSpan?(TimeSpan.FromMinutes(5.0));
			}
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0007F863 File Offset: 0x0007DA63
		protected override BaseInfoResponse CreateResponse()
		{
			return new MoveItemResponse();
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0007F86C File Offset: 0x0007DA6C
		protected override AggregateOperationResult DoOperation(StoreSession destinationSession, StoreSession sourceSession, StoreId sourceId)
		{
			return sourceSession.Move(destinationSession, this.destinationFolder.Id, sourceSession == destinationSession, new StoreId[]
			{
				sourceId
			});
		}
	}
}
