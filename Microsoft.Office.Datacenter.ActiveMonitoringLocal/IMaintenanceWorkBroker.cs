using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000036 RID: 54
	public interface IMaintenanceWorkBroker : IWorkBrokerBase
	{
		// Token: 0x06000419 RID: 1049
		void RequestRestart(string resultName, int resultId);

		// Token: 0x0600041A RID: 1050
		IDataAccessQuery<MaintenanceResult> GetMaintenanceResults(MaintenanceDefinition definition, DateTime startTime);

		// Token: 0x0600041B RID: 1051
		Task AddWorkDefinition<TDefinition>(TDefinition definition, TracingContext traceContext) where TDefinition : WorkDefinition;

		// Token: 0x0600041C RID: 1052
		Task AddWorkDefinitions<TWorkDefinition>(IEnumerable<TWorkDefinition> workDefinitions, int id, DateTime cleanBeforeTime, CancellationToken cancellationToken, TracingContext traceContext) where TWorkDefinition : WorkDefinition;

		// Token: 0x0600041D RID: 1053
		Task<int> DeleteWorkItemResult<TResult>(DateTime startTime, DateTime endTime, int timeOutInSeconds, CancellationToken cancellationToken, TracingContext traceContext) where TResult : WorkItemResult;

		// Token: 0x0600041E RID: 1054
		IDataAccessQuery<MaintenanceResult> GetLastSuccessfulMaintenanceResult(MaintenanceDefinition definition, TimeSpan searchWindow);

		// Token: 0x0600041F RID: 1055
		IDataAccessQuery<MaintenanceResult> GetLastMaintenanceResult(MaintenanceDefinition definition, TimeSpan searchWindow);

		// Token: 0x06000420 RID: 1056
		IDataAccessQuery<TopologyScope> GetTopologyScopes(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000421 RID: 1057
		Task AsyncInsertTopologyScope(int aggregationLevel, string name, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000422 RID: 1058
		Task<int> AsyncDisableWorkDefinitions(int createdById, DateTime createdBeforeTimestamp, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000423 RID: 1059
		IDataAccessQuery<TopologySchema> GetTopologyObjects<TopologySchema, TKey>(IIndexDescriptor<TopologySchema, TKey> indexDescriptor) where TopologySchema : class;

		// Token: 0x06000424 RID: 1060
		Task<IEnumerable<TableSchema>> GetTableData<TableSchema, TKey>(IIndexDescriptor<TableSchema, TKey> indexDescriptor) where TableSchema : TableEntity, new();

		// Token: 0x06000425 RID: 1061
		IDataAccessQuery<WorkDefinitionOverride> GetWorkDefinitionOverrides(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000426 RID: 1062
		IDataAccessQuery<ProbeDefinition> GetProbeDefinition(string typeName);

		// Token: 0x06000427 RID: 1063
		WorkUnit RequestWorkUnit(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000428 RID: 1064
		int HeartbeatForWorkUnit(WorkUnit workUnit, int workUnitState, out List<WorkUnitEntry> entries, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000429 RID: 1065
		Task<int> AddAndRemoveWorkUnitEntries(List<WorkUnitEntry> workUnitEntriesToAdd, List<WorkUnitEntry> workUnitEntriesToRemove, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600042A RID: 1066
		Task<int> AddWorkUnit(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600042B RID: 1067
		int GetWorkState(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600042C RID: 1068
		Task SaveAllStatusEntries(CancellationToken cancellationToken, TracingContext traceContext);
	}
}
