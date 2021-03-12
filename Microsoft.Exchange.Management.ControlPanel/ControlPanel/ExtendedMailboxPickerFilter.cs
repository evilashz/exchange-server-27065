using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200032E RID: 814
	[DataContract]
	public class ExtendedMailboxPickerFilter : RecipientPickerFilterBase
	{
		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x0008F2D8 File Offset: 0x0008D4D8
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
					(RecipientTypeDetails)((ulong)int.MinValue),
					RecipientTypeDetails.RemoteRoomMailbox,
					RecipientTypeDetails.RemoteEquipmentMailbox,
					RecipientTypeDetails.RemoteSharedMailbox,
					RecipientTypeDetails.RemoteTeamMailbox
				};
			}
		}
	}
}
