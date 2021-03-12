using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x0200017B RID: 379
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PerformanceReporter
	{
		// Token: 0x06000ACE RID: 2766 RVA: 0x00027C48 File Offset: 0x00025E48
		private PerformanceReporter()
		{
			this.SetLogger(WindowsErrorReportingLogger.Instance);
			if (PerformanceReportingOptions.Instance.EnableLatencyEventLogging)
			{
				this.SetLogger(new LatencyEventLogger());
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00027CB2 File Offset: 0x00025EB2
		public static PerformanceReporter Instance
		{
			get
			{
				return PerformanceReporter.singletonInstance;
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00027CB9 File Offset: 0x00025EB9
		public void ClearHistory()
		{
			this.checker.ClearHistory();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00027CC6 File Offset: 0x00025EC6
		public void ClearThresholds()
		{
			LatencyReportingThresholdContainer.Instance.Clear();
			this.ClearHistory();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00027CD8 File Offset: 0x00025ED8
		public bool HasHistory(string locationID)
		{
			if (string.IsNullOrEmpty(locationID))
			{
				throw new ArgumentNullException("locationID");
			}
			bool flag = false;
			LatencyDetectionLocation latencyDetectionLocation;
			if (this.container.Locations.TryGetValue(locationID, out latencyDetectionLocation))
			{
				foreach (object obj in Enum.GetValues(typeof(LoggingType)))
				{
					LoggingType type = (LoggingType)obj;
					BackLog backLog = latencyDetectionLocation.GetBackLog(type);
					flag = (backLog.Count > 0);
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00027D7C File Offset: 0x00025F7C
		public void SetLogger(ILatencyDetectionLogger logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			LoggingType type = logger.Type;
			if (this.loggers.ContainsKey(type))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Can't set logger, because one is already set. You must first call RemoveLogger({0}.{1}).", new object[]
				{
					typeof(LoggingType),
					type
				}));
			}
			this.loggers.Add(type, logger);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00027DEC File Offset: 0x00025FEC
		public void SetDefaultWindowsErrorReportingLogger()
		{
			this.RemoveLogger(LoggingType.WindowsErrorReporting);
			this.SetLogger(WindowsErrorReportingLogger.Instance);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00027E00 File Offset: 0x00026000
		public void RemoveLogger(LoggingType type)
		{
			this.loggers.Remove(type);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00027E0F File Offset: 0x0002600F
		public bool HasLogger(LoggingType type)
		{
			return this.loggers.ContainsKey(type);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00027E20 File Offset: 0x00026020
		internal void Log(LatencyDetectionContext context)
		{
			foreach (KeyValuePair<LoggingType, ILatencyDetectionLogger> keyValuePair in this.loggers)
			{
				ILatencyDetectionLogger value = keyValuePair.Value;
				LoggingType key = keyValuePair.Key;
				LatencyDetectionContext latencyDetectionContext;
				LatencyReportingThreshold threshold;
				ICollection<LatencyDetectionContext> backlog;
				bool flag = this.checker.ShouldCreateReport(context, key, out latencyDetectionContext, out threshold, out backlog);
				if (flag)
				{
					LatencyDetectionException exception = latencyDetectionContext.CreateLatencyDetectionException();
					PerformanceReporter.LogData state = new PerformanceReporter.LogData(value, threshold, latencyDetectionContext, backlog, exception);
					ThreadPool.QueueUserWorkItem(PerformanceReporter.LogReportDelegate, state);
				}
			}
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00027EBC File Offset: 0x000260BC
		private static void ReportingWorker(object state)
		{
			PerformanceReporter.LogData logData = (PerformanceReporter.LogData)state;
			logData.Logger.Log(logData.Threshold, logData.Trigger, logData.Backlog, logData.Exception);
		}

		// Token: 0x04000757 RID: 1879
		private static readonly WaitCallback LogReportDelegate = new WaitCallback(PerformanceReporter.ReportingWorker);

		// Token: 0x04000758 RID: 1880
		private static readonly PerformanceReporter singletonInstance = new PerformanceReporter();

		// Token: 0x04000759 RID: 1881
		private readonly IDictionary<LoggingType, ILatencyDetectionLogger> loggers = new Dictionary<LoggingType, ILatencyDetectionLogger>(Enum.GetValues(typeof(LoggingType)).Length);

		// Token: 0x0400075A RID: 1882
		private readonly LatencyReportingThresholdChecker checker = LatencyReportingThresholdChecker.Instance;

		// Token: 0x0400075B RID: 1883
		private readonly LatencyReportingThresholdContainer container = LatencyReportingThresholdContainer.Instance;

		// Token: 0x0200017C RID: 380
		private class LogData
		{
			// Token: 0x06000ADA RID: 2778 RVA: 0x00027F10 File Offset: 0x00026110
			internal LogData(ILatencyDetectionLogger logger, LatencyReportingThreshold threshold, LatencyDetectionContext trigger, ICollection<LatencyDetectionContext> backlog, LatencyDetectionException exception)
			{
				this.logger = logger;
				this.threshold = threshold;
				this.trigger = trigger;
				this.backlog = backlog;
				this.exception = exception;
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00027F3D File Offset: 0x0002613D
			internal LatencyReportingThreshold Threshold
			{
				get
				{
					return this.threshold;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00027F45 File Offset: 0x00026145
			internal LatencyDetectionContext Trigger
			{
				get
				{
					return this.trigger;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00027F4D File Offset: 0x0002614D
			internal ICollection<LatencyDetectionContext> Backlog
			{
				get
				{
					return this.backlog;
				}
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00027F55 File Offset: 0x00026155
			internal ILatencyDetectionLogger Logger
			{
				get
				{
					return this.logger;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00027F5D File Offset: 0x0002615D
			internal LatencyDetectionException Exception
			{
				get
				{
					return this.exception;
				}
			}

			// Token: 0x0400075C RID: 1884
			private LatencyReportingThreshold threshold;

			// Token: 0x0400075D RID: 1885
			private LatencyDetectionContext trigger;

			// Token: 0x0400075E RID: 1886
			private ICollection<LatencyDetectionContext> backlog;

			// Token: 0x0400075F RID: 1887
			private ILatencyDetectionLogger logger;

			// Token: 0x04000760 RID: 1888
			private LatencyDetectionException exception;
		}
	}
}
