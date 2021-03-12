using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000044 RID: 68
	public static class IPDelisting
	{
		// Token: 0x060019A0 RID: 6560 RVA: 0x0005234C File Offset: 0x0005054C
		public static void CheckDateAdded(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DateTime dateTime = Convert.ToDateTime(dataRow["DateAdded"].ToString());
				DateTime utcNow = DateTime.UtcNow;
				int days = (utcNow - dateTime).Days;
				dataRow["DaysSinceLastDelist"] = days;
				dataRow["DateAdded"] = string.Format("{0:d}", dateTime);
			}
		}
	}
}
