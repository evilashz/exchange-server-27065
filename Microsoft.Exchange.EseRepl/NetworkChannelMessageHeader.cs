using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200002F RID: 47
	internal struct NetworkChannelMessageHeader
	{
		// Token: 0x040000D4 RID: 212
		public NetworkChannelMessage.MessageType MessageType;

		// Token: 0x040000D5 RID: 213
		public int MessageLength;

		// Token: 0x040000D6 RID: 214
		public DateTime MessageUtc;
	}
}
