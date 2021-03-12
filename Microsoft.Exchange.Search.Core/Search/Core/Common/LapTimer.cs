using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000072 RID: 114
	internal class LapTimer
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00009433 File Offset: 0x00007633
		public void Reset()
		{
			this.lastReading = TimeSpan.Zero;
			this.stopwatch.Restart();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000944C File Offset: 0x0000764C
		public TimeSpan GetLapTime()
		{
			TimeSpan elapsed = this.stopwatch.Elapsed;
			TimeSpan result = elapsed - this.lastReading;
			this.lastReading = elapsed;
			return result;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000947A File Offset: 0x0000767A
		public TimeSpan GetSplitTime()
		{
			return this.stopwatch.Elapsed;
		}

		// Token: 0x0400012F RID: 303
		private readonly Stopwatch stopwatch = Stopwatch.StartNew();

		// Token: 0x04000130 RID: 304
		private TimeSpan lastReading;
	}
}
