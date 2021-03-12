using System;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A0 RID: 1952
	internal static class CannedProvisioningCacheKeys
	{
		// Token: 0x0400411E RID: 16670
		public static readonly Guid FirstOrgContainerId = new Guid("871EF30B-1510-4c91-95A2-0D1146399DAE");

		// Token: 0x0400411F RID: 16671
		public static readonly Guid DatabaseContainerId = new Guid("370C19FD-5A5F-4fbf-AC6A-EF0C1B030E58");

		// Token: 0x04004120 RID: 16672
		public static readonly Guid ServerContainerId = new Guid("A9D5945D-2F9B-4BE6-A25C-6286B552B7F2");

		// Token: 0x04004121 RID: 16673
		public static readonly Guid OrganizationCUContainer = new Guid("A508F341-CCD1-4e00-B3CD-3BAFB2E14B8C");

		// Token: 0x04004122 RID: 16674
		public static readonly Guid OrganizationAcceptedDomains = new Guid("89838019-6F75-4b5d-A509-20AEC9E0DABB");

		// Token: 0x04004123 RID: 16675
		public static readonly Guid OrganizationSiteMailboxAddressesTemplate = new Guid("E50B399B-A047-415C-A5C9-0D269A6A584F");

		// Token: 0x04004124 RID: 16676
		public static readonly Guid OrganizationUnifiedPolicyNotificationClients = new Guid("731B6883-7DB8-4CCF-8B32-4E7C39E93DCD");

		// Token: 0x04004125 RID: 16677
		public static readonly Guid GlobalUnifiedPolicyNotificationClientsInfo = new Guid("33DFF16D-8E3C-438E-80D9-4FB1017CE968");

		// Token: 0x04004126 RID: 16678
		public static readonly Guid DefaultMailboxPlan = new Guid("31307C39-66F8-49bb-B889-EDAE970EDA55");

		// Token: 0x04004127 RID: 16679
		public static readonly Guid EnforcementProvisioningPolicies = new Guid("BC7D8A8B-29A3-42f5-8CF6-BBD0622A61C6");

		// Token: 0x04004128 RID: 16680
		public static readonly Guid AddressBookPolicies = new Guid("1CF2919F-ED6F-410e-AF82-23F475C8DD96");

		// Token: 0x04004129 RID: 16681
		public static readonly Guid DefaultRoleAssignmentPolicyId = new Guid("DBBD48CD-BF92-4275-9EC9-FCE8485FF5F6");

		// Token: 0x0400412A RID: 16682
		public static readonly Guid ProvisioningEnabledDatabasesOnLocalSite = new Guid("77E72FA3-6DE9-4b09-96AA-5BD016C97F86");

		// Token: 0x0400412B RID: 16683
		public static readonly Guid ProvisioningEnabledDatabasesOnAllSites = new Guid("0F49753F-0F73-4018-9BEC-8B4662CD137F");

		// Token: 0x0400412C RID: 16684
		public static readonly Guid AdministrativeGroupLegDN = new Guid("1FA17CA9-E2A9-4d43-9795-F3C6AEEEA6AC");

		// Token: 0x0400412D RID: 16685
		public static readonly Guid MtaDictionary = new Guid("EB2111F7-E983-467c-83D4-2F7E9EB14652");

		// Token: 0x0400412E RID: 16686
		public static readonly Guid OrganizationIdDictionary = new Guid("31147A1D-9C2C-42b0-8419-771BCF98781C");

		// Token: 0x0400412F RID: 16687
		public static readonly Guid OrganizationalUnitDictionary = new Guid("EAA158F5-BB6E-414d-A956-8F226B41AF2C");

		// Token: 0x04004130 RID: 16688
		public static readonly Guid CacheKeyMailboxPlanIdParameterId = new Guid("3f815cbb-95d9-4aaa-9dd2-9e99bb083528");

		// Token: 0x04004131 RID: 16689
		public static readonly Guid CacheKeyMailboxPlanId = new Guid("1dfafa08-54ef-4729-9237-ee11e7de3d6c");

		// Token: 0x04004132 RID: 16690
		public static readonly Guid EomsGroupCacheKey = new Guid("46EEC2B0-3E4C-48c3-A15C-85DCA442C9E9");

		// Token: 0x04004133 RID: 16691
		public static readonly Guid EopsGroupCacheKey = new Guid("D6DDED25-2F79-4a92-B261-5228A996B395");

		// Token: 0x04004134 RID: 16692
		public static readonly Guid ServerAdminDisplayVersionCacheKey = new Guid("dede6ce4-2c89-42b3-8017-6032519f151e");

		// Token: 0x04004135 RID: 16693
		public static readonly Guid MailboxSharedConfigCacheKey = new Guid("70dce3c3-71dd-4f78-b6db-4b5311646e2e");

		// Token: 0x04004136 RID: 16694
		public static readonly Guid MailboxRoleAssignmentPolicyCacheKey = new Guid("50d6c3b5-f103-4079-b1b6-7ff6e2260d84");

		// Token: 0x04004137 RID: 16695
		public static readonly Guid MailboxDatabaseForDefaultRetentionValuesCacheKey = new Guid("142c5e6b-3fdd-4e2d-925d-dbee5d41bb01");

		// Token: 0x04004138 RID: 16696
		public static readonly Guid SharedConfigurationStateCacheKey = new Guid("202b9a71-9c83-4439-8a7e-78068cab2ac1");

		// Token: 0x04004139 RID: 16697
		public static readonly Guid IsDehydratedConfigurationCacheKey = new Guid("d3071ace-1a2f-472b-878d-bd50b544fed2");

		// Token: 0x0400413A RID: 16698
		public static readonly Guid NamespaceAuthenticationTypeCacheKey = new Guid("a54b3a77-18a9-4e33-9160-9270d9a6b42c");

		// Token: 0x0400413B RID: 16699
		public static readonly Guid TransportRuleAttachmentTextScanLimitCacheKey = new Guid("9E6CE627-E627-4E05-B3CB-F49B0D49B234");

		// Token: 0x0400413C RID: 16700
		internal static readonly Guid[] GlobalCacheKeys = new Guid[]
		{
			CannedProvisioningCacheKeys.FirstOrgContainerId,
			CannedProvisioningCacheKeys.DatabaseContainerId,
			CannedProvisioningCacheKeys.ServerContainerId,
			CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnLocalSite,
			CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites,
			CannedProvisioningCacheKeys.AdministrativeGroupLegDN,
			CannedProvisioningCacheKeys.OrganizationIdDictionary,
			CannedProvisioningCacheKeys.OrganizationalUnitDictionary,
			CannedProvisioningCacheKeys.MtaDictionary,
			CannedProvisioningCacheKeys.ServerAdminDisplayVersionCacheKey,
			CannedProvisioningCacheKeys.MailboxDatabaseForDefaultRetentionValuesCacheKey,
			CannedProvisioningCacheKeys.GlobalUnifiedPolicyNotificationClientsInfo
		};
	}
}
