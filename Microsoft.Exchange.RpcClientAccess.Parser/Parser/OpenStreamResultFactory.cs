using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000108 RID: 264
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenStreamResultFactory : StandardResultFactory
	{
		// Token: 0x06000550 RID: 1360 RVA: 0x0000FE23 File Offset: 0x0000E023
		internal OpenStreamResultFactory() : base(RopId.OpenStream)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000FE2D File Offset: 0x0000E02D
		public RopResult CreateSuccessfulResult(IServerObject serverObject, uint streamSize)
		{
			return new SuccessfulOpenStreamResult(serverObject, streamSize);
		}
	}
}
