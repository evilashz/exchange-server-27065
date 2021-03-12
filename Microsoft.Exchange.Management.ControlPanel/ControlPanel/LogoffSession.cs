using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000396 RID: 918
	internal sealed class LogoffSession : IRbacSession, IPrincipal, IIdentity
	{
		// Token: 0x060030D0 RID: 12496 RVA: 0x00094C1D File Offset: 0x00092E1D
		public LogoffSession(RbacSettings rbacSettings, IIdentity identity)
		{
			this.rbacSettings = rbacSettings;
			this.identity = identity;
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x00094C33 File Offset: 0x00092E33
		IIdentity IPrincipal.Identity
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x00094C36 File Offset: 0x00092E36
		bool IPrincipal.IsInRole(string role)
		{
			return false;
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x00094C39 File Offset: 0x00092E39
		string IIdentity.AuthenticationType
		{
			get
			{
				return this.identity.AuthenticationType;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x00094C46 File Offset: 0x00092E46
		bool IIdentity.IsAuthenticated
		{
			get
			{
				return this.identity.IsAuthenticated;
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x00094C53 File Offset: 0x00092E53
		string IIdentity.Name
		{
			get
			{
				return this.identity.GetSafeName(true);
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x00094C61 File Offset: 0x00092E61
		public IIdentity OriginalIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x00094C6C File Offset: 0x00092E6C
		void IRbacSession.RequestReceived()
		{
			HttpContext httpContext = HttpContext.Current;
			if (!((IIdentity)this).IsAuthenticated)
			{
				httpContext.Response.Redirect(EcpUrl.EcpVDir, true);
				return;
			}
			this.rbacSettings.ExpireSession();
			string text = httpContext.Request.QueryString["url"];
			string str = string.IsNullOrEmpty(text) ? string.Empty : ("?url=" + HttpUtility.UrlEncode(text));
			httpContext.Response.Redirect(EcpUrl.OwaVDir + "logoff.owa" + str, true);
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x00094CF6 File Offset: 0x00092EF6
		void IRbacSession.RequestCompleted()
		{
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x00094CF8 File Offset: 0x00092EF8
		void IRbacSession.SetCurrentThreadPrincipal()
		{
			Thread.CurrentPrincipal = this;
		}

		// Token: 0x04002399 RID: 9113
		private const string OwaLogoffUrl = "logoff.owa";

		// Token: 0x0400239A RID: 9114
		public static readonly IRbacSession AnonymousSession = new LogoffSession(null, WindowsIdentity.GetAnonymous());

		// Token: 0x0400239B RID: 9115
		private RbacSettings rbacSettings;

		// Token: 0x0400239C RID: 9116
		private IIdentity identity;
	}
}
