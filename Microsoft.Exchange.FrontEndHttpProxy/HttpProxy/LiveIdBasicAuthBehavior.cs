using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000022 RID: 34
	internal class LiveIdBasicAuthBehavior : DefaultAuthBehavior
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00005945 File Offset: 0x00003B45
		public LiveIdBasicAuthBehavior(HttpContext httpContext, int serverVersion) : base(httpContext, serverVersion)
		{
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000594F File Offset: 0x00003B4F
		public override bool ShouldDoFullAuthOnUnresolvedAnchorMailbox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005952 File Offset: 0x00003B52
		protected override string VersionSupportsBackEndFullAuthString
		{
			get
			{
				return HttpProxySettings.EnableLiveIdBasicBEAuthVersion.Value;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005960 File Offset: 0x00003B60
		public static bool IsLiveIdBasicAuth(HttpContext httpContext)
		{
			IPrincipal user = httpContext.User;
			return user != null && user.Identity != null && string.Equals(user.Identity.AuthenticationType, "LiveIdBasic", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005998 File Offset: 0x00003B98
		public override AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext)
		{
			string text = base.HttpContext.Items[Constants.WLIDMemberName] as string;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string organizationContext = base.HttpContext.Items[Constants.WLIDOrganizationContextHeaderName] as string;
			requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "LiveIdBasic-LiveIdMemberName");
			return new LiveIdMemberNameAnchorMailbox(text, organizationContext, requestContext);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005A04 File Offset: 0x00003C04
		public override string GetExecutingUserOrganization()
		{
			string text = base.HttpContext.Items[Constants.WLIDMemberName] as string;
			if (!string.IsNullOrEmpty(text))
			{
				SmtpAddress smtpAddress = new SmtpAddress(text);
				return smtpAddress.Domain;
			}
			return base.GetExecutingUserOrganization();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005A4A File Offset: 0x00003C4A
		public override bool IsFullyAuthenticated()
		{
			return base.HttpContext.Items["Item-CommonAccessToken"] is CommonAccessToken;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005A6C File Offset: 0x00003C6C
		public override void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback)
		{
			LiveIdBasicAuthModule.ContinueOnAuthenticate(app, null, callback, null);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005A78 File Offset: 0x00003C78
		public override void SetFailureStatus()
		{
			if (base.HttpContext.Response.StatusCode == 200)
			{
				base.HttpContext.Response.StatusCode = 401;
			}
		}
	}
}
