using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000026 RID: 38
	internal class ResponderDefinitionIndex : WorkDefinitionIndex<ResponderDefinition>
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000BF3F File Offset: 0x0000A13F
		internal static IIndexDescriptor<ResponderDefinition, string> AlertMask(string alertMask)
		{
			return new ResponderDefinitionIndex.ResponderDefinitionIndexDescriptorForAlertMask(alertMask);
		}

		// Token: 0x02000027 RID: 39
		private class ResponderDefinitionIndexDescriptorForAlertMask : WorkDefinitionIndex<ResponderDefinition>.WorkDefinitionIndexBase<ResponderDefinition, string>
		{
			// Token: 0x060002BF RID: 703 RVA: 0x0000BF4F File Offset: 0x0000A14F
			internal ResponderDefinitionIndexDescriptorForAlertMask(string key) : base(key)
			{
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000C030 File Offset: 0x0000A230
			public override IEnumerable<string> GetKeyValues(ResponderDefinition item)
			{
				yield return item.AlertMask;
				yield break;
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000C080 File Offset: 0x0000A280
			public override IDataAccessQuery<ResponderDefinition> ApplyIndexRestriction(IDataAccessQuery<ResponderDefinition> query)
			{
				IEnumerable<ResponderDefinition> enumerable = from d in query
				select d;
				if (IndexCapabilities.SupportsCaseInsensitiveStringComparison)
				{
					enumerable = from d in enumerable
					where d.AlertMask.Equals(base.Key, StringComparison.OrdinalIgnoreCase)
					select d;
				}
				else
				{
					enumerable = from d in enumerable
					where d.AlertMask.Equals(base.Key)
					select d;
				}
				return query.AsDataAccessQuery<ResponderDefinition>(enumerable);
			}
		}
	}
}
