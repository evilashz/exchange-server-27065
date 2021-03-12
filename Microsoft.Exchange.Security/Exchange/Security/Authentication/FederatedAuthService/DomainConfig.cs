using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.Authentication.FederatedAuthService
{
	// Token: 0x0200006C RID: 108
	internal class DomainConfig
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0001CDEC File Offset: 0x0001AFEC
		public DomainConfig(string domain, LiveIdInstanceType instance, bool federated, string authURL, bool cache, LivePreferredProtocol protocol)
		{
			this.DomainName = domain;
			this.Instance = instance;
			this.IsFederated = federated;
			this.AuthURL = authURL;
			this.Cache = cache;
			this.PreferredProtocol = protocol;
			this.ClockSkew = TimeSpan.Zero;
			this.OrgId = OrganizationId.ForestWideOrgId;
		}

		// Token: 0x0400039E RID: 926
		public string DomainName;

		// Token: 0x0400039F RID: 927
		public LiveIdInstanceType Instance;

		// Token: 0x040003A0 RID: 928
		public bool IsFederated;

		// Token: 0x040003A1 RID: 929
		public string AuthURL;

		// Token: 0x040003A2 RID: 930
		public bool Cache;

		// Token: 0x040003A3 RID: 931
		public LivePreferredProtocol PreferredProtocol;

		// Token: 0x040003A4 RID: 932
		public TimeSpan ClockSkew;

		// Token: 0x040003A5 RID: 933
		public OrganizationId OrgId;

		// Token: 0x040003A6 RID: 934
		public bool SyncedAD;

		// Token: 0x040003A7 RID: 935
		public DateTime LastUpdateTime;

		// Token: 0x040003A8 RID: 936
		public bool IsOutlookCom;
	}
}
