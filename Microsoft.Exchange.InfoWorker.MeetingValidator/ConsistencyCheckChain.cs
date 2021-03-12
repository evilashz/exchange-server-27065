using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConsistencyCheckChain<ResultType> where ResultType : ConsistencyCheckResult
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000285F File Offset: 0x00000A5F
		private ConsistencyCheckChain()
		{
			this.checkList = null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002870 File Offset: 0x00000A70
		protected bool PerformCheck<CheckResultType>(ConsistencyCheckBase<CheckResultType> check) where CheckResultType : ResultType
		{
			bool result;
			if (check.PreProcess(this.totalResult))
			{
				CheckResultType checkResultType = check.Run();
				if (checkResultType.ShouldBeReported)
				{
					this.totalResult.AddCheckResult(checkResultType);
				}
				result = this.ShouldContinue((ResultType)((object)checkResultType));
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028C9 File Offset: 0x00000AC9
		internal ConsistencyCheckChain(MeetingComparisonResult totalResult) : this(1, totalResult)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028D3 File Offset: 0x00000AD3
		internal ConsistencyCheckChain(int capacity, MeetingComparisonResult totalResult)
		{
			this.checkList = new List<ConsistencyCheckBase<ResultType>>(capacity);
			this.totalResult = totalResult;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028EE File Offset: 0x00000AEE
		internal void AddCheck(ConsistencyCheckBase<ResultType> check)
		{
			this.checkList.Add(check);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028FC File Offset: 0x00000AFC
		internal void PerformChecks()
		{
			foreach (ConsistencyCheckBase<ResultType> check in this.checkList)
			{
				if (!this.PerformCheck<ResultType>(check))
				{
					break;
				}
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002954 File Offset: 0x00000B54
		protected virtual bool ShouldContinue(ResultType lastCheckResult)
		{
			return true;
		}

		// Token: 0x0400000A RID: 10
		private List<ConsistencyCheckBase<ResultType>> checkList;

		// Token: 0x0400000B RID: 11
		private MeetingComparisonResult totalResult;
	}
}
