using System;

namespace Microsoft.Exchange.Diagnostics.Performance
{
	// Token: 0x020001D3 RID: 467
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NullPerformanceDataLogger : IPerformanceDataLogger
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x00037020 File Offset: 0x00035220
		private NullPerformanceDataLogger()
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00037028 File Offset: 0x00035228
		public void Log(string marker, string counter, TimeSpan value)
		{
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003702A File Offset: 0x0003522A
		public void Log(string marker, string counter, uint value)
		{
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003702C File Offset: 0x0003522C
		public void Log(string marker, string counter, string value)
		{
		}

		// Token: 0x040009AB RID: 2475
		public static readonly IPerformanceDataLogger Instance = new NullPerformanceDataLogger();
	}
}
