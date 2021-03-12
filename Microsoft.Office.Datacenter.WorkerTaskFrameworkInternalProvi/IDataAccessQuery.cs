using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000038 RID: 56
	public interface IDataAccessQuery<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000380 RID: 896
		IEnumerable<T> InnerQuery { get; }

		// Token: 0x06000381 RID: 897
		Task<int> ExecuteAsync(Action<T> processResult, CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000382 RID: 898
		Task<T> ExecuteAsync(CancellationToken cancellationToken, TracingContext traceContext);

		// Token: 0x06000383 RID: 899
		IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query);
	}
}
