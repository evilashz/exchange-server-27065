using System;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000021 RID: 33
	internal class ConsumerEasAuthBehavior : DefaultAuthBehavior
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00005833 File Offset: 0x00003A33
		public ConsumerEasAuthBehavior(HttpContext httpContext, int serverVersion) : base(httpContext, serverVersion)
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000583D File Offset: 0x00003A3D
		public override bool ShouldDoFullAuthOnUnresolvedAnchorMailbox
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005840 File Offset: 0x00003A40
		protected override string VersionSupportsBackEndFullAuthString
		{
			get
			{
				return HttpProxySettings.EnableRpsTokenBEAuthVersion.Value;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000584C File Offset: 0x00003A4C
		public static bool IsConsumerEasAuth(HttpContext httpContext)
		{
			IPrincipal user = httpContext.User;
			return user != null && user.Identity != null && string.Equals(user.Identity.AuthenticationType, "RpsTokenAuth", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005884 File Offset: 0x00003A84
		public override AnchorMailbox CreateAuthModuleSpecificAnchorMailbox(IRequestContext requestContext)
		{
			HttpCookie httpCookie = requestContext.HttpContext.Request.Cookies["DefaultAnchorMailbox"];
			AnchorMailbox result;
			if (httpCookie != null && !string.IsNullOrWhiteSpace(httpCookie.Value) && SmtpAddress.IsValidSmtpAddress(httpCookie.Value))
			{
				string organizationContext = base.HttpContext.Items[Constants.WLIDOrganizationContextHeaderName] as string;
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "DefaultAnchorMailboxCookie");
				result = new LiveIdMemberNameAnchorMailbox(httpCookie.Value, organizationContext, requestContext);
			}
			else
			{
				requestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "UnauthenticatedAnonymous");
				result = new AnonymousAnchorMailbox(requestContext);
			}
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000592A File Offset: 0x00003B2A
		public override bool IsFullyAuthenticated()
		{
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000592D File Offset: 0x00003B2D
		public override void ContinueOnAuthenticate(HttpApplication app, AsyncCallback callback)
		{
			throw new InvalidOperationException("Full authentication for RPS token on front end is not supported.");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005939 File Offset: 0x00003B39
		public override void SetFailureStatus()
		{
			throw new InvalidOperationException("Full authentication for RPS token on front end is not supported.");
		}
	}
}
