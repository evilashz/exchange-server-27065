using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000242 RID: 578
	public class ExtendedRecipientConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x0007347C File Offset: 0x0007167C
		public ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("Name", true, Strings.Name);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(Strings.Recipient, false, "RecipientTypeDetails", new DataTable(), ContactConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-User", new ExchangeCommandBuilder
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Group", new ExchangeCommandBuilder
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Contact", new ExchangeCommandBuilder
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			dictionary["PropertySet"] = "ConsoleSmallSet";
			dictionary["RecipientTypeDetails"] = "PublicFolder,DynamicDistributionGroup";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Recipient", new ExchangeCommandBuilder
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.ExtendedRecipientsPicker";
			return resultsLoaderProfile;
		}
	}
}
