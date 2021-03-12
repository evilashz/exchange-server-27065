using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000B4 RID: 180
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CollapseRowResultFactory : StandardResultFactory
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000E7DD File Offset: 0x0000C9DD
		internal CollapseRowResultFactory() : base(RopId.CollapseRow)
		{
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000E7E7 File Offset: 0x0000C9E7
		public RopResult CreateSuccessfulResult(int collapsedRowCount)
		{
			return new SuccessfulCollapseRowResult(collapsedRowCount);
		}
	}
}
