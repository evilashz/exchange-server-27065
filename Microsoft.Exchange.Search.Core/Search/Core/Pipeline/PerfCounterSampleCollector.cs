using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Performance;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Pipeline
{
	// Token: 0x020000A4 RID: 164
	internal sealed class PerfCounterSampleCollector
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x0000F398 File Offset: 0x0000D598
		internal PerfCounterSampleCollector(PipelineCountersInstance instancePerfCounter, IDiagnosticsSession diagnosticsSession)
		{
			this.instancePerfCounter = instancePerfCounter;
			this.diagnosticsSession = diagnosticsSession;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000F3AE File Offset: 0x0000D5AE
		internal void Start()
		{
			this.stopWatch = Stopwatch.StartNew();
			this.IncrementCounter(this.instancePerfCounter.NumberOfOutstandingDocuments);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		internal void Stop(bool succeeded)
		{
			this.stopWatch.Stop();
			this.DecrementCounter(this.instancePerfCounter.NumberOfOutstandingDocuments);
			this.IncrementCounter(this.instancePerfCounter.NumberOfProcessedDocuments);
			this.IncrementCounterBy(this.instancePerfCounter.AverageDocumentProcessingTime, this.stopWatch.ElapsedTicks);
			this.IncrementCounter(this.instancePerfCounter.AverageDocumentProcessingTimeBase);
			if (succeeded)
			{
				this.IncrementCounter(this.instancePerfCounter.NumberOfSucceededDocuments);
				return;
			}
			this.IncrementCounter(this.instancePerfCounter.NumberOfFailedDocuments);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0000F459 File Offset: 0x0000D659
		private void DecrementCounter(ExPerformanceCounter counter)
		{
			this.diagnosticsSession.DecrementCounter(counter);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0000F467 File Offset: 0x0000D667
		private void IncrementCounter(ExPerformanceCounter counter)
		{
			this.diagnosticsSession.IncrementCounter(counter);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0000F475 File Offset: 0x0000D675
		private void IncrementCounterBy(ExPerformanceCounter counter, long incrementValue)
		{
			this.diagnosticsSession.IncrementCounterBy(counter, incrementValue);
		}

		// Token: 0x04000243 RID: 579
		private readonly PipelineCountersInstance instancePerfCounter;

		// Token: 0x04000244 RID: 580
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x04000245 RID: 581
		private Stopwatch stopWatch;
	}
}
