using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000319 RID: 793
	internal class PromptProvisioningManager
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x000699FA File Offset: 0x00067BFA
		internal static void GetScope(ActivityManager manager, out PromptProvisioningManager scope)
		{
			for (scope = (manager as PromptProvisioningManager); scope == null; scope = (manager as PromptProvisioningManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00069A2D File Offset: 0x00067C2D
		internal static TransitionBase CanUpdatePrompts(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00069A37 File Offset: 0x00067C37
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00069A41 File Offset: 0x00067C41
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00069A4B File Offset: 0x00067C4B
		internal static TransitionBase ExitPromptProvisioning(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00069A55 File Offset: 0x00067C55
		internal static TransitionBase NextPlaybackIndex(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00069A5F File Offset: 0x00067C5F
		internal static TransitionBase PrepareForPlayback(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x00069A69 File Offset: 0x00067C69
		internal static TransitionBase PublishPrompt(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00069A73 File Offset: 0x00067C73
		internal static TransitionBase ResetPlaybackIndex(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00069A7D File Offset: 0x00067C7D
		internal static TransitionBase SelectAfterHoursGroup(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00069A87 File Offset: 0x00067C87
		internal static TransitionBase SelectBusinessHoursGroup(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00069A91 File Offset: 0x00067C91
		internal static TransitionBase SelectHolidaySchedule(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00069A9B File Offset: 0x00067C9B
		internal static TransitionBase SelectInfoAnnouncement(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00069AA5 File Offset: 0x00067CA5
		internal static TransitionBase SelectKeyMapping(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x00069AAF File Offset: 0x00067CAF
		internal static TransitionBase SelectMainMenuCustomPrompt(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x00069AB9 File Offset: 0x00067CB9
		internal static TransitionBase SelectNextHolidayPage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00069AC3 File Offset: 0x00067CC3
		internal static TransitionBase SelectPromptIndex(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00069ACD File Offset: 0x00067CCD
		internal static TransitionBase SelectWelcomeGreeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00069AD7 File Offset: 0x00067CD7
		internal static TransitionBase SetDialPlanContext(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00069AE1 File Offset: 0x00067CE1
		internal static object HaveAfterHoursPrompts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00069AEF File Offset: 0x00067CEF
		internal static object HaveAutoAttendantPrompts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00069AFD File Offset: 0x00067CFD
		internal static object HaveBusinessHoursPrompts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00069B0B File Offset: 0x00067D0B
		internal static object HaveDialPlanPrompts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x00069B19 File Offset: 0x00067D19
		internal static object HaveHolidayPrompts(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00069B27 File Offset: 0x00067D27
		internal static object HaveInfoAnnouncement(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00069B35 File Offset: 0x00067D35
		internal static object HaveKeyMapping(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00069B43 File Offset: 0x00067D43
		internal static object HaveMainMenu(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00069B51 File Offset: 0x00067D51
		internal static object HaveWelcomeGreeting(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x00069B5F File Offset: 0x00067D5F
		internal static object HolidayCount(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x00069B70 File Offset: 0x00067D70
		internal static ExDateTime HolidayEndDate(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x00069B9B File Offset: 0x00067D9B
		internal static string HolidayName(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00069BB0 File Offset: 0x00067DB0
		internal static ExDateTime HolidayStartDate(ActivityManager manager, string variableName)
		{
			object obj;
			if ((obj = manager.ReadVariable(variableName)) == null)
			{
				obj = default(ExDateTime);
			}
			return (ExDateTime)obj;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00069BDB File Offset: 0x00067DDB
		internal static string LastInput(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00069BEE File Offset: 0x00067DEE
		internal static object MoreHolidaysAvailable(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00069BFC File Offset: 0x00067DFC
		internal static object PlaybackIndex(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x00069C0A File Offset: 0x00067E0A
		internal static object PromptProvContext(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00069C18 File Offset: 0x00067E18
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00069C2B File Offset: 0x00067E2B
		internal static string SelectedPrompt(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00069C3E File Offset: 0x00067E3E
		internal static object SelectedPromptGroup(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00069C4C File Offset: 0x00067E4C
		internal static object SelectedPromptType(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00069C5C File Offset: 0x00067E5C
		internal static object IsNullHolidayEndDate(ActivityManager manager, string variableName)
		{
			PromptProvisioningManager promptProvisioningManager = manager as PromptProvisioningManager;
			if (promptProvisioningManager == null)
			{
				PromptProvisioningManager.GetScope(manager, out promptProvisioningManager);
			}
			return promptProvisioningManager.IsNullHolidayEndDate;
		}
	}
}
