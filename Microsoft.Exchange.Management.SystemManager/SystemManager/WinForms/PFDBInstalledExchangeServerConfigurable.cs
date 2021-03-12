using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200023F RID: 575
	public class PFDBInstalledExchangeServerConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A34 RID: 6708 RVA: 0x00072DFC File Offset: 0x00070FFC
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn("ExcludeServer", typeof(string));
			DataColumn column2 = new DataColumn("MinVersion", typeof(long));
			DataColumn column3 = new DataColumn("DesiredServerRoleBitMask", typeof(ServerRole));
			DataColumn column4 = new DataColumn("IncludeLegacyServer", typeof(bool));
			dataTable.Columns.Add(column);
			dataTable.Columns.Add(column2);
			dataTable.Columns.Add(column3);
			dataTable.Columns.Add(column4);
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("ADSiteShortName", true, Strings.ServerSite);
			ResultsColumnProfile resultsColumnProfile3 = new ResultsColumnProfile("ServerRole", true, Strings.ServerRole);
			new ResultsColumnProfile("CeipStatus", true, Strings.CeipStatus);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(Strings.PublicFolderDataBaseInstalledExchangeServerPickerText, false, "ImageProperty", dataTable, BridgeheadEdgeSubServerConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2,
				resultsColumnProfile3
			});
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-PublicFolderDataBase -IncludePreExchange2013", new ExchangeCommandBuilder(new PFDBInstalledExchangeServerFilterBuilder())
			{
				SearchType = 2,
				NamePropertyFilter = resultsLoaderProfile.NameProperty
			})
			{
				ResolveCommandText = "Get-ExchangeServer"
			});
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.PFDBInstalledExchangeServerPicker";
			return resultsLoaderProfile;
		}
	}
}
