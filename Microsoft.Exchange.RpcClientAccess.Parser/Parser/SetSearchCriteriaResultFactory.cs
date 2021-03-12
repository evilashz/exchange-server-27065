using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012C RID: 300
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetSearchCriteriaResultFactory : StandardResultFactory
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00010E73 File Offset: 0x0000F073
		internal SetSearchCriteriaResultFactory() : base(RopId.SetSearchCriteria)
		{
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00010E7D File Offset: 0x0000F07D
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetSearchCriteria, ErrorCode.None);
		}
	}
}
