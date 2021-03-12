using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000241 RID: 577
	public class UniversalGroupConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A39 RID: 6713 RVA: 0x000733A8 File Offset: 0x000715A8
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("GroupType", true, Strings.GroupTypeColumnInPicker);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(Strings.Group, false, "RecipientTypeDetails", new DataTable(), ContactConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			dictionary["RecipientTypeDetails"] = "UniversalDistributionGroup,UniversalSecurityGroup";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Group", new ExchangeCommandBuilder
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.UniversalGroupPicker";
			return resultsLoaderProfile;
		}
	}
}
