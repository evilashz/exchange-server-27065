using System;
using System.Net;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000488 RID: 1160
	internal struct ProxyParseCommonOutput
	{
		// Token: 0x04001AF4 RID: 6900
		public string SessionId;

		// Token: 0x04001AF5 RID: 6901
		public IPAddress ClientIp;

		// Token: 0x04001AF6 RID: 6902
		public int ClientPort;

		// Token: 0x04001AF7 RID: 6903
		public string ClientHelloDomain;
	}
}
