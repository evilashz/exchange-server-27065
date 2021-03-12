using System;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000002 RID: 2
	internal interface ILogUploaderPerformanceCounters
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		ExPerformanceCounter TotalLogBytesProcessed { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		ExPerformanceCounter TotalParseErrors { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		ExPerformanceCounter BatchQueueLength { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4
		ExPerformanceCounter InputBufferBatchCounts { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5
		ExPerformanceCounter InputBufferBackfilledLines { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6
		ExPerformanceCounter LogsNeverProcessedBefore { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7
		ExPerformanceCounter AverageDbWriteLatency { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000008 RID: 8
		ExPerformanceCounter AverageDbWriteLatencyBase { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000009 RID: 9
		ExPerformanceCounter AverageInactiveParseLatency { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000A RID: 10
		ExPerformanceCounter AverageInactiveParseLatencyBase { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000B RID: 11
		ExPerformanceCounter TotalLogLinesProcessed { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000C RID: 12
		ExPerformanceCounter TotalIncompleteLogs { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000D RID: 13
		ExPerformanceCounter TotalIncomingLogs { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600000E RID: 14
		ExPerformanceCounter NumberOfIncomingLogs { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600000F RID: 15
		ExPerformanceCounter TotalCompletedLogs { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000010 RID: 16
		ExPerformanceCounter TotalNewLogsBeginProcessing { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000011 RID: 17
		ExPerformanceCounter TotalOpticsTraces { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000012 RID: 18
		ExPerformanceCounter TotalOpticsTraceExtractionErrors { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000013 RID: 19
		ExPerformanceCounter OpticsTracesPerSecond { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000014 RID: 20
		ExPerformanceCounter OpticsTraceExtractionErrorsPerSecond { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000015 RID: 21
		ExPerformanceCounter TotalInvalidLogLineParseErrors { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000016 RID: 22
		ExPerformanceCounter TotalDBWrite { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000017 RID: 23
		ExPerformanceCounter TotalDBPermanentErrors { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000018 RID: 24
		ExPerformanceCounter TotalDBTransientErrors { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000019 RID: 25
		ExPerformanceCounter TotalUnexpectedWriterErrors { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001A RID: 26
		ExPerformanceCounter TotalLogReaderUnknownErrors { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001B RID: 27
		ExPerformanceCounter TotalMessageTracingDualWriteErrors { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001C RID: 28
		ExPerformanceCounter DirectoryCheck { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600001D RID: 29
		ExPerformanceCounter RawIncompleteBytes { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600001E RID: 30
		ExPerformanceCounter RawTotalLogBytes { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600001F RID: 31
		ExPerformanceCounter RawReaderParsedBytes { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000020 RID: 32
		ExPerformanceCounter RawWrittenBytes { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000021 RID: 33
		ExPerformanceCounter IncompleteBytes { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000022 RID: 34
		ExPerformanceCounter TotalLogBytes { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000023 RID: 35
		ExPerformanceCounter ReaderParsedBytes { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000024 RID: 36
		ExPerformanceCounter DBWriteActiveTime { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000025 RID: 37
		ExPerformanceCounter LogReaderActiveTime { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000026 RID: 38
		ExPerformanceCounter LogMonitorActiveTime { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000027 RID: 39
		ExPerformanceCounter ThreadSafeQueueConsumerSemaphoreCount { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000028 RID: 40
		ExPerformanceCounter ThreadSafeQueueProducerSemaphoreCount { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000029 RID: 41
		ExPerformanceCounter TotalMissingCertificateErrors { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002A RID: 42
		ExPerformanceCounter TotalMessageTypeMappingSaved { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600002B RID: 43
		ExPerformanceCounter TotalMessageTypeMappingErrors { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600002C RID: 44
		ExPerformanceCounter TotalRobocopyIncompleteLogs { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600002D RID: 45
		ExPerformanceCounter WriterPoisonDataBatch { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600002E RID: 46
		ExPerformanceCounter ReaderPoisonDataBatch { get; }
	}
}
