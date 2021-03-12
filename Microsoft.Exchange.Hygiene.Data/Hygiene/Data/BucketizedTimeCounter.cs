using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000066 RID: 102
	public class BucketizedTimeCounter : ITimerCounter, IDisposable
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x0000BC6C File Offset: 0x00009E6C
		public BucketizedTimeCounter(Func<string, ExPerformanceCounter> getInstance, SortedSet<TimeSpan> divisions, bool autoStart = true)
		{
			if (getInstance == null)
			{
				throw new ArgumentNullException("getInstance");
			}
			if (divisions == null || divisions.Count == 0)
			{
				throw new ArgumentNullException("divisions");
			}
			if (divisions.Any((TimeSpan d) => d == TimeSpan.Zero))
			{
				throw new ArgumentException("TimeSpan.Zero may not be used as a bucket division.");
			}
			this.getInstance = getInstance;
			this.divisions = divisions;
			if (autoStart)
			{
				this.Start();
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000BCEA File Offset: 0x00009EEA
		public void Start()
		{
			this.stopwatch = Stopwatch.StartNew();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public long Stop()
		{
			long elapsedTicks = this.stopwatch.ElapsedTicks;
			this.AddSample(this.stopwatch.Elapsed);
			this.stopwatch = null;
			return elapsedTicks;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000BD2A File Offset: 0x00009F2A
		void IDisposable.Dispose()
		{
			if (this.stopwatch != null)
			{
				this.Stop();
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000BD60 File Offset: 0x00009F60
		internal void AddSample(TimeSpan elapsed)
		{
			TimeSpan timeSpan = this.divisions.LastOrDefault((TimeSpan l) => l <= elapsed);
			TimeSpan timeSpan2 = this.divisions.FirstOrDefault((TimeSpan u) => u > elapsed);
			if (timeSpan == TimeSpan.Zero && timeSpan2 != TimeSpan.Zero)
			{
				this.getInstance(string.Format("< {0}", timeSpan2)).Increment();
				return;
			}
			if (timeSpan != TimeSpan.Zero && timeSpan2 != TimeSpan.Zero)
			{
				this.getInstance(string.Format("[{0}, {1})", timeSpan, timeSpan2)).Increment();
				return;
			}
			if (timeSpan != TimeSpan.Zero && timeSpan2 == TimeSpan.Zero)
			{
				this.getInstance(string.Format(">= {0}", timeSpan)).Increment();
			}
		}

		// Token: 0x04000275 RID: 629
		private Stopwatch stopwatch;

		// Token: 0x04000276 RID: 630
		private Func<string, ExPerformanceCounter> getInstance;

		// Token: 0x04000277 RID: 631
		private SortedSet<TimeSpan> divisions;
	}
}
