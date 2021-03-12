using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000007 RID: 7
	internal interface IProcessingSchedulerAdmin
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12
		bool IsRunning { get; }

		// Token: 0x0600000D RID: 13
		void Pause();

		// Token: 0x0600000E RID: 14
		void Resume();

		// Token: 0x0600000F RID: 15
		bool Shutdown(int timeoutMilliseconds = -1);

		// Token: 0x06000010 RID: 16
		SchedulerDiagnosticsInfo GetDiagnosticsInfo();
	}
}
