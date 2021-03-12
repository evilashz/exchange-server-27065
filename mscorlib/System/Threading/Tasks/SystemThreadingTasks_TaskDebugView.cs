using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000533 RID: 1331
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x06004029 RID: 16425 RVA: 0x000EF360 File Offset: 0x000ED560
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600402A RID: 16426 RVA: 0x000EF36F File Offset: 0x000ED56F
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x000EF37C File Offset: 0x000ED57C
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x0600402C RID: 16428 RVA: 0x000EF389 File Offset: 0x000ED589
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x000EF396 File Offset: 0x000ED596
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x0600402E RID: 16430 RVA: 0x000EF3A4 File Offset: 0x000ED5A4
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x000EF3D4 File Offset: 0x000ED5D4
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001A66 RID: 6758
		private Task m_task;
	}
}
