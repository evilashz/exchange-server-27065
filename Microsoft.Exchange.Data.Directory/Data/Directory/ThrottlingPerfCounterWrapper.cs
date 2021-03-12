using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009D9 RID: 2521
	internal static class ThrottlingPerfCounterWrapper
	{
		// Token: 0x17002995 RID: 10645
		// (get) Token: 0x0600749B RID: 29851 RVA: 0x001806BB File Offset: 0x0017E8BB
		// (set) Token: 0x0600749C RID: 29852 RVA: 0x001806C2 File Offset: 0x0017E8C2
		internal static ThrottlingPerfCounterWrapper.ForTestLogMassUserOverBudgetDelegate OnLogMassiveNumberOfUsersOverBudgetDelegate { get; set; }

		// Token: 0x0600749D RID: 29853 RVA: 0x001806CC File Offset: 0x0017E8CC
		public static void Initialize(BudgetType budgetType)
		{
			ThrottlingPerfCounterWrapper.Initialize(budgetType, null, false);
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x001806E9 File Offset: 0x0017E8E9
		public static void Initialize(BudgetType budgetType, int? massOverBudgetPercent)
		{
			ThrottlingPerfCounterWrapper.Initialize(budgetType, massOverBudgetPercent, false);
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x00180710 File Offset: 0x0017E910
		public static void Initialize(BudgetType budgetType, int? massOverBudgetPercent, bool allowReinitialize)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized && !allowReinitialize)
			{
				throw new InvalidOperationException(string.Format("ThrottlingPerformanceCounters were already initialized with budget type of '{0}'.", ThrottlingPerfCounterWrapper.budgetType));
			}
			if (massOverBudgetPercent != null && (massOverBudgetPercent.Value < 0 || massOverBudgetPercent.Value > 100))
			{
				throw new ArgumentOutOfRangeException("massOverBudgetPercent", massOverBudgetPercent.Value, "massOverBudgetPercent must be between 0 and 100 inclusive");
			}
			ThrottlingPerfCounterWrapper.budgetType = budgetType;
			ThrottlingPerfCounterWrapper.massiveNumberOfUsersOverBudgetPercent = ((massOverBudgetPercent != null) ? massOverBudgetPercent.Value : DefaultThrottlingAlertValues.MassUserOverBudgetPercent(budgetType));
			try
			{
				string instanceName = ThrottlingPerfCounterWrapper.GetInstanceName(budgetType.ToString());
				ThrottlingPerfCounterWrapper.throttlingPerfCounters = MSExchangeThrottling.GetInstance(instanceName);
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters = MSExchangeUserThrottling.GetInstance(instanceName);
				ThrottlingPerfCounterWrapper.PerfCountersInitialized = true;
			}
			catch (Exception ex)
			{
				ThrottlingPerfCounterWrapper.PerfCountersInitialized = false;
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_InitializePerformanceCountersFailed, string.Empty, new object[]
				{
					ex.ToString()
				});
				ExTraceGlobals.ClientThrottlingTracer.TraceError<string, string>(0L, "[ThrottlingPerfCounterWrapper.Initialize] Perf counter initialization failed with exception type: {0}, Messsage: {1}", ex.GetType().FullName, ex.Message);
			}
			ThrottlingPerfCounterWrapper.budgetsMicroDelayed = new ExactTimeoutCache<BudgetKey, BudgetKey>(delegate(BudgetKey key, BudgetKey value, RemoveReason reason)
			{
				ThrottlingPerfCounterWrapper.UpdateBudgetsMicroDelayed();
			}, null, null, 1000000, false, CacheFullBehavior.ExpireExisting);
			ThrottlingPerfCounterWrapper.budgetsAtMaximumDelay = new ExactTimeoutCache<BudgetKey, BudgetKey>(delegate(BudgetKey key, BudgetKey value, RemoveReason reason)
			{
				ThrottlingPerfCounterWrapper.UpdateBudgetsAtMaxDelay();
			}, null, null, 1000000, false, CacheFullBehavior.ExpireExisting);
			ThrottlingPerfCounterWrapper.budgetsLockedOut = new ExactTimeoutCache<BudgetKey, BudgetKey>(delegate(BudgetKey key, BudgetKey value, RemoveReason reason)
			{
				ThrottlingPerfCounterWrapper.UpdateBudgetsLockedOut();
			}, null, null, 1000000, false, CacheFullBehavior.ExpireExisting);
			ThrottlingPerfCounterWrapper.budgetsOverBudget = new ExactTimeoutCache<BudgetKey, BudgetKey>(delegate(BudgetKey key, BudgetKey value, RemoveReason reason)
			{
				ThrottlingPerfCounterWrapper.UpdateOverBudget();
			}, null, null, 1000000, false, CacheFullBehavior.ExpireExisting);
			ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency = new HashSet<BudgetKey>();
		}

		// Token: 0x17002996 RID: 10646
		// (get) Token: 0x060074A0 RID: 29856 RVA: 0x001808F4 File Offset: 0x0017EAF4
		// (set) Token: 0x060074A1 RID: 29857 RVA: 0x001808FB File Offset: 0x0017EAFB
		internal static int MinUniqueBudgetsForMassOverBudgetAlert
		{
			get
			{
				return ThrottlingPerfCounterWrapper.minUniqueBudgetsForMassiveOverBudgetAlert;
			}
			set
			{
				ThrottlingPerfCounterWrapper.minUniqueBudgetsForMassiveOverBudgetAlert = value;
			}
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x00180904 File Offset: 0x0017EB04
		public static void IncrementBudgetsLockedOut(BudgetKey key, TimeSpan lockoutTime)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				ThrottlingPerfCounterWrapper.budgetsLockedOut.TryInsertAbsolute(key, key, lockoutTime);
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersLockedOut.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsLockedOut.Count;
			}
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x00180970 File Offset: 0x0017EB70
		public static void UpdateBudgetsLockedOut()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersLockedOut.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsLockedOut.Count;
			}
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x001809CC File Offset: 0x0017EBCC
		private static void SetNumberAndPercentCounters(ExactTimeoutCache<BudgetKey, BudgetKey> cache, ExPerformanceCounter numberCounter, ExPerformanceCounter percentCounter)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			int budgetCount = ThrottlingPerfCounterWrapper.GetBudgetCount();
			int count = cache.Count;
			numberCounter.RawValue = (long)count;
			int num = (budgetCount == 0) ? 0 : (100 * count / budgetCount);
			if (num > 100)
			{
				num = 100;
			}
			percentCounter.RawValue = (long)num;
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x00180A14 File Offset: 0x0017EC14
		private static string GetInstanceName(string budgetType)
		{
			string str = null;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				str = currentProcess.ProcessName;
				if (currentProcess.ProcessName.Equals("w3wp", StringComparison.OrdinalIgnoreCase))
				{
					using (ServerManager serverManager = new ServerManager())
					{
						foreach (WorkerProcess workerProcess in serverManager.WorkerProcesses)
						{
							if (workerProcess.ProcessId == currentProcess.Id)
							{
								str = workerProcess.AppPoolName;
								break;
							}
						}
					}
				}
			}
			return str + "_" + budgetType;
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x00180ADC File Offset: 0x0017ECDC
		public static void IncrementBudgetsMicroDelayed(BudgetKey key)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.budgetsMicroDelayed.TryInsertAbsolute(key, key, ThrottlingPerfCounterWrapper.PerfCounterRefreshWindow);
			ThrottlingPerfCounterWrapper.UpdateBudgetsMicroDelayed();
		}

		// Token: 0x060074A7 RID: 29863 RVA: 0x00180AFD File Offset: 0x0017ECFD
		private static void UpdateBudgetsMicroDelayed()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.SetNumberAndPercentCounters(ThrottlingPerfCounterWrapper.budgetsMicroDelayed, ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.NumberOfUsersMicroDelayed, ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.PercentageUsersMicroDelayed);
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x00180B25 File Offset: 0x0017ED25
		public static void IncrementBudgetsAtMaxDelay(BudgetKey key)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.budgetsAtMaximumDelay.TryInsertAbsolute(key, key, ThrottlingPerfCounterWrapper.PerfCounterRefreshWindow);
			ThrottlingPerfCounterWrapper.UpdateBudgetsAtMaxDelay();
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x00180B46 File Offset: 0x0017ED46
		private static void UpdateBudgetsAtMaxDelay()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.SetNumberAndPercentCounters(ThrottlingPerfCounterWrapper.budgetsAtMaximumDelay, ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.NumberOfUsersAtMaximumDelay, ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.PercentageUsersAtMaximumDelay);
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x00180B70 File Offset: 0x0017ED70
		public static void IncrementOverBudget(BudgetKey key, TimeSpan backoffTime)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			if (backoffTime == TimeSpan.Zero || backoffTime == TimeSpan.MaxValue)
			{
				backoffTime = ThrottlingPerfCounterWrapper.PerfCounterRefreshWindow;
			}
			ThrottlingPerfCounterWrapper.budgetsOverBudget.TryInsertAbsolute(key, key, backoffTime);
			ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UniqueBudgetsOverBudget.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsOverBudget.Count;
			ThrottlingPerfCounterWrapper.LogEventsIfNecessary();
		}

		// Token: 0x060074AB RID: 29867 RVA: 0x00180BD3 File Offset: 0x0017EDD3
		private static void UpdateOverBudget()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UniqueBudgetsOverBudget.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsOverBudget.Count;
		}

		// Token: 0x060074AC RID: 29868 RVA: 0x00180BF7 File Offset: 0x0017EDF7
		public static void IncrementBudgetCount()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.TotalUniqueBudgets.Increment();
			ThrottlingPerfCounterWrapper.UpdateBudgetsAtMaxDelay();
			ThrottlingPerfCounterWrapper.UpdateBudgetsLockedOut();
			ThrottlingPerfCounterWrapper.UpdateBudgetsMicroDelayed();
			ThrottlingPerfCounterWrapper.UpdateOverBudget();
		}

		// Token: 0x060074AD RID: 29869 RVA: 0x00180C25 File Offset: 0x0017EE25
		public static void DecrementBudgetCount()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.TotalUniqueBudgets.Decrement();
			ThrottlingPerfCounterWrapper.UpdateBudgetsAtMaxDelay();
			ThrottlingPerfCounterWrapper.UpdateBudgetsLockedOut();
			ThrottlingPerfCounterWrapper.UpdateBudgetsMicroDelayed();
			ThrottlingPerfCounterWrapper.UpdateOverBudget();
		}

		// Token: 0x060074AE RID: 29870 RVA: 0x00180C54 File Offset: 0x0017EE54
		public static void IncrementBudgetsAtMaxConcurrency(BudgetKey key)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency.Add(key);
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersAtMaxConcurrency.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency.Count;
			}
		}

		// Token: 0x060074AF RID: 29871 RVA: 0x00180CBC File Offset: 0x0017EEBC
		public static void DecrementBudgetsAtMaxConcurrency(BudgetKey key)
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency.Remove(key);
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersAtMaxConcurrency.RawValue = (long)ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency.Count;
			}
		}

		// Token: 0x060074B0 RID: 29872 RVA: 0x00180D24 File Offset: 0x0017EF24
		public static void ClearCaches()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return;
			}
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.TotalUniqueBudgets.RawValue = 0L;
				ThrottlingPerfCounterWrapper.budgetsMicroDelayed.Clear();
				ThrottlingPerfCounterWrapper.budgetsAtMaximumDelay.Clear();
				ThrottlingPerfCounterWrapper.budgetsLockedOut.Clear();
				ThrottlingPerfCounterWrapper.budgetsOverBudget.Clear();
				ThrottlingPerfCounterWrapper.budgetsAtMaxConcurrency.Clear();
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.PercentageUsersMicroDelayed.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.PercentageUsersAtMaximumDelay.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.NumberOfUsersMicroDelayed.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.NumberOfUsersAtMaximumDelay.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersLockedOut.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UniqueBudgetsOverBudget.RawValue = 0L;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.UsersAtMaxConcurrency.RawValue = 0L;
			}
		}

		// Token: 0x060074B1 RID: 29873 RVA: 0x00180E20 File Offset: 0x0017F020
		private static void LogEventsIfNecessary()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			lock (ThrottlingPerfCounterWrapper.staticLock)
			{
				int budgetCount = ThrottlingPerfCounterWrapper.GetBudgetCount();
				num3 = ((budgetCount > 0) ? (100 * ThrottlingPerfCounterWrapper.budgetsOverBudget.Count / budgetCount) : 0);
			}
			if (num3 > ThrottlingPerfCounterWrapper.massiveNumberOfUsersOverBudgetPercent && ThrottlingPerfCounterWrapper.budgetsOverBudget.Count > ThrottlingPerfCounterWrapper.minUniqueBudgetsForMassiveOverBudgetAlert)
			{
				bool flag2 = false;
				lock (ThrottlingPerfCounterWrapper.staticLock)
				{
					if (ThrottlingPerfCounterWrapper.budgetsOverBudget.Count > ThrottlingPerfCounterWrapper.minUniqueBudgetsForMassiveOverBudgetAlert)
					{
						flag2 = true;
						num = ThrottlingPerfCounterWrapper.GetBudgetCount();
						num2 = ThrottlingPerfCounterWrapper.budgetsOverBudget.Count;
					}
				}
				if (flag2)
				{
					if (ThrottlingPerfCounterWrapper.OnLogMassiveNumberOfUsersOverBudgetDelegate != null)
					{
						ThrottlingPerfCounterWrapper.OnLogMassiveNumberOfUsersOverBudgetDelegate(num, num3);
					}
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_ExcessiveMassUserThrottling, ThrottlingPerfCounterWrapper.budgetType.ToString(), new object[]
					{
						num2,
						ThrottlingPerfCounterWrapper.budgetType,
						num,
						num3
					});
				}
			}
		}

		// Token: 0x17002997 RID: 10647
		// (get) Token: 0x060074B2 RID: 29874 RVA: 0x00180F5C File Offset: 0x0017F15C
		// (set) Token: 0x060074B3 RID: 29875 RVA: 0x00180F63 File Offset: 0x0017F163
		public static bool PerfCountersInitialized { get; private set; }

		// Token: 0x060074B4 RID: 29876 RVA: 0x00180F6B File Offset: 0x0017F16B
		private static int GetBudgetCount()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return 0;
			}
			return (int)ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.TotalUniqueBudgets.RawValue;
		}

		// Token: 0x060074B5 RID: 29877 RVA: 0x00180F86 File Offset: 0x0017F186
		public static void SetDelayedThreads(long delayedThreads)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.DelayedThreads.RawValue = delayedThreads;
			}
		}

		// Token: 0x060074B6 RID: 29878 RVA: 0x00180F9F File Offset: 0x0017F19F
		public static void IncrementActivePowerShellRunspaces()
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.throttlingPerfCounters.ActivePowerShellRunspaces.Increment();
			}
		}

		// Token: 0x060074B7 RID: 29879 RVA: 0x00180FB8 File Offset: 0x0017F1B8
		public static void DecrementActivePowerShellRunspaces()
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.DecrementPerfCounter(ThrottlingPerfCounterWrapper.throttlingPerfCounters.ActivePowerShellRunspaces);
			}
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x00180FD0 File Offset: 0x0017F1D0
		public static void IncrementExchangeExecutingCmdlets()
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.throttlingPerfCounters.ExchangeExecutingCmdlets.Increment();
			}
		}

		// Token: 0x060074B9 RID: 29881 RVA: 0x00180FE9 File Offset: 0x0017F1E9
		public static void DecrementExchangeExecutingCmdlets()
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.DecrementPerfCounter(ThrottlingPerfCounterWrapper.throttlingPerfCounters.ExchangeExecutingCmdlets);
			}
		}

		// Token: 0x060074BA RID: 29882 RVA: 0x00181001 File Offset: 0x0017F201
		private static void DecrementPerfCounter(ExPerformanceCounter counter)
		{
			if (counter.RawValue > 0L)
			{
				counter.Decrement();
			}
		}

		// Token: 0x060074BB RID: 29883 RVA: 0x00181014 File Offset: 0x0017F214
		public static void UpdateAverageThreadSleepTime(long newValue)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.throttlingPerfCounters.AverageThreadSleepTime.RawValue = (long)ThrottlingPerfCounterWrapper.averageThreadSleepTime.Update((float)newValue);
			}
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x00181039 File Offset: 0x0017F239
		public static MSExchangeThrottlingInstance GetThrottlingCounterForTest()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return null;
			}
			return ThrottlingPerfCounterWrapper.throttlingPerfCounters;
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x00181049 File Offset: 0x0017F249
		public static MSExchangeUserThrottlingInstance GetUserThrottlingCounterForTest()
		{
			if (!ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return null;
			}
			return ThrottlingPerfCounterWrapper.userThrottlingPerfCounters;
		}

		// Token: 0x060074BE RID: 29886 RVA: 0x0018105C File Offset: 0x0017F25C
		public static void SetFiveMinuteBudgetUsage(int usage999, int usage99, int usage75, int averageUsage)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageFiveMinuteWindow_99_9.RawValue = (long)usage999;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageFiveMinuteWindow_99.RawValue = (long)usage99;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageFiveMinuteWindow_75.RawValue = (long)usage75;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.AverageBudgetUsageFiveMinuteWindow.RawValue = (long)averageUsage;
			}
		}

		// Token: 0x060074BF RID: 29887 RVA: 0x001810B4 File Offset: 0x0017F2B4
		public static void SetOneHourBudgetUsage(int usage999, int usage99, int usage75, int averageUsage)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageOneHourWindow_99_9.RawValue = (long)usage999;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageOneHourWindow_99.RawValue = (long)usage99;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.BudgetUsageOneHourWindow_75.RawValue = (long)usage75;
				ThrottlingPerfCounterWrapper.userThrottlingPerfCounters.AverageBudgetUsageOneHourWindow.RawValue = (long)averageUsage;
			}
		}

		// Token: 0x060074C0 RID: 29888 RVA: 0x0018110C File Offset: 0x0017F30C
		public static ICachePerformanceCounters GetOrganizationThrottlingPolicyCacheCounters(long maxCacheSize)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return new CachePerformanceCounters(ThrottlingPerfCounterWrapper.throttlingPerfCounters.OrganizationThrottlingPolicyCacheHitCount, ThrottlingPerfCounterWrapper.throttlingPerfCounters.OrganizationThrottlingPolicyCacheMissCount, ThrottlingPerfCounterWrapper.throttlingPerfCounters.OrganizationThrottlingPolicyCacheLength, ThrottlingPerfCounterWrapper.throttlingPerfCounters.OrganizationThrottlingPolicyCacheLengthPercentage, maxCacheSize);
			}
			return null;
		}

		// Token: 0x060074C1 RID: 29889 RVA: 0x00181145 File Offset: 0x0017F345
		public static ICachePerformanceCounters GetThrottlingPolicyCacheCounters(long maxCacheSize)
		{
			if (ThrottlingPerfCounterWrapper.PerfCountersInitialized)
			{
				return new CachePerformanceCounters(ThrottlingPerfCounterWrapper.throttlingPerfCounters.ThrottlingPolicyCacheHitCount, ThrottlingPerfCounterWrapper.throttlingPerfCounters.ThrottlingPolicyCacheMissCount, ThrottlingPerfCounterWrapper.throttlingPerfCounters.ThrottlingPolicyCacheLength, ThrottlingPerfCounterWrapper.throttlingPerfCounters.ThrottlingPolicyCacheLengthPercentage, maxCacheSize);
			}
			return null;
		}

		// Token: 0x04004B27 RID: 19239
		private static MSExchangeThrottlingInstance throttlingPerfCounters;

		// Token: 0x04004B28 RID: 19240
		private static MSExchangeUserThrottlingInstance userThrottlingPerfCounters;

		// Token: 0x04004B29 RID: 19241
		private static BudgetType budgetType;

		// Token: 0x04004B2A RID: 19242
		private static RunningAverageFloat averageThreadSleepTime = new RunningAverageFloat(10);

		// Token: 0x04004B2B RID: 19243
		private static RunningAverageFloat averageTaskWaitTime = new RunningAverageFloat(10);

		// Token: 0x04004B2C RID: 19244
		private static ExactTimeoutCache<BudgetKey, BudgetKey> budgetsOverBudget;

		// Token: 0x04004B2D RID: 19245
		private static ExactTimeoutCache<BudgetKey, BudgetKey> budgetsMicroDelayed;

		// Token: 0x04004B2E RID: 19246
		private static ExactTimeoutCache<BudgetKey, BudgetKey> budgetsLockedOut;

		// Token: 0x04004B2F RID: 19247
		private static ExactTimeoutCache<BudgetKey, BudgetKey> budgetsAtMaximumDelay;

		// Token: 0x04004B30 RID: 19248
		private static HashSet<BudgetKey> budgetsAtMaxConcurrency;

		// Token: 0x04004B31 RID: 19249
		private static object staticLock = new object();

		// Token: 0x04004B32 RID: 19250
		private static int massiveNumberOfUsersOverBudgetPercent;

		// Token: 0x04004B33 RID: 19251
		public static readonly TimeSpan PerfCounterRefreshWindow = TimeSpan.FromMinutes(1.0);

		// Token: 0x04004B34 RID: 19252
		private static int minUniqueBudgetsForMassiveOverBudgetAlert = 100;

		// Token: 0x020009DA RID: 2522
		// (Invoke) Token: 0x060074C8 RID: 29896
		internal delegate void ForTestLogMassUserOverBudgetDelegate(int uniqueBudgetCount, int overBudgetPercent);
	}
}
