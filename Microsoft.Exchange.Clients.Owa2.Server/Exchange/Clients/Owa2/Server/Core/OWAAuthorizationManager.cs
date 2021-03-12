using System;
using System.ServiceModel;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001ED RID: 493
	public class OWAAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x0600116E RID: 4462 RVA: 0x00042E34 File Offset: 0x00041034
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			HttpContext httpContext = HttpContext.Current;
			OwaServerLogger.LogWcfLatency(httpContext);
			return httpContext.Request.IsAuthenticated && base.CheckAccessCore(operationContext);
		}
	}
}
