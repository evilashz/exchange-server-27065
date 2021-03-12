using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Common;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000172 RID: 370
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LatencyEventLogger : ILatencyDetectionLogger
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002735A File Offset: 0x0002555A
		public LoggingType Type
		{
			get
			{
				return LoggingType.EventLog;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00027360 File Offset: 0x00025560
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
			LatencyEventLogger.eventLogger.LogEvent(CommonEventLogConstants.Tuple_LatencyDetection, string.Empty, new object[]
			{
				threshold.Threshold.TotalMilliseconds.ToString(),
				string.Format("Trigger:{0}", trigger.ToString("s"))
			});
		}

		// Token: 0x04000732 RID: 1842
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.CommonTracer.Category, "MSExchange Common");
	}
}
