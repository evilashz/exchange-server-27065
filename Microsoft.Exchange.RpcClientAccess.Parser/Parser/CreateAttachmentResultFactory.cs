using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000BC RID: 188
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateAttachmentResultFactory : StandardResultFactory
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0000EBB1 File Offset: 0x0000CDB1
		internal CreateAttachmentResultFactory() : base(RopId.CreateAttachment)
		{
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000EBBB File Offset: 0x0000CDBB
		public RopResult CreateSuccessfulResult(IServerObject serverObject, uint attachmentNumber)
		{
			return new SuccessfulCreateAttachmentResult(serverObject, attachmentNumber);
		}
	}
}
