using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200000D RID: 13
	internal abstract class MonitorResultIndex : WorkItemResultIndex<MonitorResult>
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00006AF6 File Offset: 0x00004CF6
		internal static IIndexDescriptor<MonitorResult, string> ComponentNameAndExecutionEndTime(string componentName, DateTime minExecutionEndTime)
		{
			return new MonitorResultIndex.MonitorResultIndexDescriptorForComponentNameAndExecutionEndTime(componentName, minExecutionEndTime);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006AFF File Offset: 0x00004CFF
		internal static IIndexDescriptor<MonitorResult, DateTime> ExecutionEndTime(DateTime executionEndTime)
		{
			return new MonitorResultIndex.MonitorResultIndexDescriptorForExecutionEndTime(executionEndTime);
		}

		// Token: 0x0200000E RID: 14
		private class MonitorResultIndexDescriptorForComponentNameAndExecutionEndTime : WorkItemResultIndex<MonitorResult>.WorkItemResultIndexBase<MonitorResult, string>
		{
			// Token: 0x060000B4 RID: 180 RVA: 0x00006B0F File Offset: 0x00004D0F
			internal MonitorResultIndexDescriptorForComponentNameAndExecutionEndTime(string key, DateTime minExecutionEndTime) : base(key, minExecutionEndTime)
			{
			}

			// Token: 0x060000B5 RID: 181 RVA: 0x00006BF4 File Offset: 0x00004DF4
			public override IEnumerable<string> GetKeyValues(MonitorResult item)
			{
				yield return item.ComponentName;
				yield break;
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00006C40 File Offset: 0x00004E40
			public override IDataAccessQuery<MonitorResult> ApplyIndexRestriction(IDataAccessQuery<MonitorResult> query)
			{
				IEnumerable<MonitorResult> query2 = from r in query
				where r.ComponentName == base.Key && r.ExecutionEndTime > base.MinExecutionEndTime
				select r;
				return query.AsDataAccessQuery<MonitorResult>(query2);
			}
		}

		// Token: 0x0200000F RID: 15
		private class MonitorResultIndexDescriptorForExecutionEndTime : WorkItemResultIndex<MonitorResult>.WorkItemResultIndexBase<MonitorResult, DateTime>
		{
			// Token: 0x060000B8 RID: 184 RVA: 0x00006C67 File Offset: 0x00004E67
			internal MonitorResultIndexDescriptorForExecutionEndTime(DateTime key) : base(key, key)
			{
			}

			// Token: 0x060000B9 RID: 185 RVA: 0x00006D54 File Offset: 0x00004F54
			public override IEnumerable<DateTime> GetKeyValues(MonitorResult item)
			{
				yield return item.ExecutionEndTime;
				yield break;
			}

			// Token: 0x060000BA RID: 186 RVA: 0x00006D8C File Offset: 0x00004F8C
			public override IDataAccessQuery<MonitorResult> ApplyIndexRestriction(IDataAccessQuery<MonitorResult> query)
			{
				IEnumerable<MonitorResult> query2 = from r in query
				where r.ExecutionEndTime > base.Key
				select r;
				return query.AsDataAccessQuery<MonitorResult>(query2);
			}
		}
	}
}
