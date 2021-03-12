using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001EE RID: 494
	public class SharingPolicyAssistor
	{
		// Token: 0x06002621 RID: 9761 RVA: 0x000756D0 File Offset: 0x000738D0
		private static void UpdateFormattedNameAndDomains(DataRow row)
		{
			row["FormattedName"] = (true.Equals(row["Default"]) ? string.Format(Strings.DefaultSharingPolicyFormatString, row["Name"]) : row["Name"]);
			row["FormattedDomains"] = DDIHelper.JoinList<SharingPolicyDomain>(row["Domains"] as MultiValuedProperty<SharingPolicyDomain>, delegate(SharingPolicyDomain policyDomain)
			{
				if (policyDomain.Domain == "*")
				{
					return Strings.SharingDomainOptionAll;
				}
				return policyDomain.Domain;
			});
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00075764 File Offset: 0x00073964
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			SharingPolicy sharingPolicy = store.GetDataObject("SharingPolicy") as SharingPolicy;
			if (dataTable.Rows.Count == 1 && sharingPolicy != null)
			{
				SharingPolicyAssistor.UpdateFormattedNameAndDomains(dataTable.Rows[0]);
			}
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000757A4 File Offset: 0x000739A4
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow row = (DataRow)obj;
				SharingPolicyAssistor.UpdateFormattedNameAndDomains(row);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00075808 File Offset: 0x00073A08
		public static void GetDefaultPolicyPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			int count = dataTable.Rows.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				DataRow dataRow = dataTable.Rows[i];
				if (false.Equals(dataRow["Default"]))
				{
					dataTable.Rows.Remove(dataRow);
				}
			}
			if (1 == dataTable.Rows.Count)
			{
				SharingPolicyAssistor.UpdateFormattedNameAndDomains(dataTable.Rows[0]);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x04001F44 RID: 8004
		private const string ObjectName = "SharingPolicy";

		// Token: 0x04001F45 RID: 8005
		private const string DefaultColumnName = "Default";

		// Token: 0x04001F46 RID: 8006
		private const string NameColumnName = "Name";

		// Token: 0x04001F47 RID: 8007
		private const string DomainsColumnName = "Domains";

		// Token: 0x04001F48 RID: 8008
		private const string FormattedNameColumnName = "FormattedName";

		// Token: 0x04001F49 RID: 8009
		private const string FormattedDomainsColumnName = "FormattedDomains";
	}
}
