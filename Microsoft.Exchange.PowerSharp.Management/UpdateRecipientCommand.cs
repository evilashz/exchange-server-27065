using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D42 RID: 3394
	public class UpdateRecipientCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600B3BD RID: 46013 RVA: 0x00102F90 File Offset: 0x00101190
		private UpdateRecipientCommand() : base("Update-Recipient")
		{
		}

		// Token: 0x0600B3BE RID: 46014 RVA: 0x00102F9D File Offset: 0x0010119D
		public UpdateRecipientCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B3BF RID: 46015 RVA: 0x00102FAC File Offset: 0x001011AC
		public virtual UpdateRecipientCommand SetParameters(UpdateRecipientCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3C0 RID: 46016 RVA: 0x00102FB6 File Offset: 0x001011B6
		public virtual UpdateRecipientCommand SetParameters(UpdateRecipientCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D43 RID: 3395
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170084F2 RID: 34034
			// (set) Token: 0x0600B3C1 RID: 46017 RVA: 0x00102FC0 File Offset: 0x001011C0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170084F3 RID: 34035
			// (set) Token: 0x0600B3C2 RID: 46018 RVA: 0x00102FD3 File Offset: 0x001011D3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084F4 RID: 34036
			// (set) Token: 0x0600B3C3 RID: 46019 RVA: 0x00102FE6 File Offset: 0x001011E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084F5 RID: 34037
			// (set) Token: 0x0600B3C4 RID: 46020 RVA: 0x00102FFE File Offset: 0x001011FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084F6 RID: 34038
			// (set) Token: 0x0600B3C5 RID: 46021 RVA: 0x00103016 File Offset: 0x00101216
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084F7 RID: 34039
			// (set) Token: 0x0600B3C6 RID: 46022 RVA: 0x0010302E File Offset: 0x0010122E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170084F8 RID: 34040
			// (set) Token: 0x0600B3C7 RID: 46023 RVA: 0x00103046 File Offset: 0x00101246
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D44 RID: 3396
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170084F9 RID: 34041
			// (set) Token: 0x0600B3C9 RID: 46025 RVA: 0x00103066 File Offset: 0x00101266
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170084FA RID: 34042
			// (set) Token: 0x0600B3CA RID: 46026 RVA: 0x00103084 File Offset: 0x00101284
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170084FB RID: 34043
			// (set) Token: 0x0600B3CB RID: 46027 RVA: 0x00103097 File Offset: 0x00101297
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170084FC RID: 34044
			// (set) Token: 0x0600B3CC RID: 46028 RVA: 0x001030AA File Offset: 0x001012AA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170084FD RID: 34045
			// (set) Token: 0x0600B3CD RID: 46029 RVA: 0x001030C2 File Offset: 0x001012C2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170084FE RID: 34046
			// (set) Token: 0x0600B3CE RID: 46030 RVA: 0x001030DA File Offset: 0x001012DA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170084FF RID: 34047
			// (set) Token: 0x0600B3CF RID: 46031 RVA: 0x001030F2 File Offset: 0x001012F2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008500 RID: 34048
			// (set) Token: 0x0600B3D0 RID: 46032 RVA: 0x0010310A File Offset: 0x0010130A
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
