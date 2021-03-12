using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031D RID: 797
	internal class SubmitMessageManager
	{
		// Token: 0x06001B95 RID: 7061 RVA: 0x0006A856 File Offset: 0x00068A56
		internal static void GetScope(ActivityManager manager, out SubmitMessageManager scope)
		{
			for (scope = (manager as SubmitMessageManager); scope == null; scope = (manager as SubmitMessageManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x0006A889 File Offset: 0x00068A89
		internal static TransitionBase AddRecipientBySearch(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x0006A893 File Offset: 0x00068A93
		internal static TransitionBase AppendRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x0006A89D File Offset: 0x00068A9D
		internal static TransitionBase CancelMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x0006A8A7 File Offset: 0x00068AA7
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x0006A8B1 File Offset: 0x00068AB1
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0006A8BB File Offset: 0x00068ABB
		internal static TransitionBase RemoveRecipient(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0006A8C5 File Offset: 0x00068AC5
		internal static TransitionBase SendMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0006A8CF File Offset: 0x00068ACF
		internal static TransitionBase SendMessageUrgent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0006A8DC File Offset: 0x00068ADC
		internal static TransitionBase ToggleImportance(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return manager.GetTransition(submitMessageManager.ToggleImportance(vo));
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0006A908 File Offset: 0x00068B08
		internal static TransitionBase TogglePrivacy(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return manager.GetTransition(submitMessageManager.TogglePrivacy(vo));
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0006A934 File Offset: 0x00068B34
		internal static TransitionBase ClearSelection(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return manager.GetTransition(submitMessageManager.ClearSelection(vo));
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0006A960 File Offset: 0x00068B60
		internal static TransitionBase SendMessagePrivate(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return manager.GetTransition(submitMessageManager.SendMessagePrivate(vo));
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0006A98C File Offset: 0x00068B8C
		internal static TransitionBase SendMessagePrivateAndUrgent(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return manager.GetTransition(submitMessageManager.SendMessagePrivateAndUrgent(vo));
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0006A9B8 File Offset: 0x00068BB8
		internal static object LastActivity(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0006A9C6 File Offset: 0x00068BC6
		internal static object NumRecipients(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0006A9D4 File Offset: 0x00068BD4
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0006A9E7 File Offset: 0x00068BE7
		internal static object RecordingTimedOut(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006A9F5 File Offset: 0x00068BF5
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006AA04 File Offset: 0x00068C04
		internal static object DrmIsEnabled(ActivityManager manager, string variableName)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return submitMessageManager.DrmIsEnabled;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0006AA30 File Offset: 0x00068C30
		internal static object IsSentImportant(ActivityManager manager, string variableName)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return submitMessageManager.IsSentImportant;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0006AA5C File Offset: 0x00068C5C
		internal static object MessageMarkedPrivate(ActivityManager manager, string variableName)
		{
			SubmitMessageManager submitMessageManager = manager as SubmitMessageManager;
			if (submitMessageManager == null)
			{
				SubmitMessageManager.GetScope(manager, out submitMessageManager);
			}
			return submitMessageManager.MessageMarkedPrivate;
		}
	}
}
