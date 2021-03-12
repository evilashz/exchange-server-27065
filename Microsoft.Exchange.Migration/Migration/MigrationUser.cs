using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000F8 RID: 248
	[Serializable]
	public class MigrationUser : ConfigurableObject
	{
		// Token: 0x06000CE5 RID: 3301 RVA: 0x00036044 File Offset: 0x00034244
		static MigrationUser()
		{
			foreach (KeyValuePair<MigrationUserStatusSummary, MigrationUserStatus[]> keyValuePair in MigrationUser.MapFromSummaryToStatus)
			{
				foreach (MigrationUserStatus key in keyValuePair.Value)
				{
					MigrationUser.mapFromStatusToSummary.Add(key, keyValuePair.Key);
				}
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000362D0 File Offset: 0x000344D0
		public MigrationUser() : base(new SimplePropertyBag(MigrationUserSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			base.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			base.ResetChangeTracking();
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x000362FD File Offset: 0x000344FD
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x0003630F File Offset: 0x0003450F
		public new MigrationUserId Identity
		{
			get
			{
				return (MigrationUserId)this[MigrationUserSchema.Identity];
			}
			internal set
			{
				this[MigrationUserSchema.Identity] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0003631D File Offset: 0x0003451D
		public Guid Guid
		{
			get
			{
				return this.Identity.JobItemGuid;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0003632A File Offset: 0x0003452A
		public string Identifier
		{
			get
			{
				return this.Identity.Id;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x00036337 File Offset: 0x00034537
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00036349 File Offset: 0x00034549
		public MigrationBatchId BatchId
		{
			get
			{
				return (MigrationBatchId)this[MigrationUserSchema.BatchId];
			}
			internal set
			{
				this[MigrationUserSchema.BatchId] = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00036357 File Offset: 0x00034557
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x00036369 File Offset: 0x00034569
		public SmtpAddress EmailAddress
		{
			get
			{
				return (SmtpAddress)this[MigrationUserSchema.EmailAddress];
			}
			internal set
			{
				this[MigrationUserSchema.EmailAddress] = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0003637C File Offset: 0x0003457C
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x0003638E File Offset: 0x0003458E
		public MigrationUserRecipientType RecipientType
		{
			get
			{
				return (MigrationUserRecipientType)this[MigrationUserSchema.RecipientType];
			}
			internal set
			{
				this[MigrationUserSchema.RecipientType] = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x000363A1 File Offset: 0x000345A1
		// (set) Token: 0x06000CF2 RID: 3314 RVA: 0x000363B3 File Offset: 0x000345B3
		public long SkippedItemCount
		{
			get
			{
				return (long)this[MigrationUserSchema.SkippedItemCount];
			}
			internal set
			{
				this[MigrationUserSchema.SkippedItemCount] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x000363C6 File Offset: 0x000345C6
		// (set) Token: 0x06000CF4 RID: 3316 RVA: 0x000363D8 File Offset: 0x000345D8
		public long SyncedItemCount
		{
			get
			{
				return (long)this[MigrationUserSchema.SyncedItemCount];
			}
			internal set
			{
				this[MigrationUserSchema.SyncedItemCount] = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x000363EB File Offset: 0x000345EB
		// (set) Token: 0x06000CF6 RID: 3318 RVA: 0x000363FD File Offset: 0x000345FD
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[MigrationUserSchema.MailboxGuid];
			}
			internal set
			{
				this[MigrationUserSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x00036410 File Offset: 0x00034610
		// (set) Token: 0x06000CF8 RID: 3320 RVA: 0x00036422 File Offset: 0x00034622
		public string MailboxLegacyDN
		{
			get
			{
				return (string)this[MigrationUserSchema.MailboxLegacyDN];
			}
			internal set
			{
				this[MigrationUserSchema.MailboxLegacyDN] = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x00036430 File Offset: 0x00034630
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x00036442 File Offset: 0x00034642
		public Guid RequestGuid
		{
			get
			{
				return (Guid)this[MigrationUserSchema.MRSId];
			}
			internal set
			{
				this[MigrationUserSchema.MRSId] = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x00036455 File Offset: 0x00034655
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x00036467 File Offset: 0x00034667
		public DateTime? LastSuccessfulSyncTime
		{
			get
			{
				return (DateTime?)this[MigrationUserSchema.LastSuccessfulSyncTime];
			}
			internal set
			{
				this[MigrationUserSchema.LastSuccessfulSyncTime] = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0003647A File Offset: 0x0003467A
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0003648C File Offset: 0x0003468C
		public MigrationUserStatus Status
		{
			get
			{
				return (MigrationUserStatus)this[MigrationUserSchema.Status];
			}
			internal set
			{
				this[MigrationUserSchema.Status] = value;
				if (MigrationUser.mapFromStatusToSummary.ContainsKey(value))
				{
					this.StatusSummary = MigrationUser.mapFromStatusToSummary[value];
					return;
				}
				this.StatusSummary = (MigrationUserStatusSummary)value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x000364C5 File Offset: 0x000346C5
		// (set) Token: 0x06000D00 RID: 3328 RVA: 0x000364D7 File Offset: 0x000346D7
		public MigrationUserStatusSummary StatusSummary
		{
			get
			{
				return (MigrationUserStatusSummary)this[MigrationUserSchema.StatusSummary];
			}
			private set
			{
				this[MigrationUserSchema.StatusSummary] = value;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000364EA File Offset: 0x000346EA
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x000364FC File Offset: 0x000346FC
		public DateTime? LastSubscriptionCheckTime
		{
			get
			{
				return (DateTime?)this[MigrationUserSchema.SubscriptionLastChecked];
			}
			internal set
			{
				this[MigrationUserSchema.SubscriptionLastChecked] = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0003650F File Offset: 0x0003470F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MigrationUser.schema;
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00036518 File Offset: 0x00034718
		public override bool Equals(object obj)
		{
			MigrationUser migrationUser = obj as MigrationUser;
			return migrationUser != null && string.Equals(this.Identity.ToString(), migrationUser.Identity.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003654D File Offset: 0x0003474D
		public override int GetHashCode()
		{
			if (this.Identity == null)
			{
				return 0;
			}
			return this.Identity.GetHashCode();
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00036564 File Offset: 0x00034764
		public override string ToString()
		{
			return this.Guid.ToString();
		}

		// Token: 0x040004BD RID: 1213
		internal static readonly Dictionary<MigrationUserStatusSummary, MigrationUserStatus[]> MapFromSummaryToStatus = new Dictionary<MigrationUserStatusSummary, MigrationUserStatus[]>
		{
			{
				MigrationUserStatusSummary.Active,
				new MigrationUserStatus[]
				{
					MigrationUserStatus.Validating,
					MigrationUserStatus.Provisioning,
					MigrationUserStatus.ProvisionUpdating,
					MigrationUserStatus.Queued,
					MigrationUserStatus.Syncing,
					MigrationUserStatus.Completing,
					MigrationUserStatus.Starting,
					MigrationUserStatus.Stopping,
					MigrationUserStatus.Removing
				}
			},
			{
				MigrationUserStatusSummary.Synced,
				new MigrationUserStatus[]
				{
					MigrationUserStatus.Synced,
					MigrationUserStatus.IncrementalSyncing,
					MigrationUserStatus.CompletionSynced
				}
			},
			{
				MigrationUserStatusSummary.Completed,
				new MigrationUserStatus[]
				{
					MigrationUserStatus.Completed
				}
			},
			{
				MigrationUserStatusSummary.Failed,
				new MigrationUserStatus[]
				{
					MigrationUserStatus.Failed,
					MigrationUserStatus.IncrementalFailed,
					MigrationUserStatus.CompletionFailed,
					MigrationUserStatus.CompletedWithWarnings,
					MigrationUserStatus.Corrupted
				}
			},
			{
				MigrationUserStatusSummary.Stopped,
				new MigrationUserStatus[]
				{
					MigrationUserStatus.Stopped,
					MigrationUserStatus.IncrementalStopped
				}
			}
		};

		// Token: 0x040004BE RID: 1214
		internal static readonly PropertyDefinition IdPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemId;

		// Token: 0x040004BF RID: 1215
		internal static readonly PropertyDefinition IdentifierPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemIdentifier;

		// Token: 0x040004C0 RID: 1216
		internal static readonly PropertyDefinition LocalMailboxIdentifierPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemLocalMailboxIdentifier;

		// Token: 0x040004C1 RID: 1217
		internal static readonly PropertyDefinition StatusPropertyDefinition = MigrationBatchMessageSchema.MigrationUserStatus;

		// Token: 0x040004C2 RID: 1218
		internal static readonly PropertyDefinition MailboxGuidPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemMailboxId;

		// Token: 0x040004C3 RID: 1219
		internal static readonly PropertyDefinition BatchIdPropertyDefinition = MigrationBatchMessageSchema.MigrationJobId;

		// Token: 0x040004C4 RID: 1220
		internal static readonly PropertyDefinition BatchNamePropertyDefinition = MigrationBatchMessageSchema.MigrationJobName;

		// Token: 0x040004C5 RID: 1221
		internal static readonly PropertyDefinition RecipientTypePropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemRecipientType;

		// Token: 0x040004C6 RID: 1222
		internal static readonly PropertyDefinition ItemsSkippedPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemItemsSkipped;

		// Token: 0x040004C7 RID: 1223
		internal static readonly PropertyDefinition ItemsSyncedPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemItemsSynced;

		// Token: 0x040004C8 RID: 1224
		internal static readonly PropertyDefinition MailboxLegacyDNPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN;

		// Token: 0x040004C9 RID: 1225
		internal static readonly PropertyDefinition MRSIdPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemMRSId;

		// Token: 0x040004CA RID: 1226
		internal static readonly PropertyDefinition LastSuccessfulSyncTimePropertyDefinition = MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime;

		// Token: 0x040004CB RID: 1227
		internal static readonly PropertyDefinition SubscriptionLastCheckedPropertyDefinition = MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked;

		// Token: 0x040004CC RID: 1228
		internal static readonly PropertyDefinition VersionPropertyDefinition = MigrationBatchMessageSchema.MigrationVersion;

		// Token: 0x040004CD RID: 1229
		internal static readonly PropertyDefinition[] PropertyDefinitions = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			MigrationBatchMessageSchema.MigrationJobId,
			MigrationBatchMessageSchema.MigrationJobItemId,
			MigrationBatchMessageSchema.MigrationJobItemIdentifier,
			MigrationBatchMessageSchema.MigrationJobItemLocalMailboxIdentifier,
			MigrationBatchMessageSchema.MigrationJobItemRecipientType,
			MigrationBatchMessageSchema.MigrationJobItemItemsSkipped,
			MigrationBatchMessageSchema.MigrationJobItemItemsSynced,
			MigrationBatchMessageSchema.MigrationJobItemMailboxId,
			MigrationBatchMessageSchema.MigrationJobItemMailboxLegacyDN,
			MigrationBatchMessageSchema.MigrationJobItemMRSId,
			MigrationBatchMessageSchema.MigrationLastSuccessfulSyncTime,
			MigrationBatchMessageSchema.MigrationUserStatus,
			MigrationBatchMessageSchema.MigrationJobItemSubscriptionLastChecked,
			MigrationBatchMessageSchema.MigrationVersion
		};

		// Token: 0x040004CE RID: 1230
		private static readonly Dictionary<MigrationUserStatus, MigrationUserStatusSummary> mapFromStatusToSummary = new Dictionary<MigrationUserStatus, MigrationUserStatusSummary>();

		// Token: 0x040004CF RID: 1231
		private static MigrationUserSchema schema = ObjectSchema.GetInstance<MigrationUserSchema>();
	}
}
