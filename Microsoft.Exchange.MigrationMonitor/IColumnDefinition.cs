using System;
using System.Data;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200000E RID: 14
	internal interface IColumnDefinition
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000061 RID: 97
		string ColumnName { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000062 RID: 98
		Type ColumnType { get; }

		// Token: 0x06000063 RID: 99
		void InsertColumnValueInDataRow(DataRow dataRow, CsvRow row);
	}
}
