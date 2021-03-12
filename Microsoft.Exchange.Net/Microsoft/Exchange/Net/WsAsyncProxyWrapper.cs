using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Services.Protocols;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200092E RID: 2350
	internal abstract class WsAsyncProxyWrapper : SoapHttpClientProtocol, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x0007BEC0 File Offset: 0x0007A0C0
		public WsAsyncProxyWrapper()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x0007BEDF File Offset: 0x0007A0DF
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<WsAsyncProxyWrapper>(this);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x0007BEE7 File Offset: 0x0007A0E7
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x0007BEFC File Offset: 0x0007A0FC
		protected override void Dispose(bool disposing)
		{
			lock (this.syncRoot)
			{
				if (!this.disposed)
				{
					this.disposed = true;
					if (disposing)
					{
						if (this.disposeTracker != null)
						{
							this.disposeTracker.Dispose();
							this.disposeTracker = null;
						}
						if (this.timer != null)
						{
							this.timer.Dispose();
							this.timer = null;
						}
					}
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x0007BF88 File Offset: 0x0007A188
		protected new IAsyncResult BeginInvoke(string methodName, object[] parameters, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result;
			lock (this.syncRoot)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("WsAsyncProxyWrapper");
				}
				if (this.asyncResult != null)
				{
					throw new InvalidOperationException("Single WsAsyncProxyWrapper does not support multiple async call in parallel!");
				}
				this.timerCallbackInvoked = false;
				this.actualCallback = callback;
				this.exceptionFromBeginInvoke = null;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = base.BeginInvoke(methodName, parameters, new AsyncCallback(this.InternalWsCallback), asyncState);
				}
				catch (IOException ex)
				{
					this.exceptionFromBeginInvoke = ex;
				}
				catch (InvalidOperationException ex2)
				{
					this.exceptionFromBeginInvoke = ex2;
				}
				if (this.exceptionFromBeginInvoke != null)
				{
					asyncResult = new LazyAsyncResult(null, asyncState, callback);
					((LazyAsyncResult)asyncResult).InvokeCallback(this.exceptionFromBeginInvoke);
				}
				else if (!asyncResult.CompletedSynchronously)
				{
					this.asyncResult = asyncResult;
					this.StartTimer(asyncResult);
				}
				result = asyncResult;
			}
			return result;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x0007C088 File Offset: 0x0007A288
		protected new object[] EndInvoke(IAsyncResult result)
		{
			if (this.timerCallbackInvoked)
			{
				throw new WebException(string.Format(CultureInfo.InvariantCulture, "Async Request to {0} timed out", new object[]
				{
					base.Url
				}), WebExceptionStatus.Timeout);
			}
			if (this.exceptionFromBeginInvoke != null)
			{
				throw this.exceptionFromBeginInvoke;
			}
			return base.EndInvoke(result);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0007C0DC File Offset: 0x0007A2DC
		private void InternalWsCallback(IAsyncResult result)
		{
			lock (this.syncRoot)
			{
				if (!this.disposed)
				{
					if (result.CompletedSynchronously || result == this.asyncResult)
					{
						this.StopTimer();
						this.asyncResult = null;
						if (this.actualCallback != null)
						{
							AsyncCallback asyncCallback = this.actualCallback;
							this.actualCallback = null;
							asyncCallback(result);
						}
					}
				}
			}
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x0007C160 File Offset: 0x0007A360
		private void InternalTimerCallback(object state)
		{
			lock (this.syncRoot)
			{
				if (!this.disposed)
				{
					this.Abort();
					this.timerCallbackInvoked = true;
					this.InternalWsCallback(state as IAsyncResult);
				}
			}
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x0007C1C0 File Offset: 0x0007A3C0
		private void StartTimer(object state)
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
			this.timer = new Timer(new TimerCallback(this.InternalTimerCallback), state, base.Timeout, -1);
			if (this.stopwatch == null)
			{
				this.stopwatch = new Stopwatch();
			}
			else
			{
				this.stopwatch.Reset();
			}
			this.stopwatch.Start();
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x0007C22A File Offset: 0x0007A42A
		private void StopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Change(-1, -1);
			}
			if (this.stopwatch != null && this.stopwatch.IsRunning)
			{
				this.stopwatch.Stop();
			}
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x0007C262 File Offset: 0x0007A462
		public long GetElapsedMilliseconds()
		{
			if (this.stopwatch != null)
			{
				return this.stopwatch.ElapsedMilliseconds;
			}
			return 0L;
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x0007C2B8 File Offset: 0x0007A4B8
		public static AsyncCallback WrapCallbackWithUnhandledExceptionHandlerAndCrash(AsyncCallback callback)
		{
			if (callback == null)
			{
				return null;
			}
			return delegate(IAsyncResult asyncResult)
			{
				try
				{
					callback(asyncResult);
				}
				catch (Exception exception)
				{
					ExWatson.SendReportAndCrashOnAnotherThread(exception);
				}
			};
		}

		// Token: 0x04002BE3 RID: 11235
		private bool timerCallbackInvoked;

		// Token: 0x04002BE4 RID: 11236
		private Timer timer;

		// Token: 0x04002BE5 RID: 11237
		private Stopwatch stopwatch;

		// Token: 0x04002BE6 RID: 11238
		private object syncRoot = new object();

		// Token: 0x04002BE7 RID: 11239
		private AsyncCallback actualCallback;

		// Token: 0x04002BE8 RID: 11240
		private IAsyncResult asyncResult;

		// Token: 0x04002BE9 RID: 11241
		private Exception exceptionFromBeginInvoke;

		// Token: 0x04002BEA RID: 11242
		private DisposeTracker disposeTracker;

		// Token: 0x04002BEB RID: 11243
		private bool disposed;
	}
}
