using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C99 RID: 3225
	internal static class TickDiffer
	{
		// Token: 0x0600470B RID: 18187 RVA: 0x000BF344 File Offset: 0x000BD544
		public static TimeSpan Elapsed(int oldTicks)
		{
			return TickDiffer.Elapsed(oldTicks, Environment.TickCount);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x000BF354 File Offset: 0x000BD554
		public static TimeSpan Elapsed(int startTicks, int endTicks)
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds(-1 - startTicks + endTicks + 1);
			if (timeSpan >= TickDiffer.NegativeCutoff)
			{
				timeSpan = TimeSpan.FromMilliseconds(-1 - endTicks + startTicks + 1).Negate();
			}
			return timeSpan;
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x000BF395 File Offset: 0x000BD595
		public static int Add(int startTicks, int amountToAdd)
		{
			return startTicks + amountToAdd;
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x000BF39A File Offset: 0x000BD59A
		public static int Subtract(int startTicks, int amountToSubtract)
		{
			return startTicks - amountToSubtract;
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x000BF3A0 File Offset: 0x000BD5A0
		public static ulong GetTickDifference(int tickStart, int tickEnd)
		{
			long num = (long)tickStart;
			long num2 = (long)tickEnd;
			if (num2 >= num)
			{
				return (ulong)(num2 - num);
			}
			return (ulong)(2147483647L - num + (num2 - -2147483648L));
		}

		// Token: 0x04003C2E RID: 15406
		private static readonly TimeSpan NegativeCutoff = TimeSpan.FromDays(25.0);
	}
}
