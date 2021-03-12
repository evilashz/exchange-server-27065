using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F62 RID: 3938
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupingModelVersionSelector
	{
		// Token: 0x060086CC RID: 34508 RVA: 0x0024F418 File Offset: 0x0024D618
		public GroupingModelVersionSelector(IGroupingModelConfiguration configuration)
		{
			this.groupingModelconfiguration = configuration;
		}

		// Token: 0x060086CD RID: 34509 RVA: 0x0024F427 File Offset: 0x0024D627
		public int GetModelVersionToTrain()
		{
			return this.groupingModelconfiguration.CurrentVersion;
		}

		// Token: 0x060086CE RID: 34510 RVA: 0x0024F434 File Offset: 0x0024D634
		public int GetModelVersionToAccessRecommendedGroups()
		{
			return this.groupingModelconfiguration.CurrentVersion;
		}

		// Token: 0x04005A22 RID: 23074
		private readonly IGroupingModelConfiguration groupingModelconfiguration;
	}
}
