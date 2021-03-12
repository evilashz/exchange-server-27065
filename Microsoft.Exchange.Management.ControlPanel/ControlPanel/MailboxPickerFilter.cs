using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000333 RID: 819
	[DataContract]
	public class MailboxPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x0008F3C4 File Offset: 0x0008D5C4
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
					RecipientTypeDetails.EquipmentMailbox
				};
			}
		}
	}
}
