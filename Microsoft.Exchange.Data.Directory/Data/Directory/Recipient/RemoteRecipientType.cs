using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000260 RID: 608
	[Flags]
	public enum RemoteRecipientType : long
	{
		// Token: 0x04000DFB RID: 3579
		None = 0L,
		// Token: 0x04000DFC RID: 3580
		ProvisionMailbox = 1L,
		// Token: 0x04000DFD RID: 3581
		ProvisionArchive = 2L,
		// Token: 0x04000DFE RID: 3582
		Migrated = 4L,
		// Token: 0x04000DFF RID: 3583
		DeprovisionMailbox = 8L,
		// Token: 0x04000E00 RID: 3584
		DeprovisionArchive = 16L,
		// Token: 0x04000E01 RID: 3585
		RoomMailbox = 32L,
		// Token: 0x04000E02 RID: 3586
		EquipmentMailbox = 64L,
		// Token: 0x04000E03 RID: 3587
		SharedMailbox = 96L,
		// Token: 0x04000E04 RID: 3588
		TeamMailbox = 128L
	}
}
