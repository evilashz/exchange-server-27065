using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010F RID: 271
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class QueryPositionResultFactory : StandardResultFactory
	{
		// Token: 0x0600057D RID: 1405 RVA: 0x0001041F File Offset: 0x0000E61F
		internal QueryPositionResultFactory() : base(RopId.QueryPosition)
		{
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00010429 File Offset: 0x0000E629
		public RopResult CreateSuccessfulResult(uint numerator, uint denominator)
		{
			return new SuccessfulQueryPositionResult(numerator, denominator);
		}
	}
}
