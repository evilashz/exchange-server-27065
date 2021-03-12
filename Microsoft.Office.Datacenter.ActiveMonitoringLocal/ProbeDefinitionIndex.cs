using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000019 RID: 25
	internal class ProbeDefinitionIndex : WorkDefinitionIndex<ProbeDefinition>
	{
		// Token: 0x06000195 RID: 405 RVA: 0x0000969D File Offset: 0x0000789D
		internal static IIndexDescriptor<ProbeDefinition, string> TypeName(string typeName)
		{
			return new ProbeDefinitionIndex.ProbeDefinitionIndexDescriptorForTypeName(typeName);
		}

		// Token: 0x0200001A RID: 26
		private class ProbeDefinitionIndexDescriptorForTypeName : WorkDefinitionIndex<ProbeDefinition>.WorkDefinitionIndexBase<ProbeDefinition, string>
		{
			// Token: 0x06000197 RID: 407 RVA: 0x000096AD File Offset: 0x000078AD
			internal ProbeDefinitionIndexDescriptorForTypeName(string key) : base(key)
			{
			}

			// Token: 0x06000198 RID: 408 RVA: 0x00009790 File Offset: 0x00007990
			public override IEnumerable<string> GetKeyValues(ProbeDefinition item)
			{
				yield return item.TypeName;
				yield break;
			}

			// Token: 0x06000199 RID: 409 RVA: 0x000097E0 File Offset: 0x000079E0
			public override IDataAccessQuery<ProbeDefinition> ApplyIndexRestriction(IDataAccessQuery<ProbeDefinition> query)
			{
				IEnumerable<ProbeDefinition> enumerable = from d in query
				select d;
				if (IndexCapabilities.SupportsCaseInsensitiveStringComparison)
				{
					enumerable = from d in enumerable
					where d.TypeName.Equals(base.Key, StringComparison.OrdinalIgnoreCase)
					select d;
				}
				else
				{
					enumerable = from d in enumerable
					where d.TypeName.Equals(base.Key)
					select d;
				}
				return query.AsDataAccessQuery<ProbeDefinition>(enumerable);
			}
		}
	}
}
