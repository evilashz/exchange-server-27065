using System;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C81 RID: 3201
	public enum InternalMrsFlag
	{
		// Token: 0x04003D14 RID: 15636
		SkipPreFinalSyncDataProcessing = 1,
		// Token: 0x04003D15 RID: 15637
		SkipWordBreaking,
		// Token: 0x04003D16 RID: 15638
		SkipStorageProviderForSource,
		// Token: 0x04003D17 RID: 15639
		SkipMailboxReleaseCheck,
		// Token: 0x04003D18 RID: 15640
		SkipProvisioningCheck,
		// Token: 0x04003D19 RID: 15641
		CrossResourceForest,
		// Token: 0x04003D1A RID: 15642
		DoNotConvertSourceToMeu,
		// Token: 0x04003D1B RID: 15643
		ResolveServer,
		// Token: 0x04003D1C RID: 15644
		UseTcp,
		// Token: 0x04003D1D RID: 15645
		UseCertificateAuthentication,
		// Token: 0x04003D1E RID: 15646
		InvalidateContentIndexAnnotations
	}
}
