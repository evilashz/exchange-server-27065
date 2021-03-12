using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A3F RID: 2623
	public class ResumeMailboxRelocationRequestCommand : SyntheticCommandWithPipelineInput<MailboxRelocationRequestIdParameter, MailboxRelocationRequestIdParameter>
	{
		// Token: 0x060082C4 RID: 33476 RVA: 0x000C188E File Offset: 0x000BFA8E
		private ResumeMailboxRelocationRequestCommand() : base("Resume-MailboxRelocationRequest")
		{
		}

		// Token: 0x060082C5 RID: 33477 RVA: 0x000C189B File Offset: 0x000BFA9B
		public ResumeMailboxRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060082C6 RID: 33478 RVA: 0x000C18AA File Offset: 0x000BFAAA
		public virtual ResumeMailboxRelocationRequestCommand SetParameters(ResumeMailboxRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082C7 RID: 33479 RVA: 0x000C18B4 File Offset: 0x000BFAB4
		public virtual ResumeMailboxRelocationRequestCommand SetParameters(ResumeMailboxRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A40 RID: 2624
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170059FF RID: 23039
			// (set) Token: 0x060082C8 RID: 33480 RVA: 0x000C18BE File Offset: 0x000BFABE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A00 RID: 23040
			// (set) Token: 0x060082C9 RID: 33481 RVA: 0x000C18DC File Offset: 0x000BFADC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A01 RID: 23041
			// (set) Token: 0x060082CA RID: 33482 RVA: 0x000C18EF File Offset: 0x000BFAEF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A02 RID: 23042
			// (set) Token: 0x060082CB RID: 33483 RVA: 0x000C1907 File Offset: 0x000BFB07
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A03 RID: 23043
			// (set) Token: 0x060082CC RID: 33484 RVA: 0x000C191F File Offset: 0x000BFB1F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A04 RID: 23044
			// (set) Token: 0x060082CD RID: 33485 RVA: 0x000C1937 File Offset: 0x000BFB37
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A05 RID: 23045
			// (set) Token: 0x060082CE RID: 33486 RVA: 0x000C194F File Offset: 0x000BFB4F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A41 RID: 2625
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A06 RID: 23046
			// (set) Token: 0x060082D0 RID: 33488 RVA: 0x000C196F File Offset: 0x000BFB6F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A07 RID: 23047
			// (set) Token: 0x060082D1 RID: 33489 RVA: 0x000C1982 File Offset: 0x000BFB82
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A08 RID: 23048
			// (set) Token: 0x060082D2 RID: 33490 RVA: 0x000C199A File Offset: 0x000BFB9A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A09 RID: 23049
			// (set) Token: 0x060082D3 RID: 33491 RVA: 0x000C19B2 File Offset: 0x000BFBB2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A0A RID: 23050
			// (set) Token: 0x060082D4 RID: 33492 RVA: 0x000C19CA File Offset: 0x000BFBCA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A0B RID: 23051
			// (set) Token: 0x060082D5 RID: 33493 RVA: 0x000C19E2 File Offset: 0x000BFBE2
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
