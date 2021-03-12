using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.S2S.Protocols.OAuth2;
using Microsoft.IdentityModel.S2S.Tokens;

namespace Microsoft.Exchange.Management.ControlPanel.Psws
{
	// Token: 0x0200036C RID: 876
	internal sealed class TokenIssuer
	{
		// Token: 0x06002FFC RID: 12284 RVA: 0x000921A4 File Offset: 0x000903A4
		internal TokenIssuer()
		{
			this.settings = TokenIssuerSettings.CreateFromConfiguration();
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000921B7 File Offset: 0x000903B7
		internal TokenIssuer(TokenIssuerSettings settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings cannot be null");
			}
			this.settings = settings;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000921D4 File Offset: 0x000903D4
		internal string GetToken(string tenantId, IDictionary<string, string> claims = null)
		{
			if (string.IsNullOrWhiteSpace(tenantId))
			{
				throw new ArgumentNullException("tenantId cannot be null or empty");
			}
			if (claims == null)
			{
				claims = new Dictionary<string, string>();
			}
			return this.IssueUserAccessToken(tenantId, this.IssueTenantAccessToken(tenantId), claims);
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00092204 File Offset: 0x00090404
		private string IssueTenantAccessToken(string tenantId)
		{
			string text = string.Format("{0}@{1}", this.settings.PartnerId, tenantId);
			string arg = string.Format("{0}/{1}", this.settings.AcsId, this.settings.AcsUrl.Authority);
			string text2 = string.Format("{0}@{1}", arg, tenantId);
			JsonWebSecurityToken jsonWebSecurityToken = new JsonWebSecurityToken(text, text2, DateTime.UtcNow, DateTime.UtcNow.AddDays(1.0), Enumerable.Empty<JsonWebTokenClaim>(), CertificateStore.GetSigningCredentials(this.settings.CertificateSubject));
			string text3 = string.Format("{0}/{1}@{2}", this.settings.ServiceId, this.settings.ServiceHostName, tenantId);
			OAuth2AccessTokenRequest oauth2AccessTokenRequest = OAuth2MessageFactory.CreateAccessTokenRequestWithAssertion(jsonWebSecurityToken, text3);
			oauth2AccessTokenRequest.Scope = text3;
			OAuth2S2SClient oauth2S2SClient = new OAuth2S2SClient();
			OAuth2AccessTokenResponse oauth2AccessTokenResponse = (OAuth2AccessTokenResponse)oauth2S2SClient.Issue(this.settings.AcsUrl.AbsoluteUri, oauth2AccessTokenRequest);
			return oauth2AccessTokenResponse.AccessToken;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x00092310 File Offset: 0x00090510
		private string IssueUserAccessToken(string tenantId, string actorToken, IDictionary<string, string> claims)
		{
			string text = string.Format("{0}@{1}", this.settings.PartnerId, tenantId);
			string text2 = string.Format("{0}/{1}@{2}", this.settings.ServiceId, this.settings.ServiceHostName, tenantId);
			List<JsonWebTokenClaim> first = new List<JsonWebTokenClaim>
			{
				new JsonWebTokenClaim("actortoken", actorToken),
				new JsonWebTokenClaim(TokenIssuer.ClaimTypeNii, TokenIssuer.ClaimValueNii)
			};
			IEnumerable<JsonWebTokenClaim> source = first.Concat(from claim in claims
			select new JsonWebTokenClaim(claim.Key, claim.Value));
			JsonWebSecurityToken jsonWebSecurityToken = new JsonWebSecurityToken(text, text2, DateTime.UtcNow, DateTime.UtcNow.AddDays(1.0), source.ToArray<JsonWebTokenClaim>());
			return new JsonWebSecurityTokenHandler().WriteTokenAsString(jsonWebSecurityToken);
		}

		// Token: 0x04002330 RID: 9008
		internal static readonly string ClaimTypeNameId = "nameid";

		// Token: 0x04002331 RID: 9009
		internal static readonly string ClaimTypeUpn = "upn";

		// Token: 0x04002332 RID: 9010
		internal static readonly string ClaimTypeNii = "nii";

		// Token: 0x04002333 RID: 9011
		internal static readonly string ClaimValueNii = "urn:federation:microsoftonline";

		// Token: 0x04002334 RID: 9012
		internal static readonly string AuthorizationHeader = "Authorization";

		// Token: 0x04002335 RID: 9013
		internal static readonly string ItemTagUpn = "RPSMemberName";

		// Token: 0x04002336 RID: 9014
		internal static readonly string ItemTagPuid = "RPSPUID";

		// Token: 0x04002337 RID: 9015
		internal static readonly string ItemTagTenantId = "TenantId";

		// Token: 0x04002338 RID: 9016
		private TokenIssuerSettings settings;
	}
}
