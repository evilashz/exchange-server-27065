using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000204 RID: 516
	[Flags]
	public enum RequestJobInternalFlags
	{
		// Token: 0x04000AA5 RID: 2725
		None = 0,
		// Token: 0x04000AA6 RID: 2726
		RestartFromScratch = 1,
		// Token: 0x04000AA7 RID: 2727
		ForceOfflineMove = 2,
		// Token: 0x04000AA8 RID: 2728
		SkipFolderRules = 4,
		// Token: 0x04000AA9 RID: 2729
		SkipFolderACLs = 8,
		// Token: 0x04000AAA RID: 2730
		SkipFolderPromotedProperties = 16,
		// Token: 0x04000AAB RID: 2731
		SkipFolderViews = 32,
		// Token: 0x04000AAC RID: 2732
		SkipFolderRestrictions = 64,
		// Token: 0x04000AAD RID: 2733
		PreventCompletion = 128,
		// Token: 0x04000AAE RID: 2734
		SkipInitialConnectionValidation = 256,
		// Token: 0x04000AAF RID: 2735
		SkipContentVerification = 512,
		// Token: 0x04000AB0 RID: 2736
		BlockFinalization = 1024,
		// Token: 0x04000AB1 RID: 2737
		SkipStorageProviderForSource = 4096,
		// Token: 0x04000AB2 RID: 2738
		SkipPreFinalSyncDataProcessing = 8192,
		// Token: 0x04000AB3 RID: 2739
		FailOnFirstBadItem = 32768,
		// Token: 0x04000AB4 RID: 2740
		SkipMailboxReleaseCheck = 65536,
		// Token: 0x04000AB5 RID: 2741
		SkipKnownCorruptions = 131072,
		// Token: 0x04000AB6 RID: 2742
		IncrementallyUpdateGlobalCounterRanges = 262144,
		// Token: 0x04000AB7 RID: 2743
		ExecutedByTransportSync = 524288,
		// Token: 0x04000AB8 RID: 2744
		SkipProvisioningCheck = 1048576,
		// Token: 0x04000AB9 RID: 2745
		FailOnCorruptSyncState = 2097152,
		// Token: 0x04000ABA RID: 2746
		SkipConvertingSourceToMeu = 4194304,
		// Token: 0x04000ABB RID: 2747
		ResolveServer = 8388608,
		// Token: 0x04000ABC RID: 2748
		UseTcp = 16777216,
		// Token: 0x04000ABD RID: 2749
		CrossResourceForest = 67108864,
		// Token: 0x04000ABE RID: 2750
		SkipWordBreaking = 134217728,
		// Token: 0x04000ABF RID: 2751
		UseCertificateAuthentication = 268435456,
		// Token: 0x04000AC0 RID: 2752
		InvalidateContentIndexAnnotations = 536870912
	}
}
