using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A2A RID: 2602
	[Flags]
	[Serializable]
	public enum MigrationFlags
	{
		// Token: 0x04003622 RID: 13858
		[LocDescription(ServerStrings.IDs.MigrationFlagsNone)]
		None = 0,
		// Token: 0x04003623 RID: 13859
		[LocDescription(ServerStrings.IDs.MigrationFlagsStart)]
		Start = 1,
		// Token: 0x04003624 RID: 13860
		[LocDescription(ServerStrings.IDs.MigrationFlagsStop)]
		Stop = 2,
		// Token: 0x04003625 RID: 13861
		[LocDescription(ServerStrings.IDs.MigrationFlagsRemove)]
		Remove = 4,
		// Token: 0x04003626 RID: 13862
		[LocDescription(ServerStrings.IDs.MigrationFlagsReport)]
		Report = 8
	}
}
