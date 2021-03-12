using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000034 RID: 52
	public class GetActiveSyncDeviceClassCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceClass, ActiveSyncDeviceClass>
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0003414F File Offset: 0x0003234F
		private GetActiveSyncDeviceClassCommand() : base("Get-ActiveSyncDeviceClass")
		{
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0003415C File Offset: 0x0003235C
		public GetActiveSyncDeviceClassCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0003416B File Offset: 0x0003236B
		public virtual GetActiveSyncDeviceClassCommand SetParameters(GetActiveSyncDeviceClassCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00034175 File Offset: 0x00032375
		public virtual GetActiveSyncDeviceClassCommand SetParameters(GetActiveSyncDeviceClassCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000035 RID: 53
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700012A RID: 298
			// (set) Token: 0x060015DD RID: 5597 RVA: 0x0003417F File Offset: 0x0003237F
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700012B RID: 299
			// (set) Token: 0x060015DE RID: 5598 RVA: 0x00034192 File Offset: 0x00032392
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700012C RID: 300
			// (set) Token: 0x060015DF RID: 5599 RVA: 0x000341A5 File Offset: 0x000323A5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700012D RID: 301
			// (set) Token: 0x060015E0 RID: 5600 RVA: 0x000341C3 File Offset: 0x000323C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700012E RID: 302
			// (set) Token: 0x060015E1 RID: 5601 RVA: 0x000341D6 File Offset: 0x000323D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700012F RID: 303
			// (set) Token: 0x060015E2 RID: 5602 RVA: 0x000341EE File Offset: 0x000323EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000130 RID: 304
			// (set) Token: 0x060015E3 RID: 5603 RVA: 0x00034206 File Offset: 0x00032406
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000131 RID: 305
			// (set) Token: 0x060015E4 RID: 5604 RVA: 0x0003421E File Offset: 0x0003241E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000036 RID: 54
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000132 RID: 306
			// (set) Token: 0x060015E6 RID: 5606 RVA: 0x0003423E File Offset: 0x0003243E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceClassIdParameter(value) : null);
				}
			}

			// Token: 0x17000133 RID: 307
			// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0003425C File Offset: 0x0003245C
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17000134 RID: 308
			// (set) Token: 0x060015E8 RID: 5608 RVA: 0x0003426F File Offset: 0x0003246F
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17000135 RID: 309
			// (set) Token: 0x060015E9 RID: 5609 RVA: 0x00034282 File Offset: 0x00032482
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000136 RID: 310
			// (set) Token: 0x060015EA RID: 5610 RVA: 0x000342A0 File Offset: 0x000324A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000137 RID: 311
			// (set) Token: 0x060015EB RID: 5611 RVA: 0x000342B3 File Offset: 0x000324B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000138 RID: 312
			// (set) Token: 0x060015EC RID: 5612 RVA: 0x000342CB File Offset: 0x000324CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000139 RID: 313
			// (set) Token: 0x060015ED RID: 5613 RVA: 0x000342E3 File Offset: 0x000324E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700013A RID: 314
			// (set) Token: 0x060015EE RID: 5614 RVA: 0x000342FB File Offset: 0x000324FB
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
