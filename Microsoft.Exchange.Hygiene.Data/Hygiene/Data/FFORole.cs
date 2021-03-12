using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000074 RID: 116
	[Flags]
	internal enum FFORole
	{
		// Token: 0x040002D4 RID: 724
		None = 0,
		// Token: 0x040002D5 RID: 725
		HubTransport = 1,
		// Token: 0x040002D6 RID: 726
		FrontendTransport = 2,
		// Token: 0x040002D7 RID: 727
		Background = 4,
		// Token: 0x040002D8 RID: 728
		Database = 8,
		// Token: 0x040002D9 RID: 729
		WebService = 16,
		// Token: 0x040002DA RID: 730
		DomainNameServer = 32
	}
}
