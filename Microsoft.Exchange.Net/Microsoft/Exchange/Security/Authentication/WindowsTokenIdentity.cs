using System;
using System.Security.Principal;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000640 RID: 1600
	public class WindowsTokenIdentity : GenericSidIdentity
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x000345AE File Offset: 0x000327AE
		public WindowsTokenIdentity(WindowsAccessToken accessToken) : base(accessToken.LogonName, accessToken.AuthenticationType, new SecurityIdentifier(accessToken.UserSid))
		{
			this.accessToken = accessToken;
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x000345D4 File Offset: 0x000327D4
		public WindowsAccessToken AccessToken
		{
			get
			{
				return this.accessToken;
			}
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000345DC File Offset: 0x000327DC
		internal override ClientSecurityContext CreateClientSecurityContext()
		{
			return new ClientSecurityContext(this.AccessToken, AuthzFlags.AuthzSkipTokenGroups);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000345EC File Offset: 0x000327EC
		internal SerializedIdentity ToSerializedIdentity()
		{
			SerializedIdentity result;
			using (ClientSecurityContext clientSecurityContext = this.CreateClientSecurityContext())
			{
				SerializedAccessToken serializedAccessToken = new SerializedAccessToken(this.AccessToken.LogonName, this.AccessToken.AuthenticationType, clientSecurityContext);
				result = new SerializedIdentity(serializedAccessToken);
			}
			return result;
		}

		// Token: 0x04001D34 RID: 7476
		private WindowsAccessToken accessToken;
	}
}
