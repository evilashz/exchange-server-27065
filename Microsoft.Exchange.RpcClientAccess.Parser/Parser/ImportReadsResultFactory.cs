using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000139 RID: 313
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportReadsResultFactory : StandardResultFactory
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00010F76 File Offset: 0x0000F176
		internal ImportReadsResultFactory() : base(RopId.ImportReads)
		{
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00010F83 File Offset: 0x0000F183
		public RopResult CreateSuccessfulResult()
		{
			return new StandardRopResult(RopId.ImportReads, ErrorCode.None);
		}
	}
}
