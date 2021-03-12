using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Clients.Security;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000023 RID: 35
	internal class LiveIdCookieAuthBehavior : DefaultAuthBehavior
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00005AA6 File Offset: 0x00003CA6
		public LiveIdCookieAuthBehavior(HttpContext httpContext, int serverVersion) : base(httpContext, serverVersion)
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public override bool ShouldCopyAuthenticationHeaderToClientResponse
		{
			get
			{
				return base.AuthState == AuthState.BackEndFullAuth;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005ABB File Offset: 0x00003CBB
		protected override string VersionSupportsBackEndFullAuthString
		{
			get
			{
				return HttpProxySettings.EnableLiveIdCookieBEAuthVersion.Value;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public static bool IsLiveIdCookieAuth(HttpContext httpContext)
		{
			IPrincipal user = httpContext.User;
			return user != null && user.Identity != null && user.Identity is GenericIdentity && string.Equals(user.Identity.AuthenticationType, "OrgId", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005B0C File Offset: 0x00003D0C
		public override bool IsFullyAuthenticated()
		{
			return base.HttpContext.Items["Item-CommonAccessToken"] is CommonAccessToken || (base.HttpContext.Items["LiveIdSkippedAuthForAnonResource"] != null && (bool)base.HttpContext.Items["LiveIdSkippedAuthForAnonResource"]);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B6C File Offset: 0x00003D6C
		public override AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext)
		{
			HttpCookie httpCookie = requestContext.HttpContext.Request.Cookies["DefaultAnchorMailbox"];
			AnchorMailbox result;
			if (httpCookie != null && !string.IsNullOrWhiteSpace(httpCookie.Value))
			{
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "DefaultAnchorMailboxCookie");
				result = new LiveIdMemberNameAnchorMailbox(httpCookie.Value, null, requestContext);
			}
			else
			{
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "UnauthenticatedAnonymous");
				result = new AnonymousAnchorMailbox(requestContext);
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005BEA File Offset: 0x00003DEA
		public override void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback)
		{
			LiveIdAuthenticationModule.ContinueOnAuthenticate(app.Context);
			callback(null);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005BFE File Offset: 0x00003DFE
		public override void SetFailureStatus()
		{
		}
	}
}
