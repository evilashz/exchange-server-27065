using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x020001EB RID: 491
	public class OrganizationRelationshipAssistor
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x000753C8 File Offset: 0x000735C8
		private static MultiValuedProperty<string> ToStringMVP(MultiValuedProperty<SmtpDomain> domainsMVP)
		{
			MultiValuedProperty<string> multiValuedProperty = new MultiValuedProperty<string>();
			if (domainsMVP != null)
			{
				foreach (SmtpDomain smtpDomain in domainsMVP)
				{
					multiValuedProperty.Add(smtpDomain.ToString());
				}
			}
			multiValuedProperty.Sort();
			return multiValuedProperty;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x00075434 File Offset: 0x00073634
		public static void GetObjectPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			OrganizationRelationship organizationRelationship = store.GetDataObject("OrganizationRelationship") as OrganizationRelationship;
			if (organizationRelationship != null && dataTable.Rows.Count == 1)
			{
				DataRow dataRow = dataTable.Rows[0];
				if (organizationRelationship.FreeBusyAccessLevel == FreeBusyAccessLevel.None)
				{
					dataRow["FreeBusyAccessEnabled"] = false;
					dataRow["FreeBusyAccessLevel"] = FreeBusyAccessLevel.AvailabilityOnly;
				}
				dataRow["DomainNames"] = OrganizationRelationshipAssistor.ToStringMVP(organizationRelationship.DomainNames);
				dataRow["FormattedDomainNames"] = DDIHelper.JoinList<SmtpDomain>(organizationRelationship.DomainNames, (SmtpDomain domain) => domain.Domain);
			}
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000754F4 File Offset: 0x000736F4
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				dataRow["FormattedDomainNames"] = DDIHelper.JoinList<SmtpDomain>(dataRow["DomainNames"] as MultiValuedProperty<SmtpDomain>, (SmtpDomain domain) => domain.Domain);
			}
			dataTable.EndLoadData();
		}

		// Token: 0x04001F34 RID: 7988
		private const string FreeBusyAccessEnabledColumnName = "FreeBusyAccessEnabled";

		// Token: 0x04001F35 RID: 7989
		private const string FreeBusyAccessLevelColumnName = "FreeBusyAccessLevel";

		// Token: 0x04001F36 RID: 7990
		private const string OrganizationRelationShipObjectName = "OrganizationRelationship";

		// Token: 0x04001F37 RID: 7991
		private const string DomainNamesColumnName = "DomainNames";

		// Token: 0x04001F38 RID: 7992
		private const string FormattedDomainNamesColumnName = "FormattedDomainNames";
	}
}
