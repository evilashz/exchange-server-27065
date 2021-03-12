using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000084 RID: 132
	public struct StopwatchStamp
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x00014263 File Offset: 0x00012463
		public static StopwatchStamp GetStamp()
		{
			return new StopwatchStamp(Stopwatch.GetTimestamp());
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001426F File Offset: 0x0001246F
		private StopwatchStamp(long startTicks)
		{
			this.startTicks = startTicks;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00014278 File Offset: 0x00012478
		public TimeSpan ElapsedTime
		{
			get
			{
				return TimeSpan.FromTicks(StopwatchStamp.ToTimeSpanTicks(Stopwatch.GetTimestamp() - this.startTicks));
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00014290 File Offset: 0x00012490
		public static long ToTimeSpanTicks(long stopwatchTicks)
		{
			return stopwatchTicks * 10000000L / StopwatchStamp.StopwatchTicksPerSecond;
		}

		// Token: 0x04000671 RID: 1649
		private const int MillisecondsPerSecond = 1000;

		// Token: 0x04000672 RID: 1650
		private const int MicrosecondsPerMillisecond = 1000;

		// Token: 0x04000673 RID: 1651
		private const int TimeSpanTicksPerMicrosecond = 10;

		// Token: 0x04000674 RID: 1652
		private const int TimeSpanTicksPerSecond = 10000000;

		// Token: 0x04000675 RID: 1653
		public static readonly long StopwatchTicksPerSecond = Stopwatch.Frequency;

		// Token: 0x04000676 RID: 1654
		private long startTicks;
	}
}
