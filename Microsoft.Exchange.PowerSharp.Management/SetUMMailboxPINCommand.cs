using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B83 RID: 2947
	public class SetUMMailboxPINCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x06008E63 RID: 36451 RVA: 0x000D07F6 File Offset: 0x000CE9F6
		private SetUMMailboxPINCommand() : base("Set-UMMailboxPIN")
		{
		}

		// Token: 0x06008E64 RID: 36452 RVA: 0x000D0803 File Offset: 0x000CEA03
		public SetUMMailboxPINCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008E65 RID: 36453 RVA: 0x000D0812 File Offset: 0x000CEA12
		public virtual SetUMMailboxPINCommand SetParameters(SetUMMailboxPINCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008E66 RID: 36454 RVA: 0x000D081C File Offset: 0x000CEA1C
		public virtual SetUMMailboxPINCommand SetParameters(SetUMMailboxPINCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B84 RID: 2948
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006316 RID: 25366
			// (set) Token: 0x06008E67 RID: 36455 RVA: 0x000D0826 File Offset: 0x000CEA26
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x17006317 RID: 25367
			// (set) Token: 0x06008E68 RID: 36456 RVA: 0x000D0839 File Offset: 0x000CEA39
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x17006318 RID: 25368
			// (set) Token: 0x06008E69 RID: 36457 RVA: 0x000D0851 File Offset: 0x000CEA51
			public virtual bool LockedOut
			{
				set
				{
					base.PowerSharpParameters["LockedOut"] = value;
				}
			}

			// Token: 0x17006319 RID: 25369
			// (set) Token: 0x06008E6A RID: 36458 RVA: 0x000D0869 File Offset: 0x000CEA69
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x1700631A RID: 25370
			// (set) Token: 0x06008E6B RID: 36459 RVA: 0x000D087C File Offset: 0x000CEA7C
			public virtual bool SendEmail
			{
				set
				{
					base.PowerSharpParameters["SendEmail"] = value;
				}
			}

			// Token: 0x1700631B RID: 25371
			// (set) Token: 0x06008E6C RID: 36460 RVA: 0x000D0894 File Offset: 0x000CEA94
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700631C RID: 25372
			// (set) Token: 0x06008E6D RID: 36461 RVA: 0x000D08AC File Offset: 0x000CEAAC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700631D RID: 25373
			// (set) Token: 0x06008E6E RID: 36462 RVA: 0x000D08BF File Offset: 0x000CEABF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700631E RID: 25374
			// (set) Token: 0x06008E6F RID: 36463 RVA: 0x000D08D7 File Offset: 0x000CEAD7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700631F RID: 25375
			// (set) Token: 0x06008E70 RID: 36464 RVA: 0x000D08EF File Offset: 0x000CEAEF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006320 RID: 25376
			// (set) Token: 0x06008E71 RID: 36465 RVA: 0x000D0907 File Offset: 0x000CEB07
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006321 RID: 25377
			// (set) Token: 0x06008E72 RID: 36466 RVA: 0x000D091F File Offset: 0x000CEB1F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B85 RID: 2949
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006322 RID: 25378
			// (set) Token: 0x06008E74 RID: 36468 RVA: 0x000D093F File Offset: 0x000CEB3F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006323 RID: 25379
			// (set) Token: 0x06008E75 RID: 36469 RVA: 0x000D095D File Offset: 0x000CEB5D
			public virtual string Pin
			{
				set
				{
					base.PowerSharpParameters["Pin"] = value;
				}
			}

			// Token: 0x17006324 RID: 25380
			// (set) Token: 0x06008E76 RID: 36470 RVA: 0x000D0970 File Offset: 0x000CEB70
			public virtual bool PinExpired
			{
				set
				{
					base.PowerSharpParameters["PinExpired"] = value;
				}
			}

			// Token: 0x17006325 RID: 25381
			// (set) Token: 0x06008E77 RID: 36471 RVA: 0x000D0988 File Offset: 0x000CEB88
			public virtual bool LockedOut
			{
				set
				{
					base.PowerSharpParameters["LockedOut"] = value;
				}
			}

			// Token: 0x17006326 RID: 25382
			// (set) Token: 0x06008E78 RID: 36472 RVA: 0x000D09A0 File Offset: 0x000CEBA0
			public virtual string NotifyEmail
			{
				set
				{
					base.PowerSharpParameters["NotifyEmail"] = value;
				}
			}

			// Token: 0x17006327 RID: 25383
			// (set) Token: 0x06008E79 RID: 36473 RVA: 0x000D09B3 File Offset: 0x000CEBB3
			public virtual bool SendEmail
			{
				set
				{
					base.PowerSharpParameters["SendEmail"] = value;
				}
			}

			// Token: 0x17006328 RID: 25384
			// (set) Token: 0x06008E7A RID: 36474 RVA: 0x000D09CB File Offset: 0x000CEBCB
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006329 RID: 25385
			// (set) Token: 0x06008E7B RID: 36475 RVA: 0x000D09E3 File Offset: 0x000CEBE3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700632A RID: 25386
			// (set) Token: 0x06008E7C RID: 36476 RVA: 0x000D09F6 File Offset: 0x000CEBF6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700632B RID: 25387
			// (set) Token: 0x06008E7D RID: 36477 RVA: 0x000D0A0E File Offset: 0x000CEC0E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700632C RID: 25388
			// (set) Token: 0x06008E7E RID: 36478 RVA: 0x000D0A26 File Offset: 0x000CEC26
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700632D RID: 25389
			// (set) Token: 0x06008E7F RID: 36479 RVA: 0x000D0A3E File Offset: 0x000CEC3E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700632E RID: 25390
			// (set) Token: 0x06008E80 RID: 36480 RVA: 0x000D0A56 File Offset: 0x000CEC56
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
