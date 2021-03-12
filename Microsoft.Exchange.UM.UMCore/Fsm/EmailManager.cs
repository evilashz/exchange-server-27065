using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030F RID: 783
	internal class EmailManager
	{
		// Token: 0x060018F5 RID: 6389 RVA: 0x00066B12 File Offset: 0x00064D12
		internal static void GetScope(ActivityManager manager, out EmailManager scope)
		{
			for (scope = (manager as EmailManager); scope == null; scope = (manager as EmailManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00066B45 File Offset: 0x00064D45
		internal static TransitionBase AcceptMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x00066B4F File Offset: 0x00064D4F
		internal static TransitionBase AcceptMeetingTentative(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x00066B59 File Offset: 0x00064D59
		internal static TransitionBase AddRecipientBySearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x00066B63 File Offset: 0x00064D63
		internal static TransitionBase AppendRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x00066B6D File Offset: 0x00064D6D
		internal static TransitionBase CancelMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x00066B77 File Offset: 0x00064D77
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x00066B81 File Offset: 0x00064D81
		internal static TransitionBase CommitPendingDeletions(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00066B8B File Offset: 0x00064D8B
		internal static TransitionBase DeclineMeeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00066B95 File Offset: 0x00064D95
		internal static TransitionBase DeleteMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x00066B9F File Offset: 0x00064D9F
		internal static TransitionBase DeleteThread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00066BA9 File Offset: 0x00064DA9
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x00066BB3 File Offset: 0x00064DB3
		internal static TransitionBase FindByName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x00066BBD File Offset: 0x00064DBD
		internal static TransitionBase FlagMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x00066BC7 File Offset: 0x00064DC7
		internal static TransitionBase Forward(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00066BD1 File Offset: 0x00064DD1
		internal static TransitionBase HideThread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00066BDB File Offset: 0x00064DDB
		internal static TransitionBase MarkUnread(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00066BE5 File Offset: 0x00064DE5
		internal static TransitionBase More(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00066BEF File Offset: 0x00064DEF
		internal static TransitionBase NextLanguage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00066BF9 File Offset: 0x00064DF9
		internal static TransitionBase NextUnreadMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00066C03 File Offset: 0x00064E03
		internal static TransitionBase Pause(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00066C0D File Offset: 0x00064E0D
		internal static TransitionBase PreviousMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00066C17 File Offset: 0x00064E17
		internal static TransitionBase RemoveRecipient(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00066C21 File Offset: 0x00064E21
		internal static TransitionBase Reply(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x00066C2B File Offset: 0x00064E2B
		internal static TransitionBase ReplyAll(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00066C35 File Offset: 0x00064E35
		internal static TransitionBase ResetPlayback(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00066C3F File Offset: 0x00064E3F
		internal static TransitionBase SaveMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00066C49 File Offset: 0x00064E49
		internal static TransitionBase SelectLanguage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x00066C53 File Offset: 0x00064E53
		internal static TransitionBase SendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00066C5D File Offset: 0x00064E5D
		internal static TransitionBase SendMessageUrgent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00066C67 File Offset: 0x00064E67
		internal static TransitionBase SlowDown(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00066C71 File Offset: 0x00064E71
		internal static TransitionBase SpeedUp(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x00066C7B File Offset: 0x00064E7B
		internal static TransitionBase UndeleteMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00066C88 File Offset: 0x00064E88
		internal static TransitionBase NextMessage(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return manager.GetTransition(emailManager.NextMessage(vo));
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x00066CB4 File Offset: 0x00064EB4
		internal static TransitionBase Repeat(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return manager.GetTransition(emailManager.Repeat(vo));
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00066CE0 File Offset: 0x00064EE0
		internal static object CalendarStatus(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00066CEE File Offset: 0x00064EEE
		internal static object CanUndelete(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00066CFC File Offset: 0x00064EFC
		internal static object DeclineIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00066D0A File Offset: 0x00064F0A
		internal static CultureInfo DefaultLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00066D1D File Offset: 0x00064F1D
		internal static object Drm(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00066D2B File Offset: 0x00064F2B
		internal static string EmailCCField(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00066D40 File Offset: 0x00064F40
		internal static ExDateTime EmailReceivedTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00066D6C File Offset: 0x00064F6C
		internal static ExDateTime EmailRequestTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00066D97 File Offset: 0x00064F97
		internal static TimeRange EmailRequestTimeRange(ActivityManager manager, string variableName)
		{
			return (TimeRange)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00066DAA File Offset: 0x00064FAA
		internal static object EmailSender(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00066DB8 File Offset: 0x00064FB8
		internal static string EmailToField(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00066DCB File Offset: 0x00064FCB
		internal static object FindByName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x00066DD9 File Offset: 0x00064FD9
		internal static object FirstMessage(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x00066DE7 File Offset: 0x00064FE7
		internal static object ForwardIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x00066DF5 File Offset: 0x00064FF5
		internal static object InFindMode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x00066E03 File Offset: 0x00065003
		internal static object IsForward(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x00066E11 File Offset: 0x00065011
		internal static object IsHighPriority(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00066E1F File Offset: 0x0006501F
		internal static object IsMissedCall(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00066E2D File Offset: 0x0006502D
		internal static object IsRecorded(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00066E3B File Offset: 0x0006503B
		internal static object IsReply(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00066E49 File Offset: 0x00065049
		internal static object LanguageDetected(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00066E57 File Offset: 0x00065057
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00066E65 File Offset: 0x00065065
		internal static object LastMessage(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00066E73 File Offset: 0x00065073
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x00066E81 File Offset: 0x00065081
		internal static string Location(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00066E94 File Offset: 0x00065094
		internal static object MeetingCancellation(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00066EA2 File Offset: 0x000650A2
		internal static object MeetingDayOfWeek(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00066EB0 File Offset: 0x000650B0
		internal static object MeetingOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00066EBE File Offset: 0x000650BE
		internal static object MeetingRequest(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00066ECC File Offset: 0x000650CC
		internal static CultureInfo MessageLanguage(ActivityManager manager, string variableName)
		{
			return (CultureInfo)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00066EDF File Offset: 0x000650DF
		internal static object More(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00066EED File Offset: 0x000650ED
		internal static object NamesGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00066EFB File Offset: 0x000650FB
		internal static string NormalizedSubject(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00066F0E File Offset: 0x0006510E
		internal static int NumMessagesFromName(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00066F26 File Offset: 0x00065126
		internal static object NumRecipients(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00066F34 File Offset: 0x00065134
		internal static object Owner(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00066F42 File Offset: 0x00065142
		internal static object PlayedUndelete(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00066F50 File Offset: 0x00065150
		internal static object Read(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00066F5E File Offset: 0x0006515E
		internal static object ReceivedDayOfWeek(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00066F6C File Offset: 0x0006516C
		internal static object ReceivedOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00066F7A File Offset: 0x0006517A
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00066F8D File Offset: 0x0006518D
		internal static object RecordingFailureCount(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00066F9B File Offset: 0x0006519B
		internal static object RecordingTimedOut(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00066FA9 File Offset: 0x000651A9
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00066FB7 File Offset: 0x000651B7
		internal static object ReplyAllIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00066FC5 File Offset: 0x000651C5
		internal static object ReplyIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00066FD3 File Offset: 0x000651D3
		internal static List<CultureInfo> SelectableLanguages(ActivityManager manager, string variableName)
		{
			return (List<CultureInfo>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00066FE6 File Offset: 0x000651E6
		internal static PhoneNumber SenderCallerID(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00066FF9 File Offset: 0x000651F9
		internal static object UndeletedAConversation(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00067007 File Offset: 0x00065207
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00067018 File Offset: 0x00065218
		internal static NameOrNumberOfCaller SpecifiedCallerDetails(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.SpecifiedCallerDetails;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00067040 File Offset: 0x00065240
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.TargetPhoneNumber;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00067068 File Offset: 0x00065268
		internal static object MessageListIsNull(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.MessageListIsNull;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00067094 File Offset: 0x00065294
		internal static object IsRecurringMeetingRequest(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.IsRecurringMeetingRequest;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000670C0 File Offset: 0x000652C0
		internal static object IsSenderRoutable(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.IsSenderRoutable;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000670EC File Offset: 0x000652EC
		internal static object MessageHasBeenSentWithHighImportance(ActivityManager manager, string variableName)
		{
			EmailManager emailManager = manager as EmailManager;
			if (emailManager == null)
			{
				EmailManager.GetScope(manager, out emailManager);
			}
			return emailManager.MessageHasBeenSentWithHighImportance;
		}
	}
}
