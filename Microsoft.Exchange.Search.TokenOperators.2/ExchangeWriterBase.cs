using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Ceres.ContentEngine.Operators;
using Microsoft.Ceres.ContentEngine.Processing.Writer;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200000E RID: 14
	internal abstract class ExchangeWriterBase<TOperator> : AbstractWriterProducer<TOperator>, IDisposeTrackable, IDisposable where TOperator : AbstractWriterOperator<TOperator>
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public ExchangeWriterBase(IEvaluationContext context, bool forward, Microsoft.Exchange.Diagnostics.Trace tracer) : base(context, forward)
		{
			this.operatorName = base.GetType().Name;
			this.TracingContext = this.GetHashCode();
			this.Tracer = tracer;
			this.FlowIdentifier = (string)context.GetProperty("FlowIdentifier");
			this.diagnostics = OperatorDiagnosticsFactory.Instance.GetDiagnosticsContext(this.FlowIdentifier);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004C69 File Offset: 0x00002E69
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00004C71 File Offset: 0x00002E71
		private protected Microsoft.Exchange.Diagnostics.Trace Tracer { protected get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004C7A File Offset: 0x00002E7A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004C82 File Offset: 0x00002E82
		private protected int TracingContext { protected get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004C8B File Offset: 0x00002E8B
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00004C93 File Offset: 0x00002E93
		private protected string FlowIdentifier { protected get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004C9C File Offset: 0x00002E9C
		protected OperatorDiagnostics Diagnostics
		{
			[DebuggerStepThrough]
			get
			{
				return this.diagnostics;
			}
		}

		// Token: 0x060000C9 RID: 201
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x060000CA RID: 202 RVA: 0x00004CA4 File Offset: 0x00002EA4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000CB RID: 203
		protected abstract void InternalWrite(IRecord record);

		// Token: 0x060000CC RID: 204 RVA: 0x00004CBC File Offset: 0x00002EBC
		protected sealed override void Write(IRecord record)
		{
			this.Tracer.TraceDebug<string>((long)this.TracingContext, "Begin {0}", this.operatorName);
			try
			{
				this.DropBreadcrumb(OperatorLocation.BeginWrite);
				this.InternalWrite(record);
				TimeSpan timeSpan = this.DropBreadcrumb(OperatorLocation.EndWrite);
				this.Tracer.TraceDebug<string, double>((long)this.TracingContext, "End {0}. Time elapsed: {1} ms", this.operatorName, timeSpan.TotalMilliseconds);
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
				TimeSpan timeSpan = this.DropBreadcrumb(OperatorLocation.EndWriteException, ex);
				this.Tracer.TraceDebug<string, double, string>((long)this.TracingContext, "End {0}. Time elapsed: {1} ms, Exception: {2}", this.operatorName, timeSpan.TotalMilliseconds, ex.Message);
				throw;
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004D9C File Offset: 0x00002F9C
		protected override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004DA4 File Offset: 0x00002FA4
		protected TimeSpan DropBreadcrumb(OperatorLocation location)
		{
			return this.diagnostics.DropBreadcrumb(location, this.operatorName);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004DB8 File Offset: 0x00002FB8
		protected TimeSpan DropBreadcrumb(OperatorLocation location, Exception exception)
		{
			return this.diagnostics.DropBreadcrumb(location, this.operatorName, exception.Message);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004DD2 File Offset: 0x00002FD2
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

		// Token: 0x04000056 RID: 86
		private readonly string operatorName;

		// Token: 0x04000057 RID: 87
		private DisposeTracker disposeTracker;

		// Token: 0x04000058 RID: 88
		private OperatorDiagnostics diagnostics;
	}
}
