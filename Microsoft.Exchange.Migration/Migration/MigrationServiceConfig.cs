using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000021 RID: 33
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationServiceConfig : ConfigBase<MigrationServiceConfigSchema>
	{
		// Token: 0x04000040 RID: 64
		internal const string IsServiceletEnabledKey = "SyncMigrationIsEnabled";

		// Token: 0x04000041 RID: 65
		internal const string PollingBatchSizeKey = "SyncMigrationPollingBatchSize";

		// Token: 0x04000042 RID: 66
		internal const string InitialSyncPollingIntervalKey = "SyncMigrationInitialSyncStartPollingTimeout";

		// Token: 0x04000043 RID: 67
		internal const string IncrementalSyncPollingIntervalKey = "SyncMigrationPollingTimeout";

		// Token: 0x04000044 RID: 68
		internal const string ReportIntervalKey = "ReportInterval";

		// Token: 0x04000045 RID: 69
		internal const string ReportMaxAttachmentSize = "ReportMaxAttachmentSize";

		// Token: 0x04000046 RID: 70
		internal const string ReportInitial = "ReportInitial";

		// Token: 0x04000047 RID: 71
		internal const string InitialSyncSubscriptionTimeoutKey = "SyncMigrationInitialSyncTimeOutForFailingSubscriptions";

		// Token: 0x04000048 RID: 72
		internal const string MRSInitialSyncSubscriptionTimeoutKey = "MRSInitialSyncSubscriptionTimeout";

		// Token: 0x04000049 RID: 73
		internal const string IncrementalSyncSubscriptionTimeoutKey = "SyncMigrationTimeOutForFailingSubscriptions";

		// Token: 0x0400004A RID: 74
		internal const string LazyCountRescanPollingIntervalKey = "SyncMigrationLazyCountRescanPollingTimeout";

		// Token: 0x0400004B RID: 75
		internal const string CancellationBatchSizeKey = "SyncMigrationCancellationBatchSize";

		// Token: 0x0400004C RID: 76
		internal const string TransitionBatchSizeKey = "TransitionBatchSize";

		// Token: 0x0400004D RID: 77
		internal const string ProcessingBatchSizeKey = "ProcessingBatchSize";

		// Token: 0x0400004E RID: 78
		internal const string ProcessingSessionSizeKey = "ProcessingSessionSize";

		// Token: 0x0400004F RID: 79
		internal const string ProcessorIdleRunDelayKey = "SyncMigrationProcessorIdleRunDelay";

		// Token: 0x04000050 RID: 80
		internal const string ProcessorActiveRunDelayKey = "SyncMigrationProcessorActiveRunDelay";

		// Token: 0x04000051 RID: 81
		internal const string ProcessorTransientErrorRunDelayKey = "SyncMigrationProcessorTransientErrorRunDelay";

		// Token: 0x04000052 RID: 82
		internal const string ProcessorMaxWaitingJobDelay = "SyncMigrationProcessorMaxWaitingJobDelay";

		// Token: 0x04000053 RID: 83
		internal const string ProcessorAverageWaitingJobDelay = "SyncMigrationProcessorAverageWaitingJobDelay";

		// Token: 0x04000054 RID: 84
		internal const string ProcessorSyncedJobItemDelay = "SyncMigrationProcessorSyncedJobItemDelay";

		// Token: 0x04000055 RID: 85
		internal const string ProcessorMinWaitingJobDelay = "SyncMigrationProcessorMinWaitingJobDelay";

		// Token: 0x04000056 RID: 86
		internal const string MigrationServiceRpcSkeletonMaxThreadsKey = "SyncMigrationServiceRpcSkeletonMaxThreads";

		// Token: 0x04000057 RID: 87
		internal const string MigrationNotificationRpcSkeletonMaxThreadsKey = "SyncMigrationNotificationRpcSkeletonMaxThreads";

		// Token: 0x04000058 RID: 88
		internal const string ProvisioningMaxNumThreads = "ProvisioningMaxNumThreads";

		// Token: 0x04000059 RID: 89
		internal const string IsMigrationSourceMailboxLegacyExchangeDNStampingEnabledKey = "MigrationSourceMailboxLegacyExchangeDNStampingEnabled";

		// Token: 0x0400005A RID: 90
		internal const string MigrationDelayedSubscriptionThresholdKey = "MigrationDelayedSubscriptionThreshold";

		// Token: 0x0400005B RID: 91
		internal const string MaxConcurrentMigrationsKey = "MaxConcurrentMigrations";

		// Token: 0x0400005C RID: 92
		internal const string MigrationProxyRpcEndpointMaxConcurrentRpcCountKey = "MigrationProxyRpcEndpointMaxConcurrentRpcCount";

		// Token: 0x0400005D RID: 93
		internal const string MaxRowsToProcessInOnePassKey = "MaxRowsToProcessInOnePass";

		// Token: 0x0400005E RID: 94
		internal const string MaxTimeToProcessInOnePassKey = "MaxTimeToProcessInOnePass";

		// Token: 0x0400005F RID: 95
		internal const string MaxJobItemsToProcessForReportGeneration = "SyncMigrationMaxJobItemsToProcessForReportGeneration";

		// Token: 0x04000060 RID: 96
		internal const string MigrationUseDKMForEncryption = "MigrationUseDKMForEncryption";

		// Token: 0x04000061 RID: 97
		internal const string MaxItemsToProvisionInOnePassKey = "MaxItemsToProvisionInOnePass";

		// Token: 0x04000062 RID: 98
		internal const string MigrationSourceExchangeMailboxMaximumCountKey = "MigrationSourceExchangeMailboxMaximumCount";

		// Token: 0x04000063 RID: 99
		internal const string MigrationSourceExchangeRecipientMaximumCountKey = "MigrationSourceExchangeRecipientMaximumCount";

		// Token: 0x04000064 RID: 100
		internal const string MigrationSourceStagedExchangeCSVMailboxMaximumCountKey = "MigrationSourceStagedExchangeCSVMailboxMaximumCount";

		// Token: 0x04000065 RID: 101
		internal const string MigrationMaximumJobItemsPerBatch = "MigrationMaximumJobItemsPerBatch";

		// Token: 0x04000066 RID: 102
		internal const string MigrationPoisonedCountThresholdKey = "MigrationPoisonedCountThreshold";

		// Token: 0x04000067 RID: 103
		internal const string MigrationTransientErrorCountThresholdKey = "MigrationTransientErrorCountThreshold";

		// Token: 0x04000068 RID: 104
		internal const string MigrationTransientErrorIntervalThresholdKey = "MigrationTransientErrorIntervalThreshold";

		// Token: 0x04000069 RID: 105
		internal const string FailureRatioForAutoCancel = "SyncMigrationFailureRatioForAutoCancel";

		// Token: 0x0400006A RID: 106
		internal const string AbsoluteFailureCountForAutoCancel = "SyncMigrationAbsoluteFailureCountForAutoCancel";

		// Token: 0x0400006B RID: 107
		internal const string MinimumFailureCountForAutoCancel = "SyncMigrationMinimumFailureCountForAutoCancel";

		// Token: 0x0400006C RID: 108
		internal const string MaxNumberOfMailEnabledPublicFoldersToProcessInOnePassKey = "MaxNumberOfMailEnabledPublicFoldersToProcessInOnePass";

		// Token: 0x0400006D RID: 109
		internal const string IMAPSessionVersionKey = "IMAPSessionVersion";

		// Token: 0x0400006E RID: 110
		internal const string ExchangeSessionVersionKey = "ExchangeSessionVersion";

		// Token: 0x0400006F RID: 111
		internal const string BulkProvisioningSessionVersionKey = "BulkProvisioningSessionVersion";

		// Token: 0x04000070 RID: 112
		internal const string LocalMoveSessionVersionKey = "LocalMoveSessionVersion";

		// Token: 0x04000071 RID: 113
		internal const string RemoteMoveSessionVersionKey = "RemoteMoveSessionVersion";

		// Token: 0x04000072 RID: 114
		internal const string SessionCurrentVersionKey = "SessionCurrentVersion";

		// Token: 0x04000073 RID: 115
		internal const string SyncMigrationEnabledMigrationsTypesKey = "SyncMigrationEnabledMigrationTypes";

		// Token: 0x04000074 RID: 116
		internal const string MigrationSlowOperationThreshold = "MigrationSlowOperationThreshold";

		// Token: 0x04000075 RID: 117
		internal const string MigrationNspiPortKey = "MigrationSourceNspiHttpPort";

		// Token: 0x04000076 RID: 118
		internal const string MigrationNspiRfrPortKey = "MigrationSourceRfrHttpPort";

		// Token: 0x04000077 RID: 119
		internal const string MigrationGroupMembersBatchSizeKey = "MigrationGroupMembersBatchSize";

		// Token: 0x04000078 RID: 120
		internal const string MaximumNumberOfBatchesPerSessionKey = "MaximumNumberOfBatchesPerSession";

		// Token: 0x04000079 RID: 121
		internal const string MigrationReportingLoggingEnabledKey = "MigrationReportingLoggingEnabled";

		// Token: 0x0400007A RID: 122
		internal const string MigrationReportingLoggingFolderKey = "MigrationReportingLoggingFolder";

		// Token: 0x0400007B RID: 123
		internal const string MigrationReportingMaxLogAgeKey = "MigrationReportingMaxLogAge";

		// Token: 0x0400007C RID: 124
		internal const string MigrationReportingJobMaxDirSizeKey = "MigrationReportingJobMaxDirSize";

		// Token: 0x0400007D RID: 125
		internal const string MigrationReportingJobItemMaxDirSizeKey = "MigrationReportingJobItemMaxDirSize";

		// Token: 0x0400007E RID: 126
		internal const string MigrationReportingEndpointMaxDirSizeKey = "MigrationReportingEndpointMaxDirSizeKey";

		// Token: 0x0400007F RID: 127
		internal const string MigrationReportingJobMaxFileSizeKey = "MigrationReportingJobMaxFileSize";

		// Token: 0x04000080 RID: 128
		internal const string MigrationReportingJobItemMaxFileSizeKey = "MigrationReportingJobItemMaxFileSize";

		// Token: 0x04000081 RID: 129
		internal const string MigrationReportingEndpointMaxFileSizeKey = "MigrationReportingEndpointMaxFileSize";

		// Token: 0x04000082 RID: 130
		internal const string MigrationErrorTransitionThresholdKey = "MigrationErrorTransitionThreshold";

		// Token: 0x04000083 RID: 131
		internal const string MigrationUpgradeConstraintExpirationPeriod = "MigrationUpgradeConstraintExpirationPeriod";

		// Token: 0x04000084 RID: 132
		internal const string MigrationUpgradeConstraintEnforcementPeriod = "MigrationUpgradeConstraintEnforcementPeriod";

		// Token: 0x04000085 RID: 133
		internal const string MigrationSuspendedCacheEntryDelay = "SuspendedCacheEntryDelay";

		// Token: 0x04000086 RID: 134
		internal const string BlockedMigrationFeatures = "BlockedMigrationFeatures";

		// Token: 0x04000087 RID: 135
		internal const string PublishedMigrationFeatures = "PublishedMigrationFeatures";

		// Token: 0x04000088 RID: 136
		internal const string UseAsyncNotificationsKey = "MigrationAsyncNotificationEnabled";

		// Token: 0x04000089 RID: 137
		internal const string MigrationJobStoppedThresholdKey = "MigrationJobStoppedThreshold";

		// Token: 0x0400008A RID: 138
		internal const string MigrationJobInactiveThresholdKey = "MigrationJobInactiveThreshold";

		// Token: 0x0400008B RID: 139
		internal const string EndpointCountsRefreshThresholdKey = "EndpointCountsRefreshThreshold";

		// Token: 0x0400008C RID: 140
		internal const string CacheEntrySuspendedDurationKey = "CacheEntrySuspendedDuration";

		// Token: 0x0400008D RID: 141
		internal const string IssueCacheIsEnabledKey = "IssueCacheIsEnabled";

		// Token: 0x0400008E RID: 142
		internal const string IssueCacheScanFrequencyKey = "IssueCacheScanFrequency";

		// Token: 0x0400008F RID: 143
		internal const string IssueCacheItemLimitKey = "IssueCacheItemLimit";

		// Token: 0x04000090 RID: 144
		internal const string MigrationIncrementalSyncFailureThreshold = "MigrationIncrementalSyncFailureThreshold";

		// Token: 0x04000091 RID: 145
		internal const string MigrationPublicFolderCompletionFailureThreshold = "MigrationPublicFolderCompletionFailureThreshold";

		// Token: 0x04000092 RID: 146
		internal const string MaxReportItemsPerJob = "MaxReportItemsPerJob";

		// Token: 0x04000093 RID: 147
		internal const string SendGenericWatsonKey = "SendGenericWatson";
	}
}
