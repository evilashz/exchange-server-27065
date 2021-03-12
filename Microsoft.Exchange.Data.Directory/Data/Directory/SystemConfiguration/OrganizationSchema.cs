using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002D2 RID: 722
	internal class OrganizationSchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x06002053 RID: 8275 RVA: 0x0008F738 File Offset: 0x0008D938
		internal static ADPropertyDefinition HeuristicsProperty(string name, HeuristicsFlags mask, ADPropertyDefinition fieldProperty)
		{
			GetterDelegate getterDelegate = (IPropertyBag bag) => (bool)OrganizationSchema.HeuristicsFlagsGetter(mask, fieldProperty, bag);
			SetterDelegate setterDelegate = delegate(object value, IPropertyBag bag)
			{
				OrganizationSchema.HeuristicsFlagsSetter(mask, fieldProperty, value, bag);
			};
			return new ADPropertyDefinition(name, fieldProperty.VersionAdded, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ADPropertyDefinition[]
			{
				fieldProperty
			}, null, getterDelegate, setterDelegate, null, null);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0008F7B8 File Offset: 0x0008D9B8
		internal static object HeuristicsFlagsGetter(HeuristicsFlags mask, ADPropertyDefinition fieldProperty, IPropertyBag propertyBag)
		{
			HeuristicsFlags heuristicsFlags = (HeuristicsFlags)propertyBag[fieldProperty];
			return (heuristicsFlags & mask) != HeuristicsFlags.None;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0008F7E0 File Offset: 0x0008D9E0
		internal static void HeuristicsFlagsSetter(HeuristicsFlags mask, ADPropertyDefinition fieldProperty, object value, IPropertyBag propertyBag)
		{
			HeuristicsFlags heuristicsFlags = (HeuristicsFlags)propertyBag[fieldProperty];
			if ((bool)value)
			{
				heuristicsFlags |= mask;
			}
			else
			{
				heuristicsFlags &= ~mask;
			}
			propertyBag[fieldProperty] = heuristicsFlags;
		}

		// Token: 0x040013F1 RID: 5105
		internal const int MaxSupportedMaxConcurrentMigrationsValue = 1000;

		// Token: 0x040013F2 RID: 5106
		internal const int MailTipsLargeAudienceThresholdShift = 0;

		// Token: 0x040013F3 RID: 5107
		internal const int MailTipsLargeAudienceThresholdLength = 10;

		// Token: 0x040013F4 RID: 5108
		internal const int MailTipsExternalRecipientsTipsEnabledShift = 11;

		// Token: 0x040013F5 RID: 5109
		internal const int MailTipsMailboxSourcedTipsEnabledShift = 12;

		// Token: 0x040013F6 RID: 5110
		internal const int MailTipsGroupMetricsEnabledShift = 13;

		// Token: 0x040013F7 RID: 5111
		internal const int MailTipsAllTipsEnabledShift = 14;

		// Token: 0x040013F8 RID: 5112
		internal const int PreferredInternetCodePageForShiftJisShift = 2;

		// Token: 0x040013F9 RID: 5113
		internal const int PreferredInternetCodePageForShiftJisLength = 3;

		// Token: 0x040013FA RID: 5114
		internal const int RequiredCharsetCoverageRawShift = 5;

		// Token: 0x040013FB RID: 5115
		internal const int RequiredCharsetCoverageRawLength = 7;

		// Token: 0x040013FC RID: 5116
		internal const int ByteEncoderTypeFor7BitCharsetsShift = 12;

		// Token: 0x040013FD RID: 5117
		internal const int ByteEncoderTypeFor7BitCharsetsLength = 7;

		// Token: 0x040013FE RID: 5118
		internal const int RequiredCharsetCoverageInitializedShift = 19;

		// Token: 0x040013FF RID: 5119
		internal const int IsFederatedShift = 0;

		// Token: 0x04001400 RID: 5120
		internal const int IsHotmailMigrationShift = 1;

		// Token: 0x04001401 RID: 5121
		internal const int SkipToUAndParentalControlCheckShift = 2;

		// Token: 0x04001402 RID: 5122
		internal const int ReadTrackingEnabledShift = 3;

		// Token: 0x04001403 RID: 5123
		internal const int BuildMinorShift = 6;

		// Token: 0x04001404 RID: 5124
		internal const int BuildMinorLength = 6;

		// Token: 0x04001405 RID: 5125
		internal const int BuildMajorShift = 12;

		// Token: 0x04001406 RID: 5126
		internal const int BuildMajorLength = 11;

		// Token: 0x04001407 RID: 5127
		internal const int IsUpdatingServicePlanShift = 23;

		// Token: 0x04001408 RID: 5128
		internal const int IsUpgradingOrganizationShift = 24;

		// Token: 0x04001409 RID: 5129
		internal const int HideAdminAccessWarningShift = 25;

		// Token: 0x0400140A RID: 5130
		internal const int SMTPAddressCheckWithAcceptedDomainShift = 26;

		// Token: 0x0400140B RID: 5131
		internal const int ActivityBasedAuthenticationTimeoutDisabledShift = 27;

		// Token: 0x0400140C RID: 5132
		internal const int ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabledShift = 28;

		// Token: 0x0400140D RID: 5133
		internal const int MSOSyncEnabledShift = 29;

		// Token: 0x0400140E RID: 5134
		internal const int SyncMBXAndDLToMServShift = 30;

		// Token: 0x0400140F RID: 5135
		internal const int ForwardSyncLiveIdBusinessInstanceShift = 31;

		// Token: 0x04001410 RID: 5136
		internal const int MapiHttpEnabledFlagShift = 10;

		// Token: 0x04001411 RID: 5137
		internal const int IntuneManagedStatusFlagShift = 12;

		// Token: 0x04001412 RID: 5138
		internal const int RmsoSubscriptionStatusFlagsShift = 7;

		// Token: 0x04001413 RID: 5139
		internal const int RmsoSubscriptionStatusFlagsLength = 3;

		// Token: 0x04001414 RID: 5140
		internal const int OfflineAuthProvisioningFlagsShift = 3;

		// Token: 0x04001415 RID: 5141
		internal const int MaxExchangeNotificationRecipientsCount = 64;

		// Token: 0x04001416 RID: 5142
		internal const int MaxIntAsPercent = 100;

		// Token: 0x04001417 RID: 5143
		internal const int HybridConfigurationStatusFlagsShift = 13;

		// Token: 0x04001418 RID: 5144
		internal const int HybridConfigurationStatusFlagsLength = 4;

		// Token: 0x04001419 RID: 5145
		internal const int IsUMGatewayAllowedFlagShift = 17;

		// Token: 0x0400141A RID: 5146
		internal const int OAuth2ClientProfileEnabledFlagShift = 18;

		// Token: 0x0400141B RID: 5147
		internal const int RealTimeLogServiceEnabledFlagShift = 19;

		// Token: 0x0400141C RID: 5148
		internal const int CustomerLockboxEnabledFlagShift = 20;

		// Token: 0x0400141D RID: 5149
		internal const int ACLableSyncedObjectEnabledShift = 21;

		// Token: 0x0400141E RID: 5150
		internal const string MsoDirSyncStatusPendingPrefix = "Pending";

		// Token: 0x0400141F RID: 5151
		internal const int DefaultTimeWindow = 28800;

		// Token: 0x04001420 RID: 5152
		internal const int SoftDeletedFeatureShift = 13;

		// Token: 0x04001421 RID: 5153
		internal const int SoftDeletedFeatureLength = 3;

		// Token: 0x04001422 RID: 5154
		private const int GuidPrefixedLegacyDnShift = 16;

		// Token: 0x04001423 RID: 5155
		private const int IsMailboxForcedReplicationDisabledShift = 17;

		// Token: 0x04001424 RID: 5156
		private const int IsSyncPropertySetUpgradeAllowedShift = 18;

		// Token: 0x04001425 RID: 5157
		internal const int IsProcessEhaMigratedMessagesEnabledShift = 19;

		// Token: 0x04001426 RID: 5158
		internal const int IsPilotingOrganizationShift = 20;

		// Token: 0x04001427 RID: 5159
		internal const int IsTenantAccessBlockedAllowedShift = 22;

		// Token: 0x04001428 RID: 5160
		internal const int IsUpgradeOperationInProgressShift = 23;

		// Token: 0x04001429 RID: 5161
		internal static readonly int OrgConfigurationVersion = 16133;

		// Token: 0x0400142A RID: 5162
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADObject.LegacyDnProperty(ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.DoNotProvisionalClone);

		// Token: 0x0400142B RID: 5163
		public static readonly ADPropertyDefinition Heuristics = new ADPropertyDefinition("Heuristics", ExchangeObjectVersion.Exchange2003, typeof(HeuristicsFlags), "heuristics", ADPropertyDefinitionFlags.None, HeuristicsFlags.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400142C RID: 5164
		public static readonly ADPropertyDefinition BlobMimeTypes = new ADPropertyDefinition("BlobMimeTypes", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchMimeTypes", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400142D RID: 5165
		public static readonly ADPropertyDefinition MimeTypes = new ADPropertyDefinition("MimeTypes", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.BlobMimeTypes
		}, null, new GetterDelegate(Organization.MimeTypesGetter), new SetterDelegate(Organization.MimeTypesSetter), null, null);

		// Token: 0x0400142E RID: 5166
		public static readonly ADPropertyDefinition ResourceAddressLists = new ADPropertyDefinition("ResourceAddressLists", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchResourceAddressLists", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400142F RID: 5167
		public static readonly ADPropertyDefinition IsMixedMode = new ADPropertyDefinition("IsMixedMode", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchMixedMode", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001430 RID: 5168
		[Obsolete("IsPublicFolderContentReplicationDisabled is obsolete.")]
		public static readonly ADPropertyDefinition PublicFolderContentReplicationDisabled = OrganizationSchema.HeuristicsProperty("PublicFolderContentReplicationDisabled", HeuristicsFlags.SuspendFolderReplication, OrganizationSchema.Heuristics);

		// Token: 0x04001431 RID: 5169
		public static readonly ADPropertyDefinition PublicFoldersLockedForMigration = OrganizationSchema.HeuristicsProperty("PublicFoldersLockedForMigration", HeuristicsFlags.PublicFoldersLockedForMigration, OrganizationSchema.Heuristics);

		// Token: 0x04001432 RID: 5170
		public static readonly ADPropertyDefinition PublicFolderMigrationComplete = OrganizationSchema.HeuristicsProperty("PublicFolderMigrationComplete", HeuristicsFlags.PublicFolderMigrationComplete, OrganizationSchema.Heuristics);

		// Token: 0x04001433 RID: 5171
		public static readonly ADPropertyDefinition PublicFolderMailboxesLockedForNewConnections = OrganizationSchema.HeuristicsProperty("PublicFolderMailboxesLockedForNewConnections", HeuristicsFlags.PublicFolderMailboxesLockedForNewConnections, OrganizationSchema.Heuristics);

		// Token: 0x04001434 RID: 5172
		public static readonly ADPropertyDefinition PublicFolderMailboxesMigrationComplete = OrganizationSchema.HeuristicsProperty("PublicFolderMailboxesMigrationComplete", HeuristicsFlags.PublicFolderMailboxesMigrationComplete, OrganizationSchema.Heuristics);

		// Token: 0x04001435 RID: 5173
		public static readonly ADPropertyDefinition PublicFoldersEnabled = new ADPropertyDefinition("PublicFoldersEnabled", ExchangeObjectVersion.Exchange2003, typeof(PublicFoldersDeployment), null, ADPropertyDefinitionFlags.Calculated, PublicFoldersDeployment.Local, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.Heuristics
		}, null, new GetterDelegate(Organization.PublicFoldersEnabledGetter), new SetterDelegate(Organization.PublicFoldersEnabledSetter), null, null);

		// Token: 0x04001436 RID: 5174
		public static readonly ADPropertyDefinition IsAddressListPagingEnabled = new ADPropertyDefinition("IsAddressListPagingEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchAddressListPagingEnabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001437 RID: 5175
		public static readonly ADPropertyDefinition ManagedFolderHomepage = new ADPropertyDefinition("ManagedFolderHomepage", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchELCOrganizationalRootURL", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001438 RID: 5176
		public static readonly ADPropertyDefinition DefaultPublicFolderProhibitPostQuota = LegacyPublicFolderDatabaseSchema.ProhibitPostQuota;

		// Token: 0x04001439 RID: 5177
		public static readonly ADPropertyDefinition DefaultPublicFolderIssueWarningQuota = LegacyDatabaseSchema.IssueWarningQuota;

		// Token: 0x0400143A RID: 5178
		public static readonly ADPropertyDefinition DefaultPublicFolderMaxItemSize = LegacyMailboxDatabaseSchema.ProhibitSendReceiveQuota;

		// Token: 0x0400143B RID: 5179
		public static readonly ADPropertyDefinition DefaultPublicFolderDeletedItemRetention = new ADPropertyDefinition("DefaultPublicFolderDeletedItemRetention", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchPublicFolderDeletedItemRetention", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromDays(1.0), EnhancedTimeSpan.FromDays(24855.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400143C RID: 5180
		public static readonly ADPropertyDefinition DefaultPublicFolderMovedItemRetention = new ADPropertyDefinition("DefaultPublicFolderMovedItemRetention", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchPublicFolderMovedItemRetention", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(7.0), new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromDays(1.0), EnhancedTimeSpan.FromDays(24855.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400143D RID: 5181
		public static readonly ADPropertyDefinition DefaultPublicFolderAgeLimit = new ADPropertyDefinition("DefaultPublicFolderAgeLimit", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan?), "msExchOverallAgeLimit", ADPropertyDefinitionFlags.None, null, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromDays(1.0), EnhancedTimeSpan.FromDays(24855.0)),
			new NullableEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400143E RID: 5182
		public static readonly ADPropertyDefinition ServiceEndpoints = new ADPropertyDefinition("ServiceEndpoints", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchServiceEndPointURL", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400143F RID: 5183
		public static readonly ADPropertyDefinition SiteMailboxCreationURL = new ADPropertyDefinition("SiteMailboxCreationURL", ExchangeObjectVersion.Exchange2003, typeof(Uri), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ServiceEndpoints
		}, null, new GetterDelegate(Organization.SiteMailboxCreationURLGetter), new SetterDelegate(Organization.SiteMailboxCreationURLSetter), null, null);

		// Token: 0x04001440 RID: 5184
		public static readonly ADPropertyDefinition ForeignForestFQDNRaw = new ADPropertyDefinition("ForeignForestFQDNRaw", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchForeignForestFQDN", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001441 RID: 5185
		public static readonly ADPropertyDefinition ForeignForestFQDN = new ADPropertyDefinition("ForeignForestFQDN", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ForeignForestFQDNRaw
		}, null, new GetterDelegate(Organization.ForeignForestFQDNGetter), new SetterDelegate(Organization.ForeignForestFQDNSetter), null, null);

		// Token: 0x04001442 RID: 5186
		public static readonly ADPropertyDefinition ForeignForestOrgAdminUSGSid = new ADPropertyDefinition("ForeignForestOrgAdminUSGSid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "msExchForeignForestOrgAdminUSGSid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001443 RID: 5187
		public static readonly ADPropertyDefinition ForeignForestViewOnlyAdminUSGSid = new ADPropertyDefinition("ForeignForestViewOnlyAdminUSGSid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "msExchForeignForestReadOnlyAdminUSGSid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001444 RID: 5188
		public static readonly ADPropertyDefinition ForeignForestRecipientAdminUSGSid = new ADPropertyDefinition("ForeignForestRecipientAdminUSGSid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "msExchForeignForestRecipientAdminUSGSid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001445 RID: 5189
		[Obsolete("ForeignForestViewOnlyAdminUSGSid is obsolete.")]
		public static readonly ADPropertyDefinition ForeignForestPublicFolderAdminUSGSid = new ADPropertyDefinition("ForeignForestPublicFolderAdminUSGSid", ExchangeObjectVersion.Exchange2003, typeof(SecurityIdentifier), "msExchForeignForestPublicFolderAdminUSGSid", ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001446 RID: 5190
		public static readonly ADPropertyDefinition ObjectVersion = new ADPropertyDefinition("ObjectVersion", ExchangeObjectVersion.Exchange2003, typeof(int), "objectVersion", ADPropertyDefinitionFlags.PersistDefaultValue, OrganizationSchema.OrgConfigurationVersion, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001447 RID: 5191
		public static readonly ADPropertyDefinition BuildNumber = new ADPropertyDefinition("msExchProductID", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchProductID", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001448 RID: 5192
		public static readonly ADPropertyDefinition SCLJunkThreshold = new ADPropertyDefinition("SCLJunkThreshold", ExchangeObjectVersion.Exchange2003, typeof(int), null, ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.TaskPopulated, 4, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001449 RID: 5193
		public static readonly ADPropertyDefinition AcceptedDomainNames = new ADPropertyDefinition("AcceptedDomainNames", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1123)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400144A RID: 5194
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientEmailAddresses = new ADPropertyDefinition("MicrosoftExchangeRecipientEmailAddresses", ExchangeObjectVersion.Exchange2003, typeof(ProxyAddress), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1123)
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400144B RID: 5195
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientReplyRecipient = new ADPropertyDefinition("MicrosoftExchangeRecipientReplyRecipient", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400144C RID: 5196
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientPrimarySmtpAddress = new ADPropertyDefinition("MicrosoftExchangeRecipientPrimarySmtpAddress", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), null, ADPropertyDefinitionFlags.TaskPopulated, SmtpAddress.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400144D RID: 5197
		public static readonly ADPropertyDefinition MicrosoftExchangeRecipientEmailAddressPolicyEnabled = new ADPropertyDefinition("MicrosoftExchangeRecipientEmailAddressPolicyEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400144E RID: 5198
		public static readonly ADPropertyDefinition Industry = new ADPropertyDefinition("Industry", ExchangeObjectVersion.Exchange2003, typeof(IndustryType), "msExchIndustry", ADPropertyDefinitionFlags.PersistDefaultValue, IndustryType.NotSpecified, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400144F RID: 5199
		public static readonly ADPropertyDefinition CustomerFeedbackEnabled = new ADPropertyDefinition("CustomerFeedbackEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool?), "msExchCustomerFeedbackEnabled", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001450 RID: 5200
		public static readonly ADPropertyDefinition OrganizationSummary = new ADPropertyDefinition("OrganizationSummary", ExchangeObjectVersion.Exchange2003, typeof(OrganizationSummaryEntry), "msExchOrganizationSummary", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001451 RID: 5201
		public static readonly ADPropertyDefinition MailTipsSettings = new ADPropertyDefinition("MailTipsSettings", ExchangeObjectVersion.Exchange2003, typeof(long), "msExchMailTipsSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001452 RID: 5202
		public static readonly ADPropertyDefinition MailTipsLargeAudienceThreshold = ADObject.BitfieldProperty("MailTipsLargeAudienceThreshold", 0, 10, OrganizationSchema.MailTipsSettings, new RangedValueConstraint<long>(0L, 1000L));

		// Token: 0x04001453 RID: 5203
		public static readonly ADPropertyDefinition MailTipsExternalRecipientsTipsEnabled = ADObject.BitfieldProperty("MailTipsExternalRecipientsTipsEnabled", 11, OrganizationSchema.MailTipsSettings);

		// Token: 0x04001454 RID: 5204
		public static readonly ADPropertyDefinition MailTipsMailboxSourcedTipsEnabled = ADObject.BitfieldProperty("MailTipsMailboxSourcedTipsEnabled", 12, OrganizationSchema.MailTipsSettings);

		// Token: 0x04001455 RID: 5205
		public static readonly ADPropertyDefinition MailTipsGroupMetricsEnabled = ADObject.BitfieldProperty("MailTipsGroupMetricsEnabled", 13, OrganizationSchema.MailTipsSettings);

		// Token: 0x04001456 RID: 5206
		public static readonly ADPropertyDefinition MailTipsAllTipsEnabled = ADObject.BitfieldProperty("MailTipsAllTipsEnabled", 14, OrganizationSchema.MailTipsSettings);

		// Token: 0x04001457 RID: 5207
		internal static readonly ADPropertyDefinition ContentConversionFlags = new ADPropertyDefinition("msExchContentConversionSettings", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchContentConversionSettings", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001458 RID: 5208
		public static readonly ADPropertyDefinition PreferredInternetCodePageForShiftJis = ADObject.BitfieldProperty("PreferredInternetCodePageForShiftJis", 2, 3, OrganizationSchema.ContentConversionFlags);

		// Token: 0x04001459 RID: 5209
		public static readonly ADPropertyDefinition ByteEncoderTypeFor7BitCharsets = ADObject.BitfieldProperty("ByteEncoderTypeFor7BitCharsets", 12, 7, OrganizationSchema.ContentConversionFlags);

		// Token: 0x0400145A RID: 5210
		public static readonly ADPropertyDefinition RequiredCharsetCoverage = new ADPropertyDefinition("RequiredCharsetCoverage", ExchangeObjectVersion.Exchange2003, typeof(int), null, ADPropertyDefinitionFlags.Calculated, 100, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 100)
		}, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ContentConversionFlags
		}, null, new GetterDelegate(Organization.RequiredCharsetCoverageGetter), new SetterDelegate(Organization.RequiredCharsetCoverageSetter), null, null);

		// Token: 0x0400145B RID: 5211
		internal static readonly ADPropertyDefinition OrganizationFlags = new ADPropertyDefinition("OrganizationFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchOrganizationFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400145C RID: 5212
		internal static readonly ADPropertyDefinition OrganizationFlags2 = new ADPropertyDefinition("OrganizationFlags2", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchOrganizationFlags2", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400145D RID: 5213
		public static readonly ADPropertyDefinition IsFederated = ADObject.BitfieldProperty("IsFederated", 0, OrganizationSchema.OrganizationFlags);

		// Token: 0x0400145E RID: 5214
		public static readonly ADPropertyDefinition ForwardSyncLiveIdBusinessInstance = ADObject.BitfieldProperty("ForwardSyncLiveIdBusinessInstance", 31, OrganizationSchema.OrganizationFlags);

		// Token: 0x0400145F RID: 5215
		public static readonly ADPropertyDefinition IsHotmailMigration = ADObject.BitfieldProperty("IsHotmailMigration", 1, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001460 RID: 5216
		public static readonly ADPropertyDefinition SkipToUAndParentalControlCheck = ADObject.BitfieldProperty("SkipToUAndParentalControlCheck", 2, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001461 RID: 5217
		public static readonly ADPropertyDefinition HideAdminAccessWarning = ADObject.BitfieldProperty("HideAdminAccessWarning", 25, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001462 RID: 5218
		public static readonly ADPropertyDefinition SMTPAddressCheckWithAcceptedDomain = ADObject.BitfieldProperty("SMTPAddressCheckWithAcceptedDomain", 26, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001463 RID: 5219
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutInterval = new ADPropertyDefinition("ActivityBasedAuthenticationTimeoutInterval", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchActivityBasedAuthenticationTimeoutInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromHours(6.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromMinutes(5.0), EnhancedTimeSpan.FromHours(8.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.FromMinutes(5.0), EnhancedTimeSpan.FromSeconds(28800.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, null, null);

		// Token: 0x04001464 RID: 5220
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutDisabled = ADObject.BitfieldProperty("ActivityBasedAuthenticationTimeoutDisabled", 27, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001465 RID: 5221
		public static readonly ADPropertyDefinition ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled = ADObject.BitfieldProperty("ActivityBasedAuthenticationTimeoutWithSingleSignOnDisabled", 28, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001466 RID: 5222
		public static readonly ADPropertyDefinition MSOSyncEnabled = ADObject.BitfieldProperty("MSOSyncEnabled", 29, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001467 RID: 5223
		public static readonly ADPropertyDefinition MaxAddressBookPolicies = new ADPropertyDefinition("MaxAddressBookPolicies", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchMaxABP", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04001468 RID: 5224
		public static readonly ADPropertyDefinition MaxOfflineAddressBooks = new ADPropertyDefinition("MaxOfflineAddressBooks", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchMaxOAB", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, int.MaxValue)
		}, null, null);

		// Token: 0x04001469 RID: 5225
		public static readonly ADPropertyDefinition SyncMBXAndDLToMServ = ADObject.BitfieldProperty("SyncMBXAndDLToMServ", 30, OrganizationSchema.OrganizationFlags);

		// Token: 0x0400146A RID: 5226
		public static readonly ADPropertyDefinition ShowAdminAccessWarning = new ADPropertyDefinition("ShowAdminAccessWarning", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags
		}, null, new GetterDelegate(Organization.ShowAdminAccessWarningGetter), null, null, null);

		// Token: 0x0400146B RID: 5227
		public static readonly ADPropertyDefinition OrgElcMailboxFlags = new ADPropertyDefinition("ElcMailboxFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchELCMailboxFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400146C RID: 5228
		public static readonly ADPropertyDefinition CalendarVersionStoreEnabled = new ADPropertyDefinition("CalendarVersionStoreEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrgElcMailboxFlags
		}, null, ADObject.FlagGetterDelegate(4, OrganizationSchema.OrgElcMailboxFlags), ADObject.FlagSetterDelegate(4, OrganizationSchema.OrgElcMailboxFlags), null, null);

		// Token: 0x0400146D RID: 5229
		public static readonly ADPropertyDefinition ReadTrackingEnabled = ADObject.BitfieldProperty("ReadTrackingEnabled", 3, OrganizationSchema.OrganizationFlags);

		// Token: 0x0400146E RID: 5230
		internal static readonly ADPropertyDefinition SIPAccessService = new ADPropertyDefinition("SIPAccessService", ExchangeObjectVersion.Exchange2003, typeof(ProtocolConnectionSettings), "msExchSIPAccessService", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400146F RID: 5231
		internal static readonly ADPropertyDefinition AVAuthenticationService = new ADPropertyDefinition("AVAuthenticationService", ExchangeObjectVersion.Exchange2003, typeof(ProtocolConnectionSettings), "msExchAVAuthenticationService", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001470 RID: 5232
		internal static readonly ADPropertyDefinition SIPSessionBorderController = new ADPropertyDefinition("SIPSBCService", ExchangeObjectVersion.Exchange2003, typeof(ProtocolConnectionSettings), "msExchSIPSBCService", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001471 RID: 5233
		public static readonly ADPropertyDefinition BuildMajor = ADObject.BitfieldProperty("BuildMajor", 12, 11, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001472 RID: 5234
		public static readonly ADPropertyDefinition BuildMinor = ADObject.BitfieldProperty("BuildMinor", 6, 6, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001473 RID: 5235
		public static readonly ADPropertyDefinition IsUpgradingOrganization = ADObject.BitfieldProperty("IsUpgradingOrganization", 24, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001474 RID: 5236
		public static readonly ADPropertyDefinition IsUpdatingServicePlan = ADObject.BitfieldProperty("IsUpdatingServicePlan", 23, OrganizationSchema.OrganizationFlags);

		// Token: 0x04001475 RID: 5237
		public static readonly ADPropertyDefinition IsDehydrated = new ADPropertyDefinition("IsDehydrated", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 128), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 128), null, null);

		// Token: 0x04001476 RID: 5238
		public static readonly ADPropertyDefinition IsStaticConfigurationShared = new ADPropertyDefinition("IsStaticConfigurationShared", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 256), ADObject.FlagSetterDelegate(SharedPropertyDefinitions.ProvisioningFlags, 256), null, null);

		// Token: 0x04001477 RID: 5239
		public static readonly ADPropertyDefinition HABRootDepartmentLink = new ADPropertyDefinition("HABRootDepartmentLink", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchHABRootDepartmentLink", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04001478 RID: 5240
		public static readonly ADPropertyDefinition DistributionGroupDefaultOU = new ADPropertyDefinition("DistributionGroupDefaultOU", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchDistributionGroupDefaultOU", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001479 RID: 5241
		public static readonly ADPropertyDefinition DistributionGroupNameBlockedWordsList = new ADPropertyDefinition("DistributionGroupNameBlockedWordsList", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDistributionGroupNameBlockedWordsList", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400147A RID: 5242
		public static readonly ADPropertyDefinition DistributionGroupNamingPolicyRaw = new ADPropertyDefinition("DistributionGroupNamingPolicy", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDistributionGroupNamingPolicy", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400147B RID: 5243
		public static readonly ADPropertyDefinition DistributionGroupNamingPolicy = new ADPropertyDefinition("DistributionGroupNamingPolicy", ExchangeObjectVersion.Exchange2003, typeof(DistributionGroupNamingPolicy), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.DistributionGroupNamingPolicyRaw
		}, null, new GetterDelegate(Organization.DistributionGroupNamingPolicyGetter), new SetterDelegate(Organization.DistributionGroupNamingPolicySetter), null, null);

		// Token: 0x0400147C RID: 5244
		public static readonly ADPropertyDefinition ExchangeNotificationEnabled = new ADPropertyDefinition("ExchangeNotificationEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchNotificationEnabled", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400147D RID: 5245
		public static readonly ADPropertyDefinition ExchangeNotificationRecipients = new ADPropertyDefinition("ExchangeNotificationRecipients", ExchangeObjectVersion.Exchange2003, typeof(SmtpAddress), "msExchNotificationAddress", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new CollectionPropertyMaxCountConstraint(64)
		}, null, null);

		// Token: 0x0400147E RID: 5246
		internal static readonly ADPropertyDefinition SupportedSharedConfigurations = new ADPropertyDefinition("SupportedSharedConfigurations", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchSupportedSharedConfigLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400147F RID: 5247
		internal static readonly ADPropertyDefinition SupportedSharedConfigurationsBL = new ADPropertyDefinition("SupportedSharedConfigurationsBL", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchSupportedSharedConfigBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001480 RID: 5248
		internal static readonly ADPropertyDefinition SharedConfigurationInfo = new ADPropertyDefinition("SharedConfigurationInfo", ExchangeObjectVersion.Exchange2003, typeof(SharedConfigurationInfo), "msExchSharedConfigServicePlanTag", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001481 RID: 5249
		public static readonly ADPropertyDefinition MigrationFlags = new ADPropertyDefinition("MigrationFlags", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchMigrationFlags", ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.DoNotProvisionalClone, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001482 RID: 5250
		public static readonly ADPropertyDefinition IsExcludedFromOnboardMigration = new ADPropertyDefinition("IsExcludedFromOnboardMigration", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.MigrationFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.MigrationFlags, 1UL)), ADObject.FlagGetterDelegate(OrganizationSchema.MigrationFlags, 1), ADObject.FlagSetterDelegate(OrganizationSchema.MigrationFlags, 1), null, null);

		// Token: 0x04001483 RID: 5251
		public static readonly ADPropertyDefinition IsExcludedFromOffboardMigration = new ADPropertyDefinition("IsExcludedFromOffboardMigration", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.MigrationFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.MigrationFlags, 2UL)), ADObject.FlagGetterDelegate(OrganizationSchema.MigrationFlags, 2), ADObject.FlagSetterDelegate(OrganizationSchema.MigrationFlags, 2), null, null);

		// Token: 0x04001484 RID: 5252
		public static readonly ADPropertyDefinition IsFfoMigrationInProgress = new ADPropertyDefinition("IsFfoMigrationInProgress", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.MigrationFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.MigrationFlags, 4UL)), ADObject.FlagGetterDelegate(OrganizationSchema.MigrationFlags, 4), ADObject.FlagSetterDelegate(OrganizationSchema.MigrationFlags, 4), null, null);

		// Token: 0x04001485 RID: 5253
		public static readonly ADPropertyDefinition MaxConcurrentMigrations = new ADPropertyDefinition("MaxConcurrentMigrations", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<int>), "msExchMaxConcurrentMigrations", ADPropertyDefinitionFlags.None, Unlimited<int>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, 1000)
		}, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<int>(0, 1000)
		}, null, null);

		// Token: 0x04001486 RID: 5254
		public static readonly ADPropertyDefinition TenantRelocationsAllowed = new ADPropertyDefinition("TenantRelocationsAllowed", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 1UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 1), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 1), null, null);

		// Token: 0x04001487 RID: 5255
		public static readonly ADPropertyDefinition ACLableSyncedObjectEnabled = new ADPropertyDefinition("ACLableSyncedObjectEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 2097152UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 2097152), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 2097152), null, null);

		// Token: 0x04001488 RID: 5256
		public static readonly ADPropertyDefinition OpenTenantFull = new ADPropertyDefinition("OpenTenantFull", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 64UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 64), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 64), null, null);

		// Token: 0x04001489 RID: 5257
		public static readonly ADPropertyDefinition ProvisioningFlags = SharedPropertyDefinitions.ProvisioningFlags;

		// Token: 0x0400148A RID: 5258
		public static readonly ADPropertyDefinition EnableAsSharedConfiguration = new ADPropertyDefinition("EnableAsSharedConfiguration", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Organization.EnableAsSharedConfigurationFilterBuilder), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 1), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 1), null, null);

		// Token: 0x0400148B RID: 5259
		public static readonly ADPropertyDefinition ImmutableConfiguration = new ADPropertyDefinition("ImmutableConfiguration", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Organization.ImmutableConfigurationFilterBuilder), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 1024), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 1024), null, null);

		// Token: 0x0400148C RID: 5260
		public static readonly ADPropertyDefinition HostingDeploymentEnabled = new ADPropertyDefinition("MultiTenantADEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Organization.HostingDeploymentEnabledFilterBuilder), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 32), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 32), null, null);

		// Token: 0x0400148D RID: 5261
		public static readonly ADPropertyDefinition IsSharingConfiguration = new ADPropertyDefinition("IsSharingConfiguration", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400148E RID: 5262
		public static readonly ADPropertyDefinition IsLicensingEnforced = new ADPropertyDefinition("IsLicensingEnforced", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Organization.LicensingEnforcedFilterBuilder), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 2), null, null);

		// Token: 0x0400148F RID: 5263
		public static readonly ADPropertyDefinition IsTenantAccessBlocked = new ADPropertyDefinition("IsTenantAccessBlocked", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(Organization.IsTenantAccessBlockedFilterBuilder), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 4194304), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 4194304), null, null);

		// Token: 0x04001490 RID: 5264
		public static readonly ADPropertyDefinition IsUpgradeOperationInProgress = new ADPropertyDefinition("IsUpgradeOperationInProgress", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 2UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 2), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 2), null, null);

		// Token: 0x04001491 RID: 5265
		public static readonly ADPropertyDefinition UseServicePlanAsCounterInstanceName = new ADPropertyDefinition("UseServicePlanAsCounterInstanceName", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 64UL)), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 64), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 64), null, null);

		// Token: 0x04001492 RID: 5266
		public static readonly ADPropertyDefinition IsTemplateTenant = new ADPropertyDefinition("IsTemplateTenant", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 2048UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 2048), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 2048), null, null);

		// Token: 0x04001493 RID: 5267
		public static readonly ADPropertyDefinition ExcludedFromBackSync = new ADPropertyDefinition("ExcludedFromBackSync", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 4UL)), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 4), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 4), null, null);

		// Token: 0x04001494 RID: 5268
		public static readonly ADPropertyDefinition ExcludedFromForwardSyncEDU2BPOS = new ADPropertyDefinition("ExcludedFromForwardSyncEDU2BPOS", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 2048UL)), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 2048), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 2048), null, null);

		// Token: 0x04001495 RID: 5269
		public static readonly ADPropertyDefinition AllowDeleteOfExternalIdentityUponRemove = new ADPropertyDefinition("AllowDeleteOfExternalIdentityUponRemove", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 16UL)), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 16), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 16), null, null);

		// Token: 0x04001496 RID: 5270
		public static readonly ADPropertyDefinition AppsForOfficeDisabled = new ADPropertyDefinition("OwaExtensibilityDisabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.ProvisioningFlags, 512UL)), ADObject.FlagGetterDelegate(OrganizationSchema.ProvisioningFlags, 512), ADObject.FlagSetterDelegate(OrganizationSchema.ProvisioningFlags, 512), null, null);

		// Token: 0x04001497 RID: 5271
		public static readonly ADPropertyDefinition SoftDeletedFeatureStatus = ADObject.BitfieldProperty("SoftDeletedFeatureStatus", 13, 3, OrganizationSchema.ProvisioningFlags);

		// Token: 0x04001498 RID: 5272
		public static readonly ADPropertyDefinition IsGuidPrefixedLegacyDnDisabled = ADObject.BitfieldProperty("IsGuidPrefixedLegacyDnDisabled", 16, OrganizationSchema.ProvisioningFlags);

		// Token: 0x04001499 RID: 5273
		public static readonly ADPropertyDefinition IsMailboxForcedReplicationDisabled = ADObject.BitfieldProperty("IsMailboxForcedReplicationDisabled", 17, OrganizationSchema.ProvisioningFlags);

		// Token: 0x0400149A RID: 5274
		public static readonly ADPropertyDefinition IsPilotingOrganization = ADObject.BitfieldProperty("IsPilotingOrganization", 20, OrganizationSchema.ProvisioningFlags);

		// Token: 0x0400149B RID: 5275
		public static readonly ADPropertyDefinition IsSyncPropertySetUpgradeAllowed = ADObject.BitfieldProperty("IsSyncPropertySetUpgradeAllowed", 18, OrganizationSchema.ProvisioningFlags);

		// Token: 0x0400149C RID: 5276
		public static readonly ADPropertyDefinition IsProcessEhaMigratedMessagesEnabled = ADObject.BitfieldProperty("IsProcessEhaMigratedMessagesEnabled", 19, OrganizationSchema.ProvisioningFlags);

		// Token: 0x0400149D RID: 5277
		public static readonly ADPropertyDefinition IsDirSyncRunning = new ADPropertyDefinition("IsDirSyncRunning", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchIsMSODirsyncEnabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400149E RID: 5278
		public static readonly ADPropertyDefinition DirSyncStatus = new ADPropertyDefinition("DirSyncStatus", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchDirsyncStatus", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x0400149F RID: 5279
		public static readonly ADPropertyDefinition CompanyTags = new ADPropertyDefinition("CompanyTags", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchProvisioningTags", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040014A0 RID: 5280
		public static readonly ADPropertyDefinition Location = new ADPropertyDefinition("Location", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchTenantCountry", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014A1 RID: 5281
		public static readonly ADPropertyDefinition PersistedCapabilities = new ADPropertyDefinition("PersistedCapabilities", ExchangeObjectVersion.Exchange2003, typeof(Capability), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new CollectionDelegateConstraint(new CollectionValidationDelegate(ConstraintDelegates.ValidateOrganizationCapabilities))
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SharedPropertyDefinitions.RawCapabilities
		}, new CustomFilterBuilderDelegate(SharedPropertyDefinitions.CapabilitiesFilterBuilder), new GetterDelegate(SharedPropertyDefinitions.PersistedCapabilitiesGetter), new SetterDelegate(SharedPropertyDefinitions.PersistedCapabilitiesSetter), null, null);

		// Token: 0x040014A2 RID: 5282
		public static readonly ADPropertyDefinition IsDirSyncStatusPending = new ADPropertyDefinition("IsDirSyncStatusPending", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchIsDirsyncStatusPending", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014A3 RID: 5283
		public static readonly ADPropertyDefinition EwsEnabled = new ADPropertyDefinition("EwsEnabled", ExchangeObjectVersion.Exchange2003, typeof(int?), "msExchEwsEnabled", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040014A4 RID: 5284
		public static readonly ADPropertyDefinition EwsWellKnownApplicationAccessPolicies = new ADPropertyDefinition("EwsWellKnownApplicationAccessPolicies", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchEwsWellKnownApplicationPolicies", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040014A5 RID: 5285
		public static readonly ADPropertyDefinition EwsApplicationAccessPolicy = new ADPropertyDefinition("EwsApplicationAccessPolicy", ExchangeObjectVersion.Exchange2003, typeof(EwsApplicationAccessPolicy?), "msExchEwsApplicationAccessPolicy", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(EwsApplicationAccessPolicy))
		}, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040014A6 RID: 5286
		public static readonly ADPropertyDefinition EwsExceptions = new ADPropertyDefinition("EwsExceptions", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchEwsExceptions", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014A7 RID: 5287
		public static readonly ADPropertyDefinition EwsAllowOutlook = new ADPropertyDefinition("EwsAllowOutlook", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsOutlookAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsOutlookAccessPoliciesSetterDelegate(), null, null);

		// Token: 0x040014A8 RID: 5288
		public static readonly ADPropertyDefinition EwsAllowMacOutlook = new ADPropertyDefinition("EwsAllowMacOutlook", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsMacOutlookAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsMacOutlookAccessPoliciesSetterDelegate(), null, null);

		// Token: 0x040014A9 RID: 5289
		public static readonly ADPropertyDefinition EwsAllowEntourage = new ADPropertyDefinition("EwsAllowEntourage", ExchangeObjectVersion.Exchange2003, typeof(bool?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.EwsWellKnownApplicationAccessPolicies
		}, null, CASMailboxHelper.EwsEntourageAccessPoliciesGetterDelegate(), CASMailboxHelper.EwsEntourageAccessPoliciesSetterDelegate(), null, null);

		// Token: 0x040014AA RID: 5290
		public static readonly ADPropertyDefinition AsynchronousOperationIds = new ADPropertyDefinition("AsynchronousOperationIds", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchMSOForwardSyncAsyncOperationIds", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014AB RID: 5291
		public static readonly ADPropertyDefinition RBACConfigurationVersion = new ADPropertyDefinition("RBACConfigurationVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), null, ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014AC RID: 5292
		public static readonly ADPropertyDefinition AdminDisplayVersion = new ADPropertyDefinition("AdminDisplayVersion", ExchangeObjectVersion.Exchange2003, typeof(ExchangeObjectVersion), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags,
			OrganizationSchema.ObjectVersion
		}, null, new GetterDelegate(Organization.AdminDisplayVersionGetter), null, null, null);

		// Token: 0x040014AD RID: 5293
		public static readonly ADPropertyDefinition DefaultPublicFolderMailbox = new ADPropertyDefinition("DefaultPublicFolderMailbox", ExchangeObjectVersion.Exchange2003, typeof(PublicFolderInformation), "msExchDefaultPublicFolderMailbox", ADPropertyDefinitionFlags.None, PublicFolderInformation.InvalidPublicFolderInformation, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014AE RID: 5294
		public static readonly ADPropertyDefinition RemotePublicFolderMailboxes = new ADPropertyDefinition("RemotePublicFolderMailboxes", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "pFContacts", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.DoNotValidate, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014AF RID: 5295
		public static readonly ADPropertyDefinition ForestMode = new ADPropertyDefinition("ForestMode", ExchangeObjectVersion.Exchange2003, typeof(ForestModeFlags), "msExchForestModeFlag", ADPropertyDefinitionFlags.None, ForestModeFlags.Legacy, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ForestModeFlags))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014B0 RID: 5296
		public static readonly ADPropertyDefinition UMAvailableLanguagesRaw = new ADPropertyDefinition("UMAvailableLanguagesRaw", ExchangeObjectVersion.Exchange2003, typeof(int), "msExchUMAvailableLanguages", ADPropertyDefinitionFlags.MultiValued, null, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 1048576)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014B1 RID: 5297
		public static readonly ADPropertyDefinition UMAvailableLanguages = new ADPropertyDefinition("UMAvailableLanguages", ExchangeObjectVersion.Exchange2003, typeof(UMLanguage), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.UMAvailableLanguagesRaw
		}, null, new GetterDelegate(Organization.UMAvailableLanguagesGetter), new SetterDelegate(Organization.UMAvailableLanguagesSetter), null, null);

		// Token: 0x040014B2 RID: 5298
		public static readonly ADPropertyDefinition AdfsAuthenticationRawConfiguration = new ADPropertyDefinition("AdfsAuthenticationRawConfiguration", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchAdfsAuthenticationRawConfiguration", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 10240)
		}, null, null);

		// Token: 0x040014B3 RID: 5299
		public static readonly ADPropertyDefinition AdfsIssuer = new ADPropertyDefinition("AdfsIssuer", ExchangeObjectVersion.Exchange2003, typeof(Uri), null, ADPropertyDefinitionFlags.Calculated, null, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new PropertyDefinitionConstraint[]
		{
			new UriKindConstraint(UriKind.Absolute)
		}, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.AdfsAuthenticationRawConfiguration
		}, null, new GetterDelegate(Organization.AdfsIssuerGetter), new SetterDelegate(Organization.AdfsIssuerSetter), null, null);

		// Token: 0x040014B4 RID: 5300
		public static readonly ADPropertyDefinition AdfsAudienceUris = new ADPropertyDefinition("AdfsAudienceUris", ExchangeObjectVersion.Exchange2003, typeof(Uri), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.AdfsAuthenticationRawConfiguration
		}, null, new GetterDelegate(Organization.AdfsAudienceUrisGetter), new SetterDelegate(Organization.AdfsAudienceUrisSetter), null, null);

		// Token: 0x040014B5 RID: 5301
		public static readonly ADPropertyDefinition AdfsSignCertificateThumbprints = new ADPropertyDefinition("AdfsSignCertificateThumbprints", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.AdfsAuthenticationRawConfiguration
		}, null, new GetterDelegate(Organization.AdfsSignCertificateThumbprintsGetter), new SetterDelegate(Organization.AdfsSignCertificateThumbprintsSetter), null, null);

		// Token: 0x040014B6 RID: 5302
		public static readonly ADPropertyDefinition AdfsEncryptCertificateThumbprint = new ADPropertyDefinition("AdfsEncryptCertificateThumbprint", ExchangeObjectVersion.Exchange2003, typeof(string), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.AdfsAuthenticationRawConfiguration
		}, null, new GetterDelegate(Organization.AdfsEncryptCertificateThumbprintGetter), new SetterDelegate(Organization.AdfsEncryptCertificateThumbprintSetter), null, null);

		// Token: 0x040014B7 RID: 5303
		public static readonly ADPropertyDefinition ConfigurationXMLRaw = XMLSerializableBase.ConfigurationXmlRawProperty();

		// Token: 0x040014B8 RID: 5304
		public static readonly ADPropertyDefinition ConfigurationXML = XMLSerializableBase.ConfigurationXmlProperty<OrganizationConfigXML>(OrganizationSchema.ConfigurationXMLRaw);

		// Token: 0x040014B9 RID: 5305
		public static readonly ADPropertyDefinition UpgradeStatus = new ADPropertyDefinition("UpgradeStatus", ExchangeObjectVersion.Exchange2003, typeof(UpgradeStatusTypes), "msExchOrganizationUpgradeStatus", ADPropertyDefinitionFlags.None, UpgradeStatusTypes.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014BA RID: 5306
		public static readonly ADPropertyDefinition UpgradeRequest = new ADPropertyDefinition("UpgradeRequest", ExchangeObjectVersion.Exchange2003, typeof(UpgradeRequestTypes), "msExchOrganizationUpgradeRequest", ADPropertyDefinitionFlags.None, UpgradeRequestTypes.None, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014BB RID: 5307
		public static readonly ADPropertyDefinition WACDiscoveryEndpoint = new ADPropertyDefinition("WACDiscoveryEndpoint", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchWACDiscoveryEndpoint", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040014BC RID: 5308
		public static readonly ADPropertyDefinition DefaultMovePriority = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, int>("DefaultMovePriority", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, 0, (OrganizationConfigXML configXml) => configXml.DefaultMovePriority, delegate(OrganizationConfigXML configXml, int value)
		{
			configXml.DefaultMovePriority = value;
		}, null, null);

		// Token: 0x040014BD RID: 5309
		public static readonly ADPropertyDefinition UpgradeMessage = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, string>("UpgradeMessage", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeMessage, delegate(OrganizationConfigXML configXml, string value)
		{
			configXml.UpgradeMessage = value;
		}, null, null);

		// Token: 0x040014BE RID: 5310
		public static readonly ADPropertyDefinition UpgradeDetails = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, string>("UpgradeDetails", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeDetails, delegate(OrganizationConfigXML configXml, string value)
		{
			configXml.UpgradeDetails = value;
		}, null, null);

		// Token: 0x040014BF RID: 5311
		public static readonly ADPropertyDefinition UpgradeConstraints = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, UpgradeConstraintArray>("UpgradeConstraints", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeConstraints, delegate(OrganizationConfigXML configXml, UpgradeConstraintArray value)
		{
			configXml.UpgradeConstraints = value;
		}, null, null);

		// Token: 0x040014C0 RID: 5312
		public static readonly ADPropertyDefinition UpgradeStage = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, UpgradeStage?>("UpgradeStage", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeStage, delegate(OrganizationConfigXML configXml, UpgradeStage? value)
		{
			configXml.UpgradeStage = value;
		}, null, null);

		// Token: 0x040014C1 RID: 5313
		public static readonly ADPropertyDefinition UpgradeStageTimeStamp = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, DateTime?>("UpgradeStageTimeStamp", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeStageTimeStamp, delegate(OrganizationConfigXML configXml, DateTime? value)
		{
			configXml.UpgradeStageTimeStamp = value;
		}, null, null);

		// Token: 0x040014C2 RID: 5314
		public static readonly ADPropertyDefinition UpgradeE14RequestCountForCurrentStage = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, int?>("UpgradeE14RequestCountForCurrentStage", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeE14RequestCountForCurrentStage, delegate(OrganizationConfigXML configXml, int? value)
		{
			configXml.UpgradeE14RequestCountForCurrentStage = value;
		}, null, null);

		// Token: 0x040014C3 RID: 5315
		public static readonly ADPropertyDefinition UpgradeE14MbxCountForCurrentStage = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, int?>("UpgradeE14MbxCountForCurrentStage", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeE14MbxCountForCurrentStage, delegate(OrganizationConfigXML configXml, int? value)
		{
			configXml.UpgradeE14MbxCountForCurrentStage = value;
		}, null, null);

		// Token: 0x040014C4 RID: 5316
		public static readonly ADPropertyDefinition UpgradeUnitsOverride = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, int?>("UpgradeUnitsOverride", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeUnitsOverride, delegate(OrganizationConfigXML configXml, int? value)
		{
			configXml.UpgradeUnitsOverride = value;
		}, null, null);

		// Token: 0x040014C5 RID: 5317
		public static readonly ADPropertyDefinition UpgradeConstraintsDisabled = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, bool?>("UpgradeConstraintsDisabled", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeConstraintsDisabled, delegate(OrganizationConfigXML configXml, bool? value)
		{
			configXml.UpgradeConstraintsDisabled = value;
		}, null, null);

		// Token: 0x040014C6 RID: 5318
		public static readonly ADPropertyDefinition UpgradeLastE14CountsUpdateTime = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, DateTime?>("UpgradeLastE14CountsUpdateTime", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.UpgradeLastE14CountsUpdateTime, delegate(OrganizationConfigXML configXml, DateTime? value)
		{
			configXml.UpgradeLastE14CountsUpdateTime = value;
		}, null, null);

		// Token: 0x040014C7 RID: 5319
		public static readonly ADPropertyDefinition MailboxRelease = SharedPropertyDefinitions.MailboxRelease;

		// Token: 0x040014C8 RID: 5320
		public static readonly ADPropertyDefinition PreviousMailboxRelease = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, string>("PreviousMailboxRelease", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.PreviousMailboxRelease, delegate(OrganizationConfigXML configXml, string value)
		{
			configXml.PreviousMailboxRelease = value;
		}, null, null);

		// Token: 0x040014C9 RID: 5321
		public static readonly ADPropertyDefinition PilotMailboxRelease = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, string>("PilotMailboxRelease", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.PilotMailboxRelease, delegate(OrganizationConfigXML configXml, string value)
		{
			configXml.PilotMailboxRelease = value;
		}, null, null);

		// Token: 0x040014CA RID: 5322
		public static readonly ADPropertyDefinition PersistedRelocationConstraints = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, RelocationConstraintArray>("PersistedRelocationConstraints", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.RelocationConstraints, delegate(OrganizationConfigXML configXml, RelocationConstraintArray value)
		{
			configXml.RelocationConstraints = value;
		}, null, null);

		// Token: 0x040014CB RID: 5323
		public static readonly ADPropertyDefinition RelocationConstraints = new ADPropertyDefinition("RelocationConstraints", ExchangeObjectVersion.Exchange2003, typeof(RelocationConstraint), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040014CC RID: 5324
		public static readonly ADPropertyDefinition OriginatedInDifferentForest = new ADPropertyDefinition("OriginatedInDifferentForest", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADObjectSchema.CorrelationIdRaw
		}, null, new GetterDelegate(Organization.OriginatedInDifferentForestGetter), null, null, null);

		// Token: 0x040014CD RID: 5325
		public static readonly ADPropertyDefinition ReleaseTrack = XMLSerializableBase.ConfigXmlProperty<OrganizationConfigXML, ReleaseTrack?>("ReleaseTrack", ExchangeObjectVersion.Exchange2003, OrganizationSchema.ConfigurationXML, null, (OrganizationConfigXML configXml) => configXml.ReleaseTrack, delegate(OrganizationConfigXML configXml, ReleaseTrack? value)
		{
			configXml.ReleaseTrack = value;
		}, null, null);

		// Token: 0x040014CE RID: 5326
		public static readonly ADPropertyDefinition PublicComputersDetectionEnabled = new ADPropertyDefinition("PublicComputersDetectionEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			OrganizationSchema.OrganizationFlags2
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(OrganizationSchema.OrganizationFlags2, 32UL)), ADObject.FlagGetterDelegate(OrganizationSchema.OrganizationFlags2, 32), ADObject.FlagSetterDelegate(OrganizationSchema.OrganizationFlags2, 32), null, null);

		// Token: 0x040014CF RID: 5327
		public static readonly ADPropertyDefinition RmsoSubscriptionStatus = ADObject.BitfieldProperty("RmsoSubscriptionStatus", 7, 3, OrganizationSchema.OrganizationFlags2);

		// Token: 0x040014D0 RID: 5328
		public static readonly ADPropertyDefinition IntuneManagedStatus = ADObject.BitfieldProperty("IntuneManagedStatus", 12, OrganizationSchema.OrganizationFlags2);

		// Token: 0x040014D1 RID: 5329
		public static readonly ADPropertyDefinition MapiHttpEnabled = ADObject.BitfieldProperty("MapiHttpEnabled", 10, OrganizationSchema.OrganizationFlags2);

		// Token: 0x040014D2 RID: 5330
		public static readonly ADPropertyDefinition HybridConfigurationStatus = ADObject.BitfieldProperty("HybridConfigurationStatus", 13, 4, OrganizationSchema.OrganizationFlags2);

		// Token: 0x040014D3 RID: 5331
		public static readonly ADPropertyDefinition OAuth2ClientProfileEnabled = ADObject.BitfieldProperty("OAuth2ClientProfileEnabled", 18, OrganizationSchema.OrganizationFlags2);
	}
}
