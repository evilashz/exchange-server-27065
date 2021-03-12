using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000072 RID: 114
	[Flags]
	public enum EventType
	{
		// Token: 0x0400042A RID: 1066
		CriticalError = 1,
		// Token: 0x0400042B RID: 1067
		NewMail = 2,
		// Token: 0x0400042C RID: 1068
		ObjectCreated = 4,
		// Token: 0x0400042D RID: 1069
		ObjectDeleted = 8,
		// Token: 0x0400042E RID: 1070
		ObjectModified = 16,
		// Token: 0x0400042F RID: 1071
		ObjectMoved = 32,
		// Token: 0x04000430 RID: 1072
		ObjectCopied = 64,
		// Token: 0x04000431 RID: 1073
		SearchComplete = 128,
		// Token: 0x04000432 RID: 1074
		TableModified = 256,
		// Token: 0x04000433 RID: 1075
		StatusObjectModified = 512,
		// Token: 0x04000434 RID: 1076
		Ics = 512,
		// Token: 0x04000435 RID: 1077
		MailSubmitted = 1024,
		// Token: 0x04000436 RID: 1078
		MailboxCreated = 2048,
		// Token: 0x04000437 RID: 1079
		MailboxDeleted = 4096,
		// Token: 0x04000438 RID: 1080
		MailboxDisconnected = 8192,
		// Token: 0x04000439 RID: 1081
		MailboxReconnected = 16384,
		// Token: 0x0400043A RID: 1082
		MailboxMoveStarted = 32768,
		// Token: 0x0400043B RID: 1083
		MailboxMoveSucceeded = 65536,
		// Token: 0x0400043C RID: 1084
		MailboxMoveFailed = 131072,
		// Token: 0x0400043D RID: 1085
		CategRowAdded = 262144,
		// Token: 0x0400043E RID: 1086
		CategRowModified = 524288,
		// Token: 0x0400043F RID: 1087
		CategRowDeleted = 1048576,
		// Token: 0x04000440 RID: 1088
		BeginLongOperation = 2097152,
		// Token: 0x04000441 RID: 1089
		EndLongOperation = 4194304,
		// Token: 0x04000442 RID: 1090
		DaclCreated = 8388608,
		// Token: 0x04000443 RID: 1091
		MessageUnlinked = 16777216,
		// Token: 0x04000444 RID: 1092
		MailboxModified = 33554432,
		// Token: 0x04000445 RID: 1093
		MessagesLinked = 67108864,
		// Token: 0x04000446 RID: 1094
		ReservedForMapi = 1073741824,
		// Token: 0x04000447 RID: 1095
		Extended = -2147483648
	}
}
