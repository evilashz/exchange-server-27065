using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200033D RID: 829
	[DataContract]
	public class ModeratorPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000903C0 File Offset: 0x0008E5C0
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.MailUser,
					RecipientTypeDetails.MailContact,
					RecipientTypeDetails.MailForestContact,
					(RecipientTypeDetails)((ulong)int.MinValue)
				};
			}
		}
	}
}
