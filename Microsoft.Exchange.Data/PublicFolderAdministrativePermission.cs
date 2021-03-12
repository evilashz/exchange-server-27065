using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000184 RID: 388
	[Flags]
	public enum PublicFolderAdministrativePermission
	{
		// Token: 0x040007AD RID: 1965
		None = 0,
		// Token: 0x040007AE RID: 1966
		AdministerInformationStore = 1,
		// Token: 0x040007AF RID: 1967
		MailEnablePublicFolder = 4,
		// Token: 0x040007B0 RID: 1968
		ModifyPublicFolderDeletedItemRetention = 8,
		// Token: 0x040007B1 RID: 1969
		ModifyPublicFolderExpiry = 16,
		// Token: 0x040007B2 RID: 1970
		ModifyPublicFolderQuotas = 32,
		// Token: 0x040007B3 RID: 1971
		ModifyPublicFolderReplicaList = 64,
		// Token: 0x040007B4 RID: 1972
		ViewInformationStore = 128,
		// Token: 0x040007B5 RID: 1973
		ModifyPublicFolderACL = 2048,
		// Token: 0x040007B6 RID: 1974
		ModifyPublicFolderAdminACL = 4096,
		// Token: 0x040007B7 RID: 1975
		AllStoreRights = 6397,
		// Token: 0x040007B8 RID: 1976
		AllExtendedRights = -1
	}
}
