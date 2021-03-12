using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000170 RID: 368
	[CLSCompliant(false)]
	public sealed class RpcHttpRedirectorRule : ExchangeFirewallRule
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000196E3 File Offset: 0x000178E3
		protected override string ComponentName
		{
			get
			{
				return "RpcHttpLBS";
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000196EA File Offset: 0x000178EA
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_RPCHTTPLBS;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000196F1 File Offset: 0x000178F1
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(Environment.SystemDirectory, "System32\\Svchost.exe");
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00019702 File Offset: 0x00017902
		protected override string ServiceName
		{
			get
			{
				return "RpcHttpLBS";
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00019709 File Offset: 0x00017909
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00019710 File Offset: 0x00017910
		protected override bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00019713 File Offset: 0x00017913
		protected override bool InhibitServiceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400063D RID: 1597
		private const string RuleApplicationRelativePath = "System32\\Svchost.exe";
	}
}
