using System;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000927 RID: 2343
	public static class RpcProtocolSequence
	{
		// Token: 0x0600320A RID: 12810 RVA: 0x0007B430 File Offset: 0x00079630
		public static string ToDisplayString(string protocolSequence)
		{
			if (string.IsNullOrEmpty(protocolSequence))
			{
				return "RPC/UNDEFINED";
			}
			if (protocolSequence.StartsWith("ncacn_http", StringComparison.OrdinalIgnoreCase))
			{
				return "RPC/HTTP";
			}
			if (protocolSequence.StartsWith("ncacn_ip_tcp", StringComparison.OrdinalIgnoreCase))
			{
				return "RPC/TCP";
			}
			if (protocolSequence.StartsWith("ncalrpc", StringComparison.OrdinalIgnoreCase))
			{
				return "RPC/LOCAL";
			}
			if (protocolSequence.StartsWith("xrop", StringComparison.OrdinalIgnoreCase))
			{
				return "RPC/XROP";
			}
			return string.Format("RPC/{0}", protocolSequence);
		}

		// Token: 0x04002BB1 RID: 11185
		public const string Tcp = "ncacn_ip_tcp";

		// Token: 0x04002BB2 RID: 11186
		public const string Http = "ncacn_http";

		// Token: 0x04002BB3 RID: 11187
		public const string Local = "ncalrpc";

		// Token: 0x04002BB4 RID: 11188
		public const string Xrop = "xrop";
	}
}
