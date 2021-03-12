using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D3 RID: 211
	internal class NonBlockingReader : DisposableBase
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001AE06 File Offset: 0x00019006
		internal NonBlockingReader(NonBlockingReader.Operation operation, object state, TimeSpan timeout, NonBlockingReader.TimeoutCallback timeOutCallback)
		{
			this.operation = operation;
			this.userState = state;
			this.timeout = timeout;
			this.timeoutCallback = timeOutCallback;
			this.evt = new ManualResetEvent(false);
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0001AE37 File Offset: 0x00019037
		internal bool TimeOutExpired
		{
			get
			{
				return this.timedOut;
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001AE40 File Offset: 0x00019040
		internal void StartAsyncOperation()
		{
			base.CheckDisposed();
			if (!this.forciblyCompleted)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "NonBlockingReader starting async operation because it has not been forcibly completed", new object[0]);
				DisposableBase disposableBase = this.userState as DisposableBase;
				if (disposableBase != null)
				{
					disposableBase.AddReference();
				}
				base.AddReference();
				this.operation.BeginInvoke(this.userState, new AsyncCallback(this.CompleteOperation), null);
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001AEAC File Offset: 0x000190AC
		internal bool WaitForCompletion()
		{
			base.CheckDisposed();
			bool flag = false;
			if (!this.timedOut)
			{
				flag = this.evt.WaitOne(this.timeout, false);
				if (!flag)
				{
					this.timedOut = true;
					if (this.timeoutCallback != null)
					{
						this.timeoutCallback(this.userState);
					}
				}
			}
			return flag;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001AF00 File Offset: 0x00019100
		internal void ForceCompletion()
		{
			base.CheckDisposed();
			this.forciblyCompleted = true;
			this.evt.Set();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001AF1B File Offset: 0x0001911B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.evt.Close();
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001AF2B File Offset: 0x0001912B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<NonBlockingReader>(this);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001AF34 File Offset: 0x00019134
		private void CompleteOperation(IAsyncResult r)
		{
			try
			{
				this.operation.EndInvoke(r);
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UtilTracer, this, "NonBlockingReader encountered an exception in the user callback '{0}'", new object[]
				{
					ex
				});
				if (!GrayException.IsGrayException(ex))
				{
					throw;
				}
			}
			finally
			{
				this.evt.Set();
				DisposableBase disposableBase = this.userState as DisposableBase;
				if (disposableBase != null)
				{
					disposableBase.ReleaseReference();
				}
				base.ReleaseReference();
			}
		}

		// Token: 0x04000407 RID: 1031
		private bool timedOut;

		// Token: 0x04000408 RID: 1032
		private ManualResetEvent evt;

		// Token: 0x04000409 RID: 1033
		private NonBlockingReader.Operation operation;

		// Token: 0x0400040A RID: 1034
		private NonBlockingReader.TimeoutCallback timeoutCallback;

		// Token: 0x0400040B RID: 1035
		private object userState;

		// Token: 0x0400040C RID: 1036
		private TimeSpan timeout;

		// Token: 0x0400040D RID: 1037
		private bool forciblyCompleted;

		// Token: 0x020000D4 RID: 212
		// (Invoke) Token: 0x060006FB RID: 1787
		internal delegate void TimeoutCallback(object state);

		// Token: 0x020000D5 RID: 213
		// (Invoke) Token: 0x060006FF RID: 1791
		internal delegate void Operation(object state);
	}
}
