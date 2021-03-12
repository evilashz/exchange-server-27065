using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.Evaluation.Processing.Producers;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000008 RID: 8
	internal abstract class ExchangeProducerBase<TOperator> : SingleOutputProducer<TOperator>, IDisposeTrackable, IDisposable where TOperator : OperatorBase
	{
		// Token: 0x06000046 RID: 70 RVA: 0x00003460 File Offset: 0x00001660
		public ExchangeProducerBase(TOperator operatorInstance, IRecordSetTypeDescriptor inputType, IEvaluationContext context, Microsoft.Exchange.Diagnostics.Trace tracer)
		{
			this.operatorName = operatorInstance.Label;
			this.TracingContext = this.GetHashCode();
			this.OperatorInstance = operatorInstance;
			this.InputType = inputType;
			this.Tracer = tracer;
			this.FlowIdentifier = (string)context.GetProperty("FlowIdentifier");
			this.diagnostics = OperatorDiagnosticsFactory.Instance.GetDiagnosticsContext(this.FlowIdentifier);
			this.Context = context;
			this.exchangeFlowContext = (ExchangeFlowContext)context.GetProperty("ExchangeFlowContext");
			if (this.exchangeFlowContext == null)
			{
				this.exchangeFlowContext = new ExchangeFlowContext();
				context.PushProperty("ExchangeFlowContext", this.exchangeFlowContext);
			}
			this.Diagnostics.InstanceGuid = this.exchangeFlowContext.InstanceGuid;
			this.Diagnostics.InstanceName = this.exchangeFlowContext.InstanceName;
			this.disposeTracker = this.GetDisposeTracker();
			this.Snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000355F File Offset: 0x0000175F
		public ExchangeProducerBase(TOperator operatorInstance, IRecordSetTypeDescriptor inputType, IEvaluationContext context, Microsoft.Exchange.Diagnostics.Trace tracer, Guid instanceGuid, string instanceName) : this(operatorInstance, inputType, context, tracer)
		{
			this.SetInstance(instanceGuid, instanceName);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003576 File Offset: 0x00001776
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000357E File Offset: 0x0000177E
		public IEvaluationContext Context { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003587 File Offset: 0x00001787
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000358F File Offset: 0x0000178F
		public string FlowIdentifier { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000035A0 File Offset: 0x000017A0
		internal Microsoft.Exchange.Diagnostics.Trace Tracer { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000035A9 File Offset: 0x000017A9
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000035B1 File Offset: 0x000017B1
		internal int TracingContext { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000035BA File Offset: 0x000017BA
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000035C2 File Offset: 0x000017C2
		internal VariantConfigurationSnapshot Snapshot { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000035CB File Offset: 0x000017CB
		protected virtual bool StartOfFlow
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000035CE File Offset: 0x000017CE
		protected OperatorDiagnostics Diagnostics
		{
			[DebuggerStepThrough]
			get
			{
				return this.diagnostics;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000035D6 File Offset: 0x000017D6
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000035DE File Offset: 0x000017DE
		private protected IRecordSetTypeDescriptor InputType { protected get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000035E7 File Offset: 0x000017E7
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000035EF File Offset: 0x000017EF
		private protected TOperator OperatorInstance { protected get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000035F8 File Offset: 0x000017F8
		protected Guid InstanceGuid
		{
			[DebuggerStepThrough]
			get
			{
				return this.exchangeFlowContext.InstanceGuid;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003605 File Offset: 0x00001805
		protected string InstanceName
		{
			[DebuggerStepThrough]
			get
			{
				return this.exchangeFlowContext.InstanceName;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003612 File Offset: 0x00001812
		protected SearchConfig Config
		{
			[DebuggerStepThrough]
			get
			{
				return this.exchangeFlowContext.Config;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000361F File Offset: 0x0000181F
		protected ExEventLog EventLog
		{
			[DebuggerStepThrough]
			get
			{
				return this.exchangeFlowContext.EventLog;
			}
		}

		// Token: 0x0600005C RID: 92
		public abstract void InternalProcessRecord(IRecord record);

		// Token: 0x0600005D RID: 93
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x0600005E RID: 94 RVA: 0x0000362C File Offset: 0x0000182C
		public sealed override void ProcessRecord(IRecord record)
		{
			this.Tracer.TraceDebug<string>((long)this.TracingContext, "Begin {0}", this.operatorName);
			try
			{
				using (ExPerfTrace.NewActivity())
				{
					if (this.StartOfFlow)
					{
						this.Diagnostics.ClearOperatorTimings();
						this.DropBreadcrumb(OperatorLocation.BeginProcessRecord, this.GetTimeSinceSubmitTime(record));
					}
					else
					{
						this.DropBreadcrumb(OperatorLocation.BeginProcessRecord);
					}
					this.InternalProcessRecord(record);
					TimeSpan timeSpan = this.DropBreadcrumb(OperatorLocation.EndProcessRecord);
					this.Tracer.TraceDebug<string, double>((long)this.TracingContext, "End {0}. Time elapsed: {1} ms", this.operatorName, timeSpan.TotalMilliseconds);
				}
			}
			catch (OutOfMemoryException)
			{
				throw;
			}
			catch (StackOverflowException)
			{
				throw;
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception ex)
			{
				TimeSpan timeSpan = this.DropBreadcrumb(OperatorLocation.EndProcessRecordException, ex);
				this.Tracer.TraceDebug<string, double, string>((long)this.TracingContext, "End {0}. Time elapsed: {1} ms, Exception: {2}", this.operatorName, timeSpan.TotalMilliseconds, ex.Message);
				throw;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003750 File Offset: 0x00001950
		public void IncrementPerfcounter(OperatorPerformanceCounter performanceCounter)
		{
			if (this.exchangeFlowContext.FlowPerformanceCounters != null)
			{
				this.exchangeFlowContext.FlowPerformanceCounters.IncrementPerfcounter(performanceCounter);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003770 File Offset: 0x00001970
		public void IncrementPerfcounterBy(OperatorPerformanceCounter performanceCounter, long value)
		{
			if (this.exchangeFlowContext.FlowPerformanceCounters != null)
			{
				this.exchangeFlowContext.FlowPerformanceCounters.IncrementPerfcounterBy(performanceCounter, value);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003791 File Offset: 0x00001991
		public void DecrementPerfcounter(OperatorPerformanceCounter performanceCounter)
		{
			if (this.exchangeFlowContext.FlowPerformanceCounters != null)
			{
				this.exchangeFlowContext.FlowPerformanceCounters.DecrementPerfcounter(performanceCounter);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000037B1 File Offset: 0x000019B1
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037C6 File Offset: 0x000019C6
		protected override void Initialize()
		{
			base.Initialize();
			this.submitTimeIndex = this.InputType.RecordType.Position("SubmitTime");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037E9 File Offset: 0x000019E9
		protected TimeSpan DropBreadcrumb(OperatorLocation location)
		{
			return this.diagnostics.DropBreadcrumb(location, this.operatorName);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037FD File Offset: 0x000019FD
		protected TimeSpan DropBreadcrumb(OperatorLocation location, TimeSpan elapsed)
		{
			return this.diagnostics.DropBreadcrumb(location, this.operatorName, elapsed);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003812 File Offset: 0x00001A12
		protected TimeSpan DropBreadcrumb(OperatorLocation location, Exception exception)
		{
			return this.diagnostics.DropBreadcrumb(location, this.operatorName, exception.Message);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000382C File Offset: 0x00001A2C
		protected override void ReleaseManagedResources()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.diagnostics != null)
			{
				OperatorDiagnosticsFactory.Instance.ReleaseDiagnosticsContext(this.diagnostics);
			}
			base.ReleaseManagedResources();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003866 File Offset: 0x00001A66
		protected virtual void SetInstance(Guid instanceGuid, string instanceName)
		{
			this.exchangeFlowContext.SetInstance(instanceGuid, instanceName);
			this.Diagnostics.InstanceGuid = this.exchangeFlowContext.InstanceGuid;
			this.Diagnostics.InstanceName = this.exchangeFlowContext.InstanceName;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000038A1 File Offset: 0x00001AA1
		protected void SetFlowPerformanceCounters(IOperatorPerfCounter flowPerformanceCounters)
		{
			this.exchangeFlowContext.SetFlowPerformanceCounters(flowPerformanceCounters);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000038B0 File Offset: 0x00001AB0
		protected T GetUpdateableField<T>(IUpdateableRecord holder, int index) where T : class, IUpdateableField
		{
			T t = (T)((object)holder[index]);
			if (t == null)
			{
				throw new ArgumentException("index");
			}
			t.Value = null;
			return t;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000038EC File Offset: 0x00001AEC
		private TimeSpan GetTimeSinceSubmitTime(IRecord record)
		{
			if (this.submitTimeIndex == -1 || record[this.submitTimeIndex].IsNull())
			{
				return TimeSpan.Zero;
			}
			return DateTime.UtcNow - (DateTime)record[this.submitTimeIndex].Value;
		}

		// Token: 0x0400001C RID: 28
		private readonly string operatorName;

		// Token: 0x0400001D RID: 29
		private readonly OperatorDiagnostics diagnostics;

		// Token: 0x0400001E RID: 30
		private int submitTimeIndex;

		// Token: 0x0400001F RID: 31
		private DisposeTracker disposeTracker;

		// Token: 0x04000020 RID: 32
		private ExchangeFlowContext exchangeFlowContext;
	}
}
