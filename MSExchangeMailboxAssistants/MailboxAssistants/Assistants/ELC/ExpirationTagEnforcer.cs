using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.ELC;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200006B RID: 107
	internal class ExpirationTagEnforcer : TagEnforcerBase
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001AB90 File Offset: 0x00018D90
		internal ExpirationTagEnforcer(MailboxDataForTags mailboxDataForTags, ElcTagSubAssistant elcTagSubAssistant) : base(mailboxDataForTags, elcTagSubAssistant)
		{
			this.tagExpirationExecutor = new TagExpirationExecutor(mailboxDataForTags, elcTagSubAssistant);
			StoreObjectId defaultFolderId = mailboxDataForTags.MailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			if (defaultFolderId != null)
			{
				this.deletedItemsId = defaultFolderId.ProviderLevelItemId;
			}
			this.isEhaCustomer = mailboxDataForTags.ElcUserInformation.ProcessEhaMigratedMessages;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001ABDF File Offset: 0x00018DDF
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Mailbox:" + base.MailboxDataForTags.MailboxSession.MailboxOwner.ToString() + " being processed by ExpirationTagEnforcer.";
			}
			return this.toString;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001AC1C File Offset: 0x00018E1C
		internal override bool IsEnabled()
		{
			if (base.MailboxDataForTags.SuspendExpiration)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Expiration for this user is currently suspended. This user will be skipped.", this);
				return false;
			}
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Expiration for this user is not suspended. This user will be processed.", this);
			Dictionary<Guid, AdTagData> allTags = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetAllTags();
			foreach (AdTagData adTagData in allTags.Values)
			{
				foreach (ContentSetting contentSetting in adTagData.ContentSettings.Values)
				{
					if (contentSetting.RetentionEnabled)
					{
						ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: There are enabled content settings for this user. This user will be processed.", this);
						this.retentionEnabledOnTags = true;
						return true;
					}
				}
			}
			if (this.isEhaCustomer)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: This is an EHA customer. We need to process this for migrated messages even if no retention tags are enabled.", this);
				return true;
			}
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: There are no enabled content settings for this user. This user will not be processed.", this);
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001AD64 File Offset: 0x00018F64
		internal override void Invoke()
		{
			try
			{
				this.StartPerfCounterCollect();
				this.CollectItemsToExpire(base.MailboxDataForTags.MailboxSession);
				this.ExpireItemsAlready();
			}
			finally
			{
				this.StopPerfCounterCollect();
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		private void CollectItemsToExpire(MailboxSession session)
		{
			try
			{
				if (this.isEhaCustomer)
				{
					this.CollectMigratedItemsMarkedForHoldExpiration();
				}
				if (this.retentionEnabledOnTags)
				{
					this.tagExpirationExecutor.CheckReportingStatus();
					this.CollectItemsMarkedForExpiration(session);
					if (session.MailboxOwner != null && !session.MailboxOwner.MailboxInfo.IsArchive && base.MailboxDataForTags.ElcUserTagInformation.ArchivingEnabled && session.MailboxOwner.GetArchiveMailbox() != null)
					{
						this.CollectItemsMarkedForMove(session);
						this.CollectItemsToMoveByDefault(session);
					}
					base.MailboxDataForTags.ElcReporter.SendReportEmail(this.tagExpirationExecutor);
				}
			}
			catch (ObjectNotFoundException)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: No items found. The mailbox was very empty.", this);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001AE68 File Offset: 0x00019068
		private void CollectMigratedItemsMarkedForHoldExpiration()
		{
			base.Assistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: About to collect items for expiration.", this);
			AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(base.MailboxDataForTags.MailboxSession, AllItemsFolderHelper.SupportedSortBy.EhaMigrationExpiryDate, new AllItemsFolderHelper.DoQueryProcessing<bool>(this.MigratedMessageQueryProcessor), ExpirationTagEnforcer.ItemDataColumnsWithHoldExpirationDate);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001AECC File Offset: 0x000190CC
		private bool MigratedMessageQueryProcessor(QueryResult queryResults)
		{
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(ExpirationTagEnforcer.ItemDataColumnsWithHoldExpirationDate);
			ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.EHAMigrationExpiryDate, base.MailboxDataForTags.UtcNow.Date);
			if (queryResults.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
			{
				bool flag = false;
				while (!flag)
				{
					object[][] rows = queryResults.GetRows(100);
					ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
					if (rows.Length <= 0)
					{
						break;
					}
					foreach (object[] array2 in rows)
					{
						if (array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] == null || array2[propertyIndexHolder.EHAMigrationExpiryDateIndex] is PropertyError)
						{
							ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: All items from here onwards would be null hence skipping all the rest.", this);
							flag = true;
							break;
						}
						this.EnlistItem(array2, propertyIndexHolder);
					}
					base.Assistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
			return true;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001AFC4 File Offset: 0x000191C4
		private void EnlistItem(object[] itemProperties, PropertyIndexHolder propertyIndexHolder)
		{
			string text;
			if (!this.PreScreenPassed(itemProperties, propertyIndexHolder, out text))
			{
				return;
			}
			ItemData itemData = new ItemData((VersionedId)itemProperties[propertyIndexHolder.IdIndex], (int)itemProperties[propertyIndexHolder.SizeIndex]);
			this.tagExpirationExecutor.AddToDoomedHardDeleteList(itemData, false);
			this.numberItemsDeletedDueToMigrationExpiryDate++;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001B13C File Offset: 0x0001933C
		private void CollectItemsMarkedForExpiration(MailboxSession session)
		{
			base.Assistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: About to collect items for expiration.", this);
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(ExpirationTagEnforcer.ItemDataColumns);
			AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(session, AllItemsFolderHelper.SupportedSortBy.RetentionDate, delegate(QueryResult queryResults)
			{
				ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.RetentionDate, this.MailboxDataForTags.UtcNow.Date);
				if (queryResults.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
				{
					bool flag = false;
					while (!flag)
					{
						object[][] rows = queryResults.GetRows(100);
						ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
						if (rows.Length <= 0)
						{
							break;
						}
						foreach (object[] array2 in rows)
						{
							if (array2[propertyIndexHolder.RetentionDateIndex] == null || array2[propertyIndexHolder.RetentionDateIndex] is PropertyError)
							{
								flag = true;
								break;
							}
							this.EnlistItem(array2, propertyIndexHolder.PolicyTagIndex, propertyIndexHolder.RetentionDateIndex, propertyIndexHolder);
						}
						this.Assistant.ThrottleStoreCallAndCheckForShutdown(this.MailboxDataForTags.MailboxSession.MailboxOwner);
					}
				}
				return true;
			}, ExpirationTagEnforcer.ItemDataColumns);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001B360 File Offset: 0x00019560
		private void CollectItemsMarkedForMove(MailboxSession session)
		{
			base.Assistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: About to collect items marked for move via archive tag.", this);
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(ExpirationTagEnforcer.ItemDataColumns);
			AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(session, AllItemsFolderHelper.SupportedSortBy.ArchiveDate, delegate(QueryResult queryResults)
			{
				ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ArchiveDate, this.MailboxDataForTags.UtcNow.Date);
				if (queryResults.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
				{
					bool flag = false;
					while (!flag)
					{
						object[][] rows = queryResults.GetRows(100);
						ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, int>((long)this.GetHashCode(), "{0}: GetRows returned {1} items.", this, rows.Length);
						if (rows.Length <= 0)
						{
							break;
						}
						foreach (object[] array2 in rows)
						{
							if (array2[propertyIndexHolder.ArchiveDateIndex] == null || array2[propertyIndexHolder.ArchiveDateIndex] is PropertyError)
							{
								flag = true;
								break;
							}
							if (array2[propertyIndexHolder.RetentionDateIndex] != null && array2[propertyIndexHolder.RetentionDateIndex] is ExDateTime && ((ExDateTime)array2[propertyIndexHolder.RetentionDateIndex]).UniversalTime < this.MailboxDataForTags.UtcNow.Date)
							{
								ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Both retention date and archive date of the item have passed. Then the retention date wins. Skip adding the item to MTA list", this);
							}
							else
							{
								this.EnlistItem(array2, propertyIndexHolder.ArchiveTagIndex, propertyIndexHolder.ArchiveDateIndex, propertyIndexHolder);
							}
						}
						this.Assistant.ThrottleStoreCallAndCheckForShutdown(this.MailboxDataForTags.MailboxSession.MailboxOwner);
					}
				}
				return true;
			}, ExpirationTagEnforcer.ItemDataColumns);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001B3D8 File Offset: 0x000195D8
		private void CollectItemsToMoveByDefault(MailboxSession session)
		{
			if (base.MailboxDataForTags.ElcUserTagInformation.MinDefaultMovePeriod == null)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: MinDefaultMovePeriod is null. No move to archive default tag found.", this);
				return;
			}
			ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, EnhancedTimeSpan>((long)this.GetHashCode(), "{0}: MinDefaultMovePeriod is {1}. About to collect items to move based on default archive tag.", this, base.MailboxDataForTags.ElcUserTagInformation.MinDefaultMovePeriod.Value);
			PropertyIndexHolder propertyIndexHolder = new PropertyIndexHolder(ExpirationTagEnforcer.DefaultMoveQueryColumns);
			base.Assistant.ThrowIfShuttingDown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
			using (Folder noArchiveTagSearchFolder = new SearchFolderManager(base.MailboxDataForTags.MailboxSession).GetNoArchiveTagSearchFolder())
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, int>((long)this.GetHashCode(), "{0}: Created the noArchiveTagSearchFolder. Item count: {1}", this, noArchiveTagSearchFolder.ItemCount);
				if (noArchiveTagSearchFolder.ItemCount <= 0)
				{
					ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: The search folder is empty. There are no items without an archive tag. Quit.", this);
				}
				else
				{
					using (QueryResult queryResult = noArchiveTagSearchFolder.ItemQuery(ItemQueryType.None, null, new SortBy[]
					{
						new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
					}, ExpirationTagEnforcer.DefaultMoveQueryColumns))
					{
						ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, ItemSchema.ReceivedTime, base.MailboxDataForTags.UtcNow.Subtract(base.MailboxDataForTags.ElcUserTagInformation.MinDefaultMovePeriod.Value));
						if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
						{
							ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Found at least one item that satifies the age and archive tag criteria.", this);
							foreach (object[] array in queryResult.Enumerator(100))
							{
								if (array[propertyIndexHolder.RetentionDateIndex] != null && array[propertyIndexHolder.RetentionDateIndex] is ExDateTime && ((ExDateTime)array[propertyIndexHolder.RetentionDateIndex]).UniversalTime < base.MailboxDataForTags.UtcNow.Date)
								{
									ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Both retention date and archive date of the item have passed. Then the retention date wins. Skip adding the item to MTA list", this);
								}
								else
								{
									this.EvaluateAndEnlistItem(array, propertyIndexHolder);
								}
							}
						}
					}
					base.Assistant.ThrottleStoreCallAndCheckForShutdown(base.MailboxDataForTags.MailboxSession.MailboxOwner);
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001B678 File Offset: 0x00019878
		private void EnlistItem(object[] itemProperties, int policyIndex, int dateIndex, PropertyIndexHolder propertyIndexHolder)
		{
			string messageClass;
			if (!this.PreScreenPassed(itemProperties, propertyIndexHolder, out messageClass))
			{
				return;
			}
			if (itemProperties[dateIndex] == null || itemProperties[dateIndex] is PropertyError)
			{
				ExpirationTagEnforcer.Tracer.TraceError<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Item has null Retention Date. Skipping it.", this);
				return;
			}
			Guid value = ElcMailboxHelper.GetGuidFromBytes(itemProperties[policyIndex], new Guid?(Guid.Empty), false, (VersionedId)itemProperties[propertyIndexHolder.IdIndex]).Value;
			if (value.Equals(Guid.Empty))
			{
				ExpirationTagEnforcer.Tracer.TraceError<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Item has no retention tag. Skipping it.", this);
				return;
			}
			ContentSetting retentionEnabledSettingForTag = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetRetentionEnabledSettingForTag(value, messageClass);
			if (retentionEnabledSettingForTag == null)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Could not find a Content Setting for this item. Skipping it.", this);
				return;
			}
			Dictionary<Guid, AdTagData> allTags = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetAllTags();
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
			foreach (KeyValuePair<Guid, AdTagData> keyValuePair in allTags)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value.Tag.Name);
			}
			this.tagExpirationExecutor.AddToReportAndDoomedList(itemProperties, propertyIndexHolder, retentionEnabledSettingForTag, dictionary, ItemData.EnforcerType.ExpirationTagEnforcer, false);
			if (((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetTag(value).Tag.Type == ElcFolderType.Personal)
			{
				if (retentionEnabledSettingForTag.RetentionAction == RetentionActionType.MoveToArchive)
				{
					this.numberItemsMovedByPersonal++;
					return;
				}
				this.numberItemsExpiredByPersonal++;
				return;
			}
			else
			{
				if (((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetTag(value).Tag.Type != ElcFolderType.All)
				{
					this.numberItemsExpiredBySystem++;
					return;
				}
				if (retentionEnabledSettingForTag.RetentionAction == RetentionActionType.MoveToArchive)
				{
					this.numberItemsMovedByDefault++;
					return;
				}
				this.numberItemsExpiredByDefault++;
				return;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001B870 File Offset: 0x00019A70
		private void EvaluateAndEnlistItem(object[] itemProperties, PropertyIndexHolder propertyIndexHolder)
		{
			string text;
			if (!this.PreScreenPassed(itemProperties, propertyIndexHolder, out text))
			{
				return;
			}
			ContentSetting retentionEnabledSettingForTag = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetRetentionEnabledSettingForTag(((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).DefaultArchiveAdTag, text);
			if (retentionEnabledSettingForTag == null)
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: Could not find a default archive Content Setting for this item. Skipping it.", this);
				return;
			}
			Dictionary<Guid, AdTagData> allTags = ((ElcUserTagInformation)base.MailboxDataForTags.ElcUserInformation).GetAllTags();
			Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
			foreach (KeyValuePair<Guid, AdTagData> keyValuePair in allTags)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value.Tag.Name);
			}
			if (ExpirationTagEnforcer.IsTimeToDie(itemProperties, retentionEnabledSettingForTag.AgeLimitForRetention.Value, text, propertyIndexHolder, base.MailboxDataForTags.MailboxSession, this.GetHashCode(), this.ToString(), ExpirationTagEnforcer.Tracer, base.MailboxDataForTags.UtcNow))
			{
				ExpirationTagEnforcer.Tracer.TraceDebug<ExpirationTagEnforcer, string>((long)this.GetHashCode(), "{0}: Item of class {1} is old enough to die. Sniff.", this, text);
				this.tagExpirationExecutor.AddToReportAndDoomedList(itemProperties, propertyIndexHolder, retentionEnabledSettingForTag, dictionary, ItemData.EnforcerType.ExpirationTagEnforcer, false);
				this.numberItemsMovedByDefault++;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001B9C0 File Offset: 0x00019BC0
		private bool PreScreenPassed(object[] itemProperties, PropertyIndexHolder propertyIndexHolder, out string itemClass)
		{
			itemClass = null;
			VersionedId versionedId = itemProperties[propertyIndexHolder.IdIndex] as VersionedId;
			if (versionedId == null)
			{
				ExpirationTagEnforcer.Tracer.TraceError<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: We could not get id of this item. Skipping it.", this);
				return false;
			}
			StoreObjectId storeObjectId = itemProperties[propertyIndexHolder.ParentItemIdIndex] as StoreObjectId;
			if (storeObjectId == null)
			{
				ExpirationTagEnforcer.Tracer.TraceError<ExpirationTagEnforcer>((long)this.GetHashCode(), "{0}: We could not get parent id of this item. Skipping it.", this);
				return false;
			}
			if (base.MailboxDataForTags.CorruptItemList.Contains(versionedId.ObjectId))
			{
				ExpirationTagEnforcer.Tracer.TraceError<ExpirationTagEnforcer, VersionedId>((long)this.GetHashCode(), "{0}: The item {1} is in the corrupt list. Skipping it.", this, versionedId);
				return false;
			}
			itemClass = (itemProperties[propertyIndexHolder.ItemClassIndex] as string);
			itemClass = ((itemClass == null) ? string.Empty : itemClass.ToLower(CultureInfo.InvariantCulture));
			return TagAssistantHelper.IsRetainableItem(itemClass) && (!TagAssistantHelper.IsConflictableItem(itemClass, storeObjectId.ProviderLevelItemId, this.deletedItemsId) || base.ElcTagSubAssistant.ELCAssistantCalendarTaskRetentionEnabled);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001BAAE File Offset: 0x00019CAE
		private void ExpireItemsAlready()
		{
			this.tagExpirationExecutor.ExecuteTheDoomed();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001BABC File Offset: 0x00019CBC
		internal static bool IsTimeToDie(object[] itemProperties, EnhancedTimeSpan ageLimit, string itemClass, PropertyIndexHolder propertyIndexHolder, MailboxSession session, int hashCode, string enforcerName, Trace TracerHandle, DateTime Now)
		{
			DefaultFolderType defaultFolderType = DefaultFolderType.None;
			if (((StoreObjectId)itemProperties[propertyIndexHolder.ParentItemIdIndex]).Equals(session.GetDefaultFolderId(DefaultFolderType.DeletedItems)))
			{
				TracerHandle.TraceDebug<string, string>((long)hashCode, "{0}: Item of class {1} is in the Deleted Items folder.", enforcerName, itemClass);
				defaultFolderType = DefaultFolderType.DeletedItems;
			}
			ItemStartDateCalculator itemStartDateCalculator = new ItemStartDateCalculator(propertyIndexHolder, defaultFolderType.ToString(), defaultFolderType, session, ExpirationTagEnforcer.Tracer);
			DateTime startDate = itemStartDateCalculator.GetStartDate((VersionedId)itemProperties[propertyIndexHolder.IdIndex], itemClass, itemProperties);
			TracerHandle.TraceDebug((long)hashCode, "{0}: Item class: {1}. Start date is {2}. Applicable age limit is {3}", new object[]
			{
				enforcerName,
				itemClass,
				startDate,
				ageLimit
			});
			return !(startDate == DateTime.MinValue) && Now.Subtract(startDate) > ageLimit;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001BB7A File Offset: 0x00019D7A
		private void StartPerfCounterCollect()
		{
			this.numberItemsExpiredByDefault = 0;
			this.numberItemsExpiredByPersonal = 0;
			this.numberItemsExpiredBySystem = 0;
			this.numberItemsMovedByDefault = 0;
			this.numberItemsMovedByPersonal = 0;
			this.numberItemsDeletedDueToMigrationExpiryDate = 0;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		private void StopPerfCounterCollect()
		{
			ELCPerfmon.TotalItemsExpiredByDefaultExpiryTag.IncrementBy((long)(this.numberItemsExpiredByDefault + this.numberItemsExpiredBySystem));
			ELCPerfmon.TotalItemsExpiredByPersonalExpiryTag.IncrementBy((long)this.numberItemsExpiredByPersonal);
			ELCPerfmon.TotalItemsMovedByDefaultArchiveTag.IncrementBy((long)this.numberItemsMovedByDefault);
			ELCPerfmon.TotalItemsMovedByPersonalArchiveTag.IncrementBy((long)this.numberItemsMovedByPersonal);
			ELCPerfmon.TotalMigratedItemsDeletedDueToItemAgeBased.IncrementBy((long)this.numberItemsDeletedDueToMigrationExpiryDate);
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByDefaultTag += (long)this.numberItemsExpiredByDefault;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedByPersonalTag += (long)this.numberItemsExpiredByPersonal;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsDeletedBySystemTag += (long)this.numberItemsExpiredBySystem;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsArchivedByDefaultTag += (long)this.numberItemsMovedByDefault;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfItemsArchivedByPersonalTag += (long)this.numberItemsMovedByPersonal;
			base.MailboxDataForTags.StatisticsLogEntry.NumberOfMigratedItemsDeletedDueToMigrationExpiryDate += (long)this.numberItemsDeletedDueToMigrationExpiryDate;
		}

		// Token: 0x040002FF RID: 767
		private readonly bool isEhaCustomer;

		// Token: 0x04000300 RID: 768
		private static readonly Trace Tracer = ExTraceGlobals.ExpirationTagEnforcerTracer;

		// Token: 0x04000301 RID: 769
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x04000302 RID: 770
		private static readonly PropertyDefinition[] ItemDataColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			StoreObjectSchema.PolicyTag,
			ItemSchema.RetentionDate,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.ArchiveTag,
			ItemSchema.ArchiveDate,
			ItemSchema.Size,
			MessageItemSchema.SenderDisplayName,
			ItemSchema.Subject,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.ParentDisplayName,
			MessageItemSchema.MessageToMe,
			MessageItemSchema.MessageCcMe,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationId,
			ItemSchema.EHAMigrationExpiryDate
		};

		// Token: 0x04000303 RID: 771
		private static readonly PropertyDefinition[] DefaultMoveQueryColumns = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.ReceivedTime,
			StoreObjectSchema.CreationTime,
			ItemSchema.RetentionDate,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.ArchiveTag,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemInstanceSchema.EndTime,
			TaskSchema.IsTaskRecurring,
			ItemSchema.Size
		};

		// Token: 0x04000304 RID: 772
		private static readonly PropertyDefinition[] ItemDataColumnsWithHoldExpirationDate = new PropertyDefinition[]
		{
			ItemSchema.EHAMigrationExpiryDate,
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			StoreObjectSchema.ParentItemId,
			ItemSchema.Size
		};

		// Token: 0x04000305 RID: 773
		private string toString;

		// Token: 0x04000306 RID: 774
		private TagExpirationExecutor tagExpirationExecutor;

		// Token: 0x04000307 RID: 775
		private byte[] deletedItemsId;

		// Token: 0x04000308 RID: 776
		private int numberItemsExpiredByPersonal;

		// Token: 0x04000309 RID: 777
		private int numberItemsMovedByPersonal;

		// Token: 0x0400030A RID: 778
		private int numberItemsExpiredByDefault;

		// Token: 0x0400030B RID: 779
		private int numberItemsMovedByDefault;

		// Token: 0x0400030C RID: 780
		private int numberItemsExpiredBySystem;

		// Token: 0x0400030D RID: 781
		private int numberItemsDeletedDueToMigrationExpiryDate;

		// Token: 0x0400030E RID: 782
		private bool retentionEnabledOnTags;
	}
}
