using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.Exchange.Services.OnlineMeetings.ResourceContract;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x02000022 RID: 34
	internal static class UcwaNewExtensions
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003614 File Offset: 0x00001814
		internal static AttendanceAnnouncementsStatus ToOnlineMeetingValue(this EntryExitAnnouncement thisObject)
		{
			switch (thisObject)
			{
			case EntryExitAnnouncement.Unsupported:
				return AttendanceAnnouncementsStatus.Unsupported;
			case EntryExitAnnouncement.Disabled:
				return AttendanceAnnouncementsStatus.Disabled;
			case EntryExitAnnouncement.Enabled:
				return AttendanceAnnouncementsStatus.Enabled;
			default:
				throw new ArgumentOutOfRangeException("thisObject");
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003648 File Offset: 0x00001848
		internal static AutomaticLeaderAssignment ToOnlineMeetingValue(this AutomaticLeaderAssignment thisObject)
		{
			if (thisObject == AutomaticLeaderAssignment.Disabled)
			{
				return AutomaticLeaderAssignment.Disabled;
			}
			if (thisObject == AutomaticLeaderAssignment.SameEnterprise)
			{
				return AutomaticLeaderAssignment.SameEnterprise;
			}
			if (thisObject != (AutomaticLeaderAssignment)((ulong)-2147483648))
			{
				throw new ArgumentOutOfRangeException("thisObject");
			}
			return AutomaticLeaderAssignment.Everyone;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003680 File Offset: 0x00001880
		internal static AccessLevel ToOnlineMeetingValue(this AccessLevel thisObject)
		{
			switch (thisObject)
			{
			case AccessLevel.None:
				return AccessLevel.None;
			case AccessLevel.SameEnterprise:
				return AccessLevel.SameEnterprise;
			case AccessLevel.Locked:
				return AccessLevel.Locked;
			case AccessLevel.Invited:
				return AccessLevel.Invited;
			case AccessLevel.Everyone:
				return AccessLevel.Everyone;
			default:
				throw new ArgumentOutOfRangeException("thisObject");
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000036C0 File Offset: 0x000018C0
		internal static LobbyBypass ToOnlineMeetingValue(this LobbyBypassForPhoneUsers thisObject)
		{
			switch (thisObject)
			{
			case LobbyBypassForPhoneUsers.Disabled:
				return LobbyBypass.Disabled;
			case LobbyBypassForPhoneUsers.Enabled:
				return LobbyBypass.Enabled;
			default:
				throw new ArgumentOutOfRangeException("thisObject");
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000036F0 File Offset: 0x000018F0
		internal static MeetingPolicies ToOnlineMeetingValue(this OnlineMeetingPoliciesResource thisObject)
		{
			return new MeetingPolicies
			{
				AttendanceAnnouncementsStatus = ((thisObject.EntryExitAnnouncement == GenericPolicy.Enabled) ? AttendanceAnnouncementsStatus.Enabled : AttendanceAnnouncementsStatus.Disabled),
				ExternalUserRecording = ((thisObject.ExternalUserMeetingRecording == GenericPolicy.Enabled) ? Policy.Enabled : Policy.Disabled),
				MeetingRecording = ((thisObject.MeetingRecording == GenericPolicy.Enabled) ? Policy.Enabled : Policy.Disabled),
				MeetingSize = thisObject.MeetingSize,
				PstnUserAdmission = ((thisObject.PhoneUserAdmission == GenericPolicy.Enabled) ? Policy.Enabled : Policy.Disabled),
				VoipAudio = ((thisObject.VoipAudio == GenericPolicy.Enabled) ? Policy.Enabled : Policy.Disabled)
			};
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003774 File Offset: 0x00001974
		internal static DefaultValues ToOnlineMeetingDefaultValues(this OnlineMeetingDefaultValuesResource thisObject)
		{
			return new DefaultValues
			{
				AccessLevel = thisObject.AccessLevel.ToOnlineMeetingValue(),
				AttendanceAnnouncementsStatus = thisObject.EntryExitAnnouncement.ToOnlineMeetingValue(),
				AutomaticLeaderAssignment = thisObject.AutomaticLeaderAssignment.ToOnlineMeetingValue(),
				PstnLobbyByPass = thisObject.LobbyBypassForPhoneUsers.ToOnlineMeetingValue()
			};
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000037D0 File Offset: 0x000019D0
		internal static DialInInformation ToOnlineMeetingDialInValues(this PhoneDialInInformationResource thisObject)
		{
			return new DialInInformation
			{
				DialInRegions = thisObject.ToOnlineMeetingDialInRegions(),
				ExternalDirectoryUri = thisObject.ExternalDirectoryUri,
				InternalDirectoryUri = thisObject.InternalDirectoryUri,
				IsAudioConferenceProviderEnabled = thisObject.IsAudioConferenceProviderEnabled,
				ParticipantPassCode = thisObject.ParticipantPassCode,
				TollFreeNumbers = thisObject.TollFreeNumbers,
				TollNumber = thisObject.TollNumber
			};
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal static DialInRegions ToOnlineMeetingDialInRegions(this PhoneDialInInformationResource thisObject)
		{
			if (thisObject.DialInRegions != null)
			{
				IEnumerable<DialInRegion> source = from region in thisObject.DialInRegions
				where region != null && (string.IsNullOrEmpty(thisObject.DefaultRegion) || string.Compare(thisObject.DefaultRegion, region.Name, StringComparison.OrdinalIgnoreCase) == 0)
				select new DialInRegion
				{
					Languages = ((region.Languages != null) ? new Collection<string>(region.Languages) : new Collection<string>()),
					Name = region.Name,
					Number = region.Number
				};
				return new DialInRegions(source.ToList<DialInRegion>());
			}
			return new DialInRegions();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003944 File Offset: 0x00001B44
		internal static CustomizationValues ToOnlineMeetingCustomizationValues(this OnlineMeetingInvitationCustomizationResource thisObject)
		{
			return new CustomizationValues
			{
				EnterpriseHelpUrl = thisObject.EnterpriseHelpUrl,
				InvitationFooterText = thisObject.InvitationFooterText,
				InvitationHelpUrl = thisObject.InvitationHelpUrl,
				InvitationLegalUrl = thisObject.InvitationLegalUrl,
				InvitationLogoUrl = thisObject.InvitationLogoUrl
			};
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003998 File Offset: 0x00001B98
		internal static OnlineMeeting ToOnlineMeetingValue(this OnlineMeetingResource thisObject)
		{
			string[] leaders = thisObject.Leaders;
			IEnumerable<string> source = (leaders != null) ? ((IEnumerable<string>)leaders) : Enumerable.Empty<string>();
			string[] attendees = thisObject.Attendees;
			IEnumerable<string> source2 = (attendees != null) ? ((IEnumerable<string>)attendees) : Enumerable.Empty<string>();
			OnlineMeeting onlineMeeting = new OnlineMeeting(new Collection<string>(source.ToList<string>()), new Collection<string>(source2.ToList<string>()))
			{
				Accesslevel = ((thisObject.AccessLevel != null) ? thisObject.AccessLevel.Value.ToOnlineMeetingValue() : AccessLevel.None),
				AttendanceAnnouncementStatus = ((thisObject.EntryExitAnnouncement != null) ? thisObject.EntryExitAnnouncement.Value.ToOnlineMeetingValue() : AttendanceAnnouncementsStatus.Unsupported),
				AutomaticLeaderAssignment = ((thisObject.AutomaticLeaderAssignment != null) ? thisObject.AutomaticLeaderAssignment.Value.ToOnlineMeetingValue() : AutomaticLeaderAssignment.Disabled),
				Description = thisObject.Description,
				ExpiryTime = thisObject.ExpirationTime,
				Id = thisObject.OnlineMeetingId,
				IsActive = thisObject.IsActive,
				MeetingUri = thisObject.OnlineMeetingUri,
				OrganizerUri = thisObject.OrganizerUri,
				PstnMeetingId = thisObject.ConferenceId,
				PstnUserLobbyBypass = ((thisObject.LobbyBypassForPhoneUsers != null) ? thisObject.LobbyBypassForPhoneUsers.Value.ToOnlineMeetingValue() : LobbyBypass.Disabled),
				Subject = thisObject.Subject,
				WebUrl = thisObject.JoinUrl
			};
			if (thisObject.OnlineMeetingRel != null && thisObject.OnlineMeetingRel.Value == OnlineMeetingRel.MyAssignedOnlineMeeting)
			{
				onlineMeeting.IsAssignedMeeting = true;
			}
			else
			{
				onlineMeeting.IsAssignedMeeting = false;
			}
			return onlineMeeting;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003B49 File Offset: 0x00001D49
		internal static string GetReasonHeader(this WebResponse thisObject)
		{
			if (thisObject != null && thisObject.Headers != null)
			{
				return thisObject.Headers["X-Ms-diagnostics"] ?? string.Empty;
			}
			return string.Empty;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003B78 File Offset: 0x00001D78
		internal static string ToLogString(this HttpOperationException thisObject)
		{
			if (thisObject == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder((thisObject.InnerException != null) ? thisObject.InnerException.Message : thisObject.Message);
			if (thisObject.ErrorInformation != null)
			{
				stringBuilder.AppendFormat("ErrorInformation object: {0}.", thisObject.ErrorInformation.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003BD4 File Offset: 0x00001DD4
		internal static string ToLogString(this AggregateException thisObject)
		{
			if (thisObject == null || thisObject.InnerExceptions == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Exception ex in thisObject.InnerExceptions)
			{
				if (ex is HttpOperationException)
				{
					stringBuilder.AppendFormat("HttpOperationException:{0};", ((HttpOperationException)ex).ToLogString());
				}
				else
				{
					stringBuilder.AppendFormat("{0}:{1};", ex.GetType(), (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
