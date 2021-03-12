using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000318 RID: 792
	internal class PlayOnPhonePAAManager
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x0006905A File Offset: 0x0006725A
		internal static void GetScope(ActivityManager manager, out PlayOnPhonePAAManager scope)
		{
			for (scope = (manager as PlayOnPhonePAAManager); scope == null; scope = (manager as PlayOnPhonePAAManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0006908D File Offset: 0x0006728D
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00069098 File Offset: 0x00067298
		internal static TransitionBase GetAutoAttendant(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.GetAutoAttendant(vo));
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000690C4 File Offset: 0x000672C4
		internal static TransitionBase PrepareToExecutePAA(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.PrepareToExecutePAA(vo));
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000690F0 File Offset: 0x000672F0
		internal static TransitionBase GetGreeting(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.GetGreeting(vo));
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x0006911C File Offset: 0x0006731C
		internal static TransitionBase DeleteGreeting(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.DeleteGreeting(vo));
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00069148 File Offset: 0x00067348
		internal static TransitionBase ClearRecording(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.ClearRecording(vo));
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x00069174 File Offset: 0x00067374
		internal static TransitionBase SaveGreeting(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return manager.GetTransition(playOnPhonePAAManager.SaveGreeting(vo));
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000691A0 File Offset: 0x000673A0
		internal static object RecordedName(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.RecordedName;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000691C8 File Offset: 0x000673C8
		internal static string Context1(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context1;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000691F0 File Offset: 0x000673F0
		internal static object TargetName1(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName1;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00069218 File Offset: 0x00067418
		internal static PhoneNumber TargetPhone1(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone1;
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00069240 File Offset: 0x00067440
		internal static string Context2(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context2;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00069268 File Offset: 0x00067468
		internal static object TargetName2(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName2;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00069290 File Offset: 0x00067490
		internal static PhoneNumber TargetPhone2(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone2;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000692B8 File Offset: 0x000674B8
		internal static string Context3(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context3;
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000692E0 File Offset: 0x000674E0
		internal static object TargetName3(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName3;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00069308 File Offset: 0x00067508
		internal static PhoneNumber TargetPhone3(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone3;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00069330 File Offset: 0x00067530
		internal static string Context4(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context4;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00069358 File Offset: 0x00067558
		internal static object TargetName4(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName4;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x00069380 File Offset: 0x00067580
		internal static PhoneNumber TargetPhone4(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone4;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x000693A8 File Offset: 0x000675A8
		internal static string Context5(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context5;
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x000693D0 File Offset: 0x000675D0
		internal static object TargetName5(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName5;
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000693F8 File Offset: 0x000675F8
		internal static PhoneNumber TargetPhone5(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone5;
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00069420 File Offset: 0x00067620
		internal static string Context6(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context6;
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x00069448 File Offset: 0x00067648
		internal static object TargetName6(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName6;
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00069470 File Offset: 0x00067670
		internal static PhoneNumber TargetPhone6(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone6;
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x00069498 File Offset: 0x00067698
		internal static string Context7(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context7;
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000694C0 File Offset: 0x000676C0
		internal static object TargetName7(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName7;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x000694E8 File Offset: 0x000676E8
		internal static PhoneNumber TargetPhone7(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone7;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x00069510 File Offset: 0x00067710
		internal static string Context8(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context8;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x00069538 File Offset: 0x00067738
		internal static object TargetName8(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName8;
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x00069560 File Offset: 0x00067760
		internal static PhoneNumber TargetPhone8(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone8;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00069588 File Offset: 0x00067788
		internal static string Context9(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Context9;
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000695B0 File Offset: 0x000677B0
		internal static object TargetName9(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetName9;
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000695D8 File Offset: 0x000677D8
		internal static PhoneNumber TargetPhone9(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TargetPhone9;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00069600 File Offset: 0x00067800
		internal static ITempWavFile PersonalGreeting(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.PersonalGreeting;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00069628 File Offset: 0x00067828
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Recording;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00069650 File Offset: 0x00067850
		internal static object ValidPAA(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.ValidPAA;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0006967C File Offset: 0x0006787C
		internal static object HaveGreeting(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.HaveGreeting;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000696A8 File Offset: 0x000678A8
		internal static object HaveActions(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.HaveActions;
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000696D4 File Offset: 0x000678D4
		internal static object Key1Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key1Enabled;
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00069700 File Offset: 0x00067900
		internal static object MenuType1(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType1;
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00069728 File Offset: 0x00067928
		internal static object Key2Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key2Enabled;
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00069754 File Offset: 0x00067954
		internal static object MenuType2(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType2;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x0006977C File Offset: 0x0006797C
		internal static object Key3Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key3Enabled;
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000697A8 File Offset: 0x000679A8
		internal static object MenuType3(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType3;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000697D0 File Offset: 0x000679D0
		internal static object Key4Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key4Enabled;
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000697FC File Offset: 0x000679FC
		internal static object MenuType4(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType4;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00069824 File Offset: 0x00067A24
		internal static object Key5Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key5Enabled;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00069850 File Offset: 0x00067A50
		internal static object MenuType5(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType5;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00069878 File Offset: 0x00067A78
		internal static object Key6Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key6Enabled;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000698A4 File Offset: 0x00067AA4
		internal static object MenuType6(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType6;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000698CC File Offset: 0x00067ACC
		internal static object Key7Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key7Enabled;
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000698F8 File Offset: 0x00067AF8
		internal static object MenuType7(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType7;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x00069920 File Offset: 0x00067B20
		internal static object Key8Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key8Enabled;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0006994C File Offset: 0x00067B4C
		internal static object MenuType8(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType8;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00069974 File Offset: 0x00067B74
		internal static object Key9Enabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.Key9Enabled;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000699A0 File Offset: 0x00067BA0
		internal static object MenuType9(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.MenuType9;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000699C8 File Offset: 0x00067BC8
		internal static object TransferToVoiceMessageEnabled(ActivityManager manager, string variableName)
		{
			PlayOnPhonePAAManager playOnPhonePAAManager = manager as PlayOnPhonePAAManager;
			if (playOnPhonePAAManager == null)
			{
				PlayOnPhonePAAManager.GetScope(manager, out playOnPhonePAAManager);
			}
			return playOnPhonePAAManager.TransferToVoiceMessageEnabled;
		}
	}
}
