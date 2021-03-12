using System;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Management.PowerShell
{
	// Token: 0x02000007 RID: 7
	public sealed class AdminPSSnapIn : ExchangePSSnapIn
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00010E5A File Offset: 0x0000F05A
		public override string Name
		{
			get
			{
				return AdminPSSnapIn.PSSnapInName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00010E64 File Offset: 0x0000F064
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
					foreach (CmdletConfigurationEntry item2 in CmdletConfigurationEntries.ExchangeNonEdgeCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item2);
					}
					foreach (CmdletConfigurationEntry item3 in CmdletConfigurationEntries.ExchangeNonGallatinCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item3);
					}
				}
				return this.cmdlets;
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly string PSSnapInName = "Microsoft.Exchange.Management.PowerShell.E2010";

		// Token: 0x04000011 RID: 17
		private Collection<CmdletConfigurationEntry> cmdlets;
	}
}
