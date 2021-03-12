using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000042 RID: 66
	internal class OAuthTokenAccessor : CommonAccessTokenAccessor
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000CFF2 File Offset: 0x0000B1F2
		private OAuthTokenAccessor(CommonAccessToken token) : base(token)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000CFFB File Offset: 0x0000B1FB
		public override AccessTokenType TokenType
		{
			get
			{
				return AccessTokenType.OAuth;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D000 File Offset: 0x0000B200
		public static OAuthTokenAccessor Create(OAuthIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			CommonAccessToken commonAccessToken = new CommonAccessToken(AccessTokenType.OAuth);
			commonAccessToken.Version = 2;
			commonAccessToken.ExtensionData["OrganizationIdBase64"] = CommonAccessTokenAccessor.SerializeOrganizationId(identity.OrganizationId);
			commonAccessToken.ExtensionData["AppOnly"] = identity.IsAppOnly.ToString();
			identity.OAuthApplication.AddExtensionDataToCommonAccessToken(commonAccessToken);
			if (!identity.IsAppOnly)
			{
				identity.ActAsUser.AddExtensionDataToCommonAccessToken(commonAccessToken);
			}
			return new OAuthTokenAccessor(commonAccessToken);
		}

		// Token: 0x040001C3 RID: 451
		public static readonly int MinVersion = new ServerVersion(15, 0, 788, 0).ToInt();
	}
}
