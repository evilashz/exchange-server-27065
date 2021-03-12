using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000165 RID: 357
	[ClassAccessLevel(AccessLevel.Consumer)]
	public class ManageFirewallException : Task
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0003C4CA File Offset: 0x0003A6CA
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0003C4E1 File Offset: 0x0003A6E1
		[Parameter(Mandatory = false)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0003C4F4 File Offset: 0x0003A6F4
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0003C50B File Offset: 0x0003A70B
		[Parameter(Mandatory = false)]
		public LocalLongFullPath BinaryPath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["BinaryPath"];
			}
			set
			{
				base.Fields["BinaryPath"] = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0003C51E File Offset: 0x0003A71E
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0003C535 File Offset: 0x0003A735
		[Parameter(Mandatory = false)]
		public ExchangeFirewallRule FirewallRule
		{
			get
			{
				return (ExchangeFirewallRule)base.Fields["FirewallRule"];
			}
			set
			{
				base.Fields["FirewallRule"] = value;
			}
		}
	}
}
