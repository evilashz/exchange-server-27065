using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031B RID: 795
	internal class RetrieveVoicemailManager
	{
		// Token: 0x06001B16 RID: 6934 RVA: 0x00069ED6 File Offset: 0x000680D6
		internal static void GetScope(ActivityManager manager, out RetrieveVoicemailManager scope)
		{
			for (scope = (manager as RetrieveVoicemailManager); scope == null; scope = (manager as RetrieveVoicemailManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00069F09 File Offset: 0x00068109
		internal static TransitionBase AddRecipientBySearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00069F13 File Offset: 0x00068113
		internal static TransitionBase AppendRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00069F1D File Offset: 0x0006811D
		internal static TransitionBase CancelMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00069F27 File Offset: 0x00068127
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00069F31 File Offset: 0x00068131
		internal static TransitionBase DeleteVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00069F3B File Offset: 0x0006813B
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00069F45 File Offset: 0x00068145
		internal static TransitionBase FindByName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00069F4F File Offset: 0x0006814F
		internal static TransitionBase FlagVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x00069F59 File Offset: 0x00068159
		internal static TransitionBase ForwardVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00069F63 File Offset: 0x00068163
		internal static TransitionBase GetEnvelopInfo(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00069F6D File Offset: 0x0006816D
		internal static TransitionBase GetMessageReadProperty(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x00069F77 File Offset: 0x00068177
		internal static TransitionBase GetNewMessages(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x00069F81 File Offset: 0x00068181
		internal static TransitionBase GetNextMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00069F8B File Offset: 0x0006818B
		internal static TransitionBase GetPreviousMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00069F95 File Offset: 0x00068195
		internal static TransitionBase GetSavedMessages(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00069F9F File Offset: 0x0006819F
		internal static TransitionBase MarkUnreadVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00069FA9 File Offset: 0x000681A9
		internal static TransitionBase More(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00069FB3 File Offset: 0x000681B3
		internal static TransitionBase Pause(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00069FBD File Offset: 0x000681BD
		internal static TransitionBase RemoveRecipient(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00069FC7 File Offset: 0x000681C7
		internal static TransitionBase ReplyVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00069FD1 File Offset: 0x000681D1
		internal static TransitionBase ResetPlayback(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00069FDB File Offset: 0x000681DB
		internal static TransitionBase SaveVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00069FE5 File Offset: 0x000681E5
		internal static TransitionBase SendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00069FEF File Offset: 0x000681EF
		internal static TransitionBase SendMessageUrgent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00069FF9 File Offset: 0x000681F9
		internal static TransitionBase SlowDown(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0006A003 File Offset: 0x00068203
		internal static TransitionBase SpeedUp(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0006A00D File Offset: 0x0006820D
		internal static TransitionBase UndeleteVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x0006A018 File Offset: 0x00068218
		internal static TransitionBase ReplyAll(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.ReplyAll(vo));
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x0006A044 File Offset: 0x00068244
		internal static TransitionBase ToggleImportance(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.ToggleImportance(vo));
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x0006A070 File Offset: 0x00068270
		internal static TransitionBase TogglePrivacy(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.TogglePrivacy(vo));
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x0006A09C File Offset: 0x0006829C
		internal static TransitionBase ClearSelection(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.ClearSelection(vo));
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x0006A0C8 File Offset: 0x000682C8
		internal static TransitionBase Repeat(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.Repeat(vo));
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0006A0F4 File Offset: 0x000682F4
		internal static TransitionBase SendMessagePrivate(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.SendMessagePrivate(vo));
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0006A120 File Offset: 0x00068320
		internal static TransitionBase SendMessagePrivateAndUrgent(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return manager.GetTransition(retrieveVoicemailManager.SendMessagePrivateAndUrgent(vo));
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006A14C File Offset: 0x0006834C
		internal static object CanUndelete(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0006A15A File Offset: 0x0006835A
		internal static object DeclineIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0006A168 File Offset: 0x00068368
		internal static int DurationMinutes(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0006A180 File Offset: 0x00068380
		internal static int DurationSeconds(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x0006A198 File Offset: 0x00068398
		internal static object EmailSender(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0006A1A6 File Offset: 0x000683A6
		internal static object FindByName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0006A1B4 File Offset: 0x000683B4
		internal static object FirstMessage(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x0006A1C2 File Offset: 0x000683C2
		internal static object ForwardIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006A1D0 File Offset: 0x000683D0
		internal static object IsForward(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0006A1DE File Offset: 0x000683DE
		internal static object IsHighPriority(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0006A1EC File Offset: 0x000683EC
		internal static object IsProtected(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0006A1FA File Offset: 0x000683FA
		internal static object IsReply(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0006A208 File Offset: 0x00068408
		internal static object KnowSenderPhoneNumber(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0006A216 File Offset: 0x00068416
		internal static object KnowVoicemailSender(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x0006A224 File Offset: 0x00068424
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x0006A232 File Offset: 0x00068432
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006A240 File Offset: 0x00068440
		internal static ExDateTime MessageReceivedTime(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006A26B File Offset: 0x0006846B
		internal static object More(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006A279 File Offset: 0x00068479
		internal static object NamesGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006A287 File Offset: 0x00068487
		internal static int NumMessagesFromName(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006A29F File Offset: 0x0006849F
		internal static object NumRecipients(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006A2AD File Offset: 0x000684AD
		internal static object OcFeature(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0006A2BB File Offset: 0x000684BB
		internal static object PlayedUndelete(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006A2C9 File Offset: 0x000684C9
		internal static object Read(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0006A2D7 File Offset: 0x000684D7
		internal static object ReceivedDayOfWeek(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006A2E5 File Offset: 0x000684E5
		internal static object ReceivedOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0006A2F3 File Offset: 0x000684F3
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006A306 File Offset: 0x00068506
		internal static object RecordingFailureCount(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006A314 File Offset: 0x00068514
		internal static object RecordingTimedOut(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0006A322 File Offset: 0x00068522
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0006A330 File Offset: 0x00068530
		internal static object ReplyAllIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0006A33E File Offset: 0x0006853E
		internal static object ReplyIntro(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0006A34C File Offset: 0x0006854C
		internal static PhoneNumber SenderCallerID(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0006A35F File Offset: 0x0006855F
		internal static string SenderInfo(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0006A372 File Offset: 0x00068572
		internal static object Urgent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x0006A380 File Offset: 0x00068580
		internal static object Protected(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0006A38E File Offset: 0x0006858E
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0006A39C File Offset: 0x0006859C
		internal static NameOrNumberOfCaller SpecifiedCallerDetails(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.SpecifiedCallerDetails;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0006A3C4 File Offset: 0x000685C4
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.TargetPhoneNumber;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0006A3EC File Offset: 0x000685EC
		internal static object MessageListIsNull(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.MessageListIsNull;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0006A418 File Offset: 0x00068618
		internal static object IsForwardEnabled(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.IsForwardEnabled;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x0006A444 File Offset: 0x00068644
		internal static object DrmIsEnabled(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.DrmIsEnabled;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x0006A470 File Offset: 0x00068670
		internal static object IsSentImportant(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.IsSentImportant;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0006A49C File Offset: 0x0006869C
		internal static object MessageMarkedPrivate(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.MessageMarkedPrivate;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0006A4C8 File Offset: 0x000686C8
		internal static object IsFindByContactEnabled(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.IsFindByContactEnabled;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x0006A4F4 File Offset: 0x000686F4
		internal static object IsForwardToContactEnabled(ActivityManager manager, string variableName)
		{
			RetrieveVoicemailManager retrieveVoicemailManager = manager as RetrieveVoicemailManager;
			if (retrieveVoicemailManager == null)
			{
				RetrieveVoicemailManager.GetScope(manager, out retrieveVoicemailManager);
			}
			return retrieveVoicemailManager.IsForwardToContactEnabled;
		}
	}
}
