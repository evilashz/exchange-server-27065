using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LatencyDetectionLocation
	{
		// Token: 0x06000A78 RID: 2680 RVA: 0x0002711C File Offset: 0x0002531C
		internal LatencyDetectionLocation(IThresholdInitializer thresholdInitializer, string identity, TimeSpan minimumThreshold, TimeSpan defaultThreshold)
		{
			if (string.IsNullOrEmpty(identity))
			{
				throw new ArgumentException("Shouldn't be null or empty.", "identity");
			}
			if (minimumThreshold < LatencyReportingThreshold.MinimumThresholdValue)
			{
				throw new ArgumentException("May not be less than global minimum threshold.", "minimumThreshold");
			}
			if (defaultThreshold < minimumThreshold)
			{
				throw new ArgumentException("May not be less than minimum threshold.", "defaultThreshold");
			}
			this.identity = identity;
			this.minimumThreshold = minimumThreshold;
			this.defaultThreshold = defaultThreshold;
			for (int i = 0; i < LatencyDetectionLocation.ArrayLength; i++)
			{
				this.backlogs[i] = new BackLog(this.thresholds[i]);
				thresholdInitializer.SetThresholdFromConfiguration(this, (LoggingType)i);
			}
			LatencyReportingThresholdContainer.Instance.ValidateLocation(this);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x000271EC File Offset: 0x000253EC
		internal string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x000271F4 File Offset: 0x000253F4
		internal TimeSpan MinimumThreshold
		{
			get
			{
				return this.minimumThreshold;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x000271FC File Offset: 0x000253FC
		internal TimeSpan DefaultThreshold
		{
			get
			{
				return this.defaultThreshold;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00027204 File Offset: 0x00025404
		internal LatencyReportingThreshold GetThreshold(LoggingType type)
		{
			return this.thresholds[(int)type];
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0002720E File Offset: 0x0002540E
		internal LatencyReportingThreshold GetThreshold(int type)
		{
			LatencyDetectionLocation.CheckLoggingTypeIndex(type);
			return this.thresholds[type];
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002721E File Offset: 0x0002541E
		internal BackLog GetBackLog(LoggingType type)
		{
			return this.backlogs[(int)type];
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00027228 File Offset: 0x00025428
		internal BackLog GetBackLog(int type)
		{
			LatencyDetectionLocation.CheckLoggingTypeIndex(type);
			return this.backlogs[type];
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00027238 File Offset: 0x00025438
		internal void ClearBackLogs()
		{
			for (int i = 0; i < LatencyDetectionLocation.ArrayLength; i++)
			{
				this.backlogs[i].Clear();
			}
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x00027264 File Offset: 0x00025464
		internal void ClearThresholds()
		{
			foreach (object obj in Enum.GetValues(typeof(LoggingType)))
			{
				LoggingType logType = (LoggingType)obj;
				this.SetThreshold(logType, TimeSpan.MaxValue);
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000272CC File Offset: 0x000254CC
		internal LatencyReportingThreshold SetThreshold(LoggingType logType, TimeSpan threshold)
		{
			LatencyReportingThreshold latencyReportingThreshold = new LatencyReportingThreshold(logType, threshold);
			this.thresholds[(int)logType] = latencyReportingThreshold;
			this.backlogs[(int)logType].ChangeThresholdAndClear(latencyReportingThreshold);
			return latencyReportingThreshold;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x000272FB File Offset: 0x000254FB
		private static void CheckLoggingTypeIndex(int type)
		{
			if (type < 0 || type >= LatencyDetectionLocation.ArrayLength)
			{
				throw new ArgumentOutOfRangeException("type", type, LatencyDetectionLocation.TypeLimits);
			}
		}

		// Token: 0x0400072B RID: 1835
		internal static readonly int ArrayLength = Enum.GetValues(typeof(LoggingType)).Length;

		// Token: 0x0400072C RID: 1836
		private static readonly string TypeLimits = "Needs to be 0 to " + (LatencyDetectionLocation.ArrayLength - 1) + ".";

		// Token: 0x0400072D RID: 1837
		private readonly string identity;

		// Token: 0x0400072E RID: 1838
		private readonly TimeSpan minimumThreshold;

		// Token: 0x0400072F RID: 1839
		private readonly TimeSpan defaultThreshold;

		// Token: 0x04000730 RID: 1840
		private readonly LatencyReportingThreshold[] thresholds = new LatencyReportingThreshold[LatencyDetectionLocation.ArrayLength];

		// Token: 0x04000731 RID: 1841
		private readonly BackLog[] backlogs = new BackLog[LatencyDetectionLocation.ArrayLength];
	}
}
