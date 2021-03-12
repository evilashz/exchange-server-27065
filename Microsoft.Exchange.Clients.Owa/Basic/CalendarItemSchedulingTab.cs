using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Core.Directory;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MeetingSuggestions;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000092 RID: 146
	public class CalendarItemSchedulingTab : OwaForm
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00024398 File Offset: 0x00022598
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (string.Equals(this.Action, "New", StringComparison.Ordinal))
			{
				this.isNew = true;
			}
			base.Item = (this.calendarItemBase = (base.OwaContext.PreFormActionData as CalendarItemBase));
			this.CurrentFolderStoreObjectId = EditCalendarItemHelper.GetCalendarFolderId(base.Request, base.UserContext);
			if (this.calendarItemBase == null)
			{
				throw new OwaInvalidRequestException("calendarItembase cannot be null");
			}
			this.organizer = (this.calendarItemBase.Organizer ?? new Participant(base.UserContext.MailboxSession.MailboxOwner));
			this.recipientWell = new CalendarItemRecipientWell(base.UserContext, this.calendarItemBase);
			this.selectedDate = this.calendarItemBase.StartTime.Date;
			if (this.calendarItemBase.StartTime < this.calendarItemBase.EndTime)
			{
				this.meetingDuration = (int)(this.calendarItemBase.EndTime - this.calendarItemBase.StartTime).TotalMinutes;
				if (1440 >= this.meetingDuration && this.calendarItemBase.IsAllDayEvent)
				{
					this.meetingDuration = 1440;
				}
			}
			if (1440 <= this.meetingDuration || this.calendarItemBase.IsAllDayEvent)
			{
				this.show24Hours = true;
			}
			if (Utilities.IsPostRequest(base.Request))
			{
				string formParameter = Utilities.GetFormParameter(base.Request, "seldur", false);
				if (!string.IsNullOrEmpty(formParameter))
				{
					if (int.TryParse(formParameter, out this.meetingDuration) && this.meetingDuration <= 0)
					{
						throw new OwaInvalidRequestException("meetingDuration is invalid");
					}
					string formParameter2 = Utilities.GetFormParameter(base.Request, "chkwh", false);
					if (!string.IsNullOrEmpty(formParameter2))
					{
						this.show24Hours = false;
					}
					else
					{
						this.show24Hours = true;
					}
				}
				string formParameter3 = Utilities.GetFormParameter(base.Request, "hiddy", false);
				string formParameter4 = Utilities.GetFormParameter(base.Request, "hidmn", false);
				string formParameter5 = Utilities.GetFormParameter(base.Request, "hidyr", false);
				int day;
				int month;
				int year;
				if (!string.IsNullOrEmpty(formParameter3) && int.TryParse(formParameter3, out day) && !string.IsNullOrEmpty(formParameter4) && int.TryParse(formParameter4, out month) && !string.IsNullOrEmpty(formParameter5) && int.TryParse(formParameter5, out year))
				{
					try
					{
						this.selectedDate = new ExDateTime(base.UserContext.TimeZone, year, month, day);
						goto IL_293;
					}
					catch (ArgumentOutOfRangeException)
					{
						this.selectedDate = this.calendarItemBase.StartTime.Date;
						goto IL_293;
					}
				}
				this.selectedDate = this.calendarItemBase.StartTime.Date;
				IL_293:
				if (Utilities.GetQueryStringParameter(base.Request, "sndpt", false) != null)
				{
					CalendarItemUtilities.BuildSendConfirmDialogPrompt(this.calendarItemBase, out this.sendIssuesPrompt);
				}
			}
			this.GetFreeBusyData();
			if (Utilities.GetQueryStringParameter(base.Request, "cp", false) != null)
			{
				this.isBeingCanceled = true;
				base.Infobar.AddMessage(EditCalendarItemHelper.BuildCancellationPrompt(this.calendarItemBase, base.UserContext));
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000246AC File Offset: 0x000228AC
		private static void GetParticipantData(UserContext userContext, Attendee attendee, out string emailAddress, out MeetingAttendeeType attendeeType)
		{
			emailAddress = string.Empty;
			attendeeType = MeetingAttendeeType.Organizer;
			if (string.CompareOrdinal(attendee.Participant.RoutingType, "EX") == 0)
			{
				IRecipientSession session = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, userContext);
				ADRecipient recipientByLegacyExchangeDN = Utilities.GetRecipientByLegacyExchangeDN(session, attendee.Participant.EmailAddress);
				if (recipientByLegacyExchangeDN != null)
				{
					ADObjectId id = recipientByLegacyExchangeDN.Id;
					emailAddress = recipientByLegacyExchangeDN.PrimarySmtpAddress.ToString();
					if (DirectoryAssistance.IsADRecipientRoom(recipientByLegacyExchangeDN))
					{
						attendeeType = MeetingAttendeeType.Room;
						return;
					}
					if (attendee.AttendeeType == AttendeeType.Resource)
					{
						attendeeType = MeetingAttendeeType.Resource;
						return;
					}
					attendeeType = (MeetingAttendeeType)attendee.AttendeeType;
					return;
				}
			}
			else if (string.CompareOrdinal(attendee.Participant.RoutingType, "SMTP") == 0)
			{
				emailAddress = attendee.Participant.EmailAddress;
				if (attendee.AttendeeType == AttendeeType.Resource)
				{
					attendeeType = MeetingAttendeeType.Resource;
					return;
				}
				attendeeType = (MeetingAttendeeType)attendee.AttendeeType;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00024770 File Offset: 0x00022970
		private static void RenderCurrentDateForSuggestion(TextWriter writer, ExDateTime currentDate)
		{
			writer.Write("<table class=\"dsgt\" cellpadding=\"0\" cellspacing=\"0\">");
			writer.Write("<tr><td class=\"dt\">");
			Utilities.HtmlEncode(currentDate.ToString(DateTimeFormatInfo.CurrentInfo.LongDatePattern), writer);
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"sptr\">");
			writer.Write("<table class=\"w100\" cellpadding=\"0\" cellspacing=\"0\"><tr><td></td></tr></table>");
			writer.Write("</td></tr>");
			writer.Write("</table>");
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000247E1 File Offset: 0x000229E1
		private static void RenderError(TextWriter writer, string error)
		{
			writer.Write("<table class=\"err\" cellpadding=\"0\" cellspacing=\"0\">");
			writer.Write("<tr><td class=\"dt\">");
			writer.Write(error);
			writer.Write("</td></tr>");
			writer.Write("</table>");
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00024816 File Offset: 0x00022A16
		private static int CalculateTotalWorkingHours(Microsoft.Exchange.Clients.Owa.Core.WorkingHours workingHours)
		{
			return (int)Math.Ceiling((double)workingHours.WorkDayEndTimeInWorkingHoursTimeZone / 60.0) - (int)Math.Floor((double)workingHours.WorkDayStartTimeInWorkingHoursTimeZone / 60.0);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00024848 File Offset: 0x00022A48
		private void GetFreeBusyData()
		{
			ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
			bool flag = true;
			ExDateTime minValue = ExDateTime.MinValue;
			ExDateTime minValue2 = ExDateTime.MinValue;
			ExDateTime exDateTime = ExDateTime.MinValue;
			ExDateTime exDateTime2 = ExDateTime.MinValue;
			DatePickerBase.GetVisibleDateRange(this.selectedDate, out minValue, out minValue2, base.UserContext.TimeZone);
			this.UpdateRoomsListFromAutoCompleteCache();
			string text = string.Empty;
			MeetingAttendeeType attendeeType = MeetingAttendeeType.Organizer;
			string value = string.Empty;
			ArrayList arrayList = null;
			for (int i = 0; i < this.calendarItemBase.AttendeeCollection.Count; i++)
			{
				CalendarItemSchedulingTab.GetParticipantData(base.UserContext, this.calendarItemBase.AttendeeCollection[i], out text, out attendeeType);
				if (!string.IsNullOrEmpty(text))
				{
					value = this.calendarItemBase.AttendeeCollection[i].Participant.DisplayName;
					DictionaryEntry dictionaryEntry = new DictionaryEntry(text, value);
					switch (attendeeType)
					{
					case MeetingAttendeeType.Required:
						arrayList = this.requiredAttendeeList;
						break;
					case MeetingAttendeeType.Optional:
						arrayList = this.optionalAttendeeList;
						break;
					case MeetingAttendeeType.Room:
						arrayList = this.roomsList;
						break;
					case MeetingAttendeeType.Resource:
						arrayList = this.resourceAttendeeList;
						break;
					}
					if (arrayList != null && !arrayList.Contains(dictionaryEntry))
					{
						arrayList.Add(dictionaryEntry);
					}
				}
				arrayList = null;
				value = string.Empty;
			}
			int num = this.requiredAttendeeList.Count + this.optionalAttendeeList.Count + this.roomsList.Count + this.resourceAttendeeList.Count + 1;
			if (Configuration.MaximumIdentityArraySize < num)
			{
				this.suggestionsError = string.Format(LocalizedStrings.GetHtmlEncoded(-1838527881), Configuration.MaximumIdentityArraySize);
				flag = false;
			}
			if (this.calendarItemBase is CalendarItemOccurrence || this.calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				this.suggestionsError = LocalizedStrings.GetHtmlEncoded(-438819805);
				flag = false;
			}
			if (this.SelectedYear <= date.Year && this.SelectedMonth < date.Month)
			{
				this.suggestionsError = LocalizedStrings.GetHtmlEncoded(1443871515);
				flag = false;
			}
			else if (this.meetingDuration < 30)
			{
				this.suggestionsError = string.Format(LocalizedStrings.GetHtmlEncoded(-1299787530), 30);
				flag = false;
			}
			else if (1440 < this.meetingDuration)
			{
				this.suggestionsError = LocalizedStrings.GetHtmlEncoded(-2065540826);
				flag = false;
			}
			if (flag)
			{
				if (this.SelectedYear == date.Year && this.SelectedMonth == date.Month)
				{
					exDateTime = date;
				}
				else
				{
					exDateTime = minValue;
				}
				exDateTime2 = minValue2.IncrementDays(1);
				this.availabilityQuery = new AvailabilityQuery();
				this.availabilityQuery.MailboxArray = new MailboxData[num];
				this.availabilityQuery.ClientContext = ClientContext.Create(base.UserContext.LogonIdentity.ClientSecurityContext, base.UserContext.ExchangePrincipal.MailboxInfo.OrganizationId, OwaContext.TryGetCurrentBudget(), base.UserContext.TimeZone, base.UserContext.UserCulture, AvailabilityQuery.CreateNewMessageId());
				bool flag2 = this.show24Hours || CalendarItemSchedulingTab.CalculateTotalWorkingHours(base.UserContext.WorkingHours) * 60 <= this.meetingDuration;
				attendeeType = MeetingAttendeeType.Required;
				foreach (object obj in this.requiredAttendeeList)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj;
					this.AddMailboxDataToAvailabilityQuery(attendeeType, (string)dictionaryEntry2.Key);
				}
				attendeeType = MeetingAttendeeType.Optional;
				foreach (object obj2 in this.optionalAttendeeList)
				{
					DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj2;
					this.AddMailboxDataToAvailabilityQuery(attendeeType, (string)dictionaryEntry3.Key);
				}
				attendeeType = MeetingAttendeeType.Room;
				foreach (object obj3 in this.roomsList)
				{
					DictionaryEntry dictionaryEntry4 = (DictionaryEntry)obj3;
					this.AddMailboxDataToAvailabilityQuery(attendeeType, (string)dictionaryEntry4.Key);
				}
				attendeeType = MeetingAttendeeType.Resource;
				foreach (object obj4 in this.resourceAttendeeList)
				{
					DictionaryEntry dictionaryEntry5 = (DictionaryEntry)obj4;
					this.AddMailboxDataToAvailabilityQuery(attendeeType, (string)dictionaryEntry5.Key);
				}
				text = this.GetOrganizerEmailAddress();
				if (!string.IsNullOrEmpty(text))
				{
					attendeeType = MeetingAttendeeType.Organizer;
					this.AddMailboxDataToAvailabilityQuery(attendeeType, text);
				}
				FreeBusyViewOptions freeBusyViewOptions = new FreeBusyViewOptions();
				freeBusyViewOptions.TimeWindow = new Duration();
				freeBusyViewOptions.TimeWindow.StartTime = (DateTime)minValue;
				freeBusyViewOptions.TimeWindow.EndTime = (DateTime)minValue2.IncrementDays(1);
				freeBusyViewOptions.MergedFreeBusyIntervalInMinutes = 30;
				freeBusyViewOptions.RequestedView = FreeBusyViewType.MergedOnly;
				this.availabilityQuery.DesiredFreeBusyView = freeBusyViewOptions;
				ExTraceGlobals.CalendarTracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "Getting free/busy data from {0} to {1}", freeBusyViewOptions.TimeWindow.StartTime, freeBusyViewOptions.TimeWindow.EndTime);
				SuggestionsViewOptions suggestionsViewOptions = new SuggestionsViewOptions();
				suggestionsViewOptions.DetailedSuggestionsWindow = new Duration();
				suggestionsViewOptions.DetailedSuggestionsWindow.StartTime = (DateTime)exDateTime;
				suggestionsViewOptions.DetailedSuggestionsWindow.EndTime = (DateTime)exDateTime2;
				suggestionsViewOptions.MeetingDurationInMinutes = this.meetingDuration;
				if (this.calendarItemBase.Id != null)
				{
					using (CalendarItemBase item = Utilities.GetItem<CalendarItemBase>(base.UserContext, this.calendarItemBase.Id, new PropertyDefinition[]
					{
						CalendarItemInstanceSchema.StartTime,
						CalendarItemInstanceSchema.EndTime
					}))
					{
						this.savedMeetingStartTime = item.StartTime;
						this.savedMeetingEndTime = item.EndTime;
					}
					suggestionsViewOptions.CurrentMeetingTime = (DateTime)this.savedMeetingStartTime;
				}
				if (flag2)
				{
					if (this.meetingDuration == 1440)
					{
						suggestionsViewOptions.MaximumNonWorkHourResultsByDay = 1;
						suggestionsViewOptions.MaximumResultsByDay = 1;
					}
					else
					{
						suggestionsViewOptions.MaximumNonWorkHourResultsByDay = 2;
					}
				}
				this.availabilityQuery.DesiredSuggestionsView = suggestionsViewOptions;
				ExTraceGlobals.CalendarTracer.TraceDebug<DateTime, DateTime>((long)this.GetHashCode(), "Getting suggestions from {0} to {1}", suggestionsViewOptions.DetailedSuggestionsWindow.StartTime, suggestionsViewOptions.DetailedSuggestionsWindow.EndTime);
				Stopwatch watch = Utilities.StartWatch();
				if (Utilities.ExecuteAvailabilityQuery(base.OwaContext, this.availabilityQuery, out this.availabilityQueryResult) && this.availabilityQueryResult.DailyMeetingSuggestions != null)
				{
					this.dayResults = this.availabilityQueryResult.DailyMeetingSuggestions;
					if (this.dayResults != null && this.calendarItemBase.Id != null && this.savedMeetingStartTime != ExDateTime.MinValue && this.savedMeetingEndTime != ExDateTime.MinValue && this.meetingDuration <= 1440)
					{
						this.RemovePartialOverlapSuggestions(this.dayResults, this.meetingDuration, this.savedMeetingStartTime, this.savedMeetingEndTime);
					}
				}
				Utilities.StopWatch(watch, "CalendarItemSchedulingTab.GetFreeBusyData (Execute Availability Query)");
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00024F98 File Offset: 0x00023198
		private void RemovePartialOverlapSuggestions(SuggestionDayResult[] dayResults, int meetingSuggestionDurationMinutes, ExDateTime savedMeetingStartTime, ExDateTime savedMeetingEndTime)
		{
			if (this.selectedDate < (ExDateTime)dayResults[0].Date.Date)
			{
				return;
			}
			int num = this.FindDayResultForSelectedDate(dayResults);
			SuggestionDayResult suggestionDayResult = dayResults[num];
			if (suggestionDayResult.SuggestionArray != null && suggestionDayResult.SuggestionArray.Length > 0)
			{
				for (int i = 0; i < suggestionDayResult.SuggestionArray.Length; i++)
				{
					Suggestion suggestion = suggestionDayResult.SuggestionArray[i];
					ExDateTime exDateTime = new ExDateTime(base.UserContext.TimeZone, suggestion.MeetingTime);
					ExDateTime exDateTime2 = exDateTime.AddMinutes((double)meetingSuggestionDurationMinutes);
					bool flag = exDateTime < savedMeetingEndTime && exDateTime2 > savedMeetingStartTime;
					bool flag2 = exDateTime == savedMeetingStartTime && exDateTime2 == savedMeetingEndTime;
					if (flag && !flag2)
					{
						suggestionDayResult.SuggestionArray[i] = null;
					}
				}
				suggestionDayResult.SuggestionArray = Array.FindAll<Suggestion>(suggestionDayResult.SuggestionArray, (Suggestion s) => s != null);
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0002509C File Offset: 0x0002329C
		private void AddMailboxDataToAvailabilityQuery(MeetingAttendeeType attendeeType, string emailAddress)
		{
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex] = new MailboxData();
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex].Email = new EmailAddress();
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex].Email.Address = emailAddress;
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex].Email.RoutingType = "SMTP";
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex].AttendeeType = attendeeType;
			this.availabilityQuery.MailboxArray[this.mailboxArrayIndex].ExcludeConflicts = false;
			this.mailboxArrayIndex++;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00025158 File Offset: 0x00023358
		private void UpdateRoomsListFromAutoCompleteCache()
		{
			RecipientCache recipientCache = RoomsCache.TryGetCache(base.OwaContext.UserContext, false);
			if (recipientCache != null)
			{
				foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in recipientCache.CacheEntries)
				{
					if (recipientInfoCacheEntry != null)
					{
						DictionaryEntry dictionaryEntry = new DictionaryEntry(recipientInfoCacheEntry.SmtpAddress, recipientInfoCacheEntry.DisplayName);
						if (!this.roomsList.Contains(dictionaryEntry))
						{
							this.roomsList.Add(dictionaryEntry);
						}
					}
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000251F4 File Offset: 0x000233F4
		private string GetOrganizerEmailAddress()
		{
			string result = string.Empty;
			if (string.CompareOrdinal(this.organizer.RoutingType, "EX") == 0)
			{
				IRecipientSession session = Utilities.CreateADRecipientSession(CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, false, base.UserContext, !base.UserContext.IsHiddenUser);
				ADRecipient recipientByLegacyExchangeDN = Utilities.GetRecipientByLegacyExchangeDN(session, this.organizer.EmailAddress);
				if (recipientByLegacyExchangeDN != null)
				{
					result = recipientByLegacyExchangeDN.PrimarySmtpAddress.ToString();
				}
			}
			else if (string.CompareOrdinal(this.organizer.RoutingType, "SMTP") == 0)
			{
				result = this.organizer.EmailAddress;
			}
			return result;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00025295 File Offset: 0x00023495
		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000252A0 File Offset: 0x000234A0
		protected void RenderSuggestionData(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			UserOptions userOptions = base.UserContext.UserOptions;
			int i = 7;
			ExDateTime exDateTime = this.selectedDate;
			ExDateTime date = DateTimeUtilities.GetLocalTime().Date;
			if (this.dayResults != null)
			{
				int num = 0;
				if (this.selectedDate >= (ExDateTime)this.dayResults[0].Date.Date)
				{
					num = this.FindDayResultForSelectedDate(this.dayResults);
				}
				while (i > 0)
				{
					if (num >= this.dayResults.Length)
					{
						return;
					}
					SuggestionDayResult suggestionDayResult = this.dayResults[num];
					if (!this.show24Hours && !base.UserContext.WorkingHours.IsWorkDay(exDateTime.DayOfWeek))
					{
						i--;
						if (suggestionDayResult != null && exDateTime == (ExDateTime)suggestionDayResult.Date.Date)
						{
							num++;
						}
						exDateTime = exDateTime.IncrementDays(1);
					}
					else
					{
						CalendarItemSchedulingTab.RenderCurrentDateForSuggestion(writer, exDateTime);
						if (suggestionDayResult == null)
						{
							if (exDateTime < date)
							{
								CalendarItemSchedulingTab.RenderError(writer, LocalizedStrings.GetHtmlEncoded(1443871515));
							}
							else
							{
								CalendarItemSchedulingTab.RenderError(writer, LocalizedStrings.GetHtmlEncoded(376047783));
							}
						}
						else if (exDateTime < date)
						{
							CalendarItemSchedulingTab.RenderError(writer, LocalizedStrings.GetHtmlEncoded(1443871515));
						}
						else
						{
							this.meetingSuggestions = suggestionDayResult.SuggestionArray;
							if (this.meetingSuggestions.Length == 0)
							{
								if (!this.show24Hours && !base.UserContext.WorkingHours.IsWorkDay(suggestionDayResult.Date.DayOfWeek))
								{
									CalendarItemSchedulingTab.RenderError(writer, LocalizedStrings.GetHtmlEncoded(1274187420));
								}
								else
								{
									CalendarItemSchedulingTab.RenderError(writer, LocalizedStrings.GetHtmlEncoded(376047783));
								}
							}
							else
							{
								writer.Write("<table class=\"sgs\" cellpadding=\"0\" cellspacing=\"0\">");
								for (int j = 0; j < this.meetingSuggestions.Length; j++)
								{
									this.RenderSuggestion(writer, this.meetingSuggestions[j]);
								}
								writer.Write("</table>");
							}
						}
						i--;
						if (suggestionDayResult != null && exDateTime == (ExDateTime)suggestionDayResult.Date.Date)
						{
							num++;
						}
						exDateTime = exDateTime.IncrementDays(1);
					}
				}
			}
			else
			{
				CalendarItemSchedulingTab.RenderError(writer, this.suggestionsError);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000254DC File Offset: 0x000236DC
		private int FindDayResultForSelectedDate(SuggestionDayResult[] dayResults)
		{
			for (int i = 0; i < dayResults.Length; i++)
			{
				if ((ExDateTime)dayResults[i].Date.Date == this.selectedDate)
				{
					return i;
				}
			}
			throw new ArgumentException("AvailabilityQuery suggestions set doesn't include selected day.");
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00025528 File Offset: 0x00023728
		private void RenderSuggestion(TextWriter writer, Suggestion suggestion)
		{
			if (suggestion != null)
			{
				ExDateTime exDateTime = new ExDateTime(base.UserContext.TimeZone, suggestion.MeetingTime);
				ExDateTime d = exDateTime.AddMinutes((double)this.meetingDuration);
				if (!this.show24Hours && !this.IsInWorkingHours(exDateTime, this.meetingDuration))
				{
					return;
				}
				List<DictionaryEntry> list = new List<DictionaryEntry>();
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				StringBuilder stringBuilder4 = new StringBuilder();
				int i = 0;
				while (i < this.availabilityQuery.MailboxArray.Length)
				{
					AttendeeConflictData attendeeConflictData = suggestion.AttendeeConflictDataArray[i];
					MeetingAttendeeType attendeeType = this.availabilityQuery.MailboxArray[i].AttendeeType;
					StringBuilder stringBuilder5 = null;
					string text = string.Empty;
					IndividualAttendeeConflictData individualAttendeeConflictData = attendeeConflictData as IndividualAttendeeConflictData;
					if (individualAttendeeConflictData != null)
					{
						if (individualAttendeeConflictData.Attendee.AttendeeType == MeetingAttendeeType.Room)
						{
							if (!individualAttendeeConflictData.AttendeeHasConflict)
							{
								this.AddToAvailableRoomList(list, this.availabilityQuery.MailboxArray[i].Email.Address);
							}
						}
						else if (individualAttendeeConflictData.AttendeeHasConflict)
						{
							if (attendeeType != MeetingAttendeeType.Organizer)
							{
								text = this.GetDisplayName(attendeeType, this.availabilityQuery.MailboxArray[i].Email.Address);
								goto IL_286;
							}
							text = this.organizer.DisplayName;
							goto IL_286;
						}
						else if (individualAttendeeConflictData.IsMissingFreeBusyData)
						{
							stringBuilder5 = stringBuilder4;
							if (attendeeType != MeetingAttendeeType.Organizer)
							{
								text = this.GetDisplayName(attendeeType, this.availabilityQuery.MailboxArray[i].Email.Address);
								goto IL_286;
							}
							text = this.organizer.DisplayName;
							goto IL_286;
						}
					}
					else if (attendeeConflictData is UnknownAttendeeConflictData || attendeeConflictData is TooBigGroupAttendeeConflictData)
					{
						stringBuilder5 = stringBuilder4;
						if (attendeeType != MeetingAttendeeType.Organizer)
						{
							text = this.GetDisplayName(attendeeType, this.availabilityQuery.MailboxArray[i].Email.Address);
							goto IL_286;
						}
						text = this.organizer.DisplayName;
						goto IL_286;
					}
					else if (attendeeConflictData is GroupAttendeeConflictData)
					{
						GroupAttendeeConflictData groupAttendeeConflictData = (GroupAttendeeConflictData)attendeeConflictData;
						text = this.GetDisplayName(attendeeType, this.availabilityQuery.MailboxArray[i].Email.Address);
						if (0 < groupAttendeeConflictData.NumberOfMembersWithNoData)
						{
							if (0 < stringBuilder4.Length)
							{
								stringBuilder4.Append(", ");
							}
							stringBuilder4.Append(string.Format(LocalizedStrings.GetHtmlEncoded(-1170293090), Utilities.HtmlEncode(text), groupAttendeeConflictData.NumberOfMembersWithNoData));
						}
						if (groupAttendeeConflictData.NumberOfMembersWithConflict != 0)
						{
							text = string.Format(LocalizedStrings.GetNonEncoded(-1505134519), text, groupAttendeeConflictData.NumberOfMembersWithConflict, groupAttendeeConflictData.NumberOfMembers);
							goto IL_286;
						}
					}
					IL_2E9:
					i++;
					continue;
					IL_286:
					if (stringBuilder5 == null)
					{
						switch (attendeeType)
						{
						case MeetingAttendeeType.Organizer:
						case MeetingAttendeeType.Required:
							stringBuilder5 = stringBuilder;
							break;
						case MeetingAttendeeType.Optional:
							stringBuilder5 = stringBuilder2;
							break;
						case MeetingAttendeeType.Room:
						case MeetingAttendeeType.Resource:
							stringBuilder5 = stringBuilder3;
							break;
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						if (0 < stringBuilder5.Length)
						{
							stringBuilder5.Append("; ");
						}
						stringBuilder5.Append(Utilities.HtmlEncode(text));
						goto IL_2E9;
					}
					goto IL_2E9;
				}
				string text2 = Utilities.HtmlEncode(string.Format(LocalizedStrings.GetNonEncoded(466973109), exDateTime.ToString(base.UserContext.UserOptions.TimeFormat), d.ToString(base.UserContext.UserOptions.TimeFormat)));
				string arg = string.Format("<h1><a href=\"#\" onclick=\"return onClkSC({0},{1},{2},{3},{4},{5});\">{6}</a></h1>", new object[]
				{
					exDateTime.Hour,
					exDateTime.Minute,
					exDateTime.Day,
					exDateTime.Month,
					exDateTime.Year,
					(list.Count > 0) ? ("'selRms" + this.currentRoomIndex + "'") : "null",
					text2
				});
				writer.Write("<tr><td rowspan=\"3\" class=\"{0}\" nowrap><img src=\"{1}\" alt=\"\"></td>", this.SuggestionQualityStyles[(int)suggestion.SuggestionQuality], base.UserContext.GetThemeFileUrl(ThemeFileId.Clear));
				writer.Write("<td rowspan=\"3\" class=\"tm\" nowrap>{0}</td>", arg);
				int num = suggestion.RequiredAttendeeCount - suggestion.RequiredAttendeeConflictCount;
				int num2 = suggestion.OptionalAttendeeCount - suggestion.OptionalAttendeeConflictCount;
				if (this.calendarItemBase.Id == null || !(exDateTime == this.savedMeetingStartTime) || !(d == this.savedMeetingEndTime))
				{
					writer.Write("<td class=\"tdrq\" nowrap>");
					writer.Write("<img src=\"");
					base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.RequiredAttendee);
					writer.Write("\" ");
					Utilities.RenderImageAltAttribute(writer, base.UserContext, ThemeFileId.RequiredAttendee);
					writer.Write(">");
					Utilities.HtmlEncode(string.Format(LocalizedStrings.GetNonEncoded(1026164821), num, suggestion.RequiredAttendeeCount), writer);
					writer.Write("</td>");
					writer.Write("<td rowspan=\"3\" class=\"cnf\" nowrap>");
					if (0 < suggestion.RequiredAttendeeConflictCount || 0 < suggestion.OptionalAttendeeConflictCount || 0 < suggestion.ResourceAttendeeConflictCount)
					{
						writer.Write(LocalizedStrings.GetHtmlEncoded(-1322491676));
					}
					writer.Write("</td>");
					writer.Write("<td rowspan=\"3\" class=\"cfdt\" nowrap>");
					if (0 < suggestion.RequiredAttendeeConflictCount || 0 < suggestion.OptionalAttendeeConflictCount || 0 < suggestion.ResourceAttendeeConflictCount)
					{
						if (0 < stringBuilder.Length && (0 < stringBuilder2.Length || 0 < stringBuilder3.Length))
						{
							stringBuilder.Append("; ");
						}
						if (0 < stringBuilder2.Length && 0 < stringBuilder3.Length)
						{
							stringBuilder2.Append("; ");
						}
						writer.Write("<label class=\"rq\">{0}</label><label class=\"op\">{1}{2}</label>", stringBuilder.ToString(), stringBuilder2.ToString(), stringBuilder3.ToString());
					}
					writer.Write("</td>");
					writer.Write("</tr>");
					writer.Write("<tr>");
					writer.Write("<td class=\"tdop\" nowrap>");
					if (suggestion.OptionalAttendeeCount > 0)
					{
						writer.Write("<img src=\"");
						base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.OptionalAttendee);
						writer.Write("\" ");
						Utilities.RenderImageAltAttribute(writer, base.UserContext, ThemeFileId.OptionalAttendee);
						writer.Write(">");
						Utilities.HtmlEncode(string.Format(LocalizedStrings.GetNonEncoded(1026164821), num2, suggestion.OptionalAttendeeCount), writer);
					}
					writer.Write("</td>");
				}
				else
				{
					writer.Write("<td class=\"tdrq\" nowrap>");
					writer.Write(LocalizedStrings.GetHtmlEncoded(-1007655007));
					writer.Write("</td><td rowspan=\"3\" class=\"cnf\"></td>");
					writer.Write("<td rowspan=\"3\" class=\"cfdt\"></td></tr><tr>");
				}
				writer.Write("</tr>");
				writer.Write("<tr>");
				writer.Write("<td class=\"tdrm\" nowrap>");
				writer.Write("<img src=\"");
				base.UserContext.RenderThemeFileUrl(writer, ThemeFileId.ResourceAttendee);
				writer.Write("\" ");
				Utilities.RenderImageAltAttribute(writer, base.UserContext, ThemeFileId.ResourceAttendee);
				writer.Write(">");
				if (list.Count > 0)
				{
					writer.Write("<select name=\"{0}\" id=\"{0}\">", "selRms" + this.currentRoomIndex);
					list.Sort(new CalendarItemSchedulingTab.RoomComparer());
					foreach (DictionaryEntry dictionaryEntry in list)
					{
						writer.Write("<option value=\"{0}\"{2}>{1}</option>", (string)dictionaryEntry.Key, (string)dictionaryEntry.Value, string.Empty);
					}
					writer.Write("</select>");
					this.currentRoomIndex++;
				}
				else
				{
					writer.Write(LocalizedStrings.GetHtmlEncoded(1651757833));
				}
				writer.Write("</td>");
				writer.Write("</tr>");
				writer.Write("<tr><td colspan=\"5\" class=\"sptr\">");
				writer.Write("<table class=\"w100\" cellpadding=\"0\" cellspacing=\"0\">");
				writer.Write("<tr><td></td></tr></table>");
				writer.Write("</td></tr>");
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00025D34 File Offset: 0x00023F34
		private void AddToAvailableRoomList(List<DictionaryEntry> availableRooms, string emailAddress)
		{
			foreach (object obj in this.roomsList)
			{
				DictionaryEntry item = (DictionaryEntry)obj;
				if ((string)item.Key == emailAddress)
				{
					availableRooms.Add(item);
					break;
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00025DA4 File Offset: 0x00023FA4
		private string GetDisplayName(MeetingAttendeeType attendeeType, string emailAddress)
		{
			string result = string.Empty;
			ArrayList arrayList;
			switch (attendeeType)
			{
			case MeetingAttendeeType.Required:
				arrayList = this.requiredAttendeeList;
				break;
			case MeetingAttendeeType.Optional:
				arrayList = this.optionalAttendeeList;
				break;
			case MeetingAttendeeType.Room:
			case MeetingAttendeeType.Resource:
				arrayList = this.resourceAttendeeList;
				break;
			default:
				throw new ArgumentException(string.Format("The specified AttendeeType {0} is not handled.", attendeeType));
			}
			foreach (object obj in arrayList)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if ((string)dictionaryEntry.Key == emailAddress)
				{
					result = (string)dictionaryEntry.Value;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00025E74 File Offset: 0x00024074
		protected void RenderSelectedDate(TextWriter writer)
		{
			Utilities.HtmlEncode(this.selectedDate.ToString(DateTimeFormatInfo.CurrentInfo.LongDatePattern), writer);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00025E94 File Offset: 0x00024094
		protected void RenderEndDate(TextWriter writer)
		{
			ExDateTime minValue = ExDateTime.MinValue;
			Utilities.HtmlEncode(this.selectedDate.IncrementDays(6).ToString(DateTimeFormatInfo.CurrentInfo.LongDatePattern), writer);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00025ECC File Offset: 0x000240CC
		protected override void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.PeoplePicker, OptionsBar.RenderingFlags.RenderSearchLocationOnly, null);
			optionsBar.Render(helpFile);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00025EFC File Offset: 0x000240FC
		public void RenderNavigation()
		{
			Navigation navigation = new Navigation(this.navigationModule, base.OwaContext, base.Response.Output);
			navigation.Render();
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00025F2C File Offset: 0x0002412C
		public void RenderSecondaryNavigation(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("<div><table cellspacing=0 cellpadding=0 class=\"csnv\"><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1286941817));
			writer.Write("</caption><tr><td class=\"dt g\">");
			writer.Write(LocalizedStrings.GetHtmlEncoded(491536354));
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"dvdr\"><table cellspacing=0 cellpadding=0 class=\"w100\"><tr><td></td></tr></table></td></tr>");
			writer.Write("<tr><td class=\"dt\">");
			DatePicker datePicker = new DatePicker(base.OwaContext, this.CurrentFolderStoreObjectId, this.selectedDate);
			datePicker.Render(writer, this.dayResults, !this.show24Hours);
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"dvdr\"><table cellspacing=0 cellpadding=0 class=\"w100\"><tr><td></td></tr></table></td></tr>");
			writer.Write("<tr><td class=\"dt\">{0}<br>", LocalizedStrings.GetHtmlEncoded(1824876727));
			this.RenderDurationDropDown(writer);
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"dt g\">");
			writer.Write("<input name=\"{0}\" id=\"{0}\" type=\"checkbox\" value=\"1\"{2}><label for=\"{0}\">{1}</label>", "chkwh", LocalizedStrings.GetHtmlEncoded(907076665), this.show24Hours ? string.Empty : " checked");
			writer.Write("</td></tr>");
			writer.Write("<tr><td class=\"dt rt\">");
			writer.Write("<input class=\"btn\" type=\"submit\" name=\"btnupd\" id=\"btnupd\" alt=\"{0}\" value=\"{0}\">", LocalizedStrings.GetHtmlEncoded(859380341));
			writer.Write("</td></tr>");
			writer.Write("<tr><td height=\"100%\">&nbsp;</td></tr>");
			writer.Write("</table></div>");
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00026090 File Offset: 0x00024290
		private void RenderDurationDropDown(TextWriter writer)
		{
			writer.Write("<select name=\"{0}\" id=\"{0}\">", "seldur");
			int num = 60;
			int num2 = num * 24;
			int num3 = 30;
			int num4 = 0;
			if (this.meetingDuration > num4 && this.meetingDuration < num4 + num3)
			{
				writer.Write("<option value=\"{0}\"{2}>{1}</option>", this.meetingDuration, string.Format(LocalizedStrings.GetHtmlEncoded(1538724068), this.meetingDuration), " selected");
			}
			num4 += num3;
			writer.Write("<option value=\"{0}\"{2}>{1}</option>", num4, string.Format(LocalizedStrings.GetHtmlEncoded(1538724068), num4), (num4 == this.meetingDuration) ? " selected" : string.Empty);
			if (this.meetingDuration > num4 && this.meetingDuration < num4 + num3)
			{
				writer.Write("<option value=\"{0}\"{2}>{1}</option>", this.meetingDuration, string.Format(LocalizedStrings.GetHtmlEncoded(1538724068), this.meetingDuration), " selected");
			}
			num4 += num3;
			writer.Write("<option value=\"{0}\"{2}>{1}</option>", num4, string.Format(LocalizedStrings.GetHtmlEncoded(655780243), num4 / num), (num4 == this.meetingDuration) ? " selected" : string.Empty);
			if (this.meetingDuration > num4 && this.meetingDuration < num4 + num3)
			{
				writer.Write("<option value=\"{0}\"{2}>{1}</option>", this.meetingDuration, string.Format(LocalizedStrings.GetHtmlEncoded(1538724068), this.meetingDuration), " selected");
			}
			for (int i = 3; i < 48; i++)
			{
				num4 = i * num3;
				if (num4 % num == 0)
				{
					writer.Write("<option value=\"{0}\"{2}>{1}</option>", num4, string.Format(LocalizedStrings.GetHtmlEncoded(-692228354), num4 / num), (num4 == this.meetingDuration) ? " selected" : string.Empty);
				}
				else
				{
					writer.Write("<option value=\"{0}\"{2}>{1}</option>", num4, string.Format(LocalizedStrings.GetHtmlEncoded(-692228354), (double)num4 * 1.0 / (double)num), (num4 == this.meetingDuration) ? " selected" : string.Empty);
				}
				if (this.meetingDuration > num4 && this.meetingDuration < num4 + num3)
				{
					writer.Write("<option value=\"{0}\"{2}>{1}</option>", this.meetingDuration, string.Format(LocalizedStrings.GetHtmlEncoded(1538724068), this.meetingDuration), " selected");
				}
			}
			num4 += num3;
			writer.Write("<option value=\"{0}\"{2}>{1}</option>", num4, string.Format(LocalizedStrings.GetHtmlEncoded(-1258768989), num4 / num2), (num4 == this.meetingDuration) ? " selected" : string.Empty);
			writer.Write("</select>");
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00026354 File Offset: 0x00024554
		public void RenderHeaderToolbar()
		{
			EditCalendarItemToolbar editCalendarItemToolbar = new EditCalendarItemToolbar(this.IsUnsaved, this.IsMeeting, this.calendarItemBase.MeetingRequestWasSent, this.calendarItemBase.Importance, this.calendarItemBase.CalendarItemType, base.Response.Output, true, this.isBeingCanceled);
			editCalendarItemToolbar.RenderStart();
			editCalendarItemToolbar.RenderButtons();
			editCalendarItemToolbar.RenderButton(ToolbarButtons.CloseImage);
			editCalendarItemToolbar.RenderEnd();
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000263C4 File Offset: 0x000245C4
		public static void RenderFooterToolbar(TextWriter writer)
		{
			Toolbar toolbar = new Toolbar(writer, false);
			toolbar.RenderStart();
			toolbar.RenderFill();
			toolbar.RenderEnd();
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000263EB File Offset: 0x000245EB
		public string SendIssuesPrompt
		{
			get
			{
				if (this.sendIssuesPrompt != null)
				{
					return this.sendIssuesPrompt;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00026404 File Offset: 0x00024604
		private bool IsInWorkingHours(ExDateTime startTime, int duration)
		{
			int workDayStartTime = base.UserContext.WorkingHours.GetWorkDayStartTime(startTime);
			int num = base.UserContext.WorkingHours.GetWorkDayEndTime(startTime);
			if (num < workDayStartTime)
			{
				num += 1440;
			}
			ExDateTime end = startTime.AddMinutes((double)duration);
			if (num - workDayStartTime >= 1440)
			{
				return true;
			}
			if (duration >= 1440)
			{
				return false;
			}
			ExDateTime start = new ExDateTime(base.UserContext.TimeZone, startTime.Year, startTime.Month, startTime.Day).AddMinutes((double)workDayStartTime);
			ExDateTime end2 = new ExDateTime(base.UserContext.TimeZone, startTime.Year, startTime.Month, startTime.Day).AddMinutes((double)num);
			DateRange dateRange = new DateRange(start.IncrementDays(-1), end2.IncrementDays(-1));
			DateRange dateRange2 = new DateRange(start, end2);
			DateRange dateRange3 = new DateRange(start.IncrementDays(1), end2.IncrementDays(1));
			return dateRange2.Includes(startTime, end) || dateRange.Includes(startTime, end) || dateRange3.Includes(startTime, end);
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0002651D File Offset: 0x0002471D
		protected static int CalendarTabScheduling
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00026520 File Offset: 0x00024720
		protected bool IsBeingCanceled
		{
			get
			{
				return this.isBeingCanceled;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00026528 File Offset: 0x00024728
		protected string Id
		{
			get
			{
				if (this.calendarItemBase != null && this.calendarItemBase.Id != null)
				{
					return this.calendarItemBase.Id.ObjectId.ToBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0002655A File Offset: 0x0002475A
		protected bool IsMeeting
		{
			get
			{
				return this.calendarItemBase != null && this.calendarItemBase.IsMeeting;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00026571 File Offset: 0x00024771
		protected string ChangeKey
		{
			get
			{
				if (this.calendarItemBase != null && this.calendarItemBase.Id != null)
				{
					return this.calendarItemBase.Id.ChangeKeyAsBase64String();
				}
				return string.Empty;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0002659E File Offset: 0x0002479E
		protected NavigationModule NavigationModule
		{
			get
			{
				return this.navigationModule;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000265A6 File Offset: 0x000247A6
		protected int SelectedYear
		{
			get
			{
				return this.selectedDate.Year;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x000265B3 File Offset: 0x000247B3
		protected int SelectedMonth
		{
			get
			{
				return this.selectedDate.Month;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x000265C0 File Offset: 0x000247C0
		protected int SelectedDay
		{
			get
			{
				return this.selectedDate.Day;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x000265CD File Offset: 0x000247CD
		protected string CurrentFolderId
		{
			get
			{
				return this.CurrentFolderStoreObjectId.ToBase64String();
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000265DC File Offset: 0x000247DC
		protected string SuggestionTable
		{
			get
			{
				string themeFileUrl = base.UserContext.GetThemeFileUrl(ThemeFileId.Clear);
				return string.Concat(new string[]
				{
					"<table class=keyLyt><tr><td><img class=grt src=\"",
					themeFileUrl,
					"\" alt=\"",
					LocalizedStrings.GetHtmlEncoded(-1744013695),
					"\"></td><td>",
					LocalizedStrings.GetHtmlEncoded(-1744013695),
					"</td><td>&nbsp;</td><td><img class=gd src=\"",
					themeFileUrl,
					"\" alt=\"",
					LocalizedStrings.GetHtmlEncoded(361446959),
					"\"></td><td>",
					LocalizedStrings.GetHtmlEncoded(361446959),
					"</td><td>&nbsp;</td><td><img class=fr src=\"",
					themeFileUrl,
					"\" alt=\"",
					LocalizedStrings.GetHtmlEncoded(-126530650),
					"\"></td><td>",
					LocalizedStrings.GetHtmlEncoded(-126530650),
					"</td></tr></table>"
				});
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000266B9 File Offset: 0x000248B9
		protected bool IsUnsaved
		{
			get
			{
				return this.calendarItemBase != null && this.calendarItemBase.Id == null;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000266D3 File Offset: 0x000248D3
		protected bool IsDirty
		{
			get
			{
				return this.calendarItemBase.IsDirty && !this.isNew;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000266ED File Offset: 0x000248ED
		protected bool IsSendUpdateRequired
		{
			get
			{
				return this.IsMeeting && this.MeetingRequestWasSent && EditCalendarItemHelper.IsSendUpdateRequired(this.calendarItemBase, base.UserContext);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00026712 File Offset: 0x00024912
		protected bool HasRecipients
		{
			get
			{
				return this.recipientWell.HasRecipients(RecipientWellType.To) || this.recipientWell.HasRecipients(RecipientWellType.Cc) || this.recipientWell.HasRecipients(RecipientWellType.Bcc);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00026740 File Offset: 0x00024940
		protected bool HasUnresolvedRecipients
		{
			get
			{
				if (this.calendarItemBase.AttendeeCollection != null && this.calendarItemBase.AttendeeCollection.Count > 0)
				{
					foreach (Attendee attendee in this.calendarItemBase.AttendeeCollection)
					{
						if (!Utilities.IsMapiPDL(attendee.Participant.RoutingType) && (string.IsNullOrEmpty(attendee.Participant.EmailAddress) || string.IsNullOrEmpty(attendee.Participant.RoutingType)) && !string.IsNullOrEmpty(attendee.Participant.DisplayName))
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00026800 File Offset: 0x00024A00
		protected bool MeetingRequestWasSent
		{
			get
			{
				return base.Item != null && this.IsMeeting && this.calendarItemBase.MeetingRequestWasSent;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00026821 File Offset: 0x00024A21
		protected int CalendarItemBaseImportance
		{
			get
			{
				if (this.calendarItemBase == null)
				{
					return 1;
				}
				return (int)this.calendarItemBase.Importance;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00026838 File Offset: 0x00024A38
		public bool IsAllDayEvent
		{
			get
			{
				return this.calendarItemBase != null && this.calendarItemBase.IsAllDayEvent;
			}
		}

		// Token: 0x04000389 RID: 905
		private const int FreeBusyInterval = 30;

		// Token: 0x0400038A RID: 906
		private const int TotalMinutesInADay = 1440;

		// Token: 0x0400038B RID: 907
		private const int TotalDaysForSuggestion = 7;

		// Token: 0x0400038C RID: 908
		private const string YearFormParameter = "hidyr";

		// Token: 0x0400038D RID: 909
		private const string MonthFormParameter = "hidmn";

		// Token: 0x0400038E RID: 910
		private const string DayFormParameter = "hiddy";

		// Token: 0x0400038F RID: 911
		public const string DurationFormParameter = "seldur";

		// Token: 0x04000390 RID: 912
		private const string OptionText = "<option value=\"{0}\"{2}>{1}</option>";

		// Token: 0x04000391 RID: 913
		private const string SelectText = "<select name=\"{0}\" id=\"{0}\">";

		// Token: 0x04000392 RID: 914
		private const string WorkinghoursFormParameter = "chkwh";

		// Token: 0x04000393 RID: 915
		private readonly string[] SuggestionQualityStyles = new string[]
		{
			"grt",
			"gd",
			"fr",
			"por"
		};

		// Token: 0x04000394 RID: 916
		private bool isNew;

		// Token: 0x04000395 RID: 917
		private CalendarItemRecipientWell recipientWell;

		// Token: 0x04000396 RID: 918
		private NavigationModule navigationModule = NavigationModule.Calendar;

		// Token: 0x04000397 RID: 919
		private bool isBeingCanceled;

		// Token: 0x04000398 RID: 920
		private ExDateTime selectedDate = ExDateTime.MinValue;

		// Token: 0x04000399 RID: 921
		private bool show24Hours;

		// Token: 0x0400039A RID: 922
		private int meetingDuration = 30;

		// Token: 0x0400039B RID: 923
		private AvailabilityQuery availabilityQuery;

		// Token: 0x0400039C RID: 924
		private string suggestionsError = string.Empty;

		// Token: 0x0400039D RID: 925
		private AvailabilityQueryResult availabilityQueryResult;

		// Token: 0x0400039E RID: 926
		private SuggestionDayResult[] dayResults;

		// Token: 0x0400039F RID: 927
		private Suggestion[] meetingSuggestions;

		// Token: 0x040003A0 RID: 928
		private ArrayList roomsList = new ArrayList();

		// Token: 0x040003A1 RID: 929
		private ArrayList requiredAttendeeList = new ArrayList();

		// Token: 0x040003A2 RID: 930
		private ArrayList optionalAttendeeList = new ArrayList();

		// Token: 0x040003A3 RID: 931
		private ArrayList resourceAttendeeList = new ArrayList();

		// Token: 0x040003A4 RID: 932
		private string sendIssuesPrompt;

		// Token: 0x040003A5 RID: 933
		internal StoreObjectId CurrentFolderStoreObjectId;

		// Token: 0x040003A6 RID: 934
		private CalendarItemBase calendarItemBase;

		// Token: 0x040003A7 RID: 935
		private int currentRoomIndex;

		// Token: 0x040003A8 RID: 936
		private ExDateTime savedMeetingStartTime = ExDateTime.MinValue;

		// Token: 0x040003A9 RID: 937
		private ExDateTime savedMeetingEndTime = ExDateTime.MinValue;

		// Token: 0x040003AA RID: 938
		private int mailboxArrayIndex;

		// Token: 0x040003AB RID: 939
		private Participant organizer;

		// Token: 0x02000093 RID: 147
		private class RoomComparer : IComparer<DictionaryEntry>
		{
			// Token: 0x06000478 RID: 1144 RVA: 0x000268F8 File Offset: 0x00024AF8
			public int Compare(DictionaryEntry deX, DictionaryEntry deY)
			{
				return ((string)deX.Value).CompareTo((string)deY.Value);
			}
		}
	}
}
