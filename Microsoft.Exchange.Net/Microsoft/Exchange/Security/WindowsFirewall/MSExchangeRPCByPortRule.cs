using System;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200015F RID: 351
	[CLSCompliant(false)]
	public sealed class MSExchangeRPCByPortRule : ExchangeFirewallRule
	{
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00019376 File Offset: 0x00017576
		protected override string ComponentName
		{
			get
			{
				return "MSExchangeRPC (GFW)";
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0001937D File Offset: 0x0001757D
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_MSEXCHANGERPCRPC;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00019384 File Offset: 0x00017584
		protected override string ApplicationPath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0001938B File Offset: 0x0001758B
		protected override string ServiceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00019392 File Offset: 0x00017592
		protected override string LocalPorts
		{
			get
			{
				return "RPC";
			}
		}

		// Token: 0x0400062C RID: 1580
		private const string RuleApplicationRelativePath = "";
	}
}
