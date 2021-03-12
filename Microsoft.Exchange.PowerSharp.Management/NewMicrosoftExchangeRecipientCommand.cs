using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D16 RID: 3350
	public class NewMicrosoftExchangeRecipientCommand : SyntheticCommandWithPipelineInput<ADMicrosoftExchangeRecipient, ADMicrosoftExchangeRecipient>
	{
		// Token: 0x0600B1E4 RID: 45540 RVA: 0x00100923 File Offset: 0x000FEB23
		private NewMicrosoftExchangeRecipientCommand() : base("New-MicrosoftExchangeRecipient")
		{
		}

		// Token: 0x0600B1E5 RID: 45541 RVA: 0x00100930 File Offset: 0x000FEB30
		public NewMicrosoftExchangeRecipientCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B1E6 RID: 45542 RVA: 0x0010093F File Offset: 0x000FEB3F
		public virtual NewMicrosoftExchangeRecipientCommand SetParameters(NewMicrosoftExchangeRecipientCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D17 RID: 3351
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008371 RID: 33649
			// (set) Token: 0x0600B1E7 RID: 45543 RVA: 0x00100949 File Offset: 0x000FEB49
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008372 RID: 33650
			// (set) Token: 0x0600B1E8 RID: 45544 RVA: 0x00100967 File Offset: 0x000FEB67
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008373 RID: 33651
			// (set) Token: 0x0600B1E9 RID: 45545 RVA: 0x0010097A File Offset: 0x000FEB7A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008374 RID: 33652
			// (set) Token: 0x0600B1EA RID: 45546 RVA: 0x00100992 File Offset: 0x000FEB92
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008375 RID: 33653
			// (set) Token: 0x0600B1EB RID: 45547 RVA: 0x001009AA File Offset: 0x000FEBAA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008376 RID: 33654
			// (set) Token: 0x0600B1EC RID: 45548 RVA: 0x001009C2 File Offset: 0x000FEBC2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008377 RID: 33655
			// (set) Token: 0x0600B1ED RID: 45549 RVA: 0x001009DA File Offset: 0x000FEBDA
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
