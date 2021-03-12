using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000018 RID: 24
	public enum TaskId : uint
	{
		// Token: 0x04000027 RID: 39
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		None,
		// Token: 0x04000028 RID: 40
		[MapToManagement("SearchFolder", false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		SearchBacklinks,
		// Token: 0x04000029 RID: 41
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		FolderView,
		// Token: 0x0400002A RID: 42
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		AggregateCounts,
		// Token: 0x0400002B RID: 43
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		ProvisionedFolder,
		// Token: 0x0400002C RID: 44
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		ReplState,
		// Token: 0x0400002D RID: 45
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		MessagePtagCn,
		// Token: 0x0400002E RID: 46
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement("MessageId", false)]
		MidsetDeleted,
		// Token: 0x0400002F RID: 47
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		RuleMessageClass = 100U,
		// Token: 0x04000030 RID: 48
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		RestrictionFolder,
		// Token: 0x04000031 RID: 49
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		FolderACL,
		// Token: 0x04000032 RID: 50
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		UniqueMidIndex,
		// Token: 0x04000033 RID: 51
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		CorruptJunkRule,
		// Token: 0x04000034 RID: 52
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		MissingSpecialFolders,
		// Token: 0x04000035 RID: 53
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		[MapToManagement(null, false)]
		DropAllLazyIndexes,
		// Token: 0x04000036 RID: 54
		[RepairTaskAccessLevel(RepairTaskAccess.Support)]
		[MapToManagement(null, false)]
		ImapId,
		// Token: 0x04000037 RID: 55
		[RepairTaskAccessLevel(RepairTaskAccess.Test)]
		[MapToManagement(null, true)]
		InMemoryFolderHierarchy,
		// Token: 0x04000038 RID: 56
		[RepairTaskAccessLevel(RepairTaskAccess.Test)]
		[MapToManagement(null, true)]
		DiscardFolderHierarchyCache,
		// Token: 0x04000039 RID: 57
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		[MapToManagement(null, false)]
		LockedMoveTarget = 4096U,
		// Token: 0x0400003A RID: 58
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		ScheduledCheck = 8192U,
		// Token: 0x0400003B RID: 59
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		Extension1 = 32768U,
		// Token: 0x0400003C RID: 60
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		Extension2,
		// Token: 0x0400003D RID: 61
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		[MapToManagement(null, false)]
		Extension3,
		// Token: 0x0400003E RID: 62
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		Extension4,
		// Token: 0x0400003F RID: 63
		[MapToManagement(null, false)]
		[RepairTaskAccessLevel(RepairTaskAccess.Engineering)]
		Extension5
	}
}
