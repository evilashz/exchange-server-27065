using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Inference.Performance;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.AsyncTask;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.EventLog;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000A5 RID: 165
	internal sealed class Pipeline : ContainerComponent, IPipeline, IDocumentProcessor, IStartStop, IDisposable, IDiagnosable, INotifyFailed
	{
		// Token: 0x060004E5 RID: 1253 RVA: 0x0000F484 File Offset: 0x0000D684
		static Pipeline()
		{
			ComponentRegistry.Register<Pipeline>();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000F491 File Offset: 0x0000D691
		internal Pipeline(PipelineDefinition definition, string instance) : this(definition, instance, null, null)
		{
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		internal Pipeline(PipelineDefinition definition, string instance, IPipelineContext context, IPipelineErrorHandler errorHandler)
		{
			Util.ThrowOnNullArgument(definition, "definition");
			Util.ThrowOnNullOrEmptyArgument(instance, "instance");
			base.DiagnosticsSession.ComponentName = instance;
			base.DiagnosticsSession.Tracer = ExTraceGlobals.CorePipelineTracer;
			this.definition = definition;
			this.instanceName = instance;
			this.context = context;
			this.errorHandler = errorHandler;
			this.poisonComponentThreshold = ((this.definition.PoisonComponentThreshold > 0) ? this.definition.PoisonComponentThreshold : Pipeline.DefaultPoisonComponentThreshold);
			this.pipelinePerfCounter = PipelineCounters.GetInstance(this.instanceName);
			this.pipelinePerfCounter.Reset();
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000F55A File Offset: 0x0000D75A
		public int MaxConcurrency
		{
			get
			{
				return this.definition.MaxConcurrency;
			}
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		public IAsyncResult BeginProcess(IDocument document, AsyncCallback callback, object context)
		{
			base.CheckDisposed();
			Util.ThrowOnNullArgument(document, "document");
			base.DiagnosticsSession.IncrementCounter(this.pipelinePerfCounter.NumberOfIncomingDocuments);
			AsyncResult asyncResult = new AsyncResult(callback, context);
			this.BeginDispatchProcessDocSignal(document, asyncResult, delegate(IAsyncResult ar)
			{
				try
				{
					this.EndDispatchProcessDocSignal(ar);
				}
				catch (ComponentException asCompleted)
				{
					asyncResult.SetAsCompleted(asCompleted);
				}
			}, null);
			return asyncResult;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000F61B File Offset: 0x0000D81B
		public void EndProcess(IAsyncResult asyncResult)
		{
			base.CheckDisposed();
			((AsyncResult)asyncResult).End();
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0000F62E File Offset: 0x0000D82E
		public override string ToString()
		{
			return this.instanceName;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000F638 File Offset: 0x0000D838
		public void ProcessDocument(IDocument document, object context)
		{
			ComponentException ex = null;
			ComponentException ex2 = null;
			PerfCounterSampleCollector sampleCollector = this.StartProcess(document);
			for (int i = 0; i < this.components.Count; i++)
			{
				if (!this.components.IsPoisonComponent(i))
				{
					IPipelineComponent pipelineComponent = this.components[i];
					base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, document.Identity, "Processing document through component: {0} ({1})", new object[]
					{
						i,
						pipelineComponent.Name
					});
					PipelineCountersInstance instance = PipelineCounters.GetInstance(this.GetComponentInstanceName(pipelineComponent));
					base.DiagnosticsSession.IncrementCounter(instance.NumberOfIncomingDocuments);
					PerfCounterSampleCollector perfCounterSampleCollector = new PerfCounterSampleCollector(instance, base.DiagnosticsSession);
					perfCounterSampleCollector.Start();
					try
					{
						pipelineComponent.ProcessDocument(document, context);
					}
					catch (ComponentException ex3)
					{
						ex = ex3;
					}
					perfCounterSampleCollector.Stop(ex == null);
					if (!this.ContinueAfterException(document, i, ex, out ex2))
					{
						ex = ex2;
						break;
					}
					ex = null;
				}
			}
			this.CompleteProcess(document, ex, null, sampleCollector);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000F750 File Offset: 0x0000D950
		protected override XElement InternalGetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = base.InternalGetDiagnosticInfo(parameters);
			for (int i = 0; i < this.components.Count; i++)
			{
				IPipelineComponent pipelineComponent = this.components[i];
				XElement xelement2 = new XElement(string.Format("Component{0}", i + 1));
				xelement2.Add(new XElement("Name", pipelineComponent.Name));
				xelement2.Add(new XElement("Description", pipelineComponent.Description));
				IDiagnosable diagnosable = pipelineComponent as IDiagnosable;
				if (diagnosable != null)
				{
					xelement2.Add(diagnosable.GetDiagnosticInfo(parameters));
				}
				if (this.nestedPipelines.ContainsKey(i))
				{
					IDiagnosable diagnosable2 = this.nestedPipelines[i];
					xelement2.Add(diagnosable2.GetDiagnosticInfo(parameters));
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000F830 File Offset: 0x0000DA30
		protected override void CreateChildren()
		{
			this.CreateComponents();
			this.componentsMonitor = new PipelineComponentMonitor(this.components);
			for (int i = 0; i < this.components.Count; i++)
			{
				string componentInstanceName = this.GetComponentInstanceName(this.components[i]);
				PipelineCounters.ResetInstance(componentInstanceName);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000F894 File Offset: 0x0000DA94
		protected override void PrepareToStartChildrenAsync()
		{
			List<AsyncTask> list = new List<AsyncTask>(this.components.Count);
			if (this.components.HasStartStopComponent)
			{
				for (int i = 0; i < this.components.Count; i++)
				{
					IPipelineComponent pipelineComponent = this.components[i];
					if (this.nestedPipelines.ContainsKey(i))
					{
						IPipeline component = this.nestedPipelines[i];
						base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Adding AsyncTaskSequence for AsyncPrepareToStart of nested pipeline and the component", this);
						list.Add(new AsyncTaskSequence(new AsyncTask[]
						{
							new AsyncPrepareToStart(component),
							new AsyncPrepareToStart((IStartStopPipelineComponent)pipelineComponent)
						}));
					}
					else if (pipelineComponent is IStartStopPipelineComponent)
					{
						base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Adding AsyncPrepareToStart for the component that supports start/stop", this);
						list.Add(new AsyncPrepareToStart((IStartStopPipelineComponent)pipelineComponent));
					}
				}
			}
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} nested pipelines", this, this.nestedPipelines.Count);
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} components in this pipeline that support start/stop", this, list.Count);
			list.Add(new AsyncPrepareToStart(this.componentsMonitor));
			base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Preparing to start nested pipelines, components and monitor in parallel", this);
			new AsyncTaskParallel(list).Execute(delegate(AsyncTask asyncTask)
			{
				base.CompletePrepareToStart(asyncTask.Exception);
			});
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000F9EC File Offset: 0x0000DBEC
		protected override void StartChildrenAsync()
		{
			List<AsyncTask> list = new List<AsyncTask>();
			List<AsyncTask> list2 = new List<AsyncTask>(this.nestedPipelines.Count);
			foreach (IPipeline component in this.nestedPipelines.Values)
			{
				list2.Add(new AsyncStart(component));
			}
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} nested pipelines", this, this.nestedPipelines.Count);
			if (list2.Count > 0)
			{
				list.Add(new AsyncTaskParallel(list2));
			}
			List<AsyncTask> list3 = new List<AsyncTask>(this.components.Count);
			if (this.components.HasStartStopComponent)
			{
				for (int i = 0; i < this.components.Count; i++)
				{
					IPipelineComponent pipelineComponent = this.components[i];
					if (pipelineComponent is IStartStopPipelineComponent)
					{
						list3.Add(new AsyncStart((IStartStopPipelineComponent)pipelineComponent));
					}
				}
			}
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} components in this pipeline that support start/stop", this, list3.Count);
			if (list3.Count > 0)
			{
				list.Add(new AsyncTaskParallel(list3));
			}
			list.Add(new AsyncStart(this.componentsMonitor));
			base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Starting nested pipelines, components, monitor in sequence.", this);
			new AsyncTaskSequence(list).Execute(delegate(AsyncTask asyncTask)
			{
				base.CompleteStart(asyncTask.Exception);
			});
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000FB88 File Offset: 0x0000DD88
		protected override void StopChildrenAsync()
		{
			List<AsyncTask> list = new List<AsyncTask>(this.components.Count + this.nestedPipelines.Count);
			if (this.components.HasStartStopComponent)
			{
				for (int i = 0; i < this.components.Count; i++)
				{
					IPipelineComponent pipelineComponent = this.components[i];
					if (pipelineComponent is IStartStopPipelineComponent)
					{
						list.Add(new AsyncStop((IStartStopPipelineComponent)pipelineComponent));
					}
				}
			}
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} components in this pipeline that support start/stop", this, list.Count);
			foreach (IPipeline component in this.nestedPipelines.Values)
			{
				list.Add(new AsyncStop(component));
			}
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Found {1} nested pipelines", this, this.nestedPipelines.Count);
			List<AsyncTask> list2 = new List<AsyncTask>();
			list2.Add(new AsyncStop(this.componentsMonitor));
			if (list.Count > 0)
			{
				list2.Add(new AsyncTaskParallel(list));
			}
			base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Stopping monitor first, and then components and nested pipelines in parallel", this);
			new AsyncTaskSequence(list2).Execute(delegate(AsyncTask asyncTask)
			{
				base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Pipeline children components are stopped.", this);
				this.BeginDispatchDoneStoppingChildrenSignal(asyncTask.Exception, new AsyncCallback(this.EndDispatchDoneStoppingChildrenSignal), null);
			});
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		protected override void AtFail(ComponentFailedException reason)
		{
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Failing due to exception: {0}.", new object[]
			{
				reason
			});
			if (this.outstandingDocuments == 0)
			{
				base.AtFail(reason);
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000FD14 File Offset: 0x0000DF14
		protected override void DisposeChildren()
		{
			if (this.componentsMonitor != null)
			{
				this.componentsMonitor.Dispose();
				this.componentsMonitor = null;
			}
			if (this.components != null)
			{
				this.components.Dispose();
				this.components = null;
			}
			if (this.nestedPipelines.Count > 0)
			{
				foreach (IPipeline pipeline in this.nestedPipelines.Values)
				{
					pipeline.Dispose();
				}
				this.nestedPipelines.Clear();
			}
			if (this.documentSampleCollectorMap != null)
			{
				this.documentSampleCollectorMap.Clear();
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000FED0 File Offset: 0x0000E0D0
		private void CreateComponents()
		{
			this.components = new PipelineComponentList(this.definition.Components.Length, this.poisonComponentThreshold);
			for (int i = 0; i < this.definition.Components.Length; i++)
			{
				PipelineComponentDefinition componentDefinition = this.definition.Components[i];
				int index = componentDefinition.Order - 1;
				if (componentDefinition.Pipeline != null)
				{
					string nestedPipelineInstanceName = this.GetNestedPipelineInstanceName(componentDefinition.Pipeline);
					base.DiagnosticsSession.TraceDebug<Pipeline, string>("{0}: Creating a nested pipeline of instance name of {1}", this, nestedPipelineInstanceName);
					IPipeline nestedPipeline = new Pipeline(componentDefinition.Pipeline, nestedPipelineInstanceName, this.context, this.errorHandler);
					this.nestedPipelines.Add(index, nestedPipeline);
					this.components.Insert(index, delegate
					{
						this.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Creating component of index {1} with the nested pipeline", this, index);
						return componentDefinition.CreateComponent(this.context, nestedPipeline);
					});
				}
				else
				{
					this.components.Insert(index, delegate
					{
						this.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: Creating component of index {1}", this, index);
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						IPipelineComponent result = componentDefinition.CreateComponent(this.context, null);
						stopwatch.Stop();
						this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Component {0} creation timespan: {1} ms", new object[]
						{
							componentDefinition.Name,
							stopwatch.ElapsedMilliseconds
						});
						return result;
					});
				}
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00010019 File Offset: 0x0000E219
		private string GetComponentInstanceName(IPipelineComponent component)
		{
			return string.Format("{0} - {1}", this.instanceName, component.Name);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00010031 File Offset: 0x0000E231
		private string GetNestedPipelineInstanceName(PipelineDefinition nestedPipeline)
		{
			return string.Format("{0} - {1}", this.instanceName, nestedPipeline.Name);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001004C File Offset: 0x0000E24C
		private PerfCounterSampleCollector StartProcess(IDocument document)
		{
			PerfCounterSampleCollector perfCounterSampleCollector = new PerfCounterSampleCollector(this.pipelinePerfCounter, base.DiagnosticsSession);
			perfCounterSampleCollector.Start();
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: There are {1} outstanding documents in the pipeline", this, this.outstandingDocuments);
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: About to process document {1} through the pipeline", this, document);
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, document.Identity, "Beginning to process document", new object[0]);
			Interlocked.Increment(ref this.outstandingDocuments);
			return perfCounterSampleCollector;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000100C4 File Offset: 0x0000E2C4
		private void AtProcessDoc(IDocument document, AsyncResult asyncResult)
		{
			PerfCounterSampleCollector value = this.StartProcess(document);
			this.documentSampleCollectorMap.Add(document, value);
			this.BeginDispatchProcessDocInComponentSignal(document, 0, asyncResult, new AsyncCallback(this.EndDispatchProcessDocInComponentSignal), null);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00010100 File Offset: 0x0000E300
		private void AtCannotProcessDoc(IDocument document, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: There are {1} outstanding documents in the pipeline", this, this.outstandingDocuments);
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Unable to process document {1} through the pipeline", this, document);
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, document.Identity, "Cannot process document", new object[0]);
			base.DiagnosticsSession.IncrementCounter(this.pipelinePerfCounter.NumberOfRejectedDocuments);
			asyncResult.SetAsCompleted(new CannotProcessDocException());
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000101DC File Offset: 0x0000E3DC
		private void AtProcessDocInComponent(IDocument document, int index, AsyncResult asyncResult)
		{
			IPipelineComponent component = null;
			if (this.components.IsPoisonComponent(index))
			{
				this.AdvanceToNextComponent(document, index, asyncResult, null);
				return;
			}
			component = this.components[index];
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, document.Identity, "Processing document through component: {0} ({1})", new object[]
			{
				index,
				component.Name
			});
			PipelineCountersInstance instance = PipelineCounters.GetInstance(this.GetComponentInstanceName(component));
			base.DiagnosticsSession.IncrementCounter(instance.NumberOfIncomingDocuments);
			PerfCounterSampleCollector componentSampleCollector = new PerfCounterSampleCollector(instance, base.DiagnosticsSession);
			componentSampleCollector.Start();
			component.BeginProcess(document, delegate(IAsyncResult componentAsyncResult)
			{
				ComponentException ex = null;
				try
				{
					component.EndProcess(componentAsyncResult);
				}
				catch (ComponentException ex2)
				{
					ex = ex2;
				}
				componentSampleCollector.Stop(ex == null);
				this.AdvanceToNextComponent(document, index, asyncResult, ex);
			}, asyncResult.AsyncState);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00010300 File Offset: 0x0000E500
		private bool ContinueAfterException(IDocument document, int index, ComponentException exception, out ComponentException exceptionToReport)
		{
			bool result = true;
			exceptionToReport = null;
			if (exception != null)
			{
				IPipelineComponent pipelineComponent = this.components[index];
				base.DiagnosticsSession.TraceError("{0}: Exception occurred in component {1} for document {2}: {3}", new object[]
				{
					this,
					index,
					document,
					exception
				});
				base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, document.Identity, "Exception occurred in component {0} ({1}): {2}", new object[]
				{
					index,
					pipelineComponent.Name,
					exception
				});
				DocumentResolution documentResolution = DocumentResolution.CompleteError;
				if (this.errorHandler != null)
				{
					documentResolution = this.errorHandler.HandleException(pipelineComponent, exception);
				}
				switch (documentResolution)
				{
				case DocumentResolution.IgnoreAndContinue:
					base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Ignore and continue processing of document {1} in the pipeline", this, document);
					break;
				case DocumentResolution.CompleteError:
					base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Abort processing of document {1} in the pipeline", this, document);
					result = false;
					exceptionToReport = exception;
					break;
				case DocumentResolution.CompleteSuccess:
					base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Skip processing of document {1} in the pipeline", this, document);
					result = false;
					break;
				case DocumentResolution.PoisonComponentAndContinue:
					base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Ignore and continue processing of document {1} in the pipeline", this, document);
					this.HandlePoisonComponent(document, index, exception);
					break;
				}
			}
			return result;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00010424 File Offset: 0x0000E624
		private void HandlePoisonComponent(IDocument document, int index, ComponentException exception)
		{
			string name = this.components[index].Name;
			if (this.components.TrackPoisonComponent(index))
			{
				PipelineCountersInstance instance = PipelineCounters.GetInstance(this.instanceName);
				base.DiagnosticsSession.IncrementCounter(instance.NumberOfComponentsPoisoned);
				base.DiagnosticsSession.LogEvent(MSExchangeFastSearchEventLogConstants.Tuple_ComponentPoisoned, new object[]
				{
					name,
					this.instanceName,
					this.poisonComponentThreshold,
					exception.InnerException
				});
				base.DiagnosticsSession.TraceDebug<Pipeline, IDocument, string>("{0}: Poisoned component {2} during processing of document {1}", this, document, name);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000104C0 File Offset: 0x0000E6C0
		private void AdvanceToNextComponent(IDocument document, int index, AsyncResult asyncResult, ComponentException exception)
		{
			ComponentException result = null;
			if (this.ContinueAfterException(document, index, exception, out result) && index + 1 < this.components.Count)
			{
				this.BeginDispatchProcessDocInComponentSignal(document, index + 1, asyncResult, new AsyncCallback(this.EndDispatchProcessDocInComponentSignal), null);
				return;
			}
			this.BeginDispatchDoneProcessingDocSignal(document, result, asyncResult, new AsyncCallback(this.EndDispatchDoneProcessingDocSignal), null);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001051E File Offset: 0x0000E71E
		private void AtDoneProcessingDoc(IDocument document, ComponentException result, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Complete processing of document {1} in the pipeline", this, document);
			this.CompleteProcess(document, result, asyncResult);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001053B File Offset: 0x0000E73B
		private void AtCancelledProcessDoc(IDocument document, int index, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Processing of document {1} is cancelled in stopping", this, document);
			this.CompleteProcess(document, new DocProcessCanceledException(), asyncResult);
			this.SafeStop();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00010562 File Offset: 0x0000E762
		private void AtDoneProcessingDocInStopping(IDocument document, ComponentException result, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Complete processing of document {1} in stopping", this, document);
			this.CompleteProcess(document, result, asyncResult);
			this.SafeStop();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00010585 File Offset: 0x0000E785
		private void AtDoneStoppingChildren(ComponentException result)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Completes stopping of children components in stopping", this);
			this.stoppingChildrenException = result;
			this.stoppingChildrenIsDone = true;
			this.SafeStop();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000105AC File Offset: 0x0000E7AC
		private void AtCancelledProcessDocInFailing(IDocument document, int index, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Processing of document {1} is cancelled during failing", this, document);
			this.CompleteProcess(document, new DocProcessCanceledException(), asyncResult);
			if (this.outstandingDocuments == 0)
			{
				base.AtFail(base.LastFailedReason);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000105E1 File Offset: 0x0000E7E1
		private void AtDoneProcessingDocInFailing(IDocument document, ComponentException result, AsyncResult asyncResult)
		{
			base.DiagnosticsSession.TraceDebug<Pipeline, IDocument>("{0}: Complete processing of document {1} during failing", this, document);
			this.CompleteProcess(document, result, asyncResult);
			if (this.outstandingDocuments == 0)
			{
				base.AtFail(base.LastFailedReason);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00010614 File Offset: 0x0000E814
		private void CompleteProcess(IDocument document, ComponentException result, AsyncResult asyncResult)
		{
			PerfCounterSampleCollector sampleCollector = this.documentSampleCollectorMap[document];
			this.documentSampleCollectorMap.Remove(document);
			this.CompleteProcess(document, result, asyncResult, sampleCollector);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00010648 File Offset: 0x0000E848
		private void CompleteProcess(IDocument document, ComponentException result, AsyncResult asyncResult, PerfCounterSampleCollector sampleCollector)
		{
			sampleCollector.Stop(result == null);
			Interlocked.Decrement(ref this.outstandingDocuments);
			base.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, document.Identity, "Completed processing of document{0}", new object[]
			{
				(result == null) ? string.Empty : string.Format(" with exception {0}", result)
			});
			base.DiagnosticsSession.TraceDebug<Pipeline, int>("{0}: There are {1} outstanding documents in the pipeline", this, this.outstandingDocuments);
			if (asyncResult != null)
			{
				asyncResult.SetAsCompleted(result);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000106C4 File Offset: 0x0000E8C4
		private void SafeStop()
		{
			bool flag = true;
			if (this.outstandingDocuments != 0)
			{
				flag = false;
				base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Waiting for outstanding documents to be processed", this);
			}
			if (!this.stoppingChildrenIsDone)
			{
				flag = false;
				base.DiagnosticsSession.TraceDebug<Pipeline>("{0}: Waiting for stopping of children component to be done", this);
			}
			if (flag)
			{
				base.CompleteStop(this.stoppingChildrenException);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00010718 File Offset: 0x0000E918
		private void OnComponentsMonitorFailed(object sender, FailedEventArgs e)
		{
			ComponentFailedException reason = e.Reason;
			base.DiagnosticsSession.TraceDebug<Pipeline, ComponentFailedException>("{0}: The components monitor failed to revive component. Exception={1}", this, reason);
			base.BeginDispatchFailSignal(reason, new AsyncCallback(base.EndDispatchFailSignal), null);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00010754 File Offset: 0x0000E954
		private static void RegisterComponent(ComponentInfo componentInfo)
		{
			componentInfo.RegisterSignal(Pipeline.Signal.ProcessDoc, SignalPriority.Medium);
			componentInfo.RegisterSignal(Pipeline.Signal.ProcessDocInComponent, SignalPriority.Medium);
			componentInfo.RegisterSignal(Pipeline.Signal.DoneProcessingDoc, SignalPriority.Medium);
			componentInfo.RegisterSignal(Pipeline.Signal.DoneStoppingChildren, SignalPriority.Medium);
			componentInfo.RegisterState(Pipeline.State.QueueFull);
			componentInfo.RegisterTransition(6U, 9U, 6U, new ConditionMethod(Pipeline.Condition_InlineCondition__0), new ActionMethod(Pipeline.Transition_AtProcessDoc));
			componentInfo.RegisterTransition(6U, 9U, 10U, new ConditionMethod(Pipeline.Condition_InlineCondition__1), new ActionMethod(Pipeline.Transition_AtProcessDoc));
			componentInfo.RegisterTransition(6U, 10U, 6U, null, new ActionMethod(Pipeline.Transition_AtProcessDocInComponent));
			componentInfo.RegisterTransition(6U, 11U, 6U, null, new ActionMethod(Pipeline.Transition_AtDoneProcessingDoc));
			componentInfo.RegisterTransition(10U, 9U, 10U, null, new ActionMethod(Pipeline.Transition_AtCannotProcessDoc));
			componentInfo.RegisterTransition(10U, 10U, 10U, null, new ActionMethod(Pipeline.Transition_AtProcessDocInComponent));
			componentInfo.RegisterTransition(10U, 11U, 6U, null, new ActionMethod(Pipeline.Transition_AtDoneProcessingDoc));
			componentInfo.RegisterTransition(10U, 5U, 3U, null, new ActionMethod(StartStopComponent.Transition_AtStop));
			componentInfo.RegisterTransition(3U, 9U, 3U, null, new ActionMethod(Pipeline.Transition_AtCannotProcessDoc));
			componentInfo.RegisterTransition(2U, 9U, 2U, null, new ActionMethod(Pipeline.Transition_AtCannotProcessDoc));
			componentInfo.RegisterTransition(3U, 10U, 3U, null, new ActionMethod(Pipeline.Transition_AtCancelledProcessDoc));
			componentInfo.RegisterTransition(3U, 11U, 3U, null, new ActionMethod(Pipeline.Transition_AtDoneProcessingDocInStopping));
			componentInfo.RegisterTransition(9U, 10U, 9U, null, new ActionMethod(Pipeline.Transition_AtCancelledProcessDocInFailing));
			componentInfo.RegisterTransition(9U, 11U, 9U, null, new ActionMethod(Pipeline.Transition_AtDoneProcessingDocInFailing));
			componentInfo.RegisterTransition(3U, 12U, 3U, null, new ActionMethod(Pipeline.Transition_AtDoneStoppingChildren));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001091F File Offset: 0x0000EB1F
		internal static void Transition_AtProcessDoc(object component, params object[] args)
		{
			((Pipeline)component).AtProcessDoc((IDocument)args[0], (AsyncResult)args[1]);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001093C File Offset: 0x0000EB3C
		internal static void Transition_AtProcessDocInComponent(object component, params object[] args)
		{
			((Pipeline)component).AtProcessDocInComponent((IDocument)args[0], (int)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00010961 File Offset: 0x0000EB61
		internal static void Transition_AtDoneProcessingDoc(object component, params object[] args)
		{
			((Pipeline)component).AtDoneProcessingDoc((IDocument)args[0], (ComponentException)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00010986 File Offset: 0x0000EB86
		internal static void Transition_AtCannotProcessDoc(object component, params object[] args)
		{
			((Pipeline)component).AtCannotProcessDoc((IDocument)args[0], (AsyncResult)args[1]);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000109A3 File Offset: 0x0000EBA3
		internal static void Transition_AtCancelledProcessDoc(object component, params object[] args)
		{
			((Pipeline)component).AtCancelledProcessDoc((IDocument)args[0], (int)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000109C8 File Offset: 0x0000EBC8
		internal static void Transition_AtDoneProcessingDocInStopping(object component, params object[] args)
		{
			((Pipeline)component).AtDoneProcessingDocInStopping((IDocument)args[0], (ComponentException)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000109ED File Offset: 0x0000EBED
		internal static void Transition_AtCancelledProcessDocInFailing(object component, params object[] args)
		{
			((Pipeline)component).AtCancelledProcessDocInFailing((IDocument)args[0], (int)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00010A12 File Offset: 0x0000EC12
		internal static void Transition_AtDoneProcessingDocInFailing(object component, params object[] args)
		{
			((Pipeline)component).AtDoneProcessingDocInFailing((IDocument)args[0], (ComponentException)args[1], (AsyncResult)args[2]);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00010A37 File Offset: 0x0000EC37
		internal static void Transition_AtDoneStoppingChildren(object component, params object[] args)
		{
			((Pipeline)component).AtDoneStoppingChildren((ComponentException)args[0]);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010A4C File Offset: 0x0000EC4C
		private static bool Condition_InlineCondition__0(object component)
		{
			return ((Pipeline)component).outstandingDocuments < ((Pipeline)component).definition.MaxConcurrency - 1;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00010A6D File Offset: 0x0000EC6D
		private static bool Condition_InlineCondition__1(object component)
		{
			return ((Pipeline)component).outstandingDocuments == ((Pipeline)component).definition.MaxConcurrency - 1;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00010A90 File Offset: 0x0000EC90
		internal IAsyncResult BeginDispatchProcessDocSignal(IDocument document, AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 9U, callback, context, TimeSpan.Zero, new object[]
			{
				document,
				asyncResult
			});
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		internal IAsyncResult BeginDispatchProcessDocSignal(IDocument document, AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 9U, callback, context, delayInTimespan, new object[]
			{
				document,
				asyncResult
			});
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		internal void EndDispatchProcessDocSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010B04 File Offset: 0x0000ED04
		internal IAsyncResult BeginDispatchProcessDocInComponentSignal(IDocument document, int index, AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 10U, callback, context, TimeSpan.Zero, new object[]
			{
				document,
				index,
				asyncResult
			});
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00010B3C File Offset: 0x0000ED3C
		internal IAsyncResult BeginDispatchProcessDocInComponentSignal(IDocument document, int index, AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 10U, callback, context, delayInTimespan, new object[]
			{
				document,
				index,
				asyncResult
			});
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00010B7E File Offset: 0x0000ED7E
		internal void EndDispatchProcessDocInComponentSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010B88 File Offset: 0x0000ED88
		internal IAsyncResult BeginDispatchDoneProcessingDocSignal(IDocument document, ComponentException result, AsyncResult asyncResult, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 11U, callback, context, TimeSpan.Zero, new object[]
			{
				document,
				result,
				asyncResult
			});
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00010BBC File Offset: 0x0000EDBC
		internal IAsyncResult BeginDispatchDoneProcessingDocSignal(IDocument document, ComponentException result, AsyncResult asyncResult, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 11U, callback, context, delayInTimespan, new object[]
			{
				document,
				result,
				asyncResult
			});
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00010BF9 File Offset: 0x0000EDF9
		internal void EndDispatchDoneProcessingDocSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00010C04 File Offset: 0x0000EE04
		internal IAsyncResult BeginDispatchDoneStoppingChildrenSignal(ComponentException result, AsyncCallback callback, object context)
		{
			return base.InternalBeginDispatchSignal(null, 12U, callback, context, TimeSpan.Zero, new object[]
			{
				result
			});
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00010C30 File Offset: 0x0000EE30
		internal IAsyncResult BeginDispatchDoneStoppingChildrenSignal(ComponentException result, AsyncCallback callback, object context, WaitHandle waitHandle, TimeSpan delayInTimespan)
		{
			Util.ThrowOnNullArgument(waitHandle, "waitHandle");
			return base.InternalBeginDispatchSignal(waitHandle, 12U, callback, context, delayInTimespan, new object[]
			{
				result
			});
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010C63 File Offset: 0x0000EE63
		internal void EndDispatchDoneStoppingChildrenSignal(IAsyncResult asyncResult)
		{
			base.EndDispatchSignal(asyncResult);
		}

		// Token: 0x04000246 RID: 582
		private static readonly int DefaultPoisonComponentThreshold = 3;

		// Token: 0x04000247 RID: 583
		private readonly PipelineDefinition definition;

		// Token: 0x04000248 RID: 584
		private readonly IPipelineContext context;

		// Token: 0x04000249 RID: 585
		private readonly IPipelineErrorHandler errorHandler;

		// Token: 0x0400024A RID: 586
		private readonly string instanceName;

		// Token: 0x0400024B RID: 587
		private readonly int poisonComponentThreshold;

		// Token: 0x0400024C RID: 588
		private readonly PipelineCountersInstance pipelinePerfCounter;

		// Token: 0x0400024D RID: 589
		private readonly Dictionary<IDocument, PerfCounterSampleCollector> documentSampleCollectorMap = new Dictionary<IDocument, PerfCounterSampleCollector>();

		// Token: 0x0400024E RID: 590
		private readonly SortedList<int, IPipeline> nestedPipelines = new SortedList<int, IPipeline>();

		// Token: 0x0400024F RID: 591
		private PipelineComponentList components;

		// Token: 0x04000250 RID: 592
		private PipelineComponentMonitor componentsMonitor;

		// Token: 0x04000251 RID: 593
		private int outstandingDocuments;

		// Token: 0x04000252 RID: 594
		private bool stoppingChildrenIsDone;

		// Token: 0x04000253 RID: 595
		private ComponentException stoppingChildrenException;

		// Token: 0x020000A6 RID: 166
		internal new enum Signal : uint
		{
			// Token: 0x04000255 RID: 597
			ProcessDoc = 9U,
			// Token: 0x04000256 RID: 598
			ProcessDocInComponent,
			// Token: 0x04000257 RID: 599
			DoneProcessingDoc,
			// Token: 0x04000258 RID: 600
			DoneStoppingChildren,
			// Token: 0x04000259 RID: 601
			Max
		}

		// Token: 0x020000A7 RID: 167
		internal new enum State : uint
		{
			// Token: 0x0400025B RID: 603
			QueueFull = 10U,
			// Token: 0x0400025C RID: 604
			Max
		}
	}
}
