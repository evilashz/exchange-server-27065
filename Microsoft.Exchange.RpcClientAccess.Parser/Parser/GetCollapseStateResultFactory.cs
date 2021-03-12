using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000DA RID: 218
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetCollapseStateResultFactory : StandardResultFactory
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F137 File Offset: 0x0000D337
		internal GetCollapseStateResultFactory() : base(RopId.GetCollapseState)
		{
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F141 File Offset: 0x0000D341
		public RopResult CreateSuccessfulResult(byte[] collapseState)
		{
			return new SuccessfulGetCollapseStateResult(collapseState);
		}
	}
}
