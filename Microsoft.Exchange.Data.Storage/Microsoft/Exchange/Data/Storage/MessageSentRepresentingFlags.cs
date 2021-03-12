using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C89 RID: 3209
	[Flags]
	internal enum MessageSentRepresentingFlags
	{
		// Token: 0x04004D7F RID: 19839
		None = 0,
		// Token: 0x04004D80 RID: 19840
		SendAs = 1,
		// Token: 0x04004D81 RID: 19841
		SendOnBehalfOf = 2
	}
}
