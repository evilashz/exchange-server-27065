using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000127 RID: 295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetMessageStatusResultFactory : StandardResultFactory
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00010D4E File Offset: 0x0000EF4E
		internal SetMessageStatusResultFactory() : base(RopId.SetMessageStatus)
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00010D58 File Offset: 0x0000EF58
		public RopResult CreateSuccessfulResult(MessageStatusFlags oldStatus)
		{
			return new SuccessfulSetMessageStatusResult(oldStatus);
		}
	}
}
