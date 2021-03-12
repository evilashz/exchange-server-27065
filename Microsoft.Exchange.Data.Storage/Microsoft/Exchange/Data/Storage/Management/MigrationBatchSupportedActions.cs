using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A3D RID: 2621
	[Flags]
	[Serializable]
	public enum MigrationBatchSupportedActions
	{
		// Token: 0x04003674 RID: 13940
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionNone)]
		None = 0,
		// Token: 0x04003675 RID: 13941
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionStart)]
		Start = 1,
		// Token: 0x04003676 RID: 13942
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionStop)]
		Stop = 2,
		// Token: 0x04003677 RID: 13943
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionSet)]
		Set = 4,
		// Token: 0x04003678 RID: 13944
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionRemove)]
		Remove = 8,
		// Token: 0x04003679 RID: 13945
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionComplete)]
		Complete = 16,
		// Token: 0x0400367A RID: 13946
		[LocDescription(ServerStrings.IDs.MigrationBatchSupportedActionAppend)]
		Append = 32
	}
}
