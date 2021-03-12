using System;
using System.Web;
using System.Web.Routing;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000DFB RID: 3579
	internal class RouteHanlder : IRouteHandler
	{
		// Token: 0x06005C94 RID: 23700 RVA: 0x00120A03 File Offset: 0x0011EC03
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new HttpHandler();
		}
	}
}
