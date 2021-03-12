using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationCacheEntry : MigrationMessagePersistableBase
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		private MigrationCacheEntry(Guid mdbGuid)
		{
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			this.MdbGuid = mdbGuid;
			this.organizationId = new Lazy<ADObjectId>(() => this.TenantPartitionHint.GetTenantScopedADSessionSettingsServiceOnly().CurrentOrganizationId.OrganizationalUnit, LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000E848 File Offset: 0x0000CA48
		private MigrationCacheEntry(string migrationMailboxLegacyDN, Guid mdbGuid, TenantPartitionHint tenantPartitionHint)
		{
			MigrationUtil.ThrowOnNullOrEmptyArgument(migrationMailboxLegacyDN, "migrationMailboxLegacyDN");
			MigrationUtil.ThrowOnNullArgument(tenantPartitionHint, "organizationId");
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			this.MigrationMailboxLegacyDN = migrationMailboxLegacyDN;
			this.MdbGuid = mdbGuid;
			this.TenantPartitionHint = tenantPartitionHint;
			this.LastUpdated = ExDateTime.UtcNow;
			this.NextProcessTime = null;
			this.organizationId = new Lazy<ADObjectId>(() => this.TenantPartitionHint.GetTenantScopedADSessionSettingsServiceOnly().CurrentOrganizationId.OrganizationalUnit, LazyThreadSafetyMode.PublicationOnly);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000E8CA File Offset: 0x0000CACA
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000E8D2 File Offset: 0x0000CAD2
		public string MigrationMailboxLegacyDN
		{
			get
			{
				return this.migrationMailboxLegacyDN;
			}
			private set
			{
				this.migrationMailboxLegacyDN = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E8DB File Offset: 0x0000CADB
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000E8E3 File Offset: 0x0000CAE3
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
			private set
			{
				this.mdbGuid = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		public TenantPartitionHint TenantPartitionHint { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000E900 File Offset: 0x0000CB00
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000E926 File Offset: 0x0000CB26
		public ExDateTime? NextProcessTime
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime?>("NextProcessTime", null);
			}
			private set
			{
				base.ExtendedProperties.Set<ExDateTime?>("NextProcessTime", value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000E939 File Offset: 0x0000CB39
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000E941 File Offset: 0x0000CB41
		public ExDateTime LastUpdated
		{
			get
			{
				return this.lastUpdated;
			}
			private set
			{
				this.lastUpdated = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000E94A File Offset: 0x0000CB4A
		public override long MaximumSupportedVersion
		{
			get
			{
				return 1L;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000E94E File Offset: 0x0000CB4E
		public override long MinimumSupportedVersion
		{
			get
			{
				return 1L;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000E952 File Offset: 0x0000CB52
		public override long CurrentSupportedVersion
		{
			get
			{
				return 1L;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E956 File Offset: 0x0000CB56
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E96D File Offset: 0x0000CB6D
		public ExDateTime LastChecked
		{
			get
			{
				return base.ExtendedProperties.Get<ExDateTime>("LastChecked", ExDateTime.MinValue);
			}
			private set
			{
				base.ExtendedProperties.Set<ExDateTime>("LastChecked", value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E980 File Offset: 0x0000CB80
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E993 File Offset: 0x0000CB93
		public MigrationProcessorResult LastProcessorResult
		{
			get
			{
				return base.ExtendedProperties.Get<MigrationProcessorResult>("LastProcessorResult", MigrationProcessorResult.Deleted);
			}
			private set
			{
				base.ExtendedProperties.Set<MigrationProcessorResult>("LastProcessorResult", value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		public override PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MigrationCacheEntry.MessagePropertyDefinition;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000E9AD File Offset: 0x0000CBAD
		public new ADObjectId OrganizationId
		{
			get
			{
				return this.organizationId.Value;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public static MigrationCacheEntry Create(IMigrationDataProvider provider, string mailboxLegacyDN, Guid mdbGuid, TenantPartitionHint tenantPartitionHint)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(mailboxLegacyDN, "mailboxLegacyDN");
			MigrationUtil.ThrowOnNullArgument(tenantPartitionHint, "tenantPartitionHint");
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			MigrationCacheEntry.DeleteByLegacyDN(provider, mailboxLegacyDN);
			MigrationCacheEntry migrationCacheEntry = new MigrationCacheEntry(mailboxLegacyDN, mdbGuid, tenantPartitionHint);
			migrationCacheEntry.CreateInStore(provider, null);
			return migrationCacheEntry;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000EC48 File Offset: 0x0000CE48
		public static IEnumerable<MigrationCacheEntry> GetMigrationCacheEntries(IMigrationDataProvider provider, Guid mdbGuid)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnGuidEmptyArgument(mdbGuid, "mdbGuid");
			MigrationEqualityFilter primaryFilter = MigrationCacheEntry.MessageClassEqualityFilter;
			IEnumerable<StoreObjectId> messageIds = MigrationHelper.FindMessageIds(provider, primaryFilter, null, null, null);
			foreach (StoreObjectId messageId in messageIds)
			{
				MigrationCacheEntry entry = new MigrationCacheEntry(mdbGuid);
				if (!entry.TryLoad(provider, messageId))
				{
					MigrationLogger.Log(MigrationEventType.Warning, "skipping over cache entry with message id {0}", new object[]
					{
						messageId
					});
				}
				else
				{
					yield return entry;
				}
			}
			yield break;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public void Suspend()
		{
			this.UpdateFromLastRun(MigrationProcessorResult.Suspended, new TimeSpan?(ConfigBase<MigrationServiceConfigSchema>.GetConfig<TimeSpan>("CacheEntrySuspendedDuration")), true);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000EC88 File Offset: 0x0000CE88
		public void UpdateFromLastRun(MigrationProcessorResult result, TimeSpan? delayInterval, bool persist)
		{
			this.LastProcessorResult = result;
			this.LastChecked = ExDateTime.UtcNow;
			if (delayInterval != null)
			{
				MigrationLogger.Log(MigrationEventType.Information, "CacheEntry setting delay interval to {0} for {1} due to processing result {2}", new object[]
				{
					delayInterval.Value,
					this,
					result
				});
				this.NextProcessTime = new ExDateTime?(this.LastChecked + delayInterval.Value);
			}
			else
			{
				this.NextProcessTime = null;
			}
			if (!persist)
			{
				return;
			}
			using (IMigrationDataProvider migrationDataProvider = MigrationServiceFactory.Instance.CreateProviderForSystemMailbox(this.MdbGuid))
			{
				using (IMigrationMessageItem migrationMessageItem = base.FindMessageItem(migrationDataProvider, MigrationPersistableBase.MigrationBaseDefinitions))
				{
					migrationMessageItem.OpenAsReadWrite();
					this.WriteExtendedPropertiesToMessageItem(migrationMessageItem);
					migrationMessageItem.Save(SaveMode.NoConflictResolution);
				}
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public void Delete(IMigrationDataProvider provider)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			if (base.StoreObjectId != null)
			{
				provider.RemoveMessage(base.StoreObjectId);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000ED99 File Offset: 0x0000CF99
		public string GetDiagnosticComponentName()
		{
			return "MigrationCacheEntry";
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public override XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			if (this.TenantPartitionHint != null)
			{
				xelement.Add(new XElement("tenantPartitionHint", this.TenantPartitionHint));
			}
			xelement.Add(new object[]
			{
				new XElement("lastUpdated", this.LastUpdated),
				new XElement("mailboxLegacyDN", this.MigrationMailboxLegacyDN),
				new XElement("messageId", base.StoreObjectId),
				new XElement("databaseGuid", this.MdbGuid)
			});
			return base.GetDiagnosticInfo(dataProvider, argument, xelement);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000EE62 File Offset: 0x0000D062
		public override string ToString()
		{
			return this.MigrationMailboxLegacyDN;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000EE6C File Offset: 0x0000D06C
		public override bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			MigrationUtil.ThrowOnNullArgument(message, "message");
			if (!base.ReadFromMessageItem(message))
			{
				return false;
			}
			this.MigrationMailboxLegacyDN = (string)message[MigrationBatchMessageSchema.MigrationCacheEntryMailboxLegacyDN];
			byte[] valueOrDefault = message.GetValueOrDefault<byte[]>(MigrationBatchMessageSchema.MigrationCacheEntryTenantPartitionHint, null);
			if (valueOrDefault == null || valueOrDefault.Length <= 0)
			{
				MigrationLogger.Log(MigrationEventType.Error, "Skipping cache entry {0} with leg dn {1} because no partition hint", new object[]
				{
					message.Id,
					this.MigrationMailboxLegacyDN
				});
				return false;
			}
			this.TenantPartitionHint = TenantPartitionHint.FromPersistablePartitionHint(valueOrDefault);
			this.LastUpdated = MigrationHelper.GetExDateTimePropertyOrDefault(message, MigrationBatchMessageSchema.MigrationCacheEntryLastUpdated, message.CreationTime);
			return true;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000EF08 File Offset: 0x0000D108
		public override void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[StoreObjectSchema.ItemClass] = MigrationBatchMessageSchema.MigrationCacheEntryClass;
			message[MigrationBatchMessageSchema.MigrationCacheEntryMailboxLegacyDN] = this.MigrationMailboxLegacyDN;
			message[MigrationBatchMessageSchema.MigrationCacheEntryTenantPartitionHint] = this.TenantPartitionHint.GetPersistablePartitionHint();
			MigrationHelperBase.SetExDateTimeProperty(message, MigrationBatchMessageSchema.MigrationCacheEntryLastUpdated, new ExDateTime?(this.LastUpdated));
			base.WriteToMessageItem(message, loaded);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000EF6C File Offset: 0x0000D16C
		private static void DeleteByLegacyDN(IMigrationDataProvider provider, string mailboxLegacyDN)
		{
			MigrationUtil.ThrowOnNullArgument(provider, "provider");
			MigrationUtil.ThrowOnNullOrEmptyArgument(mailboxLegacyDN, "mailboxLegacyDN");
			MigrationEqualityFilter primaryFilter = new MigrationEqualityFilter(MigrationBatchMessageSchema.MigrationCacheEntryMailboxLegacyDN, mailboxLegacyDN);
			IEnumerable<StoreObjectId> collection = MigrationHelper.FindMessageIds(provider, primaryFilter, new MigrationEqualityFilter[]
			{
				MigrationCacheEntry.MessageClassEqualityFilter
			}, null, null);
			List<StoreObjectId> list = new List<StoreObjectId>(collection);
			foreach (StoreObjectId messageId in list)
			{
				provider.RemoveMessage(messageId);
			}
		}

		// Token: 0x04000118 RID: 280
		private const string LastCheckedKey = "LastChecked";

		// Token: 0x04000119 RID: 281
		private const string NextProcessTimeKey = "NextProcessTime";

		// Token: 0x0400011A RID: 282
		private const string LastProcessorResultKey = "LastProcessorResult";

		// Token: 0x0400011B RID: 283
		private const long MigrationCacheEntryCurrentSupportedVersion = 1L;

		// Token: 0x0400011C RID: 284
		private static readonly PropertyDefinition[] MessagePropertyDefinition = MigrationHelper.AggregateProperties(new IList<PropertyDefinition>[]
		{
			new PropertyDefinition[]
			{
				MigrationBatchMessageSchema.MigrationCacheEntryMailboxLegacyDN,
				MigrationBatchMessageSchema.MigrationCacheEntryTenantPartitionHint,
				MigrationBatchMessageSchema.MigrationCacheEntryLastUpdated
			},
			MigrationPersistableBase.MigrationBaseDefinitions
		});

		// Token: 0x0400011D RID: 285
		private static readonly MigrationEqualityFilter MessageClassEqualityFilter = new MigrationEqualityFilter(StoreObjectSchema.ItemClass, MigrationBatchMessageSchema.MigrationCacheEntryClass);

		// Token: 0x0400011E RID: 286
		private readonly Lazy<ADObjectId> organizationId;

		// Token: 0x0400011F RID: 287
		private string migrationMailboxLegacyDN;

		// Token: 0x04000120 RID: 288
		private Guid mdbGuid;

		// Token: 0x04000121 RID: 289
		private ExDateTime lastUpdated;
	}
}
