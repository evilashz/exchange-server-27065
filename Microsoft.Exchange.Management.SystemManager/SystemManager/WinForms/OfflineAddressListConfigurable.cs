using System;
using System.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200023A RID: 570
	public class OfflineAddressListConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A26 RID: 6694 RVA: 0x0007270C File Offset: 0x0007090C
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable inputTable = new DataTable();
			DataTable commonDataTable = ConfigurableHelper.GetCommonDataTable();
			DataColumn dataColumn = new DataColumn("ImageProperty", typeof(string));
			dataColumn.DefaultValue = "OfflineAddressBookPicker";
			commonDataTable.Columns.Add(dataColumn);
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			return new ResultsLoaderProfile(Strings.OfflineAddressList, "ImageProperty", "Get-OfflineAddressBook", inputTable, commonDataTable, new ResultsColumnProfile[]
			{
				resultsColumnProfile
			}, new ExchangeCommandBuilder())
			{
				HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.OfflineAddressListPicker"
			};
		}
	}
}
