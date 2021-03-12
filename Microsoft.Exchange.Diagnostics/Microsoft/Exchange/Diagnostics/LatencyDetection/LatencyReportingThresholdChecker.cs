using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LatencyReportingThresholdChecker
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x000274B0 File Offset: 0x000256B0
		private LatencyReportingThresholdChecker()
		{
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000274C3 File Offset: 0x000256C3
		internal static LatencyReportingThresholdChecker Instance
		{
			get
			{
				return LatencyReportingThresholdChecker.singletonInstance;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x000274CC File Offset: 0x000256CC
		internal void ClearHistory()
		{
			foreach (LatencyDetectionLocation latencyDetectionLocation in this.thresholdCollection.Locations.Values)
			{
				latencyDetectionLocation.ClearBackLogs();
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00027524 File Offset: 0x00025724
		internal bool ShouldCreateReport(LatencyDetectionContext currentContext, LoggingType loggingType, out LatencyDetectionContext trigger, out LatencyReportingThreshold thresholdToCheck, out ICollection<LatencyDetectionContext> dataToLog)
		{
			bool flag = false;
			dataToLog = null;
			thresholdToCheck = null;
			trigger = null;
			if (Thread.VolatileRead(ref this.creatingReport) == 0)
			{
				LatencyDetectionLocation location = currentContext.Location;
				BackLog backLog = location.GetBackLog(loggingType);
				thresholdToCheck = location.GetThreshold(loggingType);
				flag = backLog.AddAndQueryThreshold(currentContext);
				if (flag)
				{
					flag = (Interlocked.CompareExchange(ref this.creatingReport, 1, 0) == 0);
					if (flag)
					{
						try
						{
							flag = backLog.IsBeyondThreshold(out trigger);
							if (flag)
							{
								dataToLog = this.MoveBacklogDataToReport(loggingType);
							}
						}
						finally
						{
							Thread.VolatileWrite(ref this.creatingReport, 0);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000275B8 File Offset: 0x000257B8
		private ICollection<LatencyDetectionContext> MoveBacklogDataToReport(LoggingType type)
		{
			IDictionary<string, LatencyDetectionLocation> locations = this.thresholdCollection.Locations;
			int count = locations.Count;
			List<LatencyDetectionContext> list = null;
			foreach (KeyValuePair<string, LatencyDetectionLocation> keyValuePair in locations)
			{
				BackLog backLog = keyValuePair.Value.GetBackLog(type);
				if (list == null)
				{
					list = new List<LatencyDetectionContext>(count * backLog.Count);
				}
				backLog.MoveToList(list);
			}
			return list;
		}

		// Token: 0x0400073F RID: 1855
		private static readonly LatencyReportingThresholdChecker singletonInstance = new LatencyReportingThresholdChecker();

		// Token: 0x04000740 RID: 1856
		private readonly LatencyReportingThresholdContainer thresholdCollection = LatencyReportingThresholdContainer.Instance;

		// Token: 0x04000741 RID: 1857
		private int creatingReport;
	}
}
