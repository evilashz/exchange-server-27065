using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200040A RID: 1034
	internal enum ParserState
	{
		// Token: 0x04001768 RID: 5992
		INIT = -1,
		// Token: 0x04001769 RID: 5993
		NONE,
		// Token: 0x0400176A RID: 5994
		CR1,
		// Token: 0x0400176B RID: 5995
		LF1,
		// Token: 0x0400176C RID: 5996
		DOT,
		// Token: 0x0400176D RID: 5997
		CR2,
		// Token: 0x0400176E RID: 5998
		EOD,
		// Token: 0x0400176F RID: 5999
		EOHCR2
	}
}
