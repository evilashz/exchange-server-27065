using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000BD RID: 189
	internal class CalendarSyncAssistantHelper
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x0003886C File Offset: 0x00036A6C
		private static Dictionary<SynchronizableFolderType, List<FolderRow>> CreateSynchronizableFolderRowsDictionary()
		{
			Dictionary<SynchronizableFolderType, List<FolderRow>> dictionary = new Dictionary<SynchronizableFolderType, List<FolderRow>>();
			foreach (SynchronizableFolderType key in SynchronizableFolderType.All)
			{
				dictionary.Add(key, new List<FolderRow>());
			}
			return dictionary;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000388C4 File Offset: 0x00036AC4
		private static int CompareFolderPairsByLastAttemptedSyncTime(KeyValuePair<SynchronizableFolderType, FolderRow> x, KeyValuePair<SynchronizableFolderType, FolderRow> y)
		{
			return x.Value.LastAttemptedSyncTime.CompareTo(y.Value.LastAttemptedSyncTime);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000388F1 File Offset: 0x00036AF1
		private CalendarSyncPerformanceCountersInstance PerformanceCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000388F9 File Offset: 0x00036AF9
		public CalendarSyncAssistantHelper() : this(null, null)
		{
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00038903 File Offset: 0x00036B03
		public CalendarSyncAssistantHelper(AssistantBase assistant, CalendarSyncPerformanceCountersInstance counters)
		{
			this.assistant = assistant;
			this.perfCounters = counters;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003891C File Offset: 0x00036B1C
		public void ProcessMailbox(MailboxSession mailboxSession, TimeSpan maxSyncTimePerMailbox, Guid activityId, CalendarSyncLogEntry logEntry)
		{
			Dictionary<SynchronizableFolderType, List<FolderRow>> synchronizableFolderRows = this.GetSynchronizableFolderRows(mailboxSession, SynchronizableFolderType.All);
			List<KeyValuePair<SynchronizableFolderType, FolderRow>> list = this.ToList(synchronizableFolderRows);
			list.Sort(new Comparison<KeyValuePair<SynchronizableFolderType, FolderRow>>(CalendarSyncAssistantHelper.CompareFolderPairsByLastAttemptedSyncTime));
			if (this.PerformanceCounters != null)
			{
				this.PerformanceCounters.AverageSubscriptionsPerMailbox.IncrementBy((long)list.Count);
				this.PerformanceCounters.AverageSubscriptionsPerMailboxBase.Increment();
			}
			Deadline deadline = this.GetDeadline(maxSyncTimePerMailbox);
			bool flag = false;
			int num = 0;
			logEntry.MaxTimeoutValue = (long)maxSyncTimePerMailbox.TotalMilliseconds;
			logEntry.NumOfFoldersToSync = list.Count;
			foreach (KeyValuePair<SynchronizableFolderType, FolderRow> keyValuePair in list)
			{
				this.ThrowIfShuttingDown((mailboxSession != null) ? mailboxSession.MailboxOwner : null);
				CalendarSyncFolderOperationLogEntry calendarSyncFolderOperationLogEntry = new CalendarSyncFolderOperationLogEntry
				{
					MailboxGuid = mailboxSession.MailboxGuid,
					FolderId = keyValuePair.Value.FolderId.ToString(),
					FolderType = keyValuePair.Key.FolderTypeName,
					DisplayName = keyValuePair.Value.DisplayName,
					TenantGuid = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid(),
					ActivityId = activityId
				};
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (!keyValuePair.Key.Synchronize(mailboxSession, keyValuePair.Value, deadline, this.PerformanceCounters, calendarSyncFolderOperationLogEntry))
				{
					CalendarSyncAssistantHelper.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.ProcessMailbox: didn't finish sync'ing folder {1} with id {2} for mailbox {3}.", new object[]
					{
						TraceContext.Get(),
						keyValuePair.Value.DisplayName,
						keyValuePair.Value.FolderId,
						this.GetDisplayName(mailboxSession)
					});
				}
				else
				{
					num++;
					this.UpdateTimeBasedPerfCounters(utcNow, keyValuePair.Value);
					calendarSyncFolderOperationLogEntry.IsSyncSuccess = true;
				}
				calendarSyncFolderOperationLogEntry.LastAttemptedSyncTime = keyValuePair.Value.LastAttemptedSyncTime;
				calendarSyncFolderOperationLogEntry.LastSyncSuccessTime = keyValuePair.Value.LastSuccessfulSyncTime;
				logEntry.AddFolderDetailLogline(calendarSyncFolderOperationLogEntry);
				flag = (num == list.Count);
				if (deadline.IsOver && !flag)
				{
					CalendarSyncAssistantHelper.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.ProcessMailbox: deadline expired before sync'ing all folders for mailbox {1}. Processed {2}/{3} folders. MaxSyncTime: {4}", new object[]
					{
						TraceContext.Get(),
						this.GetDisplayName(mailboxSession),
						num,
						list.Count,
						maxSyncTimePerMailbox
					});
					if (mailboxSession != null)
					{
						logEntry.IsDeadlineExpired = true;
						break;
					}
					break;
				}
			}
			logEntry.NumOfFoldersActuallySynced = num;
			this.ProcessExtendedProperties(mailboxSession, synchronizableFolderRows);
			if (list.Count > 0 && !flag)
			{
				throw new SyncAssistantPastDeadlineException(string.Format("Deadline expired before processing all the folders for mailbox {0}. Processed {1}/{2} folders. MaxSyncTime: {3}", new object[]
				{
					this.GetDisplayName(mailboxSession),
					num,
					list.Count,
					maxSyncTimePerMailbox
				}));
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00038C34 File Offset: 0x00036E34
		public void ProcessFolder(MailboxSession mailboxSession, string parameters, CalendarSyncLogEntry logEntry)
		{
			Exception ex = null;
			try
			{
				StoreObjectId folderId = this.StoreObjectIdFromParameters(parameters);
				this.ProcessFolder(mailboxSession, folderId, logEntry);
			}
			catch (CorruptDataException ex2)
			{
				ex = ex2;
			}
			catch (FormatException ex3)
			{
				ex = ex3;
			}
			catch (ArgumentException ex4)
			{
				ex = ex4;
			}
			catch (ObjectNotFoundException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				CalendarSyncAssistantHelper.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.ProcessFolder with parameter {1} for mailbox {2} hit the following exception: {3}", new object[]
				{
					TraceContext.Get(),
					parameters,
					this.GetDisplayName(mailboxSession),
					ex
				});
				logEntry.Exception = ex;
				logEntry.ExceptionType = ex.GetType().FullName;
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00038D00 File Offset: 0x00036F00
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.IsMailboxInteresting: called for mailbox {1}.", TraceContext.Get(), mailboxInformation.DisplayName);
			return !mailboxInformation.IsPublicFolderMailbox() && this.AreExtendedPropertiesInteresting(mailboxInformation.DisplayName, mailboxInformation);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00038D3A File Offset: 0x00036F3A
		public virtual bool IsDeletedItemsFolder(MailboxSession mailboxSession, StoreObjectId parentId)
		{
			return mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems).Equals(parentId);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00038D4C File Offset: 0x00036F4C
		public Dictionary<SynchronizableFolderType, List<FolderRow>> GetSynchronizableFolderRows(MailboxSession mailboxSession, ICollection<SynchronizableFolderType> folderTypes)
		{
			Dictionary<SynchronizableFolderType, List<FolderRow>> synchronizableFolderRows;
			using (Folder rootFolder = this.GetRootFolder(mailboxSession))
			{
				CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.GetSynchronizableFolderRows obtained root folder for mailbox: {1}.", TraceContext.Get(), this.GetDisplayName(mailboxSession));
				synchronizableFolderRows = this.GetSynchronizableFolderRows(rootFolder, folderTypes);
			}
			return synchronizableFolderRows;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00038DAC File Offset: 0x00036FAC
		public void IncrementSubscriptionCount(MailboxSession mailboxSession, SynchronizableFolderType folderType)
		{
			this.IncrementOrResetSubscriptionCount(mailboxSession, folderType, 1);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00038DB7 File Offset: 0x00036FB7
		public void DecrementSubscriptionCount(MailboxSession mailboxSession, SynchronizableFolderType folderType)
		{
			this.IncrementOrResetSubscriptionCount(mailboxSession, folderType, -1);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00038DC4 File Offset: 0x00036FC4
		public void SetSubscriptionCount(MailboxSession mailboxSession, SynchronizableFolderType folderType, int value, bool needsToLoadProperty)
		{
			if (needsToLoadProperty)
			{
				mailboxSession.Mailbox.Load(new PropertyDefinition[]
				{
					folderType.CounterProperty
				});
			}
			mailboxSession.Mailbox[folderType.CounterProperty] = value;
			mailboxSession.Mailbox.Save();
			mailboxSession.Mailbox.Load();
			CalendarSyncAssistantHelper.Tracer.TraceDebug<PropertyDefinition, string, int>((long)this.GetHashCode(), "CalendarSyncAssistantHelper.SetSubscriptionCount for {0}: number of calendar subscriptions for mailbox {1} is {2}.", folderType.CounterProperty, mailboxSession.DisplayName, value);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00038E41 File Offset: 0x00037041
		protected virtual string GetDisplayName(MailboxSession mailboxSession)
		{
			return mailboxSession.MailboxOwner.MailboxInfo.DisplayName;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00038E54 File Offset: 0x00037054
		protected Dictionary<SynchronizableFolderType, List<FolderRow>> GetSynchronizableFolderRows(MailboxSession mailboxSession, object[][] rows)
		{
			Dictionary<SynchronizableFolderType, List<FolderRow>> dictionary = CalendarSyncAssistantHelper.CreateSynchronizableFolderRowsDictionary();
			for (int i = 0; i < rows.Length; i++)
			{
				FolderRow folderRow = FolderRow.FromRawData(rows[i]);
				SynchronizableFolderType synchronizableFolderType = SynchronizableFolderType.FromFolderRow(folderRow);
				if (synchronizableFolderType != null && !this.IsDeletedItemsFolder(mailboxSession, folderRow.ParentItemId))
				{
					StoreObjectId folderId = folderRow.FolderId;
					CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string, PropertyDefinition>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.GetSynchronizableFolderRows identified folder id {1} as a synchronizable folder of type {2}.", TraceContext.Get(), folderId.ToString(), synchronizableFolderType.CounterProperty);
					if (synchronizableFolderType.HasSubscription(mailboxSession, folderId))
					{
						CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.GetSynchronizableFolderRows found a subscription for folder id {1}.", TraceContext.Get(), folderId.ToString());
						dictionary[synchronizableFolderType].Add(folderRow);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00038F08 File Offset: 0x00037108
		protected virtual Folder GetRootFolder(MailboxSession mailboxSession)
		{
			return Folder.Bind(mailboxSession, DefaultFolderType.Root, FolderRow.CalendarSyncAssistantFolderProps);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00038F18 File Offset: 0x00037118
		protected virtual Dictionary<SynchronizableFolderType, List<FolderRow>> GetSynchronizableFolderRows(Folder rootFolder, ICollection<SynchronizableFolderType> desiredTypes)
		{
			Dictionary<SynchronizableFolderType, List<FolderRow>> dictionary = CalendarSyncAssistantHelper.CreateSynchronizableFolderRowsDictionary();
			using (QueryResult queryResult = rootFolder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, FolderRow.CalendarSyncAssistantFolderProps))
			{
				object[][] array = null;
				do
				{
					array = queryResult.GetRows(10000);
					CalendarSyncAssistantHelper.Tracer.TraceDebug<object, int>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.GetSynchronizableFolderRows retrieved {1} folder rows.", TraceContext.Get(), array.Length);
					Dictionary<SynchronizableFolderType, List<FolderRow>> synchronizableFolderRows = this.GetSynchronizableFolderRows((MailboxSession)rootFolder.Session, array);
					foreach (KeyValuePair<SynchronizableFolderType, List<FolderRow>> keyValuePair in synchronizableFolderRows)
					{
						if (desiredTypes.Contains(keyValuePair.Key))
						{
							dictionary[keyValuePair.Key].AddRange(keyValuePair.Value);
						}
					}
				}
				while (array.Length > 0);
			}
			return dictionary;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00039000 File Offset: 0x00037200
		protected bool AreExtendedPropertiesInteresting(string mailboxDisplayName, MailboxInformation mailboxInformation)
		{
			object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.ConsumerSharingCalendarSubscriptionCount);
			object mailboxProperty2 = mailboxInformation.GetMailboxProperty(MailboxSchema.ExternalSharingCalendarSubscriptionCount);
			object mailboxProperty3 = mailboxInformation.GetMailboxProperty(MailboxSchema.InternetCalendarSubscriptionCount);
			object mailboxProperty4 = mailboxInformation.GetMailboxProperty(MailboxSchema.ExternalSharingContactSubscriptionCount);
			bool flag = this.IsExtendedPropertyInteresting(mailboxDisplayName, mailboxProperty) || this.IsExtendedPropertyInteresting(mailboxDisplayName, mailboxProperty2) || this.IsExtendedPropertyInteresting(mailboxDisplayName, mailboxProperty3) || this.IsExtendedPropertyInteresting(mailboxDisplayName, mailboxProperty4);
			object mailboxProperty5 = mailboxInformation.GetMailboxProperty(MailboxSchema.MailboxTypeDetail);
			object mailboxProperty6 = mailboxInformation.GetMailboxProperty(MailboxSchema.MailboxMiscFlags);
			if (mailboxProperty6 != null)
			{
				flag = (flag && (MailboxMiscFlags)mailboxProperty6 != MailboxMiscFlags.CreatedByMove);
			}
			CalendarSyncMailboxInterestingLogEntry calendarSyncMailboxInterestingLogEntry = new CalendarSyncMailboxInterestingLogEntry
			{
				MailboxGuid = mailboxInformation.MailboxGuid,
				IsMailboxInteresting = flag,
				ConsumerCalendarsCount = ((mailboxProperty == null) ? -1 : ((int)mailboxProperty)),
				ExternalCalendarsCount = ((mailboxProperty2 == null) ? -1 : ((int)mailboxProperty2)),
				InternetCalendarsCount = ((mailboxProperty3 == null) ? -1 : ((int)mailboxProperty3)),
				ExternalContactsCount = ((mailboxProperty4 == null) ? -1 : ((int)mailboxProperty4)),
				MailboxType = ((mailboxProperty5 == null) ? StoreMailboxTypeDetail.None : ((StoreMailboxTypeDetail)mailboxProperty5)),
				MailboxFlags = ((mailboxProperty6 == null) ? MailboxMiscFlags.None : ((MailboxMiscFlags)mailboxProperty6))
			};
			AssistantsLog.LogMailboxInterestingEvent(Guid.Empty, "CalendarSyncAssistant", calendarSyncMailboxInterestingLogEntry.FormatCustomData(), mailboxInformation.MailboxGuid);
			return flag;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0003915C File Offset: 0x0003735C
		protected virtual void ProcessExtendedProperties(MailboxSession mailboxSession, Dictionary<SynchronizableFolderType, List<FolderRow>> folders)
		{
			foreach (KeyValuePair<SynchronizableFolderType, List<FolderRow>> folder in folders)
			{
				this.PopulateExtendedPropertyIfNecessary(mailboxSession, folder);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000391AC File Offset: 0x000373AC
		protected virtual Deadline GetDeadline(TimeSpan maxSyncTimePerMailbox)
		{
			return new Deadline(maxSyncTimePerMailbox);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000391B4 File Offset: 0x000373B4
		private void UpdateTimeBasedPerfCounters(ExDateTime beginProcessingTimeStamp, FolderRow folderRow)
		{
			if (this.PerformanceCounters != null)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (folderRow.LastSuccessfulSyncTime != ExDateTime.MinValue)
				{
					this.PerformanceCounters.AverageTimeBetweenSyncs.IncrementBy((utcNow - folderRow.LastSuccessfulSyncTime).Ticks);
					this.PerformanceCounters.AverageTimeBetweenSyncsBase.Increment();
				}
				this.PerformanceCounters.AverageSubscriptionProcessingTime.IncrementBy((utcNow - beginProcessingTimeStamp).Ticks);
				this.PerformanceCounters.AverageSubscriptionProcessingTimeBase.Increment();
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00039248 File Offset: 0x00037448
		private List<KeyValuePair<SynchronizableFolderType, FolderRow>> ToList(Dictionary<SynchronizableFolderType, List<FolderRow>> folders)
		{
			List<KeyValuePair<SynchronizableFolderType, FolderRow>> list = new List<KeyValuePair<SynchronizableFolderType, FolderRow>>();
			foreach (KeyValuePair<SynchronizableFolderType, List<FolderRow>> keyValuePair in folders)
			{
				foreach (FolderRow value in keyValuePair.Value)
				{
					list.Add(new KeyValuePair<SynchronizableFolderType, FolderRow>(keyValuePair.Key, value));
				}
			}
			return list;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000392E8 File Offset: 0x000374E8
		private StoreObjectId StoreObjectIdFromParameters(string parameters)
		{
			StoreObjectId storeObjectId = StoreObjectId.FromHexEntryId(parameters);
			if (!storeObjectId.IsFolderId)
			{
				throw new ArgumentException(string.Format("id {0} isn't the id of a folder", parameters));
			}
			return storeObjectId;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00039318 File Offset: 0x00037518
		private bool IsExtendedPropertyInteresting(string mailboxDisplayName, object subscriptionCount)
		{
			bool result;
			if (subscriptionCount == null)
			{
				CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Mailbox {1} doesn't have extended property. Marking as interesting.", TraceContext.Get(), mailboxDisplayName);
				result = true;
			}
			else
			{
				int num = (int)subscriptionCount;
				CalendarSyncAssistantHelper.Tracer.TraceDebug<object, int, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantType.IsExtendedPropertiesInteresting found {1} folders for mailbox {2}.", TraceContext.Get(), num, mailboxDisplayName);
				result = (num > 0);
			}
			return result;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00039374 File Offset: 0x00037574
		private void ProcessFolder(MailboxSession mailboxSession, StoreObjectId folderId, CalendarSyncLogEntry logEntry)
		{
			CalendarSyncAssistantHelper.Tracer.TraceDebug<object, StoreObjectId, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantType.ProcessFolder will try to process folder {1} from mailbox {2}.", TraceContext.Get(), folderId, this.GetDisplayName(mailboxSession));
			using (Folder folder = Folder.Bind(mailboxSession, folderId))
			{
				SynchronizableFolderType synchronizableFolderType = SynchronizableFolderType.FromFolder(folder);
				CalendarSyncFolderOperationLogEntry calendarSyncFolderOperationLogEntry = new CalendarSyncFolderOperationLogEntry
				{
					MailboxGuid = mailboxSession.MailboxGuid,
					FolderId = folderId.ToString(),
					DisplayName = folder.DisplayName,
					IsOnDemandJob = true,
					TenantGuid = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid()
				};
				if (synchronizableFolderType != null)
				{
					FolderRow folderRow = FolderRow.FromFolder(folder);
					this.ThrowIfShuttingDown((mailboxSession != null) ? mailboxSession.MailboxOwner : null);
					if (!synchronizableFolderType.Synchronize(mailboxSession, folderRow, this.GetDeadline(SynchronizableFolderType.MaxSyncTimePerFolder), this.PerformanceCounters, calendarSyncFolderOperationLogEntry))
					{
						CalendarSyncAssistantHelper.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.ProcessFolder: deadline expired while processing folder {1} from mailbox {2}. MaxSyncTime {3}.", new object[]
						{
							TraceContext.Get(),
							folderId,
							this.GetDisplayName(mailboxSession),
							SynchronizableFolderType.MaxSyncTimePerFolder
						});
						calendarSyncFolderOperationLogEntry.IsDeadlineExpired = true;
					}
					else
					{
						calendarSyncFolderOperationLogEntry.IsSyncSuccess = true;
					}
					calendarSyncFolderOperationLogEntry.FolderType = synchronizableFolderType.FolderTypeName;
				}
				else
				{
					CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string, StoreObjectId>((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.ProcessFolder for mailbox {1}: id {2}. Couldn't find a corresponding SynchronizableFolderType object.", TraceContext.Get(), this.GetDisplayName(mailboxSession), folderId);
					calendarSyncFolderOperationLogEntry.FolderType = "UnknownFolderType";
				}
				logEntry.AddFolderDetailLogline(calendarSyncFolderOperationLogEntry);
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00039508 File Offset: 0x00037708
		private void PopulateExtendedPropertyIfNecessary(MailboxSession mailboxSession, KeyValuePair<SynchronizableFolderType, List<FolderRow>> folder)
		{
			mailboxSession.Mailbox.Load(new PropertyDefinition[]
			{
				folder.Key.CounterProperty
			});
			object obj = mailboxSession.Mailbox.TryGetProperty(folder.Key.CounterProperty);
			if (obj is PropertyError)
			{
				this.SetSubscriptionCount(mailboxSession, folder.Key, folder.Value.Count, false);
				return;
			}
			CalendarSyncAssistantHelper.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: CalendarSyncAssistantHelper.PopulateExtendedPropertyIfNecessary: mailbox {1} already has a value of {2} for {3}.", new object[]
			{
				TraceContext.Get(),
				this.GetDisplayName(mailboxSession),
				obj,
				folder.Key.CounterProperty
			});
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000395B8 File Offset: 0x000377B8
		private object GetSubscriptionCount(MailboxSession mailboxSession, SynchronizableFolderType folderType, bool needsToLoadProperty)
		{
			if (needsToLoadProperty)
			{
				mailboxSession.Mailbox.Load(new PropertyDefinition[]
				{
					folderType.CounterProperty
				});
			}
			return mailboxSession.Mailbox.TryGetProperty(folderType.CounterProperty);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000395F8 File Offset: 0x000377F8
		private void IncrementOrResetSubscriptionCount(MailboxSession mailboxSession, SynchronizableFolderType folderType, int incrementIfPropertyExists)
		{
			object subscriptionCount = this.GetSubscriptionCount(mailboxSession, folderType, true);
			int value;
			if (subscriptionCount is PropertyError)
			{
				value = this.GetSynchronizableFolderRows(mailboxSession, folderType.ToList())[folderType].Count;
			}
			else
			{
				value = (int)subscriptionCount + incrementIfPropertyExists;
			}
			this.SetSubscriptionCount(mailboxSession, folderType, value, false);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00039648 File Offset: 0x00037848
		private void ThrowIfShuttingDown(IExchangePrincipal mailboxOwner)
		{
			if (this.assistant != null && this.assistant.Shutdown)
			{
				CalendarSyncAssistantHelper.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Shutdown called during processing of mailbox '{1}'.", TraceContext.Get(), (mailboxOwner != null) ? mailboxOwner.ToString() : "No mailbox specified");
				throw new ShutdownException();
			}
		}

		// Token: 0x040005BD RID: 1469
		internal const string unknownFolderTypeStr = "UnknownFolderType";

		// Token: 0x040005BE RID: 1470
		private static readonly Trace Tracer = ExTraceGlobals.CalendarSyncAssistantTracer;

		// Token: 0x040005BF RID: 1471
		public static readonly PropertyTagPropertyDefinition[] MailboxTableExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.ConsumerSharingCalendarSubscriptionCount,
			MailboxSchema.ExternalSharingCalendarSubscriptionCount,
			MailboxSchema.InternetCalendarSubscriptionCount,
			MailboxSchema.ExternalSharingContactSubscriptionCount,
			MailboxSchema.MailboxTypeDetail,
			MailboxSchema.MailboxMiscFlags
		};

		// Token: 0x040005C0 RID: 1472
		private readonly CalendarSyncPerformanceCountersInstance perfCounters;

		// Token: 0x040005C1 RID: 1473
		private readonly AssistantBase assistant;
	}
}
