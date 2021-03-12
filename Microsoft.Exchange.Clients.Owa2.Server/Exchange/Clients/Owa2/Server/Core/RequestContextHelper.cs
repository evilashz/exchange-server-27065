using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000248 RID: 584
	public class RequestContextHelper
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x0004E508 File Offset: 0x0004C708
		public static bool IsSuiteServiceProxyRequestType(HttpApplication httpApplication)
		{
			RequestContext requestContext = RequestContext.Get(httpApplication.Context);
			return OwaRequestType.SuiteServiceProxyPage == requestContext.RequestType;
		}
	}
}
