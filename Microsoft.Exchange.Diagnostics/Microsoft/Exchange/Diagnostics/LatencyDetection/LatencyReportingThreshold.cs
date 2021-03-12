using System;

namespace Microsoft.Exchange.Diagnostics.LatencyDetection
{
	// Token: 0x02000174 RID: 372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LatencyReportingThreshold
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x00027417 File Offset: 0x00025617
		internal LatencyReportingThreshold(LoggingType type, ushort needed, TimeSpan limit, TimeSpan value)
		{
			this.value = ((value >= LatencyReportingThreshold.minimumThresholdValue) ? value : LatencyReportingThreshold.minimumThresholdValue);
			this.loggingType = type;
			this.numberRequired = needed;
			this.requiredWithin = limit;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00027451 File Offset: 0x00025651
		internal LatencyReportingThreshold(LoggingType type, TimeSpan threshold) : this(type, 1, LatencyReportingThreshold.defaultRequiredWithin, threshold)
		{
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00027461 File Offset: 0x00025661
		public static TimeSpan MinimumThresholdValue
		{
			get
			{
				return LatencyReportingThreshold.minimumThresholdValue;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00027468 File Offset: 0x00025668
		public ushort NumberRequired
		{
			get
			{
				return this.numberRequired;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00027470 File Offset: 0x00025670
		public TimeSpan TimeLimit
		{
			get
			{
				return this.requiredWithin;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00027478 File Offset: 0x00025678
		public TimeSpan Threshold
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00027480 File Offset: 0x00025680
		public LoggingType LogType
		{
			get
			{
				return this.loggingType;
			}
		}

		// Token: 0x04000738 RID: 1848
		private const ushort DefaultNumberRequired = 1;

		// Token: 0x04000739 RID: 1849
		private static readonly TimeSpan defaultRequiredWithin = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400073A RID: 1850
		private static readonly TimeSpan minimumThresholdValue = TimeSpan.FromMilliseconds(10.0);

		// Token: 0x0400073B RID: 1851
		private ushort numberRequired;

		// Token: 0x0400073C RID: 1852
		private TimeSpan requiredWithin;

		// Token: 0x0400073D RID: 1853
		private LoggingType loggingType;

		// Token: 0x0400073E RID: 1854
		private TimeSpan value;
	}
}
