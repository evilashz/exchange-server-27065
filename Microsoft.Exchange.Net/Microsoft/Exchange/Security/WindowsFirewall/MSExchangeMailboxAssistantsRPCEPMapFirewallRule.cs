using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200014D RID: 333
	[CLSCompliant(false)]
	public sealed class MSExchangeMailboxAssistantsRPCEPMapFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00018FCA File Offset: 0x000171CA
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeMailboxAssistants - RPCEPMap";
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00018FD1 File Offset: 0x000171D1
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGEMAILBOXASSISTANTSRPCEPMAP;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00018FD8 File Offset: 0x000171D8
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(ExchangeFirewallRule.ExchangeInstallPath, "Bin\\MSExchangeMailboxAssistants.exe");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00018FE9 File Offset: 0x000171E9
		protected override string ServiceName
		{
			get
			{
				return "MSExchangeMailboxAssistants";
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00018FF0 File Offset: 0x000171F0
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x0400061A RID: 1562
		private const string RuleApplicationRelativePath = "Bin\\MSExchangeMailboxAssistants.exe";
	}
}
