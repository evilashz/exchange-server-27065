using System;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000D9 RID: 217
	[Flags]
	internal enum RowType
	{
		// Token: 0x040003F2 RID: 1010
		Message = 1,
		// Token: 0x040003F3 RID: 1011
		Recipient = 2,
		// Token: 0x040003F4 RID: 1012
		All = 3
	}
}
