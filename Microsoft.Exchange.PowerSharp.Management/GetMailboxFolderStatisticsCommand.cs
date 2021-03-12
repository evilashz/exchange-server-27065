using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000196 RID: 406
	public class GetMailboxFolderStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxFolderConfiguration, MailboxFolderConfiguration>
	{
		// Token: 0x06002388 RID: 9096 RVA: 0x0004597C File Offset: 0x00043B7C
		private GetMailboxFolderStatisticsCommand() : base("Get-MailboxFolderStatistics")
		{
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00045989 File Offset: 0x00043B89
		public GetMailboxFolderStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00045998 File Offset: 0x00043B98
		public virtual GetMailboxFolderStatisticsCommand SetParameters(GetMailboxFolderStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x000459A2 File Offset: 0x00043BA2
		public virtual GetMailboxFolderStatisticsCommand SetParameters(GetMailboxFolderStatisticsCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x000459AC File Offset: 0x00043BAC
		public virtual GetMailboxFolderStatisticsCommand SetParameters(GetMailboxFolderStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000197 RID: 407
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000C15 RID: 3093
			// (set) Token: 0x0600238D RID: 9101 RVA: 0x000459B6 File Offset: 0x00043BB6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17000C16 RID: 3094
			// (set) Token: 0x0600238E RID: 9102 RVA: 0x000459D4 File Offset: 0x00043BD4
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17000C17 RID: 3095
			// (set) Token: 0x0600238F RID: 9103 RVA: 0x000459EC File Offset: 0x00043BEC
			public virtual ElcFolderType? FolderScope
			{
				set
				{
					base.PowerSharpParameters["FolderScope"] = value;
				}
			}

			// Token: 0x17000C18 RID: 3096
			// (set) Token: 0x06002390 RID: 9104 RVA: 0x00045A04 File Offset: 0x00043C04
			public virtual SwitchParameter IncludeOldestAndNewestItems
			{
				set
				{
					base.PowerSharpParameters["IncludeOldestAndNewestItems"] = value;
				}
			}

			// Token: 0x17000C19 RID: 3097
			// (set) Token: 0x06002391 RID: 9105 RVA: 0x00045A1C File Offset: 0x00043C1C
			public virtual SwitchParameter IncludeAnalysis
			{
				set
				{
					base.PowerSharpParameters["IncludeAnalysis"] = value;
				}
			}

			// Token: 0x17000C1A RID: 3098
			// (set) Token: 0x06002392 RID: 9106 RVA: 0x00045A34 File Offset: 0x00043C34
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C1B RID: 3099
			// (set) Token: 0x06002393 RID: 9107 RVA: 0x00045A47 File Offset: 0x00043C47
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C1C RID: 3100
			// (set) Token: 0x06002394 RID: 9108 RVA: 0x00045A5F File Offset: 0x00043C5F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C1D RID: 3101
			// (set) Token: 0x06002395 RID: 9109 RVA: 0x00045A77 File Offset: 0x00043C77
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C1E RID: 3102
			// (set) Token: 0x06002396 RID: 9110 RVA: 0x00045A8F File Offset: 0x00043C8F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000198 RID: 408
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x17000C1F RID: 3103
			// (set) Token: 0x06002398 RID: 9112 RVA: 0x00045AAF File Offset: 0x00043CAF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17000C20 RID: 3104
			// (set) Token: 0x06002399 RID: 9113 RVA: 0x00045ACD File Offset: 0x00043CCD
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17000C21 RID: 3105
			// (set) Token: 0x0600239A RID: 9114 RVA: 0x00045AE5 File Offset: 0x00043CE5
			public virtual ElcFolderType? FolderScope
			{
				set
				{
					base.PowerSharpParameters["FolderScope"] = value;
				}
			}

			// Token: 0x17000C22 RID: 3106
			// (set) Token: 0x0600239B RID: 9115 RVA: 0x00045AFD File Offset: 0x00043CFD
			public virtual SwitchParameter IncludeOldestAndNewestItems
			{
				set
				{
					base.PowerSharpParameters["IncludeOldestAndNewestItems"] = value;
				}
			}

			// Token: 0x17000C23 RID: 3107
			// (set) Token: 0x0600239C RID: 9116 RVA: 0x00045B15 File Offset: 0x00043D15
			public virtual SwitchParameter IncludeAnalysis
			{
				set
				{
					base.PowerSharpParameters["IncludeAnalysis"] = value;
				}
			}

			// Token: 0x17000C24 RID: 3108
			// (set) Token: 0x0600239D RID: 9117 RVA: 0x00045B2D File Offset: 0x00043D2D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C25 RID: 3109
			// (set) Token: 0x0600239E RID: 9118 RVA: 0x00045B40 File Offset: 0x00043D40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C26 RID: 3110
			// (set) Token: 0x0600239F RID: 9119 RVA: 0x00045B58 File Offset: 0x00043D58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C27 RID: 3111
			// (set) Token: 0x060023A0 RID: 9120 RVA: 0x00045B70 File Offset: 0x00043D70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C28 RID: 3112
			// (set) Token: 0x060023A1 RID: 9121 RVA: 0x00045B88 File Offset: 0x00043D88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000199 RID: 409
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000C29 RID: 3113
			// (set) Token: 0x060023A3 RID: 9123 RVA: 0x00045BA8 File Offset: 0x00043DA8
			public virtual ElcFolderType? FolderScope
			{
				set
				{
					base.PowerSharpParameters["FolderScope"] = value;
				}
			}

			// Token: 0x17000C2A RID: 3114
			// (set) Token: 0x060023A4 RID: 9124 RVA: 0x00045BC0 File Offset: 0x00043DC0
			public virtual SwitchParameter IncludeOldestAndNewestItems
			{
				set
				{
					base.PowerSharpParameters["IncludeOldestAndNewestItems"] = value;
				}
			}

			// Token: 0x17000C2B RID: 3115
			// (set) Token: 0x060023A5 RID: 9125 RVA: 0x00045BD8 File Offset: 0x00043DD8
			public virtual SwitchParameter IncludeAnalysis
			{
				set
				{
					base.PowerSharpParameters["IncludeAnalysis"] = value;
				}
			}

			// Token: 0x17000C2C RID: 3116
			// (set) Token: 0x060023A6 RID: 9126 RVA: 0x00045BF0 File Offset: 0x00043DF0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000C2D RID: 3117
			// (set) Token: 0x060023A7 RID: 9127 RVA: 0x00045C03 File Offset: 0x00043E03
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000C2E RID: 3118
			// (set) Token: 0x060023A8 RID: 9128 RVA: 0x00045C1B File Offset: 0x00043E1B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000C2F RID: 3119
			// (set) Token: 0x060023A9 RID: 9129 RVA: 0x00045C33 File Offset: 0x00043E33
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000C30 RID: 3120
			// (set) Token: 0x060023AA RID: 9130 RVA: 0x00045C4B File Offset: 0x00043E4B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
