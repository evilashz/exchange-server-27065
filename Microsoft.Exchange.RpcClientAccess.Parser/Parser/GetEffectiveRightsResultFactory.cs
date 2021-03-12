using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DD RID: 221
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetEffectiveRightsResultFactory : StandardResultFactory
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		internal GetEffectiveRightsResultFactory() : base(RopId.GetEffectiveRights)
		{
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		public RopResult CreateSuccessfulResult(Rights effectiveRights)
		{
			return new SuccessfulGetEffectiveRightsResult(effectiveRights);
		}
	}
}
