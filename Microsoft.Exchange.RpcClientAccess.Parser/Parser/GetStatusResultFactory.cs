using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000EF RID: 239
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetStatusResultFactory : StandardResultFactory
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000F43A File Offset: 0x0000D63A
		internal GetStatusResultFactory() : base(RopId.GetStatus)
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000F444 File Offset: 0x0000D644
		public RopResult CreateSuccessfulResult(TableStatus tableStatus)
		{
			return new SuccessfulGetStatusResult(tableStatus);
		}
	}
}
