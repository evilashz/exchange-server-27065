using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000EE RID: 238
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetSearchCriteriaResultFactory : StandardResultFactory
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F419 File Offset: 0x0000D619
		internal GetSearchCriteriaResultFactory(byte logonIndex) : base(RopId.GetSearchCriteria)
		{
			this.logonIndex = logonIndex;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000F42A File Offset: 0x0000D62A
		public RopResult CreateSuccessfulResult(Restriction restriction, StoreId[] folderIds, SearchState searchState)
		{
			return new SuccessfulGetSearchCriteriaResult(restriction, this.logonIndex, folderIds, searchState);
		}

		// Token: 0x040002F2 RID: 754
		private readonly byte logonIndex;
	}
}
