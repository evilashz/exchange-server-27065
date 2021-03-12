using System;
using System.Data;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000236 RID: 566
	public class AcceptedDomainUPNSuffixesConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A1D RID: 6685 RVA: 0x0007201C File Offset: 0x0007021C
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("OtherSuffix", typeof(string)));
			DataTable dataTable2 = new DataTable();
			dataTable2.Columns.Add(new DataColumn("Default", typeof(bool)));
			dataTable2.Columns.AddColumnWithExpectedType("DomainName", typeof(SmtpDomainWithSubdomains));
			DataColumn dataColumn = new DataColumn("SmtpDomainToString", typeof(string));
			dataColumn.ExtendedProperties["LambdaExpression"] = string.Format("{0}=>SmtpDomainWithSubdomains(@0[{0}]).SmtpDomain.ToString()", "DomainName");
			dataTable2.Columns.Add(dataColumn);
			dataTable2.PrimaryKey = new DataColumn[]
			{
				dataColumn
			};
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("SmtpDomainToString", true, Strings.DomainNameColumnInPicker);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(null, true, null, dataTable, dataTable2, new ResultsColumnProfile[]
			{
				resultsColumnProfile
			});
			resultsLoaderProfile.NameProperty = "SmtpDomainToString";
			resultsLoaderProfile.DistinguishIdentity = "SmtpDomainToString";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-AcceptedDomain", new ExchangeCommandBuilder()));
			resultsLoaderProfile.AddTableFiller(new OtherUPNSuffixFiller("OtherSuffix", "DomainName"));
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.UPNSuffixesPicker";
			resultsLoaderProfile.FillType = 1;
			return resultsLoaderProfile;
		}
	}
}
