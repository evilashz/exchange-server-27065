using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000136 RID: 310
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportMessageChangePartialResultFactory : StandardResultFactory
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00010F3B File Offset: 0x0000F13B
		internal ImportMessageChangePartialResultFactory() : base(RopId.ImportMessageChangePartial)
		{
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00010F48 File Offset: 0x0000F148
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId messageId)
		{
			return new SuccessfulImportMessageChangePartialResult(serverObject, messageId);
		}
	}
}
