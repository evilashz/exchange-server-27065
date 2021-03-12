using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000151 RID: 337
	[CLSCompliant(false)]
	public sealed class MSExchangeMigrationRPCEPMapperFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00019094 File Offset: 0x00017294
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMigration - RPCEPMap";
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001909B File Offset: 0x0001729B
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMIGRATIONRPCEPMAP;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000190A2 File Offset: 0x000172A2
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangemigration.exe");
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x000190B3 File Offset: 0x000172B3
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMigration";
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000190BA File Offset: 0x000172BA
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x0400061E RID: 1566
		private const string RuleApplicationRelativePath = "Bin\\msexchangemigration.exe";
	}
}
