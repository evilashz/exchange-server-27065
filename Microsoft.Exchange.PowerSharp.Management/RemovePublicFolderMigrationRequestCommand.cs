using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A86 RID: 2694
	public class RemovePublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequestIdParameter, PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x0600858B RID: 34187 RVA: 0x000C51C2 File Offset: 0x000C33C2
		private RemovePublicFolderMigrationRequestCommand() : base("Remove-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x0600858C RID: 34188 RVA: 0x000C51CF File Offset: 0x000C33CF
		public RemovePublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600858D RID: 34189 RVA: 0x000C51DE File Offset: 0x000C33DE
		public virtual RemovePublicFolderMigrationRequestCommand SetParameters(RemovePublicFolderMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600858E RID: 34190 RVA: 0x000C51E8 File Offset: 0x000C33E8
		public virtual RemovePublicFolderMigrationRequestCommand SetParameters(RemovePublicFolderMigrationRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600858F RID: 34191 RVA: 0x000C51F2 File Offset: 0x000C33F2
		public virtual RemovePublicFolderMigrationRequestCommand SetParameters(RemovePublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A87 RID: 2695
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005C38 RID: 23608
			// (set) Token: 0x06008590 RID: 34192 RVA: 0x000C51FC File Offset: 0x000C33FC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C39 RID: 23609
			// (set) Token: 0x06008591 RID: 34193 RVA: 0x000C521A File Offset: 0x000C341A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C3A RID: 23610
			// (set) Token: 0x06008592 RID: 34194 RVA: 0x000C522D File Offset: 0x000C342D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C3B RID: 23611
			// (set) Token: 0x06008593 RID: 34195 RVA: 0x000C5245 File Offset: 0x000C3445
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C3C RID: 23612
			// (set) Token: 0x06008594 RID: 34196 RVA: 0x000C525D File Offset: 0x000C345D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C3D RID: 23613
			// (set) Token: 0x06008595 RID: 34197 RVA: 0x000C5275 File Offset: 0x000C3475
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C3E RID: 23614
			// (set) Token: 0x06008596 RID: 34198 RVA: 0x000C528D File Offset: 0x000C348D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005C3F RID: 23615
			// (set) Token: 0x06008597 RID: 34199 RVA: 0x000C52A5 File Offset: 0x000C34A5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A88 RID: 2696
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005C40 RID: 23616
			// (set) Token: 0x06008599 RID: 34201 RVA: 0x000C52C5 File Offset: 0x000C34C5
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005C41 RID: 23617
			// (set) Token: 0x0600859A RID: 34202 RVA: 0x000C52D8 File Offset: 0x000C34D8
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005C42 RID: 23618
			// (set) Token: 0x0600859B RID: 34203 RVA: 0x000C52F0 File Offset: 0x000C34F0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C43 RID: 23619
			// (set) Token: 0x0600859C RID: 34204 RVA: 0x000C5303 File Offset: 0x000C3503
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C44 RID: 23620
			// (set) Token: 0x0600859D RID: 34205 RVA: 0x000C531B File Offset: 0x000C351B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C45 RID: 23621
			// (set) Token: 0x0600859E RID: 34206 RVA: 0x000C5333 File Offset: 0x000C3533
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C46 RID: 23622
			// (set) Token: 0x0600859F RID: 34207 RVA: 0x000C534B File Offset: 0x000C354B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C47 RID: 23623
			// (set) Token: 0x060085A0 RID: 34208 RVA: 0x000C5363 File Offset: 0x000C3563
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005C48 RID: 23624
			// (set) Token: 0x060085A1 RID: 34209 RVA: 0x000C537B File Offset: 0x000C357B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A89 RID: 2697
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005C49 RID: 23625
			// (set) Token: 0x060085A3 RID: 34211 RVA: 0x000C539B File Offset: 0x000C359B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C4A RID: 23626
			// (set) Token: 0x060085A4 RID: 34212 RVA: 0x000C53AE File Offset: 0x000C35AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C4B RID: 23627
			// (set) Token: 0x060085A5 RID: 34213 RVA: 0x000C53C6 File Offset: 0x000C35C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C4C RID: 23628
			// (set) Token: 0x060085A6 RID: 34214 RVA: 0x000C53DE File Offset: 0x000C35DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C4D RID: 23629
			// (set) Token: 0x060085A7 RID: 34215 RVA: 0x000C53F6 File Offset: 0x000C35F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C4E RID: 23630
			// (set) Token: 0x060085A8 RID: 34216 RVA: 0x000C540E File Offset: 0x000C360E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005C4F RID: 23631
			// (set) Token: 0x060085A9 RID: 34217 RVA: 0x000C5426 File Offset: 0x000C3626
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
