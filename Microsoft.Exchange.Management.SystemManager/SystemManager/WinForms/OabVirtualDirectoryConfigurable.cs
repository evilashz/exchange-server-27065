using System;
using System.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200023B RID: 571
	public class OabVirtualDirectoryConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x000727B0 File Offset: 0x000709B0
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable inputTable = new DataTable();
			DataTable commonDataTable = ConfigurableHelper.GetCommonDataTable();
			DataColumn dataColumn = new DataColumn("ImageProperty", typeof(string));
			dataColumn.DefaultValue = "OabVirtualDirectoryPicker";
			commonDataTable.Columns.Add(dataColumn);
			commonDataTable.Columns.Add(new DataColumn("Server", typeof(string)));
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("Server", true, Strings.ServerColumn);
			return new ResultsLoaderProfile(Strings.OabVirtualDirectory, "ImageProperty", "Get-OabVirtualDirectory", inputTable, commonDataTable, new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2
			}, new ExchangeCommandBuilder())
			{
				HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.OabVirtualDirectoryPicker"
			};
		}
	}
}
