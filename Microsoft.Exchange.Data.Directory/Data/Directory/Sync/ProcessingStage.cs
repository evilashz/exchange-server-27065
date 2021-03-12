using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007BE RID: 1982
	public enum ProcessingStage
	{
		// Token: 0x040041E9 RID: 16873
		ObjectFullSyncConfiguration,
		// Token: 0x040041EA RID: 16874
		RecipientTypeFilter,
		// Token: 0x040041EB RID: 16875
		OrganizationFilter,
		// Token: 0x040041EC RID: 16876
		RecipientDeletedDuringOrganizationDeletionFilter,
		// Token: 0x040041ED RID: 16877
		RelocationStageFilter,
		// Token: 0x040041EE RID: 16878
		RelocationPartOfRelocationSyncFilter
	}
}
