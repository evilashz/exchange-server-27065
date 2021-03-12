using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x02000021 RID: 33
	internal interface IPerformanceCounterAccessor
	{
		// Token: 0x0600007A RID: 122
		void AddDequeueLatency(long latency);

		// Token: 0x0600007B RID: 123
		void AddProcessingCompletionEvent(ProcessingCompletionEvent pEvent, long latency);

		// Token: 0x0600007C RID: 124
		void AddProcessorEvent(ProcessorEvent pEvent);

		// Token: 0x0600007D RID: 125
		void AddQueueEvent(QueueEvent qEvent);

		// Token: 0x0600007E RID: 126
		void UpdateCounters();
	}
}
