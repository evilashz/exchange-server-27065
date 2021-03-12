using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011D RID: 285
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SaveChangesMessageResultFactory : StandardResultFactory
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00010C44 File Offset: 0x0000EE44
		internal SaveChangesMessageResultFactory(byte realHandleTableIndex) : base(RopId.SaveChangesMessage)
		{
			this.realHandleTableIndex = realHandleTableIndex;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00010C55 File Offset: 0x0000EE55
		public RopResult CreateSuccessfulResult(StoreId messageId)
		{
			return new SuccessfulSaveChangesMessageResult(this.realHandleTableIndex, messageId);
		}

		// Token: 0x04000322 RID: 802
		private readonly byte realHandleTableIndex;
	}
}
