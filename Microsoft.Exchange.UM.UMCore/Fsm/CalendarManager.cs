using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030C RID: 780
	internal class CalendarManager
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x000660E6 File Offset: 0x000642E6
		internal static void GetScope(ActivityManager manager, out CalendarManager scope)
		{
			for (scope = (manager as CalendarManager); scope == null; scope = (manager as CalendarManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00066119 File Offset: 0x00064319
		internal static TransitionBase AcceptMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00066123 File Offset: 0x00064323
		internal static TransitionBase AddRecipientBySearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006612D File Offset: 0x0006432D
		internal static TransitionBase AppendRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00066137 File Offset: 0x00064337
		internal static TransitionBase CallOrganizer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00066141 File Offset: 0x00064341
		internal static TransitionBase CancelMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x0006614B File Offset: 0x0006434B
		internal static TransitionBase CancelOrDecline(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00066155 File Offset: 0x00064355
		internal static TransitionBase CancelSeveral(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x0006615F File Offset: 0x0006435F
		internal static TransitionBase ClearMinutesLate(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00066169 File Offset: 0x00064369
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00066173 File Offset: 0x00064373
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006617D File Offset: 0x0006437D
		internal static TransitionBase FirstMeetingSameDay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00066187 File Offset: 0x00064387
		internal static TransitionBase Forward(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00066191 File Offset: 0x00064391
		internal static TransitionBase GetDetails(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0006619B File Offset: 0x0006439B
		internal static TransitionBase GetParticipants(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000661A5 File Offset: 0x000643A5
		internal static TransitionBase GetTodaysMeetings(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000661AF File Offset: 0x000643AF
		internal static TransitionBase GiveLateMinutesHint(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000661B9 File Offset: 0x000643B9
		internal static TransitionBase GiveShortcutHint(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000661C3 File Offset: 0x000643C3
		internal static TransitionBase IsValidMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000661CD File Offset: 0x000643CD
		internal static TransitionBase LastMeetingSameDay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x000661D7 File Offset: 0x000643D7
		internal static TransitionBase LateForMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x000661E1 File Offset: 0x000643E1
		internal static TransitionBase MarkAsTentative(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001860 RID: 6240 RVA: 0x000661EB File Offset: 0x000643EB
		internal static TransitionBase More(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x000661F5 File Offset: 0x000643F5
		internal static TransitionBase NextDay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000661FF File Offset: 0x000643FF
		internal static TransitionBase NextMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x00066209 File Offset: 0x00064409
		internal static TransitionBase NextMeetingSameDay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x00066213 File Offset: 0x00064413
		internal static TransitionBase OpenCalendarDate(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0006621D File Offset: 0x0006441D
		internal static TransitionBase ParseClearHours(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00066227 File Offset: 0x00064427
		internal static TransitionBase ParseClearTimeDays(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00066231 File Offset: 0x00064431
		internal static TransitionBase ParseDateSpeech(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0006623B File Offset: 0x0006443B
		internal static TransitionBase ParseLateMinutes(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00066245 File Offset: 0x00064445
		internal static TransitionBase PreviousMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0006624F File Offset: 0x0006444F
		internal static TransitionBase PreviousMeetingSameDay(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00066259 File Offset: 0x00064459
		internal static TransitionBase ReadTheHeader(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00066263 File Offset: 0x00064463
		internal static TransitionBase RemoveRecipient(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0006626D File Offset: 0x0006446D
		internal static TransitionBase ReplyToAll(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00066277 File Offset: 0x00064477
		internal static TransitionBase ReplyToOrganizer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x00066281 File Offset: 0x00064481
		internal static TransitionBase Rewind(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0006628B File Offset: 0x0006448B
		internal static TransitionBase SeekValidMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00066295 File Offset: 0x00064495
		internal static TransitionBase SelectLanguage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0006629F File Offset: 0x0006449F
		internal static TransitionBase SendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000662A9 File Offset: 0x000644A9
		internal static TransitionBase SendMessageUrgent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000662B3 File Offset: 0x000644B3
		internal static TransitionBase SkipHeader(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x000662BD File Offset: 0x000644BD
		internal static string AcceptedList(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x000662D0 File Offset: 0x000644D0
		internal static string AttendeeList(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000662E4 File Offset: 0x000644E4
		internal static ExDateTime CalendarDate(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x0006630F File Offset: 0x0006450F
		internal static object CancelIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x0006631D File Offset: 0x0006451D
		internal static object ClearCalendarIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x0006632B File Offset: 0x0006452B
		internal static int ClearDays(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00066344 File Offset: 0x00064544
		internal static ExDateTime ClearTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0006636F File Offset: 0x0006456F
		internal static object ConflictWithLastHeard(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0006637D File Offset: 0x0006457D
		internal static object Current(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0006638B File Offset: 0x0006458B
		internal static object DateChanged(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x00066399 File Offset: 0x00064599
		internal static object DayOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x000663A7 File Offset: 0x000645A7
		internal static object DayOfWeek(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x000663B5 File Offset: 0x000645B5
		internal static string DeclinedList(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x000663C8 File Offset: 0x000645C8
		internal static object DeclineIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x000663D6 File Offset: 0x000645D6
		internal static CultureInfo DefaultLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x000663E9 File Offset: 0x000645E9
		internal static object First(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000663F7 File Offset: 0x000645F7
		internal static object ForwardIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001886 RID: 6278 RVA: 0x00066405 File Offset: 0x00064605
		internal static object GiveMinutesLateHint(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x00066413 File Offset: 0x00064613
		internal static object GiveShortcutHint(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x00066421 File Offset: 0x00064621
		internal static object Initial(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0006642F File Offset: 0x0006462F
		internal static object IsAllDayEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0006643D File Offset: 0x0006463D
		internal static object IsMeeting(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0006644B File Offset: 0x0006464B
		internal static object Last(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00066459 File Offset: 0x00064659
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00066467 File Offset: 0x00064667
		internal static string LastInput(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0006647A File Offset: 0x0006467A
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00066488 File Offset: 0x00064688
		internal static string Location(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0006649B File Offset: 0x0006469B
		internal static PhoneNumber LocationPhone(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x000664AE File Offset: 0x000646AE
		internal static TimeRange MeetingTimeRange(ActivityManager manager, string variableName)
		{
			return (TimeRange)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000664C1 File Offset: 0x000646C1
		internal static CultureInfo MessageLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000664D4 File Offset: 0x000646D4
		internal static int MinutesLateMax(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000664EC File Offset: 0x000646EC
		internal static int MinutesLateMin(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x00066504 File Offset: 0x00064704
		internal static object More(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00066512 File Offset: 0x00064712
		internal static object NamesGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00066520 File Offset: 0x00064720
		internal static int NumAccepted(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00066538 File Offset: 0x00064738
		internal static int NumAttendees(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00066550 File Offset: 0x00064750
		internal static int NumDeclined(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00066568 File Offset: 0x00064768
		internal static object NumRecipients(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00066576 File Offset: 0x00064776
		internal static int NumUndecided(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0006658E File Offset: 0x0006478E
		internal static PhoneNumber OrganizerPhone(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x000665A1 File Offset: 0x000647A1
		internal static object Owner(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000665AF File Offset: 0x000647AF
		internal static object OwnerName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000665BD File Offset: 0x000647BD
		internal static object Present(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000665CB File Offset: 0x000647CB
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000665DE File Offset: 0x000647DE
		internal static object RecordingFailureCount(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000665EC File Offset: 0x000647EC
		internal static object RecordingTimedOut(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000665FA File Offset: 0x000647FA
		internal static int Remaining(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00066612 File Offset: 0x00064812
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00066620 File Offset: 0x00064820
		internal static object ReplyAllIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x0006662E File Offset: 0x0006482E
		internal static object ReplyIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x0006663C File Offset: 0x0006483C
		internal static List<CultureInfo> SelectableLanguages(ActivityManager manager, string variableName)
		{
			return (List<CultureInfo>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x0006664F File Offset: 0x0006484F
		internal static object SkipHeader(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00066660 File Offset: 0x00064860
		internal static ExDateTime StartTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006668B File Offset: 0x0006488B
		internal static string Subject(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x0006669E File Offset: 0x0006489E
		internal static object Tentative(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000666AC File Offset: 0x000648AC
		internal static string UndecidedList(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000666BF File Offset: 0x000648BF
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000666D0 File Offset: 0x000648D0
		internal static int LastInputNum(ActivityManager manager, string variableName)
		{
			CalendarManager calendarManager = manager as CalendarManager;
			if (calendarManager == null)
			{
				CalendarManager.GetScope(manager, out calendarManager);
			}
			return calendarManager.LastInputNum;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000666F8 File Offset: 0x000648F8
		internal static object MessageHasBeenSentWithHighImportance(ActivityManager manager, string variableName)
		{
			CalendarManager calendarManager = manager as CalendarManager;
			if (calendarManager == null)
			{
				CalendarManager.GetScope(manager, out calendarManager);
			}
			return calendarManager.MessageHasBeenSentWithHighImportance;
		}
	}
}
