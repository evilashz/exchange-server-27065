using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000388 RID: 904
	public class NewTenantRelocationRequestCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x060038AE RID: 14510 RVA: 0x0006160B File Offset: 0x0005F80B
		private NewTenantRelocationRequestCommand() : base("New-TenantRelocationRequest")
		{
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x00061618 File Offset: 0x0005F818
		public NewTenantRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x00061627 File Offset: 0x0005F827
		public virtual NewTenantRelocationRequestCommand SetParameters(NewTenantRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00061631 File Offset: 0x0005F831
		public virtual NewTenantRelocationRequestCommand SetParameters(NewTenantRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000389 RID: 905
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001D57 RID: 7511
			// (set) Token: 0x060038B2 RID: 14514 RVA: 0x0006163B File Offset: 0x0005F83B
			public virtual AccountPartitionIdParameter TargetAccountPartition
			{
				set
				{
					base.PowerSharpParameters["TargetAccountPartition"] = value;
				}
			}

			// Token: 0x17001D58 RID: 7512
			// (set) Token: 0x060038B3 RID: 14515 RVA: 0x0006164E File Offset: 0x0005F84E
			public virtual Schedule SafeLockdownSchedule
			{
				set
				{
					base.PowerSharpParameters["SafeLockdownSchedule"] = value;
				}
			}

			// Token: 0x17001D59 RID: 7513
			// (set) Token: 0x060038B4 RID: 14516 RVA: 0x00061661 File Offset: 0x0005F861
			public virtual bool AutoCompletionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoCompletionEnabled"] = value;
				}
			}

			// Token: 0x17001D5A RID: 7514
			// (set) Token: 0x060038B5 RID: 14517 RVA: 0x00061679 File Offset: 0x0005F879
			public virtual bool LargeTenantModeEnabled
			{
				set
				{
					base.PowerSharpParameters["LargeTenantModeEnabled"] = value;
				}
			}

			// Token: 0x17001D5B RID: 7515
			// (set) Token: 0x060038B6 RID: 14518 RVA: 0x00061691 File Offset: 0x0005F891
			public virtual RelocationStateRequestedByCmdlet RelocationStateRequested
			{
				set
				{
					base.PowerSharpParameters["RelocationStateRequested"] = value;
				}
			}

			// Token: 0x17001D5C RID: 7516
			// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000616A9 File Offset: 0x0005F8A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D5D RID: 7517
			// (set) Token: 0x060038B8 RID: 14520 RVA: 0x000616BC File Offset: 0x0005F8BC
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17001D5E RID: 7518
			// (set) Token: 0x060038B9 RID: 14521 RVA: 0x000616D4 File Offset: 0x0005F8D4
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17001D5F RID: 7519
			// (set) Token: 0x060038BA RID: 14522 RVA: 0x000616EC File Offset: 0x0005F8EC
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17001D60 RID: 7520
			// (set) Token: 0x060038BB RID: 14523 RVA: 0x00061704 File Offset: 0x0005F904
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17001D61 RID: 7521
			// (set) Token: 0x060038BC RID: 14524 RVA: 0x00061717 File Offset: 0x0005F917
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17001D62 RID: 7522
			// (set) Token: 0x060038BD RID: 14525 RVA: 0x0006172A File Offset: 0x0005F92A
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17001D63 RID: 7523
			// (set) Token: 0x060038BE RID: 14526 RVA: 0x00061742 File Offset: 0x0005F942
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17001D64 RID: 7524
			// (set) Token: 0x060038BF RID: 14527 RVA: 0x0006175A File Offset: 0x0005F95A
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17001D65 RID: 7525
			// (set) Token: 0x060038C0 RID: 14528 RVA: 0x00061772 File Offset: 0x0005F972
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17001D66 RID: 7526
			// (set) Token: 0x060038C1 RID: 14529 RVA: 0x0006178A File Offset: 0x0005F98A
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17001D67 RID: 7527
			// (set) Token: 0x060038C2 RID: 14530 RVA: 0x000617A2 File Offset: 0x0005F9A2
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17001D68 RID: 7528
			// (set) Token: 0x060038C3 RID: 14531 RVA: 0x000617BA File Offset: 0x0005F9BA
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17001D69 RID: 7529
			// (set) Token: 0x060038C4 RID: 14532 RVA: 0x000617D2 File Offset: 0x0005F9D2
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17001D6A RID: 7530
			// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000617EA File Offset: 0x0005F9EA
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17001D6B RID: 7531
			// (set) Token: 0x060038C6 RID: 14534 RVA: 0x000617FD File Offset: 0x0005F9FD
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17001D6C RID: 7532
			// (set) Token: 0x060038C7 RID: 14535 RVA: 0x00061810 File Offset: 0x0005FA10
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17001D6D RID: 7533
			// (set) Token: 0x060038C8 RID: 14536 RVA: 0x00061823 File Offset: 0x0005FA23
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17001D6E RID: 7534
			// (set) Token: 0x060038C9 RID: 14537 RVA: 0x00061836 File Offset: 0x0005FA36
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17001D6F RID: 7535
			// (set) Token: 0x060038CA RID: 14538 RVA: 0x00061849 File Offset: 0x0005FA49
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D70 RID: 7536
			// (set) Token: 0x060038CB RID: 14539 RVA: 0x00061861 File Offset: 0x0005FA61
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D71 RID: 7537
			// (set) Token: 0x060038CC RID: 14540 RVA: 0x00061879 File Offset: 0x0005FA79
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D72 RID: 7538
			// (set) Token: 0x060038CD RID: 14541 RVA: 0x00061891 File Offset: 0x0005FA91
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D73 RID: 7539
			// (set) Token: 0x060038CE RID: 14542 RVA: 0x000618A9 File Offset: 0x0005FAA9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200038A RID: 906
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001D74 RID: 7540
			// (set) Token: 0x060038D0 RID: 14544 RVA: 0x000618C9 File Offset: 0x0005FAC9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001D75 RID: 7541
			// (set) Token: 0x060038D1 RID: 14545 RVA: 0x000618E7 File Offset: 0x0005FAE7
			public virtual AccountPartitionIdParameter TargetAccountPartition
			{
				set
				{
					base.PowerSharpParameters["TargetAccountPartition"] = value;
				}
			}

			// Token: 0x17001D76 RID: 7542
			// (set) Token: 0x060038D2 RID: 14546 RVA: 0x000618FA File Offset: 0x0005FAFA
			public virtual Schedule SafeLockdownSchedule
			{
				set
				{
					base.PowerSharpParameters["SafeLockdownSchedule"] = value;
				}
			}

			// Token: 0x17001D77 RID: 7543
			// (set) Token: 0x060038D3 RID: 14547 RVA: 0x0006190D File Offset: 0x0005FB0D
			public virtual bool AutoCompletionEnabled
			{
				set
				{
					base.PowerSharpParameters["AutoCompletionEnabled"] = value;
				}
			}

			// Token: 0x17001D78 RID: 7544
			// (set) Token: 0x060038D4 RID: 14548 RVA: 0x00061925 File Offset: 0x0005FB25
			public virtual bool LargeTenantModeEnabled
			{
				set
				{
					base.PowerSharpParameters["LargeTenantModeEnabled"] = value;
				}
			}

			// Token: 0x17001D79 RID: 7545
			// (set) Token: 0x060038D5 RID: 14549 RVA: 0x0006193D File Offset: 0x0005FB3D
			public virtual RelocationStateRequestedByCmdlet RelocationStateRequested
			{
				set
				{
					base.PowerSharpParameters["RelocationStateRequested"] = value;
				}
			}

			// Token: 0x17001D7A RID: 7546
			// (set) Token: 0x060038D6 RID: 14550 RVA: 0x00061955 File Offset: 0x0005FB55
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001D7B RID: 7547
			// (set) Token: 0x060038D7 RID: 14551 RVA: 0x00061968 File Offset: 0x0005FB68
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17001D7C RID: 7548
			// (set) Token: 0x060038D8 RID: 14552 RVA: 0x00061980 File Offset: 0x0005FB80
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17001D7D RID: 7549
			// (set) Token: 0x060038D9 RID: 14553 RVA: 0x00061998 File Offset: 0x0005FB98
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17001D7E RID: 7550
			// (set) Token: 0x060038DA RID: 14554 RVA: 0x000619B0 File Offset: 0x0005FBB0
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17001D7F RID: 7551
			// (set) Token: 0x060038DB RID: 14555 RVA: 0x000619C3 File Offset: 0x0005FBC3
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17001D80 RID: 7552
			// (set) Token: 0x060038DC RID: 14556 RVA: 0x000619D6 File Offset: 0x0005FBD6
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17001D81 RID: 7553
			// (set) Token: 0x060038DD RID: 14557 RVA: 0x000619EE File Offset: 0x0005FBEE
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17001D82 RID: 7554
			// (set) Token: 0x060038DE RID: 14558 RVA: 0x00061A06 File Offset: 0x0005FC06
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17001D83 RID: 7555
			// (set) Token: 0x060038DF RID: 14559 RVA: 0x00061A1E File Offset: 0x0005FC1E
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17001D84 RID: 7556
			// (set) Token: 0x060038E0 RID: 14560 RVA: 0x00061A36 File Offset: 0x0005FC36
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17001D85 RID: 7557
			// (set) Token: 0x060038E1 RID: 14561 RVA: 0x00061A4E File Offset: 0x0005FC4E
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17001D86 RID: 7558
			// (set) Token: 0x060038E2 RID: 14562 RVA: 0x00061A66 File Offset: 0x0005FC66
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17001D87 RID: 7559
			// (set) Token: 0x060038E3 RID: 14563 RVA: 0x00061A7E File Offset: 0x0005FC7E
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17001D88 RID: 7560
			// (set) Token: 0x060038E4 RID: 14564 RVA: 0x00061A96 File Offset: 0x0005FC96
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17001D89 RID: 7561
			// (set) Token: 0x060038E5 RID: 14565 RVA: 0x00061AA9 File Offset: 0x0005FCA9
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17001D8A RID: 7562
			// (set) Token: 0x060038E6 RID: 14566 RVA: 0x00061ABC File Offset: 0x0005FCBC
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17001D8B RID: 7563
			// (set) Token: 0x060038E7 RID: 14567 RVA: 0x00061ACF File Offset: 0x0005FCCF
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17001D8C RID: 7564
			// (set) Token: 0x060038E8 RID: 14568 RVA: 0x00061AE2 File Offset: 0x0005FCE2
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17001D8D RID: 7565
			// (set) Token: 0x060038E9 RID: 14569 RVA: 0x00061AF5 File Offset: 0x0005FCF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001D8E RID: 7566
			// (set) Token: 0x060038EA RID: 14570 RVA: 0x00061B0D File Offset: 0x0005FD0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001D8F RID: 7567
			// (set) Token: 0x060038EB RID: 14571 RVA: 0x00061B25 File Offset: 0x0005FD25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001D90 RID: 7568
			// (set) Token: 0x060038EC RID: 14572 RVA: 0x00061B3D File Offset: 0x0005FD3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001D91 RID: 7569
			// (set) Token: 0x060038ED RID: 14573 RVA: 0x00061B55 File Offset: 0x0005FD55
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
