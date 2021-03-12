using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.EventLog;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200006A RID: 106
	internal sealed class FailureMonitor : StartStopComponent, INotifyFailed
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00006467 File Offset: 0x00004667
		static FailureMonitor()
		{
			ComponentRegistry.Register<FailureMonitor>();
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006481 File Offset: 0x00004681
		internal FailureMonitor(params INotifyFailed[] initialComponents) : this(FailureMonitor.DefaultReviveWait, initialComponents)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000648F File Offset: 0x0000468F
		internal FailureMonitor(TimeSpan reviveDueTime, params INotifyFailed[] initialComponents) : this(reviveDueTime, DeferredStarter.NoRetryPeriod, initialComponents)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000064A0 File Offset: 0x000046A0
		internal FailureMonitor(TimeSpan reviveDueTime, TimeSpan retryPeriod, params INotifyFailed[] initialComponents)
		{
			base.DiagnosticsSession.ComponentName = "FailureMonitor";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CoreFailureMonitorTracer;
			this.reviveDueTime = reviveDueTime;
			this.retryPeriod = retryPeriod;
			this.isRetryMode = (retryPeriod != DeferredStarter.NoRetryPeriod);
			this.initialComponents = initialComponents;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006514 File Offset: 0x00004714
		public void AttachComponent(INotifyFailed component, bool startNow)
		{
			base.CheckDisposed();
			base.DiagnosticsSession.TraceDebug<INotifyFailed>("Attaching {0} to be monitored", component);
			lock (this.lockObject)
			{
				if (this.monitoredComponents == null)
				{
					throw new InvalidOperationException("Cannot attach component before monitoredComponents is initialized.");
				}
				this.monitoredComponents.Add(component);
			}
			if (startNow)
			{
				this.InternalReviveComponent((IStartStop)component, null);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00006598 File Offset: 0x00004798
		public void DetachComponent(INotifyFailed component)
		{
			base.CheckDisposed();
			base.DiagnosticsSession.TraceDebug<INotifyFailed>("Detaching {0} from being monitored", component);
			lock (this.lockObject)
			{
				if (this.monitoredComponents == null)
				{
					throw new InvalidOperationException("Cannot detach component before monitoredComponents is initialized.");
				}
				this.monitoredComponents.Remove(component);
				this.DisposeStarterNoLock((IStartStop)component);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000661C File Offset: 0x0000481C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.componentStarters != null)
			{
				foreach (DeferredStarter deferredStarter in this.componentStarters.Values)
				{
					deferredStarter.Dispose();
				}
				this.componentStarters.Clear();
				this.componentStarters = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006698 File Offset: 0x00004898
		protected sealed override void AtStart(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug("Starting hook failure on components", new object[0]);
			base.DiagnosticsSession.Assert(this.monitoredComponents == null, "monitoredComponents must be null", new object[0]);
			this.monitoredComponents = new List<INotifyFailed>(10);
			if (this.initialComponents != null)
			{
				foreach (INotifyFailed component in this.initialComponents)
				{
					this.AttachComponent(component, false);
				}
			}
			base.AtStart(asyncResult);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000673C File Offset: 0x0000493C
		protected sealed override void AtStop(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug("AtStop: Stopping hook failure on components", new object[0]);
			if (this.monitoredComponents != null)
			{
				this.DetachAllComponents();
			}
			base.AtStop(asyncResult);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000676B File Offset: 0x0000496B
		protected override void AtFail(ComponentFailedException reason)
		{
			base.DiagnosticsSession.TraceDebug("AtFail: Stopping hook failure on components", new object[0]);
			if (this.monitoredComponents != null)
			{
				this.DetachAllComponents();
			}
			base.AtFail(reason);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000685C File Offset: 0x00004A5C
		private void InternalReviveComponent(IStartStop component, ComponentFailedException error)
		{
			base.CheckDisposed();
			TimeSpan dueTime = (error == null) ? TimeSpan.Zero : this.reviveDueTime;
			base.DiagnosticsSession.TraceDebug<IStartStop, double, object>("Reviving {0} in {1} ms due to {2}", component, dueTime.TotalMilliseconds, (error == null) ? "requested" : error);
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = this.lockObject, ref flag);
				if (this.monitoredComponents == null || !this.monitoredComponents.Contains((INotifyFailed)component))
				{
					base.DiagnosticsSession.TraceDebug<IStartStop>("Skip reviving {0} which was not attached.", component);
				}
				else
				{
					this.DisposeStarterNoLock(component);
					DeferredStarter starter = new DeferredStarter(component, dueTime, this.retryPeriod);
					this.componentStarters.Add(component, starter);
					starter.BeginInvoke(delegate(IAsyncResult ar)
					{
						ComponentException ex = null;
						try
						{
							starter.EndInvoke(ar);
						}
						catch (ComponentException ex2)
						{
							ex = ex2;
						}
						this.DiagnosticsSession.TraceDebug<IStartStop, object>("Reviving {0} is done. Error = {1}", component, (ex == null) ? "none" : error);
						if (ex != null && !this.isRetryMode)
						{
							this.BeginDispatchFailSignal(new ComponentFailedPermanentException(ex), new AsyncCallback(this.EndDispatchFailSignal), null);
						}
					}, null);
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000069A0 File Offset: 0x00004BA0
		private void DetachAllComponents()
		{
			base.CheckDisposed();
			lock (this.lockObject)
			{
				List<INotifyFailed> list = new List<INotifyFailed>(this.monitoredComponents.Count);
				list.AddRange(this.monitoredComponents);
				foreach (INotifyFailed component in list)
				{
					this.DetachComponent(component);
				}
				this.monitoredComponents = null;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00006A48 File Offset: 0x00004C48
		private void DisposeStarterNoLock(IStartStop component)
		{
			DeferredStarter deferredStarter;
			if (this.componentStarters.TryGetValue(component, out deferredStarter))
			{
				try
				{
					deferredStarter.Cancel();
				}
				finally
				{
					deferredStarter.Dispose();
					this.componentStarters.Remove(component);
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00006A94 File Offset: 0x00004C94
		private void ComponentFailed(object sender, FailedEventArgs e)
		{
			base.CheckDisposed();
			IDiagnosable diagnosable = sender as IDiagnosable;
			base.DiagnosticsSession.LogPeriodicEvent(MSExchangeFastSearchEventLogConstants.Tuple_ComponentFailed, (diagnosable == null) ? "DefaultComponentFailedEventPeriodKey" : diagnosable.GetDiagnosticComponentName(), new object[]
			{
				e.Reason
			});
			if (this.isRetryMode || e.Reason is ComponentFailedTransientException)
			{
				this.InternalReviveComponent((IStartStop)sender, e.Reason);
				return;
			}
			if (e.Reason is ComponentFailedPermanentException)
			{
				base.BeginDispatchFailSignal(e.Reason, new AsyncCallback(base.EndDispatchFailSignal), null);
				return;
			}
			base.DiagnosticsSession.Assert(false, "Unexpected exception type.", new object[0]);
		}

		// Token: 0x04000110 RID: 272
		private const string DefaultComponentFailedEventPeriodKey = "DefaultComponentFailedEventPeriodKey";

		// Token: 0x04000111 RID: 273
		private static readonly TimeSpan DefaultReviveWait = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000112 RID: 274
		private readonly TimeSpan reviveDueTime;

		// Token: 0x04000113 RID: 275
		private readonly TimeSpan retryPeriod;

		// Token: 0x04000114 RID: 276
		private readonly bool isRetryMode;

		// Token: 0x04000115 RID: 277
		private readonly IEnumerable<INotifyFailed> initialComponents;

		// Token: 0x04000116 RID: 278
		private readonly object lockObject = new object();

		// Token: 0x04000117 RID: 279
		private volatile List<INotifyFailed> monitoredComponents;

		// Token: 0x04000118 RID: 280
		private Dictionary<IStartStop, DeferredStarter> componentStarters = new Dictionary<IStartStop, DeferredStarter>(10);
	}
}
