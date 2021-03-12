using System;

namespace Microsoft.Exchange.Data.MailboxSignature
{
	// Token: 0x02000230 RID: 560
	[Flags]
	internal enum MailboxSignatureSectionType : short
	{
		// Token: 0x04000B5F RID: 2911
		None = 0,
		// Token: 0x04000B60 RID: 2912
		BasicInformation = 1,
		// Token: 0x04000B61 RID: 2913
		MappingMetadata = 2,
		// Token: 0x04000B62 RID: 2914
		NamedPropertyMapping = 4,
		// Token: 0x04000B63 RID: 2915
		ReplidGuidMapping = 8,
		// Token: 0x04000B64 RID: 2916
		TenantHint = 16,
		// Token: 0x04000B65 RID: 2917
		MailboxShape = 32,
		// Token: 0x04000B66 RID: 2918
		MailboxTypeVersion = 64,
		// Token: 0x04000B67 RID: 2919
		PartitionInformation = 128,
		// Token: 0x04000B68 RID: 2920
		UserInformation = 256
	}
}
