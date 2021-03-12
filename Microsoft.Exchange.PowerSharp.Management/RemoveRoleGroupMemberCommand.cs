using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000348 RID: 840
	public class RemoveRoleGroupMemberCommand : SyntheticCommandWithPipelineInputNoOutput<SecurityPrincipalIdParameter>
	{
		// Token: 0x06003663 RID: 13923 RVA: 0x0005E6C4 File Offset: 0x0005C8C4
		private RemoveRoleGroupMemberCommand() : base("Remove-RoleGroupMember")
		{
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x0005E6D1 File Offset: 0x0005C8D1
		public RemoveRoleGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		public virtual RemoveRoleGroupMemberCommand SetParameters(RemoveRoleGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0005E6EA File Offset: 0x0005C8EA
		public virtual RemoveRoleGroupMemberCommand SetParameters(RemoveRoleGroupMemberCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000349 RID: 841
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001B8C RID: 7052
			// (set) Token: 0x06003667 RID: 13927 RVA: 0x0005E6F4 File Offset: 0x0005C8F4
			public virtual string Member
			{
				set
				{
					base.PowerSharpParameters["Member"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001B8D RID: 7053
			// (set) Token: 0x06003668 RID: 13928 RVA: 0x0005E712 File Offset: 0x0005C912
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B8E RID: 7054
			// (set) Token: 0x06003669 RID: 13929 RVA: 0x0005E72A File Offset: 0x0005C92A
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B8F RID: 7055
			// (set) Token: 0x0600366A RID: 13930 RVA: 0x0005E742 File Offset: 0x0005C942
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B90 RID: 7056
			// (set) Token: 0x0600366B RID: 13931 RVA: 0x0005E755 File Offset: 0x0005C955
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B91 RID: 7057
			// (set) Token: 0x0600366C RID: 13932 RVA: 0x0005E76D File Offset: 0x0005C96D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B92 RID: 7058
			// (set) Token: 0x0600366D RID: 13933 RVA: 0x0005E785 File Offset: 0x0005C985
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B93 RID: 7059
			// (set) Token: 0x0600366E RID: 13934 RVA: 0x0005E79D File Offset: 0x0005C99D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B94 RID: 7060
			// (set) Token: 0x0600366F RID: 13935 RVA: 0x0005E7B5 File Offset: 0x0005C9B5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001B95 RID: 7061
			// (set) Token: 0x06003670 RID: 13936 RVA: 0x0005E7CD File Offset: 0x0005C9CD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200034A RID: 842
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001B96 RID: 7062
			// (set) Token: 0x06003672 RID: 13938 RVA: 0x0005E7ED File Offset: 0x0005C9ED
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupIdParameter(value) : null);
				}
			}

			// Token: 0x17001B97 RID: 7063
			// (set) Token: 0x06003673 RID: 13939 RVA: 0x0005E80B File Offset: 0x0005CA0B
			public virtual string Member
			{
				set
				{
					base.PowerSharpParameters["Member"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17001B98 RID: 7064
			// (set) Token: 0x06003674 RID: 13940 RVA: 0x0005E829 File Offset: 0x0005CA29
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B99 RID: 7065
			// (set) Token: 0x06003675 RID: 13941 RVA: 0x0005E841 File Offset: 0x0005CA41
			public virtual SwitchParameter BypassSecurityGroupManagerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassSecurityGroupManagerCheck"] = value;
				}
			}

			// Token: 0x17001B9A RID: 7066
			// (set) Token: 0x06003676 RID: 13942 RVA: 0x0005E859 File Offset: 0x0005CA59
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B9B RID: 7067
			// (set) Token: 0x06003677 RID: 13943 RVA: 0x0005E86C File Offset: 0x0005CA6C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B9C RID: 7068
			// (set) Token: 0x06003678 RID: 13944 RVA: 0x0005E884 File Offset: 0x0005CA84
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B9D RID: 7069
			// (set) Token: 0x06003679 RID: 13945 RVA: 0x0005E89C File Offset: 0x0005CA9C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B9E RID: 7070
			// (set) Token: 0x0600367A RID: 13946 RVA: 0x0005E8B4 File Offset: 0x0005CAB4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001B9F RID: 7071
			// (set) Token: 0x0600367B RID: 13947 RVA: 0x0005E8CC File Offset: 0x0005CACC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001BA0 RID: 7072
			// (set) Token: 0x0600367C RID: 13948 RVA: 0x0005E8E4 File Offset: 0x0005CAE4
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
