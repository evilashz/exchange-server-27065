using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AF8 RID: 2808
	public class NewRecipientEnforcementProvisioningPolicyCommand : SyntheticCommandWithPipelineInput<RecipientEnforcementProvisioningPolicy, RecipientEnforcementProvisioningPolicy>
	{
		// Token: 0x06008A22 RID: 35362 RVA: 0x000CB154 File Offset: 0x000C9354
		private NewRecipientEnforcementProvisioningPolicyCommand() : base("New-RecipientEnforcementProvisioningPolicy")
		{
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x000CB161 File Offset: 0x000C9361
		public NewRecipientEnforcementProvisioningPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x000CB170 File Offset: 0x000C9370
		public virtual NewRecipientEnforcementProvisioningPolicyCommand SetParameters(NewRecipientEnforcementProvisioningPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AF9 RID: 2809
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005FEB RID: 24555
			// (set) Token: 0x06008A25 RID: 35365 RVA: 0x000CB17A File Offset: 0x000C937A
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x17005FEC RID: 24556
			// (set) Token: 0x06008A26 RID: 35366 RVA: 0x000CB192 File Offset: 0x000C9392
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005FED RID: 24557
			// (set) Token: 0x06008A27 RID: 35367 RVA: 0x000CB1B0 File Offset: 0x000C93B0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005FEE RID: 24558
			// (set) Token: 0x06008A28 RID: 35368 RVA: 0x000CB1C3 File Offset: 0x000C93C3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005FEF RID: 24559
			// (set) Token: 0x06008A29 RID: 35369 RVA: 0x000CB1DB File Offset: 0x000C93DB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005FF0 RID: 24560
			// (set) Token: 0x06008A2A RID: 35370 RVA: 0x000CB1F3 File Offset: 0x000C93F3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005FF1 RID: 24561
			// (set) Token: 0x06008A2B RID: 35371 RVA: 0x000CB20B File Offset: 0x000C940B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005FF2 RID: 24562
			// (set) Token: 0x06008A2C RID: 35372 RVA: 0x000CB223 File Offset: 0x000C9423
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
