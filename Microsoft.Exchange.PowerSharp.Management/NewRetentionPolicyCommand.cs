using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001C3 RID: 451
	public class NewRetentionPolicyCommand : SyntheticCommandWithPipelineInput<RetentionPolicy, RetentionPolicy>
	{
		// Token: 0x0600260E RID: 9742 RVA: 0x000490EE File Offset: 0x000472EE
		private NewRetentionPolicyCommand() : base("New-RetentionPolicy")
		{
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000490FB File Offset: 0x000472FB
		public NewRetentionPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x0004910A File Offset: 0x0004730A
		public virtual NewRetentionPolicyCommand SetParameters(NewRetentionPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001C4 RID: 452
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E41 RID: 3649
			// (set) Token: 0x06002611 RID: 9745 RVA: 0x00049114 File Offset: 0x00047314
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x17000E42 RID: 3650
			// (set) Token: 0x06002612 RID: 9746 RVA: 0x0004912C File Offset: 0x0004732C
			public virtual RetentionPolicyTagIdParameter RetentionPolicyTagLinks
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicyTagLinks"] = value;
				}
			}

			// Token: 0x17000E43 RID: 3651
			// (set) Token: 0x06002613 RID: 9747 RVA: 0x0004913F File Offset: 0x0004733F
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17000E44 RID: 3652
			// (set) Token: 0x06002614 RID: 9748 RVA: 0x00049157 File Offset: 0x00047357
			public virtual SwitchParameter IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000E45 RID: 3653
			// (set) Token: 0x06002615 RID: 9749 RVA: 0x0004916F File Offset: 0x0004736F
			public virtual SwitchParameter IsDefaultArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["IsDefaultArbitrationMailbox"] = value;
				}
			}

			// Token: 0x17000E46 RID: 3654
			// (set) Token: 0x06002616 RID: 9750 RVA: 0x00049187 File Offset: 0x00047387
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000E47 RID: 3655
			// (set) Token: 0x06002617 RID: 9751 RVA: 0x000491A5 File Offset: 0x000473A5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E48 RID: 3656
			// (set) Token: 0x06002618 RID: 9752 RVA: 0x000491B8 File Offset: 0x000473B8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E49 RID: 3657
			// (set) Token: 0x06002619 RID: 9753 RVA: 0x000491CB File Offset: 0x000473CB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E4A RID: 3658
			// (set) Token: 0x0600261A RID: 9754 RVA: 0x000491E3 File Offset: 0x000473E3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E4B RID: 3659
			// (set) Token: 0x0600261B RID: 9755 RVA: 0x000491FB File Offset: 0x000473FB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E4C RID: 3660
			// (set) Token: 0x0600261C RID: 9756 RVA: 0x00049213 File Offset: 0x00047413
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E4D RID: 3661
			// (set) Token: 0x0600261D RID: 9757 RVA: 0x0004922B File Offset: 0x0004742B
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
