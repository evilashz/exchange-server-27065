using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000C7 RID: 199
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EchoIntResultFactory : StandardResultFactory
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000ED5D File Offset: 0x0000CF5D
		internal EchoIntResultFactory() : base(RopId.EchoInt)
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		public RopResult CreateSuccessfulResult(int returnValue, int outParameter)
		{
			return new SuccessfulEchoIntResult(returnValue, outParameter);
		}
	}
}
