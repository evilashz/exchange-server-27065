using System;
using System.Data;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000233 RID: 563
	public class ADRMSTemplateConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A17 RID: 6679 RVA: 0x00071C4C File Offset: 0x0006FE4C
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable inputTable = new DataTable();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
			dataTable.Columns.Add(new DataColumn("Identity", typeof(ObjectId)));
			DataColumn dataColumn = new DataColumn("Guid", typeof(Guid));
			dataColumn.ExtendedProperties["LambdaExpression"] = string.Format("{0}=>RmsTemplateIdentity(@0[{0}]).TemplateId", "Identity");
			dataTable.Columns.Add(dataColumn);
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["Guid"]
			};
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(Strings.RMSTemplate, true, null, "Get-RMSTemplate", inputTable, dataTable, new ResultsColumnProfile[]
			{
				resultsColumnProfile
			}, new ExchangeCommandBuilder())
			{
				SerializationLevel = ExchangeRunspaceConfigurationSettings.SerializationLevel.Full
			};
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.ADRMSTemplatePicker";
			return resultsLoaderProfile;
		}
	}
}
