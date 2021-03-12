using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x02000015 RID: 21
	internal sealed class RefinersResultRow
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00005575 File Offset: 0x00003775
		private RefinersResultRow(string entryName, long entryCount, double sum, double min, double max)
		{
			this.entryName = entryName;
			this.entryCount = entryCount;
			this.sum = sum;
			this.min = min;
			this.max = max;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000055A2 File Offset: 0x000037A2
		internal string EntryName
		{
			get
			{
				return this.entryName;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000055AA File Offset: 0x000037AA
		internal long EntryCount
		{
			get
			{
				return this.entryCount;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000055B2 File Offset: 0x000037B2
		internal double Sum
		{
			get
			{
				return this.sum;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000055BA File Offset: 0x000037BA
		internal double Min
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000055C2 File Offset: 0x000037C2
		internal double Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000055CA File Offset: 0x000037CA
		internal static RefinersResultRow NewRow(string entryName, long entryCount)
		{
			return RefinersResultRow.NewRow(entryName, entryCount, 0.0, 0.0, 0.0);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000055EE File Offset: 0x000037EE
		internal static RefinersResultRow NewRow(string entryName, long entryCount, double sum, double min, double max)
		{
			Globals.AssertRetail(!string.IsNullOrEmpty(entryName), "Invalid Refiner Entry Name");
			Globals.AssertRetail(entryCount >= 0L, "Invalid Refiner Entry Count");
			return new RefinersResultRow(entryName, entryCount, sum, min, max);
		}

		// Token: 0x04000062 RID: 98
		private readonly string entryName;

		// Token: 0x04000063 RID: 99
		private readonly long entryCount;

		// Token: 0x04000064 RID: 100
		private readonly double sum;

		// Token: 0x04000065 RID: 101
		private readonly double max;

		// Token: 0x04000066 RID: 102
		private readonly double min;
	}
}
