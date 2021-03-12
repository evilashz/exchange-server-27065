using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009E4 RID: 2532
	public class SuspendMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MoveRequestIdParameter>
	{
		// Token: 0x06007F25 RID: 32549 RVA: 0x000BCDB1 File Offset: 0x000BAFB1
		private SuspendMoveRequestCommand() : base("Suspend-MoveRequest")
		{
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x000BCDBE File Offset: 0x000BAFBE
		public SuspendMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x000BCDCD File Offset: 0x000BAFCD
		public virtual SuspendMoveRequestCommand SetParameters(SuspendMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x000BCDD7 File Offset: 0x000BAFD7
		public virtual SuspendMoveRequestCommand SetParameters(SuspendMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009E5 RID: 2533
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005716 RID: 22294
			// (set) Token: 0x06007F29 RID: 32553 RVA: 0x000BCDE1 File Offset: 0x000BAFE1
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005717 RID: 22295
			// (set) Token: 0x06007F2A RID: 32554 RVA: 0x000BCDF4 File Offset: 0x000BAFF4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005718 RID: 22296
			// (set) Token: 0x06007F2B RID: 32555 RVA: 0x000BCE07 File Offset: 0x000BB007
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005719 RID: 22297
			// (set) Token: 0x06007F2C RID: 32556 RVA: 0x000BCE1F File Offset: 0x000BB01F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700571A RID: 22298
			// (set) Token: 0x06007F2D RID: 32557 RVA: 0x000BCE37 File Offset: 0x000BB037
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700571B RID: 22299
			// (set) Token: 0x06007F2E RID: 32558 RVA: 0x000BCE4F File Offset: 0x000BB04F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700571C RID: 22300
			// (set) Token: 0x06007F2F RID: 32559 RVA: 0x000BCE67 File Offset: 0x000BB067
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700571D RID: 22301
			// (set) Token: 0x06007F30 RID: 32560 RVA: 0x000BCE7F File Offset: 0x000BB07F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020009E6 RID: 2534
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700571E RID: 22302
			// (set) Token: 0x06007F32 RID: 32562 RVA: 0x000BCE9F File Offset: 0x000BB09F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700571F RID: 22303
			// (set) Token: 0x06007F33 RID: 32563 RVA: 0x000BCEBD File Offset: 0x000BB0BD
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005720 RID: 22304
			// (set) Token: 0x06007F34 RID: 32564 RVA: 0x000BCED0 File Offset: 0x000BB0D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005721 RID: 22305
			// (set) Token: 0x06007F35 RID: 32565 RVA: 0x000BCEE3 File Offset: 0x000BB0E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005722 RID: 22306
			// (set) Token: 0x06007F36 RID: 32566 RVA: 0x000BCEFB File Offset: 0x000BB0FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005723 RID: 22307
			// (set) Token: 0x06007F37 RID: 32567 RVA: 0x000BCF13 File Offset: 0x000BB113
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005724 RID: 22308
			// (set) Token: 0x06007F38 RID: 32568 RVA: 0x000BCF2B File Offset: 0x000BB12B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005725 RID: 22309
			// (set) Token: 0x06007F39 RID: 32569 RVA: 0x000BCF43 File Offset: 0x000BB143
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005726 RID: 22310
			// (set) Token: 0x06007F3A RID: 32570 RVA: 0x000BCF5B File Offset: 0x000BB15B
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
