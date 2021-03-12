using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x020000B2 RID: 178
	public static class MessagingPoliciesEventLogConstants
	{
		// Token: 0x040002E6 RID: 742
		public const string EventSource = "MSExchange Messaging Policies";

		// Token: 0x040002E7 RID: 743
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingRulesLoaded = new ExEventLog.EventTuple(1074004968U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002E8 RID: 744
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingRulesConfigurationLoadError = new ExEventLog.EventTuple(3221488617U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002E9 RID: 745
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingDroppingJournalReportToDCMailbox = new ExEventLog.EventTuple(2147746794U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EA RID: 746
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnJournalingPermanentError = new ExEventLog.EventTuple(3221488619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EB RID: 747
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnJournalingTransientError = new ExEventLog.EventTuple(2147746796U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EC RID: 748
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalFilterTransportSettingLoadError = new ExEventLog.EventTuple(3221488621U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002ED RID: 749
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingTargetDGEmptyError = new ExEventLog.EventTuple(3221488622U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EE RID: 750
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingTargetDGNotFoundError = new ExEventLog.EventTuple(3221488623U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EF RID: 751
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingLITenantNotFoundInResourceForestError = new ExEventLog.EventTuple(3221488625U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F0 RID: 752
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingLogConfigureError = new ExEventLog.EventTuple(3221488626U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F1 RID: 753
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_JournalingLogException = new ExEventLog.EventTuple(3221488627U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F2 RID: 754
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AttachFilterConfigLoaded = new ExEventLog.EventTuple(1074005968U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F3 RID: 755
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AttachFilterConfigCorrupt = new ExEventLog.EventTuple(3221489617U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F4 RID: 756
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AddressRewriteConfigLoaded = new ExEventLog.EventTuple(1074006968U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F5 RID: 757
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AddressRewriteConfigCorrupt = new ExEventLog.EventTuple(3221490617U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F6 RID: 758
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleActionLogEvent = new ExEventLog.EventTuple(1074007968U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002F7 RID: 759
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleCollectionLoaded = new ExEventLog.EventTuple(1074007970U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002F8 RID: 760
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleCollectionLoadingTransientError = new ExEventLog.EventTuple(2147749795U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002F9 RID: 761
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleCollectionLoadingError = new ExEventLog.EventTuple(3221491620U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FA RID: 762
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleLoadTimeExceededThreshold = new ExEventLog.EventTuple(2147749797U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FB RID: 763
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleExecutionTimeExceededThreshold = new ExEventLog.EventTuple(2147749798U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FC RID: 764
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleEvaluationFailure = new ExEventLog.EventTuple(3221491623U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FD RID: 765
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleEvaluationFilteringServiceFailure = new ExEventLog.EventTuple(3221491624U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FE RID: 766
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleEvaluationIgnoredFailure = new ExEventLog.EventTuple(3221491625U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002FF RID: 767
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleEvaluationIgnoredFilteringServiceFailure = new ExEventLog.EventTuple(3221491626U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000300 RID: 768
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RuleDetectedExcessiveBifurcation = new ExEventLog.EventTuple(3221491627U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000301 RID: 769
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApplyPolicyOperationNDRedDueToEncryptionOff = new ExEventLog.EventTuple(3221231473U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000302 RID: 770
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ApplyPolicyOperationFailNDR = new ExEventLog.EventTuple(3221231475U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000303 RID: 771
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ApplyPolicyOperationFailDefer = new ExEventLog.EventTuple(2147489653U, 6, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000304 RID: 772
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SkippedDecryptionForMaliciousTargetAddress = new ExEventLog.EventTuple(2147752793U, 7, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000305 RID: 773
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLoadDrmMessage = new ExEventLog.EventTuple(3221232480U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000306 RID: 774
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsGeneralFailure = new ExEventLog.EventTuple(3221233472U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000307 RID: 775
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsSpecialFailure = new ExEventLog.EventTuple(3221233473U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000308 RID: 776
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsConnectFailure = new ExEventLog.EventTuple(3221233474U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000309 RID: 777
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsTrustFailure = new ExEventLog.EventTuple(3221233475U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030A RID: 778
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_Rms401Failure = new ExEventLog.EventTuple(3221233476U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030B RID: 779
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_Rms403Failure = new ExEventLog.EventTuple(3221233477U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030C RID: 780
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_Rms404Failure = new ExEventLog.EventTuple(3221233478U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030D RID: 781
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RmsNoRightFailure = new ExEventLog.EventTuple(3221233479U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030E RID: 782
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToLoadIRMConfiguration = new ExEventLog.EventTuple(3221233672U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400030F RID: 783
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportDecryptionSucceeded = new ExEventLog.EventTuple(1074012169U, 9, EventLogEntryType.Information, ExEventLog.EventLevel.High, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000310 RID: 784
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TransportDecryptionTransientException = new ExEventLog.EventTuple(3221495818U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000311 RID: 785
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportDecryptionPermanentException = new ExEventLog.EventTuple(3221495819U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000312 RID: 786
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportReEncryptionFailed = new ExEventLog.EventTuple(3221495821U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000313 RID: 787
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_TransportReEncryptionFailedInvalidPLOrUL = new ExEventLog.EventTuple(3221495822U, 9, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000314 RID: 788
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_RedirectionAgentCreated = new ExEventLog.EventTuple(1073750031U, 10, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000315 RID: 789
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidForwardingSmtpAddressError = new ExEventLog.EventTuple(3221233680U, 10, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000316 RID: 790
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_TemplateDoesNotExist = new ExEventLog.EventTuple(3221236472U, 11, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x020000B3 RID: 179
		private enum Category : short
		{
			// Token: 0x04000318 RID: 792
			Journaling = 1,
			// Token: 0x04000319 RID: 793
			AttachFilter,
			// Token: 0x0400031A RID: 794
			AddressRewrite,
			// Token: 0x0400031B RID: 795
			Rules,
			// Token: 0x0400031C RID: 796
			Prelicensing,
			// Token: 0x0400031D RID: 797
			PolicyApplication,
			// Token: 0x0400031E RID: 798
			JournalReportDecryption,
			// Token: 0x0400031F RID: 799
			RightsManagement,
			// Token: 0x04000320 RID: 800
			TransportDecryption,
			// Token: 0x04000321 RID: 801
			RedirectionAgent,
			// Token: 0x04000322 RID: 802
			Information_Rights_Management
		}

		// Token: 0x020000B4 RID: 180
		internal enum Message : uint
		{
			// Token: 0x04000324 RID: 804
			JournalingRulesLoaded = 1074004968U,
			// Token: 0x04000325 RID: 805
			JournalingRulesConfigurationLoadError = 3221488617U,
			// Token: 0x04000326 RID: 806
			JournalingDroppingJournalReportToDCMailbox = 2147746794U,
			// Token: 0x04000327 RID: 807
			UnJournalingPermanentError = 3221488619U,
			// Token: 0x04000328 RID: 808
			UnJournalingTransientError = 2147746796U,
			// Token: 0x04000329 RID: 809
			JournalFilterTransportSettingLoadError = 3221488621U,
			// Token: 0x0400032A RID: 810
			JournalingTargetDGEmptyError,
			// Token: 0x0400032B RID: 811
			JournalingTargetDGNotFoundError,
			// Token: 0x0400032C RID: 812
			JournalingLITenantNotFoundInResourceForestError = 3221488625U,
			// Token: 0x0400032D RID: 813
			JournalingLogConfigureError,
			// Token: 0x0400032E RID: 814
			JournalingLogException,
			// Token: 0x0400032F RID: 815
			AttachFilterConfigLoaded = 1074005968U,
			// Token: 0x04000330 RID: 816
			AttachFilterConfigCorrupt = 3221489617U,
			// Token: 0x04000331 RID: 817
			AddressRewriteConfigLoaded = 1074006968U,
			// Token: 0x04000332 RID: 818
			AddressRewriteConfigCorrupt = 3221490617U,
			// Token: 0x04000333 RID: 819
			RuleActionLogEvent = 1074007968U,
			// Token: 0x04000334 RID: 820
			RuleCollectionLoaded = 1074007970U,
			// Token: 0x04000335 RID: 821
			RuleCollectionLoadingTransientError = 2147749795U,
			// Token: 0x04000336 RID: 822
			RuleCollectionLoadingError = 3221491620U,
			// Token: 0x04000337 RID: 823
			RuleLoadTimeExceededThreshold = 2147749797U,
			// Token: 0x04000338 RID: 824
			RuleExecutionTimeExceededThreshold,
			// Token: 0x04000339 RID: 825
			RuleEvaluationFailure = 3221491623U,
			// Token: 0x0400033A RID: 826
			RuleEvaluationFilteringServiceFailure,
			// Token: 0x0400033B RID: 827
			RuleEvaluationIgnoredFailure,
			// Token: 0x0400033C RID: 828
			RuleEvaluationIgnoredFilteringServiceFailure,
			// Token: 0x0400033D RID: 829
			RuleDetectedExcessiveBifurcation,
			// Token: 0x0400033E RID: 830
			ApplyPolicyOperationNDRedDueToEncryptionOff = 3221231473U,
			// Token: 0x0400033F RID: 831
			ApplyPolicyOperationFailNDR = 3221231475U,
			// Token: 0x04000340 RID: 832
			ApplyPolicyOperationFailDefer = 2147489653U,
			// Token: 0x04000341 RID: 833
			SkippedDecryptionForMaliciousTargetAddress = 2147752793U,
			// Token: 0x04000342 RID: 834
			FailedToLoadDrmMessage = 3221232480U,
			// Token: 0x04000343 RID: 835
			RmsGeneralFailure = 3221233472U,
			// Token: 0x04000344 RID: 836
			RmsSpecialFailure,
			// Token: 0x04000345 RID: 837
			RmsConnectFailure,
			// Token: 0x04000346 RID: 838
			RmsTrustFailure,
			// Token: 0x04000347 RID: 839
			Rms401Failure,
			// Token: 0x04000348 RID: 840
			Rms403Failure,
			// Token: 0x04000349 RID: 841
			Rms404Failure,
			// Token: 0x0400034A RID: 842
			RmsNoRightFailure,
			// Token: 0x0400034B RID: 843
			FailedToLoadIRMConfiguration = 3221233672U,
			// Token: 0x0400034C RID: 844
			TransportDecryptionSucceeded = 1074012169U,
			// Token: 0x0400034D RID: 845
			TransportDecryptionTransientException = 3221495818U,
			// Token: 0x0400034E RID: 846
			TransportDecryptionPermanentException,
			// Token: 0x0400034F RID: 847
			TransportReEncryptionFailed = 3221495821U,
			// Token: 0x04000350 RID: 848
			TransportReEncryptionFailedInvalidPLOrUL,
			// Token: 0x04000351 RID: 849
			RedirectionAgentCreated = 1073750031U,
			// Token: 0x04000352 RID: 850
			InvalidForwardingSmtpAddressError = 3221233680U,
			// Token: 0x04000353 RID: 851
			TemplateDoesNotExist = 3221236472U
		}
	}
}
