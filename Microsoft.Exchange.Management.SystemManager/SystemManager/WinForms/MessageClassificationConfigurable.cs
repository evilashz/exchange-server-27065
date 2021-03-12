using System;
using System.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000234 RID: 564
	public class MessageClassificationConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A19 RID: 6681 RVA: 0x00071D70 File Offset: 0x0006FF70
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable inputTable = new DataTable();
			DataTable commonDataTable = ConfigurableHelper.GetCommonDataTable();
			commonDataTable.Columns.Add(new DataColumn("DisplayName", typeof(string)));
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("DisplayName", true, Strings.DisplayNameColumnInPicker);
			return new ResultsLoaderProfile(Strings.MessageClassification, true, null, "Get-MessageClassification", inputTable, commonDataTable, new ResultsColumnProfile[]
			{
				resultsColumnProfile
			}, new ExchangeCommandBuilder())
			{
				NameProperty = "DisplayName",
				HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.MessageClassificationPicker"
			};
		}
	}
}
