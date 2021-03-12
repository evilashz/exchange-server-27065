using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E0 RID: 224
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetMessageStatusResultFactory : StandardResultFactory
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000F1EF File Offset: 0x0000D3EF
		internal GetMessageStatusResultFactory() : base(RopId.SetMessageStatus)
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000F1F9 File Offset: 0x0000D3F9
		public RopResult CreateSuccessfulResult(MessageStatusFlags messageStatus)
		{
			return new SuccessfulGetMessageStatusResult(messageStatus);
		}
	}
}
