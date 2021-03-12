using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000838 RID: 2104
	internal class SyncUserSchema : SyncOrgPersonSchema
	{
		// Token: 0x170024E6 RID: 9446
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x0016F2C5 File Offset: 0x0016D4C5
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.User;
			}
		}

		// Token: 0x04004497 RID: 17559
		public static SyncPropertyDefinition AssignedPlan = new SyncPropertyDefinition("AssignedPlan", "AssignedPlan", typeof(AssignedPlanValue), typeof(AssignedPlanValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x04004498 RID: 17560
		public static SyncPropertyDefinition MSExchUserCreatedTimestamp = new SyncPropertyDefinition("MSExchUserCreatedTimestamp", "MSExchUserCreatedTimestamp", typeof(DateTime), typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion18, DateTime.MinValue);

		// Token: 0x04004499 RID: 17561
		public static SyncPropertyDefinition SKUCapability = new SyncPropertyDefinition("SKUCapability", null, typeof(Capability), typeof(Capability), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, Capability.None, new ProviderPropertyDefinition[]
		{
			SyncUserSchema.AssignedPlan
		}, new GetterDelegate(SyncUser.SKUCapabilityGetter), null);

		// Token: 0x0400449A RID: 17562
		public static SyncPropertyDefinition AddOnSKUCapability = new SyncPropertyDefinition("AddOnSKUCapability", null, typeof(Capability), typeof(Capability), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, null, new ProviderPropertyDefinition[]
		{
			SyncUserSchema.AssignedPlan
		}, new GetterDelegate(SyncUser.AddOnSKUCapabilityGetter), null);

		// Token: 0x0400449B RID: 17563
		public static SyncPropertyDefinition SKUCapabilityStatus = new SyncPropertyDefinition("SKUCapabilityStatus", null, typeof(AssignedCapabilityStatus?), typeof(AssignedCapabilityStatus?), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, null, new ProviderPropertyDefinition[]
		{
			SyncUserSchema.AssignedPlan
		}, new GetterDelegate(SyncUser.SKUCapabilityStatusGetter), null);

		// Token: 0x0400449C RID: 17564
		public static SyncPropertyDefinition SKUAssigned = new SyncPropertyDefinition("SKUAssigned", null, typeof(bool), typeof(bool), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, false, new ProviderPropertyDefinition[]
		{
			SyncUserSchema.AssignedPlan
		}, new GetterDelegate(SyncUser.SKUAssignedGetter), null);

		// Token: 0x0400449D RID: 17565
		public static SyncPropertyDefinition ServiceInfo = new SyncPropertyDefinition("ServiceInfo", "ServiceInfo", typeof(ServiceInfoValue), typeof(ServiceInfoValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion16, null);

		// Token: 0x0400449E RID: 17566
		public static SyncPropertyDefinition ReleaseTrack = new SyncPropertyDefinition("ReleaseTrack", null, typeof(string), typeof(string), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion16, string.Empty);

		// Token: 0x0400449F RID: 17567
		public static SyncPropertyDefinition CloudMsExchArchiveStatus = new SyncPropertyDefinition(ADUserSchema.ArchiveStatus, "CloudMSExchArchiveStatus", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A0 RID: 17568
		public static SyncPropertyDefinition CloudMsExchBlockedSendersHash = new SyncPropertyDefinition(ADRecipientSchema.BlockedSendersHash, "CloudMSExchBlockedSendersHash", typeof(DirectoryPropertyBinarySingleLength1To4000), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A1 RID: 17569
		public static SyncPropertyDefinition CloudMsExchSafeRecipientsHash = new SyncPropertyDefinition(ADRecipientSchema.SafeRecipientsHash, "CloudMSExchSafeRecipientsHash", typeof(DirectoryPropertyBinarySingleLength1To12000), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A2 RID: 17570
		public static SyncPropertyDefinition CloudMsExchSafeSendersHash = new SyncPropertyDefinition(ADRecipientSchema.SafeSendersHash, "CloudMSExchSafeSendersHash", typeof(DirectoryPropertyBinarySingleLength1To32000), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A3 RID: 17571
		public static SyncPropertyDefinition CloudMsExchUCVoiceMailSettings = new SyncPropertyDefinition(ADUserSchema.VoiceMailSettings, "CloudMSExchUCVoiceMailSettings", typeof(DirectoryPropertyStringLength1To1123), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A4 RID: 17572
		public static SyncPropertyDefinition CloudPublicDelegates = new SyncPropertyDefinition(ADRecipientSchema.GrantSendOnBehalfTo, "CloudPublicDelegates", typeof(SyncLink), typeof(CloudPublicDelegates), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A5 RID: 17573
		public static SyncPropertyDefinition CloudSiteMailboxOwners = new SyncPropertyDefinition(ADUserSchema.Owners, "CloudMSExchTeamMailboxOwners", typeof(PropertyReference), typeof(DirectoryPropertyReferenceAddressList), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A6 RID: 17574
		public static SyncPropertyDefinition CloudSiteMailboxUsers = new SyncPropertyDefinition(ADMailboxRecipientSchema.DelegateListLink, "CloudMSExchDelegateListLink", typeof(SyncLink), typeof(CloudMSExchDelegateListLink), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A7 RID: 17575
		public static SyncPropertyDefinition CloudSiteMailboxClosedTime = new SyncPropertyDefinition(ADUserSchema.TeamMailboxClosedTime, "CloudMSExchTeamMailboxExpiration", typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A8 RID: 17576
		public static SyncPropertyDefinition CloudSharePointUrl = new SyncPropertyDefinition(ADMailboxRecipientSchema.SharePointUrl, "CloudMSExchTeamMailboxSharePointUrl", typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044A9 RID: 17577
		public static SyncPropertyDefinition ExchangeGuid = new SyncPropertyDefinition(ADMailboxRecipientSchema.ExchangeGuid, "MSExchMailboxGuid", typeof(Guid), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AA RID: 17578
		public static SyncPropertyDefinition ImmutableId = new SyncPropertyDefinition(ADRecipientSchema.ImmutableId, "MSExchImmutableId", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AB RID: 17579
		public static SyncPropertyDefinition Manager = new SyncPropertyDefinition(ADUserSchema.Manager, "Manager", typeof(SyncLink), typeof(Manager), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AC RID: 17580
		public static SyncPropertyDefinition NetID = new SyncPropertyDefinition(ADUserSchema.NetID, "WindowsLiveNetId", typeof(DirectoryPropertyBinarySingleLength8), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AD RID: 17581
		public static SyncPropertyDefinition Picture = new SyncPropertyDefinition(ADRecipientSchema.ThumbnailPhoto, "ThumbnailPhoto", typeof(DirectoryPropertyBinarySingleLength1To102400), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AE RID: 17582
		public static SyncPropertyDefinition RecipientSoftDeletedStatus = new SyncPropertyDefinition(ADRecipientSchema.RecipientSoftDeletedStatus, "RecipientSoftDeletedStatus", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.BackSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044AF RID: 17583
		public static SyncPropertyDefinition WhenSoftDeleted = new SyncPropertyDefinition(ADRecipientSchema.WhenSoftDeleted, "SoftDeletionTimestamp", typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B0 RID: 17584
		public static SyncPropertyDefinition ServiceOriginatedResource = new SyncPropertyDefinition(ADRecipientSchema.RawCapabilities, "ServiceOriginatedResource", typeof(DirectoryPropertyXmlServiceOriginatedResource), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud | SyncPropertyDefinitionFlags.MultiValued | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B1 RID: 17585
		public static SyncPropertyDefinition WindowsLiveID = new SyncPropertyDefinition(ADRecipientSchema.WindowsLiveID, "UserPrincipalName", typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B2 RID: 17586
		public static SyncPropertyDefinition ArchiveGuid = new SyncPropertyDefinition(ADUserSchema.ArchiveGuid, "MSExchArchiveGuid", typeof(DirectoryPropertyGuidSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B3 RID: 17587
		public static SyncPropertyDefinition ArchiveName = new SyncPropertyDefinition(ADUserSchema.ArchiveName, "MSExchArchiveName", typeof(DirectoryPropertyStringLength1To512), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B4 RID: 17588
		public static SyncPropertyDefinition AuditAdminFlags = new SyncPropertyDefinition(ADRecipientSchema.AuditAdminFlags, "MSExchAuditAdmin", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B5 RID: 17589
		public static SyncPropertyDefinition AuditBypassEnabled = new SyncPropertyDefinition(ADRecipientSchema.AuditBypassEnabled, "MSExchBypassAudit", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B6 RID: 17590
		public static SyncPropertyDefinition AuditDelegateFlags = new SyncPropertyDefinition(ADRecipientSchema.AuditDelegateFlags, "MSExchAuditDelegate", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B7 RID: 17591
		public static SyncPropertyDefinition AuditDelegateAdminFlags = new SyncPropertyDefinition(ADRecipientSchema.AuditDelegateAdminFlags, "MSExchAuditDelegateAdmin", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B8 RID: 17592
		public static SyncPropertyDefinition AuditEnabled = new SyncPropertyDefinition(ADRecipientSchema.AuditEnabled, "MSExchMailboxAuditEnable", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044B9 RID: 17593
		public static SyncPropertyDefinition AuditOwnerFlags = new SyncPropertyDefinition(ADRecipientSchema.AuditOwnerFlags, "MSExchAuditOwner", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BA RID: 17594
		public static SyncPropertyDefinition AuditLogAgeLimit = new SyncPropertyDefinition(ADRecipientSchema.AuditLogAgeLimit, "MSExchMailboxAuditLogAgeLimit", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BB RID: 17595
		public static SyncPropertyDefinition DeliverToMailboxAndForward = new SyncPropertyDefinition(ADMailboxRecipientSchema.DeliverToMailboxAndForward, "DeliverAndRedirect", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BC RID: 17596
		public static SyncPropertyDefinition ElcExpirationSuspensionEndDate = new SyncPropertyDefinition(ADUserSchema.ElcExpirationSuspensionEndDate, "MSExchElcExpirySuspensionEnd", typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BD RID: 17597
		public static SyncPropertyDefinition ElcExpirationSuspensionStartDate = new SyncPropertyDefinition(ADUserSchema.ElcExpirationSuspensionStartDate, "MSExchElcExpirySuspensionStart", typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BE RID: 17598
		public static SyncPropertyDefinition ElcMailboxFlags = new SyncPropertyDefinition(ADUserSchema.ElcMailboxFlags, "MSExchElcMailboxFlags", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044BF RID: 17599
		public static SyncPropertyDefinition InPlaceHoldsRaw = new SyncPropertyDefinition("InPlaceHoldsRaw", "MSExchUserHoldPolicies", typeof(string), typeof(DirectoryPropertyStringLength1To40), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion8, null);

		// Token: 0x040044C0 RID: 17600
		public static SyncPropertyDefinition CloudMsExchUserHoldPolicies = new SyncPropertyDefinition(ADRecipientSchema.InPlaceHoldsRaw, "CloudMSExchUserHoldPolicies", typeof(DirectoryPropertyStringLength1To40), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.Cloud | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion8);

		// Token: 0x040044C1 RID: 17601
		public static SyncPropertyDefinition ResourceCapacity = new SyncPropertyDefinition(ADRecipientSchema.ResourceCapacity, "MSExchResourceCapacity", typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C2 RID: 17602
		public static SyncPropertyDefinition ResourcePropertiesDisplay = new SyncPropertyDefinition(ADRecipientSchema.ResourcePropertiesDisplay, "MSExchResourceDisplay", typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C3 RID: 17603
		public static SyncPropertyDefinition ResourceMetaData = new SyncPropertyDefinition(ADRecipientSchema.ResourceMetaData, "MSExchResourceMetadata", typeof(DirectoryPropertyStringLength1To1024), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C4 RID: 17604
		public static SyncPropertyDefinition ResourceSearchProperties = new SyncPropertyDefinition(ADRecipientSchema.ResourceSearchProperties, "MSExchResourceSearchProperties", typeof(DirectoryPropertyStringLength1To1024), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C5 RID: 17605
		public static SyncPropertyDefinition RemoteRecipientType = new SyncPropertyDefinition(ADUserSchema.RemoteRecipientType, "MSExchRemoteRecipientType", typeof(DirectoryPropertyInt64Single), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C6 RID: 17606
		public static SyncPropertyDefinition UsageLocation = new SyncPropertyDefinition(ADRecipientSchema.UsageLocation, "UsageLocation", typeof(DirectoryPropertyStringSingleLength1To3), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040044C7 RID: 17607
		public static SyncPropertyDefinition SiteMailboxOwners = new SyncPropertyDefinition("TeamMailboxOwners", "MSExchTeamMailboxOwners", typeof(PropertyReference), typeof(DirectoryPropertyReferenceAddressList), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion9, null);

		// Token: 0x040044C8 RID: 17608
		public static SyncPropertyDefinition SiteMailboxUsers = new SyncPropertyDefinition("SiteMailboxUsers", "MSExchDelegateListLink", typeof(SyncLink), typeof(MSExchDelegateListLink), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion9, null);

		// Token: 0x040044C9 RID: 17609
		public static SyncPropertyDefinition SiteMailboxClosedTime = new SyncPropertyDefinition("TeamMailboxClosedTime", "MSExchTeamMailboxExpiration", typeof(DateTime?), typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion9, null);

		// Token: 0x040044CA RID: 17610
		public static SyncPropertyDefinition SharePointUrl = new SyncPropertyDefinition("SharePointUrl", "MSExchTeamMailboxSharePointUrl", typeof(Uri), typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion9, null);

		// Token: 0x040044CB RID: 17611
		public static SyncPropertyDefinition ProvisionedPlan = new SyncPropertyDefinition("ProvisionedPlan", null, typeof(ProvisionedPlanValue), typeof(ProvisionedPlanValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.IgnoredSyncPropertySetVersion, null);

		// Token: 0x040044CC RID: 17612
		public static SyncPropertyDefinition AccountEnabled = new SyncPropertyDefinition("AccountEnabled", "AccountEnabled", typeof(bool?), typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion19, null);

		// Token: 0x040044CD RID: 17613
		public static SyncPropertyDefinition StsRefreshTokensValidFrom = new SyncPropertyDefinition("StsRefreshTokensValidFrom", "StsRefreshTokensValidFrom", typeof(DateTime?), typeof(DirectoryPropertyDateTimeSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion19, null);
	}
}
