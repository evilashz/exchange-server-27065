using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200042C RID: 1068
	public class DisableServiceEmailChannelCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06003E50 RID: 15952 RVA: 0x00068A27 File Offset: 0x00066C27
		private DisableServiceEmailChannelCommand() : base("Disable-ServiceEmailChannel")
		{
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x00068A34 File Offset: 0x00066C34
		public DisableServiceEmailChannelCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x00068A43 File Offset: 0x00066C43
		public virtual DisableServiceEmailChannelCommand SetParameters(DisableServiceEmailChannelCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00068A4D File Offset: 0x00066C4D
		public virtual DisableServiceEmailChannelCommand SetParameters(DisableServiceEmailChannelCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200042D RID: 1069
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170021B1 RID: 8625
			// (set) Token: 0x06003E54 RID: 15956 RVA: 0x00068A57 File Offset: 0x00066C57
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021B2 RID: 8626
			// (set) Token: 0x06003E55 RID: 15957 RVA: 0x00068A6A File Offset: 0x00066C6A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021B3 RID: 8627
			// (set) Token: 0x06003E56 RID: 15958 RVA: 0x00068A82 File Offset: 0x00066C82
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021B4 RID: 8628
			// (set) Token: 0x06003E57 RID: 15959 RVA: 0x00068A9A File Offset: 0x00066C9A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021B5 RID: 8629
			// (set) Token: 0x06003E58 RID: 15960 RVA: 0x00068AB2 File Offset: 0x00066CB2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021B6 RID: 8630
			// (set) Token: 0x06003E59 RID: 15961 RVA: 0x00068ACA File Offset: 0x00066CCA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170021B7 RID: 8631
			// (set) Token: 0x06003E5A RID: 15962 RVA: 0x00068AE2 File Offset: 0x00066CE2
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200042E RID: 1070
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170021B8 RID: 8632
			// (set) Token: 0x06003E5C RID: 15964 RVA: 0x00068B02 File Offset: 0x00066D02
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170021B9 RID: 8633
			// (set) Token: 0x06003E5D RID: 15965 RVA: 0x00068B20 File Offset: 0x00066D20
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170021BA RID: 8634
			// (set) Token: 0x06003E5E RID: 15966 RVA: 0x00068B33 File Offset: 0x00066D33
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170021BB RID: 8635
			// (set) Token: 0x06003E5F RID: 15967 RVA: 0x00068B4B File Offset: 0x00066D4B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170021BC RID: 8636
			// (set) Token: 0x06003E60 RID: 15968 RVA: 0x00068B63 File Offset: 0x00066D63
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170021BD RID: 8637
			// (set) Token: 0x06003E61 RID: 15969 RVA: 0x00068B7B File Offset: 0x00066D7B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170021BE RID: 8638
			// (set) Token: 0x06003E62 RID: 15970 RVA: 0x00068B93 File Offset: 0x00066D93
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170021BF RID: 8639
			// (set) Token: 0x06003E63 RID: 15971 RVA: 0x00068BAB File Offset: 0x00066DAB
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
