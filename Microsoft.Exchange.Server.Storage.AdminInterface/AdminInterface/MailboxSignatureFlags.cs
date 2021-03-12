using System;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200005F RID: 95
	[Flags]
	internal enum MailboxSignatureFlags : uint
	{
		// Token: 0x040001DD RID: 477
		None = 0U,
		// Token: 0x040001DE RID: 478
		GetLegacy = 1U,
		// Token: 0x040001DF RID: 479
		GetMailboxSignature = 2U,
		// Token: 0x040001E0 RID: 480
		GetNamedPropertyMapping = 4U,
		// Token: 0x040001E1 RID: 481
		GetReplidGuidMapping = 8U,
		// Token: 0x040001E2 RID: 482
		GetMappingMetadata = 16U,
		// Token: 0x040001E3 RID: 483
		GetMailboxShape = 32U,
		// Token: 0x040001E4 RID: 484
		AcceptableFlagsMask = 63U
	}
}
