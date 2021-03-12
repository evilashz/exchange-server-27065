using System;
using System.Data;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200002B RID: 43
	public class AddressBookPolicyServiceCodeBehind
	{
		// Token: 0x060018E0 RID: 6368 RVA: 0x0004E330 File Offset: 0x0004C530
		public static void SortAndAddNoPolicyRow(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["ABPName"] = Strings.AddressBookPolicyNoPolicy;
			dataRow["ABPIdentity"] = string.Empty;
			dataRow["IsNoPolicy"] = true.ToString();
			dataTable.Rows.InsertAt(dataRow, 0);
			dataTable.DefaultView.Sort = "IsNoPolicy DESC, ABPName ASC";
		}
	}
}
