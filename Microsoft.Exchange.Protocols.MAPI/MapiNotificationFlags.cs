using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x02000016 RID: 22
	[Flags]
	public enum MapiNotificationFlags : uint
	{
		// Token: 0x040000BE RID: 190
		TotalMessageCountChanged = 4096U,
		// Token: 0x040000BF RID: 191
		UnreadMessageCountChanged = 8192U,
		// Token: 0x040000C0 RID: 192
		SearchFolder = 16384U,
		// Token: 0x040000C1 RID: 193
		Message = 32768U
	}
}
