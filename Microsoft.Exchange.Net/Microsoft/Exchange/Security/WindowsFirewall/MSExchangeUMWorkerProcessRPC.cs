using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x02000175 RID: 373
	[CLSCompliant(false)]
	public sealed class MSExchangeUMWorkerProcessRPC : ExchangeFirewallRule
	{
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000197DE File Offset: 0x000179DE
		protected override string ComponentName
		{
			get
			{
				return "UMWorkerProcess - RPC";
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x000197E5 File Offset: 0x000179E5
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_UMWORKERPROCESSRPC;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x000197EC File Offset: 0x000179EC
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\UMWorkerProcess.exe");
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000197FD File Offset: 0x000179FD
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00019804 File Offset: 0x00017A04
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x04000642 RID: 1602
		private const string RuleApplicationRelativePath = "Bin\\UMWorkerProcess.exe";
	}
}
