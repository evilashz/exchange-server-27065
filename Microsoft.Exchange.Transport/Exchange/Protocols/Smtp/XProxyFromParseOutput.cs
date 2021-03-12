using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000491 RID: 1169
	internal struct XProxyFromParseOutput
	{
		// Token: 0x04001B18 RID: 6936
		public ProxyParseCommonOutput ProxyParseCommonOutput;

		// Token: 0x04001B19 RID: 6937
		public uint SequenceNumber;

		// Token: 0x04001B1A RID: 6938
		public uint? PermissionsInt;

		// Token: 0x04001B1B RID: 6939
		public AuthenticationSource? AuthSource;
	}
}
