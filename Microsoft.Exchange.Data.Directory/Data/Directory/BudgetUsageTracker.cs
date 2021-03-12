using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009AC RID: 2476
	internal static class BudgetUsageTracker
	{
		// Token: 0x06007239 RID: 29241 RVA: 0x0017A3B0 File Offset: 0x001785B0
		public static PercentileUsage Get(BudgetKey key)
		{
			PercentileUsage percentileUsage = null;
			if (!BudgetUsageTracker.budgetUsage.TryGetValue(key, out percentileUsage) || percentileUsage == null)
			{
				lock (BudgetUsageTracker.staticLock)
				{
					if (!BudgetUsageTracker.budgetUsage.TryGetValue(key, out percentileUsage) || percentileUsage == null)
					{
						percentileUsage = new PercentileUsage();
						BudgetUsageTracker.budgetUsage[key] = percentileUsage;
					}
				}
			}
			return percentileUsage;
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x0017A424 File Offset: 0x00178624
		public static void ClearForTest()
		{
			lock (BudgetUsageTracker.staticLock)
			{
				BudgetUsageTracker.budgetUsage.Clear();
			}
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x0017A468 File Offset: 0x00178668
		private static void HandleTimer(object state)
		{
			bool isOneHour = false;
			BudgetUsageTracker.budgetUsageClearIndex++;
			if (BudgetUsageTracker.budgetUsageClearIndex % 12 == 0)
			{
				BudgetUsageTracker.budgetUsageClearIndex = 0;
				isOneHour = true;
			}
			BudgetUsageTracker.Update(isOneHour);
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x0017A49C File Offset: 0x0017869C
		public static void Update(bool isOneHour)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			int[] array = null;
			int averageUsage = 0;
			int[] array2 = null;
			int averageUsage2 = 0;
			PercentileUsage[] array3 = null;
			if (BudgetUsageTracker.budgetUsage.Count > 0)
			{
				List<BudgetKey> list = null;
				lock (BudgetUsageTracker.staticLock)
				{
					if (BudgetUsageTracker.budgetUsage.Count > 0)
					{
						array3 = new PercentileUsage[BudgetUsageTracker.budgetUsage.Count];
						int num = 0;
						foreach (KeyValuePair<BudgetKey, PercentileUsage> keyValuePair in BudgetUsageTracker.budgetUsage)
						{
							array3[num++] = new PercentileUsage(keyValuePair.Value);
							if (keyValuePair.Value.FiveMinuteUsage == 0 && keyValuePair.Value.OneHourUsage == 0 && TimeProvider.UtcNow - keyValuePair.Value.CreationTime > BudgetUsageTracker.PeriodicLoggingInterval)
							{
								if (list == null)
								{
									list = new List<BudgetKey>();
								}
								list.Add(keyValuePair.Key);
							}
							keyValuePair.Value.Clear(isOneHour);
						}
						if (list != null)
						{
							foreach (BudgetKey key in list)
							{
								PercentileUsage percentileUsage;
								if (BudgetUsageTracker.budgetUsage.TryGetValue(key, out percentileUsage))
								{
									percentileUsage.Expired = true;
									BudgetUsageTracker.budgetUsage.Remove(key);
								}
							}
						}
					}
				}
			}
			if (array3 != null)
			{
				BudgetUsageTracker.GetPercentileUsage(array3, false, out array, out averageUsage);
				ThrottlingPerfCounterWrapper.SetFiveMinuteBudgetUsage(array[0], array[1], array[2], averageUsage);
				if (isOneHour)
				{
					BudgetUsageTracker.GetPercentileUsage(array3, true, out array2, out averageUsage2);
					ThrottlingPerfCounterWrapper.SetOneHourBudgetUsage(array2[0], array2[1], array2[2], averageUsage2);
					return;
				}
			}
			else
			{
				if (isOneHour)
				{
					ThrottlingPerfCounterWrapper.SetOneHourBudgetUsage(0, 0, 0, 0);
					return;
				}
				ThrottlingPerfCounterWrapper.SetFiveMinuteBudgetUsage(0, 0, 0, 0);
			}
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x0017A6BC File Offset: 0x001788BC
		private static void GetPercentileUsage(PercentileUsage[] usages, bool isOneHour, out int[] percentiles, out int averageUsage)
		{
			Array.Sort<PercentileUsage>(usages, isOneHour ? new Comparison<PercentileUsage>(PercentileUsage.OneHourComparer) : new Comparison<PercentileUsage>(PercentileUsage.FiveMinuteComparer));
			percentiles = new int[3];
			percentiles[0] = BudgetUsageTracker.GetUsageAtPercentage(isOneHour, 0.999f, usages);
			percentiles[1] = BudgetUsageTracker.GetUsageAtPercentage(isOneHour, 0.99f, usages);
			percentiles[2] = BudgetUsageTracker.GetUsageAtPercentage(isOneHour, 0.75f, usages);
			double num = 0.0;
			double num2 = 1.0 / (double)usages.Length;
			foreach (PercentileUsage percentileUsage in usages)
			{
				num += num2 * (double)(isOneHour ? percentileUsage.OneHourUsage : percentileUsage.FiveMinuteUsage);
			}
			averageUsage = (int)num;
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x0017A774 File Offset: 0x00178974
		private static int GetUsageAtPercentage(bool isOneHour, float factor, PercentileUsage[] usages)
		{
			int num = (int)Math.Round((double)(factor * (float)(usages.Length - 1)));
			if (!isOneHour)
			{
				return usages[num].FiveMinuteUsage;
			}
			return usages[num].OneHourUsage;
		}

		// Token: 0x04004A09 RID: 18953
		private const float Factor99_9 = 0.999f;

		// Token: 0x04004A0A RID: 18954
		private const float Factor99 = 0.99f;

		// Token: 0x04004A0B RID: 18955
		private const float Factor75 = 0.75f;

		// Token: 0x04004A0C RID: 18956
		private static readonly TimeSpan PeriodicLoggingInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04004A0D RID: 18957
		private static Dictionary<BudgetKey, PercentileUsage> budgetUsage = new Dictionary<BudgetKey, PercentileUsage>();

		// Token: 0x04004A0E RID: 18958
		private static int budgetUsageClearIndex;

		// Token: 0x04004A0F RID: 18959
		private static object staticLock = new object();

		// Token: 0x04004A10 RID: 18960
		private static Timer timer = new Timer(new TimerCallback(BudgetUsageTracker.HandleTimer), null, BudgetUsageTracker.PeriodicLoggingInterval, BudgetUsageTracker.PeriodicLoggingInterval);
	}
}
