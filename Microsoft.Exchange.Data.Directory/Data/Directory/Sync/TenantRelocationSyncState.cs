using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000808 RID: 2056
	internal enum TenantRelocationSyncState
	{
		// Token: 0x04004338 RID: 17208
		PreSyncAllObjects,
		// Token: 0x04004339 RID: 17209
		EnumerateConfigUnitLiveObjects,
		// Token: 0x0400433A RID: 17210
		EnumerateConfigUnitLinksInPage,
		// Token: 0x0400433B RID: 17211
		EnumerateOrganizationalUnitLiveObjects,
		// Token: 0x0400433C RID: 17212
		EnumerateOrganizationalUnitLinksInPage,
		// Token: 0x0400433D RID: 17213
		EnumerateConfigUnitDeletedObjects,
		// Token: 0x0400433E RID: 17214
		EnumerateOrganizationalUnitDeletedObjects,
		// Token: 0x0400433F RID: 17215
		EnumerateSpecialObjects,
		// Token: 0x04004340 RID: 17216
		Complete
	}
}
