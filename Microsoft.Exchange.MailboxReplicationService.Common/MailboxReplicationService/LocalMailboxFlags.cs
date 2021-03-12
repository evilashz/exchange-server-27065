using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002B RID: 43
	[Flags]
	internal enum LocalMailboxFlags
	{
		// Token: 0x0400016E RID: 366
		None = 0,
		// Token: 0x0400016F RID: 367
		StripLargeRulesForDownlevelTargets = 1,
		// Token: 0x04000170 RID: 368
		UseHomeMDB = 2,
		// Token: 0x04000171 RID: 369
		PureMAPI = 4,
		// Token: 0x04000172 RID: 370
		CredentialIsNotAdmin = 8,
		// Token: 0x04000173 RID: 371
		UseNTLMAuth = 16,
		// Token: 0x04000174 RID: 372
		ConnectToMoMT = 32,
		// Token: 0x04000175 RID: 373
		LegacyPublicFolders = 64,
		// Token: 0x04000176 RID: 374
		Restore = 128,
		// Token: 0x04000177 RID: 375
		LocalMachineMapiOnly = 256,
		// Token: 0x04000178 RID: 376
		UseMapiProvider = 512,
		// Token: 0x04000179 RID: 377
		Move = 1024,
		// Token: 0x0400017A RID: 378
		PublicFolderMove = 2048,
		// Token: 0x0400017B RID: 379
		WordBreak = 4096,
		// Token: 0x0400017C RID: 380
		PstImport = 8192,
		// Token: 0x0400017D RID: 381
		EasSync = 16384,
		// Token: 0x0400017E RID: 382
		AggregatedMailbox = 32768,
		// Token: 0x0400017F RID: 383
		CreateNewPartition = 65536,
		// Token: 0x04000180 RID: 384
		InvalidateContentIndexAnnotations = 131072,
		// Token: 0x04000181 RID: 385
		FolderMove = 262144,
		// Token: 0x04000182 RID: 386
		OlcSync = 524288,
		// Token: 0x04000183 RID: 387
		PstExport = 1048576,
		// Token: 0x04000184 RID: 388
		ParallelPublicFolderMigration = 4194304
	}
}
