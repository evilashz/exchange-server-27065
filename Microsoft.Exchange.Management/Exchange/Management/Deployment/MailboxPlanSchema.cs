using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200023F RID: 575
	internal class MailboxPlanSchema : ServicePlanElementSchema
	{
		// Token: 0x04000847 RID: 2119
		public static readonly FeatureDefinition AutoGroupPermissions = new FeatureDefinition("AutoGroupPermissions", FeatureCategory.MailboxPlanRoleAssignment, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000848 RID: 2120
		public static readonly FeatureDefinition ActiveSyncPermissions = new FeatureDefinition("ActiveSyncPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000849 RID: 2121
		public static readonly FeatureDefinition ImapPermissions = new FeatureDefinition("ImapPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084A RID: 2122
		public static readonly FeatureDefinition MailTipsPermissions = new FeatureDefinition("MailTipsPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084B RID: 2123
		public static readonly FeatureDefinition ModeratedRecipientsPermissions = new FeatureDefinition("ModeratedRecipientsPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084C RID: 2124
		public static readonly FeatureDefinition PopPermissions = new FeatureDefinition("PopPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084D RID: 2125
		public static readonly FeatureDefinition ProfileUpdatePermissions = new FeatureDefinition("ProfileUpdatePermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084E RID: 2126
		public static readonly FeatureDefinition OpenDomainProfileUpdatePermissions = new FeatureDefinition("OpenDomainProfileUpdatePermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400084F RID: 2127
		public static readonly FeatureDefinition TeamMailboxPermissions = new FeatureDefinition("TeamMailboxPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000850 RID: 2128
		public static readonly FeatureDefinition SMSPermissions = new FeatureDefinition("SMSPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000851 RID: 2129
		public static readonly FeatureDefinition UMCloudServicePermissions = new FeatureDefinition("UMCloudServicePermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000852 RID: 2130
		public static readonly FeatureDefinition UMPermissions = new FeatureDefinition("UMPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000853 RID: 2131
		public static readonly FeatureDefinition UMSMSMsgWaitingPermissions = new FeatureDefinition("UMSMSMsgWaitingPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000854 RID: 2132
		public static readonly FeatureDefinition PopSyncPermissions = new FeatureDefinition("PopSyncPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000855 RID: 2133
		public static readonly FeatureDefinition EXOCoreFeatures = new FeatureDefinition("EXOCoreFeatures", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000856 RID: 2134
		public static readonly FeatureDefinition HotmailSyncPermissions = new FeatureDefinition("HotmailSyncPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000857 RID: 2135
		public static readonly FeatureDefinition ImapSyncPermissions = new FeatureDefinition("ImapSyncPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000858 RID: 2136
		public static readonly FeatureDefinition ResetUserPasswordManagementPermissions = new FeatureDefinition("ResetUserPasswordManagementPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000859 RID: 2137
		public static readonly FeatureDefinition UserMailboxAccessPermissions = new FeatureDefinition("UserMailboxAccessPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400085A RID: 2138
		public static readonly FeatureDefinition OrganizationalAffinityPermissions = new FeatureDefinition("OrganizationalAffinityPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400085B RID: 2139
		public static readonly FeatureDefinition MessageTrackingPermissions = new FeatureDefinition("MessageTrackingPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400085C RID: 2140
		public static readonly FeatureDefinition ActiveSyncDeviceDataAccessPermissions = new FeatureDefinition("ActiveSyncDeviceDataAccessPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400085D RID: 2141
		public static readonly FeatureDefinition MOWADeviceDataAccessPermissions = new FeatureDefinition("MOWADeviceDataAccessPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400085E RID: 2142
		public static readonly FeatureDefinition ViewSupervisionListPermissions = new FeatureDefinition("ViewSupervisionListPermissions", FeatureCategory.MailboxPlanPermissions, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x0400085F RID: 2143
		public static readonly FeatureDefinition MaxReceiveTransportQuota = new FeatureDefinition("MaxReceiveTransportQuota", FeatureCategory.MailboxPlanConfiguration, typeof(string), ServicePlanSkus.All);

		// Token: 0x04000860 RID: 2144
		public static readonly FeatureDefinition MaxSendTransportQuota = new FeatureDefinition("MaxSendTransportQuota", FeatureCategory.MailboxPlanConfiguration, typeof(string), ServicePlanSkus.All);

		// Token: 0x04000861 RID: 2145
		public static readonly FeatureDefinition MaxRecipientsTransportQuota = new FeatureDefinition("MaxRecipientsTransportQuota", FeatureCategory.MailboxPlanConfiguration, typeof(string), ServicePlanSkus.All);

		// Token: 0x04000862 RID: 2146
		public static readonly FeatureDefinition ProhibitSendReceiveMaiboxQuota = new FeatureDefinition("ProhibitSendReceiveMaiboxQuota", FeatureCategory.MailboxPlanConfiguration, typeof(string), ServicePlanSkus.All);

		// Token: 0x04000863 RID: 2147
		public static readonly FeatureDefinition ArchiveQuota = new FeatureDefinition("ArchiveQuota", FeatureCategory.MailboxPlanConfiguration, typeof(string), ServicePlanSkus.All);

		// Token: 0x04000864 RID: 2148
		public static readonly FeatureDefinition ImapEnabled = new FeatureDefinition("ImapEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000865 RID: 2149
		public static readonly FeatureDefinition PopEnabled = new FeatureDefinition("PopEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000866 RID: 2150
		public static readonly FeatureDefinition ShowInAddressListsEnabled = new FeatureDefinition("ShowInAddressListsEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000867 RID: 2151
		public static readonly FeatureDefinition OutlookAnywhereEnabled = new FeatureDefinition("OutlookAnywhereEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000868 RID: 2152
		public static readonly FeatureDefinition ActiveSyncEnabled = new FeatureDefinition("ActiveSyncEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x04000869 RID: 2153
		public static readonly FeatureDefinition MOWAEnabled = new FeatureDefinition("MOWAEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400086A RID: 2154
		public static readonly FeatureDefinition EwsEnabled = new FeatureDefinition("EwsEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400086B RID: 2155
		public static readonly FeatureDefinition OrganizationalQueryBaseDNEnabled = new FeatureDefinition("OrganizationalQueryBaseDNEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400086C RID: 2156
		public static readonly FeatureDefinition UMEnabled = new FeatureDefinition("UMEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(UMDeploymentModeOptions), ServicePlanSkus.Datacenter);

		// Token: 0x0400086D RID: 2157
		public static readonly FeatureDefinition SkipResetPasswordOnFirstLogonEnabled = new FeatureDefinition("SkipResetPasswordOnFirstLogonEnabled", FeatureCategory.MailboxPlanSatellite, typeof(bool), ServicePlanSkus.All);

		// Token: 0x0400086E RID: 2158
		public static readonly FeatureDefinition SyncAccountsEnabled = new FeatureDefinition("SyncAccountsEnabled", FeatureCategory.MailboxPlanSatellite, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x0400086F RID: 2159
		public static readonly FeatureDefinition SyncAccountsSyncNowEnabled = new FeatureDefinition("SyncAccountsSyncNowEnabled", FeatureCategory.MailboxPlanSatellite, typeof(bool), ServicePlanSkus.Datacenter);

		// Token: 0x04000870 RID: 2160
		public static readonly FeatureDefinition SyncAccountsMaxAccountsQuota = new FeatureDefinition("SyncAccountsMaxAccountsQuota", FeatureCategory.MailboxPlanSatellite, typeof(string), ServicePlanSkus.Datacenter);

		// Token: 0x04000871 RID: 2161
		public static readonly FeatureDefinition SyncAccountsPollingInterval = new FeatureDefinition("SyncAccountsPollingInterval", FeatureCategory.MailboxPlanSatellite, typeof(string), ServicePlanSkus.Datacenter);

		// Token: 0x04000872 RID: 2162
		public static readonly FeatureDefinition SyncAccountsTimeBeforeInactive = new FeatureDefinition("SyncAccountsTimeBeforeInactive", FeatureCategory.MailboxPlanSatellite, typeof(string), ServicePlanSkus.Datacenter);

		// Token: 0x04000873 RID: 2163
		public static readonly FeatureDefinition SyncAccountsTimeBeforeDormant = new FeatureDefinition("SyncAccountsTimeBeforeDormant", FeatureCategory.MailboxPlanSatellite, typeof(string), ServicePlanSkus.Datacenter);

		// Token: 0x04000874 RID: 2164
		public static readonly FeatureDefinition SkuCapability = new FeatureDefinition("SkuCapability", FeatureCategory.MailboxPlanConfiguration, typeof(Capability), ServicePlanSkus.Datacenter);

		// Token: 0x04000875 RID: 2165
		public static readonly FeatureDefinition SingleItemRecoveryEnabled = new FeatureDefinition("SingleItemRecoveryEnabled", FeatureCategory.MailboxPlanConfiguration, typeof(bool), ServicePlanSkus.Datacenter);
	}
}
