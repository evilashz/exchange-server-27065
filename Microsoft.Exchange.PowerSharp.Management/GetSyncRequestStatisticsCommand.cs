using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AB5 RID: 2741
	public class GetSyncRequestStatisticsCommand : SyntheticCommandWithPipelineInput<SyncRequestStatistics, SyncRequestStatistics>
	{
		// Token: 0x0600876D RID: 34669 RVA: 0x000C78C5 File Offset: 0x000C5AC5
		private GetSyncRequestStatisticsCommand() : base("Get-SyncRequestStatistics")
		{
		}

		// Token: 0x0600876E RID: 34670 RVA: 0x000C78D2 File Offset: 0x000C5AD2
		public GetSyncRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600876F RID: 34671 RVA: 0x000C78E1 File Offset: 0x000C5AE1
		public virtual GetSyncRequestStatisticsCommand SetParameters(GetSyncRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008770 RID: 34672 RVA: 0x000C78EB File Offset: 0x000C5AEB
		public virtual GetSyncRequestStatisticsCommand SetParameters(GetSyncRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x000C78F5 File Offset: 0x000C5AF5
		public virtual GetSyncRequestStatisticsCommand SetParameters(GetSyncRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AB6 RID: 2742
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005DBC RID: 23996
			// (set) Token: 0x06008772 RID: 34674 RVA: 0x000C78FF File Offset: 0x000C5AFF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005DBD RID: 23997
			// (set) Token: 0x06008773 RID: 34675 RVA: 0x000C791D File Offset: 0x000C5B1D
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005DBE RID: 23998
			// (set) Token: 0x06008774 RID: 34676 RVA: 0x000C7935 File Offset: 0x000C5B35
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DBF RID: 23999
			// (set) Token: 0x06008775 RID: 34677 RVA: 0x000C7948 File Offset: 0x000C5B48
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005DC0 RID: 24000
			// (set) Token: 0x06008776 RID: 34678 RVA: 0x000C7960 File Offset: 0x000C5B60
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005DC1 RID: 24001
			// (set) Token: 0x06008777 RID: 34679 RVA: 0x000C7973 File Offset: 0x000C5B73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DC2 RID: 24002
			// (set) Token: 0x06008778 RID: 34680 RVA: 0x000C798B File Offset: 0x000C5B8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DC3 RID: 24003
			// (set) Token: 0x06008779 RID: 34681 RVA: 0x000C79A3 File Offset: 0x000C5BA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DC4 RID: 24004
			// (set) Token: 0x0600877A RID: 34682 RVA: 0x000C79BB File Offset: 0x000C5BBB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AB7 RID: 2743
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005DC5 RID: 24005
			// (set) Token: 0x0600877C RID: 34684 RVA: 0x000C79DB File Offset: 0x000C5BDB
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005DC6 RID: 24006
			// (set) Token: 0x0600877D RID: 34685 RVA: 0x000C79F3 File Offset: 0x000C5BF3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DC7 RID: 24007
			// (set) Token: 0x0600877E RID: 34686 RVA: 0x000C7A06 File Offset: 0x000C5C06
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005DC8 RID: 24008
			// (set) Token: 0x0600877F RID: 34687 RVA: 0x000C7A1E File Offset: 0x000C5C1E
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005DC9 RID: 24009
			// (set) Token: 0x06008780 RID: 34688 RVA: 0x000C7A31 File Offset: 0x000C5C31
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DCA RID: 24010
			// (set) Token: 0x06008781 RID: 34689 RVA: 0x000C7A49 File Offset: 0x000C5C49
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DCB RID: 24011
			// (set) Token: 0x06008782 RID: 34690 RVA: 0x000C7A61 File Offset: 0x000C5C61
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DCC RID: 24012
			// (set) Token: 0x06008783 RID: 34691 RVA: 0x000C7A79 File Offset: 0x000C5C79
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AB8 RID: 2744
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005DCD RID: 24013
			// (set) Token: 0x06008785 RID: 34693 RVA: 0x000C7A99 File Offset: 0x000C5C99
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005DCE RID: 24014
			// (set) Token: 0x06008786 RID: 34694 RVA: 0x000C7AAC File Offset: 0x000C5CAC
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005DCF RID: 24015
			// (set) Token: 0x06008787 RID: 34695 RVA: 0x000C7AC4 File Offset: 0x000C5CC4
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005DD0 RID: 24016
			// (set) Token: 0x06008788 RID: 34696 RVA: 0x000C7ADC File Offset: 0x000C5CDC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DD1 RID: 24017
			// (set) Token: 0x06008789 RID: 34697 RVA: 0x000C7AEF File Offset: 0x000C5CEF
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005DD2 RID: 24018
			// (set) Token: 0x0600878A RID: 34698 RVA: 0x000C7B07 File Offset: 0x000C5D07
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005DD3 RID: 24019
			// (set) Token: 0x0600878B RID: 34699 RVA: 0x000C7B1A File Offset: 0x000C5D1A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DD4 RID: 24020
			// (set) Token: 0x0600878C RID: 34700 RVA: 0x000C7B32 File Offset: 0x000C5D32
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DD5 RID: 24021
			// (set) Token: 0x0600878D RID: 34701 RVA: 0x000C7B4A File Offset: 0x000C5D4A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DD6 RID: 24022
			// (set) Token: 0x0600878E RID: 34702 RVA: 0x000C7B62 File Offset: 0x000C5D62
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
