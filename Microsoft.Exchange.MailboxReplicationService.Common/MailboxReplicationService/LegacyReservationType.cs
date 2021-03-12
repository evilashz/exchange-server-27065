using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200001D RID: 29
	internal enum LegacyReservationType
	{
		// Token: 0x040000FF RID: 255
		Read = 1,
		// Token: 0x04000100 RID: 256
		Write,
		// Token: 0x04000101 RID: 257
		Release,
		// Token: 0x04000102 RID: 258
		Expire,
		// Token: 0x04000103 RID: 259
		ExpiredRead,
		// Token: 0x04000104 RID: 260
		ExpiredWrite
	}
}
