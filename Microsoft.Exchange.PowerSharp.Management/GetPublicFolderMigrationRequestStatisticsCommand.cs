using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A7E RID: 2686
	public class GetPublicFolderMigrationRequestStatisticsCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequestStatistics, PublicFolderMigrationRequestStatistics>
	{
		// Token: 0x0600851D RID: 34077 RVA: 0x000C48C9 File Offset: 0x000C2AC9
		private GetPublicFolderMigrationRequestStatisticsCommand() : base("Get-PublicFolderMigrationRequestStatistics")
		{
		}

		// Token: 0x0600851E RID: 34078 RVA: 0x000C48D6 File Offset: 0x000C2AD6
		public GetPublicFolderMigrationRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600851F RID: 34079 RVA: 0x000C48E5 File Offset: 0x000C2AE5
		public virtual GetPublicFolderMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMigrationRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008520 RID: 34080 RVA: 0x000C48EF File Offset: 0x000C2AEF
		public virtual GetPublicFolderMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMigrationRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008521 RID: 34081 RVA: 0x000C48F9 File Offset: 0x000C2AF9
		public virtual GetPublicFolderMigrationRequestStatisticsCommand SetParameters(GetPublicFolderMigrationRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A7F RID: 2687
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005BDA RID: 23514
			// (set) Token: 0x06008522 RID: 34082 RVA: 0x000C4903 File Offset: 0x000C2B03
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005BDB RID: 23515
			// (set) Token: 0x06008523 RID: 34083 RVA: 0x000C4921 File Offset: 0x000C2B21
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005BDC RID: 23516
			// (set) Token: 0x06008524 RID: 34084 RVA: 0x000C4939 File Offset: 0x000C2B39
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BDD RID: 23517
			// (set) Token: 0x06008525 RID: 34085 RVA: 0x000C494C File Offset: 0x000C2B4C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005BDE RID: 23518
			// (set) Token: 0x06008526 RID: 34086 RVA: 0x000C4964 File Offset: 0x000C2B64
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005BDF RID: 23519
			// (set) Token: 0x06008527 RID: 34087 RVA: 0x000C4977 File Offset: 0x000C2B77
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BE0 RID: 23520
			// (set) Token: 0x06008528 RID: 34088 RVA: 0x000C498F File Offset: 0x000C2B8F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BE1 RID: 23521
			// (set) Token: 0x06008529 RID: 34089 RVA: 0x000C49A7 File Offset: 0x000C2BA7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BE2 RID: 23522
			// (set) Token: 0x0600852A RID: 34090 RVA: 0x000C49BF File Offset: 0x000C2BBF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A80 RID: 2688
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005BE3 RID: 23523
			// (set) Token: 0x0600852C RID: 34092 RVA: 0x000C49DF File Offset: 0x000C2BDF
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005BE4 RID: 23524
			// (set) Token: 0x0600852D RID: 34093 RVA: 0x000C49F7 File Offset: 0x000C2BF7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BE5 RID: 23525
			// (set) Token: 0x0600852E RID: 34094 RVA: 0x000C4A0A File Offset: 0x000C2C0A
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005BE6 RID: 23526
			// (set) Token: 0x0600852F RID: 34095 RVA: 0x000C4A22 File Offset: 0x000C2C22
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005BE7 RID: 23527
			// (set) Token: 0x06008530 RID: 34096 RVA: 0x000C4A35 File Offset: 0x000C2C35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BE8 RID: 23528
			// (set) Token: 0x06008531 RID: 34097 RVA: 0x000C4A4D File Offset: 0x000C2C4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BE9 RID: 23529
			// (set) Token: 0x06008532 RID: 34098 RVA: 0x000C4A65 File Offset: 0x000C2C65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BEA RID: 23530
			// (set) Token: 0x06008533 RID: 34099 RVA: 0x000C4A7D File Offset: 0x000C2C7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A81 RID: 2689
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005BEB RID: 23531
			// (set) Token: 0x06008535 RID: 34101 RVA: 0x000C4A9D File Offset: 0x000C2C9D
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005BEC RID: 23532
			// (set) Token: 0x06008536 RID: 34102 RVA: 0x000C4AB0 File Offset: 0x000C2CB0
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005BED RID: 23533
			// (set) Token: 0x06008537 RID: 34103 RVA: 0x000C4AC8 File Offset: 0x000C2CC8
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005BEE RID: 23534
			// (set) Token: 0x06008538 RID: 34104 RVA: 0x000C4AE0 File Offset: 0x000C2CE0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BEF RID: 23535
			// (set) Token: 0x06008539 RID: 34105 RVA: 0x000C4AF3 File Offset: 0x000C2CF3
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005BF0 RID: 23536
			// (set) Token: 0x0600853A RID: 34106 RVA: 0x000C4B0B File Offset: 0x000C2D0B
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005BF1 RID: 23537
			// (set) Token: 0x0600853B RID: 34107 RVA: 0x000C4B1E File Offset: 0x000C2D1E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BF2 RID: 23538
			// (set) Token: 0x0600853C RID: 34108 RVA: 0x000C4B36 File Offset: 0x000C2D36
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BF3 RID: 23539
			// (set) Token: 0x0600853D RID: 34109 RVA: 0x000C4B4E File Offset: 0x000C2D4E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BF4 RID: 23540
			// (set) Token: 0x0600853E RID: 34110 RVA: 0x000C4B66 File Offset: 0x000C2D66
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
