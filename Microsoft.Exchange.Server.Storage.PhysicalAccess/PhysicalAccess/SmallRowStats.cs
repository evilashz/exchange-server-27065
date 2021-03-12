using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000073 RID: 115
	public struct SmallRowStats
	{
		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x00018790 File Offset: 0x00016990
		public bool IsEmpty
		{
			get
			{
				foreach (int num in this.counters)
				{
					if (num != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x000187C0 File Offset: 0x000169C0
		private static SmallRowStatsTableClassIndex[] CreateTableClassIndex()
		{
			SmallRowStatsTableClassIndex[] array = new SmallRowStatsTableClassIndex[66];
			array[1] = SmallRowStatsTableClassIndex.Temp;
			array[2] = SmallRowStatsTableClassIndex.Index;
			array[3] = SmallRowStatsTableClassIndex.Index;
			return array;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000187E2 File Offset: 0x000169E2
		private static SmallRowStatsTableClassIndex GetTableClassIndex(TableClass tableClass)
		{
			return SmallRowStats.tableClassIndex[(int)tableClass];
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000187EB File Offset: 0x000169EB
		private static int GetCounterIndex(SmallRowStatsTableClassIndex tableClassIndex, RowStatsCounterType counterType)
		{
			return (int)(tableClassIndex * (SmallRowStatsTableClassIndex)6 + (int)counterType);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000187F2 File Offset: 0x000169F2
		private static int GetCounterIndex(TableClass tableClass, RowStatsCounterType counterType)
		{
			return SmallRowStats.GetCounterIndex(SmallRowStats.GetTableClassIndex(tableClass), counterType);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00018800 File Offset: 0x00016A00
		public void Initialize()
		{
			this.counters = new int[18];
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001880F File Offset: 0x00016A0F
		public void IncrementCount(TableClass tableClass, RowStatsCounterType counterType)
		{
			this.counters[SmallRowStats.GetCounterIndex(tableClass, counterType)]++;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00018830 File Offset: 0x00016A30
		public void AddCount(TableClass tableClass, RowStatsCounterType counterType, int value)
		{
			this.counters[SmallRowStats.GetCounterIndex(tableClass, counterType)] += value;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00018851 File Offset: 0x00016A51
		private int GetCounter(SmallRowStatsTableClassIndex tableClassIndex, RowStatsCounterType counterType)
		{
			return this.counters[SmallRowStats.GetCounterIndex(tableClassIndex, counterType)];
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00018864 File Offset: 0x00016A64
		private bool IsTableClassEmpty(SmallRowStatsTableClassIndex tableClassIndex)
		{
			return this.GetCounter(tableClassIndex, RowStatsCounterType.Read) == 0 && this.GetCounter(tableClassIndex, RowStatsCounterType.Seek) == 0 && this.GetCounter(tableClassIndex, RowStatsCounterType.Accept) == 0 && this.GetCounter(tableClassIndex, RowStatsCounterType.Write) == 0 && this.GetCounter(tableClassIndex, RowStatsCounterType.ReadBytes) == 0 && 0 == this.GetCounter(tableClassIndex, RowStatsCounterType.WriteBytes);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x000188B0 File Offset: 0x00016AB0
		public void Aggregate(SmallRowStats other)
		{
			for (int i = 0; i < this.counters.Length; i++)
			{
				this.counters[i] += other.counters[i];
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000188F4 File Offset: 0x00016AF4
		public override string ToString()
		{
			TraceContentBuilder traceContentBuilder = TraceContentBuilder.Create();
			this.AppendToString(traceContentBuilder);
			return traceContentBuilder.ToString();
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00018918 File Offset: 0x00016B18
		public bool AppendToString(TraceContentBuilder cb)
		{
			bool flag = false;
			if (this.counters != null)
			{
				flag |= this.AppendCountersToString(cb, "IDX", SmallRowStatsTableClassIndex.Index, flag);
				flag |= this.AppendCountersToString(cb, "BASE", SmallRowStatsTableClassIndex.Base, flag);
				flag |= this.AppendCountersToString(cb, "TMP", SmallRowStatsTableClassIndex.Temp, flag);
			}
			return flag;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00018964 File Offset: 0x00016B64
		private bool AppendCountersToString(TraceContentBuilder cb, string name, SmallRowStatsTableClassIndex tableClassIndex, bool continueList)
		{
			if (!this.IsTableClassEmpty(tableClassIndex))
			{
				if (name != null)
				{
					if (continueList)
					{
						cb.Append(" ");
					}
					cb.Append(name);
					cb.Append(":[");
				}
				continueList = false;
				continueList = RowStats.AppendCounterToString(cb, "r", this.GetCounter(tableClassIndex, RowStatsCounterType.Read), continueList);
				continueList = RowStats.AppendCounterToString(cb, "s", this.GetCounter(tableClassIndex, RowStatsCounterType.Seek), continueList);
				continueList = RowStats.AppendCounterToString(cb, "a", this.GetCounter(tableClassIndex, RowStatsCounterType.Accept), continueList);
				continueList = RowStats.AppendCounterToString(cb, "w", this.GetCounter(tableClassIndex, RowStatsCounterType.Write), continueList);
				continueList = RowStats.AppendCounterToString(cb, "rb", this.GetCounter(tableClassIndex, RowStatsCounterType.ReadBytes), continueList);
				continueList = RowStats.AppendCounterToString(cb, "wb", this.GetCounter(tableClassIndex, RowStatsCounterType.WriteBytes), continueList);
				if (name != null)
				{
					cb.Append("]");
				}
				return true;
			}
			return continueList;
		}

		// Token: 0x04000191 RID: 401
		private static readonly SmallRowStatsTableClassIndex[] tableClassIndex = SmallRowStats.CreateTableClassIndex();

		// Token: 0x04000192 RID: 402
		private int[] counters;
	}
}
