using System;
using System.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200023E RID: 574
	public class CeipServerConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x00072CC4 File Offset: 0x00070EC4
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			DataColumn column = new DataColumn("MinVersion", typeof(long));
			DataColumn column2 = new DataColumn("DesiredServerRoleBitMask", typeof(ServerRole));
			DataColumn column3 = new DataColumn("IncludeLegacyServer", typeof(bool));
			dataTable.Columns.Add(column);
			dataTable.Columns.Add(column2);
			dataTable.Columns.Add(column3);
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("ADSiteShortName", true, Strings.ServerSite);
			ResultsColumnProfile resultsColumnProfile3 = new ResultsColumnProfile("ServerRole", true, Strings.ServerRole);
			ResultsColumnProfile resultsColumnProfile4 = new ResultsColumnProfile("CeipStatus", true, Strings.CeipStatus);
			return new ResultsLoaderProfile(Strings.ExchangeServer, "ImageProperty", "Get-ExchangeServer", dataTable, BridgeheadEdgeSubServerConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2,
				resultsColumnProfile3,
				resultsColumnProfile4
			}, new ExchangeCommandBuilder(new ExchangeServerFilterBuilder()))
			{
				HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.CeipServerPicker"
			};
		}
	}
}
