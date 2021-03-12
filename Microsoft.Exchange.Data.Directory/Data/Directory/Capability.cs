using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C3 RID: 195
	public enum Capability
	{
		// Token: 0x040003D0 RID: 976
		[LocDescription(DirectoryStrings.IDs.CapabilityNone)]
		None,
		// Token: 0x040003D1 RID: 977
		[SKUCapability("4a82b400-a79f-41a4-b4e2-e94f5787b113")]
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSDeskless)]
		BPOS_S_Deskless,
		// Token: 0x040003D2 RID: 978
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSStandard)]
		[SKUCapability("9aaf7827-d63c-4b61-89c3-182f06f82e5c")]
		BPOS_S_Standard,
		// Token: 0x040003D3 RID: 979
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSEnterprise)]
		[SKUCapability("efb87545-963c-4e0d-99df-69c6916d9eb0")]
		BPOS_S_Enterprise,
		// Token: 0x040003D4 RID: 980
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSArchive)]
		[SKUCapability("da040e0a-b393-4bea-bb76-928b3fa1cf5a", AddOnSKU = true)]
		BPOS_S_Archive = 5,
		// Token: 0x040003D5 RID: 981
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSLite)]
		[SKUCapability("d42bdbd6-c335-4231-ab3d-c8f348d5aff5")]
		BPOS_L_Standard,
		// Token: 0x040003D6 RID: 982
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSBasic)]
		[SKUCapability("90927877-dcff-4af6-b346-2332c0b15bb7")]
		BPOS_B_Standard,
		// Token: 0x040003D7 RID: 983
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSBasicCustomDomain)]
		[SKUCapability("e4ed42b9-801e-4374-bffa-9bca1d5ceb28")]
		BPOS_B_CustomDomain,
		// Token: 0x040003D8 RID: 984
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSMidSize)]
		[SKUCapability("FC52CC4B-ED7D-472d-BBE7-B081C23ECC56")]
		BPOS_S_MidSize,
		// Token: 0x040003D9 RID: 985
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityBPOSSArchiveAddOn)]
		[SKUCapability("176a09a6-7ec5-4039-ac02-b2791c6ba793", AddOnSKU = true)]
		BPOS_S_ArchiveAddOn,
		// Token: 0x040003DA RID: 986
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityEOPStandardAddOn)]
		[SKUCapability("326e2b78-9d27-42c9-8509-46c827743a17", AddOnSKU = true)]
		BPOS_S_EopStandardAddOn,
		// Token: 0x040003DB RID: 987
		[SKUCapability("75badc48-628e-4446-8460-41344d73abd6", AddOnSKU = true)]
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityEOPPremiumAddOn)]
		BPOS_S_EopPremiumAddOn,
		// Token: 0x040003DC RID: 988
		[LocDescription(DirectoryStrings.IDs.SKUCapabilityUnmanaged)]
		[SKUCapability("6C5DEA44-8DA9-4EA5-A8BF-AF72ED983FAC")]
		BPOS_Unmanaged,
		// Token: 0x040003DD RID: 989
		[LocDescription(DirectoryStrings.IDs.CapabilityTOUSigned)]
		TOU_Signed = 32,
		// Token: 0x040003DE RID: 990
		[LocDescription(DirectoryStrings.IDs.CapabilityFederatedUser)]
		FederatedUser,
		// Token: 0x040003DF RID: 991
		[LocDescription(DirectoryStrings.IDs.CapabilityPartnerManaged)]
		Partner_Managed,
		// Token: 0x040003E0 RID: 992
		[LocDescription(DirectoryStrings.IDs.CapabilityMasteredOnPremise)]
		MasteredOnPremise,
		// Token: 0x040003E1 RID: 993
		[LocDescription(DirectoryStrings.IDs.CapabilityResourceMailbox)]
		ResourceMailbox,
		// Token: 0x040003E2 RID: 994
		[LocDescription(DirectoryStrings.IDs.CapabilityExcludedFromBackSync)]
		ExcludedFromBackSync,
		// Token: 0x040003E3 RID: 995
		[LocDescription(DirectoryStrings.IDs.CapabilityUMFeatureRestricted)]
		UMFeatureRestricted,
		// Token: 0x040003E4 RID: 996
		[LocDescription(DirectoryStrings.IDs.CapabilityRichCoexistence)]
		RichCoexistence,
		// Token: 0x040003E5 RID: 997
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMGrammar)]
		OrganizationCapabilityUMGrammar,
		// Token: 0x040003E6 RID: 998
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMDataStorage)]
		OrganizationCapabilityUMDataStorage,
		// Token: 0x040003E7 RID: 999
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityOABGen)]
		OrganizationCapabilityOABGen,
		// Token: 0x040003E8 RID: 1000
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityGMGen)]
		OrganizationCapabilityGMGen,
		// Token: 0x040003E9 RID: 1001
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityClientExtensions)]
		OrganizationCapabilityClientExtensions,
		// Token: 0x040003EA RID: 1002
		[LocDescription(DirectoryStrings.IDs.CapabilityBEVDirLockdown)]
		BEVDirLockdown,
		// Token: 0x040003EB RID: 1003
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityUMGrammarReady)]
		OrganizationCapabilityUMGrammarReady,
		// Token: 0x040003EC RID: 1004
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMailRouting)]
		OrganizationCapabilityMailRouting,
		// Token: 0x040003ED RID: 1005
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityManagement)]
		OrganizationCapabilityManagement,
		// Token: 0x040003EE RID: 1006
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityTenantUpgrade)]
		OrganizationCapabilityTenantUpgrade,
		// Token: 0x040003EF RID: 1007
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityScaleOut)]
		OrganizationCapabilityScaleOut,
		// Token: 0x040003F0 RID: 1008
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMessageTracking)]
		OrganizationCapabilityMessageTracking,
		// Token: 0x040003F1 RID: 1009
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityPstProvider)]
		OrganizationCapabilityPstProvider,
		// Token: 0x040003F2 RID: 1010
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilitySuiteServiceStorage)]
		OrganizationCapabilitySuiteServiceStorage,
		// Token: 0x040003F3 RID: 1011
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityOfficeMessageEncryption)]
		OrganizationCapabilityOfficeMessageEncryption,
		// Token: 0x040003F4 RID: 1012
		[LocDescription(DirectoryStrings.IDs.OrganizationCapabilityMigration)]
		OrganizationCapabilityMigration
	}
}
