using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A10 RID: 2576
	public class ResumeMailboxExportRequestCommand : SyntheticCommandWithPipelineInput<MailboxExportRequestIdParameter, MailboxExportRequestIdParameter>
	{
		// Token: 0x060080FD RID: 33021 RVA: 0x000BF3F6 File Offset: 0x000BD5F6
		private ResumeMailboxExportRequestCommand() : base("Resume-MailboxExportRequest")
		{
		}

		// Token: 0x060080FE RID: 33022 RVA: 0x000BF403 File Offset: 0x000BD603
		public ResumeMailboxExportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060080FF RID: 33023 RVA: 0x000BF412 File Offset: 0x000BD612
		public virtual ResumeMailboxExportRequestCommand SetParameters(ResumeMailboxExportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008100 RID: 33024 RVA: 0x000BF41C File Offset: 0x000BD61C
		public virtual ResumeMailboxExportRequestCommand SetParameters(ResumeMailboxExportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A11 RID: 2577
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005896 RID: 22678
			// (set) Token: 0x06008101 RID: 33025 RVA: 0x000BF426 File Offset: 0x000BD626
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005897 RID: 22679
			// (set) Token: 0x06008102 RID: 33026 RVA: 0x000BF444 File Offset: 0x000BD644
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005898 RID: 22680
			// (set) Token: 0x06008103 RID: 33027 RVA: 0x000BF457 File Offset: 0x000BD657
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005899 RID: 22681
			// (set) Token: 0x06008104 RID: 33028 RVA: 0x000BF46F File Offset: 0x000BD66F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700589A RID: 22682
			// (set) Token: 0x06008105 RID: 33029 RVA: 0x000BF487 File Offset: 0x000BD687
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700589B RID: 22683
			// (set) Token: 0x06008106 RID: 33030 RVA: 0x000BF49F File Offset: 0x000BD69F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700589C RID: 22684
			// (set) Token: 0x06008107 RID: 33031 RVA: 0x000BF4B7 File Offset: 0x000BD6B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A12 RID: 2578
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700589D RID: 22685
			// (set) Token: 0x06008109 RID: 33033 RVA: 0x000BF4D7 File Offset: 0x000BD6D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700589E RID: 22686
			// (set) Token: 0x0600810A RID: 33034 RVA: 0x000BF4EA File Offset: 0x000BD6EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700589F RID: 22687
			// (set) Token: 0x0600810B RID: 33035 RVA: 0x000BF502 File Offset: 0x000BD702
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058A0 RID: 22688
			// (set) Token: 0x0600810C RID: 33036 RVA: 0x000BF51A File Offset: 0x000BD71A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058A1 RID: 22689
			// (set) Token: 0x0600810D RID: 33037 RVA: 0x000BF532 File Offset: 0x000BD732
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170058A2 RID: 22690
			// (set) Token: 0x0600810E RID: 33038 RVA: 0x000BF54A File Offset: 0x000BD74A
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
