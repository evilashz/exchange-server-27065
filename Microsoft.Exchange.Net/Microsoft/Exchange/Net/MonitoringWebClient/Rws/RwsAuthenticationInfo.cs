using System;
using System.Security;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007ED RID: 2029
	internal class RwsAuthenticationInfo
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0005CB20 File Offset: 0x0005AD20
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0005CB28 File Offset: 0x0005AD28
		public CommonAccessToken Token { get; private set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x0005CB31 File Offset: 0x0005AD31
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0005CB39 File Offset: 0x0005AD39
		public string UserName { get; private set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x0005CB42 File Offset: 0x0005AD42
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x0005CB4A File Offset: 0x0005AD4A
		public string UserDomain { get; private set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x0005CB53 File Offset: 0x0005AD53
		// (set) Token: 0x06002A88 RID: 10888 RVA: 0x0005CB5B File Offset: 0x0005AD5B
		public SecureString Password { get; private set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x0005CB64 File Offset: 0x0005AD64
		// (set) Token: 0x06002A8A RID: 10890 RVA: 0x0005CB6C File Offset: 0x0005AD6C
		public RwsAuthenticationType AuthenticationType { get; private set; }

		// Token: 0x06002A8B RID: 10891 RVA: 0x0005CB75 File Offset: 0x0005AD75
		public RwsAuthenticationInfo(CommonAccessToken token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token");
			}
			this.AuthenticationType = RwsAuthenticationType.Brick;
			this.Token = token;
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0005CB9C File Offset: 0x0005AD9C
		public RwsAuthenticationInfo(string userName, string userDomain, SecureString password)
		{
			if (userName == null)
			{
				throw new ArgumentNullException("userName");
			}
			if (userDomain == null)
			{
				throw new ArgumentNullException("userDomain");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			this.AuthenticationType = RwsAuthenticationType.LiveIdBasic;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
		}
	}
}
