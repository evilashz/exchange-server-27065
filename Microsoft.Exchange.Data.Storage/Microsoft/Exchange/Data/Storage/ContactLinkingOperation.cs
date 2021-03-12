using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004C0 RID: 1216
	internal enum ContactLinkingOperation
	{
		// Token: 0x04001CA6 RID: 7334
		None,
		// Token: 0x04001CA7 RID: 7335
		AutoLinkViaEmailAddress,
		// Token: 0x04001CA8 RID: 7336
		AutoLinkViaIMAddress,
		// Token: 0x04001CA9 RID: 7337
		AutoLinkSkippedConflictPersonSets,
		// Token: 0x04001CAA RID: 7338
		AutoLinkSkippedInLinkRejectHistory,
		// Token: 0x04001CAB RID: 7339
		ManualLinking,
		// Token: 0x04001CAC RID: 7340
		Unlinking,
		// Token: 0x04001CAD RID: 7341
		RejectSuggestion,
		// Token: 0x04001CAE RID: 7342
		AutoLinkViaGalLinkId,
		// Token: 0x04001CAF RID: 7343
		AutoLinkViaEmailOrImAddressInDirectoryPerson,
		// Token: 0x04001CB0 RID: 7344
		AutoLinkSkippedDirectoryPersonAlreadyLinked,
		// Token: 0x04001CB1 RID: 7345
		AutoLinkSkippedDirectoryPersonUnlinked,
		// Token: 0x04001CB2 RID: 7346
		AutoLinkSkippedConflictingGALLinkState
	}
}
