using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002CD RID: 717
	public class SetServerMonitorCommand : SyntheticCommandWithPipelineInputNoOutput<ServerIdParameter>
	{
		// Token: 0x0600318E RID: 12686 RVA: 0x0005836D File Offset: 0x0005656D
		private SetServerMonitorCommand() : base("Set-ServerMonitor")
		{
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0005837A File Offset: 0x0005657A
		public SetServerMonitorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00058389 File Offset: 0x00056589
		public virtual SetServerMonitorCommand SetParameters(SetServerMonitorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002CE RID: 718
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170017AD RID: 6061
			// (set) Token: 0x06003191 RID: 12689 RVA: 0x00058393 File Offset: 0x00056593
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x170017AE RID: 6062
			// (set) Token: 0x06003192 RID: 12690 RVA: 0x000583A6 File Offset: 0x000565A6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170017AF RID: 6063
			// (set) Token: 0x06003193 RID: 12691 RVA: 0x000583B9 File Offset: 0x000565B9
			public virtual string TargetResource
			{
				set
				{
					base.PowerSharpParameters["TargetResource"] = value;
				}
			}

			// Token: 0x170017B0 RID: 6064
			// (set) Token: 0x06003194 RID: 12692 RVA: 0x000583CC File Offset: 0x000565CC
			public virtual bool Repairing
			{
				set
				{
					base.PowerSharpParameters["Repairing"] = value;
				}
			}

			// Token: 0x170017B1 RID: 6065
			// (set) Token: 0x06003195 RID: 12693 RVA: 0x000583E4 File Offset: 0x000565E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017B2 RID: 6066
			// (set) Token: 0x06003196 RID: 12694 RVA: 0x000583FC File Offset: 0x000565FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017B3 RID: 6067
			// (set) Token: 0x06003197 RID: 12695 RVA: 0x00058414 File Offset: 0x00056614
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017B4 RID: 6068
			// (set) Token: 0x06003198 RID: 12696 RVA: 0x0005842C File Offset: 0x0005662C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017B5 RID: 6069
			// (set) Token: 0x06003199 RID: 12697 RVA: 0x00058444 File Offset: 0x00056644
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
