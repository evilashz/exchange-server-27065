using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000261 RID: 609
	internal static class RemoteRecipientTypeHelper
	{
		// Token: 0x04000E05 RID: 3589
		public const long RemoteMailboxTypeMask = 224L;

		// Token: 0x04000E06 RID: 3590
		public const long ProvisionOrDeprovisionTypeMask = 251L;

		// Token: 0x04000E07 RID: 3591
		public static readonly RemoteRecipientType[] AllowedProvisionOrDeprovisionType = new RemoteRecipientType[]
		{
			RemoteRecipientType.None,
			RemoteRecipientType.ProvisionMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.RoomMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.RoomMailbox | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.TeamMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.ProvisionArchive,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.ProvisionArchive | RemoteRecipientType.RoomMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.ProvisionArchive | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.ProvisionArchive | RemoteRecipientType.RoomMailbox | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.ProvisionArchive | RemoteRecipientType.TeamMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.DeprovisionArchive,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.DeprovisionArchive | RemoteRecipientType.RoomMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.DeprovisionArchive | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.DeprovisionArchive | RemoteRecipientType.RoomMailbox | RemoteRecipientType.EquipmentMailbox,
			RemoteRecipientType.ProvisionMailbox | RemoteRecipientType.DeprovisionArchive | RemoteRecipientType.TeamMailbox,
			RemoteRecipientType.DeprovisionMailbox,
			RemoteRecipientType.ProvisionArchive | RemoteRecipientType.DeprovisionMailbox,
			RemoteRecipientType.DeprovisionMailbox | RemoteRecipientType.DeprovisionArchive,
			RemoteRecipientType.ProvisionArchive,
			RemoteRecipientType.DeprovisionArchive
		};
	}
}
