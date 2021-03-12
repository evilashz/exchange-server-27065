using System;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000315 RID: 789
	internal class PersonalOptionsManager
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x00068B4A File Offset: 0x00066D4A
		internal static void GetScope(ActivityManager manager, out PersonalOptionsManager scope)
		{
			for (scope = (manager as PersonalOptionsManager); scope == null; scope = (manager as PersonalOptionsManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x00068B7D File Offset: 0x00066D7D
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00068B87 File Offset: 0x00066D87
		internal static TransitionBase DeleteExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00068B91 File Offset: 0x00066D91
		internal static TransitionBase DeleteName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00068B9B File Offset: 0x00066D9B
		internal static TransitionBase DeleteOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00068BA5 File Offset: 0x00066DA5
		internal static TransitionBase FindTimeZone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00068BAF File Offset: 0x00066DAF
		internal static TransitionBase FirstTimeUserComplete(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00068BB9 File Offset: 0x00066DB9
		internal static TransitionBase FirstTimeZone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00068BC3 File Offset: 0x00066DC3
		internal static TransitionBase GetExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00068BCD File Offset: 0x00066DCD
		internal static TransitionBase GetFirstTimeUserTask(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00068BD7 File Offset: 0x00066DD7
		internal static TransitionBase GetName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00068BE1 File Offset: 0x00066DE1
		internal static TransitionBase GetOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00068BEB File Offset: 0x00066DEB
		internal static TransitionBase GetSystemTask(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00068BF5 File Offset: 0x00066DF5
		internal static TransitionBase MatchPasswords(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x00068BFF File Offset: 0x00066DFF
		internal static TransitionBase NextTimeZone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00068C09 File Offset: 0x00066E09
		internal static TransitionBase SaveExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00068C13 File Offset: 0x00066E13
		internal static TransitionBase SaveName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00068C1D File Offset: 0x00066E1D
		internal static TransitionBase SaveOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00068C27 File Offset: 0x00066E27
		internal static TransitionBase SelectTimeZone(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00068C31 File Offset: 0x00066E31
		internal static TransitionBase SetGreetingsAction(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00068C3B File Offset: 0x00066E3B
		internal static TransitionBase ToggleASR(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00068C45 File Offset: 0x00066E45
		internal static TransitionBase ToggleEmailOOF(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00068C4F File Offset: 0x00066E4F
		internal static TransitionBase ToggleOOF(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00068C59 File Offset: 0x00066E59
		internal static TransitionBase ToggleTimeFormat(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00068C63 File Offset: 0x00066E63
		internal static TransitionBase ValidatePassword(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00068C6D File Offset: 0x00066E6D
		internal static int AdminMinPwdLen(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00068C85 File Offset: 0x00066E85
		internal static int AdminOldPwdLen(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00068C9D File Offset: 0x00066E9D
		internal static object CanToggleASR(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00068CAB File Offset: 0x00066EAB
		internal static object CanToggleTimeFormat(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00068CB9 File Offset: 0x00066EB9
		internal static ExTimeZone CurrentTimeZone(ActivityManager manager, string variableName)
		{
			return (ExTimeZone)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00068CCC File Offset: 0x00066ECC
		internal static object EmailOof(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00068CDA File Offset: 0x00066EDA
		internal static ITempWavFile Greeting(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00068CED File Offset: 0x00066EED
		internal static object LastAction(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00068CFB File Offset: 0x00066EFB
		internal static string LastInput(ActivityManager manager, string variableName)
		{
			return (string)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00068D0E File Offset: 0x00066F0E
		internal static int OffsetHours(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00068D26 File Offset: 0x00066F26
		internal static int OffsetMinutes(ActivityManager manager, string variableName)
		{
			return (int)(manager.ReadVariable(variableName) ?? 0);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00068D3E File Offset: 0x00066F3E
		internal static object Oof(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x00068D4C File Offset: 0x00066F4C
		internal static object PlayGMTOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00068D5A File Offset: 0x00066F5A
		internal static object PositiveOffset(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00068D68 File Offset: 0x00066F68
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00068D7B File Offset: 0x00066F7B
		internal static object TimeFormat24(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00068D89 File Offset: 0x00066F89
		internal static object TimeZoneIndex(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00068D97 File Offset: 0x00066F97
		internal static object UseAsr(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}
	}
}
