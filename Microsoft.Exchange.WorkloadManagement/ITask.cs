using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x0200003C RID: 60
	internal interface ITask
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600023F RID: 575
		IBudget Budget { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000240 RID: 576
		TimeSpan MaxExecutionTime { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000241 RID: 577
		// (set) Token: 0x06000242 RID: 578
		object State { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000243 RID: 579
		// (set) Token: 0x06000244 RID: 580
		string Description { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000245 RID: 581
		WorkloadSettings WorkloadSettings { get; }

		// Token: 0x06000246 RID: 582
		IActivityScope GetActivityScope();

		// Token: 0x06000247 RID: 583
		TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime);

		// Token: 0x06000248 RID: 584
		void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime);

		// Token: 0x06000249 RID: 585
		void Cancel();

		// Token: 0x0600024A RID: 586
		void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime);

		// Token: 0x0600024B RID: 587
		ResourceKey[] GetResources();

		// Token: 0x0600024C RID: 588
		TaskExecuteResult CancelStep(LocalizedException exception);
	}
}
