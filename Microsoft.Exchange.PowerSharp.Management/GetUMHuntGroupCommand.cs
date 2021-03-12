using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B4E RID: 2894
	public class GetUMHuntGroupCommand : SyntheticCommandWithPipelineInput<UMHuntGroup, UMHuntGroup>
	{
		// Token: 0x06008CD7 RID: 36055 RVA: 0x000CE88D File Offset: 0x000CCA8D
		private GetUMHuntGroupCommand() : base("Get-UMHuntGroup")
		{
		}

		// Token: 0x06008CD8 RID: 36056 RVA: 0x000CE89A File Offset: 0x000CCA9A
		public GetUMHuntGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CD9 RID: 36057 RVA: 0x000CE8A9 File Offset: 0x000CCAA9
		public virtual GetUMHuntGroupCommand SetParameters(GetUMHuntGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008CDA RID: 36058 RVA: 0x000CE8B3 File Offset: 0x000CCAB3
		public virtual GetUMHuntGroupCommand SetParameters(GetUMHuntGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B4F RID: 2895
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061F4 RID: 25076
			// (set) Token: 0x06008CDB RID: 36059 RVA: 0x000CE8BD File Offset: 0x000CCABD
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061F5 RID: 25077
			// (set) Token: 0x06008CDC RID: 36060 RVA: 0x000CE8DB File Offset: 0x000CCADB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061F6 RID: 25078
			// (set) Token: 0x06008CDD RID: 36061 RVA: 0x000CE8F9 File Offset: 0x000CCAF9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061F7 RID: 25079
			// (set) Token: 0x06008CDE RID: 36062 RVA: 0x000CE90C File Offset: 0x000CCB0C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061F8 RID: 25080
			// (set) Token: 0x06008CDF RID: 36063 RVA: 0x000CE924 File Offset: 0x000CCB24
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061F9 RID: 25081
			// (set) Token: 0x06008CE0 RID: 36064 RVA: 0x000CE93C File Offset: 0x000CCB3C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061FA RID: 25082
			// (set) Token: 0x06008CE1 RID: 36065 RVA: 0x000CE954 File Offset: 0x000CCB54
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B50 RID: 2896
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170061FB RID: 25083
			// (set) Token: 0x06008CE3 RID: 36067 RVA: 0x000CE974 File Offset: 0x000CCB74
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMHuntGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170061FC RID: 25084
			// (set) Token: 0x06008CE4 RID: 36068 RVA: 0x000CE992 File Offset: 0x000CCB92
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061FD RID: 25085
			// (set) Token: 0x06008CE5 RID: 36069 RVA: 0x000CE9B0 File Offset: 0x000CCBB0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061FE RID: 25086
			// (set) Token: 0x06008CE6 RID: 36070 RVA: 0x000CE9CE File Offset: 0x000CCBCE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061FF RID: 25087
			// (set) Token: 0x06008CE7 RID: 36071 RVA: 0x000CE9E1 File Offset: 0x000CCBE1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006200 RID: 25088
			// (set) Token: 0x06008CE8 RID: 36072 RVA: 0x000CE9F9 File Offset: 0x000CCBF9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006201 RID: 25089
			// (set) Token: 0x06008CE9 RID: 36073 RVA: 0x000CEA11 File Offset: 0x000CCC11
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006202 RID: 25090
			// (set) Token: 0x06008CEA RID: 36074 RVA: 0x000CEA29 File Offset: 0x000CCC29
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
