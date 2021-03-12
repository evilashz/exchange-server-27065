using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200034C RID: 844
	[DataContract]
	public class RecipientPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x00091048 File Offset: 0x0008F248
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.RoomMailbox,
					RecipientTypeDetails.EquipmentMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.MailContact,
					RecipientTypeDetails.DynamicDistributionGroup,
					RecipientTypeDetails.MailForestContact,
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup,
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.PublicFolder,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.SharedMailbox
				};
			}
		}
	}
}
