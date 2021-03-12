using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B7 RID: 183
	[Flags]
	internal enum ItemSupport
	{
		// Token: 0x040002F2 RID: 754
		None = 0,
		// Token: 0x040002F3 RID: 755
		Email = 1,
		// Token: 0x040002F4 RID: 756
		Contacts = 2,
		// Token: 0x040002F5 RID: 757
		Generic = 32
	}
}
