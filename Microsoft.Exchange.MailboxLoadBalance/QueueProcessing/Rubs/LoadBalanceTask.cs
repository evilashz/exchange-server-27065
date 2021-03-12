using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Logging;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxLoadBalance.QueueProcessing.Rubs
{
	// Token: 0x020000EA RID: 234
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadBalanceTask : SystemTaskBase
	{
		// Token: 0x06000729 RID: 1833 RVA: 0x0001450F File Offset: 0x0001270F
		public LoadBalanceTask(SystemWorkloadBase workload, ResourceReservation reservation, IRequest request) : base(workload, reservation)
		{
			this.request = request;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00014520 File Offset: 0x00012720
		protected override TaskStepResult Execute()
		{
			this.request.Process();
			DatabaseRequestLog.Write(this.request);
			return TaskStepResult.Complete;
		}

		// Token: 0x040002C6 RID: 710
		private readonly IRequest request;
	}
}
