using System;
using System.IO;

namespace Microsoft.Exchange.Security.WindowsFirewall
{
	// Token: 0x0200016F RID: 367
	[CLSCompliant(false)]
	public sealed class RpcEndPointMapperFirewallRule : ExchangeFirewallRule
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000196A8 File Offset: 0x000178A8
		protected override string ComponentName
		{
			get
			{
				return "RPC Endpoint Mapper";
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000196AF File Offset: 0x000178AF
		protected override IndirectStrings DescriptionIndirectString
		{
			get
			{
				return IndirectStrings.IDS_FIREWALLRULE_DESC_RPC_ENDPOINT_MAPPER;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000196B6 File Offset: 0x000178B6
		protected override string ApplicationPath
		{
			get
			{
				return Path.Combine(Environment.SystemDirectory, "System32\\Svchost.exe");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000196C7 File Offset: 0x000178C7
		protected override string ServiceName
		{
			get
			{
				return "RPCSS";
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000196CE File Offset: 0x000178CE
		protected override string LocalPorts
		{
			get
			{
				return "RPC-EPMap";
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000196D5 File Offset: 0x000178D5
		protected override bool InhibitApplicationPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000196D8 File Offset: 0x000178D8
		protected override bool InhibitServiceName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0400063C RID: 1596
		private const string RuleApplicationRelativePath = "System32\\Svchost.exe";
	}
}
