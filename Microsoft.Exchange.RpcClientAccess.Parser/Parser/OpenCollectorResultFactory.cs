using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013B RID: 315
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class OpenCollectorResultFactory : StandardResultFactory
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00010FA5 File Offset: 0x0000F1A5
		internal OpenCollectorResultFactory() : base(RopId.OpenCollector)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00010FAF File Offset: 0x0000F1AF
		public RopResult CreateSuccessfulResult(IServerObject serverObject)
		{
			return new SuccessfulOpenCollectorResult(serverObject);
		}
	}
}
