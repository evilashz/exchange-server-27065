using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000030 RID: 48
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Constants
	{
		// Token: 0x04000073 RID: 115
		public const string HttpProxyInfoHeaderName = "Via";

		// Token: 0x04000074 RID: 116
		public const string RpcProxyPath = "rpc/rpcproxy.dll";

		// Token: 0x04000075 RID: 117
		public const int EmsmdbEndpoint = 6001;

		// Token: 0x04000076 RID: 118
		public const int RfriEndpoint = 6002;

		// Token: 0x04000077 RID: 119
		public const int NspiEndpoint = 6004;

		// Token: 0x04000078 RID: 120
		public const int ConsolidatedEndpoint = 6001;

		// Token: 0x04000079 RID: 121
		public const int GuidSize = 16;

		// Token: 0x0400007A RID: 122
		public static readonly string DiagInfoHeaderName = WellKnownHeader.E14DiagInfo;

		// Token: 0x0400007B RID: 123
		public static readonly string ClientAccessServerHeaderName = WellKnownHeader.XFEServer;

		// Token: 0x0400007C RID: 124
		public static readonly string BackendServerHeaderName = WellKnownHeader.XCalculatedBETarget;

		// Token: 0x0400007D RID: 125
		public static readonly TimeSpan DefaultRpcTimeout = TimeSpan.FromSeconds(58.0);
	}
}
