using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000773 RID: 1907
	internal sealed class TenantOrganizationPresentationObjectSchema : ADPresentationSchema
	{
		// Token: 0x06005D9C RID: 23964 RVA: 0x00142C5B File Offset: 0x00140E5B
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ExchangeConfigurationUnitSchema>();
		}

		// Token: 0x04003F3B RID: 16187
		public new static readonly ADPropertyDefinition OrganizationId = ExchangeConfigurationUnitSchema.OrganizationId;

		// Token: 0x04003F3C RID: 16188
		public static readonly ADPropertyDefinition OrganizationStatus = ExchangeConfigurationUnitSchema.OrganizationStatus;

		// Token: 0x04003F3D RID: 16189
		public static readonly ADPropertyDefinition WhenOrganizationStatusSet = ExchangeConfigurationUnitSchema.WhenOrganizationStatusSet;

		// Token: 0x04003F3E RID: 16190
		public static readonly ADPropertyDefinition IsUpgradingOrganization = OrganizationSchema.IsUpgradingOrganization;

		// Token: 0x04003F3F RID: 16191
		public static readonly ADPropertyDefinition IsPilotingOrganization = OrganizationSchema.IsPilotingOrganization;

		// Token: 0x04003F40 RID: 16192
		public static readonly ADPropertyDefinition IsUpdatingServicePlan = OrganizationSchema.IsUpdatingServicePlan;

		// Token: 0x04003F41 RID: 16193
		public static readonly ADPropertyDefinition IsFfoMigrationInProgress = OrganizationSchema.IsFfoMigrationInProgress;

		// Token: 0x04003F42 RID: 16194
		public static readonly ADPropertyDefinition IsUpgradeOperationInProgress = OrganizationSchema.IsUpgradeOperationInProgress;

		// Token: 0x04003F43 RID: 16195
		public static readonly ADPropertyDefinition ServicePlan = ExchangeConfigurationUnitSchema.ServicePlan;

		// Token: 0x04003F44 RID: 16196
		public static readonly ADPropertyDefinition TargetServicePlan = ExchangeConfigurationUnitSchema.TargetServicePlan;

		// Token: 0x04003F45 RID: 16197
		public static readonly ADPropertyDefinition ProgramId = ExchangeConfigurationUnitSchema.ProgramId;

		// Token: 0x04003F46 RID: 16198
		public static readonly ADPropertyDefinition OfferId = ExchangeConfigurationUnitSchema.OfferId;

		// Token: 0x04003F47 RID: 16199
		public static readonly ADPropertyDefinition ManagementSiteLink = ExchangeConfigurationUnitSchema.ManagementSiteLink;

		// Token: 0x04003F48 RID: 16200
		public static readonly ADPropertyDefinition IsFederated = OrganizationSchema.IsFederated;

		// Token: 0x04003F49 RID: 16201
		public static readonly ADPropertyDefinition SkipToUAndParentalControlCheck = OrganizationSchema.SkipToUAndParentalControlCheck;

		// Token: 0x04003F4A RID: 16202
		public static readonly ADPropertyDefinition HideAdminAccessWarning = OrganizationSchema.HideAdminAccessWarning;

		// Token: 0x04003F4B RID: 16203
		public static readonly ADPropertyDefinition ShowAdminAccessWarning = OrganizationSchema.ShowAdminAccessWarning;

		// Token: 0x04003F4C RID: 16204
		public static readonly ADPropertyDefinition SMTPAddressCheckWithAcceptedDomain = OrganizationSchema.SMTPAddressCheckWithAcceptedDomain;

		// Token: 0x04003F4D RID: 16205
		public static readonly ADPropertyDefinition ExternalDirectoryOrganizationId = ExchangeConfigurationUnitSchema.ExternalDirectoryOrganizationId;

		// Token: 0x04003F4E RID: 16206
		public static readonly ADPropertyDefinition SupportedSharedConfigurations = OrganizationSchema.SupportedSharedConfigurations;

		// Token: 0x04003F4F RID: 16207
		public static readonly ADPropertyDefinition SharedConfigurationInfo = OrganizationSchema.SharedConfigurationInfo;

		// Token: 0x04003F50 RID: 16208
		public static readonly ADPropertyDefinition EnableAsSharedConfiguration = OrganizationSchema.EnableAsSharedConfiguration;

		// Token: 0x04003F51 RID: 16209
		public static readonly ADPropertyDefinition ImmutableConfiguration = OrganizationSchema.ImmutableConfiguration;

		// Token: 0x04003F52 RID: 16210
		public static readonly ADPropertyDefinition IsLicensingEnforced = OrganizationSchema.IsLicensingEnforced;

		// Token: 0x04003F53 RID: 16211
		public static readonly ADPropertyDefinition IsTenantAccessBlocked = OrganizationSchema.IsTenantAccessBlocked;

		// Token: 0x04003F54 RID: 16212
		public static readonly ADPropertyDefinition IsDehydrated = OrganizationSchema.IsDehydrated;

		// Token: 0x04003F55 RID: 16213
		public static readonly ADPropertyDefinition IsStaticConfigurationShared = OrganizationSchema.IsStaticConfigurationShared;

		// Token: 0x04003F56 RID: 16214
		public static readonly ADPropertyDefinition IsSharingConfiguration = OrganizationSchema.IsSharingConfiguration;

		// Token: 0x04003F57 RID: 16215
		public static readonly ADPropertyDefinition IsTemplateTenant = OrganizationSchema.IsTemplateTenant;

		// Token: 0x04003F58 RID: 16216
		public static readonly ADPropertyDefinition ExcludedFromBackSync = OrganizationSchema.ExcludedFromBackSync;

		// Token: 0x04003F59 RID: 16217
		public static readonly ADPropertyDefinition ExcludedFromForwardSyncEDU2BPOS = OrganizationSchema.ExcludedFromForwardSyncEDU2BPOS;

		// Token: 0x04003F5A RID: 16218
		public static readonly ADPropertyDefinition MSOSyncEnabled = OrganizationSchema.MSOSyncEnabled;

		// Token: 0x04003F5B RID: 16219
		public static readonly ADPropertyDefinition MaxAddressBookPolicies = OrganizationSchema.MaxAddressBookPolicies;

		// Token: 0x04003F5C RID: 16220
		public static readonly ADPropertyDefinition MaxOfflineAddressBooks = OrganizationSchema.MaxOfflineAddressBooks;

		// Token: 0x04003F5D RID: 16221
		public static readonly ADPropertyDefinition UseServicePlanAsCounterInstanceName = OrganizationSchema.UseServicePlanAsCounterInstanceName;

		// Token: 0x04003F5E RID: 16222
		public static readonly ADPropertyDefinition AllowDeleteOfExternalIdentityUponRemove = OrganizationSchema.AllowDeleteOfExternalIdentityUponRemove;

		// Token: 0x04003F5F RID: 16223
		public static readonly ADPropertyDefinition ExchangeUpgradeBucket = ExchangeConfigurationUnitSchema.ExchangeUpgradeBucket;

		// Token: 0x04003F60 RID: 16224
		public static readonly ADPropertyDefinition AdminDisplayVersion = OrganizationSchema.AdminDisplayVersion;

		// Token: 0x04003F61 RID: 16225
		public static readonly ADPropertyDefinition DefaultPublicFolderProhibitPostQuota = OrganizationSchema.DefaultPublicFolderProhibitPostQuota;

		// Token: 0x04003F62 RID: 16226
		public static readonly ADPropertyDefinition DefaultPublicFolderIssueWarningQuota = OrganizationSchema.DefaultPublicFolderIssueWarningQuota;

		// Token: 0x04003F63 RID: 16227
		public static readonly ADPropertyDefinition AsynchronousOperationIds = OrganizationSchema.AsynchronousOperationIds;

		// Token: 0x04003F64 RID: 16228
		public static readonly ADPropertyDefinition IsDirSyncRunning = OrganizationSchema.IsDirSyncRunning;

		// Token: 0x04003F65 RID: 16229
		public static readonly ADPropertyDefinition IsDirSyncStatusPending = OrganizationSchema.IsDirSyncStatusPending;

		// Token: 0x04003F66 RID: 16230
		public static readonly ADPropertyDefinition DirSyncStatus = OrganizationSchema.DirSyncStatus;

		// Token: 0x04003F67 RID: 16231
		public static readonly ADPropertyDefinition DirSyncServiceInstance = ExchangeConfigurationUnitSchema.DirSyncServiceInstance;

		// Token: 0x04003F68 RID: 16232
		public static readonly ADPropertyDefinition SoftDeletedFeatureStatus = OrganizationSchema.SoftDeletedFeatureStatus;

		// Token: 0x04003F69 RID: 16233
		public static readonly ADPropertyDefinition UpgradeStatus = OrganizationSchema.UpgradeStatus;

		// Token: 0x04003F6A RID: 16234
		public static readonly ADPropertyDefinition UpgradeRequest = OrganizationSchema.UpgradeRequest;

		// Token: 0x04003F6B RID: 16235
		public static readonly ADPropertyDefinition UpgradeConstraints = OrganizationSchema.UpgradeConstraints;

		// Token: 0x04003F6C RID: 16236
		public static readonly ADPropertyDefinition UpgradeMessage = OrganizationSchema.UpgradeMessage;

		// Token: 0x04003F6D RID: 16237
		public static readonly ADPropertyDefinition UpgradeDetails = OrganizationSchema.UpgradeDetails;

		// Token: 0x04003F6E RID: 16238
		public static readonly ADPropertyDefinition UpgradeStage = OrganizationSchema.UpgradeStage;

		// Token: 0x04003F6F RID: 16239
		public static readonly ADPropertyDefinition UpgradeStageTimeStamp = OrganizationSchema.UpgradeStageTimeStamp;

		// Token: 0x04003F70 RID: 16240
		public static readonly ADPropertyDefinition UpgradeE14MbxCountForCurrentStage = OrganizationSchema.UpgradeE14MbxCountForCurrentStage;

		// Token: 0x04003F71 RID: 16241
		public static readonly ADPropertyDefinition UpgradeE14RequestCountForCurrentStage = OrganizationSchema.UpgradeE14RequestCountForCurrentStage;

		// Token: 0x04003F72 RID: 16242
		public static readonly ADPropertyDefinition UpgradeLastE14CountsUpdateTime = OrganizationSchema.UpgradeLastE14CountsUpdateTime;

		// Token: 0x04003F73 RID: 16243
		public static readonly ADPropertyDefinition UpgradeConstraintsDisabled = OrganizationSchema.UpgradeConstraintsDisabled;

		// Token: 0x04003F74 RID: 16244
		public static readonly ADPropertyDefinition UpgradeUnitsOverride = OrganizationSchema.UpgradeUnitsOverride;

		// Token: 0x04003F75 RID: 16245
		public static readonly ADPropertyDefinition DefaultMovePriority = OrganizationSchema.DefaultMovePriority;

		// Token: 0x04003F76 RID: 16246
		public static readonly ADPropertyDefinition OrganizationName = ExchangeConfigurationUnitSchema.OrganizationName;

		// Token: 0x04003F77 RID: 16247
		public static readonly ADPropertyDefinition CompanyTags = OrganizationSchema.CompanyTags;

		// Token: 0x04003F78 RID: 16248
		public static readonly ADPropertyDefinition MailboxRelease = OrganizationSchema.MailboxRelease;

		// Token: 0x04003F79 RID: 16249
		public static readonly ADPropertyDefinition PreviousMailboxRelease = OrganizationSchema.PreviousMailboxRelease;

		// Token: 0x04003F7A RID: 16250
		public static readonly ADPropertyDefinition PilotMailboxRelease = OrganizationSchema.PilotMailboxRelease;

		// Token: 0x04003F7B RID: 16251
		public static readonly ADPropertyDefinition ReleaseTrack = OrganizationSchema.ReleaseTrack;

		// Token: 0x04003F7C RID: 16252
		public static readonly ADPropertyDefinition PersistedCapabilities = OrganizationSchema.PersistedCapabilities;

		// Token: 0x04003F7D RID: 16253
		public static readonly ADPropertyDefinition Location = OrganizationSchema.Location;

		// Token: 0x04003F7E RID: 16254
		public static readonly ADPropertyDefinition RelocationConstraints = OrganizationSchema.RelocationConstraints;

		// Token: 0x04003F7F RID: 16255
		public static readonly ADPropertyDefinition OriginatedInDifferentForest = OrganizationSchema.OriginatedInDifferentForest;

		// Token: 0x04003F80 RID: 16256
		public static readonly ADPropertyDefinition IOwnMigrationTenant = ExchangeConfigurationUnitSchema.IOwnMigrationTenant;

		// Token: 0x04003F81 RID: 16257
		public static readonly ADPropertyDefinition IOwnMigrationStatus = ExchangeConfigurationUnitSchema.IOwnMigrationStatus;

		// Token: 0x04003F82 RID: 16258
		public static readonly ADPropertyDefinition IOwnMigrationStatusReport = ExchangeConfigurationUnitSchema.IOwnMigrationStatusReport;

		// Token: 0x04003F83 RID: 16259
		public static readonly ADPropertyDefinition AdminDisplayName = ADConfigurationObjectSchema.AdminDisplayName;
	}
}
