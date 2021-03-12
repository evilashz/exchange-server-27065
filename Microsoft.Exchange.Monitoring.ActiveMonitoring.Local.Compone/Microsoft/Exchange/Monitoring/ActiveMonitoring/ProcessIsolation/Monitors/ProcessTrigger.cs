using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessIsolation.Monitors
{
	// Token: 0x02000299 RID: 665
	public enum ProcessTrigger
	{
		// Token: 0x04000E30 RID: 3632
		PrivateWorkingSetTrigger_Warning,
		// Token: 0x04000E31 RID: 3633
		PrivateWorkingSetTrigger_Error,
		// Token: 0x04000E32 RID: 3634
		ProcessProcessorTimeTrigger_Warning,
		// Token: 0x04000E33 RID: 3635
		ProcessProcessorTimeTrigger_Error,
		// Token: 0x04000E34 RID: 3636
		ExchangeCrashEventTrigger_Error,
		// Token: 0x04000E35 RID: 3637
		LongRunningWatsonTrigger_Warning,
		// Token: 0x04000E36 RID: 3638
		LongRunningWerMgrTrigger_Warning
	}
}
