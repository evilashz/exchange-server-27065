using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A33 RID: 2611
	public class GetMailboxRelocationRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MailboxRelocationRequestStatistics, MailboxRelocationRequestStatistics>
	{
		// Token: 0x06008241 RID: 33345 RVA: 0x000C0DE4 File Offset: 0x000BEFE4
		private GetMailboxRelocationRequestStatisticsCommand() : base("Get-MailboxRelocationRequestStatistics")
		{
		}

		// Token: 0x06008242 RID: 33346 RVA: 0x000C0DF1 File Offset: 0x000BEFF1
		public GetMailboxRelocationRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008243 RID: 33347 RVA: 0x000C0E00 File Offset: 0x000BF000
		public virtual GetMailboxRelocationRequestStatisticsCommand SetParameters(GetMailboxRelocationRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008244 RID: 33348 RVA: 0x000C0E0A File Offset: 0x000BF00A
		public virtual GetMailboxRelocationRequestStatisticsCommand SetParameters(GetMailboxRelocationRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008245 RID: 33349 RVA: 0x000C0E14 File Offset: 0x000BF014
		public virtual GetMailboxRelocationRequestStatisticsCommand SetParameters(GetMailboxRelocationRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A34 RID: 2612
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005994 RID: 22932
			// (set) Token: 0x06008246 RID: 33350 RVA: 0x000C0E1E File Offset: 0x000BF01E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005995 RID: 22933
			// (set) Token: 0x06008247 RID: 33351 RVA: 0x000C0E3C File Offset: 0x000BF03C
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005996 RID: 22934
			// (set) Token: 0x06008248 RID: 33352 RVA: 0x000C0E54 File Offset: 0x000BF054
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005997 RID: 22935
			// (set) Token: 0x06008249 RID: 33353 RVA: 0x000C0E67 File Offset: 0x000BF067
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005998 RID: 22936
			// (set) Token: 0x0600824A RID: 33354 RVA: 0x000C0E7F File Offset: 0x000BF07F
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005999 RID: 22937
			// (set) Token: 0x0600824B RID: 33355 RVA: 0x000C0E92 File Offset: 0x000BF092
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700599A RID: 22938
			// (set) Token: 0x0600824C RID: 33356 RVA: 0x000C0EAA File Offset: 0x000BF0AA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700599B RID: 22939
			// (set) Token: 0x0600824D RID: 33357 RVA: 0x000C0EC2 File Offset: 0x000BF0C2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700599C RID: 22940
			// (set) Token: 0x0600824E RID: 33358 RVA: 0x000C0EDA File Offset: 0x000BF0DA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A35 RID: 2613
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700599D RID: 22941
			// (set) Token: 0x06008250 RID: 33360 RVA: 0x000C0EFA File Offset: 0x000BF0FA
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700599E RID: 22942
			// (set) Token: 0x06008251 RID: 33361 RVA: 0x000C0F12 File Offset: 0x000BF112
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700599F RID: 22943
			// (set) Token: 0x06008252 RID: 33362 RVA: 0x000C0F25 File Offset: 0x000BF125
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170059A0 RID: 22944
			// (set) Token: 0x06008253 RID: 33363 RVA: 0x000C0F3D File Offset: 0x000BF13D
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170059A1 RID: 22945
			// (set) Token: 0x06008254 RID: 33364 RVA: 0x000C0F50 File Offset: 0x000BF150
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059A2 RID: 22946
			// (set) Token: 0x06008255 RID: 33365 RVA: 0x000C0F68 File Offset: 0x000BF168
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059A3 RID: 22947
			// (set) Token: 0x06008256 RID: 33366 RVA: 0x000C0F80 File Offset: 0x000BF180
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059A4 RID: 22948
			// (set) Token: 0x06008257 RID: 33367 RVA: 0x000C0F98 File Offset: 0x000BF198
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000A36 RID: 2614
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x170059A5 RID: 22949
			// (set) Token: 0x06008259 RID: 33369 RVA: 0x000C0FB8 File Offset: 0x000BF1B8
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x170059A6 RID: 22950
			// (set) Token: 0x0600825A RID: 33370 RVA: 0x000C0FCB File Offset: 0x000BF1CB
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x170059A7 RID: 22951
			// (set) Token: 0x0600825B RID: 33371 RVA: 0x000C0FE3 File Offset: 0x000BF1E3
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170059A8 RID: 22952
			// (set) Token: 0x0600825C RID: 33372 RVA: 0x000C0FFB File Offset: 0x000BF1FB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170059A9 RID: 22953
			// (set) Token: 0x0600825D RID: 33373 RVA: 0x000C100E File Offset: 0x000BF20E
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170059AA RID: 22954
			// (set) Token: 0x0600825E RID: 33374 RVA: 0x000C1026 File Offset: 0x000BF226
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170059AB RID: 22955
			// (set) Token: 0x0600825F RID: 33375 RVA: 0x000C1039 File Offset: 0x000BF239
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170059AC RID: 22956
			// (set) Token: 0x06008260 RID: 33376 RVA: 0x000C1051 File Offset: 0x000BF251
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170059AD RID: 22957
			// (set) Token: 0x06008261 RID: 33377 RVA: 0x000C1069 File Offset: 0x000BF269
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170059AE RID: 22958
			// (set) Token: 0x06008262 RID: 33378 RVA: 0x000C1081 File Offset: 0x000BF281
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
