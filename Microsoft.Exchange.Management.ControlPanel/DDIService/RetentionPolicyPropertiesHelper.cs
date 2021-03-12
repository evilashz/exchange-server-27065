using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000266 RID: 614
	public static class RetentionPolicyPropertiesHelper
	{
		// Token: 0x06002943 RID: 10563 RVA: 0x00081C6C File Offset: 0x0007FE6C
		public static void GetListPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			List<DataRow> list = new List<DataRow>();
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (string.Compare((string)dataRow["Name"], "ArbitrationMailbox", StringComparison.Ordinal) == 0)
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

		// Token: 0x06002944 RID: 10564 RVA: 0x00081D3C File Offset: 0x0007FF3C
		public static void WebServiceDropDownForRetention(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Text"] = Strings.NoRetentionPolicy;
			dataRow["Value"] = string.Empty;
			dataTable.Rows.InsertAt(dataRow, 0);
			for (int i = dataTable.Rows.Count - 1; i >= 1; i--)
			{
				if (string.Compare((string)dataTable.Rows[i]["Text"], "ArbitrationMailbox", StringComparison.Ordinal) == 0)
				{
					dataTable.Rows.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		public static void GetForSDOPostAction(DataRow inputrow, DataTable dataTable, DataObjectStore store)
		{
			if (dataTable.Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = dataTable.Rows[0];
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)dataRow["RetentionPolicyTagLinks"];
			dataRow["RetentionPolicyTagLinkLabel"] = ((multiValuedProperty.Count > 0) ? Strings.ViewRetentionPolicyTagLinksLabel : Strings.ViewRetentionPolicyEmptyTagLinks);
		}

		// Token: 0x040020D0 RID: 8400
		private const string ArbitrationPolicy = "ArbitrationMailbox";
	}
}
