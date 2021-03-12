using System;
using System.Web;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000375 RID: 885
	internal struct AuthenticationSettings
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x000930B8 File Offset: 0x000912B8
		public AuthenticationSettings(HttpContext context)
		{
			if (!context.Request.IsAuthenticated || Utility.IsResourceRequest(context.Request.Path))
			{
				this.Session = (context.IsLogoffRequest() ? LogoffSession.AnonymousSession : AnonymousSession.Instance);
				return;
			}
			RbacSettings rbacSettings = new RbacSettings(context);
			if (context.IsLogoffRequest())
			{
				this.Session = new LogoffSession(rbacSettings, context.User.Identity);
				return;
			}
			this.Session = rbacSettings.Session;
		}

		// Token: 0x04002351 RID: 9041
		public readonly IRbacSession Session;
	}
}
