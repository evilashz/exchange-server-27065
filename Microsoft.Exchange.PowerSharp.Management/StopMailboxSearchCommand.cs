using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200040F RID: 1039
	public class StopMailboxSearchCommand : SyntheticCommandWithPipelineInput<MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x00067651 File Offset: 0x00065851
		private StopMailboxSearchCommand() : base("Stop-MailboxSearch")
		{
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x0006765E File Offset: 0x0006585E
		public StopMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x0006766D File Offset: 0x0006586D
		public virtual StopMailboxSearchCommand SetParameters(StopMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x00067677 File Offset: 0x00065877
		public virtual StopMailboxSearchCommand SetParameters(StopMailboxSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000410 RID: 1040
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170020EF RID: 8431
			// (set) Token: 0x06003D58 RID: 15704 RVA: 0x00067681 File Offset: 0x00065881
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020F0 RID: 8432
			// (set) Token: 0x06003D59 RID: 15705 RVA: 0x00067694 File Offset: 0x00065894
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020F1 RID: 8433
			// (set) Token: 0x06003D5A RID: 15706 RVA: 0x000676AC File Offset: 0x000658AC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020F2 RID: 8434
			// (set) Token: 0x06003D5B RID: 15707 RVA: 0x000676C4 File Offset: 0x000658C4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020F3 RID: 8435
			// (set) Token: 0x06003D5C RID: 15708 RVA: 0x000676DC File Offset: 0x000658DC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020F4 RID: 8436
			// (set) Token: 0x06003D5D RID: 15709 RVA: 0x000676F4 File Offset: 0x000658F4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170020F5 RID: 8437
			// (set) Token: 0x06003D5E RID: 15710 RVA: 0x0006770C File Offset: 0x0006590C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000411 RID: 1041
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170020F6 RID: 8438
			// (set) Token: 0x06003D60 RID: 15712 RVA: 0x0006772C File Offset: 0x0006592C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170020F7 RID: 8439
			// (set) Token: 0x06003D61 RID: 15713 RVA: 0x0006774A File Offset: 0x0006594A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170020F8 RID: 8440
			// (set) Token: 0x06003D62 RID: 15714 RVA: 0x0006775D File Offset: 0x0006595D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170020F9 RID: 8441
			// (set) Token: 0x06003D63 RID: 15715 RVA: 0x00067775 File Offset: 0x00065975
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170020FA RID: 8442
			// (set) Token: 0x06003D64 RID: 15716 RVA: 0x0006778D File Offset: 0x0006598D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170020FB RID: 8443
			// (set) Token: 0x06003D65 RID: 15717 RVA: 0x000677A5 File Offset: 0x000659A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170020FC RID: 8444
			// (set) Token: 0x06003D66 RID: 15718 RVA: 0x000677BD File Offset: 0x000659BD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170020FD RID: 8445
			// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000677D5 File Offset: 0x000659D5
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
