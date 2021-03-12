using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000104 RID: 260
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenAttachmentResultFactory : StandardResultFactory
	{
		// Token: 0x06000545 RID: 1349 RVA: 0x0000FCA1 File Offset: 0x0000DEA1
		internal OpenAttachmentResultFactory() : base(RopId.OpenAttachment)
		{
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0000FCAB File Offset: 0x0000DEAB
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulOpenAttachmentResult(serverObject);
		}
	}
}
