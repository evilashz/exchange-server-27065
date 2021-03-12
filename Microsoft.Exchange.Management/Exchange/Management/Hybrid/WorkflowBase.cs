using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02000913 RID: 2323
	internal abstract class WorkflowBase : IWorkflow
	{
		// Token: 0x0600527D RID: 21117 RVA: 0x0015366C File Offset: 0x0015186C
		public WorkflowBase()
		{
			this.tasks = new List<ITask>();
			this.Initialize();
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x00153685 File Offset: 0x00151885
		public IEnumerable<ITask> Tasks
		{
			get
			{
				return this.tasks;
			}
		}

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x0600527F RID: 21119 RVA: 0x0015368D File Offset: 0x0015188D
		public int PercentCompleted
		{
			get
			{
				if (this.totalWeight != 0)
				{
					return (int)(100f * (float)this.completed / (float)this.totalWeight + 0.5f);
				}
				return 0;
			}
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x001536B5 File Offset: 0x001518B5
		public void Initialize()
		{
			this.completed = 0;
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x001536BE File Offset: 0x001518BE
		public void UpdateProgress(ITask task)
		{
			this.UpdateProgress(task.Weight);
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x001536CC File Offset: 0x001518CC
		protected void UpdateProgress(int weight)
		{
			this.completed += weight;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x001536DC File Offset: 0x001518DC
		protected void AddTask(ITask task)
		{
			this.tasks.Add(task);
			this.CalculateTotalWeight();
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x001536F0 File Offset: 0x001518F0
		protected void AddOverhead(int weight)
		{
			this.overheadWeight += weight;
			this.CalculateTotalWeight();
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x00153708 File Offset: 0x00151908
		private void CalculateTotalWeight()
		{
			this.totalWeight = this.overheadWeight;
			foreach (ITask task in this.tasks)
			{
				this.totalWeight += task.Weight;
			}
		}

		// Token: 0x04002FF0 RID: 12272
		private int overheadWeight;

		// Token: 0x04002FF1 RID: 12273
		private int totalWeight;

		// Token: 0x04002FF2 RID: 12274
		private int completed;

		// Token: 0x04002FF3 RID: 12275
		private IList<ITask> tasks;
	}
}
