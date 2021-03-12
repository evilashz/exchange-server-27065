using System;

namespace Microsoft.Exchange.Security.OAuth.OAuthProtocols
{
	// Token: 0x020000E9 RID: 233
	internal class OAuth2AccessTokenResponse : OAuth2Message
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00036210 File Offset: 0x00034410
		public static OAuth2AccessTokenResponse Read(string responseString)
		{
			OAuth2AccessTokenResponse oauth2AccessTokenResponse = new OAuth2AccessTokenResponse();
			oauth2AccessTokenResponse.DecodeFromJson(responseString);
			return oauth2AccessTokenResponse;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0003622B File Offset: 0x0003442B
		public override string ToString()
		{
			return base.EncodeToJson();
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00036233 File Offset: 0x00034433
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00036245 File Offset: 0x00034445
		public string AccessToken
		{
			get
			{
				return base.Message["access_token"];
			}
			set
			{
				base.Message["access_token"] = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x00036258 File Offset: 0x00034458
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x0003626A File Offset: 0x0003446A
		public virtual string ExpiresIn
		{
			get
			{
				return base.Message["expires_in"];
			}
			set
			{
				base.Message["expires_in"] = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0003627D File Offset: 0x0003447D
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0003628F File Offset: 0x0003448F
		public string RefreshToken
		{
			get
			{
				return base.Message["refresh_token"];
			}
			set
			{
				base.Message["refresh_token"] = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000362A2 File Offset: 0x000344A2
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000362B4 File Offset: 0x000344B4
		public string Scope
		{
			get
			{
				return base.Message["scope"];
			}
			set
			{
				base.Message["scope"] = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000362C7 File Offset: 0x000344C7
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x000362D9 File Offset: 0x000344D9
		public string TokenType
		{
			get
			{
				return base.Message["token_type"];
			}
			set
			{
				base.Message["token_type"] = value;
			}
		}
	}
}
