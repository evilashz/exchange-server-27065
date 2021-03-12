using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001CE RID: 462
	public class NewRoleAssignmentPolicyCommand : SyntheticCommandWithPipelineInput<RoleAssignmentPolicy, RoleAssignmentPolicy>
	{
		// Token: 0x0600266C RID: 9836 RVA: 0x0004987F File Offset: 0x00047A7F
		private NewRoleAssignmentPolicyCommand() : base("New-RoleAssignmentPolicy")
		{
		}

		// Token: 0x0600266D RID: 9837 RVA: 0x0004988C File Offset: 0x00047A8C
		public NewRoleAssignmentPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600266E RID: 9838 RVA: 0x0004989B File Offset: 0x00047A9B
		public virtual NewRoleAssignmentPolicyCommand SetParameters(NewRoleAssignmentPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001CF RID: 463
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E89 RID: 3721
			// (set) Token: 0x0600266F RID: 9839 RVA: 0x000498A5 File Offset: 0x00047AA5
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17000E8A RID: 3722
			// (set) Token: 0x06002670 RID: 9840 RVA: 0x000498B8 File Offset: 0x00047AB8
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000E8B RID: 3723
			// (set) Token: 0x06002671 RID: 9841 RVA: 0x000498D0 File Offset: 0x00047AD0
			public virtual RoleIdParameter Roles
			{
				set
				{
					base.PowerSharpParameters["Roles"] = value;
				}
			}

			// Token: 0x17000E8C RID: 3724
			// (set) Token: 0x06002672 RID: 9842 RVA: 0x000498E3 File Offset: 0x00047AE3
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E8D RID: 3725
			// (set) Token: 0x06002673 RID: 9843 RVA: 0x000498FB File Offset: 0x00047AFB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000E8E RID: 3726
			// (set) Token: 0x06002674 RID: 9844 RVA: 0x00049919 File Offset: 0x00047B19
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E8F RID: 3727
			// (set) Token: 0x06002675 RID: 9845 RVA: 0x0004992C File Offset: 0x00047B2C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E90 RID: 3728
			// (set) Token: 0x06002676 RID: 9846 RVA: 0x0004993F File Offset: 0x00047B3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E91 RID: 3729
			// (set) Token: 0x06002677 RID: 9847 RVA: 0x00049957 File Offset: 0x00047B57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E92 RID: 3730
			// (set) Token: 0x06002678 RID: 9848 RVA: 0x0004996F File Offset: 0x00047B6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E93 RID: 3731
			// (set) Token: 0x06002679 RID: 9849 RVA: 0x00049987 File Offset: 0x00047B87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E94 RID: 3732
			// (set) Token: 0x0600267A RID: 9850 RVA: 0x0004999F File Offset: 0x00047B9F
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
