using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AttendeeOnListCheck : ConsistencyCheckBase<ConsistencyCheckResult>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000021E4 File Offset: 0x000003E4
		internal AttendeeOnListCheck(CalendarValidationContext context)
		{
			this.Initialize(ConsistencyCheckType.AttendeeOnListCheck, "Checkes to make sure that the attendee is listed on the organizer's list of attendees.", SeverityType.Error, context, null);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021FC File Offset: 0x000003FC
		protected override ConsistencyCheckResult DetectInconsistencies()
		{
			ConsistencyCheckResult result = ConsistencyCheckResult.CreateInstance(base.Type, base.Description);
			if (base.Context.OrganizerItem != null)
			{
				switch (base.Context.AttendeeItem.ResponseType)
				{
				case ResponseType.None:
				case ResponseType.Organizer:
					this.FailCheck(result);
					break;
				default:
					this.CheckAttendeeList(result);
					break;
				}
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000225A File Offset: 0x0000045A
		protected override void ProcessResult(ConsistencyCheckResult result)
		{
			result.ShouldBeReported = (result.Status == CheckStatusType.Passed || !base.Context.BaseItem.IsCancelled);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002284 File Offset: 0x00000484
		private void CheckAttendeeList(ConsistencyCheckResult result)
		{
			if (!base.Context.OrganizerItem.ResponseRequested)
			{
				return;
			}
			Participant participant = new Participant(base.Context.Attendee.ExchangePrincipal);
			bool flag = false;
			bool flag2 = false;
			DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, base.Context.Attendee.ExchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings(), 90, "CheckAttendeeList", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\MeetingValidator\\ConsistencyChecks\\AttendeeListCheck.cs");
			foreach (Attendee attendee in base.Context.OrganizerItem.AttendeeCollection)
			{
				bool flag3 = attendee.Participant.RoutingType == "EX" && (attendee.IsDistributionList() ?? true);
				if (flag3)
				{
					flag2 = true;
				}
				else if (attendee.Participant.ValidationStatus == ParticipantValidationStatus.NoError && !(attendee.Participant.RoutingType == null) && !(attendee.Participant.RoutingType == "MAPIPDL") && !string.IsNullOrEmpty(attendee.Participant.EmailAddress) && Participant.HasSameEmail(participant, attendee.Participant, base.Context.LocalUser.ExchangePrincipal))
				{
					base.Context.Attendee.SetResponse(attendee.ResponseType, attendee.ReplyTime);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				flag = flag2;
			}
			if (!flag && (base.Context.AttendeeItem.ResponseType == ResponseType.Accept || base.Context.AttendeeItem.ResponseType == ResponseType.Tentative))
			{
				this.FailCheck(result);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002444 File Offset: 0x00000644
		private void FailCheck(ConsistencyCheckResult result)
		{
			result.Status = CheckStatusType.Failed;
			result.AddInconsistency(base.Context, ResponseInconsistency.CreateInstance(base.Context.AttendeeItem.ResponseType, ResponseType.None, base.Context.AttendeeItem.AppointmentReplyTime, base.Context.Attendee.ReplyTime, base.Context));
		}

		// Token: 0x04000006 RID: 6
		internal const string CheckDescription = "Checkes to make sure that the attendee is listed on the organizer's list of attendees.";
	}
}
