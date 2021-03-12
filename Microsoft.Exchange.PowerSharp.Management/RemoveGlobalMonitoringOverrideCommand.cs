using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002C9 RID: 713
	public class RemoveGlobalMonitoringOverrideCommand : SyntheticCommandWithPipelineInput<MonitoringOverride, MonitoringOverride>
	{
		// Token: 0x06003174 RID: 12660 RVA: 0x0005817F File Offset: 0x0005637F
		private RemoveGlobalMonitoringOverrideCommand() : base("Remove-GlobalMonitoringOverride")
		{
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x0005818C File Offset: 0x0005638C
		public RemoveGlobalMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x0005819B File Offset: 0x0005639B
		public virtual RemoveGlobalMonitoringOverrideCommand SetParameters(RemoveGlobalMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002CA RID: 714
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700179B RID: 6043
			// (set) Token: 0x06003177 RID: 12663 RVA: 0x000581A5 File Offset: 0x000563A5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700179C RID: 6044
			// (set) Token: 0x06003178 RID: 12664 RVA: 0x000581B8 File Offset: 0x000563B8
			public virtual MonitoringItemTypeEnum ItemType
			{
				set
				{
					base.PowerSharpParameters["ItemType"] = value;
				}
			}

			// Token: 0x1700179D RID: 6045
			// (set) Token: 0x06003179 RID: 12665 RVA: 0x000581D0 File Offset: 0x000563D0
			public virtual string PropertyName
			{
				set
				{
					base.PowerSharpParameters["PropertyName"] = value;
				}
			}

			// Token: 0x1700179E RID: 6046
			// (set) Token: 0x0600317A RID: 12666 RVA: 0x000581E3 File Offset: 0x000563E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700179F RID: 6047
			// (set) Token: 0x0600317B RID: 12667 RVA: 0x000581F6 File Offset: 0x000563F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170017A0 RID: 6048
			// (set) Token: 0x0600317C RID: 12668 RVA: 0x0005820E File Offset: 0x0005640E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170017A1 RID: 6049
			// (set) Token: 0x0600317D RID: 12669 RVA: 0x00058226 File Offset: 0x00056426
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170017A2 RID: 6050
			// (set) Token: 0x0600317E RID: 12670 RVA: 0x0005823E File Offset: 0x0005643E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170017A3 RID: 6051
			// (set) Token: 0x0600317F RID: 12671 RVA: 0x00058256 File Offset: 0x00056456
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
