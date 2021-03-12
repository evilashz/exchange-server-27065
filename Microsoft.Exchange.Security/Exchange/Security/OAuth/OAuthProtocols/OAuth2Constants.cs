using System;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000EC RID: 236
	internal static class OAuth2Constants
	{
		// Token: 0x0400074C RID: 1868
		public const string AccessToken = "access_token";

		// Token: 0x0400074D RID: 1869
		public const string Assertion = "assertion";

		// Token: 0x0400074E RID: 1870
		public const string ClientId = "client_id";

		// Token: 0x0400074F RID: 1871
		public const string ClientSecret = "client_secret";

		// Token: 0x04000750 RID: 1872
		public const string Code = "code";

		// Token: 0x04000751 RID: 1873
		public const string ExpiresIn = "expires_in";

		// Token: 0x04000752 RID: 1874
		public const string GrantType = "grant_type";

		// Token: 0x04000753 RID: 1875
		public const string BearerAuthenticationType = "Bearer";

		// Token: 0x04000754 RID: 1876
		public const string Password = "password";

		// Token: 0x04000755 RID: 1877
		public const string RedirectUri = "redirect_uri";

		// Token: 0x04000756 RID: 1878
		public const string RefreshToken = "refresh_token";

		// Token: 0x04000757 RID: 1879
		public const string ResponseType = "response_type";

		// Token: 0x04000758 RID: 1880
		public const string Scope = "scope";

		// Token: 0x04000759 RID: 1881
		public const string State = "state";

		// Token: 0x0400075A RID: 1882
		public const string TokenType = "token_type";

		// Token: 0x0400075B RID: 1883
		public const string UserId = "user_id";

		// Token: 0x0400075C RID: 1884
		public const string Username = "username";

		// Token: 0x0400075D RID: 1885
		public const string AppContext = "AppContext";

		// Token: 0x0400075E RID: 1886
		public const string AuthorizationRequest = "AuthorizationRequest";

		// Token: 0x0400075F RID: 1887
		public const string AuthorizationResponseClaimType = "AuthorizationResponse";

		// Token: 0x04000760 RID: 1888
		public const string CacheLocation = "cache_location";

		// Token: 0x04000761 RID: 1889
		public const string CacheRealm = "cache_realm";

		// Token: 0x04000762 RID: 1890
		public const string IdentityProvider = "identity_provider";

		// Token: 0x04000763 RID: 1891
		public const string Realm = "realm";

		// Token: 0x04000764 RID: 1892
		public const string Resource = "resource";

		// Token: 0x020000ED RID: 237
		public static class GrantTypeConstants
		{
			// Token: 0x04000765 RID: 1893
			public const string Assertion = "assertion";

			// Token: 0x04000766 RID: 1894
			public const string AuthorizationCode = "authorization_code";

			// Token: 0x04000767 RID: 1895
			public const string ClientCredentials = "client_credentials";

			// Token: 0x04000768 RID: 1896
			public const string None = "none";

			// Token: 0x04000769 RID: 1897
			public const string Password = "password";

			// Token: 0x0400076A RID: 1898
			public const string RefreshToken = "refresh_token";
		}

		// Token: 0x020000EE RID: 238
		public static class ContentTypes
		{
			// Token: 0x0400076B RID: 1899
			public const string Json = "application/json";

			// Token: 0x0400076C RID: 1900
			public const string UrlEncoded = "application/x-www-form-urlencoded";
		}

		// Token: 0x020000EF RID: 239
		public static class ErrorConstants
		{
			// Token: 0x0400076D RID: 1901
			public const string Error = "error";

			// Token: 0x0400076E RID: 1902
			public const string ErrorDescription = "error_description";

			// Token: 0x0400076F RID: 1903
			public const string ErrorUri = "error_uri";
		}

		// Token: 0x020000F0 RID: 240
		public static class ErrorCodes
		{
			// Token: 0x04000770 RID: 1904
			public const string InvalidClient = "invalid_client";

			// Token: 0x04000771 RID: 1905
			public const string InvalidGrant = "invalid_grant";

			// Token: 0x04000772 RID: 1906
			public const string InvalidRequest = "invalid_request";

			// Token: 0x04000773 RID: 1907
			public const string InvalidScope = "invalid_scope";

			// Token: 0x04000774 RID: 1908
			public const string UnauthorizedClient = "unauthorized_client";

			// Token: 0x04000775 RID: 1909
			public const string UnsupportedGrantType = "unsupported_grant_type";

			// Token: 0x04000776 RID: 1910
			public const string TemporarilyUnavailable = "temporarily_unavailable";
		}
	}
}
