using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002A RID: 42
	internal class WorkDefinitionOverrideIndex : IIndexDescriptor<WorkDefinitionOverride, WorkDefinitionOverride>, IIndexDescriptor
	{
		// Token: 0x060002F2 RID: 754 RVA: 0x0000AC19 File Offset: 0x00008E19
		internal WorkDefinitionOverrideIndex()
		{
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000AC21 File Offset: 0x00008E21
		public WorkDefinitionOverride Key
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000ACF8 File Offset: 0x00008EF8
		public IEnumerable<WorkDefinitionOverride> GetKeyValues(WorkDefinitionOverride item)
		{
			yield return item;
			yield break;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public IDataAccessQuery<WorkDefinitionOverride> ApplyIndexRestriction(IDataAccessQuery<WorkDefinitionOverride> query)
		{
			return query;
		}
	}
}
