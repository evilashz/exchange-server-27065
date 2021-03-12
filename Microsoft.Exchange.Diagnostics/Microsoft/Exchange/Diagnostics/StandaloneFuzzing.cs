using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000161 RID: 353
	internal static class StandaloneFuzzing
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00026150 File Offset: 0x00024350
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00026175 File Offset: 0x00024375
		public static bool IsEnabled
		{
			get
			{
				return StandaloneFuzzing.isFuzzingEnabled ?? false;
			}
			set
			{
				if (StandaloneFuzzing.isFuzzingEnabled != null)
				{
					throw new InvalidOperationException("Can set StandaloneFuzzing.IsEnabled only once.");
				}
				StandaloneFuzzing.isFuzzingEnabled = new bool?(value);
			}
		}

		// Token: 0x040006E5 RID: 1765
		private static bool? isFuzzingEnabled;
	}
}
