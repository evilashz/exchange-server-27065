using System;
using System.IdentityModel.Tokens;
using System.Web;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200010E RID: 270
	internal class OAuthCookieExtensionAuthenticationHandler : IOAuthExtensionAuthenticationHandler
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x000393DC File Offset: 0x000375DC
		public bool TryHandleRequestPreAuthentication(OAuthExtensionContext context, out bool isAuthenticationNeeded)
		{
			isAuthenticationNeeded = true;
			return false;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000393E4 File Offset: 0x000375E4
		public bool TryGetBearerToken(OAuthExtensionContext context, out string token)
		{
			bool result = false;
			token = null;
			HttpContext httpContext = context.HttpContext;
			V1CallbackTokenCookie v1CallbackTokenCookie;
			if (V1CallbackTokenCookie.TryGetCookieFromHttpRequest(httpContext.Request, out v1CallbackTokenCookie))
			{
				token = v1CallbackTokenCookie.RawCookieValue;
				result = true;
			}
			return result;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00039418 File Offset: 0x00037618
		public bool TryHandleRequestPostAuthentication(OAuthExtensionContext context)
		{
			HttpContext httpContext = context.HttpContext;
			if (httpContext.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) && httpContext.Request.QueryString["wa"] == "wsignin1.0")
			{
				OAuthTokenHandler tokenHandler = context.TokenHandler;
				JwtSecurityToken token = tokenHandler.Token;
				LocalTokenIssuer localTokenIssuer = new LocalTokenIssuer(tokenHandler.GetOAuthIdentity().OrganizationId);
				TokenResult selfIssuedV1CallbackToken = localTokenIssuer.GetSelfIssuedV1CallbackToken(httpContext.Request.Url, token);
				V1CallbackTokenCookie.AddCookieToResponse(httpContext, selfIssuedV1CallbackToken);
			}
			return false;
		}
	}
}
