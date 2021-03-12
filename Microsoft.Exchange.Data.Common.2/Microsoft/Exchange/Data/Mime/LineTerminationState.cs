using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000039 RID: 57
	internal enum LineTerminationState : byte
	{
		// Token: 0x040001A7 RID: 423
		CRLF,
		// Token: 0x040001A8 RID: 424
		CR,
		// Token: 0x040001A9 RID: 425
		Other,
		// Token: 0x040001AA RID: 426
		Unknown,
		// Token: 0x040001AB RID: 427
		NotInteresting
	}
}
