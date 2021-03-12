using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001C5 RID: 453
	public class RemoveRetentionPolicyCommand : SyntheticCommandWithPipelineInput<RetentionPolicy, RetentionPolicy>
	{
		// Token: 0x0600261F RID: 9759 RVA: 0x0004924B File Offset: 0x0004744B
		private RemoveRetentionPolicyCommand() : base("Remove-RetentionPolicy")
		{
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x00049258 File Offset: 0x00047458
		public RemoveRetentionPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x00049267 File Offset: 0x00047467
		public virtual RemoveRetentionPolicyCommand SetParameters(RemoveRetentionPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x00049271 File Offset: 0x00047471
		public virtual RemoveRetentionPolicyCommand SetParameters(RemoveRetentionPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001C6 RID: 454
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000E4E RID: 3662
			// (set) Token: 0x06002623 RID: 9763 RVA: 0x0004927B File Offset: 0x0004747B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E4F RID: 3663
			// (set) Token: 0x06002624 RID: 9764 RVA: 0x00049293 File Offset: 0x00047493
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E50 RID: 3664
			// (set) Token: 0x06002625 RID: 9765 RVA: 0x000492A6 File Offset: 0x000474A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E51 RID: 3665
			// (set) Token: 0x06002626 RID: 9766 RVA: 0x000492BE File Offset: 0x000474BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E52 RID: 3666
			// (set) Token: 0x06002627 RID: 9767 RVA: 0x000492D6 File Offset: 0x000474D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E53 RID: 3667
			// (set) Token: 0x06002628 RID: 9768 RVA: 0x000492EE File Offset: 0x000474EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E54 RID: 3668
			// (set) Token: 0x06002629 RID: 9769 RVA: 0x00049306 File Offset: 0x00047506
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000E55 RID: 3669
			// (set) Token: 0x0600262A RID: 9770 RVA: 0x0004931E File Offset: 0x0004751E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001C7 RID: 455
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000E56 RID: 3670
			// (set) Token: 0x0600262C RID: 9772 RVA: 0x0004933E File Offset: 0x0004753E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000E57 RID: 3671
			// (set) Token: 0x0600262D RID: 9773 RVA: 0x0004935C File Offset: 0x0004755C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000E58 RID: 3672
			// (set) Token: 0x0600262E RID: 9774 RVA: 0x00049374 File Offset: 0x00047574
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000E59 RID: 3673
			// (set) Token: 0x0600262F RID: 9775 RVA: 0x00049387 File Offset: 0x00047587
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E5A RID: 3674
			// (set) Token: 0x06002630 RID: 9776 RVA: 0x0004939F File Offset: 0x0004759F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E5B RID: 3675
			// (set) Token: 0x06002631 RID: 9777 RVA: 0x000493B7 File Offset: 0x000475B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E5C RID: 3676
			// (set) Token: 0x06002632 RID: 9778 RVA: 0x000493CF File Offset: 0x000475CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E5D RID: 3677
			// (set) Token: 0x06002633 RID: 9779 RVA: 0x000493E7 File Offset: 0x000475E7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000E5E RID: 3678
			// (set) Token: 0x06002634 RID: 9780 RVA: 0x000493FF File Offset: 0x000475FF
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
