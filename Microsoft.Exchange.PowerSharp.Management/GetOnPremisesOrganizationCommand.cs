using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000803 RID: 2051
	public class GetOnPremisesOrganizationCommand : SyntheticCommandWithPipelineInput<OnPremisesOrganization, OnPremisesOrganization>
	{
		// Token: 0x060065B3 RID: 26035 RVA: 0x0009B501 File Offset: 0x00099701
		private GetOnPremisesOrganizationCommand() : base("Get-OnPremisesOrganization")
		{
		}

		// Token: 0x060065B4 RID: 26036 RVA: 0x0009B50E File Offset: 0x0009970E
		public GetOnPremisesOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060065B5 RID: 26037 RVA: 0x0009B51D File Offset: 0x0009971D
		public virtual GetOnPremisesOrganizationCommand SetParameters(GetOnPremisesOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060065B6 RID: 26038 RVA: 0x0009B527 File Offset: 0x00099727
		public virtual GetOnPremisesOrganizationCommand SetParameters(GetOnPremisesOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000804 RID: 2052
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004166 RID: 16742
			// (set) Token: 0x060065B7 RID: 26039 RVA: 0x0009B531 File Offset: 0x00099731
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004167 RID: 16743
			// (set) Token: 0x060065B8 RID: 26040 RVA: 0x0009B54F File Offset: 0x0009974F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004168 RID: 16744
			// (set) Token: 0x060065B9 RID: 26041 RVA: 0x0009B562 File Offset: 0x00099762
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004169 RID: 16745
			// (set) Token: 0x060065BA RID: 26042 RVA: 0x0009B57A File Offset: 0x0009977A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700416A RID: 16746
			// (set) Token: 0x060065BB RID: 26043 RVA: 0x0009B592 File Offset: 0x00099792
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700416B RID: 16747
			// (set) Token: 0x060065BC RID: 26044 RVA: 0x0009B5AA File Offset: 0x000997AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000805 RID: 2053
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700416C RID: 16748
			// (set) Token: 0x060065BE RID: 26046 RVA: 0x0009B5CA File Offset: 0x000997CA
			public virtual OnPremisesOrganizationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700416D RID: 16749
			// (set) Token: 0x060065BF RID: 26047 RVA: 0x0009B5DD File Offset: 0x000997DD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700416E RID: 16750
			// (set) Token: 0x060065C0 RID: 26048 RVA: 0x0009B5FB File Offset: 0x000997FB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700416F RID: 16751
			// (set) Token: 0x060065C1 RID: 26049 RVA: 0x0009B60E File Offset: 0x0009980E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004170 RID: 16752
			// (set) Token: 0x060065C2 RID: 26050 RVA: 0x0009B626 File Offset: 0x00099826
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004171 RID: 16753
			// (set) Token: 0x060065C3 RID: 26051 RVA: 0x0009B63E File Offset: 0x0009983E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004172 RID: 16754
			// (set) Token: 0x060065C4 RID: 26052 RVA: 0x0009B656 File Offset: 0x00099856
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
