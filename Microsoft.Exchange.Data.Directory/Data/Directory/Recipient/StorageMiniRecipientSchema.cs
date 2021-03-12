using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E0 RID: 480
	internal class StorageMiniRecipientSchema : MiniRecipientSchema
	{
		// Token: 0x04000B23 RID: 2851
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x04000B24 RID: 2852
		public static readonly ADPropertyDefinition ArchiveDomain = ADUserSchema.ArchiveDomain;

		// Token: 0x04000B25 RID: 2853
		public static readonly ADPropertyDefinition ArchiveStatus = ADUserSchema.ArchiveStatus;

		// Token: 0x04000B26 RID: 2854
		public static readonly ADPropertyDefinition ImmutableId = ADRecipientSchema.ImmutableId;

		// Token: 0x04000B27 RID: 2855
		public static readonly ADPropertyDefinition RawOnPremisesObjectId = ADRecipientSchema.RawOnPremisesObjectId;
	}
}
