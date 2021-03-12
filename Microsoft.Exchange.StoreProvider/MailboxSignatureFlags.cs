using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200003F RID: 63
	[Flags]
	internal enum MailboxSignatureFlags
	{
		// Token: 0x040003F8 RID: 1016
		None = 0,
		// Token: 0x040003F9 RID: 1017
		GetLegacy = 1,
		// Token: 0x040003FA RID: 1018
		GetMailboxSignature = 2,
		// Token: 0x040003FB RID: 1019
		GetNamedPropertyMapping = 4,
		// Token: 0x040003FC RID: 1020
		GetReplidGuidMapping = 8,
		// Token: 0x040003FD RID: 1021
		GetMappingMetadata = 16,
		// Token: 0x040003FE RID: 1022
		GetMailboxShape = 32
	}
}
