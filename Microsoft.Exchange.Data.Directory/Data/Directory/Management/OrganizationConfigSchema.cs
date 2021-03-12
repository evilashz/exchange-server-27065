using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000740 RID: 1856
	internal sealed class OrganizationConfigSchema : ADPresentationSchema
	{
		// Token: 0x06005A1F RID: 23071 RVA: 0x0013CE91 File Offset: 0x0013B091
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADOrganizationConfigSchema>();
		}

		// Token: 0x04003C64 RID: 15460
		public new static readonly ADPropertyDefinition OrganizationId = ADObjectSchema.OrganizationId;

		// Token: 0x04003C65 RID: 15461
		public static readonly ADPropertyDefinition AdminDisplayName = ADConfigurationObjectSchema.AdminDisplayName;

		// Token: 0x04003C66 RID: 15462
		public static readonly ADPropertyDefinition LegacyExchangeDN = OrganizationSchema.LegacyExchangeDN;

		// Token: 0x04003C67 RID: 15463
		public static readonly ADPropertyDefinition Heuristics = OrganizationSchema.Heuristics;

		// Token: 0x04003C68 RID: 15464
		public static readonly ADPropertyDefinition ResourceAddressLists = OrganizationSchema.ResourceAddressLists;

		// Token: 0x04003C69 RID: 15465
		public static readonly ADPropertyDefinition IsMixedMode = OrganizationSchema.IsMixedMode;

		// Token: 0x04003C6A RID: 15466
		public static readonly ADPropertyDefinition IsAddressListPagingEnabled = OrganizationSchema.IsAddressListPagingEnabled;

		// Token: 0x04003C6B RID: 15467
		public static readonly ADPropertyDefinition ManagedFolderHomepage = OrganizationSchema.ManagedFolderHomepage;

		// Token: 0x04003C6C RID: 15468
		public static readonly ADPropertyDefinition ForeignForestFQDN = OrganizationSchema.ForeignForestFQDN;

		// Token: 0x04003C6D RID: 15469
		public static readonly ADPropertyDefinition ForeignForestOrgAdminUSGSid = OrganizationSchema.ForeignForestOrgAdminUSGSid;

		// Token: 0x04003C6E RID: 15470
		public static readonly ADPropertyDefinition ForeignForestRecipientAdminUSGSid = OrganizationSchema.ForeignForestRecipientAdminUSGSid;

		// Token: 0x04003C6F RID: 15471
		public static readonly ADPropertyDefinition ForeignForestViewOnlyAdminUSGSid = OrganizationSchema.ForeignForestViewOnlyAdminUSGSid;

		// Token: 0x04003C70 RID: 15472
		public static readonly ADPropertyDefinition ObjectVersion = OrganizationSchema.ObjectVersion;

		// Token: 0x04003C71 RID: 15473
		public static readonly ADPropertyDefinition SCLJunkThreshold = OrganizationSchema.SCLJunkThreshold;

		// Token: 0x04003C72 RID: 15474
		public static readonly ADPropertyDefinition AcceptedDomainNames = OrganizationSchema.AcceptedDomainNames;

		// Token: 0x04003C73 RID: 15475
		public static readonly ADPropertyDefinition MimeTypes = OrganizationSchema.MimeTypes;

		// Token: 0x04003C74 RID: 15476
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientEmailAddresses = OrganizationSchema.MicrosoftExchangeRecipientEmailAddresses;

		// Token: 0x04003C75 RID: 15477
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientReplyRecipient = OrganizationSchema.MicrosoftExchangeRecipientReplyRecipient;

		// Token: 0x04003C76 RID: 15478
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientPrimarySmtpAddress = OrganizationSchema.MicrosoftExchangeRecipientPrimarySmtpAddress;

		// Token: 0x04003C77 RID: 15479
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientEmailAddressPolicyEnabled = OrganizationSchema.MicrosoftExchangeRecipientEmailAddressPolicyEnabled;

		// Token: 0x04003C78 RID: 15480
		public static readonly ADPropertyDefinition Industry = OrganizationSchema.Industry;

		// Token: 0x04003C79 RID: 15481
		public static readonly ADPropertyDefinition CustomerFeedbackEnabled = OrganizationSchema.CustomerFeedbackEnabled;

		// Token: 0x04003C7A RID: 15482
		public static readonly ADPropertyDefinition OrganizationSummary = OrganizationSchema.OrganizationSummary;

		// Token: 0x04003C7B RID: 15483
		public static readonly ADPropertyDefinition MailTipsExternalRecipientsTipsEnabled = OrganizationSchema.MailTipsExternalRecipientsTipsEnabled;

		// Token: 0x04003C7C RID: 15484
		public static readonly ADPropertyDefinition MailTipsLargeAudienceThreshold = OrganizationSchema.MailTipsLargeAudienceThreshold;

		// Token: 0x04003C7D RID: 15485
		public static readonly ADPropertyDefinition MailTipsMailboxSourcedTipsEnabled = OrganizationSchema.MailTipsMailboxSourcedTipsEnabled;

		// Token: 0x04003C7E RID: 15486
		public static readonly ADPropertyDefinition MailTipsGroupMetricsEnabled = OrganizationSchema.MailTipsGroupMetricsEnabled;

		// Token: 0x04003C7F RID: 15487
		public static readonly ADPropertyDefinition MailTipsAllTipsEnabled = OrganizationSchema.MailTipsAllTipsEnabled;

		// Token: 0x04003C80 RID: 15488
		public static readonly ADPropertyDefinition ReadTrackingEnabled = OrganizationSchema.ReadTrackingEnabled;

		// Token: 0x04003C81 RID: 15489
		public static readonly ADPropertyDefinition DistributionGroupDefaultOU = OrganizationSchema.DistributionGroupDefaultOU;

		// Token: 0x04003C82 RID: 15490
		public static readonly ADPropertyDefinition DistributionGroupNameBlockedWordsList = OrganizationSchema.DistributionGroupNameBlockedWordsList;

		// Token: 0x04003C83 RID: 15491
		public static readonly ADPropertyDefinition DistributionGroupNamingPolicy = OrganizationSchema.DistributionGroupNamingPolicy;

		// Token: 0x04003C84 RID: 15492
		public static readonly ADPropertyDefinition ForwardSyncLiveIdBusinessInstance = OrganizationSchema.ForwardSyncLiveIdBusinessInstance;

		// Token: 0x04003C85 RID: 15493
		public static readonly ADPropertyDefinition ExchangeNotificationEnabled = OrganizationSchema.ExchangeNotificationEnabled;

		// Token: 0x04003C86 RID: 15494
		public static readonly ADPropertyDefinition ExchangeNotificationRecipients = OrganizationSchema.ExchangeNotificationRecipients;

		// Token: 0x04003C87 RID: 15495
		public static readonly ADPropertyDefinition EwsEnabled = OrganizationSchema.EwsEnabled;

		// Token: 0x04003C88 RID: 15496
		public static readonly ADPropertyDefinition EwsAllowOutlook = OrganizationSchema.EwsAllowOutlook;

		// Token: 0x04003C89 RID: 15497
		public static readonly ADPropertyDefinition EwsAllowMacOutlook = OrganizationSchema.EwsAllowMacOutlook;

		// Token: 0x04003C8A RID: 15498
		public static readonly ADPropertyDefinition EwsAllowEntourage = OrganizationSchema.EwsAllowEntourage;

		// Token: 0x04003C8B RID: 15499
		public static readonly ADPropertyDefinition EwsApplicationAccessPolicy = OrganizationSchema.EwsApplicationAccessPolicy;

		// Token: 0x04003C8C RID: 15500
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutInterval = OrganizationSchema.ActivityBasedAuthenticationTimeoutInterval;

		// Token: 0x04003C8D RID: 15501
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutDisabled = OrganizationSchema.ActivityBasedAuthenticationTimeoutDisabled;

		// Token: 0x04003C8E RID: 15502
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled = OrganizationSchema.ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled;

		// Token: 0x04003C8F RID: 15503
		public static readonly ADPropertyDefinition AppsForOfficeDisabled = OrganizationSchema.AppsForOfficeDisabled;

		// Token: 0x04003C90 RID: 15504
		public static readonly ADPropertyDefinition IsLicensingEnforced = OrganizationSchema.IsLicensingEnforced;

		// Token: 0x04003C91 RID: 15505
		public static readonly ADPropertyDefinition IsTenantAccessBlocked = OrganizationSchema.IsTenantAccessBlocked;

		// Token: 0x04003C92 RID: 15506
		public static readonly ADPropertyDefinition IsDehydrated = OrganizationSchema.IsDehydrated;

		// Token: 0x04003C93 RID: 15507
		public static readonly ADPropertyDefinition RBACConfigurationVersion = OrganizationSchema.RBACConfigurationVersion;

		// Token: 0x04003C94 RID: 15508
		public static readonly ADPropertyDefinition HABRootDepartmentLink = OrganizationSchema.HABRootDepartmentLink;

		// Token: 0x04003C95 RID: 15509
		public static readonly ADPropertyDefinition EwsExceptions = OrganizationSchema.EwsExceptions;

		// Token: 0x04003C96 RID: 15510
		public static readonly ADPropertyDefinition AVAuthenticationService = OrganizationSchema.AVAuthenticationService;

		// Token: 0x04003C97 RID: 15511
		public static readonly ADPropertyDefinition SIPAccessService = OrganizationSchema.SIPAccessService;

		// Token: 0x04003C98 RID: 15512
		public static readonly ADPropertyDefinition SIPSessionBorderController = OrganizationSchema.SIPSessionBorderController;

		// Token: 0x04003C99 RID: 15513
		public static readonly ADPropertyDefinition IsGuidPrefixedLegacyDnDisabled = OrganizationSchema.IsGuidPrefixedLegacyDnDisabled;

		// Token: 0x04003C9A RID: 15514
		public static readonly ADPropertyDefinition DefaultPublicFolderMailbox = OrganizationSchema.DefaultPublicFolderMailbox;

		// Token: 0x04003C9B RID: 15515
		public static readonly ADPropertyDefinition RemotePublicFolderMailboxes = OrganizationSchema.RemotePublicFolderMailboxes;

		// Token: 0x04003C9C RID: 15516
		public static readonly ADPropertyDefinition UMAvailableLanguages = OrganizationSchema.UMAvailableLanguages;

		// Token: 0x04003C9D RID: 15517
		public static readonly ADPropertyDefinition MaxConcurrentMigrations = OrganizationSchema.MaxConcurrentMigrations;

		// Token: 0x04003C9E RID: 15518
		public static readonly ADPropertyDefinition MaxAddressBookPolicies = OrganizationSchema.MaxAddressBookPolicies;

		// Token: 0x04003C9F RID: 15519
		public static readonly ADPropertyDefinition MaxOfflineAddressBooks = OrganizationSchema.MaxOfflineAddressBooks;

		// Token: 0x04003CA0 RID: 15520
		public static readonly ADPropertyDefinition IsExcludedFromOnboardMigration = OrganizationSchema.IsExcludedFromOnboardMigration;

		// Token: 0x04003CA1 RID: 15521
		public static readonly ADPropertyDefinition IsExcludedFromOffboardMigration = OrganizationSchema.IsExcludedFromOffboardMigration;

		// Token: 0x04003CA2 RID: 15522
		public static readonly ADPropertyDefinition IsFfoMigrationInProgress = OrganizationSchema.IsFfoMigrationInProgress;

		// Token: 0x04003CA3 RID: 15523
		public static readonly ADPropertyDefinition IsProcessEhaMigratedMessagesEnabled = OrganizationSchema.IsProcessEhaMigratedMessagesEnabled;

		// Token: 0x04003CA4 RID: 15524
		public static readonly ADPropertyDefinition PublicFoldersLockedForMigration = OrganizationSchema.PublicFoldersLockedForMigration;

		// Token: 0x04003CA5 RID: 15525
		public static readonly ADPropertyDefinition PublicFolderMigrationComplete = OrganizationSchema.PublicFolderMigrationComplete;

		// Token: 0x04003CA6 RID: 15526
		public static readonly ADPropertyDefinition PublicFolderMailboxesLockedForNewConnections = OrganizationSchema.PublicFolderMailboxesLockedForNewConnections;

		// Token: 0x04003CA7 RID: 15527
		public static readonly ADPropertyDefinition PublicFolderMailboxesMigrationComplete = OrganizationSchema.PublicFolderMailboxesMigrationComplete;

		// Token: 0x04003CA8 RID: 15528
		public static readonly ADPropertyDefinition PublicFoldersEnabled = OrganizationSchema.PublicFoldersEnabled;

		// Token: 0x04003CA9 RID: 15529
		public static readonly ADPropertyDefinition IsMailboxForcedReplicationDisabled = OrganizationSchema.IsMailboxForcedReplicationDisabled;

		// Token: 0x04003CAA RID: 15530
		public static readonly ADPropertyDefinition AdfsAuthenticationRawConfiguration = OrganizationSchema.AdfsAuthenticationRawConfiguration;

		// Token: 0x04003CAB RID: 15531
		public static readonly ADPropertyDefinition AdfsIssuer = OrganizationSchema.AdfsIssuer;

		// Token: 0x04003CAC RID: 15532
		public static readonly ADPropertyDefinition AdfsAudienceUris = OrganizationSchema.AdfsAudienceUris;

		// Token: 0x04003CAD RID: 15533
		public static readonly ADPropertyDefinition AdfsSignCertificateThumbprints = OrganizationSchema.AdfsSignCertificateThumbprints;

		// Token: 0x04003CAE RID: 15534
		public static readonly ADPropertyDefinition AdfsEncryptCertificateThumbprint = OrganizationSchema.AdfsEncryptCertificateThumbprint;

		// Token: 0x04003CAF RID: 15535
		public static readonly ADPropertyDefinition IsSyncPropertySetUpgradeAllowed = OrganizationSchema.IsSyncPropertySetUpgradeAllowed;

		// Token: 0x04003CB0 RID: 15536
		public static readonly ADPropertyDefinition AdminDisplayVersion = OrganizationSchema.AdminDisplayVersion;

		// Token: 0x04003CB1 RID: 15537
		public static readonly ADPropertyDefinition PreviousAdminDisplayVersion = OrganizationSchema.PreviousMailboxRelease;

		// Token: 0x04003CB2 RID: 15538
		public static readonly ADPropertyDefinition IsUpgradingOrganization = OrganizationSchema.IsUpgradingOrganization;

		// Token: 0x04003CB3 RID: 15539
		public static readonly ADPropertyDefinition IsUpdatingServicePlan = OrganizationSchema.IsUpdatingServicePlan;

		// Token: 0x04003CB4 RID: 15540
		public static readonly ADPropertyDefinition ServicePlan = ADOrganizationConfigSchema.ServicePlan;

		// Token: 0x04003CB5 RID: 15541
		public static readonly ADPropertyDefinition TargetServicePlan = ADOrganizationConfigSchema.TargetServicePlan;

		// Token: 0x04003CB6 RID: 15542
		public static readonly ADPropertyDefinition WACDiscoveryEndpoint = OrganizationSchema.WACDiscoveryEndpoint;

		// Token: 0x04003CB7 RID: 15543
		public static readonly ADPropertyDefinition DefaultPublicFolderAgeLimit = OrganizationSchema.DefaultPublicFolderAgeLimit;

		// Token: 0x04003CB8 RID: 15544
		public static readonly ADPropertyDefinition DefaultPublicFolderIssueWarningQuota = OrganizationSchema.DefaultPublicFolderIssueWarningQuota;

		// Token: 0x04003CB9 RID: 15545
		public static readonly ADPropertyDefinition DefaultPublicFolderProhibitPostQuota = OrganizationSchema.DefaultPublicFolderProhibitPostQuota;

		// Token: 0x04003CBA RID: 15546
		public static readonly ADPropertyDefinition DefaultPublicFolderMaxItemSize = OrganizationSchema.DefaultPublicFolderMaxItemSize;

		// Token: 0x04003CBB RID: 15547
		public static readonly ADPropertyDefinition DefaultPublicFolderDeletedItemRetention = OrganizationSchema.DefaultPublicFolderDeletedItemRetention;

		// Token: 0x04003CBC RID: 15548
		public static readonly ADPropertyDefinition DefaultPublicFolderMovedItemRetention = OrganizationSchema.DefaultPublicFolderMovedItemRetention;

		// Token: 0x04003CBD RID: 15549
		public static readonly ADPropertyDefinition SiteMailboxCreationURL = OrganizationSchema.SiteMailboxCreationURL;

		// Token: 0x04003CBE RID: 15550
		public static readonly ADPropertyDefinition PreferredInternetCodePageForShiftJis = OrganizationSchema.PreferredInternetCodePageForShiftJis;

		// Token: 0x04003CBF RID: 15551
		public static readonly ADPropertyDefinition ByteEncoderTypeFor7BitCharsets = OrganizationSchema.ByteEncoderTypeFor7BitCharsets;

		// Token: 0x04003CC0 RID: 15552
		public static readonly ADPropertyDefinition RequiredCharsetCoverage = OrganizationSchema.RequiredCharsetCoverage;

		// Token: 0x04003CC1 RID: 15553
		public static readonly ADPropertyDefinition PublicComputersDetectionEnabled = OrganizationSchema.PublicComputersDetectionEnabled;

		// Token: 0x04003CC2 RID: 15554
		public static readonly ADPropertyDefinition RmsoSubscriptionStatus = OrganizationSchema.RmsoSubscriptionStatus;

		// Token: 0x04003CC3 RID: 15555
		public static readonly ADPropertyDefinition IntuneManagedStatus = OrganizationSchema.IntuneManagedStatus;

		// Token: 0x04003CC4 RID: 15556
		public static readonly ADPropertyDefinition HybridConfigurationStatus = OrganizationSchema.HybridConfigurationStatus;

		// Token: 0x04003CC5 RID: 15557
		public static readonly ADPropertyDefinition ReleaseTrack = OrganizationSchema.ReleaseTrack;

		// Token: 0x04003CC6 RID: 15558
		public static readonly ADPropertyDefinition SharePointUrl = ADOrganizationConfigSchema.SharePointUrl;

		// Token: 0x04003CC7 RID: 15559
		public static readonly ADPropertyDefinition MapiHttpEnabled = OrganizationSchema.MapiHttpEnabled;

		// Token: 0x04003CC8 RID: 15560
		public static readonly ADPropertyDefinition OAuth2ClientProfileEnabled = OrganizationSchema.OAuth2ClientProfileEnabled;

		// Token: 0x04003CC9 RID: 15561
		public static readonly ADPropertyDefinition ACLableSyncedObjectEnabled = OrganizationSchema.ACLableSyncedObjectEnabled;
	}
}
