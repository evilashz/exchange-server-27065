using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AA9 RID: 2729
	public class GetPublicFolderMailboxMigrationRequestStatisticsCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequestStatistics, PublicFolderMailboxMigrationRequestStatistics>
	{
		// Token: 0x06008704 RID: 34564 RVA: 0x000C7072 File Offset: 0x000C5272
		private GetPublicFolderMailboxMigrationRequestStatisticsCommand() : base("Get-PublicFolderMailboxMigrationRequestStatistics")
		{
		}

		// Token: 0x06008705 RID: 34565 RVA: 0x000C707F File Offset: 0x000C527F
		public GetPublicFolderMailboxMigrationRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008706 RID: 34566 RVA: 0x000C708E File Offset: 0x000C528E
		public virtual GetPublicFolderMailboxMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMailboxMigrationRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x000C7098 File Offset: 0x000C5298
		public virtual GetPublicFolderMailboxMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMailboxMigrationRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x000C70A2 File Offset: 0x000C52A2
		public virtual GetPublicFolderMailboxMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMailboxMigrationRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AAA RID: 2730
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005D6B RID: 23915
			// (set) Token: 0x06008709 RID: 34569 RVA: 0x000C70AC File Offset: 0x000C52AC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D6C RID: 23916
			// (set) Token: 0x0600870A RID: 34570 RVA: 0x000C70CA File Offset: 0x000C52CA
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005D6D RID: 23917
			// (set) Token: 0x0600870B RID: 34571 RVA: 0x000C70E2 File Offset: 0x000C52E2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D6E RID: 23918
			// (set) Token: 0x0600870C RID: 34572 RVA: 0x000C70F5 File Offset: 0x000C52F5
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005D6F RID: 23919
			// (set) Token: 0x0600870D RID: 34573 RVA: 0x000C710D File Offset: 0x000C530D
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005D70 RID: 23920
			// (set) Token: 0x0600870E RID: 34574 RVA: 0x000C7120 File Offset: 0x000C5320
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D71 RID: 23921
			// (set) Token: 0x0600870F RID: 34575 RVA: 0x000C7138 File Offset: 0x000C5338
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D72 RID: 23922
			// (set) Token: 0x06008710 RID: 34576 RVA: 0x000C7150 File Offset: 0x000C5350
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D73 RID: 23923
			// (set) Token: 0x06008711 RID: 34577 RVA: 0x000C7168 File Offset: 0x000C5368
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AAB RID: 2731
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D74 RID: 23924
			// (set) Token: 0x06008713 RID: 34579 RVA: 0x000C7188 File Offset: 0x000C5388
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005D75 RID: 23925
			// (set) Token: 0x06008714 RID: 34580 RVA: 0x000C71A0 File Offset: 0x000C53A0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D76 RID: 23926
			// (set) Token: 0x06008715 RID: 34581 RVA: 0x000C71B3 File Offset: 0x000C53B3
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005D77 RID: 23927
			// (set) Token: 0x06008716 RID: 34582 RVA: 0x000C71CB File Offset: 0x000C53CB
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005D78 RID: 23928
			// (set) Token: 0x06008717 RID: 34583 RVA: 0x000C71DE File Offset: 0x000C53DE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D79 RID: 23929
			// (set) Token: 0x06008718 RID: 34584 RVA: 0x000C71F6 File Offset: 0x000C53F6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D7A RID: 23930
			// (set) Token: 0x06008719 RID: 34585 RVA: 0x000C720E File Offset: 0x000C540E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D7B RID: 23931
			// (set) Token: 0x0600871A RID: 34586 RVA: 0x000C7226 File Offset: 0x000C5426
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AAC RID: 2732
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005D7C RID: 23932
			// (set) Token: 0x0600871C RID: 34588 RVA: 0x000C7246 File Offset: 0x000C5446
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005D7D RID: 23933
			// (set) Token: 0x0600871D RID: 34589 RVA: 0x000C7259 File Offset: 0x000C5459
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005D7E RID: 23934
			// (set) Token: 0x0600871E RID: 34590 RVA: 0x000C7271 File Offset: 0x000C5471
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005D7F RID: 23935
			// (set) Token: 0x0600871F RID: 34591 RVA: 0x000C7289 File Offset: 0x000C5489
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D80 RID: 23936
			// (set) Token: 0x06008720 RID: 34592 RVA: 0x000C729C File Offset: 0x000C549C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005D81 RID: 23937
			// (set) Token: 0x06008721 RID: 34593 RVA: 0x000C72B4 File Offset: 0x000C54B4
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005D82 RID: 23938
			// (set) Token: 0x06008722 RID: 34594 RVA: 0x000C72C7 File Offset: 0x000C54C7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D83 RID: 23939
			// (set) Token: 0x06008723 RID: 34595 RVA: 0x000C72DF File Offset: 0x000C54DF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D84 RID: 23940
			// (set) Token: 0x06008724 RID: 34596 RVA: 0x000C72F7 File Offset: 0x000C54F7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D85 RID: 23941
			// (set) Token: 0x06008725 RID: 34597 RVA: 0x000C730F File Offset: 0x000C550F
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
