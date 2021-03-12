using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031A RID: 794
	internal class RecordVoicemailManager
	{
		// Token: 0x06001AFC RID: 6908 RVA: 0x00069C8E File Offset: 0x00067E8E
		internal static void GetScope(ActivityManager manager, out RecordVoicemailManager scope)
		{
			for (scope = (manager as RecordVoicemailManager); scope == null; scope = (manager as RecordVoicemailManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x00069CC1 File Offset: 0x00067EC1
		internal static TransitionBase AppendRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x00069CCB File Offset: 0x00067ECB
		internal static TransitionBase CanAnnonLeaveMessage(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x00069CD5 File Offset: 0x00067ED5
		internal static TransitionBase ClearRecording(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x00069CDF File Offset: 0x00067EDF
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00069CE9 File Offset: 0x00067EE9
		internal static TransitionBase GetGreeting(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x00069CF3 File Offset: 0x00067EF3
		internal static TransitionBase GetName(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00069CFD File Offset: 0x00067EFD
		internal static TransitionBase IsQuotaExceeded(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00069D07 File Offset: 0x00067F07
		internal static TransitionBase IsPipelineHealthy(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00069D11 File Offset: 0x00067F11
		internal static TransitionBase SubmitVoiceMail(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x00069D1B File Offset: 0x00067F1B
		internal static TransitionBase SubmitVoiceMailUrgent(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x00069D28 File Offset: 0x00067F28
		internal static TransitionBase ToggleImportance(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return manager.GetTransition(recordVoicemailManager.ToggleImportance(vo));
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00069D54 File Offset: 0x00067F54
		internal static TransitionBase TogglePrivacy(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return manager.GetTransition(recordVoicemailManager.TogglePrivacy(vo));
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00069D80 File Offset: 0x00067F80
		internal static TransitionBase ClearSelection(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return manager.GetTransition(recordVoicemailManager.ClearSelection(vo));
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00069DAC File Offset: 0x00067FAC
		internal static ITempWavFile Greeting(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00069DBF File Offset: 0x00067FBF
		internal static object Mode(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00069DCD File Offset: 0x00067FCD
		internal static object OcFeature(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00069DDB File Offset: 0x00067FDB
		internal static PhoneNumber OperatorNumber(ActivityManager manager, string variableName)
		{
			return (PhoneNumber)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00069DEE File Offset: 0x00067FEE
		internal static ITempWavFile Recording(ActivityManager manager, string variableName)
		{
			return (ITempWavFile)(manager.ReadVariable(variableName) ?? null);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x00069E01 File Offset: 0x00068001
		internal static object RecordingTimedOut(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00069E0F File Offset: 0x0006800F
		internal static object UserName(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x00069E20 File Offset: 0x00068020
		internal static object VoiceMailAnalysisWarningRequired(ActivityManager manager, string variableName)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return recordVoicemailManager.VoiceMailAnalysisWarningRequired;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x00069E4C File Offset: 0x0006804C
		internal static object AllowMarkAsPrivate(ActivityManager manager, string variableName)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return recordVoicemailManager.AllowMarkAsPrivate;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00069E78 File Offset: 0x00068078
		internal static object IsSentImportant(ActivityManager manager, string variableName)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return recordVoicemailManager.IsSentImportant;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x00069EA4 File Offset: 0x000680A4
		internal static object MessageMarkedPrivate(ActivityManager manager, string variableName)
		{
			RecordVoicemailManager recordVoicemailManager = manager as RecordVoicemailManager;
			if (recordVoicemailManager == null)
			{
				RecordVoicemailManager.GetScope(manager, out recordVoicemailManager);
			}
			return recordVoicemailManager.MessageMarkedPrivate;
		}
	}
}
