using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000018 RID: 24
	internal class TimeTracer : DisposableBase
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00039E82 File Offset: 0x00038082
		public TimeTracer(string text)
		{
			this.text = text;
			this.start = Stopwatch.GetTimestamp();
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00039E9C File Offset: 0x0003809C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TimeTracer>(this);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00039EA4 File Offset: 0x000380A4
		protected override void InternalDispose(bool calledFromDispose)
		{
			this.end = Stopwatch.GetTimestamp();
			if (ExTraceGlobals.RopTimingTracer.IsTraceEnabled(TraceType.PerformanceTrace))
			{
				ExTraceGlobals.RopTimingTracer.TracePerformance<string, TimeSpan>(0L, "{0}: Operation took {1}", this.text, TimeTracer.GetTimeSpanFromTicks(this.end - this.start));
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00039EF4 File Offset: 0x000380F4
		private static TimeSpan GetTimeSpanFromTicks(long ticks)
		{
			double num = (double)ticks / (double)TimeTracer.frequency;
			return new TimeSpan((long)((int)(num * 10000000.0)));
		}

		// Token: 0x04000188 RID: 392
		private static long frequency = Stopwatch.Frequency;

		// Token: 0x04000189 RID: 393
		private long start;

		// Token: 0x0400018A RID: 394
		private long end;

		// Token: 0x0400018B RID: 395
		private string text;
	}
}
