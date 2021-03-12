using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000005 RID: 5
	internal abstract class MonitorDefinitionIndex : WorkDefinitionIndex<MonitorDefinition>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00004A8F File Offset: 0x00002C8F
		internal static IIndexDescriptor<MonitorDefinition, string> SampleMask(string sampleMask)
		{
			return new MonitorDefinitionIndex.MonitorDefinitionIndexDescriptorForSampleMask(sampleMask);
		}

		// Token: 0x02000006 RID: 6
		private class MonitorDefinitionIndexDescriptorForSampleMask : WorkDefinitionIndex<MonitorDefinition>.WorkDefinitionIndexBase<MonitorDefinition, string>
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00004A9F File Offset: 0x00002C9F
			internal MonitorDefinitionIndexDescriptorForSampleMask(string key) : base(key)
			{
			}

			// Token: 0x0600000C RID: 12 RVA: 0x00004B80 File Offset: 0x00002D80
			public override IEnumerable<string> GetKeyValues(MonitorDefinition item)
			{
				yield return item.SampleMask;
				yield break;
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00004BD0 File Offset: 0x00002DD0
			public override IDataAccessQuery<MonitorDefinition> ApplyIndexRestriction(IDataAccessQuery<MonitorDefinition> query)
			{
				IEnumerable<MonitorDefinition> enumerable = from d in query
				select d;
				if (IndexCapabilities.SupportsCaseInsensitiveStringComparison)
				{
					enumerable = from d in enumerable
					where d.SampleMask.Equals(base.Key, StringComparison.OrdinalIgnoreCase)
					select d;
				}
				else
				{
					enumerable = from d in enumerable
					where d.SampleMask.Equals(base.Key)
					select d;
				}
				return query.AsDataAccessQuery<MonitorDefinition>(enumerable);
			}
		}
	}
}
