using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.AssistantsClientResources;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x02000123 RID: 291
	internal class ResponseTextGenerator
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0004C860 File Offset: 0x0004AA60
		internal ResponseTextGenerator(CultureInfo cultureInfo, ExTimeZone calendarItemTz, bool isRecurrence, string additionalResponse, bool includeResponseDetails, bool includeOrganizerInfo, string resourceDisplayName, List<MeetingConflict> conflicts)
		{
			this.CultureInfo = cultureInfo;
			this.CalendarItemTz = calendarItemTz;
			this.IsRecurrence = isRecurrence;
			this.AdditionalResponse = additionalResponse;
			this.IncludeOrganizerInfo = includeOrganizerInfo;
			this.IncludeResponseDetails = includeResponseDetails;
			this.ResourceDisplayName = resourceDisplayName;
			this.Conflicts = conflicts;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0004C8B0 File Offset: 0x0004AAB0
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0004C8C7 File Offset: 0x0004AAC7
		public CultureInfo DelegateCultureInfo
		{
			get
			{
				if (this.delegateCultureInfo == null)
				{
					return this.CultureInfo;
				}
				return this.delegateCultureInfo;
			}
			set
			{
				this.delegateCultureInfo = ((value != null) ? value : this.CultureInfo);
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0004CA90 File Offset: 0x0004AC90
		internal void GenerateSingleOutOfPolicyResponse(ConflictType type, int leadDays, ExDateTime validWindowLocal)
		{
			this.GetResponseBody = delegate()
			{
				string result;
				if (type == ConflictType.Booked)
				{
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descSingleBooked.ToString(this.CultureInfo) + this.GeneratingConflictingMeetingTable(Strings.descSingleConflicts));
				}
				else if (type == ConflictType.BookedSomeAccepted)
				{
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descSingleBookedSomeAccepted.ToString(this.CultureInfo) + this.GeneratingConflictingMeetingTable(Strings.descSingleConflicts));
				}
				else
				{
					string date = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descBookingWindow(leadDays.ToString(), date).ToString(this.CultureInfo));
				}
				return result;
			};
			this.GetResponseSubjectPrefix = (() => Strings.descDeclined.ToString(this.CultureInfo));
			this.GetDelegateBody = delegate()
			{
				string mainBody;
				if (type == ConflictType.Booked)
				{
					mainBody = Strings.descDelegateConflicts.ToString(this.DelegateCultureInfo);
				}
				else
				{
					string date = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.DelegateCultureInfo.DateTimeFormat.ShortDatePattern);
					mainBody = Strings.descDelegateEndDate(date).ToString(this.DelegateCultureInfo);
				}
				return this.GenerateOutOfPolicyDelegateResponse(mainBody);
			};
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0004CEC4 File Offset: 0x0004B0C4
		internal void GenerateRecurringOutOfPolicyResponse(ConflictType type, int leadDays, ExDateTime validWindowLocal)
		{
			this.GetResponseBody = delegate()
			{
				string result;
				if (type == ConflictType.Booked)
				{
					result = this.GenerateResponseWithTimeZoneInfo(this.GeneratingConflictingMeetingTable(Strings.descRecurringBooked));
				}
				else if (type == ConflictType.BookingWindow)
				{
					string date = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descBookingWindow(leadDays.ToString(), date).ToString(this.CultureInfo));
				}
				else if (type == ConflictType.EndDate)
				{
					string endDate = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descRecurringEndDate(endDate).ToString(this.CultureInfo));
				}
				else if (type == ConflictType.Recurring)
				{
					this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descRecurringNotAllowed.ToString(this.CultureInfo));
				}
				else
				{
					string endDate2 = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descRecurringEndDateWindow(leadDays.ToString(), endDate2).ToString(this.CultureInfo));
				}
				return result;
			};
			this.GetResponseSubjectPrefix = (() => Strings.descDeclinedAll.ToString(this.CultureInfo));
			this.GetDelegateBody = delegate()
			{
				string mainBody = string.Empty;
				if (type == ConflictType.Booked)
				{
					mainBody = Strings.descDelegateConflicts.ToString(this.DelegateCultureInfo);
				}
				else if (type == ConflictType.BookingWindow)
				{
					string date = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.DelegateCultureInfo.DateTimeFormat.ShortDatePattern);
					mainBody = Strings.descBookingWindow(leadDays.ToString(), date).ToString(this.DelegateCultureInfo);
				}
				else if (type == ConflictType.EndDate)
				{
					string date2 = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.DelegateCultureInfo.DateTimeFormat.ShortDatePattern);
					mainBody = Strings.descDelegateEndDate(date2).ToString(this.DelegateCultureInfo);
				}
				else if (type == ConflictType.Recurring)
				{
					this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.DelegateCultureInfo.DateTimeFormat.ShortDatePattern);
					mainBody = Strings.descDelegateRecurring.ToString(this.DelegateCultureInfo);
				}
				else if (type == ConflictType.EndDateWindow)
				{
					string date3 = this.CalendarItemTz.ConvertDateTime(validWindowLocal).ToString(this.DelegateCultureInfo.DateTimeFormat.ShortDatePattern);
					mainBody = Strings.descDelegateEndDate(date3).ToString(this.DelegateCultureInfo);
				}
				return this.GenerateOutOfPolicyDelegateResponse(mainBody);
			};
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0004D000 File Offset: 0x0004B200
		internal void GenerateOutOfPolicyMeetingTooLong(int maxDurationInMinutes)
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descToLong(maxDurationInMinutes.ToString(this.CultureInfo)).ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = delegate()
			{
				if (!this.IsRecurrence)
				{
					return Strings.descDeclined.ToString(this.CultureInfo);
				}
				return Strings.descDeclinedAll.ToString(this.CultureInfo);
			};
			this.GetDelegateBody = (() => this.GenerateOutOfPolicyDelegateResponse(Strings.descDelegateTooLong(maxDurationInMinutes.ToString(this.DelegateCultureInfo)).ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0004D0E2 File Offset: 0x0004B2E2
		internal void GenerateOutOfPolicyRoleNotAllowed()
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descRoleNotAllowed.ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = delegate()
			{
				if (!this.IsRecurrence)
				{
					return Strings.descDeclined.ToString(this.CultureInfo);
				}
				return Strings.descDeclinedAll.ToString(this.CultureInfo);
			};
			this.GetDelegateBody = (() => this.GenerateOutOfPolicyDelegateResponse(Strings.descDelegateNoPerm.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0004D174 File Offset: 0x0004B374
		internal void GenerateInPolicyRoleNotAllowed()
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descRoleNotAllowed.ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = (() => string.Empty);
			this.GetDelegateBody = (() => this.GenerateInPolicyDelegateResponse(Strings.descDelegateNoPerm.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0004D252 File Offset: 0x0004B452
		internal void GenerateDeclineMeetingPast()
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descInThePast.ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = delegate()
			{
				if (!this.IsRecurrence)
				{
					return Strings.descDeclined.ToString(this.CultureInfo);
				}
				return Strings.descDeclinedAll.ToString(this.CultureInfo);
			};
			this.GetDelegateBody = (() => this.GenerateOutOfPolicyDelegateResponse(Strings.descInThePast.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0004D360 File Offset: 0x0004B560
		internal void GenerateOutOfPolicyOutOfWorkingHours(WorkingHours wh)
		{
			WorkingPeriod workingPeriod = wh.WorkingPeriodArray[0];
			StringCollection stringCollection = new StringCollection();
			string[] dayNames = this.CultureInfo.DateTimeFormat.DayNames;
			for (int i = 0; i < 7; i++)
			{
				DaysOfWeek daysOfWeek = WorkingHours.DayToDays((DayOfWeek)i);
				if ((workingPeriod.DayOfWeek & daysOfWeek) != (DaysOfWeek)0)
				{
					stringCollection.Add(dayNames[i]);
				}
			}
			ExDateTime now = ExDateTime.GetNow(this.CalendarItemTz);
			ExDateTime exDateTime = new ExDateTime(this.CalendarItemTz, now.Year, now.Month, now.Day, this.CultureInfo.Calendar);
			ExDateTime exDateTime2 = exDateTime.AddMinutes((double)workingPeriod.StartTimeInMinutes);
			ExDateTime exDateTime3 = exDateTime.AddMinutes((double)workingPeriod.EndTimeInMinutes);
			string startDisplay = exDateTime2.ToString(this.CultureInfo.DateTimeFormat.ShortTimePattern);
			string endDisplay = exDateTime3.ToString(this.CultureInfo.DateTimeFormat.ShortTimePattern);
			string daysString;
			if (stringCollection.Count == 0)
			{
				daysString = string.Empty;
			}
			else
			{
				daysString = " " + Strings.descOn(stringCollection[0]).ToString(this.CultureInfo);
				if (stringCollection.Count > 1)
				{
					for (int j = 1; j < stringCollection.Count - 1; j++)
					{
						daysString = Strings.descCommaList(daysString, stringCollection[j]).ToString(this.CultureInfo);
					}
					daysString = Strings.descAndList(daysString, stringCollection[stringCollection.Count - 1]).ToString(this.CultureInfo);
				}
			}
			this.GetResponseBody = (() => this.GenerateResponseWithTimeZoneInfo(Strings.descWorkingHours(startDisplay, endDisplay, daysString).ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = delegate()
			{
				if (!this.IsRecurrence)
				{
					return Strings.descDeclined.ToString(this.CultureInfo);
				}
				return Strings.descDeclinedAll.ToString(this.CultureInfo);
			};
			this.GetDelegateBody = (() => this.GenerateOutOfPolicyDelegateResponse(Strings.descDelegateWorkHours(Strings.descWorkingHours(startDisplay, endDisplay, daysString).ToString(this.DelegateCultureInfo))));
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0004D5C2 File Offset: 0x0004B7C2
		internal void GenerateSingleInPolicyResponse()
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descSingleAccepted.ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = (() => Strings.descAccepted.ToString(this.CultureInfo));
			this.GetDelegateBody = (() => this.GenerateInPolicyDelegateResponse(Strings.descDelegateNoPerm.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0004D740 File Offset: 0x0004B940
		internal void GenerateRecurringInPolicyResponse(RecurringAcceptType type, OccurrenceInfo last, ExDateTime bookingWindowLocal)
		{
			bookingWindowLocal = bookingWindowLocal.AddSeconds(-1.0);
			ExDateTime exDateTime;
			if (last == null || last.EndTime > bookingWindowLocal)
			{
				exDateTime = this.CalendarItemTz.ConvertDateTime(bookingWindowLocal);
			}
			else
			{
				exDateTime = this.CalendarItemTz.ConvertDateTime(last.EndTime);
			}
			string date = exDateTime.ToString(this.CultureInfo.DateTimeFormat.ShortDatePattern);
			this.GetResponseBody = delegate()
			{
				string result;
				if (type == RecurringAcceptType.Free)
				{
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descRecurringAccepted(date).ToString(this.CultureInfo));
				}
				else
				{
					result = this.GenerateResponseWithTimeZoneInfo(Strings.descRecurringAccepted(date).ToString(this.CultureInfo) + this.GeneratingConflictingMeetingTable(Strings.descRecurringSomeAccepted));
				}
				return result;
			};
			this.GetResponseSubjectPrefix = delegate()
			{
				string result;
				if (last == null || last.EndTime > bookingWindowLocal)
				{
					result = Strings.descAcceptedThrough(date).ToString(this.CultureInfo);
				}
				else if (type == RecurringAcceptType.Free)
				{
					result = Strings.descAcceptedAll.ToString(this.CultureInfo);
				}
				else
				{
					result = Strings.descAccepted.ToString(this.CultureInfo);
				}
				return result;
			};
			this.GetDelegateBody = (() => this.GenerateInPolicyDelegateResponse(Strings.descDelegateNoPerm.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0004D880 File Offset: 0x0004BA80
		internal void GenerateAcknowledgementResponse()
		{
			this.GetResponseBody = (() => this.GenerateResponse(Strings.descAcknowledgeReceived.ToString(this.CultureInfo)));
			this.GetResponseSubjectPrefix = (() => string.Empty);
			this.GetDelegateBody = (() => this.GenerateOutOfPolicyDelegateResponse(Strings.descDelegateNoPerm.ToString(this.DelegateCultureInfo)));
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004D8D4 File Offset: 0x0004BAD4
		private string GenerateResponse(string mainBody)
		{
			return this.GenerateResponse(mainBody, false);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
		private string GenerateResponse(string mainBody, bool includeTimeZoneInfo)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.AdditionalResponse) || this.IncludeResponseDetails)
			{
				stringBuilder.Append(this.GenerateHeader());
				if (this.IncludeResponseDetails)
				{
					stringBuilder.Append(mainBody);
					if (includeTimeZoneInfo)
					{
						stringBuilder.Append(this.GenerateTimeZoneInfo());
					}
				}
				stringBuilder.Append(this.GenerateFooter());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0004D948 File Offset: 0x0004BB48
		private string GenerateResponseWithTimeZoneInfo(string mainBody)
		{
			return this.GenerateResponse(mainBody, true);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0004D954 File Offset: 0x0004BB54
		private string GenerateOutOfPolicyDelegateResponse(string mainBody)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GenerateHeader());
			stringBuilder.Append(Strings.descOutOfPolicyDelegateMessage.ToString(this.DelegateCultureInfo));
			stringBuilder.Append(mainBody);
			if (this.Conflicts != null && this.Conflicts.Count != 0)
			{
				stringBuilder.Append(Strings.descDelegatePleaseVerify(this.ResourceDisplayName).ToString(this.DelegateCultureInfo));
				stringBuilder.Append(this.GeneratingConflictingMeetingTable(Strings.descDelegateConflictList));
			}
			stringBuilder.Append(this.GenerateFooter());
			return stringBuilder.ToString();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0004D9F0 File Offset: 0x0004BBF0
		private string GenerateInPolicyDelegateResponse(string mainBody)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.GenerateHeader());
			stringBuilder.Append(Strings.descInPolicyDelegateMessage.ToString(this.DelegateCultureInfo));
			stringBuilder.Append(mainBody);
			if (this.Conflicts != null && this.Conflicts.Count != 0)
			{
				stringBuilder.Append(Strings.descDelegatePleaseVerify(this.ResourceDisplayName).ToString(this.DelegateCultureInfo));
				stringBuilder.Append(this.GeneratingConflictingMeetingTable(Strings.descDelegateConflictList));
			}
			stringBuilder.Append(this.GenerateFooter());
			return stringBuilder.ToString();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004DA8C File Offset: 0x0004BC8C
		private string GeneratingConflictingMeetingTable(LocalizedString tableHeader)
		{
			string empty = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(tableHeader.ToString(this.CultureInfo));
			LocalizedString localizedString;
			if (this.IncludeOrganizerInfo)
			{
				localizedString = Strings.descMeetingOrganizerAndTimeLabel;
			}
			else
			{
				localizedString = Strings.descMeetingTimeLabel;
			}
			stringBuilder.Append("<table border=\"0\" cellpadding=\"2\"><tr><td width=\"29\">&nbsp;</td><td><b>" + Strings.descTahomaGreyMediumFontTag.ToString(this.CultureInfo) + localizedString.ToString(this.CultureInfo) + "</font></b></td></tr>");
			foreach (MeetingConflict meetingConflict in this.Conflicts)
			{
				stringBuilder.Append("<tr><td width=\"29\">&nbsp;</td><td>");
				stringBuilder.Append(Strings.descTahomaBlackMediumFontTag.ToString(this.CultureInfo));
				if (this.IncludeOrganizerInfo)
				{
					stringBuilder.Append("<a href=\"mailto:");
					stringBuilder.Append(meetingConflict.OrganizerSmtp);
					stringBuilder.Append("\">");
					stringBuilder.Append(meetingConflict.OrganizerDisplay + "</a>&nbsp;-&nbsp;");
				}
				stringBuilder.Append(Strings.descToList(new ExDateTime(this.CalendarItemTz, meetingConflict.MeetingStartTimeLocal.ToUniversalTime()).ToString(this.CultureInfo.DateTimeFormat.FullDateTimePattern), new ExDateTime(this.CalendarItemTz, meetingConflict.MeetingEndTimeLocal.ToUniversalTime()).ToString(this.CultureInfo.DateTimeFormat.FullDateTimePattern)).ToString(this.CultureInfo.DateTimeFormat));
				stringBuilder.Append("</font></td></tr>");
			}
			stringBuilder.Append("</table><br/>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0004DC8B File Offset: 0x0004BE8B
		public void GenerateMessageForCorruptWorkingHours()
		{
			this.GetResponseBody = (() => Strings.descCorruptWorkingHours.ToString(this.CultureInfo));
			this.GetResponseSubjectPrefix = (() => string.Empty);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0004DCC4 File Offset: 0x0004BEC4
		private string GenerateHeader()
		{
			return "<html><head></head><body>" + Strings.descTahomaBlackMediumFontTag.ToString(this.DelegateCultureInfo);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0004DCF0 File Offset: 0x0004BEF0
		private string GenerateFooter()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<HR/>");
			if (!string.IsNullOrEmpty(this.AdditionalResponse))
			{
				stringBuilder.Append(this.AdditionalResponse);
				stringBuilder.Append("<HR/>");
			}
			stringBuilder.Append(Strings.descArialGreySmallFontTag.ToString(this.DelegateCultureInfo));
			stringBuilder.Append(Strings.descCredit.ToString(this.DelegateCultureInfo));
			stringBuilder.Append("</font></body></html>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0004DD7C File Offset: 0x0004BF7C
		private string GenerateTimeZoneInfo()
		{
			return string.Concat(new string[]
			{
				Strings.descTahomaGreyMediumFontTag.ToString(this.DelegateCultureInfo),
				Strings.descTimeZoneInfo.ToString(this.DelegateCultureInfo),
				"&nbsp;",
				this.CalendarItemTz.LocalizableDisplayName.ToString(this.DelegateCultureInfo),
				"</font>"
			});
		}

		// Token: 0x0400073F RID: 1855
		internal readonly CultureInfo CultureInfo;

		// Token: 0x04000740 RID: 1856
		internal readonly ExTimeZone CalendarItemTz;

		// Token: 0x04000741 RID: 1857
		internal readonly bool IsRecurrence;

		// Token: 0x04000742 RID: 1858
		internal readonly string AdditionalResponse;

		// Token: 0x04000743 RID: 1859
		internal readonly bool IncludeResponseDetails;

		// Token: 0x04000744 RID: 1860
		internal readonly bool IncludeOrganizerInfo;

		// Token: 0x04000745 RID: 1861
		internal readonly string ResourceDisplayName;

		// Token: 0x04000746 RID: 1862
		internal readonly List<MeetingConflict> Conflicts;

		// Token: 0x04000747 RID: 1863
		internal ResponseTextGenerator.ResponseBodyDelegate GetResponseBody;

		// Token: 0x04000748 RID: 1864
		internal ResponseTextGenerator.ResponseSubjectPrefixDelegate GetResponseSubjectPrefix;

		// Token: 0x04000749 RID: 1865
		internal ResponseTextGenerator.ForwardBodyDelegate GetDelegateBody;

		// Token: 0x0400074A RID: 1866
		private CultureInfo delegateCultureInfo;

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x06000BC2 RID: 3010
		internal delegate string ResponseBodyDelegate();

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000BC6 RID: 3014
		internal delegate string ResponseSubjectPrefixDelegate();

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x06000BCA RID: 3018
		internal delegate string ForwardBodyDelegate();
	}
}
