using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A1E RID: 2590
	public class GetMailboxImportRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxImportRequestStatistics, MailboxImportRequestStatistics>
	{
		// Token: 0x06008175 RID: 33141 RVA: 0x000BFD8C File Offset: 0x000BDF8C
		private GetMailboxImportRequestStatisticsCommand() : base("Get-MailboxImportRequestStatistics")
		{
		}

		// Token: 0x06008176 RID: 33142 RVA: 0x000BFD99 File Offset: 0x000BDF99
		public GetMailboxImportRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008177 RID: 33143 RVA: 0x000BFDA8 File Offset: 0x000BDFA8
		public virtual GetMailboxImportRequestStatisticsCommand SetParameters(GetMailboxImportRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008178 RID: 33144 RVA: 0x000BFDB2 File Offset: 0x000BDFB2
		public virtual GetMailboxImportRequestStatisticsCommand SetParameters(GetMailboxImportRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008179 RID: 33145 RVA: 0x000BFDBC File Offset: 0x000BDFBC
		public virtual GetMailboxImportRequestStatisticsCommand SetParameters(GetMailboxImportRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A1F RID: 2591
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170058F2 RID: 22770
			// (set) Token: 0x0600817A RID: 33146 RVA: 0x000BFDC6 File Offset: 0x000BDFC6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170058F3 RID: 22771
			// (set) Token: 0x0600817B RID: 33147 RVA: 0x000BFDE4 File Offset: 0x000BDFE4
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170058F4 RID: 22772
			// (set) Token: 0x0600817C RID: 33148 RVA: 0x000BFDFC File Offset: 0x000BDFFC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058F5 RID: 22773
			// (set) Token: 0x0600817D RID: 33149 RVA: 0x000BFE0F File Offset: 0x000BE00F
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170058F6 RID: 22774
			// (set) Token: 0x0600817E RID: 33150 RVA: 0x000BFE27 File Offset: 0x000BE027
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170058F7 RID: 22775
			// (set) Token: 0x0600817F RID: 33151 RVA: 0x000BFE3A File Offset: 0x000BE03A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170058F8 RID: 22776
			// (set) Token: 0x06008180 RID: 33152 RVA: 0x000BFE52 File Offset: 0x000BE052
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170058F9 RID: 22777
			// (set) Token: 0x06008181 RID: 33153 RVA: 0x000BFE6A File Offset: 0x000BE06A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170058FA RID: 22778
			// (set) Token: 0x06008182 RID: 33154 RVA: 0x000BFE82 File Offset: 0x000BE082
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A20 RID: 2592
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170058FB RID: 22779
			// (set) Token: 0x06008184 RID: 33156 RVA: 0x000BFEA2 File Offset: 0x000BE0A2
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170058FC RID: 22780
			// (set) Token: 0x06008185 RID: 33157 RVA: 0x000BFEBA File Offset: 0x000BE0BA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170058FD RID: 22781
			// (set) Token: 0x06008186 RID: 33158 RVA: 0x000BFECD File Offset: 0x000BE0CD
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170058FE RID: 22782
			// (set) Token: 0x06008187 RID: 33159 RVA: 0x000BFEE5 File Offset: 0x000BE0E5
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170058FF RID: 22783
			// (set) Token: 0x06008188 RID: 33160 RVA: 0x000BFEF8 File Offset: 0x000BE0F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005900 RID: 22784
			// (set) Token: 0x06008189 RID: 33161 RVA: 0x000BFF10 File Offset: 0x000BE110
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005901 RID: 22785
			// (set) Token: 0x0600818A RID: 33162 RVA: 0x000BFF28 File Offset: 0x000BE128
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005902 RID: 22786
			// (set) Token: 0x0600818B RID: 33163 RVA: 0x000BFF40 File Offset: 0x000BE140
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A21 RID: 2593
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005903 RID: 22787
			// (set) Token: 0x0600818D RID: 33165 RVA: 0x000BFF60 File Offset: 0x000BE160
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005904 RID: 22788
			// (set) Token: 0x0600818E RID: 33166 RVA: 0x000BFF73 File Offset: 0x000BE173
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005905 RID: 22789
			// (set) Token: 0x0600818F RID: 33167 RVA: 0x000BFF8B File Offset: 0x000BE18B
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005906 RID: 22790
			// (set) Token: 0x06008190 RID: 33168 RVA: 0x000BFFA3 File Offset: 0x000BE1A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005907 RID: 22791
			// (set) Token: 0x06008191 RID: 33169 RVA: 0x000BFFB6 File Offset: 0x000BE1B6
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005908 RID: 22792
			// (set) Token: 0x06008192 RID: 33170 RVA: 0x000BFFCE File Offset: 0x000BE1CE
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005909 RID: 22793
			// (set) Token: 0x06008193 RID: 33171 RVA: 0x000BFFE1 File Offset: 0x000BE1E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700590A RID: 22794
			// (set) Token: 0x06008194 RID: 33172 RVA: 0x000BFFF9 File Offset: 0x000BE1F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700590B RID: 22795
			// (set) Token: 0x06008195 RID: 33173 RVA: 0x000C0011 File Offset: 0x000BE211
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700590C RID: 22796
			// (set) Token: 0x06008196 RID: 33174 RVA: 0x000C0029 File Offset: 0x000BE229
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
