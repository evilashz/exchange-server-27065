using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration.DataAccessLayer
{
	// Token: 0x02000037 RID: 55
	internal class BasicMigrationSlotProvider
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00009EE6 File Offset: 0x000080E6
		private BasicMigrationSlotProvider(Unlimited<int> maximumConcurrentMigrations, Unlimited<int> maximumConcurrentIncrementalSyncs, Guid associatedGuid)
		{
			this.MaximumConcurrentMigrations = maximumConcurrentMigrations;
			this.MaximumConcurrentIncrementalSyncs = maximumConcurrentIncrementalSyncs;
			this.SlotProviderGuid = associatedGuid;
			this.cachedItemCounts = new MigrationCountCache();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00009F0E File Offset: 0x0000810E
		public static BasicMigrationSlotProvider Unlimited
		{
			get
			{
				return BasicMigrationSlotProvider.UnlimitedSlotProvider;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00009F15 File Offset: 0x00008115
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00009F1D File Offset: 0x0000811D
		public Unlimited<int> MaximumConcurrentMigrations { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009F26 File Offset: 0x00008126
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00009F2E File Offset: 0x0000812E
		public Unlimited<int> MaximumConcurrentIncrementalSyncs { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009F37 File Offset: 0x00008137
		public Unlimited<int> AvailableInitialSeedingSlots
		{
			get
			{
				return this.MaximumConcurrentMigrations - this.ActiveMigrationCount;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00009F4F File Offset: 0x0000814F
		public Unlimited<int> AvailableIncrementalSyncSlots
		{
			get
			{
				return this.MaximumConcurrentIncrementalSyncs - this.ActiveIncrementalSyncCount;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009F67 File Offset: 0x00008167
		public int ActiveMigrationCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedOtherCount("InitialSeeding");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00009F79 File Offset: 0x00008179
		public int ActiveIncrementalSyncCount
		{
			get
			{
				return this.cachedItemCounts.GetCachedOtherCount("IncrementaSync");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009F8B File Offset: 0x0000818B
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00009F93 File Offset: 0x00008193
		public Guid SlotProviderGuid { get; private set; }

		// Token: 0x0600022B RID: 555 RVA: 0x00009F9C File Offset: 0x0000819C
		public static BasicMigrationSlotProvider Get(Guid ownerId, Unlimited<int> maximumConcurrentMigrations, Unlimited<int> maximumConcurrentIncrementalSyncs)
		{
			BasicMigrationSlotProvider basicMigrationSlotProvider = new BasicMigrationSlotProvider(maximumConcurrentMigrations, maximumConcurrentIncrementalSyncs, ownerId);
			MigrationLogger.Log(MigrationEventType.Verbose, "Getting new slot provider for owner {0}, {1}/{2} initial seeding slots, {3}/{4} incremental sync slots.", new object[]
			{
				ownerId,
				basicMigrationSlotProvider.AvailableInitialSeedingSlots,
				basicMigrationSlotProvider.MaximumConcurrentMigrations,
				basicMigrationSlotProvider.AvailableIncrementalSyncSlots,
				basicMigrationSlotProvider.MaximumConcurrentIncrementalSyncs
			});
			return basicMigrationSlotProvider;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A008 File Offset: 0x00008208
		public static BasicMigrationSlotProvider FromMessageItem(Guid ownerId, IMigrationStoreObject message)
		{
			BasicMigrationSlotProvider basicMigrationSlotProvider = new BasicMigrationSlotProvider(0, 0, ownerId);
			basicMigrationSlotProvider.ReadFromMessageItem(message);
			return basicMigrationSlotProvider;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A030 File Offset: 0x00008230
		public BasicMigrationSlotProvider.SlotAcquisitionGuard AcquireSlot(MigrationJobItem jobItem, MigrationSlotType slotType, IMigrationDataProvider dataProvider)
		{
			if (jobItem.ConsumedSlotType == slotType)
			{
				return new BasicMigrationSlotProvider.SlotAcquisitionGuard();
			}
			this.UpdateAllocationCounts(dataProvider);
			MigrationLogger.Log(MigrationEventType.Verbose, "Attempting to acquire a slot of type {0} from {1}. Job item is currently using a slot of type {2} from {3}.", new object[]
			{
				slotType,
				this.SlotProviderGuid,
				jobItem.ConsumedSlotType,
				jobItem.MigrationSlotProviderGuid
			});
			Unlimited<int> maximumCapacity;
			string slotCountKey;
			switch (slotType)
			{
			case MigrationSlotType.InitialSeeding:
				maximumCapacity = this.MaximumConcurrentMigrations;
				slotCountKey = "InitialSeeding";
				break;
			case MigrationSlotType.IncrementalSync:
				maximumCapacity = this.MaximumConcurrentIncrementalSyncs;
				slotCountKey = "IncrementaSync";
				break;
			default:
				return new BasicMigrationSlotProvider.SlotAcquisitionGuard();
			}
			this.AcquireSlotCapacity(slotCountKey, maximumCapacity, 1);
			return new BasicMigrationSlotProvider.SlotAcquisitionGuard(this, dataProvider, jobItem, new BasicMigrationSlotProvider.SlotInformation(this.SlotProviderGuid, slotType));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public void ReleaseSlot(MigrationJobItem jobItem, IMigrationDataProvider dataProvider)
		{
			this.UpdateAllocationCounts(dataProvider);
			MigrationSlotType consumedSlotType = jobItem.ConsumedSlotType;
			jobItem.UpdateConsumedSlot(Guid.Empty, MigrationSlotType.None, dataProvider);
			this.ReleaseSlot(consumedSlotType);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A120 File Offset: 0x00008320
		public void ReleaseSlot(MigrationSlotType slotType)
		{
			switch (slotType)
			{
			case MigrationSlotType.None:
				return;
			case MigrationSlotType.InitialSeeding:
				this.ReleaseInitialSeedingSlot();
				break;
			case MigrationSlotType.IncrementalSync:
				this.ReleaseIncrementalSyncSlot();
				break;
			default:
				return;
			}
			MigrationLogger.Log(MigrationEventType.Verbose, "Released {0} slot consumed by job item.", new object[]
			{
				slotType
			});
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A170 File Offset: 0x00008370
		public void WriteToMessageItem(IMigrationStoreObject message)
		{
			message[MigrationBatchMessageSchema.MigrationSlotMaximumInitialSeedings] = this.MaximumConcurrentMigrations.ToString();
			message[MigrationBatchMessageSchema.MigrationSlotMaximumIncrementalSeedings] = this.MaximumConcurrentIncrementalSyncs.ToString();
			this.WriteCachedCountsToMessageItem(message);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A1C2 File Offset: 0x000083C2
		public void WriteCachedCountsToMessageItem(IMigrationStoreObject message)
		{
			message[MigrationBatchMessageSchema.MigrationJobCountCache] = this.cachedItemCounts.Serialize();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A1DC File Offset: 0x000083DC
		public void ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.MaximumConcurrentMigrations = (MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationSlotMaximumInitialSeedings) ?? Unlimited<int>.UnlimitedValue);
			this.MaximumConcurrentIncrementalSyncs = (MigrationHelper.ReadUnlimitedProperty(message, MigrationBatchMessageSchema.MigrationSlotMaximumIncrementalSeedings) ?? Unlimited<int>.UnlimitedValue);
			string text = (string)message[MigrationBatchMessageSchema.MigrationJobCountCache];
			if (!string.IsNullOrEmpty(text))
			{
				this.cachedItemCounts = MigrationCountCache.Deserialize(text);
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A260 File Offset: 0x00008460
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("SlotProvider");
			xelement.Add(new XElement("MaximumConcurrentMigrations", this.MaximumConcurrentMigrations));
			xelement.Add(new XElement("MaximumConcurrentIncrementalSyncs", this.MaximumConcurrentIncrementalSyncs));
			xelement.Add(this.cachedItemCounts.GetDiagnosticInfo(dataProvider, argument));
			return xelement;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A39C File Offset: 0x0000859C
		internal void UpdateAllocationCounts(IMigrationDataProvider dataProvider)
		{
			TimeSpan config = ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("EndpointCountsRefreshThreshold");
			ExDateTime exDateTime = this.cachedItemCounts.GetCachedTimestamp("AllocationsLastUpdatedOn") ?? ExDateTime.MinValue;
			if (exDateTime + config > ExDateTime.UtcNow)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Allocation counts for {0} are only {1} old, not refreshing since our threshold is {2}.", new object[]
				{
					this.SlotProviderGuid,
					ExDateTime.UtcNow - exDateTime,
					config
				});
				return;
			}
			MigrationUtil.RunTimedOperation(delegate()
			{
				int count = MigrationJobItem.GetCount(dataProvider, this.SlotProviderGuid, MigrationSlotType.IncrementalSync);
				int count2 = MigrationJobItem.GetCount(dataProvider, this.SlotProviderGuid, MigrationSlotType.InitialSeeding);
				MigrationLogger.Log(MigrationEventType.Verbose, "Updated allocation counts for {0}, {1} items in initial seeding and {2} items in incremental syncs.", new object[]
				{
					this.SlotProviderGuid,
					count2,
					count
				});
				this.cachedItemCounts.SetCachedOtherCount("InitialSeeding", count2);
				this.cachedItemCounts.SetCachedOtherCount("IncrementaSync", count);
				this.cachedItemCounts.SetCachedTimestamp("AllocationsLastUpdatedOn", new ExDateTime?(ExDateTime.UtcNow));
			}, string.Format("EndpointId={0}", this.SlotProviderGuid));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A470 File Offset: 0x00008670
		private void ReleaseInitialSeedingSlot()
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "Releasing initial seeding slot from provider {0}. Current usage: {1}/{2}", new object[]
			{
				this.SlotProviderGuid,
				this.ActiveMigrationCount,
				this.MaximumConcurrentMigrations
			});
			this.AcquireSlotCapacity("InitialSeeding", this.MaximumConcurrentMigrations, -1);
			if (this.AvailableInitialSeedingSlots > this.MaximumConcurrentMigrations)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Released too many initial seeding slots {0}/{1}.", new object[]
				{
					this.AvailableInitialSeedingSlots,
					this.MaximumConcurrentMigrations
				});
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A510 File Offset: 0x00008710
		private void ReleaseIncrementalSyncSlot()
		{
			MigrationLogger.Log(MigrationEventType.Verbose, "Releasing incremental sync slot from provider {0}. Current usage: {1}/{2}", new object[]
			{
				this.SlotProviderGuid,
				this.ActiveIncrementalSyncCount,
				this.MaximumConcurrentIncrementalSyncs
			});
			this.AcquireSlotCapacity("IncrementaSync", this.MaximumConcurrentIncrementalSyncs, -1);
			if (this.AvailableIncrementalSyncSlots > this.MaximumConcurrentIncrementalSyncs)
			{
				MigrationLogger.Log(MigrationEventType.Verbose, "Released too many incremental sync slots {0}/{1}.", new object[]
				{
					this.AvailableIncrementalSyncSlots,
					this.MaximumConcurrentIncrementalSyncs
				});
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A5B0 File Offset: 0x000087B0
		private void AcquireSlotCapacity(string slotCountKey, Unlimited<int> maximumCapacity, int requestedCount)
		{
			int num = this.cachedItemCounts.GetCachedOtherCount(slotCountKey);
			if (requestedCount > 0 && !maximumCapacity.IsUnlimited && num + requestedCount > maximumCapacity)
			{
				throw new MigrationSlotCapacityExceededException(maximumCapacity - num, requestedCount);
			}
			num += requestedCount;
			this.cachedItemCounts.SetCachedOtherCount(slotCountKey, Math.Max(0, num));
		}

		// Token: 0x040000C3 RID: 195
		public const string CacheUpdateKey = "AllocationsLastUpdatedOn";

		// Token: 0x040000C4 RID: 196
		public const string IncrementalSyncCountKey = "IncrementaSync";

		// Token: 0x040000C5 RID: 197
		public const string InitialSeedingCountKey = "InitialSeeding";

		// Token: 0x040000C6 RID: 198
		public static readonly PropertyDefinition[] ConcurrencyPropertyDefinition = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationSlotMaximumInitialSeedings,
			MigrationBatchMessageSchema.MigrationSlotMaximumIncrementalSeedings
		};

		// Token: 0x040000C7 RID: 199
		public static readonly StorePropertyDefinition[] CachedCountsPropertyDefinition = new StorePropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationJobCountCache
		};

		// Token: 0x040000C8 RID: 200
		public static readonly PropertyDefinition[] PropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			BasicMigrationSlotProvider.ConcurrencyPropertyDefinition,
			BasicMigrationSlotProvider.CachedCountsPropertyDefinition
		});

		// Token: 0x040000C9 RID: 201
		private static readonly Guid UnlimitedSlotProviderId = Guid.Parse("e1489fce-b9f5-43c9-9981-a36cdde44843");

		// Token: 0x040000CA RID: 202
		private static readonly BasicMigrationSlotProvider UnlimitedSlotProvider = new BasicMigrationSlotProvider(Unlimited<int>.UnlimitedValue, Unlimited<int>.UnlimitedValue, BasicMigrationSlotProvider.UnlimitedSlotProviderId);

		// Token: 0x040000CB RID: 203
		private MigrationCountCache cachedItemCounts;

		// Token: 0x02000038 RID: 56
		internal class SlotAcquisitionGuard : DisposeTrackableBase
		{
			// Token: 0x06000239 RID: 569 RVA: 0x0000A6A0 File Offset: 0x000088A0
			internal SlotAcquisitionGuard(BasicMigrationSlotProvider slotProvider, IMigrationDataProvider dataProvider, MigrationJobItem jobItem, BasicMigrationSlotProvider.SlotInformation slotInformation)
			{
				MigrationUtil.ThrowOnNullArgument(slotProvider, "slotProvider");
				MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
				MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
				this.slotProvider = slotProvider;
				this.jobItem = jobItem;
				this.dataProvider = dataProvider;
				this.slotInformation = slotInformation;
				this.success = false;
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x0600023A RID: 570 RVA: 0x0000A6F8 File Offset: 0x000088F8
			public BasicMigrationSlotProvider.SlotInformation SlotInformation
			{
				get
				{
					return this.slotInformation;
				}
			}

			// Token: 0x0600023B RID: 571 RVA: 0x0000A700 File Offset: 0x00008900
			internal SlotAcquisitionGuard()
			{
				this.success = true;
			}

			// Token: 0x0600023C RID: 572 RVA: 0x0000A70F File Offset: 0x0000890F
			public void Success()
			{
				if (!this.success)
				{
					this.jobItem.UpdateConsumedSlot(this.slotInformation.SlotProviderGuid, this.slotInformation.SlotType, this.dataProvider);
				}
				this.success = true;
			}

			// Token: 0x0600023D RID: 573 RVA: 0x0000A747 File Offset: 0x00008947
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<BasicMigrationSlotProvider.SlotAcquisitionGuard>(this);
			}

			// Token: 0x0600023E RID: 574 RVA: 0x0000A750 File Offset: 0x00008950
			protected override void InternalDispose(bool disposing)
			{
				if (!disposing || this.success || this.dataProvider == null)
				{
					return;
				}
				MigrationLogger.Log(MigrationEventType.Warning, "Did not received a success signal while protecting the slot acquisition for job item {0} (type={1}), releasing slot.", new object[]
				{
					this.jobItem,
					this.jobItem.ConsumedSlotType
				});
				this.slotProvider.ReleaseSlot(this.jobItem, this.dataProvider);
			}

			// Token: 0x040000CF RID: 207
			private readonly BasicMigrationSlotProvider slotProvider;

			// Token: 0x040000D0 RID: 208
			private readonly MigrationJobItem jobItem;

			// Token: 0x040000D1 RID: 209
			private readonly IMigrationDataProvider dataProvider;

			// Token: 0x040000D2 RID: 210
			private readonly BasicMigrationSlotProvider.SlotInformation slotInformation;

			// Token: 0x040000D3 RID: 211
			private bool success;
		}

		// Token: 0x02000039 RID: 57
		internal class SlotInformation
		{
			// Token: 0x0600023F RID: 575 RVA: 0x0000A7B7 File Offset: 0x000089B7
			public SlotInformation(Guid slotProviderGuid, MigrationSlotType slotType)
			{
				this.SlotProviderGuid = slotProviderGuid;
				this.SlotType = slotType;
			}

			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x06000240 RID: 576 RVA: 0x0000A7CD File Offset: 0x000089CD
			// (set) Token: 0x06000241 RID: 577 RVA: 0x0000A7D5 File Offset: 0x000089D5
			public Guid SlotProviderGuid { get; private set; }

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A7DE File Offset: 0x000089DE
			// (set) Token: 0x06000243 RID: 579 RVA: 0x0000A7E6 File Offset: 0x000089E6
			public MigrationSlotType SlotType { get; private set; }
		}
	}
}
