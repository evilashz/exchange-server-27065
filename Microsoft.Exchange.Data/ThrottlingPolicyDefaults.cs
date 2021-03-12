using System;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AF RID: 431
	internal static class ThrottlingPolicyDefaults
	{
		// Token: 0x06000E19 RID: 3609 RVA: 0x0002D9FC File Offset: 0x0002BBFC
		static ThrottlingPolicyDefaults()
		{
			bool flag = Datacenter.IsMultiTenancyEnabled();
			if (flag)
			{
				ThrottlingPolicyDefaults.SetAnonymousDefaults(1U, 120000U, 420000U, 720000U);
				ThrottlingPolicyDefaults.SetEasDefaults(4U, 480000U, 1800000U, 600000U, 100U, 20U, 180U);
				ThrottlingPolicyDefaults.SetEwsDefaults(27U, 300000U, 900000U, 3000000U, 20U);
				ThrottlingPolicyDefaults.SetImapDefaults(20U, 3600000U, 600000U, 600000U);
				ThrottlingPolicyDefaults.SetOutlookServiceDefaults(27U, 300000U, 900000U, 3000000U, 20U, 4U, 12U);
				ThrottlingPolicyDefaults.SetOwaDefaults(20U, 480000U, 1800000U, Unlimited<uint>.UnlimitedValue, 3U, 75000U, 375000U, 525000U);
				ThrottlingPolicyDefaults.SetPopDefaults(20U, 3600000U, 600000U, 600000U);
				ThrottlingPolicyDefaults.SetPowerShellDefaults(3U, 9U, 600000U, 1800000U, 3000000U, 400U, 5U, 25U, 50U, 120U, 60U, 200U, 5U, 18U, 60U);
				ThrottlingPolicyDefaults.SetPswsDefaults(3U, 25U, 5U);
				ThrottlingPolicyDefaults.SetRcaDefaults(40U, 150000U, 900000U, Unlimited<uint>.UnlimitedValue);
				ThrottlingPolicyDefaults.SetCpaDefaults(20U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
				ThrottlingPolicyDefaults.SetDiscoveryDefaults(2U, 5000U, 500U, 5000U, 100U, 200U, 25U, 10U, 32U, 10U);
				ThrottlingPolicyDefaults.SetGeneralDefaults(30U, 10000U, 10U);
				ThrottlingPolicyDefaults.SetPushNotificationDefaults(10U, 30000U, 120000U, 240000U, 10U, 6U, 600000U);
				ThrottlingPolicyDefaults.SetE4eSenderDefaults(200U, 4800000U, 18000000U, Unlimited<uint>.UnlimitedValue);
				ThrottlingPolicyDefaults.SetE4eRecipientDefaults(20U, 480000U, 1800000U, Unlimited<uint>.UnlimitedValue);
				ThrottlingPolicyDefaults.SetComplianceDefaults(10000U, 25U);
				return;
			}
			ThrottlingPolicyDefaults.SetAnonymousDefaults(1U, 120000U, 420000U, 720000U);
			ThrottlingPolicyDefaults.SetEasDefaults(10U, 480000U, 1800000U, 600000U, 100U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetEwsDefaults(27U, 300000U, 900000U, 3000000U, 5000U);
			ThrottlingPolicyDefaults.SetImapDefaults(Unlimited<uint>.UnlimitedValue, 3600000U, 600000U, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetOutlookServiceDefaults(27U, 300000U, 900000U, 3000000U, 5000U, 4U, 12U);
			ThrottlingPolicyDefaults.SetOwaDefaults(20U, 480000U, 1800000U, Unlimited<uint>.UnlimitedValue, 3U, 75000U, 375000U, 525000U);
			ThrottlingPolicyDefaults.SetPopDefaults(20U, 3600000U, 600000U, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetPowerShellDefaults(18U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetPswsDefaults(18U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetRcaDefaults(40U, 150000U, 900000U, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetCpaDefaults(20U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetDiscoveryDefaults(2U, 5000U, 500U, 5000U, 100U, 200U, 25U, 10U, 32U, 10U);
			ThrottlingPolicyDefaults.SetGeneralDefaults(Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetPushNotificationDefaults(20U, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, Unlimited<uint>.UnlimitedValue, 10U, 6U, 600000U);
			ThrottlingPolicyDefaults.SetE4eSenderDefaults(200U, 4800000U, 18000000U, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetE4eRecipientDefaults(20U, 480000U, 1800000U, Unlimited<uint>.UnlimitedValue);
			ThrottlingPolicyDefaults.SetComplianceDefaults(10000U, 25U);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0002E086 File Offset: 0x0002C286
		private static void SetEasDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> maxDevices, Unlimited<uint> maxDeviceDeletesPerMonth, Unlimited<uint> maxInactivityForDeviceCleanup)
		{
			ThrottlingPolicyDefaults.EasMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.EasMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.EasRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.EasCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.EasMaxDevices = maxDevices;
			ThrottlingPolicyDefaults.EasMaxDeviceDeletesPerMonth = maxDeviceDeletesPerMonth;
			ThrottlingPolicyDefaults.EasMaxInactivityForDeviceCleanup = maxInactivityForDeviceCleanup;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002E0B5 File Offset: 0x0002C2B5
		private static void SetEwsDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> maxSubscriptions)
		{
			ThrottlingPolicyDefaults.EwsMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.EwsMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.EwsRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.EwsCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.EwsMaxSubscriptions = maxSubscriptions;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002E0D6 File Offset: 0x0002C2D6
		private static void SetImapDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.ImapMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.ImapMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.ImapRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.ImapCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		private static void SetOutlookServiceDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> maxSubscriptions, Unlimited<uint> maxSocketConnectionsPerDevice, Unlimited<uint> maxSocketConnectionsPerUser)
		{
			ThrottlingPolicyDefaults.OutlookServiceMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.OutlookServiceMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.OutlookServiceRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.OutlookServiceCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.OutlookServiceMaxSubscriptions = maxSubscriptions;
			ThrottlingPolicyDefaults.OutlookServiceMaxSocketConnectionsPerDevice = maxSocketConnectionsPerDevice;
			ThrottlingPolicyDefaults.OutlookServiceMaxSocketConnectionsPerUser = maxSocketConnectionsPerUser;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002E11F File Offset: 0x0002C31F
		private static void SetOwaDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> voiceMaxConcurrency, Unlimited<uint> voiceMaxBurst, Unlimited<uint> voiceRechargeRate, Unlimited<uint> voiceCutoffBalance)
		{
			ThrottlingPolicyDefaults.OwaMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.OwaMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.OwaRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.OwaCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.OwaVoiceMaxConcurrency = voiceMaxConcurrency;
			ThrottlingPolicyDefaults.OwaVoiceMaxBurst = voiceMaxBurst;
			ThrottlingPolicyDefaults.OwaVoiceRechargeRate = voiceRechargeRate;
			ThrottlingPolicyDefaults.OwaVoiceCutoffBalance = voiceCutoffBalance;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002E155 File Offset: 0x0002C355
		private static void SetPopDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.PopMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.PopMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.PopRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.PopCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002E170 File Offset: 0x0002C370
		private static void SetPowerShellDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxTenantConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> maxOperations, Unlimited<uint> maxCmdletsTimePeriod, Unlimited<uint> exchangeMaxCmdlets, Unlimited<uint> maxCmdletQueueDepth, Unlimited<uint> maxDestructiveCmdlets, Unlimited<uint> maxDestructiveCmdletsTimePeriod, Unlimited<uint> maxCmdlets, Unlimited<uint> maxRunspaces, Unlimited<uint> maxTenantRunspaces, Unlimited<uint> maxRunspacesTimePeriod)
		{
			ThrottlingPolicyDefaults.PowerShellMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.PowerShellMaxTenantConcurrency = maxTenantConcurrency;
			ThrottlingPolicyDefaults.PowerShellMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.PowerShellRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.PowerShellCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.PowerShellMaxOperations = maxOperations;
			ThrottlingPolicyDefaults.PowerShellMaxCmdletsTimePeriod = maxCmdletsTimePeriod;
			ThrottlingPolicyDefaults.ExchangeMaxCmdlets = exchangeMaxCmdlets;
			ThrottlingPolicyDefaults.PowerShellMaxCmdletQueueDepth = maxCmdletQueueDepth;
			ThrottlingPolicyDefaults.PowerShellMaxDestructiveCmdlets = maxDestructiveCmdlets;
			ThrottlingPolicyDefaults.PowerShellMaxDestructiveCmdletsTimePeriod = maxDestructiveCmdletsTimePeriod;
			ThrottlingPolicyDefaults.PowerShellMaxCmdlets = maxCmdlets;
			ThrottlingPolicyDefaults.PowerShellMaxRunspaces = maxRunspaces;
			ThrottlingPolicyDefaults.PowerShellMaxTenantRunspaces = maxTenantRunspaces;
			ThrottlingPolicyDefaults.PowerShellMaxRunspacesTimePeriod = maxRunspacesTimePeriod;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002E1E2 File Offset: 0x0002C3E2
		private static void SetPswsDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxRequest, Unlimited<uint> maxRequestTimePeriod)
		{
			ThrottlingPolicyDefaults.PswsMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.PswsMaxRequest = maxRequest;
			ThrottlingPolicyDefaults.PswsMaxRequestTimePeriod = maxRequestTimePeriod;
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002E1F6 File Offset: 0x0002C3F6
		private static void SetAnonymousDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.AnonymousMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.AnonymousMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.AnonymousRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.AnonymousCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002E210 File Offset: 0x0002C410
		private static void SetRcaDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.RcaMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.RcaMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.RcaRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.RcaCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002E22A File Offset: 0x0002C42A
		private static void SetCpaDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.CpaMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.CpaMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.CpaRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.CpaCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002E244 File Offset: 0x0002C444
		private static void SetGeneralDefaults(Unlimited<uint> messageRateLimit, Unlimited<uint> recipientRateLimit, Unlimited<uint> forwardeeLimit)
		{
			ThrottlingPolicyDefaults.MessageRateLimit = messageRateLimit;
			ThrottlingPolicyDefaults.RecipientRateLimit = recipientRateLimit;
			ThrottlingPolicyDefaults.ForwardeeLimit = forwardeeLimit;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002E258 File Offset: 0x0002C458
		private static void SetDiscoveryDefaults(Unlimited<uint> discoveryMaxConcurrency, Unlimited<uint> discoveryMaxMailboxes, Unlimited<uint> discoveryMaxKeywords, Unlimited<uint> discoveryMaxPreviewSearchMailboxes, Unlimited<uint> discoveryMaxStatsSearchMailboxes, Unlimited<uint> discoveryPreviewSearchResultsPageSize, Unlimited<uint> discoveryMaxKeywordsPerPage, Unlimited<uint> discoveryMaxRefinerResults, Unlimited<uint> discoveryMaxSearchQueueDepth, Unlimited<uint> discoverySearchTimeoutPeriod)
		{
			ThrottlingPolicyDefaults.DiscoveryMaxConcurrency = discoveryMaxConcurrency;
			ThrottlingPolicyDefaults.DiscoveryMaxMailboxes = discoveryMaxMailboxes;
			ThrottlingPolicyDefaults.DiscoveryMaxKeywords = discoveryMaxKeywords;
			ThrottlingPolicyDefaults.DiscoveryMaxPreviewSearchMailboxes = discoveryMaxPreviewSearchMailboxes;
			ThrottlingPolicyDefaults.DiscoveryMaxStatsSearchMailboxes = discoveryMaxStatsSearchMailboxes;
			ThrottlingPolicyDefaults.DiscoveryPreviewSearchResultsPageSize = discoveryPreviewSearchResultsPageSize;
			ThrottlingPolicyDefaults.DiscoveryMaxKeywordsPerPage = discoveryMaxKeywordsPerPage;
			ThrottlingPolicyDefaults.DiscoveryMaxRefinerResults = discoveryMaxRefinerResults;
			ThrottlingPolicyDefaults.DiscoveryMaxSearchQueueDepth = discoveryMaxSearchQueueDepth;
			ThrottlingPolicyDefaults.DiscoverySearchTimeoutPeriod = discoverySearchTimeoutPeriod;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002E2A7 File Offset: 0x0002C4A7
		private static void SetPushNotificationDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance, Unlimited<uint> maxBurstPerDevice, Unlimited<uint> rechargeRatePerDevice, Unlimited<uint> samplingPeriodPerDevice)
		{
			ThrottlingPolicyDefaults.PushNotificationMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.PushNotificationMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.PushNotificationRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.PushNotificationCutoffBalance = cutoffBalance;
			ThrottlingPolicyDefaults.PushNotificationMaxBurstPerDevice = maxBurstPerDevice;
			ThrottlingPolicyDefaults.PushNotificationRechargeRatePerDevice = rechargeRatePerDevice;
			ThrottlingPolicyDefaults.PushNotificationSamplingPeriodPerDevice = samplingPeriodPerDevice;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002E2D6 File Offset: 0x0002C4D6
		private static void SetE4eSenderDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.EncryptionSenderMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.EncryptionSenderMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.EncryptionSenderRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.EncryptionSenderCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002E2F0 File Offset: 0x0002C4F0
		private static void SetE4eRecipientDefaults(Unlimited<uint> maxConcurrency, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> cutoffBalance)
		{
			ThrottlingPolicyDefaults.EncryptionRecipientMaxConcurrency = maxConcurrency;
			ThrottlingPolicyDefaults.EncryptionRecipientMaxBurst = maxBurst;
			ThrottlingPolicyDefaults.EncryptionRecipientRechargeRate = rechargeRate;
			ThrottlingPolicyDefaults.EncryptionRecipientCutoffBalance = cutoffBalance;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002E30A File Offset: 0x0002C50A
		private static void SetComplianceDefaults(Unlimited<uint> complianceMaxExpansionDGRecipients, Unlimited<uint> complianceMaxExpansionNestedDGs)
		{
			ThrottlingPolicyDefaults.ComplianceMaxExpansionDGRecipients = complianceMaxExpansionDGRecipients;
			ThrottlingPolicyDefaults.ComplianceMaxExpansionNestedDGs = complianceMaxExpansionNestedDGs;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000E2B RID: 3627 RVA: 0x0002E318 File Offset: 0x0002C518
		// (set) Token: 0x06000E2C RID: 3628 RVA: 0x0002E31F File Offset: 0x0002C51F
		public static Unlimited<uint> EasMaxConcurrency { get; private set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002E327 File Offset: 0x0002C527
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0002E32E File Offset: 0x0002C52E
		public static Unlimited<uint> EasMaxBurst { get; private set; }

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x0002E336 File Offset: 0x0002C536
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x0002E33D File Offset: 0x0002C53D
		public static Unlimited<uint> EasRechargeRate { get; private set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0002E345 File Offset: 0x0002C545
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x0002E34C File Offset: 0x0002C54C
		public static Unlimited<uint> EasCutoffBalance { get; private set; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000E33 RID: 3635 RVA: 0x0002E354 File Offset: 0x0002C554
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x0002E35B File Offset: 0x0002C55B
		public static Unlimited<uint> EasMaxDevices { get; private set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0002E363 File Offset: 0x0002C563
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x0002E36A File Offset: 0x0002C56A
		public static Unlimited<uint> EasMaxDeviceDeletesPerMonth { get; private set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0002E372 File Offset: 0x0002C572
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x0002E379 File Offset: 0x0002C579
		public static Unlimited<uint> EasMaxInactivityForDeviceCleanup { get; private set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0002E381 File Offset: 0x0002C581
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x0002E388 File Offset: 0x0002C588
		public static Unlimited<uint> EwsMaxConcurrency { get; private set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0002E390 File Offset: 0x0002C590
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0002E397 File Offset: 0x0002C597
		public static Unlimited<uint> EwsMaxBurst { get; private set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0002E39F File Offset: 0x0002C59F
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0002E3A6 File Offset: 0x0002C5A6
		public static Unlimited<uint> EwsRechargeRate { get; private set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0002E3AE File Offset: 0x0002C5AE
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x0002E3B5 File Offset: 0x0002C5B5
		public static Unlimited<uint> EwsCutoffBalance { get; private set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0002E3BD File Offset: 0x0002C5BD
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x0002E3C4 File Offset: 0x0002C5C4
		public static Unlimited<uint> EwsMaxSubscriptions { get; private set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0002E3D3 File Offset: 0x0002C5D3
		public static Unlimited<uint> ImapMaxConcurrency { get; private set; }

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x0002E3DB File Offset: 0x0002C5DB
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0002E3E2 File Offset: 0x0002C5E2
		public static Unlimited<uint> ImapMaxBurst { get; private set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0002E3EA File Offset: 0x0002C5EA
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0002E3F1 File Offset: 0x0002C5F1
		public static Unlimited<uint> ImapRechargeRate { get; private set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002E3F9 File Offset: 0x0002C5F9
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0002E400 File Offset: 0x0002C600
		public static Unlimited<uint> ImapCutoffBalance { get; private set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0002E408 File Offset: 0x0002C608
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0002E40F File Offset: 0x0002C60F
		public static Unlimited<uint> OutlookServiceMaxConcurrency { get; private set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0002E417 File Offset: 0x0002C617
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0002E41E File Offset: 0x0002C61E
		public static Unlimited<uint> OutlookServiceMaxBurst { get; private set; }

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0002E426 File Offset: 0x0002C626
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x0002E42D File Offset: 0x0002C62D
		public static Unlimited<uint> OutlookServiceRechargeRate { get; private set; }

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0002E435 File Offset: 0x0002C635
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x0002E43C File Offset: 0x0002C63C
		public static Unlimited<uint> OutlookServiceCutoffBalance { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0002E444 File Offset: 0x0002C644
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0002E44B File Offset: 0x0002C64B
		public static Unlimited<uint> OutlookServiceMaxSubscriptions { get; private set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0002E453 File Offset: 0x0002C653
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0002E45A File Offset: 0x0002C65A
		public static Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice { get; private set; }

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0002E462 File Offset: 0x0002C662
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0002E469 File Offset: 0x0002C669
		public static Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0002E471 File Offset: 0x0002C671
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0002E478 File Offset: 0x0002C678
		public static Unlimited<uint> OwaMaxConcurrency { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0002E480 File Offset: 0x0002C680
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0002E487 File Offset: 0x0002C687
		public static Unlimited<uint> OwaMaxBurst { get; private set; }

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0002E48F File Offset: 0x0002C68F
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0002E496 File Offset: 0x0002C696
		public static Unlimited<uint> OwaRechargeRate { get; private set; }

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002E49E File Offset: 0x0002C69E
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0002E4A5 File Offset: 0x0002C6A5
		public static Unlimited<uint> OwaCutoffBalance { get; private set; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0002E4AD File Offset: 0x0002C6AD
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0002E4B4 File Offset: 0x0002C6B4
		public static Unlimited<uint> OwaVoiceMaxConcurrency { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0002E4BC File Offset: 0x0002C6BC
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0002E4C3 File Offset: 0x0002C6C3
		public static Unlimited<uint> OwaVoiceMaxBurst { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0002E4CB File Offset: 0x0002C6CB
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0002E4D2 File Offset: 0x0002C6D2
		public static Unlimited<uint> OwaVoiceRechargeRate { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0002E4DA File Offset: 0x0002C6DA
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0002E4E1 File Offset: 0x0002C6E1
		public static Unlimited<uint> OwaVoiceCutoffBalance { get; private set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002E4E9 File Offset: 0x0002C6E9
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0002E4F0 File Offset: 0x0002C6F0
		public static Unlimited<uint> PopMaxConcurrency { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0002E4FF File Offset: 0x0002C6FF
		public static Unlimited<uint> PopMaxBurst { get; private set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0002E507 File Offset: 0x0002C707
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0002E50E File Offset: 0x0002C70E
		public static Unlimited<uint> PopRechargeRate { get; private set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0002E516 File Offset: 0x0002C716
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x0002E51D File Offset: 0x0002C71D
		public static Unlimited<uint> PopCutoffBalance { get; private set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0002E525 File Offset: 0x0002C725
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0002E52C File Offset: 0x0002C72C
		public static Unlimited<uint> PowerShellMaxConcurrency { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002E534 File Offset: 0x0002C734
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0002E53B File Offset: 0x0002C73B
		public static Unlimited<uint> PowerShellMaxTenantConcurrency { get; private set; }

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0002E543 File Offset: 0x0002C743
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0002E54A File Offset: 0x0002C74A
		public static Unlimited<uint> PowerShellMaxBurst { get; private set; }

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0002E552 File Offset: 0x0002C752
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0002E559 File Offset: 0x0002C759
		public static Unlimited<uint> PowerShellRechargeRate { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0002E561 File Offset: 0x0002C761
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0002E568 File Offset: 0x0002C768
		public static Unlimited<uint> PowerShellCutoffBalance { get; private set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0002E570 File Offset: 0x0002C770
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0002E577 File Offset: 0x0002C777
		public static Unlimited<uint> PowerShellMaxOperations { get; private set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0002E57F File Offset: 0x0002C77F
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0002E586 File Offset: 0x0002C786
		public static Unlimited<uint> PowerShellMaxCmdletsTimePeriod { get; private set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0002E58E File Offset: 0x0002C78E
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0002E595 File Offset: 0x0002C795
		public static Unlimited<uint> ExchangeMaxCmdlets { get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002E59D File Offset: 0x0002C79D
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
		public static Unlimited<uint> PowerShellMaxCmdletQueueDepth { get; private set; }

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002E5AC File Offset: 0x0002C7AC
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0002E5B3 File Offset: 0x0002C7B3
		public static Unlimited<uint> PowerShellMaxDestructiveCmdlets { get; private set; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0002E5BB File Offset: 0x0002C7BB
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0002E5C2 File Offset: 0x0002C7C2
		public static Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod { get; private set; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0002E5CA File Offset: 0x0002C7CA
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0002E5D1 File Offset: 0x0002C7D1
		public static Unlimited<uint> PowerShellMaxCmdlets { get; private set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0002E5D9 File Offset: 0x0002C7D9
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		public static Unlimited<uint> PowerShellMaxRunspaces { get; private set; }

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0002E5E8 File Offset: 0x0002C7E8
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0002E5EF File Offset: 0x0002C7EF
		public static Unlimited<uint> PowerShellMaxTenantRunspaces { get; private set; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0002E5F7 File Offset: 0x0002C7F7
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0002E5FE File Offset: 0x0002C7FE
		public static Unlimited<uint> PowerShellMaxRunspacesTimePeriod { get; private set; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0002E606 File Offset: 0x0002C806
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0002E60D File Offset: 0x0002C80D
		public static Unlimited<uint> PswsMaxConcurrency { get; private set; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x0002E615 File Offset: 0x0002C815
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0002E61C File Offset: 0x0002C81C
		public static Unlimited<uint> PswsMaxRequest { get; private set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002E624 File Offset: 0x0002C824
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0002E62B File Offset: 0x0002C82B
		public static Unlimited<uint> PswsMaxRequestTimePeriod { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x0002E633 File Offset: 0x0002C833
		// (set) Token: 0x06000E96 RID: 3734 RVA: 0x0002E63A File Offset: 0x0002C83A
		public static Unlimited<uint> AnonymousMaxConcurrency { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x0002E642 File Offset: 0x0002C842
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x0002E649 File Offset: 0x0002C849
		public static Unlimited<uint> AnonymousMaxBurst { get; private set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0002E651 File Offset: 0x0002C851
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0002E658 File Offset: 0x0002C858
		public static Unlimited<uint> AnonymousRechargeRate { get; private set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0002E660 File Offset: 0x0002C860
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0002E667 File Offset: 0x0002C867
		public static Unlimited<uint> AnonymousCutoffBalance { get; private set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0002E66F File Offset: 0x0002C86F
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x0002E676 File Offset: 0x0002C876
		public static Unlimited<uint> RcaMaxConcurrency { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0002E67E File Offset: 0x0002C87E
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x0002E685 File Offset: 0x0002C885
		public static Unlimited<uint> RcaMaxBurst { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0002E68D File Offset: 0x0002C88D
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0002E694 File Offset: 0x0002C894
		public static Unlimited<uint> RcaRechargeRate { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0002E69C File Offset: 0x0002C89C
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x0002E6A3 File Offset: 0x0002C8A3
		public static Unlimited<uint> RcaCutoffBalance { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0002E6AB File Offset: 0x0002C8AB
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0002E6B2 File Offset: 0x0002C8B2
		public static Unlimited<uint> CpaMaxConcurrency { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002E6BA File Offset: 0x0002C8BA
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x0002E6C1 File Offset: 0x0002C8C1
		public static Unlimited<uint> CpaMaxBurst { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0002E6C9 File Offset: 0x0002C8C9
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public static Unlimited<uint> CpaRechargeRate { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0002E6D8 File Offset: 0x0002C8D8
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x0002E6DF File Offset: 0x0002C8DF
		public static Unlimited<uint> CpaCutoffBalance { get; private set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0002E6E7 File Offset: 0x0002C8E7
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x0002E6EE File Offset: 0x0002C8EE
		public static Unlimited<uint> MessageRateLimit { get; private set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0002E6F6 File Offset: 0x0002C8F6
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x0002E6FD File Offset: 0x0002C8FD
		public static Unlimited<uint> RecipientRateLimit { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0002E705 File Offset: 0x0002C905
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x0002E70C File Offset: 0x0002C90C
		public static Unlimited<uint> ForwardeeLimit { get; private set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0002E714 File Offset: 0x0002C914
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0002E71B File Offset: 0x0002C91B
		public static Unlimited<uint> DiscoveryMaxConcurrency { get; private set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0002E723 File Offset: 0x0002C923
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0002E72A File Offset: 0x0002C92A
		public static Unlimited<uint> DiscoveryMaxMailboxes { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002E732 File Offset: 0x0002C932
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0002E739 File Offset: 0x0002C939
		public static Unlimited<uint> DiscoveryMaxKeywords { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0002E741 File Offset: 0x0002C941
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x0002E748 File Offset: 0x0002C948
		public static Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes { get; private set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0002E750 File Offset: 0x0002C950
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0002E757 File Offset: 0x0002C957
		public static Unlimited<uint> DiscoveryMaxStatsSearchMailboxes { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0002E75F File Offset: 0x0002C95F
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x0002E766 File Offset: 0x0002C966
		public static Unlimited<uint> DiscoveryPreviewSearchResultsPageSize { get; private set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0002E76E File Offset: 0x0002C96E
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0002E775 File Offset: 0x0002C975
		public static Unlimited<uint> DiscoveryMaxKeywordsPerPage { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0002E77D File Offset: 0x0002C97D
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x0002E784 File Offset: 0x0002C984
		public static Unlimited<uint> DiscoveryMaxRefinerResults { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0002E78C File Offset: 0x0002C98C
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x0002E793 File Offset: 0x0002C993
		public static Unlimited<uint> DiscoveryMaxSearchQueueDepth { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0002E79B File Offset: 0x0002C99B
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0002E7A2 File Offset: 0x0002C9A2
		public static Unlimited<uint> DiscoverySearchTimeoutPeriod { get; private set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0002E7AA File Offset: 0x0002C9AA
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x0002E7B1 File Offset: 0x0002C9B1
		public static Unlimited<uint> ComplianceMaxExpansionDGRecipients { get; private set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0002E7B9 File Offset: 0x0002C9B9
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
		public static Unlimited<uint> ComplianceMaxExpansionNestedDGs { get; private set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0002E7C8 File Offset: 0x0002C9C8
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0002E7CF File Offset: 0x0002C9CF
		public static Unlimited<uint> PushNotificationMaxConcurrency { get; private set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0002E7D7 File Offset: 0x0002C9D7
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0002E7DE File Offset: 0x0002C9DE
		public static Unlimited<uint> PushNotificationMaxBurst { get; private set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002E7E6 File Offset: 0x0002C9E6
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x0002E7ED File Offset: 0x0002C9ED
		public static Unlimited<uint> PushNotificationRechargeRate { get; private set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x0002E7F5 File Offset: 0x0002C9F5
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		public static Unlimited<uint> PushNotificationCutoffBalance { get; private set; }

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002E804 File Offset: 0x0002CA04
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0002E80B File Offset: 0x0002CA0B
		public static Unlimited<uint> PushNotificationMaxBurstPerDevice { get; private set; }

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002E813 File Offset: 0x0002CA13
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0002E81A File Offset: 0x0002CA1A
		public static Unlimited<uint> PushNotificationRechargeRatePerDevice { get; private set; }

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0002E822 File Offset: 0x0002CA22
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0002E829 File Offset: 0x0002CA29
		public static Unlimited<uint> PushNotificationSamplingPeriodPerDevice { get; private set; }

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x0002E831 File Offset: 0x0002CA31
		public static Unlimited<uint> ServiceAccountEwsMaxConcurrency
		{
			get
			{
				return 27U;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0002E83A File Offset: 0x0002CA3A
		public static Unlimited<uint> ServiceAccountImapMaxConcurrency
		{
			get
			{
				return 50U;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0002E843 File Offset: 0x0002CA43
		public static Unlimited<uint> ServiceAccountOutlookServiceMaxConcurrency
		{
			get
			{
				return 27U;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0002E84C File Offset: 0x0002CA4C
		public static Unlimited<uint> ServiceAccountRcaMaxConcurrency
		{
			get
			{
				return 60U;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0002E855 File Offset: 0x0002CA55
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0002E85C File Offset: 0x0002CA5C
		public static Unlimited<uint> EncryptionSenderMaxConcurrency { get; private set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0002E864 File Offset: 0x0002CA64
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x0002E86B File Offset: 0x0002CA6B
		public static Unlimited<uint> EncryptionSenderMaxBurst { get; private set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0002E873 File Offset: 0x0002CA73
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x0002E87A File Offset: 0x0002CA7A
		public static Unlimited<uint> EncryptionSenderRechargeRate { get; private set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0002E882 File Offset: 0x0002CA82
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x0002E889 File Offset: 0x0002CA89
		public static Unlimited<uint> EncryptionSenderCutoffBalance { get; private set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0002E891 File Offset: 0x0002CA91
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x0002E898 File Offset: 0x0002CA98
		public static Unlimited<uint> EncryptionRecipientMaxConcurrency { get; private set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0002E8A7 File Offset: 0x0002CAA7
		public static Unlimited<uint> EncryptionRecipientMaxBurst { get; private set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0002E8AF File Offset: 0x0002CAAF
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0002E8B6 File Offset: 0x0002CAB6
		public static Unlimited<uint> EncryptionRecipientRechargeRate { get; private set; }

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x0002E8BE File Offset: 0x0002CABE
		// (set) Token: 0x06000EEC RID: 3820 RVA: 0x0002E8C5 File Offset: 0x0002CAC5
		public static Unlimited<uint> EncryptionRecipientCutoffBalance { get; private set; }
	}
}
