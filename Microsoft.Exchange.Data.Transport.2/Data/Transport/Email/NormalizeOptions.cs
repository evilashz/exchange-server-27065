using System;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000D3 RID: 211
	[Flags]
	internal enum NormalizeOptions
	{
		// Token: 0x040002D6 RID: 726
		NormalizeMimeStructure = 1,
		// Token: 0x040002D7 RID: 727
		NormalizeMessageId = 2,
		// Token: 0x040002D8 RID: 728
		NormalizeCte = 4,
		// Token: 0x040002D9 RID: 729
		MergeAddressHeaders = 8,
		// Token: 0x040002DA RID: 730
		RemoveDuplicateHeaders = 16,
		// Token: 0x040002DB RID: 731
		NormalizeMime = 65535,
		// Token: 0x040002DC RID: 732
		DropTnefRecipientTable = 65536,
		// Token: 0x040002DD RID: 733
		DropTnefSenderProperties = 131072,
		// Token: 0x040002DE RID: 734
		NormalizeTnef = 2147418112,
		// Token: 0x040002DF RID: 735
		All = 2147483647
	}
}
