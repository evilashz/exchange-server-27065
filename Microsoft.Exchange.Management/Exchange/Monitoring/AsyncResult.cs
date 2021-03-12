using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000585 RID: 1413
	internal class AsyncResult<T> : IAsyncResult, IDisposable where T : TransactionOutcomeBase
	{
		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000CA8E0 File Offset: 0x000C8AE0
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("AsyncResult");
				}
				if (this.asyncEvent == null)
				{
					lock (this.lockObject)
					{
						if (this.asyncEvent == null)
						{
							this.asyncEvent = new ManualResetEvent(this.completed);
						}
					}
				}
				return this.asyncEvent;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000CA954 File Offset: 0x000C8B54
		public bool IsCompleted
		{
			get
			{
				return this.completed;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x000CA95C File Offset: 0x000C8B5C
		public object AsyncState
		{
			get
			{
				return this.Outcomes;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000CA964 File Offset: 0x000C8B64
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000CA967 File Offset: 0x000C8B67
		public List<T> Outcomes
		{
			get
			{
				if (this.outcomes == null)
				{
					this.outcomes = new List<T>();
				}
				return this.outcomes;
			}
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000CA984 File Offset: 0x000C8B84
		internal void Complete()
		{
			if (!this.completed)
			{
				if (this.asyncEvent != null && !this.disposed)
				{
					lock (this.lockObject)
					{
						if (!this.completed)
						{
							this.completed = true;
							if (this.asyncEvent != null && !this.disposed)
							{
								this.asyncEvent.Set();
							}
						}
						return;
					}
				}
				this.completed = true;
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000CAA08 File Offset: 0x000C8C08
		internal void SetTimeout()
		{
			if (!this.timeout)
			{
				lock (this.lockObject)
				{
					this.timeout = true;
				}
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000CAA54 File Offset: 0x000C8C54
		public bool DidTimeout()
		{
			return this.timeout;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000CAA5C File Offset: 0x000C8C5C
		public void Dispose()
		{
			if (!this.disposed)
			{
				lock (this.lockObject)
				{
					if (!this.disposed)
					{
						this.disposed = true;
						if (this.asyncEvent != null)
						{
							this.asyncEvent.Close();
							this.asyncEvent = null;
						}
					}
				}
			}
		}

		// Token: 0x04002334 RID: 9012
		private bool completed;

		// Token: 0x04002335 RID: 9013
		private bool timeout;

		// Token: 0x04002336 RID: 9014
		private ManualResetEvent asyncEvent;

		// Token: 0x04002337 RID: 9015
		private object lockObject = new object();

		// Token: 0x04002338 RID: 9016
		private List<T> outcomes;

		// Token: 0x04002339 RID: 9017
		private bool disposed;
	}
}
