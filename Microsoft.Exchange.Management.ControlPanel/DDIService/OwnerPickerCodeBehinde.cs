using System;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000345 RID: 837
	public class OwnerPickerCodeBehinde : DDICodeBehind
	{
		// Token: 0x06002F4E RID: 12110 RVA: 0x0009058C File Offset: 0x0008E78C
		public static void GetSecurityGroupFilterPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store, PowerShellResults[] results)
		{
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = (string)dataRow["DisplayName"];
				if (DBNull.Value.Equals(text) || string.IsNullOrEmpty(text))
				{
					dataRow["DisplayName"] = dataRow["Name"];
				}
			}
			dataTable.EndLoadData();
		}
	}
}
