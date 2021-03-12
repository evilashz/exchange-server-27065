using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000075 RID: 117
	[Flags]
	internal enum EXORole
	{
		// Token: 0x040002DC RID: 732
		None = 0,
		// Token: 0x040002DD RID: 733
		HubTransportRole = 1,
		// Token: 0x040002DE RID: 734
		FrontendTransportRole = 2,
		// Token: 0x040002DF RID: 735
		ClientAccessRole = 4,
		// Token: 0x040002E0 RID: 736
		CafeRole = 8,
		// Token: 0x040002E1 RID: 737
		MailboxRole = 16,
		// Token: 0x040002E2 RID: 738
		UnifiedMessagingRole = 32
	}
}
