using System;
using System.Security.Permissions;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000126 RID: 294
	public class Global : HttpApplication
	{
		// Token: 0x060009AB RID: 2475 RVA: 0x000450C7 File Offset: 0x000432C7
		public Global()
		{
			if (!string.IsNullOrEmpty(HttpRuntime.AppDomainAppId) && HttpRuntime.AppDomainAppId.EndsWith("calendar", StringComparison.CurrentCultureIgnoreCase))
			{
				this.owaApplication = new CalendarVDirApplication();
				return;
			}
			this.owaApplication = new OwaApplication();
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00045104 File Offset: 0x00043304
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		public override void Init()
		{
			base.Init();
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0004510C File Offset: 0x0004330C
		internal void ExecuteApplicationStart(object sender, EventArgs e)
		{
			this.owaApplication.ExecuteApplicationStart(sender, e);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0004511B File Offset: 0x0004331B
		internal void ExecuteApplicationEnd(object sender, EventArgs e)
		{
			this.owaApplication.ExecuteApplicationEnd(sender, e);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004512A File Offset: 0x0004332A
		private void Application_Start(object sender, EventArgs e)
		{
			this.ExecuteApplicationStart(sender, e);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00045134 File Offset: 0x00043334
		private void Application_End(object sender, EventArgs e)
		{
			this.ExecuteApplicationEnd(sender, e);
		}

		// Token: 0x04000730 RID: 1840
		private OwaApplicationBase owaApplication;
	}
}
