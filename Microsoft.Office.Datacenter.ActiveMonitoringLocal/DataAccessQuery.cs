using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000086 RID: 134
	public class DataAccessQuery<T> : IDataAccessQuery<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0001D19B File Offset: 0x0001B39B
		internal DataAccessQuery(IEnumerable<T> innerQuery, DataAccess dataAccess)
		{
			this.dataAccess = dataAccess;
			this.innerQuery = innerQuery;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001D1B1 File Offset: 0x0001B3B1
		public IEnumerable<T> InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001D1B9 File Offset: 0x0001B3B9
		public IEnumerator<T> GetEnumerator()
		{
			return this.innerQuery.GetEnumerator();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001D1C6 File Offset: 0x0001B3C6
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001D1CD File Offset: 0x0001B3CD
		public Task<int> ExecuteAsync(Action<T> processResult, CancellationToken cancellationToken, TracingContext traceContext)
		{
			return new LocalDataAccess().AsyncExecuteReader<T>(this, processResult, cancellationToken, traceContext);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001D1DD File Offset: 0x0001B3DD
		public Task<T> ExecuteAsync(CancellationToken cancellationToken, TracingContext traceContext)
		{
			return new LocalDataAccess().AsyncExecuteScalar<T>(this, cancellationToken, traceContext);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001D1EC File Offset: 0x0001B3EC
		public IDataAccessQuery<TEntity> AsDataAccessQuery<TEntity>(IEnumerable<TEntity> query)
		{
			return this.dataAccess.AsDataAccessQuery<TEntity>(query);
		}

		// Token: 0x04000461 RID: 1121
		private BaseDataAccess dataAccess;

		// Token: 0x04000462 RID: 1122
		private IEnumerable<T> innerQuery;
	}
}
