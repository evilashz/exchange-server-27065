using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200004A RID: 74
	internal class CalendarManager : SendMessageManager
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000D814 File Offset: 0x0000BA14
		internal CalendarManager(ActivityManager manager, CalendarManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000D81E File Offset: 0x0000BA1E
		internal CalendarNavigator Navigator
		{
			get
			{
				if (this.nav == null)
				{
					this.nav = new CalendarNavigator(this.user);
				}
				return this.nav;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000D83F File Offset: 0x0000BA3F
		internal override bool LargeGrammarsNeeded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000D842 File Offset: 0x0000BA42
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000D84A File Offset: 0x0000BA4A
		internal CalendarNavigator.AgendaContext Context
		{
			get
			{
				return this.agendaCtx;
			}
			set
			{
				this.agendaCtx = value;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000D853 File Offset: 0x0000BA53
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			vo.IncrementCounter(SubscriberAccessCounters.CalendarAccessed);
			base.Start(vo, refInfo);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000D868 File Offset: 0x0000BA68
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Calendar Manager asked to do action: {0}.", new object[]
			{
				action
			});
			this.user = vo.CurrentCallContext.CallerInfo;
			string input = null;
			if (string.Equals(action, "getTodaysMeetings", StringComparison.OrdinalIgnoreCase))
			{
				this.targetDate = this.user.Now.Date;
				input = this.OpenCalendarDate(vo);
			}
			else if (string.Equals(action, "nextMeeting", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextMeeting(vo);
			}
			else if (string.Equals(action, "nextDay", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextDay(vo);
			}
			else if (string.Equals(action, "previousMeeting", StringComparison.OrdinalIgnoreCase))
			{
				input = this.PreviousMeeting(vo);
			}
			else if (string.Equals(action, "getDetails", StringComparison.OrdinalIgnoreCase))
			{
				input = this.GetDetails();
			}
			else if (string.Equals(action, "getParticipants", StringComparison.OrdinalIgnoreCase))
			{
				input = this.GetParticipants();
			}
			else if (string.Equals(action, "lateForMeeting", StringComparison.OrdinalIgnoreCase))
			{
				input = this.LateForMeeting();
			}
			else if (string.Equals(action, "cancelOrDecline", StringComparison.OrdinalIgnoreCase))
			{
				CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				if (!meetingInfo.IsOrganizer)
				{
					vo.IncrementCounter(SubscriberAccessCounters.MeetingsDeclined);
				}
				base.SendMsg = new CalendarManager.CancelOrDecline(vo, this.user, meetingInfo, this);
				base.WriteReplyIntroType(IntroType.Cancel);
			}
			else if (string.Equals(action, "cancelSeveral", StringComparison.OrdinalIgnoreCase))
			{
				CalendarNavigator.MeetingInfo mi = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				ExDateTime startTimeToClearFrom = this.GetStartTimeToClearFrom(mi);
				base.SendMsg = new CalendarManager.CancelSeveralMeetings(vo, this.user, startTimeToClearFrom, this, true);
				base.WriteReplyIntroType(IntroType.ClearCalendar);
			}
			else if (string.Equals(action, "replyToOrganizer", StringComparison.OrdinalIgnoreCase))
			{
				CalendarNavigator.MeetingInfo mi2 = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				base.SendMsg = new CalendarManager.ReplyToOrganizer(vo, this.user, this, mi2);
				base.WriteReplyIntroType(IntroType.Reply);
			}
			else if (string.Equals(action, "replyToAll", StringComparison.OrdinalIgnoreCase))
			{
				CalendarNavigator.MeetingInfo mi3 = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				base.SendMsg = new CalendarManager.ReplyToAll(vo, this.user, this, mi3);
				base.WriteReplyIntroType(IntroType.ReplyAll);
			}
			else if (string.Equals(action, "forward", StringComparison.OrdinalIgnoreCase))
			{
				CalendarNavigator.MeetingInfo mi4 = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				base.SendMsg = new CalendarManager.CalendarForward(vo, this.user, this, mi4);
				base.WriteReplyIntroType(IntroType.Forward);
			}
			else if (string.Equals(action, "giveShortcutHint", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("giveShortcutHint", true);
			}
			else if (string.Equals(action, "parseDateSpeech", StringComparison.OrdinalIgnoreCase))
			{
				this.ParseDateSpeech();
			}
			else if (string.Equals(action, "openCalendarDate", StringComparison.OrdinalIgnoreCase))
			{
				input = this.OpenCalendarDate(vo);
			}
			else if (string.Equals(action, "nextMeetingSameDay", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextMeetingSameDay(vo);
			}
			else if (string.Equals(action, "previousMeetingSameDay", StringComparison.OrdinalIgnoreCase))
			{
				input = this.PreviousMeetingSameDay(vo);
			}
			else if (string.Equals(action, "firstMeetingSameDay", StringComparison.OrdinalIgnoreCase))
			{
				input = this.FirstMeetingSameDay(vo);
			}
			else if (string.Equals(action, "lastMeetingSameDay", StringComparison.OrdinalIgnoreCase))
			{
				input = this.LastMeetingSameDay(vo);
			}
			else if (string.Equals(action, "acceptMeeting", StringComparison.OrdinalIgnoreCase))
			{
				this.AcceptMeeting();
			}
			else if (string.Equals(action, "markAsTentative", StringComparison.OrdinalIgnoreCase))
			{
				this.MarkAsTentative();
			}
			else if (string.Equals(action, "seekValidMeeting", StringComparison.OrdinalIgnoreCase))
			{
				input = this.SeekValidMeeting(vo);
			}
			else if (string.Equals(action, "isValidMeeting", StringComparison.OrdinalIgnoreCase))
			{
				if (!this.Context.IsValid)
				{
					input = "noMeetings";
				}
				this.WriteMeetingVariables(vo);
			}
			else if (string.Equals(action, "skipHeader", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("skipHeader", true);
			}
			else if (string.Equals(action, "more", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("skipHeader", true);
				base.ExecuteAction(action, vo);
			}
			else if (string.Equals(action, "readTheHeader", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("skipHeader", false);
				base.WriteVariable("repeat", true);
				base.WriteVariable("more", false);
			}
			else if (string.Equals(action, "clearMinutesLate", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("minutesLateMax", 0);
				base.WriteVariable("minutesLateMin", 0);
			}
			else if (string.Equals(action, "parseLateMinutes", StringComparison.OrdinalIgnoreCase))
			{
				this.ParseLateMinutes();
			}
			else if (string.Equals(action, "parseClearTimeDays", StringComparison.OrdinalIgnoreCase))
			{
				input = this.ParseClearTimeDays();
			}
			else if (string.Equals(action, "parseClearHours", StringComparison.OrdinalIgnoreCase))
			{
				this.ParseClearHours();
			}
			else if (string.Equals(action, "giveLateMinutesHint", StringComparison.OrdinalIgnoreCase))
			{
				base.WriteVariable("giveMinutesLateHint", true);
			}
			else if (string.Equals(action, "callOrganizer", StringComparison.OrdinalIgnoreCase))
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalledMeetingOrganizer);
			}
			else if (string.Equals(action, "selectLanguage", StringComparison.OrdinalIgnoreCase))
			{
				input = base.SelectLanguage();
			}
			else
			{
				if (!string.Equals(action, "nextLanguage", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				input = base.NextLanguage(vo);
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000DE1C File Offset: 0x0000C01C
		internal string EndOfAgenda()
		{
			string result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "No more meetings today.", new object[0]);
			if (!this.Navigator.SeekNext())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "No more meetings within seek threshold.", new object[0]);
				result = "emptyCalendar";
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "creating new agenda context.", new object[0]);
				this.Context = new CalendarNavigator.AgendaContext(this.Navigator.CurrentAgenda, this.user, false, false);
			}
			return result;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000DEA4 File Offset: 0x0000C0A4
		protected override string SendMessage(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarManager::SendMessage.", new object[0]);
			string result = base.SendMessage(vo);
			IntroType messageIntroType = base.MessageIntroType;
			this.WriteMeetingVariables(vo);
			base.WriteReplyIntroType(messageIntroType);
			return result;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		private string SeekValidMeeting(BaseUMCallSession vo)
		{
			string result = null;
			ExDateTime currentDay = this.Navigator.CurrentDay;
			if (!this.Context.IsValid)
			{
				result = this.EndOfAgenda();
			}
			this.WriteMeetingVariables(vo);
			base.WriteVariable("dateChanged", currentDay.Date != this.Navigator.CurrentDay.Date);
			return result;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000DF50 File Offset: 0x0000C150
		private string PreviousMeeting(BaseUMCallSession vo)
		{
			string result = null;
			ExDateTime currentDay = this.Navigator.CurrentDay;
			if (!this.Context.Previous())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarManager.Previous called with no previous meeting.", new object[0]);
				this.Navigator.Previous();
				this.Context = new CalendarNavigator.AgendaContext(this.Navigator.CurrentAgenda, this.user, false, false);
				if (!this.Context.SeekLast())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Context.SeekLast returns false.  Firing autoevent NoMeetings.", new object[0]);
					result = "noMeetings";
				}
			}
			else if (this.Context.Current.StartTime.Date != this.Navigator.CurrentDay.Date)
			{
				this.Navigator.Goto(this.Context.Current.StartTime.Date);
			}
			base.WriteVariable("dateChanged", currentDay.Date != this.Navigator.CurrentDay.Date);
			this.WriteMeetingVariables(vo);
			return result;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E070 File Offset: 0x0000C270
		private string NextDay(BaseUMCallSession vo)
		{
			string result = null;
			this.Navigator.Next();
			this.Context = new CalendarNavigator.AgendaContext(this.Navigator.CurrentAgenda, this.user, false, false);
			if (0 < this.Context.TotalCount)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarManager.NextDay found remaining={0} meetings on day={1}.", new object[]
				{
					this.Context.Remaining,
					this.Navigator.CurrentDay
				});
			}
			else
			{
				result = "noMeetings";
			}
			base.WriteVariable("dateChanged", true);
			this.WriteMeetingVariables(vo);
			return result;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E118 File Offset: 0x0000C318
		private string NextMeeting(BaseUMCallSession vo)
		{
			string result = null;
			ExDateTime currentDay = this.Navigator.CurrentDay;
			if (!this.Context.Next())
			{
				result = this.EndOfAgenda();
			}
			else
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalendarItemsHeard);
			}
			base.WriteVariable("dateChanged", currentDay.Date != this.Navigator.CurrentDay.Date);
			this.WriteMeetingVariables(vo);
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E18C File Offset: 0x0000C38C
		private string PreviousMeetingSameDay(BaseUMCallSession vo)
		{
			string result = null;
			StoreObjectId uniqueId = this.Context.Current.UniqueId;
			this.Context.Previous();
			if (!uniqueId.Equals(this.Context.Current.UniqueId))
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalendarItemsHeard);
				this.WriteMeetingVariables(vo);
			}
			else
			{
				result = "noMeetings";
			}
			return result;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E1EC File Offset: 0x0000C3EC
		private string FirstMeetingSameDay(BaseUMCallSession vo)
		{
			string result = null;
			StoreObjectId uniqueId = this.Context.Current.UniqueId;
			this.Context.SeekFirst();
			if (!uniqueId.Equals(this.Context.Current.UniqueId))
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalendarItemsHeard);
				this.WriteMeetingVariables(vo);
			}
			else
			{
				result = "noMeetings";
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E24C File Offset: 0x0000C44C
		private string LastMeetingSameDay(BaseUMCallSession vo)
		{
			string result = null;
			StoreObjectId uniqueId = this.Context.Current.UniqueId;
			this.Context.SeekLast();
			if (!uniqueId.Equals(this.Context.Current.UniqueId))
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalendarItemsHeard);
				this.WriteMeetingVariables(vo);
			}
			else
			{
				result = "noMeetings";
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		private string NextMeetingSameDay(BaseUMCallSession vo)
		{
			string result = null;
			if (!this.Context.Next(true))
			{
				result = "noMeetings";
			}
			else
			{
				vo.IncrementCounter(SubscriberAccessCounters.CalendarItemsHeard);
			}
			this.WriteMeetingVariables(vo);
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		private void WriteMeetingVariables(BaseUMCallSession vo)
		{
			ExDateTime now = this.user.Now;
			ExDateTime date = now.Date;
			base.WriteVariable("skipHeader", false);
			base.WriteReplyIntroType(IntroType.None);
			base.WriteVariable("remaining", this.Context.Remaining);
			base.WriteVariable("calendarDate", this.Navigator.CurrentDay);
			base.WriteVariable("numConflicts", this.Context.ConflictCount);
			base.WriteVariable("current", null);
			base.WriteVariable("calendarDate", this.Navigator.CurrentDay);
			base.WriteVariable("dayOfWeek", (int)this.Navigator.CurrentDay.DayOfWeek);
			base.WriteVariable("dayOffset", (this.Navigator.CurrentDay - date).Days);
			base.WriteVariable("messageLanguage", null);
			base.WriteVariable("languageDetected", null);
			if (this.Context.IsValid)
			{
				CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				base.WriteVariable("current", meetingInfo);
				base.MessagePlayerContext.Reset(meetingInfo.UniqueId);
				base.WriteVariable("startTime", meetingInfo.StartTime);
				base.WriteVariable("endTime", meetingInfo.EndTime);
				base.WriteVariable("meetingTimeRange", new TimeRange(meetingInfo.StartTime, meetingInfo.EndTime));
				base.WriteVariable("time", meetingInfo.StartTime);
				base.WriteVariable("subject", meetingInfo.Subject);
				base.WriteVariable("location", meetingInfo.Location);
				bool flag = meetingInfo.StartTime.Date == date;
				bool flag2 = meetingInfo.EndTime < now;
				bool flag3 = meetingInfo.StartTime > now;
				base.WriteVariable("today", flag);
				base.WriteVariable("past", flag2);
				base.WriteVariable("future", flag3);
				base.WriteVariable("present", !flag2 && !flag3);
				base.WriteVariable("first", this.Context.IsFirst);
				base.WriteVariable("firstConflict", this.Context.IsFirstConflict);
				base.WriteVariable("middle", !this.Context.IsFirst && !this.Context.IsLast);
				base.WriteVariable("last", this.Context.IsLast);
				base.WriteVariable("initial", this.Context.IsInitialPosition);
				base.WriteVariable("tentative", meetingInfo.FreeBusyStatus == BusyType.Tentative);
				base.WriteVariable("owner", meetingInfo.IsOrganizer);
				base.WriteVariable("conflictTime", this.Context.ConflictTime);
				base.WriteVariable("isAllDayEvent", meetingInfo.IsAllDayEvent);
				base.WriteVariable("organizerPhone", meetingInfo.OrganizerPhone);
				base.WriteVariable("locationPhone", meetingInfo.LocationPhone);
				base.WriteVariable("isMeeting", meetingInfo.IsMeeting);
				base.WriteVariable("conflictWithLastHeard", this.Context.ConflictsWithLastHeard);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		private string GetDetails()
		{
			string result = null;
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			base.CallSession.IncrementCounter(SubscriberAccessCounters.CalendarItemsDetailsRequested);
			if (meetingInfo.IsOrganizer)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				foreach (CalendarNavigator.AttendeeInfo attendeeInfo in meetingInfo.Attendees)
				{
					switch (attendeeInfo.ResponseType)
					{
					case ResponseType.None:
					case ResponseType.Tentative:
					case ResponseType.NotResponded:
						num3++;
						break;
					case ResponseType.Accept:
						num++;
						break;
					case ResponseType.Decline:
						num2++;
						break;
					}
				}
				base.WriteVariable("numAccepted", num);
				base.WriteVariable("numDeclined", num2);
				base.WriteVariable("numUndecided", num3);
			}
			else
			{
				base.WriteVariable("ownerName", meetingInfo.OrganizerName);
				base.WriteVariable("numAttendees", meetingInfo.Attendees.Count);
			}
			return result;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
		private string GetParticipants()
		{
			string result = null;
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			if (meetingInfo.IsOrganizer)
			{
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				StringBuilder stringBuilder3 = new StringBuilder();
				foreach (CalendarNavigator.AttendeeInfo attendeeInfo in meetingInfo.Attendees)
				{
					switch (attendeeInfo.ResponseType)
					{
					case ResponseType.None:
					case ResponseType.Tentative:
					case ResponseType.NotResponded:
						stringBuilder3.Append(attendeeInfo.Participant.DisplayName);
						stringBuilder3.Append(", ");
						break;
					case ResponseType.Accept:
						stringBuilder.Append(attendeeInfo.Participant.DisplayName);
						stringBuilder.Append(", ");
						break;
					case ResponseType.Decline:
						stringBuilder2.Append(attendeeInfo.Participant.DisplayName);
						stringBuilder2.Append(", ");
						break;
					}
				}
				base.WriteVariable("acceptedList", (stringBuilder.Length == 0) ? null : stringBuilder.ToString());
				base.WriteVariable("declinedList", (stringBuilder2.Length == 0) ? null : stringBuilder2.ToString());
				base.WriteVariable("undecidedList", (stringBuilder3.Length == 0) ? null : stringBuilder3.ToString());
			}
			else
			{
				base.WriteVariable("ownerName", meetingInfo.OrganizerName);
				StringBuilder stringBuilder4 = new StringBuilder();
				foreach (CalendarNavigator.AttendeeInfo attendeeInfo2 in meetingInfo.Attendees)
				{
					stringBuilder4.Append(attendeeInfo2.Participant.DisplayName);
					stringBuilder4.Append(", ");
				}
				base.WriteVariable("attendeeList", (stringBuilder4.Length == 0) ? null : stringBuilder4.ToString());
			}
			return result;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		private string LateForMeeting()
		{
			string result = null;
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			int num = (int)(this.ReadVariable("minutesLateMin") ?? 0);
			int num2 = (int)(this.ReadVariable("minutesLateMax") ?? 0);
			if (base.NumericInput != 0)
			{
				num = 0;
				num2 = base.NumericInput;
			}
			base.WriteVariable("minutesLateMin", num);
			base.WriteVariable("minutesLateMax", num2);
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Sending I'll Be Late message with minutesLateMax={0}, minutesLateMin={1}.", new object[]
			{
				num2,
				num
			});
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(mailboxSessionLock.Session, meetingInfo.UniqueId))
				{
					using (MessageItem messageItem = calendarItemBase.CreateReplyAll(XsoUtil.GetDraftsFolderId(mailboxSessionLock.Session), new ReplyForwardConfiguration(BodyFormat.TextHtml)))
					{
						using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
						{
							textWriter.Write(this.BuildLateMessageBody(num, num2, calendarItemBase));
						}
						messageItem.Send();
					}
				}
			}
			base.CallSession.IncrementCounter(SubscriberAccessCounters.CalendarLateAttendance);
			PIIMessage data = PIIMessage.Create(PIIType._User, this.user);
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, data, "Succesfully sent I'll be late message for user=_User.", new object[0]);
			return result;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000EBC4 File Offset: 0x0000CDC4
		private string BuildLateMessageBody(int minutesLateMin, int minutesLateMax, CalendarItemBase cal)
		{
			MessageContentBuilder messageContentBuilder = MessageContentBuilder.Create(this.user.TelephonyCulture);
			LocalizedString lateInfo;
			if (Regex.Match(minutesLateMax.ToString(this.user.TelephonyCulture), PromptConfigBase.PromptResourceManager.GetString("Singular", this.user.TelephonyCulture)).Success)
			{
				lateInfo = ((minutesLateMin > 0) ? Strings.LateForMeetingRange_Singular(minutesLateMin, minutesLateMax) : Strings.LateForMeeting_Singular(minutesLateMax));
			}
			else if (Regex.Match(minutesLateMax.ToString(this.user.TelephonyCulture), PromptConfigBase.PromptResourceManager.GetString("Plural", this.user.TelephonyCulture)).Success)
			{
				lateInfo = ((minutesLateMin > 0) ? Strings.LateForMeetingRange_Plural(minutesLateMin, minutesLateMax) : Strings.LateForMeeting_Plural(minutesLateMax));
			}
			else if (Regex.Match(minutesLateMax.ToString(this.user.TelephonyCulture), PromptConfigBase.PromptResourceManager.GetString("Plural2", this.user.TelephonyCulture)).Success)
			{
				lateInfo = ((minutesLateMin > 0) ? Strings.LateForMeetingRange_Plural2(minutesLateMin, minutesLateMax) : Strings.LateForMeeting_Plural2(minutesLateMax));
			}
			else
			{
				lateInfo = ((minutesLateMin > 0) ? Strings.LateForMeetingRange_Zero(minutesLateMin, minutesLateMax) : Strings.LateForMeeting_Zero(minutesLateMax));
			}
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				messageContentBuilder.AddLateForMeetingBody(cal, mailboxSessionLock.Session.ExTimeZone, lateInfo);
			}
			return messageContentBuilder.ToString();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000ED24 File Offset: 0x0000CF24
		private void ParseDateSpeech()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarManager::ParseDateSpeech.", new object[0]);
			if (!(base.RecoResult["RelativeDayOffset"] is string))
			{
				this.ParseAbsoluteDateSpeech();
			}
			else
			{
				this.ParseRelativeDateSpeech();
			}
			base.WriteVariable("calendarDate", this.targetDate);
			base.WriteVariable("dayOfWeek", (int)this.targetDate.DayOfWeek);
			base.WriteVariable("dayOffset", (this.targetDate - this.user.Now.Date).Days);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		private void ParseAbsoluteDateSpeech()
		{
			int num = int.Parse((string)base.RecoResult["Year"], CultureInfo.InvariantCulture);
			int num2 = int.Parse((string)base.RecoResult["Month"], CultureInfo.InvariantCulture);
			int num3 = int.Parse((string)base.RecoResult["Day"], CultureInfo.InvariantCulture);
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Setting absolute date with year={0}, month={1}, day={2}.", new object[]
			{
				num,
				num2,
				num3
			});
			this.targetDate = new ExDateTime(this.user.TimeZone, num, num2, num3);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000EE94 File Offset: 0x0000D094
		private void ParseRelativeDateSpeech()
		{
			string text = (string)base.RecoResult["RelativeDayOffset"];
			ExDateTime date = this.user.Now.Date;
			int num;
			if (!string.IsNullOrEmpty(text))
			{
				num = int.Parse(text, CultureInfo.InvariantCulture);
			}
			else
			{
				int num2 = int.Parse((string)base.RecoResult["SpokenDay"], CultureInfo.InvariantCulture);
				int dayOfWeek = (int)date.DayOfWeek;
				if (dayOfWeek < num2)
				{
					num = num2 - dayOfWeek;
				}
				else
				{
					num = 7 + (num2 - dayOfWeek);
				}
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Setting relative day with dateOffset={0}.", new object[]
			{
				num
			});
			this.targetDate = date.AddDays((double)num);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000EF54 File Offset: 0x0000D154
		private string OpenCalendarDate(BaseUMCallSession vo)
		{
			string result = null;
			ExDateTime date = this.user.Now.Date;
			this.Navigator.Goto(this.targetDate);
			this.Context = new CalendarNavigator.AgendaContext(this.Navigator.CurrentAgenda, this.user, this.targetDate.Date == date, false);
			if ((date == this.targetDate && 0 < this.Context.Remaining) || (date != this.targetDate && 0 < this.Context.TotalCount))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarManager.OpenCalendarDate found remaining={0} meetings on day={1}.", new object[]
				{
					this.Context.Remaining,
					this.Navigator.CurrentDay
				});
			}
			else
			{
				result = "noMeetings";
			}
			this.WriteMeetingVariables(vo);
			return result;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F03C File Offset: 0x0000D23C
		private void AcceptMeeting()
		{
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			meetingInfo.AcceptMeeting();
			base.CallSession.IncrementCounter(SubscriberAccessCounters.MeetingsAccepted);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F088 File Offset: 0x0000D288
		private void MarkAsTentative()
		{
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			meetingInfo.MarkAsTentative();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		private void ParseLateMinutes()
		{
			base.WriteVariable("minutesLateMax", 0);
			base.WriteVariable("minutesLateMin", 0);
			string text = base.RecoResult["RangeStart"] as string;
			string text2 = base.RecoResult["RangeEnd"] as string;
			string text3 = base.RecoResult["Minutes"] as string;
			int num = 0;
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				int val = int.Parse(text, CultureInfo.InvariantCulture);
				int val2 = int.Parse(text2, CultureInfo.InvariantCulture);
				num = Math.Max(val, val2);
				int num2 = Math.Min(val, val2);
				base.WriteVariable("minutesLateMin", num2);
			}
			else if (!string.IsNullOrEmpty(text3))
			{
				num = int.Parse(text3, CultureInfo.InvariantCulture);
			}
			base.WriteVariable("minutesLateMax", num);
			if (string.Compare(base.LastRecoEvent, "recoSendLateMessageMinutes", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(base.LastRecoEvent, "recoNotSure", StringComparison.OrdinalIgnoreCase) == 0)
			{
				base.WriteVariable("giveMinutesLateHint", false);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		private ExDateTime ParseClearTime(IUMRecognitionResult result)
		{
			int num = int.Parse((string)result["Hour"], CultureInfo.InvariantCulture);
			int num2 = int.Parse((string)result["Minute"], CultureInfo.InvariantCulture);
			int num3 = num;
			if (!int.TryParse((string)result["AlternateHour"], out num3))
			{
				num3 = num;
			}
			CalendarNavigator.MeetingInfo mi = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			ExDateTime startTimeToClearFrom = this.GetStartTimeToClearFrom(mi);
			if (num < startTimeToClearFrom.Hour)
			{
				num = num3;
			}
			return startTimeToClearFrom.Date.AddHours((double)num).AddMinutes((double)num2);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		private ExDateTime ParseClearDays(IUMRecognitionResult result)
		{
			int num = int.Parse((string)result["Days"], CultureInfo.InvariantCulture);
			CalendarNavigator.MeetingInfo mi = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			ExDateTime result2 = this.GetStartTimeToClearFrom(mi).Date.AddDays((double)num).AddSeconds(-1.0);
			base.WriteVariable("clearDays", num);
			return result2;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F338 File Offset: 0x0000D538
		private string ParseClearTimeDays()
		{
			ExDateTime exDateTime = ExDateTime.MaxValue;
			string result = null;
			string lastRecoEvent;
			if ((lastRecoEvent = base.LastRecoEvent) != null)
			{
				if (!(lastRecoEvent == "recoTimePhrase"))
				{
					if (!(lastRecoEvent == "recoDayPhrase"))
					{
						if (!(lastRecoEvent == "recoAmbiguousPhrase"))
						{
							if (!(lastRecoEvent == "recoTimeOfDay"))
							{
								if (lastRecoEvent == "recoNumberOfDays")
								{
									exDateTime = this.ambiguousDay;
								}
							}
							else
							{
								exDateTime = this.ambiguousTime;
							}
						}
						else
						{
							this.ambiguousTime = this.ParseClearTime(base.RecoResult);
							this.ambiguousDay = this.ParseClearDays(base.RecoResult);
						}
					}
					else
					{
						exDateTime = this.ParseClearDays(base.RecoResult);
					}
				}
				else
				{
					exDateTime = this.ParseClearTime(base.RecoResult);
				}
			}
			if (ExDateTime.MaxValue != exDateTime)
			{
				base.WriteVariable("clearTime", exDateTime);
				CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
				if (exDateTime < meetingInfo.StartTime)
				{
					result = "invalidTime";
				}
			}
			return result;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F44C File Offset: 0x0000D64C
		private void ParseClearHours()
		{
			int numericInput = base.NumericInput;
			CalendarNavigator.MeetingInfo meetingInfo = (this.Context.ConflictCount > 0) ? this.Context.CurrentConflict : this.Context.Current;
			ExDateTime exDateTime = meetingInfo.StartTime.AddHours((double)numericInput);
			base.WriteVariable("clearTime", exDateTime);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		private ExDateTime GetStartTimeToClearFrom(CalendarNavigator.MeetingInfo mi)
		{
			ExDateTime exDateTime = (ExDateTime)this.ReadVariable("calendarDate");
			if (!(mi.StartTime > exDateTime))
			{
				return exDateTime;
			}
			return mi.StartTime;
		}

		// Token: 0x040000F5 RID: 245
		private const int MaxDigitsInHours = 3;

		// Token: 0x040000F6 RID: 246
		private CalendarNavigator nav;

		// Token: 0x040000F7 RID: 247
		private CalendarNavigator.AgendaContext agendaCtx;

		// Token: 0x040000F8 RID: 248
		private UMSubscriber user;

		// Token: 0x040000F9 RID: 249
		private ExDateTime targetDate;

		// Token: 0x040000FA RID: 250
		private ExDateTime ambiguousTime;

		// Token: 0x040000FB RID: 251
		private ExDateTime ambiguousDay;

		// Token: 0x0200004B RID: 75
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000325 RID: 805 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000326 RID: 806 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing CalendarManager", new object[0]);
				return new CalendarManager(manager, this);
			}
		}

		// Token: 0x0200004E RID: 78
		internal abstract class ReplyToBase : XsoRecordedMessage
		{
			// Token: 0x0600033F RID: 831 RVA: 0x0000F923 File Offset: 0x0000DB23
			internal ReplyToBase(BaseUMCallSession vo, UMSubscriber user, CalendarManager manager, CalendarNavigator.MeetingInfo mi) : base(vo, user, manager)
			{
				this.mi = mi;
			}

			// Token: 0x06000340 RID: 832 RVA: 0x0000F936 File Offset: 0x0000DB36
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "ReplyToBase::DoPostSubmit.", new object[0]);
				base.Session.IncrementCounter(SubscriberAccessCounters.ReplyMessagesSent);
				base.DoPostSubmit();
			}

			// Token: 0x06000341 RID: 833 RVA: 0x0000F964 File Offset: 0x0000DB64
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "ReplyToBase::GenerateMessage.", new object[0]);
				MessageItem result;
				using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(session, this.mi.UniqueId))
				{
					base.SetAttachmentName(calendarItemBase.AttachmentCollection);
					MessageItem messageItem = this.CreateReplyMessage(calendarItemBase, base.PrepareMessageBodyPrefix(calendarItemBase), BodyFormat.TextHtml, XsoUtil.GetDraftsFolderId(session));
					result = messageItem;
				}
				return result;
			}

			// Token: 0x06000342 RID: 834
			protected abstract MessageItem CreateReplyMessage(CalendarItemBase cal, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId);

			// Token: 0x06000343 RID: 835 RVA: 0x0000F9DC File Offset: 0x0000DBDC
			protected override void AddRecordedMessageText(MessageContentBuilder content)
			{
				content.AddRecordedReplyText(base.User.DisplayName);
			}

			// Token: 0x06000344 RID: 836 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
			protected override void AddMessageHeader(Item originalMessage, MessageContentBuilder content)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = base.User.CreateSessionLock())
				{
					content.AddCalendarHeader((CalendarItemBase)originalMessage, mailboxSessionLock.Session.ExTimeZone, true);
				}
			}

			// Token: 0x04000104 RID: 260
			private CalendarNavigator.MeetingInfo mi;
		}

		// Token: 0x0200004F RID: 79
		internal class ReplyToOrganizer : CalendarManager.ReplyToBase
		{
			// Token: 0x06000345 RID: 837 RVA: 0x0000FA40 File Offset: 0x0000DC40
			internal ReplyToOrganizer(BaseUMCallSession vo, UMSubscriber user, CalendarManager manager, CalendarNavigator.MeetingInfo mi) : base(vo, user, manager, mi)
			{
			}

			// Token: 0x06000346 RID: 838 RVA: 0x0000FA4D File Offset: 0x0000DC4D
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "ReplyToOrganizer::DoPostSubmit.", new object[0]);
				base.Session.IncrementCounter(SubscriberAccessCounters.RepliedToOrganizer);
				base.DoPostSubmit();
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000FA7C File Offset: 0x0000DC7C
			protected override MessageItem CreateReplyMessage(CalendarItemBase cal, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId)
			{
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
				return cal.CreateReply(parentFolderId, replyForwardConfiguration);
			}
		}

		// Token: 0x02000050 RID: 80
		internal class ReplyToAll : CalendarManager.ReplyToBase
		{
			// Token: 0x06000348 RID: 840 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
			internal ReplyToAll(BaseUMCallSession vo, UMSubscriber user, CalendarManager manager, CalendarNavigator.MeetingInfo mi) : base(vo, user, manager, mi)
			{
			}

			// Token: 0x06000349 RID: 841 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
			protected override MessageItem CreateReplyMessage(CalendarItemBase cal, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId)
			{
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
				return cal.CreateReplyAll(parentFolderId, replyForwardConfiguration);
			}
		}

		// Token: 0x02000051 RID: 81
		internal class CalendarForward : XsoRecordedMessage
		{
			// Token: 0x0600034A RID: 842 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
			internal CalendarForward(BaseUMCallSession vo, UMSubscriber user, CalendarManager manager, CalendarNavigator.MeetingInfo mi) : base(vo, user, manager)
			{
				this.mi = mi;
			}

			// Token: 0x0600034B RID: 843 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				MessageItem result;
				using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(session, this.mi.UniqueId))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CalendarForward::GenerateResponse.", new object[0]);
					base.SetAttachmentName(calendarItemBase.AttachmentCollection);
					ReplyForwardConfiguration replyForwardParameters = new ReplyForwardConfiguration(BodyFormat.TextHtml);
					result = calendarItemBase.CreateForward(XsoUtil.GetDraftsFolderId(session), replyForwardParameters);
				}
				return result;
			}

			// Token: 0x04000105 RID: 261
			private CalendarNavigator.MeetingInfo mi;
		}

		// Token: 0x02000052 RID: 82
		internal class CancelOrDecline : XsoRecordedMessage
		{
			// Token: 0x0600034C RID: 844 RVA: 0x0000FB5C File Offset: 0x0000DD5C
			internal CancelOrDecline(BaseUMCallSession vo, UMSubscriber user, CalendarNavigator.MeetingInfo mi, CalendarManager cm, bool autoPostSubmit) : base(vo, user, cm, autoPostSubmit)
			{
				this.mi = mi;
			}

			// Token: 0x0600034D RID: 845 RVA: 0x0000FB71 File Offset: 0x0000DD71
			internal CancelOrDecline(BaseUMCallSession vo, UMSubscriber user, CalendarNavigator.MeetingInfo mi, CalendarManager cm) : this(vo, user, mi, cm, true)
			{
			}

			// Token: 0x0600034E RID: 846 RVA: 0x0000FB80 File Offset: 0x0000DD80
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelOrDecline::DoPostSubmit.", new object[0]);
				CalendarManager calendarManager = (CalendarManager)base.Manager;
				calendarManager.Context.Remove(this.mi.UniqueId);
				UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = null;
				try
				{
					mailboxSessionLock = base.User.CreateSessionLock();
					mailboxSessionLock.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
					{
						this.mi.UniqueId
					});
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelOrDecline::DoPostSubmit successfully deleted original calendar item.", new object[0]);
				}
				catch (StorageTransientException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.CalendarTracer, this, "CancelOrDecline::DoPostSubmit failed to delete the calendar item! e={0}", new object[]
					{
						ex
					});
				}
				catch (StoragePermanentException ex2)
				{
					CallIdTracer.TraceError(ExTraceGlobals.CalendarTracer, this, "CancelOrDecline::DoPostSubmit failed to delete the calendar item! e={0}", new object[]
					{
						ex2
					});
				}
				finally
				{
					if (mailboxSessionLock != null)
					{
						mailboxSessionLock.Dispose();
					}
				}
				base.DoPostSubmit();
			}

			// Token: 0x0600034F RID: 847 RVA: 0x0000FC90 File Offset: 0x0000DE90
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelOrDecline::GenerateMessage.", new object[0]);
				CalendarManager calendarManager = (CalendarManager)base.Manager;
				MessageItem messageItem = null;
				using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(session, this.mi.UniqueId))
				{
					calendarItemBase.OpenAsReadWrite();
					if (this.mi.IsOrganizer && this.mi.IsMeeting)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "User is organizer...creating meeting cancellation.", new object[0]);
						messageItem = calendarItemBase.CancelMeeting(null, null);
					}
					else
					{
						if (this.mi.IsOrganizer)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelMeeeting::GenerateMessage creating message for appointment.", new object[0]);
							using (calendarItemBase.CancelMeeting(null, null))
							{
								StoreObjectId draftsFolderId = XsoUtil.GetDraftsFolderId(session);
								messageItem = MessageItem.Create(session, draftsFolderId);
								messageItem.Subject = Strings.CancelledMeetingSubject((string)XsoUtil.SafeGetProperty(calendarItemBase, ItemSchema.NormalizedSubject, string.Empty)).ToString(base.User.TelephonyCulture);
								Participant participant = new Participant(base.User.DisplayName, base.User.ExchangeLegacyDN, "EX");
								messageItem.Recipients.Add(participant, RecipientItemType.To);
								goto IL_159;
							}
						}
						CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "User is NOT organizer...creating meeting decline.", new object[0]);
						messageItem = XsoUtil.RespondToMeetingRequest(calendarItemBase, ResponseType.Decline);
					}
					IL_159:
					calendarItemBase.Load(new PropertyDefinition[]
					{
						ItemSchema.HasAttachment
					});
					base.SetAttachmentName(calendarItemBase.AttachmentCollection);
				}
				return messageItem;
			}

			// Token: 0x04000106 RID: 262
			private CalendarNavigator.MeetingInfo mi;
		}

		// Token: 0x02000053 RID: 83
		internal class CancelSeveralMeetings : IRecordedMessage
		{
			// Token: 0x06000350 RID: 848 RVA: 0x0000FE5C File Offset: 0x0000E05C
			internal CancelSeveralMeetings(BaseUMCallSession vo, UMSubscriber user, ExDateTime initialStartTime, CalendarManager cm, bool reserveTime)
			{
				this.vo = vo;
				this.meetingsToCancel = new List<CalendarNavigator.MeetingInfo>(16);
				this.cm = cm;
				this.startTime = initialStartTime;
				this.endTime = this.startTime;
				this.user = user;
				this.reserveTime = reserveTime;
			}

			// Token: 0x06000351 RID: 849 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
			public void DoSubmit(Importance imp, bool markPrivate, Stack<Participant> recips)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelSeveralMeetings::DoSubmit.", new object[0]);
				ExAssert.RetailAssert(!markPrivate, "Calendar meetings cannot be marked private");
				this.BuildMeetingList();
				foreach (CalendarNavigator.MeetingInfo meetingInfo in this.meetingsToCancel)
				{
					if (!meetingInfo.IsCancelled)
					{
						CalendarManager.CancelOrDecline cancelOrDecline = new CalendarManager.CancelOrDecline(this.vo, this.user, meetingInfo, this.cm, false);
						cancelOrDecline.DoSubmit(imp, false, recips);
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelSeveralMeetings::DoPostSubmit.", new object[0]);
				this.DoPostSubmit();
			}

			// Token: 0x06000352 RID: 850 RVA: 0x0000FF70 File Offset: 0x0000E170
			public void DoSubmit(Importance imp)
			{
				this.DoSubmit(imp, false, null);
			}

			// Token: 0x06000353 RID: 851 RVA: 0x0000FF7C File Offset: 0x0000E17C
			public void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelSeveralMeetings::DoPostSubmit.", new object[0]);
				this.cm.Context.RemoveMeetings(this.meetingIdsToDelete);
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Removed items between start={0} and end={1} from the agenda.", new object[]
				{
					this.startTime,
					this.endTime
				});
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					mailboxSessionLock.Session.Delete(DeleteItemFlags.MoveToDeletedItems, this.meetingIdsToDelete.ToArray());
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "Removed items between start={0} and end={1} from the store.", new object[]
				{
					this.startTime,
					this.endTime
				});
				if (this.reserveTime)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelSeveralMeetings::DoPostSubmit::Creating a reserved time entry.", new object[0]);
					this.CreateReservedTimeEntry();
				}
				this.vo.IncrementCounter(SubscriberAccessCounters.VoiceMessagesSent, (long)this.meetingsToCancel.Count);
				this.cm.RecordContext.Reset();
			}

			// Token: 0x06000354 RID: 852 RVA: 0x000100B0 File Offset: 0x0000E2B0
			private void BuildMeetingList()
			{
				this.meetingsToCancel.Clear();
				this.meetingIdsToDelete = null;
				this.endTime = (ExDateTime)this.cm.ReadVariable("clearTime");
				CallIdTracer.TraceDebug(ExTraceGlobals.CalendarTracer, this, "CancelSeveralMeetings::DoSubmit cancelling all meetings between {0} and {1}.", new object[]
				{
					this.startTime,
					this.endTime
				});
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar)))
					{
						object[][] calendarView = calendarFolder.GetCalendarView(this.startTime, this.endTime, new PropertyDefinition[]
						{
							ItemSchema.Id,
							CalendarItemInstanceSchema.StartTime,
							CalendarItemInstanceSchema.EndTime,
							CalendarItemBaseSchema.IsAllDayEvent,
							CalendarItemBaseSchema.AppointmentState
						});
						this.meetingIdsToDelete = new List<StoreObjectId>(calendarView.Length);
						for (int i = 0; i < calendarView.Length; i++)
						{
							ExDateTime t = (ExDateTime)calendarView[i][1];
							ExDateTime t2 = (ExDateTime)calendarView[i][2];
							if (t >= this.startTime && t2 <= this.endTime)
							{
								this.meetingsToCancel.Add(new CalendarNavigator.MeetingInfo(calendarView[i], this.user));
								this.meetingIdsToDelete.Add(((VersionedId)calendarView[i][0]).ObjectId);
							}
						}
					}
				}
			}

			// Token: 0x06000355 RID: 853 RVA: 0x0001024C File Offset: 0x0000E44C
			private void CreateReservedTimeEntry()
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					using (CalendarItem calendarItem = CalendarItem.Create(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar)))
					{
						LocalizedString localizedString = Strings.ReservedTimeBody(this.user.DisplayName, calendarItem.CreationTime.ToString(this.user.TelephonyCulture));
						calendarItem.StartTime = this.startTime;
						calendarItem.EndTime = this.endTime;
						calendarItem.FreeBusyStatus = BusyType.OOF;
						calendarItem.Reminder.IsSet = false;
						calendarItem.Subject = Strings.ReservedTimeTitle.ToString(this.user.TelephonyCulture);
						using (TextWriter textWriter = calendarItem.Body.OpenTextWriter(BodyFormat.TextPlain))
						{
							textWriter.Write(localizedString.ToString(this.user.TelephonyCulture));
						}
						calendarItem.Save(SaveMode.NoConflictResolution);
						calendarItem.Load(new PropertyDefinition[]
						{
							ItemSchema.Id
						});
						this.cm.Navigator.SkipMeeting(calendarItem.Id.ObjectId);
					}
				}
			}

			// Token: 0x04000107 RID: 263
			private List<CalendarNavigator.MeetingInfo> meetingsToCancel;

			// Token: 0x04000108 RID: 264
			private List<StoreObjectId> meetingIdsToDelete;

			// Token: 0x04000109 RID: 265
			private ExDateTime startTime;

			// Token: 0x0400010A RID: 266
			private ExDateTime endTime;

			// Token: 0x0400010B RID: 267
			private CalendarManager cm;

			// Token: 0x0400010C RID: 268
			private UMSubscriber user;

			// Token: 0x0400010D RID: 269
			private bool reserveTime;

			// Token: 0x0400010E RID: 270
			private BaseUMCallSession vo;
		}
	}
}
