using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A67 RID: 2663
	public class GetPublicFolderMoveRequestStatisticsCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequestStatistics, PublicFolderMoveRequestStatistics>
	{
		// Token: 0x06008456 RID: 33878 RVA: 0x000C38E6 File Offset: 0x000C1AE6
		private GetPublicFolderMoveRequestStatisticsCommand() : base("Get-PublicFolderMoveRequestStatistics")
		{
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x000C38F3 File Offset: 0x000C1AF3
		public GetPublicFolderMoveRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x000C3902 File Offset: 0x000C1B02
		public virtual GetPublicFolderMoveRequestStatisticsCommand SetParameters(GetPublicFolderMoveRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008459 RID: 33881 RVA: 0x000C390C File Offset: 0x000C1B0C
		public virtual GetPublicFolderMoveRequestStatisticsCommand SetParameters(GetPublicFolderMoveRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600845A RID: 33882 RVA: 0x000C3916 File Offset: 0x000C1B16
		public virtual GetPublicFolderMoveRequestStatisticsCommand SetParameters(GetPublicFolderMoveRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A68 RID: 2664
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B41 RID: 23361
			// (set) Token: 0x0600845B RID: 33883 RVA: 0x000C3920 File Offset: 0x000C1B20
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B42 RID: 23362
			// (set) Token: 0x0600845C RID: 33884 RVA: 0x000C393E File Offset: 0x000C1B3E
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005B43 RID: 23363
			// (set) Token: 0x0600845D RID: 33885 RVA: 0x000C3956 File Offset: 0x000C1B56
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B44 RID: 23364
			// (set) Token: 0x0600845E RID: 33886 RVA: 0x000C3969 File Offset: 0x000C1B69
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005B45 RID: 23365
			// (set) Token: 0x0600845F RID: 33887 RVA: 0x000C3981 File Offset: 0x000C1B81
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005B46 RID: 23366
			// (set) Token: 0x06008460 RID: 33888 RVA: 0x000C3994 File Offset: 0x000C1B94
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B47 RID: 23367
			// (set) Token: 0x06008461 RID: 33889 RVA: 0x000C39AC File Offset: 0x000C1BAC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B48 RID: 23368
			// (set) Token: 0x06008462 RID: 33890 RVA: 0x000C39C4 File Offset: 0x000C1BC4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B49 RID: 23369
			// (set) Token: 0x06008463 RID: 33891 RVA: 0x000C39DC File Offset: 0x000C1BDC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A69 RID: 2665
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B4A RID: 23370
			// (set) Token: 0x06008465 RID: 33893 RVA: 0x000C39FC File Offset: 0x000C1BFC
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005B4B RID: 23371
			// (set) Token: 0x06008466 RID: 33894 RVA: 0x000C3A14 File Offset: 0x000C1C14
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B4C RID: 23372
			// (set) Token: 0x06008467 RID: 33895 RVA: 0x000C3A27 File Offset: 0x000C1C27
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005B4D RID: 23373
			// (set) Token: 0x06008468 RID: 33896 RVA: 0x000C3A3F File Offset: 0x000C1C3F
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005B4E RID: 23374
			// (set) Token: 0x06008469 RID: 33897 RVA: 0x000C3A52 File Offset: 0x000C1C52
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B4F RID: 23375
			// (set) Token: 0x0600846A RID: 33898 RVA: 0x000C3A6A File Offset: 0x000C1C6A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B50 RID: 23376
			// (set) Token: 0x0600846B RID: 33899 RVA: 0x000C3A82 File Offset: 0x000C1C82
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B51 RID: 23377
			// (set) Token: 0x0600846C RID: 33900 RVA: 0x000C3A9A File Offset: 0x000C1C9A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A6A RID: 2666
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005B52 RID: 23378
			// (set) Token: 0x0600846E RID: 33902 RVA: 0x000C3ABA File Offset: 0x000C1CBA
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005B53 RID: 23379
			// (set) Token: 0x0600846F RID: 33903 RVA: 0x000C3ACD File Offset: 0x000C1CCD
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005B54 RID: 23380
			// (set) Token: 0x06008470 RID: 33904 RVA: 0x000C3AE5 File Offset: 0x000C1CE5
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005B55 RID: 23381
			// (set) Token: 0x06008471 RID: 33905 RVA: 0x000C3AFD File Offset: 0x000C1CFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B56 RID: 23382
			// (set) Token: 0x06008472 RID: 33906 RVA: 0x000C3B10 File Offset: 0x000C1D10
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005B57 RID: 23383
			// (set) Token: 0x06008473 RID: 33907 RVA: 0x000C3B28 File Offset: 0x000C1D28
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005B58 RID: 23384
			// (set) Token: 0x06008474 RID: 33908 RVA: 0x000C3B3B File Offset: 0x000C1D3B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B59 RID: 23385
			// (set) Token: 0x06008475 RID: 33909 RVA: 0x000C3B53 File Offset: 0x000C1D53
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B5A RID: 23386
			// (set) Token: 0x06008476 RID: 33910 RVA: 0x000C3B6B File Offset: 0x000C1D6B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B5B RID: 23387
			// (set) Token: 0x06008477 RID: 33911 RVA: 0x000C3B83 File Offset: 0x000C1D83
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
