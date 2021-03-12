using System;
using System.Threading;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B65 RID: 2917
	internal abstract class AsyncResultBase : IAsyncResult
	{
		// Token: 0x060052AA RID: 21162 RVA: 0x0010B68D File Offset: 0x0010988D
		protected AsyncResultBase(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.thisLock = new object();
		}

		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x0010B6AE File Offset: 0x001098AE
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x060052AC RID: 21164 RVA: 0x0010B6B8 File Offset: 0x001098B8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.manualResetEvent != null)
				{
					return this.manualResetEvent;
				}
				lock (this.ThisLock)
				{
					if (this.manualResetEvent == null)
					{
						this.manualResetEvent = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.manualResetEvent;
			}
		}

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x0010B720 File Offset: 0x00109920
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x060052AE RID: 21166 RVA: 0x0010B728 File Offset: 0x00109928
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x0010B730 File Offset: 0x00109930
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x060052B0 RID: 21168 RVA: 0x0010B738 File Offset: 0x00109938
		protected void Complete(bool completedSynchronously)
		{
			if (this.isCompleted)
			{
				throw new InvalidOperationException("Cannot call Complete twice");
			}
			this.completedSynchronously = completedSynchronously;
			if (completedSynchronously)
			{
				this.isCompleted = true;
			}
			else
			{
				lock (this.ThisLock)
				{
					this.isCompleted = true;
					if (this.manualResetEvent != null)
					{
						this.manualResetEvent.Set();
					}
				}
			}
			if (this.callback != null)
			{
				this.callback(this);
			}
		}

		// Token: 0x060052B1 RID: 21169 RVA: 0x0010B7C8 File Offset: 0x001099C8
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0010B7D8 File Offset: 0x001099D8
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResultBase
		{
			if (result == null)
			{
				throw new ArgumentNullException("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw new ArgumentException("Invalid async result.", "result");
			}
			if (tasyncResult.endCalled)
			{
				throw new InvalidOperationException("Async object already ended.");
			}
			tasyncResult.endCalled = true;
			if (!tasyncResult.isCompleted)
			{
				tasyncResult.AsyncWaitHandle.WaitOne();
			}
			if (tasyncResult.manualResetEvent != null)
			{
				tasyncResult.manualResetEvent.Close();
			}
			if (tasyncResult.exception != null)
			{
				throw tasyncResult.exception;
			}
			return tasyncResult;
		}

		// Token: 0x04002E0A RID: 11786
		private AsyncCallback callback;

		// Token: 0x04002E0B RID: 11787
		private object state;

		// Token: 0x04002E0C RID: 11788
		private bool completedSynchronously;

		// Token: 0x04002E0D RID: 11789
		private bool endCalled;

		// Token: 0x04002E0E RID: 11790
		private Exception exception;

		// Token: 0x04002E0F RID: 11791
		private bool isCompleted;

		// Token: 0x04002E10 RID: 11792
		private ManualResetEvent manualResetEvent;

		// Token: 0x04002E11 RID: 11793
		private object thisLock;
	}
}
