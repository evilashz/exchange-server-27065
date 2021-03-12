using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200000F RID: 15
	internal class ColumnDefinition<T> : IColumnDefinition
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003AD3 File Offset: 0x00001CD3
		public ColumnDefinition() : this(string.Empty)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AE0 File Offset: 0x00001CE0
		public ColumnDefinition(string columnName)
		{
			this.ColumnName = columnName;
			this.DataTableKeyColumnName = columnName;
			this.KnownStringType = KnownStringType.None;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003AFD File Offset: 0x00001CFD
		public ColumnDefinition(string columnName, string dataTableKeyColumnName, KnownStringType knownStringType) : this(columnName)
		{
			this.DataTableKeyColumnName = dataTableKeyColumnName;
			this.KnownStringType = knownStringType;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003B14 File Offset: 0x00001D14
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003B1C File Offset: 0x00001D1C
		public string ColumnName { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003B25 File Offset: 0x00001D25
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003B2D File Offset: 0x00001D2D
		public string DataTableKeyColumnName { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003B36 File Offset: 0x00001D36
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003B3E File Offset: 0x00001D3E
		public KnownStringType KnownStringType { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003B47 File Offset: 0x00001D47
		public Type ColumnType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003B53 File Offset: 0x00001D53
		public void InsertColumnValueInDataRow(DataRow dataRow, CsvRow row)
		{
			dataRow[this.ColumnName] = MigMonUtilities.GetColumnValue<T>(row, this.ColumnName);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003B74 File Offset: 0x00001D74
		public string GetConvertedRowString(CsvRow row)
		{
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, this.ColumnName);
			return KnownStringsHelper.ConvertStringValueByType(this.KnownStringType, columnStringValue);
		}
	}
}
