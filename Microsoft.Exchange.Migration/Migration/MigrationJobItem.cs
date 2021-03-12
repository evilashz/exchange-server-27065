using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200005D RID: 93
	internal sealed class MigrationJobItem : MigrationMessagePersistableBase
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00016312 File Offset: 0x00014512
		private MigrationJobItem(MigrationType migrationType)
		{
			this.currentSupportedVersion = 3L;
			this.MigrationType = migrationType;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x00016329 File Offset: 0x00014529
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x00016331 File Offset: 0x00014531
		public string Identifier { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001633A File Offset: 0x0001453A
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x00016342 File Offset: 0x00014542
		public string LocalMailboxIdentifier { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001634B File Offset: 0x0001454B
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x00016353 File Offset: 0x00014553
		public MigrationType MigrationType { get; private set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001635C File Offset: 0x0001455C
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x00016364 File Offset: 0x00014564
		public int CursorPosition { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001636D File Offset: 0x0001456D
		public MigrationUserStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00016375 File Offset: 0x00014575
		public MigrationState State
		{
			get
			{
				return this.StatusData.State;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00016382 File Offset: 0x00014582
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001638A File Offset: 0x0001458A
		public MigrationFlags Flags { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00016393 File Offset: 0x00014593
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001639B File Offset: 0x0001459B
		public MigrationWorkflowPosition WorkflowPosition { get; private set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000163A4 File Offset: 0x000145A4
		public MigrationStep[] SupportedSteps
		{
			get
			{
				if (this.MigrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					switch (this.RecipientType)
					{
					case MigrationUserRecipientType.Mailbox:
						return new MigrationStep[]
						{
							MigrationStep.Initialization,
							MigrationStep.Provisioning,
							MigrationStep.ProvisioningUpdate,
							MigrationStep.DataMigration
						};
					case MigrationUserRecipientType.Contact:
					case MigrationUserRecipientType.Group:
					case MigrationUserRecipientType.Mailuser:
						return new MigrationStep[]
						{
							MigrationStep.Initialization,
							MigrationStep.Provisioning,
							MigrationStep.ProvisioningUpdate
						};
					}
					throw new NotSupportedException("didn't expect recipient type");
				}
				return new MigrationStep[]
				{
					MigrationStep.Initialization,
					MigrationStep.Provisioning,
					MigrationStep.ProvisioningUpdate,
					MigrationStep.DataMigration
				};
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00016437 File Offset: 0x00014637
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001643F File Offset: 0x0001463F
		public MigrationUserRecipientType RecipientType { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00016448 File Offset: 0x00014648
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00016450 File Offset: 0x00014650
		public MigrationStatusData<MigrationUserStatus> StatusData
		{
			get
			{
				return this.statusData;
			}
			private set
			{
				this.statusData = value;
				if (this.statusData != null)
				{
					this.status = this.statusData.Status;
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00016472 File Offset: 0x00014672
		public ExDateTime? StateLastUpdated
		{
			get
			{
				return this.statusData.StateLastUpdated;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001647F File Offset: 0x0001467F
		public LocalizedString? LocalizedError
		{
			get
			{
				return this.statusData.LocalizedError;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0001648C File Offset: 0x0001468C
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x00016494 File Offset: 0x00014694
		public Guid MigrationJobId
		{
			get
			{
				return this.migrationJobId;
			}
			private set
			{
				this.migrationJobId = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001649D File Offset: 0x0001469D
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x000164A5 File Offset: 0x000146A5
		public IMailboxData LocalMailbox
		{
			get
			{
				return this.localMailbox;
			}
			private set
			{
				this.localMailbox = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x000164AE File Offset: 0x000146AE
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x000164B6 File Offset: 0x000146B6
		public string RemoteIdentifier { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000164BF File Offset: 0x000146BF
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x000164C7 File Offset: 0x000146C7
		public ProvisioningDataStorageBase ProvisioningData { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000164D0 File Offset: 0x000146D0
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000164D8 File Offset: 0x000146D8
		public ProvisioningSnapshot ProvisioningStatistics { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000164E1 File Offset: 0x000146E1
		public ProvisioningId ProvisioningId
		{
			get
			{
				return new ProvisioningId(this.JobItemGuid, this.MigrationJobId);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000164F4 File Offset: 0x000146F4
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x000164FC File Offset: 0x000146FC
		public ISubscriptionSettings SubscriptionSettings { get; private set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00016505 File Offset: 0x00014705
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x0001650D File Offset: 0x0001470D
		public ISubscriptionStatistics SubscriptionStatistics { get; private set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00016516 File Offset: 0x00014716
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x0001651E File Offset: 0x0001471E
		public ISubscriptionId SubscriptionId { get; private set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00016527 File Offset: 0x00014727
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x0001652F File Offset: 0x0001472F
		public ExDateTime? SubscriptionLastChecked
		{
			get
			{
				return this.subscriptionLastChecked;
			}
			private set
			{
				this.subscriptionLastChecked = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00016538 File Offset: 0x00014738
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00016540 File Offset: 0x00014740
		public ExDateTime? SubscriptionSettingsLastUpdatedTime { get; private set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00016549 File Offset: 0x00014749
		public long ItemsSynced
		{
			get
			{
				if (this.SubscriptionStatistics != null)
				{
					return this.SubscriptionStatistics.NumItemsSynced;
				}
				return 0L;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x00016561 File Offset: 0x00014761
		public long ItemsSkipped
		{
			get
			{
				if (this.SubscriptionStatistics != null)
				{
					return this.SubscriptionStatistics.NumItemsSkipped;
				}
				return 0L;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00016579 File Offset: 0x00014779
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00016581 File Offset: 0x00014781
		public MigrationJob MigrationJob { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x0001658A File Offset: 0x0001478A
		public bool SupportsAdvancedValidation
		{
			get
			{
				return this.MigrationType == MigrationType.ExchangeLocalMove || this.MigrationType == MigrationType.ExchangeRemoteMove || this.MigrationType == MigrationType.PublicFolder;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x000165AF File Offset: 0x000147AF
		public bool ShouldProvision
		{
			get
			{
				return this.MigrationType == MigrationType.ExchangeOutlookAnywhere || this.MigrationType == MigrationType.XO1;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x000165C5 File Offset: 0x000147C5
		public bool ShouldMigrate
		{
			get
			{
				return (this.MigrationType != MigrationType.ExchangeOutlookAnywhere || this.RecipientType == MigrationUserRecipientType.Mailbox) && this.MigrationType != MigrationType.XO1;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x000165E8 File Offset: 0x000147E8
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				string key = this.MigrationType.ToString() + (this.IsPAW ? "PAW" : "non-PAW");
				PropertyDefinition[] array;
				if (!MigrationJobItem.PropertyDefinitionsHash.TryGetValue(key, out array))
				{
					array = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
					{
						MigrationJobItem.MigrationJobItemColumnsIndex,
						this.MailboxDataPropertyDefinitions,
						this.ProvisioningDataPropertyDefinitions,
						this.SubscriptionIdPropertyDefinitions,
						this.SubscriptionSettingsPropertyDefinitions,
						new PropertyDefinition[]
						{
							this.CursorPositionPropertyDefinition
						},
						this.MigrationJobSlotPropertyDefinitions
					});
					MigrationJobItem.PropertyDefinitionsHash[key] = array;
				}
				return array;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00016692 File Offset: 0x00014892
		public override PropertyDefinition[] InitializationPropertyDefinitions
		{
			get
			{
				return MigrationJobItem.MigrationJobItemColumnsTypeIndex;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00016699 File Offset: 0x00014899
		public override long MaximumSupportedVersion
		{
			get
			{
				return 4L;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001669D File Offset: 0x0001489D
		public override long MinimumSupportedVersion
		{
			get
			{
				return 3L;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x000166A1 File Offset: 0x000148A1
		public override long MinimumSupportedPersistableVersion
		{
			get
			{
				return 3L;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x000166A5 File Offset: 0x000148A5
		public override long CurrentSupportedVersion
		{
			get
			{
				return this.currentSupportedVersion;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x000166AD File Offset: 0x000148AD
		public bool IsPAW
		{
			get
			{
				return base.Version >= 4L;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000166BC File Offset: 0x000148BC
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000166C4 File Offset: 0x000148C4
		public ExDateTime? SubscriptionDisableTime { get; private set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x000166CD File Offset: 0x000148CD
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x000166E0 File Offset: 0x000148E0
		public string BatchInputId
		{
			get
			{
				return base.ExtendedProperties.Get<string>("BatchInputId", null);
			}
			private set
			{
				base.ExtendedProperties.Set<string>("BatchInputId", value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000166F3 File Offset: 0x000148F3
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x000166FB File Offset: 0x000148FB
		public int LastFinalizationAttempt { get; internal set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00016704 File Offset: 0x00014904
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001672A File Offset: 0x0001492A
		public TimeSpan? InitialSyncDuration
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan?>("InitialSyncDuration", null);
			}
			private set
			{
				base.ExtendedProperties.Set<TimeSpan?>("InitialSyncDuration", value);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00016740 File Offset: 0x00014940
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x00016766 File Offset: 0x00014966
		public TimeSpan? IncrementalSyncDuration
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan?>("IncrementalSyncDuration", null);
			}
			private set
			{
				base.ExtendedProperties.Set<TimeSpan?>("IncrementalSyncDuration", value);
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00016779 File Offset: 0x00014979
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00016781 File Offset: 0x00014981
		public ExDateTime? ProvisionedTime { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001678A File Offset: 0x0001498A
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00016792 File Offset: 0x00014992
		public ExDateTime? SubscriptionQueuedTime { get; private set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001679B File Offset: 0x0001499B
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x000167A3 File Offset: 0x000149A3
		public TimeSpan? OverallCmdletDuration { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000167AC File Offset: 0x000149AC
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x000167B4 File Offset: 0x000149B4
		public TimeSpan? SubscriptionInjectionDuration { get; private set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000167BD File Offset: 0x000149BD
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000167C5 File Offset: 0x000149C5
		public TimeSpan? ProvisioningDuration { get; private set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000167D0 File Offset: 0x000149D0
		public ExDateTime? LastSuccessfulSyncTime
		{
			get
			{
				if (this.SubscriptionStatistics != null)
				{
					return this.SubscriptionStatistics.LastSyncTime;
				}
				return null;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x000167FA File Offset: 0x000149FA
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00016802 File Offset: 0x00014A02
		public ExDateTime? LastRestartTime { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001680B File Offset: 0x00014A0B
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00016813 File Offset: 0x00014A13
		public ExDateTime? NextProcessTime { get; private set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0001681C File Offset: 0x00014A1C
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0001682F File Offset: 0x00014A2F
		public int IncrementalSyncFailures
		{
			get
			{
				return base.ExtendedProperties.Get<int>("IncrementalSyncFailures", 0);
			}
			internal set
			{
				base.ExtendedProperties.Set<int>("IncrementalSyncFailures", value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00016842 File Offset: 0x00014A42
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x00016855 File Offset: 0x00014A55
		public int PublicFolderCompletionFailures
		{
			get
			{
				return base.ExtendedProperties.Get<int>("PublicFolderCompletionFailures", 0);
			}
			internal set
			{
				base.ExtendedProperties.Set<int>("PublicFolderCompletionFailures", value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00016868 File Offset: 0x00014A68
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00016890 File Offset: 0x00014A90
		public TimeSpan? IncrementalSyncInterval
		{
			get
			{
				return base.ExtendedProperties.Get<TimeSpan?>("IncrementalSyncInterval", null);
			}
			private set
			{
				TimeSpan? value2 = value;
				if (value2 != null)
				{
					base.ExtendedProperties.Set<TimeSpan?>("IncrementalSyncInterval", value2);
					return;
				}
				base.ExtendedProperties.Remove("IncrementalSyncInterval");
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000168CA File Offset: 0x00014ACA
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000168DD File Offset: 0x00014ADD
		public string TroubleshooterNotes
		{
			get
			{
				return base.ExtendedProperties.Get<string>("TroubleshooterNotes", null);
			}
			private set
			{
				base.ExtendedProperties.Set<string>("TroubleshooterNotes", value);
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000168F0 File Offset: 0x00014AF0
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x000168F8 File Offset: 0x00014AF8
		internal string TenantName { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00016901 File Offset: 0x00014B01
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x00016918 File Offset: 0x00014B18
		internal string JobName
		{
			get
			{
				return base.ExtendedProperties.Get<string>("JobName", string.Empty);
			}
			private set
			{
				base.ExtendedProperties.Set<string>("JobName", value);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0001692B File Offset: 0x00014B2B
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0001693E File Offset: 0x00014B3E
		internal bool IsStaged
		{
			get
			{
				return base.ExtendedProperties.Get<bool>("IsStaged", false);
			}
			private set
			{
				base.ExtendedProperties.Set<bool>("IsStaged", value);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00016951 File Offset: 0x00014B51
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x00016959 File Offset: 0x00014B59
		internal MigrationSlotType ConsumedSlotType { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00016962 File Offset: 0x00014B62
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0001696A File Offset: 0x00014B6A
		internal Guid MigrationSlotProviderGuid { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00016973 File Offset: 0x00014B73
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0001697B File Offset: 0x00014B7B
		internal Guid JobItemGuid { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00016984 File Offset: 0x00014B84
		internal MigrationObjectsCount CountSelf
		{
			get
			{
				switch (this.RecipientType)
				{
				case MigrationUserRecipientType.Mailbox:
					return new MigrationObjectsCount(new int?(1));
				case MigrationUserRecipientType.Contact:
					return new MigrationObjectsCount(null, null, new int?(1), false);
				case MigrationUserRecipientType.Group:
					return new MigrationObjectsCount(null, new int?(1), null, false);
				case MigrationUserRecipientType.PublicFolder:
					return new MigrationObjectsCount(null, null, null, true);
				case MigrationUserRecipientType.Mailuser:
				case MigrationUserRecipientType.MailboxOrMailuser:
					if (this.MigrationType == MigrationType.ExchangeRemoteMove || this.MigrationType == MigrationType.ExchangeLocalMove)
					{
						return new MigrationObjectsCount(new int?(1));
					}
					return new MigrationObjectsCount(null, null, new int?(1), false);
				}
				throw new InvalidOperationException("This method should not be invoked if the RecipientType is " + this.RecipientType);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00016A8C File Offset: 0x00014C8C
		internal bool SupportsIncrementalSync
		{
			get
			{
				return this.SubscriptionLastChecked != null && this.SubscriptionLastChecked.Value != MigrationJobItem.MaxDateTimeValue;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x00016AC3 File Offset: 0x00014CC3
		private PropertyDefinition CursorPositionPropertyDefinition
		{
			get
			{
				return MigrationJobItem.GetCursorPositionProperty(this.MigrationType);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00016AD0 File Offset: 0x00014CD0
		private PropertyDefinition[] SubscriptionSettingsPropertyDefinitions
		{
			get
			{
				return JobItemSubscriptionSettingsBase.GetPropertyDefinitions(this.MigrationType);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x00016ADD File Offset: 0x00014CDD
		private PropertyDefinition[] SubscriptionIdPropertyDefinitions
		{
			get
			{
				return SubscriptionIdHelper.GetPropertyDefinitions(this.MigrationType, this.IsPAW);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00016AF0 File Offset: 0x00014CF0
		private PropertyDefinition[] MailboxDataPropertyDefinitions
		{
			get
			{
				return MailboxDataHelper.GetPropertyDefinitions(this.MigrationType);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x00016AFD File Offset: 0x00014CFD
		private PropertyDefinition[] ProvisioningDataPropertyDefinitions
		{
			get
			{
				return ProvisioningDataStorageBase.GetPropertyDefinitions(this.MigrationType);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00016B0C File Offset: 0x00014D0C
		private PropertyDefinition[] MigrationJobSlotPropertyDefinitions
		{
			get
			{
				return new StorePropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobItemSlotType,
					MigrationBatchMessageSchema.MigrationJobItemSlotProviderId
				};
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00016B34 File Offset: 0x00014D34
		public static MigrationJobItem Create(IMigrationDataProvider dataProvider, MigrationJob job, MigrationUserStatus status, IMigrationDataRow dataRow, MailboxData mailboxData)
		{
			MigrationUtil.AssertOrThrow(job.MigrationType == dataRow.MigrationType, "Job type is {0} but data row is {1}. They should both be the same.", new object[]
			{
				job.MigrationType,
				dataRow.MigrationType
			});
			MigrationJobItem migrationJobItem = new MigrationJobItem(job.MigrationType);
			migrationJobItem.Initialize(job, dataRow, mailboxData, status, null, null, null);
			migrationJobItem.MigrationJob = job;
			migrationJobItem.CreateInStore(dataProvider, null);
			job.ReportData.Append(Strings.MigrationReportJobItemCreatedInternal(migrationJobItem.Identifier));
			return migrationJobItem;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00016BC8 File Offset: 0x00014DC8
		public static MigrationJobItem Create(IMigrationDataProvider dataProvider, MigrationJob job, IMigrationDataRow dataRow, MigrationUserStatus status, MigrationState? state = null, MigrationWorkflowPosition position = null)
		{
			MigrationUtil.AssertOrThrow(job.MigrationType == dataRow.MigrationType, "Job type is {0} but data row is {1}. They should both be the same.", new object[]
			{
				job.MigrationType,
				dataRow.MigrationType
			});
			MigrationJobItem migrationJobItem = new MigrationJobItem(job.MigrationType);
			migrationJobItem.Initialize(job, dataRow, null, status, null, state, position);
			migrationJobItem.MigrationJob = job;
			migrationJobItem.CreateInStore(dataProvider, null);
			job.ReportData.Append(Strings.MigrationReportJobItemCreatedInternal(migrationJobItem.Identifier));
			return migrationJobItem;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00016C52 File Offset: 0x00014E52
		public static MigrationJobItem CreateFailed(IMigrationDataProvider dataProvider, MigrationJob job, InvalidDataRow dataRow, MigrationState? state = null, MigrationWorkflowPosition position = null)
		{
			return MigrationJobItem.CreateFailed(dataProvider, job, dataRow, new LocalizedException(dataRow.Error.LocalizedErrorMessage), state, position);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00016C70 File Offset: 0x00014E70
		public static MigrationJobItem CreateFailed(IMigrationDataProvider dataProvider, MigrationJob job, IMigrationDataRow dataRow, LocalizedException localizedError, MigrationState? state = null, MigrationWorkflowPosition position = null)
		{
			MigrationUtil.AssertOrThrow(job.MigrationType == dataRow.MigrationType, "Job type is {0} but data row is {1}. They should both be the same.", new object[]
			{
				job.MigrationType,
				dataRow.MigrationType
			});
			MigrationJobItem migrationJobItem = new MigrationJobItem(job.MigrationType);
			migrationJobItem.Initialize(job, dataRow, null, MigrationUserStatus.Failed, localizedError, state, position);
			migrationJobItem.CreateInStore(dataProvider, null);
			job.ReportData.Append(Strings.MigrationReportJobItemWithError(migrationJobItem.Identifier, localizedError.LocalizedString), localizedError, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
			MigrationFailureLog.LogFailureEvent(migrationJobItem, localizedError, MigrationFailureFlags.Fatal, null);
			return migrationJobItem;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016D78 File Offset: 0x00014F78
		public static IEnumerable<MigrationJobItem> GetMigratableByStateLastUpdated(IMigrationDataProvider provider, MigrationJob job, ExDateTime? stateLastUpdatedCutoff, MigrationUserStatus status, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(provider);
			migrationJobObjectCache.PreSeed(job);
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationUserStatus, status);
			SortBy[] additionalSorts = new SortBy[]
			{
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated, SortOrder.Ascending)
			};
			ExDateTime timeNow = ExDateTime.UtcNow;
			IEnumerable<StoreObjectId> messageIdList = provider.FindMessageIds(primaryFilter, MigrationJobItem.MigrationJobItemSubscriptionStateLastUpdated, additionalSorts, delegate(IDictionary<PropertyDefinition, object> rowData)
			{
				object obj;
				if (rowData.TryGetValue(MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked, out obj) && obj is ExDateTime && (ExDateTime)obj > timeNow)
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationJobItem.FilterJobItemsByColumnLastUpdated(rowData, MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated, new Guid?(job.JobId), stateLastUpdatedCutoff, new MigrationUserStatus?(status));
			}, new int?(maxCount));
			return MigrationJobItem.LoadJobItemsWithStatus(provider, messageIdList, status, migrationJobObjectCache);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00016E54 File Offset: 0x00015054
		public static ExDateTime? GetOldestLastSyncSubscriptionTime(IMigrationDataProvider provider, MigrationType migrationType, Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime, SortOrder.Ascending)
			};
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				MigrationJobItem.MigrationJobItemMessageClassFilter,
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId)
			});
			if (migrationType == MigrationType.ExchangeOutlookAnywhere)
			{
				sortBy = new SortBy[]
				{
					new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
					new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
					new SortBy(MigrationBatchMessageSchema.MigrationJobItemRecipientType, SortOrder.Ascending),
					new SortBy(MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime, SortOrder.Ascending)
				};
				filter = new AndFilter(new QueryFilter[]
				{
					MigrationJobItem.MigrationJobItemMessageClassFilter,
					new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId),
					new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobItemRecipientType, MigrationUserRecipientType.Mailbox)
				});
			}
			object[] array = provider.QueryRow(filter, sortBy, new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime
			});
			if (array != null && !(array[0] is PropertyError))
			{
				return (ExDateTime?)array[0];
			}
			return null;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00016F9A File Offset: 0x0001519A
		public static IEnumerable<MigrationJobItem> GetBySubscriptionLastChecked(IMigrationDataProvider provider, MigrationJob job, ExDateTime? lastCheckedTime, MigrationUserStatus status, int maxCount)
		{
			return MigrationJobItem.GetByColumnLastUpdated(provider, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked, job, lastCheckedTime, status, maxCount);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017344 File Offset: 0x00015544
		public static IEnumerable<MigrationJobItem> GetItemsNotInStatus(IMigrationDataProvider provider, MigrationJob job, MigrationUserStatus status, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, job.JobId);
			SortBy[] sort = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationUserStatus, SortOrder.Ascending)
			};
			PropertyDefinition[] filterColumns = new StorePropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationUserStatus
			};
			IEnumerable<StoreObjectId> messageIdList = provider.FindMessageIds(primaryFilter, filterColumns, sort, delegate(IDictionary<PropertyDefinition, object> rowData)
			{
				if (!MigrationHelper.IsEqualXsoValues(job.JobId, rowData[MigrationBatchMessageSchema.MigrationJobId]))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!StringComparer.InvariantCultureIgnoreCase.Equals(rowData[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				if (status == (MigrationUserStatus)rowData[MigrationBatchMessageSchema.MigrationUserStatus])
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, new int?(maxCount));
			messageIdList = new List<StoreObjectId>(messageIdList);
			MigrationJobObjectCache jobCache = new MigrationJobObjectCache(provider);
			jobCache.PreSeed(job);
			foreach (StoreObjectId messageId in messageIdList)
			{
				MigrationJobItem jobItem = MigrationJobItem.Load(provider, messageId, jobCache, true);
				if (jobItem.Status != status)
				{
					yield return jobItem;
				}
			}
			yield break;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00017378 File Offset: 0x00015578
		public static IEnumerable<MigrationJobItem> GetAll(IMigrationDataProvider provider, MigrationJob job, int? maxSize)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(provider);
			MigrationEqualityFilter[] secondaryFilters = null;
			if (job != null)
			{
				secondaryFilters = new MigrationEqualityFilter[]
				{
					new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, job.JobId)
				};
				migrationJobObjectCache.PreSeed(job);
			}
			return MigrationJobItem.GetByFilter(provider, MigrationJobItem.MessageClassEqualityFilter, secondaryFilters, null, migrationJobObjectCache, maxSize);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000173D4 File Offset: 0x000155D4
		public static IEnumerable<MigrationJobItem> GetAllSortedByIdentifier(IMigrationDataProvider provider, MigrationJob job, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, job.JobId);
			SortBy[] additionalSorts = new SortBy[]
			{
				new SortBy(MigrationBatchMessageSchema.MigrationJobItemIdentifier, SortOrder.Ascending)
			};
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(provider);
			migrationJobObjectCache.PreSeed(job);
			return MigrationJobItem.GetByFilter(provider, primaryFilter, MigrationJobItem.MigrationJobItemMessageClassFilterCollection, additionalSorts, migrationJobObjectCache, new int?(maxCount));
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000176C8 File Offset: 0x000158C8
		public static IEnumerable<MigrationJobItem> GetNextJobItems(IMigrationDataProvider provider, MigrationJob job, string identifier, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullOrEmptyArgument(identifier, "identifier");
			maxCount++;
			MigrationEqualityFilter primarySortIndex = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemIdentifier, identifier);
			PropertyDefinition[] additionalDataColumns = new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId
			};
			IEnumerable<StoreObjectId> messageIds = provider.FindMessageIds(primarySortIndex, additionalDataColumns, null, delegate(IDictionary<PropertyDefinition, object> rowData)
			{
				if (!MigrationBatchMessageSchema.MigrationJobItemClass.Equals(rowData[StoreObjectSchema.ItemClass]))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				if (!MigrationHelper.IsEqualXsoValues(job.JobId, rowData[MigrationBatchMessageSchema.MigrationJobId]))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, new int?(maxCount));
			IEnumerator<StoreObjectId> messageIdEnumerator = messageIds.GetEnumerator();
			messageIdEnumerator.MoveNext();
			MigrationJobObjectCache jobCache = new MigrationJobObjectCache(provider);
			jobCache.PreSeed(job);
			while (messageIdEnumerator.MoveNext())
			{
				StoreObjectId messageId = messageIdEnumerator.Current;
				MigrationJobItem item = MigrationJobItem.Load(provider, messageId, jobCache, true);
				yield return item;
			}
			yield break;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000176FC File Offset: 0x000158FC
		public static IEnumerable<MigrationJobItem> GetByUserId(IMigrationDataProvider provider, MigrationUserId id)
		{
			if (id.JobItemGuid != Guid.Empty)
			{
				MigrationJobItem byGuid = MigrationJobItem.GetByGuid(provider, id.JobItemGuid);
				return Enumerable.Repeat<MigrationJobItem>(byGuid, (byGuid != null) ? 1 : 0);
			}
			return MigrationJobItem.GetByIdentifier(provider, null, id.Id, null);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00017744 File Offset: 0x00015944
		public static MigrationJobItem GetByGuid(IMigrationDataProvider provider, Guid identity)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(identity, "identity");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemId, identity);
			return MigrationJobItem.GetByFilter(provider, primaryFilter, null, null, new MigrationJobObjectCache(provider), null).FirstOrDefault<MigrationJobItem>();
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001779C File Offset: 0x0001599C
		public static IEnumerable<MigrationJobItem> GetByIdentifier(IMigrationDataProvider provider, MigrationJob job, string identifier, MigrationJobObjectCache jobCache = null)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(identifier, "identifier");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemIdentifier, identifier);
			List<MigrationEqualityFilter> list = new List<MigrationEqualityFilter>(2);
			if (jobCache == null)
			{
				jobCache = new MigrationJobObjectCache(provider);
			}
			if (job != null)
			{
				list.Add(new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, job.JobId));
				jobCache.PreSeed(job);
			}
			list.Add(MigrationJobItem.MessageClassEqualityFilter);
			return MigrationJobItem.GetByFilter(provider, primaryFilter, list.ToArray(), null, jobCache, null);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00017828 File Offset: 0x00015A28
		public static int GetCount(IMigrationDataProvider dataProvider, Guid endpointGuid, MigrationSlotType consumedSlotType)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnGuidEmptyArgument(endpointGuid, "endpointGuid");
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobItemSlotProviderId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobItemSlotType, SortOrder.Ascending)
			};
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				MigrationJobItem.MigrationJobItemMessageClassFilter,
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobItemSlotProviderId, endpointGuid),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobItemSlotType, consumedSlotType)
			});
			return dataProvider.CountMessages(filter, sortBy);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000178C4 File Offset: 0x00015AC4
		public static IEnumerable<MigrationJobItem> GetBySlotId(IMigrationDataProvider provider, Guid slotId, int? maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemSlotProviderId, slotId);
			return MigrationJobItem.GetByFilter(provider, primaryFilter, MigrationJobItem.MigrationJobItemMessageClassFilterCollection, null, new MigrationJobObjectCache(provider), maxCount);
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00017904 File Offset: 0x00015B04
		public static IEnumerable<MigrationJobItem> GetByLegacyDN(IMigrationDataProvider provider, string identifier)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(identifier, "identifier");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN, identifier);
			return MigrationJobItem.GetByFilter(provider, primaryFilter, MigrationJobItem.MigrationJobItemMessageClassFilterCollection, null, new MigrationJobObjectCache(provider), null);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00017D8C File Offset: 0x00015F8C
		public static IEnumerable<MigrationJobItem> GetJobItemsByTypeAndGroupMemberProvisionedState(IMigrationDataProvider provider, MigrationJob job, MigrationUserRecipientType recipientType, MigrationUserStatus status, GroupMembershipProvisioningState provisioningState, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobItemRecipientType, recipientType);
			PropertyDefinition[] filterColumns = new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationUserStatus,
				MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState,
				StoreObjectSchema.ItemClass
			};
			SortBy[] additionalSorts = new SortBy[]
			{
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationUserStatus, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState, SortOrder.Ascending)
			};
			IEnumerable<StoreObjectId> messageIdList = provider.FindMessageIds(primaryFilter, filterColumns, additionalSorts, delegate(IDictionary<PropertyDefinition, object> rowData)
			{
				if (!object.Equals(rowData[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				if ((MigrationUserStatus)rowData[MigrationBatchMessageSchema.MigrationUserStatus] != status)
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				if (!MigrationHelper.IsEqualXsoValues(job.JobId, rowData[MigrationBatchMessageSchema.MigrationJobId]))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				if (recipientType != (MigrationUserRecipientType)rowData[MigrationBatchMessageSchema.MigrationJobItemRecipientType])
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				object obj;
				if (!rowData.TryGetValue(MigrationBatchMessageSchema.MigrationJobItemGroupMemberProvisioningState, out obj))
				{
					MigrationLogger.Log(MigrationEventType.Error, "We should not hit this case. This will not cause incorrect behavior but will hide perf issues", new object[0]);
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				int num = (obj == null) ? 0 : ((int)obj);
				if (!Enum.IsDefined(typeof(GroupMembershipProvisioningState), num))
				{
					throw new MigrationDataCorruptionException("Invalid MigrationJobItemGroupMemberProvisioningState.");
				}
				GroupMembershipProvisioningState groupMembershipProvisioningState = (GroupMembershipProvisioningState)num;
				if (groupMembershipProvisioningState == provisioningState)
				{
					return MigrationRowSelectorResult.AcceptRow;
				}
				return MigrationRowSelectorResult.RejectRowStopProcessing;
			}, new int?(maxCount));
			messageIdList = new List<StoreObjectId>(messageIdList);
			MigrationJobObjectCache jobCache = new MigrationJobObjectCache(provider);
			jobCache.PreSeed(job);
			foreach (StoreObjectId messageId in messageIdList)
			{
				yield return MigrationJobItem.Load(provider, messageId, jobCache, true);
			}
			yield break;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00017DD0 File Offset: 0x00015FD0
		public static int GetProvisionedCount(IMigrationDataProvider provider, Guid jobId)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				MigrationJobItem.MigrationJobItemMessageClassFilter,
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId),
				new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, MigrationBatchMessageSchema.MigrationProvisionedTime, ExDateTime.MinValue)
			});
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationProvisionedTime, SortOrder.Ascending)
			};
			return provider.CountMessages(filter, sortBy);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00017E68 File Offset: 0x00016068
		public static int GetCount(IMigrationDataProvider provider, Guid jobId, params MigrationUserStatus[] statuses)
		{
			return MigrationJobItem.GetCount(provider, jobId, null, statuses);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00017E88 File Offset: 0x00016088
		public static int GetCount(IMigrationDataProvider provider, Guid jobId, ExDateTime? lastRestartTime, params MigrationUserStatus[] statuses)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			SortBy[] sortBy = new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationUserStatus, SortOrder.Ascending),
				new SortBy(MigrationBatchMessageSchema.MigrationJobLastRestartTime, SortOrder.Ascending)
			};
			if (statuses == null || statuses.Length == 0)
			{
				QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					MigrationJobItem.MigrationJobItemMessageClassFilter,
					new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId)
				});
				return provider.CountMessages(filter, sortBy);
			}
			int num = 0;
			foreach (MigrationUserStatus migrationUserStatus in statuses)
			{
				QueryFilter filter2;
				if (lastRestartTime != null)
				{
					filter2 = QueryFilter.AndTogether(new QueryFilter[]
					{
						MigrationJobItem.MigrationJobItemMessageClassFilter,
						new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId),
						new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationUserStatus, migrationUserStatus),
						new ComparisonFilter(ComparisonOperator.LessThan, MigrationBatchMessageSchema.MigrationJobLastRestartTime, lastRestartTime.Value)
					});
				}
				else
				{
					filter2 = QueryFilter.AndTogether(new QueryFilter[]
					{
						MigrationJobItem.MigrationJobItemMessageClassFilter,
						new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, jobId),
						new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationUserStatus, migrationUserStatus)
					});
				}
				num += provider.CountMessages(filter2, sortBy);
			}
			return num;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00018008 File Offset: 0x00016208
		public static IEnumerable<MigrationJobItem> GetByStatus(IMigrationDataProvider provider, MigrationJob job, MigrationUserStatus status, int? maxCount)
		{
			return MigrationJobItem.GetByStatusAndRestartTime(provider, job, status, null, maxCount);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00018384 File Offset: 0x00016584
		public static IEnumerable<MigrationJobItem> GetByStatusAndRestartTime(IMigrationDataProvider provider, MigrationJob job, MigrationUserStatus status, ExDateTime? lastRestartTime, int? maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationEqualityFilter primaryFilter = MigrationJobItem.MessageClassEqualityFilter;
			List<MigrationEqualityFilter> secondaryFilters = new List<MigrationEqualityFilter>(2);
			MigrationJobObjectCache jobCache = new MigrationJobObjectCache(provider);
			if (job != null)
			{
				secondaryFilters.Add(new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobId, job.JobId));
				jobCache.PreSeed(job);
			}
			secondaryFilters.Add(new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationUserStatus, status));
			if (lastRestartTime != null)
			{
				secondaryFilters.Add(new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationJobLastRestartTime, lastRestartTime, ComparisonOperator.LessThan));
			}
			IEnumerable<MigrationJobItem> jobItems = MigrationJobItem.GetByFilter(provider, primaryFilter, secondaryFilters.ToArray(), null, jobCache, maxCount);
			foreach (MigrationJobItem jobItem in jobItems)
			{
				if (jobItem.Status == status && (lastRestartTime == null || jobItem.LastRestartTime == null || jobItem.LastRestartTime.Value < lastRestartTime.Value))
				{
					yield return jobItem;
				}
				else
				{
					MigrationLogger.Log(MigrationEventType.Information, "MigrationJobItem.GetByStatus: jobitem {0}, status {1} changed since load to {2}", new object[]
					{
						jobItem,
						status,
						jobItem.Status
					});
				}
			}
			yield break;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000183BE File Offset: 0x000165BE
		public override bool TryLoad(IMigrationDataProvider dataProvider, StoreObjectId id)
		{
			this.TenantName = dataProvider.TenantName;
			return base.TryLoad(dataProvider, id);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x000183D4 File Offset: 0x000165D4
		public bool HasFinalized()
		{
			return this.Status == MigrationUserStatus.Completing || this.Status == MigrationUserStatus.Completed || this.Status == MigrationUserStatus.CompletionSynced || this.Status == MigrationUserStatus.CompletedWithWarnings || this.Status == MigrationUserStatus.CompletionFailed;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018408 File Offset: 0x00016608
		public void SetUserMailboxProperties(IMigrationDataProvider provider, MigrationUserStatus? status, MailboxData mailboxData, Exception error, ExDateTime? provisionedTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (error != null && status == null)
			{
				throw new ArgumentException("if error has value, then status needs to be passed in");
			}
			MigrationStatusData<MigrationUserStatus> migrationStatusData = null;
			if (status != null)
			{
				migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				if (MigrationJobItem.IsFailedStatus(status.Value))
				{
					migrationStatusData.UpdateStatus(status.Value, error, MigrationLogger.CombineInternalError("SetUserMailboxProperties: failed status", error), true, null);
				}
				else if (error != null)
				{
					migrationStatusData.UpdateStatus(status.Value, error, MigrationLogger.CombineInternalError("SetUserMailboxProroperties: setting warning", error), null);
				}
				else
				{
					migrationStatusData.UpdateStatus(status.Value, null);
				}
			}
			this.SetUserMailboxProperties(provider, migrationStatusData, mailboxData, provisionedTime);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018558 File Offset: 0x00016758
		public void UpdateAndEnableJobItem(IMigrationDataProvider dataProvider, MigrationJob job, MigrationUserStatus newStatus)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.UpdateJobData on migration item", new object[0]);
			MigrationStatusData<MigrationUserStatus> statusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			statusData.ClearError();
			statusData.UpdateStatus(newStatus, null);
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.updatePropertyDefinitionsBase,
				MigrationJobItem.DisableMigrationProperties,
				this.SubscriptionSettingsPropertyDefinitions,
				this.SubscriptionIdPropertyDefinitions,
				this.MigrationJobSlotPropertyDefinitions,
				new PropertyDefinition[]
				{
					this.CursorPositionPropertyDefinition,
					MigrationBatchMessageSchema.MigrationJobLastRestartTime
				}
			});
			ExDateTime timeNowUtc = ExDateTime.UtcNow;
			this.UpdatePersistedMessage(dataProvider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				statusData.WriteToMessageItem(message, true);
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime, new ExDateTime?(timeNowUtc));
				this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				this.WriteExtendedPropertiesToMessageItem(message);
				if (this.ShouldMigrate)
				{
					message.Delete(MigrationBatchMessageSchema.MigrationDisableTime);
					message.Delete(MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked);
				}
			});
			this.LastRestartTime = new ExDateTime?(timeNowUtc);
			this.StatusData = statusData;
			if (this.ShouldMigrate)
			{
				this.SubscriptionLastChecked = null;
				this.SubscriptionDisableTime = null;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018798 File Offset: 0x00016998
		public bool UpdateDataRow(IMigrationDataProvider dataProvider, MigrationJob job, IMigrationDataRow request)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(request, "request");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.UpdateDataRow on migration item", new object[0]);
			bool result = this.MigrationJobId == job.JobId;
			this.BatchInputId = job.BatchInputId;
			this.MigrationJob = job;
			this.IsStaged = job.IsStaged;
			this.JobName = job.JobName;
			this.IncrementalSyncInterval = null;
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.updatePropertyDefinitionsBase,
				MigrationJobItem.DisableMigrationProperties,
				this.ProvisioningDataPropertyDefinitions,
				this.SubscriptionSettingsPropertyDefinitions,
				this.SubscriptionIdPropertyDefinitions,
				new PropertyDefinition[]
				{
					this.CursorPositionPropertyDefinition,
					MigrationBatchMessageSchema.MigrationJobItemIncomingUsername
				}
			});
			ProvisioningDataStorageBase provisioningData = ProvisioningDataStorageBase.CreateFromDataRow(request, false);
			JobItemSubscriptionSettingsBase jobItemSubscriptionSettingsBase;
			if (this.SubscriptionSettings != null)
			{
				jobItemSubscriptionSettingsBase = ((JobItemSubscriptionSettingsBase)this.SubscriptionSettings).Clone();
				jobItemSubscriptionSettingsBase.UpdateFromDataRow(request);
			}
			else
			{
				jobItemSubscriptionSettingsBase = JobItemSubscriptionSettingsBase.CreateFromDataRow(request);
			}
			JobItemSubscriptionSettingsBase subscriptionSettingsToSave = jobItemSubscriptionSettingsBase ?? JobItemSubscriptionSettingsBase.Create(this.MigrationType);
			this.UpdatePersistedMessage(dataProvider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				message[MigrationBatchMessageSchema.MigrationJobId] = job.JobId;
				message[this.CursorPositionPropertyDefinition] = request.CursorPosition;
				if (provisioningData != null)
				{
					provisioningData.WriteToMessageItem(message, true);
				}
				if (subscriptionSettingsToSave != null)
				{
					subscriptionSettingsToSave.WriteToMessageItem(message, true);
				}
				if (request.SupportsRemoteIdentifier)
				{
					message[MigrationBatchMessageSchema.MigrationJobItemIncomingUsername] = request.RemoteIdentifier;
				}
				this.WriteExtendedPropertiesToMessageItem(message);
				if (!this.ShouldMigrate)
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "setting subscription last check to max date time for recipient type: {0}", new object[]
					{
						this.RecipientType
					});
					message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked] = MigrationJobItem.MaxDateTimeValue;
				}
			});
			if (request.SupportsRemoteIdentifier)
			{
				this.RemoteIdentifier = request.RemoteIdentifier;
			}
			this.MigrationJobId = job.JobId;
			this.ProvisioningData = provisioningData;
			this.CursorPosition = request.CursorPosition;
			this.SubscriptionSettings = jobItemSubscriptionSettingsBase;
			if (!this.ShouldMigrate)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "setting subscription last check to max date time for recipient type: {0}", new object[]
				{
					this.RecipientType
				});
				this.SubscriptionLastChecked = new ExDateTime?(MigrationJobItem.MaxDateTimeValue);
			}
			return result;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000189E0 File Offset: 0x00016BE0
		public void SetTransientError(IMigrationDataProvider provider, Exception exception)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(exception, "exception");
			string diagnosticInfo = MigrationLogger.GetDiagnosticInfo(exception, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJobItem.SetTransientError: job {0}, {1}", new object[]
			{
				this,
				diagnosticInfo
			});
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			migrationStatusData.SetTransientError(exception, null, null);
			this.SetStatusData(provider, migrationStatusData);
			MigrationFailureLog.LogFailureEvent(this, exception, MigrationFailureFlags.None, null);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00018A60 File Offset: 0x00016C60
		public void SetCorruptStatus(IMigrationDataProvider provider, Exception exception)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(exception, "exception");
			string diagnosticInfo = MigrationLogger.GetDiagnosticInfo(exception, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJobItem.SetFailedStatus: jobitem {0}, error {1}", new object[]
			{
				this,
				diagnosticInfo
			});
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			migrationStatusData.SetFailedStatus(MigrationUserStatus.Corrupted, exception, diagnosticInfo, null);
			this.SetStatusData(provider, migrationStatusData);
			if (this.MigrationJob != null && this.MigrationJob.ReportData != null)
			{
				this.MigrationJob.ReportData.Append(Strings.MigrationReportJobItemCorrupted(this.Identifier), exception, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				provider.FlushReport(this.MigrationJob.ReportData);
			}
			MigrationFailureLog.LogFailureEvent(this, exception, MigrationFailureFlags.Corruption, null);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018B1C File Offset: 0x00016D1C
		public void SetFailedStatus(IMigrationDataProvider provider, MigrationUserStatus status, LocalizedException localizedError, string internalError)
		{
			this.SetFailedStatus(provider, status, localizedError, internalError, false);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018B2C File Offset: 0x00016D2C
		public void SetFailedStatus(IMigrationDataProvider provider, MigrationUserStatus status, LocalizedException localizedError, string internalError, bool setLastRestartTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (!MigrationJobItem.IsFailedStatus(status))
			{
				throw new ArgumentException("Expect a failed status");
			}
			MigrationUtil.ThrowOnNullOrEmptyArgument(internalError, "internalError");
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			migrationStatusData.UpdateStatus(status, localizedError, internalError, true, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJobItem.SetStatus: jobitem {0}, statusData {1}", new object[]
			{
				this,
				migrationStatusData
			});
			this.SetStatusData(provider, migrationStatusData, setLastRestartTime);
			if (this.MigrationJob != null && this.MigrationJob.ReportData != null)
			{
				this.MigrationJob.ReportData.Append(Strings.MigrationReportJobItemFailed(this.Identifier, localizedError.LocalizedString), localizedError, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				provider.FlushReport(this.MigrationJob.ReportData);
			}
			MigrationFailureLog.LogFailureEvent(this, localizedError, MigrationFailureFlags.Fatal, null);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00018DDC File Offset: 0x00016FDC
		public void SetStatus(IMigrationDataProvider provider, MigrationUserStatus status, MigrationState state, MigrationFlags? flags = null, MigrationWorkflowPosition position = null, TimeSpan? delayTime = null, IMailboxData mailboxData = null, IStepSettings stepSettings = null, IStepSnapshot stepSnapshot = null, bool updated = false, LocalizedException exception = null)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			string internalError = null;
			if (exception != null)
			{
				internalError = MigrationLogger.GetDiagnosticInfo(exception, null);
			}
			MigrationStatusData<MigrationUserStatus> statusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			if (state == MigrationState.Failed || state == MigrationState.Corrupted)
			{
				statusData.SetFailedStatus(status, exception, internalError, new MigrationState?(state));
			}
			else if (exception != null)
			{
				statusData.SetTransientError(exception, new MigrationUserStatus?(status), new MigrationState?(state));
			}
			else
			{
				statusData.UpdateStatus(status, new MigrationState?(state));
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetStatusAndSubscription: jobitem {0}, status {1}", new object[]
			{
				this,
				statusData
			});
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.MigrationJobItemColumnsStatusIndex,
				this.MailboxDataPropertyDefinitions,
				MigrationWorkflowPosition.MigrationWorkflowPositionProperties,
				this.SubscriptionIdPropertyDefinitions,
				this.SubscriptionSettingsPropertyDefinitions,
				this.MigrationJobSlotPropertyDefinitions,
				new StorePropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationFlags,
					MigrationBatchMessageSchema.MigrationNextProcessTime,
					MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime,
					MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
					MigrationBatchMessageSchema.MigrationJobItemSubscriptionQueuedTime,
					MigrationBatchMessageSchema.MigrationProvisionedTime
				}
			});
			ExDateTime? subscriptionLastChecked = null;
			ExDateTime? provisionedTime = null;
			ExDateTime? subscriptionQueuedTime = null;
			if (stepSnapshot != null)
			{
				if (stepSnapshot is ISubscriptionStatistics)
				{
					subscriptionLastChecked = new ExDateTime?(ExDateTime.UtcNow);
					subscriptionQueuedTime = stepSnapshot.InjectionCompletedTime;
				}
				else if (stepSnapshot is ProvisioningSnapshot)
				{
					if (stepSnapshot.Status == SnapshotStatus.Finalized && this.WorkflowPosition.Step == MigrationStep.Provisioning)
					{
						provisionedTime = stepSnapshot.InjectionCompletedTime;
					}
					mailboxData = ((ProvisioningSnapshot)stepSnapshot).MailboxData;
				}
			}
			ExDateTime? nextProcessTime = null;
			if (delayTime != null)
			{
				nextProcessTime = new ExDateTime?(ExDateTime.UtcNow + delayTime.Value);
			}
			else
			{
				nextProcessTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			ExDateTime? subscriptionSettingsLastUpdatedTime = null;
			if (updated)
			{
				subscriptionSettingsLastUpdatedTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
					this.WriteExtendedPropertiesToMessageItem(message);
					this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				}
				if (nextProcessTime != null)
				{
					MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationNextProcessTime, new ExDateTime?(nextProcessTime.Value));
				}
				if (flags != null)
				{
					message[MigrationBatchMessageSchema.MigrationFlags] = flags.Value;
				}
				if (position != null)
				{
					position.WriteToMessageItem(message, true);
				}
				if (subscriptionLastChecked != null)
				{
					message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked] = subscriptionLastChecked.Value;
				}
				if (subscriptionQueuedTime != null)
				{
					message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionQueuedTime] = subscriptionQueuedTime.Value;
				}
				if (provisionedTime != null)
				{
					message[MigrationBatchMessageSchema.MigrationProvisionedTime] = provisionedTime.Value;
				}
				if (mailboxData != null)
				{
					mailboxData.WriteToMessageItem(message, true);
				}
				if (stepSettings != null)
				{
					stepSettings.WriteToMessageItem(message, true);
				}
				if (stepSnapshot != null && stepSnapshot is IMigrationSerializable)
				{
					((IMigrationSerializable)stepSnapshot).WriteToMessageItem(message, true);
					if (stepSnapshot.Id != null && stepSnapshot.Id is IMigrationSerializable)
					{
						((IMigrationSerializable)stepSnapshot.Id).WriteToMessageItem(message, true);
					}
				}
				if (subscriptionSettingsLastUpdatedTime != null)
				{
					MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime, new ExDateTime?(subscriptionSettingsLastUpdatedTime.Value));
				}
			});
			if (stepSnapshot != null)
			{
				if (stepSnapshot is ISubscriptionStatistics)
				{
					this.SubscriptionStatistics = (ISubscriptionStatistics)stepSnapshot;
					this.SubscriptionId = (ISubscriptionId)stepSnapshot.Id;
				}
				else if (stepSnapshot is ProvisioningSnapshot)
				{
					this.ProvisioningStatistics = (ProvisioningSnapshot)stepSnapshot;
				}
			}
			if (stepSettings != null)
			{
				if (stepSettings is JobItemSubscriptionSettingsBase)
				{
					this.SubscriptionSettings = (JobItemSubscriptionSettingsBase)stepSettings;
				}
				else if (stepSettings is ProvisioningDataStorageBase)
				{
					this.ProvisioningData = (ProvisioningDataStorageBase)stepSettings;
				}
			}
			if (mailboxData != null)
			{
				this.LocalMailbox = mailboxData;
			}
			if (subscriptionLastChecked != null)
			{
				this.SubscriptionLastChecked = new ExDateTime?(subscriptionLastChecked.Value);
			}
			if (subscriptionQueuedTime != null)
			{
				this.SubscriptionQueuedTime = new ExDateTime?(subscriptionQueuedTime.Value);
			}
			if (provisionedTime != null)
			{
				this.ProvisionedTime = new ExDateTime?(provisionedTime.Value);
			}
			if (flags != null)
			{
				this.Flags = flags.Value;
			}
			if (position != null)
			{
				this.WorkflowPosition = position;
			}
			if (nextProcessTime != null)
			{
				this.NextProcessTime = nextProcessTime;
			}
			if (subscriptionSettingsLastUpdatedTime != null)
			{
				this.SubscriptionSettingsLastUpdatedTime = new ExDateTime?(subscriptionSettingsLastUpdatedTime.Value);
			}
			MigrationUserStatus migrationUserStatus = this.Status;
			this.StatusData = statusData;
			if (this.Status != migrationUserStatus)
			{
				this.LogStatusEvent();
			}
			if (exception != null)
			{
				MigrationFailureFlags failureFlags;
				switch (state)
				{
				case MigrationState.Failed:
					failureFlags = MigrationFailureFlags.Fatal;
					break;
				case MigrationState.Corrupted:
					failureFlags = MigrationFailureFlags.Corruption;
					break;
				default:
					failureFlags = MigrationFailureFlags.None;
					break;
				}
				MigrationFailureLog.LogFailureEvent(this, exception, failureFlags, null);
			}
			if (state == MigrationState.Failed && this.MigrationJob != null && this.MigrationJob.ReportData != null)
			{
				this.MigrationJob.ReportData.Append(Strings.MigrationReportJobItemFailed(this.Identifier, exception.LocalizedString), exception, ReportEntryFlags.Failure | ReportEntryFlags.Fatal | ReportEntryFlags.Target);
				provider.FlushReport(this.MigrationJob.ReportData);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019344 File Offset: 0x00017544
		public List<string> GetGroupMembersInfo(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			MigrationUtil.AssertOrThrow(this.ShouldProvision, "Does not support provisioning!", new object[0]);
			MigrationUtil.AssertOrThrow(this.ProvisioningData != null, "Provisioning Data missing!", new object[0]);
			MigrationUtil.AssertOrThrow(this.ProvisioningData is ExchangeProvisioningDataStorage, "Provisioning data wrong type!", new object[0]);
			MigrationUtil.AssertOrThrow(((ExchangeProvisioningDataStorage)this.ProvisioningData).ExchangeRecipient != null, "Provisioning Recipient Data missing!", new object[0]);
			MigrationUtil.AssertOrThrow(this.RecipientType == MigrationUserRecipientType.Group, "ExchangeRecipient has to be type of Group", new object[0]);
			List<string> members = null;
			this.UpdatePersistedMessage(provider, MigrationJobItem.GroupProperties, delegate(IMigrationMessageItem message)
			{
				members = ((LegacyExchangeMigrationGroupRecipient)((ExchangeProvisioningDataStorage)this.ProvisioningData).ExchangeRecipient).GetNextBatchOfMembers(provider, message);
			});
			return members;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000194FC File Offset: 0x000176FC
		public void SetGroupProperties(IMigrationDataProvider provider, MigrationUserStatus? status, ExDateTime? provisionedTime, string[] members, int membersProvisioned, int membersSkipped)
		{
			MigrationUtil.AssertOrThrow(this.ShouldProvision, "Does not support provisioning!", new object[0]);
			MigrationUtil.AssertOrThrow(this.ProvisioningData != null, "Provisioning Data missing!", new object[0]);
			MigrationUtil.AssertOrThrow(this.ProvisioningData is ExchangeProvisioningDataStorage, "Provisioning data wrong type!", new object[0]);
			MigrationUtil.AssertOrThrow(((ExchangeProvisioningDataStorage)this.ProvisioningData).ExchangeRecipient != null, "Provisioning Recipient Data missing!", new object[0]);
			MigrationUtil.AssertOrThrow(this.RecipientType == MigrationUserRecipientType.Group, "ExchangeRecipient has to be type of Group", new object[0]);
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationStatusData<MigrationUserStatus> statusData = null;
			if (status != null)
			{
				statusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				statusData.UpdateStatus(status.Value, null);
			}
			this.UpdatePersistedMessage(provider, MigrationJobItem.GroupProperties, delegate(IMigrationMessageItem message)
			{
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
					this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				}
				if (provisionedTime != null)
				{
					message[MigrationBatchMessageSchema.MigrationProvisionedTime] = provisionedTime.Value;
				}
				LegacyExchangeMigrationGroupRecipient legacyExchangeMigrationGroupRecipient = (LegacyExchangeMigrationGroupRecipient)((ExchangeProvisioningDataStorage)this.ProvisioningData).ExchangeRecipient;
				if (members != null)
				{
					legacyExchangeMigrationGroupRecipient.SetGroupMembersInfo(message, members);
					return;
				}
				legacyExchangeMigrationGroupRecipient.UpdateGroupMembersInfo(message, membersProvisioned, membersSkipped);
			});
			if (provisionedTime != null)
			{
				this.ProvisionedTime = provisionedTime;
			}
			if (statusData != null)
			{
				this.StatusData = statusData;
				this.LogStatusEvent();
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000196C0 File Offset: 0x000178C0
		public void DisableMigration(IMigrationDataProvider provider, MigrationUserStatus status)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (!MigrationJobItem.IsFailedStatus(status) && !MigrationJobItem.IsStoppedStatus(status))
			{
				throw new ArgumentException("Expect a failed or stopped status");
			}
			MigrationStatusData<MigrationUserStatus> newStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			if ((!MigrationJobItem.IsFailedStatus(this.Status) && MigrationJobItem.IsFailedStatus(status)) || (!MigrationJobItem.IsStoppedStatus(this.Status) && MigrationJobItem.IsStoppedStatus(status)))
			{
				MigrationLogger.Log(MigrationEventType.Information, "MigrationJobItem.DisableMigration: jobitem {0}, updating status {1}", new object[]
				{
					this,
					status
				});
				newStatusData.UpdateStatus(status, new MigrationCancelledByUserRequestException(), "migration disabled", true, null);
			}
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.DisableMigrationProperties,
				this.MigrationJobSlotPropertyDefinitions
			});
			ExDateTime timeNow = ExDateTime.UtcNow;
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				newStatusData.WriteToMessageItem(message, true);
				message[MigrationBatchMessageSchema.MigrationDisableTime] = timeNow;
				message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked] = MigrationJobItem.MaxDateTimeValue;
				this.CheckAndReleaseSlotAssignmentIfNeeded(newStatusData.Status, message);
			});
			this.StatusData = newStatusData;
			this.SubscriptionLastChecked = new ExDateTime?(MigrationJobItem.MaxDateTimeValue);
			this.SubscriptionDisableTime = new ExDateTime?(timeNow);
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000197EC File Offset: 0x000179EC
		public void SetStatus(IMigrationDataProvider provider, MigrationUserStatus status)
		{
			if (MigrationJobItem.IsFailedStatus(status))
			{
				throw new ArgumentException("Use SetFailedStatus instead");
			}
			MigrationStatusData<MigrationUserStatus> migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
			migrationStatusData.UpdateStatus(status, null);
			MigrationLogger.Log(MigrationEventType.Error, "MigrationJobItem.SetStatus: jobitem {0}, statusData {1}", new object[]
			{
				this,
				migrationStatusData
			});
			this.SetStatusData(provider, migrationStatusData);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001984C File Offset: 0x00017A4C
		public void SetStatusAndSubscriptionLastChecked(IMigrationDataProvider provider, MigrationUserStatus? status, LocalizedException localizedError, ExDateTime? subscriptionLastChecked, ISubscriptionStatistics stats)
		{
			this.SetStatusAndSubscriptionLastChecked(provider, status, localizedError, subscriptionLastChecked, true, stats);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001985C File Offset: 0x00017A5C
		public void SetSubscriptionFailed(IMigrationDataProvider provider, MigrationUserStatus status, LocalizedException localizedError)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(localizedError, "localizedError");
			if (!MigrationJobItem.IsFailedStatus(status))
			{
				throw new ArgumentException("Failed state not supported for " + status.ToString());
			}
			this.SetStatusAndSubscriptionLastChecked(provider, new MigrationUserStatus?(status), localizedError, new ExDateTime?(ExDateTime.UtcNow), true, null);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000198BC File Offset: 0x00017ABC
		public void SetStatusAndSubscriptionLastChecked(IMigrationDataProvider provider, MigrationUserStatus? status, LocalizedException localizedError, ExDateTime? subscriptionLastChecked, bool supportIncrementalSync, ISubscriptionStatistics stats)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (status == null && localizedError != null)
			{
				throw new ArgumentException("error should not be updated without updating status");
			}
			if (status != null && (status == MigrationUserStatus.Failed || status == MigrationUserStatus.IncrementalFailed || status == MigrationUserStatus.CompletedWithWarnings || status == MigrationUserStatus.CompletionFailed) && localizedError == null)
			{
				throw new ArgumentException("An error message must be provided if the status is failed, CompletionFailed or CompletedWithWarnings");
			}
			MigrationStatusData<MigrationUserStatus> migrationStatusData = null;
			if (status != null)
			{
				migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				if (MigrationJobItem.IsFailedStatus(status.Value))
				{
					migrationStatusData.UpdateStatus(status.Value, localizedError, MigrationLogger.CombineInternalError("SetStatusAndSubscriptionLastChecked: failed status", localizedError), true, null);
				}
				else
				{
					if (status.Value == MigrationUserStatus.Synced || status.Value == MigrationUserStatus.Completed || status.Value == MigrationUserStatus.CompletedWithWarnings)
					{
						if (this.StateLastUpdated == null)
						{
							throw MigrationHelperBase.CreatePermanentExceptionWithInternalData<MigrationUnknownException>("if we've completed, we should have a state last updated");
						}
						TimeSpan timeSpan = ExDateTime.UtcNow - this.StateLastUpdated.Value;
						if (this.InitialSyncDuration == null)
						{
							this.InitialSyncDuration = new TimeSpan?(timeSpan);
						}
						else if (this.IncrementalSyncDuration == null)
						{
							this.IncrementalSyncDuration = new TimeSpan?(timeSpan);
						}
						else
						{
							this.IncrementalSyncDuration += timeSpan;
						}
					}
					if (localizedError != null)
					{
						migrationStatusData.UpdateStatus(status.Value, localizedError, MigrationLogger.CombineInternalError("SetStatusAndSubscriptionLastChecked: setting warning", localizedError), null);
					}
					else
					{
						migrationStatusData.UpdateStatus(status.Value, null);
					}
				}
			}
			else if (subscriptionLastChecked != null && !string.IsNullOrEmpty(this.StatusData.InternalError))
			{
				migrationStatusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				MigrationLogger.Log(MigrationEventType.Information, "MigrationJobItem.SetStatusAndSubscriptionLastChecked: jobitem {0}, clearing out error: {1}", new object[]
				{
					this,
					migrationStatusData
				});
				migrationStatusData.ClearError();
			}
			if (!supportIncrementalSync || !this.ShouldMigrate)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "setting subscription last check to max date time for recipient type: {0}, overriding {1}", new object[]
				{
					this.RecipientType,
					subscriptionLastChecked
				});
				subscriptionLastChecked = new ExDateTime?(MigrationJobItem.MaxDateTimeValue);
			}
			this.SetStatusAndSubscriptionLastChecked(provider, migrationStatusData, subscriptionLastChecked, stats);
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00019B68 File Offset: 0x00017D68
		public void Delete(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (base.StoreObjectId != null)
			{
				provider.RemoveMessage(base.StoreObjectId);
				if (this.MigrationJob != null && this.MigrationJob.ReportData != null)
				{
					this.MigrationJob.ReportData.Append(Strings.MigrationReportJobItemRemovedInternal(this.Identifier));
				}
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00019BC4 File Offset: 0x00017DC4
		public void SetStatusData(IMigrationDataProvider provider, MigrationStatusData<MigrationUserStatus> statusData)
		{
			this.SetStatusData(provider, statusData, false);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00019C40 File Offset: 0x00017E40
		public void SetSubscriptionId(IMigrationDataProvider provider, ISubscriptionId subscriptionId, MigrationUserStatus? itemStatus)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetSubscriptionId: Setting SubscriptionId for migration item: {0}.", new object[]
			{
				subscriptionId
			});
			ISubscriptionId subscriptionIdToSave = subscriptionId ?? SubscriptionIdHelper.Create(this.MigrationType, null, this.IsPAW);
			ExDateTime timeNow = ExDateTime.UtcNow;
			MigrationStatusData<MigrationUserStatus> statusData = null;
			if (itemStatus != null)
			{
				statusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				statusData.UpdateStatus(itemStatus.Value, null);
			}
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				this.SubscriptionIdPropertyDefinitions,
				MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition,
				new StorePropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked
				}
			});
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				if (subscriptionIdToSave != null)
				{
					subscriptionIdToSave.WriteToMessageItem(message, true);
				}
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
					this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				}
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked, new ExDateTime?(timeNow));
			});
			this.SubscriptionId = subscriptionId;
			this.SubscriptionLastChecked = new ExDateTime?(timeNow);
			if (statusData != null)
			{
				this.StatusData = statusData;
				this.LogStatusEvent();
			}
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00019D98 File Offset: 0x00017F98
		public void SetMigrationFlags(IMigrationDataProvider provider, MigrationFlags flags)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetMigrationFlags: job-item {0} flags {1}", new object[]
			{
				this,
				flags
			});
			PropertyDefinition[] array = new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationFlags
			};
			MigrationStatusData<MigrationUserStatus> statusData = null;
			MigrationUserStatus? migrationUserStatus = MigrationJobItem.ResolveFlagStatus(flags);
			if (migrationUserStatus != null)
			{
				array = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition,
					array
				});
				statusData = new MigrationStatusData<MigrationUserStatus>(this.StatusData);
				statusData.UpdateStatus(migrationUserStatus.Value, null);
			}
			this.UpdatePersistedMessage(provider, array, delegate(IMigrationMessageItem message)
			{
				message[MigrationBatchMessageSchema.MigrationFlags] = flags;
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
				}
			});
			this.Flags = flags;
			if (statusData != null)
			{
				this.StatusData = statusData;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00019EB4 File Offset: 0x000180B4
		public void SetSubscriptionSettings(IMigrationDataProvider provider, JobItemSubscriptionSettingsBase subscriptionSettings)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetSubscriptionSettings for migration item: {0}.", new object[]
			{
				this
			});
			JobItemSubscriptionSettingsBase subscriptionSettingsToSave = subscriptionSettings ?? JobItemSubscriptionSettingsBase.Create(this.MigrationType);
			this.UpdatePersistedMessage(provider, this.SubscriptionSettingsPropertyDefinitions, delegate(IMigrationMessageItem message)
			{
				if (subscriptionSettingsToSave != null)
				{
					subscriptionSettingsToSave.WriteToMessageItem(message, true);
				}
			});
			this.SubscriptionSettings = subscriptionSettings;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00019F94 File Offset: 0x00018194
		public void SetStatusData(IMigrationDataProvider provider, MigrationStatusData<MigrationUserStatus> statusData, bool setLastRestartTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(statusData, "statusData");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.updatePropertyDefinitionsBase,
				new StorePropertyDefinition[]
				{
					MigrationBatchMessageSchema.MigrationJobLastRestartTime
				}
			});
			this.UpdateIncrementalSyncFailureCount(statusData);
			ExDateTime timeNowUtc = ExDateTime.UtcNow;
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				statusData.WriteToMessageItem(message, true);
				this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				if (setLastRestartTime)
				{
					MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime, new ExDateTime?(timeNowUtc));
				}
			});
			if (setLastRestartTime)
			{
				this.LastRestartTime = new ExDateTime?(timeNowUtc);
			}
			MigrationUserStatus migrationUserStatus = this.Status;
			this.StatusData = statusData;
			if (this.Status != migrationUserStatus)
			{
				this.LogStatusEvent();
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001A085 File Offset: 0x00018285
		public void SetTroubleshooterNotes(IMigrationDataProvider provider, string notes)
		{
			this.TroubleshooterNotes = notes;
			this.UpdatePersistedMessage(provider, MigrationPersistableBase.MigrationBaseDefinitions, SaveMode.ResolveConflicts, new Action<IMigrationMessageItem>(this.WriteExtendedPropertiesToMessageItem));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001A0A8 File Offset: 0x000182A8
		public void UpdateConsumedSlot(Guid providerGuid, MigrationSlotType slotType, IMigrationDataProvider dataProvider)
		{
			this.MigrationSlotProviderGuid = providerGuid;
			this.ConsumedSlotType = slotType;
			this.UpdatePersistedMessage(dataProvider, this.MigrationJobSlotPropertyDefinitions, new Action<IMigrationMessageItem>(this.SaveConsumedSlotInformationToMessage));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001A0EC File Offset: 0x000182EC
		public void SetSubscriptionLastUpdatedTime(IMigrationDataProvider provider, ExDateTime? subscriptionSettingsLastUpdatedTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetLastSubscriptionUpdatedTime: Setting last subscription updated time for migration item to: {0}.", new object[]
			{
				subscriptionSettingsLastUpdatedTime
			});
			this.UpdatePersistedMessage(provider, new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime
			}, delegate(IMigrationMessageItem message)
			{
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime, subscriptionSettingsLastUpdatedTime);
			});
			this.SubscriptionSettingsLastUpdatedTime = subscriptionSettingsLastUpdatedTime;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001A164 File Offset: 0x00018364
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationJobItem", new object[]
			{
				new XAttribute("name", this.Identifier),
				new XAttribute("guid", this.JobItemGuid)
			});
			if (this.StatusData != null)
			{
				xelement.Add(this.StatusData.GetDiagnosticInfo(dataProvider, argument));
			}
			if (this.IsPAW)
			{
				xelement.Add(new object[]
				{
					new XElement("flags", this.Flags),
					new XElement("nextProcessTime", this.NextProcessTime ?? ExDateTime.MinValue)
				});
				if (this.WorkflowPosition != null)
				{
					xelement.Add(this.WorkflowPosition.GetDiagnosticInfo(dataProvider, argument));
				}
			}
			base.GetDiagnosticInfo(dataProvider, argument, xelement);
			if (!argument.HasArgument("verbose"))
			{
				return xelement;
			}
			xelement.Add(new XElement("localMailboxIdentifier", this.LocalMailboxIdentifier));
			xelement.Add(new XElement("remoteIdentifier", this.RemoteIdentifier));
			xelement.Add(new XElement("migrationJobItemType", this.MigrationType));
			xelement.Add(new XElement("recipientType", this.RecipientType));
			if (this.LocalMailbox != null)
			{
				xelement.Add(this.LocalMailbox.GetDiagnosticInfo(dataProvider, argument));
			}
			xelement.Add(new object[]
			{
				new XElement("version", base.Version),
				new XElement("messageId", base.StoreObjectId),
				new XElement("belongsToJob", this.MigrationJobId),
				new XElement("subscriptionLastChecked", this.SubscriptionLastChecked),
				new XElement("subscriptionQueuedTime", this.SubscriptionQueuedTime),
				new XElement("provisionedTime", this.ProvisionedTime),
				new XElement("SubscriptionSettingsLastUpdatedTime", this.SubscriptionSettingsLastUpdatedTime),
				new XElement("MigrationSlotProviderGuid", this.MigrationSlotProviderGuid),
				new XElement("ConsumedSlotType", this.ConsumedSlotType)
			});
			if (this.ProvisioningData != null)
			{
				xelement.Add(this.ProvisioningData.GetDiagnosticInfo(dataProvider, argument));
			}
			if (this.MigrationType == MigrationType.PublicFolder)
			{
				xelement.Add(new XElement("LastFinalizationAttempt", this.LastFinalizationAttempt));
				xelement.Add(new XElement("PublicFolderCompletionFailures", this.PublicFolderCompletionFailures));
			}
			if (this.IsPAW)
			{
				IMigrationSerializable migrationSerializable = this.ProvisioningStatistics as IMigrationSerializable;
				if (migrationSerializable != null)
				{
					xelement.Add(migrationSerializable.GetDiagnosticInfo(dataProvider, argument));
				}
			}
			if (this.SubscriptionId != null)
			{
				xelement.Add(this.SubscriptionId.GetDiagnosticInfo(dataProvider, argument));
			}
			if (this.SubscriptionSettings != null)
			{
				xelement.Add(this.SubscriptionSettings.GetDiagnosticInfo(dataProvider, argument));
			}
			if (this.SubscriptionStatistics != null)
			{
				xelement.Add(this.SubscriptionStatistics.GetDiagnosticInfo(dataProvider, argument));
			}
			return xelement;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001A500 File Offset: 0x00018700
		public override string ToString()
		{
			if (this.IsPAW)
			{
				return string.Format("{0} ({1}\\{2}) {3} {4}", new object[]
				{
					this.Identifier,
					this.MigrationJobId,
					this.JobItemGuid,
					this.WorkflowPosition,
					this.State
				});
			}
			return string.Format("{0}:{1}:{2}:{3}", new object[]
			{
				this.JobItemGuid,
				this.MigrationJobId,
				this.Identifier,
				this.Status
			});
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001A5AC File Offset: 0x000187AC
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			base.ReadFromMessageItem(message);
			if (this.IsPAW)
			{
				this.Flags = MigrationHelper.GetEnumProperty<MigrationFlags>(message, MigrationBatchMessageSchema.MigrationFlags);
				this.NextProcessTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationNextProcessTime);
				this.WorkflowPosition = MigrationWorkflowPosition.CreateFromMessage(message);
			}
			this.MigrationJobId = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobId, true);
			this.Identifier = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemIdentifier, null);
			this.LocalMailboxIdentifier = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemLocalMailboxIdentifier, this.Identifier);
			this.JobItemGuid = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemId, false);
			this.StatusData = MigrationStatusData<MigrationUserStatus>.Create(message, MigrationJobItem.StatusDataVersionMap[base.Version]);
			this.LocalMailbox = MailboxDataHelper.CreateFromMessage(message, this.MigrationType);
			if (this.LocalMailbox != null)
			{
				this.LocalMailbox.Update(this.LocalMailboxIdentifier, base.OrganizationId);
			}
			this.RecipientType = MigrationHelper.GetEnumProperty<MigrationUserRecipientType>(message, MigrationBatchMessageSchema.MigrationJobItemRecipientType);
			this.SubscriptionLastChecked = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked);
			this.SubscriptionQueuedTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionQueuedTime);
			this.SubscriptionDisableTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationDisableTime);
			this.ProvisionedTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationProvisionedTime);
			this.LastRestartTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime);
			this.SubscriptionSettingsLastUpdatedTime = MigrationHelper.GetExDateTimePropertyOrNull(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime);
			this.CursorPosition = message.GetValueOrDefault<int>(this.CursorPositionPropertyDefinition, -1);
			this.ConsumedSlotType = (MigrationHelper.GetEnumPropertyOrNull<MigrationSlotType>(message, MigrationBatchMessageSchema.MigrationJobItemSlotType) ?? MigrationSlotType.None);
			this.MigrationSlotProviderGuid = MigrationHelper.GetGuidProperty(message, MigrationBatchMessageSchema.MigrationJobItemSlotProviderId, false);
			this.RemoteIdentifier = message.GetValueOrDefault<string>(MigrationBatchMessageSchema.MigrationJobItemIncomingUsername, null);
			this.LastFinalizationAttempt = message.GetValueOrDefault<int>(MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt, 0);
			if (this.ShouldProvision)
			{
				this.ProvisioningData = ProvisioningDataStorageBase.CreateFromMessage(message, this.MigrationType, this.RecipientType, this.IsPAW);
				if (this.IsPAW)
				{
					this.ProvisioningStatistics = ProvisioningSnapshot.CreateFromMessage(message, this.RecipientType);
				}
			}
			this.SubscriptionId = SubscriptionIdHelper.CreateFromMessage(message, this.MigrationType, this.LocalMailbox, this.IsPAW);
			this.SubscriptionSettings = JobItemSubscriptionSettingsBase.CreateFromMessage(message, this.MigrationType);
			this.SubscriptionStatistics = SubscriptionSnapshot.CreateFromMessage(message);
			return true;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationType] = (int)this.MigrationType;
			message[MigrationBatchMessageSchema.MigrationVersion] = base.Version;
			message[StoreObjectSchema.ItemClass] = MigrationBatchMessageSchema.MigrationJobItemClass;
			message[MigrationBatchMessageSchema.MigrationJobId] = this.MigrationJobId;
			message[MigrationBatchMessageSchema.MigrationJobItemId] = this.JobItemGuid;
			message[MigrationBatchMessageSchema.MigrationJobItemRecipientType] = this.RecipientType;
			message[MigrationBatchMessageSchema.MigrationJobItemIdentifier] = this.Identifier;
			message[MigrationBatchMessageSchema.MigrationJobItemItemsSynced] = this.ItemsSynced;
			message[MigrationBatchMessageSchema.MigrationJobItemItemsSkipped] = this.ItemsSkipped;
			if (!string.Equals(this.Identifier, this.LocalMailboxIdentifier, StringComparison.OrdinalIgnoreCase))
			{
				message[MigrationBatchMessageSchema.MigrationJobItemLocalMailboxIdentifier] = this.LocalMailboxIdentifier;
			}
			if (this.IsPAW)
			{
				message[MigrationBatchMessageSchema.MigrationFlags] = this.Flags;
				MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationNextProcessTime, this.NextProcessTime);
				if (this.WorkflowPosition != null)
				{
					this.WorkflowPosition.WriteToMessageItem(message, loaded);
				}
			}
			this.StatusData.WriteToMessageItem(message, loaded);
			if (this.LocalMailbox != null)
			{
				this.LocalMailbox.WriteToMessageItem(message, loaded);
			}
			if (this.SubscriptionLastChecked != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked] = this.SubscriptionLastChecked.Value;
			}
			if (this.SubscriptionDisableTime != null)
			{
				message[MigrationBatchMessageSchema.MigrationDisableTime] = this.SubscriptionDisableTime.Value;
			}
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobLastRestartTime, this.LastRestartTime);
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime, this.SubscriptionSettingsLastUpdatedTime);
			this.SaveConsumedSlotInformationToMessage(message);
			if (this.RemoteIdentifier != null)
			{
				message[MigrationBatchMessageSchema.MigrationJobItemIncomingUsername] = this.RemoteIdentifier;
			}
			message[this.CursorPositionPropertyDefinition] = this.CursorPosition;
			if (this.ProvisioningData != null)
			{
				this.ProvisioningData.WriteToMessageItem(message, loaded);
			}
			if (this.MigrationType == MigrationType.PublicFolder)
			{
				message[MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt] = this.LastFinalizationAttempt;
			}
			if (this.IsPAW)
			{
				IMigrationSerializable migrationSerializable = this.ProvisioningStatistics as IMigrationSerializable;
				if (migrationSerializable != null)
				{
					migrationSerializable.WriteToMessageItem(message, loaded);
				}
			}
			if (this.SubscriptionId != null)
			{
				this.SubscriptionId.WriteToMessageItem(message, loaded);
			}
			if (this.SubscriptionSettings != null)
			{
				this.SubscriptionSettings.WriteToMessageItem(message, loaded);
			}
			base.WriteToMessageItem(message, loaded);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001AA88 File Offset: 0x00018C88
		internal static MigrationJobItem Load(IMigrationDataProvider provider, StoreObjectId messageId, MigrationJobObjectCache jobCache, bool throwOnError = true)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(messageId, "messageId");
			MigrationUtil.ThrowOnNullArgument(jobCache, "jobCache");
			MigrationJobItem result;
			using (IMigrationMessageItem migrationMessageItem = provider.FindMessage(messageId, new StorePropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationType,
				MigrationBatchMessageSchema.MigrationJobId
			}))
			{
				MigrationType valueOrDefault = migrationMessageItem.GetValueOrDefault<MigrationType>(MigrationBatchMessageSchema.MigrationType, MigrationType.None);
				Guid valueOrDefault2 = migrationMessageItem.GetValueOrDefault<Guid>(MigrationBatchMessageSchema.MigrationJobId, Guid.Empty);
				MigrationJobItem migrationJobItem = new MigrationJobItem(valueOrDefault);
				migrationJobItem.MigrationJob = jobCache.GetJob(valueOrDefault2);
				if (!migrationJobItem.TryLoad(provider, messageId))
				{
					if (throwOnError)
					{
						throw new CouldNotLoadMigrationPersistedItemTransientException(messageId.ToHexEntryId());
					}
					result = null;
				}
				else
				{
					result = migrationJobItem;
				}
			}
			return result;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001AB4C File Offset: 0x00018D4C
		internal static MigrationUserStatus? ResolveFlagStatus(MigrationFlags flags)
		{
			MigrationUserStatus? result = null;
			if (flags.HasFlag(MigrationFlags.Remove))
			{
				result = new MigrationUserStatus?(MigrationUserStatus.Removing);
			}
			else if (flags.HasFlag(MigrationFlags.Stop))
			{
				result = new MigrationUserStatus?(MigrationUserStatus.Stopping);
			}
			else if (flags.HasFlag(MigrationFlags.Start))
			{
				result = new MigrationUserStatus?(MigrationUserStatus.Starting);
			}
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001ABBC File Offset: 0x00018DBC
		internal static bool IsFailedStatus(MigrationUserStatus status)
		{
			foreach (MigrationUserStatus migrationUserStatus in MigrationJobItem.FailedStatuses)
			{
				if (status == migrationUserStatus)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001ABEC File Offset: 0x00018DEC
		internal static bool IsStoppedStatus(MigrationUserStatus status)
		{
			foreach (MigrationUserStatus migrationUserStatus in MigrationJobItem.StoppedStatuses)
			{
				if (status == migrationUserStatus)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001ACBC File Offset: 0x00018EBC
		internal static IEnumerable<StoreObjectId> GetIdsByState(IMigrationDataProvider provider, MigrationJob job, MigrationState state, ExDateTime? nextProcessTime = null, int? maxCount = null)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			List<QueryFilter> list = new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, job.JobId),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationState, state)
			};
			List<PropertyDefinition> list2 = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationState
			};
			if (nextProcessTime != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.LessThanOrEqual, MigrationBatchMessageSchema.MigrationNextProcessTime, nextProcessTime));
				list2.Add(MigrationBatchMessageSchema.MigrationNextProcessTime);
			}
			return provider.FindMessageIds(QueryFilter.AndTogether(list.ToArray()), list2.ToArray(), MigrationJobItem.StateSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!object.Equals(row[MigrationBatchMessageSchema.MigrationJobId], job.JobId))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if ((MigrationState)row[MigrationBatchMessageSchema.MigrationState] != state)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (nextProcessTime != null && ExDateTime.Compare((ExDateTime)row[MigrationBatchMessageSchema.MigrationNextProcessTime], nextProcessTime.Value) > 0)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001AEB4 File Offset: 0x000190B4
		internal static IEnumerable<StoreObjectId> GetIdsByFlag(IMigrationDataProvider provider, MigrationJob job, MigrationFlags flag, MigrationState? state = null, int? maxCount = null)
		{
			List<QueryFilter> list = new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, job.JobId),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationFlags, flag)
			};
			List<PropertyDefinition> list2 = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationFlags,
				MigrationBatchMessageSchema.MigrationState
			};
			if (state != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationState, state.Value));
			}
			else
			{
				list.Add(new ComparisonFilter(ComparisonOperator.NotEqual, MigrationBatchMessageSchema.MigrationState, MigrationState.Disabled));
			}
			return provider.FindMessageIds(QueryFilter.AndTogether(list.ToArray()), list2.ToArray(), MigrationJobItem.FlagSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!object.Equals(row[MigrationBatchMessageSchema.MigrationJobId], job.JobId))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!object.Equals((MigrationFlags)row[MigrationBatchMessageSchema.MigrationFlags], flag))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if ((state != null && (MigrationState)row[MigrationBatchMessageSchema.MigrationState] != state.Value) || (state == null && (MigrationState)row[MigrationBatchMessageSchema.MigrationState] == MigrationState.Disabled))
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001B074 File Offset: 0x00019274
		internal static IEnumerable<StoreObjectId> GetIdsWithFlagPresence(IMigrationDataProvider provider, MigrationJob job, bool present, int? maxCount = null)
		{
			List<QueryFilter> list = new List<QueryFilter>
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, job.JobId),
				new ComparisonFilter(present ? ComparisonOperator.NotEqual : ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationFlags, MigrationFlags.None),
				new ComparisonFilter(ComparisonOperator.NotEqual, MigrationBatchMessageSchema.MigrationState, MigrationState.Disabled)
			};
			List<PropertyDefinition> list2 = new List<PropertyDefinition>
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationFlags,
				MigrationBatchMessageSchema.MigrationState
			};
			return provider.FindMessageIds(QueryFilter.AndTogether(list.ToArray()), list2.ToArray(), MigrationJobItem.FlagSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!object.Equals(row[MigrationBatchMessageSchema.MigrationJobId], job.JobId))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				bool flag = (MigrationFlags)row[MigrationBatchMessageSchema.MigrationFlags] != MigrationFlags.None;
				if (present != flag)
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if ((MigrationState)row[MigrationBatchMessageSchema.MigrationState] == MigrationState.Disabled)
				{
					return MigrationRowSelectorResult.RejectRowContinueProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001B1B8 File Offset: 0x000193B8
		internal static IEnumerable<StoreObjectId> GetAllIds(IMigrationDataProvider provider, MigrationJob job, int? maxCount = null)
		{
			QueryFilter[] filters = new ComparisonFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass),
				new ComparisonFilter(ComparisonOperator.Equal, MigrationBatchMessageSchema.MigrationJobId, job.JobId)
			};
			return provider.FindMessageIds(QueryFilter.AndTogether(filters), new StorePropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId
			}, MigrationJobItem.FlagSort, delegate(IDictionary<PropertyDefinition, object> row)
			{
				if (!object.Equals(row[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				if (!object.Equals(row[MigrationBatchMessageSchema.MigrationJobId], job.JobId))
				{
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
				return MigrationRowSelectorResult.AcceptRow;
			}, maxCount);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001B46C File Offset: 0x0001966C
		internal static IEnumerable<MigrationJobItem> GetByFilter(IMigrationDataProvider provider, MigrationEqualityFilter primaryFilter, MigrationEqualityFilter[] secondaryFilters, SortBy[] additionalSorts, MigrationJobObjectCache jobCache, int? maxCount)
		{
			IEnumerable<StoreObjectId> messageIds = MigrationHelper.FindMessageIds(provider, primaryFilter, secondaryFilters, additionalSorts, maxCount);
			if (maxCount != null)
			{
				messageIds = new List<StoreObjectId>(messageIds);
			}
			foreach (StoreObjectId messageId in messageIds)
			{
				MigrationJobItem jobItem = MigrationJobItem.Load(provider, messageId, jobCache, true);
				yield return jobItem;
			}
			yield break;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001B4B0 File Offset: 0x000196B0
		internal static MigrationRowSelectorResult FilterJobItemsByColumnLastUpdated(IDictionary<PropertyDefinition, object> rowData, StorePropertyDefinition columnWithDateInfo, Guid? jobId, ExDateTime? lastCheckedTime, MigrationUserStatus? status)
		{
			if (!StringComparer.InvariantCultureIgnoreCase.Equals(rowData[StoreObjectSchema.ItemClass], MigrationBatchMessageSchema.MigrationJobItemClass))
			{
				return MigrationRowSelectorResult.RejectRowContinueProcessing;
			}
			if (status != null && status.Value != (MigrationUserStatus)rowData[MigrationBatchMessageSchema.MigrationUserStatus])
			{
				return MigrationRowSelectorResult.RejectRowStopProcessing;
			}
			if (jobId != null && !MigrationHelper.IsEqualXsoValues(jobId.Value, rowData[MigrationBatchMessageSchema.MigrationJobId]))
			{
				return MigrationRowSelectorResult.RejectRowContinueProcessing;
			}
			object obj;
			if (lastCheckedTime != null && rowData.TryGetValue(columnWithDateInfo, out obj) && obj is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)obj;
				if (exDateTime >= lastCheckedTime.Value)
				{
					MigrationLogger.Log(MigrationEventType.Information, "the stored time {0} the cutoff time {1}", new object[]
					{
						exDateTime,
						lastCheckedTime.Value
					});
					return MigrationRowSelectorResult.RejectRowStopProcessing;
				}
			}
			return MigrationRowSelectorResult.AcceptRow;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001B764 File Offset: 0x00019964
		internal static IEnumerable<MigrationJobItem> LoadJobItemsWithStatus(IMigrationDataProvider provider, IEnumerable<StoreObjectId> messageIdList, MigrationUserStatus status, MigrationJobObjectCache jobCache)
		{
			foreach (StoreObjectId messageId in new List<StoreObjectId>(messageIdList))
			{
				MigrationJobItem jobItem = MigrationJobItem.Load(provider, messageId, jobCache, true);
				if (jobItem.Status == status)
				{
					yield return jobItem;
				}
			}
			yield break;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001B798 File Offset: 0x00019998
		protected override bool InitializeFromMessageItem(IMigrationStoreObject message)
		{
			if (!base.InitializeFromMessageItem(message))
			{
				return false;
			}
			MigrationType migrationType = (MigrationType)message[MigrationBatchMessageSchema.MigrationType];
			if (this.MigrationType != migrationType)
			{
				throw new MigrationDataCorruptionException(string.Format("job type not set correctly.  expected {0}, found {1}", this.MigrationType, migrationType));
			}
			return true;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001B824 File Offset: 0x00019A24
		private static IEnumerable<MigrationJobItem> GetByColumnLastUpdated(IMigrationDataProvider provider, StorePropertyDefinition columnWithDateInfo, MigrationJob job, ExDateTime? lastCheckedTime, MigrationUserStatus status, int maxCount)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationJobObjectCache migrationJobObjectCache = new MigrationJobObjectCache(provider);
			migrationJobObjectCache.PreSeed(job);
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationUserStatus, status);
			PropertyDefinition[] filterColumns = new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobId,
				columnWithDateInfo,
				StoreObjectSchema.ItemClass
			};
			SortBy[] additionalSorts = new SortBy[]
			{
				new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
				new SortBy(columnWithDateInfo, SortOrder.Ascending)
			};
			IEnumerable<StoreObjectId> messageIdList = provider.FindMessageIds(primaryFilter, filterColumns, additionalSorts, (IDictionary<PropertyDefinition, object> rowData) => MigrationJobItem.FilterJobItemsByColumnLastUpdated(rowData, columnWithDateInfo, new Guid?(job.JobId), lastCheckedTime, new MigrationUserStatus?(status)), new int?(maxCount));
			return MigrationJobItem.LoadJobItemsWithStatus(provider, messageIdList, status, migrationJobObjectCache);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001B924 File Offset: 0x00019B24
		private static StorePropertyDefinition GetCursorPositionProperty(MigrationType migrationType)
		{
			StorePropertyDefinition result;
			if (migrationType == MigrationType.ExchangeOutlookAnywhere)
			{
				result = MigrationBatchMessageSchema.MigrationJobItemExchangeRecipientIndex;
			}
			else
			{
				result = MigrationBatchMessageSchema.MigrationJobItemRowIndex;
			}
			return result;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		private void SetUserMailboxProperties(IMigrationDataProvider provider, MigrationStatusData<MigrationUserStatus> statusData, MailboxData mailboxData, ExDateTime? provisionedTime)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.UpdateMailboxServer: jobitem {0}, status {1}, mailboxdata {2}", new object[]
			{
				this,
				statusData,
				mailboxData
			});
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.MailboxDataUpdateIndex,
				this.MigrationJobSlotPropertyDefinitions
			});
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				if (mailboxData != null)
				{
					mailboxData.WriteToMessageItem(message, true);
				}
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
					this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				}
				if (provisionedTime != null)
				{
					message[MigrationBatchMessageSchema.MigrationProvisionedTime] = provisionedTime.Value;
				}
			});
			if (mailboxData != null)
			{
				this.LocalMailbox = mailboxData;
			}
			if (provisionedTime != null)
			{
				this.ProvisionedTime = provisionedTime;
			}
			if (statusData != null)
			{
				MigrationUserStatus migrationUserStatus = this.Status;
				this.StatusData = statusData;
				if (this.Status != migrationUserStatus)
				{
					this.LogStatusEvent();
				}
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001BAB6 File Offset: 0x00019CB6
		private void UpdateIncrementalSyncFailureCount(MigrationStatusData<MigrationUserStatus> newStatus)
		{
			if (newStatus.Status == MigrationUserStatus.IncrementalFailed)
			{
				this.IncrementalSyncFailures++;
				return;
			}
			if (newStatus.Status != MigrationUserStatus.IncrementalSyncing)
			{
				this.IncrementalSyncFailures = 0;
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001BAE1 File Offset: 0x00019CE1
		private void SaveConsumedSlotInformationToMessage(IMigrationStoreObject message)
		{
			message[MigrationBatchMessageSchema.MigrationJobItemSlotProviderId] = this.MigrationSlotProviderGuid;
			message[MigrationBatchMessageSchema.MigrationJobItemSlotType] = this.ConsumedSlotType;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001BC30 File Offset: 0x00019E30
		private void SetStatusAndSubscriptionLastChecked(IMigrationDataProvider provider, MigrationStatusData<MigrationUserStatus> statusData, ExDateTime? subscriptionLastChecked, ISubscriptionStatistics stats)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			if (statusData == null && subscriptionLastChecked == null && stats == null)
			{
				throw new ArgumentException("One or more of status,subcriptionCheckDate or stats should be specified");
			}
			if (stats != null)
			{
				MigrationUtil.ThrowOnLessThanZeroArgument(stats.NumItemsSynced, "itemsSynced");
				MigrationUtil.ThrowOnLessThanZeroArgument(stats.NumItemsSkipped, "itemsSkipped");
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "MigrationJobItem.SetStatusAndCheckDate: jobitem {0}, status {1}, lastCheck {2}", new object[]
			{
				this,
				statusData,
				subscriptionLastChecked
			});
			if (statusData != null)
			{
				this.UpdateIncrementalSyncFailureCount(statusData);
			}
			PropertyDefinition[] propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
			{
				MigrationJobItem.MigrationJobItemColumnsStatusIndex,
				this.MigrationJobSlotPropertyDefinitions
			});
			this.UpdatePersistedMessage(provider, propertiesToUpdate, delegate(IMigrationMessageItem message)
			{
				if (statusData != null)
				{
					statusData.WriteToMessageItem(message, true);
					this.WriteExtendedPropertiesToMessageItem(message);
					this.CheckAndReleaseSlotAssignmentIfNeeded(statusData.Status, message);
				}
				if (subscriptionLastChecked != null)
				{
					if (this.IncrementalSyncInterval != null)
					{
						ExDateTime exDateTime = subscriptionLastChecked.Value + this.IncrementalSyncInterval.Value;
						MigrationLogger.Log(MigrationEventType.Verbose, "changing subscription last checked from {0} to {1}", new object[]
						{
							subscriptionLastChecked.Value,
							exDateTime
						});
						subscriptionLastChecked = new ExDateTime?(exDateTime);
					}
					message[MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked] = subscriptionLastChecked.Value;
				}
				this.WriteLastFinalizationAttemptData(stats, statusData, message);
				if (stats != null)
				{
					stats.WriteToMessageItem(message, true);
				}
			});
			if (subscriptionLastChecked != null)
			{
				this.SubscriptionLastChecked = subscriptionLastChecked;
			}
			if (stats != null)
			{
				this.SubscriptionStatistics = stats;
			}
			if (statusData != null)
			{
				MigrationUserStatus migrationUserStatus = this.Status;
				this.StatusData = statusData;
				if (this.Status != migrationUserStatus)
				{
					this.LogStatusEvent();
				}
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		private void WriteLastFinalizationAttemptData(ISubscriptionStatistics stats, MigrationStatusData<MigrationUserStatus> statusData, IMigrationMessageItem message)
		{
			if (this.MigrationType != MigrationType.PublicFolder)
			{
				return;
			}
			if (this.MigrationJob.Status == MigrationJobStatus.CompletionInitializing && this.MigrationJob.LastFinalizationAttempt + this.LastFinalizationAttempt == 0 && statusData != null)
			{
				if (statusData.Status == MigrationUserStatus.Synced && stats != null && stats.LastSyncTime >= this.MigrationJob.GetEffectiveFinalizationTime())
				{
					this.LastFinalizationAttempt = this.MigrationJob.LastFinalizationAttempt;
				}
				else if (MigrationJobItem.IsFailedStatus(statusData.Status))
				{
					this.PublicFolderCompletionFailures++;
					this.WriteExtendedPropertiesToMessageItem(message);
					int config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<int>("MigrationPublicFolderCompletionFailureThreshold");
					if (this.PublicFolderCompletionFailures >= config)
					{
						this.LastFinalizationAttempt = this.MigrationJob.LastFinalizationAttempt;
					}
				}
				message[MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt] = this.LastFinalizationAttempt;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		private void Initialize(MigrationJob job, IMigrationDataRow dataRow, IMailboxData mailboxData, MigrationUserStatus status, LocalizedException localizedError, MigrationState? state = null, MigrationWorkflowPosition position = null)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(dataRow, "dataRow");
			bool isPAW = job.IsPAW;
			if (isPAW)
			{
				this.currentSupportedVersion = 4L;
			}
			else
			{
				this.currentSupportedVersion = 3L;
			}
			if (dataRow.MigrationType != this.MigrationType)
			{
				throw new ArgumentException(string.Format("DataRow should be of type {0}, but was {1}", this.MigrationType, dataRow.MigrationType));
			}
			this.Identifier = dataRow.Identifier;
			this.LocalMailboxIdentifier = dataRow.LocalMailboxIdentifier;
			this.JobItemGuid = Guid.NewGuid();
			if (dataRow.SupportsRemoteIdentifier)
			{
				this.RemoteIdentifier = dataRow.RemoteIdentifier;
			}
			this.MigrationJobId = job.JobId;
			this.BatchInputId = job.BatchInputId;
			if (MigrationJobItem.IsFailedStatus(status) || !isPAW)
			{
				this.LastRestartTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			this.StatusData = new MigrationStatusData<MigrationUserStatus>(status, localizedError, MigrationJobItem.StatusDataVersionMap[this.currentSupportedVersion], state);
			this.RecipientType = dataRow.RecipientType;
			if (position != null)
			{
				this.WorkflowPosition = position;
			}
			this.JobName = job.JobName;
			this.IsStaged = job.IsStaged;
			if (mailboxData != null)
			{
				this.LocalMailbox = mailboxData;
			}
			this.CursorPosition = dataRow.CursorPosition;
			this.ProvisioningData = ProvisioningDataStorageBase.CreateFromDataRow(dataRow, isPAW);
			this.SubscriptionSettings = JobItemSubscriptionSettingsBase.CreateFromDataRow(dataRow);
			MigrationPreexistingDataRow migrationPreexistingDataRow = dataRow as MigrationPreexistingDataRow;
			if (migrationPreexistingDataRow != null)
			{
				this.SubscriptionId = migrationPreexistingDataRow.SubscriptionId;
			}
			if (!this.ShouldMigrate)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "setting subscription last check to max date time for recipient type: {0}", new object[]
				{
					this.RecipientType
				});
				this.SubscriptionLastChecked = new ExDateTime?(MigrationJobItem.MaxDateTimeValue);
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001C055 File Offset: 0x0001A255
		private void LogStatusEvent()
		{
			if (MigrationServiceFactory.Instance.ShouldLog)
			{
				MigrationJobItemLog.LogStatusEvent(this);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001C069 File Offset: 0x0001A269
		private void UpdatePersistedMessage(IMigrationDataProvider dataProvider, PropertyDefinition[] propertiesToUpdate, Action<IMigrationMessageItem> updateAction)
		{
			this.UpdatePersistedMessage(dataProvider, propertiesToUpdate, SaveMode.NoConflictResolution, updateAction);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001C078 File Offset: 0x0001A278
		private void UpdatePersistedMessage(IMigrationDataProvider dataProvider, PropertyDefinition[] propertiesToUpdate, SaveMode saveMode, Action<IMigrationMessageItem> updateAction)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			MigrationUtil.ThrowOnNullArgument(updateAction, "updateAction");
			MigrationUtil.ThrowOnNullArgument(propertiesToUpdate, "propertiesToUpdate");
			HashSet<PropertyDefinition> hashSet = new HashSet<PropertyDefinition>(propertiesToUpdate);
			bool flag = hashSet.Contains(MigrationBatchMessageSchema.MigrationUserStatus);
			if (flag && !hashSet.Contains(MigrationBatchMessageSchema.MigrationJobItemSlotProviderId))
			{
				propertiesToUpdate = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
				{
					propertiesToUpdate,
					this.MigrationJobSlotPropertyDefinitions
				});
			}
			hashSet.Clear();
			MigrationUtil.AssertOrThrow(base.StoreObjectId != null, "Should only work on an item that's been persisted", new object[0]);
			using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(dataProvider, propertiesToUpdate))
			{
				migrationMessageItem.OpenAsReadWrite();
				updateAction(migrationMessageItem);
				if (flag)
				{
					MigrationStatusData<MigrationUserStatus> migrationStatusData = MigrationStatusData<MigrationUserStatus>.Create(migrationMessageItem, MigrationJobItem.StatusDataVersionMap[base.Version]);
					MigrationUserStatus migrationUserStatus = migrationStatusData.Status;
					if (migrationUserStatus != this.Status)
					{
						this.CheckAndReleaseSlotAssignmentIfNeeded(migrationUserStatus, migrationMessageItem);
					}
				}
				migrationMessageItem.Save(saveMode);
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001C17C File Offset: 0x0001A37C
		private bool CheckAndReleaseSlotAssignmentIfNeeded(MigrationUserStatus newStatus, IMigrationMessageItem message)
		{
			if (newStatus == MigrationUserStatus.Completing || newStatus == MigrationUserStatus.IncrementalSyncing || newStatus == MigrationUserStatus.Syncing)
			{
				return false;
			}
			if (this.MigrationSlotProviderGuid != Guid.Empty || this.ConsumedSlotType != MigrationSlotType.None)
			{
				this.ConsumedSlotType = MigrationSlotType.None;
				this.MigrationSlotProviderGuid = Guid.Empty;
				this.SaveConsumedSlotInformationToMessage(message);
				return true;
			}
			return false;
		}

		// Token: 0x040001B9 RID: 441
		public const long MigrationJobItemPAWVersion = 4L;

		// Token: 0x040001BA RID: 442
		private const long MigrationJobItemSupportPersistableBaseVersion = 3L;

		// Token: 0x040001BB RID: 443
		private const string BatchInputIdKey = "BatchInputId";

		// Token: 0x040001BC RID: 444
		private const string InitialSyncDurationKey = "InitialSyncDuration";

		// Token: 0x040001BD RID: 445
		private const string IncrementalSyncDurationKey = "IncrementalSyncDuration";

		// Token: 0x040001BE RID: 446
		private const string IncrementalSyncFailuresKey = "IncrementalSyncFailures";

		// Token: 0x040001BF RID: 447
		private const string PublicFolderCompletionFailuresKey = "PublicFolderCompletionFailures";

		// Token: 0x040001C0 RID: 448
		private const string IncrementalSyncIntervalKey = "IncrementalSyncInterval";

		// Token: 0x040001C1 RID: 449
		private const string TroubleshooterNotesKey = "TroubleshooterNotes";

		// Token: 0x040001C2 RID: 450
		internal static readonly MigrationEqualityFilter MessageClassEqualityFilter = new MigrationEqualityFilter(StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass);

		// Token: 0x040001C3 RID: 451
		internal static readonly PropertyDefinition[] MigrationJobItemColumnsIndex = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationJobItemIdentifier,
				MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN,
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
				MigrationBatchMessageSchema.MigrationType,
				MigrationBatchMessageSchema.MigrationJobItemItemsSynced,
				MigrationBatchMessageSchema.MigrationJobItemItemsSkipped,
				MigrationBatchMessageSchema.MigrationJobItemRecipientType,
				MigrationBatchMessageSchema.MigrationDisableTime,
				MigrationBatchMessageSchema.MigrationProvisionedTime,
				MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime,
				MigrationBatchMessageSchema.MigrationJobLastRestartTime,
				MigrationBatchMessageSchema.MigrationJobItemId,
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionSettingsLastUpdatedTime,
				MigrationBatchMessageSchema.MigrationJobItemIncomingUsername,
				MigrationBatchMessageSchema.MigrationFlags,
				MigrationBatchMessageSchema.MigrationNextProcessTime,
				MigrationBatchMessageSchema.MigrationJobItemLocalMailboxIdentifier,
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionQueuedTime,
				MigrationBatchMessageSchema.MigrationJobLastFinalizationAttempt
			},
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition,
			MigrationWorkflowPosition.MigrationWorkflowPositionProperties,
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x040001C4 RID: 452
		internal static readonly MigrationUserStatus[] FailedStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Failed,
			MigrationUserStatus.IncrementalFailed,
			MigrationUserStatus.CompletionFailed
		};

		// Token: 0x040001C5 RID: 453
		internal static readonly MigrationUserStatus[] StoppedStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Stopped,
			MigrationUserStatus.IncrementalStopped
		};

		// Token: 0x040001C6 RID: 454
		internal static readonly MigrationUserStatus[] ErrorStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Failed,
			MigrationUserStatus.IncrementalFailed,
			MigrationUserStatus.CompletionFailed,
			MigrationUserStatus.Corrupted
		};

		// Token: 0x040001C7 RID: 455
		internal static readonly MigrationUserStatus[] PreventPublicFolderCompletionErrorStatuses = new MigrationUserStatus[]
		{
			MigrationUserStatus.Failed,
			MigrationUserStatus.CompletionFailed,
			MigrationUserStatus.Corrupted
		};

		// Token: 0x040001C8 RID: 456
		internal static readonly PropertyDefinition[] MigrationJobItemColumnsTypeIndex = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MigrationBatchMessageSchema.MigrationVersion,
			MigrationBatchMessageSchema.MigrationType
		};

		// Token: 0x040001C9 RID: 457
		internal static readonly PropertyDefinition[] MailboxDataUpdateIndex = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass,
				MigrationBatchMessageSchema.MigrationProvisionedTime
			},
			MailboxData.MailboxDataPropertyDefinition,
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition
		});

		// Token: 0x040001CA RID: 458
		internal static readonly PropertyDefinition[] DisableMigrationProperties = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
				MigrationBatchMessageSchema.MigrationDisableTime
			},
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition
		});

		// Token: 0x040001CB RID: 459
		internal static readonly ComparisonFilter MigrationJobItemMessageClassFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationJobItemClass);

		// Token: 0x040001CC RID: 460
		private static readonly SortBy[] StateSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationState, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationNextProcessTime, SortOrder.Ascending)
		};

		// Token: 0x040001CD RID: 461
		private static readonly SortBy[] FlagSort = new SortBy[]
		{
			new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationJobId, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationFlags, SortOrder.Ascending),
			new SortBy(MigrationBatchMessageSchema.MigrationState, SortOrder.Ascending)
		};

		// Token: 0x040001CE RID: 462
		private static readonly PropertyDefinition[] GroupProperties = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			LegacyExchangeMigrationGroupRecipient.GroupPropertyDefinitions,
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition
		});

		// Token: 0x040001CF RID: 463
		private static readonly ExDateTime MaxDateTimeValue = ExDateTime.MaxValue;

		// Token: 0x040001D0 RID: 464
		private static readonly Dictionary<long, long> StatusDataVersionMap = new Dictionary<long, long>
		{
			{
				3L,
				1L
			},
			{
				4L,
				2L
			}
		};

		// Token: 0x040001D1 RID: 465
		private static readonly PropertyDefinition[] MigrationJobItemSubscriptionStateLastUpdated = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MigrationBatchMessageSchema.MigrationJobId,
			MigrationBatchMessageSchema.MigrationJobItemStateLastUpdated,
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked
		};

		// Token: 0x040001D2 RID: 466
		private static readonly PropertyDefinition[] MigrationJobItemColumnsStatusIndex = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
				MigrationBatchMessageSchema.MigrationJobItemItemsSynced,
				MigrationBatchMessageSchema.MigrationJobItemItemsSkipped,
				MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime
			},
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition,
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x040001D3 RID: 467
		private static readonly PropertyDefinition[] updatePropertyDefinitionsBase = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationJobId,
				MigrationBatchMessageSchema.MigrationJobItemRecipientType
			},
			MigrationStatusData<MigrationUserStatus>.StatusPropertyDefinition
		});

		// Token: 0x040001D4 RID: 468
		private static readonly ConcurrentDictionary<string, PropertyDefinition[]> PropertyDefinitionsHash = new ConcurrentDictionary<string, PropertyDefinition[]>();

		// Token: 0x040001D5 RID: 469
		private static readonly MigrationEqualityFilter[] MigrationJobItemMessageClassFilterCollection = new MigrationEqualityFilter[]
		{
			MigrationJobItem.MessageClassEqualityFilter
		};

		// Token: 0x040001D6 RID: 470
		private MigrationUserStatus status;

		// Token: 0x040001D7 RID: 471
		private MigrationStatusData<MigrationUserStatus> statusData;

		// Token: 0x040001D8 RID: 472
		private Guid migrationJobId;

		// Token: 0x040001D9 RID: 473
		private IMailboxData localMailbox;

		// Token: 0x040001DA RID: 474
		private ExDateTime? subscriptionLastChecked;

		// Token: 0x040001DB RID: 475
		private long currentSupportedVersion;
	}
}
