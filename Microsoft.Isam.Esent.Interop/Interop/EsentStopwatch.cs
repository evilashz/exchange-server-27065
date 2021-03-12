using System;
using System.Diagnostics;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000224 RID: 548
	public class EsentStopwatch
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x00014FC4 File Offset: 0x000131C4
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00014FCC File Offset: 0x000131CC
		public bool IsRunning { get; private set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00014FD5 File Offset: 0x000131D5
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00014FDD File Offset: 0x000131DD
		public JET_THREADSTATS ThreadStats { get; private set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00014FE6 File Offset: 0x000131E6
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00014FEE File Offset: 0x000131EE
		public TimeSpan Elapsed { get; private set; }

		// Token: 0x06000A61 RID: 2657 RVA: 0x00014FF8 File Offset: 0x000131F8
		public static EsentStopwatch StartNew()
		{
			EsentStopwatch esentStopwatch = new EsentStopwatch();
			esentStopwatch.Start();
			return esentStopwatch;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00015014 File Offset: 0x00013214
		public override string ToString()
		{
			if (!this.IsRunning)
			{
				return this.Elapsed.ToString();
			}
			return "EsentStopwatch (running)";
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00015043 File Offset: 0x00013243
		public void Start()
		{
			this.Reset();
			this.stopwatch = Stopwatch.StartNew();
			this.IsRunning = true;
			if (EsentVersion.SupportsVistaFeatures)
			{
				VistaApi.JetGetThreadStats(out this.statsAtStart);
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00015070 File Offset: 0x00013270
		public void Stop()
		{
			if (this.IsRunning)
			{
				this.IsRunning = false;
				this.stopwatch.Stop();
				this.Elapsed = this.stopwatch.Elapsed;
				if (EsentVersion.SupportsVistaFeatures)
				{
					JET_THREADSTATS jet_THREADSTATS;
					VistaApi.JetGetThreadStats(out jet_THREADSTATS);
					this.ThreadStats = jet_THREADSTATS - this.statsAtStart;
				}
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000150C8 File Offset: 0x000132C8
		public void Reset()
		{
			this.stopwatch = null;
			this.ThreadStats = default(JET_THREADSTATS);
			this.Elapsed = TimeSpan.Zero;
			this.IsRunning = false;
		}

		// Token: 0x0400033A RID: 826
		private Stopwatch stopwatch;

		// Token: 0x0400033B RID: 827
		private JET_THREADSTATS statsAtStart;
	}
}
