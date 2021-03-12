using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000247 RID: 583
	public class MerRecipientConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A45 RID: 6725 RVA: 0x00073C24 File Offset: 0x00071E24
		public virtual ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("ExcludeObject", typeof(ADObjectId)));
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("DisplayName", true, Strings.DisplayNameColumnInPicker);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("Alias", true, Strings.AliasColumnInPicker);
			ResultsColumnProfile resultsColumnProfile3 = new ResultsColumnProfile("RecipientTypeDetails", true, Strings.RecipientTypeDetailsColumnInPicker);
			ResultsColumnProfile resultsColumnProfile4 = new ResultsColumnProfile("PrimarySmtpAddressToString", true, Strings.PrimarySmtpAddressColumnInPicker);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(Strings.Recipient, false, "RecipientTypeDetails", dataTable, ContactConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2,
				resultsColumnProfile3,
				resultsColumnProfile4
			})
			{
				SerializationLevel = ExchangeRunspaceConfigurationSettings.SerializationLevel.Full,
				ScopeSupportingLevel = ScopeSupportingLevel.FullScoping,
				WholeObjectProperty = "WholeObjectProperty",
				NameProperty = "DisplayName"
			};
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			dictionary["PropertySet"] = "ConsoleSmallSet";
			dictionary["RecipientTypeDetails"] = "MailUser,RemoteUserMailbox,RemoteRoomMailbox,RemoteEquipmentMailbox,RemoteSharedMailbox,MailContact,DynamicDistributionGroup,MailUniversalDistributionGroup,MailUniversalSecurityGroup,MailNonUniversalGroup,UserMailbox,LinkedMailbox,SharedMailbox,LegacyMailbox,RoomMailbox,EquipmentMailbox";
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Recipient", new ExchangeCommandBuilder(new ExcludeObjectFilterBuilder())
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.MerRecipientsPicker";
			return resultsLoaderProfile;
		}
	}
}
