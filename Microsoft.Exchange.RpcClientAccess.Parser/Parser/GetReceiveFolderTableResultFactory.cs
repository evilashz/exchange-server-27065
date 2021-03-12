using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000EC RID: 236
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetReceiveFolderTableResultFactory : StandardResultFactory
	{
		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F3DD File Offset: 0x0000D5DD
		internal GetReceiveFolderTableResultFactory() : base(RopId.GetReceiveFolderTable)
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F3E7 File Offset: 0x0000D5E7
		public RopResult CreateSuccessfulResult(PropertyValue[][] rowValues)
		{
			return new SuccessfulGetReceiveFolderTableResult(rowValues);
		}
	}
}
