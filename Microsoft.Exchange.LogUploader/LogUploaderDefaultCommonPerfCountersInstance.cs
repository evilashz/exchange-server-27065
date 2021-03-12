using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000003 RID: 3
	internal class LogUploaderDefaultCommonPerfCountersInstance : ILogUploaderPerformanceCounters
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000020D0 File Offset: 0x000002D0
		public LogUploaderDefaultCommonPerfCountersInstance(string instanceName, LogUploaderDefaultCommonPerfCountersInstance autoUpdateTotalInstance = null)
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes processed/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalLogBytesProcessed = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes processed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalLogBytesProcessed, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalLogBytesProcessed);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log parse errors/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalParseErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log parse errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalParseErrors, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalParseErrors);
				this.BatchQueueLength = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log batch current queue length", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.BatchQueueLength, new ExPerformanceCounter[0]);
				list.Add(this.BatchQueueLength);
				this.InputBufferBatchCounts = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Input buffer batch count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InputBufferBatchCounts, new ExPerformanceCounter[0]);
				list.Add(this.InputBufferBatchCounts);
				this.InputBufferBackfilledLines = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Input buffer backfilled lines/sec", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.InputBufferBackfilledLines, new ExPerformanceCounter[0]);
				list.Add(this.InputBufferBackfilledLines);
				this.LogsNeverProcessedBefore = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Number of logs that have never been processed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogsNeverProcessedBefore, new ExPerformanceCounter[0]);
				list.Add(this.LogsNeverProcessedBefore);
				this.AverageDbWriteLatency = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Average DB write time in seconds", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageDbWriteLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageDbWriteLatency);
				this.AverageDbWriteLatencyBase = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Average DB write time in seconds Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageDbWriteLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageDbWriteLatencyBase);
				this.AverageInactiveParseLatency = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Average inactive log parse time in seconds", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageInactiveParseLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageInactiveParseLatency);
				this.AverageInactiveParseLatencyBase = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Average inactive log parse time in seconds Base", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageInactiveParseLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageInactiveParseLatencyBase);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log lines written to database/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalLogLinesProcessed = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total log lines written to database", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalLogLinesProcessed, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalLogLinesProcessed);
				this.TotalIncompleteLogs = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of incomplete logs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalIncompleteLogs, new ExPerformanceCounter[0]);
				list.Add(this.TotalIncompleteLogs);
				this.TotalIncomingLogs = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of incoming logs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalIncomingLogs, new ExPerformanceCounter[0]);
				list.Add(this.TotalIncomingLogs);
				this.NumberOfIncomingLogs = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Number of new logs generated in the last directory scan interval", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NumberOfIncomingLogs, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfIncomingLogs);
				this.TotalCompletedLogs = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Number of logs that are completely processed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalCompletedLogs, new ExPerformanceCounter[0]);
				list.Add(this.TotalCompletedLogs);
				this.TotalNewLogsBeginProcessing = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Number of new logs begin to be processed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalNewLogsBeginProcessing, new ExPerformanceCounter[0]);
				list.Add(this.TotalNewLogsBeginProcessing);
				this.TotalOpticsTraces = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of traces sent to optics pipelines", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalOpticsTraces, new ExPerformanceCounter[0]);
				list.Add(this.TotalOpticsTraces);
				this.TotalOpticsTraceExtractionErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Optics trace data extraction errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalOpticsTraceExtractionErrors, new ExPerformanceCounter[0]);
				list.Add(this.TotalOpticsTraceExtractionErrors);
				this.OpticsTracesPerSecond = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Rate of traces sent to optics pipelines in seconds.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OpticsTracesPerSecond, new ExPerformanceCounter[0]);
				list.Add(this.OpticsTracesPerSecond);
				this.OpticsTraceExtractionErrorsPerSecond = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Rate of optics traces extraction errors.", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.OpticsTraceExtractionErrorsPerSecond, new ExPerformanceCounter[0]);
				list.Add(this.OpticsTraceExtractionErrorsPerSecond);
				this.TotalInvalidLogLineParseErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of log parse errors because of invalid log line", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalInvalidLogLineParseErrors, new ExPerformanceCounter[0]);
				list.Add(this.TotalInvalidLogLineParseErrors);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "DB writes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalDBWrite = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of database writes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalDBWrite, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalDBWrite);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "DAL permanent errors/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.TotalDBPermanentErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of database permanent errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalDBPermanentErrors, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.TotalDBPermanentErrors);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "database transient errors/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.TotalDBTransientErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of database transient errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalDBTransientErrors, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.TotalDBTransientErrors);
				this.TotalUnexpectedWriterErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of unexpected writer errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalUnexpectedWriterErrors, new ExPerformanceCounter[0]);
				list.Add(this.TotalUnexpectedWriterErrors);
				this.TotalLogReaderUnknownErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of unexpected log reader errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalLogReaderUnknownErrors, new ExPerformanceCounter[0]);
				list.Add(this.TotalLogReaderUnknownErrors);
				this.TotalMessageTracingDualWriteErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of MessageTracing dual write errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMessageTracingDualWriteErrors, new ExPerformanceCounter[0]);
				list.Add(this.TotalMessageTracingDualWriteErrors);
				this.DirectoryCheck = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Directory check numbers", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DirectoryCheck, new ExPerformanceCounter[0]);
				list.Add(this.DirectoryCheck);
				this.RawIncompleteBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data unprocessed bytes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RawIncompleteBytes, new ExPerformanceCounter[0]);
				list.Add(this.RawIncompleteBytes);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data input/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.RawTotalLogBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data input bytes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RawTotalLogBytes, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.RawTotalLogBytes);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data parsed bytes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.RawReaderParsedBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data parsed bytes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RawReaderParsedBytes, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.RawReaderParsedBytes);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data processed bytes/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.RawWrittenBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Combo raw data processed bytes", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.RawWrittenBytes, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.RawWrittenBytes);
				this.IncompleteBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes unprocessed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.IncompleteBytes, new ExPerformanceCounter[0]);
				list.Add(this.IncompleteBytes);
				ExPerformanceCounter exPerformanceCounter10 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes input/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter10);
				this.TotalLogBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes input", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalLogBytes, new ExPerformanceCounter[]
				{
					exPerformanceCounter10
				});
				list.Add(this.TotalLogBytes);
				ExPerformanceCounter exPerformanceCounter11 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes parsed/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter11);
				this.ReaderParsedBytes = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Log bytes parsed", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReaderParsedBytes, new ExPerformanceCounter[]
				{
					exPerformanceCounter11
				});
				list.Add(this.ReaderParsedBytes);
				this.DBWriteActiveTime = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "DB Write Active Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.DBWriteActiveTime, new ExPerformanceCounter[0]);
				list.Add(this.DBWriteActiveTime);
				this.LogReaderActiveTime = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "LogReader Active Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogReaderActiveTime, new ExPerformanceCounter[0]);
				list.Add(this.LogReaderActiveTime);
				this.LogMonitorActiveTime = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "LogMonitor Active Time", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.LogMonitorActiveTime, new ExPerformanceCounter[0]);
				list.Add(this.LogMonitorActiveTime);
				this.ThreadSafeQueueConsumerSemaphoreCount = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "queue consumer semaphore count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ThreadSafeQueueConsumerSemaphoreCount, new ExPerformanceCounter[0]);
				list.Add(this.ThreadSafeQueueConsumerSemaphoreCount);
				this.ThreadSafeQueueProducerSemaphoreCount = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "queue producer semaphore count", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ThreadSafeQueueProducerSemaphoreCount, new ExPerformanceCounter[0]);
				list.Add(this.ThreadSafeQueueProducerSemaphoreCount);
				ExPerformanceCounter exPerformanceCounter12 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Missing certificate errors/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter12);
				this.TotalMissingCertificateErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of missing certificate errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMissingCertificateErrors, new ExPerformanceCounter[]
				{
					exPerformanceCounter12
				});
				list.Add(this.TotalMissingCertificateErrors);
				ExPerformanceCounter exPerformanceCounter13 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Saving MessageTypeMapping Saved/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter13);
				this.TotalMessageTypeMappingSaved = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of MessageTypeMapping saved", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMessageTypeMappingSaved, new ExPerformanceCounter[]
				{
					exPerformanceCounter13
				});
				list.Add(this.TotalMessageTypeMappingSaved);
				ExPerformanceCounter exPerformanceCounter14 = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Saving MessageTypeMappingerrors/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter14);
				this.TotalMessageTypeMappingErrors = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of saving MessageTypeMapping errors", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalMessageTypeMappingErrors, new ExPerformanceCounter[]
				{
					exPerformanceCounter14
				});
				list.Add(this.TotalMessageTypeMappingErrors);
				this.TotalRobocopyIncompleteLogs = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of robo copy incomplete logs", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalRobocopyIncompleteLogs, new ExPerformanceCounter[0]);
				list.Add(this.TotalRobocopyIncompleteLogs);
				this.WriterPoisonDataBatch = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of writing poison batches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.WriterPoisonDataBatch, new ExPerformanceCounter[0]);
				list.Add(this.WriterPoisonDataBatch);
				this.ReaderPoisonDataBatch = new ExPerformanceCounter("Microsoft Forefront Message Tracing service counters.", "Total number of parsing poison batches", instanceName, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ReaderPoisonDataBatch, new ExPerformanceCounter[0]);
				list.Add(this.ReaderPoisonDataBatch);
				long num = this.TotalLogBytesProcessed.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter15 in list)
					{
						exPerformanceCounter15.Close();
					}
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002D24 File Offset: 0x00000F24
		public ExPerformanceCounter TotalLogBytesProcessed { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D2D File Offset: 0x00000F2D
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002D35 File Offset: 0x00000F35
		public ExPerformanceCounter TotalParseErrors { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002D3E File Offset: 0x00000F3E
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002D46 File Offset: 0x00000F46
		public ExPerformanceCounter BatchQueueLength { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002D4F File Offset: 0x00000F4F
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002D57 File Offset: 0x00000F57
		public ExPerformanceCounter InputBufferBatchCounts { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002D60 File Offset: 0x00000F60
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002D68 File Offset: 0x00000F68
		public ExPerformanceCounter InputBufferBackfilledLines { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002D71 File Offset: 0x00000F71
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002D79 File Offset: 0x00000F79
		public ExPerformanceCounter LogsNeverProcessedBefore { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D82 File Offset: 0x00000F82
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002D8A File Offset: 0x00000F8A
		public ExPerformanceCounter AverageDbWriteLatency { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D93 File Offset: 0x00000F93
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002D9B File Offset: 0x00000F9B
		public ExPerformanceCounter AverageDbWriteLatencyBase { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002DA4 File Offset: 0x00000FA4
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002DAC File Offset: 0x00000FAC
		public ExPerformanceCounter AverageInactiveParseLatency { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002DB5 File Offset: 0x00000FB5
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002DBD File Offset: 0x00000FBD
		public ExPerformanceCounter AverageInactiveParseLatencyBase { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002DC6 File Offset: 0x00000FC6
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002DCE File Offset: 0x00000FCE
		public ExPerformanceCounter TotalLogLinesProcessed { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DD7 File Offset: 0x00000FD7
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002DDF File Offset: 0x00000FDF
		public ExPerformanceCounter TotalIncompleteLogs { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DE8 File Offset: 0x00000FE8
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public ExPerformanceCounter TotalIncomingLogs { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DF9 File Offset: 0x00000FF9
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002E01 File Offset: 0x00001001
		public ExPerformanceCounter NumberOfIncomingLogs { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E0A File Offset: 0x0000100A
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002E12 File Offset: 0x00001012
		public ExPerformanceCounter TotalCompletedLogs { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002E1B File Offset: 0x0000101B
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002E23 File Offset: 0x00001023
		public ExPerformanceCounter TotalNewLogsBeginProcessing { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002E2C File Offset: 0x0000102C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002E34 File Offset: 0x00001034
		public ExPerformanceCounter TotalOpticsTraces { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002E3D File Offset: 0x0000103D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002E45 File Offset: 0x00001045
		public ExPerformanceCounter TotalOpticsTraceExtractionErrors { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002E4E File Offset: 0x0000104E
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002E56 File Offset: 0x00001056
		public ExPerformanceCounter OpticsTracesPerSecond { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002E5F File Offset: 0x0000105F
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002E67 File Offset: 0x00001067
		public ExPerformanceCounter OpticsTraceExtractionErrorsPerSecond { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E78 File Offset: 0x00001078
		public ExPerformanceCounter TotalInvalidLogLineParseErrors { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00002E89 File Offset: 0x00001089
		public ExPerformanceCounter TotalDBWrite { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002E92 File Offset: 0x00001092
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002E9A File Offset: 0x0000109A
		public ExPerformanceCounter TotalDBPermanentErrors { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002EA3 File Offset: 0x000010A3
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002EAB File Offset: 0x000010AB
		public ExPerformanceCounter TotalDBTransientErrors { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002EB4 File Offset: 0x000010B4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002EBC File Offset: 0x000010BC
		public ExPerformanceCounter TotalUnexpectedWriterErrors { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EC5 File Offset: 0x000010C5
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002ECD File Offset: 0x000010CD
		public ExPerformanceCounter TotalLogReaderUnknownErrors { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002ED6 File Offset: 0x000010D6
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002EDE File Offset: 0x000010DE
		public ExPerformanceCounter TotalMessageTracingDualWriteErrors { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002EE7 File Offset: 0x000010E7
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002EEF File Offset: 0x000010EF
		public ExPerformanceCounter DirectoryCheck { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002EF8 File Offset: 0x000010F8
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002F00 File Offset: 0x00001100
		public ExPerformanceCounter RawIncompleteBytes { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002F09 File Offset: 0x00001109
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00002F11 File Offset: 0x00001111
		public ExPerformanceCounter RawTotalLogBytes { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002F1A File Offset: 0x0000111A
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002F22 File Offset: 0x00001122
		public ExPerformanceCounter RawReaderParsedBytes { get; private set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002F2B File Offset: 0x0000112B
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002F33 File Offset: 0x00001133
		public ExPerformanceCounter RawWrittenBytes { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002F3C File Offset: 0x0000113C
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002F44 File Offset: 0x00001144
		public ExPerformanceCounter IncompleteBytes { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002F4D File Offset: 0x0000114D
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002F55 File Offset: 0x00001155
		public ExPerformanceCounter TotalLogBytes { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F5E File Offset: 0x0000115E
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002F66 File Offset: 0x00001166
		public ExPerformanceCounter ReaderParsedBytes { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002F6F File Offset: 0x0000116F
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002F77 File Offset: 0x00001177
		public ExPerformanceCounter DBWriteActiveTime { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002F80 File Offset: 0x00001180
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002F88 File Offset: 0x00001188
		public ExPerformanceCounter LogReaderActiveTime { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002F91 File Offset: 0x00001191
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002F99 File Offset: 0x00001199
		public ExPerformanceCounter LogMonitorActiveTime { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002FA2 File Offset: 0x000011A2
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002FAA File Offset: 0x000011AA
		public ExPerformanceCounter ThreadSafeQueueConsumerSemaphoreCount { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002FB3 File Offset: 0x000011B3
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002FBB File Offset: 0x000011BB
		public ExPerformanceCounter ThreadSafeQueueProducerSemaphoreCount { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002FCC File Offset: 0x000011CC
		public ExPerformanceCounter TotalMissingCertificateErrors { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002FD5 File Offset: 0x000011D5
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002FDD File Offset: 0x000011DD
		public ExPerformanceCounter TotalMessageTypeMappingSaved { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002FE6 File Offset: 0x000011E6
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002FEE File Offset: 0x000011EE
		public ExPerformanceCounter TotalMessageTypeMappingErrors { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002FF7 File Offset: 0x000011F7
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00002FFF File Offset: 0x000011FF
		public ExPerformanceCounter TotalRobocopyIncompleteLogs { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003008 File Offset: 0x00001208
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003010 File Offset: 0x00001210
		public ExPerformanceCounter WriterPoisonDataBatch { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003019 File Offset: 0x00001219
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003021 File Offset: 0x00001221
		public ExPerformanceCounter ReaderPoisonDataBatch { get; private set; }

		// Token: 0x04000001 RID: 1
		private const string CategoryName = "Microsoft Forefront Message Tracing service counters.";
	}
}
