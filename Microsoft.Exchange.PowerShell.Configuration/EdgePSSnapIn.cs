using System;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Management.PowerShell
{
	// Token: 0x02000005 RID: 5
	public sealed class EdgePSSnapIn : ExchangePSSnapIn
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00010D67 File Offset: 0x0000EF67
		public override string Name
		{
			get
			{
				return AdminPSSnapIn.PSSnapInName;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00010D70 File Offset: 0x0000EF70
		public override Collection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				if (this.cmdlets == null)
				{
					this.cmdlets = new Collection<CmdletConfigurationEntry>();
					foreach (CmdletConfigurationEntry item in CmdletConfigurationEntries.ExchangeCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item);
					}
					foreach (CmdletConfigurationEntry item2 in CmdletConfigurationEntries.ExchangeEdgeCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item2);
					}
				}
				return this.cmdlets;
			}
		}

		// Token: 0x0400000D RID: 13
		private Collection<CmdletConfigurationEntry> cmdlets;
	}
}
