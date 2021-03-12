using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A59 RID: 2649
	public class ResumeMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequestIdParameter, MailboxRestoreRequestIdParameter>
	{
		// Token: 0x060083DB RID: 33755 RVA: 0x000C2F22 File Offset: 0x000C1122
		private ResumeMailboxRestoreRequestCommand() : base("Resume-MailboxRestoreRequest")
		{
		}

		// Token: 0x060083DC RID: 33756 RVA: 0x000C2F2F File Offset: 0x000C112F
		public ResumeMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060083DD RID: 33757 RVA: 0x000C2F3E File Offset: 0x000C113E
		public virtual ResumeMailboxRestoreRequestCommand SetParameters(ResumeMailboxRestoreRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060083DE RID: 33758 RVA: 0x000C2F48 File Offset: 0x000C1148
		public virtual ResumeMailboxRestoreRequestCommand SetParameters(ResumeMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A5A RID: 2650
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005AE2 RID: 23266
			// (set) Token: 0x060083DF RID: 33759 RVA: 0x000C2F52 File Offset: 0x000C1152
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005AE3 RID: 23267
			// (set) Token: 0x060083E0 RID: 33760 RVA: 0x000C2F70 File Offset: 0x000C1170
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005AE4 RID: 23268
			// (set) Token: 0x060083E1 RID: 33761 RVA: 0x000C2F83 File Offset: 0x000C1183
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AE5 RID: 23269
			// (set) Token: 0x060083E2 RID: 33762 RVA: 0x000C2F9B File Offset: 0x000C119B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AE6 RID: 23270
			// (set) Token: 0x060083E3 RID: 33763 RVA: 0x000C2FB3 File Offset: 0x000C11B3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AE7 RID: 23271
			// (set) Token: 0x060083E4 RID: 33764 RVA: 0x000C2FCB File Offset: 0x000C11CB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AE8 RID: 23272
			// (set) Token: 0x060083E5 RID: 33765 RVA: 0x000C2FE3 File Offset: 0x000C11E3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A5B RID: 2651
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005AE9 RID: 23273
			// (set) Token: 0x060083E7 RID: 33767 RVA: 0x000C3003 File Offset: 0x000C1203
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005AEA RID: 23274
			// (set) Token: 0x060083E8 RID: 33768 RVA: 0x000C3016 File Offset: 0x000C1216
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AEB RID: 23275
			// (set) Token: 0x060083E9 RID: 33769 RVA: 0x000C302E File Offset: 0x000C122E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AEC RID: 23276
			// (set) Token: 0x060083EA RID: 33770 RVA: 0x000C3046 File Offset: 0x000C1246
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AED RID: 23277
			// (set) Token: 0x060083EB RID: 33771 RVA: 0x000C305E File Offset: 0x000C125E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AEE RID: 23278
			// (set) Token: 0x060083EC RID: 33772 RVA: 0x000C3076 File Offset: 0x000C1276
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
