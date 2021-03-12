using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000119 RID: 281
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ResetTableResultFactory : StandardResultFactory
	{
		// Token: 0x0600059F RID: 1439 RVA: 0x000107DA File Offset: 0x0000E9DA
		internal ResetTableResultFactory() : base(RopId.ResetTable)
		{
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000107E7 File Offset: 0x0000E9E7
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.ResetTable, ErrorCode.None);
		}
	}
}
