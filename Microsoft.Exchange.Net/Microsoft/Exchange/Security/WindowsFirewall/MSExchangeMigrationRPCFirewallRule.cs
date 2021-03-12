using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000150 RID: 336
	[CLSCompliant(false)]
	public sealed class MSExchangeMigrationRPCFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001905F File Offset: 0x0001725F
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMigration - RPC";
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00019066 File Offset: 0x00017266
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMIGRATIONRPC;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001906D File Offset: 0x0001726D
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\msexchangemigration.exe");
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001907E File Offset: 0x0001727E
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMigration";
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00019085 File Offset: 0x00017285
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400061D RID: 1565
		private const string RuleApplicationRelativePath = "Bin\\msexchangemigration.exe";
	}
}
