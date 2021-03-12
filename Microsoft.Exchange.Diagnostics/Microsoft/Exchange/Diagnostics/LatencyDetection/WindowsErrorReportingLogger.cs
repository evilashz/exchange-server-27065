using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class WindowsErrorReportingLogger : ILatencyDetectionLogger
	{
		// Token: 0x06000B10 RID: 2832 RVA: 0x00028701 File Offset: 0x00026901
		private WindowsErrorReportingLogger()
		{
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00028714 File Offset: 0x00026914
		public LoggingType Type
		{
			get
			{
				return LoggingType.WindowsErrorReporting;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00028717 File Offset: 0x00026917
		internal static ILatencyDetectionLogger Instance
		{
			get
			{
				return WindowsErrorReportingLogger.singletonInstance;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00028720 File Offset: 0x00026920
		public void Log(LatencyReportingThreshold threshold, LatencyDetectionContext trigger, ICollection<LatencyDetectionContext> context, LatencyDetectionException exception)
		{
			if (threshold == null)
			{
				throw new ArgumentNullException("threshold");
			}
			if (trigger == null)
			{
				throw new ArgumentNullException("trigger");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			DateTime utcNow = DateTime.UtcNow;
			if (ExWatson.LastWatsonReport + TimeSpan.FromMinutes(1.0) < utcNow && this.lastReport + PerformanceReportingOptions.Instance.WatsonThrottle < utcNow)
			{
				this.lastReport = DateTime.UtcNow;
				WindowsErrorReportingLogger.CreateReport(threshold, trigger, context, exception);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000287C0 File Offset: 0x000269C0
		private static void CreateReport(LatencyReportingThreshold threshold, LatencyDetectionContext trigger, ICollection<LatencyDetectionContext> dataToLog, LatencyDetectionException exception)
		{
			StringBuilder stringBuilder = new StringBuilder(Math.Min(LatencyDetectionContext.EstimatedStringCapacity * (dataToLog.Count + 1), 42000));
			stringBuilder.Append("Latency Threshold: ").Append(threshold.Threshold.TotalMilliseconds).AppendLine(" ms");
			stringBuilder.AppendLine("Trigger").AppendLine(trigger.ToString());
			if (dataToLog.Count > 0)
			{
				stringBuilder.Append(dataToLog.Count).AppendLine(" Backlog Entries");
				foreach (LatencyDetectionContext latencyDetectionContext in dataToLog)
				{
					stringBuilder.AppendLine(latencyDetectionContext.ToString());
				}
			}
			stringBuilder.AppendLine(exception.StackTrace);
			string text = trigger.Version;
			if (string.IsNullOrEmpty(text))
			{
				text = "00.00.0000.000";
			}
			string callstack = (trigger.StackTraceContext ?? string.Empty) + Environment.NewLine + exception.StackTrace;
			ExWatson.SendLatencyWatsonReport(text, trigger.Location.Identity, exception.WatsonExceptionName, callstack, exception.WatsonMethodName, stringBuilder.ToString());
		}

		// Token: 0x04000790 RID: 1936
		private static readonly ILatencyDetectionLogger singletonInstance = new WindowsErrorReportingLogger();

		// Token: 0x04000791 RID: 1937
		private DateTime lastReport = DateTime.MinValue;
	}
}
