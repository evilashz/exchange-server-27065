using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B8E RID: 2958
	public class SetUMMailboxConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<ADUser>
	{
		// Token: 0x06008F32 RID: 36658 RVA: 0x000D1917 File Offset: 0x000CFB17
		private SetUMMailboxConfigurationCommand() : base("Set-UMMailboxConfiguration")
		{
		}

		// Token: 0x06008F33 RID: 36659 RVA: 0x000D1924 File Offset: 0x000CFB24
		public SetUMMailboxConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008F34 RID: 36660 RVA: 0x000D1933 File Offset: 0x000CFB33
		public virtual SetUMMailboxConfigurationCommand SetParameters(SetUMMailboxConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008F35 RID: 36661 RVA: 0x000D193D File Offset: 0x000CFB3D
		public virtual SetUMMailboxConfigurationCommand SetParameters(SetUMMailboxConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B8F RID: 2959
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170063CF RID: 25551
			// (set) Token: 0x06008F36 RID: 36662 RVA: 0x000D1947 File Offset: 0x000CFB47
			public virtual MailboxGreetingEnum Greeting
			{
				set
				{
					base.PowerSharpParameters["Greeting"] = value;
				}
			}

			// Token: 0x170063D0 RID: 25552
			// (set) Token: 0x06008F37 RID: 36663 RVA: 0x000D195F File Offset: 0x000CFB5F
			public virtual string FolderToReadEmailsFrom
			{
				set
				{
					base.PowerSharpParameters["FolderToReadEmailsFrom"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x170063D1 RID: 25553
			// (set) Token: 0x06008F38 RID: 36664 RVA: 0x000D197D File Offset: 0x000CFB7D
			public virtual bool ReadOldestUnreadVoiceMessagesFirst
			{
				set
				{
					base.PowerSharpParameters["ReadOldestUnreadVoiceMessagesFirst"] = value;
				}
			}

			// Token: 0x170063D2 RID: 25554
			// (set) Token: 0x06008F39 RID: 36665 RVA: 0x000D1995 File Offset: 0x000CFB95
			public virtual string DefaultPlayOnPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["DefaultPlayOnPhoneNumber"] = value;
				}
			}

			// Token: 0x170063D3 RID: 25555
			// (set) Token: 0x06008F3A RID: 36666 RVA: 0x000D19A8 File Offset: 0x000CFBA8
			public virtual bool ReceivedVoiceMailPreviewEnabled
			{
				set
				{
					base.PowerSharpParameters["ReceivedVoiceMailPreviewEnabled"] = value;
				}
			}

			// Token: 0x170063D4 RID: 25556
			// (set) Token: 0x06008F3B RID: 36667 RVA: 0x000D19C0 File Offset: 0x000CFBC0
			public virtual bool SentVoiceMailPreviewEnabled
			{
				set
				{
					base.PowerSharpParameters["SentVoiceMailPreviewEnabled"] = value;
				}
			}

			// Token: 0x170063D5 RID: 25557
			// (set) Token: 0x06008F3C RID: 36668 RVA: 0x000D19D8 File Offset: 0x000CFBD8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170063D6 RID: 25558
			// (set) Token: 0x06008F3D RID: 36669 RVA: 0x000D19EB File Offset: 0x000CFBEB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063D7 RID: 25559
			// (set) Token: 0x06008F3E RID: 36670 RVA: 0x000D1A03 File Offset: 0x000CFC03
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063D8 RID: 25560
			// (set) Token: 0x06008F3F RID: 36671 RVA: 0x000D1A1B File Offset: 0x000CFC1B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063D9 RID: 25561
			// (set) Token: 0x06008F40 RID: 36672 RVA: 0x000D1A33 File Offset: 0x000CFC33
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170063DA RID: 25562
			// (set) Token: 0x06008F41 RID: 36673 RVA: 0x000D1A4B File Offset: 0x000CFC4B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B90 RID: 2960
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170063DB RID: 25563
			// (set) Token: 0x06008F43 RID: 36675 RVA: 0x000D1A6B File Offset: 0x000CFC6B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170063DC RID: 25564
			// (set) Token: 0x06008F44 RID: 36676 RVA: 0x000D1A89 File Offset: 0x000CFC89
			public virtual MailboxGreetingEnum Greeting
			{
				set
				{
					base.PowerSharpParameters["Greeting"] = value;
				}
			}

			// Token: 0x170063DD RID: 25565
			// (set) Token: 0x06008F45 RID: 36677 RVA: 0x000D1AA1 File Offset: 0x000CFCA1
			public virtual string FolderToReadEmailsFrom
			{
				set
				{
					base.PowerSharpParameters["FolderToReadEmailsFrom"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x170063DE RID: 25566
			// (set) Token: 0x06008F46 RID: 36678 RVA: 0x000D1ABF File Offset: 0x000CFCBF
			public virtual bool ReadOldestUnreadVoiceMessagesFirst
			{
				set
				{
					base.PowerSharpParameters["ReadOldestUnreadVoiceMessagesFirst"] = value;
				}
			}

			// Token: 0x170063DF RID: 25567
			// (set) Token: 0x06008F47 RID: 36679 RVA: 0x000D1AD7 File Offset: 0x000CFCD7
			public virtual string DefaultPlayOnPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["DefaultPlayOnPhoneNumber"] = value;
				}
			}

			// Token: 0x170063E0 RID: 25568
			// (set) Token: 0x06008F48 RID: 36680 RVA: 0x000D1AEA File Offset: 0x000CFCEA
			public virtual bool ReceivedVoiceMailPreviewEnabled
			{
				set
				{
					base.PowerSharpParameters["ReceivedVoiceMailPreviewEnabled"] = value;
				}
			}

			// Token: 0x170063E1 RID: 25569
			// (set) Token: 0x06008F49 RID: 36681 RVA: 0x000D1B02 File Offset: 0x000CFD02
			public virtual bool SentVoiceMailPreviewEnabled
			{
				set
				{
					base.PowerSharpParameters["SentVoiceMailPreviewEnabled"] = value;
				}
			}

			// Token: 0x170063E2 RID: 25570
			// (set) Token: 0x06008F4A RID: 36682 RVA: 0x000D1B1A File Offset: 0x000CFD1A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170063E3 RID: 25571
			// (set) Token: 0x06008F4B RID: 36683 RVA: 0x000D1B2D File Offset: 0x000CFD2D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170063E4 RID: 25572
			// (set) Token: 0x06008F4C RID: 36684 RVA: 0x000D1B45 File Offset: 0x000CFD45
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170063E5 RID: 25573
			// (set) Token: 0x06008F4D RID: 36685 RVA: 0x000D1B5D File Offset: 0x000CFD5D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170063E6 RID: 25574
			// (set) Token: 0x06008F4E RID: 36686 RVA: 0x000D1B75 File Offset: 0x000CFD75
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170063E7 RID: 25575
			// (set) Token: 0x06008F4F RID: 36687 RVA: 0x000D1B8D File Offset: 0x000CFD8D
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
