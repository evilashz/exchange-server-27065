using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D3 RID: 467
	[DataContract]
	public class PreviewPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x000724C1 File Offset: 0x000706C1
		// (set) Token: 0x06002557 RID: 9559 RVA: 0x000724C9 File Offset: 0x000706C9
		[DataMember]
		public SupportRecipientFilterObject CustomizedFilters { get; set; }

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x000724D2 File Offset: 0x000706D2
		// (set) Token: 0x06002559 RID: 9561 RVA: 0x000724DA File Offset: 0x000706DA
		[DataMember]
		public string PreviewType { get; set; }

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x000724E3 File Offset: 0x000706E3
		// (set) Token: 0x0600255B RID: 9563 RVA: 0x000724EB File Offset: 0x000706EB
		public bool HasCondition { get; set; }

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x000724F4 File Offset: 0x000706F4
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.EquipmentMailbox,
					RecipientTypeDetails.DynamicDistributionGroup,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.MailContact,
					RecipientTypeDetails.MailForestContact,
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup,
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.PublicFolder,
					(RecipientTypeDetails)((ulong)int.MinValue),
					RecipientTypeDetails.RemoteRoomMailbox,
					RecipientTypeDetails.RemoteEquipmentMailbox,
					RecipientTypeDetails.RemoteSharedMailbox,
					RecipientTypeDetails.RemoteTeamMailbox,
					RecipientTypeDetails.RoomMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.UserMailbox
				};
			}
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x000725C0 File Offset: 0x000707C0
		protected override void UpdateFilterProperty()
		{
			string text = null;
			if (this.CustomizedFilters != null)
			{
				if (this.CustomizedFilters.DataObject != null && !string.IsNullOrEmpty(this.CustomizedFilters.DataObject.LdapRecipientFilter))
				{
					text = this.CustomizedFilters.DataObject.LdapRecipientFilter;
					this.HasCondition = true;
				}
				else if (!string.IsNullOrEmpty(this.CustomizedFilters.LdapRecipientFilter))
				{
					text = this.CustomizedFilters.LdapRecipientFilter;
					this.HasCondition = true;
				}
				if (this.CustomizedFilters.RecipientContainer != null)
				{
					base["OrganizationalUnit"] = this.CustomizedFilters.RecipientContainer;
					this.HasCondition = true;
				}
			}
			if (this.HasCondition)
			{
				bool flag = this.PreviewType != null && this.PreviewType.Equals("al", StringComparison.InvariantCultureIgnoreCase);
				if (text != null)
				{
					if (flag)
					{
						text = string.Format("(&{0}{1})", PreviewPickerFilter.ldapHiddenFromALFilter, text);
					}
					base["RecipientPreviewFilter"] = text;
					return;
				}
				if (flag)
				{
					base["Filter"] = PreviewPickerFilter.recipientHiddenFromALFilter;
				}
			}
		}

		// Token: 0x04001ECB RID: 7883
		public new const string RbacParameters = "?ResultSize&Filter&RecipientTypeDetails&Properties&RecipientPreviewFilter&OrganizationalUnit";

		// Token: 0x04001ECC RID: 7884
		private const string PreviewType_AL = "al";

		// Token: 0x04001ECD RID: 7885
		private static ComparisonFilter hiddenFromALFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ADRecipientSchema.HiddenFromAddressListsValue, true);

		// Token: 0x04001ECE RID: 7886
		private static string ldapHiddenFromALFilter = LdapFilterBuilder.LdapFilterFromQueryFilter(PreviewPickerFilter.hiddenFromALFilter);

		// Token: 0x04001ECF RID: 7887
		private static string recipientHiddenFromALFilter = PreviewPickerFilter.hiddenFromALFilter.GenerateInfixString(FilterLanguage.Monad);
	}
}
