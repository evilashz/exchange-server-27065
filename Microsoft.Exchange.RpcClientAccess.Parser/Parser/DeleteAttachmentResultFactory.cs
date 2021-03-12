using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C1 RID: 193
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeleteAttachmentResultFactory : StandardResultFactory
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0000EC16 File Offset: 0x0000CE16
		internal DeleteAttachmentResultFactory() : base(RopId.DeleteAttachment)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.DeleteAttachment, ErrorCode.None);
		}
	}
}
