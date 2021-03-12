using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000132 RID: 306
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SpoolerRulesResultFactory : StandardResultFactory
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x00010EEF File Offset: 0x0000F0EF
		internal SpoolerRulesResultFactory() : base(RopId.SpoolerRules)
		{
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00010EF9 File Offset: 0x0000F0F9
		public RopResult CreateSuccessfulResult(StoreId? folderId)
		{
			return new SuccessfulSpoolerRulesResult(folderId);
		}
	}
}
