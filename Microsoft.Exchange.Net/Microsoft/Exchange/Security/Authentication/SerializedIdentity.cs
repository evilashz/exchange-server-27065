using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200063E RID: 1598
	internal class SerializedIdentity : GenericSidIdentity
	{
		// Token: 0x06001CF4 RID: 7412 RVA: 0x00034539 File Offset: 0x00032739
		public SerializedIdentity(SerializedAccessToken accessToken) : base(accessToken.LogonName, accessToken.AuthenticationType, new SecurityIdentifier(accessToken.UserSid))
		{
			this.accessToken = accessToken;
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x0003455F File Offset: 0x0003275F
		public SerializedAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00034567 File Offset: 0x00032767
		internal override ClientSecurityContext CreateClientSecurityContext()
		{
			return new ClientSecurityContext(this.AccessToken, AuthzFlags.AuthzSkipTokenGroups);
		}

		// Token: 0x04001D32 RID: 7474
		private SerializedAccessToken accessToken;
	}
}
