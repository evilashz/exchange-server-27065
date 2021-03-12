using System;
using System.Security.Principal;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000493 RID: 1171
	internal struct XProxyParseOutput
	{
		// Token: 0x04001B1D RID: 6941
		public ProxyParseCommonOutput ProxyParseCommonOutput;

		// Token: 0x04001B1E RID: 6942
		public SecurityIdentifier SecurityId;

		// Token: 0x04001B1F RID: 6943
		public SmtpAddress ClientProxyAddress;

		// Token: 0x04001B20 RID: 6944
		public byte[] RedactedBuffer;

		// Token: 0x04001B21 RID: 6945
		public int? CapabilitiesInt;
	}
}
