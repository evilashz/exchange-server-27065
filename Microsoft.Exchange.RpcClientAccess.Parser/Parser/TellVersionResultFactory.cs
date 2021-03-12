using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200013F RID: 319
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class TellVersionResultFactory : StandardResultFactory
	{
		// Token: 0x060005FF RID: 1535 RVA: 0x00010FF3 File Offset: 0x0000F1F3
		internal TellVersionResultFactory() : base(RopId.TellVersion)
		{
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00011000 File Offset: 0x0000F200
		public RopResult CreateSuccessfulResult()
		{
			return new SuccessfulTellVersionResult();
		}
	}
}
