using System;
using System.ServiceModel;
using System.Web;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DA4 RID: 3492
	public class UMAuthorizationManager : ServiceAuthorizationManager
	{
		// Token: 0x060058B0 RID: 22704 RVA: 0x00114414 File Offset: 0x00112614
		protected override bool CheckAccessCore(OperationContext operationContext)
		{
			HttpContext httpContext = HttpContext.Current;
			if (!httpContext.Request.IsAuthenticated)
			{
				EWSAuthorizationManager.Return401UnauthorizedResponse(operationContext, "Request was unauthenticated.");
				return false;
			}
			return base.CheckAccessCore(operationContext);
		}
	}
}
