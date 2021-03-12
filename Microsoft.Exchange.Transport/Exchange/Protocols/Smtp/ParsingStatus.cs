using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000436 RID: 1078
	internal enum ParsingStatus
	{
		// Token: 0x0400181C RID: 6172
		ProtocolError,
		// Token: 0x0400181D RID: 6173
		Error,
		// Token: 0x0400181E RID: 6174
		MoreDataRequired,
		// Token: 0x0400181F RID: 6175
		Complete,
		// Token: 0x04001820 RID: 6176
		IgnorableProtocolError
	}
}
