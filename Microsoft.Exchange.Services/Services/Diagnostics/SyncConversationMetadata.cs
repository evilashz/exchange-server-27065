using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000058 RID: 88
	internal enum SyncConversationMetadata
	{
		// Token: 0x040004E0 RID: 1248
		[DisplayName("SCo", "TotalT")]
		TotalTime,
		// Token: 0x040004E1 RID: 1249
		[DisplayName("SCo", "IcsT")]
		IcsTime,
		// Token: 0x040004E2 RID: 1250
		[DisplayName("SCo", "QueryT")]
		QueryTime,
		// Token: 0x040004E3 RID: 1251
		[DisplayName("SCo", "DraftT")]
		DraftTime,
		// Token: 0x040004E4 RID: 1252
		[DisplayName("SCo", "ReadFlagT")]
		ReadFlagTime,
		// Token: 0x040004E5 RID: 1253
		[DisplayName("SCo", "IcsBindT")]
		IcsBindTime,
		// Token: 0x040004E6 RID: 1254
		[DisplayName("SCo", "CatchUpT")]
		CatchUpTime,
		// Token: 0x040004E7 RID: 1255
		[DisplayName("SCo", "ChangesT")]
		ChangesTime,
		// Token: 0x040004E8 RID: 1256
		[DisplayName("SCo", "ChangesCallC")]
		ChangesCallCount,
		// Token: 0x040004E9 RID: 1257
		[DisplayName("SCo", "FetchT")]
		FetchTime,
		// Token: 0x040004EA RID: 1258
		[DisplayName("SCo", "FetchC")]
		FetchCount,
		// Token: 0x040004EB RID: 1259
		[DisplayName("SCo", "FetchQT")]
		FetchQueryTime,
		// Token: 0x040004EC RID: 1260
		[DisplayName("SCo", "FetchUnC")]
		FetchUnneededCount,
		// Token: 0x040004ED RID: 1261
		[DisplayName("SCo", "LeftOverC")]
		LeftOverCount,
		// Token: 0x040004EE RID: 1262
		[DisplayName("SCo", "LeftOverQT")]
		LeftOverQueryTime,
		// Token: 0x040004EF RID: 1263
		[DisplayName("SCo", "QueryBindT")]
		QueryBindTime,
		// Token: 0x040004F0 RID: 1264
		[DisplayName("SCo", "FC")]
		FolderCount,
		// Token: 0x040004F1 RID: 1265
		[DisplayName("SCo", "SSS")]
		SyncStateSize,
		// Token: 0x040004F2 RID: 1266
		[DisplayName("SCo", "SSH")]
		SyncStateHash,
		// Token: 0x040004F3 RID: 1267
		[DisplayName("SCo", "CC")]
		ConversationCount,
		// Token: 0x040004F4 RID: 1268
		[DisplayName("SCo", "DCC")]
		DeletedConversationCount,
		// Token: 0x040004F5 RID: 1269
		[DisplayName("SCo", "Last")]
		IncludesLastItemInRange,
		// Token: 0x040004F6 RID: 1270
		[DisplayName("SCo", "XcptId")]
		ExceptionConversationId
	}
}
