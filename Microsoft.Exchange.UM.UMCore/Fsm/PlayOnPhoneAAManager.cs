using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000316 RID: 790
	internal class PlayOnPhoneAAManager
	{
		// Token: 0x06001A77 RID: 6775 RVA: 0x00068DAD File Offset: 0x00066FAD
		internal static void GetScope(ActivityManager manager, out PlayOnPhoneAAManager scope)
		{
			for (scope = (manager as PlayOnPhoneAAManager); scope == null; scope = (manager as PlayOnPhoneAAManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00068DE0 File Offset: 0x00066FE0
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x00068DEA File Offset: 0x00066FEA
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00068DF4 File Offset: 0x00066FF4
		internal static TransitionBase SetOperationResultFailed(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return manager.GetTransition(playOnPhoneAAManager.SetOperationResultFailed(vo));
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00068E20 File Offset: 0x00067020
		internal static TransitionBase ExistingGreetingAlreadyPlayed(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return manager.GetTransition(playOnPhoneAAManager.ExistingGreetingAlreadyPlayed(vo));
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00068E4C File Offset: 0x0006704C
		internal static TransitionBase SaveGreeting(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return manager.GetTransition(playOnPhoneAAManager.SaveGreeting(vo));
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00068E78 File Offset: 0x00067078
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00068E8C File Offset: 0x0006708C
		internal static string ExistingFilePath(ActivityManager manager, string variableName)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return playOnPhoneAAManager.ExistingFilePath;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00068EB4 File Offset: 0x000670B4
		internal static object FileExists(ActivityManager manager, string variableName)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return playOnPhoneAAManager.FileExists;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00068EE0 File Offset: 0x000670E0
		internal static object PlayingExistingGreetingFirstTime(ActivityManager manager, string variableName)
		{
			PlayOnPhoneAAManager playOnPhoneAAManager = manager as PlayOnPhoneAAManager;
			if (playOnPhoneAAManager == null)
			{
				PlayOnPhoneAAManager.GetScope(manager, out playOnPhoneAAManager);
			}
			return playOnPhoneAAManager.PlayingExistingGreetingFirstTime;
		}
	}
}
