using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000123 RID: 291
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SetColumnsResultFactory : StandardResultFactory
	{
		// Token: 0x060005BF RID: 1471 RVA: 0x00010CC2 File Offset: 0x0000EEC2
		internal SetColumnsResultFactory() : base(RopId.SetColumns)
		{
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00010CCC File Offset: 0x0000EECC
		public RopResult CreateSuccessfulResult(TableStatus tableStatus)
		{
			return new SuccessfulSetColumnsResult(tableStatus);
		}
	}
}
