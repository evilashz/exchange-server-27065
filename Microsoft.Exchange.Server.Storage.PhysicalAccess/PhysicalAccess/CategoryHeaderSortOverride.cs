using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x0200007C RID: 124
	public class CategoryHeaderSortOverride
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00019894 File Offset: 0x00017A94
		public Column Column
		{
			get
			{
				return this.sortColumnForAggregation.Column;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000198B0 File Offset: 0x00017AB0
		public bool Ascending
		{
			get
			{
				return this.sortColumnForAggregation.Ascending;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x000198CB File Offset: 0x00017ACB
		public bool AggregateByMaxValue
		{
			get
			{
				return this.aggregateByMaxValue;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000198D3 File Offset: 0x00017AD3
		public CategoryHeaderSortOverride(Column column, bool ascending, bool aggregateByMaxValue)
		{
			this.sortColumnForAggregation = new SortColumn(column, ascending);
			this.aggregateByMaxValue = aggregateByMaxValue;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000198F0 File Offset: 0x00017AF0
		public static int NumberOfOverrides(CategoryHeaderSortOverride[] categoryHeaderSortOverrides)
		{
			int num = 0;
			foreach (CategoryHeaderSortOverride categoryHeaderSortOverride in categoryHeaderSortOverrides)
			{
				if (categoryHeaderSortOverride != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001991C File Offset: 0x00017B1C
		public static bool ContainsColumn(IList<CategoryHeaderSortOverride> categoryHeaderSortOverrides, Column column)
		{
			foreach (CategoryHeaderSortOverride categoryHeaderSortOverride in categoryHeaderSortOverrides)
			{
				if (categoryHeaderSortOverride != null && categoryHeaderSortOverride.Column == column)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00019978 File Offset: 0x00017B78
		public static CategoryHeaderSortOverride Deserialize(byte[] buffer, ref int offset, Func<int, string, Column> convertToColumn)
		{
			string arg = SerializedValue.ParseString(buffer, ref offset);
			int arg2 = SerializedValue.ParseInt32(buffer, ref offset);
			bool ascending = SerializedValue.ParseBoolean(buffer, ref offset);
			bool flag = SerializedValue.ParseBoolean(buffer, ref offset);
			return new CategoryHeaderSortOverride(convertToColumn(arg2, arg), ascending, flag);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000199B4 File Offset: 0x00017BB4
		public int Serialize(byte[] buffer, int startingOffset)
		{
			string value;
			uint value2;
			this.Column.GetNameOrIdForSerialization(out value, out value2);
			int num = startingOffset + SerializedValue.SerializeString(value, buffer, startingOffset);
			num += SerializedValue.SerializeInt32((int)value2, buffer, num);
			num += SerializedValue.SerializeBoolean(this.Ascending, buffer, num);
			num += SerializedValue.SerializeBoolean(this.AggregateByMaxValue, buffer, num);
			return num - startingOffset;
		}

		// Token: 0x040001A3 RID: 419
		private readonly SortColumn sortColumnForAggregation;

		// Token: 0x040001A4 RID: 420
		private readonly bool aggregateByMaxValue;
	}
}
