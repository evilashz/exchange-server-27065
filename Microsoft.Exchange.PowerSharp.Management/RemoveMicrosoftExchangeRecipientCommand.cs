using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D18 RID: 3352
	public class RemoveMicrosoftExchangeRecipientCommand : SyntheticCommandWithPipelineInput<ADMicrosoftExchangeRecipient, ADMicrosoftExchangeRecipient>
	{
		// Token: 0x0600B1EF RID: 45551 RVA: 0x001009FA File Offset: 0x000FEBFA
		private RemoveMicrosoftExchangeRecipientCommand() : base("Remove-MicrosoftExchangeRecipient")
		{
		}

		// Token: 0x0600B1F0 RID: 45552 RVA: 0x00100A07 File Offset: 0x000FEC07
		public RemoveMicrosoftExchangeRecipientCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B1F1 RID: 45553 RVA: 0x00100A16 File Offset: 0x000FEC16
		public virtual RemoveMicrosoftExchangeRecipientCommand SetParameters(RemoveMicrosoftExchangeRecipientCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D19 RID: 3353
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008378 RID: 33656
			// (set) Token: 0x0600B1F2 RID: 45554 RVA: 0x00100A20 File Offset: 0x000FEC20
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008379 RID: 33657
			// (set) Token: 0x0600B1F3 RID: 45555 RVA: 0x00100A33 File Offset: 0x000FEC33
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700837A RID: 33658
			// (set) Token: 0x0600B1F4 RID: 45556 RVA: 0x00100A4B File Offset: 0x000FEC4B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700837B RID: 33659
			// (set) Token: 0x0600B1F5 RID: 45557 RVA: 0x00100A63 File Offset: 0x000FEC63
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700837C RID: 33660
			// (set) Token: 0x0600B1F6 RID: 45558 RVA: 0x00100A7B File Offset: 0x000FEC7B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700837D RID: 33661
			// (set) Token: 0x0600B1F7 RID: 45559 RVA: 0x00100A93 File Offset: 0x000FEC93
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700837E RID: 33662
			// (set) Token: 0x0600B1F8 RID: 45560 RVA: 0x00100AAB File Offset: 0x000FECAB
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
