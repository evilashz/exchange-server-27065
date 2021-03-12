using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200054B RID: 1355
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x060040F9 RID: 16633 RVA: 0x000F1AA8 File Offset: 0x000EFCA8
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_FromCurrentSynchronizationContext_NoCurrent"));
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000F1ADB File Offset: 0x000EFCDB
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000F1AEE File Offset: 0x000EFCEE
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000F1B06 File Offset: 0x000EFD06
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x000F1B09 File Offset: 0x000EFD09
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000F1B0C File Offset: 0x000EFD0C
		private static void PostCallback(object obj)
		{
			Task task = (Task)obj;
			task.ExecuteEntry(true);
		}

		// Token: 0x04001AB8 RID: 6840
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001AB9 RID: 6841
		private static SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextTaskScheduler.PostCallback);
	}
}
