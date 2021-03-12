using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5D RID: 3933
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecommendedGroupsAccessorFactory
	{
		// Token: 0x060086B3 RID: 34483 RVA: 0x0024F008 File Offset: 0x0024D208
		public IRecommendedGroupsGetter GetReadOnlyAccessor()
		{
			return new RecommendedGroupsAccessor();
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x0024F00F File Offset: 0x0024D20F
		public IRecommendedGroupsAccessor GetReadWriteAccessor()
		{
			return new RecommendedGroupsAccessor();
		}
	}
}
