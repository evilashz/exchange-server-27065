using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Management.Powershell
{
	// Token: 0x02000002 RID: 2
	[RunInstaller(true)]
	public class OfficeDatacenterAMCmdletSnapIn : CustomPSSnapIn
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public override string Description
		{
			get
			{
				return "Provides a Windows Powershell interface for Office Datacenter Monitoring Service Management Tools.";
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D7 File Offset: 0x000002D7
		public override string Name
		{
			get
			{
				return OfficeDatacenterAMCmdletSnapIn.PSSnapInName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020DE File Offset: 0x000002DE
		public override string Vendor
		{
			get
			{
				return "Microsoft Corporation";
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public override Collection<CmdletConfigurationEntry> Cmdlets
		{
			get
			{
				if (this.cmdlets == null)
				{
					this.cmdlets = new Collection<CmdletConfigurationEntry>();
					foreach (CmdletConfigurationEntry item in CmdletConfiguration.CmdletConfigurationEntries)
					{
						this.cmdlets.Add(item);
					}
				}
				return this.cmdlets;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002134 File Offset: 0x00000334
		public override Collection<FormatConfigurationEntry> Formats
		{
			get
			{
				if (this.formats == null)
				{
					this.formats = new Collection<FormatConfigurationEntry>();
					foreach (FormatConfigurationEntry item in CmdletConfiguration.FormatConfigurationEntries)
					{
						this.formats.Add(item);
					}
				}
				return this.formats;
			}
		}

		// Token: 0x04000001 RID: 1
		public static readonly string PSSnapInName = " Microsoft.Office.Datacenter.ActiveMonitoring.Management.Powershell";

		// Token: 0x04000002 RID: 2
		private Collection<CmdletConfigurationEntry> cmdlets;

		// Token: 0x04000003 RID: 3
		private Collection<FormatConfigurationEntry> formats;
	}
}
