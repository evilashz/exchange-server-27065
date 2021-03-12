using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000911 RID: 2321
	internal abstract class TaskBase : ITask
	{
		// Token: 0x0600526C RID: 21100 RVA: 0x001535A4 File Offset: 0x001517A4
		public TaskBase(string taskName, int weight)
		{
			this.Name = taskName;
			this.Weight = weight;
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x001535BA File Offset: 0x001517BA
		// (set) Token: 0x0600526E RID: 21102 RVA: 0x001535C2 File Offset: 0x001517C2
		public string Name { get; private set; }

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x001535CB File Offset: 0x001517CB
		// (set) Token: 0x06005270 RID: 21104 RVA: 0x001535D3 File Offset: 0x001517D3
		public int Weight { get; private set; }

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06005271 RID: 21105 RVA: 0x001535DC File Offset: 0x001517DC
		// (set) Token: 0x06005272 RID: 21106 RVA: 0x001535E4 File Offset: 0x001517E4
		private protected ITaskContext TaskContext { protected get; private set; }

		// Token: 0x06005273 RID: 21107 RVA: 0x001535ED File Offset: 0x001517ED
		public virtual bool CheckPrereqs(ITaskContext taskContext)
		{
			this.TaskContext = taskContext;
			return true;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x001535F7 File Offset: 0x001517F7
		public virtual bool NeedsConfiguration(ITaskContext taskContext)
		{
			this.TaskContext = taskContext;
			return false;
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x00153601 File Offset: 0x00151801
		public virtual bool Configure(ITaskContext taskContext)
		{
			this.TaskContext = taskContext;
			return true;
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0015360B File Offset: 0x0015180B
		public virtual bool ValidateConfiguration(ITaskContext taskContext)
		{
			this.TaskContext = taskContext;
			return true;
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x00153615 File Offset: 0x00151815
		protected void AddLocalizedStringError(LocalizedString errorMessage)
		{
			this.TaskContext.Errors.Add(errorMessage);
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x00153628 File Offset: 0x00151828
		protected void AddLocalizedStringWarning(LocalizedString warningMessage)
		{
			this.TaskContext.Warnings.Add(warningMessage);
		}
	}
}
