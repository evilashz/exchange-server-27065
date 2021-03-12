using System;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000912 RID: 2322
	internal abstract class SessionTask : TaskBase
	{
		// Token: 0x06005279 RID: 21113 RVA: 0x0015363B File Offset: 0x0015183B
		public SessionTask(string taskName, int weight) : base(taskName, weight)
		{
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x0600527A RID: 21114 RVA: 0x00153645 File Offset: 0x00151845
		protected ITenantSession TenantSession
		{
			get
			{
				return base.TaskContext.TenantSession;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x0600527B RID: 21115 RVA: 0x00153652 File Offset: 0x00151852
		protected IOnPremisesSession OnPremisesSession
		{
			get
			{
				return base.TaskContext.OnPremisesSession;
			}
		}

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x0600527C RID: 21116 RVA: 0x0015365F File Offset: 0x0015185F
		protected ILogger Logger
		{
			get
			{
				return base.TaskContext.Logger;
			}
		}
	}
}
