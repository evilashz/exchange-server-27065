using System;
using System.Collections.Generic;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000006 RID: 6
	internal class GracefulDegradationManager
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002B86 File Offset: 0x00000D86
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002B8D File Offset: 0x00000D8D
		internal static DateTime RecentGracefulDegradationExecutionTime
		{
			get
			{
				return GracefulDegradationManager.recentGracefulDegradationExecutionTime;
			}
			private set
			{
				GracefulDegradationManager.recentGracefulDegradationExecutionTime = value;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B98 File Offset: 0x00000D98
		internal static void DetermineFeatureStatusAndUpdate(IEnumerable<MdbInfo> allMDBs, ISearchServiceConfig config, IDiagnosticsSession diagnosticsSession)
		{
			SearchFeatureState currentState = SearchFeatureState.GetCurrentState(config, diagnosticsSession, allMDBs);
			long andLogUsageBySearchMemoryModel = GracefulDegradationManager.GetAndLogUsageBySearchMemoryModel(currentState, diagnosticsSession);
			currentState.SearchMemoryModel.SearchMemoryUsageDrift = currentState.SearchMemoryModel.SearchMemoryUsage - andLogUsageBySearchMemoryModel;
			if (config.GracefulDegradationEnabled)
			{
				if (config.TimerForGracefulDegradation < 1)
				{
					GracefulDegradationManager.counterForGracefulDegradationExecution = 1;
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Config TimerForGracefulDegradation: unexpected value {0}.", new object[]
					{
						config.TimerForGracefulDegradation
					});
				}
				if (GracefulDegradationManager.counterForGracefulDegradationExecution > 1)
				{
					GracefulDegradationManager.counterForGracefulDegradationExecution--;
					diagnosticsSession.TraceDebug<int>("Graceful degradation skipped: next execution in {0} minutes", GracefulDegradationManager.counterForGracefulDegradationExecution * config.SyncMdbsInterval.Minutes);
					return;
				}
				SearchFeatureState nextState = currentState.GetNextState();
				SearchFeatureState allOnState = SearchFeatureState.GetAllOnState(config, diagnosticsSession, allMDBs);
				if (!nextState.Equals(allOnState) && allOnState.SearchMemoryModel.IsUnderSearchBudget())
				{
					string memoryUsageInfo = string.Format("Search memory usage: {0}; Search memory budget: {1}; Expected search memory usage with all features on: {2}; Current available memory: {3}.", new object[]
					{
						SearchMemoryModel.GetSearchMemoryUsage(),
						currentState.SearchMemoryModel.SearchDesiredFreeMemory,
						allOnState.SearchMemoryModel.SearchMemoryUsage,
						currentState.SearchMemoryModel.AvailPhys
					});
					EventNotificationItem.Publish(ExchangeComponent.Search.Name, "GracefulDegradationManagerFailure", string.Empty, Strings.GracefulDegradationManagerException(memoryUsageInfo, new CatalogItemStatistics(currentState.MdbInfos).ToString()), ResultSeverityLevel.Error, false);
				}
				nextState.WriteCurrentStateToRegistry();
				GracefulDegradationManager.recentGracefulDegradationExecutionTime = DateTime.UtcNow;
				GracefulDegradationManager.counterForGracefulDegradationExecution = config.TimerForGracefulDegradation;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D20 File Offset: 0x00000F20
		private static long GetAndLogUsageBySearchMemoryModel(SearchFeatureState searchFeatureState, IDiagnosticsSession diagnosticsSession)
		{
			CatalogItemStatistics catalogItemStatistics = new CatalogItemStatistics(searchFeatureState.MdbInfos);
			long expectedSearchMemoryUsage = searchFeatureState.SearchMemoryModel.GetExpectedSearchMemoryUsage(catalogItemStatistics.ActiveItems, catalogItemStatistics.PassiveItemsCatalogSuspendedOff, catalogItemStatistics.ActiveItemsInstantSearchOn, catalogItemStatistics.ActiveItemsRefinersOn);
			float searchMemoryDriftRatio = (float)(searchFeatureState.SearchMemoryModel.SearchMemoryUsage - expectedSearchMemoryUsage) / (float)searchFeatureState.Config.MemoryMeasureDrift;
			diagnosticsSession.LogGracefulDegradationInfo(DiagnosticsLoggingTag.Informational, searchFeatureState.SearchMemoryModel.TotalPhys, searchFeatureState.SearchMemoryModel.AvailPhys, searchFeatureState.SearchMemoryModel.SearchMemoryUsage, expectedSearchMemoryUsage, searchMemoryDriftRatio, CatalogItemStatistics.GenerateFeatureStateLoggingInfo(searchFeatureState.MdbInfos), new object[0]);
			return expectedSearchMemoryUsage;
		}

		// Token: 0x04000017 RID: 23
		internal const string GracefulDegradationManagerFailure = "GracefulDegradationManagerFailure";

		// Token: 0x04000018 RID: 24
		internal const string SearchMemoryModelFailure = "SearchMemoryModelFailure";

		// Token: 0x04000019 RID: 25
		private static DateTime recentGracefulDegradationExecutionTime = DateTime.MinValue;

		// Token: 0x0400001A RID: 26
		private static int counterForGracefulDegradationExecution = 1;
	}
}
