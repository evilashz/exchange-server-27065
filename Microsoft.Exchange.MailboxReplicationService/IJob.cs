using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000020 RID: 32
	internal interface IJob
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013A RID: 314
		JobState State { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013B RID: 315
		bool ShouldWakeup { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316
		JobSortKey JobSortKey { get; }

		// Token: 0x0600013D RID: 317
		MrsSystemTask GetTask(SystemWorkloadBase systemWorkload, ResourceReservationContext context);

		// Token: 0x0600013E RID: 318
		void ProcessTaskExecutionResult(MrsSystemTask systemTask);

		// Token: 0x0600013F RID: 319
		void RevertToPreviousUnthrottledState();

		// Token: 0x06000140 RID: 320
		void WaitForJobToBeDone();

		// Token: 0x06000141 RID: 321
		void ResetJob();
	}
}
