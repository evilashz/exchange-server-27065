using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000641 RID: 1601
	public class RecipientSlabControl : SlabControl
	{
		// Token: 0x1700270F RID: 9999
		// (get) Token: 0x0600462F RID: 17967 RVA: 0x000D4274 File Offset: 0x000D2474
		// (set) Token: 0x06004630 RID: 17968 RVA: 0x000D427C File Offset: 0x000D247C
		public LocalSearchFilterEditorFeatureSet RecipientFilterEditorFeatureSet { get; set; }

		// Token: 0x06004631 RID: 17969 RVA: 0x000D4288 File Offset: 0x000D2488
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.listView = this.Controls.OfType<ListView>().First<ListView>();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(RecipientSlabControl.dictCommonValidPropValues);
			dictionary.Add("RecipientTypeDetails", RecipientSlabControl.dictValidRecTypes[this.RecipientFilterEditorFeatureSet]);
			if (this.RecipientFilterEditorFeatureSet == LocalSearchFilterEditorFeatureSet.Contacts || this.RecipientFilterEditorFeatureSet == LocalSearchFilterEditorFeatureSet.Mailboxes || this.RecipientFilterEditorFeatureSet == LocalSearchFilterEditorFeatureSet.ResourceMailboxes || this.RecipientFilterEditorFeatureSet == LocalSearchFilterEditorFeatureSet.SharedMailboxes || this.RecipientFilterEditorFeatureSet == LocalSearchFilterEditorFeatureSet.Members)
			{
				List<string> list = new List<string>(CountryInfo.AllCountryInfos.Count * 2);
				foreach (CountryInfo countryInfo in CountryInfo.AllCountryInfos)
				{
					list.Add(countryInfo.Name);
					list.Add(countryInfo.LocalizedDisplayName);
				}
				dictionary.Add("CountryOrRegion", string.Join(",", list.ToArray()));
			}
			this.listView.SearchTextBox.Attributes.Add("vm-ValidPropertyValueRange", dictionary.ToJsonString(null).ToLowerInvariant());
		}

		// Token: 0x04002F6F RID: 12143
		protected ListView listView;

		// Token: 0x04002F70 RID: 12144
		private static readonly Dictionary<LocalSearchFilterEditorFeatureSet, string> dictValidRecTypes = new Dictionary<LocalSearchFilterEditorFeatureSet, string>
		{
			{
				LocalSearchFilterEditorFeatureSet.Contacts,
				"MailContact,MailUser"
			},
			{
				LocalSearchFilterEditorFeatureSet.DistributionGroups,
				"MailNonUniversalGroup,MailUniversalDistributionGroup,MailUniversalSecurityGroup,DynamicDistributionGroup"
			},
			{
				LocalSearchFilterEditorFeatureSet.Mailboxes,
				"UserMailbox,LinkedMailbox,LegacyMailbox,RemoteUserMailbox"
			},
			{
				LocalSearchFilterEditorFeatureSet.ResourceMailboxes,
				"RoomMailbox,EquipmentMailbox"
			},
			{
				LocalSearchFilterEditorFeatureSet.SharedMailboxes,
				"SharedMailbox"
			},
			{
				LocalSearchFilterEditorFeatureSet.Members,
				"Members"
			}
		};

		// Token: 0x04002F71 RID: 12145
		private static readonly Dictionary<string, object> dictCommonValidPropValues = (from a in StringExtension.QueryNameToPropertyDef
		where a.Value.Type == typeof(bool)
		select a.Key).ToDictionary((string p) => p, (string q) => "True,False", StringComparer.InvariantCultureIgnoreCase);
	}
}
