using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x0200016B RID: 363
	internal class DataStorePeriodicChecker : TimerComponent
	{
		// Token: 0x06000E96 RID: 3734 RVA: 0x0003E5D4 File Offset: 0x0003C7D4
		public DataStorePeriodicChecker(int startupDelayInSec, int intervalInSec) : base(TimeSpan.FromSeconds((double)startupDelayInSec), TimeSpan.FromSeconds((double)intervalInSec), "Database Consistency Checker")
		{
			this.lastRecordedDueTime = DateTimeOffset.MinValue;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003E5FA File Offset: 0x0003C7FA
		public DataStorePeriodicChecker() : this(RegistryParameters.DistributedStoreConsistencyStartupDelayInSecs, RegistryParameters.DistributedStoreConsistencyCheckPeriodicIntervalInSecs)
		{
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003E60C File Offset: 0x0003C80C
		protected override void TimerCallbackInternal()
		{
			AmConfig config = AmSystemManager.Instance.Config;
			if (config != null && config.IsPAM)
			{
				this.CheckDataStoresBestEffort();
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003E638 File Offset: 0x0003C838
		internal T GetDxStorePrivateProperty<T>(string propertyName, T defaultValue)
		{
			T value;
			using (IDistributedStoreKey baseKey = DistributedStore.Instance.DxStoreKeyFactoryInstance.GetBaseKey(DxStoreKeyAccessMode.Read, null, null, true))
			{
				value = baseKey.GetValue(propertyName, defaultValue, null);
			}
			return value;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003E680 File Offset: 0x0003C880
		internal void SetDxStorePrivateProperty<T>(string propertyName, T propertyValue)
		{
			using (IDistributedStoreKey baseKey = DistributedStore.Instance.DxStoreKeyFactoryInstance.GetBaseKey(DxStoreKeyAccessMode.Write, null, null, true))
			{
				baseKey.SetValue(propertyName, propertyValue, false, null);
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003E6C8 File Offset: 0x0003C8C8
		internal DateTimeOffset GetLastAnalyzeTime()
		{
			string dxStorePrivateProperty = this.GetDxStorePrivateProperty<string>("LastSuccessfulDataStoreAnalyzeTime", string.Empty);
			if (string.IsNullOrWhiteSpace(dxStorePrivateProperty))
			{
				return DateTimeOffset.MinValue;
			}
			DateTimeOffset result;
			if (!DateTimeOffset.TryParse(dxStorePrivateProperty, out result))
			{
				result = DateTimeOffset.MinValue;
			}
			result = result.ToLocalTime();
			return result;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0003E710 File Offset: 0x0003C910
		internal void SetLastAnalyzeTimeToNow()
		{
			this.SetDxStorePrivateProperty<string>("LastSuccessfulDataStoreAnalyzeTime", DateTimeOffset.UtcNow.ToString("o"));
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0003E744 File Offset: 0x0003C944
		internal void CheckDataStoresBestEffort()
		{
			Exception ex = Utils.RunBestEffort(delegate
			{
				this.CheckDataStores();
			});
			if (ex != null)
			{
				DxStoreHACrimsonEvents.DataStoreValidationFailed.Log<string, Exception>("CheckDataStores() failed.", ex);
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0003E7AC File Offset: 0x0003C9AC
		internal void CheckDataStores()
		{
			IActiveManagerSettings settings = DxStoreSetting.Instance.GetSettings();
			if (!settings.DxStoreIsPeriodicFixupEnabled || settings.DxStoreRunMode != DxStoreMode.Shadow)
			{
				return;
			}
			DateTimeOffset lastUpdateTime = DateTimeOffset.MinValue;
			DataStoreSnapshotAnalyzer analyzer = new DataStoreSnapshotAnalyzer((DiffReportVerboseMode)RegistryParameters.DistributedStoreDiffReportVerboseFlags);
			string text = null;
			if (!analyzer.IsPaxosConfiguredAndLeaderExist(out text))
			{
				if (!this.isPaxosNotReadyLogged)
				{
					DxStoreHACrimsonEvents.DataStoreValidationSkipped.Log<string, string>("Paxos either not configured or paxos leader does not exist", text);
					this.isPaxosNotReadyLogged = true;
				}
				return;
			}
			this.isPaxosNotReadyLogged = false;
			Exception ex = Utils.RunBestEffort(delegate
			{
				lastUpdateTime = this.GetLastAnalyzeTime();
			});
			if (ex != null)
			{
				if (!this.isLastAnalyzeTimeFailureLogged)
				{
					DxStoreHACrimsonEvents.DataStoreValidationFailed.Log<string, Exception>("GetLastAnalyzeTime", ex);
					this.isLastAnalyzeTimeFailureLogged = true;
				}
				return;
			}
			this.isLastAnalyzeTimeFailureLogged = false;
			DateTimeOffset now = DateTimeOffset.Now;
			DateTimeOffset dateTimeOffset = now;
			if (lastUpdateTime != DateTimeOffset.MinValue)
			{
				dateTimeOffset = lastUpdateTime.Add(TimeSpan.FromSeconds((double)RegistryParameters.DistributedStoreConsistencyVerifyIntervalInSecs));
			}
			if (dateTimeOffset > now)
			{
				if (this.lastRecordedDueTime < dateTimeOffset)
				{
					DxStoreHACrimsonEvents.DataStoreValidationSkipped.Log<string, DateTimeOffset>("Time not elapsed", dateTimeOffset);
					this.lastRecordedDueTime = dateTimeOffset;
				}
				return;
			}
			ex = Utils.RunBestEffort(delegate
			{
				analyzer.AnalyzeDataStores();
			});
			string timingInfoAsString = analyzer.GetTimingInfoAsString();
			if (ex != null)
			{
				DxStoreHACrimsonEvents.DataStoreValidationFailed.Log<string, Exception>(string.Format("AnalyzeDataStores() failed. Phase: {0}, Timing: {1}", analyzer.AnalysisPhase, timingInfoAsString), ex);
				return;
			}
			DataStoreDiffReport report = analyzer.Container.Report;
			DxStoreHACrimsonEvents.DataStoreValidationCompleted.Log<bool, string>(report.IsEverythingMatches, timingInfoAsString);
			analyzer.LogDiffDetailsToEventLog();
			this.SetLastAnalyzeTimeToNow();
			bool flag = false;
			if (!report.IsEverythingMatches)
			{
				if (RegistryParameters.DistributedStoreDisableDxStoreFixUp)
				{
					DxStoreHACrimsonEvents.DataStoreValidationSkipped.Log<string, string>("Database fixup skipped since it is disabled in registry", string.Empty);
				}
				else if (report.TotalClusdbPropertiesCount > 0)
				{
					if (!this.IsLastLogPropertiesAreTheOnlyDifference(report))
					{
						flag = true;
					}
					else
					{
						DxStoreHACrimsonEvents.DataStoreValidationSkipped.Log<string, string>("Database fixup skipped since last log properties are the only entries that have changed", string.Empty);
					}
				}
				else
				{
					DxStoreHACrimsonEvents.DataStoreValidationSkipped.Log<string, string>("Database fixup skipped since clusdb does not have single property", string.Empty);
				}
			}
			if (flag)
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				ex = Utils.RunBestEffort(delegate
				{
					analyzer.CopyClusdbSnapshotToDxStore();
				});
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				if (ex != null)
				{
					DxStoreHACrimsonEvents.DataStoreFailedToUpdate.Log<long, Exception>(stopwatch.ElapsedMilliseconds, ex);
					return;
				}
				DxStoreHACrimsonEvents.DataStoreSuccessfulyUpdated.Log<long, string>(stopwatch.ElapsedMilliseconds, string.Empty);
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0003EA30 File Offset: 0x0003CC30
		internal bool IsLastLogPropertiesAreTheOnlyDifference(DataStoreDiffReport report)
		{
			if (report.CountKeysOnlyInClusdb == 0 && report.CountKeysOnlyInDxStore == 0 && report.CountKeysInClusdbAndDxStoreButPropertiesDifferent == 1)
			{
				DataStoreMergedContainer.KeyEntry keyEntry = report.KeysInBothButPropertiesMismatch.FirstOrDefault<DataStoreMergedContainer.KeyEntry>();
				if (keyEntry != null && Utils.IsEqual(keyEntry.Name, "\\ExchangeActiveManager\\LastLog", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000603 RID: 1539
		public const string LastSuccessfulAnalyzeTimePropertyName = "LastSuccessfulDataStoreAnalyzeTime";

		// Token: 0x04000604 RID: 1540
		private DateTimeOffset lastRecordedDueTime;

		// Token: 0x04000605 RID: 1541
		private bool isPaxosNotReadyLogged;

		// Token: 0x04000606 RID: 1542
		private bool isLastAnalyzeTimeFailureLogged;
	}
}
