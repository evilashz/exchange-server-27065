using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000D8 RID: 216
	internal struct ProxyServerSettings
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0000D76F File Offset: 0x0000B96F
		public ProxyServerSettings(string fqdn, LocalMailboxFlags extraFlags, ProxyScenarios proxyScenario)
		{
			this = default(ProxyServerSettings);
			this.Fqdn = fqdn;
			this.ExtraFlags = extraFlags;
			this.Scenario = proxyScenario;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0000D78D File Offset: 0x0000B98D
		// (set) Token: 0x0600081A RID: 2074 RVA: 0x0000D795 File Offset: 0x0000B995
		public string Fqdn { get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0000D79E File Offset: 0x0000B99E
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0000D7A6 File Offset: 0x0000B9A6
		public LocalMailboxFlags ExtraFlags { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0000D7B7 File Offset: 0x0000B9B7
		public ProxyScenarios Scenario { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public bool IsProxyLocal
		{
			get
			{
				return this.Scenario == ProxyScenarios.LocalMdbAndProxy || this.Scenario == ProxyScenarios.LocalProxyRemoteMdb;
			}
		}
	}
}
