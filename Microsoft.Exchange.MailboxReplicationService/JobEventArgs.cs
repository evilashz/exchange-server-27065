using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000023 RID: 35
	internal class JobEventArgs : EventArgs
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000910F File Offset: 0x0000730F
		public JobEventArgs(IJob job, JobState state)
		{
			this.job = job;
			this.state = state;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009125 File Offset: 0x00007325
		public IJob Job
		{
			get
			{
				return this.job;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000912D File Offset: 0x0000732D
		public JobState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04000094 RID: 148
		private IJob job;

		// Token: 0x04000095 RID: 149
		private JobState state;
	}
}
