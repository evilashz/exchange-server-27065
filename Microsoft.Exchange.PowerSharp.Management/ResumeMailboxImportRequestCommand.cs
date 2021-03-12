using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A29 RID: 2601
	public class ResumeMailboxImportRequestCommand : SyntheticCommandWithPipelineInput<MailboxImportRequestIdParameter, MailboxImportRequestIdParameter>
	{
		// Token: 0x060081EF RID: 33263 RVA: 0x000C0760 File Offset: 0x000BE960
		private ResumeMailboxImportRequestCommand() : base("Resume-MailboxImportRequest")
		{
		}

		// Token: 0x060081F0 RID: 33264 RVA: 0x000C076D File Offset: 0x000BE96D
		public ResumeMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x000C077C File Offset: 0x000BE97C
		public virtual ResumeMailboxImportRequestCommand SetParameters(ResumeMailboxImportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x000C0786 File Offset: 0x000BE986
		public virtual ResumeMailboxImportRequestCommand SetParameters(ResumeMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A2A RID: 2602
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005956 RID: 22870
			// (set) Token: 0x060081F3 RID: 33267 RVA: 0x000C0790 File Offset: 0x000BE990
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005957 RID: 22871
			// (set) Token: 0x060081F4 RID: 33268 RVA: 0x000C07AE File Offset: 0x000BE9AE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005958 RID: 22872
			// (set) Token: 0x060081F5 RID: 33269 RVA: 0x000C07C1 File Offset: 0x000BE9C1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005959 RID: 22873
			// (set) Token: 0x060081F6 RID: 33270 RVA: 0x000C07D9 File Offset: 0x000BE9D9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700595A RID: 22874
			// (set) Token: 0x060081F7 RID: 33271 RVA: 0x000C07F1 File Offset: 0x000BE9F1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700595B RID: 22875
			// (set) Token: 0x060081F8 RID: 33272 RVA: 0x000C0809 File Offset: 0x000BEA09
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700595C RID: 22876
			// (set) Token: 0x060081F9 RID: 33273 RVA: 0x000C0821 File Offset: 0x000BEA21
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A2B RID: 2603
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700595D RID: 22877
			// (set) Token: 0x060081FB RID: 33275 RVA: 0x000C0841 File Offset: 0x000BEA41
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700595E RID: 22878
			// (set) Token: 0x060081FC RID: 33276 RVA: 0x000C0854 File Offset: 0x000BEA54
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700595F RID: 22879
			// (set) Token: 0x060081FD RID: 33277 RVA: 0x000C086C File Offset: 0x000BEA6C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005960 RID: 22880
			// (set) Token: 0x060081FE RID: 33278 RVA: 0x000C0884 File Offset: 0x000BEA84
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005961 RID: 22881
			// (set) Token: 0x060081FF RID: 33279 RVA: 0x000C089C File Offset: 0x000BEA9C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005962 RID: 22882
			// (set) Token: 0x06008200 RID: 33280 RVA: 0x000C08B4 File Offset: 0x000BEAB4
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
