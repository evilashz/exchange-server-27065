using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ConsistencyCheckBase<ResultType> where ResultType : ConsistencyCheckResult
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected virtual void Initialize(ConsistencyCheckType type, string description, SeverityType severity, CalendarValidationContext context, IEnumerable<ConsistencyCheckType> dependsOnCheckPassList)
		{
			this.Type = type;
			this.Description = description;
			this.Severity = severity;
			this.Context = context;
			this.dependsOnCheckPassList = dependsOnCheckPassList;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020F7 File Offset: 0x000002F7
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020FF File Offset: 0x000002FF
		internal ConsistencyCheckType Type { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00000308
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		internal string Description { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002121 File Offset: 0x00000321
		internal SeverityType Severity { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002132 File Offset: 0x00000332
		internal CalendarValidationContext Context { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x0000213C File Offset: 0x0000033C
		internal ResultType Run()
		{
			ResultType result = this.DetectInconsistencies();
			this.ProcessResult(result);
			result.Severity = this.Severity;
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000216C File Offset: 0x0000036C
		internal virtual bool PreProcess(MeetingComparisonResult totalResults)
		{
			if (this.dependsOnCheckPassList != null)
			{
				foreach (ConsistencyCheckType check in this.dependsOnCheckPassList)
				{
					CheckStatusType? checkStatusType = totalResults[check];
					if (checkStatusType == null || checkStatusType.Value != CheckStatusType.Passed)
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600000C RID: 12
		protected abstract ResultType DetectInconsistencies();

		// Token: 0x0600000D RID: 13
		protected abstract void ProcessResult(ResultType result);

		// Token: 0x04000001 RID: 1
		private IEnumerable<ConsistencyCheckType> dependsOnCheckPassList;
	}
}
