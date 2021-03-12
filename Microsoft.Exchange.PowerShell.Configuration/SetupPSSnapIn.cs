using System;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;

namespace Microsoft.Exchange.Management.PowerShell
{
	// Token: 0x02000006 RID: 6
	public sealed class SetupPSSnapIn : ExchangePSSnapIn
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00010DEE File Offset: 0x0000EFEE
		public override string Name
		{
			get
			{
				return SetupPSSnapIn.PSSnapInName;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00010DF5 File Offset: 0x0000EFF5
		public override Collection<FormatConfigurationEntry> Formats
		{
			get
			{
				return new Collection<FormatConfigurationEntry>();
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00010DFC File Offset: 0x0000EFFC
		public override Collection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				if (this.cmdlets == null)
				{
					this.cmdlets = new Collection<CmdletConfigurationEntry>();
					foreach (CmdletConfigurationEntry item in CmdletConfigurationEntries.SetupCmdletConfigurationEntries)
					{
						this.cmdlets.Add(item);
					}
				}
				return this.cmdlets;
			}
		}

		// Token: 0x0400000E RID: 14
		public static readonly string PSSnapInName = "Microsoft.Exchange.Management.PowerShell.Setup";

		// Token: 0x0400000F RID: 15
		private Collection<CmdletConfigurationEntry> cmdlets;
	}
}
