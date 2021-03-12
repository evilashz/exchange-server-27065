using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000145 RID: 325
	public class SetSyncServiceInstanceCommand : SyntheticCommandWithPipelineInputNoOutput<SyncServiceInstance>
	{
		// Token: 0x06002091 RID: 8337 RVA: 0x00041DD6 File Offset: 0x0003FFD6
		private SetSyncServiceInstanceCommand() : base("Set-SyncServiceInstance")
		{
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00041DE3 File Offset: 0x0003FFE3
		public SetSyncServiceInstanceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00041DF2 File Offset: 0x0003FFF2
		public virtual SetSyncServiceInstanceCommand SetParameters(SetSyncServiceInstanceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00041DFC File Offset: 0x0003FFFC
		public virtual SetSyncServiceInstanceCommand SetParameters(SetSyncServiceInstanceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000146 RID: 326
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170009C0 RID: 2496
			// (set) Token: 0x06002095 RID: 8341 RVA: 0x00041E06 File Offset: 0x00040006
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170009C1 RID: 2497
			// (set) Token: 0x06002096 RID: 8342 RVA: 0x00041E19 File Offset: 0x00040019
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170009C2 RID: 2498
			// (set) Token: 0x06002097 RID: 8343 RVA: 0x00041E31 File Offset: 0x00040031
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170009C3 RID: 2499
			// (set) Token: 0x06002098 RID: 8344 RVA: 0x00041E44 File Offset: 0x00040044
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x170009C4 RID: 2500
			// (set) Token: 0x06002099 RID: 8345 RVA: 0x00041E57 File Offset: 0x00040057
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x170009C5 RID: 2501
			// (set) Token: 0x0600209A RID: 8346 RVA: 0x00041E6A File Offset: 0x0004006A
			public virtual int ActiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["ActiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170009C6 RID: 2502
			// (set) Token: 0x0600209B RID: 8347 RVA: 0x00041E82 File Offset: 0x00040082
			public virtual int PassiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["PassiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170009C7 RID: 2503
			// (set) Token: 0x0600209C RID: 8348 RVA: 0x00041E9A File Offset: 0x0004009A
			public virtual bool IsEnabled
			{
				set
				{
					base.PowerSharpParameters["IsEnabled"] = value;
				}
			}

			// Token: 0x170009C8 RID: 2504
			// (set) Token: 0x0600209D RID: 8349 RVA: 0x00041EB2 File Offset: 0x000400B2
			public virtual bool UseCentralConfig
			{
				set
				{
					base.PowerSharpParameters["UseCentralConfig"] = value;
				}
			}

			// Token: 0x170009C9 RID: 2505
			// (set) Token: 0x0600209E RID: 8350 RVA: 0x00041ECA File Offset: 0x000400CA
			public virtual bool IsHalted
			{
				set
				{
					base.PowerSharpParameters["IsHalted"] = value;
				}
			}

			// Token: 0x170009CA RID: 2506
			// (set) Token: 0x0600209F RID: 8351 RVA: 0x00041EE2 File Offset: 0x000400E2
			public virtual bool IsHaltRecoveryDisabled
			{
				set
				{
					base.PowerSharpParameters["IsHaltRecoveryDisabled"] = value;
				}
			}

			// Token: 0x170009CB RID: 2507
			// (set) Token: 0x060020A0 RID: 8352 RVA: 0x00041EFA File Offset: 0x000400FA
			public virtual bool IsMultiObjectCookieEnabled
			{
				set
				{
					base.PowerSharpParameters["IsMultiObjectCookieEnabled"] = value;
				}
			}

			// Token: 0x170009CC RID: 2508
			// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00041F12 File Offset: 0x00040112
			public virtual bool IsNewCookieBlocked
			{
				set
				{
					base.PowerSharpParameters["IsNewCookieBlocked"] = value;
				}
			}

			// Token: 0x170009CD RID: 2509
			// (set) Token: 0x060020A2 RID: 8354 RVA: 0x00041F2A File Offset: 0x0004012A
			public virtual bool IsUsedForTenantToServiceInstanceAssociation
			{
				set
				{
					base.PowerSharpParameters["IsUsedForTenantToServiceInstanceAssociation"] = value;
				}
			}

			// Token: 0x170009CE RID: 2510
			// (set) Token: 0x060020A3 RID: 8355 RVA: 0x00041F42 File Offset: 0x00040142
			public virtual Version NewTenantMinVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMinVersion"] = value;
				}
			}

			// Token: 0x170009CF RID: 2511
			// (set) Token: 0x060020A4 RID: 8356 RVA: 0x00041F55 File Offset: 0x00040155
			public virtual Version NewTenantMaxVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMaxVersion"] = value;
				}
			}

			// Token: 0x170009D0 RID: 2512
			// (set) Token: 0x060020A5 RID: 8357 RVA: 0x00041F68 File Offset: 0x00040168
			public virtual Version TargetServerMinVersion
			{
				set
				{
					base.PowerSharpParameters["TargetServerMinVersion"] = value;
				}
			}

			// Token: 0x170009D1 RID: 2513
			// (set) Token: 0x060020A6 RID: 8358 RVA: 0x00041F7B File Offset: 0x0004017B
			public virtual Version TargetServerMaxVersion
			{
				set
				{
					base.PowerSharpParameters["TargetServerMaxVersion"] = value;
				}
			}

			// Token: 0x170009D2 RID: 2514
			// (set) Token: 0x060020A7 RID: 8359 RVA: 0x00041F8E File Offset: 0x0004018E
			public virtual string ForwardSyncConfigurationXML
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncConfigurationXML"] = value;
				}
			}

			// Token: 0x170009D3 RID: 2515
			// (set) Token: 0x060020A8 RID: 8360 RVA: 0x00041FA1 File Offset: 0x000401A1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170009D4 RID: 2516
			// (set) Token: 0x060020A9 RID: 8361 RVA: 0x00041FB4 File Offset: 0x000401B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009D5 RID: 2517
			// (set) Token: 0x060020AA RID: 8362 RVA: 0x00041FCC File Offset: 0x000401CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009D6 RID: 2518
			// (set) Token: 0x060020AB RID: 8363 RVA: 0x00041FE4 File Offset: 0x000401E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009D7 RID: 2519
			// (set) Token: 0x060020AC RID: 8364 RVA: 0x00041FFC File Offset: 0x000401FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009D8 RID: 2520
			// (set) Token: 0x060020AD RID: 8365 RVA: 0x00042014 File Offset: 0x00040214
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170009D9 RID: 2521
			// (set) Token: 0x060020AE RID: 8366 RVA: 0x0004202C File Offset: 0x0004022C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000147 RID: 327
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170009DA RID: 2522
			// (set) Token: 0x060020B0 RID: 8368 RVA: 0x0004204C File Offset: 0x0004024C
			public virtual ServiceInstanceIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170009DB RID: 2523
			// (set) Token: 0x060020B1 RID: 8369 RVA: 0x0004205F File Offset: 0x0004025F
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170009DC RID: 2524
			// (set) Token: 0x060020B2 RID: 8370 RVA: 0x00042072 File Offset: 0x00040272
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170009DD RID: 2525
			// (set) Token: 0x060020B3 RID: 8371 RVA: 0x0004208A File Offset: 0x0004028A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170009DE RID: 2526
			// (set) Token: 0x060020B4 RID: 8372 RVA: 0x0004209D File Offset: 0x0004029D
			public virtual Version MinVersion
			{
				set
				{
					base.PowerSharpParameters["MinVersion"] = value;
				}
			}

			// Token: 0x170009DF RID: 2527
			// (set) Token: 0x060020B5 RID: 8373 RVA: 0x000420B0 File Offset: 0x000402B0
			public virtual Version MaxVersion
			{
				set
				{
					base.PowerSharpParameters["MaxVersion"] = value;
				}
			}

			// Token: 0x170009E0 RID: 2528
			// (set) Token: 0x060020B6 RID: 8374 RVA: 0x000420C3 File Offset: 0x000402C3
			public virtual int ActiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["ActiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170009E1 RID: 2529
			// (set) Token: 0x060020B7 RID: 8375 RVA: 0x000420DB File Offset: 0x000402DB
			public virtual int PassiveInstanceSleepInterval
			{
				set
				{
					base.PowerSharpParameters["PassiveInstanceSleepInterval"] = value;
				}
			}

			// Token: 0x170009E2 RID: 2530
			// (set) Token: 0x060020B8 RID: 8376 RVA: 0x000420F3 File Offset: 0x000402F3
			public virtual bool IsEnabled
			{
				set
				{
					base.PowerSharpParameters["IsEnabled"] = value;
				}
			}

			// Token: 0x170009E3 RID: 2531
			// (set) Token: 0x060020B9 RID: 8377 RVA: 0x0004210B File Offset: 0x0004030B
			public virtual bool UseCentralConfig
			{
				set
				{
					base.PowerSharpParameters["UseCentralConfig"] = value;
				}
			}

			// Token: 0x170009E4 RID: 2532
			// (set) Token: 0x060020BA RID: 8378 RVA: 0x00042123 File Offset: 0x00040323
			public virtual bool IsHalted
			{
				set
				{
					base.PowerSharpParameters["IsHalted"] = value;
				}
			}

			// Token: 0x170009E5 RID: 2533
			// (set) Token: 0x060020BB RID: 8379 RVA: 0x0004213B File Offset: 0x0004033B
			public virtual bool IsHaltRecoveryDisabled
			{
				set
				{
					base.PowerSharpParameters["IsHaltRecoveryDisabled"] = value;
				}
			}

			// Token: 0x170009E6 RID: 2534
			// (set) Token: 0x060020BC RID: 8380 RVA: 0x00042153 File Offset: 0x00040353
			public virtual bool IsMultiObjectCookieEnabled
			{
				set
				{
					base.PowerSharpParameters["IsMultiObjectCookieEnabled"] = value;
				}
			}

			// Token: 0x170009E7 RID: 2535
			// (set) Token: 0x060020BD RID: 8381 RVA: 0x0004216B File Offset: 0x0004036B
			public virtual bool IsNewCookieBlocked
			{
				set
				{
					base.PowerSharpParameters["IsNewCookieBlocked"] = value;
				}
			}

			// Token: 0x170009E8 RID: 2536
			// (set) Token: 0x060020BE RID: 8382 RVA: 0x00042183 File Offset: 0x00040383
			public virtual bool IsUsedForTenantToServiceInstanceAssociation
			{
				set
				{
					base.PowerSharpParameters["IsUsedForTenantToServiceInstanceAssociation"] = value;
				}
			}

			// Token: 0x170009E9 RID: 2537
			// (set) Token: 0x060020BF RID: 8383 RVA: 0x0004219B File Offset: 0x0004039B
			public virtual Version NewTenantMinVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMinVersion"] = value;
				}
			}

			// Token: 0x170009EA RID: 2538
			// (set) Token: 0x060020C0 RID: 8384 RVA: 0x000421AE File Offset: 0x000403AE
			public virtual Version NewTenantMaxVersion
			{
				set
				{
					base.PowerSharpParameters["NewTenantMaxVersion"] = value;
				}
			}

			// Token: 0x170009EB RID: 2539
			// (set) Token: 0x060020C1 RID: 8385 RVA: 0x000421C1 File Offset: 0x000403C1
			public virtual Version TargetServerMinVersion
			{
				set
				{
					base.PowerSharpParameters["TargetServerMinVersion"] = value;
				}
			}

			// Token: 0x170009EC RID: 2540
			// (set) Token: 0x060020C2 RID: 8386 RVA: 0x000421D4 File Offset: 0x000403D4
			public virtual Version TargetServerMaxVersion
			{
				set
				{
					base.PowerSharpParameters["TargetServerMaxVersion"] = value;
				}
			}

			// Token: 0x170009ED RID: 2541
			// (set) Token: 0x060020C3 RID: 8387 RVA: 0x000421E7 File Offset: 0x000403E7
			public virtual string ForwardSyncConfigurationXML
			{
				set
				{
					base.PowerSharpParameters["ForwardSyncConfigurationXML"] = value;
				}
			}

			// Token: 0x170009EE RID: 2542
			// (set) Token: 0x060020C4 RID: 8388 RVA: 0x000421FA File Offset: 0x000403FA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170009EF RID: 2543
			// (set) Token: 0x060020C5 RID: 8389 RVA: 0x0004220D File Offset: 0x0004040D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009F0 RID: 2544
			// (set) Token: 0x060020C6 RID: 8390 RVA: 0x00042225 File Offset: 0x00040425
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009F1 RID: 2545
			// (set) Token: 0x060020C7 RID: 8391 RVA: 0x0004223D File Offset: 0x0004043D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009F2 RID: 2546
			// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00042255 File Offset: 0x00040455
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009F3 RID: 2547
			// (set) Token: 0x060020C9 RID: 8393 RVA: 0x0004226D File Offset: 0x0004046D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170009F4 RID: 2548
			// (set) Token: 0x060020CA RID: 8394 RVA: 0x00042285 File Offset: 0x00040485
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
