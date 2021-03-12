using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031C RID: 796
	internal class SpeechAutoAttendantManager
	{
		// Token: 0x06001B68 RID: 7016 RVA: 0x0006A526 File Offset: 0x00068726
		internal static void GetScope(ActivityManager manager, out SpeechAutoAttendantManager scope)
		{
			for (scope = (manager as SpeechAutoAttendantManager); scope == null; scope = (manager as SpeechAutoAttendantManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x0006A559 File Offset: 0x00068759
		internal static TransitionBase CanonicalizeNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x0006A563 File Offset: 0x00068763
		internal static TransitionBase CheckDialPermissions(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0006A56D File Offset: 0x0006876D
		internal static TransitionBase CheckRestrictedUser(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6C RID: 7020 RVA: 0x0006A577 File Offset: 0x00068777
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x0006A581 File Offset: 0x00068781
		internal static TransitionBase InitializeNamesGrammar(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0006A58B File Offset: 0x0006878B
		internal static TransitionBase InitializeState(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0006A595 File Offset: 0x00068795
		internal static TransitionBase PrepareForProtectedSubscriberOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0006A59F File Offset: 0x0006879F
		internal static TransitionBase PrepareForTransferToSendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0006A5A9 File Offset: 0x000687A9
		internal static TransitionBase PrepareForTransferToKeyMappingAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0006A5B3 File Offset: 0x000687B3
		internal static TransitionBase PrepareForTransferToDtmfFallbackAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0006A5BD File Offset: 0x000687BD
		internal static TransitionBase PrepareForTransferToPaa(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0006A5C7 File Offset: 0x000687C7
		internal static TransitionBase TransferToPAASiteFailed(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0006A5D1 File Offset: 0x000687D1
		internal static TransitionBase ProcessResult(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0006A5DB File Offset: 0x000687DB
		internal static TransitionBase QuickMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0006A5E5 File Offset: 0x000687E5
		internal static TransitionBase RetryAsrSearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0006A5EF File Offset: 0x000687EF
		internal static TransitionBase SetCustomExtensionNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0006A5F9 File Offset: 0x000687F9
		internal static TransitionBase SetCustomMenuVoicemailTarget(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0006A603 File Offset: 0x00068803
		internal static TransitionBase SetCustomMenuTargetPAA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0006A60D File Offset: 0x0006880D
		internal static TransitionBase SetCustomMenuAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0006A617 File Offset: 0x00068817
		internal static TransitionBase SetFallbackAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0006A621 File Offset: 0x00068821
		internal static TransitionBase SetOperatorNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0006A62C File Offset: 0x0006882C
		internal static TransitionBase PrepareForCallAnswering(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return manager.GetTransition(speechAutoAttendantManager.PrepareForCallAnswering(vo));
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0006A658 File Offset: 0x00068858
		internal static TransitionBase EnableMainMenuRepetition(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return manager.GetTransition(speechAutoAttendantManager.EnableMainMenuRepetition(vo));
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0006A684 File Offset: 0x00068884
		internal static TransitionBase DisableMainMenuRepetition(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return manager.GetTransition(speechAutoAttendantManager.DisableMainMenuRepetition(vo));
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0006A6B0 File Offset: 0x000688B0
		internal static object Aa_customizedMenuEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0006A6BE File Offset: 0x000688BE
		internal static object Aa_goto_dtmf_autoattendant(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0006A6CC File Offset: 0x000688CC
		internal static object Aa_goto_operator(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0006A6DA File Offset: 0x000688DA
		internal static object Aa_isBusinessHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x0006A6E8 File Offset: 0x000688E8
		internal static object Aa_transferToOperatorEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x0006A6F6 File Offset: 0x000688F6
		internal static string CustomMenuOptionPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0006A709 File Offset: 0x00068909
		internal static object CustomMenuOption(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x0006A717 File Offset: 0x00068917
		internal static object HaveCustomMenuOptionPrompt(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0006A725 File Offset: 0x00068925
		internal static object HolidayHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0006A733 File Offset: 0x00068933
		internal static string HolidayIntroductoryGreetingPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0006A746 File Offset: 0x00068946
		internal static object InfoAnnouncementEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0006A754 File Offset: 0x00068954
		internal static string InfoAnnouncementFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x0006A767 File Offset: 0x00068967
		internal static object ResultTypeString(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x0006A775 File Offset: 0x00068975
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x0006A784 File Offset: 0x00068984
		internal static AutoAttendantContext AAContext(ActivityManager manager, string variableName)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return speechAutoAttendantManager.AAContext;
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0006A7AC File Offset: 0x000689AC
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return speechAutoAttendantManager.TargetPhoneNumber;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x0006A7D4 File Offset: 0x000689D4
		internal static AutoAttendantLocationContext AALocationContext(ActivityManager manager, string variableName)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return speechAutoAttendantManager.AALocationContext;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x0006A7FC File Offset: 0x000689FC
		internal static UMAutoAttendant ThisAutoAttendant(ActivityManager manager, string variableName)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return speechAutoAttendantManager.ThisAutoAttendant;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0006A824 File Offset: 0x00068A24
		internal static object ForwardCallsToDefaultMailbox(ActivityManager manager, string variableName)
		{
			SpeechAutoAttendantManager speechAutoAttendantManager = manager as SpeechAutoAttendantManager;
			if (speechAutoAttendantManager == null)
			{
				SpeechAutoAttendantManager.GetScope(manager, out speechAutoAttendantManager);
			}
			return speechAutoAttendantManager.ForwardCallsToDefaultMailbox;
		}
	}
}
