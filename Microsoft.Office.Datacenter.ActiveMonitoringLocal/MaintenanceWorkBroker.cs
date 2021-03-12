using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000037 RID: 55
	public class MaintenanceWorkBroker<TDataAccess> : TypedWorkBroker<MaintenanceDefinition, MaintenanceWorkItem, MaintenanceResult, TDataAccess>, IMaintenanceWorkBroker, IWorkBrokerBase where TDataAccess : DataAccess, new()
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x00010A0E File Offset: 0x0000EC0E
		public MaintenanceWorkBroker(WorkItemFactory factory) : base(factory)
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00010A22 File Offset: 0x0000EC22
		public void RequestRestart(string resultName, int resultId)
		{
			base.RequestRestart(RestartRequest.CreateMaintenanceRestartRequest(resultName, resultId));
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00010A31 File Offset: 0x0000EC31
		public IDataAccessQuery<MaintenanceResult> GetMaintenanceResults(MaintenanceDefinition definition, DateTime startTime)
		{
			return base.GetResultsQuery(definition, startTime);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00010A3C File Offset: 0x0000EC3C
		public Task AddWorkDefinition<TDefinition>(TDefinition definition, TracingContext traceContext) where TDefinition : WorkDefinition
		{
			this.InitializeCreatedByWorkItem<TDefinition>(definition, traceContext);
			this.ValidateAndSyncAttributes<TDefinition>(definition);
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncInsert<TDefinition>(definition, default(CancellationToken), traceContext);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00010A78 File Offset: 0x0000EC78
		public Task AddWorkDefinitions<TWorkDefinition>(IEnumerable<TWorkDefinition> workDefinitions, int id, DateTime cleanBeforeTime, CancellationToken cancellationToken, TracingContext traceContext) where TWorkDefinition : WorkDefinition
		{
			foreach (TWorkDefinition tworkDefinition in workDefinitions)
			{
				WorkDefinition definition = tworkDefinition;
				this.InitializeCreatedByWorkItem<WorkDefinition>(definition, traceContext);
				this.ValidateAndSyncAttributes<WorkDefinition>(definition);
			}
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncInsertManyDefinitionsAndCleanup<TWorkDefinition>(workDefinitions, id, cleanBeforeTime, cancellationToken, traceContext);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		public Task<int> DeleteWorkItemResult<TResult>(DateTime startTime, DateTime endTime, int timeOutInSeconds, CancellationToken cancellationToken, TracingContext traceContext) where TResult : WorkItemResult
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncDeleteWorkItemResult<TResult>(startTime, endTime, timeOutInSeconds, cancellationToken, traceContext);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010B0F File Offset: 0x0000ED0F
		public IDataAccessQuery<MaintenanceResult> GetLastSuccessfulMaintenanceResult(MaintenanceDefinition definition, TimeSpan searchWindow)
		{
			return base.GetLastSuccessfulResultQuery(definition, searchWindow);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public IDataAccessQuery<MaintenanceResult> GetLastMaintenanceResult(MaintenanceDefinition definition, TimeSpan searchWindow)
		{
			IEnumerable<MaintenanceResult> source = from r in base.GetResultsQuery(definition, DateTime.UtcNow - searchWindow)
			select r;
			return this.AsDataAccessQuery<MaintenanceResult>(source.Take(1));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00010B70 File Offset: 0x0000ED70
		public IDataAccessQuery<TopologyScope> GetTopologyScopes(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<TopologyScope> query = from s in tdataAccess.GetTable<TopologyScope, TopologyScope>(new TopologyScopeIndex())
			select s;
			return this.AsDataAccessQuery<TopologyScope>(query);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00010BC4 File Offset: 0x0000EDC4
		public IDataAccessQuery<ProbeDefinition> GetProbeDefinition(string typeName)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<ProbeDefinition> query = from s in tdataAccess.GetTable<ProbeDefinition, string>(ProbeDefinitionIndex.TypeName(typeName))
			select s;
			return this.AsDataAccessQuery<ProbeDefinition>(query);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00010C14 File Offset: 0x0000EE14
		public Task AsyncInsertTopologyScope(int aggregationLevel, string name, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncInsert<TopologyScope>(new TopologyScope
			{
				AggregationLevel = aggregationLevel,
				Name = name
			}, cancellationToken, traceContext);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public Task<int> AsyncDisableWorkDefinitions(int createdById, DateTime createdBeforeTimestamp, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AsyncDisableWorkDefinitions(createdById, createdBeforeTimestamp, cancellationToken, traceContext);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00010C74 File Offset: 0x0000EE74
		public IDataAccessQuery<TopologySchema> GetTopologyObjects<TopologySchema, TKey>(IIndexDescriptor<TopologySchema, TKey> indexDescriptor) where TopologySchema : class
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.GetTopologyDataAccessProvider().GetTable<TopologySchema, TKey>(indexDescriptor);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00010C9C File Offset: 0x0000EE9C
		public Task<IEnumerable<TableSchema>> GetTableData<TableSchema, TKey>(IIndexDescriptor<TableSchema, TKey> indexDescriptor) where TableSchema : TableEntity, new()
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.GetTopologyDataAccessProvider().GetTableData<TableSchema, TKey>(indexDescriptor, default(CancellationToken), new TracingContext());
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00010CD4 File Offset: 0x0000EED4
		public IDataAccessQuery<WorkDefinitionOverride> GetWorkDefinitionOverrides(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			IEnumerable<WorkDefinitionOverride> query = from o in tdataAccess.GetTable<WorkDefinitionOverride, WorkDefinitionOverride>(new WorkDefinitionOverrideIndex())
			select o;
			return this.AsDataAccessQuery<WorkDefinitionOverride>(query);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010D24 File Offset: 0x0000EF24
		public WorkUnit RequestWorkUnit(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.RequestWorkUnit(cancellationToken, traceContext);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00010D48 File Offset: 0x0000EF48
		public int HeartbeatForWorkUnit(WorkUnit workUnit, int workUnitState, out List<WorkUnitEntry> entries, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.HeartbeatForWorkUnit(workUnit, workUnitState, out entries, cancellationToken, traceContext);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00010D70 File Offset: 0x0000EF70
		public Task<int> AddAndRemoveWorkUnitEntries(List<WorkUnitEntry> workUnitEntriesToAdd, List<WorkUnitEntry> workUnitEntriesToRemove, CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AddAndRemoveWorkUnitEntries(workUnitEntriesToAdd, workUnitEntriesToRemove, cancellationToken, traceContext);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00010D98 File Offset: 0x0000EF98
		public Task<int> AddWorkUnit(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.AddWorkUnit(cancellationToken, traceContext);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00010DBC File Offset: 0x0000EFBC
		public int GetWorkState(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			return tdataAccess.GetWorkState(cancellationToken, traceContext);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00010DE0 File Offset: 0x0000EFE0
		public Task SaveAllStatusEntries(CancellationToken cancellationToken, TracingContext traceContext)
		{
			TDataAccess tdataAccess = Activator.CreateInstance<TDataAccess>();
			Task<List<StatusEntryCollection>> allStatusEntries = tdataAccess.GetAllStatusEntries(cancellationToken, traceContext);
			List<StatusEntryCollection> result = allStatusEntries.Result;
			foreach (StatusEntryCollection statusEntryCollection in result)
			{
				foreach (StatusEntry entry in statusEntryCollection.ItemsToRemove)
				{
					tdataAccess.SaveStatusEntry(entry, cancellationToken, traceContext).Wait();
				}
				foreach (StatusEntry entry2 in statusEntryCollection)
				{
					tdataAccess.SaveStatusEntry(entry2, cancellationToken, traceContext).Wait();
				}
			}
			TaskCompletionSource<int> taskCompletionSource = new TaskCompletionSource<int>();
			taskCompletionSource.SetResult(0);
			return taskCompletionSource.Task;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00010F00 File Offset: 0x0000F100
		private void ValidateAndSyncAttributes<TDefinition>(TDefinition definition) where TDefinition : WorkDefinition
		{
			List<string> list = new List<string>();
			if (!definition.Validate(list))
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("The work definition fails validation or misses mandatory properties. ");
				list.ForEach(delegate(string e)
				{
					sb.AppendLine(e);
				});
				throw new ArgumentException(sb.ToString());
			}
			definition.SyncExtensionAttributes(false);
			if (definition.StartTime == DateTime.MinValue)
			{
				definition.StartTime = DateTime.UtcNow.AddSeconds((double)this.random.Next(definition.RecurrenceIntervalSeconds));
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00010FC2 File Offset: 0x0000F1C2
		private void InitializeCreatedByWorkItem<TDefinition>(TDefinition definition, TracingContext traceContext) where TDefinition : WorkDefinition
		{
			if (definition != null && definition.CreatedById == 0 && traceContext != null && traceContext.WorkItem != null)
			{
				definition.CreatedById = traceContext.WorkItem.Id;
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00010FFE File Offset: 0x0000F1FE
		bool IWorkBrokerBase.IsLocal()
		{
			return base.IsLocal();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00011006 File Offset: 0x0000F206
		TimeSpan IWorkBrokerBase.get_DefaultResultWindow()
		{
			return base.DefaultResultWindow;
		}

		// Token: 0x04000319 RID: 793
		private Random random = new Random();
	}
}
