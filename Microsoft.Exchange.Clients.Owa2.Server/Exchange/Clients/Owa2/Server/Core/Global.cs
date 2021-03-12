using System;
using System.Security.Permissions;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200012B RID: 299
	public class Global : HttpApplication
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00022EC1 File Offset: 0x000210C1
		public Global()
		{
			this.application = BaseApplication.CreateInstance();
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00022ED4 File Offset: 0x000210D4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public override void Init()
		{
			base.Init();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00022EDC File Offset: 0x000210DC
		private void Application_Start(object sender, EventArgs e)
		{
			this.application.ExecuteApplicationStart(sender, e);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00022EEB File Offset: 0x000210EB
		private void Application_End(object sender, EventArgs e)
		{
			this.application.ExecuteApplicationEnd(sender, e);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00022EFA File Offset: 0x000210FA
		private void Application_BeginRequest(object sender, EventArgs e)
		{
			this.application.ExecuteApplicationBeginRequest(sender, e);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00022F09 File Offset: 0x00021109
		private void Application_EndRequest(object sender, EventArgs e)
		{
			this.application.ExecuteApplicationEndRequest(sender, e);
		}

		// Token: 0x040006A3 RID: 1699
		private readonly BaseApplication application;
	}
}
