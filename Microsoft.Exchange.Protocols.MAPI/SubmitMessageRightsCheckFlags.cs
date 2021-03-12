using System;

namespace Microsoft.Exchange.Protocols.MAPI
{
	// Token: 0x0200006A RID: 106
	[Flags]
	public enum SubmitMessageRightsCheckFlags
	{
		// Token: 0x040001ED RID: 493
		None = 0,
		// Token: 0x040001EE RID: 494
		SendAsRights = 1,
		// Token: 0x040001EF RID: 495
		SOBORights = 2,
		// Token: 0x040001F0 RID: 496
		SendingAsDL = 4
	}
}
