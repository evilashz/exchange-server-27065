using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000005 RID: 5
	public abstract class BaseDataAccess
	{
		// Token: 0x06000012 RID: 18
		internal abstract IDataAccessQuery<TEntity> GetTable<TEntity, TKey>(IIndexDescriptor<TEntity, TKey> indexDescriptor) where TEntity : class;

		// Token: 0x06000013 RID: 19
		internal abstract Task<int> AsyncGetWorkItemPackages<TWorkItem>(int deploymentId, Action<string> processResult, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000014 RID: 20
		internal abstract Task AsyncInsert<TEntity>(TEntity entity, CancellationToken cancellationToken, TracingContext traceContext) where TEntity : class, IWorkData;

		// Token: 0x06000015 RID: 21
		internal abstract Task AsyncInsertMany<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken, TracingContext traceContext) where TEntity : class, IWorkData;

		// Token: 0x06000016 RID: 22
		internal abstract Task AsyncInsertManyDefinitionsAndCleanup<TEntity>(IEnumerable<TEntity> entities, int id, DateTime cleanBeforeTime, CancellationToken cancellationToken, TracingContext traceContext) where TEntity : WorkDefinition;

		// Token: 0x06000017 RID: 23
		internal abstract Task<int> AsyncExecuteReader<TEntity>(IDataAccessQuery<TEntity> query, Action<TEntity> processResult, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000018 RID: 24
		internal abstract Task<TEntity> AsyncExecuteScalar<TEntity>(IDataAccessQuery<TEntity> query, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000019 RID: 25
		internal abstract Task<IEnumerable<TopologySchema>> GetTableData<TopologySchema, TKey>(IIndexDescriptor<TopologySchema, TKey> indexDescriptor, CancellationToken cancellationToken, TracingContext traceContext) where TopologySchema : TableEntity, new();

		// Token: 0x0600001A RID: 26
		internal abstract WorkUnit RequestWorkUnit(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600001B RID: 27
		internal abstract int HeartbeatForWorkUnit(WorkUnit workUnit, int workUnitState, out List<WorkUnitEntry> entries, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600001C RID: 28
		internal abstract Task<int> AddAndRemoveWorkUnitEntries(List<WorkUnitEntry> workUnitEntriesToAdd, List<WorkUnitEntry> workUnitEntriesToRemove, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600001D RID: 29
		internal abstract Task<int> AddWorkUnit(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600001E RID: 30
		internal abstract int GetWorkState(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x0600001F RID: 31
		internal abstract Task<bool> RequestRecovery(string metricName, string recoveryType, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000020 RID: 32
		internal abstract Task<StatusEntryCollection> GetStatusEntries(string key, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000021 RID: 33
		internal abstract Task SaveStatusEntry(StatusEntry entry, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000022 RID: 34
		internal abstract IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);
	}
}
