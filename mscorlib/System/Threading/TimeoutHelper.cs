using System;

namespace System.Threading
{
	// Token: 0x0200050D RID: 1293
	internal static class TimeoutHelper
	{
		// Token: 0x06003DA4 RID: 15780 RVA: 0x000E500F File Offset: 0x000E320F
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x000E5018 File Offset: 0x000E3218
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
