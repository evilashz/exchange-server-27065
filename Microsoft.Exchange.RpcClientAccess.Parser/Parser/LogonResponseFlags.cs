using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F6 RID: 758
	[Flags]
	internal enum LogonResponseFlags : byte
	{
		// Token: 0x0400098A RID: 2442
		None = 0,
		// Token: 0x0400098B RID: 2443
		IsMailboxLocalized = 1,
		// Token: 0x0400098C RID: 2444
		IsMailboxOwner = 2,
		// Token: 0x0400098D RID: 2445
		HasSendAsRights = 4,
		// Token: 0x0400098E RID: 2446
		IsOOF = 16
	}
}
