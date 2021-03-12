using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000353 RID: 851
	[DataContract]
	public class SearchMailboxPickerFilter : RecipientPickerVersionFilter
	{
		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000913C4 File Offset: 0x0008F5C4
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
