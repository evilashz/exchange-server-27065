using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200031E RID: 798
	internal class UmDiagnosticManager
	{
		// Token: 0x06001BAC RID: 7084 RVA: 0x0006AA8E File Offset: 0x00068C8E
		internal static void GetScope(ActivityManager manager, out UmDiagnosticManager scope)
		{
			for (scope = (manager as UmDiagnosticManager); scope == null; scope = (manager as UmDiagnosticManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x0006AAC1 File Offset: 0x00068CC1
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x0006AACB File Offset: 0x00068CCB
		internal static TransitionBase IsLocal(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0006AAD5 File Offset: 0x00068CD5
		internal static TransitionBase SendDtmf(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}
	}
}
