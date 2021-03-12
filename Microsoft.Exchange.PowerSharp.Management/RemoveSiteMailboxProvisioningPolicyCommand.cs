using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001DE RID: 478
	public class RemoveSiteMailboxProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<TeamMailboxProvisioningPolicy, TeamMailboxProvisioningPolicy>
	{
		// Token: 0x0600278D RID: 10125 RVA: 0x0004B142 File Offset: 0x00049342
		private RemoveSiteMailboxProvisioningPolicyCommand() : base("Remove-SiteMailboxProvisioningPolicy")
		{
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0004B14F File Offset: 0x0004934F
		public RemoveSiteMailboxProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0004B15E File Offset: 0x0004935E
		public virtual RemoveSiteMailboxProvisioningPolicyCommand SetParameters(RemoveSiteMailboxProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x0004B168 File Offset: 0x00049368
		public virtual RemoveSiteMailboxProvisioningPolicyCommand SetParameters(RemoveSiteMailboxProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001DF RID: 479
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000F8A RID: 3978
			// (set) Token: 0x06002791 RID: 10129 RVA: 0x0004B172 File Offset: 0x00049372
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000F8B RID: 3979
			// (set) Token: 0x06002792 RID: 10130 RVA: 0x0004B185 File Offset: 0x00049385
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F8C RID: 3980
			// (set) Token: 0x06002793 RID: 10131 RVA: 0x0004B19D File Offset: 0x0004939D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F8D RID: 3981
			// (set) Token: 0x06002794 RID: 10132 RVA: 0x0004B1B5 File Offset: 0x000493B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F8E RID: 3982
			// (set) Token: 0x06002795 RID: 10133 RVA: 0x0004B1CD File Offset: 0x000493CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000F8F RID: 3983
			// (set) Token: 0x06002796 RID: 10134 RVA: 0x0004B1E5 File Offset: 0x000493E5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000F90 RID: 3984
			// (set) Token: 0x06002797 RID: 10135 RVA: 0x0004B1FD File Offset: 0x000493FD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001E0 RID: 480
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000F91 RID: 3985
			// (set) Token: 0x06002799 RID: 10137 RVA: 0x0004B21D File Offset: 0x0004941D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000F92 RID: 3986
			// (set) Token: 0x0600279A RID: 10138 RVA: 0x0004B23B File Offset: 0x0004943B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000F93 RID: 3987
			// (set) Token: 0x0600279B RID: 10139 RVA: 0x0004B24E File Offset: 0x0004944E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000F94 RID: 3988
			// (set) Token: 0x0600279C RID: 10140 RVA: 0x0004B266 File Offset: 0x00049466
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000F95 RID: 3989
			// (set) Token: 0x0600279D RID: 10141 RVA: 0x0004B27E File Offset: 0x0004947E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000F96 RID: 3990
			// (set) Token: 0x0600279E RID: 10142 RVA: 0x0004B296 File Offset: 0x00049496
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000F97 RID: 3991
			// (set) Token: 0x0600279F RID: 10143 RVA: 0x0004B2AE File Offset: 0x000494AE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000F98 RID: 3992
			// (set) Token: 0x060027A0 RID: 10144 RVA: 0x0004B2C6 File Offset: 0x000494C6
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
