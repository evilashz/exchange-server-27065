using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000148 RID: 328
	public class StartMsoFullSyncCommand : SyntheticCommandWithPipelineInput<ExchangeConfigurationUnit, ExchangeConfigurationUnit>
	{
		// Token: 0x060020CC RID: 8396 RVA: 0x000422A5 File Offset: 0x000404A5
		private StartMsoFullSyncCommand() : base("Start-MsoFullSync")
		{
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000422B2 File Offset: 0x000404B2
		public StartMsoFullSyncCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000422C1 File Offset: 0x000404C1
		public virtual StartMsoFullSyncCommand SetParameters(StartMsoFullSyncCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000422CB File Offset: 0x000404CB
		public virtual StartMsoFullSyncCommand SetParameters(StartMsoFullSyncCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000149 RID: 329
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170009F5 RID: 2549
			// (set) Token: 0x060020D0 RID: 8400 RVA: 0x000422D5 File Offset: 0x000404D5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170009F6 RID: 2550
			// (set) Token: 0x060020D1 RID: 8401 RVA: 0x000422F3 File Offset: 0x000404F3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170009F7 RID: 2551
			// (set) Token: 0x060020D2 RID: 8402 RVA: 0x00042306 File Offset: 0x00040506
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x170009F8 RID: 2552
			// (set) Token: 0x060020D3 RID: 8403 RVA: 0x0004231E File Offset: 0x0004051E
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x170009F9 RID: 2553
			// (set) Token: 0x060020D4 RID: 8404 RVA: 0x00042336 File Offset: 0x00040536
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x170009FA RID: 2554
			// (set) Token: 0x060020D5 RID: 8405 RVA: 0x0004234E File Offset: 0x0004054E
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x170009FB RID: 2555
			// (set) Token: 0x060020D6 RID: 8406 RVA: 0x00042361 File Offset: 0x00040561
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x170009FC RID: 2556
			// (set) Token: 0x060020D7 RID: 8407 RVA: 0x00042374 File Offset: 0x00040574
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x170009FD RID: 2557
			// (set) Token: 0x060020D8 RID: 8408 RVA: 0x0004238C File Offset: 0x0004058C
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x170009FE RID: 2558
			// (set) Token: 0x060020D9 RID: 8409 RVA: 0x000423A4 File Offset: 0x000405A4
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x170009FF RID: 2559
			// (set) Token: 0x060020DA RID: 8410 RVA: 0x000423BC File Offset: 0x000405BC
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17000A00 RID: 2560
			// (set) Token: 0x060020DB RID: 8411 RVA: 0x000423D4 File Offset: 0x000405D4
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17000A01 RID: 2561
			// (set) Token: 0x060020DC RID: 8412 RVA: 0x000423EC File Offset: 0x000405EC
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17000A02 RID: 2562
			// (set) Token: 0x060020DD RID: 8413 RVA: 0x00042404 File Offset: 0x00040604
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17000A03 RID: 2563
			// (set) Token: 0x060020DE RID: 8414 RVA: 0x0004241C File Offset: 0x0004061C
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17000A04 RID: 2564
			// (set) Token: 0x060020DF RID: 8415 RVA: 0x00042434 File Offset: 0x00040634
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17000A05 RID: 2565
			// (set) Token: 0x060020E0 RID: 8416 RVA: 0x00042447 File Offset: 0x00040647
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17000A06 RID: 2566
			// (set) Token: 0x060020E1 RID: 8417 RVA: 0x0004245A File Offset: 0x0004065A
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17000A07 RID: 2567
			// (set) Token: 0x060020E2 RID: 8418 RVA: 0x0004246D File Offset: 0x0004066D
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17000A08 RID: 2568
			// (set) Token: 0x060020E3 RID: 8419 RVA: 0x00042480 File Offset: 0x00040680
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17000A09 RID: 2569
			// (set) Token: 0x060020E4 RID: 8420 RVA: 0x00042493 File Offset: 0x00040693
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A0A RID: 2570
			// (set) Token: 0x060020E5 RID: 8421 RVA: 0x000424AB File Offset: 0x000406AB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A0B RID: 2571
			// (set) Token: 0x060020E6 RID: 8422 RVA: 0x000424C3 File Offset: 0x000406C3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A0C RID: 2572
			// (set) Token: 0x060020E7 RID: 8423 RVA: 0x000424DB File Offset: 0x000406DB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000A0D RID: 2573
			// (set) Token: 0x060020E8 RID: 8424 RVA: 0x000424F3 File Offset: 0x000406F3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200014A RID: 330
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000A0E RID: 2574
			// (set) Token: 0x060020EA RID: 8426 RVA: 0x00042513 File Offset: 0x00040713
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000A0F RID: 2575
			// (set) Token: 0x060020EB RID: 8427 RVA: 0x00042526 File Offset: 0x00040726
			public virtual bool IsHotmailMigration
			{
				set
				{
					base.PowerSharpParameters["IsHotmailMigration"] = value;
				}
			}

			// Token: 0x17000A10 RID: 2576
			// (set) Token: 0x060020EC RID: 8428 RVA: 0x0004253E File Offset: 0x0004073E
			public virtual bool SyncMBXAndDLToMServ
			{
				set
				{
					base.PowerSharpParameters["SyncMBXAndDLToMServ"] = value;
				}
			}

			// Token: 0x17000A11 RID: 2577
			// (set) Token: 0x060020ED RID: 8429 RVA: 0x00042556 File Offset: 0x00040756
			public virtual OrganizationStatus OrganizationStatus
			{
				set
				{
					base.PowerSharpParameters["OrganizationStatus"] = value;
				}
			}

			// Token: 0x17000A12 RID: 2578
			// (set) Token: 0x060020EE RID: 8430 RVA: 0x0004256E File Offset: 0x0004076E
			public virtual string IOwnMigrationTenant
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationTenant"] = value;
				}
			}

			// Token: 0x17000A13 RID: 2579
			// (set) Token: 0x060020EF RID: 8431 RVA: 0x00042581 File Offset: 0x00040781
			public virtual string IOwnMigrationStatusReport
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatusReport"] = value;
				}
			}

			// Token: 0x17000A14 RID: 2580
			// (set) Token: 0x060020F0 RID: 8432 RVA: 0x00042594 File Offset: 0x00040794
			public virtual IOwnMigrationStatusFlagsEnum IOwnMigrationStatus
			{
				set
				{
					base.PowerSharpParameters["IOwnMigrationStatus"] = value;
				}
			}

			// Token: 0x17000A15 RID: 2581
			// (set) Token: 0x060020F1 RID: 8433 RVA: 0x000425AC File Offset: 0x000407AC
			public virtual bool MSOSyncEnabled
			{
				set
				{
					base.PowerSharpParameters["MSOSyncEnabled"] = value;
				}
			}

			// Token: 0x17000A16 RID: 2582
			// (set) Token: 0x060020F2 RID: 8434 RVA: 0x000425C4 File Offset: 0x000407C4
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderIssueWarningQuota"] = value;
				}
			}

			// Token: 0x17000A17 RID: 2583
			// (set) Token: 0x060020F3 RID: 8435 RVA: 0x000425DC File Offset: 0x000407DC
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderMaxItemSize"] = value;
				}
			}

			// Token: 0x17000A18 RID: 2584
			// (set) Token: 0x060020F4 RID: 8436 RVA: 0x000425F4 File Offset: 0x000407F4
			public virtual Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
			{
				set
				{
					base.PowerSharpParameters["DefaultPublicFolderProhibitPostQuota"] = value;
				}
			}

			// Token: 0x17000A19 RID: 2585
			// (set) Token: 0x060020F5 RID: 8437 RVA: 0x0004260C File Offset: 0x0004080C
			public virtual bool IsDirSyncRunning
			{
				set
				{
					base.PowerSharpParameters["IsDirSyncRunning"] = value;
				}
			}

			// Token: 0x17000A1A RID: 2586
			// (set) Token: 0x060020F6 RID: 8438 RVA: 0x00042624 File Offset: 0x00040824
			public virtual UpgradeStatusTypes UpgradeStatus
			{
				set
				{
					base.PowerSharpParameters["UpgradeStatus"] = value;
				}
			}

			// Token: 0x17000A1B RID: 2587
			// (set) Token: 0x060020F7 RID: 8439 RVA: 0x0004263C File Offset: 0x0004083C
			public virtual UpgradeRequestTypes UpgradeRequest
			{
				set
				{
					base.PowerSharpParameters["UpgradeRequest"] = value;
				}
			}

			// Token: 0x17000A1C RID: 2588
			// (set) Token: 0x060020F8 RID: 8440 RVA: 0x00042654 File Offset: 0x00040854
			public virtual MultiValuedProperty<string> CompanyTags
			{
				set
				{
					base.PowerSharpParameters["CompanyTags"] = value;
				}
			}

			// Token: 0x17000A1D RID: 2589
			// (set) Token: 0x060020F9 RID: 8441 RVA: 0x00042667 File Offset: 0x00040867
			public virtual string Location
			{
				set
				{
					base.PowerSharpParameters["Location"] = value;
				}
			}

			// Token: 0x17000A1E RID: 2590
			// (set) Token: 0x060020FA RID: 8442 RVA: 0x0004267A File Offset: 0x0004087A
			public virtual MultiValuedProperty<string> DirSyncStatus
			{
				set
				{
					base.PowerSharpParameters["DirSyncStatus"] = value;
				}
			}

			// Token: 0x17000A1F RID: 2591
			// (set) Token: 0x060020FB RID: 8443 RVA: 0x0004268D File Offset: 0x0004088D
			public virtual MultiValuedProperty<string> AsynchronousOperationIds
			{
				set
				{
					base.PowerSharpParameters["AsynchronousOperationIds"] = value;
				}
			}

			// Token: 0x17000A20 RID: 2592
			// (set) Token: 0x060020FC RID: 8444 RVA: 0x000426A0 File Offset: 0x000408A0
			public virtual string DirSyncServiceInstance
			{
				set
				{
					base.PowerSharpParameters["DirSyncServiceInstance"] = value;
				}
			}

			// Token: 0x17000A21 RID: 2593
			// (set) Token: 0x060020FD RID: 8445 RVA: 0x000426B3 File Offset: 0x000408B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000A22 RID: 2594
			// (set) Token: 0x060020FE RID: 8446 RVA: 0x000426CB File Offset: 0x000408CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000A23 RID: 2595
			// (set) Token: 0x060020FF RID: 8447 RVA: 0x000426E3 File Offset: 0x000408E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000A24 RID: 2596
			// (set) Token: 0x06002100 RID: 8448 RVA: 0x000426FB File Offset: 0x000408FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000A25 RID: 2597
			// (set) Token: 0x06002101 RID: 8449 RVA: 0x00042713 File Offset: 0x00040913
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
