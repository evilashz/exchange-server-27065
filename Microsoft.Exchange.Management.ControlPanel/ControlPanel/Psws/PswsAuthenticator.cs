using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.PswsClient;
using Microsoft.IdentityModel.S2S.Web;

namespace Microsoft.Exchange.Management.ControlPanel.Psws
{
	// Token: 0x0200036B RID: 875
	internal sealed class PswsAuthenticator : IAuthenticator
	{
		// Token: 0x06002FF9 RID: 12281 RVA: 0x00092080 File Offset: 0x00090280
		internal static PswsAuthenticator Create()
		{
			string text = RbacPrincipal.Current.RbacConfiguration.ExecutingUserOrganizationId.ToExternalDirectoryOrganizationId();
			string text2 = (string)HttpContext.Current.Items[TokenIssuer.ItemTagUpn];
			string text3 = (string)HttpContext.Current.Items[TokenIssuer.ItemTagPuid];
			return new PswsAuthenticator(text, text2, text3);
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000920E0 File Offset: 0x000902E0
		internal PswsAuthenticator(string tenantId, string upn, string puid)
		{
			if (string.IsNullOrWhiteSpace(tenantId))
			{
				throw new ArgumentNullException("tenantId cannot be null or empty");
			}
			if (string.IsNullOrWhiteSpace(upn))
			{
				throw new ArgumentNullException("upn cannot be null or empty");
			}
			if (string.IsNullOrWhiteSpace(puid))
			{
				throw new ArgumentNullException("puid cannot be null or empty");
			}
			this.tenantId = tenantId;
			this.upn = upn;
			this.puid = puid;
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x00092144 File Offset: 0x00090344
		public IDisposable Authenticate(HttpWebRequest request)
		{
			TokenIssuer tokenIssuer = new TokenIssuer();
			string token = tokenIssuer.GetToken(this.tenantId, new Dictionary<string, string>
			{
				{
					TokenIssuer.ClaimTypeUpn,
					this.upn
				},
				{
					TokenIssuer.ClaimTypeNameId,
					this.puid
				}
			});
			request.Headers.Add(TokenIssuer.AuthorizationHeader, OAuth2ProtectedResourceUtility.WriteAuthorizationHeader(token));
			return null;
		}

		// Token: 0x0400232D RID: 9005
		private readonly string tenantId;

		// Token: 0x0400232E RID: 9006
		private readonly string upn;

		// Token: 0x0400232F RID: 9007
		private readonly string puid;
	}
}
