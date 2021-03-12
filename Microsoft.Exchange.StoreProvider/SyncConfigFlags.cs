using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000250 RID: 592
	[Flags]
	internal enum SyncConfigFlags
	{
		// Token: 0x04001058 RID: 4184
		None = 0,
		// Token: 0x04001059 RID: 4185
		Unicode = 1,
		// Token: 0x0400105A RID: 4186
		NoDeletions = 2,
		// Token: 0x0400105B RID: 4187
		NoSoftDeletions = 4,
		// Token: 0x0400105C RID: 4188
		ReadState = 8,
		// Token: 0x0400105D RID: 4189
		Associated = 16,
		// Token: 0x0400105E RID: 4190
		Normal = 32,
		// Token: 0x0400105F RID: 4191
		NoConflicts = 64,
		// Token: 0x04001060 RID: 4192
		OnlySpecifiedProps = 128,
		// Token: 0x04001061 RID: 4193
		NoForeignKeys = 256,
		// Token: 0x04001062 RID: 4194
		LimitedIMessage = 512,
		// Token: 0x04001063 RID: 4195
		Catchup = 1024,
		// Token: 0x04001064 RID: 4196
		Conversations = 2048,
		// Token: 0x04001065 RID: 4197
		MsgSelective = 4096,
		// Token: 0x04001066 RID: 4198
		BestBody = 8192,
		// Token: 0x04001067 RID: 4199
		IgnoreSpecifiedOnAssociated = 16384,
		// Token: 0x04001068 RID: 4200
		ProgressMode = 32768,
		// Token: 0x04001069 RID: 4201
		FXRecoverMode = 65536,
		// Token: 0x0400106A RID: 4202
		DeferConfig = 131072,
		// Token: 0x0400106B RID: 4203
		ForceUnicode = 262144,
		// Token: 0x0400106C RID: 4204
		NoChanges = 524288,
		// Token: 0x0400106D RID: 4205
		OrderByDeliveryTime = 1048576,
		// Token: 0x0400106E RID: 4206
		ReevaluateOnRestrictionChange = 2097152,
		// Token: 0x0400106F RID: 4207
		ManifestHierReturnDeletedEntryIds = 4194304,
		// Token: 0x04001070 RID: 4208
		UseCpId = 16777216,
		// Token: 0x04001071 RID: 4209
		SendPropsErrors = 33554432,
		// Token: 0x04001072 RID: 4210
		ManifestMode = 67108864,
		// Token: 0x04001073 RID: 4211
		CatchupFull = 134217728
	}
}
