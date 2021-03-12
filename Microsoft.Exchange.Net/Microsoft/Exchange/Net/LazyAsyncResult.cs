using System;
using System.Globalization;
using System.Threading;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BD8 RID: 3032
	internal class LazyAsyncResult : IAsyncResult
	{
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x000B035C File Offset: 0x000AE55C
		private static LazyAsyncResult.ThreadContext CurrentThreadContext
		{
			get
			{
				LazyAsyncResult.ThreadContext threadContext = LazyAsyncResult.threadContext;
				if (threadContext == null)
				{
					threadContext = new LazyAsyncResult.ThreadContext();
					LazyAsyncResult.threadContext = threadContext;
				}
				return threadContext;
			}
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000B037F File Offset: 0x000AE57F
		public LazyAsyncResult(object worker, object callerState, AsyncCallback callerCallback)
		{
			this.asyncObject = worker;
			this.asyncState = callerState;
			this.asyncCallback = callerCallback;
			this.result = DBNull.Value;
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x000B03A8 File Offset: 0x000AE5A8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				this.userEvent = true;
				Interlocked.CompareExchange(ref this.intCompleted, int.MinValue, 0);
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.internalEvent;
				while (manualResetEvent == null)
				{
					this.LazilyCreateEvent(out manualResetEvent);
				}
				return manualResetEvent;
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000B03E9 File Offset: 0x000AE5E9
		public bool CompletedSynchronously
		{
			get
			{
				return Interlocked.CompareExchange(ref this.intCompleted, int.MinValue, 0) > 0;
			}
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600421C RID: 16924 RVA: 0x000B03FF File Offset: 0x000AE5FF
		public bool IsCompleted
		{
			get
			{
				return (Interlocked.CompareExchange(ref this.intCompleted, int.MinValue, 0) & int.MaxValue) != 0;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x000B041E File Offset: 0x000AE61E
		public object AsyncState
		{
			get
			{
				return this.asyncState;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600421E RID: 16926 RVA: 0x000B0426 File Offset: 0x000AE626
		public object AsyncObject
		{
			get
			{
				return this.asyncObject;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x000B042E File Offset: 0x000AE62E
		internal bool InternalPeekCompleted
		{
			get
			{
				return (this.intCompleted & int.MaxValue) != 0;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06004220 RID: 16928 RVA: 0x000B0442 File Offset: 0x000AE642
		internal object Result
		{
			get
			{
				if (this.result != DBNull.Value)
				{
					return this.result;
				}
				return null;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x000B0459 File Offset: 0x000AE659
		// (set) Token: 0x06004222 RID: 16930 RVA: 0x000B0467 File Offset: 0x000AE667
		internal bool EndCalled
		{
			get
			{
				return this.endCalled != 0;
			}
			set
			{
				this.endCalled = (value ? 1 : 0);
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06004223 RID: 16931 RVA: 0x000B0476 File Offset: 0x000AE676
		// (set) Token: 0x06004224 RID: 16932 RVA: 0x000B047E File Offset: 0x000AE67E
		internal int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06004225 RID: 16933 RVA: 0x000B0487 File Offset: 0x000AE687
		// (set) Token: 0x06004226 RID: 16934 RVA: 0x000B048F File Offset: 0x000AE68F
		protected AsyncCallback AsyncCallback
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

		// Token: 0x06004227 RID: 16935 RVA: 0x000B0498 File Offset: 0x000AE698
		internal object InternalWaitForCompletion()
		{
			return this.WaitForCompletion(true);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000B04A1 File Offset: 0x000AE6A1
		internal object InternalWaitForCompletionNoSideEffects()
		{
			return this.WaitForCompletion(false);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000B04AC File Offset: 0x000AE6AC
		internal void InternalCleanup()
		{
			ManualResetEvent manualResetEvent = (ManualResetEvent)this.internalEvent;
			this.internalEvent = null;
			if (manualResetEvent != null)
			{
				manualResetEvent.Close();
			}
			if ((Interlocked.Increment(ref this.intCompleted) & 2147483647) == 1)
			{
				this.result = null;
				this.Cleanup();
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000B04F6 File Offset: 0x000AE6F6
		public bool InvokeCallback(object value)
		{
			return this.ProtectedInvokeCallback(value, IntPtr.Zero);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000B0504 File Offset: 0x000AE704
		public bool InvokeCallback()
		{
			return this.ProtectedInvokeCallback(null, IntPtr.Zero);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000B0514 File Offset: 0x000AE714
		public static T EndAsyncOperation<T>(IAsyncResult asyncResult) where T : LazyAsyncResult
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			T t = asyncResult as T;
			if (t == null)
			{
				throw new ArgumentException("asyncResult");
			}
			if (Interlocked.Increment(ref t.endCalled) != 1)
			{
				throw new InvalidOperationException(NetException.EndAlreadyCalled);
			}
			t.InternalWaitForCompletion();
			return t;
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000B0580 File Offset: 0x000AE780
		protected bool ProtectedInvokeCallback(object value, IntPtr userToken)
		{
			if ((Interlocked.Increment(ref this.intCompleted) & 2147483647) == 1)
			{
				if (this.result == DBNull.Value)
				{
					this.result = value;
				}
				this.Complete(userToken);
				return true;
			}
			return false;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000B05B4 File Offset: 0x000AE7B4
		protected virtual void Cleanup()
		{
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000B05B8 File Offset: 0x000AE7B8
		protected virtual void Complete(IntPtr userToken)
		{
			LazyAsyncResult.ThreadContext currentThreadContext = LazyAsyncResult.CurrentThreadContext;
			try
			{
				currentThreadContext.nestedIOCount++;
				if (this.asyncCallback != null)
				{
					if (currentThreadContext.nestedIOCount >= 50)
					{
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.WorkerThreadComplete));
					}
					else
					{
						this.WorkerThreadComplete(null);
					}
				}
				else
				{
					this.WorkerThreadComplete(null);
				}
			}
			finally
			{
				currentThreadContext.nestedIOCount--;
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x000B0630 File Offset: 0x000AE830
		private static string HashString(object objectValue)
		{
			if (objectValue == null)
			{
				return "(null)";
			}
			string text = objectValue as string;
			if (!string.IsNullOrEmpty(text))
			{
				return text.GetHashCode().ToString(NumberFormatInfo.InvariantInfo);
			}
			return "(string.empty)";
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000B0670 File Offset: 0x000AE870
		private bool LazilyCreateEvent(out ManualResetEvent waitHandle)
		{
			waitHandle = new ManualResetEvent(false);
			bool flag;
			try
			{
				if (Interlocked.CompareExchange(ref this.internalEvent, waitHandle, null) == null)
				{
					if (this.InternalPeekCompleted)
					{
						waitHandle.Set();
					}
					flag = true;
				}
				else
				{
					waitHandle.Close();
					waitHandle = (ManualResetEvent)this.internalEvent;
					flag = false;
				}
			}
			catch
			{
				this.internalEvent = null;
				waitHandle.Close();
				throw;
			}
			return flag;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000B06E4 File Offset: 0x000AE8E4
		private object WaitForCompletion(bool snap)
		{
			ManualResetEvent manualResetEvent = null;
			bool flag = false;
			if (!(snap ? this.IsCompleted : this.InternalPeekCompleted))
			{
				manualResetEvent = (ManualResetEvent)this.internalEvent;
				if (manualResetEvent == null)
				{
					flag = this.LazilyCreateEvent(out manualResetEvent);
				}
			}
			if (manualResetEvent == null)
			{
				goto IL_88;
			}
			try
			{
				try
				{
					if (!manualResetEvent.WaitOne(-1, false))
					{
						throw new TimeoutException(NetException.InternalOperationFailure);
					}
				}
				catch (ObjectDisposedException)
				{
				}
				goto IL_88;
			}
			finally
			{
				if (flag && !this.userEvent)
				{
					ManualResetEvent manualResetEvent2 = (ManualResetEvent)this.internalEvent;
					this.internalEvent = null;
					if (!this.userEvent)
					{
						manualResetEvent2.Close();
					}
				}
			}
			IL_82:
			Thread.SpinWait(1);
			IL_88:
			if (this.result != DBNull.Value)
			{
				return this.result;
			}
			goto IL_82;
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000B07A8 File Offset: 0x000AE9A8
		private void WorkerThreadComplete(object state)
		{
			try
			{
				if (this.asyncCallback != null)
				{
					this.asyncCallback(this);
				}
			}
			finally
			{
				ManualResetEvent manualResetEvent = (ManualResetEvent)this.internalEvent;
				if (manualResetEvent != null)
				{
					try
					{
						manualResetEvent.Set();
					}
					catch (ObjectDisposedException)
					{
					}
				}
				this.Cleanup();
			}
		}

		// Token: 0x04003899 RID: 14489
		private const bool Trace = false;

		// Token: 0x0400389A RID: 14490
		private const int HighBit = -2147483648;

		// Token: 0x0400389B RID: 14491
		private const int ForceAsyncCount = 50;

		// Token: 0x0400389C RID: 14492
		[ThreadStatic]
		private static LazyAsyncResult.ThreadContext threadContext;

		// Token: 0x0400389D RID: 14493
		private object asyncObject;

		// Token: 0x0400389E RID: 14494
		private object asyncState;

		// Token: 0x0400389F RID: 14495
		private AsyncCallback asyncCallback;

		// Token: 0x040038A0 RID: 14496
		private object result;

		// Token: 0x040038A1 RID: 14497
		private int errorCode;

		// Token: 0x040038A2 RID: 14498
		private int intCompleted;

		// Token: 0x040038A3 RID: 14499
		private int endCalled;

		// Token: 0x040038A4 RID: 14500
		private bool userEvent;

		// Token: 0x040038A5 RID: 14501
		private object internalEvent;

		// Token: 0x02000BD9 RID: 3033
		private class ThreadContext
		{
			// Token: 0x040038A6 RID: 14502
			internal int nestedIOCount;
		}
	}
}
