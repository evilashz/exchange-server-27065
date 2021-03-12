using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000520 RID: 1312
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x06003EAA RID: 16042 RVA: 0x000E85B3 File Offset: 0x000E67B3
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06003EAB RID: 16043 RVA: 0x000E85C4 File Offset: 0x000E67C4
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06003EAC RID: 16044 RVA: 0x000E85F4 File Offset: 0x000E67F4
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06003EAD RID: 16045 RVA: 0x000E8601 File Offset: 0x000E6801
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06003EAE RID: 16046 RVA: 0x000E860E File Offset: 0x000E680E
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06003EAF RID: 16047 RVA: 0x000E861B File Offset: 0x000E681B
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003EB0 RID: 16048 RVA: 0x000E8628 File Offset: 0x000E6828
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06003EB1 RID: 16049 RVA: 0x000E8658 File Offset: 0x000E6858
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001A06 RID: 6662
		private Task<TResult> m_task;
	}
}
