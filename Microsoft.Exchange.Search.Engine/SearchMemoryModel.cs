using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000011 RID: 17
	internal class SearchMemoryModel
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00005594 File Offset: 0x00003794
		internal SearchMemoryModel(SearchMemoryModel searchMemoryModel)
		{
			this.config = searchMemoryModel.Config;
			this.diagnosticsSession = searchMemoryModel.DiagnosticsSession;
			this.totalPhys = searchMemoryModel.TotalPhys;
			this.availPhys = searchMemoryModel.AvailPhys;
			this.searchDesiredFreeMemory = searchMemoryModel.SearchDesiredFreeMemory;
			this.searchMemoryUsage = searchMemoryModel.SearchMemoryUsage;
			this.searchMemoryUsageDrift = searchMemoryModel.SearchMemoryUsageDrift;
			this.searchMemoryOperation = new SearchMemoryOperation(searchMemoryModel.searchMemoryOperation);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000560C File Offset: 0x0000380C
		internal SearchMemoryModel(ISearchServiceConfig config, IDiagnosticsSession diagnosticsSession)
		{
			this.config = config;
			this.diagnosticsSession = diagnosticsSession;
			NativeMethods.MemoryStatusEx memoryStatusEx;
			if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
			{
				this.totalPhys = (long)memoryStatusEx.TotalPhys;
				this.availPhys = (long)memoryStatusEx.AvailPhys;
				this.searchDesiredFreeMemory = this.config.SearchWorkingSetMemoryUsageThreshold;
				this.searchMemoryOperation = new SearchMemoryOperation(config, this.totalPhys);
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Failed to get the total physical memory with error {0}.", new object[]
			{
				lastWin32Error
			});
			throw new Win32Exception(lastWin32Error);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000056A4 File Offset: 0x000038A4
		internal static float MemoryUsageAdjustmentMultiplier
		{
			get
			{
				return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Search.MemoryModel.MemoryUsageAdjustmentMultiplier;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000056CF File Offset: 0x000038CF
		internal IDiagnosticsSession DiagnosticsSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000056D7 File Offset: 0x000038D7
		internal ISearchServiceConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000056DF File Offset: 0x000038DF
		internal long TotalPhys
		{
			get
			{
				return this.totalPhys;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000056E7 File Offset: 0x000038E7
		internal long AvailPhys
		{
			get
			{
				return this.availPhys;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000056EF File Offset: 0x000038EF
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000056F7 File Offset: 0x000038F7
		internal long SearchMemoryUsage
		{
			get
			{
				return this.searchMemoryUsage;
			}
			set
			{
				this.searchMemoryUsage = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00005700 File Offset: 0x00003900
		internal long SearchDesiredFreeMemory
		{
			get
			{
				return this.searchDesiredFreeMemory;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005708 File Offset: 0x00003908
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005710 File Offset: 0x00003910
		internal long SearchMemoryUsageDrift
		{
			get
			{
				return this.searchMemoryUsageDrift;
			}
			set
			{
				this.searchMemoryUsageDrift = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005719 File Offset: 0x00003919
		internal SearchMemoryOperation SearchMemoryOperation
		{
			get
			{
				return this.searchMemoryOperation;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005724 File Offset: 0x00003924
		internal static long GetSearchMemoryUsage()
		{
			if (DateTime.UtcNow > SearchMemoryModel.recentSearchMemoryUsageCacheTimeoutTime)
			{
				lock (SearchMemoryModel.recentSearchMemoryUsageCacheLock)
				{
					if (DateTime.UtcNow > SearchMemoryModel.recentSearchMemoryUsageCacheTimeoutTime)
					{
						SearchMemoryModel.recentSearchMemoryUsageCached = SearchMemoryModel.GetFreshSearchMemoryUsage();
						SearchMemoryModel.recentSearchMemoryUsageCacheTimeoutTime = DateTime.UtcNow.Add(SearchConfig.Instance.RecentSearchMemoryUsageCachedTimeout);
					}
				}
			}
			return SearchMemoryModel.recentSearchMemoryUsageCached;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000057AC File Offset: 0x000039AC
		internal static long GetFreshSearchMemoryUsage()
		{
			long num = 0L;
			foreach (string processName in SearchMemoryModel.SearchRelatedProcesses)
			{
				Process[] processesByName = Process.GetProcessesByName(processName);
				if (processesByName != null && processesByName.Length > 0)
				{
					foreach (Process process in processesByName)
					{
						num += process.WorkingSet64;
					}
				}
			}
			return num;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005814 File Offset: 0x00003A14
		internal long GetExpectedSearchMemoryUsage(long activeItems, long passiveItems, long activeItemsInstantSearchOn, long activeItemsRefinersOn)
		{
			return (long)((float)(this.config.SearchMemoryModelBaseCost + this.config.BaselineCostPerActiveItem * activeItems + this.config.BaselineCostPerPassiveItem * passiveItems + this.config.InstantSearchCostPerActiveItem * activeItemsInstantSearchOn + this.config.RefinersCostPerActiveItem * activeItemsRefinersOn) * SearchMemoryModel.MemoryUsageAdjustmentMultiplier);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000586D File Offset: 0x00003A6D
		internal bool IsUnderSearchBudget()
		{
			return this.searchMemoryUsage <= this.searchDesiredFreeMemory;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005880 File Offset: 0x00003A80
		internal bool IsBetter(long memoryChange)
		{
			long num = this.totalPhys - this.availPhys;
			this.diagnosticsSession.TraceDebug("availPhys: {0}, availableMemoryHighLine: {1}, availableMemoryLowLine: {2}, adjustedUsedPhys: {3}", new object[]
			{
				this.availPhys,
				this.searchMemoryOperation.MemoryUsageHighLine,
				this.searchMemoryOperation.MemoryUsageLowLine,
				num
			});
			if (this.searchMemoryOperation.IsTotalMemoryUsageHigh(num) && (!this.config.ShouldConsiderSearchMemoryUsageBudget || this.searchMemoryOperation.IsSearchMemoryUsageHigh(this.searchMemoryUsage)))
			{
				if (memoryChange < 0L && this.searchMemoryOperation.IsTotalMemoryUsageLow(num + memoryChange))
				{
					this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, "Memory change causes crossing the low line. memory change:{0}, memory change result:{1}, low line:{2}", new object[]
					{
						memoryChange,
						num + memoryChange,
						this.searchMemoryOperation.MemoryUsageLowLine + this.searchMemoryOperation.MemoryMeasureDrift
					});
				}
				return memoryChange < 0L;
			}
			return (this.searchMemoryOperation.IsTotalMemoryUsageLow(num) || (this.config.ShouldConsiderSearchMemoryUsageBudget && this.searchMemoryOperation.IsSearchMemoryUsageLow(this.searchMemoryUsage))) && memoryChange > 0L && !this.searchMemoryOperation.IsTotalMemoryUsageHigh(num + memoryChange);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000059D8 File Offset: 0x00003BD8
		internal void ApplyMemoryChange(long memoryChange, string mdbInfo)
		{
			this.diagnosticsSession.TraceDebug("Available memory- before: {0}, after: {1}; Search memory usage - before: {2}, after: {3}. Affected database - {4}.", new object[]
			{
				this.availPhys,
				this.availPhys - memoryChange,
				this.searchMemoryUsage,
				this.searchMemoryUsage + memoryChange,
				mdbInfo
			});
			this.availPhys -= memoryChange;
			this.searchMemoryUsage += memoryChange;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005A5C File Offset: 0x00003C5C
		internal SearchMemoryModel.ActionDirection GetActionDirection()
		{
			long usedPhys = this.totalPhys - this.availPhys;
			if (this.searchMemoryOperation.IsTotalMemoryUsageHigh(usedPhys) && (!this.config.ShouldConsiderSearchMemoryUsageBudget || this.searchMemoryOperation.IsSearchMemoryUsageHigh(this.searchMemoryUsage)))
			{
				return SearchMemoryModel.ActionDirection.Degrade;
			}
			if (this.searchMemoryOperation.IsTotalMemoryUsageLow(usedPhys) || (this.config.ShouldConsiderSearchMemoryUsageBudget && this.searchMemoryOperation.IsSearchMemoryUsageLow(this.searchMemoryUsage)))
			{
				return SearchMemoryModel.ActionDirection.Restore;
			}
			return SearchMemoryModel.ActionDirection.None;
		}

		// Token: 0x04000045 RID: 69
		private static readonly object recentSearchMemoryUsageCacheLock = new object();

		// Token: 0x04000046 RID: 70
		private static readonly string[] SearchRelatedProcesses = new string[]
		{
			"noderunner",
			"hostcontrollerservice",
			"microsoft.exchange.search.service",
			"parserserver"
		};

		// Token: 0x04000047 RID: 71
		private static long recentSearchMemoryUsageCached;

		// Token: 0x04000048 RID: 72
		private static DateTime recentSearchMemoryUsageCacheTimeoutTime = DateTime.MinValue;

		// Token: 0x04000049 RID: 73
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400004A RID: 74
		private readonly ISearchServiceConfig config;

		// Token: 0x0400004B RID: 75
		private readonly long totalPhys;

		// Token: 0x0400004C RID: 76
		private readonly long searchDesiredFreeMemory;

		// Token: 0x0400004D RID: 77
		private long availPhys;

		// Token: 0x0400004E RID: 78
		private long searchMemoryUsage;

		// Token: 0x0400004F RID: 79
		private long searchMemoryUsageDrift;

		// Token: 0x04000050 RID: 80
		private SearchMemoryOperation searchMemoryOperation;

		// Token: 0x02000012 RID: 18
		internal enum ActionDirection
		{
			// Token: 0x04000052 RID: 82
			None,
			// Token: 0x04000053 RID: 83
			Degrade,
			// Token: 0x04000054 RID: 84
			Restore
		}
	}
}
