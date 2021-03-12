using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DDC RID: 3548
	public class UpdateSyncStatisticsCommand : SyntheticCommandWithPipelineInput<GALSyncOrganization, GALSyncOrganization>
	{
		// Token: 0x0600D35F RID: 54111 RVA: 0x0012CACC File Offset: 0x0012ACCC
		private UpdateSyncStatisticsCommand() : base("Update-SyncStatistics")
		{
		}

		// Token: 0x0600D360 RID: 54112 RVA: 0x0012CAD9 File Offset: 0x0012ACD9
		public UpdateSyncStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D361 RID: 54113 RVA: 0x0012CAE8 File Offset: 0x0012ACE8
		public virtual UpdateSyncStatisticsCommand SetParameters(UpdateSyncStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DDD RID: 3549
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A360 RID: 41824
			// (set) Token: 0x0600D362 RID: 54114 RVA: 0x0012CAF2 File Offset: 0x0012ACF2
			public virtual int NumberOfMailboxesCreated
			{
				set
				{
					base.PowerSharpParameters["NumberOfMailboxesCreated"] = value;
				}
			}

			// Token: 0x1700A361 RID: 41825
			// (set) Token: 0x0600D363 RID: 54115 RVA: 0x0012CB0A File Offset: 0x0012AD0A
			public virtual int NumberOfMailboxesToCreate
			{
				set
				{
					base.PowerSharpParameters["NumberOfMailboxesToCreate"] = value;
				}
			}

			// Token: 0x1700A362 RID: 41826
			// (set) Token: 0x0600D364 RID: 54116 RVA: 0x0012CB22 File Offset: 0x0012AD22
			public virtual int MailboxCreationElapsedMilliseconds
			{
				set
				{
					base.PowerSharpParameters["MailboxCreationElapsedMilliseconds"] = value;
				}
			}

			// Token: 0x1700A363 RID: 41827
			// (set) Token: 0x0600D365 RID: 54117 RVA: 0x0012CB3A File Offset: 0x0012AD3A
			public virtual int NumberOfExportSyncRuns
			{
				set
				{
					base.PowerSharpParameters["NumberOfExportSyncRuns"] = value;
				}
			}

			// Token: 0x1700A364 RID: 41828
			// (set) Token: 0x0600D366 RID: 54118 RVA: 0x0012CB52 File Offset: 0x0012AD52
			public virtual int NumberOfImportSyncRuns
			{
				set
				{
					base.PowerSharpParameters["NumberOfImportSyncRuns"] = value;
				}
			}

			// Token: 0x1700A365 RID: 41829
			// (set) Token: 0x0600D367 RID: 54119 RVA: 0x0012CB6A File Offset: 0x0012AD6A
			public virtual int NumberOfSucessfulExportSyncRuns
			{
				set
				{
					base.PowerSharpParameters["NumberOfSucessfulExportSyncRuns"] = value;
				}
			}

			// Token: 0x1700A366 RID: 41830
			// (set) Token: 0x0600D368 RID: 54120 RVA: 0x0012CB82 File Offset: 0x0012AD82
			public virtual int NumberOfSucessfulImportSyncRuns
			{
				set
				{
					base.PowerSharpParameters["NumberOfSucessfulImportSyncRuns"] = value;
				}
			}

			// Token: 0x1700A367 RID: 41831
			// (set) Token: 0x0600D369 RID: 54121 RVA: 0x0012CB9A File Offset: 0x0012AD9A
			public virtual int NumberOfConnectionErrors
			{
				set
				{
					base.PowerSharpParameters["NumberOfConnectionErrors"] = value;
				}
			}

			// Token: 0x1700A368 RID: 41832
			// (set) Token: 0x0600D36A RID: 54122 RVA: 0x0012CBB2 File Offset: 0x0012ADB2
			public virtual int NumberOfPermissionErrors
			{
				set
				{
					base.PowerSharpParameters["NumberOfPermissionErrors"] = value;
				}
			}

			// Token: 0x1700A369 RID: 41833
			// (set) Token: 0x0600D36B RID: 54123 RVA: 0x0012CBCA File Offset: 0x0012ADCA
			public virtual int NumberOfIlmLogicErrors
			{
				set
				{
					base.PowerSharpParameters["NumberOfIlmLogicErrors"] = value;
				}
			}

			// Token: 0x1700A36A RID: 41834
			// (set) Token: 0x0600D36C RID: 54124 RVA: 0x0012CBE2 File Offset: 0x0012ADE2
			public virtual int NumberOfIlmOtherErrors
			{
				set
				{
					base.PowerSharpParameters["NumberOfIlmOtherErrors"] = value;
				}
			}

			// Token: 0x1700A36B RID: 41835
			// (set) Token: 0x0600D36D RID: 54125 RVA: 0x0012CBFA File Offset: 0x0012ADFA
			public virtual int NumberOfLiveIdErrors
			{
				set
				{
					base.PowerSharpParameters["NumberOfLiveIdErrors"] = value;
				}
			}

			// Token: 0x1700A36C RID: 41836
			// (set) Token: 0x0600D36E RID: 54126 RVA: 0x0012CC12 File Offset: 0x0012AE12
			public virtual string ClientData
			{
				set
				{
					base.PowerSharpParameters["ClientData"] = value;
				}
			}

			// Token: 0x1700A36D RID: 41837
			// (set) Token: 0x0600D36F RID: 54127 RVA: 0x0012CC25 File Offset: 0x0012AE25
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A36E RID: 41838
			// (set) Token: 0x0600D370 RID: 54128 RVA: 0x0012CC38 File Offset: 0x0012AE38
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A36F RID: 41839
			// (set) Token: 0x0600D371 RID: 54129 RVA: 0x0012CC50 File Offset: 0x0012AE50
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A370 RID: 41840
			// (set) Token: 0x0600D372 RID: 54130 RVA: 0x0012CC68 File Offset: 0x0012AE68
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A371 RID: 41841
			// (set) Token: 0x0600D373 RID: 54131 RVA: 0x0012CC80 File Offset: 0x0012AE80
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A372 RID: 41842
			// (set) Token: 0x0600D374 RID: 54132 RVA: 0x0012CC98 File Offset: 0x0012AE98
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
