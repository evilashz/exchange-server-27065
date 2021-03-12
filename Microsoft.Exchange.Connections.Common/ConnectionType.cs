using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000006 RID: 6
	[Flags]
	[Serializable]
	public enum ConnectionType
	{
		// Token: 0x0400000A RID: 10
		Unknown = 0,
		// Token: 0x0400000B RID: 11
		Imap = 1,
		// Token: 0x0400000C RID: 12
		Pop = 2,
		// Token: 0x0400000D RID: 13
		DeltaSyncMail = 4,
		// Token: 0x0400000E RID: 14
		Facebook = 16,
		// Token: 0x0400000F RID: 15
		LinkedIn = 32,
		// Token: 0x04000010 RID: 16
		AllEMail = 7,
		// Token: 0x04000011 RID: 17
		AllPeople = 48,
		// Token: 0x04000012 RID: 18
		AllThatSupportSendAs = 7,
		// Token: 0x04000013 RID: 19
		AllThatSupportPolicyInducedDeletion = 48,
		// Token: 0x04000014 RID: 20
		All = 255
	}
}
