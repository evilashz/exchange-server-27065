using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200017B RID: 379
	[CLSCompliant(false)]
	public sealed class MSExchangeCASToMBXProxyPortFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00019908 File Offset: 0x00017B08
		protected override string ComponentName
		{
			get
			{
				return "MSExchange CAS-MBX Proxy";
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x0001990F File Offset: 0x00017B0F
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGECASTOMBXPROXYPORT;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00019916 File Offset: 0x00017B16
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001991D File Offset: 0x00017B1D
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00019924 File Offset: 0x00017B24
		protected override string LocalPorts
		{
			get
			{
				return "81,444";
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0001992B File Offset: 0x00017B2B
		protected override bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001992E File Offset: 0x00017B2E
		protected override bool InhibitServiceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000648 RID: 1608
		private const string RuleApplicationRelativePath = "";
	}
}
