using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016A RID: 362
	[CLSCompliant(false)]
	public sealed class MSExchangeTransportWorkerRPCEPMapperFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000195A9 File Offset: 0x000177A9
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeTransportWorker - RPCEPMap";
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000195B0 File Offset: 0x000177B0
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGETRANSPORTWORKERRPCEPMAP;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x000195B7 File Offset: 0x000177B7
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\edgetransport.exe");
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000195C8 File Offset: 0x000177C8
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeTransportWorker";
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x000195CF File Offset: 0x000177CF
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x04000637 RID: 1591
		private const string RuleApplicationRelativePath = "Bin\\edgetransport.exe";
	}
}
