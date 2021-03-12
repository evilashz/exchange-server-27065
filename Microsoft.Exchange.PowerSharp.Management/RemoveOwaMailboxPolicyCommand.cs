using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001C0 RID: 448
	public class RemoveOwaMailboxPolicyCommand : SyntheticCommandWithPipelineInput<OwaMailboxPolicy, OwaMailboxPolicy>
	{
		// Token: 0x060025F7 RID: 9719 RVA: 0x00048F1A File Offset: 0x0004711A
		private RemoveOwaMailboxPolicyCommand() : base("Remove-OwaMailboxPolicy")
		{
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x00048F27 File Offset: 0x00047127
		public RemoveOwaMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x00048F36 File Offset: 0x00047136
		public virtual RemoveOwaMailboxPolicyCommand SetParameters(RemoveOwaMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x00048F40 File Offset: 0x00047140
		public virtual RemoveOwaMailboxPolicyCommand SetParameters(RemoveOwaMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001C1 RID: 449
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E30 RID: 3632
			// (set) Token: 0x060025FB RID: 9723 RVA: 0x00048F4A File Offset: 0x0004714A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E31 RID: 3633
			// (set) Token: 0x060025FC RID: 9724 RVA: 0x00048F62 File Offset: 0x00047162
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E32 RID: 3634
			// (set) Token: 0x060025FD RID: 9725 RVA: 0x00048F75 File Offset: 0x00047175
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E33 RID: 3635
			// (set) Token: 0x060025FE RID: 9726 RVA: 0x00048F8D File Offset: 0x0004718D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E34 RID: 3636
			// (set) Token: 0x060025FF RID: 9727 RVA: 0x00048FA5 File Offset: 0x000471A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E35 RID: 3637
			// (set) Token: 0x06002600 RID: 9728 RVA: 0x00048FBD File Offset: 0x000471BD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E36 RID: 3638
			// (set) Token: 0x06002601 RID: 9729 RVA: 0x00048FD5 File Offset: 0x000471D5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000E37 RID: 3639
			// (set) Token: 0x06002602 RID: 9730 RVA: 0x00048FED File Offset: 0x000471ED
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001C2 RID: 450
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000E38 RID: 3640
			// (set) Token: 0x06002604 RID: 9732 RVA: 0x0004900D File Offset: 0x0004720D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000E39 RID: 3641
			// (set) Token: 0x06002605 RID: 9733 RVA: 0x0004902B File Offset: 0x0004722B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E3A RID: 3642
			// (set) Token: 0x06002606 RID: 9734 RVA: 0x00049043 File Offset: 0x00047243
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E3B RID: 3643
			// (set) Token: 0x06002607 RID: 9735 RVA: 0x00049056 File Offset: 0x00047256
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E3C RID: 3644
			// (set) Token: 0x06002608 RID: 9736 RVA: 0x0004906E File Offset: 0x0004726E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E3D RID: 3645
			// (set) Token: 0x06002609 RID: 9737 RVA: 0x00049086 File Offset: 0x00047286
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E3E RID: 3646
			// (set) Token: 0x0600260A RID: 9738 RVA: 0x0004909E File Offset: 0x0004729E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E3F RID: 3647
			// (set) Token: 0x0600260B RID: 9739 RVA: 0x000490B6 File Offset: 0x000472B6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000E40 RID: 3648
			// (set) Token: 0x0600260C RID: 9740 RVA: 0x000490CE File Offset: 0x000472CE
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
