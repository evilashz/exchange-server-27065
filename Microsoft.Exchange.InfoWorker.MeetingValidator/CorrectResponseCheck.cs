using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CorrectResponseCheck : ConsistencyCheckBase<ConsistencyCheckResult>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000024A0 File Offset: 0x000006A0
		internal CorrectResponseCheck(CalendarValidationContext context)
		{
			SeverityType severity = context.AreItemsOccurrences ? SeverityType.Warning : SeverityType.Error;
			this.Initialize(ConsistencyCheckType.CorrectResponseCheck, "Checkes to make sure that the organizaer has the correct response recorded for the attendee.", severity, context, (context.BaseRole == RoleType.Attendee) ? CorrectResponseCheck.attendeeDependsOnCheckPassList : null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024E0 File Offset: 0x000006E0
		protected override ConsistencyCheckResult DetectInconsistencies()
		{
			ConsistencyCheckResult consistencyCheckResult = ConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			ResponseType responseType = base.Context.AttendeeItem.ResponseType;
			ResponseType responseType2 = base.Context.Attendee.ResponseType;
			ExDateTime replyTime = base.Context.Attendee.ReplyTime;
			ExDateTime appointmentReplyTime = base.Context.AttendeeItem.AppointmentReplyTime;
			if (!ExDateTime.MinValue.Equals(replyTime) && responseType != ResponseType.NotResponded && responseType2 != responseType)
			{
				consistencyCheckResult.Status = CheckStatusType.Failed;
				consistencyCheckResult.AddInconsistency(base.Context, ResponseInconsistency.CreateInstance(responseType, responseType2, appointmentReplyTime, replyTime, base.Context));
			}
			return consistencyCheckResult;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002581 File Offset: 0x00000781
		protected override void ProcessResult(ConsistencyCheckResult result)
		{
			result.ShouldBeReported = true;
		}

		// Token: 0x04000007 RID: 7
		internal const string CheckDescription = "Checkes to make sure that the organizaer has the correct response recorded for the attendee.";

		// Token: 0x04000008 RID: 8
		protected static IEnumerable<ConsistencyCheckType> attendeeDependsOnCheckPassList = new ConsistencyCheckType[]
		{
			ConsistencyCheckType.AttendeeOnListCheck
		};
	}
}
