using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B4B RID: 2891
	public class GetUMDialPlanCommand : SyntheticCommandWithPipelineInput<UMDialPlan, UMDialPlan>
	{
		// Token: 0x06008CC4 RID: 36036 RVA: 0x000CE70D File Offset: 0x000CC90D
		private GetUMDialPlanCommand() : base("Get-UMDialPlan")
		{
		}

		// Token: 0x06008CC5 RID: 36037 RVA: 0x000CE71A File Offset: 0x000CC91A
		public GetUMDialPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CC6 RID: 36038 RVA: 0x000CE729 File Offset: 0x000CC929
		public virtual GetUMDialPlanCommand SetParameters(GetUMDialPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008CC7 RID: 36039 RVA: 0x000CE733 File Offset: 0x000CC933
		public virtual GetUMDialPlanCommand SetParameters(GetUMDialPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B4C RID: 2892
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061E7 RID: 25063
			// (set) Token: 0x06008CC8 RID: 36040 RVA: 0x000CE73D File Offset: 0x000CC93D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061E8 RID: 25064
			// (set) Token: 0x06008CC9 RID: 36041 RVA: 0x000CE75B File Offset: 0x000CC95B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061E9 RID: 25065
			// (set) Token: 0x06008CCA RID: 36042 RVA: 0x000CE76E File Offset: 0x000CC96E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061EA RID: 25066
			// (set) Token: 0x06008CCB RID: 36043 RVA: 0x000CE786 File Offset: 0x000CC986
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061EB RID: 25067
			// (set) Token: 0x06008CCC RID: 36044 RVA: 0x000CE79E File Offset: 0x000CC99E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061EC RID: 25068
			// (set) Token: 0x06008CCD RID: 36045 RVA: 0x000CE7B6 File Offset: 0x000CC9B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B4D RID: 2893
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170061ED RID: 25069
			// (set) Token: 0x06008CCF RID: 36047 RVA: 0x000CE7D6 File Offset: 0x000CC9D6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170061EE RID: 25070
			// (set) Token: 0x06008CD0 RID: 36048 RVA: 0x000CE7F4 File Offset: 0x000CC9F4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061EF RID: 25071
			// (set) Token: 0x06008CD1 RID: 36049 RVA: 0x000CE812 File Offset: 0x000CCA12
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061F0 RID: 25072
			// (set) Token: 0x06008CD2 RID: 36050 RVA: 0x000CE825 File Offset: 0x000CCA25
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061F1 RID: 25073
			// (set) Token: 0x06008CD3 RID: 36051 RVA: 0x000CE83D File Offset: 0x000CCA3D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061F2 RID: 25074
			// (set) Token: 0x06008CD4 RID: 36052 RVA: 0x000CE855 File Offset: 0x000CCA55
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061F3 RID: 25075
			// (set) Token: 0x06008CD5 RID: 36053 RVA: 0x000CE86D File Offset: 0x000CCA6D
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
