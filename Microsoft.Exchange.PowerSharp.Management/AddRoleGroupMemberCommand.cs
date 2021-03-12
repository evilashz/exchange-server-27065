using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000342 RID: 834
	public class AddRoleGroupMemberCommand : SyntheticCommandWithPipelineInputNoOutput<SecurityPrincipalIdParameter>
	{
		// Token: 0x06003633 RID: 13875 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		private AddRoleGroupMemberCommand() : base("Add-RoleGroupMember")
		{
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x0005E2ED File Offset: 0x0005C4ED
		public AddRoleGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x0005E2FC File Offset: 0x0005C4FC
		public virtual AddRoleGroupMemberCommand SetParameters(AddRoleGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0005E306 File Offset: 0x0005C506
		public virtual AddRoleGroupMemberCommand SetParameters(AddRoleGroupMemberCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000343 RID: 835
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001B68 RID: 7016
			// (set) Token: 0x06003637 RID: 13879 RVA: 0x0005E310 File Offset: 0x0005C510
			public virtual string Member
			{
				set
				{
					base.PowerSharpParameters["Member"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001B69 RID: 7017
			// (set) Token: 0x06003638 RID: 13880 RVA: 0x0005E32E File Offset: 0x0005C52E
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B6A RID: 7018
			// (set) Token: 0x06003639 RID: 13881 RVA: 0x0005E346 File Offset: 0x0005C546
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B6B RID: 7019
			// (set) Token: 0x0600363A RID: 13882 RVA: 0x0005E35E File Offset: 0x0005C55E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B6C RID: 7020
			// (set) Token: 0x0600363B RID: 13883 RVA: 0x0005E371 File Offset: 0x0005C571
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B6D RID: 7021
			// (set) Token: 0x0600363C RID: 13884 RVA: 0x0005E389 File Offset: 0x0005C589
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B6E RID: 7022
			// (set) Token: 0x0600363D RID: 13885 RVA: 0x0005E3A1 File Offset: 0x0005C5A1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B6F RID: 7023
			// (set) Token: 0x0600363E RID: 13886 RVA: 0x0005E3B9 File Offset: 0x0005C5B9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B70 RID: 7024
			// (set) Token: 0x0600363F RID: 13887 RVA: 0x0005E3D1 File Offset: 0x0005C5D1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000344 RID: 836
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001B71 RID: 7025
			// (set) Token: 0x06003641 RID: 13889 RVA: 0x0005E3F1 File Offset: 0x0005C5F1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B72 RID: 7026
			// (set) Token: 0x06003642 RID: 13890 RVA: 0x0005E40F File Offset: 0x0005C60F
			public virtual string Member
			{
				set
				{
					base.PowerSharpParameters["Member"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001B73 RID: 7027
			// (set) Token: 0x06003643 RID: 13891 RVA: 0x0005E42D File Offset: 0x0005C62D
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B74 RID: 7028
			// (set) Token: 0x06003644 RID: 13892 RVA: 0x0005E445 File Offset: 0x0005C645
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B75 RID: 7029
			// (set) Token: 0x06003645 RID: 13893 RVA: 0x0005E45D File Offset: 0x0005C65D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B76 RID: 7030
			// (set) Token: 0x06003646 RID: 13894 RVA: 0x0005E470 File Offset: 0x0005C670
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B77 RID: 7031
			// (set) Token: 0x06003647 RID: 13895 RVA: 0x0005E488 File Offset: 0x0005C688
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B78 RID: 7032
			// (set) Token: 0x06003648 RID: 13896 RVA: 0x0005E4A0 File Offset: 0x0005C6A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B79 RID: 7033
			// (set) Token: 0x06003649 RID: 13897 RVA: 0x0005E4B8 File Offset: 0x0005C6B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B7A RID: 7034
			// (set) Token: 0x0600364A RID: 13898 RVA: 0x0005E4D0 File Offset: 0x0005C6D0
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
