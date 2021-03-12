using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200099B RID: 2459
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class CallbackHandler : IDisposeTrackable, IDisposable
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x00086DF4 File Offset: 0x00084FF4
		internal CallbackHandler()
		{
			this.resetEvent = new AutoResetEvent(false);
			this.callbackDelegate = new CallbackDelegate(this.OnStatus);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x00086E26 File Offset: 0x00085026
		internal CallbackDelegate CallbackDelegate
		{
			get
			{
				return this.callbackDelegate;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06003527 RID: 13607 RVA: 0x00086E2E File Offset: 0x0008502E
		internal string CallbackData
		{
			get
			{
				return this.callbackData;
			}
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x00086E36 File Offset: 0x00085036
		internal void WaitForCompletion()
		{
			this.resetEvent.WaitOne();
			if (this.exception != null)
			{
				throw this.exception;
			}
			Errors.ThrowOnErrorCode(this.hr);
		}

		// Token: 0x06003529 RID: 13609 RVA: 0x00086E60 File Offset: 0x00085060
		internal bool WaitForCompletion(TimeSpan timeOut)
		{
			bool result = this.resetEvent.WaitOne(timeOut);
			if (this.exception != null)
			{
				throw this.exception;
			}
			Errors.ThrowOnErrorCode(this.hr);
			return result;
		}

		// Token: 0x0600352A RID: 13610 RVA: 0x00086E95 File Offset: 0x00085095
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x00086EB8 File Offset: 0x000850B8
		private int OnStatus(StatusMessage status, int hr, IntPtr pvParam, IntPtr pvContext)
		{
			if (hr == 315140 || hr < 0)
			{
				this.exception = null;
				try
				{
					this.hr = hr;
					if (pvParam != IntPtr.Zero)
					{
						this.callbackData = Marshal.PtrToStringUni(pvParam);
					}
				}
				catch (Exception ex)
				{
					this.exception = ex;
				}
				finally
				{
					this.resetEvent.Set();
				}
			}
			return 0;
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x00086F30 File Offset: 0x00085130
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CallbackHandler>(this);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x00086F38 File Offset: 0x00085138
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x00086F4D File Offset: 0x0008514D
		private void Dispose(bool disposing)
		{
			if (!this.disposed && disposing && this.resetEvent != null)
			{
				this.resetEvent.Set();
				((IDisposable)this.resetEvent).Dispose();
				this.resetEvent = null;
			}
			this.disposed = true;
		}

		// Token: 0x04002D97 RID: 11671
		private CallbackDelegate callbackDelegate;

		// Token: 0x04002D98 RID: 11672
		private AutoResetEvent resetEvent;

		// Token: 0x04002D99 RID: 11673
		private string callbackData;

		// Token: 0x04002D9A RID: 11674
		private int hr;

		// Token: 0x04002D9B RID: 11675
		private Exception exception;

		// Token: 0x04002D9C RID: 11676
		private DisposeTracker disposeTracker;

		// Token: 0x04002D9D RID: 11677
		private bool disposed;
	}
}
