using System;
using System.Threading;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200069F RID: 1695
	internal class ServiceAsyncResult<T> : IAsyncResult
	{
		// Token: 0x0600342C RID: 13356 RVA: 0x000BC046 File Offset: 0x000BA246
		internal ServiceAsyncResult()
		{
			this.isCompleted = false;
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x000BC060 File Offset: 0x000BA260
		internal void Complete(object state)
		{
			if (!this.isCompleted)
			{
				lock (this.instanceLock)
				{
					if (!this.isCompleted)
					{
						this.completionState = state;
						this.isCompleted = true;
						if (this.asyncCallback != null)
						{
							this.asyncCallback(this);
						}
						if (this.completedEvent != null)
						{
							this.completedEvent.Set();
						}
					}
				}
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000BC0E4 File Offset: 0x000BA2E4
		public object CompletionState
		{
			get
			{
				return this.completionState;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000BC0EC File Offset: 0x000BA2EC
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000BC0F4 File Offset: 0x000BA2F4
		public T Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000BC0FD File Offset: 0x000BA2FD
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000BC105 File Offset: 0x000BA305
		public AsyncCallback AsyncCallback
		{
			get
			{
				return this.asyncCallback;
			}
			set
			{
				this.asyncCallback = value;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000BC10E File Offset: 0x000BA30E
		// (set) Token: 0x06003434 RID: 13364 RVA: 0x000BC116 File Offset: 0x000BA316
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
			set
			{
				this.asyncState = value;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06003435 RID: 13365 RVA: 0x000BC120 File Offset: 0x000BA320
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.completedEvent == null)
				{
					lock (this.instanceLock)
					{
						if (this.completedEvent == null)
						{
							this.completedEvent = new ManualResetEvent(false);
						}
					}
				}
				return this.completedEvent;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x000BC184 File Offset: 0x000BA384
		public bool CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06003437 RID: 13367 RVA: 0x000BC187 File Offset: 0x000BA387
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x04001D83 RID: 7555
		private AsyncCallback asyncCallback;

		// Token: 0x04001D84 RID: 7556
		private object asyncState;

		// Token: 0x04001D85 RID: 7557
		private T data;

		// Token: 0x04001D86 RID: 7558
		private bool isCompleted;

		// Token: 0x04001D87 RID: 7559
		private object completionState;

		// Token: 0x04001D88 RID: 7560
		private object instanceLock = new object();

		// Token: 0x04001D89 RID: 7561
		private volatile ManualResetEvent completedEvent;
	}
}
