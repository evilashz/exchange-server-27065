using System;

namespace System.Threading
{
	// Token: 0x0200050C RID: 1292
	internal static class PlatformHelper
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x000E4FC0 File Offset: 0x000E31C0
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000E5005 File Offset: 0x000E3205
		internal static bool IsSingleProcessor
		{
			get
			{
				return PlatformHelper.ProcessorCount == 1;
			}
		}

		// Token: 0x040019AB RID: 6571
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x040019AC RID: 6572
		private static volatile int s_processorCount;

		// Token: 0x040019AD RID: 6573
		private static volatile int s_lastProcessorCountRefreshTicks;
	}
}
