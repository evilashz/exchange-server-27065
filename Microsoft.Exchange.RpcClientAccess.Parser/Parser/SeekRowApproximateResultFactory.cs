using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200011F RID: 287
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeekRowApproximateResultFactory : StandardResultFactory
	{
		// Token: 0x060005B7 RID: 1463 RVA: 0x00010C77 File Offset: 0x0000EE77
		internal SeekRowApproximateResultFactory() : base(RopId.SeekRowApproximate)
		{
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00010C81 File Offset: 0x0000EE81
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SeekRowApproximate, ErrorCode.None);
		}
	}
}
