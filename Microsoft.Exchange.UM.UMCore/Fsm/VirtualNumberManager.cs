using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000320 RID: 800
	internal class VirtualNumberManager
	{
		// Token: 0x06001BB7 RID: 7095 RVA: 0x0006ABB0 File Offset: 0x00068DB0
		internal static void GetScope(ActivityManager manager, out VirtualNumberManager scope)
		{
			for (scope = (manager as VirtualNumberManager); scope == null; scope = (manager as VirtualNumberManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0006ABE3 File Offset: 0x00068DE3
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0006ABF0 File Offset: 0x00068DF0
		internal static TransitionBase CheckIfCallFromBlockedNumber(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			VirtualNumberManager virtualNumberManager = manager as VirtualNumberManager;
			if (virtualNumberManager == null)
			{
				VirtualNumberManager.GetScope(manager, out virtualNumberManager);
			}
			return manager.GetTransition(virtualNumberManager.CheckIfCallFromBlockedNumber(vo));
		}
	}
}
