using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search
{
	// Token: 0x0200048A RID: 1162
	internal static class SearchStrings
	{
		// Token: 0x06001D62 RID: 7522 RVA: 0x000AFE03 File Offset: 0x000AE003
		internal static string SearchEscalateResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "Escalate");
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000AFE15 File Offset: 0x000AE015
		internal static string SearchRestartServiceResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "RestartSearchService");
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000AFE27 File Offset: 0x000AE027
		internal static string SearchDatabaseFailoverResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "DatabaseFailover");
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000AFE39 File Offset: 0x000AE039
		internal static string SearchRestartHostControllerServiceResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "RestartHostControllerService");
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000AFE4B File Offset: 0x000AE04B
		internal static string SearchResumeCatalogResponderName(string monitorName)
		{
			return monitorName.Replace("Monitor", "ResumeCatalog");
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000AFE5D File Offset: 0x000AE05D
		internal static string HostControllerServiceRestartNodeResponderName(string monitorName)
		{
			if (monitorName.Contains("Monitor"))
			{
				return monitorName.Replace("Monitor", "RestartNode");
			}
			return monitorName + "RestartNode";
		}

		// Token: 0x04001428 RID: 5160
		internal const string SearchMountedCopyStatusProbeName = "SearchMountedCopyStatusProbe";

		// Token: 0x04001429 RID: 5161
		internal const string SearchMountedCopyStatusMonitorName = "SearchMountedCopyStatusMonitor";

		// Token: 0x0400142A RID: 5162
		internal const string SearchIndexSuspendedProbeName = "SearchIndexSuspendedProbe";

		// Token: 0x0400142B RID: 5163
		internal const string SearchIndexSuspendedMonitorName = "SearchIndexSuspendedMonitor";

		// Token: 0x0400142C RID: 5164
		internal const string HostControllerServiceRunningProbeName = "HostControllerServiceRunningProbe";

		// Token: 0x0400142D RID: 5165
		internal const string HostControllerServiceRunningMonitorName = "HostControllerServiceRunningMonitor";

		// Token: 0x0400142E RID: 5166
		internal const string FastNodeCrashProbeName = "FastNodeCrashProbe";

		// Token: 0x0400142F RID: 5167
		internal const string FastNodeCrashMonitorName = "FastNodeCrashMonitor";

		// Token: 0x04001430 RID: 5168
		internal const string FastNodeAvailabilityProbeName = "FastNodeAvailabilityProbe";

		// Token: 0x04001431 RID: 5169
		internal const string FastNodeAvailabilityMonitorName = "FastNodeAvailabilityMonitor";

		// Token: 0x04001432 RID: 5170
		internal const string FastNodeRestartProbeName = "FastNodeRestartProbe";

		// Token: 0x04001433 RID: 5171
		internal const string FastNodeRestartMonitorName = "FastNodeRestartMonitor";

		// Token: 0x04001434 RID: 5172
		internal const string SearchQueryStxProbeName = "SearchQueryStxProbe";

		// Token: 0x04001435 RID: 5173
		internal const string SearchQueryStxMonitorName = "SearchQueryStxMonitor";

		// Token: 0x04001436 RID: 5174
		internal const string SearchInstantSearchStxProbeName = "SearchInstantSearchStxProbe";

		// Token: 0x04001437 RID: 5175
		internal const string SearchInstantSearchStxMonitorName = "SearchInstantSearchStxMonitor";

		// Token: 0x04001438 RID: 5176
		internal const string SearchCatalogAvailabilityProbeName = "SearchCatalogAvailabilityProbe";

		// Token: 0x04001439 RID: 5177
		internal const string SearchCatalogAvailabilityMonitorName = "SearchCatalogAvailabilityMonitor";

		// Token: 0x0400143A RID: 5178
		internal const string SearchServiceRunningProbeName = "SearchServiceRunningProbe";

		// Token: 0x0400143B RID: 5179
		internal const string SearchServiceRunningMonitorName = "SearchServiceRunningMonitor";

		// Token: 0x0400143C RID: 5180
		internal const string SearchIndexBacklogProbeName = "SearchIndexBacklogProbe";

		// Token: 0x0400143D RID: 5181
		internal const string SearchIndexBacklogMonitorName = "SearchIndexBacklogMonitor";

		// Token: 0x0400143E RID: 5182
		internal const string SearchIndexFailureProbeName = "SearchIndexFailureProbe";

		// Token: 0x0400143F RID: 5183
		internal const string SearchIndexFailureMonitorName = "SearchIndexFailureMonitor";

		// Token: 0x04001440 RID: 5184
		internal const string SearchSingleCopyProbeName = "SearchSingleCopyProbe";

		// Token: 0x04001441 RID: 5185
		internal const string SearchSingleCopyMonitorName = "SearchSingleCopyMonitor";

		// Token: 0x04001442 RID: 5186
		internal const string SearchLocalMountedCopyStatusProbeName = "SearchLocalMountedCopyStatusProbe";

		// Token: 0x04001443 RID: 5187
		internal const string SearchLocalMountedCopyStatusMonitorName = "SearchLocalMountedCopyStatusMonitor";

		// Token: 0x04001444 RID: 5188
		internal const string SearchLocalPassiveCopyStatusProbeName = "SearchLocalPassiveCopyStatusProbe";

		// Token: 0x04001445 RID: 5189
		internal const string SearchLocalPassiveCopyStatusMonitorName = "SearchLocalPassiveCopyStatusMonitor";

		// Token: 0x04001446 RID: 5190
		internal const string SearchCrawlingProgressProbeName = "SearchCrawlingProgressProbe";

		// Token: 0x04001447 RID: 5191
		internal const string SearchCrawlingProgressMonitorName = "SearchCrawlingProgressMonitor";

		// Token: 0x04001448 RID: 5192
		internal const string SearchQueryFailureProbeName = "SearchQueryFailureProbe";

		// Token: 0x04001449 RID: 5193
		internal const string SearchQueryFailureMonitorName = "SearchQueryFailureMonitor";

		// Token: 0x0400144A RID: 5194
		internal const string SearchServiceCrashProbeName = "SearchServiceCrashProbe";

		// Token: 0x0400144B RID: 5195
		internal const string SearchServiceCrashMonitorName = "SearchServiceCrashMonitor";

		// Token: 0x0400144C RID: 5196
		internal const string SearchCatalogSizeProbeName = "SearchCatalogSizeProbe";

		// Token: 0x0400144D RID: 5197
		internal const string SearchCatalogSizeMonitorName = "SearchCatalogSizeMonitor";

		// Token: 0x0400144E RID: 5198
		internal const string SearchTransportAgentProbeName = "SearchTransportAgentProbe";

		// Token: 0x0400144F RID: 5199
		internal const string SearchTransportAgentMonitorName = "SearchTransportAgentMonitor";

		// Token: 0x04001450 RID: 5200
		internal const string SearchWordBreakerLoadingProbeName = "SearchWordBreakerLoadingProbe";

		// Token: 0x04001451 RID: 5201
		internal const string SearchWordBreakerLoadingMonitorName = "SearchWordBreakerLoadingMonitor";

		// Token: 0x04001452 RID: 5202
		internal const string SearchResourceLoadProbeName = "SearchResourceLoadProbe";

		// Token: 0x04001453 RID: 5203
		internal const string SearchResourceLoadMonitorName = "SearchResourceLoadMonitor";

		// Token: 0x04001454 RID: 5204
		internal const string SearchFeedingControllerFailureMonitorName = "SearchFeedingControllerFailureMonitor";

		// Token: 0x04001455 RID: 5205
		internal const string SearchGracefulDegradationManagerFailureMonitorName = "SearchGracefulDegradationManagerFailureMonitor";

		// Token: 0x04001456 RID: 5206
		internal const string FastIndexNumDiskPartsMonitorName = "FastIndexNumDiskPartsMonitor";

		// Token: 0x04001457 RID: 5207
		internal const string Monitor = "Monitor";

		// Token: 0x04001458 RID: 5208
		internal const string FromInvokeMonitoringItemPropertyName = "FromInvokeMonitoringItem";

		// Token: 0x04001459 RID: 5209
		internal const string SearchParserServerDegradeProbeName = "SearchParserServerDegradeProbe";

		// Token: 0x0400145A RID: 5210
		internal const string SearchMemoryOverThresholdProbeName = "SearchMemoryOverThresholdProbe";

		// Token: 0x0400145B RID: 5211
		internal const string SearchMemoryOverThresholdMonitorName = "SearchMemoryOverThresholdMonitor";

		// Token: 0x0400145C RID: 5212
		internal const string SearchGracefulDegradationStatusProbeName = "SearchGracefulDegradationStatusProbe";

		// Token: 0x0400145D RID: 5213
		internal const string SearchGracefulDegradationStatusMonitorName = "SearchGracefulDegradationStatusMonitor";

		// Token: 0x0400145E RID: 5214
		internal const string SearchRopNotSupportedMonitorName = "SearchRopNotSupportedMonitor";

		// Token: 0x0400145F RID: 5215
		internal const string SearchCopyStatusHaImpactingProbeName = "SearchCopyStatusHaImpactingProbe";

		// Token: 0x04001460 RID: 5216
		internal const string SearchCopyStatusHaImpactingMonitorName = "SearchCopyStatusHaImpactingMonitor";
	}
}
