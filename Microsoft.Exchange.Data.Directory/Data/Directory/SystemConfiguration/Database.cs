using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003D4 RID: 980
	[ObjectScope(ConfigScopes.Database)]
	[Serializable]
	public class Database : ADLegacyVersionableObject
	{
		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000B7BD3 File Offset: 0x000B5DD3
		internal override ADObjectSchema Schema
		{
			get
			{
				return Database.schema;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000B7BDA File Offset: 0x000B5DDA
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMDB";
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000B7BE4 File Offset: 0x000B5DE4
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, MailboxDatabase.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, PublicFolderDatabase.MostDerivedClass)
				});
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000B7C24 File Offset: 0x000B5E24
		protected override void ValidateRead(List<ValidationError> errors)
		{
			base.ValidateRead(errors);
			if (this.IsExchange2009OrLater && this.MasterServerOrAvailabilityGroup == null)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorMasterServerInvalid(this.Name), DatabaseSchema.MasterServerOrAvailabilityGroup, this));
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000B7C5C File Offset: 0x000B5E5C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (!base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				AsciiCharactersOnlyConstraint asciiCharactersOnlyConstraint = new AsciiCharactersOnlyConstraint();
				PropertyConstraintViolationError propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.Name, DatabaseSchema.Name, null);
				if (propertyConstraintViolationError != null)
				{
					errors.Add(propertyConstraintViolationError);
				}
				if (null != this.EdbFilePath)
				{
					propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.EdbFilePath, DatabaseSchema.EdbFilePath, null);
					if (propertyConstraintViolationError != null)
					{
						errors.Add(propertyConstraintViolationError);
					}
				}
				if (!base.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					if (null == this.LogFolderPath)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.PropertyRequired(DatabaseSchema.LogFolderPath.Name, base.GetType().ToString()), this.Identity, string.Empty));
					}
					else
					{
						propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.LogFolderPath, StorageGroupSchema.LogFolderPath, null);
						if (propertyConstraintViolationError != null)
						{
							errors.Add(propertyConstraintViolationError);
						}
					}
					if (null == this.SystemFolderPath)
					{
						errors.Add(new ObjectValidationError(DirectoryStrings.PropertyRequired(DatabaseSchema.SystemFolderPath.Name, base.GetType().ToString()), this.Identity, string.Empty));
					}
					else
					{
						propertyConstraintViolationError = asciiCharactersOnlyConstraint.Validate(this.SystemFolderPath, StorageGroupSchema.SystemFolderPath, null);
						if (propertyConstraintViolationError != null)
						{
							errors.Add(propertyConstraintViolationError);
						}
					}
				}
			}
			if (null != this.EdbFilePath && this.EdbFilePath.IsPathInRootDirectory)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorEdbFilePathInRoot(this.EdbFilePath.PathName), this.Identity, string.Empty));
			}
			if (base.Id.DomainId != null && base.Id.Depth - base.Id.DomainId.Depth < 8)
			{
				errors.Add(new ObjectValidationError(DirectoryStrings.ErrorInvalidDNDepth, this.Identity, string.Empty));
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000B7E2C File Offset: 0x000B602C
		public ReplicationType ReplicationType
		{
			get
			{
				ReplicationType result;
				lock (this)
				{
					if (this.replicationType == ReplicationType.Unknown)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.replicationType;
				}
				return result;
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000B7E78 File Offset: 0x000B6078
		internal bool IsValidDatabase(bool allowInvalid)
		{
			return allowInvalid || this.IsValid;
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000B7E85 File Offset: 0x000B6085
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06002C60 RID: 11360 RVA: 0x000B7E8C File Offset: 0x000B608C
		// (set) Token: 0x06002C61 RID: 11361 RVA: 0x000B7EC7 File Offset: 0x000B60C7
		public ServerVersion AdminDisplayVersion
		{
			get
			{
				ServerVersion serverVersion = (ServerVersion)this[DatabaseSchema.AdminDisplayVersion];
				if (serverVersion == null)
				{
					serverVersion = Microsoft.Exchange.Data.Directory.SystemConfiguration.Server.GetServerVersion(this.ServerName);
					this.AdminDisplayVersion = serverVersion;
				}
				return serverVersion;
			}
			internal set
			{
				this[DatabaseSchema.AdminDisplayVersion] = value;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002C62 RID: 11362 RVA: 0x000B7ED5 File Offset: 0x000B60D5
		public ADObjectId AdministrativeGroup
		{
			get
			{
				return (ADObjectId)this[DatabaseSchema.AdministrativeGroup];
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000B7EE7 File Offset: 0x000B60E7
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000B7EF9 File Offset: 0x000B60F9
		[Parameter(Mandatory = false)]
		public bool AllowFileRestore
		{
			get
			{
				return (bool)this[DatabaseSchema.AllowFileRestore];
			}
			set
			{
				this[DatabaseSchema.AllowFileRestore] = value;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000B7F0C File Offset: 0x000B610C
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x000B7F1E File Offset: 0x000B611E
		[Parameter(Mandatory = false)]
		public bool BackgroundDatabaseMaintenance
		{
			get
			{
				return (bool)this[DatabaseSchema.BackgroundDatabaseMaintenance];
			}
			set
			{
				this[DatabaseSchema.BackgroundDatabaseMaintenance] = value;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x000B7F31 File Offset: 0x000B6131
		public bool? ReplayBackgroundDatabaseMaintenance
		{
			get
			{
				return (bool?)this[DatabaseSchema.ReplayBackgroundDatabaseMaintenance];
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002C68 RID: 11368 RVA: 0x000B7F43 File Offset: 0x000B6143
		public bool? BackgroundDatabaseMaintenanceSerialization
		{
			get
			{
				return (bool?)this[DatabaseSchema.BackgroundDatabaseMaintenanceSerialization];
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000B7F55 File Offset: 0x000B6155
		public int? BackgroundDatabaseMaintenanceDelay
		{
			get
			{
				return (int?)this[DatabaseSchema.BackgroundDatabaseMaintenanceDelay];
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002C6A RID: 11370 RVA: 0x000B7F67 File Offset: 0x000B6167
		public int? ReplayBackgroundDatabaseMaintenanceDelay
		{
			get
			{
				return (int?)this[DatabaseSchema.ReplayBackgroundDatabaseMaintenanceDelay];
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000B7F79 File Offset: 0x000B6179
		public int? MimimumBackgroundDatabaseMaintenanceInterval
		{
			get
			{
				return (int?)this[DatabaseSchema.MimimumBackgroundDatabaseMaintenanceInterval];
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06002C6C RID: 11372 RVA: 0x000B7F8B File Offset: 0x000B618B
		public int? MaximumBackgroundDatabaseMaintenanceInterval
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumBackgroundDatabaseMaintenanceInterval];
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000B7F9D File Offset: 0x000B619D
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000B7FA5 File Offset: 0x000B61A5
		public bool? BackupInProgress
		{
			get
			{
				return this.databaseBackupInProgress;
			}
			internal set
			{
				this.databaseBackupInProgress = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000B7FAE File Offset: 0x000B61AE
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000B7FC0 File Offset: 0x000B61C0
		public bool DatabaseCreated
		{
			get
			{
				return (bool)this[DatabaseSchema.DatabaseCreated];
			}
			internal set
			{
				this[DatabaseSchema.DatabaseCreated] = value;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000B7FD3 File Offset: 0x000B61D3
		internal DeliveryMechanisms DeliveryMechanism
		{
			get
			{
				return (DeliveryMechanisms)this[DatabaseSchema.DeliveryMechanism];
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x000B7FE5 File Offset: 0x000B61E5
		public string Description
		{
			get
			{
				return (string)this[DatabaseSchema.Description];
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000B7FF7 File Offset: 0x000B61F7
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x000B8009 File Offset: 0x000B6209
		public EdbFilePath EdbFilePath
		{
			get
			{
				return (EdbFilePath)this[DatabaseSchema.EdbFilePath];
			}
			internal set
			{
				this[DatabaseSchema.EdbFilePath] = value;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000B8017 File Offset: 0x000B6217
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x000B8029 File Offset: 0x000B6229
		public string ExchangeLegacyDN
		{
			get
			{
				return (string)this[DatabaseSchema.ExchangeLegacyDN];
			}
			internal set
			{
				this[DatabaseSchema.ExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06002C77 RID: 11383 RVA: 0x000B8037 File Offset: 0x000B6237
		// (set) Token: 0x06002C78 RID: 11384 RVA: 0x000B8049 File Offset: 0x000B6249
		internal bool FixedFont
		{
			get
			{
				return (bool)this[DatabaseSchema.FixedFont];
			}
			set
			{
				this[DatabaseSchema.FixedFont] = value;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06002C79 RID: 11385 RVA: 0x000B805C File Offset: 0x000B625C
		public DatabaseCopy[] DatabaseCopies
		{
			get
			{
				DatabaseCopy[] result;
				lock (this)
				{
					if (this.databaseCopies == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.databaseCopies;
				}
				return result;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000B80A8 File Offset: 0x000B62A8
		public DatabaseCopy[] InvalidDatabaseCopies
		{
			get
			{
				DatabaseCopy[] result;
				lock (this)
				{
					if (this.invalidDatabaseCopies == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.invalidDatabaseCopies;
				}
				return result;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x000B80F4 File Offset: 0x000B62F4
		public DatabaseCopy[] AllDatabaseCopies
		{
			get
			{
				DatabaseCopy[] result;
				lock (this)
				{
					if (this.allDatabaseCopies == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.allDatabaseCopies;
				}
				return result;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000B8140 File Offset: 0x000B6340
		public ADObjectId[] Servers
		{
			get
			{
				lock (this)
				{
					if (this.servers == null)
					{
						this.CompleteAllCalculatedProperties();
					}
				}
				return this.servers;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000B818C File Offset: 0x000B638C
		public KeyValuePair<ADObjectId, int>[] ActivationPreference
		{
			get
			{
				KeyValuePair<ADObjectId, int>[] result;
				lock (this)
				{
					if (this.activationPreference == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.activationPreference;
				}
				return result;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000B81D8 File Offset: 0x000B63D8
		public KeyValuePair<ADObjectId, EnhancedTimeSpan>[] ReplayLagTimes
		{
			get
			{
				KeyValuePair<ADObjectId, EnhancedTimeSpan>[] result;
				lock (this)
				{
					if (this.replayLagTimes == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.replayLagTimes;
				}
				return result;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x000B8224 File Offset: 0x000B6424
		public KeyValuePair<ADObjectId, EnhancedTimeSpan>[] TruncationLagTimes
		{
			get
			{
				KeyValuePair<ADObjectId, EnhancedTimeSpan>[] result;
				lock (this)
				{
					if (this.truncationLagTimes == null)
					{
						this.CompleteAllCalculatedProperties();
					}
					result = this.truncationLagTimes;
				}
				return result;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000B8270 File Offset: 0x000B6470
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000B8282 File Offset: 0x000B6482
		internal string RpcClientAccessServerLegacyDN
		{
			get
			{
				return (string)this[DatabaseSchema.RpcClientAccessServerExchangeLegacyDN];
			}
			set
			{
				this[DatabaseSchema.RpcClientAccessServerExchangeLegacyDN] = value;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002C82 RID: 11394 RVA: 0x000B8290 File Offset: 0x000B6490
		// (set) Token: 0x06002C83 RID: 11395 RVA: 0x000B8298 File Offset: 0x000B6498
		public string RpcClientAccessServer
		{
			get
			{
				return this.rpcClientAccessServerFqdn;
			}
			internal set
			{
				this.rpcClientAccessServerFqdn = value;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002C84 RID: 11396 RVA: 0x000B82A1 File Offset: 0x000B64A1
		// (set) Token: 0x06002C85 RID: 11397 RVA: 0x000B82A9 File Offset: 0x000B64A9
		public string MountedOnServer
		{
			get
			{
				return this.mountedOnServer;
			}
			internal set
			{
				this.mountedOnServer = value;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x000B82B2 File Offset: 0x000B64B2
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x000B82C4 File Offset: 0x000B64C4
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan DeletedItemRetention
		{
			get
			{
				return (EnhancedTimeSpan)this[DatabaseSchema.DeletedItemRetention];
			}
			set
			{
				this[DatabaseSchema.DeletedItemRetention] = value;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000B82D7 File Offset: 0x000B64D7
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x000B82DF File Offset: 0x000B64DF
		public bool? SnapshotLastFullBackup
		{
			get
			{
				return this.snapshotLastFullBackup;
			}
			internal set
			{
				this.snapshotLastFullBackup = value;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000B82E8 File Offset: 0x000B64E8
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x000B82F0 File Offset: 0x000B64F0
		public bool? SnapshotLastIncrementalBackup
		{
			get
			{
				return this.snapshotLastIncrementalBackup;
			}
			internal set
			{
				this.snapshotLastIncrementalBackup = value;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000B82F9 File Offset: 0x000B64F9
		// (set) Token: 0x06002C8D RID: 11405 RVA: 0x000B8301 File Offset: 0x000B6501
		public bool? SnapshotLastDifferentialBackup
		{
			get
			{
				return this.snapshotLastDifferentialBackup;
			}
			internal set
			{
				this.snapshotLastDifferentialBackup = value;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000B830A File Offset: 0x000B650A
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x000B8312 File Offset: 0x000B6512
		public bool? SnapshotLastCopyBackup
		{
			get
			{
				return this.snapshotLastCopyBackup;
			}
			internal set
			{
				this.snapshotLastCopyBackup = value;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002C90 RID: 11408 RVA: 0x000B831B File Offset: 0x000B651B
		// (set) Token: 0x06002C91 RID: 11409 RVA: 0x000B8323 File Offset: 0x000B6523
		public DateTime? LastFullBackup
		{
			get
			{
				return this.databaseLastFullBackup;
			}
			internal set
			{
				this.databaseLastFullBackup = value;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000B832C File Offset: 0x000B652C
		// (set) Token: 0x06002C93 RID: 11411 RVA: 0x000B8334 File Offset: 0x000B6534
		public DateTime? LastIncrementalBackup
		{
			get
			{
				return this.databaseLastIncrementalBackup;
			}
			internal set
			{
				this.databaseLastIncrementalBackup = value;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x000B833D File Offset: 0x000B653D
		// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000B8345 File Offset: 0x000B6545
		public DateTime? LastDifferentialBackup
		{
			get
			{
				return this.databaseLastDifferentialBackup;
			}
			internal set
			{
				this.databaseLastDifferentialBackup = value;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002C96 RID: 11414 RVA: 0x000B834E File Offset: 0x000B654E
		// (set) Token: 0x06002C97 RID: 11415 RVA: 0x000B8356 File Offset: 0x000B6556
		public DateTime? LastCopyBackup
		{
			get
			{
				return this.databaseLastCopyBackup;
			}
			internal set
			{
				this.databaseLastCopyBackup = value;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000B835F File Offset: 0x000B655F
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x000B8367 File Offset: 0x000B6567
		public ByteQuantifiedSize? DatabaseSize
		{
			get
			{
				return this.databaseSize;
			}
			internal set
			{
				this.databaseSize = value;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000B8370 File Offset: 0x000B6570
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x000B8378 File Offset: 0x000B6578
		public ByteQuantifiedSize? AvailableNewMailboxSpace
		{
			get
			{
				return this.availableNewMailboxSpace;
			}
			internal set
			{
				this.availableNewMailboxSpace = value;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000B8381 File Offset: 0x000B6581
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x000B8393 File Offset: 0x000B6593
		[Parameter(Mandatory = false)]
		public Schedule MaintenanceSchedule
		{
			get
			{
				return (Schedule)this[DatabaseSchema.MaintenanceSchedule];
			}
			set
			{
				this[DatabaseSchema.MaintenanceSchedule] = value;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x000B83A1 File Offset: 0x000B65A1
		internal ScheduleMode MaintenanceScheduleMode
		{
			get
			{
				return (ScheduleMode)this[DatabaseSchema.MaintenanceScheduleMode];
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x000B83B3 File Offset: 0x000B65B3
		internal int MaxCachedViews
		{
			get
			{
				return (int)this[DatabaseSchema.MaxCachedViews];
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x000B83C5 File Offset: 0x000B65C5
		// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x000B83D7 File Offset: 0x000B65D7
		[Parameter(Mandatory = false)]
		public bool MountAtStartup
		{
			get
			{
				return (bool)this[DatabaseSchema.MountAtStartup];
			}
			set
			{
				this[DatabaseSchema.MountAtStartup] = value;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x000B83EA File Offset: 0x000B65EA
		// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x000B83F2 File Offset: 0x000B65F2
		public bool? Mounted
		{
			get
			{
				return this.databaseMounted;
			}
			internal set
			{
				this.databaseMounted = value;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002CA4 RID: 11428 RVA: 0x000B83FB File Offset: 0x000B65FB
		// (set) Token: 0x06002CA5 RID: 11429 RVA: 0x000B8403 File Offset: 0x000B6603
		internal bool? OnlineMaintenanceInProgress
		{
			get
			{
				return this.databaseOnlineMaintenanceInProgress;
			}
			set
			{
				this.databaseOnlineMaintenanceInProgress = value;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002CA6 RID: 11430 RVA: 0x000B840C File Offset: 0x000B660C
		public ADObjectId Organization
		{
			get
			{
				return (ADObjectId)this[DatabaseSchema.Organization];
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002CA7 RID: 11431 RVA: 0x000B841E File Offset: 0x000B661E
		internal ScheduleMode QuotaNotificationMode
		{
			get
			{
				return (ScheduleMode)this[DatabaseSchema.QuotaNotificationMode];
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000B8430 File Offset: 0x000B6630
		// (set) Token: 0x06002CA9 RID: 11433 RVA: 0x000B8442 File Offset: 0x000B6642
		[Parameter(Mandatory = false)]
		public Schedule QuotaNotificationSchedule
		{
			get
			{
				return (Schedule)this[DatabaseSchema.QuotaNotificationSchedule];
			}
			set
			{
				this[DatabaseSchema.QuotaNotificationSchedule] = value;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002CAA RID: 11434 RVA: 0x000B8450 File Offset: 0x000B6650
		// (set) Token: 0x06002CAB RID: 11435 RVA: 0x000B8462 File Offset: 0x000B6662
		public bool Recovery
		{
			get
			{
				return (bool)this[DatabaseSchema.Recovery];
			}
			internal set
			{
				this[DatabaseSchema.Recovery] = value;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000B8475 File Offset: 0x000B6675
		// (set) Token: 0x06002CAD RID: 11437 RVA: 0x000B8487 File Offset: 0x000B6687
		internal bool RestoreInProgress
		{
			get
			{
				return (bool)this[DatabaseSchema.RestoreInProgress];
			}
			set
			{
				this[DatabaseSchema.RestoreInProgress] = value;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000B849A File Offset: 0x000B669A
		// (set) Token: 0x06002CAF RID: 11439 RVA: 0x000B84AC File Offset: 0x000B66AC
		[Parameter(Mandatory = false)]
		public bool RetainDeletedItemsUntilBackup
		{
			get
			{
				return (bool)this[DatabaseSchema.RetainDeletedItemsUntilBackup];
			}
			set
			{
				this[DatabaseSchema.RetainDeletedItemsUntilBackup] = value;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000B84BF File Offset: 0x000B66BF
		// (set) Token: 0x06002CB1 RID: 11441 RVA: 0x000B84D1 File Offset: 0x000B66D1
		public ADObjectId Server
		{
			get
			{
				return (ADObjectId)this[DatabaseSchema.Server];
			}
			internal set
			{
				this[DatabaseSchema.Server] = value;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002CB2 RID: 11442 RVA: 0x000B84DF File Offset: 0x000B66DF
		// (set) Token: 0x06002CB3 RID: 11443 RVA: 0x000B84F1 File Offset: 0x000B66F1
		public ADObjectId MasterServerOrAvailabilityGroup
		{
			get
			{
				return (ADObjectId)this[DatabaseSchema.MasterServerOrAvailabilityGroup];
			}
			internal set
			{
				this[DatabaseSchema.MasterServerOrAvailabilityGroup] = value;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000B84FF File Offset: 0x000B66FF
		// (set) Token: 0x06002CB5 RID: 11445 RVA: 0x000B8507 File Offset: 0x000B6707
		public int? WorkerProcessId
		{
			get
			{
				return this.workerProcessId;
			}
			internal set
			{
				this.workerProcessId = value;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x000B8510 File Offset: 0x000B6710
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x000B8518 File Offset: 0x000B6718
		public string CurrentSchemaVersion
		{
			get
			{
				return this.currentSchemaVersion;
			}
			internal set
			{
				this.currentSchemaVersion = value;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002CB8 RID: 11448 RVA: 0x000B8521 File Offset: 0x000B6721
		// (set) Token: 0x06002CB9 RID: 11449 RVA: 0x000B8529 File Offset: 0x000B6729
		public string RequestedSchemaVersion
		{
			get
			{
				return this.requestedSchemaVersion;
			}
			internal set
			{
				this.requestedSchemaVersion = value;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000B8532 File Offset: 0x000B6732
		// (set) Token: 0x06002CBB RID: 11451 RVA: 0x000B8544 File Offset: 0x000B6744
		internal DatabaseAutoDagFlags AutoDagFlags
		{
			get
			{
				return (DatabaseAutoDagFlags)this[DatabaseSchema.AutoDagFlags];
			}
			set
			{
				this[DatabaseSchema.AutoDagFlags] = value;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000B8557 File Offset: 0x000B6757
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000B8569 File Offset: 0x000B6769
		[Parameter(Mandatory = false)]
		public bool AutoDagExcludeFromMonitoring
		{
			get
			{
				return (bool)this[DatabaseSchema.AutoDagExcludeFromMonitoring];
			}
			set
			{
				this[DatabaseSchema.AutoDagExcludeFromMonitoring] = value;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002CBE RID: 11454 RVA: 0x000B857C File Offset: 0x000B677C
		// (set) Token: 0x06002CBF RID: 11455 RVA: 0x000B858E File Offset: 0x000B678E
		[Parameter(Mandatory = false)]
		public AutoDatabaseMountDial AutoDatabaseMountDial
		{
			get
			{
				return (AutoDatabaseMountDial)this[DatabaseSchema.AutoDatabaseMountDialType];
			}
			set
			{
				this[DatabaseSchema.AutoDatabaseMountDialType] = value;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x000B85A1 File Offset: 0x000B67A1
		// (set) Token: 0x06002CC1 RID: 11457 RVA: 0x000B85B3 File Offset: 0x000B67B3
		internal bool InvalidDatabaseCopiesAllowed
		{
			get
			{
				return (bool)this[DatabaseSchema.InvalidDatabaseCopiesAllowed];
			}
			set
			{
				this[DatabaseSchema.InvalidDatabaseCopiesAllowed] = value;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002CC2 RID: 11458 RVA: 0x000B85C6 File Offset: 0x000B67C6
		// (set) Token: 0x06002CC3 RID: 11459 RVA: 0x000B85D8 File Offset: 0x000B67D8
		[Parameter(Mandatory = false)]
		public string DatabaseGroup
		{
			get
			{
				return (string)this[DatabaseSchema.DatabaseGroup];
			}
			set
			{
				this[DatabaseSchema.DatabaseGroup] = value;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000B85E6 File Offset: 0x000B67E6
		public MasterType MasterType
		{
			get
			{
				if (this.m_masterType == null)
				{
					this.CalculateMasterType();
				}
				return this.m_masterType.Value;
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000B8606 File Offset: 0x000B6806
		internal void CalculateMasterType()
		{
			this.m_masterType = new MasterType?(Database.FindMasterType(this.MasterServerOrAvailabilityGroup));
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000B861E File Offset: 0x000B681E
		public string ServerName
		{
			get
			{
				return (string)this[DatabaseSchema.ServerName];
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000B8630 File Offset: 0x000B6830
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000B8642 File Offset: 0x000B6842
		internal bool SMimeSignatureEnabled
		{
			get
			{
				return (bool)this[DatabaseSchema.SMimeSignatureEnabled];
			}
			set
			{
				this[DatabaseSchema.SMimeSignatureEnabled] = value;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000B8655 File Offset: 0x000B6855
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000B8667 File Offset: 0x000B6867
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> IssueWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[DatabaseSchema.IssueWarningQuota];
			}
			set
			{
				this[DatabaseSchema.IssueWarningQuota] = value;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000B867A File Offset: 0x000B687A
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x000B868C File Offset: 0x000B688C
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan EventHistoryRetentionPeriod
		{
			get
			{
				return (EnhancedTimeSpan)this[DatabaseSchema.EventHistoryRetentionPeriod];
			}
			set
			{
				this[DatabaseSchema.EventHistoryRetentionPeriod] = value;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000B869F File Offset: 0x000B689F
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x000B86B1 File Offset: 0x000B68B1
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public new string Name
		{
			get
			{
				return (string)this[DatabaseSchema.Name];
			}
			set
			{
				this[DatabaseSchema.Name] = value;
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
				this[DatabaseSchema.DisplayName] = value;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000B86D7 File Offset: 0x000B68D7
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x000B86E9 File Offset: 0x000B68E9
		public NonRootLocalLongFullPath LogFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseSchema.LogFolderPath];
			}
			internal set
			{
				this[DatabaseSchema.LogFolderPath] = value;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000B86F7 File Offset: 0x000B68F7
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x000B8709 File Offset: 0x000B6909
		internal NonRootLocalLongFullPath SystemFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseSchema.SystemFolderPath];
			}
			set
			{
				this[DatabaseSchema.SystemFolderPath] = value;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000B8717 File Offset: 0x000B6917
		public NonRootLocalLongFullPath TemporaryDataFolderPath
		{
			get
			{
				return (NonRootLocalLongFullPath)this[DatabaseSchema.TemporaryDataFolderPath];
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x000B8729 File Offset: 0x000B6929
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x000B873B File Offset: 0x000B693B
		[Parameter(Mandatory = false)]
		public bool CircularLoggingEnabled
		{
			get
			{
				return (bool)this[DatabaseSchema.CircularLoggingEnabled];
			}
			set
			{
				this[DatabaseSchema.CircularLoggingEnabled] = value;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000B874E File Offset: 0x000B694E
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x000B8760 File Offset: 0x000B6960
		public string LogFilePrefix
		{
			get
			{
				return (string)this[DatabaseSchema.LogFilePrefix];
			}
			internal set
			{
				this[DatabaseSchema.LogFilePrefix] = value;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000B876E File Offset: 0x000B696E
		public int LogFileSize
		{
			get
			{
				return (int)this[DatabaseSchema.LogFileSize];
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000B8780 File Offset: 0x000B6980
		public int? LogBuffers
		{
			get
			{
				return (int?)this[DatabaseSchema.LogBuffers];
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000B8792 File Offset: 0x000B6992
		public int? MaximumOpenTables
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumOpenTables];
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000B87A4 File Offset: 0x000B69A4
		public int? MaximumTemporaryTables
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumTemporaryTables];
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000B87B6 File Offset: 0x000B69B6
		public int? MaximumCursors
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumCursors];
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002CDD RID: 11485 RVA: 0x000B87C8 File Offset: 0x000B69C8
		public int? MaximumSessions
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumSessions];
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000B87DA File Offset: 0x000B69DA
		public int? MaximumVersionStorePages
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumVersionStorePages];
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000B87EC File Offset: 0x000B69EC
		public int? PreferredVersionStorePages
		{
			get
			{
				return (int?)this[DatabaseSchema.PreferredVersionStorePages];
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000B87FE File Offset: 0x000B69FE
		public int? DatabaseExtensionSize
		{
			get
			{
				return (int?)this[DatabaseSchema.DatabaseExtensionSize];
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000B8810 File Offset: 0x000B6A10
		public int? LogCheckpointDepth
		{
			get
			{
				return (int?)this[DatabaseSchema.LogCheckpointDepth];
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000B8822 File Offset: 0x000B6A22
		public int? ReplayCheckpointDepth
		{
			get
			{
				return (int?)this[DatabaseSchema.ReplayCheckpointDepth];
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000B8834 File Offset: 0x000B6A34
		public int? CachedClosedTables
		{
			get
			{
				return (int?)this[DatabaseSchema.CachedClosedTables];
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002CE4 RID: 11492 RVA: 0x000B8846 File Offset: 0x000B6A46
		public int? CachePriority
		{
			get
			{
				return (int?)this[DatabaseSchema.CachePriority];
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000B8858 File Offset: 0x000B6A58
		public int? ReplayCachePriority
		{
			get
			{
				return (int?)this[DatabaseSchema.ReplayCachePriority];
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x000B886A File Offset: 0x000B6A6A
		public int? MaximumPreReadPages
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumPreReadPages];
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000B887C File Offset: 0x000B6A7C
		public int? MaximumReplayPreReadPages
		{
			get
			{
				return (int?)this[DatabaseSchema.MaximumReplayPreReadPages];
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x000B888E File Offset: 0x000B6A8E
		// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x000B88A0 File Offset: 0x000B6AA0
		[Parameter(Mandatory = false)]
		public DataMoveReplicationConstraintParameter DataMoveReplicationConstraint
		{
			get
			{
				return (DataMoveReplicationConstraintParameter)this[DatabaseSchema.DataMoveReplicationConstraintDefinition];
			}
			set
			{
				this[DatabaseSchema.DataMoveReplicationConstraintDefinition] = value;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000B88B3 File Offset: 0x000B6AB3
		public bool IsMailboxDatabase
		{
			get
			{
				return base.ObjectClass.Contains(MailboxDatabase.MostDerivedClass);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000B88C5 File Offset: 0x000B6AC5
		public bool IsPublicFolderDatabase
		{
			get
			{
				return base.ObjectClass.Contains(PublicFolderDatabase.MostDerivedClass);
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002CEC RID: 11500 RVA: 0x000B88D7 File Offset: 0x000B6AD7
		// (set) Token: 0x06002CED RID: 11501 RVA: 0x000B88E9 File Offset: 0x000B6AE9
		public MailboxProvisioningAttributes MailboxProvisioningAttributes
		{
			get
			{
				return this[DatabaseSchema.MailboxProvisioningAttributes] as MailboxProvisioningAttributes;
			}
			set
			{
				this[DatabaseSchema.MailboxProvisioningAttributes] = value;
			}
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000B88F7 File Offset: 0x000B6AF7
		internal Server GetServer()
		{
			return base.Session.Read<Server>(this.Server);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000B890C File Offset: 0x000B6B0C
		internal DatabaseCopy[] GetDatabaseCopies()
		{
			DatabaseCopy[] result;
			lock (this)
			{
				if (this.databaseCopies == null)
				{
					this.CompleteAllCalculatedProperties();
				}
				result = this.databaseCopies;
			}
			return result;
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000B8978 File Offset: 0x000B6B78
		internal void ExcludeDatabaseCopyFromProperties(string hostServerToExclude)
		{
			lock (this)
			{
				if (this.allDatabaseCopies == null)
				{
					this.CompleteAllCalculatedProperties();
				}
				if (this.allDatabaseCopies != null && this.allDatabaseCopies.Length > 1)
				{
					DatabaseCopy[] array = (from dbCopy in this.allDatabaseCopies
					where !string.Equals(dbCopy.Name, hostServerToExclude, StringComparison.OrdinalIgnoreCase)
					select dbCopy).ToArray<DatabaseCopy>();
					if (array.Length != this.allDatabaseCopies.Length)
					{
						this.CompletePropertiesFromDbCopies(array);
					}
				}
			}
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000B8A18 File Offset: 0x000B6C18
		internal void CompleteAllCalculatedProperties()
		{
			IConfigurationSession configurationSession = this.CreateCustomConfigSessionIfNecessary();
			if (configurationSession == null)
			{
				return;
			}
			this.AdminDisplayVersion = this.AdminDisplayVersion;
			DatabaseCopy[] knownCopies = configurationSession.Find<DatabaseCopy>((ADObjectId)this.Identity, QueryScope.SubTree, null, null, 0);
			this.CompletePropertiesFromDbCopies(knownCopies);
			if (this.rpcClientAccessServerFqdn == null)
			{
				this.CalculateRpcClientAccessServer();
			}
			if (this.m_masterType == null)
			{
				this.CalculateMasterType();
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000B8A7C File Offset: 0x000B6C7C
		private void CompletePropertiesFromDbCopies(DatabaseCopy[] knownCopies)
		{
			if (knownCopies == null || knownCopies.Length == 0)
			{
				this.databaseCopies = new DatabaseCopy[0];
				this.invalidDatabaseCopies = new DatabaseCopy[0];
				this.allDatabaseCopies = new DatabaseCopy[0];
				this.servers = new ADObjectId[0];
				this.activationPreference = new KeyValuePair<ADObjectId, int>[0];
				this.replayLagTimes = new KeyValuePair<ADObjectId, EnhancedTimeSpan>[0];
				this.truncationLagTimes = new KeyValuePair<ADObjectId, EnhancedTimeSpan>[0];
				this.replicationType = ReplicationType.None;
				return;
			}
			Array.Sort<DatabaseCopy>(knownCopies);
			int num = 1;
			List<DatabaseCopy> list = new List<DatabaseCopy>(knownCopies.Length);
			List<DatabaseCopy> list2 = new List<DatabaseCopy>(knownCopies.Length);
			List<DatabaseCopy> list3 = new List<DatabaseCopy>(knownCopies.Length);
			List<ADObjectId> list4 = new List<ADObjectId>(knownCopies.Length);
			List<KeyValuePair<ADObjectId, int>> list5 = new List<KeyValuePair<ADObjectId, int>>(knownCopies.Length);
			List<KeyValuePair<ADObjectId, EnhancedTimeSpan>> list6 = new List<KeyValuePair<ADObjectId, EnhancedTimeSpan>>(knownCopies.Length);
			List<KeyValuePair<ADObjectId, EnhancedTimeSpan>> list7 = new List<KeyValuePair<ADObjectId, EnhancedTimeSpan>>(knownCopies.Length);
			foreach (DatabaseCopy databaseCopy in knownCopies)
			{
				if (databaseCopy.IsValidForRead && databaseCopy.IsHostServerPresent)
				{
					databaseCopy.ActivationPreference = num++;
					list.Add(databaseCopy);
					list3.Add(databaseCopy);
					list4.Add(databaseCopy.HostServer);
					list5.Add(new KeyValuePair<ADObjectId, int>(databaseCopy.HostServer, databaseCopy.ActivationPreference));
					list6.Add(new KeyValuePair<ADObjectId, EnhancedTimeSpan>(databaseCopy.HostServer, databaseCopy.ReplayLagTime));
					list7.Add(new KeyValuePair<ADObjectId, EnhancedTimeSpan>(databaseCopy.HostServer, databaseCopy.TruncationLagTime));
				}
				else
				{
					databaseCopy.ActivationPreference = num++;
					list2.Add(databaseCopy);
					list3.Add(databaseCopy);
				}
			}
			this.databaseCopies = list.ToArray();
			this.invalidDatabaseCopies = list2.ToArray();
			this.allDatabaseCopies = list3.ToArray();
			this.servers = list4.ToArray();
			this.activationPreference = list5.ToArray();
			this.replayLagTimes = list6.ToArray();
			this.truncationLagTimes = list7.ToArray();
			if (this.allDatabaseCopies.Length > 1)
			{
				this.replicationType = ReplicationType.Remote;
				return;
			}
			this.replicationType = ReplicationType.None;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000B8C78 File Offset: 0x000B6E78
		private IConfigurationSession CreateCustomConfigSessionIfNecessary()
		{
			IConfigurationSession configurationSession = base.Session;
			if (configurationSession != null && configurationSession.ConsistencyMode != ConsistencyMode.PartiallyConsistent)
			{
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configurationSession.DomainController, configurationSession.ReadOnly, ConsistencyMode.PartiallyConsistent, configurationSession.NetworkCredential, configurationSession.SessionSettings, 2385, "CreateCustomConfigSessionIfNecessary", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\database.cs");
			}
			return configurationSession;
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000B8CCC File Offset: 0x000B6ECC
		private void CalculateRpcClientAccessServer()
		{
			MiniClientAccessServerOrArray miniClientAccessServerOrArray;
			if (base.Session != null && ((ITopologyConfigurationSession)base.Session).TryFindByExchangeLegacyDN(this.RpcClientAccessServerLegacyDN, Database.s_propertiesNeededFromServer, out miniClientAccessServerOrArray))
			{
				this.rpcClientAccessServerFqdn = miniClientAccessServerOrArray.Fqdn;
				return;
			}
			this.rpcClientAccessServerFqdn = string.Empty;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000B8D18 File Offset: 0x000B6F18
		internal DatabaseCopy GetDatabaseCopy(ADObjectId server)
		{
			return this.GetDatabaseCopy(server.Name);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000B8D28 File Offset: 0x000B6F28
		internal DatabaseCopy GetDatabaseCopy(string serverShortName)
		{
			DatabaseCopy result = null;
			lock (this)
			{
				if (this.databaseCopies == null)
				{
					this.CompleteAllCalculatedProperties();
				}
				foreach (DatabaseCopy databaseCopy in this.databaseCopies)
				{
					if (MachineName.Comparer.Equals(databaseCopy.HostServerName, serverShortName))
					{
						result = databaseCopy;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000B8DA8 File Offset: 0x000B6FA8
		internal bool IsExchange2009OrLater
		{
			get
			{
				return (bool)this[DatabaseSchema.IsExchange2009OrLater];
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000B8DBC File Offset: 0x000B6FBC
		internal Database CreateDetachedWriteableClone<TDatabase>() where TDatabase : Database, new()
		{
			TDatabase tdatabase = (TDatabase)((object)this.Clone());
			tdatabase.SetIsReadOnly(false);
			return tdatabase;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000B8DEC File Offset: 0x000B6FEC
		private static MasterType FindMasterType(ADObjectId masterId)
		{
			if (masterId != null)
			{
				ADObjectId parent = masterId.Parent;
				if (parent != null)
				{
					if (string.Compare(parent.Name, DatabaseAvailabilityGroupContainer.DefaultName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return MasterType.DatabaseAvailabilityGroup;
					}
					if (string.Compare(parent.Name, ServersContainer.DefaultName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return MasterType.Server;
					}
				}
			}
			return MasterType.Unknown;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000B8E34 File Offset: 0x000B7034
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(DatabaseSchema.MaintenanceSchedule))
			{
				this.MaintenanceSchedule = Schedule.DailyFrom1AMTo5AM;
			}
			if (!base.IsModified(DatabaseSchema.QuotaNotificationSchedule))
			{
				this.QuotaNotificationSchedule = Schedule.Daily1AM;
			}
			if (!base.IsModified(DatabaseSchema.IssueWarningQuota))
			{
				this.IssueWarningQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromMB(1945UL));
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x000B8E9C File Offset: 0x000B709C
		internal static ValidationError[] ValidateAscendingQuotas(PropertyBag propertyBag, ProviderPropertyDefinition[] ascendingQuotaDefinitions, ObjectId identity)
		{
			List<ValidationError> list = new List<ValidationError>(ascendingQuotaDefinitions.Length);
			ProviderPropertyDefinition providerPropertyDefinition = null;
			Unlimited<ByteQuantifiedSize> unlimited = default(Unlimited<ByteQuantifiedSize>);
			int num = 0;
			while (ascendingQuotaDefinitions.Length > num)
			{
				ProviderPropertyDefinition providerPropertyDefinition2 = ascendingQuotaDefinitions[num];
				Unlimited<ByteQuantifiedSize> unlimited2 = (Unlimited<ByteQuantifiedSize>)propertyBag[providerPropertyDefinition2];
				if (!unlimited2.IsUnlimited)
				{
					if (providerPropertyDefinition != null && 0 < unlimited.CompareTo(unlimited2))
					{
						if (propertyBag.IsChanged(providerPropertyDefinition))
						{
							list.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1GtProperty2(providerPropertyDefinition.Name, unlimited.ToString(), providerPropertyDefinition2.Name, unlimited2.ToString()), identity, string.Empty));
						}
						else
						{
							list.Add(new ObjectValidationError(DirectoryStrings.ErrorProperty1LtProperty2(providerPropertyDefinition2.Name, unlimited2.ToString(), providerPropertyDefinition.Name, unlimited.ToString()), identity, string.Empty));
						}
					}
					providerPropertyDefinition = providerPropertyDefinition2;
					unlimited = unlimited2;
				}
				num++;
			}
			return list.ToArray();
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x000B8F98 File Offset: 0x000B7198
		internal static void InternalAssertComparisonFilter(SinglePropertyFilter filter, PropertyDefinition propertyDefinition)
		{
			string name = propertyDefinition.Name;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter == null)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(name, filter.GetType(), typeof(ComparisonFilter)));
			}
			object propertyValue = comparisonFilter.PropertyValue;
			if (propertyValue == null)
			{
				throw new ArgumentNullException("filter.PropertyValue");
			}
			Type type = propertyValue.GetType();
			if (type != propertyDefinition.Type)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedPropertyValueType(name, type, propertyDefinition.Type));
			}
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x000B9010 File Offset: 0x000B7210
		internal static object AdministrativeGroupGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ((ADObjectId)propertyBag[ADObjectSchema.Id]).DescendantDN(6);
			}
			catch (NullReferenceException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdministrativeGroup", ex.Message), DatabaseSchema.AdministrativeGroup, propertyBag[ADObjectSchema.Id]), ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("AdministrativeGroup", ex2.Message), DatabaseSchema.AdministrativeGroup, propertyBag[ADObjectSchema.Id]), ex2);
			}
			return result;
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000B90B0 File Offset: 0x000B72B0
		internal static object MaintenanceScheduleGetter(IPropertyBag propertyBag)
		{
			return propertyBag[DatabaseSchema.MaintenanceScheduleBitmaps];
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000B90BD File Offset: 0x000B72BD
		internal static void MaintenanceScheduleSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.MaintenanceScheduleBitmaps] = value;
			if (value == null)
			{
				propertyBag[DatabaseSchema.MaintenanceScheduleMode] = ScheduleMode.Never;
				return;
			}
			propertyBag[DatabaseSchema.MaintenanceScheduleMode] = ((Schedule)value).Mode;
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000B90FC File Offset: 0x000B72FC
		internal static QueryFilter MountAtStartupFilterBuilder(SinglePropertyFilter filter)
		{
			Database.InternalAssertComparisonFilter(filter, DatabaseSchema.MountAtStartup);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, DatabaseSchema.EdbOfflineAtStartup, !(bool)comparisonFilter.PropertyValue);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000B9140 File Offset: 0x000B7340
		internal static object OrganizationGetter(IPropertyBag propertyBag)
		{
			object result;
			try
			{
				result = ((ADObjectId)propertyBag[ADObjectSchema.Id]).DescendantDN(4);
			}
			catch (NullReferenceException ex)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Organization", ex.Message), DatabaseSchema.Organization, propertyBag[ADObjectSchema.Id]), ex);
			}
			catch (InvalidOperationException ex2)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty("Organization", ex2.Message), DatabaseSchema.Organization, propertyBag[ADObjectSchema.Id]), ex2);
			}
			return result;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000B91E0 File Offset: 0x000B73E0
		internal static object QuotaNotificationScheduleGetter(IPropertyBag propertyBag)
		{
			return propertyBag[DatabaseSchema.QuotaNotificationScheduleBitmaps];
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000B91ED File Offset: 0x000B73ED
		internal static void QuotaNotificationScheduleSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.QuotaNotificationScheduleBitmaps] = value;
			if (value == null)
			{
				propertyBag[DatabaseSchema.QuotaNotificationMode] = ScheduleMode.Never;
				return;
			}
			propertyBag[DatabaseSchema.QuotaNotificationMode] = ((Schedule)value).Mode;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000B922C File Offset: 0x000B742C
		internal static QueryFilter RetainDeletedItemsUntilBackupFilterBuilder(SinglePropertyFilter filter)
		{
			Database.InternalAssertComparisonFilter(filter, DatabaseSchema.RetainDeletedItemsUntilBackup);
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			return new ComparisonFilter(comparisonFilter.ComparisonOperator, DatabaseSchema.DelItemAfterBackupEnum, ((bool)comparisonFilter.PropertyValue) ? Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod : Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainForCustomPeriod);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000B9271 File Offset: 0x000B7471
		internal static object RetainDeletedItemsUntilBackupGetter(IPropertyBag propertyBag)
		{
			return Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod == (DeletedItemRetention)propertyBag[DatabaseSchema.DelItemAfterBackupEnum];
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000B928B File Offset: 0x000B748B
		internal static void RetainDeletedItemsUntilBackupSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.DelItemAfterBackupEnum] = (((bool)value) ? Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainUntilBackupOrCustomPeriod : Microsoft.Exchange.Data.Directory.Recipient.DeletedItemRetention.RetainForCustomPeriod);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000B92AC File Offset: 0x000B74AC
		internal static object ServerNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[DatabaseSchema.Server];
			if (adobjectId == null)
			{
				return DatabaseSchema.ServerName.DefaultValue;
			}
			return adobjectId.Name;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000B92E0 File Offset: 0x000B74E0
		internal static object IsExchange2009OrLaterGetter(IPropertyBag propertyBag)
		{
			ExchangeObjectVersion exchangeObjectVersion = (ExchangeObjectVersion)propertyBag[ADObjectSchema.ExchangeVersion];
			return !exchangeObjectVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000B9314 File Offset: 0x000B7514
		internal static object MasterServerOrAvailabilityGroupNameGetter(IPropertyBag propertyBag)
		{
			ADObjectId adobjectId = (ADObjectId)propertyBag[DatabaseSchema.MasterServerOrAvailabilityGroup];
			if (adobjectId == null)
			{
				return DatabaseSchema.MasterServerOrAvailabilityGroupName.DefaultValue;
			}
			return adobjectId.Name;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000B9346 File Offset: 0x000B7546
		internal static object DatabaseNameGetter(IPropertyBag propertyBag)
		{
			return propertyBag[ADObjectSchema.RawName];
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000B9353 File Offset: 0x000B7553
		internal static void DatabaseNameSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[ADObjectSchema.RawName] = value;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000B9361 File Offset: 0x000B7561
		internal static LegacyDN GetRcaLegacyDNFromDatabaseLegacyDN(LegacyDN database)
		{
			return database.GetParentLegacyDN();
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B9369 File Offset: 0x000B7569
		internal static LegacyDN GetDatabaseLegacyDNFromRcaLegacyDN(LegacyDN rpcClientAccessServer, bool isPublic)
		{
			return new LegacyDN(rpcClientAccessServer, "cn", isPublic ? "Microsoft Public MDB" : "Microsoft Private MDB");
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B9388 File Offset: 0x000B7588
		internal static object RpcClientAccessServerExchangeLegacyDNGetter(IPropertyBag propertyBag)
		{
			string text = (string)propertyBag[DatabaseSchema.ExchangeLegacyDN];
			LegacyDN database;
			if (LegacyDN.TryParse(text, out database))
			{
				return Database.GetRcaLegacyDNFromDatabaseLegacyDN(database).ToString();
			}
			throw new DataValidationException(new ValidLegacyDNConstraint().Validate(text, DatabaseSchema.ExchangeLegacyDN, propertyBag));
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B93D2 File Offset: 0x000B75D2
		internal static void RpcClientAccessServerExchangeLegacyDNSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[DatabaseSchema.ExchangeLegacyDN] = Database.GetDatabaseLegacyDNFromRcaLegacyDN(LegacyDN.Parse((string)value), ((MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass]).Contains(PublicFolderDatabase.MostDerivedClass)).ToString();
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B9410 File Offset: 0x000B7610
		internal static object MailboxProvisioningAttributesGetter(IPropertyBag propertyBag)
		{
			DatabaseConfigXml databaseConfigXml = (DatabaseConfigXml)DatabaseSchema.ConfigurationXML.GetterDelegate(propertyBag);
			List<MailboxProvisioningAttribute> list;
			if (databaseConfigXml != null && databaseConfigXml.MailboxProvisioningAttributes != null)
			{
				list = new List<MailboxProvisioningAttribute>(databaseConfigXml.MailboxProvisioningAttributes.Attributes);
			}
			else
			{
				list = new List<MailboxProvisioningAttribute>();
			}
			MailboxProvisioningAttribute item = new MailboxProvisioningAttribute
			{
				Key = MailboxProvisioningAttributesSchema.DatabaseName.Name,
				Value = (string)Database.DatabaseNameGetter(propertyBag)
			};
			list.Add(item);
			MailboxProvisioningAttribute item2 = new MailboxProvisioningAttribute
			{
				Key = MailboxProvisioningAttributesSchema.ServerName.Name,
				Value = (string)Database.ServerNameGetter(propertyBag)
			};
			list.Add(item2);
			ADObjectId adobjectId = (ADObjectId)propertyBag[DatabaseSchema.MasterServerOrAvailabilityGroup];
			if (Database.FindMasterType(adobjectId) == MasterType.DatabaseAvailabilityGroup)
			{
				MailboxProvisioningAttribute item3 = new MailboxProvisioningAttribute
				{
					Key = MailboxProvisioningAttributesSchema.DagName.Name,
					Value = adobjectId.Name
				};
				list.Add(item3);
			}
			return new MailboxProvisioningAttributes(list.ToArray());
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B9518 File Offset: 0x000B7718
		internal static void MailboxProvisioningAttributesSetter(object value, IPropertyBag propertyBag)
		{
			MailboxProvisioningAttributes mailboxProvisioningAttributes = (MailboxProvisioningAttributes)value;
			DatabaseConfigXml databaseConfigXml = (DatabaseConfigXml)DatabaseSchema.ConfigurationXML.GetterDelegate(propertyBag);
			if (databaseConfigXml == null)
			{
				databaseConfigXml = new DatabaseConfigXml();
			}
			databaseConfigXml.MailboxProvisioningAttributes = mailboxProvisioningAttributes;
			DatabaseSchema.ConfigurationXML.SetterDelegate(databaseConfigXml, propertyBag);
		}

		// Token: 0x04001E1F RID: 7711
		private const string MostDerivedClassInternal = "msExchMDB";

		// Token: 0x04001E20 RID: 7712
		private const string PrivateMdbCnInternal = "Microsoft Private MDB";

		// Token: 0x04001E21 RID: 7713
		private const string PublicMdbCnInternal = "Microsoft Public MDB";

		// Token: 0x04001E22 RID: 7714
		private static readonly DatabaseSchema schema = ObjectSchema.GetInstance<DatabaseSchema>();

		// Token: 0x04001E23 RID: 7715
		private static readonly PropertyDefinition[] s_propertiesNeededFromServer = new PropertyDefinition[]
		{
			ServerSchema.Fqdn
		};

		// Token: 0x04001E24 RID: 7716
		private bool? databaseMounted;

		// Token: 0x04001E25 RID: 7717
		private bool? databaseOnlineMaintenanceInProgress;

		// Token: 0x04001E26 RID: 7718
		private bool? databaseBackupInProgress;

		// Token: 0x04001E27 RID: 7719
		private bool? snapshotLastFullBackup;

		// Token: 0x04001E28 RID: 7720
		private bool? snapshotLastIncrementalBackup;

		// Token: 0x04001E29 RID: 7721
		private bool? snapshotLastDifferentialBackup;

		// Token: 0x04001E2A RID: 7722
		private bool? snapshotLastCopyBackup;

		// Token: 0x04001E2B RID: 7723
		private DateTime? databaseLastFullBackup;

		// Token: 0x04001E2C RID: 7724
		private DateTime? databaseLastIncrementalBackup;

		// Token: 0x04001E2D RID: 7725
		private DateTime? databaseLastDifferentialBackup;

		// Token: 0x04001E2E RID: 7726
		private DateTime? databaseLastCopyBackup;

		// Token: 0x04001E2F RID: 7727
		private ByteQuantifiedSize? databaseSize;

		// Token: 0x04001E30 RID: 7728
		private ByteQuantifiedSize? availableNewMailboxSpace;

		// Token: 0x04001E31 RID: 7729
		private DatabaseCopy[] databaseCopies;

		// Token: 0x04001E32 RID: 7730
		private DatabaseCopy[] invalidDatabaseCopies;

		// Token: 0x04001E33 RID: 7731
		private DatabaseCopy[] allDatabaseCopies;

		// Token: 0x04001E34 RID: 7732
		private ADObjectId[] servers;

		// Token: 0x04001E35 RID: 7733
		private KeyValuePair<ADObjectId, int>[] activationPreference;

		// Token: 0x04001E36 RID: 7734
		private KeyValuePair<ADObjectId, EnhancedTimeSpan>[] replayLagTimes;

		// Token: 0x04001E37 RID: 7735
		private KeyValuePair<ADObjectId, EnhancedTimeSpan>[] truncationLagTimes;

		// Token: 0x04001E38 RID: 7736
		private ReplicationType replicationType = ReplicationType.Unknown;

		// Token: 0x04001E39 RID: 7737
		private string mountedOnServer;

		// Token: 0x04001E3A RID: 7738
		private string rpcClientAccessServerFqdn;

		// Token: 0x04001E3B RID: 7739
		private int? workerProcessId;

		// Token: 0x04001E3C RID: 7740
		private string currentSchemaVersion;

		// Token: 0x04001E3D RID: 7741
		private string requestedSchemaVersion;

		// Token: 0x04001E3E RID: 7742
		private MasterType? m_masterType = null;
	}
}
