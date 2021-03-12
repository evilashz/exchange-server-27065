using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000240 RID: 576
	internal class OrganizationSettingsSchema : ServicePlanElementSchema
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x00054F34 File Offset: 0x00053134
		// Note: this type is marked as 'beforefieldinit'.
		static OrganizationSettingsSchema()
		{
			string name = "ManagedFoldersPermissions";
			Type typeFromHandle = typeof(bool);
			ServicePlanSkus servicePlanSkus = ServicePlanSkus.Datacenter;
			FeatureCategory[] array = new FeatureCategory[2];
			array[0] = FeatureCategory.AdminPermissions;
			OrganizationSettingsSchema.ManagedFoldersPermissions = new FeatureDefinition(name, typeFromHandle, servicePlanSkus, array);
			OrganizationSettingsSchema.SearchMessagePermissions = new FeatureDefinition("SearchMessagePermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.ProfileUpdatePermissions = new FeatureDefinition("ProfileUpdatePermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.OpenDomainProfileUpdatePermissions = new FeatureDefinition("OpenDomainProfileUpdatePermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.JournalingRulesPermissions = new FeatureDefinition("JournalingRulesPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.ModeratedRecipientsPermissions = new FeatureDefinition("ModeratedRecipientsPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			string name2 = "TransportRulesPermissions";
			Type typeFromHandle2 = typeof(bool);
			ServicePlanSkus servicePlanSkus2 = ServicePlanSkus.All;
			FeatureCategory[] array2 = new FeatureCategory[2];
			array2[0] = FeatureCategory.AdminPermissions;
			OrganizationSettingsSchema.TransportRulesPermissions = new FeatureDefinition(name2, typeFromHandle2, servicePlanSkus2, array2);
			OrganizationSettingsSchema.UMAutoAttendantPermissions = new FeatureDefinition("UMAutoAttendantPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ChangeMailboxPlanAssignmentPermissions = new FeatureDefinition("ChangeMailboxPlanAssignmentPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.TeamMailboxPermissions = new FeatureDefinition("TeamMailboxPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.ConfigCustomizationsPermissions = new FeatureDefinition("ConfigCustomizationsPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.UMSMSMsgWaitingPermissions = new FeatureDefinition("UMSMSMsgWaitingPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.UMPBXPermissions = new FeatureDefinition("UMPBXPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.UMCloudServicePermissions = new FeatureDefinition("UMCloudServicePermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.OutlookAnywherePermissions = new FeatureDefinition("OutlookAnywherePermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.PopPermissions = new FeatureDefinition("PopPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.ImapPermissions = new FeatureDefinition("ImapPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.ActiveSyncPermissions = new FeatureDefinition("ActiveSyncPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.DeviceFiltersSetupEnabled = new FeatureDefinition("DeviceFiltersSetupEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.EwsPermissions = new FeatureDefinition("EwsPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			string name3 = "UMPermissions";
			Type typeFromHandle3 = typeof(bool);
			ServicePlanSkus servicePlanSkus3 = ServicePlanSkus.Datacenter;
			FeatureCategory[] array3 = new FeatureCategory[3];
			array3[0] = FeatureCategory.AdminPermissions;
			array3[1] = FeatureCategory.RoleGroupRoleAssignment;
			OrganizationSettingsSchema.UMPermissions = new FeatureDefinition(name3, typeFromHandle3, servicePlanSkus3, array3);
			OrganizationSettingsSchema.UMFaxPermissions = new FeatureDefinition("UMFaxPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.UMOutDialingPermissions = new FeatureDefinition("UMOutDialingPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.UMPersonalAutoAttendantPermissions = new FeatureDefinition("UMPersonalAutoAttendantPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MailTipsPermissions = new FeatureDefinition("MailTipsPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.OWAPermissions = new FeatureDefinition("OWAPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.SMSPermissions = new FeatureDefinition("SMSPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.SetHiddenFromAddressListPermissions = new FeatureDefinition("SetHiddenFromAddressListPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.NewUserPasswordManagementPermissions = new FeatureDefinition("NewUserPasswordManagementPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.NewUserResetPasswordOnNextLogonPermissions = new FeatureDefinition("NewUserResetPasswordOnNextLogonPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.UserLiveIdManagementPermissions = new FeatureDefinition("UserLiveIdManagementPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MSOIdPermissions = new FeatureDefinition("MSOIdPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ResetUserPasswordManagementPermissions = new FeatureDefinition("ResetUserPasswordManagementPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.UserMailboxAccessPermissions = new FeatureDefinition("UserMailboxAccessPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.MailboxRecoveryPermissions = new FeatureDefinition("MailboxRecoveryPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.PopSyncPermissions = new FeatureDefinition("PopSyncPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.AddSecondaryDomainPermissions = new FeatureDefinition("AddSecondaryDomainPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.HotmailSyncPermissions = new FeatureDefinition("HotmailSyncPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ImapSyncPermissions = new FeatureDefinition("ImapSyncPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.OrganizationalAffinityPermissions = new FeatureDefinition("OrganizationalAffinityPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.MessageTrackingPermissions = new FeatureDefinition("MessageTrackingPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.ActiveSyncDeviceDataAccessPermissions = new FeatureDefinition("ActiveSyncDeviceDataAccessPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.MOWADeviceDataAccessPermissions = new FeatureDefinition("MOWADeviceDataAccessPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.GroupAsGroupSyncPermissions = new FeatureDefinition("GroupAsGroupSyncPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.LitigationHoldPermissions = new FeatureDefinition("LitigationHoldPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.ArchivePermissions = new FeatureDefinition("ArchivePermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.PermissionManagementEnabled = new FeatureDefinition("PermissionManagementEnabled", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.RoleGroupRoleAssignment
			});
			string name4 = "PrivacyLink";
			Type typeFromHandle4 = typeof(string);
			ServicePlanSkus servicePlanSkus4 = ServicePlanSkus.Datacenter;
			FeatureCategory[] categories = new FeatureCategory[1];
			OrganizationSettingsSchema.PrivacyLink = new FeatureDefinition(name4, typeFromHandle4, servicePlanSkus4, categories);
			OrganizationSettingsSchema.ApplicationImpersonationEnabled = new FeatureDefinition("ApplicationImpersonationEnabled", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.MailTipsEnabled = new FeatureDefinition("MailTipsEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.AddressListsEnabled = new FeatureDefinition("AddressListsEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.FastRecipientCountingDisabled = new FeatureDefinition("FastRecipientCountingDisabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.OfflineAddressBookEnabled = new FeatureDefinition("OfflineAddressBookEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.OpenDomainRoutingEnabled = new FeatureDefinition("OpenDomainRoutingEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.AddOutlookAcceptedDomains = new FeatureDefinition("AddOutlookAcceptedDomains", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.GALSyncEnabled = new FeatureDefinition("GALSyncEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.CommonConfiguration = new FeatureDefinition("CommonConfiguration", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.HideAdminAccessWarningEnabled = new FeatureDefinition("HideAdminAccessWarningEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.SkipToUAndParentalControlCheckEnabled = new FeatureDefinition("SkipToUAndParentalControlCheckEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.SMTPAddressCheckWithAcceptedDomainEnabled = new FeatureDefinition("SMTPAddressCheckWithAcceptedDomainEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.AutoReplyEnabled = new FeatureDefinition("AutoReplyEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.AutoForwardEnabled = new FeatureDefinition("AutoForwardEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.MSOSyncEnabled = new FeatureDefinition("MSOSyncEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.AllowDeleteOfExternalIdentityUponRemove = new FeatureDefinition("AllowDeleteOfExternalIdentityUponRemove", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			string name5 = "SearchMessageEnabled";
			Type typeFromHandle5 = typeof(bool);
			ServicePlanSkus servicePlanSkus5 = ServicePlanSkus.All;
			FeatureCategory[] categories2 = new FeatureCategory[1];
			OrganizationSettingsSchema.SearchMessageEnabled = new FeatureDefinition(name5, typeFromHandle5, servicePlanSkus5, categories2);
			OrganizationSettingsSchema.OwaInstantMessagingType = new FeatureDefinition("OwaInstantMessagingType", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.RecipientManagementPermissions = new FeatureDefinition("RecipientManagementPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.All);
			OrganizationSettingsSchema.DistributionListCountQuota = new FeatureDefinition("DistributionListCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.MailboxCountQuota = new FeatureDefinition("MailboxCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.MailUserCountQuota = new FeatureDefinition("MailUserCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.ContactCountQuota = new FeatureDefinition("ContactCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.TeamMailboxCountQuota = new FeatureDefinition("TeamMailboxCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.PublicFolderMailboxCountQuota = new FeatureDefinition("PublicFolderMailboxCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.MailPublicFolderCountQuota = new FeatureDefinition("MailPublicFolderCountQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.SupervisionPermissions = new FeatureDefinition("SupervisionPermissions", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.SupervisionEnabled = new FeatureDefinition("SupervisionEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.TemplateTenant = new FeatureDefinition("TemplateTenant", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.TransportRulesCollectionsEnabled = new FeatureDefinition("TransportRulesCollectionsEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			string name6 = "SyncAccountsEnabled";
			Type typeFromHandle6 = typeof(bool);
			ServicePlanSkus servicePlanSkus6 = ServicePlanSkus.Datacenter;
			FeatureCategory[] categories3 = new FeatureCategory[1];
			OrganizationSettingsSchema.SyncAccountsEnabled = new FeatureDefinition(name6, typeFromHandle6, servicePlanSkus6, categories3);
			OrganizationSettingsSchema.RecipientMailSubmissionRateQuota = new FeatureDefinition("RecipientMailSubmissionRateQuota", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.All);
			OrganizationSettingsSchema.ImapMigrationPermissions = new FeatureDefinition("ImapMigrationPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.HotmailMigrationPermissions = new FeatureDefinition("HotmailMigrationPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ExchangeMigrationPermissions = new FeatureDefinition("ExchangeMigrationPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MultiEngineAVEnabled = new FeatureDefinition("MultiEngineAVEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.CommonHydrateableObjectsSharedEnabled = new FeatureDefinition("CommonHydrateableObjectsSharedEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.AdvancedHydrateableObjectsSharedEnabled = new FeatureDefinition("AdvancedHydrateableObjectsSharedEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.ShareableConfigurationEnabled = new FeatureDefinition("ShareableConfigurationEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.IRMPremiumFeaturesPermissions = new FeatureDefinition("IRMPremiumFeaturesPermissions", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions,
				FeatureCategory.RoleGroupRoleAssignment
			});
			OrganizationSettingsSchema.RBACManagementPermissions = new FeatureDefinition("RBACManagementPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.PerMBXPlanRoleAssignmentPolicyEnabled = new FeatureDefinition("PerMBXPlanRoleAssignmentPolicyEnabled", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.RoleAssignmentPolicyPermissions = new FeatureDefinition("RoleAssignmentPolicyPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.PerMBXPlanOWAPolicyEnabled = new FeatureDefinition("PerMBXPlanOWAPolicyEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.OWAMailboxPolicyPermissions = new FeatureDefinition("OWAMailboxPolicyPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions
			});
			string name7 = "PerMBXPlanRetentionPolicyEnabled";
			Type typeFromHandle7 = typeof(bool);
			ServicePlanSkus servicePlanSkus7 = ServicePlanSkus.Datacenter;
			FeatureCategory[] categories4 = new FeatureCategory[1];
			OrganizationSettingsSchema.PerMBXPlanRetentionPolicyEnabled = new FeatureDefinition(name7, typeFromHandle7, servicePlanSkus7, categories4);
			string name8 = "ReducedOutOfTheBoxMrmTagsEnabled";
			Type typeFromHandle8 = typeof(bool);
			ServicePlanSkus servicePlanSkus8 = ServicePlanSkus.Datacenter;
			FeatureCategory[] categories5 = new FeatureCategory[1];
			OrganizationSettingsSchema.ReducedOutOfTheBoxMrmTagsEnabled = new FeatureDefinition(name8, typeFromHandle8, servicePlanSkus8, categories5);
			OrganizationSettingsSchema.AddressBookPolicyPermissions = new FeatureDefinition("AddressBookPolicyPermissions", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.LicenseEnforcementEnabled = new FeatureDefinition("LicenseEnforcementEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.PerimeterSafelistingUIMode = new FeatureDefinition("PerimeterSafelistingUIMode", FeatureCategory.OrgWideConfiguration, typeof(string), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ExchangeHostedFilteringAdminCenterAvailabilityEnabled = new FeatureDefinition("ExchangeHostedFilteringAdminCenterAvailabilityEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ExchangeHostedFilteringPerimeterEnabled = new FeatureDefinition("ExchangeHostedFilteringPerimeterEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.ApplicationImpersonationRegularRoleAssignmentEnabled = new FeatureDefinition("ApplicationImpersonationRegularRoleAssignmentEnabled", FeatureCategory.RoleGroupRoleAssignment, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MailboxImportExportRegularRoleAssignmentEnabled = new FeatureDefinition("MailboxImportExportRegularRoleAssignmentEnabled", FeatureCategory.RoleGroupRoleAssignment, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MailboxQuotaPermissions = new FeatureDefinition("MailboxQuotaPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.MailboxSIRPermissions = new FeatureDefinition("MailboxSIRPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.PublicFoldersEnabled = new FeatureDefinition("PublicFoldersEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.RoleGroupRoleAssignment,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.QuarantineEnabled = new FeatureDefinition("QuarantineEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.RoleGroupRoleAssignment,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.UseServicePlanAsCounterInstanceName = new FeatureDefinition("UseServicePlanAsCounterInstanceName", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.RIMRoleGroupEnabled = new FeatureDefinition("RIMRoleGroupEnabled", FeatureCategory.RoleGroupRoleAssignment, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.CalendarVersionStoreEnabled = new FeatureDefinition("CalendarVersionStoreEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.HostedSpamFilteringPolicyCustomizationEnabled = new FeatureDefinition("HostedSpamFilteringPolicyCustomizationEnabled", typeof(bool), ServicePlanSkus.Datacenter, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.MalwareFilteringPolicyCustomizationEnabled = new FeatureDefinition("MalwareFilteringPolicyCustomizationEnabled", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.EXOCoreFeatures = new FeatureDefinition("EXOCoreFeatures", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.RoleGroupRoleAssignment,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.MessageTrace = new FeatureDefinition("MessageTrace", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.AcceptedDomains = new FeatureDefinition("AcceptedDomains", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.ServiceConnectors = new FeatureDefinition("ServiceConnectors", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
			OrganizationSettingsSchema.SoftDeletedFeatureManagementPermissions = new FeatureDefinition("SoftDeletedFeatureManagementPermissions", FeatureCategory.AdminPermissions, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.PilotEnabled = new FeatureDefinition("PilotEnabled", FeatureCategory.OrgWideConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
			OrganizationSettingsSchema.DataLossPreventionEnabled = new FeatureDefinition("DataLossPreventionEnabled", typeof(bool), ServicePlanSkus.All, new FeatureCategory[]
			{
				FeatureCategory.OrgWideConfiguration,
				FeatureCategory.AdminPermissions
			});
		}

		// Token: 0x04000876 RID: 2166
		public static readonly FeatureDefinition ManagedFoldersPermissions;

		// Token: 0x04000877 RID: 2167
		public static readonly FeatureDefinition SearchMessagePermissions;

		// Token: 0x04000878 RID: 2168
		public static readonly FeatureDefinition ProfileUpdatePermissions;

		// Token: 0x04000879 RID: 2169
		public static readonly FeatureDefinition OpenDomainProfileUpdatePermissions;

		// Token: 0x0400087A RID: 2170
		public static readonly FeatureDefinition JournalingRulesPermissions;

		// Token: 0x0400087B RID: 2171
		public static readonly FeatureDefinition ModeratedRecipientsPermissions;

		// Token: 0x0400087C RID: 2172
		public static readonly FeatureDefinition TransportRulesPermissions;

		// Token: 0x0400087D RID: 2173
		public static readonly FeatureDefinition UMAutoAttendantPermissions;

		// Token: 0x0400087E RID: 2174
		public static readonly FeatureDefinition ChangeMailboxPlanAssignmentPermissions;

		// Token: 0x0400087F RID: 2175
		public static readonly FeatureDefinition TeamMailboxPermissions;

		// Token: 0x04000880 RID: 2176
		public static readonly FeatureDefinition ConfigCustomizationsPermissions;

		// Token: 0x04000881 RID: 2177
		public static readonly FeatureDefinition UMSMSMsgWaitingPermissions;

		// Token: 0x04000882 RID: 2178
		public static readonly FeatureDefinition UMPBXPermissions;

		// Token: 0x04000883 RID: 2179
		public static readonly FeatureDefinition UMCloudServicePermissions;

		// Token: 0x04000884 RID: 2180
		public static readonly FeatureDefinition OutlookAnywherePermissions;

		// Token: 0x04000885 RID: 2181
		public static readonly FeatureDefinition PopPermissions;

		// Token: 0x04000886 RID: 2182
		public static readonly FeatureDefinition ImapPermissions;

		// Token: 0x04000887 RID: 2183
		public static readonly FeatureDefinition ActiveSyncPermissions;

		// Token: 0x04000888 RID: 2184
		public static readonly FeatureDefinition DeviceFiltersSetupEnabled;

		// Token: 0x04000889 RID: 2185
		public static readonly FeatureDefinition EwsPermissions;

		// Token: 0x0400088A RID: 2186
		public static readonly FeatureDefinition UMPermissions;

		// Token: 0x0400088B RID: 2187
		public static readonly FeatureDefinition UMFaxPermissions;

		// Token: 0x0400088C RID: 2188
		public static readonly FeatureDefinition UMOutDialingPermissions;

		// Token: 0x0400088D RID: 2189
		public static readonly FeatureDefinition UMPersonalAutoAttendantPermissions;

		// Token: 0x0400088E RID: 2190
		public static readonly FeatureDefinition MailTipsPermissions;

		// Token: 0x0400088F RID: 2191
		public static readonly FeatureDefinition OWAPermissions;

		// Token: 0x04000890 RID: 2192
		public static readonly FeatureDefinition SMSPermissions;

		// Token: 0x04000891 RID: 2193
		public static readonly FeatureDefinition SetHiddenFromAddressListPermissions;

		// Token: 0x04000892 RID: 2194
		public static readonly FeatureDefinition NewUserPasswordManagementPermissions;

		// Token: 0x04000893 RID: 2195
		public static readonly FeatureDefinition NewUserResetPasswordOnNextLogonPermissions;

		// Token: 0x04000894 RID: 2196
		public static readonly FeatureDefinition UserLiveIdManagementPermissions;

		// Token: 0x04000895 RID: 2197
		public static readonly FeatureDefinition MSOIdPermissions;

		// Token: 0x04000896 RID: 2198
		public static readonly FeatureDefinition ResetUserPasswordManagementPermissions;

		// Token: 0x04000897 RID: 2199
		public static readonly FeatureDefinition UserMailboxAccessPermissions;

		// Token: 0x04000898 RID: 2200
		public static readonly FeatureDefinition MailboxRecoveryPermissions;

		// Token: 0x04000899 RID: 2201
		public static readonly FeatureDefinition PopSyncPermissions;

		// Token: 0x0400089A RID: 2202
		public static readonly FeatureDefinition AddSecondaryDomainPermissions;

		// Token: 0x0400089B RID: 2203
		public static readonly FeatureDefinition HotmailSyncPermissions;

		// Token: 0x0400089C RID: 2204
		public static readonly FeatureDefinition ImapSyncPermissions;

		// Token: 0x0400089D RID: 2205
		public static readonly FeatureDefinition OrganizationalAffinityPermissions;

		// Token: 0x0400089E RID: 2206
		public static readonly FeatureDefinition MessageTrackingPermissions;

		// Token: 0x0400089F RID: 2207
		public static readonly FeatureDefinition ActiveSyncDeviceDataAccessPermissions;

		// Token: 0x040008A0 RID: 2208
		public static readonly FeatureDefinition MOWADeviceDataAccessPermissions;

		// Token: 0x040008A1 RID: 2209
		public static readonly FeatureDefinition GroupAsGroupSyncPermissions;

		// Token: 0x040008A2 RID: 2210
		public static readonly FeatureDefinition LitigationHoldPermissions;

		// Token: 0x040008A3 RID: 2211
		public static readonly FeatureDefinition ArchivePermissions;

		// Token: 0x040008A4 RID: 2212
		public static readonly FeatureDefinition PermissionManagementEnabled;

		// Token: 0x040008A5 RID: 2213
		public static readonly FeatureDefinition PrivacyLink;

		// Token: 0x040008A6 RID: 2214
		public static readonly FeatureDefinition ApplicationImpersonationEnabled;

		// Token: 0x040008A7 RID: 2215
		public static readonly FeatureDefinition MailTipsEnabled;

		// Token: 0x040008A8 RID: 2216
		public static readonly FeatureDefinition AddressListsEnabled;

		// Token: 0x040008A9 RID: 2217
		public static readonly FeatureDefinition FastRecipientCountingDisabled;

		// Token: 0x040008AA RID: 2218
		public static readonly FeatureDefinition OfflineAddressBookEnabled;

		// Token: 0x040008AB RID: 2219
		public static readonly FeatureDefinition OpenDomainRoutingEnabled;

		// Token: 0x040008AC RID: 2220
		public static readonly FeatureDefinition AddOutlookAcceptedDomains;

		// Token: 0x040008AD RID: 2221
		public static readonly FeatureDefinition GALSyncEnabled;

		// Token: 0x040008AE RID: 2222
		public static readonly FeatureDefinition CommonConfiguration;

		// Token: 0x040008AF RID: 2223
		public static readonly FeatureDefinition HideAdminAccessWarningEnabled;

		// Token: 0x040008B0 RID: 2224
		public static readonly FeatureDefinition SkipToUAndParentalControlCheckEnabled;

		// Token: 0x040008B1 RID: 2225
		public static readonly FeatureDefinition SMTPAddressCheckWithAcceptedDomainEnabled;

		// Token: 0x040008B2 RID: 2226
		public static readonly FeatureDefinition AutoReplyEnabled;

		// Token: 0x040008B3 RID: 2227
		public static readonly FeatureDefinition AutoForwardEnabled;

		// Token: 0x040008B4 RID: 2228
		public static readonly FeatureDefinition MSOSyncEnabled;

		// Token: 0x040008B5 RID: 2229
		public static readonly FeatureDefinition AllowDeleteOfExternalIdentityUponRemove;

		// Token: 0x040008B6 RID: 2230
		public static readonly FeatureDefinition SearchMessageEnabled;

		// Token: 0x040008B7 RID: 2231
		public static readonly FeatureDefinition OwaInstantMessagingType;

		// Token: 0x040008B8 RID: 2232
		public static readonly FeatureDefinition RecipientManagementPermissions;

		// Token: 0x040008B9 RID: 2233
		public static readonly FeatureDefinition DistributionListCountQuota;

		// Token: 0x040008BA RID: 2234
		public static readonly FeatureDefinition MailboxCountQuota;

		// Token: 0x040008BB RID: 2235
		public static readonly FeatureDefinition MailUserCountQuota;

		// Token: 0x040008BC RID: 2236
		public static readonly FeatureDefinition ContactCountQuota;

		// Token: 0x040008BD RID: 2237
		public static readonly FeatureDefinition TeamMailboxCountQuota;

		// Token: 0x040008BE RID: 2238
		public static readonly FeatureDefinition PublicFolderMailboxCountQuota;

		// Token: 0x040008BF RID: 2239
		public static readonly FeatureDefinition MailPublicFolderCountQuota;

		// Token: 0x040008C0 RID: 2240
		public static readonly FeatureDefinition SupervisionPermissions;

		// Token: 0x040008C1 RID: 2241
		public static readonly FeatureDefinition SupervisionEnabled;

		// Token: 0x040008C2 RID: 2242
		public static readonly FeatureDefinition TemplateTenant;

		// Token: 0x040008C3 RID: 2243
		public static readonly FeatureDefinition TransportRulesCollectionsEnabled;

		// Token: 0x040008C4 RID: 2244
		public static readonly FeatureDefinition SyncAccountsEnabled;

		// Token: 0x040008C5 RID: 2245
		public static readonly FeatureDefinition RecipientMailSubmissionRateQuota;

		// Token: 0x040008C6 RID: 2246
		public static readonly FeatureDefinition ImapMigrationPermissions;

		// Token: 0x040008C7 RID: 2247
		public static readonly FeatureDefinition HotmailMigrationPermissions;

		// Token: 0x040008C8 RID: 2248
		public static readonly FeatureDefinition ExchangeMigrationPermissions;

		// Token: 0x040008C9 RID: 2249
		public static readonly FeatureDefinition MultiEngineAVEnabled;

		// Token: 0x040008CA RID: 2250
		public static readonly FeatureDefinition CommonHydrateableObjectsSharedEnabled;

		// Token: 0x040008CB RID: 2251
		public static readonly FeatureDefinition AdvancedHydrateableObjectsSharedEnabled;

		// Token: 0x040008CC RID: 2252
		public static readonly FeatureDefinition ShareableConfigurationEnabled;

		// Token: 0x040008CD RID: 2253
		public static readonly FeatureDefinition IRMPremiumFeaturesPermissions;

		// Token: 0x040008CE RID: 2254
		public static readonly FeatureDefinition RBACManagementPermissions;

		// Token: 0x040008CF RID: 2255
		public static readonly FeatureDefinition PerMBXPlanRoleAssignmentPolicyEnabled;

		// Token: 0x040008D0 RID: 2256
		public static readonly FeatureDefinition RoleAssignmentPolicyPermissions;

		// Token: 0x040008D1 RID: 2257
		public static readonly FeatureDefinition PerMBXPlanOWAPolicyEnabled;

		// Token: 0x040008D2 RID: 2258
		public static readonly FeatureDefinition OWAMailboxPolicyPermissions;

		// Token: 0x040008D3 RID: 2259
		public static readonly FeatureDefinition PerMBXPlanRetentionPolicyEnabled;

		// Token: 0x040008D4 RID: 2260
		public static readonly FeatureDefinition ReducedOutOfTheBoxMrmTagsEnabled;

		// Token: 0x040008D5 RID: 2261
		public static readonly FeatureDefinition AddressBookPolicyPermissions;

		// Token: 0x040008D6 RID: 2262
		public static readonly FeatureDefinition LicenseEnforcementEnabled;

		// Token: 0x040008D7 RID: 2263
		public static readonly FeatureDefinition PerimeterSafelistingUIMode;

		// Token: 0x040008D8 RID: 2264
		public static readonly FeatureDefinition ExchangeHostedFilteringAdminCenterAvailabilityEnabled;

		// Token: 0x040008D9 RID: 2265
		public static readonly FeatureDefinition ExchangeHostedFilteringPerimeterEnabled;

		// Token: 0x040008DA RID: 2266
		public static readonly FeatureDefinition ApplicationImpersonationRegularRoleAssignmentEnabled;

		// Token: 0x040008DB RID: 2267
		public static readonly FeatureDefinition MailboxImportExportRegularRoleAssignmentEnabled;

		// Token: 0x040008DC RID: 2268
		public static readonly FeatureDefinition MailboxQuotaPermissions;

		// Token: 0x040008DD RID: 2269
		public static readonly FeatureDefinition MailboxSIRPermissions;

		// Token: 0x040008DE RID: 2270
		public static readonly FeatureDefinition PublicFoldersEnabled;

		// Token: 0x040008DF RID: 2271
		public static readonly FeatureDefinition QuarantineEnabled;

		// Token: 0x040008E0 RID: 2272
		public static readonly FeatureDefinition UseServicePlanAsCounterInstanceName;

		// Token: 0x040008E1 RID: 2273
		public static readonly FeatureDefinition RIMRoleGroupEnabled;

		// Token: 0x040008E2 RID: 2274
		public static readonly FeatureDefinition CalendarVersionStoreEnabled;

		// Token: 0x040008E3 RID: 2275
		public static readonly FeatureDefinition HostedSpamFilteringPolicyCustomizationEnabled;

		// Token: 0x040008E4 RID: 2276
		public static readonly FeatureDefinition MalwareFilteringPolicyCustomizationEnabled;

		// Token: 0x040008E5 RID: 2277
		public static readonly FeatureDefinition EXOCoreFeatures;

		// Token: 0x040008E6 RID: 2278
		public static readonly FeatureDefinition MessageTrace;

		// Token: 0x040008E7 RID: 2279
		public static readonly FeatureDefinition AcceptedDomains;

		// Token: 0x040008E8 RID: 2280
		public static readonly FeatureDefinition ServiceConnectors;

		// Token: 0x040008E9 RID: 2281
		public static readonly FeatureDefinition SoftDeletedFeatureManagementPermissions;

		// Token: 0x040008EA RID: 2282
		public static readonly FeatureDefinition PilotEnabled;

		// Token: 0x040008EB RID: 2283
		public static readonly FeatureDefinition DataLossPreventionEnabled;
	}
}
