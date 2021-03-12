using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009D0 RID: 2512
	public class GetMoveRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MoveRequestStatistics, MoveRequestStatistics>
	{
		// Token: 0x06007DD5 RID: 32213 RVA: 0x000BB166 File Offset: 0x000B9366
		private GetMoveRequestStatisticsCommand() : base("Get-MoveRequestStatistics")
		{
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x000BB173 File Offset: 0x000B9373
		public GetMoveRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x000BB182 File Offset: 0x000B9382
		public virtual GetMoveRequestStatisticsCommand SetParameters(GetMoveRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x000BB18C File Offset: 0x000B938C
		public virtual GetMoveRequestStatisticsCommand SetParameters(GetMoveRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007DD9 RID: 32217 RVA: 0x000BB196 File Offset: 0x000B9396
		public virtual GetMoveRequestStatisticsCommand SetParameters(GetMoveRequestStatisticsCommand.MigrationMoveRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009D1 RID: 2513
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170055EE RID: 21998
			// (set) Token: 0x06007DDA RID: 32218 RVA: 0x000BB1A0 File Offset: 0x000B93A0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170055EF RID: 21999
			// (set) Token: 0x06007DDB RID: 32219 RVA: 0x000BB1BE File Offset: 0x000B93BE
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170055F0 RID: 22000
			// (set) Token: 0x06007DDC RID: 32220 RVA: 0x000BB1D6 File Offset: 0x000B93D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055F1 RID: 22001
			// (set) Token: 0x06007DDD RID: 32221 RVA: 0x000BB1E9 File Offset: 0x000B93E9
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170055F2 RID: 22002
			// (set) Token: 0x06007DDE RID: 32222 RVA: 0x000BB201 File Offset: 0x000B9401
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170055F3 RID: 22003
			// (set) Token: 0x06007DDF RID: 32223 RVA: 0x000BB214 File Offset: 0x000B9414
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055F4 RID: 22004
			// (set) Token: 0x06007DE0 RID: 32224 RVA: 0x000BB22C File Offset: 0x000B942C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055F5 RID: 22005
			// (set) Token: 0x06007DE1 RID: 32225 RVA: 0x000BB244 File Offset: 0x000B9444
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055F6 RID: 22006
			// (set) Token: 0x06007DE2 RID: 32226 RVA: 0x000BB25C File Offset: 0x000B945C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009D2 RID: 2514
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170055F7 RID: 22007
			// (set) Token: 0x06007DE4 RID: 32228 RVA: 0x000BB27C File Offset: 0x000B947C
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170055F8 RID: 22008
			// (set) Token: 0x06007DE5 RID: 32229 RVA: 0x000BB294 File Offset: 0x000B9494
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170055F9 RID: 22009
			// (set) Token: 0x06007DE6 RID: 32230 RVA: 0x000BB2A7 File Offset: 0x000B94A7
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170055FA RID: 22010
			// (set) Token: 0x06007DE7 RID: 32231 RVA: 0x000BB2BF File Offset: 0x000B94BF
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170055FB RID: 22011
			// (set) Token: 0x06007DE8 RID: 32232 RVA: 0x000BB2D2 File Offset: 0x000B94D2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170055FC RID: 22012
			// (set) Token: 0x06007DE9 RID: 32233 RVA: 0x000BB2EA File Offset: 0x000B94EA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170055FD RID: 22013
			// (set) Token: 0x06007DEA RID: 32234 RVA: 0x000BB302 File Offset: 0x000B9502
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170055FE RID: 22014
			// (set) Token: 0x06007DEB RID: 32235 RVA: 0x000BB31A File Offset: 0x000B951A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009D3 RID: 2515
		public class MigrationMoveRequestQueueParameters : ParametersBase
		{
			// Token: 0x170055FF RID: 22015
			// (set) Token: 0x06007DED RID: 32237 RVA: 0x000BB33A File Offset: 0x000B953A
			public virtual DatabaseIdParameter MoveRequestQueue
			{
				set
				{
					base.PowerSharpParameters["MoveRequestQueue"] = value;
				}
			}

			// Token: 0x17005600 RID: 22016
			// (set) Token: 0x06007DEE RID: 32238 RVA: 0x000BB34D File Offset: 0x000B954D
			public virtual Guid MailboxGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxGuid"] = value;
				}
			}

			// Token: 0x17005601 RID: 22017
			// (set) Token: 0x06007DEF RID: 32239 RVA: 0x000BB365 File Offset: 0x000B9565
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005602 RID: 22018
			// (set) Token: 0x06007DF0 RID: 32240 RVA: 0x000BB37D File Offset: 0x000B957D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005603 RID: 22019
			// (set) Token: 0x06007DF1 RID: 32241 RVA: 0x000BB390 File Offset: 0x000B9590
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005604 RID: 22020
			// (set) Token: 0x06007DF2 RID: 32242 RVA: 0x000BB3A8 File Offset: 0x000B95A8
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005605 RID: 22021
			// (set) Token: 0x06007DF3 RID: 32243 RVA: 0x000BB3BB File Offset: 0x000B95BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005606 RID: 22022
			// (set) Token: 0x06007DF4 RID: 32244 RVA: 0x000BB3D3 File Offset: 0x000B95D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005607 RID: 22023
			// (set) Token: 0x06007DF5 RID: 32245 RVA: 0x000BB3EB File Offset: 0x000B95EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005608 RID: 22024
			// (set) Token: 0x06007DF6 RID: 32246 RVA: 0x000BB403 File Offset: 0x000B9603
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
