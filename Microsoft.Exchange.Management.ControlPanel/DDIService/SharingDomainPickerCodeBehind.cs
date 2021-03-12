using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200035B RID: 859
	public class SharingDomainPickerCodeBehind
	{
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000918F4 File Offset: 0x0008FAF4
		public static void FilterForSharingDomain(DataRow row, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if ((AcceptedDomainType)dataRow["DomainType"] == AcceptedDomainType.ExternalRelay || ((string)dataRow["DomainName"]).IndexOf("*") >= 0)
				{
					list.Add(dataRow);
				}
			}
			foreach (DataRow row2 in list)
			{
				dataTable.Rows.Remove(row2);
			}
		}
	}
}
