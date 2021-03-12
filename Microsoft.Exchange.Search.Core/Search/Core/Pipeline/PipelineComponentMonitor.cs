using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.AsyncTask;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000AF RID: 175
	internal sealed class PipelineComponentMonitor : StartStopComponent, INotifyFailed
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00011348 File Offset: 0x0000F548
		static PipelineComponentMonitor()
		{
			ComponentRegistry.Register<PipelineComponentMonitor>();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00011350 File Offset: 0x0000F550
		internal PipelineComponentMonitor(PipelineComponentList components)
		{
			Util.ThrowOnNullArgument(components, "components");
			base.DiagnosticsSession.ComponentName = "PipelineComponentMonitor";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CorePipelineTracer;
			if (components.HasStartStopComponent)
			{
				List<IStartStopPipelineComponent> list = new List<IStartStopPipelineComponent>(components.Count);
				for (int i = 0; i < components.Count; i++)
				{
					IPipelineComponent pipelineComponent = components[i];
					if (pipelineComponent is IStartStopPipelineComponent)
					{
						list.Add((IStartStopPipelineComponent)pipelineComponent);
					}
				}
				base.DiagnosticsSession.TraceDebug<int>("Creating failure monitor to watch on {0} components that support start stop", list.Count);
				this.failureMonitor = new FailureMonitor(list.ToArray());
			}
			this.components = components;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00011400 File Offset: 0x0000F600
		protected sealed override void AtPrepareToStart(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug("Preparing to start PipelineComponentMonitor", new object[0]);
			if (this.failureMonitor != null)
			{
				this.pendingAsyncResult = asyncResult;
				base.DiagnosticsSession.TraceDebug("Preparing to start failure monitor", new object[0]);
				new AsyncPrepareToStart(this.failureMonitor).Execute(new TaskCompleteCallback(this.AtPrepareToStartCallback));
				return;
			}
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is prepared to start.", new object[0]);
			base.AtPrepareToStart(asyncResult);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00011484 File Offset: 0x0000F684
		protected sealed override void AtStart(AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug("Starting to hook failure on components for PipelineComponentMonitor", new object[0]);
			for (int i = 0; i < this.components.Count; i++)
			{
				IPipelineComponent pipelineComponent = this.components[i];
			}
			if (this.failureMonitor != null)
			{
				this.pendingAsyncResult = asyncResult;
				base.DiagnosticsSession.TraceDebug("Starting failure monitor", new object[0]);
				new AsyncStart(this.failureMonitor).Execute(new TaskCompleteCallback(this.AtStartCallback));
				return;
			}
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is started.", new object[0]);
			base.AtStart(asyncResult);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00011529 File Offset: 0x0000F729
		protected sealed override void AtStop(AsyncResult asyncResult)
		{
			this.UnhookComponents();
			this.InternalStop(asyncResult);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00011538 File Offset: 0x0000F738
		protected override void AtFail(ComponentFailedException reason)
		{
			this.UnhookComponents();
			base.AtFail(reason);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00011547 File Offset: 0x0000F747
		protected override void AtStopInFailed(AsyncResult asyncResult)
		{
			this.InternalStop(asyncResult);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00011550 File Offset: 0x0000F750
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.DisposeChildren();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00011562 File Offset: 0x0000F762
		private void DisposeChildren()
		{
			if (this.failureMonitor != null)
			{
				this.failureMonitor.Dispose();
				this.failureMonitor = null;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011580 File Offset: 0x0000F780
		private void UnhookComponents()
		{
			base.DiagnosticsSession.TraceDebug("Stopping hooking failure on components for PipelineComponentMonitor", new object[0]);
			for (int i = 0; i < this.components.Count; i++)
			{
				IPipelineComponent pipelineComponent = this.components[i];
			}
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000115C8 File Offset: 0x0000F7C8
		private void InternalStop(AsyncResult asyncResult)
		{
			if (this.failureMonitor != null)
			{
				this.pendingAsyncResult = asyncResult;
				base.DiagnosticsSession.TraceDebug("Stopping failure monitor", new object[0]);
				new AsyncStop(this.failureMonitor).Execute(new TaskCompleteCallback(this.AtStopCallback));
				return;
			}
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is stopped.", new object[0]);
			base.AtStop(asyncResult);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00011634 File Offset: 0x0000F834
		private void AtPipelineComponentFailed(IPipelineComponent component, ComponentFailedException reason)
		{
			base.DiagnosticsSession.TraceDebug<IPipelineComponent, ComponentFailedException>("Component {0} is failed with reason {1}.", component, reason);
			int num = this.components.IndexOf(component);
			base.DiagnosticsSession.TraceDebug<int>("Recreating a component and replace it at index of {0}.", num);
			this.components.Recreate(num);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00011680 File Offset: 0x0000F880
		private void PipelineComponentFailed(object sender, FailedEventArgs e)
		{
			if (sender is IStartStopPipelineComponent)
			{
				base.DiagnosticsSession.TraceDebug<object, ComponentFailedException>("Defer to failure monitor to revive the component {0} that failed with exception {1}", sender, e.Reason);
				return;
			}
			this.BeginDispatchPipelineComponentFailedSignal((IPipelineComponent)sender, e.Reason, new AsyncCallback(this.EndDispatchPipelineComponentFailedSignal), null);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000116CD File Offset: 0x0000F8CD
		private void AtPrepareToStartCallback(AsyncTask asyncTask)
		{
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is prepared to start.", new object[0]);
			if (asyncTask.Exception != null)
			{
				this.pendingAsyncResult.SetAsCompleted(asyncTask.Exception);
				return;
			}
			base.AtPrepareToStart(this.pendingAsyncResult);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001170B File Offset: 0x0000F90B
		private void AtStartCallback(AsyncTask asyncTask)
		{
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is started.", new object[0]);
			if (asyncTask.Exception != null)
			{
				this.pendingAsyncResult.SetAsCompleted(asyncTask.Exception);
				return;
			}
			base.AtStart(this.pendingAsyncResult);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00011749 File Offset: 0x0000F949
		private void AtStopCallback(AsyncTask asyncTask)
		{
			base.DiagnosticsSession.TraceDebug("PipelineComponentMonitor is stopped.", new object[0]);
			if (asyncTask.Exception != null)
			{
				this.pendingAsyncResult.SetAsCompleted(asyncTask.Exception);
				return;
			}
			base.AtStop(this.pendingAsyncResult);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00011788 File Offset: 0x0000F988
		private void OnFailureMonitorFailed(object sender, FailedEventArgs e)
		{
			ComponentFailedException reason = e.Reason;
			base.DiagnosticsSession.TraceDebug<ComponentFailedException>("The failure monitor failed to revive component. Exception=", reason);
			base.BeginDispatchFailSignal(reason, new AsyncCallback(base.EndDispatchFailSignal), null);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000117C2 File Offset: 0x0000F9C2
		private static void RegisterComponent(ComponentInfo componentInfo)
		{
			componentInfo.RegisterSignal(PipelineComponentMonitor.Signal.PipelineComponentFailed, SignalPriority.Medium);
			componentInfo.RegisterTransition(6U, 9U, 6U, null, new ActionMethod(PipelineComponentMonitor.Transition_AtPipelineComponentFailed));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000117E9 File Offset: 0x0000F9E9
		internal static void Transition_AtPipelineComponentFailed(object component, params object[] args)
		{
			((PipelineComponentMonitor)component).AtPipelineComponentFailed((IPipelineComponent)args[0], (ComponentFailedException)args[1]);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00011808 File Offset: 0x0000FA08
		internal IAsyncResult BeginDispatchPipelineComponentFailedSignal(IPipelineComponent component, ComponentFailedException reason, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 9U, callback, context, TimeSpan.Zero, new object[]
			{
				component,
				reason
			});
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011838 File Offset: 0x0000FA38
		internal IAsyncResult BeginDispatchPipelineComponentFailedSignal(IPipelineComponent component, ComponentFailedException reason, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 9U, callback, context, delayInTimespan, new object[]
			{
				component,
				reason
			});
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011870 File Offset: 0x0000FA70
		internal void EndDispatchPipelineComponentFailedSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x04000273 RID: 627
		private readonly PipelineComponentList components;

		// Token: 0x04000274 RID: 628
		private FailureMonitor failureMonitor;

		// Token: 0x04000275 RID: 629
		private AsyncResult pendingAsyncResult;

		// Token: 0x020000B0 RID: 176
		internal new enum Signal : uint
		{
			// Token: 0x04000277 RID: 631
			PipelineComponentFailed = 9U,
			// Token: 0x04000278 RID: 632
			Max
		}

		// Token: 0x020000B1 RID: 177
		internal new enum State : uint
		{
			// Token: 0x0400027A RID: 634
			Max = 10U
		}
	}
}
