using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000235 RID: 565
	public class AcceptedDomainConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A1B RID: 6683 RVA: 0x00071E0C File Offset: 0x0007000C
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("ExcludeExternalRelay", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("ExcludeAuthoritative", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("ExcludeInternalRelay", typeof(bool)));
			dataTable.Columns.Add(new DataColumn("ExcludeDomainWithSubDomain", typeof(bool)));
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.AddRange(ConfigurableHelper.GetCommonDataColumns());
			dataTable2.Columns.Add(new DataColumn("Default", typeof(bool)));
			dataTable2.Columns.AddColumnWithExpectedType("DomainName", typeof(SmtpDomainWithSubdomains));
			DataColumn dataColumn = new DataColumn("ImageProperty", typeof(string));
			dataColumn.DefaultValue = "AcceptedDomainPicker";
			dataTable2.Columns.Add(dataColumn);
			DataColumn dataColumn2 = new DataColumn("DisplayedDomainName", typeof(string));
			dataColumn2.ExtendedProperties["LambdaExpression"] = string.Format("{0} => @0[{0}].ToString()", "DomainName");
			dataTable2.Columns.Add(dataColumn2);
			DataColumn dataColumn3 = new DataColumn("SmtpDomain", typeof(SmtpDomain));
			dataColumn3.ExtendedProperties["LambdaExpression"] = string.Format("{0}=>SmtpDomainWithSubdomains(@0[{0}]).SmtpDomain", "DomainName");
			dataTable2.Columns.Add(dataColumn3);
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("DisplayedDomainName", true, Strings.DomainNameColumnInPicker);
			return new ResultsLoaderProfile(Strings.AcceptedDomain, "ImageProperty", "Get-AcceptedDomain", dataTable, dataTable2, new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2
			}, new ExchangeCommandBuilder(new AcceptedDomainFilterBuilder()))
			{
				HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.AcceptedDomainPicker"
			};
		}
	}
}
