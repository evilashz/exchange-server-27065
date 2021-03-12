using System;
using System.Collections.Generic;
using System.Configuration;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200001D RID: 29
	public abstract class ConfigurationSchema
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00005AA0 File Offset: 0x00003CA0
		static ConfigurationSchema()
		{
			ConfigurationSchema.DoNothingAndSilenceStyleCop();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00007328 File Offset: 0x00005528
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000732F File Offset: 0x0000552F
		public static bool IsZeroBox { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00007337 File Offset: 0x00005537
		public static IReadOnlyList<ConfigurationSchema> RegisteredConfigurations
		{
			get
			{
				return ConfigurationSchema.registeredConfigurations;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000733E File Offset: 0x0000553E
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00007345 File Offset: 0x00005545
		public static string LocalDatabaseRegistryKey { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000271 RID: 625
		internal abstract ConfigurationProperty ConfigurationProperty { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000734D File Offset: 0x0000554D
		public static DateTime NextRefreshTimeUTC
		{
			get
			{
				return ConfigurationSchema.nextRefreshTime;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007354 File Offset: 0x00005554
		public static void Initialize()
		{
			StoreConfigContext.Initialize();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007360 File Offset: 0x00005560
		public static void SetDatabaseContext(Guid? databaseGuid, Guid? dagOrServerGuid)
		{
			StoreConfigContext.Default.SetDatabaseContext(databaseGuid, dagOrServerGuid);
			ConfigurationSchema.LocalDatabaseRegistryKey = string.Format("SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}", Environment.MachineName, "Private", databaseGuid);
			ConfigurationSchema.ReloadTask(null, null, () => true);
			ConfigurationSchema.reloadTask = new RecurringTask<object>(new Task<object>.TaskCallback(ConfigurationSchema.ReloadTask), null, TimeSpan.FromMinutes(15.0), TimeSpan.FromMinutes(15.0), true);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000073F0 File Offset: 0x000055F0
		private static void DoNothingAndSilenceStyleCop()
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000073F4 File Offset: 0x000055F4
		private static void ReloadTask(TaskExecutionDiagnosticsProxy diagnosticsContext, object context, Func<bool> shouldContinue)
		{
			int num = 0;
			while (num < ConfigurationSchema.registeredConfigurations.Count && shouldContinue())
			{
				ConfigurationSchema.registeredConfigurations[num].Reload();
				num++;
			}
			ConfigurationSchema.nextRefreshTime = DateTime.UtcNow + TimeSpan.FromMinutes(15.0);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000744C File Offset: 0x0000564C
		private static bool TryTimeSpanFromMilliseconds(string value, out TimeSpan result)
		{
			double value2;
			if (double.TryParse(value, out value2))
			{
				result = TimeSpan.FromMilliseconds(value2);
				return true;
			}
			result = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00007480 File Offset: 0x00005680
		private static bool TryTimeSpanFromSeconds(string value, out TimeSpan result)
		{
			double value2;
			if (double.TryParse(value, out value2))
			{
				result = TimeSpan.FromSeconds(value2);
				return true;
			}
			result = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000074B4 File Offset: 0x000056B4
		private static bool TryTimeSpanFromMinutes(string value, out TimeSpan result)
		{
			double value2;
			if (double.TryParse(value, out value2))
			{
				result = TimeSpan.FromMinutes(value2);
				return true;
			}
			result = TimeSpan.Zero;
			return false;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000074E5 File Offset: 0x000056E5
		private static bool ValueGreaterThanZeroButNotUnlimited(UnlimitedItems value)
		{
			return value > UnlimitedItems.Zero && !value.IsUnlimited;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000074FF File Offset: 0x000056FF
		private static bool ValueGreaterThanZero(UnlimitedItems value)
		{
			return value > UnlimitedItems.Zero;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000750C File Offset: 0x0000570C
		private static bool ValueGreaterThanZero(long value)
		{
			return value > 0L;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007513 File Offset: 0x00005713
		private static bool ValueGreaterThanZero(TimeSpan value)
		{
			return value > TimeSpan.Zero;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007520 File Offset: 0x00005720
		private static Func<T, T> NoPostProcess<T>()
		{
			return null;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007523 File Offset: 0x00005723
		private static long ObjectLimitsPostProcess(long value, long defaultValue)
		{
			if (value < -1L)
			{
				value = 0L;
			}
			if (value == 0L)
			{
				value = defaultValue;
			}
			return value;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00007537 File Offset: 0x00005737
		protected ConfigurationSchema(string name)
		{
			this.name = name;
			if (ConfigurationSchema.registeredConfigurations == null)
			{
				ConfigurationSchema.registeredConfigurations = new List<ConfigurationSchema>();
			}
			ConfigurationSchema.registeredConfigurations.Add(this);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00007562 File Offset: 0x00005762
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06000282 RID: 642
		public abstract void Reload();

		// Token: 0x0400030A RID: 778
		public static ConfigurationSchema<int> MaxBreadcrumbs = new ReadOnceConfigurationSchema<int>("MaxBreadcrumbs", 1000);

		// Token: 0x0400030B RID: 779
		public static ConfigurationSchema<bool> TrackAllLockAcquisition = new ReadOnceConfigurationSchema<bool>("TrackAllLockAcquisition", DefaultSettings.Get.TrackAllLockAcquisition);

		// Token: 0x0400030C RID: 780
		public static ConfigurationSchema<string> LogPath = new ReadOnceConfigurationSchema<string>("LogPath", "%ExchangeInstallDir%\\Logging\\Store\\");

		// Token: 0x0400030D RID: 781
		public static ConfigurationSchema<TimeSpan> ThreadHangDetectionTimeout = new ReadOnceConfigurationSchema<TimeSpan>("ThreadHangDetectionTimeout", DefaultSettings.Get.ThreadHangDetectionTimeout, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromSeconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400030E RID: 782
		public static ConfigurationSchema<bool> RetailAssertOnUnexpectedJetErrors = new ReadOnceConfigurationSchema<bool>("RetailAssertOnUnexpectedJetErrors", DefaultSettings.Get.RetailAssertOnUnexpectedJetErrors);

		// Token: 0x0400030F RID: 783
		public static ConfigurationSchema<bool> RetailAssertOnJetVsom = new ReadOnceConfigurationSchema<bool>("RetailAssertOnJetVsom", DefaultSettings.Get.RetailAssertOnJetVsom);

		// Token: 0x04000310 RID: 784
		public static ConfigurationSchema<int> AttachmentBlobMaxSupportedDescendantCountRead = new ConfigurationSchema<int>("AttachmentBlobMaxSupportedDescendantCountRead", 100000, (int value) => Math.Max(100000, value));

		// Token: 0x04000311 RID: 785
		public static ConfigurationSchema<int> AttachmentBlobMaxSupportedDescendantCountWrite = new ConfigurationSchema<int>("AttachmentBlobMaxSupportedDescendantCountWrite", 10000, (int value) => Math.Min(ConfigurationSchema.AttachmentBlobMaxSupportedDescendantCountRead.Value, Math.Max(1000, value)));

		// Token: 0x04000312 RID: 786
		public static ConfigurationSchema<TimeSpan> MaintenanceControlPeriod = new ReadOnceConfigurationSchema<TimeSpan>("MaintenanceControlPeriod", DefaultSettings.Get.MaintenanceControlPeriod, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000313 RID: 787
		public static ConfigurationSchema<string> WorkerEnvironmentSettings = new ConfigurationSchema<string>("WorkerEnvironmentSettings", null);

		// Token: 0x04000314 RID: 788
		public static ConfigurationSchema<int> MailboxLockMaximumWaitCount = new ReadOnceConfigurationSchema<int>("MailboxLockMaximumWaitCount", DefaultSettings.Get.MailboxLockMaximumWaitCount);

		// Token: 0x04000315 RID: 789
		public static ConfigurationSchema<bool> EnableTraceBreadCrumbs = new ConfigurationSchema<bool>("EnableTraceBreadCrumbs", DefaultSettings.Get.EnableTraceBreadCrumbs);

		// Token: 0x04000316 RID: 790
		public static ConfigurationSchema<bool> EnableTraceDiagnosticQuery = new ConfigurationSchema<bool>("EnableTraceDiagnosticQuery", DefaultSettings.Get.EnableTraceDiagnosticQuery);

		// Token: 0x04000317 RID: 791
		public static ConfigurationSchema<bool> EnableTraceFullTextIndexQuery = new ConfigurationSchema<bool>("EnableTraceFullTextIndexQuery", DefaultSettings.Get.EnableTraceFullTextIndexQuery);

		// Token: 0x04000318 RID: 792
		public static ConfigurationSchema<bool> EnableTraceHeavyClientActivity = new ConfigurationSchema<bool>("EnableTraceHeavyClientActivity", DefaultSettings.Get.EnableTraceHeavyClientActivity);

		// Token: 0x04000319 RID: 793
		public static ConfigurationSchema<bool> EnableTraceLockContention = new ConfigurationSchema<bool>("EnableTraceLockContention", DefaultSettings.Get.EnableTraceLockContention);

		// Token: 0x0400031A RID: 794
		public static ConfigurationSchema<bool> EnableTraceLongOperation = new ConfigurationSchema<bool>("EnableTraceLongOperation", DefaultSettings.Get.EnableTraceLongOperation);

		// Token: 0x0400031B RID: 795
		public static ConfigurationSchema<bool> EnableTraceReferenceData = new ConfigurationSchema<bool>("EnableTraceReferenceData", DefaultSettings.Get.EnableTraceReferenceData);

		// Token: 0x0400031C RID: 796
		public static ConfigurationSchema<bool> EnableTraceRopResource = new ConfigurationSchema<bool>("EnableTraceRopResource", DefaultSettings.Get.EnableTraceRopResource);

		// Token: 0x0400031D RID: 797
		public static ConfigurationSchema<bool> EnableTraceRopSummary = new ConfigurationSchema<bool>("EnableTraceRopSummary", DefaultSettings.Get.EnableTraceRopSummary);

		// Token: 0x0400031E RID: 798
		public static ConfigurationSchema<bool> EnableTraceSyntheticCounters = new ConfigurationSchema<bool>("EnableTraceSyntheticCounters", DefaultSettings.Get.EnableTraceSyntheticCounters);

		// Token: 0x0400031F RID: 799
		public static ConfigurationSchema<TimeSpan> TraceIntervalRopLockContention = new ConfigurationSchema<TimeSpan>("TraceIntervalRopLockContention", DefaultSettings.Get.TraceIntervalRopLockContention, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds));

		// Token: 0x04000320 RID: 800
		public static ConfigurationSchema<TimeSpan> TraceIntervalRopResource = new ConfigurationSchema<TimeSpan>("TraceIntervalRopResource", DefaultSettings.Get.TraceIntervalRopResource, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds));

		// Token: 0x04000321 RID: 801
		public static ConfigurationSchema<TimeSpan> TraceIntervalRopSummary = new ConfigurationSchema<TimeSpan>("TraceIntervalRopSummary", DefaultSettings.Get.TraceIntervalRopSummary, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds));

		// Token: 0x04000322 RID: 802
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdDatabaseTime = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdDatabaseTime", DefaultSettings.Get.DiagnosticsThresholdDatabaseTime, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000323 RID: 803
		public static ConfigurationSchema<int> DiagnosticsThresholdDirectoryCalls = new ConfigurationSchema<int>("DiagnosticsThresholdDirectoryCalls", DefaultSettings.Get.DiagnosticsThresholdDirectoryCalls);

		// Token: 0x04000324 RID: 804
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdDirectoryTime = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdDirectoryTime", DefaultSettings.Get.DiagnosticsThresholdDirectoryTime, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000325 RID: 805
		public static ConfigurationSchema<int> DiagnosticsThresholdHeavyActivityRpcCount = new ConfigurationSchema<int>("DiagnosticsThresholdHeavyActivityRpcCount", DefaultSettings.Get.DiagnosticsThresholdHeavyActivityRpcCount);

		// Token: 0x04000326 RID: 806
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdInstantSearchFolderView = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdInstantSearchFolderView", DefaultSettings.Get.DiagnosticsThresholdInstantSearchFolderView, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000327 RID: 807
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdInteractionTime = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdInteractionTime", DefaultSettings.Get.DiagnosticsThresholdInteractionTime, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000328 RID: 808
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdLockTime = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdLockTime", DefaultSettings.Get.DiagnosticsThresholdLockTime, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000329 RID: 809
		public static ConfigurationSchema<int> DiagnosticsThresholdPagesDirtied = new ConfigurationSchema<int>("DiagnosticsThresholdPagesDirtied", DefaultSettings.Get.DiagnosticsThresholdPagesDirtied);

		// Token: 0x0400032A RID: 810
		public static ConfigurationSchema<int> DiagnosticsThresholdPagesPreread = new ConfigurationSchema<int>("DiagnosticsThresholdPagesPreread", DefaultSettings.Get.DiagnosticsThresholdPagesPreread);

		// Token: 0x0400032B RID: 811
		public static ConfigurationSchema<int> DiagnosticsThresholdPagesRead = new ConfigurationSchema<int>("DiagnosticsThresholdPagesRead", DefaultSettings.Get.DiagnosticsThresholdPagesRead);

		// Token: 0x0400032C RID: 812
		public static ConfigurationSchema<TimeSpan> DiagnosticsThresholdChunkElapsedTime = new ConfigurationSchema<TimeSpan>("DiagnosticsThresholdChunkElapsedTime", DefaultSettings.Get.DiagnosticsThresholdChunkElapsedTime, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400032D RID: 813
		public static ConfigurationSchema<int> MaximumNumberOfExceptions = new ConfigurationSchema<int>("MaximumNumberOfExceptions", DefaultSettings.Get.MaximumNumberOfExceptions, (int value) => Math.Min(100, value));

		// Token: 0x0400032E RID: 814
		public static ConfigurationSchema<UnlimitedItems> MailboxMessagesPerFolderCountWarningQuota = new ConfigurationSchema<UnlimitedItems>("MailboxMessagesPerFolderCountWarningQuota", DefaultSettings.Get.MailboxMessagesPerFolderCountWarningQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400032F RID: 815
		public static ConfigurationSchema<UnlimitedItems> MailboxMessagesPerFolderCountReceiveQuota = new ConfigurationSchema<UnlimitedItems>("MailboxMessagesPerFolderCountReceiveQuota", DefaultSettings.Get.MailboxMessagesPerFolderCountReceiveQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000330 RID: 816
		public static ConfigurationSchema<UnlimitedItems> DumpsterMessagesPerFolderCountWarningQuota = new ConfigurationSchema<UnlimitedItems>("DumpsterMessagesPerFolderCountWarningQuota", DefaultSettings.Get.DumpsterMessagesPerFolderCountWarningQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000331 RID: 817
		public static ConfigurationSchema<UnlimitedItems> DumpsterMessagesPerFolderCountReceiveQuota = new ConfigurationSchema<UnlimitedItems>("DumpsterMessagesPerFolderCountReceiveQuota", DefaultSettings.Get.DumpsterMessagesPerFolderCountReceiveQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000332 RID: 818
		public static ConfigurationSchema<UnlimitedItems> FolderHierarchyChildrenCountWarningQuota = new ConfigurationSchema<UnlimitedItems>("FolderHierarchyChildrenCountWarningQuota", DefaultSettings.Get.FolderHierarchyChildrenCountWarningQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000333 RID: 819
		public static ConfigurationSchema<UnlimitedItems> FolderHierarchyChildrenCountReceiveQuota = new ConfigurationSchema<UnlimitedItems>("FolderHierarchyChildrenCountReceiveQuota", DefaultSettings.Get.FolderHierarchyChildrenCountReceiveQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000334 RID: 820
		public static ConfigurationSchema<UnlimitedItems> FolderHierarchyDepthWarningQuota = new ConfigurationSchema<UnlimitedItems>("FolderHierarchyDepthWarningQuota", DefaultSettings.Get.FolderHierarchyDepthWarningQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000335 RID: 821
		public static ConfigurationSchema<UnlimitedItems> FolderHierarchyDepthReceiveQuota = new ConfigurationSchema<UnlimitedItems>("FolderHierarchyDepthReceiveQuota", DefaultSettings.Get.FolderHierarchyDepthReceiveQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000336 RID: 822
		public static ConfigurationSchema<UnlimitedItems> FoldersCountWarningQuota = new ConfigurationSchema<UnlimitedItems>("FoldersCountWarningQuota", DefaultSettings.Get.FoldersCountWarningQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000337 RID: 823
		public static ConfigurationSchema<UnlimitedItems> FoldersCountReceiveQuota = new ConfigurationSchema<UnlimitedItems>("FoldersCountReceiveQuota", DefaultSettings.Get.FoldersCountReceiveQuota, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000338 RID: 824
		public static ConfigurationSchema<UnlimitedItems> MaxChildCountForDumpsterHierarchyPublicFolder = new ConfigurationSchema<UnlimitedItems>("MaxChildCountForDumpsterHierarchyPublicFolder", DefaultSettings.Get.MaxChildCountForDumpsterHierarchyPublicFolder, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZeroButNotUnlimited));

		// Token: 0x04000339 RID: 825
		public static ConfigurationSchema<int> DestinationMailboxReservedCounterRangeConstant = new ConfigurationSchema<int>("DestinationMailboxReservedCounterRangeConstant", DefaultSettings.Get.DestinationMailboxReservedCounterRangeConstant);

		// Token: 0x0400033A RID: 826
		public static ConfigurationSchema<int> DestinationMailboxReservedCounterRangeGradient = new ConfigurationSchema<int>("DestinationMailboxReservedCounterRangeGradient", DefaultSettings.Get.DestinationMailboxReservedCounterRangeGradient);

		// Token: 0x0400033B RID: 827
		public static ConfigurationSchema<Dictionary<Guid, MailboxShape>> MailboxShapeQuotas = new ConfigurationSchema<Dictionary<Guid, MailboxShape>>("MailboxShapeQuotas", null, new ConfigurationSchema<Dictionary<Guid, MailboxShape>>.TryParse(MailboxShape.TryParse));

		// Token: 0x0400033C RID: 828
		public static ConfigurationSchema<bool> EnablePropertiesPromotionValidation = new ConfigurationSchema<bool>("EnablePropertiesPromotionValidation", DefaultSettings.Get.EnablePropertiesPromotionValidation);

		// Token: 0x0400033D RID: 829
		public static ConfigurationSchema<TimeSpan> SearchTraceFastOperationThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceFastOperationThreshold", DefaultSettings.Get.SearchTraceFastOperationThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400033E RID: 830
		public static ConfigurationSchema<TimeSpan> SearchTraceFastTotalThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceFastTotalThreshold", DefaultSettings.Get.SearchTraceFastTotalThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400033F RID: 831
		public static ConfigurationSchema<TimeSpan> SearchTraceStoreOperationThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceStoreOperationThreshold", DefaultSettings.Get.SearchTraceStoreOperationThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000340 RID: 832
		public static ConfigurationSchema<TimeSpan> SearchTraceStoreTotalThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceStoreTotalThreshold", DefaultSettings.Get.SearchTraceStoreTotalThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000341 RID: 833
		public static ConfigurationSchema<TimeSpan> SearchTraceFirstLinkedThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceFirstLinkedThreshold", DefaultSettings.Get.SearchTraceFirstLinkedThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000342 RID: 834
		public static ConfigurationSchema<TimeSpan> SearchTraceGetRestrictionThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceGetRestrictionThreshold", DefaultSettings.Get.SearchTraceGetRestrictionThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000343 RID: 835
		public static ConfigurationSchema<TimeSpan> SearchTraceGetQueryPlanThreshold = new ConfigurationSchema<TimeSpan>("SearchTraceGetQueryPlanThreshold", DefaultSettings.Get.SearchTraceGetQueryPlanThreshold, new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMilliseconds), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000344 RID: 836
		public static ConfigurationSchema<int> MaximumActivePropertyPromotions = new ConfigurationSchema<int>("MaximumActivePropertyPromotions", DefaultSettings.Get.MaximumActivePropertyPromotions, (int value) => Math.Max(1, Math.Min(100, value)));

		// Token: 0x04000345 RID: 837
		public static ConfigurationSchema<int> PerUserCacheSize = new ReadOnceConfigurationSchema<int>("PerUserCacheSize", 1000);

		// Token: 0x04000346 RID: 838
		public static ConfigurationSchema<TimeSpan> PerUserCacheExpiration = new ReadOnceConfigurationSchema<TimeSpan>("PerUserCacheExpirationInMinutes", TimeSpan.FromMinutes(10.0), new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromMinutes), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000347 RID: 839
		public static ConfigurationSchema<bool> EnableOptimizedConversationSearch = new ConfigurationSchema<bool>("EnableOptimizedConversationSearch", true);

		// Token: 0x04000348 RID: 840
		public static ConfigurationSchema<long> SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold = new ReadOnceConfigurationSchema<long>("SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold", DefaultSettings.Get.SubobjectCleanupUrgentMaintenanceNumberOfItemsThreshold);

		// Token: 0x04000349 RID: 841
		public static ConfigurationSchema<long> SubobjectCleanupUrgentMaintenanceTotalSizeThreshold = new ReadOnceConfigurationSchema<long>("SubobjectCleanupUrgentMaintenanceTotalSizeThreshold", DefaultSettings.Get.SubobjectCleanupUrgentMaintenanceTotalSizeThreshold);

		// Token: 0x0400034A RID: 842
		public static ConfigurationSchema<bool> DirectIndexUpdateInstrumentation = new ReadOnceConfigurationSchema<bool>("DirectIndexUpdateInstrumentation", false);

		// Token: 0x0400034B RID: 843
		public static ConfigurationSchema<bool> IndexUpdateBreadcrumbsInstrumentation = new ReadOnceConfigurationSchema<bool>("IndexUpdateBreadcrumbsInstrumentation", DefaultSettings.Get.IndexUpdateBreadcrumbsInstrumentation);

		// Token: 0x0400034C RID: 844
		public static ConfigurationSchema<bool> ChunkedIndexPopulationEnabled = new ReadOnceConfigurationSchema<bool>("ChunkedIndexPopulationEnabled", DefaultSettings.Get.ChunkedIndexPopulationEnabled);

		// Token: 0x0400034D RID: 845
		public static ConfigurationSchema<int> ChunkedIndexPopulationMinChunkTimeMilliseconds = new ReadOnceConfigurationSchema<int>("ChunkedIndexPopulationMinChunkTimeMilliseconds", 10);

		// Token: 0x0400034E RID: 846
		public static ConfigurationSchema<int> ChunkedIndexPopulationMaxChunkTimeMilliseconds = new ReadOnceConfigurationSchema<int>("ChunkedIndexPopulationMaxChunkTimeMilliseconds", 2000);

		// Token: 0x0400034F RID: 847
		public static ConfigurationSchema<int> ChunkedIndexPopulationFolderSizeThreshold = new ReadOnceConfigurationSchema<int>("ChunkedIndexPopulationFolderSizeThreshold", 300);

		// Token: 0x04000350 RID: 848
		public static ConfigurationSchema<int> EseStageFlighting = new ReadOnceConfigurationSchema<int>("EseStageFlighting", DefaultSettings.Get.EseStageFlighting);

		// Token: 0x04000351 RID: 849
		public static ConfigurationSchema<int> ChunkedIndexPopulationMaxWaiters = new ReadOnceConfigurationSchema<int>("ChunkedIndexPopulationMaxWaiters", 2);

		// Token: 0x04000352 RID: 850
		public static ConfigurationSchema<TimeSpan> InstanceStatusTimeout = new ReadOnceConfigurationSchema<TimeSpan>("InstanceStatusTimeoutSeconds", TimeSpan.FromSeconds(10.0), new ConfigurationSchema<TimeSpan>.TryParse(ConfigurationSchema.TryTimeSpanFromSeconds), (TimeSpan value) => TimeSpan.FromTicks(Math.Max(0L, Math.Min(TimeSpan.FromHours(1.0).Ticks, value.Ticks))));

		// Token: 0x04000353 RID: 851
		public static ConfigurationSchema<bool> DisableIndexCorruptionAssertThrottling = new ReadOnceConfigurationSchema<bool>("DisableIndexCorruptionAssertThrottling", StoreEnvironment.IsTestEnvironment);

		// Token: 0x04000354 RID: 852
		public static ConfigurationSchema<int> MapiMessageMaxSupportedAttachmentCount = new ConfigurationSchema<int>("MapiMessageMaxSupportedAttachmentCount", 1024);

		// Token: 0x04000355 RID: 853
		public static ConfigurationSchema<int> MapiMessageMaxSupportedDescendantCount = new ConfigurationSchema<int>("MapiMessageMaxSupportedDescendantCount", 2048);

		// Token: 0x04000356 RID: 854
		public static ConfigurationSchema<bool> EnableMaterializedRestriction = new ConfigurationSchema<bool>("EnableMaterializedRestriction", DefaultSettings.Get.EnableMaterializedRestriction);

		// Token: 0x04000357 RID: 855
		public static ConfigurationSchema<ulong> MinRangeSizeForCnRestriction = new ConfigurationSchema<ulong>("MinRangeSizeForCnRestriction", 10000UL);

		// Token: 0x04000358 RID: 856
		public static ConfigurationSchema<int> GetChangedMessagesBatchSize = new ConfigurationSchema<int>("GetChangedMessagesBatchSize", 100);

		// Token: 0x04000359 RID: 857
		public static ConfigurationSchema<TimeSpan> IntegrityCheckJobAgeoutTimeSpan = new ConfigurationSchema<TimeSpan>("IntegrityCheckJobAgeoutTimeSpan", DefaultSettings.Get.IntegrityCheckJobAgeoutTimeSpan, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400035A RID: 858
		public static ConfigurationSchema<int> IntegrityCheckJobStorageCapacity = new ConfigurationSchema<int>("IntegrityCheckJobStorageCapacity", 1000);

		// Token: 0x0400035B RID: 859
		public static ConfigurationSchema<TimeSpan> DiscoverPotentiallyExpiredMailboxesTaskPeriod = new ReadOnceConfigurationSchema<TimeSpan>("DiscoverPotentiallyExpiredMailboxesTaskPeriod", DefaultSettings.Get.DiscoverPotentiallyExpiredMailboxesTaskPeriod, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400035C RID: 860
		public static ConfigurationSchema<TimeSpan> DiscoverPotentiallyDisabledMailboxesTaskPeriod = new ReadOnceConfigurationSchema<TimeSpan>("DiscoverPotentiallyDisabledMailboxesTaskPeriod", DefaultSettings.Get.DiscoverPotentiallyDisabledMailboxesTaskPeriod, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400035D RID: 861
		public static ConfigurationSchema<TimeSpan> IdleAccessibleMailboxTime = new ConfigurationSchema<TimeSpan>("IdleAccessibleMailboxTime", DefaultSettings.Get.IdleAccessibleMailboxesTime, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400035E RID: 862
		public static ConfigurationSchema<int> ProcessorNumberOfColumnResults = new ConfigurationSchema<int>("ProcessorNumberOfColumnResults", DefaultSettings.Get.ProcessorNumberOfColumnResults);

		// Token: 0x0400035F RID: 863
		public static ConfigurationSchema<int> ProcessorNumberOfPropertyResults = new ConfigurationSchema<int>("ProcessorNumberOfPropertyResults", DefaultSettings.Get.ProcessorNumberOfPropertyResults);

		// Token: 0x04000360 RID: 864
		public static ConfigurationSchema<int> DynamicSearchFolderPerScopeCountReceiveQuota = new ConfigurationSchema<int>("DynamicSearchFolderPerScopeCountReceiveQuota", DefaultSettings.Get.DynamicSearchFolderPerScopeCountReceiveQuota);

		// Token: 0x04000361 RID: 865
		public static ConfigurationSchema<bool> DisableSchemaUpgraders = new ConfigurationSchema<bool>("DisableSchemaUpgraders", false);

		// Token: 0x04000362 RID: 866
		public static ConfigurationSchema<ComponentVersion> MaximumRequestableSchemaVersion = new ConfigurationSchema<ComponentVersion>("MaximumRequestableSchemaVersion", DefaultSettings.Get.MaximumRequestableDatabaseSchemaVersion, new ConfigurationSchema<ComponentVersion>.TryParse(ComponentVersion.TryParse));

		// Token: 0x04000363 RID: 867
		public static ConfigurationSchema<int> DefaultMailboxSharedObjectPropertyBagDataCacheSize = new ConfigurationSchema<int>("DefaultMailboxSharedObjectPropertyBagDataCacheSize", DefaultSettings.Get.DefaultMailboxSharedObjectPropertyBagDataCacheSize);

		// Token: 0x04000364 RID: 868
		public static ConfigurationSchema<int> PublicFolderMailboxSharedObjectPropertyBagDataCacheSize = new ConfigurationSchema<int>("PublicFolderMailboxSharedObjectPropertyBagDataCacheSize", DefaultSettings.Get.PublicFolderMailboxSharedObjectPropertyBagDataCacheSize);

		// Token: 0x04000365 RID: 869
		public static ConfigurationSchema<int> SharedObjectPropertyBagDataCacheCleanupMultiplier = new ConfigurationSchema<int>("SharedObjectPropertyBagDataCacheCleanupMultiplier", DefaultSettings.Get.SharedObjectPropertyBagDataCacheCleanupMultiplier);

		// Token: 0x04000366 RID: 870
		public static ConfigurationSchema<TimeSpan> SharedObjectPropertyBagDataCacheTimeToLive = new ConfigurationSchema<TimeSpan>("SharedObjectPropertyBagDataCacheTimeToLive", TimeSpan.FromMinutes(15.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000367 RID: 871
		public static ConfigurationSchema<int> LogicalIndexCacheSize = new ConfigurationSchema<int>("LogicalIndexCacheSize", 24);

		// Token: 0x04000368 RID: 872
		public static ConfigurationSchema<TimeSpan> LogicalIndexCacheTimeToLive = new ConfigurationSchema<TimeSpan>("LogicalIndexCacheTimeToLive", TimeSpan.FromMinutes(15.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000369 RID: 873
		public static ConfigurationSchema<TimeSpan> MaxIdleCleanupPeriod = new ReadOnceConfigurationSchema<TimeSpan>("MaxIdleCleanupPeriod", TimeSpan.FromDays(30.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400036A RID: 874
		public static ConfigurationSchema<int> StopMaintenanceThreshold = new ReadOnceConfigurationSchema<int>("StopMaintenanceThreshold", 50000);

		// Token: 0x0400036B RID: 875
		public static ConfigurationSchema<int> WlmMaintenanceThreshold = new ReadOnceConfigurationSchema<int>("WlmMaintenanceThreshold", 100000);

		// Token: 0x0400036C RID: 876
		public static ConfigurationSchema<int> NumberOfRecordsToMaintain = new ReadOnceConfigurationSchema<int>("NumberOfRecordsToMaintain", 5000);

		// Token: 0x0400036D RID: 877
		public static ConfigurationSchema<int> NumberOfRecordsToReadFromMaintenanceTable = new ReadOnceConfigurationSchema<int>("NumberOfRecordsToReadFromMaintenanceTable", 20000);

		// Token: 0x0400036E RID: 878
		public static ConfigurationSchema<TimeSpan> MaintenanceTimePeriodToKeep = new ReadOnceConfigurationSchema<TimeSpan>("MaintenanceTimePeriodToKeep", TimeSpan.FromDays(30.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400036F RID: 879
		public static ConfigurationSchema<int> WlmMinNumberOfChunksToProceed = new ReadOnceConfigurationSchema<int>("WlmMinNumberOfChunksToProceed", 1);

		// Token: 0x04000370 RID: 880
		public static ConfigurationSchema<TimeSpan> TombstoneMailboxExpirationPeriod = new ReadOnceConfigurationSchema<TimeSpan>("TombstoneMailboxExpirationPeriod", TimeSpan.FromDays(30.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000371 RID: 881
		public static ConfigurationSchema<int> PromotionChunkSize = new ReadOnceConfigurationSchema<int>("PromotionChunkSize", 250);

		// Token: 0x04000372 RID: 882
		public static ConfigurationSchema<int> ActiveMailboxCacheSize = new ConfigurationSchema<int>("ActiveMailboxCacheSize", 1000);

		// Token: 0x04000373 RID: 883
		public static ConfigurationSchema<TimeSpan> ActiveMailboxCacheTimeToLive = new ConfigurationSchema<TimeSpan>("ActiveMailboxCacheTimeToLive", TimeSpan.FromMinutes(30.0), ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x04000374 RID: 884
		public static ConfigurationSchema<int> ActiveMailboxCacheCleanupThreshold = new ConfigurationSchema<int>("ActiveMailboxCacheCleanupThreshold", 5);

		// Token: 0x04000375 RID: 885
		public static ConfigurationSchema<bool> AggresiveUpdateMailboxTableSizeStatistics = new ReadOnceConfigurationSchema<bool>("AggresiveUpdateMailboxTableSizeStatistics", false);

		// Token: 0x04000376 RID: 886
		public static ConfigurationSchema<int> StoreQueryLimitRows = new ReadOnceConfigurationSchema<int>("StoreQueryLimitRows", DefaultSettings.Get.StoreQueryLimitRows);

		// Token: 0x04000377 RID: 887
		public static ConfigurationSchema<TimeSpan> StoreQueryLimitTime = new ReadOnceConfigurationSchema<TimeSpan>("StoreQueryLimitTime", DefaultSettings.Get.StoreQueryLimitTime);

		// Token: 0x04000378 RID: 888
		public static ConfigurationSchema<bool> EnableReadFromPassive = new ReadOnceConfigurationSchema<bool>("EnableReadFromPassive", DefaultSettings.Get.EnableReadFromPassive);

		// Token: 0x04000379 RID: 889
		public static ConfigurationSchema<TimeSpan> InferenceLogRetentionPeriod = new ReadOnceConfigurationSchema<TimeSpan>("InferenceLogRetentionPeriod", DefaultSettings.Get.InferenceLogRetentionPeriod, ConfigurationSchema.NoPostProcess<TimeSpan>(), new Func<TimeSpan, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400037A RID: 890
		public static ConfigurationSchema<UnlimitedItems> InferenceLogMaxRows = new ConfigurationSchema<UnlimitedItems>("InferenceLogMaxRows", DefaultSettings.Get.InferenceLogMaxRows, new ConfigurationSchema<UnlimitedItems>.TryParse(UnlimitedItems.TryParse), ConfigurationSchema.NoPostProcess<UnlimitedItems>(), new Func<UnlimitedItems, bool>(ConfigurationSchema.ValueGreaterThanZero));

		// Token: 0x0400037B RID: 891
		public static ConfigurationSchema<bool> EnableDbDivergenceDetection = new ReadOnceConfigurationSchema<bool>("EnableDbDivergenceDetection", DefaultSettings.Get.EnableDbDivergenceDetection);

		// Token: 0x0400037C RID: 892
		public static ConfigurationSchema<bool> EnableConversationMasterIndex = new ReadOnceConfigurationSchema<bool>("EnableConversationMasterIndex", DefaultSettings.Get.EnableConversationMasterIndex);

		// Token: 0x0400037D RID: 893
		public static ConfigurationSchema<bool> ForceConversationMasterIndexUpgrade = new ReadOnceConfigurationSchema<bool>("ForceConversationMasterIndexUpgrade", DefaultSettings.Get.ForceConversationMasterIndexUpgrade);

		// Token: 0x0400037E RID: 894
		public static ConfigurationSchema<bool> ForceRimQueryMaterialization = new ReadOnceConfigurationSchema<bool>("ForceRimQueryMaterialization", DefaultSettings.Get.ForceRimQueryMaterialization);

		// Token: 0x0400037F RID: 895
		public static ConfigurationSchema<TimeSpan> LazyTransactionCommitTimeout = new ReadOnceConfigurationSchema<TimeSpan>("LazyTransactionCommitTimeout", DefaultSettings.Get.LazyTransactionCommitTimeout);

		// Token: 0x04000380 RID: 896
		public static ConfigurationSchema<bool> ScheduledISIntegEnabled = new ConfigurationSchema<bool>("ScheduledISIntegEnabled", DefaultSettings.Get.ScheduledISIntegEnabled);

		// Token: 0x04000381 RID: 897
		public static ConfigurationSchema<string> ScheduledISIntegDetectOnly = new ConfigurationSchema<string>("ScheduledISIntegDetectOnly", DefaultSettings.Get.ScheduledISIntegDetectOnly);

		// Token: 0x04000382 RID: 898
		public static ConfigurationSchema<string> ScheduledISIntegDetectAndFix = new ConfigurationSchema<string>("ScheduledISIntegDetectAndFix", DefaultSettings.Get.ScheduledISIntegDetectAndFix);

		// Token: 0x04000383 RID: 899
		public static ConfigurationSchema<int> StoreQueryMaximumResultSize = new ConfigurationSchema<int>("StoreQueryMaximumResultSize", 5242880);

		// Token: 0x04000384 RID: 900
		public static ConfigurationSchema<uint> DatabaseSizeLimitGB = new ConfigurationSchema<uint>("DatabaseSizeLimitGB", 0U, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}", "Database Size Limit in Gb");

		// Token: 0x04000385 RID: 901
		public static ConfigurationSchema<uint> DatabaseWarningThresholdPercent = new ConfigurationSchema<uint>("DatabaseWarningThresholdPercent", 10U, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}", "Database Size Buffer in Percentage");

		// Token: 0x04000386 RID: 902
		public static ConfigurationSchema<bool> SkipMoveEventExclusion = new ConfigurationSchema<bool>("SkipMoveEventExclusion", false, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "SkipMoveEventExclusion");

		// Token: 0x04000387 RID: 903
		public static ConfigurationSchema<bool> DisableSearchFolderAgeOut = new ConfigurationSchema<bool>("DisableSearchFolderAgeOut", false, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Disable Search Folder Age-Out");

		// Token: 0x04000388 RID: 904
		public static ConfigurationSchema<bool> CheckStreamSize = new ConfigurationSchema<bool>("CheckStreamSize", true, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Check stream size");

		// Token: 0x04000389 RID: 905
		public static ConfigurationSchema<long> PerUserSessionLimit = new ConfigurationSchema<long>("PerUserSessionLimit", 32L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 32L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Maximum Allowed Sessions Per User");

		// Token: 0x0400038A RID: 906
		public static ConfigurationSchema<long> PerAdminSessionLimit = new ConfigurationSchema<long>("PerAdminSessionLimit", 65536L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 65536L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Maximum Allowed Sessions Per Administrator");

		// Token: 0x0400038B RID: 907
		public static ConfigurationSchema<long> PerOtherSessionLimit = new ConfigurationSchema<long>("PerOtherSessionLimit", 16L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 16L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Maximum Allowed Service Sessions Per User");

		// Token: 0x0400038C RID: 908
		public static ConfigurationSchema<long> PerServiceSessionLimit = new ConfigurationSchema<long>("PerServiceSessionLimit", 10000L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 10000L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "Maximum Allowed Exchange Sessions Per Service");

		// Token: 0x0400038D RID: 909
		public static ConfigurationSchema<long> PerSessionFolderLimit = new ConfigurationSchema<long>("PerSessionFolderLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtFolder");

		// Token: 0x0400038E RID: 910
		public static ConfigurationSchema<long> PerSessionMessageLimit = new ConfigurationSchema<long>("PerSessionMessageLimit", 250L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 250L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtMessage");

		// Token: 0x0400038F RID: 911
		public static ConfigurationSchema<long> PerSessionAttachmentLimit = new ConfigurationSchema<long>("PerSessionAttachmentLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtAttachment");

		// Token: 0x04000390 RID: 912
		public static ConfigurationSchema<long> PerSessionStreamLimit = new ConfigurationSchema<long>("PerSessionStreamLimit", 250L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 250L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtStream");

		// Token: 0x04000391 RID: 913
		public static ConfigurationSchema<long> PerSessionNotifyLimit = new ConfigurationSchema<long>("PerSessionNotifyLimit", 500000L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500000L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtNotify");

		// Token: 0x04000392 RID: 914
		public static ConfigurationSchema<long> PerSessionFolderViewLimit = new ConfigurationSchema<long>("PerSessionFolderViewLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtFolderView");

		// Token: 0x04000393 RID: 915
		public static ConfigurationSchema<long> PerSessionMessageViewLimit = new ConfigurationSchema<long>("PerSessionMessageViewLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtMessageView");

		// Token: 0x04000394 RID: 916
		public static ConfigurationSchema<long> PerSessionAttachmentViewLimit = new ConfigurationSchema<long>("PerSessionAttachmentViewLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtAttachView");

		// Token: 0x04000395 RID: 917
		public static ConfigurationSchema<long> PerSessionACLViewLimit = new ConfigurationSchema<long>("PerSessionACLViewLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtACLView");

		// Token: 0x04000396 RID: 918
		public static ConfigurationSchema<long> PerSessionFxSrcLimit = new ConfigurationSchema<long>("PerSessionFxSrcLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtFXSrcStrm");

		// Token: 0x04000397 RID: 919
		public static ConfigurationSchema<long> PerSessionFxDstLimit = new ConfigurationSchema<long>("PerSessionFxDstLimit", 500L, (long value) => ConfigurationSchema.ObjectLimitsPostProcess(value, 500L), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem\\MaxObjsPerMapiSession", "objtFXDstStrm");

		// Token: 0x04000398 RID: 920
		public static ConfigurationSchema<ushort> MAPINamedPropsQuota = new ConfigurationSchema<ushort>("MAPINamedPropsQuota", 16384, (ushort value) => Math.Min(value, 32767), "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\{0}\\{1}-{2}", "Named Props Quota");

		// Token: 0x04000399 RID: 921
		public static ConfigurationSchema<int> MailboxMaintenanceIntervalMinutes = new ReadOnceConfigurationSchema<int>("MailboxMaintenanceIntervalMinutes", (int)TimeSpan.FromDays(1.0).TotalMinutes, "SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ParametersSystem", "MailboxMaintenanceIntervalMinutes");

		// Token: 0x0400039A RID: 922
		public static ConfigurationSchema<bool> EnableDatabaseUnusedSpaceScrubbing = new ReadOnceConfigurationSchema<bool>("EnableDatabaseUnusedSpaceScrubbing", DefaultSettings.Get.EnableDatabaseUnusedSpaceScrubbing);

		// Token: 0x0400039B RID: 923
		public static ConfigurationSchema<TimeSpan> ScheduledISIntegExecutePeriod = new ReadOnceConfigurationSchema<TimeSpan>("ScheduledISIntegExecutePeriod", DefaultSettings.Get.ScheduledISIntegExecutePeriod, ConfigurationSchema.NoPostProcess<TimeSpan>(), (TimeSpan value) => value > (StoreEnvironment.IsTestEnvironment ? TimeSpan.FromDays(0.0) : TimeSpan.FromDays(1.0)));

		// Token: 0x0400039C RID: 924
		public static ConfigurationSchema<int> ConfigurableSharedLockStage = new ConfigurationSchema<int>("ConfigurableSharedLockStage", DefaultSettings.Get.ConfigurableSharedLockStage);

		// Token: 0x0400039D RID: 925
		public static ConfigurationSchema<TimeSpan> EseLrukCorrInterval = new ReadOnceConfigurationSchema<TimeSpan>("EseLrukCorrInterval", DefaultSettings.Get.EseLrukCorrInterval);

		// Token: 0x0400039E RID: 926
		public static ConfigurationSchema<TimeSpan> EseLrukTimeout = new ReadOnceConfigurationSchema<TimeSpan>("EseLrukTimeout", DefaultSettings.Get.EseLrukTimeout);

		// Token: 0x0400039F RID: 927
		public static ConfigurationSchema<bool> DisableBypassTempStream = new ConfigurationSchema<bool>("DisableBypassTempStream", DefaultSettings.Get.DisableBypassTempStream);

		// Token: 0x040003A0 RID: 928
		public static ConfigurationSchema<int> MaxIcsStatePropertySize = new ConfigurationSchema<int>("MaxIcsStatePropertySize", 11534336);

		// Token: 0x040003A1 RID: 929
		public static ConfigurationSchema<TimeSpan> ADOperationTimeout = new ReadOnceConfigurationSchema<TimeSpan>("ADOperationTimeout", TimeSpan.FromMinutes(30.0));

		// Token: 0x040003A2 RID: 930
		public static ConfigurationSchema<TimeSpan> DatabaseOperationTimeout = new ReadOnceConfigurationSchema<TimeSpan>("DatabaseOperationTimeout", TimeSpan.FromMinutes(10.0));

		// Token: 0x040003A3 RID: 931
		public static ConfigurationSchema<int> MaxNumberOfMapiProperties = new ConfigurationSchema<int>("MaxNumberOfMapiProperties", 7168);

		// Token: 0x040003A4 RID: 932
		public static ConfigurationSchema<bool> CleanupTempStreamBuffersOnRelease = new ReadOnceConfigurationSchema<bool>("CleanupTempStreamBuffersOnRelease", true);

		// Token: 0x040003A5 RID: 933
		public static ConfigurationSchema<bool> CheckQuotaOnMessageCreate = new ConfigurationSchema<bool>("CheckQuotaOnMessageCreate", DefaultSettings.Get.CheckQuotaOnMessageCreate);

		// Token: 0x040003A6 RID: 934
		public static ConfigurationSchema<bool> ValidatePublicFolderMailboxTypeMatch = new ConfigurationSchema<bool>("ValidatePublicFolderMailboxTypeMatch", true);

		// Token: 0x040003A7 RID: 935
		public static ConfigurationSchema<int> MailboxInfoCacheSize = new ReadOnceConfigurationSchema<int>("MailboxInfoCacheSize", DefaultSettings.Get.MailboxInfoCacheSize);

		// Token: 0x040003A8 RID: 936
		public static ConfigurationSchema<TimeSpan> MailboxInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("MailboxInfoCacheTTL", TimeSpan.FromMinutes(15.0));

		// Token: 0x040003A9 RID: 937
		public static ConfigurationSchema<int> ForeignMailboxInfoCacheSize = new ReadOnceConfigurationSchema<int>("ForeignMailboxInfoCacheSize", DefaultSettings.Get.ForeignMailboxInfoCacheSize);

		// Token: 0x040003AA RID: 938
		public static ConfigurationSchema<TimeSpan> ForeignMailboxInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("ForeignMailboxInfoCacheTTL", TimeSpan.FromMinutes(15.0));

		// Token: 0x040003AB RID: 939
		public static ConfigurationSchema<int> AddressInfoCacheSize = new ReadOnceConfigurationSchema<int>("AddressInfoCacheSize", DefaultSettings.Get.AddressInfoCacheSize);

		// Token: 0x040003AC RID: 940
		public static ConfigurationSchema<TimeSpan> AddressInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("AddressInfoCacheTTL", TimeSpan.FromMinutes(15.0));

		// Token: 0x040003AD RID: 941
		public static ConfigurationSchema<int> ForeignAddressInfoCacheSize = new ReadOnceConfigurationSchema<int>("ForeignAddressInfoCacheSize", DefaultSettings.Get.ForeignAddressInfoCacheSize);

		// Token: 0x040003AE RID: 942
		public static ConfigurationSchema<TimeSpan> ForeignAddressInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("ForeignAddressInfoCacheTTL", TimeSpan.FromMinutes(15.0));

		// Token: 0x040003AF RID: 943
		public static ConfigurationSchema<int> DatabaseInfoCacheSize = new ReadOnceConfigurationSchema<int>("DatabaseInfoCacheSize", DefaultSettings.Get.DatabaseInfoCacheSize);

		// Token: 0x040003B0 RID: 944
		public static ConfigurationSchema<TimeSpan> DatabaseInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("DatabaseInfoCacheTTL", TimeSpan.FromMinutes(30.0));

		// Token: 0x040003B1 RID: 945
		public static ConfigurationSchema<int> OrganizationContainerCacheSize = new ReadOnceConfigurationSchema<int>("OrganizationContainerCacheSize", DefaultSettings.Get.OrganizationContainerCacheSize);

		// Token: 0x040003B2 RID: 946
		public static ConfigurationSchema<TimeSpan> OrganizationContainerCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("OrganizationContainerCacheTTL", TimeSpan.FromMinutes(15.0));

		// Token: 0x040003B3 RID: 947
		public static ConfigurationSchema<bool> UseDirectorySharedCache = new ConfigurationSchema<bool>("UseDirectorySharedCache", DefaultSettings.Get.UseDirectorySharedCache);

		// Token: 0x040003B4 RID: 948
		public static ConfigurationSchema<bool> SeparateDirectoryCaches = new ConfigurationSchema<bool>("SeparateDirectoryCaches", true);

		// Token: 0x040003B5 RID: 949
		public static ConfigurationSchema<int> MaterializedRestrictionSearchFolderConfigStage = new ReadOnceConfigurationSchema<int>("MaterializedRestrictionSearchFolderConfigStage", DefaultSettings.Get.MaterializedRestrictionSearchFolderConfigStage, ConfigurationSchema.NoPostProcess<int>(), (int value) => value >= 0 && value <= 1);

		// Token: 0x040003B6 RID: 950
		public static ConfigurationSchema<bool> AllowRecursiveFolderHierarchyRowCountApproximation = new ConfigurationSchema<bool>("AllowRecursiveFolderHierarchyRowCountApproximation", true);

		// Token: 0x040003B7 RID: 951
		public static ConfigurationSchema<TimeSpan> ServerInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("ServerInfoCacheTTL", TimeSpan.FromMinutes(10.0));

		// Token: 0x040003B8 RID: 952
		public static ConfigurationSchema<TimeSpan> TransportInfoCacheTTL = new ReadOnceConfigurationSchema<TimeSpan>("TransportInfoCacheTTL", TimeSpan.FromMinutes(10.0));

		// Token: 0x040003B9 RID: 953
		public static ConfigurationSchema<bool> AttachmentMessageSaveChunking = new ConfigurationSchema<bool>("AttachmentMessageSaveChunking", DefaultSettings.Get.AttachmentMessageSaveChunking);

		// Token: 0x040003BA RID: 954
		public static ConfigurationSchema<int> AttachmentMessageSaveChunkingMinSize = new ReadOnceConfigurationSchema<int>("AttachmentMessageSaveChunkingMinSize", 262144);

		// Token: 0x040003BB RID: 955
		public static ConfigurationSchema<bool> EnableSetSenderSpoofingFix = new ConfigurationSchema<bool>("EnableSetSenderSpoofingFix", true);

		// Token: 0x040003BC RID: 956
		public static ConfigurationSchema<bool> FixMessageCreatorSidOnMailboxMove = new ConfigurationSchema<bool>("FixMessageCreatorSidOnMailboxMove", true);

		// Token: 0x040003BD RID: 957
		public static ConfigurationSchema<int> TimeZoneBlobTruncationLength = new ConfigurationSchema<int>("TimeZoneBlobTruncationLength", DefaultSettings.Get.TimeZoneBlobTruncationLength, ConfigurationSchema.NoPostProcess<int>(), (int value) => value >= 510 && value <= 5120);

		// Token: 0x040003BE RID: 958
		public static ConfigurationSchema<int> MaxHitsForFullTextIndexSearches = new ConfigurationSchema<int>("MaxHitsForFullTextIndexSearches", 250, ConfigurationSchema.NoPostProcess<int>(), (int value) => value > 0 && value <= 1000000);

		// Token: 0x040003BF RID: 959
		public static ConfigurationSchema<bool> MultipleSyncSourceClientsForPublicFolderMailbox = new ConfigurationSchema<bool>("MultipleSyncSourceClientsForPublicFolderMailbox", true);

		// Token: 0x040003C0 RID: 960
		private static List<ConfigurationSchema> registeredConfigurations;

		// Token: 0x040003C1 RID: 961
		private static Task reloadTask;

		// Token: 0x040003C2 RID: 962
		private static DateTime nextRefreshTime;

		// Token: 0x040003C3 RID: 963
		private readonly string name;
	}
}
