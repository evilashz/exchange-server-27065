using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FF RID: 511
	internal enum LastAction
	{
		// Token: 0x04000E8F RID: 3727
		Open,
		// Token: 0x04000E90 RID: 3728
		VotingOptionMin,
		// Token: 0x04000E91 RID: 3729
		VotingOptionMax = 31,
		// Token: 0x04000E92 RID: 3730
		First = 100,
		// Token: 0x04000E93 RID: 3731
		ReplyToSender = 102,
		// Token: 0x04000E94 RID: 3732
		ReplyToAll,
		// Token: 0x04000E95 RID: 3733
		Forward,
		// Token: 0x04000E96 RID: 3734
		ReplyToFolder = 108
	}
}
