using System;
using System.ComponentModel;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x020000CF RID: 207
	public class ColumnValueCalculator
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00018A31 File Offset: 0x00016C31
		public ColumnValueCalculator(string dependentColumn, string resultColumn, TypeConverter typeConverter)
		{
			this.DependentColumn = dependentColumn;
			this.ResultColumn = resultColumn;
			this.TypeConverter = typeConverter;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00018A4E File Offset: 0x00016C4E
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x00018A56 File Offset: 0x00016C56
		public string DependentColumn { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00018A5F File Offset: 0x00016C5F
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x00018A67 File Offset: 0x00016C67
		public string ResultColumn { get; private set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00018A70 File Offset: 0x00016C70
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x00018A78 File Offset: 0x00016C78
		public TypeConverter TypeConverter { get; private set; }

		// Token: 0x06000748 RID: 1864 RVA: 0x00018A84 File Offset: 0x00016C84
		public void Calculate(DataRow dataRow)
		{
			object obj = dataRow[this.DependentColumn];
			Type dataType = dataRow.Table.Columns[this.DependentColumn].DataType;
			if (!DBNull.Value.Equals(obj) && dataType.IsEnum)
			{
				obj = Enum.ToObject(dataType, obj);
			}
			dataRow[this.ResultColumn] = this.TypeConverter.ConvertFrom(obj);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018AF0 File Offset: 0x00016CF0
		public void Calcuate(DataTable table)
		{
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				this.Calculate(dataRow);
			}
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018B4C File Offset: 0x00016D4C
		public static void CalculateAll(DataRow dataRow)
		{
			foreach (object obj in dataRow.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.ExtendedProperties["ColumnValueCalculator"] != null)
				{
					(dataColumn.ExtendedProperties["ColumnValueCalculator"] as ColumnValueCalculator).Calculate(dataRow);
				}
			}
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00018BD0 File Offset: 0x00016DD0
		public static void CalculateAll(DataTable table)
		{
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ColumnValueCalculator.CalculateAll(dataRow);
			}
		}

		// Token: 0x0400037A RID: 890
		public const string ColumnValueCalculatorProperty = "ColumnValueCalculator";
	}
}
