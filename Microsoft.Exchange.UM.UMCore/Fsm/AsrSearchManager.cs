using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030A RID: 778
	internal class AsrSearchManager
	{
		// Token: 0x060017B3 RID: 6067 RVA: 0x00065742 File Offset: 0x00063942
		internal static void GetScope(ActivityManager manager, out AsrSearchManager scope)
		{
			for (scope = (manager as AsrSearchManager); scope == null; scope = (manager as AsrSearchManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00065775 File Offset: 0x00063975
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0006577F File Offset: 0x0006397F
		internal static TransitionBase HandleChoice(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00065789 File Offset: 0x00063989
		internal static TransitionBase HandleDtmfChoice(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00065793 File Offset: 0x00063993
		internal static TransitionBase HandleFaxTone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0006579D File Offset: 0x0006399D
		internal static TransitionBase HandleNo(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000657A7 File Offset: 0x000639A7
		internal static TransitionBase HandleNotListed(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x000657B1 File Offset: 0x000639B1
		internal static TransitionBase HandleRecognition(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x000657BB File Offset: 0x000639BB
		internal static TransitionBase HandleYes(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000657C5 File Offset: 0x000639C5
		internal static TransitionBase InitAskAgainQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000657CF File Offset: 0x000639CF
		internal static TransitionBase InitConfirmAgainQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x000657D9 File Offset: 0x000639D9
		internal static TransitionBase InitConfirmQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000657E3 File Offset: 0x000639E3
		internal static TransitionBase InitConfirmViaListQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000657ED File Offset: 0x000639ED
		internal static TransitionBase InitNameCollisionQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000657F7 File Offset: 0x000639F7
		internal static TransitionBase InitPromptForAliasConfirmQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00065801 File Offset: 0x00063A01
		internal static TransitionBase InitPromptForAliasQA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0006580B File Offset: 0x00063A0B
		internal static TransitionBase PrepareForANROperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00065815 File Offset: 0x00063A15
		internal static TransitionBase PrepareForUserInitiatedOperatorTransferFromOpeningMenu(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0006581F File Offset: 0x00063A1F
		internal static TransitionBase PrepareForUserInitiatedOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00065829 File Offset: 0x00063A29
		internal static TransitionBase ProcessCustomMenuSelection(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00065833 File Offset: 0x00063A33
		internal static TransitionBase ResetSearchState(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0006583D File Offset: 0x00063A3D
		internal static TransitionBase SetExtensionNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00065847 File Offset: 0x00063A47
		internal static TransitionBase SetInitialSearchTargetContacts(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00065851 File Offset: 0x00063A51
		internal static TransitionBase SetPromptProvContext(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0006585B File Offset: 0x00063A5B
		internal static object Aa_contactSomeoneEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00065869 File Offset: 0x00063A69
		internal static object Aa_customizedMenuEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00065877 File Offset: 0x00063A77
		internal static object Aa_dtmfFallbackEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00065885 File Offset: 0x00063A85
		internal static object Aa_goto_dtmf_autoattendant(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00065893 File Offset: 0x00063A93
		internal static object Aa_goto_operator(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x000658A1 File Offset: 0x00063AA1
		internal static object Aa_isBusinessHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x000658AF File Offset: 0x00063AAF
		internal static object Aa_transferToOperatorEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x000658BD File Offset: 0x00063ABD
		internal static object Aa_welcomeGreetingEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x000658CB File Offset: 0x00063ACB
		internal static object Contacts_nameLookupEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x000658D9 File Offset: 0x00063AD9
		internal static string DepartmentName(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x000658EC File Offset: 0x00063AEC
		internal static object DistributionListGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000658FA File Offset: 0x00063AFA
		internal static object DtmfKey1(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00065908 File Offset: 0x00063B08
		internal static object DtmfKey2(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00065916 File Offset: 0x00063B16
		internal static object DtmfKey3(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00065924 File Offset: 0x00063B24
		internal static object DtmfKey4(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00065932 File Offset: 0x00063B32
		internal static object DtmfKey5(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00065940 File Offset: 0x00063B40
		internal static object DtmfKey6(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0006594E File Offset: 0x00063B4E
		internal static object DtmfKey7(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0006595C File Offset: 0x00063B5C
		internal static object DtmfKey8(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0006596A File Offset: 0x00063B6A
		internal static object DtmfKey9(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00065978 File Offset: 0x00063B78
		internal static object EmailAliasGrammar(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00065986 File Offset: 0x00063B86
		internal static string FirstDepartment(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00065999 File Offset: 0x00063B99
		internal static object HaveNameRecording(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000659A7 File Offset: 0x00063BA7
		internal static object HolidayHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000659B5 File Offset: 0x00063BB5
		internal static string HolidayIntroductoryGreetingPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000659C8 File Offset: 0x00063BC8
		internal static object InfoAnnouncementEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000659D6 File Offset: 0x00063BD6
		internal static string InfoAnnouncementFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000659E9 File Offset: 0x00063BE9
		internal static object InitialSearchTarget(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000659F7 File Offset: 0x00063BF7
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00065A05 File Offset: 0x00063C05
		internal static object LastRecoEvent(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00065A13 File Offset: 0x00063C13
		internal static object MainMenuCustomPromptEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00065A21 File Offset: 0x00063C21
		internal static string MainMenuCustomPromptFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00065A34 File Offset: 0x00063C34
		internal static object Mode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00065A42 File Offset: 0x00063C42
		internal static object NamesOnly(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00065A50 File Offset: 0x00063C50
		internal static int NumUsers(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00065A68 File Offset: 0x00063C68
		internal static object RecordedNamesOnly(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00065A76 File Offset: 0x00063C76
		internal static object Repeat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00065A84 File Offset: 0x00063C84
		internal static object ResultType(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00065A92 File Offset: 0x00063C92
		internal static object RetryAsrSearch(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00065AA0 File Offset: 0x00063CA0
		internal static object TimeoutOption(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00065AAE File Offset: 0x00063CAE
		internal static List<string> SelectableDepartments(ActivityManager manager, string variableName)
		{
			return (List<string>)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00065AC1 File Offset: 0x00063CC1
		internal static string NameOfDepartmentTimeOut(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00065AD4 File Offset: 0x00063CD4
		internal static object TuiPromptEditingEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00065AE2 File Offset: 0x00063CE2
		internal static object User1(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00065AF0 File Offset: 0x00063CF0
		internal static object User2(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00065AFE File Offset: 0x00063CFE
		internal static object User3(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00065B0C File Offset: 0x00063D0C
		internal static object User4(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00065B1A File Offset: 0x00063D1A
		internal static object User5(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00065B28 File Offset: 0x00063D28
		internal static object User6(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00065B36 File Offset: 0x00063D36
		internal static object User7(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00065B44 File Offset: 0x00063D44
		internal static object User8(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00065B52 File Offset: 0x00063D52
		internal static object User9(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00065B60 File Offset: 0x00063D60
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00065B70 File Offset: 0x00063D70
		internal static AutoAttendantContext AAContext(ActivityManager manager, string variableName)
		{
			AsrSearchManager asrSearchManager = manager as AsrSearchManager;
			if (asrSearchManager == null)
			{
				AsrSearchManager.GetScope(manager, out asrSearchManager);
			}
			return asrSearchManager.AAContext;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00065B98 File Offset: 0x00063D98
		internal static object RepeatMainMenu(ActivityManager manager, string variableName)
		{
			AsrSearchManager asrSearchManager = manager as AsrSearchManager;
			if (asrSearchManager == null)
			{
				AsrSearchManager.GetScope(manager, out asrSearchManager);
			}
			return asrSearchManager.RepeatMainMenu;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00065BC4 File Offset: 0x00063DC4
		internal static object MaxPersonalContactsExceeded(ActivityManager manager, string variableName)
		{
			AsrSearchManager asrSearchManager = manager as AsrSearchManager;
			if (asrSearchManager == null)
			{
				AsrSearchManager.GetScope(manager, out asrSearchManager);
			}
			return asrSearchManager.MaxPersonalContactsExceeded;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00065BF0 File Offset: 0x00063DF0
		internal static object StarOutToDialPlanEnabled(ActivityManager manager, string variableName)
		{
			AsrSearchManager asrSearchManager = manager as AsrSearchManager;
			if (asrSearchManager == null)
			{
				AsrSearchManager.GetScope(manager, out asrSearchManager);
			}
			return asrSearchManager.StarOutToDialPlanEnabled;
		}
	}
}
