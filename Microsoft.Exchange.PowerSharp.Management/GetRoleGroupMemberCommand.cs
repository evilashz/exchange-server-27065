using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000345 RID: 837
	public class GetRoleGroupMemberCommand : SyntheticCommandWithPipelineInput<ReducedRecipient, ReducedRecipient>
	{
		// Token: 0x0600364C RID: 13900 RVA: 0x0005E4F0 File Offset: 0x0005C6F0
		private GetRoleGroupMemberCommand() : base("Get-RoleGroupMember")
		{
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0005E4FD File Offset: 0x0005C6FD
		public GetRoleGroupMemberCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x0005E50C File Offset: 0x0005C70C
		public virtual GetRoleGroupMemberCommand SetParameters(GetRoleGroupMemberCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0005E516 File Offset: 0x0005C716
		public virtual GetRoleGroupMemberCommand SetParameters(GetRoleGroupMemberCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000346 RID: 838
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001B7B RID: 7035
			// (set) Token: 0x06003650 RID: 13904 RVA: 0x0005E520 File Offset: 0x0005C720
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleGroupMemberIdParameter(value) : null);
				}
			}

			// Token: 0x17001B7C RID: 7036
			// (set) Token: 0x06003651 RID: 13905 RVA: 0x0005E53E File Offset: 0x0005C73E
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B7D RID: 7037
			// (set) Token: 0x06003652 RID: 13906 RVA: 0x0005E556 File Offset: 0x0005C756
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001B7E RID: 7038
			// (set) Token: 0x06003653 RID: 13907 RVA: 0x0005E56E File Offset: 0x0005C76E
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001B7F RID: 7039
			// (set) Token: 0x06003654 RID: 13908 RVA: 0x0005E586 File Offset: 0x0005C786
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B80 RID: 7040
			// (set) Token: 0x06003655 RID: 13909 RVA: 0x0005E599 File Offset: 0x0005C799
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B81 RID: 7041
			// (set) Token: 0x06003656 RID: 13910 RVA: 0x0005E5B1 File Offset: 0x0005C7B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B82 RID: 7042
			// (set) Token: 0x06003657 RID: 13911 RVA: 0x0005E5C9 File Offset: 0x0005C7C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B83 RID: 7043
			// (set) Token: 0x06003658 RID: 13912 RVA: 0x0005E5E1 File Offset: 0x0005C7E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000347 RID: 839
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001B84 RID: 7044
			// (set) Token: 0x0600365A RID: 13914 RVA: 0x0005E601 File Offset: 0x0005C801
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17001B85 RID: 7045
			// (set) Token: 0x0600365B RID: 13915 RVA: 0x0005E619 File Offset: 0x0005C819
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001B86 RID: 7046
			// (set) Token: 0x0600365C RID: 13916 RVA: 0x0005E631 File Offset: 0x0005C831
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17001B87 RID: 7047
			// (set) Token: 0x0600365D RID: 13917 RVA: 0x0005E649 File Offset: 0x0005C849
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001B88 RID: 7048
			// (set) Token: 0x0600365E RID: 13918 RVA: 0x0005E65C File Offset: 0x0005C85C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001B89 RID: 7049
			// (set) Token: 0x0600365F RID: 13919 RVA: 0x0005E674 File Offset: 0x0005C874
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001B8A RID: 7050
			// (set) Token: 0x06003660 RID: 13920 RVA: 0x0005E68C File Offset: 0x0005C88C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001B8B RID: 7051
			// (set) Token: 0x06003661 RID: 13921 RVA: 0x0005E6A4 File Offset: 0x0005C8A4
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
