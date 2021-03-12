using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000097 RID: 151
	internal class DumpsterExpirationEnforcer : SysCleanupEnforcerBase
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0002C594 File Offset: 0x0002A794
		internal DumpsterExpirationEnforcer(MailboxDataForTags mailboxDataForTags, SysCleanupSubAssistant sysCleanupSubAssistant) : base(mailboxDataForTags, sysCleanupSubAssistant)
		{
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0002C59E File Offset: 0x0002A79E
		protected override bool QueryIsEnabled()
		{
			return true;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0002C5A4 File Offset: 0x0002A7A4
		protected override void CollectItemsToExpire()
		{
			this.ResolveDeletionInfo();
			this.ResolveMoveInfo();
			DumpsterExpirationEnforcer.AgeLimitAndAction ageLimitAndAction = new DumpsterExpirationEnforcer.AgeLimitAndAction
			{
				AgeLimit = this.deletionInfo.AgeLimit,
				RetentionAction = RetentionActionType.PermanentlyDelete
			};
			if (!base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				ageLimitAndAction = this.GetAgeLimitAndAction();
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsRoot, ageLimitAndAction);
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsDeletions, ageLimitAndAction);
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsVersions, ageLimitAndAction);
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsPurges, ageLimitAndAction);
				return;
			}
			if (this.moveInfo.IsEnabled)
			{
				if (!this.IsMoveDeletionsToPurgesFlightingEnabled() || this.moveInfo.AgeLimit.Days < this.deletionInfo.AgeLimit.Days)
				{
					ageLimitAndAction = new DumpsterExpirationEnforcer.AgeLimitAndAction
					{
						AgeLimit = this.moveInfo.AgeLimit,
						RetentionAction = RetentionActionType.MoveToArchive
					};
				}
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsDeletions, ageLimitAndAction);
				ageLimitAndAction = new DumpsterExpirationEnforcer.AgeLimitAndAction
				{
					AgeLimit = this.moveInfo.AgeLimit,
					RetentionAction = RetentionActionType.MoveToArchive
				};
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsRoot, ageLimitAndAction);
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsVersions, ageLimitAndAction);
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsPurges, ageLimitAndAction);
				return;
			}
			if (this.IsMoveDeletionsToPurgesFlightingEnabled())
			{
				this.ProcessFolderType(DefaultFolderType.RecoverableItemsDeletions, ageLimitAndAction);
			}
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
		private void ProcessFolderType(DefaultFolderType defaultFolderType, DumpsterExpirationEnforcer.AgeLimitAndAction ageLimitAndAction)
		{
			using (Folder folder = Folder.Bind(base.MailboxDataForTags.MailboxSession, defaultFolderType))
			{
				this.ProcessFolderContents(folder, defaultFolderType, ItemQueryType.None, ageLimitAndAction);
				this.ProcessFolderContents(folder, defaultFolderType, ItemQueryType.Associated, ageLimitAndAction);
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002C730 File Offset: 0x0002A930
		private void ProcessFolderContents(Folder folder, DefaultFolderType folderTypeToCollect, ItemQueryType itemQueryType, DumpsterExpirationEnforcer.AgeLimitAndAction ageLimitAndAction)
		{
			int num = base.FolderItemTypeCount(folder, itemQueryType);
			if (num <= 0)
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, string, ItemQueryType>((long)this.GetHashCode(), "{0}:{1} Folder is Empty of type {2}", this, folder.Id.ObjectId.ToHexEntryId(), itemQueryType);
				return;
			}
			using (QueryResult queryResult = folder.ItemQuery(itemQueryType, null, new SortBy[]
			{
				new SortBy(StoreObjectSchema.LastModifiedTime, SortOrder.Ascending)
			}, DumpsterExpirationEnforcer.PropertyColumns.PropertyDefinitions))
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, 0);
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResult.GetRows(100);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] rawProperties in rows)
					{
						PropertyArrayProxy propertyArrayProxy = new PropertyArrayProxy(DumpsterExpirationEnforcer.PropertyColumns, rawProperties);
						if (!ElcMailboxHelper.Exists(propertyArrayProxy[StoreObjectSchema.LastModifiedTime]))
						{
							DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: Last Modified date is missing. Skipping items from here on.", this);
							flag = true;
							break;
						}
						if (!this.EnlistItem(propertyArrayProxy, folderTypeToCollect, ageLimitAndAction))
						{
							flag = true;
							break;
						}
					}
					base.SysCleanupSubAssistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002C868 File Offset: 0x0002AA68
		private bool IsMoveDeletionsToPurgesFlightingEnabled()
		{
			bool result = false;
			if (base.MailboxDataForTags.MailboxSession != null)
			{
				VariantConfigurationSnapshot configuration = base.MailboxDataForTags.MailboxSession.MailboxOwner.GetConfiguration();
				if (configuration != null)
				{
					result = configuration.Ipaed.MoveDeletionsToPurges.Enabled;
				}
			}
			return result;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		private DumpsterExpirationEnforcer.AgeLimitAndAction GetAgeLimitAndAction()
		{
			DumpsterExpirationEnforcer.AgeLimitAndAction result = new DumpsterExpirationEnforcer.AgeLimitAndAction
			{
				RetentionAction = RetentionActionType.PermanentlyDelete,
				AgeLimit = this.deletionInfo.AgeLimit
			};
			if (this.moveInfo.IsEnabled && this.moveInfo.AgeLimit < result.AgeLimit)
			{
				result.RetentionAction = RetentionActionType.MoveToArchive;
				result.AgeLimit = this.moveInfo.AgeLimit;
			}
			return result;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0002C928 File Offset: 0x0002AB28
		private void ResolveDeletionInfo()
		{
			if (this.deletionInfo != null)
			{
				return;
			}
			this.deletionInfo = new DumpsterExpirationEnforcer.ExpirationInfoPair
			{
				IsEnabled = false,
				AgeLimit = EnhancedTimeSpan.MaxValue
			};
			if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: This user is under litigation hold. This user's dumpster will be skipped.", this);
				this.deletionInfo.IsEnabled = false;
			}
			else
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: This user is not under litigation hold. His dumpster will be processed.", this);
				this.deletionInfo.IsEnabled = true;
			}
			if (base.MailboxDataForTags.ElcUserTagInformation.ADUser.UseDatabaseRetentionDefaults)
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, EnhancedTimeSpan?>((long)this.GetHashCode(), "{0}: UseDatabaseRetentionDefaults is true. The DB retention period of {1} will be used.", this, base.SysCleanupSubAssistant.DatabaseConfig.DumpsterRetentionPeriod);
				this.deletionInfo.AgeLimit = (base.SysCleanupSubAssistant.DatabaseConfig.DumpsterRetentionPeriod ?? EnhancedTimeSpan.MaxValue);
				if (base.SysCleanupSubAssistant.DatabaseConfig.RetainDeletedItemsUntilBackup)
				{
					EnhancedTimeSpan enhancedTimeSpan = base.MailboxDataForTags.UtcNow - base.SysCleanupSubAssistant.DatabaseConfig.LastFullBackup;
					if (this.deletionInfo.AgeLimit < enhancedTimeSpan)
					{
						this.deletionInfo.AgeLimit = enhancedTimeSpan;
					}
					DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, EnhancedTimeSpan, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: DatabaseConfig.RetainDeletedItemsUntilBackup is true. ageLimitFromBackup={1}, nullableAgeLimitForRegularItems is {2}.", this, enhancedTimeSpan, this.deletionInfo.AgeLimit);
				}
			}
			else
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: UseDatabaseRetentionDefaults is false. The mailbox retention period of {1} will be used.", this, base.MailboxDataForTags.ElcUserTagInformation.ADUser.RetainDeletedItemsFor);
				this.deletionInfo.AgeLimit = base.MailboxDataForTags.ElcUserTagInformation.ADUser.RetainDeletedItemsFor;
				if (base.MailboxDataForTags.ElcUserTagInformation.ADUser.RetainDeletedItemsUntilBackup)
				{
					EnhancedTimeSpan enhancedTimeSpan2 = base.MailboxDataForTags.UtcNow - base.SysCleanupSubAssistant.DatabaseConfig.LastFullBackup;
					if (this.deletionInfo.AgeLimit < enhancedTimeSpan2)
					{
						this.deletionInfo.AgeLimit = enhancedTimeSpan2;
					}
					DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, EnhancedTimeSpan, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: Mailbox.RetainDeletedItemsUntilBackup is true. ageLimitFromBackup={1}, nullableAgeLimitForRegularItems is {2}.", this, enhancedTimeSpan2, this.deletionInfo.AgeLimit);
				}
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				TimeSpan config = ElcGlobals.Configuration.GetConfig<TimeSpan>("SingleItemRecoveryRetention");
				if (!config.Equals(TimeSpan.Zero))
				{
					DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, TimeSpan>((long)this.GetHashCode(), "{0}: ElcConfigSchema.Setting.SingleItemRecoveryRetention is {1}. Set the mailbox retention period to that value.", this, config);
					this.deletionInfo.AgeLimit = config;
				}
			}
			base.MailboxDataForTags.StatisticsLogEntry.DeletionAgeLimit = this.deletionInfo.AgeLimit.ToString();
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0002CC08 File Offset: 0x0002AE08
		private void ResolveMoveInfo()
		{
			if (this.moveInfo != null)
			{
				return;
			}
			this.moveInfo = new DumpsterExpirationEnforcer.ExpirationInfoPair
			{
				IsEnabled = false,
				AgeLimit = EnhancedTimeSpan.MaxValue
			};
			if (base.MailboxDataForTags.MailboxSession.MailboxOwner.MailboxInfo.IsArchive || base.MailboxDataForTags.MailboxSession.MailboxOwner.GetArchiveMailbox() == null)
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: ResolveMoveInfo: No archive exists for this user. IsEnabled is false.", this);
				return;
			}
			List<AdTagData> policyTagsList = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetPolicyTagsList();
			if (policyTagsList == null)
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: ResolveMoveInfo: No policy linked to this user. IsEnabled is false.", this);
				return;
			}
			foreach (AdTagData adTagData in policyTagsList)
			{
				if (adTagData.Tag.Type == ElcFolderType.RecoverableItems)
				{
					foreach (ContentSetting contentSetting in adTagData.ContentSettings.Values)
					{
						if (contentSetting.RetentionAction == RetentionActionType.MoveToArchive && contentSetting.RetentionEnabled)
						{
							DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, EnhancedTimeSpan?>((long)this.GetHashCode(), "{0}: There is an enabled 'move to archive dumpster' tag for this user. Agelimit is {1}. IsEnabled is true.", this, contentSetting.AgeLimitForRetention);
							this.moveInfo.AgeLimit = contentSetting.AgeLimitForRetention.Value;
							this.moveInfo.IsEnabled = true;
						}
					}
				}
			}
			DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: There are no enabled 'move to archive dumpster' tags for this user. IsEnabled is false.", this);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		private bool EnlistItem(PropertyArrayProxy itemProperties, DefaultFolderType folderTypeToCollect, DumpsterExpirationEnforcer.AgeLimitAndAction ageLimitAndAction)
		{
			VersionedId versionedId = itemProperties[ItemSchema.Id] as VersionedId;
			if (versionedId == null)
			{
				DumpsterExpirationEnforcer.Tracer.TraceError<DumpsterExpirationEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return true;
			}
			DumpsterExpirationEnforcer.AgeLimitAndAction ageLimitAndAction2 = ageLimitAndAction;
			if (!this.IsTimeToDie(itemProperties, ageLimitAndAction2.AgeLimit))
			{
				DumpsterExpirationEnforcer.Tracer.TraceDebug<DumpsterExpirationEnforcer, VersionedId, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: Item {1} newer than minAgeLimitForFolder {2}.", this, versionedId, ageLimitAndAction2.AgeLimit);
				return false;
			}
			if (ageLimitAndAction2.RetentionAction == RetentionActionType.PermanentlyDelete)
			{
				bool disableCalendarLogging = false;
				if (CalendarLoggingHelper.IsCalendarItem(versionedId.ObjectId))
				{
					disableCalendarLogging = this.IsTimeToDie(itemProperties, ageLimitAndAction2.AgeLimit);
				}
				if (base.MailboxDataForTags.AbsoluteLitigationHoldEnabled)
				{
					ItemData itemData = new ItemData(versionedId, (StoreObjectId)itemProperties[StoreObjectSchema.ParentItemId], ItemData.EnforcerType.DumpsterExpirationEnforcer, (int)itemProperties[ItemSchema.Size]);
					base.TagExpirationExecutor.AddToDoomedMoveToPurgesList(itemData, disableCalendarLogging);
					this.itemsMovedToPurges++;
				}
				else if (base.MailboxDataForTags.QueryBasedHoldEnabled)
				{
					ItemData itemData2 = new ItemData(versionedId, (StoreObjectId)itemProperties[StoreObjectSchema.ParentItemId], ItemData.EnforcerType.DumpsterExpirationEnforcer, (int)itemProperties[ItemSchema.Size]);
					base.TagExpirationExecutor.AddToDoomedMoveToDiscoveryHoldsList(itemData2, disableCalendarLogging);
				}
				else
				{
					ItemData itemData3 = new ItemData(versionedId, ItemData.EnforcerType.DumpsterExpirationEnforcer, (int)itemProperties[ItemSchema.Size]);
					base.TagExpirationExecutor.AddToDoomedHardDeleteList(itemData3, disableCalendarLogging);
				}
				this.itemsExpired++;
			}
			else
			{
				base.TagExpirationExecutor.AddToDoomedMoveToArchiveDumpsterList(new ItemData(versionedId, ItemData.EnforcerType.DumpsterExpirationEnforcer, (int)itemProperties[ItemSchema.Size]), folderTypeToCollect);
				this.itemsMoved++;
			}
			return true;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0002CF54 File Offset: 0x0002B154
		private bool IsTimeToDie(PropertyArrayProxy itemProperties, EnhancedTimeSpan ageLimit)
		{
			if (ElcGlobals.ExpireDumpsterRightNow)
			{
				return true;
			}
			object obj = itemProperties[StoreObjectSchema.LastModifiedTime];
			TimeSpan t = base.MailboxDataForTags.UtcNow - (DateTime)((ExDateTime)obj).ToUtc();
			return t >= ageLimit;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002CFA1 File Offset: 0x0002B1A1
		protected override void StartPerfCounterCollect()
		{
			this.itemsExpired = 0;
			this.itemsMoved = 0;
			this.itemsMovedToPurges = 0;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002CFB8 File Offset: 0x0002B1B8
		protected override void StopPerfCounterCollect(long timeElapsed)
		{
			if (!base.IsEnabled)
			{
				ELCPerfmon.TotalSkippedDumpsters.Increment();
			}
			ELCPerfmon.TotalExpiredDumpsterItems.IncrementBy((long)this.itemsExpired);
			ELCPerfmon.TotalMovedDumpsterItems.IncrementBy((long)this.itemsMoved);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByDumpsterExpirationEnforcer += (long)this.itemsExpired;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsArchivedByDumpsterExpirationEnforcer += (long)this.itemsMoved;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsMovedToPurgesByDumpsterExpirationEnforcer += (long)this.itemsMovedToPurges;
			base.MailboxDataForTags.StatisticsLogEntry.DumpsterExpirationEnforcerProcessingTime = timeElapsed;
		}

		// Token: 0x04000456 RID: 1110
		private static readonly Trace Tracer = ExTraceGlobals.DumpsterExpirationEnforcerTracer;

		// Token: 0x04000457 RID: 1111
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000458 RID: 1112
		private static readonly PropertyDefinitionArray PropertyColumns = new PropertyDefinitionArray(new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId
		});

		// Token: 0x04000459 RID: 1113
		private DumpsterExpirationEnforcer.ExpirationInfoPair deletionInfo;

		// Token: 0x0400045A RID: 1114
		private DumpsterExpirationEnforcer.ExpirationInfoPair moveInfo;

		// Token: 0x0400045B RID: 1115
		private int itemsExpired;

		// Token: 0x0400045C RID: 1116
		private int itemsMoved;

		// Token: 0x0400045D RID: 1117
		private int itemsMovedToPurges;

		// Token: 0x02000098 RID: 152
		private class ExpirationInfoPair
		{
			// Token: 0x1700015A RID: 346
			// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0002D0BB File Offset: 0x0002B2BB
			// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0002D0C3 File Offset: 0x0002B2C3
			public bool IsEnabled { get; set; }

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0002D0CC File Offset: 0x0002B2CC
			// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
			public EnhancedTimeSpan AgeLimit { get; set; }
		}

		// Token: 0x02000099 RID: 153
		private struct AgeLimitAndAction
		{
			// Token: 0x1700015C RID: 348
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x0002D0E5 File Offset: 0x0002B2E5
			// (set) Token: 0x060005EB RID: 1515 RVA: 0x0002D0ED File Offset: 0x0002B2ED
			public EnhancedTimeSpan AgeLimit { get; set; }

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060005EC RID: 1516 RVA: 0x0002D0F6 File Offset: 0x0002B2F6
			// (set) Token: 0x060005ED RID: 1517 RVA: 0x0002D0FE File Offset: 0x0002B2FE
			public RetentionActionType RetentionAction { get; set; }
		}
	}
}
