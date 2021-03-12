using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A4D RID: 2637
	public class GetMailboxRestoreRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequestStatistics, MailboxRestoreRequestStatistics>
	{
		// Token: 0x0600833A RID: 33594 RVA: 0x000C21F9 File Offset: 0x000C03F9
		private GetMailboxRestoreRequestStatisticsCommand() : base("Get-MailboxRestoreRequestStatistics")
		{
		}

		// Token: 0x0600833B RID: 33595 RVA: 0x000C2206 File Offset: 0x000C0406
		public GetMailboxRestoreRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600833C RID: 33596 RVA: 0x000C2215 File Offset: 0x000C0415
		public virtual GetMailboxRestoreRequestStatisticsCommand SetParameters(GetMailboxRestoreRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600833D RID: 33597 RVA: 0x000C221F File Offset: 0x000C041F
		public virtual GetMailboxRestoreRequestStatisticsCommand SetParameters(GetMailboxRestoreRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600833E RID: 33598 RVA: 0x000C2229 File Offset: 0x000C0429
		public virtual GetMailboxRestoreRequestStatisticsCommand SetParameters(GetMailboxRestoreRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A4E RID: 2638
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005A59 RID: 23129
			// (set) Token: 0x0600833F RID: 33599 RVA: 0x000C2233 File Offset: 0x000C0433
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A5A RID: 23130
			// (set) Token: 0x06008340 RID: 33600 RVA: 0x000C2251 File Offset: 0x000C0451
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005A5B RID: 23131
			// (set) Token: 0x06008341 RID: 33601 RVA: 0x000C2269 File Offset: 0x000C0469
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A5C RID: 23132
			// (set) Token: 0x06008342 RID: 33602 RVA: 0x000C227C File Offset: 0x000C047C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005A5D RID: 23133
			// (set) Token: 0x06008343 RID: 33603 RVA: 0x000C2294 File Offset: 0x000C0494
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005A5E RID: 23134
			// (set) Token: 0x06008344 RID: 33604 RVA: 0x000C22A7 File Offset: 0x000C04A7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A5F RID: 23135
			// (set) Token: 0x06008345 RID: 33605 RVA: 0x000C22BF File Offset: 0x000C04BF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A60 RID: 23136
			// (set) Token: 0x06008346 RID: 33606 RVA: 0x000C22D7 File Offset: 0x000C04D7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A61 RID: 23137
			// (set) Token: 0x06008347 RID: 33607 RVA: 0x000C22EF File Offset: 0x000C04EF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A4F RID: 2639
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A62 RID: 23138
			// (set) Token: 0x06008349 RID: 33609 RVA: 0x000C230F File Offset: 0x000C050F
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005A63 RID: 23139
			// (set) Token: 0x0600834A RID: 33610 RVA: 0x000C2327 File Offset: 0x000C0527
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A64 RID: 23140
			// (set) Token: 0x0600834B RID: 33611 RVA: 0x000C233A File Offset: 0x000C053A
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005A65 RID: 23141
			// (set) Token: 0x0600834C RID: 33612 RVA: 0x000C2352 File Offset: 0x000C0552
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005A66 RID: 23142
			// (set) Token: 0x0600834D RID: 33613 RVA: 0x000C2365 File Offset: 0x000C0565
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A67 RID: 23143
			// (set) Token: 0x0600834E RID: 33614 RVA: 0x000C237D File Offset: 0x000C057D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A68 RID: 23144
			// (set) Token: 0x0600834F RID: 33615 RVA: 0x000C2395 File Offset: 0x000C0595
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A69 RID: 23145
			// (set) Token: 0x06008350 RID: 33616 RVA: 0x000C23AD File Offset: 0x000C05AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A50 RID: 2640
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005A6A RID: 23146
			// (set) Token: 0x06008352 RID: 33618 RVA: 0x000C23CD File Offset: 0x000C05CD
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005A6B RID: 23147
			// (set) Token: 0x06008353 RID: 33619 RVA: 0x000C23E0 File Offset: 0x000C05E0
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005A6C RID: 23148
			// (set) Token: 0x06008354 RID: 33620 RVA: 0x000C23F8 File Offset: 0x000C05F8
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005A6D RID: 23149
			// (set) Token: 0x06008355 RID: 33621 RVA: 0x000C2410 File Offset: 0x000C0610
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A6E RID: 23150
			// (set) Token: 0x06008356 RID: 33622 RVA: 0x000C2423 File Offset: 0x000C0623
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005A6F RID: 23151
			// (set) Token: 0x06008357 RID: 33623 RVA: 0x000C243B File Offset: 0x000C063B
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005A70 RID: 23152
			// (set) Token: 0x06008358 RID: 33624 RVA: 0x000C244E File Offset: 0x000C064E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A71 RID: 23153
			// (set) Token: 0x06008359 RID: 33625 RVA: 0x000C2466 File Offset: 0x000C0666
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A72 RID: 23154
			// (set) Token: 0x0600835A RID: 33626 RVA: 0x000C247E File Offset: 0x000C067E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A73 RID: 23155
			// (set) Token: 0x0600835B RID: 33627 RVA: 0x000C2496 File Offset: 0x000C0696
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
