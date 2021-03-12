using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B61 RID: 2913
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SamlSecurityTokenManager : ClientCredentialsSecurityTokenManager
	{
		// Token: 0x06006989 RID: 27017 RVA: 0x001C4BC6 File Offset: 0x001C2DC6
		public SamlSecurityTokenManager(SamlClientCredentials samlClientCredentials) : base(samlClientCredentials)
		{
			this.samlClientCredentials = samlClientCredentials;
		}

		// Token: 0x0600698A RID: 27018 RVA: 0x001C4BD8 File Offset: 0x001C2DD8
		public override SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement)
		{
			if (string.Equals(tokenRequirement.TokenType, SecurityTokenTypes.Saml, StringComparison.OrdinalIgnoreCase) || string.Equals(tokenRequirement.TokenType, "http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV1.1", StringComparison.OrdinalIgnoreCase))
			{
				if (this.cachedSecurityTokenProvider == null)
				{
					this.cachedSecurityTokenProvider = new SamlSecurityTokenProvider(this.samlClientCredentials);
				}
				return this.cachedSecurityTokenProvider;
			}
			return base.CreateSecurityTokenProvider(tokenRequirement);
		}

		// Token: 0x04003C0B RID: 15371
		private SamlClientCredentials samlClientCredentials;

		// Token: 0x04003C0C RID: 15372
		private SamlSecurityTokenProvider cachedSecurityTokenProvider;
	}
}
