using System;
using System.Threading;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000B0C RID: 2828
	internal class BasicAsyncResult : IAsyncResult
	{
		// Token: 0x06003CD9 RID: 15577 RVA: 0x0009E816 File Offset: 0x0009CA16
		public BasicAsyncResult(AsyncCallback asyncCallback, object asyncState)
		{
			this.internalState = 0;
			this.AsyncCallback = asyncCallback;
			this.AsyncState = asyncState;
		}

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x0009E833 File Offset: 0x0009CA33
		// (set) Token: 0x06003CDB RID: 15579 RVA: 0x0009E83B File Offset: 0x0009CA3B
		public object AsyncState { get; private set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x0009E844 File Offset: 0x0009CA44
		public bool CompletedSynchronously
		{
			get
			{
				int num = Interlocked.CompareExchange(ref this.internalState, 1, 1);
				return num == 1 || num == 3;
			}
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x0009E86C File Offset: 0x0009CA6C
		public bool IsCompleted
		{
			get
			{
				int num = Interlocked.CompareExchange(ref this.internalState, 0, 0);
				return num != 0;
			}
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x0009E890 File Offset: 0x0009CA90
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEvent manualResetEvent = new ManualResetEvent(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEvent>(ref this.completionEvent, manualResetEvent, null) != null)
					{
						manualResetEvent.Close();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						this.completionEvent.Set();
					}
				}
				return this.completionEvent;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x0009E8E7 File Offset: 0x0009CAE7
		public int InternalState
		{
			get
			{
				return this.internalState;
			}
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x0009E8EF File Offset: 0x0009CAEF
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x0009E8F7 File Offset: 0x0009CAF7
		private Exception Exception { get; set; }

		// Token: 0x06003CE2 RID: 15586 RVA: 0x0009E900 File Offset: 0x0009CB00
		public void Complete(Exception exception, bool completedSynchronously = false)
		{
			this.Exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x0009E910 File Offset: 0x0009CB10
		public void Complete(bool completedSynchronously = false)
		{
			int num = Interlocked.Exchange(ref this.internalState, completedSynchronously ? 1 : 2);
			if (num != 0)
			{
				throw new InvalidOperationException("You can complete a result only once");
			}
			if (this.completionEvent != null)
			{
				this.completionEvent.Set();
			}
			try
			{
				this.InvokeCallback();
			}
			catch (Exception ex)
			{
				if (this.ShouldThrowCallbackException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x0009E978 File Offset: 0x0009CB78
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x0009E980 File Offset: 0x0009CB80
		private protected AsyncCallback AsyncCallback { protected get; private set; }

		// Token: 0x06003CE6 RID: 15590 RVA: 0x0009E98C File Offset: 0x0009CB8C
		public void End()
		{
			if (!this.IsCompleted)
			{
				this.AsyncWaitHandle.WaitOne();
			}
			int num;
			if (this.CompletedSynchronously)
			{
				num = Interlocked.CompareExchange(ref this.internalState, 3, 1);
			}
			else
			{
				num = Interlocked.CompareExchange(ref this.internalState, 4, 2);
			}
			if (num == 1 || num == 2)
			{
				this.AsyncWaitHandle.Close();
			}
			if (this.Exception != null)
			{
				Exception ex = this.CreateEndException(this.Exception);
				if (ex != null)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x0009EA00 File Offset: 0x0009CC00
		protected virtual void InvokeCallback()
		{
			if (this.AsyncCallback != null)
			{
				this.AsyncCallback(this);
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x0009EA16 File Offset: 0x0009CC16
		protected virtual bool ShouldThrowCallbackException(Exception ex)
		{
			return ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x0009EA33 File Offset: 0x0009CC33
		protected virtual Exception CreateEndException(Exception currentException)
		{
			return AsyncExceptionWrapperHelper.GetAsyncWrapper(currentException);
		}

		// Token: 0x04003560 RID: 13664
		private const int StatePending = 0;

		// Token: 0x04003561 RID: 13665
		private const int StateCompletedSync = 1;

		// Token: 0x04003562 RID: 13666
		private const int StateCompletedASync = 2;

		// Token: 0x04003563 RID: 13667
		private const int StateEndedSync = 3;

		// Token: 0x04003564 RID: 13668
		private const int StateEndedASync = 4;

		// Token: 0x04003565 RID: 13669
		private int internalState;

		// Token: 0x04003566 RID: 13670
		private ManualResetEvent completionEvent;
	}
}
