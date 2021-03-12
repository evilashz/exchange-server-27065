using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.AsyncTask;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000B3 RID: 179
	internal abstract class PipelineConnectorBase : ContainerComponent, IPipelineConnector, IStartStop, IDisposable, IDiagnosable
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x000118A2 File Offset: 0x0000FAA2
		protected PipelineConnectorBase()
		{
			base.DiagnosticsSession.ComponentName = "PipelineConnectorBase";
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CorePipelineTracer;
		}

		// Token: 0x0600056C RID: 1388
		protected abstract void CreateFeeding(IPipeline pipeline);

		// Token: 0x0600056D RID: 1389
		protected abstract void DisposeFeeding();

		// Token: 0x0600056E RID: 1390
		protected abstract AsyncTask PrepareFeeding(IPipeline pipeline);

		// Token: 0x0600056F RID: 1391
		protected abstract AsyncTask StartFeeding();

		// Token: 0x06000570 RID: 1392
		protected abstract AsyncTask StopFeeding();

		// Token: 0x06000571 RID: 1393
		protected abstract Pipeline CreatePipeline();

		// Token: 0x06000572 RID: 1394 RVA: 0x000118CA File Offset: 0x0000FACA
		protected sealed override void CreateChildren()
		{
			base.DiagnosticsSession.TraceDebug("Creating pipeline and feeding.", new object[0]);
			this.pipeline = this.CreatePipeline();
			base.AddComponent(this.pipeline);
			this.CreateFeeding(this.pipeline);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00011914 File Offset: 0x0000FB14
		protected sealed override void PrepareToStartChildrenAsync()
		{
			AsyncTask asyncTask2 = this.PrepareFeeding(this.pipeline);
			base.DiagnosticsSession.TraceDebug("Preparing to start pipeline and feeding in parallel.", new object[0]);
			new AsyncTaskParallel(new AsyncTask[]
			{
				new AsyncPrepareToStart(this.pipeline),
				asyncTask2
			}).Execute(delegate(AsyncTask asyncTask)
			{
				base.CompletePrepareToStart(asyncTask.Exception);
			});
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011984 File Offset: 0x0000FB84
		protected sealed override void StartChildrenAsync()
		{
			this.pipeline.Failed += this.OnPipelineFailed;
			AsyncTask asyncTask2 = this.StartFeeding();
			base.DiagnosticsSession.TraceDebug("Starting the pipeline and feeding in sequence.", new object[0]);
			new AsyncTaskSequence(new AsyncTask[]
			{
				new AsyncStart(this.pipeline),
				asyncTask2
			}).Execute(delegate(AsyncTask asyncTask)
			{
				base.CompleteStart(asyncTask.Exception);
			});
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00011A04 File Offset: 0x0000FC04
		protected sealed override void StopChildrenAsync()
		{
			if (this.pipeline != null)
			{
				this.pipeline.Failed -= this.OnPipelineFailed;
				AsyncTask asyncTask2 = this.StopFeeding();
				base.DiagnosticsSession.TraceDebug("Stopping the pipeline and feeding in parallel.", new object[0]);
				new AsyncTaskParallel(new AsyncTask[]
				{
					new AsyncStop(this.pipeline),
					asyncTask2
				}).Execute(delegate(AsyncTask asyncTask)
				{
					base.CompleteStop(asyncTask.Exception);
				});
				return;
			}
			base.CompleteStop(null);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00011A8C File Offset: 0x0000FC8C
		protected override void AtFail(ComponentFailedException reason)
		{
			base.DiagnosticsSession.TraceDebug("Pipeline connector is failing and is stopping feeding.", new object[0]);
			this.pipeline.Failed -= this.OnPipelineFailed;
			this.StopFeeding().Execute(new TaskCompleteCallback(this.AtFailCallback));
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00011ADD File Offset: 0x0000FCDD
		protected sealed override void DisposeChildren()
		{
			if (this.pipeline != null)
			{
				base.RemoveComponent(this.pipeline);
				this.pipeline.Dispose();
				this.pipeline = null;
			}
			this.DisposeFeeding();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011B0B File Offset: 0x0000FD0B
		private void AtFailCallback(AsyncTask asyncTask)
		{
			base.DiagnosticsSession.TraceDebug("Pipeline connector is failed.", new object[0]);
			base.AtFail(base.LastFailedReason);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011B30 File Offset: 0x0000FD30
		private void OnPipelineFailed(object sender, FailedEventArgs e)
		{
			ComponentFailedPermanentException ex = (ComponentFailedPermanentException)e.Reason;
			base.DiagnosticsSession.TraceDebug<ComponentFailedPermanentException>("The pipeline failed due to error: {0}", ex);
			base.BeginDispatchFailSignal(ex, new AsyncCallback(base.EndDispatchFailSignal), null);
		}

		// Token: 0x0400027C RID: 636
		private Pipeline pipeline;
	}
}
