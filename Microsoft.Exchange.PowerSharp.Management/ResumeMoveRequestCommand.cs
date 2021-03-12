using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009DE RID: 2526
	public class ResumeMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<MoveRequestIdParameter>
	{
		// Token: 0x06007ED6 RID: 32470 RVA: 0x000BC72D File Offset: 0x000BA92D
		private ResumeMoveRequestCommand() : base("Resume-MoveRequest")
		{
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x000BC73A File Offset: 0x000BA93A
		public ResumeMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x000BC749 File Offset: 0x000BA949
		public virtual ResumeMoveRequestCommand SetParameters(ResumeMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x000BC753 File Offset: 0x000BA953
		public virtual ResumeMoveRequestCommand SetParameters(ResumeMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009DF RID: 2527
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170056D3 RID: 22227
			// (set) Token: 0x06007EDA RID: 32474 RVA: 0x000BC75D File Offset: 0x000BA95D
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170056D4 RID: 22228
			// (set) Token: 0x06007EDB RID: 32475 RVA: 0x000BC775 File Offset: 0x000BA975
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170056D5 RID: 22229
			// (set) Token: 0x06007EDC RID: 32476 RVA: 0x000BC793 File Offset: 0x000BA993
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056D6 RID: 22230
			// (set) Token: 0x06007EDD RID: 32477 RVA: 0x000BC7A6 File Offset: 0x000BA9A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056D7 RID: 22231
			// (set) Token: 0x06007EDE RID: 32478 RVA: 0x000BC7BE File Offset: 0x000BA9BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056D8 RID: 22232
			// (set) Token: 0x06007EDF RID: 32479 RVA: 0x000BC7D6 File Offset: 0x000BA9D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056D9 RID: 22233
			// (set) Token: 0x06007EE0 RID: 32480 RVA: 0x000BC7EE File Offset: 0x000BA9EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056DA RID: 22234
			// (set) Token: 0x06007EE1 RID: 32481 RVA: 0x000BC806 File Offset: 0x000BAA06
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009E0 RID: 2528
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170056DB RID: 22235
			// (set) Token: 0x06007EE3 RID: 32483 RVA: 0x000BC826 File Offset: 0x000BAA26
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170056DC RID: 22236
			// (set) Token: 0x06007EE4 RID: 32484 RVA: 0x000BC839 File Offset: 0x000BAA39
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170056DD RID: 22237
			// (set) Token: 0x06007EE5 RID: 32485 RVA: 0x000BC851 File Offset: 0x000BAA51
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170056DE RID: 22238
			// (set) Token: 0x06007EE6 RID: 32486 RVA: 0x000BC869 File Offset: 0x000BAA69
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170056DF RID: 22239
			// (set) Token: 0x06007EE7 RID: 32487 RVA: 0x000BC881 File Offset: 0x000BAA81
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170056E0 RID: 22240
			// (set) Token: 0x06007EE8 RID: 32488 RVA: 0x000BC899 File Offset: 0x000BAA99
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
