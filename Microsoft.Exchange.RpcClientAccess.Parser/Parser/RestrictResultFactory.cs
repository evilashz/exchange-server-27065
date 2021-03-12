using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011A RID: 282
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RestrictResultFactory : StandardResultFactory
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x000107F4 File Offset: 0x0000E9F4
		internal RestrictResultFactory() : base(RopId.Restrict)
		{
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000107FE File Offset: 0x0000E9FE
		public RopResult CreateSuccessfulResult(TableStatus tableStatus)
		{
			return new SuccessfulRestrictResult(tableStatus);
		}
	}
}
