using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001D0 RID: 464
	public class RemoveRoleAssignmentPolicyCommand : SyntheticCommandWithPipelineInput<RoleAssignmentPolicy, RoleAssignmentPolicy>
	{
		// Token: 0x0600267C RID: 9852 RVA: 0x000499BF File Offset: 0x00047BBF
		private RemoveRoleAssignmentPolicyCommand() : base("Remove-RoleAssignmentPolicy")
		{
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x000499CC File Offset: 0x00047BCC
		public RemoveRoleAssignmentPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x000499DB File Offset: 0x00047BDB
		public virtual RemoveRoleAssignmentPolicyCommand SetParameters(RemoveRoleAssignmentPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x000499E5 File Offset: 0x00047BE5
		public virtual RemoveRoleAssignmentPolicyCommand SetParameters(RemoveRoleAssignmentPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001D1 RID: 465
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E95 RID: 3733
			// (set) Token: 0x06002680 RID: 9856 RVA: 0x000499EF File Offset: 0x00047BEF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E96 RID: 3734
			// (set) Token: 0x06002681 RID: 9857 RVA: 0x00049A02 File Offset: 0x00047C02
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E97 RID: 3735
			// (set) Token: 0x06002682 RID: 9858 RVA: 0x00049A1A File Offset: 0x00047C1A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E98 RID: 3736
			// (set) Token: 0x06002683 RID: 9859 RVA: 0x00049A32 File Offset: 0x00047C32
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E99 RID: 3737
			// (set) Token: 0x06002684 RID: 9860 RVA: 0x00049A4A File Offset: 0x00047C4A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E9A RID: 3738
			// (set) Token: 0x06002685 RID: 9861 RVA: 0x00049A62 File Offset: 0x00047C62
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000E9B RID: 3739
			// (set) Token: 0x06002686 RID: 9862 RVA: 0x00049A7A File Offset: 0x00047C7A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001D2 RID: 466
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000E9C RID: 3740
			// (set) Token: 0x06002688 RID: 9864 RVA: 0x00049A9A File Offset: 0x00047C9A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000E9D RID: 3741
			// (set) Token: 0x06002689 RID: 9865 RVA: 0x00049AB8 File Offset: 0x00047CB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E9E RID: 3742
			// (set) Token: 0x0600268A RID: 9866 RVA: 0x00049ACB File Offset: 0x00047CCB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E9F RID: 3743
			// (set) Token: 0x0600268B RID: 9867 RVA: 0x00049AE3 File Offset: 0x00047CE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000EA0 RID: 3744
			// (set) Token: 0x0600268C RID: 9868 RVA: 0x00049AFB File Offset: 0x00047CFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000EA1 RID: 3745
			// (set) Token: 0x0600268D RID: 9869 RVA: 0x00049B13 File Offset: 0x00047D13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000EA2 RID: 3746
			// (set) Token: 0x0600268E RID: 9870 RVA: 0x00049B2B File Offset: 0x00047D2B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000EA3 RID: 3747
			// (set) Token: 0x0600268F RID: 9871 RVA: 0x00049B43 File Offset: 0x00047D43
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
