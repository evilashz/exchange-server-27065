using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DBE RID: 3518
	public class RemoveSyncMailContactCommand : SyntheticCommandWithPipelineInputNoOutput<MailContactIdParameter>
	{
		// Token: 0x0600CB72 RID: 52082 RVA: 0x001224AE File Offset: 0x001206AE
		private RemoveSyncMailContactCommand() : base("Remove-SyncMailContact")
		{
		}

		// Token: 0x0600CB73 RID: 52083 RVA: 0x001224BB File Offset: 0x001206BB
		public RemoveSyncMailContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600CB74 RID: 52084 RVA: 0x001224CA File Offset: 0x001206CA
		public virtual RemoveSyncMailContactCommand SetParameters(RemoveSyncMailContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600CB75 RID: 52085 RVA: 0x001224D4 File Offset: 0x001206D4
		public virtual RemoveSyncMailContactCommand SetParameters(RemoveSyncMailContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DBF RID: 3519
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17009BAF RID: 39855
			// (set) Token: 0x0600CB76 RID: 52086 RVA: 0x001224DE File Offset: 0x001206DE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009BB0 RID: 39856
			// (set) Token: 0x0600CB77 RID: 52087 RVA: 0x001224F6 File Offset: 0x001206F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009BB1 RID: 39857
			// (set) Token: 0x0600CB78 RID: 52088 RVA: 0x00122509 File Offset: 0x00120709
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009BB2 RID: 39858
			// (set) Token: 0x0600CB79 RID: 52089 RVA: 0x00122521 File Offset: 0x00120721
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009BB3 RID: 39859
			// (set) Token: 0x0600CB7A RID: 52090 RVA: 0x00122539 File Offset: 0x00120739
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009BB4 RID: 39860
			// (set) Token: 0x0600CB7B RID: 52091 RVA: 0x00122551 File Offset: 0x00120751
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009BB5 RID: 39861
			// (set) Token: 0x0600CB7C RID: 52092 RVA: 0x00122569 File Offset: 0x00120769
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17009BB6 RID: 39862
			// (set) Token: 0x0600CB7D RID: 52093 RVA: 0x00122581 File Offset: 0x00120781
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DC0 RID: 3520
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17009BB7 RID: 39863
			// (set) Token: 0x0600CB7F RID: 52095 RVA: 0x001225A1 File Offset: 0x001207A1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailContactIdParameter(value) : null);
				}
			}

			// Token: 0x17009BB8 RID: 39864
			// (set) Token: 0x0600CB80 RID: 52096 RVA: 0x001225BF File Offset: 0x001207BF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17009BB9 RID: 39865
			// (set) Token: 0x0600CB81 RID: 52097 RVA: 0x001225D7 File Offset: 0x001207D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17009BBA RID: 39866
			// (set) Token: 0x0600CB82 RID: 52098 RVA: 0x001225EA File Offset: 0x001207EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17009BBB RID: 39867
			// (set) Token: 0x0600CB83 RID: 52099 RVA: 0x00122602 File Offset: 0x00120802
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17009BBC RID: 39868
			// (set) Token: 0x0600CB84 RID: 52100 RVA: 0x0012261A File Offset: 0x0012081A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17009BBD RID: 39869
			// (set) Token: 0x0600CB85 RID: 52101 RVA: 0x00122632 File Offset: 0x00120832
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17009BBE RID: 39870
			// (set) Token: 0x0600CB86 RID: 52102 RVA: 0x0012264A File Offset: 0x0012084A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17009BBF RID: 39871
			// (set) Token: 0x0600CB87 RID: 52103 RVA: 0x00122662 File Offset: 0x00120862
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
