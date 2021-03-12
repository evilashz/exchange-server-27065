using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A7 RID: 1191
	[ObjectScope(ConfigScopes.Database)]
	[Serializable]
	public sealed class MailboxDatabase : Database, IProvisioningCacheInvalidation
	{
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600365E RID: 13918 RVA: 0x000D5B4C File Offset: 0x000D3D4C
		internal override ADObjectSchema Schema
		{
			get
			{
				return MailboxDatabase.schema;
			}
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x000D5B53 File Offset: 0x000D3D53
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MailboxDatabase.MostDerivedClass;
			}
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003660 RID: 13920 RVA: 0x000D5B5A File Offset: 0x000D3D5A
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000D5B70 File Offset: 0x000D3D70
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				DatabaseSchema.IssueWarningQuota,
				MailboxDatabaseSchema.ProhibitSendQuota,
				MailboxDatabaseSchema.ProhibitSendReceiveQuota
			}, this.Identity));
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				MailboxDatabaseSchema.RecoverableItemsWarningQuota,
				MailboxDatabaseSchema.RecoverableItemsQuota
			}, this.Identity));
			errors.AddRange(Database.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				MailboxDatabaseSchema.CalendarLoggingQuota,
				MailboxDatabaseSchema.RecoverableItemsQuota
			}, this.Identity));
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000D5C1C File Offset: 0x000D3E1C
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(MailboxDatabaseSchema.ProhibitSendReceiveQuota))
			{
				this.ProhibitSendReceiveQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromMB(2355UL));
			}
			if (!base.IsModified(MailboxDatabaseSchema.ProhibitSendQuota))
			{
				this.ProhibitSendQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(2UL));
			}
			if (!base.IsModified(MailboxDatabaseSchema.RecoverableItemsWarningQuota))
			{
				this.RecoverableItemsWarningQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(20UL));
			}
			if (!base.IsModified(MailboxDatabaseSchema.RecoverableItemsQuota))
			{
				this.RecoverableItemsQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(30UL));
			}
			if (!base.IsModified(MailboxDatabaseSchema.CalendarLoggingQuota))
			{
				this.CalendarLoggingQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(6UL));
			}
			this.ReservedFlag = true;
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000D5CD8 File Offset: 0x000D3ED8
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.ObjectState == ObjectState.New || base.ObjectState == ObjectState.Deleted)
			{
				keys = new Guid[]
				{
					CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnLocalSite,
					CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites,
					CannedProvisioningCacheKeys.MailboxDatabaseForDefaultRetentionValuesCacheKey
				};
			}
			else if (base.ObjectState == ObjectState.Changed)
			{
				List<Guid> list = new List<Guid>();
				if (base.IsChanged(MailboxDatabaseSchema.IsExcludedFromProvisioning) || base.IsChanged(MailboxDatabaseSchema.IsSuspendedFromProvisioning) || base.IsChanged(DatabaseSchema.Recovery))
				{
					list.Add(CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnLocalSite);
					list.Add(CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingGlobalData, CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites.ToString(), new object[]
					{
						CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites.ToString()
					});
				}
				else if (base.IsChanged(MailboxDatabaseSchema.IsExcludedFromInitialProvisioning))
				{
					list.Add(CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites);
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCResettingGlobalData, CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites.ToString(), new object[]
					{
						CannedProvisioningCacheKeys.ProvisioningEnabledDatabasesOnAllSites.ToString()
					});
				}
				if (base.IsChanged(DatabaseSchema.DeletedItemRetention) || base.IsChanged(DatabaseSchema.RetainDeletedItemsUntilBackup))
				{
					list.Add(CannedProvisioningCacheKeys.MailboxDatabaseForDefaultRetentionValuesCacheKey);
				}
				if (list.Count > 0)
				{
					keys = list.ToArray();
				}
			}
			return keys != null;
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000D5E63 File Offset: 0x000D4063
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x000D5E6D File Offset: 0x000D406D
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x000D5E7F File Offset: 0x000D407F
		public ADObjectId JournalRecipient
		{
			get
			{
				return (ADObjectId)this[MailboxDatabaseSchema.JournalRecipient];
			}
			set
			{
				this[MailboxDatabaseSchema.JournalRecipient] = value;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x000D5E8D File Offset: 0x000D408D
		// (set) Token: 0x06003668 RID: 13928 RVA: 0x000D5E9F File Offset: 0x000D409F
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxRetention
		{
			get
			{
				return (EnhancedTimeSpan)this[MailboxDatabaseSchema.MailboxRetention];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxRetention] = value;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x000D5EB2 File Offset: 0x000D40B2
		// (set) Token: 0x0600366A RID: 13930 RVA: 0x000D5EC4 File Offset: 0x000D40C4
		public ADObjectId OfflineAddressBook
		{
			get
			{
				return (ADObjectId)this[MailboxDatabaseSchema.OfflineAddressBook];
			}
			set
			{
				this[MailboxDatabaseSchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x000D5ED2 File Offset: 0x000D40D2
		// (set) Token: 0x0600366C RID: 13932 RVA: 0x000D5EE4 File Offset: 0x000D40E4
		public ADObjectId OriginalDatabase
		{
			get
			{
				return (ADObjectId)this[MailboxDatabaseSchema.OriginalDatabase];
			}
			internal set
			{
				this[MailboxDatabaseSchema.OriginalDatabase] = value;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x000D5EF2 File Offset: 0x000D40F2
		// (set) Token: 0x0600366E RID: 13934 RVA: 0x000D5F04 File Offset: 0x000D4104
		public ADObjectId PublicFolderDatabase
		{
			get
			{
				return (ADObjectId)this[MailboxDatabaseSchema.PublicFolderDatabase];
			}
			set
			{
				this[MailboxDatabaseSchema.PublicFolderDatabase] = value;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000D5F12 File Offset: 0x000D4112
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x000D5F24 File Offset: 0x000D4124
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendReceiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxDatabaseSchema.ProhibitSendReceiveQuota];
			}
			set
			{
				this[MailboxDatabaseSchema.ProhibitSendReceiveQuota] = value;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000D5F37 File Offset: 0x000D4137
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x000D5F49 File Offset: 0x000D4149
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitSendQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxDatabaseSchema.ProhibitSendQuota];
			}
			set
			{
				this[MailboxDatabaseSchema.ProhibitSendQuota] = value;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000D5F5C File Offset: 0x000D415C
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x000D5F6E File Offset: 0x000D416E
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxDatabaseSchema.RecoverableItemsQuota];
			}
			set
			{
				this[MailboxDatabaseSchema.RecoverableItemsQuota] = value;
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x000D5F81 File Offset: 0x000D4181
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x000D5F93 File Offset: 0x000D4193
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxDatabaseSchema.RecoverableItemsWarningQuota];
			}
			set
			{
				this[MailboxDatabaseSchema.RecoverableItemsWarningQuota] = value;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x000D5FA6 File Offset: 0x000D41A6
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x000D5FB8 File Offset: 0x000D41B8
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> CalendarLoggingQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailboxDatabaseSchema.CalendarLoggingQuota];
			}
			set
			{
				this[MailboxDatabaseSchema.CalendarLoggingQuota] = value;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000D5FCB File Offset: 0x000D41CB
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x000D5FDD File Offset: 0x000D41DD
		[Parameter(Mandatory = false)]
		public bool IndexEnabled
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IndexEnabled];
			}
			set
			{
				this[MailboxDatabaseSchema.IndexEnabled] = value;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000D5FF0 File Offset: 0x000D41F0
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x000D6002 File Offset: 0x000D4202
		internal bool ReservedFlag
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.ReservedFlag];
			}
			set
			{
				this[MailboxDatabaseSchema.ReservedFlag] = value;
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000D6015 File Offset: 0x000D4215
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x000D6027 File Offset: 0x000D4227
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromProvisioning
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IsExcludedFromProvisioning];
			}
			set
			{
				this[MailboxDatabaseSchema.IsExcludedFromProvisioning] = value;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000D603A File Offset: 0x000D423A
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x000D604C File Offset: 0x000D424C
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromProvisioningBySchemaVersionMonitoring
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IsExcludedFromProvisioningBySchemaVersionMonitoring];
			}
			set
			{
				this[MailboxDatabaseSchema.IsExcludedFromProvisioningBySchemaVersionMonitoring] = value;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000D605F File Offset: 0x000D425F
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x000D6071 File Offset: 0x000D4271
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromInitialProvisioning
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IsExcludedFromInitialProvisioning];
			}
			set
			{
				this[MailboxDatabaseSchema.IsExcludedFromInitialProvisioning] = value;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000D6084 File Offset: 0x000D4284
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x000D6096 File Offset: 0x000D4296
		[Parameter(Mandatory = false)]
		public bool IsSuspendedFromProvisioning
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IsSuspendedFromProvisioning];
			}
			set
			{
				this[MailboxDatabaseSchema.IsSuspendedFromProvisioning] = value;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000D60A9 File Offset: 0x000D42A9
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x000D60BB File Offset: 0x000D42BB
		[Parameter(Mandatory = false)]
		public bool IsExcludedFromProvisioningBySpaceMonitoring
		{
			get
			{
				return (bool)this[MailboxDatabaseSchema.IsExcludedFromProvisioningBySpaceMonitoring];
			}
			set
			{
				this[MailboxDatabaseSchema.IsExcludedFromProvisioningBySpaceMonitoring] = value;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x000D60CE File Offset: 0x000D42CE
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x000D60E0 File Offset: 0x000D42E0
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize? MailboxLoadBalanceMaximumEdbFileSize
		{
			get
			{
				return (ByteQuantifiedSize?)this[MailboxDatabaseSchema.MailboxLoadBalanceMaximumEdbFileSize];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxLoadBalanceMaximumEdbFileSize] = value;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x000D60F3 File Offset: 0x000D42F3
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x000D6105 File Offset: 0x000D4305
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceRelativeLoadCapacity
		{
			get
			{
				return (int?)this[MailboxDatabaseSchema.MailboxLoadBalanceRelativeLoadCapacity];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxLoadBalanceRelativeLoadCapacity] = value;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x000D6118 File Offset: 0x000D4318
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x000D612A File Offset: 0x000D432A
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceOverloadedThreshold
		{
			get
			{
				return (int?)this[MailboxDatabaseSchema.MailboxLoadBalanceOverloadedThreshold];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxLoadBalanceOverloadedThreshold] = value;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000D613D File Offset: 0x000D433D
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x000D614F File Offset: 0x000D434F
		[Parameter(Mandatory = false)]
		public int? MailboxLoadBalanceUnderloadedThreshold
		{
			get
			{
				return (int?)this[MailboxDatabaseSchema.MailboxLoadBalanceUnderloadedThreshold];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxLoadBalanceUnderloadedThreshold] = value;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000D6162 File Offset: 0x000D4362
		// (set) Token: 0x06003690 RID: 13968 RVA: 0x000D6174 File Offset: 0x000D4374
		[Parameter(Mandatory = false)]
		public bool? MailboxLoadBalanceEnabled
		{
			get
			{
				return (bool?)this[MailboxDatabaseSchema.MailboxLoadBalanceEnabled];
			}
			set
			{
				this[MailboxDatabaseSchema.MailboxLoadBalanceEnabled] = value;
			}
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000D6187 File Offset: 0x000D4387
		internal static QueryFilter IsExcludedFromProvisioningFilterBuilder(SinglePropertyFilter filter)
		{
			return SharedPropertyDefinitions.ProvisioningFlagsFilterBuilder(DatabaseProvisioningFlags.IsExcludedFromProvisioning, filter);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000D6190 File Offset: 0x000D4390
		internal static QueryFilter IsSuspendedFromProvisioningFilterBuilder(SinglePropertyFilter filter)
		{
			return SharedPropertyDefinitions.ProvisioningFlagsFilterBuilder(DatabaseProvisioningFlags.IsSuspendedFromProvisioning, filter);
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000D6199 File Offset: 0x000D4399
		public DumpsterStatisticsEntry[] DumpsterStatistics
		{
			get
			{
				return this.m_DumpsterStatistics;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003694 RID: 13972 RVA: 0x000D61A1 File Offset: 0x000D43A1
		public string[] DumpsterServersNotAvailable
		{
			get
			{
				return this.m_DumpsterServersNotAvailable;
			}
		}

		// Token: 0x040024D2 RID: 9426
		private static MailboxDatabaseSchema schema = ObjectSchema.GetInstance<MailboxDatabaseSchema>();

		// Token: 0x040024D3 RID: 9427
		internal static readonly string MostDerivedClass = "msExchPrivateMDB";

		// Token: 0x040024D4 RID: 9428
		internal DumpsterStatisticsEntry[] m_DumpsterStatistics;

		// Token: 0x040024D5 RID: 9429
		internal string[] m_DumpsterServersNotAvailable;
	}
}
