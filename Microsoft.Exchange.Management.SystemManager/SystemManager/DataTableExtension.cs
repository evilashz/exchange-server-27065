using System;
using System.Data;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200002E RID: 46
	public static class DataTableExtension
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000849C File Offset: 0x0000669C
		public static void ImportRows(this DataTable dataTable, DataRow[] rows)
		{
			try
			{
				dataTable.BeginLoadData();
				foreach (DataRow row in rows)
				{
					dataTable.ImportRow(row);
				}
			}
			finally
			{
				dataTable.EndLoadData();
			}
		}
	}
}
