using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200002E RID: 46
	public class GetActiveSyncDeviceAccessRuleCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceAccessRule, ActiveSyncDeviceAccessRule>
	{
		// Token: 0x060015B5 RID: 5557 RVA: 0x00033E8B File Offset: 0x0003208B
		private GetActiveSyncDeviceAccessRuleCommand() : base("Get-ActiveSyncDeviceAccessRule")
		{
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00033E98 File Offset: 0x00032098
		public GetActiveSyncDeviceAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00033EA7 File Offset: 0x000320A7
		public virtual GetActiveSyncDeviceAccessRuleCommand SetParameters(GetActiveSyncDeviceAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00033EB1 File Offset: 0x000320B1
		public virtual GetActiveSyncDeviceAccessRuleCommand SetParameters(GetActiveSyncDeviceAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200002F RID: 47
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000112 RID: 274
			// (set) Token: 0x060015B9 RID: 5561 RVA: 0x00033EBB File Offset: 0x000320BB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000113 RID: 275
			// (set) Token: 0x060015BA RID: 5562 RVA: 0x00033ED9 File Offset: 0x000320D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000114 RID: 276
			// (set) Token: 0x060015BB RID: 5563 RVA: 0x00033EEC File Offset: 0x000320EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000115 RID: 277
			// (set) Token: 0x060015BC RID: 5564 RVA: 0x00033F04 File Offset: 0x00032104
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000116 RID: 278
			// (set) Token: 0x060015BD RID: 5565 RVA: 0x00033F1C File Offset: 0x0003211C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000117 RID: 279
			// (set) Token: 0x060015BE RID: 5566 RVA: 0x00033F34 File Offset: 0x00032134
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000030 RID: 48
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000118 RID: 280
			// (set) Token: 0x060015C0 RID: 5568 RVA: 0x00033F54 File Offset: 0x00032154
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17000119 RID: 281
			// (set) Token: 0x060015C1 RID: 5569 RVA: 0x00033F72 File Offset: 0x00032172
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700011A RID: 282
			// (set) Token: 0x060015C2 RID: 5570 RVA: 0x00033F90 File Offset: 0x00032190
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700011B RID: 283
			// (set) Token: 0x060015C3 RID: 5571 RVA: 0x00033FA3 File Offset: 0x000321A3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700011C RID: 284
			// (set) Token: 0x060015C4 RID: 5572 RVA: 0x00033FBB File Offset: 0x000321BB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700011D RID: 285
			// (set) Token: 0x060015C5 RID: 5573 RVA: 0x00033FD3 File Offset: 0x000321D3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700011E RID: 286
			// (set) Token: 0x060015C6 RID: 5574 RVA: 0x00033FEB File Offset: 0x000321EB
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
