using System;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000013 RID: 19
	internal class SearchMemoryOperation
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005B30 File Offset: 0x00003D30
		internal SearchMemoryOperation(SearchMemoryOperation searchMemoryOperation)
		{
			this.maxRestoreAmount = searchMemoryOperation.MaxRestoreAmount;
			this.memoryMeasureDrift = searchMemoryOperation.MemoryMeasureDrift;
			this.memoryUsageHighLine = searchMemoryOperation.MemoryUsageHighLine;
			this.memoryUsageLowLine = searchMemoryOperation.MemoryUsageLowLine;
			this.searchMemoryUsageBudgetHighLine = searchMemoryOperation.SearchMemoryUsageBudgetHighLine;
			this.searchMemoryUsageBudgetLowLine = searchMemoryOperation.SearchMemoryUsageBudgetLowLine;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005B8C File Offset: 0x00003D8C
		internal SearchMemoryOperation(ISearchServiceConfig config, long totalPhys)
		{
			this.maxRestoreAmount = config.MaxRestoreAmount;
			this.memoryMeasureDrift = config.MemoryMeasureDrift;
			this.memoryUsageHighLine = totalPhys - totalPhys * (long)config.LowAvailableSystemWorkingSetMemoryRatio / 100L;
			this.memoryUsageLowLine = Math.Max(this.memoryUsageHighLine - config.MemoryMeasureDrift * 2L - this.maxRestoreAmount, 0L);
			this.searchMemoryUsageBudgetHighLine = config.SearchMemoryUsageBudget;
			this.searchMemoryUsageBudgetLowLine = config.SearchMemoryUsageBudget - config.SearchMemoryUsageBudgetFloatingAmount;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005C0F File Offset: 0x00003E0F
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005C17 File Offset: 0x00003E17
		internal long MemoryUsageHighLine
		{
			get
			{
				return this.memoryUsageHighLine;
			}
			set
			{
				this.memoryUsageHighLine = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005C20 File Offset: 0x00003E20
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00005C28 File Offset: 0x00003E28
		internal long MemoryUsageLowLine
		{
			get
			{
				return this.memoryUsageLowLine;
			}
			set
			{
				this.memoryUsageLowLine = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005C31 File Offset: 0x00003E31
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00005C39 File Offset: 0x00003E39
		internal long SearchMemoryUsageBudgetHighLine
		{
			get
			{
				return this.searchMemoryUsageBudgetHighLine;
			}
			set
			{
				this.searchMemoryUsageBudgetHighLine = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00005C42 File Offset: 0x00003E42
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00005C4A File Offset: 0x00003E4A
		internal long SearchMemoryUsageBudgetLowLine
		{
			get
			{
				return this.searchMemoryUsageBudgetLowLine;
			}
			set
			{
				this.searchMemoryUsageBudgetLowLine = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005C53 File Offset: 0x00003E53
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005C5B File Offset: 0x00003E5B
		internal long MaxRestoreAmount
		{
			get
			{
				return this.maxRestoreAmount;
			}
			set
			{
				this.maxRestoreAmount = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005C64 File Offset: 0x00003E64
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005C6C File Offset: 0x00003E6C
		internal long MemoryMeasureDrift
		{
			get
			{
				return this.memoryMeasureDrift;
			}
			set
			{
				this.memoryMeasureDrift = value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005C75 File Offset: 0x00003E75
		internal bool IsTotalMemoryUsageHigh(long usedPhys)
		{
			return usedPhys > this.memoryUsageHighLine - this.memoryMeasureDrift;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005C87 File Offset: 0x00003E87
		internal bool IsTotalMemoryUsageLow(long usedPhys)
		{
			return usedPhys < this.memoryUsageLowLine + this.memoryMeasureDrift;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00005C99 File Offset: 0x00003E99
		internal bool IsSearchMemoryUsageHigh(long searchUsage)
		{
			return searchUsage > this.searchMemoryUsageBudgetHighLine;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00005CA4 File Offset: 0x00003EA4
		internal bool IsSearchMemoryUsageLow(long searchUsage)
		{
			return searchUsage < this.searchMemoryUsageBudgetLowLine;
		}

		// Token: 0x04000055 RID: 85
		private long searchMemoryUsageBudgetHighLine;

		// Token: 0x04000056 RID: 86
		private long searchMemoryUsageBudgetLowLine;

		// Token: 0x04000057 RID: 87
		private long memoryUsageHighLine;

		// Token: 0x04000058 RID: 88
		private long memoryUsageLowLine;

		// Token: 0x04000059 RID: 89
		private long maxRestoreAmount;

		// Token: 0x0400005A RID: 90
		private long memoryMeasureDrift;
	}
}
