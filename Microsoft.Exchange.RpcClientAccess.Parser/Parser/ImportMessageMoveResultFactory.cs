using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000138 RID: 312
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportMessageMoveResultFactory : StandardResultFactory
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x00010F64 File Offset: 0x0000F164
		internal ImportMessageMoveResultFactory() : base(RopId.ImportMessageMove)
		{
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00010F6E File Offset: 0x0000F16E
		public RopResult CreateSuccessfulResult(StoreId messageId)
		{
			return new SuccessfulImportMessageMoveResult(messageId);
		}
	}
}
