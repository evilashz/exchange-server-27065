using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x02000311 RID: 785
	internal class FindMeSubscriberManager
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x000671C9 File Offset: 0x000653C9
		internal static void GetScope(ActivityManager manager, out FindMeSubscriberManager scope)
		{
			for (scope = (manager as FindMeSubscriberManager); scope == null; scope = (manager as FindMeSubscriberManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000671FC File Offset: 0x000653FC
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x00067208 File Offset: 0x00065408
		internal static TransitionBase SendDtmf(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			FindMeSubscriberManager findMeSubscriberManager = manager as FindMeSubscriberManager;
			if (findMeSubscriberManager == null)
			{
				FindMeSubscriberManager.GetScope(manager, out findMeSubscriberManager);
			}
			return manager.GetTransition(findMeSubscriberManager.SendDtmf(vo));
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00067234 File Offset: 0x00065434
		internal static TransitionBase TerminateFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			FindMeSubscriberManager findMeSubscriberManager = manager as FindMeSubscriberManager;
			if (findMeSubscriberManager == null)
			{
				FindMeSubscriberManager.GetScope(manager, out findMeSubscriberManager);
			}
			return manager.GetTransition(findMeSubscriberManager.TerminateFindMe(vo));
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00067260 File Offset: 0x00065460
		internal static TransitionBase AcceptFindMe(ActivityManager manager, string actionName, BaseUMCallSession vo)
		{
			FindMeSubscriberManager findMeSubscriberManager = manager as FindMeSubscriberManager;
			if (findMeSubscriberManager == null)
			{
				FindMeSubscriberManager.GetScope(manager, out findMeSubscriberManager);
			}
			return manager.GetTransition(findMeSubscriberManager.AcceptFindMe(vo));
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006728C File Offset: 0x0006548C
		internal static object CalleeRecordName(ActivityManager manager, string variableName)
		{
			FindMeSubscriberManager findMeSubscriberManager = manager as FindMeSubscriberManager;
			if (findMeSubscriberManager == null)
			{
				FindMeSubscriberManager.GetScope(manager, out findMeSubscriberManager);
			}
			return findMeSubscriberManager.CalleeRecordName;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000672B4 File Offset: 0x000654B4
		internal static object CallerRecordedName(ActivityManager manager, string variableName)
		{
			FindMeSubscriberManager findMeSubscriberManager = manager as FindMeSubscriberManager;
			if (findMeSubscriberManager == null)
			{
				FindMeSubscriberManager.GetScope(manager, out findMeSubscriberManager);
			}
			return findMeSubscriberManager.CallerRecordedName;
		}
	}
}
