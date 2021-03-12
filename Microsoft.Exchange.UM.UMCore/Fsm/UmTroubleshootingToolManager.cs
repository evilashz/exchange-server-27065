using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031F RID: 799
	internal class UmTroubleshootingToolManager
	{
		// Token: 0x06001BB1 RID: 7089 RVA: 0x0006AAE7 File Offset: 0x00068CE7
		internal static void GetScope(ActivityManager manager, out UmTroubleshootingToolManager scope)
		{
			for (scope = (manager as UmTroubleshootingToolManager); scope == null; scope = (manager as UmTroubleshootingToolManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x0006AB1A File Offset: 0x00068D1A
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0006AB24 File Offset: 0x00068D24
		internal static TransitionBase ConfirmAcceptedCallType(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			UmTroubleshootingToolManager umTroubleshootingToolManager = manager as UmTroubleshootingToolManager;
			if (umTroubleshootingToolManager == null)
			{
				UmTroubleshootingToolManager.GetScope(manager, out umTroubleshootingToolManager);
			}
			return manager.GetTransition(umTroubleshootingToolManager.ConfirmAcceptedCallType(vo));
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0006AB50 File Offset: 0x00068D50
		internal static TransitionBase EchoBackDtmf(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			UmTroubleshootingToolManager umTroubleshootingToolManager = manager as UmTroubleshootingToolManager;
			if (umTroubleshootingToolManager == null)
			{
				UmTroubleshootingToolManager.GetScope(manager, out umTroubleshootingToolManager);
			}
			return manager.GetTransition(umTroubleshootingToolManager.EchoBackDtmf(vo));
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0006AB7C File Offset: 0x00068D7C
		internal static TransitionBase SendStopRecordingDtmf(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			UmTroubleshootingToolManager umTroubleshootingToolManager = manager as UmTroubleshootingToolManager;
			if (umTroubleshootingToolManager == null)
			{
				UmTroubleshootingToolManager.GetScope(manager, out umTroubleshootingToolManager);
			}
			return manager.GetTransition(umTroubleshootingToolManager.SendStopRecordingDtmf(vo));
		}
	}
}
