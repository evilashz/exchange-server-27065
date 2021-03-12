using System;
using System.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200002C RID: 44
	public sealed class PerfDateTime
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00008898 File Offset: 0x00006A98
		public PerfDateTime()
		{
			PerfDateTime.EnsureStopwatch();
			this.initialElapsedTicks = PerfDateTime.stopwatch.ElapsedTicks;
			this.initialDateTime = DateTime.UtcNow;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000088C0 File Offset: 0x00006AC0
		public DateTime UtcNow
		{
			get
			{
				double value = (double)(PerfDateTime.stopwatch.ElapsedTicks - this.initialElapsedTicks) / PerfDateTime.stopwatchFrequency;
				return this.initialDateTime.AddSeconds(value);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000088F8 File Offset: 0x00006AF8
		private static void EnsureStopwatch()
		{
			if (PerfDateTime.stopwatch == null)
			{
				lock (PerfDateTime.stopwatchLock)
				{
					if (PerfDateTime.stopwatch == null)
					{
						PerfDateTime.stopwatch = Stopwatch.StartNew();
						PerfDateTime.stopwatchFrequency = (double)Stopwatch.Frequency;
					}
				}
			}
		}

		// Token: 0x040000D6 RID: 214
		private static readonly object stopwatchLock = new object();

		// Token: 0x040000D7 RID: 215
		private static Stopwatch stopwatch;

		// Token: 0x040000D8 RID: 216
		private static double stopwatchFrequency;

		// Token: 0x040000D9 RID: 217
		private readonly long initialElapsedTicks;

		// Token: 0x040000DA RID: 218
		private readonly DateTime initialDateTime;
	}
}
