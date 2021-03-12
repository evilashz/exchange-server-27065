using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200034B RID: 843
	public class UpdateRoleGroupMemberCommand : SyntheticCommandWithPipelineInputNoOutput<RoleGroupIdParameter>
	{
		// Token: 0x0600367E RID: 13950 RVA: 0x0005E904 File Offset: 0x0005CB04
		private UpdateRoleGroupMemberCommand() : base("Update-RoleGroupMember")
		{
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x0005E911 File Offset: 0x0005CB11
		public UpdateRoleGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x0005E920 File Offset: 0x0005CB20
		public virtual UpdateRoleGroupMemberCommand SetParameters(UpdateRoleGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x0005E92A File Offset: 0x0005CB2A
		public virtual UpdateRoleGroupMemberCommand SetParameters(UpdateRoleGroupMemberCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200034C RID: 844
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001BA1 RID: 7073
			// (set) Token: 0x06003682 RID: 13954 RVA: 0x0005E934 File Offset: 0x0005CB34
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17001BA2 RID: 7074
			// (set) Token: 0x06003683 RID: 13955 RVA: 0x0005E947 File Offset: 0x0005CB47
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001BA3 RID: 7075
			// (set) Token: 0x06003684 RID: 13956 RVA: 0x0005E95F File Offset: 0x0005CB5F
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001BA4 RID: 7076
			// (set) Token: 0x06003685 RID: 13957 RVA: 0x0005E977 File Offset: 0x0005CB77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BA5 RID: 7077
			// (set) Token: 0x06003686 RID: 13958 RVA: 0x0005E98A File Offset: 0x0005CB8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BA6 RID: 7078
			// (set) Token: 0x06003687 RID: 13959 RVA: 0x0005E9A2 File Offset: 0x0005CBA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BA7 RID: 7079
			// (set) Token: 0x06003688 RID: 13960 RVA: 0x0005E9BA File Offset: 0x0005CBBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BA8 RID: 7080
			// (set) Token: 0x06003689 RID: 13961 RVA: 0x0005E9D2 File Offset: 0x0005CBD2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001BA9 RID: 7081
			// (set) Token: 0x0600368A RID: 13962 RVA: 0x0005E9EA File Offset: 0x0005CBEA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001BAA RID: 7082
			// (set) Token: 0x0600368B RID: 13963 RVA: 0x0005EA02 File Offset: 0x0005CC02
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200034D RID: 845
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001BAB RID: 7083
			// (set) Token: 0x0600368D RID: 13965 RVA: 0x0005EA22 File Offset: 0x0005CC22
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001BAC RID: 7084
			// (set) Token: 0x0600368E RID: 13966 RVA: 0x0005EA40 File Offset: 0x0005CC40
			public virtual MultiValuedProperty<SecurityPrincipalIdParameter> Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x17001BAD RID: 7085
			// (set) Token: 0x0600368F RID: 13967 RVA: 0x0005EA53 File Offset: 0x0005CC53
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001BAE RID: 7086
			// (set) Token: 0x06003690 RID: 13968 RVA: 0x0005EA6B File Offset: 0x0005CC6B
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001BAF RID: 7087
			// (set) Token: 0x06003691 RID: 13969 RVA: 0x0005EA83 File Offset: 0x0005CC83
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001BB0 RID: 7088
			// (set) Token: 0x06003692 RID: 13970 RVA: 0x0005EA96 File Offset: 0x0005CC96
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001BB1 RID: 7089
			// (set) Token: 0x06003693 RID: 13971 RVA: 0x0005EAAE File Offset: 0x0005CCAE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001BB2 RID: 7090
			// (set) Token: 0x06003694 RID: 13972 RVA: 0x0005EAC6 File Offset: 0x0005CCC6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001BB3 RID: 7091
			// (set) Token: 0x06003695 RID: 13973 RVA: 0x0005EADE File Offset: 0x0005CCDE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001BB4 RID: 7092
			// (set) Token: 0x06003696 RID: 13974 RVA: 0x0005EAF6 File Offset: 0x0005CCF6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001BB5 RID: 7093
			// (set) Token: 0x06003697 RID: 13975 RVA: 0x0005EB0E File Offset: 0x0005CD0E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
