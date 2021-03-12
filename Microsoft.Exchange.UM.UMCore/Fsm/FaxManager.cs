using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000310 RID: 784
	internal class FaxManager
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x0006711E File Offset: 0x0006531E
		internal static void GetScope(ActivityManager manager, out FaxManager scope)
		{
			for (scope = (manager as FaxManager); scope == null; scope = (manager as FaxManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00067151 File Offset: 0x00065351
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006715B File Offset: 0x0006535B
		internal static TransitionBase IsQuotaExceeded(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00067165 File Offset: 0x00065365
		internal static TransitionBase IsPipelineHealthy(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00067170 File Offset: 0x00065370
		internal static TransitionBase HandleFailedTransfer(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			FaxManager faxManager = manager as FaxManager;
			if (faxManager == null)
			{
				FaxManager.GetScope(manager, out faxManager);
			}
			return manager.GetTransition(faxManager.HandleFailedTransfer(vo));
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006719C File Offset: 0x0006539C
		internal static PhoneNumber TargetPhoneNumber(ActivityManager manager, string variableName)
		{
			FaxManager faxManager = manager as FaxManager;
			if (faxManager == null)
			{
				FaxManager.GetScope(manager, out faxManager);
			}
			return faxManager.TargetPhoneNumber;
		}
	}
}
