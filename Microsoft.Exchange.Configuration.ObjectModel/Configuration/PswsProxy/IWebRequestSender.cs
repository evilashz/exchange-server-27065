using System;
using System.Collections.Specialized;
using System.Net;

namespace Microsoft.Exchange.Configuration.PswsProxy
{
	// Token: 0x020000CB RID: 203
	internal interface IWebRequestSender
	{
		// Token: 0x0600078F RID: 1935
		WebResponse SendRequest(string requestUri, NetworkCredential credential, string method, int timeout, bool allowAutoRedirect, string contentType, NameValueCollection headers, string requestContent);
	}
}
