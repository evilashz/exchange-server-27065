using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A05 RID: 2565
	public class GetMailboxExportRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxExportRequestStatistics, MailboxExportRequestStatistics>
	{
		// Token: 0x06008082 RID: 32898 RVA: 0x000BEA14 File Offset: 0x000BCC14
		private GetMailboxExportRequestStatisticsCommand() : base("Get-MailboxExportRequestStatistics")
		{
		}

		// Token: 0x06008083 RID: 32899 RVA: 0x000BEA21 File Offset: 0x000BCC21
		public GetMailboxExportRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008084 RID: 32900 RVA: 0x000BEA30 File Offset: 0x000BCC30
		public virtual GetMailboxExportRequestStatisticsCommand SetParameters(GetMailboxExportRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008085 RID: 32901 RVA: 0x000BEA3A File Offset: 0x000BCC3A
		public virtual GetMailboxExportRequestStatisticsCommand SetParameters(GetMailboxExportRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008086 RID: 32902 RVA: 0x000BEA44 File Offset: 0x000BCC44
		public virtual GetMailboxExportRequestStatisticsCommand SetParameters(GetMailboxExportRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A06 RID: 2566
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005831 RID: 22577
			// (set) Token: 0x06008087 RID: 32903 RVA: 0x000BEA4E File Offset: 0x000BCC4E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxExportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005832 RID: 22578
			// (set) Token: 0x06008088 RID: 32904 RVA: 0x000BEA6C File Offset: 0x000BCC6C
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005833 RID: 22579
			// (set) Token: 0x06008089 RID: 32905 RVA: 0x000BEA84 File Offset: 0x000BCC84
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005834 RID: 22580
			// (set) Token: 0x0600808A RID: 32906 RVA: 0x000BEA97 File Offset: 0x000BCC97
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005835 RID: 22581
			// (set) Token: 0x0600808B RID: 32907 RVA: 0x000BEAAF File Offset: 0x000BCCAF
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005836 RID: 22582
			// (set) Token: 0x0600808C RID: 32908 RVA: 0x000BEAC2 File Offset: 0x000BCCC2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005837 RID: 22583
			// (set) Token: 0x0600808D RID: 32909 RVA: 0x000BEADA File Offset: 0x000BCCDA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005838 RID: 22584
			// (set) Token: 0x0600808E RID: 32910 RVA: 0x000BEAF2 File Offset: 0x000BCCF2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005839 RID: 22585
			// (set) Token: 0x0600808F RID: 32911 RVA: 0x000BEB0A File Offset: 0x000BCD0A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A07 RID: 2567
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700583A RID: 22586
			// (set) Token: 0x06008091 RID: 32913 RVA: 0x000BEB2A File Offset: 0x000BCD2A
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700583B RID: 22587
			// (set) Token: 0x06008092 RID: 32914 RVA: 0x000BEB42 File Offset: 0x000BCD42
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700583C RID: 22588
			// (set) Token: 0x06008093 RID: 32915 RVA: 0x000BEB55 File Offset: 0x000BCD55
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x1700583D RID: 22589
			// (set) Token: 0x06008094 RID: 32916 RVA: 0x000BEB6D File Offset: 0x000BCD6D
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x1700583E RID: 22590
			// (set) Token: 0x06008095 RID: 32917 RVA: 0x000BEB80 File Offset: 0x000BCD80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700583F RID: 22591
			// (set) Token: 0x06008096 RID: 32918 RVA: 0x000BEB98 File Offset: 0x000BCD98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005840 RID: 22592
			// (set) Token: 0x06008097 RID: 32919 RVA: 0x000BEBB0 File Offset: 0x000BCDB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005841 RID: 22593
			// (set) Token: 0x06008098 RID: 32920 RVA: 0x000BEBC8 File Offset: 0x000BCDC8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A08 RID: 2568
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005842 RID: 22594
			// (set) Token: 0x0600809A RID: 32922 RVA: 0x000BEBE8 File Offset: 0x000BCDE8
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005843 RID: 22595
			// (set) Token: 0x0600809B RID: 32923 RVA: 0x000BEBFB File Offset: 0x000BCDFB
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005844 RID: 22596
			// (set) Token: 0x0600809C RID: 32924 RVA: 0x000BEC13 File Offset: 0x000BCE13
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005845 RID: 22597
			// (set) Token: 0x0600809D RID: 32925 RVA: 0x000BEC2B File Offset: 0x000BCE2B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005846 RID: 22598
			// (set) Token: 0x0600809E RID: 32926 RVA: 0x000BEC3E File Offset: 0x000BCE3E
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005847 RID: 22599
			// (set) Token: 0x0600809F RID: 32927 RVA: 0x000BEC56 File Offset: 0x000BCE56
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005848 RID: 22600
			// (set) Token: 0x060080A0 RID: 32928 RVA: 0x000BEC69 File Offset: 0x000BCE69
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005849 RID: 22601
			// (set) Token: 0x060080A1 RID: 32929 RVA: 0x000BEC81 File Offset: 0x000BCE81
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700584A RID: 22602
			// (set) Token: 0x060080A2 RID: 32930 RVA: 0x000BEC99 File Offset: 0x000BCE99
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700584B RID: 22603
			// (set) Token: 0x060080A3 RID: 32931 RVA: 0x000BECB1 File Offset: 0x000BCEB1
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
