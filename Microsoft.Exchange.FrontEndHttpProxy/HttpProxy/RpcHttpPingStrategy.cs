using System;
using System.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200002C RID: 44
	internal class RpcHttpPingStrategy : ProtocolPingStrategyBase
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000763C File Offset: 0x0000583C
		public override Uri BuildUrl(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			return new UriBuilder
			{
				Scheme = Uri.UriSchemeHttps,
				Host = fqdn,
				Path = "rpc/rpcproxy.dll"
			}.Uri;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007685 File Offset: 0x00005885
		protected override void PrepareRequest(HttpWebRequest request)
		{
			base.PrepareRequest(request);
			request.Method = "RPC_IN_DATA";
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000769C File Offset: 0x0000589C
		protected override bool IsWebExceptionExpected(WebException exception)
		{
			HttpWebResponse httpWebResponse = exception.Response as HttpWebResponse;
			return httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.Unauthorized;
		}
	}
}
