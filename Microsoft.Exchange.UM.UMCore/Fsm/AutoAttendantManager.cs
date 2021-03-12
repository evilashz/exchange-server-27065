using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030B RID: 779
	internal class AutoAttendantManager
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x00065C22 File Offset: 0x00063E22
		internal static void GetScope(ActivityManager manager, out AutoAttendantManager scope)
		{
			for (scope = (manager as AutoAttendantManager); scope == null; scope = (manager as AutoAttendantManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00065C55 File Offset: 0x00063E55
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00065C5F File Offset: 0x00063E5F
		internal static TransitionBase HandleFaxTone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00065C69 File Offset: 0x00063E69
		internal static TransitionBase PrepareForTransferToSendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00065C73 File Offset: 0x00063E73
		internal static TransitionBase PrepareForProtectedSubscriberOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00065C7D File Offset: 0x00063E7D
		internal static TransitionBase PrepareForTransferToPaa(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00065C87 File Offset: 0x00063E87
		internal static TransitionBase TransferToPAASiteFailed(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00065C91 File Offset: 0x00063E91
		internal static TransitionBase PrepareForTransferToKeyMappingAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00065C9B File Offset: 0x00063E9B
		internal static TransitionBase PrepareForUserInitiatedOperatorTransferFromOpeningMenu(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00065CA5 File Offset: 0x00063EA5
		internal static TransitionBase PrepareForUserInitiatedOperatorTransfer(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00065CAF File Offset: 0x00063EAF
		internal static TransitionBase ProcessCustomMenuSelection(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00065CB9 File Offset: 0x00063EB9
		internal static TransitionBase ProcessResult(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00065CC3 File Offset: 0x00063EC3
		internal static TransitionBase QuickMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00065CCD File Offset: 0x00063ECD
		internal static TransitionBase SetCustomExtensionNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00065CD7 File Offset: 0x00063ED7
		internal static TransitionBase SetCustomMenuAutoAttendant(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00065CE1 File Offset: 0x00063EE1
		internal static TransitionBase SetCustomMenuVoicemailTarget(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00065CEB File Offset: 0x00063EEB
		internal static TransitionBase SetCustomMenuTargetPAA(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00065CF5 File Offset: 0x00063EF5
		internal static TransitionBase SetExtensionNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00065CFF File Offset: 0x00063EFF
		internal static TransitionBase SetOperatorNumber(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00065D09 File Offset: 0x00063F09
		internal static TransitionBase SetPromptProvContext(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00065D14 File Offset: 0x00063F14
		internal static TransitionBase PrepareForCallAnswering(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return manager.GetTransition(autoAttendantManager.PrepareForCallAnswering(vo));
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00065D40 File Offset: 0x00063F40
		internal static TransitionBase CheckNonUmExtension(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return manager.GetTransition(autoAttendantManager.CheckNonUmExtension(vo));
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00065D6C File Offset: 0x00063F6C
		internal static object Aa_contactSomeoneEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00065D7A File Offset: 0x00063F7A
		internal static object Aa_customizedMenuEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00065D88 File Offset: 0x00063F88
		internal static object Aa_isBusinessHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00065D96 File Offset: 0x00063F96
		internal static object Aa_transferToOperatorEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00065DA4 File Offset: 0x00063FA4
		internal static object AllowCall(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00065DB2 File Offset: 0x00063FB2
		internal static object AllowMessage(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00065DC0 File Offset: 0x00063FC0
		internal static string NameOfDepartment1(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00065DD3 File Offset: 0x00063FD3
		internal static string NameOfDepartment2(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00065DE6 File Offset: 0x00063FE6
		internal static string NameOfDepartment3(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00065DF9 File Offset: 0x00063FF9
		internal static string NameOfDepartment4(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00065E0C File Offset: 0x0006400C
		internal static string NameOfDepartment5(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00065E1F File Offset: 0x0006401F
		internal static string NameOfDepartment6(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00065E32 File Offset: 0x00064032
		internal static string NameOfDepartment7(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00065E45 File Offset: 0x00064045
		internal static string NameOfDepartment8(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00065E58 File Offset: 0x00064058
		internal static string NameOfDepartment9(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00065E6B File Offset: 0x0006406B
		internal static string NameOfDepartmentTimeOut(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00065E7E File Offset: 0x0006407E
		internal static string CustomMenuOptionPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00065E91 File Offset: 0x00064091
		internal static object CustomMenuOption(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00065E9F File Offset: 0x0006409F
		internal static object DirectorySearchEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00065EAD File Offset: 0x000640AD
		internal static object DtmfKey1(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00065EBB File Offset: 0x000640BB
		internal static object DtmfKey2(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00065EC9 File Offset: 0x000640C9
		internal static object DtmfKey3(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00065ED7 File Offset: 0x000640D7
		internal static object DtmfKey4(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00065EE5 File Offset: 0x000640E5
		internal static object DtmfKey5(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00065EF3 File Offset: 0x000640F3
		internal static object DtmfKey6(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00065F01 File Offset: 0x00064101
		internal static object DtmfKey7(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00065F0F File Offset: 0x0006410F
		internal static object DtmfKey8(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00065F1D File Offset: 0x0006411D
		internal static object DtmfKey9(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00065F2B File Offset: 0x0006412B
		internal static object HaveCustomMenuOptionPrompt(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00065F39 File Offset: 0x00064139
		internal static object HolidayHours(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00065F47 File Offset: 0x00064147
		internal static string HolidayIntroductoryGreetingPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00065F5A File Offset: 0x0006415A
		internal static object InfoAnnouncementEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00065F68 File Offset: 0x00064168
		internal static string InfoAnnouncementFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00065F7B File Offset: 0x0006417B
		internal static string InvalidExtension(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00065F8E File Offset: 0x0006418E
		internal static object MainMenuCustomPromptEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00065F9C File Offset: 0x0006419C
		internal static string MainMenuCustomPromptFilename(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00065FAF File Offset: 0x000641AF
		internal static object TextPart(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00065FBD File Offset: 0x000641BD
		internal static object TimeoutOption(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00065FCB File Offset: 0x000641CB
		internal static object TuiPromptEditingEnabled(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00065FD9 File Offset: 0x000641D9
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00065FE8 File Offset: 0x000641E8
		internal static AutoAttendantContext AAContext(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.AAContext;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00066010 File Offset: 0x00064210
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.TargetPhoneNumber;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00066038 File Offset: 0x00064238
		internal static AutoAttendantLocationContext AALocationContext(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.AALocationContext;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00066060 File Offset: 0x00064260
		internal static UMAutoAttendant ThisAutoAttendant(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.ThisAutoAttendant;
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00066088 File Offset: 0x00064288
		internal static object ForwardCallsToDefaultMailbox(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.ForwardCallsToDefaultMailbox;
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000660B4 File Offset: 0x000642B4
		internal static object StarOutToDialPlanEnabled(ActivityManager manager, string variableName)
		{
			AutoAttendantManager autoAttendantManager = manager as AutoAttendantManager;
			if (autoAttendantManager == null)
			{
				AutoAttendantManager.GetScope(manager, out autoAttendantManager);
			}
			return autoAttendantManager.StarOutToDialPlanEnabled;
		}
	}
}
