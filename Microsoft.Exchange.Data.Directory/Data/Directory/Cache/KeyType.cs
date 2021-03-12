using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A2 RID: 162
	[Flags]
	internal enum KeyType
	{
		// Token: 0x040002EF RID: 751
		None = 0,
		// Token: 0x040002F0 RID: 752
		Name = 1,
		// Token: 0x040002F1 RID: 753
		DistinguishedName = 2,
		// Token: 0x040002F2 RID: 754
		Guid = 4,
		// Token: 0x040002F3 RID: 755
		ExternalDirectoryOrganizationId = 8,
		// Token: 0x040002F4 RID: 756
		DomainName = 16,
		// Token: 0x040002F5 RID: 757
		Sid = 32,
		// Token: 0x040002F6 RID: 758
		ExchangeGuid = 64,
		// Token: 0x040002F7 RID: 759
		AggregatedMailboxGuid = 128,
		// Token: 0x040002F8 RID: 760
		ArchiveGuid = 256,
		// Token: 0x040002F9 RID: 761
		LegacyExchangeDN = 512,
		// Token: 0x040002FA RID: 762
		EmailAddresses = 1024,
		// Token: 0x040002FB RID: 763
		MasterAccountSid = 2048,
		// Token: 0x040002FC RID: 764
		SidHistory = 4096,
		// Token: 0x040002FD RID: 765
		OrgCUDN = 8192,
		// Token: 0x040002FE RID: 766
		NetId = 16384
	}
}
