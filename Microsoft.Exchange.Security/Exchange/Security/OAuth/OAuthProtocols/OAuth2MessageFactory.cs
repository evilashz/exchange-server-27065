using System;
using System.IdentityModel.Tokens;
using System.IO;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000EB RID: 235
	internal static class OAuth2MessageFactory
	{
		// Token: 0x06000808 RID: 2056 RVA: 0x000363D4 File Offset: 0x000345D4
		public static OAuth2Message CreateFromEncodedResponse(StreamReader reader)
		{
			string text = reader.ReadToEnd();
			if (text.StartsWith("{\"error"))
			{
				return OAuth2ErrorResponse.CreateFromEncodedResponse(text);
			}
			return OAuth2AccessTokenResponse.Read(text);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00036404 File Offset: 0x00034604
		public static OAuth2AccessTokenRequest CreateAccessTokenRequestWithAssertion(JwtSecurityToken token, string resource)
		{
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			string assertion = jwtSecurityTokenHandler.WriteToken(token);
			return new OAuth2AccessTokenRequest
			{
				GrantType = "http://oauth.net/grant_type/jwt/1.0/bearer",
				Assertion = assertion,
				Resource = resource
			};
		}
	}
}
