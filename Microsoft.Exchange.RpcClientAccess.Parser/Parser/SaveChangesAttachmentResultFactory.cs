using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011C RID: 284
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SaveChangesAttachmentResultFactory : StandardResultFactory
	{
		// Token: 0x060005B1 RID: 1457 RVA: 0x00010C30 File Offset: 0x0000EE30
		internal SaveChangesAttachmentResultFactory() : base(RopId.SaveChangesAttachment)
		{
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010C3A File Offset: 0x0000EE3A
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SaveChangesAttachment, ErrorCode.None);
		}
	}
}
