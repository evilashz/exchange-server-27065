using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000130 RID: 304
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SortTableResultFactory : StandardResultFactory
	{
		// Token: 0x060005E1 RID: 1505 RVA: 0x00010EC9 File Offset: 0x0000F0C9
		internal SortTableResultFactory() : base(RopId.SortTable)
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00010ED3 File Offset: 0x0000F0D3
		public RopResult CreateSuccessfulResult(TableStatus tableStatus)
		{
			return new SuccessfulSortTableResult(tableStatus);
		}
	}
}
