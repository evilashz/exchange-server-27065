using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000068 RID: 104
	internal abstract class Executable : IExecutable, IDiagnosable, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00005F50 File Offset: 0x00004150
		protected Executable(ISearchServiceConfig config)
		{
			this.config = config;
			this.diagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("Executable", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreComponentTracer, (long)this.GetHashCode());
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005FA8 File Offset: 0x000041A8
		public IDiagnosticsSession DiagnosticsSession
		{
			[DebuggerStepThrough]
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00005FB0 File Offset: 0x000041B0
		public ISearchServiceConfig Config
		{
			[DebuggerStepThrough]
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00005FB8 File Offset: 0x000041B8
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00005FD4 File Offset: 0x000041D4
		public string InstanceName
		{
			[DebuggerStepThrough]
			get
			{
				if (!string.IsNullOrEmpty(this.instanceName))
				{
					return this.instanceName;
				}
				return this.GetDiagnosticComponentName();
			}
			[DebuggerStepThrough]
			set
			{
				this.instanceName = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00005FDD File Offset: 0x000041DD
		public ICancelableAsyncResult AsyncResult
		{
			[DebuggerStepThrough]
			get
			{
				return this.executeAsyncResult;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00005FE5 File Offset: 0x000041E5
		public WaitHandle StopEvent
		{
			[DebuggerStepThrough]
			get
			{
				return this.stopEvent;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00005FED File Offset: 0x000041ED
		public bool Stopping
		{
			[DebuggerStepThrough]
			get
			{
				return this.stopping;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00005FF8 File Offset: 0x000041F8
		public IAsyncResult BeginExecute(AsyncCallback callback, object state)
		{
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new LazyAsyncResultWithTimeout(callback, state, new AsyncCallback(this.ExecuteComplete));
			if (Interlocked.CompareExchange<LazyAsyncResultWithTimeout>(ref this.executeAsyncResult, lazyAsyncResultWithTimeout, null) != null)
			{
				lazyAsyncResultWithTimeout.InvokeCallback(new InvalidOperationException("Only one execution allowed."));
				return lazyAsyncResultWithTimeout;
			}
			ThreadPool.QueueUserWorkItem(CallbackWrapper.WaitCallback(new WaitCallback(this.InternalExecutionStart)));
			return this.executeAsyncResult;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006058 File Offset: 0x00004258
		public void EndExecute(IAsyncResult asyncResult)
		{
			if (this.executeAsyncResult == null)
			{
				throw new InvalidOperationException("BeginExecute must be called before EndExecute.");
			}
			if (!object.ReferenceEquals(this.executeAsyncResult, asyncResult))
			{
				throw new InvalidOperationException("Passed in IAsyncResult does not match outstanding IASyncResult for this Executable.");
			}
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = LazyAsyncResult.EndAsyncOperation<LazyAsyncResultWithTimeout>(asyncResult);
			if (lazyAsyncResultWithTimeout.IsCanceled)
			{
				this.diagnosticsSession.TraceDebug("Execution complete. (Canceled)", new object[0]);
				return;
			}
			if (lazyAsyncResultWithTimeout.Result == null)
			{
				this.diagnosticsSession.TraceDebug("Execution complete.", new object[0]);
				return;
			}
			if (lazyAsyncResultWithTimeout.Result is Exception)
			{
				this.diagnosticsSession.TraceError("Execution complete with exception: {0}", new object[]
				{
					lazyAsyncResultWithTimeout.Result
				});
				throw new OperationFailedException((Exception)lazyAsyncResultWithTimeout.Result);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006118 File Offset: 0x00004318
		public void CancelExecute()
		{
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = new LazyAsyncResultWithTimeout(null, null, null);
			lazyAsyncResultWithTimeout.Cancel();
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout2 = Interlocked.CompareExchange<LazyAsyncResultWithTimeout>(ref this.executeAsyncResult, lazyAsyncResultWithTimeout, null);
			if (lazyAsyncResultWithTimeout2 != null)
			{
				lazyAsyncResultWithTimeout2.Cancel();
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000614B File Offset: 0x0000434B
		public string GetDiagnosticComponentName()
		{
			return base.GetType().Name;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006158 File Offset: 0x00004358
		public virtual XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			if (this.executeAsyncResult != null && this.executeAsyncResult.Result != null)
			{
				xelement.Add(new XElement("Error", this.executeAsyncResult.Result));
			}
			return xelement;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000061AC File Offset: 0x000043AC
		public void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000061BB File Offset: 0x000043BB
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600026F RID: 623
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000270 RID: 624
		protected abstract void InternalExecutionStart();

		// Token: 0x06000271 RID: 625
		protected abstract void InternalExecutionFinish();

		// Token: 0x06000272 RID: 626 RVA: 0x000061D0 File Offset: 0x000043D0
		protected virtual void Dispose(bool calledFromDispose)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000061F0 File Offset: 0x000043F0
		protected bool TryRunUnderExceptionHandler<TReturnValue>(Func<TReturnValue> action, out TReturnValue returnValue, LocalizedString message)
		{
			TReturnValue tempReturnValue = default(TReturnValue);
			bool result = this.TryRunUnderExceptionHandler(delegate()
			{
				tempReturnValue = action();
			}, message);
			returnValue = tempReturnValue;
			return result;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006238 File Offset: 0x00004438
		protected bool TryRunUnderExceptionHandler(Action action, LocalizedString message)
		{
			ComponentFailedException value = null;
			try
			{
				action();
				return true;
			}
			catch (ComponentFailedPermanentException innerException)
			{
				value = new ComponentFailedPermanentException(message, innerException);
			}
			catch (ComponentFailedTransientException innerException2)
			{
				value = new ComponentFailedTransientException(message, innerException2);
			}
			catch (OperationFailedException innerException3)
			{
				value = new ComponentFailedTransientException(message, innerException3);
			}
			catch (Exception ex)
			{
				this.diagnosticsSession.SendWatsonReport(ex);
				value = new ComponentFailedPermanentException(message, ex);
			}
			this.executeAsyncResult.InvokeCallback(value);
			return false;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000062D4 File Offset: 0x000044D4
		protected void CompleteExecute(object result)
		{
			this.DiagnosticsSession.TraceDebug("CompleteExecute: {0}", new object[]
			{
				result ?? "Success"
			});
			this.executeAsyncResult.InvokeCallback(result);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006313 File Offset: 0x00004513
		protected XElement BuildDiagnosticsErrorNode(string reason)
		{
			this.diagnosticsSession.TraceError<string>("Error executing Diagnostics command: {0}", reason);
			return new XElement("Error", reason);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006354 File Offset: 0x00004554
		private void ExecuteComplete(IAsyncResult passedAsyncResult)
		{
			this.stopping = true;
			this.stopEvent.Set();
			this.InternalExecutionFinish();
			LazyAsyncResultWithTimeout lazyAsyncResultWithTimeout = (LazyAsyncResultWithTimeout)passedAsyncResult;
			AsyncCallback userCallback = (AsyncCallback)lazyAsyncResultWithTimeout.AsyncObject;
			if (userCallback != null)
			{
				ThreadPool.QueueUserWorkItem(CallbackWrapper.WaitCallback(delegate(object ar)
				{
					userCallback((IAsyncResult)ar);
				}), lazyAsyncResultWithTimeout);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000063C0 File Offset: 0x000045C0
		private void InternalExecutionStart(object state)
		{
			try
			{
				this.InternalExecutionStart();
			}
			catch (Exception result)
			{
				this.CompleteExecute(result);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000063F0 File Offset: 0x000045F0
		private void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.AsyncResult != null && !this.AsyncResult.IsCompleted)
			{
				this.CompleteExecute(null);
				this.AsyncResult.AsyncWaitHandle.WaitOne(this.Config.MaxOperationTimeout);
			}
			this.Dispose(calledFromDispose);
			if (calledFromDispose)
			{
				this.stopEvent.Dispose();
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
		}

		// Token: 0x04000104 RID: 260
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000105 RID: 261
		private readonly ISearchServiceConfig config;

		// Token: 0x04000106 RID: 262
		private DisposeTracker disposeTracker;

		// Token: 0x04000107 RID: 263
		private string instanceName;

		// Token: 0x04000108 RID: 264
		private LazyAsyncResultWithTimeout executeAsyncResult;

		// Token: 0x04000109 RID: 265
		private ManualResetEvent stopEvent = new ManualResetEvent(false);

		// Token: 0x0400010A RID: 266
		private bool stopping;
	}
}
