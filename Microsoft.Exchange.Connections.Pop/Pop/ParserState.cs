using System;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x0200001F RID: 31
	internal enum ParserState
	{
		// Token: 0x040000C4 RID: 196
		INIT = -1,
		// Token: 0x040000C5 RID: 197
		NONE,
		// Token: 0x040000C6 RID: 198
		CR1,
		// Token: 0x040000C7 RID: 199
		LF1,
		// Token: 0x040000C8 RID: 200
		DOT,
		// Token: 0x040000C9 RID: 201
		CR2,
		// Token: 0x040000CA RID: 202
		EOD,
		// Token: 0x040000CB RID: 203
		EOHCR2
	}
}
