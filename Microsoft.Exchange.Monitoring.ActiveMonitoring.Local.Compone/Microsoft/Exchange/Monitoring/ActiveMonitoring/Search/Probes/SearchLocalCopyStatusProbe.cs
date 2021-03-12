using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Ceres.CoreServices.Services.HealthCheck;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Search;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Exchange.Search.Core;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000480 RID: 1152
	public class SearchLocalCopyStatusProbe : SearchProbeBase
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x000AA69C File Offset: 0x000A889C
		protected override bool SkipOnAutoDagExcludeFromMonitoring
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000AA6A0 File Offset: 0x000A88A0
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			SearchLocalCopyStatusProbe.TargetStatus @enum = base.AttributeHelper.GetEnum<SearchLocalCopyStatusProbe.TargetStatus>("TargetStatus", true, SearchLocalCopyStatusProbe.TargetStatus.Mounted);
			int @int = base.AttributeHelper.GetInt("FailedAndSuspendedAlertThresholdMinutes", false, 0, null, null);
			bool @bool = base.AttributeHelper.GetBool("CheckHealthyAndUpdgrading", false, true);
			string targetResource = base.Definition.TargetResource;
			IndexStatus indexStatus = null;
			if (SearchRootController.ShouldWaitForConfigureMountPointsPostReInstall())
			{
				base.Result.StateAttribute4 = "ConfigureMountPointsPostReInstall";
				return;
			}
			try
			{
				indexStatus = SearchMonitoringHelper.GetCachedLocalDatabaseIndexStatus(targetResource, true);
			}
			catch (IndexStatusException ex)
			{
				CopyStatusClientCachedEntry cachedLocalDatabaseCopyStatus = SearchMonitoringHelper.GetCachedLocalDatabaseCopyStatus(targetResource);
				if (cachedLocalDatabaseCopyStatus == null || cachedLocalDatabaseCopyStatus.CopyStatus == null)
				{
					base.Result.StateAttribute4 = "CopyStatusNull";
					return;
				}
				base.Result.StateAttribute4 = cachedLocalDatabaseCopyStatus.CopyStatus.CopyStatus.ToString();
				if ((@enum == SearchLocalCopyStatusProbe.TargetStatus.Mounted && cachedLocalDatabaseCopyStatus.CopyStatus.CopyStatus == CopyStatusEnum.Mounted) || (@enum == SearchLocalCopyStatusProbe.TargetStatus.Passive && cachedLocalDatabaseCopyStatus.CopyStatus.CopyStatus == CopyStatusEnum.Healthy))
				{
					throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(ex.LocalizedString, SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
				}
			}
			base.Result.StateAttribute1 = indexStatus.IndexingState.ToString();
			if (@enum == SearchLocalCopyStatusProbe.TargetStatus.Mounted)
			{
				this.LogCopyStatusChange(cancellationToken);
			}
			if (indexStatus.IndexingState != ContentIndexStatusType.Healthy)
			{
				if (@enum == SearchLocalCopyStatusProbe.TargetStatus.MountedAndCrawling)
				{
					if (indexStatus.IndexingState == ContentIndexStatusType.Crawling || indexStatus.IndexingState == ContentIndexStatusType.HealthyAndUpgrading)
					{
						if (indexStatus.IndexingState == ContentIndexStatusType.HealthyAndUpgrading && !@bool)
						{
							base.Result.StateAttribute5 = "Skip CheckHealthyAndUpdgrading";
							return;
						}
						LocalizedString value;
						if (!this.IsCrawlingHealthy(cancellationToken, indexStatus, out value))
						{
							throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(value, SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
						}
					}
					return;
				}
				CopyStatusClientCachedEntry cachedLocalDatabaseCopyStatus = SearchMonitoringHelper.GetCachedLocalDatabaseCopyStatus(targetResource);
				if (cachedLocalDatabaseCopyStatus == null || cachedLocalDatabaseCopyStatus.CopyStatus == null)
				{
					base.Result.StateAttribute4 = "CopyStatusNull";
					return;
				}
				RpcDatabaseCopyStatus2 copyStatus = cachedLocalDatabaseCopyStatus.CopyStatus;
				if (@enum == SearchLocalCopyStatusProbe.TargetStatus.Mounted && copyStatus.CopyStatus != CopyStatusEnum.Mounted)
				{
					base.Result.StateAttribute4 = "NotMounted";
					return;
				}
				if (@enum == SearchLocalCopyStatusProbe.TargetStatus.Passive && copyStatus.CopyStatus != CopyStatusEnum.Healthy)
				{
					base.Result.StateAttribute4 = "NotHealthy";
					return;
				}
				base.Result.StateAttribute4 = copyStatus.CopyStatus.ToString();
				if (copyStatus.ContentIndexStatus == ContentIndexStatusType.Seeding)
				{
					this.SetSeedingTimestamp(cancellationToken);
					LocalizedString value2;
					if (!this.IsSeedingHealthy(cancellationToken, copyStatus, out value2))
					{
						throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(value2, SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
					}
					return;
				}
				else
				{
					if (copyStatus.ContentIndexStatus == ContentIndexStatusType.FailedAndSuspended && (copyStatus.CopyStatus == CopyStatusEnum.Healthy || copyStatus.CopyStatus == CopyStatusEnum.Mounted))
					{
						List<CopyStatusClientCachedEntry> cachedDatabaseCopyStatus = SearchMonitoringHelper.GetCachedDatabaseCopyStatus(targetResource);
						if (cachedDatabaseCopyStatus != null)
						{
							foreach (CopyStatusClientCachedEntry copyStatusClientCachedEntry in cachedDatabaseCopyStatus)
							{
								RpcDatabaseCopyStatus2 copyStatus2 = copyStatusClientCachedEntry.CopyStatus;
								if (copyStatus2 != null && copyStatus2.ContentIndexStatus == ContentIndexStatusType.Healthy)
								{
									DateTime d = this.SetSeedingTimestamp(cancellationToken);
									if (DateTime.UtcNow - d > TimeSpan.FromMinutes((double)@int))
									{
										throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(Strings.SearchCatalogInFailedAndSuspendedState(targetResource, ContentIndexStatusType.FailedAndSuspended.ToString(), copyStatus2.MailboxServer, d.ToString(), Environment.MachineName), SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
									}
									break;
								}
							}
						}
						return;
					}
					if (copyStatus.ContentIndexStatus != ContentIndexStatusType.Failed && copyStatus.ContentIndexStatus != ContentIndexStatusType.Unknown)
					{
						return;
					}
					base.Result.StateAttribute3 = copyStatus.ContentIndexErrorMessage;
					if (copyStatus.CopyStatus != CopyStatusEnum.Mounted && copyStatus.CopyStatus != CopyStatusEnum.Healthy)
					{
						return;
					}
					if (copyStatus.ContentIndexErrorMessage == Strings.DatabaseOffline || copyStatus.ContentIndexErrorMessage == Strings.MapiNetworkError)
					{
						return;
					}
					string diagnosticInfoError = Strings.SearchInformationNotAvailable;
					string nodesInfo = Strings.SearchInformationNotAvailable;
					try
					{
						DiagnosticInfo.FeedingControllerDiagnosticInfo cachedFeedingControllerDiagnosticInfo = SearchMonitoringHelper.GetCachedFeedingControllerDiagnosticInfo(targetResource);
						if (cachedFeedingControllerDiagnosticInfo != null && !string.IsNullOrWhiteSpace(cachedFeedingControllerDiagnosticInfo.Error))
						{
							diagnosticInfoError = cachedFeedingControllerDiagnosticInfo.Error;
						}
					}
					catch (Exception ex2)
					{
						if (!(ex2 is TimeoutException))
						{
							SearchMonitoringHelper.LogInfo(this, "Exception caught getting diagnostic info for the feeding controller.", new object[]
							{
								ex2
							});
						}
					}
					try
					{
						StringBuilder stringBuilder = new StringBuilder();
						foreach (HealthCheckInfo healthCheckInfo in NodeManagementClient.Instance.GetSystemInfo())
						{
							stringBuilder.AppendFormat("Name: '{0}', State : '{1}', Description: '{2}'.", healthCheckInfo.Name, healthCheckInfo.State, healthCheckInfo.Description);
							stringBuilder.AppendLine();
						}
						nodesInfo = stringBuilder.ToString();
					}
					catch (Exception ex3)
					{
						SearchMonitoringHelper.LogInfo(this, "Exception caught getting nodes info from FAST.", new object[]
						{
							ex3
						});
					}
					if (copyStatus.CopyStatus == CopyStatusEnum.Mounted)
					{
						throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(Strings.SearchIndexActiveCopyUnhealthy(targetResource, copyStatus.ContentIndexStatus.ToString(), copyStatus.ContentIndexErrorMessage, diagnosticInfoError, nodesInfo), SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
					}
					throw new SearchProbeFailureException(Strings.SearchIndexServerCopyStatus(Strings.SearchIndexCopyUnhealthy(targetResource, copyStatus.ContentIndexStatus.ToString(), copyStatus.ContentIndexErrorMessage, diagnosticInfoError, nodesInfo), SearchMonitoringHelper.GetAllLocalDatabaseCopyStatusString()));
				}
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000AAC24 File Offset: 0x000A8E24
		private bool IsCrawlingHealthy(CancellationToken cancellationToken, IndexStatus indexStatus, out LocalizedString errorMessage)
		{
			errorMessage = default(LocalizedString);
			string targetResource = base.Definition.TargetResource;
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			long performanceCounterValue = SearchMonitoringHelper.GetPerformanceCounterValue("MSExchange Search Indexes", "Crawler: Items Processed", targetResource);
			base.Result.StateAttribute6 = (double)performanceCounterValue;
			if (lastProbeResult != null && lastProbeResult.StateAttribute6 == (double)performanceCounterValue)
			{
				errorMessage = Strings.SearchIndexCrawlingNoProgress(targetResource, indexStatus.IndexingState.ToString(), performanceCounterValue, lastProbeResult.ExecutionEndTime, DateTime.UtcNow);
				return false;
			}
			return true;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000AACAC File Offset: 0x000A8EAC
		private bool IsSeedingHealthy(CancellationToken cancellationToken, RpcDatabaseCopyStatus2 copyStatus, out LocalizedString errorMessage)
		{
			string targetResource = base.Definition.TargetResource;
			int? contentIndexSeedingPercent = copyStatus.ContentIndexSeedingPercent;
			string seedingSource = Strings.SearchInformationNotAvailable;
			if (!string.IsNullOrEmpty(copyStatus.ContentIndexSeedingSource))
			{
				seedingSource = copyStatus.ContentIndexSeedingSource;
			}
			if (contentIndexSeedingPercent != null)
			{
				base.Result.StateAttribute7 = (double)contentIndexSeedingPercent.Value;
				ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
				if (lastProbeResult != null && lastProbeResult.StateAttribute7 == (double)contentIndexSeedingPercent.Value)
				{
					errorMessage = Strings.SearchIndexSeedingNoProgres(targetResource, contentIndexSeedingPercent.ToString(), seedingSource);
					return false;
				}
			}
			errorMessage = default(LocalizedString);
			return true;
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000AAD4C File Offset: 0x000A8F4C
		private DateTime SetSeedingTimestamp(CancellationToken cancellationToken)
		{
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			DateTime result = DateTime.UtcNow;
			if (lastProbeResult != null && !string.IsNullOrEmpty(lastProbeResult.StateAttribute2))
			{
				result = DateTime.Parse(lastProbeResult.StateAttribute2);
			}
			base.Result.StateAttribute2 = result.ToString();
			return result;
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000AADA4 File Offset: 0x000A8FA4
		private void LogCopyStatusChange(CancellationToken cancellationToken)
		{
			string targetResource = base.Definition.TargetResource;
			bool flag = SearchMonitoringHelper.IsDatabaseActive(targetResource);
			base.Result.StateAttribute11 = flag.ToString();
			ProbeResult lastProbeResult = SearchMonitoringHelper.GetLastProbeResult(this, base.Broker, cancellationToken);
			if (lastProbeResult == null)
			{
				SearchMonitoringHelper.LogStatusChange("'{0}' now has ContentIndexState: {1}, Mounted: {2}.", new object[]
				{
					targetResource,
					base.Result.StateAttribute1,
					base.Result.StateAttribute11
				});
				return;
			}
			if (base.Result.StateAttribute1 != lastProbeResult.StateAttribute1 || base.Result.StateAttribute11 != lastProbeResult.StateAttribute11)
			{
				SearchMonitoringHelper.LogStatusChange("'{0}' now has ContentIndexState: {1}, Mounted: {2}. The previous state at {3} was ContentIndexState: {4}, Mounted: {5}.", new object[]
				{
					targetResource,
					base.Result.StateAttribute1,
					base.Result.StateAttribute11,
					lastProbeResult.ExecutionStartTime.ToString("u"),
					lastProbeResult.StateAttribute1,
					lastProbeResult.StateAttribute11
				});
			}
		}

		// Token: 0x02000481 RID: 1153
		internal enum TargetStatus
		{
			// Token: 0x040013EA RID: 5098
			Mounted,
			// Token: 0x040013EB RID: 5099
			MountedAndCrawling,
			// Token: 0x040013EC RID: 5100
			Passive
		}
	}
}
