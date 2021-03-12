using System;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000215 RID: 533
	internal sealed class ProxyLogonRequest : ProxyProtocolRequest
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x0006E1D4 File Offset: 0x0006C3D4
		internal void BeginSend(OwaContext owaContext, HttpRequest originalRequest, SerializedClientSecurityContext serializedContext, AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug(0L, "ProxyLogonRequest.BeginSend");
			string proxyRequestBody = serializedContext.Serialize();
			base.BeginSend(owaContext, originalRequest, OwaUrl.ProxyLogon.GetExplicitUrl(owaContext), proxyRequestBody, callback, extraData);
		}
	}
}
