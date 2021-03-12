using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000137 RID: 311
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportMessageChangeResultFactory : StandardResultFactory
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00010F51 File Offset: 0x0000F151
		internal ImportMessageChangeResultFactory() : base(RopId.ImportMessageChange)
		{
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00010F5B File Offset: 0x0000F15B
		public RopResult CreateSuccessfulResult(IServerObject serverObject, StoreId messageId)
		{
			return new SuccessfulImportMessageChangeResult(serverObject, messageId);
		}
	}
}
