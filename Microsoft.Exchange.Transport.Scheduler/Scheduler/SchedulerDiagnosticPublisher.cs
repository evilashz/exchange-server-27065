using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;
using Microsoft.Exchange.Transport.Scheduler.Processing;

namespace Microsoft.Exchange.Transport.Scheduler
{
	// Token: 0x0200002B RID: 43
	internal class SchedulerDiagnosticPublisher
	{
		// Token: 0x060000EE RID: 238 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public SchedulerDiagnosticPublisher(IProcessingSchedulerAdmin processingSchedulerAdmin)
		{
			ArgumentValidator.ThrowIfNull("processingSchedulerAdmin", processingSchedulerAdmin);
			this.processingSchedulerAdmin = processingSchedulerAdmin;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004C04 File Offset: 0x00002E04
		public void Publish()
		{
			SchedulerDiagnosticsInfo diagnosticsInfo = this.processingSchedulerAdmin.GetDiagnosticsInfo();
			this.perfCountersInstance.OldestLockAge.RawValue = this.GetAgeTicks(diagnosticsInfo.OldestLockTimeStamp);
			this.perfCountersInstance.OldestScopedQueueAge.RawValue = this.GetAgeTicks(diagnosticsInfo.OldestScopedQueueCreateTime);
			this.perfCountersInstance.TotalScopedQueues.RawValue = diagnosticsInfo.TotalScopedQueues;
			this.perfCountersInstance.TotalReceived.RawValue = diagnosticsInfo.Received;
			this.perfCountersInstance.TotalScheduled.RawValue = diagnosticsInfo.Dispatched;
			this.perfCountersInstance.ThrottlingRate.RawValue = diagnosticsInfo.ThrottleRate;
			this.perfCountersInstance.ScopedQueuesCreationRate.RawValue = diagnosticsInfo.ScopedQueuesCreatedRate;
			this.perfCountersInstance.ScopedQueuesRemovalRate.RawValue = diagnosticsInfo.ScopedQueuesDestroyedRate;
			this.perfCountersInstance.ProcessingVelocity.RawValue = diagnosticsInfo.ProcessRate - diagnosticsInfo.ReceiveRate;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004CF8 File Offset: 0x00002EF8
		private long GetAgeTicks(DateTime timeStamp)
		{
			if (!(timeStamp == DateTime.MaxValue))
			{
				return timeStamp.Ticks;
			}
			return DateTime.UtcNow.Ticks;
		}

		// Token: 0x0400008F RID: 143
		private readonly IProcessingSchedulerAdmin processingSchedulerAdmin;

		// Token: 0x04000090 RID: 144
		private SchedulerPerfCountersInstance perfCountersInstance = SchedulerPerfCounters.GetInstance("other");
	}
}
