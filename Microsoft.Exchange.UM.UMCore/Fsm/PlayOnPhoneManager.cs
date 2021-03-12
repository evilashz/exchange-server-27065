using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000317 RID: 791
	internal class PlayOnPhoneManager
	{
		// Token: 0x06001A82 RID: 6786 RVA: 0x00068F12 File Offset: 0x00067112
		internal static void GetScope(ActivityManager manager, out PlayOnPhoneManager scope)
		{
			for (scope = (manager as PlayOnPhoneManager); scope == null; scope = (manager as PlayOnPhoneManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x00068F45 File Offset: 0x00067145
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00068F4F File Offset: 0x0006714F
		internal static TransitionBase DeleteExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00068F59 File Offset: 0x00067159
		internal static TransitionBase DeleteOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00068F63 File Offset: 0x00067163
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00068F6D File Offset: 0x0006716D
		internal static TransitionBase GetExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00068F77 File Offset: 0x00067177
		internal static TransitionBase GetOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00068F81 File Offset: 0x00067181
		internal static TransitionBase GetPlayOnPhoneType(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00068F8B File Offset: 0x0006718B
		internal static TransitionBase ResetCallType(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00068F95 File Offset: 0x00067195
		internal static TransitionBase SaveExternal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00068F9F File Offset: 0x0006719F
		internal static TransitionBase SaveOof(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00068FAC File Offset: 0x000671AC
		internal static TransitionBase SetOperationResultFailed(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			PlayOnPhoneManager playOnPhoneManager = manager as PlayOnPhoneManager;
			if (playOnPhoneManager == null)
			{
				PlayOnPhoneManager.GetScope(manager, out playOnPhoneManager);
			}
			return manager.GetTransition(playOnPhoneManager.SetOperationResultFailed(vo));
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00068FD8 File Offset: 0x000671D8
		internal static ITempWavFile Greeting(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00068FEB File Offset: 0x000671EB
		internal static object GreetingType(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00068FF9 File Offset: 0x000671F9
		internal static object NormalCustom(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00069007 File Offset: 0x00067207
		internal static object OofCustom(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00069015 File Offset: 0x00067215
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00069028 File Offset: 0x00067228
		internal static object ProtectedMessage(ActivityManager manager, string variableName)
		{
			PlayOnPhoneManager playOnPhoneManager = manager as PlayOnPhoneManager;
			if (playOnPhoneManager == null)
			{
				PlayOnPhoneManager.GetScope(manager, out playOnPhoneManager);
			}
			return playOnPhoneManager.ProtectedMessage;
		}
	}
}
