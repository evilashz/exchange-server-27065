using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AFA RID: 2810
	public class NewRecipientTemplateProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientTemplateProvisioningPolicy, RecipientTemplateProvisioningPolicy>
	{
		// Token: 0x06008A2E RID: 35374 RVA: 0x000CB243 File Offset: 0x000C9443
		private NewRecipientTemplateProvisioningPolicyCommand() : base("New-RecipientTemplateProvisioningPolicy")
		{
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x000CB250 File Offset: 0x000C9450
		public NewRecipientTemplateProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x000CB25F File Offset: 0x000C945F
		public virtual NewRecipientTemplateProvisioningPolicyCommand SetParameters(NewRecipientTemplateProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AFB RID: 2811
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FF3 RID: 24563
			// (set) Token: 0x06008A31 RID: 35377 RVA: 0x000CB269 File Offset: 0x000C9469
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FF4 RID: 24564
			// (set) Token: 0x06008A32 RID: 35378 RVA: 0x000CB287 File Offset: 0x000C9487
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FF5 RID: 24565
			// (set) Token: 0x06008A33 RID: 35379 RVA: 0x000CB29A File Offset: 0x000C949A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FF6 RID: 24566
			// (set) Token: 0x06008A34 RID: 35380 RVA: 0x000CB2B2 File Offset: 0x000C94B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FF7 RID: 24567
			// (set) Token: 0x06008A35 RID: 35381 RVA: 0x000CB2CA File Offset: 0x000C94CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FF8 RID: 24568
			// (set) Token: 0x06008A36 RID: 35382 RVA: 0x000CB2E2 File Offset: 0x000C94E2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FF9 RID: 24569
			// (set) Token: 0x06008A37 RID: 35383 RVA: 0x000CB2FA File Offset: 0x000C94FA
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
