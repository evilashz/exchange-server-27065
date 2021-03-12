using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200012D RID: 301
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetSpoolerResultFactory : StandardResultFactory
	{
		// Token: 0x060005DB RID: 1499 RVA: 0x00010E87 File Offset: 0x0000F087
		internal SetSpoolerResultFactory() : base(RopId.SetSpooler)
		{
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00010E91 File Offset: 0x0000F091
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.SetSpooler, ErrorCode.None);
		}
	}
}
