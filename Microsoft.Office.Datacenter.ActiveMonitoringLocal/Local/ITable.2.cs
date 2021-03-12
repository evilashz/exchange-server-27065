using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x02000077 RID: 119
	internal interface ITable<TItem> : ITable
	{
		// Token: 0x060006B7 RID: 1719
		IEnumerable<TItem> GetItems<TKey>(IIndexDescriptor<TItem, TKey> indexDescriptor);

		// Token: 0x060006B8 RID: 1720
		void Insert(TItem item, TracingContext traceContext);
	}
}
