using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000039 RID: 57
	internal class TaskProvider : DisposeTrackableBase, ITaskProvider, IDisposable
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000A62B File Offset: 0x0000882B
		public TaskProvider(ClassificationBlock classificationBlock)
		{
			this.classificationBlock = classificationBlock;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A63C File Offset: 0x0000883C
		public SystemTaskBase GetNextTask()
		{
			SystemWorkloadBase nextWorkload = this.classificationBlock.GetNextWorkload();
			if (nextWorkload != null)
			{
				return nextWorkload.InternalGetTask();
			}
			return null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A660 File Offset: 0x00008860
		protected override void InternalDispose(bool disposing)
		{
			this.classificationBlock.Deactivate();
			this.classificationBlock = null;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A674 File Offset: 0x00008874
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TaskProvider>(this);
		}

		// Token: 0x0400011D RID: 285
		private ClassificationBlock classificationBlock;
	}
}
