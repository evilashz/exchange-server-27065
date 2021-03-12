using System;
using System.IdentityModel.Tokens;
using System.Text;
using Microsoft.Exchange.Security.OAuth.OAuthProtocols;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000F1 RID: 241
	internal sealed class TokenResult
	{
		// Token: 0x0600080A RID: 2058 RVA: 0x0003643F File Offset: 0x0003463F
		public TokenResult(string tokenString, DateTime expireDate)
		{
			this.tokenString = tokenString;
			this.expirationDate = expireDate;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00036458 File Offset: 0x00034658
		public TokenResult(OAuth2AccessTokenResponse response) : this(response.AccessToken, DateTime.UtcNow.AddSeconds((double)int.Parse(response.ExpiresIn)))
		{
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0003648A File Offset: 0x0003468A
		public TokenResult(JwtSecurityToken tokenObject, DateTime expireDate)
		{
			this.token = tokenObject;
			this.expirationDate = expireDate;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000364A0 File Offset: 0x000346A0
		public JwtSecurityToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000364A8 File Offset: 0x000346A8
		public string TokenString
		{
			get
			{
				if (this.tokenString == null && this.token != null)
				{
					this.tokenString = new JwtSecurityTokenHandler().WriteToken(this.token);
				}
				return this.tokenString;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000364D8 File Offset: 0x000346D8
		public string Base64String
		{
			get
			{
				if (this.base64String == null)
				{
					string text = this.ToString();
					if (text != null)
					{
						this.base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
					}
				}
				return this.base64String;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00036513 File Offset: 0x00034713
		public DateTime ExpirationDate
		{
			get
			{
				return this.expirationDate;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0003651B File Offset: 0x0003471B
		public TimeSpan RemainingTokenLifeTime
		{
			get
			{
				return this.expirationDate - DateTime.UtcNow;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0003652D File Offset: 0x0003472D
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = OAuthCommon.GetReadableTokenString(this.TokenString);
			}
			return this.toString;
		}

		// Token: 0x04000777 RID: 1911
		private readonly DateTime expirationDate;

		// Token: 0x04000778 RID: 1912
		private JwtSecurityToken token;

		// Token: 0x04000779 RID: 1913
		private string tokenString;

		// Token: 0x0400077A RID: 1914
		private string toString;

		// Token: 0x0400077B RID: 1915
		private string base64String;
	}
}
