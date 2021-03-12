using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001D6 RID: 470
	public class SetRoleAssignmentPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<RoleAssignmentPolicy>
	{
		// Token: 0x060026A6 RID: 9894 RVA: 0x00049D13 File Offset: 0x00047F13
		private SetRoleAssignmentPolicyCommand() : base("Set-RoleAssignmentPolicy")
		{
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x00049D20 File Offset: 0x00047F20
		public SetRoleAssignmentPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00049D2F File Offset: 0x00047F2F
		public virtual SetRoleAssignmentPolicyCommand SetParameters(SetRoleAssignmentPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00049D39 File Offset: 0x00047F39
		public virtual SetRoleAssignmentPolicyCommand SetParameters(SetRoleAssignmentPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001D7 RID: 471
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000EB3 RID: 3763
			// (set) Token: 0x060026AA RID: 9898 RVA: 0x00049D43 File Offset: 0x00047F43
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000EB4 RID: 3764
			// (set) Token: 0x060026AB RID: 9899 RVA: 0x00049D5B File Offset: 0x00047F5B
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17000EB5 RID: 3765
			// (set) Token: 0x060026AC RID: 9900 RVA: 0x00049D6E File Offset: 0x00047F6E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000EB6 RID: 3766
			// (set) Token: 0x060026AD RID: 9901 RVA: 0x00049D81 File Offset: 0x00047F81
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000EB7 RID: 3767
			// (set) Token: 0x060026AE RID: 9902 RVA: 0x00049D94 File Offset: 0x00047F94
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000EB8 RID: 3768
			// (set) Token: 0x060026AF RID: 9903 RVA: 0x00049DAC File Offset: 0x00047FAC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000EB9 RID: 3769
			// (set) Token: 0x060026B0 RID: 9904 RVA: 0x00049DC4 File Offset: 0x00047FC4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000EBA RID: 3770
			// (set) Token: 0x060026B1 RID: 9905 RVA: 0x00049DDC File Offset: 0x00047FDC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000EBB RID: 3771
			// (set) Token: 0x060026B2 RID: 9906 RVA: 0x00049DF4 File Offset: 0x00047FF4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001D8 RID: 472
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000EBC RID: 3772
			// (set) Token: 0x060026B4 RID: 9908 RVA: 0x00049E14 File Offset: 0x00048014
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000EBD RID: 3773
			// (set) Token: 0x060026B5 RID: 9909 RVA: 0x00049E32 File Offset: 0x00048032
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000EBE RID: 3774
			// (set) Token: 0x060026B6 RID: 9910 RVA: 0x00049E4A File Offset: 0x0004804A
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17000EBF RID: 3775
			// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00049E5D File Offset: 0x0004805D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000EC0 RID: 3776
			// (set) Token: 0x060026B8 RID: 9912 RVA: 0x00049E70 File Offset: 0x00048070
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000EC1 RID: 3777
			// (set) Token: 0x060026B9 RID: 9913 RVA: 0x00049E83 File Offset: 0x00048083
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000EC2 RID: 3778
			// (set) Token: 0x060026BA RID: 9914 RVA: 0x00049E9B File Offset: 0x0004809B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000EC3 RID: 3779
			// (set) Token: 0x060026BB RID: 9915 RVA: 0x00049EB3 File Offset: 0x000480B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000EC4 RID: 3780
			// (set) Token: 0x060026BC RID: 9916 RVA: 0x00049ECB File Offset: 0x000480CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000EC5 RID: 3781
			// (set) Token: 0x060026BD RID: 9917 RVA: 0x00049EE3 File Offset: 0x000480E3
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
