using System;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B94 RID: 2964
	internal sealed class FederatedClientCredentials
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x000A81C0 File Offset: 0x000A63C0
		public FederatedClientCredentials(FederatedIdentity userFederatedIdentity, string userEmailAddress, FedOrgCredentials organizationCredentials)
		{
			if (userFederatedIdentity == null)
			{
				throw new ArgumentException("userFederatedIdentity");
			}
			if (userEmailAddress == null)
			{
				throw new ArgumentException("userEmailAddress");
			}
			if (organizationCredentials == null)
			{
				throw new ArgumentException("organizationCredentials");
			}
			this.userFederatedIdentity = userFederatedIdentity;
			this.organizationCredentials = organizationCredentials;
			this.userEmailAddress = userEmailAddress;
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000A8212 File Offset: 0x000A6412
		private FederatedClientCredentials(FederatedClientCredentials other)
		{
			this.userFederatedIdentity = other.userFederatedIdentity;
			this.organizationCredentials = other.organizationCredentials;
			this.userEmailAddress = other.userEmailAddress;
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003F6F RID: 16239 RVA: 0x000A823E File Offset: 0x000A643E
		internal string UserEmailAddress
		{
			get
			{
				return this.userEmailAddress;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000A8246 File Offset: 0x000A6446
		internal FederatedIdentity UserFederatedIdentity
		{
			get
			{
				return this.userFederatedIdentity;
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x000A824E File Offset: 0x000A644E
		public RequestedToken GetToken()
		{
			return this.organizationCredentials.GetToken();
		}

		// Token: 0x0400373E RID: 14142
		private FederatedIdentity userFederatedIdentity;

		// Token: 0x0400373F RID: 14143
		private string userEmailAddress;

		// Token: 0x04003740 RID: 14144
		private FedOrgCredentials organizationCredentials;
	}
}
