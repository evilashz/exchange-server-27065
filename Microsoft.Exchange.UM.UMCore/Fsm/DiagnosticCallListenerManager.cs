using System;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;

namespace Microsoft.Exchange.UM.Fsm
{
	// Token: 0x0200030D RID: 781
	internal class DiagnosticCallListenerManager
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0006672A File Offset: 0x0006492A
		internal static void GetScope(ActivityManager manager, out DiagnosticCallListenerManager scope)
		{
			for (scope = (manager as DiagnosticCallListenerManager); scope == null; scope = (manager as DiagnosticCallListenerManager))
			{
				if (manager.Manager == null)
				{
					throw new FsmConfigurationException(string.Empty);
				}
				manager = manager.Manager;
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0006675D File Offset: 0x0006495D
		internal static TransitionBase Disconnect(ActivityManager manager, string methodName, BaseUMCallSession vo)
		{
			return manager.ExecuteAction(methodName, vo);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00066767 File Offset: 0x00064967
		internal static object DiagnosticTUILogonCheck(ActivityManager manager, string variableName)
		{
			return manager.ReadVariable(variableName) ?? null;
		}
	}
}
