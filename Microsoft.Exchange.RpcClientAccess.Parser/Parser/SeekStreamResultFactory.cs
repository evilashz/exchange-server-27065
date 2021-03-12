using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000121 RID: 289
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SeekStreamResultFactory : StandardResultFactory
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00010C9E File Offset: 0x0000EE9E
		internal SeekStreamResultFactory() : base(RopId.SeekStream)
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public RopResult CreateSuccessfulResult(ulong resultOffset)
		{
			return new SuccessfulSeekStreamResult(resultOffset);
		}
	}
}
