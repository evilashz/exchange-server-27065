using System;
using System.Web.Routing;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000DFA RID: 3578
	internal static class HandlerInstaller
	{
		// Token: 0x06005C93 RID: 23699 RVA: 0x001209E8 File Offset: 0x0011EBE8
		public static void Initialize()
		{
			RouteTable.Routes.Add(new Route("Odata/{*pathInfo}", new RouteHanlder()));
		}
	}
}
