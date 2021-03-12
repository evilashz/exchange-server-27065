using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000AF6 RID: 2806
	[Flags]
	internal enum PropertyBagSaveFlags
	{
		// Token: 0x040039E8 RID: 14824
		Default = 0,
		// Token: 0x040039E9 RID: 14825
		IgnoreMapiComputedErrors = 1,
		// Token: 0x040039EA RID: 14826
		IgnoreUnresolvedHeaders = 2,
		// Token: 0x040039EB RID: 14827
		SaveFolderPropertyBagConditional = 4,
		// Token: 0x040039EC RID: 14828
		IgnoreAccessDeniedErrors = 8,
		// Token: 0x040039ED RID: 14829
		DisableNewXHeaderMapping = 16,
		// Token: 0x040039EE RID: 14830
		NoChangeTracking = 32,
		// Token: 0x040039EF RID: 14831
		ForceNotificationPublish = 64,
		// Token: 0x040039F0 RID: 14832
		ResolveSenderProperties = 128
	}
}
