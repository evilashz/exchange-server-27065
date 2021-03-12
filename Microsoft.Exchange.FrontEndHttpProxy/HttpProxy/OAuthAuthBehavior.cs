using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000024 RID: 36
	internal class OAuthAuthBehavior : DefaultAuthBehavior
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00005C00 File Offset: 0x00003E00
		public OAuthAuthBehavior(HttpContext httpContext, int serverVersion) : base(httpContext, serverVersion)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005C0A File Offset: 0x00003E0A
		public override bool ShouldDoFullAuthOnUnresolvedAnchorMailbox
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005C0D File Offset: 0x00003E0D
		public override bool ShouldCopyAuthenticationHeaderToClientResponse
		{
			get
			{
				return base.AuthState == AuthState.BackEndFullAuth;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005C18 File Offset: 0x00003E18
		protected override string VersionSupportsBackEndFullAuthString
		{
			get
			{
				return HttpProxySettings.EnableOAuthBEAuthVersion.Value;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005C24 File Offset: 0x00003E24
		public static bool IsOAuth(HttpContext httpContext)
		{
			IPrincipal user = httpContext.User;
			return user != null && user.Identity != null && user.Identity is GenericIdentity && (string.Equals(user.Identity.AuthenticationType, Constants.BearerAuthenticationType, StringComparison.OrdinalIgnoreCase) || string.Equals(user.Identity.AuthenticationType, Constants.BearerPreAuthenticationType, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005C84 File Offset: 0x00003E84
		public override AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext)
		{
			HttpContext httpContext = requestContext.HttpContext;
			IPrincipal user = httpContext.User;
			IIdentity identity = httpContext.User.Identity;
			OAuthPreAuthIdentity oauthPreAuthIdentity = identity as OAuthPreAuthIdentity;
			if (oauthPreAuthIdentity == null)
			{
				return null;
			}
			string text = httpContext.Request.Headers[Constants.ExternalDirectoryObjectIdHeaderName];
			if (!string.IsNullOrEmpty(text))
			{
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-ExternalDirectoryObjectId-Header");
				return new ExternalDirectoryObjectIdAnchorMailbox(text, oauthPreAuthIdentity.OrganizationId, requestContext);
			}
			switch (oauthPreAuthIdentity.PreAuthType)
			{
			case OAuthPreAuthType.OrganizationOnly:
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-AppOrganization");
				return new OrganizationAnchorMailbox(oauthPreAuthIdentity.OrganizationId, requestContext);
			case OAuthPreAuthType.Smtp:
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-Smtp");
				return new SmtpAnchorMailbox(oauthPreAuthIdentity.LookupValue, requestContext);
			case OAuthPreAuthType.WindowsLiveID:
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-LiveID");
				return new LiveIdMemberNameAnchorMailbox(oauthPreAuthIdentity.LookupValue, null, requestContext);
			case OAuthPreAuthType.ExternalDirectoryObjectId:
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-ExternalDirectoryObjectId");
				return new ExternalDirectoryObjectIdAnchorMailbox(oauthPreAuthIdentity.LookupValue, oauthPreAuthIdentity.OrganizationId, requestContext);
			case OAuthPreAuthType.Puid:
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "PreAuth-Puid");
				return new PuidAnchorMailbox(oauthPreAuthIdentity.LookupValue, "fake@outlook.com", requestContext);
			default:
				throw new InvalidOperationException("unknown preauth type");
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005DEC File Offset: 0x00003FEC
		public override bool IsFullyAuthenticated()
		{
			OAuthIdentity oauthIdentity = base.HttpContext.User.Identity as OAuthIdentity;
			return oauthIdentity != null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005E16 File Offset: 0x00004016
		public override void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback)
		{
			OAuthHttpModule.ContinueOnAuthenticate(app, callback);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005E1F File Offset: 0x0000401F
		public override void SetFailureStatus()
		{
		}
	}
}
