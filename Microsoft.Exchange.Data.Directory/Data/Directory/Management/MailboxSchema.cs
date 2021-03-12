using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000727 RID: 1831
	internal class MailboxSchema : MailEnabledOrgPersonSchema
	{
		// Token: 0x060056D0 RID: 22224 RVA: 0x0013811F File Offset: 0x0013631F
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003AA8 RID: 15016
		public static readonly ADPropertyDefinition AdminDisplayVersion = ADUserSchema.AdminDisplayVersion;

		// Token: 0x04003AA9 RID: 15017
		public static readonly ADPropertyDefinition Database = ADMailboxRecipientSchema.Database;

		// Token: 0x04003AAA RID: 15018
		public static readonly ADPropertyDefinition PreviousDatabase = ADUserSchema.PreviousDatabase;

		// Token: 0x04003AAB RID: 15019
		public static readonly ADPropertyDefinition UseDatabaseRetentionDefaults = ADUserSchema.UseDatabaseRetentionDefaults;

		// Token: 0x04003AAC RID: 15020
		public static readonly ADPropertyDefinition RetainDeletedItemsUntilBackup = ADUserSchema.RetainDeletedItemsUntilBackup;

		// Token: 0x04003AAD RID: 15021
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = ADMailboxRecipientSchema.DeliverToMailboxAndForward;

		// Token: 0x04003AAE RID: 15022
		public static readonly ADPropertyDefinition LitigationHoldEnabled = ADUserSchema.LitigationHoldEnabled;

		// Token: 0x04003AAF RID: 15023
		public static readonly ADPropertyDefinition LitigationHoldDuration = ADRecipientSchema.LitigationHoldDuration;

		// Token: 0x04003AB0 RID: 15024
		public static readonly ADPropertyDefinition SingleItemRecoveryEnabled = ADUserSchema.SingleItemRecoveryEnabled;

		// Token: 0x04003AB1 RID: 15025
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEnabled = ADUserSchema.ElcExpirationSuspensionEnabled;

		// Token: 0x04003AB2 RID: 15026
		public static readonly ADPropertyDefinition ElcExpirationSuspensionEndDate = ADUserSchema.ElcExpirationSuspensionEndDate;

		// Token: 0x04003AB3 RID: 15027
		public static readonly ADPropertyDefinition ElcExpirationSuspensionStartDate = ADUserSchema.ElcExpirationSuspensionStartDate;

		// Token: 0x04003AB4 RID: 15028
		public static readonly ADPropertyDefinition RetentionComment = ADUserSchema.RetentionComment;

		// Token: 0x04003AB5 RID: 15029
		public static readonly ADPropertyDefinition RetentionUrl = ADUserSchema.RetentionUrl;

		// Token: 0x04003AB6 RID: 15030
		public static readonly ADPropertyDefinition LitigationHoldDate = ADUserSchema.LitigationHoldDate;

		// Token: 0x04003AB7 RID: 15031
		public static readonly ADPropertyDefinition LitigationHoldOwner = ADUserSchema.LitigationHoldOwner;

		// Token: 0x04003AB8 RID: 15032
		public static readonly ADPropertyDefinition ManagedFolderMailboxPolicy = ADUserSchema.ManagedFolderMailboxPolicy;

		// Token: 0x04003AB9 RID: 15033
		public static readonly ADPropertyDefinition RetentionPolicy = ADUserSchema.RetentionPolicy;

		// Token: 0x04003ABA RID: 15034
		public static readonly ADPropertyDefinition AddressBookPolicy = ADRecipientSchema.AddressBookPolicy;

		// Token: 0x04003ABB RID: 15035
		public static readonly ADPropertyDefinition ShouldUseDefaultRetentionPolicy = ADUserSchema.ShouldUseDefaultRetentionPolicy;

		// Token: 0x04003ABC RID: 15036
		public static readonly ADPropertyDefinition MessageTrackingReadStatusDisabled = ADRecipientSchema.MessageTrackingReadStatusDisabled;

		// Token: 0x04003ABD RID: 15037
		public static readonly ADPropertyDefinition CalendarRepairDisabled = ADUserSchema.CalendarRepairDisabled;

		// Token: 0x04003ABE RID: 15038
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x04003ABF RID: 15039
		public static readonly ADPropertyDefinition MailboxContainerGuid = ADUserSchema.MailboxContainerGuid;

		// Token: 0x04003AC0 RID: 15040
		public static readonly ADPropertyDefinition AggregatedMailboxGuids = ADUserSchema.AggregatedMailboxGuids;

		// Token: 0x04003AC1 RID: 15041
		public static readonly ADPropertyDefinition MailboxLocations = ADRecipientSchema.MailboxLocations;

		// Token: 0x04003AC2 RID: 15042
		public static readonly ADPropertyDefinition UnifiedMailbox = IADMailStorageSchema.UnifiedMailbox;

		// Token: 0x04003AC3 RID: 15043
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = ADMailboxRecipientSchema.ExchangeSecurityDescriptor;

		// Token: 0x04003AC4 RID: 15044
		public static readonly ADPropertyDefinition ExchangeUserAccountControl = ADUserSchema.ExchangeUserAccountControl;

		// Token: 0x04003AC5 RID: 15045
		public static readonly ADPropertyDefinition ExternalOofOptions = ADMailboxRecipientSchema.ExternalOofOptions;

		// Token: 0x04003AC6 RID: 15046
		public static readonly ADPropertyDefinition ForwardingAddress = ADRecipientSchema.ForwardingAddress;

		// Token: 0x04003AC7 RID: 15047
		public static readonly ADPropertyDefinition ForwardingSmtpAddress = ADRecipientSchema.ForwardingSmtpAddress;

		// Token: 0x04003AC8 RID: 15048
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = ADMailboxRecipientSchema.RetainDeletedItemsFor;

		// Token: 0x04003AC9 RID: 15049
		public static readonly ADPropertyDefinition IsMailboxEnabled = ADMailboxRecipientSchema.IsMailboxEnabled;

		// Token: 0x04003ACA RID: 15050
		public static readonly ADPropertyDefinition LanguagesRaw = ADOrgPersonSchema.LanguagesRaw;

		// Token: 0x04003ACB RID: 15051
		public static readonly ADPropertyDefinition Languages = ADOrgPersonSchema.Languages;

		// Token: 0x04003ACC RID: 15052
		public static readonly ADPropertyDefinition OfflineAddressBook = ADMailboxRecipientSchema.OfflineAddressBook;

		// Token: 0x04003ACD RID: 15053
		public static readonly ADPropertyDefinition ProhibitSendQuota = ADMailboxRecipientSchema.ProhibitSendQuota;

		// Token: 0x04003ACE RID: 15054
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = ADMailboxRecipientSchema.ProhibitSendReceiveQuota;

		// Token: 0x04003ACF RID: 15055
		public static readonly ADPropertyDefinition RecoverableItemsQuota = ADUserSchema.RecoverableItemsQuota;

		// Token: 0x04003AD0 RID: 15056
		public static readonly ADPropertyDefinition RecoverableItemsWarningQuota = ADUserSchema.RecoverableItemsWarningQuota;

		// Token: 0x04003AD1 RID: 15057
		public static readonly ADPropertyDefinition CalendarLoggingQuota = ADUserSchema.CalendarLoggingQuota;

		// Token: 0x04003AD2 RID: 15058
		public static readonly ADPropertyDefinition ProtocolSettings = ADRecipientSchema.ReadOnlyProtocolSettings;

		// Token: 0x04003AD3 RID: 15059
		public static readonly ADPropertyDefinition DowngradeHighPriorityMessagesEnabled = ADUserSchema.DowngradeHighPriorityMessagesEnabled;

		// Token: 0x04003AD4 RID: 15060
		public static readonly ADPropertyDefinition RecipientLimits = ADRecipientSchema.RecipientLimits;

		// Token: 0x04003AD5 RID: 15061
		public static readonly ADPropertyDefinition IsLinked = ADRecipientSchema.IsLinked;

		// Token: 0x04003AD6 RID: 15062
		public static readonly ADPropertyDefinition IsRootPublicFolderMailbox = ADRecipientSchema.IsRootPublicFolderMailbox;

		// Token: 0x04003AD7 RID: 15063
		public static readonly ADPropertyDefinition IsShared = ADRecipientSchema.IsShared;

		// Token: 0x04003AD8 RID: 15064
		public static readonly ADPropertyDefinition IsResource = ADRecipientSchema.IsResource;

		// Token: 0x04003AD9 RID: 15065
		public static readonly ADPropertyDefinition MasterAccountSid = ADRecipientSchema.MasterAccountSid;

		// Token: 0x04003ADA RID: 15066
		public static readonly ADPropertyDefinition LinkedMasterAccount = ADRecipientSchema.LinkedMasterAccount;

		// Token: 0x04003ADB RID: 15067
		public static readonly ADPropertyDefinition ResetPasswordOnNextLogon = ADUserSchema.ResetPasswordOnNextLogon;

		// Token: 0x04003ADC RID: 15068
		public static readonly ADPropertyDefinition ResourceCapacity = ADRecipientSchema.ResourceCapacity;

		// Token: 0x04003ADD RID: 15069
		public static readonly ADPropertyDefinition ResourceCustom = ADRecipientSchema.ResourceCustom;

		// Token: 0x04003ADE RID: 15070
		public static readonly ADPropertyDefinition ResourceType = ADRecipientSchema.ResourceType;

		// Token: 0x04003ADF RID: 15071
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003AE0 RID: 15072
		public static readonly ADPropertyDefinition SCLDeleteThreshold = ADRecipientSchema.SCLDeleteThreshold;

		// Token: 0x04003AE1 RID: 15073
		public static readonly ADPropertyDefinition SCLDeleteEnabled = ADRecipientSchema.SCLDeleteEnabled;

		// Token: 0x04003AE2 RID: 15074
		public static readonly ADPropertyDefinition SCLRejectThreshold = ADRecipientSchema.SCLRejectThreshold;

		// Token: 0x04003AE3 RID: 15075
		public static readonly ADPropertyDefinition SCLRejectEnabled = ADRecipientSchema.SCLRejectEnabled;

		// Token: 0x04003AE4 RID: 15076
		public static readonly ADPropertyDefinition SCLQuarantineThreshold = ADRecipientSchema.SCLQuarantineThreshold;

		// Token: 0x04003AE5 RID: 15077
		public static readonly ADPropertyDefinition SCLQuarantineEnabled = ADRecipientSchema.SCLQuarantineEnabled;

		// Token: 0x04003AE6 RID: 15078
		public static readonly ADPropertyDefinition SCLJunkThreshold = ADRecipientSchema.SCLJunkThreshold;

		// Token: 0x04003AE7 RID: 15079
		public static readonly ADPropertyDefinition SCLJunkEnabled = ADRecipientSchema.SCLJunkEnabled;

		// Token: 0x04003AE8 RID: 15080
		public static readonly ADPropertyDefinition AntispamBypassEnabled = ADRecipientSchema.AntispamBypassEnabled;

		// Token: 0x04003AE9 RID: 15081
		public static readonly ADPropertyDefinition ServerLegacyDN = ADMailboxRecipientSchema.ServerLegacyDN;

		// Token: 0x04003AEA RID: 15082
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x04003AEB RID: 15083
		public static readonly ADPropertyDefinition UseDatabaseQuotaDefaults = ADMailboxRecipientSchema.UseDatabaseQuotaDefaults;

		// Token: 0x04003AEC RID: 15084
		public static readonly ADPropertyDefinition IssueWarningQuota = ADMailboxRecipientSchema.IssueWarningQuota;

		// Token: 0x04003AED RID: 15085
		public static readonly ADPropertyDefinition RulesQuota = ADMailboxRecipientSchema.RulesQuota;

		// Token: 0x04003AEE RID: 15086
		public static readonly ADPropertyDefinition Office = ADUserSchema.Office;

		// Token: 0x04003AEF RID: 15087
		public static readonly ADPropertyDefinition UserPrincipalName = ADUserSchema.UserPrincipalName;

		// Token: 0x04003AF0 RID: 15088
		public static readonly ADPropertyDefinition NetID = ADUserSchema.NetID;

		// Token: 0x04003AF1 RID: 15089
		public static readonly ADPropertyDefinition OriginalNetID = ADUserSchema.OriginalNetID;

		// Token: 0x04003AF2 RID: 15090
		public static readonly ADPropertyDefinition UMEnabled = ADUserSchema.UMEnabled;

		// Token: 0x04003AF3 RID: 15091
		public static readonly ADPropertyDefinition MaxSafeSenders = ADUserSchema.MaxSafeSenders;

		// Token: 0x04003AF4 RID: 15092
		public static readonly ADPropertyDefinition MaxBlockedSenders = ADUserSchema.MaxBlockedSenders;

		// Token: 0x04003AF5 RID: 15093
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x04003AF6 RID: 15094
		public static readonly ADPropertyDefinition MailboxPlan = ADRecipientSchema.MailboxPlan;

		// Token: 0x04003AF7 RID: 15095
		public static readonly ADPropertyDefinition RoleAssignmentPolicy = ADRecipientSchema.RoleAssignmentPolicy;

		// Token: 0x04003AF8 RID: 15096
		public static readonly ADPropertyDefinition ThrottlingPolicy = ADRecipientSchema.ThrottlingPolicy;

		// Token: 0x04003AF9 RID: 15097
		public static readonly ADPropertyDefinition ArchiveDatabase = ADUserSchema.ArchiveDatabase;

		// Token: 0x04003AFA RID: 15098
		public static readonly ADPropertyDefinition ArchiveGuid = ADUserSchema.ArchiveGuid;

		// Token: 0x04003AFB RID: 15099
		public static readonly ADPropertyDefinition ArchiveName = ADUserSchema.ArchiveName;

		// Token: 0x04003AFC RID: 15100
		public static readonly ADPropertyDefinition ArchiveQuota = ADUserSchema.ArchiveQuota;

		// Token: 0x04003AFD RID: 15101
		public static readonly ADPropertyDefinition ArchiveWarningQuota = ADUserSchema.ArchiveWarningQuota;

		// Token: 0x04003AFE RID: 15102
		public static readonly ADPropertyDefinition ArchiveDomain = ADUserSchema.ArchiveDomain;

		// Token: 0x04003AFF RID: 15103
		public static readonly ADPropertyDefinition ArchiveStatus = ADUserSchema.ArchiveStatus;

		// Token: 0x04003B00 RID: 15104
		public static readonly ADPropertyDefinition ArchiveState = ADUserSchema.ArchiveState;

		// Token: 0x04003B01 RID: 15105
		public static readonly ADPropertyDefinition IsAuxMailbox = ADUserSchema.IsAuxMailbox;

		// Token: 0x04003B02 RID: 15106
		public static readonly ADPropertyDefinition AuxMailboxParentObjectId = ADUserSchema.AuxMailboxParentObjectId;

		// Token: 0x04003B03 RID: 15107
		public static readonly ADPropertyDefinition ChildAuxMailboxObjectIds = ADUserSchema.AuxMailboxParentObjectIdBL;

		// Token: 0x04003B04 RID: 15108
		public static readonly ADPropertyDefinition MailboxRelationType = ADUserSchema.MailboxRelationType;

		// Token: 0x04003B05 RID: 15109
		public static readonly ADPropertyDefinition JournalArchiveAddress = ADRecipientSchema.JournalArchiveAddress;

		// Token: 0x04003B06 RID: 15110
		public static readonly ADPropertyDefinition RemoteRecipientType = ADUserSchema.RemoteRecipientType;

		// Token: 0x04003B07 RID: 15111
		public static readonly ADPropertyDefinition DisabledArchiveDatabase = ADUserSchema.DisabledArchiveDatabase;

		// Token: 0x04003B08 RID: 15112
		public static readonly ADPropertyDefinition DisabledArchiveGuid = ADUserSchema.DisabledArchiveGuid;

		// Token: 0x04003B09 RID: 15113
		public static readonly ADPropertyDefinition QueryBaseDNRestrictionEnabled = ADRecipientSchema.QueryBaseDNRestrictionEnabled;

		// Token: 0x04003B0A RID: 15114
		public static readonly ADPropertyDefinition SharingPolicy = ADUserSchema.SharingPolicy;

		// Token: 0x04003B0B RID: 15115
		public static readonly ADPropertyDefinition RemoteAccountPolicy = ADUserSchema.RemoteAccountPolicy;

		// Token: 0x04003B0C RID: 15116
		public static readonly ADPropertyDefinition RemotePowerShellEnabled = ADRecipientSchema.RemotePowerShellEnabled;

		// Token: 0x04003B0D RID: 15117
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = ADUserSchema.MailboxMoveTargetMDB;

		// Token: 0x04003B0E RID: 15118
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = ADUserSchema.MailboxMoveSourceMDB;

		// Token: 0x04003B0F RID: 15119
		public static readonly ADPropertyDefinition MailboxMoveTargetArchiveMDB = ADUserSchema.MailboxMoveTargetArchiveMDB;

		// Token: 0x04003B10 RID: 15120
		public static readonly ADPropertyDefinition MailboxMoveSourceArchiveMDB = ADUserSchema.MailboxMoveSourceArchiveMDB;

		// Token: 0x04003B11 RID: 15121
		public static readonly ADPropertyDefinition MailboxMoveFlags = ADUserSchema.MailboxMoveFlags;

		// Token: 0x04003B12 RID: 15122
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = ADUserSchema.MailboxMoveRemoteHostName;

		// Token: 0x04003B13 RID: 15123
		public static readonly ADPropertyDefinition MailboxMoveBatchName = ADUserSchema.MailboxMoveBatchName;

		// Token: 0x04003B14 RID: 15124
		public static readonly ADPropertyDefinition MailboxMoveStatus = ADUserSchema.MailboxMoveStatus;

		// Token: 0x04003B15 RID: 15125
		public static readonly ADPropertyDefinition MailboxRelease = ADUserSchema.MailboxRelease;

		// Token: 0x04003B16 RID: 15126
		public static readonly ADPropertyDefinition ArchiveRelease = ADUserSchema.ArchiveRelease;

		// Token: 0x04003B17 RID: 15127
		public static readonly ADPropertyDefinition IsPersonToPersonTextMessagingEnabled = ADRecipientSchema.IsPersonToPersonTextMessagingEnabled;

		// Token: 0x04003B18 RID: 15128
		public static readonly ADPropertyDefinition IsMachineToPersonTextMessagingEnabled = ADRecipientSchema.IsMachineToPersonTextMessagingEnabled;

		// Token: 0x04003B19 RID: 15129
		public static readonly ADPropertyDefinition UserSMimeCertificate = ADRecipientSchema.SMimeCertificate;

		// Token: 0x04003B1A RID: 15130
		public static readonly ADPropertyDefinition UserCertificate = ADRecipientSchema.Certificate;

		// Token: 0x04003B1B RID: 15131
		public static readonly ADPropertyDefinition CalendarVersionStoreDisabled = ADUserSchema.CalendarVersionStoreDisabled;

		// Token: 0x04003B1C RID: 15132
		public static readonly ADPropertyDefinition ImmutableId = ADRecipientSchema.ImmutableId;

		// Token: 0x04003B1D RID: 15133
		public static readonly ADPropertyDefinition PersistedCapabilities = SharedPropertyDefinitions.PersistedCapabilities;

		// Token: 0x04003B1E RID: 15134
		public static readonly ADPropertyDefinition SKUAssigned = ADRecipientSchema.SKUAssigned;

		// Token: 0x04003B1F RID: 15135
		public static readonly ADPropertyDefinition WhenMailboxCreated = ADMailboxRecipientSchema.WhenMailboxCreated;

		// Token: 0x04003B20 RID: 15136
		public static readonly ADPropertyDefinition SourceAnchor = ADUserSchema.SourceAnchor;

		// Token: 0x04003B21 RID: 15137
		public static readonly ADPropertyDefinition AuditEnabled = ADRecipientSchema.AuditEnabled;

		// Token: 0x04003B22 RID: 15138
		public static readonly ADPropertyDefinition DefaultPublicFolderMailboxValue = ADRecipientSchema.DefaultPublicFolderMailbox;

		// Token: 0x04003B23 RID: 15139
		public static readonly ADPropertyDefinition IsExcludedFromServingHierarchy = ADRecipientSchema.IsExcludedFromServingHierarchy;

		// Token: 0x04003B24 RID: 15140
		public static readonly ADPropertyDefinition IsHierarchyReady = ADRecipientSchema.IsHierarchyReady;

		// Token: 0x04003B25 RID: 15141
		public static readonly ADPropertyDefinition AuditAdmin = ADRecipientSchema.AuditAdmin;

		// Token: 0x04003B26 RID: 15142
		public static readonly ADPropertyDefinition AuditDelegate = ADRecipientSchema.AuditDelegate;

		// Token: 0x04003B27 RID: 15143
		public static readonly ADPropertyDefinition AuditDelegateAdmin = ADRecipientSchema.AuditDelegateAdmin;

		// Token: 0x04003B28 RID: 15144
		public static readonly ADPropertyDefinition AuditOwner = ADRecipientSchema.AuditOwner;

		// Token: 0x04003B29 RID: 15145
		public static readonly ADPropertyDefinition AuditLogAgeLimit = ADRecipientSchema.AuditLogAgeLimit;

		// Token: 0x04003B2A RID: 15146
		public static readonly ADPropertyDefinition ReconciliationId = ADRecipientSchema.ReconciliationId;

		// Token: 0x04003B2B RID: 15147
		public static readonly ADPropertyDefinition UsageLocation = ADRecipientSchema.UsageLocation;

		// Token: 0x04003B2C RID: 15148
		public static readonly ADPropertyDefinition IsSoftDeletedByRemove = ADRecipientSchema.IsSoftDeletedByRemove;

		// Token: 0x04003B2D RID: 15149
		public static readonly ADPropertyDefinition IsSoftDeletedByDisable = ADRecipientSchema.IsSoftDeletedByDisable;

		// Token: 0x04003B2E RID: 15150
		public static readonly ADPropertyDefinition IsInactiveMailbox = ADRecipientSchema.IsInactiveMailbox;

		// Token: 0x04003B2F RID: 15151
		public static readonly ADPropertyDefinition WhenSoftDeleted = ADRecipientSchema.WhenSoftDeleted;

		// Token: 0x04003B30 RID: 15152
		public static readonly ADPropertyDefinition IncludeInGarbageCollection = ADRecipientSchema.IncludeInGarbageCollection;

		// Token: 0x04003B31 RID: 15153
		public static readonly ADPropertyDefinition QueryBaseDN = ADUserSchema.QueryBaseDN;

		// Token: 0x04003B32 RID: 15154
		public static readonly ADPropertyDefinition InPlaceHolds = ADRecipientSchema.InPlaceHolds;

		// Token: 0x04003B33 RID: 15155
		public static readonly ADPropertyDefinition MailboxProvisioningConstraint = ADRecipientSchema.PersistedMailboxProvisioningConstraint;

		// Token: 0x04003B34 RID: 15156
		public static readonly ADPropertyDefinition MailboxProvisioningPreferences = ADRecipientSchema.MailboxProvisioningPreferences;

		// Token: 0x04003B35 RID: 15157
		public static readonly ADPropertyDefinition UCSImListMigrationCompleted = ADRecipientSchema.UCSImListMigrationCompleted;

		// Token: 0x04003B36 RID: 15158
		public static readonly ADPropertyDefinition GeneratedOfflineAddressBooks = ADRecipientSchema.GeneratedOfflineAddressBooks;

		// Token: 0x04003B37 RID: 15159
		public static readonly ADPropertyDefinition MessageCopyForSentAsEnabled = ADRecipientSchema.MessageCopyForSentAsEnabled;

		// Token: 0x04003B38 RID: 15160
		public static readonly ADPropertyDefinition MessageCopyForSendOnBehalfEnabled = ADRecipientSchema.MessageCopyForSendOnBehalfEnabled;
	}
}
