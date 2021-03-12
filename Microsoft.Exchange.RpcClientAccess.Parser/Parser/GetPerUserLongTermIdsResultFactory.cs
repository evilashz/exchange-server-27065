using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000E6 RID: 230
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetPerUserLongTermIdsResultFactory : StandardResultFactory
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000F283 File Offset: 0x0000D483
		internal GetPerUserLongTermIdsResultFactory() : base(RopId.GetPerUserLongTermIds)
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000F28D File Offset: 0x0000D48D
		public RopResult CreateSuccessfulResult(StoreLongTermId[] longTermIds)
		{
			return new SuccessfulGetPerUserLongTermIdsResult(longTermIds);
		}
	}
}
