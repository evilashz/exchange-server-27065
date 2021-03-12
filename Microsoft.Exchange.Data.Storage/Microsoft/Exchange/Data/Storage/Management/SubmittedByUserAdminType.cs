using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A4C RID: 2636
	internal enum SubmittedByUserAdminType
	{
		// Token: 0x040036E0 RID: 14048
		[LocDescription(ServerStrings.IDs.MigrationUserAdminTypeUnknown)]
		Unknown,
		// Token: 0x040036E1 RID: 14049
		[LocDescription(ServerStrings.IDs.MigrationUserAdminTypeTenantAdmin)]
		TenantAdmin,
		// Token: 0x040036E2 RID: 14050
		[LocDescription(ServerStrings.IDs.MigrationUserAdminTypePartner)]
		Partner,
		// Token: 0x040036E3 RID: 14051
		[LocDescription(ServerStrings.IDs.MigrationUserAdminTypePartnerTenant)]
		PartnerTenant,
		// Token: 0x040036E4 RID: 14052
		[LocDescription(ServerStrings.IDs.MigrationUserAdminTypeDCAdmin)]
		DataCenterAdmin
	}
}
