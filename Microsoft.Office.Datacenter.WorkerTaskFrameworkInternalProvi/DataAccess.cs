using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000006 RID: 6
	public abstract class DataAccess : BaseDataAccess
	{
		// Token: 0x06000024 RID: 36
		internal abstract Task<int> AsyncGetExclusive<TEntity>(int maxWorkitemCount, int deploymentId, Action<TEntity> processResult, Func<object, Exception, bool> corruptRowHandler, CancellationToken cancellationToken, TracingContext traceContext) where TEntity : WorkDefinition, new();

		// Token: 0x06000025 RID: 37
		internal abstract Task<int> AsyncDeleteWorkItemResult<TWorkItemResult>(DateTime startTime, DateTime endTime, int timeOutInSeconds, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000026 RID: 38
		internal abstract TimeSpan? GetQuarantineTimeSpan<TEntity>(TEntity definition) where TEntity : WorkDefinition;

		// Token: 0x06000027 RID: 39
		internal abstract Task<int> AsyncDisableWorkDefinitions(int createdById, DateTime createdBeforeTimestamp, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000028 RID: 40
		internal abstract Task<List<StatusEntryCollection>> GetAllStatusEntries(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000029 RID: 41
		internal abstract BaseDataAccess GetTopologyDataAccessProvider();
	}
}
