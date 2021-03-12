using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MeetingCancellationCheck : ConsistencyCheckBase<ConsistencyCheckResult>
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002B48 File Offset: 0x00000D48
		internal MeetingCancellationCheck(CalendarValidationContext context)
		{
			IEnumerable<ConsistencyCheckType> dependsOnCheckPassList = null;
			if (context.BaseRole == RoleType.Attendee && context.AttendeeItem.IsCancelled)
			{
				dependsOnCheckPassList = MeetingCancellationCheck.cancelledAttendeeDependsOnCheckPassList;
			}
			this.Initialize(ConsistencyCheckType.MeetingCancellationCheck, "Checkes to make sure that the meeting cancellations statuses match.", SeverityType.Error, context, dependsOnCheckPassList);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B88 File Offset: 0x00000D88
		protected override ConsistencyCheckResult DetectInconsistencies()
		{
			ConsistencyCheckResult consistencyCheckResult = ConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			bool isCancelled = base.Context.BaseItem.IsCancelled;
			bool isCancelled2 = base.Context.OppositeItem.IsCancelled;
			if (isCancelled != isCancelled2)
			{
				consistencyCheckResult.Status = CheckStatusType.Failed;
				consistencyCheckResult.AddInconsistency(base.Context, PropertyInconsistency.CreateInstance(base.Context.OppositeRole, CalendarInconsistencyFlag.Cancellation, "IsCancelled", isCancelled, isCancelled2, base.Context));
			}
			return consistencyCheckResult;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002C0A File Offset: 0x00000E0A
		protected override void ProcessResult(ConsistencyCheckResult result)
		{
			result.ShouldBeReported = true;
		}

		// Token: 0x0400000E RID: 14
		internal const string CheckDescription = "Checkes to make sure that the meeting cancellations statuses match.";

		// Token: 0x0400000F RID: 15
		protected static IEnumerable<ConsistencyCheckType> cancelledAttendeeDependsOnCheckPassList = new ConsistencyCheckType[]
		{
			ConsistencyCheckType.AttendeeOnListCheck
		};
	}
}
