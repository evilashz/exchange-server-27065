using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000276 RID: 630
	public class OwaMailboxPolicyProperties
	{
		// Token: 0x060029A9 RID: 10665 RVA: 0x00083354 File Offset: 0x00081554
		public static void GetDefaultPolicyPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			List<DataRow> list = new List<DataRow>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				if (false.Equals(dataRow["IsDefault"]))
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row in list)
			{
				dataTable.Rows.Remove(row);
			}
			dataTable.EndLoadData();
		}
	}
}
