using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.AsyncTask;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000065 RID: 101
	internal sealed class DeferredStarter : Disposable
	{
		// Token: 0x06000248 RID: 584 RVA: 0x000058F7 File Offset: 0x00003AF7
		internal DeferredStarter(IStartStop component, TimeSpan dueTime) : this(component, dueTime, DeferredStarter.NoRetryPeriod)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005908 File Offset: 0x00003B08
		internal DeferredStarter(IStartStop component, TimeSpan dueTime, TimeSpan retryPeriod)
		{
			Util.ThrowOnNullArgument(component, "component");
			this.component = component;
			this.dueTime = dueTime;
			this.retryPeriod = retryPeriod;
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("DeferredStarter", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreGeneralTracer, (long)this.GetHashCode());
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00005978 File Offset: 0x00003B78
		internal IStartStop Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005980 File Offset: 0x00003B80
		public IAsyncResult BeginInvoke(AsyncCallback callback, object context)
		{
			base.CheckDisposed();
			this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::BeginInvoke: Invoking start of component {0}", this.component);
			if (this.asyncTaskStarter == null)
			{
				this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::BeginInvoke: Create AsyncTaskSequence for component {0}", this.component);
				AsyncTaskSequence value = new AsyncTaskSequence(new AsyncTask[]
				{
					new AsyncStop(this.component),
					new AsyncPrepareToStart(this.component),
					new AsyncStart(this.component)
				});
				if (Interlocked.CompareExchange<AsyncTaskSequence>(ref this.asyncTaskStarter, value, null) != null)
				{
					throw new InvalidOperationException("Another thread has invoked.");
				}
			}
			AsyncResult asyncResult = new AsyncResult(callback, context);
			lock (this.cancelLocker)
			{
				if (this.isCancel)
				{
					asyncResult.SetAsCompleted();
				}
				else if (this.dueTime == TimeSpan.Zero)
				{
					this.InternalBeginInvoke(asyncResult);
				}
				else
				{
					this.diagnosticsSession.TraceDebug<IStartStop, double>("DeferredStarter::BeginInvoke: Deferred start of component {0} in {1} ms.", this.component, this.dueTime.TotalMilliseconds);
					RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(this.cancelEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.DeferredBeginInvoke)), asyncResult, this.dueTime, true);
					this.isDeferredInvokePending = true;
				}
			}
			return asyncResult;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public void EndInvoke(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public void Cancel()
		{
			base.CheckDisposed();
			this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::Cancel: Cancelling the start of component: {0}.", this.component);
			lock (this.cancelLocker)
			{
				if (this.isCancel)
				{
					throw new InvalidOperationException("Cannot cancel the starter more than once");
				}
				this.isCancel = true;
				this.cancelEvent.Set();
			}
			SpinWait spinWait = default(SpinWait);
			while (this.isDeferredInvokePending)
			{
				spinWait.SpinOnce();
			}
			if (this.asyncTaskStarter != null)
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					this.asyncTaskStarter.Cancel(manualResetEvent);
					manualResetEvent.WaitOne();
				}
			}
			this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::Cancel: Cancelled the start of component {0}.", this.component);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00005BCC File Offset: 0x00003DCC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DeferredStarter>(this);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00005BD4 File Offset: 0x00003DD4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.cancelEvent != null)
			{
				this.cancelEvent.Close();
				this.cancelEvent = null;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005D78 File Offset: 0x00003F78
		private void InternalBeginInvoke(AsyncResult asyncResult)
		{
			this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::InternalBeginInvoke for component {0}.", this.component);
			this.asyncTaskStarter.Execute(delegate(AsyncTask task)
			{
				this.CheckDisposed();
				if (!task.Cancelled && task.Exception != null)
				{
					this.diagnosticsSession.TraceDebug<IStartStop, ComponentException>("DeferredStarter::InternalBeginInvoke: failed to start component {0} due to {1}.", this.component, task.Exception);
					if (this.retryPeriod != DeferredStarter.NoRetryPeriod)
					{
						this.diagnosticsSession.TraceDebug<IStartStop, double>("DeferredStarter::InternalBeginInvoke: Restart of component {0} in {1} ms.", this.component, this.retryPeriod.TotalMilliseconds);
						lock (this.cancelLocker)
						{
							if (!this.isCancel)
							{
								this.diagnosticsSession.TraceDebug<IStartStop, double>("DeferredStarter::InternalBeginInvoke: Deferred start of component {0} in {1} ms.", this.component, this.retryPeriod.TotalMilliseconds);
								RegisteredWaitHandleWrapper.RegisterWaitForSingleObject(this.cancelEvent, CallbackWrapper.WaitOrTimerCallback(new WaitOrTimerCallback(this.DeferredBeginInvoke)), asyncResult, this.retryPeriod, true);
								this.isDeferredInvokePending = true;
							}
							return;
						}
					}
					asyncResult.SetAsCompleted(new ComponentFailedPermanentException(task.Exception));
					return;
				}
				asyncResult.SetAsCompleted();
			});
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00005DC8 File Offset: 0x00003FC8
		private void DeferredBeginInvoke(object state, bool timedOut)
		{
			base.CheckDisposed();
			try
			{
				AsyncResult asyncResult = (AsyncResult)state;
				if (timedOut)
				{
					this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::DeferredBeginInvoke: Time out for component {0}.", this.component);
					this.InternalBeginInvoke(asyncResult);
				}
				else
				{
					this.diagnosticsSession.TraceDebug<IStartStop>("DeferredStarter::DeferredBeginInvoke: Cancelled for component {0}.", this.component);
					asyncResult.SetAsCompleted();
				}
			}
			finally
			{
				this.isDeferredInvokePending = false;
			}
		}

		// Token: 0x040000DF RID: 223
		internal static readonly TimeSpan NoRetryPeriod = TimeSpan.Zero;

		// Token: 0x040000E0 RID: 224
		private readonly IStartStop component;

		// Token: 0x040000E1 RID: 225
		private readonly TimeSpan dueTime;

		// Token: 0x040000E2 RID: 226
		private readonly TimeSpan retryPeriod;

		// Token: 0x040000E3 RID: 227
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000E4 RID: 228
		private readonly object cancelLocker = new object();

		// Token: 0x040000E5 RID: 229
		private AsyncTaskSequence asyncTaskStarter;

		// Token: 0x040000E6 RID: 230
		private volatile bool isDeferredInvokePending;

		// Token: 0x040000E7 RID: 231
		private ManualResetEvent cancelEvent = new ManualResetEvent(false);

		// Token: 0x040000E8 RID: 232
		private bool isCancel;
	}
}
