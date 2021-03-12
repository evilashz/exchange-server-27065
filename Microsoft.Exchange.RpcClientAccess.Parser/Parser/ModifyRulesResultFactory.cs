using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000FE RID: 254
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ModifyRulesResultFactory : StandardResultFactory
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x0000F74E File Offset: 0x0000D94E
		internal ModifyRulesResultFactory() : base(RopId.ModifyRules)
		{
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000F758 File Offset: 0x0000D958
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.ModifyRules, ErrorCode.None);
		}
	}
}
