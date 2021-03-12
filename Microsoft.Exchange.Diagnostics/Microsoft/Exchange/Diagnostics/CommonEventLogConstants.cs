using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000208 RID: 520
	public static class CommonEventLogConstants
	{
		// Token: 0x04000AC8 RID: 2760
		public const string EventSource = "MSExchange Common";

		// Token: 0x04000AC9 RID: 2761
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExchangeCrash = new ExEventLog.EventTuple(1074008967U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACA RID: 2762
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_WatsonReportError = new ExEventLog.EventTuple(2147750790U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACB RID: 2763
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_PrivilegeRemovalFailure = new ExEventLog.EventTuple(3221488195U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACC RID: 2764
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NoAdapterDnsServerList = new ExEventLog.EventTuple(3221487821U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000ACD RID: 2765
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_DnsQueryReturnedPartialResults = new ExEventLog.EventTuple(2147745998U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Expert, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ACE RID: 2766
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_NoMachineDnsServerList = new ExEventLog.EventTuple(3221487823U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ACF RID: 2767
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DnsServerConfigurationFetchFailed = new ExEventLog.EventTuple(3221487824U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD0 RID: 2768
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PerfCounterProblem = new ExEventLog.EventTuple(3221487722U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD1 RID: 2769
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TraceConfigFileAccessDeniedProblem = new ExEventLog.EventTuple(3221488022U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD2 RID: 2770
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskProblem = new ExEventLog.EventTuple(3221488122U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD3 RID: 2771
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DiskHealthy = new ExEventLog.EventTuple(1074004473U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD4 RID: 2772
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_LatencyDetection = new ExEventLog.EventTuple(2147746299U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD5 RID: 2773
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeleteOldLog = new ExEventLog.EventTuple(1074009969U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD6 RID: 2774
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeleteLogDueToQuota = new ExEventLog.EventTuple(1074009970U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD7 RID: 2775
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToCreateDirectory = new ExEventLog.EventTuple(3221493619U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AD8 RID: 2776
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToAppendLog = new ExEventLog.EventTuple(3221493620U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AD9 RID: 2777
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PendingLoggingRequestsReachedMaximum = new ExEventLog.EventTuple(2147751797U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADA RID: 2778
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SslCertificateTrustError = new ExEventLog.EventTuple(3221230475U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADB RID: 2779
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsAccountCertificateTrustError = new ExEventLog.EventTuple(3221230476U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADC RID: 2780
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerVersionError = new ExEventLog.EventTuple(3221230477U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADD RID: 2781
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerAuthenticationError = new ExEventLog.EventTuple(3221230478U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADE RID: 2782
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerAuthorizationError = new ExEventLog.EventTuple(3221230479U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000ADF RID: 2783
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerResourceNotFound = new ExEventLog.EventTuple(3221230480U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE0 RID: 2784
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerNameResolutionError = new ExEventLog.EventTuple(3221230481U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE1 RID: 2785
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerDecommissioned = new ExEventLog.EventTuple(3221230482U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE2 RID: 2786
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RightsManagementServerRedirected = new ExEventLog.EventTuple(3221230483U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE3 RID: 2787
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnknownTemplate = new ExEventLog.EventTuple(3221230485U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE4 RID: 2788
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_IssuanceLicenseTrustError = new ExEventLog.EventTuple(3221230486U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE5 RID: 2789
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorOpeningLanguagePackRegistryKey = new ExEventLog.EventTuple(3221226472U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE6 RID: 2790
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorScanningLanguagePackFolders = new ExEventLog.EventTuple(3221226572U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE7 RID: 2791
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidCultureIdentifier = new ExEventLog.EventTuple(2147484848U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE8 RID: 2792
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederationTrustOrganizationCertificateNotFound = new ExEventLog.EventTuple(3221225873U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AE9 RID: 2793
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederationTrustOrganizationCertificateNoPrivateKey = new ExEventLog.EventTuple(3221225874U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AEA RID: 2794
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FederationTrustCertificateExpired = new ExEventLog.EventTuple(3221225875U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AEB RID: 2795
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TenantMonitoringWorkflowError = new ExEventLog.EventTuple(3221231472U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AEC RID: 2796
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TenantMonitoringTestEvent = new ExEventLog.EventTuple(3221231473U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AED RID: 2797
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationLoopTestRedEvent = new ExEventLog.EventTuple(1073748825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AEE RID: 2798
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_EscalationLoopTestYellowEvent = new ExEventLog.EventTuple(1073748826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AEF RID: 2799
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_CannotContactMserveCacheService = new ExEventLog.EventTuple(3221495617U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AF0 RID: 2800
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_MserveCacheServiceModeChanged = new ExEventLog.EventTuple(1074011970U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AF1 RID: 2801
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_WcfClientConfigError = new ExEventLog.EventTuple(3221233475U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AF2 RID: 2802
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivityRollupReportWithUsageEvent = new ExEventLog.EventTuple(1073748827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF3 RID: 2803
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ActivityRollupReportWithNoUsageEvent = new ExEventLog.EventTuple(1073748828U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF4 RID: 2804
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ErrorOpeningUMLanguagePackRegistryKey = new ExEventLog.EventTuple(3221232477U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000AF5 RID: 2805
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RulesBasedHttpModule_InvalidRuleConfigured = new ExEventLog.EventTuple(3221233472U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF6 RID: 2806
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RulesBasedHttpModule_UserAccessDenied = new ExEventLog.EventTuple(3221233473U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF7 RID: 2807
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeploymentF5ConnectToFloaterErrorEvent = new ExEventLog.EventTuple(3221234472U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF8 RID: 2808
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeploymentF5ConnectToPartnerErrorEvent = new ExEventLog.EventTuple(3221234473U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AF9 RID: 2809
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_DeploymentF5PartnerErrorEvent = new ExEventLog.EventTuple(3221234474U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AFA RID: 2810
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_StreamInsightsDataUploaderGetValueFailed = new ExEventLog.EventTuple(2147492651U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000AFB RID: 2811
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_OutOfMemory = new ExEventLog.EventTuple(3221245473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AFC RID: 2812
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_UnhandledException = new ExEventLog.EventTuple(3221225473U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AFD RID: 2813
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_NonCrashingException = new ExEventLog.EventTuple(3221225474U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000AFE RID: 2814
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AppSettingLoadException = new ExEventLog.EventTuple(3221225475U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x02000209 RID: 521
		private enum Category : short
		{
			// Token: 0x04000B00 RID: 2816
			General = 1,
			// Token: 0x04000B01 RID: 2817
			Configuration,
			// Token: 0x04000B02 RID: 2818
			Logging,
			// Token: 0x04000B03 RID: 2819
			RightsManagement,
			// Token: 0x04000B04 RID: 2820
			LanguagePackInfo,
			// Token: 0x04000B05 RID: 2821
			TenantMonitoring,
			// Token: 0x04000B06 RID: 2822
			Deployment
		}

		// Token: 0x0200020A RID: 522
		internal enum Message : uint
		{
			// Token: 0x04000B08 RID: 2824
			ExchangeCrash = 1074008967U,
			// Token: 0x04000B09 RID: 2825
			WatsonReportError = 2147750790U,
			// Token: 0x04000B0A RID: 2826
			PrivilegeRemovalFailure = 3221488195U,
			// Token: 0x04000B0B RID: 2827
			NoAdapterDnsServerList = 3221487821U,
			// Token: 0x04000B0C RID: 2828
			DnsQueryReturnedPartialResults = 2147745998U,
			// Token: 0x04000B0D RID: 2829
			NoMachineDnsServerList = 3221487823U,
			// Token: 0x04000B0E RID: 2830
			DnsServerConfigurationFetchFailed,
			// Token: 0x04000B0F RID: 2831
			PerfCounterProblem = 3221487722U,
			// Token: 0x04000B10 RID: 2832
			TraceConfigFileAccessDeniedProblem = 3221488022U,
			// Token: 0x04000B11 RID: 2833
			DiskProblem = 3221488122U,
			// Token: 0x04000B12 RID: 2834
			DiskHealthy = 1074004473U,
			// Token: 0x04000B13 RID: 2835
			LatencyDetection = 2147746299U,
			// Token: 0x04000B14 RID: 2836
			DeleteOldLog = 1074009969U,
			// Token: 0x04000B15 RID: 2837
			DeleteLogDueToQuota,
			// Token: 0x04000B16 RID: 2838
			FailedToCreateDirectory = 3221493619U,
			// Token: 0x04000B17 RID: 2839
			FailedToAppendLog,
			// Token: 0x04000B18 RID: 2840
			PendingLoggingRequestsReachedMaximum = 2147751797U,
			// Token: 0x04000B19 RID: 2841
			SslCertificateTrustError = 3221230475U,
			// Token: 0x04000B1A RID: 2842
			RightsAccountCertificateTrustError,
			// Token: 0x04000B1B RID: 2843
			RightsManagementServerVersionError,
			// Token: 0x04000B1C RID: 2844
			RightsManagementServerAuthenticationError,
			// Token: 0x04000B1D RID: 2845
			RightsManagementServerAuthorizationError,
			// Token: 0x04000B1E RID: 2846
			RightsManagementServerResourceNotFound,
			// Token: 0x04000B1F RID: 2847
			RightsManagementServerNameResolutionError,
			// Token: 0x04000B20 RID: 2848
			RightsManagementServerDecommissioned,
			// Token: 0x04000B21 RID: 2849
			RightsManagementServerRedirected,
			// Token: 0x04000B22 RID: 2850
			UnknownTemplate = 3221230485U,
			// Token: 0x04000B23 RID: 2851
			IssuanceLicenseTrustError,
			// Token: 0x04000B24 RID: 2852
			ErrorOpeningLanguagePackRegistryKey = 3221226472U,
			// Token: 0x04000B25 RID: 2853
			ErrorScanningLanguagePackFolders = 3221226572U,
			// Token: 0x04000B26 RID: 2854
			InvalidCultureIdentifier = 2147484848U,
			// Token: 0x04000B27 RID: 2855
			FederationTrustOrganizationCertificateNotFound = 3221225873U,
			// Token: 0x04000B28 RID: 2856
			FederationTrustOrganizationCertificateNoPrivateKey,
			// Token: 0x04000B29 RID: 2857
			FederationTrustCertificateExpired,
			// Token: 0x04000B2A RID: 2858
			TenantMonitoringWorkflowError = 3221231472U,
			// Token: 0x04000B2B RID: 2859
			TenantMonitoringTestEvent,
			// Token: 0x04000B2C RID: 2860
			EscalationLoopTestRedEvent = 1073748825U,
			// Token: 0x04000B2D RID: 2861
			EscalationLoopTestYellowEvent,
			// Token: 0x04000B2E RID: 2862
			CannotContactMserveCacheService = 3221495617U,
			// Token: 0x04000B2F RID: 2863
			MserveCacheServiceModeChanged = 1074011970U,
			// Token: 0x04000B30 RID: 2864
			WcfClientConfigError = 3221233475U,
			// Token: 0x04000B31 RID: 2865
			ActivityRollupReportWithUsageEvent = 1073748827U,
			// Token: 0x04000B32 RID: 2866
			ActivityRollupReportWithNoUsageEvent,
			// Token: 0x04000B33 RID: 2867
			ErrorOpeningUMLanguagePackRegistryKey = 3221232477U,
			// Token: 0x04000B34 RID: 2868
			RulesBasedHttpModule_InvalidRuleConfigured = 3221233472U,
			// Token: 0x04000B35 RID: 2869
			RulesBasedHttpModule_UserAccessDenied,
			// Token: 0x04000B36 RID: 2870
			DeploymentF5ConnectToFloaterErrorEvent = 3221234472U,
			// Token: 0x04000B37 RID: 2871
			DeploymentF5ConnectToPartnerErrorEvent,
			// Token: 0x04000B38 RID: 2872
			DeploymentF5PartnerErrorEvent,
			// Token: 0x04000B39 RID: 2873
			StreamInsightsDataUploaderGetValueFailed = 2147492651U,
			// Token: 0x04000B3A RID: 2874
			OutOfMemory = 3221245473U,
			// Token: 0x04000B3B RID: 2875
			UnhandledException = 3221225473U,
			// Token: 0x04000B3C RID: 2876
			NonCrashingException,
			// Token: 0x04000B3D RID: 2877
			AppSettingLoadException
		}
	}
}
