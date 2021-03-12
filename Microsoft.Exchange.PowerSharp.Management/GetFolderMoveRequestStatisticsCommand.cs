using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009B9 RID: 2489
	public class GetFolderMoveRequestStatisticsCommand : SyntheticCommandWithPipelineInput<FolderMoveRequestStatistics, FolderMoveRequestStatistics>
	{
		// Token: 0x06007CF7 RID: 31991 RVA: 0x000B9F66 File Offset: 0x000B8166
		private GetFolderMoveRequestStatisticsCommand() : base("Get-FolderMoveRequestStatistics")
		{
		}

		// Token: 0x06007CF8 RID: 31992 RVA: 0x000B9F73 File Offset: 0x000B8173
		public GetFolderMoveRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007CF9 RID: 31993 RVA: 0x000B9F82 File Offset: 0x000B8182
		public virtual GetFolderMoveRequestStatisticsCommand SetParameters(GetFolderMoveRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007CFA RID: 31994 RVA: 0x000B9F8C File Offset: 0x000B818C
		public virtual GetFolderMoveRequestStatisticsCommand SetParameters(GetFolderMoveRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007CFB RID: 31995 RVA: 0x000B9F96 File Offset: 0x000B8196
		public virtual GetFolderMoveRequestStatisticsCommand SetParameters(GetFolderMoveRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009BA RID: 2490
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700553E RID: 21822
			// (set) Token: 0x06007CFC RID: 31996 RVA: 0x000B9FA0 File Offset: 0x000B81A0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700553F RID: 21823
			// (set) Token: 0x06007CFD RID: 31997 RVA: 0x000B9FBE File Offset: 0x000B81BE
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005540 RID: 21824
			// (set) Token: 0x06007CFE RID: 31998 RVA: 0x000B9FD6 File Offset: 0x000B81D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005541 RID: 21825
			// (set) Token: 0x06007CFF RID: 31999 RVA: 0x000B9FE9 File Offset: 0x000B81E9
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005542 RID: 21826
			// (set) Token: 0x06007D00 RID: 32000 RVA: 0x000BA001 File Offset: 0x000B8201
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005543 RID: 21827
			// (set) Token: 0x06007D01 RID: 32001 RVA: 0x000BA014 File Offset: 0x000B8214
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005544 RID: 21828
			// (set) Token: 0x06007D02 RID: 32002 RVA: 0x000BA02C File Offset: 0x000B822C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005545 RID: 21829
			// (set) Token: 0x06007D03 RID: 32003 RVA: 0x000BA044 File Offset: 0x000B8244
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005546 RID: 21830
			// (set) Token: 0x06007D04 RID: 32004 RVA: 0x000BA05C File Offset: 0x000B825C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009BB RID: 2491
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005547 RID: 21831
			// (set) Token: 0x06007D06 RID: 32006 RVA: 0x000BA07C File Offset: 0x000B827C
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005548 RID: 21832
			// (set) Token: 0x06007D07 RID: 32007 RVA: 0x000BA094 File Offset: 0x000B8294
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005549 RID: 21833
			// (set) Token: 0x06007D08 RID: 32008 RVA: 0x000BA0A7 File Offset: 0x000B82A7
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x1700554A RID: 21834
			// (set) Token: 0x06007D09 RID: 32009 RVA: 0x000BA0BF File Offset: 0x000B82BF
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x1700554B RID: 21835
			// (set) Token: 0x06007D0A RID: 32010 RVA: 0x000BA0D2 File Offset: 0x000B82D2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700554C RID: 21836
			// (set) Token: 0x06007D0B RID: 32011 RVA: 0x000BA0EA File Offset: 0x000B82EA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700554D RID: 21837
			// (set) Token: 0x06007D0C RID: 32012 RVA: 0x000BA102 File Offset: 0x000B8302
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700554E RID: 21838
			// (set) Token: 0x06007D0D RID: 32013 RVA: 0x000BA11A File Offset: 0x000B831A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009BC RID: 2492
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x1700554F RID: 21839
			// (set) Token: 0x06007D0F RID: 32015 RVA: 0x000BA13A File Offset: 0x000B833A
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005550 RID: 21840
			// (set) Token: 0x06007D10 RID: 32016 RVA: 0x000BA14D File Offset: 0x000B834D
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005551 RID: 21841
			// (set) Token: 0x06007D11 RID: 32017 RVA: 0x000BA165 File Offset: 0x000B8365
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005552 RID: 21842
			// (set) Token: 0x06007D12 RID: 32018 RVA: 0x000BA17D File Offset: 0x000B837D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005553 RID: 21843
			// (set) Token: 0x06007D13 RID: 32019 RVA: 0x000BA190 File Offset: 0x000B8390
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005554 RID: 21844
			// (set) Token: 0x06007D14 RID: 32020 RVA: 0x000BA1A8 File Offset: 0x000B83A8
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005555 RID: 21845
			// (set) Token: 0x06007D15 RID: 32021 RVA: 0x000BA1BB File Offset: 0x000B83BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005556 RID: 21846
			// (set) Token: 0x06007D16 RID: 32022 RVA: 0x000BA1D3 File Offset: 0x000B83D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005557 RID: 21847
			// (set) Token: 0x06007D17 RID: 32023 RVA: 0x000BA1EB File Offset: 0x000B83EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005558 RID: 21848
			// (set) Token: 0x06007D18 RID: 32024 RVA: 0x000BA203 File Offset: 0x000B8403
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
