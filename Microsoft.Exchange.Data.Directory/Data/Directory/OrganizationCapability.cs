using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C4 RID: 196
	public enum OrganizationCapability
	{
		// Token: 0x040003F6 RID: 1014
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityClientExtensions)]
		ClientExtensions = 44,
		// Token: 0x040003F7 RID: 1015
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityGMGen)]
		GMGen = 43,
		// Token: 0x040003F8 RID: 1016
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMailRouting)]
		MailRouting = 47,
		// Token: 0x040003F9 RID: 1017
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityOABGen)]
		OABGen = 42,
		// Token: 0x040003FA RID: 1018
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMDataStorage)]
		UMDataStorage = 41,
		// Token: 0x040003FB RID: 1019
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMGrammar)]
		UMGrammar = 40,
		// Token: 0x040003FC RID: 1020
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMGrammarReady)]
		UMGrammarReady = 46,
		// Token: 0x040003FD RID: 1021
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityManagement)]
		Management = 48,
		// Token: 0x040003FE RID: 1022
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityTenantUpgrade)]
		TenantUpgrade,
		// Token: 0x040003FF RID: 1023
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityScaleOut)]
		ScaleOut,
		// Token: 0x04000400 RID: 1024
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMessageTracking)]
		MessageTracking,
		// Token: 0x04000401 RID: 1025
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityPstProvider)]
		PstProvider,
		// Token: 0x04000402 RID: 1026
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilitySuiteServiceStorage)]
		SuiteServiceStorage,
		// Token: 0x04000403 RID: 1027
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityOfficeMessageEncryption)]
		OfficeMessageEncryption,
		// Token: 0x04000404 RID: 1028
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMigration)]
		Migration
	}
}
