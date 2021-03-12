using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200001C RID: 28
	public interface IIndexDescriptor<TEntity, TKey> : IIndexDescriptor
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002A6 RID: 678
		TKey Key { get; }

		// Token: 0x060002A7 RID: 679
		IDataAccessQuery<TEntity> ApplyIndexRestriction(IDataAccessQuery<TEntity> query);

		// Token: 0x060002A8 RID: 680
		IEnumerable<TKey> GetKeyValues(TEntity item);
	}
}
