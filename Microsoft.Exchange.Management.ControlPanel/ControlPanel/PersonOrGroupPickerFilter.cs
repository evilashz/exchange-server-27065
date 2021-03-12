using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200034D RID: 845
	[DataContract]
	public class PersonOrGroupPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000910E0 File Offset: 0x0008F2E0
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.RoomMailbox,
					RecipientTypeDetails.EquipmentMailbox,
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.MailContact,
					RecipientTypeDetails.MailForestContact,
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup,
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.DynamicDistributionGroup,
					(RecipientTypeDetails)((ulong)int.MinValue)
				};
			}
		}
	}
}
