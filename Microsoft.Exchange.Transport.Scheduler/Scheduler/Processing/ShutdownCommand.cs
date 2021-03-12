using System;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200002F RID: 47
	internal class ShutdownCommand : ISchedulerCommand
	{
		// Token: 0x0600011B RID: 283 RVA: 0x0000553E File Offset: 0x0000373E
		public ShutdownCommand(ProcessingScheduler scheduler)
		{
			this.scheduler = scheduler;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000554D File Offset: 0x0000374D
		public void Execute()
		{
			this.scheduler.StartShutdown();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000555A File Offset: 0x0000375A
		public bool WaitForDone(int timeoutMilliseconds = -1)
		{
			return this.scheduler.WaitForShutdown(timeoutMilliseconds);
		}

		// Token: 0x040000A6 RID: 166
		private readonly ProcessingScheduler scheduler;
	}
}
