using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002A7 RID: 679
	public class GetGlobalMonitoringOverrideCommand : SyntheticCommandWithPipelineInput<MonitoringOverride, MonitoringOverride>
	{
		// Token: 0x06003069 RID: 12393 RVA: 0x00056CDA File Offset: 0x00054EDA
		private GetGlobalMonitoringOverrideCommand() : base("Get-GlobalMonitoringOverride")
		{
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x00056CE7 File Offset: 0x00054EE7
		public GetGlobalMonitoringOverrideCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x00056CF6 File Offset: 0x00054EF6
		public virtual GetGlobalMonitoringOverrideCommand SetParameters(GetGlobalMonitoringOverrideCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002A8 RID: 680
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170016D4 RID: 5844
			// (set) Token: 0x0600306C RID: 12396 RVA: 0x00056D00 File Offset: 0x00054F00
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170016D5 RID: 5845
			// (set) Token: 0x0600306D RID: 12397 RVA: 0x00056D13 File Offset: 0x00054F13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016D6 RID: 5846
			// (set) Token: 0x0600306E RID: 12398 RVA: 0x00056D2B File Offset: 0x00054F2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016D7 RID: 5847
			// (set) Token: 0x0600306F RID: 12399 RVA: 0x00056D43 File Offset: 0x00054F43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016D8 RID: 5848
			// (set) Token: 0x06003070 RID: 12400 RVA: 0x00056D5B File Offset: 0x00054F5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
