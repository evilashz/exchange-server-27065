using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000135 RID: 309
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportHierarchyChangeResultFactory : StandardResultFactory
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00010F29 File Offset: 0x0000F129
		internal ImportHierarchyChangeResultFactory() : base(RopId.ImportHierarchyChange)
		{
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00010F33 File Offset: 0x0000F133
		public RopResult CreateSuccessfulResult(StoreId folderId)
		{
			return new SuccessfulImportHierarchyChangeResult(folderId);
		}
	}
}
