using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AFF RID: 2815
	public class RemoveRecipientTemplateProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientTemplateProvisioningPolicy, RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x06008A4E RID: 35406 RVA: 0x000CB4BE File Offset: 0x000C96BE
		private RemoveRecipientTemplateProvisioningPolicyCommand() : base("Remove-RecipientTemplateProvisioningPolicy")
		{
		}

		// Token: 0x06008A4F RID: 35407 RVA: 0x000CB4CB File Offset: 0x000C96CB
		public RemoveRecipientTemplateProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A50 RID: 35408 RVA: 0x000CB4DA File Offset: 0x000C96DA
		public virtual RemoveRecipientTemplateProvisioningPolicyCommand SetParameters(RemoveRecipientTemplateProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x000CB4E4 File Offset: 0x000C96E4
		public virtual RemoveRecipientTemplateProvisioningPolicyCommand SetParameters(RemoveRecipientTemplateProvisioningPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B00 RID: 2816
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006009 RID: 24585
			// (set) Token: 0x06008A52 RID: 35410 RVA: 0x000CB4EE File Offset: 0x000C96EE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700600A RID: 24586
			// (set) Token: 0x06008A53 RID: 35411 RVA: 0x000CB501 File Offset: 0x000C9701
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700600B RID: 24587
			// (set) Token: 0x06008A54 RID: 35412 RVA: 0x000CB519 File Offset: 0x000C9719
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700600C RID: 24588
			// (set) Token: 0x06008A55 RID: 35413 RVA: 0x000CB531 File Offset: 0x000C9731
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700600D RID: 24589
			// (set) Token: 0x06008A56 RID: 35414 RVA: 0x000CB549 File Offset: 0x000C9749
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700600E RID: 24590
			// (set) Token: 0x06008A57 RID: 35415 RVA: 0x000CB561 File Offset: 0x000C9761
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700600F RID: 24591
			// (set) Token: 0x06008A58 RID: 35416 RVA: 0x000CB579 File Offset: 0x000C9779
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B01 RID: 2817
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006010 RID: 24592
			// (set) Token: 0x06008A5A RID: 35418 RVA: 0x000CB599 File Offset: 0x000C9799
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ProvisioningPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17006011 RID: 24593
			// (set) Token: 0x06008A5B RID: 35419 RVA: 0x000CB5B7 File Offset: 0x000C97B7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006012 RID: 24594
			// (set) Token: 0x06008A5C RID: 35420 RVA: 0x000CB5CA File Offset: 0x000C97CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006013 RID: 24595
			// (set) Token: 0x06008A5D RID: 35421 RVA: 0x000CB5E2 File Offset: 0x000C97E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006014 RID: 24596
			// (set) Token: 0x06008A5E RID: 35422 RVA: 0x000CB5FA File Offset: 0x000C97FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006015 RID: 24597
			// (set) Token: 0x06008A5F RID: 35423 RVA: 0x000CB612 File Offset: 0x000C9812
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006016 RID: 24598
			// (set) Token: 0x06008A60 RID: 35424 RVA: 0x000CB62A File Offset: 0x000C982A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006017 RID: 24599
			// (set) Token: 0x06008A61 RID: 35425 RVA: 0x000CB642 File Offset: 0x000C9842
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
