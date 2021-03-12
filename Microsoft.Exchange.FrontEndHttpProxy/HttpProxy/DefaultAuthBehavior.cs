using System;
using System.Web;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000020 RID: 32
	internal class DefaultAuthBehavior : IAuthBehavior
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000056F4 File Offset: 0x000038F4
		protected DefaultAuthBehavior(HttpContext httpContext, int serverVersion)
		{
			this.HttpContext = httpContext;
			this.VersionSupportsBackEndFullAuth = Utilities.ConvertToServerVersion(this.VersionSupportsBackEndFullAuthString);
			this.SetState(serverVersion);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000DA RID: 218 RVA: 0x0000571B File Offset: 0x0000391B
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00005723 File Offset: 0x00003923
		public AuthState AuthState { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000572C File Offset: 0x0000392C
		public virtual bool ShouldDoFullAuthOnUnresolvedAnchorMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000572F File Offset: 0x0000392F
		public virtual bool ShouldCopyAuthenticationHeaderToClientResponse
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005732 File Offset: 0x00003932
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000573A File Offset: 0x0000393A
		private protected HttpContext HttpContext { protected get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005743 File Offset: 0x00003943
		protected virtual string VersionSupportsBackEndFullAuthString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000574A File Offset: 0x0000394A
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00005752 File Offset: 0x00003952
		private ServerVersion VersionSupportsBackEndFullAuth { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000575B File Offset: 0x0000395B
		private bool BackEndFullAuthEnabled
		{
			get
			{
				return this.VersionSupportsBackEndFullAuth != null;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005769 File Offset: 0x00003969
		public static IAuthBehavior CreateAuthBehavior(HttpContext httpContext)
		{
			return DefaultAuthBehavior.CreateAuthBehavior(httpContext, 0);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005774 File Offset: 0x00003974
		public static IAuthBehavior CreateAuthBehavior(HttpContext httpContext, int serverVersion)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (LiveIdBasicAuthBehavior.IsLiveIdBasicAuth(httpContext))
			{
				return new LiveIdBasicAuthBehavior(httpContext, serverVersion);
			}
			if (OAuthAuthBehavior.IsOAuth(httpContext))
			{
				return new OAuthAuthBehavior(httpContext, serverVersion);
			}
			if (LiveIdCookieAuthBehavior.IsLiveIdCookieAuth(httpContext))
			{
				return new LiveIdCookieAuthBehavior(httpContext, serverVersion);
			}
			if (ConsumerEasAuthBehavior.IsConsumerEasAuth(httpContext))
			{
				return new ConsumerEasAuthBehavior(httpContext, serverVersion);
			}
			return new DefaultAuthBehavior(httpContext, serverVersion);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000057D6 File Offset: 0x000039D6
		public void SetState(int serverVersion)
		{
			this.AuthState = AuthState.FrontEndFullAuth;
			if (this.BackEndFullAuthEnabled)
			{
				this.AuthState = ((serverVersion >= this.VersionSupportsBackEndFullAuth.ToInt()) ? AuthState.BackEndFullAuth : AuthState.FrontEndContinueAuth);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000057FF File Offset: 0x000039FF
		public void ResetState()
		{
			this.SetState(0);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005808 File Offset: 0x00003A08
		public virtual AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext)
		{
			return null;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000580B File Offset: 0x00003A0B
		public virtual string GetExecutingUserOrganization()
		{
			return string.Empty;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005812 File Offset: 0x00003A12
		public virtual bool IsFullyAuthenticated()
		{
			return true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00005815 File Offset: 0x00003A15
		public virtual void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000581C File Offset: 0x00003A1C
		public virtual void SetFailureStatus()
		{
			this.HttpContext.Response.StatusCode = 401;
		}
	}
}
