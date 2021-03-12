using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001D3 RID: 467
	public class GetRoleAssignmentPolicyCommand : SyntheticCommandWithPipelineInput<RoleAssignmentPolicy, RoleAssignmentPolicy>
	{
		// Token: 0x06002691 RID: 9873 RVA: 0x00049B63 File Offset: 0x00047D63
		private GetRoleAssignmentPolicyCommand() : base("Get-RoleAssignmentPolicy")
		{
		}

		// Token: 0x06002692 RID: 9874 RVA: 0x00049B70 File Offset: 0x00047D70
		public GetRoleAssignmentPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002693 RID: 9875 RVA: 0x00049B7F File Offset: 0x00047D7F
		public virtual GetRoleAssignmentPolicyCommand SetParameters(GetRoleAssignmentPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x00049B89 File Offset: 0x00047D89
		public virtual GetRoleAssignmentPolicyCommand SetParameters(GetRoleAssignmentPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001D4 RID: 468
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000EA4 RID: 3748
			// (set) Token: 0x06002695 RID: 9877 RVA: 0x00049B93 File Offset: 0x00047D93
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000EA5 RID: 3749
			// (set) Token: 0x06002696 RID: 9878 RVA: 0x00049BAB File Offset: 0x00047DAB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000EA6 RID: 3750
			// (set) Token: 0x06002697 RID: 9879 RVA: 0x00049BC9 File Offset: 0x00047DC9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000EA7 RID: 3751
			// (set) Token: 0x06002698 RID: 9880 RVA: 0x00049BDC File Offset: 0x00047DDC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000EA8 RID: 3752
			// (set) Token: 0x06002699 RID: 9881 RVA: 0x00049BF4 File Offset: 0x00047DF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000EA9 RID: 3753
			// (set) Token: 0x0600269A RID: 9882 RVA: 0x00049C0C File Offset: 0x00047E0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000EAA RID: 3754
			// (set) Token: 0x0600269B RID: 9883 RVA: 0x00049C24 File Offset: 0x00047E24
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020001D5 RID: 469
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000EAB RID: 3755
			// (set) Token: 0x0600269D RID: 9885 RVA: 0x00049C44 File Offset: 0x00047E44
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000EAC RID: 3756
			// (set) Token: 0x0600269E RID: 9886 RVA: 0x00049C62 File Offset: 0x00047E62
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000EAD RID: 3757
			// (set) Token: 0x0600269F RID: 9887 RVA: 0x00049C7A File Offset: 0x00047E7A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000EAE RID: 3758
			// (set) Token: 0x060026A0 RID: 9888 RVA: 0x00049C98 File Offset: 0x00047E98
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000EAF RID: 3759
			// (set) Token: 0x060026A1 RID: 9889 RVA: 0x00049CAB File Offset: 0x00047EAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000EB0 RID: 3760
			// (set) Token: 0x060026A2 RID: 9890 RVA: 0x00049CC3 File Offset: 0x00047EC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000EB1 RID: 3761
			// (set) Token: 0x060026A3 RID: 9891 RVA: 0x00049CDB File Offset: 0x00047EDB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000EB2 RID: 3762
			// (set) Token: 0x060026A4 RID: 9892 RVA: 0x00049CF3 File Offset: 0x00047EF3
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
