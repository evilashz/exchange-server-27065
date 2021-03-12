using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200035F RID: 863
	[DataContract]
	public class SourceMailboxPickerFilter : RecipientPickerVersionFilter
	{
		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000919F0 File Offset: 0x0008FBF0
		protected override RecipientTypeDetails[] RecipientTypeDetailsParam
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.UserMailbox,
					RecipientTypeDetails.LinkedMailbox,
					RecipientTypeDetails.TeamMailbox,
					RecipientTypeDetails.SharedMailbox,
					RecipientTypeDetails.LegacyMailbox,
					RecipientTypeDetails.RoomMailbox,
					RecipientTypeDetails.EquipmentMailbox,
					(RecipientTypeDetails)((ulong)int.MinValue),
					RecipientTypeDetails.RemoteRoomMailbox,
					RecipientTypeDetails.RemoteEquipmentMailbox,
					RecipientTypeDetails.RemoteTeamMailbox,
					RecipientTypeDetails.RemoteSharedMailbox
				};
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x00091A70 File Offset: 0x0008FC70
		protected override RecipientTypeDetails[] RecipientTypeDetailsWithoutVersionRestriction
		{
			get
			{
				return new RecipientTypeDetails[]
				{
					RecipientTypeDetails.MailUniversalDistributionGroup,
					RecipientTypeDetails.MailUniversalSecurityGroup,
					RecipientTypeDetails.MailNonUniversalGroup,
					RecipientTypeDetails.DynamicDistributionGroup
				};
			}
		}
	}
}
