using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000491 RID: 1169
	internal interface IThrottlingPolicy
	{
		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060034E7 RID: 13543
		bool IsFallback { get; }

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060034E8 RID: 13544
		ThrottlingPolicyScopeType ThrottlingPolicyScope { get; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060034E9 RID: 13545
		bool IsServiceAccount { get; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060034EA RID: 13546
		bool IsUnthrottled { get; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x060034EB RID: 13547
		Unlimited<uint> AnonymousMaxConcurrency { get; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x060034EC RID: 13548
		Unlimited<uint> AnonymousMaxBurst { get; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x060034ED RID: 13549
		Unlimited<uint> AnonymousRechargeRate { get; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x060034EE RID: 13550
		Unlimited<uint> AnonymousCutoffBalance { get; }

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x060034EF RID: 13551
		Unlimited<uint> EasMaxConcurrency { get; }

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x060034F0 RID: 13552
		Unlimited<uint> EasMaxBurst { get; }

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x060034F1 RID: 13553
		Unlimited<uint> EasRechargeRate { get; }

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x060034F2 RID: 13554
		Unlimited<uint> EasCutoffBalance { get; }

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060034F3 RID: 13555
		Unlimited<uint> EasMaxDevices { get; }

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060034F4 RID: 13556
		Unlimited<uint> EasMaxDeviceDeletesPerMonth { get; }

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060034F5 RID: 13557
		Unlimited<uint> EasMaxInactivityForDeviceCleanup { get; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060034F6 RID: 13558
		Unlimited<uint> EwsMaxConcurrency { get; }

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060034F7 RID: 13559
		Unlimited<uint> EwsMaxBurst { get; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060034F8 RID: 13560
		Unlimited<uint> EwsRechargeRate { get; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060034F9 RID: 13561
		Unlimited<uint> EwsCutoffBalance { get; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x060034FA RID: 13562
		Unlimited<uint> EwsMaxSubscriptions { get; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x060034FB RID: 13563
		Unlimited<uint> ImapMaxConcurrency { get; }

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x060034FC RID: 13564
		Unlimited<uint> ImapMaxBurst { get; }

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x060034FD RID: 13565
		Unlimited<uint> ImapRechargeRate { get; }

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x060034FE RID: 13566
		Unlimited<uint> ImapCutoffBalance { get; }

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x060034FF RID: 13567
		Unlimited<uint> OutlookServiceMaxConcurrency { get; }

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06003500 RID: 13568
		Unlimited<uint> OutlookServiceMaxBurst { get; }

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06003501 RID: 13569
		Unlimited<uint> OutlookServiceRechargeRate { get; }

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06003502 RID: 13570
		Unlimited<uint> OutlookServiceCutoffBalance { get; }

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06003503 RID: 13571
		Unlimited<uint> OutlookServiceMaxSubscriptions { get; }

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06003504 RID: 13572
		Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice { get; }

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06003505 RID: 13573
		Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser { get; }

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06003506 RID: 13574
		Unlimited<uint> OwaMaxConcurrency { get; }

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06003507 RID: 13575
		Unlimited<uint> OwaMaxBurst { get; }

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06003508 RID: 13576
		Unlimited<uint> OwaRechargeRate { get; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06003509 RID: 13577
		Unlimited<uint> OwaCutoffBalance { get; }

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x0600350A RID: 13578
		Unlimited<uint> OwaVoiceMaxConcurrency { get; }

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x0600350B RID: 13579
		Unlimited<uint> OwaVoiceMaxBurst { get; }

		// Token: 0x17000FCF RID: 4047
		// (get) Token: 0x0600350C RID: 13580
		Unlimited<uint> OwaVoiceRechargeRate { get; }

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x0600350D RID: 13581
		Unlimited<uint> OwaVoiceCutoffBalance { get; }

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x0600350E RID: 13582
		Unlimited<uint> PopMaxConcurrency { get; }

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x0600350F RID: 13583
		Unlimited<uint> PopMaxBurst { get; }

		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06003510 RID: 13584
		Unlimited<uint> PopRechargeRate { get; }

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06003511 RID: 13585
		Unlimited<uint> PopCutoffBalance { get; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06003512 RID: 13586
		Unlimited<uint> RcaMaxConcurrency { get; }

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06003513 RID: 13587
		Unlimited<uint> RcaMaxBurst { get; }

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06003514 RID: 13588
		Unlimited<uint> RcaRechargeRate { get; }

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06003515 RID: 13589
		Unlimited<uint> RcaCutoffBalance { get; }

		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06003516 RID: 13590
		Unlimited<uint> CpaMaxConcurrency { get; }

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06003517 RID: 13591
		Unlimited<uint> CpaMaxBurst { get; }

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003518 RID: 13592
		Unlimited<uint> CpaRechargeRate { get; }

		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003519 RID: 13593
		Unlimited<uint> CpaCutoffBalance { get; }

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x0600351A RID: 13594
		Unlimited<uint> PowerShellMaxConcurrency { get; }

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x0600351B RID: 13595
		Unlimited<uint> PowerShellMaxBurst { get; }

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x0600351C RID: 13596
		Unlimited<uint> PowerShellRechargeRate { get; }

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x0600351D RID: 13597
		Unlimited<uint> PowerShellCutoffBalance { get; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x0600351E RID: 13598
		Unlimited<uint> PowerShellMaxTenantConcurrency { get; }

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x0600351F RID: 13599
		Unlimited<uint> PowerShellMaxOperations { get; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06003520 RID: 13600
		Unlimited<uint> PowerShellMaxCmdletsTimePeriod { get; }

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003521 RID: 13601
		Unlimited<uint> PowerShellMaxCmdletQueueDepth { get; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06003522 RID: 13602
		Unlimited<uint> ExchangeMaxCmdlets { get; }

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06003523 RID: 13603
		Unlimited<uint> PowerShellMaxDestructiveCmdlets { get; }

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06003524 RID: 13604
		Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod { get; }

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06003525 RID: 13605
		Unlimited<uint> PowerShellMaxCmdlets { get; }

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003526 RID: 13606
		Unlimited<uint> PowerShellMaxRunspaces { get; }

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003527 RID: 13607
		Unlimited<uint> PowerShellMaxTenantRunspaces { get; }

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003528 RID: 13608
		Unlimited<uint> PowerShellMaxRunspacesTimePeriod { get; }

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003529 RID: 13609
		Unlimited<uint> PswsMaxConcurrency { get; }

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x0600352A RID: 13610
		Unlimited<uint> PswsMaxRequest { get; }

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x0600352B RID: 13611
		Unlimited<uint> PswsMaxRequestTimePeriod { get; }

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600352C RID: 13612
		Unlimited<uint> MessageRateLimit { get; }

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600352D RID: 13613
		Unlimited<uint> RecipientRateLimit { get; }

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600352E RID: 13614
		Unlimited<uint> ForwardeeLimit { get; }

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600352F RID: 13615
		Unlimited<uint> DiscoveryMaxConcurrency { get; }

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003530 RID: 13616
		Unlimited<uint> DiscoveryMaxMailboxes { get; }

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003531 RID: 13617
		Unlimited<uint> DiscoveryMaxKeywords { get; }

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003532 RID: 13618
		Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes { get; }

		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x06003533 RID: 13619
		Unlimited<uint> DiscoveryMaxStatsSearchMailboxes { get; }

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06003534 RID: 13620
		Unlimited<uint> DiscoveryPreviewSearchResultsPageSize { get; }

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06003535 RID: 13621
		Unlimited<uint> DiscoveryMaxKeywordsPerPage { get; }

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06003536 RID: 13622
		Unlimited<uint> DiscoveryMaxRefinerResults { get; }

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003537 RID: 13623
		Unlimited<uint> DiscoveryMaxSearchQueueDepth { get; }

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003538 RID: 13624
		Unlimited<uint> DiscoverySearchTimeoutPeriod { get; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003539 RID: 13625
		Unlimited<uint> PushNotificationMaxConcurrency { get; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x0600353A RID: 13626
		Unlimited<uint> PushNotificationMaxBurst { get; }

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x0600353B RID: 13627
		Unlimited<uint> PushNotificationRechargeRate { get; }

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x0600353C RID: 13628
		Unlimited<uint> PushNotificationCutoffBalance { get; }

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x0600353D RID: 13629
		Unlimited<uint> PushNotificationMaxBurstPerDevice { get; }

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x0600353E RID: 13630
		Unlimited<uint> PushNotificationRechargeRatePerDevice { get; }

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600353F RID: 13631
		Unlimited<uint> PushNotificationSamplingPeriodPerDevice { get; }

		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06003540 RID: 13632
		Unlimited<uint> EncryptionSenderMaxConcurrency { get; }

		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003541 RID: 13633
		Unlimited<uint> EncryptionSenderMaxBurst { get; }

		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06003542 RID: 13634
		Unlimited<uint> EncryptionSenderRechargeRate { get; }

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003543 RID: 13635
		Unlimited<uint> EncryptionSenderCutoffBalance { get; }

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003544 RID: 13636
		Unlimited<uint> EncryptionRecipientMaxConcurrency { get; }

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003545 RID: 13637
		Unlimited<uint> EncryptionRecipientMaxBurst { get; }

		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003546 RID: 13638
		Unlimited<uint> EncryptionRecipientRechargeRate { get; }

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06003547 RID: 13639
		Unlimited<uint> EncryptionRecipientCutoffBalance { get; }

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06003548 RID: 13640
		Unlimited<uint> ComplianceMaxExpansionDGRecipients { get; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06003549 RID: 13641
		Unlimited<uint> ComplianceMaxExpansionNestedDGs { get; }

		// Token: 0x0600354A RID: 13642
		string GetIdentityString();

		// Token: 0x0600354B RID: 13643
		string GetShortIdentityString();
	}
}
