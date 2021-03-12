using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000043 RID: 67
	internal class TopologyScopeIndex : IIndexDescriptor<TopologyScope, TopologyScope>, IIndexDescriptor
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x00011F99 File Offset: 0x00010199
		internal TopologyScopeIndex()
		{
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00011FA1 File Offset: 0x000101A1
		public TopologyScope Key
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00012078 File Offset: 0x00010278
		public IEnumerable<TopologyScope> GetKeyValues(TopologyScope item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001209C File Offset: 0x0001029C
		public IDataAccessQuery<TopologyScope> ApplyIndexRestriction(IDataAccessQuery<TopologyScope> query)
		{
			return query;
		}
	}
}
