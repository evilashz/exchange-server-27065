using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000248 RID: 584
	public class MerMailEnableRecipientConfigurable : IResultsLoaderConfiguration
	{
		// Token: 0x06001A47 RID: 6727 RVA: 0x00073D9B File Offset: 0x00071F9B
		public MerMailEnableRecipientConfigurable() : this(true, true, true, Strings.Recipient)
		{
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00073DB0 File Offset: 0x00071FB0
		protected MerMailEnableRecipientConfigurable(bool allowedGroups, bool allowedNonGroups, bool allowedPublicFolder, string displayName)
		{
			this.allowedGroups = allowedGroups;
			this.allowedNonGroups = allowedNonGroups;
			this.allowedPublicFolder = allowedPublicFolder;
			this.displayName = displayName;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00073DD8 File Offset: 0x00071FD8
		public virtual ResultsLoaderProfile BuildResultsLoaderProfile()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("ExcludeObject", typeof(ADObjectId)));
			ResultsColumnProfile resultsColumnProfile = new ResultsColumnProfile("DisplayName", true, Strings.DisplayNameColumnInPicker);
			ResultsColumnProfile resultsColumnProfile2 = new ResultsColumnProfile("Alias", true, Strings.AliasColumnInPicker);
			ResultsColumnProfile resultsColumnProfile3 = new ResultsColumnProfile("RecipientTypeDetails", true, Strings.RecipientTypeDetailsColumnInPicker);
			ResultsColumnProfile resultsColumnProfile4 = new ResultsColumnProfile("PrimarySmtpAddressToString", true, Strings.PrimarySmtpAddressColumnInPicker);
			ResultsLoaderProfile resultsLoaderProfile = new ResultsLoaderProfile(this.displayName, false, "RecipientTypeDetails", dataTable, ContactConfigurable.GetDataTable(), new ResultsColumnProfile[]
			{
				resultsColumnProfile,
				resultsColumnProfile2,
				resultsColumnProfile3,
				resultsColumnProfile4
			});
			resultsLoaderProfile.ScopeSupportingLevel = ScopeSupportingLevel.FullScoping;
			resultsLoaderProfile.WholeObjectProperty = "WholeObjectProperty";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["ResultSize"] = "Unlimited";
			dictionary["PropertySet"] = "ConsoleSmallSet";
			StringBuilder stringBuilder = new StringBuilder();
			if (this.allowedNonGroups)
			{
				stringBuilder.Append(",RoomMailbox,EquipmentMailbox,LegacyMailbox,LinkedMailbox,UserMailbox,MailContact,MailForestContact,MailUser,RemoteUserMailbox,RemoteRoomMailbox,RemoteEquipmentMailbox,RemoteSharedMailbox,SharedMailbox");
			}
			if (this.allowedPublicFolder)
			{
				stringBuilder.Append(",PublicFolder");
			}
			if (this.allowedGroups)
			{
				stringBuilder.Append(",DynamicDistributionGroup,MailNonUniversalGroup,MailUniversalDistributionGroup,MailUniversalSecurityGroup");
			}
			stringBuilder.Remove(0, 1);
			dictionary["RecipientTypeDetails"] = stringBuilder.ToString();
			resultsLoaderProfile.AddTableFiller(new MonadAdapterFiller("Get-Recipient", new ExchangeCommandBuilder(new WithPrimarySmtpAddressRecipientFilterBuilder())
			{
				SearchType = 0
			})
			{
				Parameters = dictionary
			});
			resultsLoaderProfile.NameProperty = "DisplayName";
			resultsLoaderProfile.HelpTopic = "Microsoft.Exchange.Management.SystemManager.WinForms.MerMailEnableRecipientConfigurable";
			return resultsLoaderProfile;
		}

		// Token: 0x040009CF RID: 2511
		private bool allowedGroups;

		// Token: 0x040009D0 RID: 2512
		private bool allowedNonGroups;

		// Token: 0x040009D1 RID: 2513
		private bool allowedPublicFolder;

		// Token: 0x040009D2 RID: 2514
		private string displayName;
	}
}
