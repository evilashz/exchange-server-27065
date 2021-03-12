using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000074 RID: 116
	[Flags]
	public enum ExtendedEventFlags : long
	{
		// Token: 0x04000453 RID: 1107
		None = 0L,
		// Token: 0x04000454 RID: 1108
		NoReminderPropertyModified = 1L,
		// Token: 0x04000455 RID: 1109
		NoCIPropertyModified = 2L,
		// Token: 0x04000456 RID: 1110
		RetentionTagModified = 4L,
		// Token: 0x04000457 RID: 1111
		RetentionPropertiesModified = 8L,
		// Token: 0x04000458 RID: 1112
		MoveDestination = 16L,
		// Token: 0x04000459 RID: 1113
		AppointmentTimeNotModified = 64L,
		// Token: 0x0400045A RID: 1114
		AppointmentFreeBusyNotModified = 128L,
		// Token: 0x0400045B RID: 1115
		PublicFolderMailbox = 512L,
		// Token: 0x0400045C RID: 1116
		FolderPermissionChanged = 2048L,
		// Token: 0x0400045D RID: 1117
		NonIPMFolder = 1073741824L,
		// Token: 0x0400045E RID: 1118
		IPMFolder = 2147483648L,
		// Token: 0x0400045F RID: 1119
		NeedGroupExpansion = 4294967296L,
		// Token: 0x04000460 RID: 1120
		InferenceProcessingNeeded = 8589934592L,
		// Token: 0x04000461 RID: 1121
		ModernRemindersChanged = 17179869184L,
		// Token: 0x04000462 RID: 1122
		FolderIsNotPartOfContentIndexing = 34359738368L,
		// Token: 0x04000463 RID: 1123
		TimerEventFired = 68719476736L,
		// Token: 0x04000464 RID: 1124
		InternalAccessFolder = 137438953472L
	}
}
