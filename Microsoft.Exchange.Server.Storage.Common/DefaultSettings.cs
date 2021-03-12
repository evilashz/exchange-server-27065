using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200002B RID: 43
	public static class DefaultSettings
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00007FB3 File Offset: 0x000061B3
		public static DefaultSettings.DefaultSettingsValues Get
		{
			get
			{
				return DefaultSettings.GetInstance();
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007FBC File Offset: 0x000061BC
		private static DefaultSettings.DefaultSettingsValues GetInstance()
		{
			if (DefaultSettings.isTestEnvironment.Value)
			{
				if (StoreEnvironment.IsPerformanceEnvironment)
				{
					return DefaultSettings.performance;
				}
				return DefaultSettings.test;
			}
			else
			{
				if (DefaultSettings.isDogfoodEnvironment.Value)
				{
					return DefaultSettings.dogfood;
				}
				if (DefaultSettings.isDatacenterEnvironment.Value)
				{
					if (DefaultSettings.isSdfEnvironment.Value)
					{
						return DefaultSettings.sdf;
					}
					return DefaultSettings.datacenter;
				}
				else
				{
					if (DefaultSettings.isDedicatedEnvironment.Value)
					{
						return DefaultSettings.dedicated;
					}
					return DefaultSettings.customer;
				}
			}
		}

		// Token: 0x04000427 RID: 1063
		private static DefaultSettings.DefaultSettingsValues test = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 3000,
			DestinationMailboxReservedCounterRangeGradient = 3,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = new UnlimitedItems(3000000L),
			DumpsterMessagesPerFolderCountWarningQuota = new UnlimitedItems(2750000L),
			DynamicSearchFolderPerScopeCountReceiveQuota = 100,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = new UnlimitedItems(10000L),
			FolderHierarchyChildrenCountWarningQuota = new UnlimitedItems(9000L),
			FolderHierarchyDepthReceiveQuota = new UnlimitedItems(300L),
			FolderHierarchyDepthWarningQuota = new UnlimitedItems(250L),
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(1.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromSeconds(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = new UnlimitedItems(1000000L),
			MailboxMessagesPerFolderCountWarningQuota = new UnlimitedItems(900000L),
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(60L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10000,
			Name = "Test",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromSeconds(15.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = true,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 10000,
			StoreQueryLimitTime = TimeSpan.FromMinutes(2.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 1048576L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 104857600,
			CheckpointDepthOnActive = 104857600,
			DatabaseCacheSizePercentage = 25,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(100.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(100.0),
			EnableTestCaseIdLookup = true,
			EnableReadFromPassive = true,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = true,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = true,
			ScheduledISIntegDetectOnly = "FolderACL,MissingSpecialFolders",
			ScheduledISIntegDetectAndFix = "MidsetDeleted,RuleMessageClass,CorruptJunkRule,SearchBacklinks,ImapId,RestrictionFolder,UniqueMidIndex",
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 64,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 1000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = true,
			EseLrukCorrInterval = TimeSpan.FromSeconds(30.0),
			EseLrukTimeout = TimeSpan.FromHours(1.0),
			UserInformationIsEnabled = true,
			EnableUnifiedMailbox = true,
			VersionStoreCleanupMaintenanceTaskSupported = true,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = true,
			UseDirectorySharedCache = true,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 100,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x04000428 RID: 1064
		private static DefaultSettings.DefaultSettingsValues performance = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 1000000,
			DestinationMailboxReservedCounterRangeGradient = 10,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(10.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			DumpsterMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			DynamicSearchFolderPerScopeCountReceiveQuota = 10000000,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyChildrenCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(30.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			MailboxMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "PerformanceTest",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = true,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 10000,
			StoreQueryLimitTime = TimeSpan.FromMinutes(2.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 500000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 53687091200L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 52428800,
			CheckpointDepthOnActive = 52428800,
			DatabaseCacheSizePercentage = 20,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(100.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(100.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = true,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = true,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = false,
			ScheduledISIntegDetectOnly = string.Empty,
			ScheduledISIntegDetectAndFix = string.Empty,
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 64,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 1000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = false,
			EseLrukCorrInterval = TimeSpan.FromSeconds(30.0),
			EseLrukTimeout = TimeSpan.FromHours(1.0),
			UserInformationIsEnabled = true,
			EnableUnifiedMailbox = false,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = true,
			UseDirectorySharedCache = true,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 100,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x04000429 RID: 1065
		private static DefaultSettings.DefaultSettingsValues dogfood = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 10000,
			DestinationMailboxReservedCounterRangeGradient = 1,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = new UnlimitedItems(3000000L),
			DumpsterMessagesPerFolderCountWarningQuota = new UnlimitedItems(2750000L),
			DynamicSearchFolderPerScopeCountReceiveQuota = 100,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = new UnlimitedItems(10000L),
			FolderHierarchyChildrenCountWarningQuota = new UnlimitedItems(9000L),
			FolderHierarchyDepthReceiveQuota = new UnlimitedItems(300L),
			FolderHierarchyDepthWarningQuota = new UnlimitedItems(250L),
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(1.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = new UnlimitedItems(1000000L),
			MailboxMessagesPerFolderCountWarningQuota = new UnlimitedItems(900000L),
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "Dogfood",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = true,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 1000,
			StoreQueryLimitTime = TimeSpan.FromSeconds(10.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 10737418240L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 104857600,
			CheckpointDepthOnActive = 104857600,
			DatabaseCacheSizePercentage = 25,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(400.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(400.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = false,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = true,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = true,
			ScheduledISIntegDetectOnly = "FolderACL,MissingSpecialFolders",
			ScheduledISIntegDetectAndFix = "MidsetDeleted,RuleMessageClass,CorruptJunkRule,SearchBacklinks,ImapId,RestrictionFolder,UniqueMidIndex",
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 1024,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 1000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = true,
			EseLrukCorrInterval = TimeSpan.FromMilliseconds(128.0),
			EseLrukTimeout = TimeSpan.FromSeconds(100.0),
			UserInformationIsEnabled = false,
			EnableUnifiedMailbox = false,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = true,
			UseDirectorySharedCache = true,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 100,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x0400042A RID: 1066
		private static DefaultSettings.DefaultSettingsValues sdf = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 1000000,
			DestinationMailboxReservedCounterRangeGradient = 10,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(10.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = new UnlimitedItems(3000000L),
			DumpsterMessagesPerFolderCountWarningQuota = new UnlimitedItems(2750000L),
			DynamicSearchFolderPerScopeCountReceiveQuota = 10000,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = new UnlimitedItems(10000L),
			FolderHierarchyChildrenCountWarningQuota = new UnlimitedItems(9000L),
			FolderHierarchyDepthReceiveQuota = new UnlimitedItems(300L),
			FolderHierarchyDepthWarningQuota = new UnlimitedItems(250L),
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(30.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = new UnlimitedItems(1000000L),
			MailboxMessagesPerFolderCountWarningQuota = new UnlimitedItems(900000L),
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "DatacenterDogfood",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = false,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 1000,
			StoreQueryLimitTime = TimeSpan.FromSeconds(10.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 10737418240L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 52428800,
			CheckpointDepthOnActive = 52428800,
			DatabaseCacheSizePercentage = 20,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(400.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(400.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = false,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = true,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = true,
			ScheduledISIntegDetectOnly = "FolderACL,MissingSpecialFolders",
			ScheduledISIntegDetectAndFix = "MidsetDeleted,RuleMessageClass,CorruptJunkRule,SearchBacklinks,ImapId,RestrictionFolder,UniqueMidIndex",
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 4096,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 1000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = false,
			EseLrukCorrInterval = TimeSpan.FromSeconds(30.0),
			EseLrukTimeout = TimeSpan.FromHours(1.0),
			UserInformationIsEnabled = true,
			EnableUnifiedMailbox = false,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = true,
			UseDirectorySharedCache = true,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 1000,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x0400042B RID: 1067
		private static DefaultSettings.DefaultSettingsValues datacenter = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 1000000,
			DestinationMailboxReservedCounterRangeGradient = 10,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(10.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = new UnlimitedItems(3000000L),
			DumpsterMessagesPerFolderCountWarningQuota = new UnlimitedItems(2750000L),
			DynamicSearchFolderPerScopeCountReceiveQuota = 10000,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = new UnlimitedItems(10000L),
			FolderHierarchyChildrenCountWarningQuota = new UnlimitedItems(9000L),
			FolderHierarchyDepthReceiveQuota = new UnlimitedItems(300L),
			FolderHierarchyDepthWarningQuota = new UnlimitedItems(250L),
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(30.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = new UnlimitedItems(1000000L),
			MailboxMessagesPerFolderCountWarningQuota = new UnlimitedItems(900000L),
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "Datacenter",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = false,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 1000,
			StoreQueryLimitTime = TimeSpan.FromSeconds(10.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 10737418240L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 52428800,
			CheckpointDepthOnActive = 52428800,
			DatabaseCacheSizePercentage = 20,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(400.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(400.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = false,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = true,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = true,
			ScheduledISIntegDetectOnly = string.Empty,
			ScheduledISIntegDetectAndFix = "MidsetDeleted,RuleMessageClass,CorruptJunkRule",
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 4194304,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 1000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = false,
			EseLrukCorrInterval = TimeSpan.FromSeconds(30.0),
			EseLrukTimeout = TimeSpan.FromHours(1.0),
			UserInformationIsEnabled = true,
			EnableUnifiedMailbox = false,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = false,
			UseDirectorySharedCache = false,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 1000,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x0400042C RID: 1068
		private static DefaultSettings.DefaultSettingsValues customer = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 1000000,
			DestinationMailboxReservedCounterRangeGradient = 10,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			DumpsterMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			DynamicSearchFolderPerScopeCountReceiveQuota = 100,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = false,
			EnableTraceDiagnosticQuery = false,
			EnableTraceFullTextIndexQuery = false,
			EnableTraceHeavyClientActivity = false,
			EnableTraceLockContention = false,
			EnableTraceLongOperation = false,
			EnableTraceReferenceData = false,
			EnableTraceRopResource = false,
			EnableTraceRopSummary = false,
			EnableTraceSyntheticCounters = false,
			FolderHierarchyChildrenCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyChildrenCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(1.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			MailboxMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "Customer",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = false,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 1000,
			StoreQueryLimitTime = TimeSpan.FromSeconds(10.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 10737418240L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 104857600,
			CheckpointDepthOnActive = 104857600,
			DatabaseCacheSizePercentage = 25,
			CachedClosedTables = 30000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(100.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(100.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = false,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = false,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ForceRimQueryMaterialization = true,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			ScheduledISIntegEnabled = false,
			ScheduledISIntegDetectOnly = string.Empty,
			ScheduledISIntegDetectAndFix = string.Empty,
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 0,
			EnableDatabaseUnusedSpaceScrubbing = true,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 10000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = false,
			EseLrukCorrInterval = TimeSpan.FromMilliseconds(128.0),
			EseLrukTimeout = TimeSpan.FromSeconds(100.0),
			UserInformationIsEnabled = false,
			EnableUnifiedMailbox = false,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = false,
			UseDirectorySharedCache = false,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 100,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = false
		};

		// Token: 0x0400042D RID: 1069
		private static DefaultSettings.DefaultSettingsValues dedicated = new DefaultSettings.DefaultSettingsValues
		{
			DestinationMailboxReservedCounterRangeConstant = 1000000,
			DestinationMailboxReservedCounterRangeGradient = 10,
			DiagnosticsThresholdChunkElapsedTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDatabaseTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdDirectoryCalls = 50,
			DiagnosticsThresholdDirectoryTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdHeavyActivityRpcCount = 200,
			DiagnosticsThresholdInstantSearchFolderView = TimeSpan.FromMilliseconds(250.0),
			DiagnosticsThresholdInteractionTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdLockTime = TimeSpan.FromSeconds(2.0),
			DiagnosticsThresholdPagesDirtied = 150,
			DiagnosticsThresholdPagesPreread = 300,
			DiagnosticsThresholdPagesRead = 150,
			DiscoverPotentiallyDisabledMailboxesTaskPeriod = TimeSpan.FromDays(10.0),
			DiscoverPotentiallyExpiredMailboxesTaskPeriod = TimeSpan.FromDays(1.0),
			DumpsterMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			DumpsterMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			DynamicSearchFolderPerScopeCountReceiveQuota = 10000,
			EnableMaterializedRestriction = true,
			EnableTraceBreadCrumbs = true,
			EnableTraceDiagnosticQuery = true,
			EnableTraceFullTextIndexQuery = true,
			EnableTraceHeavyClientActivity = true,
			EnableTraceLockContention = true,
			EnableTraceLongOperation = true,
			EnableTraceReferenceData = true,
			EnableTraceRopResource = true,
			EnableTraceRopSummary = true,
			EnableTraceSyntheticCounters = true,
			FolderHierarchyChildrenCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyChildrenCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthReceiveQuota = UnlimitedItems.UnlimitedValue,
			FolderHierarchyDepthWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountWarningQuota = UnlimitedItems.UnlimitedValue,
			FoldersCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			IdleAccessibleMailboxesTime = TimeSpan.FromDays(30.0),
			IdleIndexTimeForEmptyFolderOperation = TimeSpan.FromMinutes(10.0),
			MailboxLockMaximumWaitCount = 10,
			MailboxMessagesPerFolderCountReceiveQuota = UnlimitedItems.UnlimitedValue,
			MailboxMessagesPerFolderCountWarningQuota = UnlimitedItems.UnlimitedValue,
			MaintenanceControlPeriod = TimeSpan.FromMinutes(30.0),
			MaxChildCountForDumpsterHierarchyPublicFolder = new UnlimitedItems(1000L),
			MaximumMountedDatabases = 100,
			MaximumActivePropertyPromotions = 2,
			MaximumNumberOfExceptions = 10,
			Name = "Dedicated",
			ProcessorNumberOfColumnResults = 3,
			ProcessorNumberOfPropertyResults = 3,
			QueryVersionRefreshInterval = TimeSpan.FromMinutes(2.0),
			RetailAssertOnJetVsom = false,
			RetailAssertOnUnexpectedJetErrors = false,
			RopSummarySlowThreshold = TimeSpan.FromMilliseconds(70.0),
			TraceIntervalRopLockContention = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopResource = TimeSpan.FromMinutes(5.0),
			TraceIntervalRopSummary = TimeSpan.FromMinutes(5.0),
			SearchTraceFastOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceFastTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			SearchTraceFirstLinkedThreshold = TimeSpan.FromMilliseconds(250.0),
			SearchTraceGetQueryPlanThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceGetRestrictionThreshold = TimeSpan.FromMilliseconds(100.0),
			SearchTraceStoreOperationThreshold = TimeSpan.FromMilliseconds(5.0),
			SearchTraceStoreTotalThreshold = TimeSpan.FromMilliseconds(50.0),
			StoreQueryLimitRows = 1000,
			StoreQueryLimitTime = TimeSpan.FromSeconds(10.0),
			TrackAllLockAcquisition = false,
			SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = 100000L,
			SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = 10737418240L,
			LockManagerCrashingThresholdTimeout = TimeSpan.FromHours(2.0),
			ThreadHangDetectionTimeout = TimeSpan.FromHours(2.0),
			IntegrityCheckJobAgeoutTimeSpan = TimeSpan.FromDays(10.0),
			LastLogRefreshCheckDurationInSeconds = (int)TimeSpan.FromMinutes(2.0).TotalSeconds,
			LastLogUpdateAdvancingLimit = 50L,
			LastLogUpdateLaggingLimit = 1000L,
			MaximumSupportableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			MaximumRequestableDatabaseSchemaVersion = new ComponentVersion(0, 131),
			CheckpointDepthOnPassive = 104857600,
			CheckpointDepthOnActive = 104857600,
			DatabaseCacheSizePercentage = 25,
			CachedClosedTables = 10000,
			DbScanThrottleOnActive = TimeSpan.FromMilliseconds(400.0),
			DbScanThrottleOnPassive = TimeSpan.FromMilliseconds(400.0),
			EnableTestCaseIdLookup = false,
			EnableReadFromPassive = false,
			InferenceLogRetentionPeriod = TimeSpan.FromDays(10.0),
			InferenceLogMaxRows = new UnlimitedItems(100000L),
			ChunkedIndexPopulationEnabled = true,
			EnableDbDivergenceDetection = false,
			LazyTransactionCommitTimeout = TimeSpan.Zero,
			DiagnosticQueryLockTimeout = TimeSpan.FromMinutes(1.0),
			EnableConversationMasterIndex = false,
			ForceConversationMasterIndexUpgrade = false,
			ScheduledISIntegEnabled = false,
			ScheduledISIntegDetectOnly = string.Empty,
			ScheduledISIntegDetectAndFix = string.Empty,
			ScheduledISIntegExecutePeriod = TimeSpan.FromDays(30.0),
			EseStageFlighting = 0,
			EnableDatabaseUnusedSpaceScrubbing = false,
			DefaultMailboxSharedObjectPropertyBagDataCacheSize = 24,
			PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = 10000,
			SharedObjectPropertyBagDataCacheCleanupMultiplier = 10,
			ConfigurableSharedLockStage = 6,
			EnableIMAPDuplicateDetection = false,
			IndexUpdateBreadcrumbsInstrumentation = false,
			EseLrukCorrInterval = TimeSpan.FromSeconds(30.0),
			EseLrukTimeout = TimeSpan.FromHours(1.0),
			UserInformationIsEnabled = false,
			EnableUnifiedMailbox = false,
			ForceRimQueryMaterialization = true,
			VersionStoreCleanupMaintenanceTaskSupported = false,
			DisableBypassTempStream = false,
			CheckQuotaOnMessageCreate = false,
			UseDirectorySharedCache = false,
			MailboxInfoCacheSize = 1000,
			ForeignMailboxInfoCacheSize = 1000,
			AddressInfoCacheSize = 1000,
			ForeignAddressInfoCacheSize = 1000,
			DatabaseInfoCacheSize = 100,
			OrganizationContainerCacheSize = 100,
			MaterializedRestrictionSearchFolderConfigStage = 1,
			AttachmentMessageSaveChunking = true,
			TimeZoneBlobTruncationLength = 2560,
			EnablePropertiesPromotionValidation = true
		};

		// Token: 0x0400042E RID: 1070
		private static Hookable<bool> isTestEnvironment = Hookable<bool>.Create(true, StoreEnvironment.IsTestEnvironment);

		// Token: 0x0400042F RID: 1071
		private static Hookable<bool> isDatacenterEnvironment = Hookable<bool>.Create(true, StoreEnvironment.IsDatacenterEnvironment);

		// Token: 0x04000430 RID: 1072
		private static Hookable<bool> isSdfEnvironment = Hookable<bool>.Create(true, StoreEnvironment.IsSdfEnvironment);

		// Token: 0x04000431 RID: 1073
		private static Hookable<bool> isDogfoodEnvironment = Hookable<bool>.Create(true, StoreEnvironment.IsDogfoodEnvironment);

		// Token: 0x04000432 RID: 1074
		private static Hookable<bool> isDedicatedEnvironment = Hookable<bool>.Create(true, StoreEnvironment.IsDedicatedEnvironment);

		// Token: 0x0200002C RID: 44
		internal static class Test
		{
			// Token: 0x06000303 RID: 771 RVA: 0x0000AC63 File Offset: 0x00008E63
			internal static IDisposable SetIsTestEnvironment(bool isTestEnvironment)
			{
				return DefaultSettings.isTestEnvironment.SetTestHook(isTestEnvironment);
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000AC70 File Offset: 0x00008E70
			internal static IDisposable SetIsDatacenterEnvironment(bool isDatacenterEnvironment)
			{
				return DefaultSettings.isDatacenterEnvironment.SetTestHook(isDatacenterEnvironment);
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000AC7D File Offset: 0x00008E7D
			internal static IDisposable SetIsSdfEnvironment(bool isSdfEnvironment)
			{
				return DefaultSettings.isSdfEnvironment.SetTestHook(isSdfEnvironment);
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000AC8A File Offset: 0x00008E8A
			internal static IDisposable SetIsDogfoodEnvironment(bool isDogfoodEnvironment)
			{
				return DefaultSettings.isDogfoodEnvironment.SetTestHook(isDogfoodEnvironment);
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000AC97 File Offset: 0x00008E97
			internal static IDisposable SetIsDedicatedEnvironment(bool isDedicatedEnvironment)
			{
				return DefaultSettings.isDedicatedEnvironment.SetTestHook(isDedicatedEnvironment);
			}
		}

		// Token: 0x0200002D RID: 45
		public class DefaultSettingsValues
		{
			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000ACA4 File Offset: 0x00008EA4
			// (set) Token: 0x06000309 RID: 777 RVA: 0x0000ACAC File Offset: 0x00008EAC
			public int DestinationMailboxReservedCounterRangeConstant { get; internal set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000ACB5 File Offset: 0x00008EB5
			// (set) Token: 0x0600030B RID: 779 RVA: 0x0000ACBD File Offset: 0x00008EBD
			public int DestinationMailboxReservedCounterRangeGradient { get; internal set; }

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000ACC6 File Offset: 0x00008EC6
			// (set) Token: 0x0600030D RID: 781 RVA: 0x0000ACCE File Offset: 0x00008ECE
			public TimeSpan DiagnosticsThresholdDatabaseTime { get; internal set; }

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600030E RID: 782 RVA: 0x0000ACD7 File Offset: 0x00008ED7
			// (set) Token: 0x0600030F RID: 783 RVA: 0x0000ACDF File Offset: 0x00008EDF
			public int DiagnosticsThresholdDirectoryCalls { get; internal set; }

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x06000310 RID: 784 RVA: 0x0000ACE8 File Offset: 0x00008EE8
			// (set) Token: 0x06000311 RID: 785 RVA: 0x0000ACF0 File Offset: 0x00008EF0
			public TimeSpan DiagnosticsThresholdDirectoryTime { get; internal set; }

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000312 RID: 786 RVA: 0x0000ACF9 File Offset: 0x00008EF9
			// (set) Token: 0x06000313 RID: 787 RVA: 0x0000AD01 File Offset: 0x00008F01
			public int DiagnosticsThresholdHeavyActivityRpcCount { get; internal set; }

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x06000314 RID: 788 RVA: 0x0000AD0A File Offset: 0x00008F0A
			// (set) Token: 0x06000315 RID: 789 RVA: 0x0000AD12 File Offset: 0x00008F12
			public TimeSpan DiagnosticsThresholdInstantSearchFolderView { get; internal set; }

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x06000316 RID: 790 RVA: 0x0000AD1B File Offset: 0x00008F1B
			// (set) Token: 0x06000317 RID: 791 RVA: 0x0000AD23 File Offset: 0x00008F23
			public TimeSpan DiagnosticsThresholdInteractionTime { get; internal set; }

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x06000318 RID: 792 RVA: 0x0000AD2C File Offset: 0x00008F2C
			// (set) Token: 0x06000319 RID: 793 RVA: 0x0000AD34 File Offset: 0x00008F34
			public TimeSpan DiagnosticsThresholdLockTime { get; internal set; }

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0000AD3D File Offset: 0x00008F3D
			// (set) Token: 0x0600031B RID: 795 RVA: 0x0000AD45 File Offset: 0x00008F45
			public int DiagnosticsThresholdPagesDirtied { get; internal set; }

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000AD4E File Offset: 0x00008F4E
			// (set) Token: 0x0600031D RID: 797 RVA: 0x0000AD56 File Offset: 0x00008F56
			public int DiagnosticsThresholdPagesPreread { get; internal set; }

			// Token: 0x1700005F RID: 95
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0000AD5F File Offset: 0x00008F5F
			// (set) Token: 0x0600031F RID: 799 RVA: 0x0000AD67 File Offset: 0x00008F67
			public int DiagnosticsThresholdPagesRead { get; internal set; }

			// Token: 0x17000060 RID: 96
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000AD70 File Offset: 0x00008F70
			// (set) Token: 0x06000321 RID: 801 RVA: 0x0000AD78 File Offset: 0x00008F78
			public TimeSpan DiagnosticsThresholdChunkElapsedTime { get; internal set; }

			// Token: 0x17000061 RID: 97
			// (get) Token: 0x06000322 RID: 802 RVA: 0x0000AD81 File Offset: 0x00008F81
			// (set) Token: 0x06000323 RID: 803 RVA: 0x0000AD89 File Offset: 0x00008F89
			public TimeSpan DiscoverPotentiallyDisabledMailboxesTaskPeriod { get; internal set; }

			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000AD92 File Offset: 0x00008F92
			// (set) Token: 0x06000325 RID: 805 RVA: 0x0000AD9A File Offset: 0x00008F9A
			public TimeSpan DiscoverPotentiallyExpiredMailboxesTaskPeriod { get; internal set; }

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000326 RID: 806 RVA: 0x0000ADA3 File Offset: 0x00008FA3
			// (set) Token: 0x06000327 RID: 807 RVA: 0x0000ADAB File Offset: 0x00008FAB
			public UnlimitedItems DumpsterMessagesPerFolderCountReceiveQuota { get; internal set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x06000328 RID: 808 RVA: 0x0000ADB4 File Offset: 0x00008FB4
			// (set) Token: 0x06000329 RID: 809 RVA: 0x0000ADBC File Offset: 0x00008FBC
			public UnlimitedItems DumpsterMessagesPerFolderCountWarningQuota { get; internal set; }

			// Token: 0x17000065 RID: 101
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000ADC5 File Offset: 0x00008FC5
			// (set) Token: 0x0600032B RID: 811 RVA: 0x0000ADCD File Offset: 0x00008FCD
			public int DynamicSearchFolderPerScopeCountReceiveQuota { get; internal set; }

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000ADD6 File Offset: 0x00008FD6
			// (set) Token: 0x0600032D RID: 813 RVA: 0x0000ADDE File Offset: 0x00008FDE
			public bool EnableMaterializedRestriction { get; internal set; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000ADE7 File Offset: 0x00008FE7
			// (set) Token: 0x0600032F RID: 815 RVA: 0x0000ADEF File Offset: 0x00008FEF
			public bool EnableTraceBreadCrumbs { get; internal set; }

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000330 RID: 816 RVA: 0x0000ADF8 File Offset: 0x00008FF8
			// (set) Token: 0x06000331 RID: 817 RVA: 0x0000AE00 File Offset: 0x00009000
			public bool EnableTraceDiagnosticQuery { get; internal set; }

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000332 RID: 818 RVA: 0x0000AE09 File Offset: 0x00009009
			// (set) Token: 0x06000333 RID: 819 RVA: 0x0000AE11 File Offset: 0x00009011
			public bool EnableTraceFullTextIndexQuery { get; internal set; }

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000334 RID: 820 RVA: 0x0000AE1A File Offset: 0x0000901A
			// (set) Token: 0x06000335 RID: 821 RVA: 0x0000AE22 File Offset: 0x00009022
			public bool EnableTraceHeavyClientActivity { get; internal set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x06000336 RID: 822 RVA: 0x0000AE2B File Offset: 0x0000902B
			// (set) Token: 0x06000337 RID: 823 RVA: 0x0000AE33 File Offset: 0x00009033
			public bool EnableTraceLockContention { get; internal set; }

			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000338 RID: 824 RVA: 0x0000AE3C File Offset: 0x0000903C
			// (set) Token: 0x06000339 RID: 825 RVA: 0x0000AE44 File Offset: 0x00009044
			public bool EnableTraceLongOperation { get; internal set; }

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x0600033A RID: 826 RVA: 0x0000AE4D File Offset: 0x0000904D
			// (set) Token: 0x0600033B RID: 827 RVA: 0x0000AE55 File Offset: 0x00009055
			public bool EnableTraceReferenceData { get; internal set; }

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x0600033C RID: 828 RVA: 0x0000AE5E File Offset: 0x0000905E
			// (set) Token: 0x0600033D RID: 829 RVA: 0x0000AE66 File Offset: 0x00009066
			public bool EnableTraceRopResource { get; internal set; }

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0000AE6F File Offset: 0x0000906F
			// (set) Token: 0x0600033F RID: 831 RVA: 0x0000AE77 File Offset: 0x00009077
			public bool EnableTraceRopSummary { get; internal set; }

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x06000340 RID: 832 RVA: 0x0000AE80 File Offset: 0x00009080
			// (set) Token: 0x06000341 RID: 833 RVA: 0x0000AE88 File Offset: 0x00009088
			public bool EnableTraceSyntheticCounters { get; internal set; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x06000342 RID: 834 RVA: 0x0000AE91 File Offset: 0x00009091
			// (set) Token: 0x06000343 RID: 835 RVA: 0x0000AE99 File Offset: 0x00009099
			public UnlimitedItems FolderHierarchyChildrenCountReceiveQuota { get; internal set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000344 RID: 836 RVA: 0x0000AEA2 File Offset: 0x000090A2
			// (set) Token: 0x06000345 RID: 837 RVA: 0x0000AEAA File Offset: 0x000090AA
			public UnlimitedItems FolderHierarchyChildrenCountWarningQuota { get; internal set; }

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0000AEB3 File Offset: 0x000090B3
			// (set) Token: 0x06000347 RID: 839 RVA: 0x0000AEBB File Offset: 0x000090BB
			public UnlimitedItems FolderHierarchyDepthReceiveQuota { get; internal set; }

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000AEC4 File Offset: 0x000090C4
			// (set) Token: 0x06000349 RID: 841 RVA: 0x0000AECC File Offset: 0x000090CC
			public UnlimitedItems FolderHierarchyDepthWarningQuota { get; internal set; }

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0000AED5 File Offset: 0x000090D5
			// (set) Token: 0x0600034B RID: 843 RVA: 0x0000AEDD File Offset: 0x000090DD
			public UnlimitedItems FoldersCountWarningQuota { get; internal set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0000AEE6 File Offset: 0x000090E6
			// (set) Token: 0x0600034D RID: 845 RVA: 0x0000AEEE File Offset: 0x000090EE
			public UnlimitedItems FoldersCountReceiveQuota { get; internal set; }

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600034E RID: 846 RVA: 0x0000AEF7 File Offset: 0x000090F7
			// (set) Token: 0x0600034F RID: 847 RVA: 0x0000AEFF File Offset: 0x000090FF
			public TimeSpan IdleAccessibleMailboxesTime { get; internal set; }

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x06000350 RID: 848 RVA: 0x0000AF08 File Offset: 0x00009108
			// (set) Token: 0x06000351 RID: 849 RVA: 0x0000AF10 File Offset: 0x00009110
			public TimeSpan IdleIndexTimeForEmptyFolderOperation { get; internal set; }

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000352 RID: 850 RVA: 0x0000AF19 File Offset: 0x00009119
			// (set) Token: 0x06000353 RID: 851 RVA: 0x0000AF21 File Offset: 0x00009121
			public int MailboxLockMaximumWaitCount { get; internal set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000354 RID: 852 RVA: 0x0000AF2A File Offset: 0x0000912A
			// (set) Token: 0x06000355 RID: 853 RVA: 0x0000AF32 File Offset: 0x00009132
			public UnlimitedItems MailboxMessagesPerFolderCountReceiveQuota { get; internal set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000356 RID: 854 RVA: 0x0000AF3B File Offset: 0x0000913B
			// (set) Token: 0x06000357 RID: 855 RVA: 0x0000AF43 File Offset: 0x00009143
			public UnlimitedItems MailboxMessagesPerFolderCountWarningQuota { get; internal set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000358 RID: 856 RVA: 0x0000AF4C File Offset: 0x0000914C
			// (set) Token: 0x06000359 RID: 857 RVA: 0x0000AF54 File Offset: 0x00009154
			public TimeSpan MaintenanceControlPeriod { get; internal set; }

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000AF5D File Offset: 0x0000915D
			// (set) Token: 0x0600035B RID: 859 RVA: 0x0000AF65 File Offset: 0x00009165
			public UnlimitedItems MaxChildCountForDumpsterHierarchyPublicFolder { get; internal set; }

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0000AF6E File Offset: 0x0000916E
			// (set) Token: 0x0600035D RID: 861 RVA: 0x0000AF76 File Offset: 0x00009176
			public byte MaximumMountedDatabases { get; internal set; }

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000AF7F File Offset: 0x0000917F
			// (set) Token: 0x0600035F RID: 863 RVA: 0x0000AF87 File Offset: 0x00009187
			public int MaximumActivePropertyPromotions { get; internal set; }

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x06000360 RID: 864 RVA: 0x0000AF90 File Offset: 0x00009190
			// (set) Token: 0x06000361 RID: 865 RVA: 0x0000AF98 File Offset: 0x00009198
			public int MaximumNumberOfExceptions { get; internal set; }

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0000AFA1 File Offset: 0x000091A1
			// (set) Token: 0x06000363 RID: 867 RVA: 0x0000AFA9 File Offset: 0x000091A9
			public string Name { get; internal set; }

			// Token: 0x17000082 RID: 130
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000AFB2 File Offset: 0x000091B2
			// (set) Token: 0x06000365 RID: 869 RVA: 0x0000AFBA File Offset: 0x000091BA
			public int ProcessorNumberOfColumnResults { get; internal set; }

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000AFC3 File Offset: 0x000091C3
			// (set) Token: 0x06000367 RID: 871 RVA: 0x0000AFCB File Offset: 0x000091CB
			public int ProcessorNumberOfPropertyResults { get; internal set; }

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x06000368 RID: 872 RVA: 0x0000AFD4 File Offset: 0x000091D4
			// (set) Token: 0x06000369 RID: 873 RVA: 0x0000AFDC File Offset: 0x000091DC
			public TimeSpan QueryVersionRefreshInterval { get; internal set; }

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x0600036A RID: 874 RVA: 0x0000AFE5 File Offset: 0x000091E5
			// (set) Token: 0x0600036B RID: 875 RVA: 0x0000AFED File Offset: 0x000091ED
			public bool RetailAssertOnJetVsom { get; internal set; }

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x0600036C RID: 876 RVA: 0x0000AFF6 File Offset: 0x000091F6
			// (set) Token: 0x0600036D RID: 877 RVA: 0x0000AFFE File Offset: 0x000091FE
			public bool RetailAssertOnUnexpectedJetErrors { get; internal set; }

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600036E RID: 878 RVA: 0x0000B007 File Offset: 0x00009207
			// (set) Token: 0x0600036F RID: 879 RVA: 0x0000B00F File Offset: 0x0000920F
			public TimeSpan RopSummarySlowThreshold { get; internal set; }

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0000B018 File Offset: 0x00009218
			// (set) Token: 0x06000371 RID: 881 RVA: 0x0000B020 File Offset: 0x00009220
			public TimeSpan TraceIntervalRopLockContention { get; internal set; }

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000372 RID: 882 RVA: 0x0000B029 File Offset: 0x00009229
			// (set) Token: 0x06000373 RID: 883 RVA: 0x0000B031 File Offset: 0x00009231
			public TimeSpan TraceIntervalRopResource { get; internal set; }

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000374 RID: 884 RVA: 0x0000B03A File Offset: 0x0000923A
			// (set) Token: 0x06000375 RID: 885 RVA: 0x0000B042 File Offset: 0x00009242
			public TimeSpan TraceIntervalRopSummary { get; internal set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000376 RID: 886 RVA: 0x0000B04B File Offset: 0x0000924B
			// (set) Token: 0x06000377 RID: 887 RVA: 0x0000B053 File Offset: 0x00009253
			public TimeSpan SearchTraceFastOperationThreshold { get; internal set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000378 RID: 888 RVA: 0x0000B05C File Offset: 0x0000925C
			// (set) Token: 0x06000379 RID: 889 RVA: 0x0000B064 File Offset: 0x00009264
			public TimeSpan SearchTraceFastTotalThreshold { get; internal set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000B06D File Offset: 0x0000926D
			// (set) Token: 0x0600037B RID: 891 RVA: 0x0000B075 File Offset: 0x00009275
			public TimeSpan SearchTraceFirstLinkedThreshold { get; internal set; }

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600037C RID: 892 RVA: 0x0000B07E File Offset: 0x0000927E
			// (set) Token: 0x0600037D RID: 893 RVA: 0x0000B086 File Offset: 0x00009286
			public TimeSpan SearchTraceGetQueryPlanThreshold { get; internal set; }

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0000B08F File Offset: 0x0000928F
			// (set) Token: 0x0600037F RID: 895 RVA: 0x0000B097 File Offset: 0x00009297
			public TimeSpan SearchTraceGetRestrictionThreshold { get; internal set; }

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x06000380 RID: 896 RVA: 0x0000B0A0 File Offset: 0x000092A0
			// (set) Token: 0x06000381 RID: 897 RVA: 0x0000B0A8 File Offset: 0x000092A8
			public TimeSpan SearchTraceStoreOperationThreshold { get; internal set; }

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x06000382 RID: 898 RVA: 0x0000B0B1 File Offset: 0x000092B1
			// (set) Token: 0x06000383 RID: 899 RVA: 0x0000B0B9 File Offset: 0x000092B9
			public TimeSpan SearchTraceStoreTotalThreshold { get; internal set; }

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000384 RID: 900 RVA: 0x0000B0C2 File Offset: 0x000092C2
			// (set) Token: 0x06000385 RID: 901 RVA: 0x0000B0CA File Offset: 0x000092CA
			public int StoreQueryLimitRows { get; internal set; }

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000386 RID: 902 RVA: 0x0000B0D3 File Offset: 0x000092D3
			// (set) Token: 0x06000387 RID: 903 RVA: 0x0000B0DB File Offset: 0x000092DB
			public TimeSpan StoreQueryLimitTime { get; internal set; }

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000388 RID: 904 RVA: 0x0000B0E4 File Offset: 0x000092E4
			// (set) Token: 0x06000389 RID: 905 RVA: 0x0000B0EC File Offset: 0x000092EC
			public bool TrackAllLockAcquisition { get; internal set; }

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x0600038A RID: 906 RVA: 0x0000B0F5 File Offset: 0x000092F5
			// (set) Token: 0x0600038B RID: 907 RVA: 0x0000B0FD File Offset: 0x000092FD
			public long SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold { get; internal set; }

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x0600038C RID: 908 RVA: 0x0000B106 File Offset: 0x00009306
			// (set) Token: 0x0600038D RID: 909 RVA: 0x0000B10E File Offset: 0x0000930E
			public long SubobjectCleanupUrgentMaintenanceTotalSizeThreshold { get; internal set; }

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600038E RID: 910 RVA: 0x0000B117 File Offset: 0x00009317
			// (set) Token: 0x0600038F RID: 911 RVA: 0x0000B11F File Offset: 0x0000931F
			public TimeSpan LockManagerCrashingThresholdTimeout { get; internal set; }

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x06000390 RID: 912 RVA: 0x0000B128 File Offset: 0x00009328
			// (set) Token: 0x06000391 RID: 913 RVA: 0x0000B130 File Offset: 0x00009330
			public TimeSpan ThreadHangDetectionTimeout { get; internal set; }

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0000B139 File Offset: 0x00009339
			// (set) Token: 0x06000393 RID: 915 RVA: 0x0000B141 File Offset: 0x00009341
			public TimeSpan IntegrityCheckJobAgeoutTimeSpan { get; internal set; }

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000394 RID: 916 RVA: 0x0000B14A File Offset: 0x0000934A
			// (set) Token: 0x06000395 RID: 917 RVA: 0x0000B152 File Offset: 0x00009352
			public int LastLogRefreshCheckDurationInSeconds { get; internal set; }

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x06000396 RID: 918 RVA: 0x0000B15B File Offset: 0x0000935B
			// (set) Token: 0x06000397 RID: 919 RVA: 0x0000B163 File Offset: 0x00009363
			public long LastLogUpdateAdvancingLimit { get; internal set; }

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x06000398 RID: 920 RVA: 0x0000B16C File Offset: 0x0000936C
			// (set) Token: 0x06000399 RID: 921 RVA: 0x0000B174 File Offset: 0x00009374
			public long LastLogUpdateLaggingLimit { get; internal set; }

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x0600039A RID: 922 RVA: 0x0000B17D File Offset: 0x0000937D
			// (set) Token: 0x0600039B RID: 923 RVA: 0x0000B185 File Offset: 0x00009385
			public ComponentVersion MaximumSupportableDatabaseSchemaVersion { get; internal set; }

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x0600039C RID: 924 RVA: 0x0000B18E File Offset: 0x0000938E
			// (set) Token: 0x0600039D RID: 925 RVA: 0x0000B196 File Offset: 0x00009396
			public ComponentVersion MaximumRequestableDatabaseSchemaVersion { get; internal set; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x0600039E RID: 926 RVA: 0x0000B19F File Offset: 0x0000939F
			// (set) Token: 0x0600039F RID: 927 RVA: 0x0000B1A7 File Offset: 0x000093A7
			public int CheckpointDepthOnPassive { get; internal set; }

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000B1B0 File Offset: 0x000093B0
			// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000B1B8 File Offset: 0x000093B8
			public int CheckpointDepthOnActive { get; internal set; }

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B1C1 File Offset: 0x000093C1
			// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000B1C9 File Offset: 0x000093C9
			public int DatabaseCacheSizePercentage { get; internal set; }

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000B1D2 File Offset: 0x000093D2
			// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000B1DA File Offset: 0x000093DA
			public int CachedClosedTables { get; internal set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B1E3 File Offset: 0x000093E3
			// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000B1EB File Offset: 0x000093EB
			public TimeSpan DbScanThrottleOnActive { get; internal set; }

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000B1F4 File Offset: 0x000093F4
			// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000B1FC File Offset: 0x000093FC
			public TimeSpan DbScanThrottleOnPassive { get; internal set; }

			// Token: 0x170000A5 RID: 165
			// (get) Token: 0x060003AA RID: 938 RVA: 0x0000B205 File Offset: 0x00009405
			// (set) Token: 0x060003AB RID: 939 RVA: 0x0000B20D File Offset: 0x0000940D
			public bool EnableTestCaseIdLookup { get; internal set; }

			// Token: 0x170000A6 RID: 166
			// (get) Token: 0x060003AC RID: 940 RVA: 0x0000B216 File Offset: 0x00009416
			// (set) Token: 0x060003AD RID: 941 RVA: 0x0000B21E File Offset: 0x0000941E
			public bool EnableReadFromPassive { get; internal set; }

			// Token: 0x170000A7 RID: 167
			// (get) Token: 0x060003AE RID: 942 RVA: 0x0000B227 File Offset: 0x00009427
			// (set) Token: 0x060003AF RID: 943 RVA: 0x0000B22F File Offset: 0x0000942F
			public TimeSpan InferenceLogRetentionPeriod { get; internal set; }

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000B238 File Offset: 0x00009438
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000B240 File Offset: 0x00009440
			public UnlimitedItems InferenceLogMaxRows { get; internal set; }

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000B249 File Offset: 0x00009449
			// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000B251 File Offset: 0x00009451
			public bool ChunkedIndexPopulationEnabled { get; internal set; }

			// Token: 0x170000AA RID: 170
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000B25A File Offset: 0x0000945A
			// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000B262 File Offset: 0x00009462
			public bool EnableDbDivergenceDetection { get; internal set; }

			// Token: 0x170000AB RID: 171
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000B26B File Offset: 0x0000946B
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000B273 File Offset: 0x00009473
			public bool EnableConversationMasterIndex { get; internal set; }

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000B27C File Offset: 0x0000947C
			// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000B284 File Offset: 0x00009484
			public bool ForceConversationMasterIndexUpgrade { get; internal set; }

			// Token: 0x170000AD RID: 173
			// (get) Token: 0x060003BA RID: 954 RVA: 0x0000B28D File Offset: 0x0000948D
			// (set) Token: 0x060003BB RID: 955 RVA: 0x0000B295 File Offset: 0x00009495
			public bool ForceRimQueryMaterialization { get; internal set; }

			// Token: 0x170000AE RID: 174
			// (get) Token: 0x060003BC RID: 956 RVA: 0x0000B29E File Offset: 0x0000949E
			// (set) Token: 0x060003BD RID: 957 RVA: 0x0000B2A6 File Offset: 0x000094A6
			public bool ScheduledISIntegEnabled { get; internal set; }

			// Token: 0x170000AF RID: 175
			// (get) Token: 0x060003BE RID: 958 RVA: 0x0000B2AF File Offset: 0x000094AF
			// (set) Token: 0x060003BF RID: 959 RVA: 0x0000B2B7 File Offset: 0x000094B7
			public string ScheduledISIntegDetectOnly { get; internal set; }

			// Token: 0x170000B0 RID: 176
			// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000B2C0 File Offset: 0x000094C0
			// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000B2C8 File Offset: 0x000094C8
			public string ScheduledISIntegDetectAndFix { get; internal set; }

			// Token: 0x170000B1 RID: 177
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B2D1 File Offset: 0x000094D1
			// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000B2D9 File Offset: 0x000094D9
			public TimeSpan ScheduledISIntegExecutePeriod { get; internal set; }

			// Token: 0x170000B2 RID: 178
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000B2E2 File Offset: 0x000094E2
			// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000B2EA File Offset: 0x000094EA
			public TimeSpan LazyTransactionCommitTimeout { get; internal set; }

			// Token: 0x170000B3 RID: 179
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000B2F3 File Offset: 0x000094F3
			// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000B2FB File Offset: 0x000094FB
			public TimeSpan DiagnosticQueryLockTimeout { get; internal set; }

			// Token: 0x170000B4 RID: 180
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000B304 File Offset: 0x00009504
			// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000B30C File Offset: 0x0000950C
			public int EseStageFlighting { get; internal set; }

			// Token: 0x170000B5 RID: 181
			// (get) Token: 0x060003CA RID: 970 RVA: 0x0000B315 File Offset: 0x00009515
			// (set) Token: 0x060003CB RID: 971 RVA: 0x0000B31D File Offset: 0x0000951D
			public bool EnableDatabaseUnusedSpaceScrubbing { get; internal set; }

			// Token: 0x170000B6 RID: 182
			// (get) Token: 0x060003CC RID: 972 RVA: 0x0000B326 File Offset: 0x00009526
			// (set) Token: 0x060003CD RID: 973 RVA: 0x0000B32E File Offset: 0x0000952E
			public int DefaultMailboxSharedObjectPropertyBagDataCacheSize { get; internal set; }

			// Token: 0x170000B7 RID: 183
			// (get) Token: 0x060003CE RID: 974 RVA: 0x0000B337 File Offset: 0x00009537
			// (set) Token: 0x060003CF RID: 975 RVA: 0x0000B33F File Offset: 0x0000953F
			public int PublicFolderMailboxSharedObjectPropertyBagDataCacheSize { get; internal set; }

			// Token: 0x170000B8 RID: 184
			// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000B348 File Offset: 0x00009548
			// (set) Token: 0x060003D1 RID: 977 RVA: 0x0000B350 File Offset: 0x00009550
			public int SharedObjectPropertyBagDataCacheCleanupMultiplier { get; internal set; }

			// Token: 0x170000B9 RID: 185
			// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000B359 File Offset: 0x00009559
			// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000B361 File Offset: 0x00009561
			public int ConfigurableSharedLockStage { get; internal set; }

			// Token: 0x170000BA RID: 186
			// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000B36A File Offset: 0x0000956A
			// (set) Token: 0x060003D5 RID: 981 RVA: 0x0000B372 File Offset: 0x00009572
			public TimeSpan EseLrukCorrInterval { get; internal set; }

			// Token: 0x170000BB RID: 187
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000B37B File Offset: 0x0000957B
			// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000B383 File Offset: 0x00009583
			public TimeSpan EseLrukTimeout { get; internal set; }

			// Token: 0x170000BC RID: 188
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000B38C File Offset: 0x0000958C
			// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000B394 File Offset: 0x00009594
			public bool EnableIMAPDuplicateDetection { get; internal set; }

			// Token: 0x170000BD RID: 189
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0000B39D File Offset: 0x0000959D
			// (set) Token: 0x060003DB RID: 987 RVA: 0x0000B3A5 File Offset: 0x000095A5
			public bool IndexUpdateBreadcrumbsInstrumentation { get; internal set; }

			// Token: 0x170000BE RID: 190
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0000B3AE File Offset: 0x000095AE
			// (set) Token: 0x060003DD RID: 989 RVA: 0x0000B3B6 File Offset: 0x000095B6
			public bool UserInformationIsEnabled { get; internal set; }

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B3BF File Offset: 0x000095BF
			// (set) Token: 0x060003DF RID: 991 RVA: 0x0000B3C7 File Offset: 0x000095C7
			public bool EnableUnifiedMailbox { get; internal set; }

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000B3D0 File Offset: 0x000095D0
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000B3D8 File Offset: 0x000095D8
			public bool VersionStoreCleanupMaintenanceTaskSupported { get; internal set; }

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000B3E1 File Offset: 0x000095E1
			// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000B3E9 File Offset: 0x000095E9
			public bool DisableBypassTempStream { get; internal set; }

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000B3F2 File Offset: 0x000095F2
			// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000B3FA File Offset: 0x000095FA
			public bool CheckQuotaOnMessageCreate { get; internal set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000B403 File Offset: 0x00009603
			// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000B40B File Offset: 0x0000960B
			public bool UseDirectorySharedCache { get; internal set; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000B414 File Offset: 0x00009614
			// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000B41C File Offset: 0x0000961C
			public int MailboxInfoCacheSize { get; internal set; }

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000B425 File Offset: 0x00009625
			// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000B42D File Offset: 0x0000962D
			public int ForeignMailboxInfoCacheSize { get; internal set; }

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000B436 File Offset: 0x00009636
			// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000B43E File Offset: 0x0000963E
			public int AddressInfoCacheSize { get; internal set; }

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000B447 File Offset: 0x00009647
			// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000B44F File Offset: 0x0000964F
			public int ForeignAddressInfoCacheSize { get; internal set; }

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000B458 File Offset: 0x00009658
			// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000B460 File Offset: 0x00009660
			public int DatabaseInfoCacheSize { get; internal set; }

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000B469 File Offset: 0x00009669
			// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000B471 File Offset: 0x00009671
			public int OrganizationContainerCacheSize { get; internal set; }

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000B47A File Offset: 0x0000967A
			// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000B482 File Offset: 0x00009682
			public int MaterializedRestrictionSearchFolderConfigStage { get; internal set; }

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000B48B File Offset: 0x0000968B
			// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000B493 File Offset: 0x00009693
			public bool AttachmentMessageSaveChunking { get; internal set; }

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000B49C File Offset: 0x0000969C
			// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000B4A4 File Offset: 0x000096A4
			public int TimeZoneBlobTruncationLength { get; internal set; }

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000B4AD File Offset: 0x000096AD
			// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000B4B5 File Offset: 0x000096B5
			public bool EnablePropertiesPromotionValidation { get; internal set; }

			// Token: 0x060003FD RID: 1021 RVA: 0x0000B4C8 File Offset: 0x000096C8
			public IDisposable HookValue<T>(string name, T newValue)
			{
				name = string.Format("<{0}>k__BackingField", name);
				Hookable<T> hookable = Hookable<T>.Create<DefaultSettings.DefaultSettingsValues>(true, name, this);
				return hookable.SetTestHook(newValue);
			}
		}
	}
}
