using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000453 RID: 1107
	internal class SmtpInBdatState
	{
		// Token: 0x04001A0F RID: 6671
		internal long TotalChunkSize;

		// Token: 0x04001A10 RID: 6672
		internal bool DiscardingMessage;

		// Token: 0x04001A11 RID: 6673
		internal bool SeenEoh;

		// Token: 0x04001A12 RID: 6674
		internal string MessageId;

		// Token: 0x04001A13 RID: 6675
		internal long OriginalMessageSize;

		// Token: 0x04001A14 RID: 6676
		internal long MessageSizeLimit;

		// Token: 0x04001A15 RID: 6677
		internal SmtpInBdatProxyParser ProxyParser;

		// Token: 0x04001A16 RID: 6678
		internal InboundBdatProxyLayer ProxyLayer;
	}
}
