using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200001E RID: 30
	internal abstract class ProbeResultIndex : WorkItemResultIndex<ProbeResult>
	{
		// Token: 0x06000218 RID: 536 RVA: 0x0000B061 File Offset: 0x00009261
		internal static IIndexDescriptor<ProbeResult, string> ScopeNameAndExecutionEndTime(string scopeName, DateTime minExecutionEndTime)
		{
			return new ProbeResultIndex.ProbeResultIndexDescriptorForScopeNameAndExecutionEndTime(scopeName, minExecutionEndTime);
		}

		// Token: 0x0200001F RID: 31
		private class ProbeResultIndexDescriptorForScopeNameAndExecutionEndTime : WorkItemResultIndex<ProbeResult>.WorkItemResultIndexBase<ProbeResult, string>
		{
			// Token: 0x0600021A RID: 538 RVA: 0x0000B072 File Offset: 0x00009272
			internal ProbeResultIndexDescriptorForScopeNameAndExecutionEndTime(string key, DateTime minExecutionEndTime) : base(key, minExecutionEndTime)
			{
			}

			// Token: 0x0600021B RID: 539 RVA: 0x0000B154 File Offset: 0x00009354
			public override IEnumerable<string> GetKeyValues(ProbeResult item)
			{
				yield return item.ScopeName;
				yield break;
			}

			// Token: 0x0600021C RID: 540 RVA: 0x0000B1A0 File Offset: 0x000093A0
			public override IDataAccessQuery<ProbeResult> ApplyIndexRestriction(IDataAccessQuery<ProbeResult> query)
			{
				IEnumerable<ProbeResult> query2 = from r in query
				where r.ScopeName == base.Key && r.ExecutionEndTime > base.MinExecutionEndTime
				select r;
				return query.AsDataAccessQuery<ProbeResult>(query2);
			}
		}
	}
}
