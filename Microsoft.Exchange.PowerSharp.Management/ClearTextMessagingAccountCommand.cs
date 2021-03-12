using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000467 RID: 1127
	public class ClearTextMessagingAccountCommand : SyntheticCommandWithPipelineInput<TextMessagingAccount, TextMessagingAccount>
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x0006B5D4 File Offset: 0x000697D4
		private ClearTextMessagingAccountCommand() : base("Clear-TextMessagingAccount")
		{
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x0006B5E1 File Offset: 0x000697E1
		public ClearTextMessagingAccountCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x0006B5F0 File Offset: 0x000697F0
		public virtual ClearTextMessagingAccountCommand SetParameters(ClearTextMessagingAccountCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x0006B5FA File Offset: 0x000697FA
		public virtual ClearTextMessagingAccountCommand SetParameters(ClearTextMessagingAccountCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000468 RID: 1128
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002356 RID: 9046
			// (set) Token: 0x0600406F RID: 16495 RVA: 0x0006B604 File Offset: 0x00069804
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17002357 RID: 9047
			// (set) Token: 0x06004070 RID: 16496 RVA: 0x0006B622 File Offset: 0x00069822
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17002358 RID: 9048
			// (set) Token: 0x06004071 RID: 16497 RVA: 0x0006B63A File Offset: 0x0006983A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002359 RID: 9049
			// (set) Token: 0x06004072 RID: 16498 RVA: 0x0006B64D File Offset: 0x0006984D
			public virtual RegionInfo CountryRegionId
			{
				set
				{
					base.PowerSharpParameters["CountryRegionId"] = value;
				}
			}

			// Token: 0x1700235A RID: 9050
			// (set) Token: 0x06004073 RID: 16499 RVA: 0x0006B660 File Offset: 0x00069860
			public virtual int MobileOperatorId
			{
				set
				{
					base.PowerSharpParameters["MobileOperatorId"] = value;
				}
			}

			// Token: 0x1700235B RID: 9051
			// (set) Token: 0x06004074 RID: 16500 RVA: 0x0006B678 File Offset: 0x00069878
			public virtual E164Number NotificationPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["NotificationPhoneNumber"] = value;
				}
			}

			// Token: 0x1700235C RID: 9052
			// (set) Token: 0x06004075 RID: 16501 RVA: 0x0006B68B File Offset: 0x0006988B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700235D RID: 9053
			// (set) Token: 0x06004076 RID: 16502 RVA: 0x0006B6A3 File Offset: 0x000698A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700235E RID: 9054
			// (set) Token: 0x06004077 RID: 16503 RVA: 0x0006B6BB File Offset: 0x000698BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700235F RID: 9055
			// (set) Token: 0x06004078 RID: 16504 RVA: 0x0006B6D3 File Offset: 0x000698D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002360 RID: 9056
			// (set) Token: 0x06004079 RID: 16505 RVA: 0x0006B6EB File Offset: 0x000698EB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002361 RID: 9057
			// (set) Token: 0x0600407A RID: 16506 RVA: 0x0006B703 File Offset: 0x00069903
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000469 RID: 1129
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002362 RID: 9058
			// (set) Token: 0x0600407C RID: 16508 RVA: 0x0006B723 File Offset: 0x00069923
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17002363 RID: 9059
			// (set) Token: 0x0600407D RID: 16509 RVA: 0x0006B73B File Offset: 0x0006993B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002364 RID: 9060
			// (set) Token: 0x0600407E RID: 16510 RVA: 0x0006B74E File Offset: 0x0006994E
			public virtual RegionInfo CountryRegionId
			{
				set
				{
					base.PowerSharpParameters["CountryRegionId"] = value;
				}
			}

			// Token: 0x17002365 RID: 9061
			// (set) Token: 0x0600407F RID: 16511 RVA: 0x0006B761 File Offset: 0x00069961
			public virtual int MobileOperatorId
			{
				set
				{
					base.PowerSharpParameters["MobileOperatorId"] = value;
				}
			}

			// Token: 0x17002366 RID: 9062
			// (set) Token: 0x06004080 RID: 16512 RVA: 0x0006B779 File Offset: 0x00069979
			public virtual E164Number NotificationPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["NotificationPhoneNumber"] = value;
				}
			}

			// Token: 0x17002367 RID: 9063
			// (set) Token: 0x06004081 RID: 16513 RVA: 0x0006B78C File Offset: 0x0006998C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002368 RID: 9064
			// (set) Token: 0x06004082 RID: 16514 RVA: 0x0006B7A4 File Offset: 0x000699A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002369 RID: 9065
			// (set) Token: 0x06004083 RID: 16515 RVA: 0x0006B7BC File Offset: 0x000699BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700236A RID: 9066
			// (set) Token: 0x06004084 RID: 16516 RVA: 0x0006B7D4 File Offset: 0x000699D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700236B RID: 9067
			// (set) Token: 0x06004085 RID: 16517 RVA: 0x0006B7EC File Offset: 0x000699EC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700236C RID: 9068
			// (set) Token: 0x06004086 RID: 16518 RVA: 0x0006B804 File Offset: 0x00069A04
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
