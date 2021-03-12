using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004A4 RID: 1188
	public class SetMailboxSpellingConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxSpellingConfiguration>
	{
		// Token: 0x06004292 RID: 17042 RVA: 0x0006E1D6 File Offset: 0x0006C3D6
		private SetMailboxSpellingConfigurationCommand() : base("Set-MailboxSpellingConfiguration")
		{
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0006E1E3 File Offset: 0x0006C3E3
		public SetMailboxSpellingConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x0006E1F2 File Offset: 0x0006C3F2
		public virtual SetMailboxSpellingConfigurationCommand SetParameters(SetMailboxSpellingConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x0006E1FC File Offset: 0x0006C3FC
		public virtual SetMailboxSpellingConfigurationCommand SetParameters(SetMailboxSpellingConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004A5 RID: 1189
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002503 RID: 9475
			// (set) Token: 0x06004296 RID: 17046 RVA: 0x0006E206 File Offset: 0x0006C406
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002504 RID: 9476
			// (set) Token: 0x06004297 RID: 17047 RVA: 0x0006E219 File Offset: 0x0006C419
			public virtual bool CheckBeforeSend
			{
				set
				{
					base.PowerSharpParameters["CheckBeforeSend"] = value;
				}
			}

			// Token: 0x17002505 RID: 9477
			// (set) Token: 0x06004298 RID: 17048 RVA: 0x0006E231 File Offset: 0x0006C431
			public virtual SpellcheckerSupportedLanguage DictionaryLanguage
			{
				set
				{
					base.PowerSharpParameters["DictionaryLanguage"] = value;
				}
			}

			// Token: 0x17002506 RID: 9478
			// (set) Token: 0x06004299 RID: 17049 RVA: 0x0006E249 File Offset: 0x0006C449
			public virtual bool IgnoreUppercase
			{
				set
				{
					base.PowerSharpParameters["IgnoreUppercase"] = value;
				}
			}

			// Token: 0x17002507 RID: 9479
			// (set) Token: 0x0600429A RID: 17050 RVA: 0x0006E261 File Offset: 0x0006C461
			public virtual bool IgnoreMixedDigits
			{
				set
				{
					base.PowerSharpParameters["IgnoreMixedDigits"] = value;
				}
			}

			// Token: 0x17002508 RID: 9480
			// (set) Token: 0x0600429B RID: 17051 RVA: 0x0006E279 File Offset: 0x0006C479
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002509 RID: 9481
			// (set) Token: 0x0600429C RID: 17052 RVA: 0x0006E291 File Offset: 0x0006C491
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700250A RID: 9482
			// (set) Token: 0x0600429D RID: 17053 RVA: 0x0006E2A9 File Offset: 0x0006C4A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700250B RID: 9483
			// (set) Token: 0x0600429E RID: 17054 RVA: 0x0006E2C1 File Offset: 0x0006C4C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700250C RID: 9484
			// (set) Token: 0x0600429F RID: 17055 RVA: 0x0006E2D9 File Offset: 0x0006C4D9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004A6 RID: 1190
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700250D RID: 9485
			// (set) Token: 0x060042A1 RID: 17057 RVA: 0x0006E2F9 File Offset: 0x0006C4F9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700250E RID: 9486
			// (set) Token: 0x060042A2 RID: 17058 RVA: 0x0006E317 File Offset: 0x0006C517
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700250F RID: 9487
			// (set) Token: 0x060042A3 RID: 17059 RVA: 0x0006E32A File Offset: 0x0006C52A
			public virtual bool CheckBeforeSend
			{
				set
				{
					base.PowerSharpParameters["CheckBeforeSend"] = value;
				}
			}

			// Token: 0x17002510 RID: 9488
			// (set) Token: 0x060042A4 RID: 17060 RVA: 0x0006E342 File Offset: 0x0006C542
			public virtual SpellcheckerSupportedLanguage DictionaryLanguage
			{
				set
				{
					base.PowerSharpParameters["DictionaryLanguage"] = value;
				}
			}

			// Token: 0x17002511 RID: 9489
			// (set) Token: 0x060042A5 RID: 17061 RVA: 0x0006E35A File Offset: 0x0006C55A
			public virtual bool IgnoreUppercase
			{
				set
				{
					base.PowerSharpParameters["IgnoreUppercase"] = value;
				}
			}

			// Token: 0x17002512 RID: 9490
			// (set) Token: 0x060042A6 RID: 17062 RVA: 0x0006E372 File Offset: 0x0006C572
			public virtual bool IgnoreMixedDigits
			{
				set
				{
					base.PowerSharpParameters["IgnoreMixedDigits"] = value;
				}
			}

			// Token: 0x17002513 RID: 9491
			// (set) Token: 0x060042A7 RID: 17063 RVA: 0x0006E38A File Offset: 0x0006C58A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002514 RID: 9492
			// (set) Token: 0x060042A8 RID: 17064 RVA: 0x0006E3A2 File Offset: 0x0006C5A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002515 RID: 9493
			// (set) Token: 0x060042A9 RID: 17065 RVA: 0x0006E3BA File Offset: 0x0006C5BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002516 RID: 9494
			// (set) Token: 0x060042AA RID: 17066 RVA: 0x0006E3D2 File Offset: 0x0006C5D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002517 RID: 9495
			// (set) Token: 0x060042AB RID: 17067 RVA: 0x0006E3EA File Offset: 0x0006C5EA
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
