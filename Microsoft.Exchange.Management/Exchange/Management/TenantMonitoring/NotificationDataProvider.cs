using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.TenantMonitoring;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Management.TenantMonitoring
{
	// Token: 0x02000CF1 RID: 3313
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NotificationDataProvider : IConfigDataProvider
	{
		// Token: 0x06007F7D RID: 32637 RVA: 0x00208B80 File Offset: 0x00206D80
		public NotificationDataProvider(ADUser adUser, ADSessionSettings sessionSettings)
		{
			if (adUser == null)
			{
				throw new ArgumentNullException("adUser");
			}
			if (sessionSettings == null)
			{
				throw new ArgumentNullException("sessionSettings");
			}
			this.adUser = adUser;
			this.sessionSettings = sessionSettings;
			this.source = adUser.ToString();
		}

		// Token: 0x1700279D RID: 10141
		// (get) Token: 0x06007F7E RID: 32638 RVA: 0x00208BBE File Offset: 0x00206DBE
		public string Source
		{
			get
			{
				return this.source ?? string.Empty;
			}
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x00208BD0 File Offset: 0x00206DD0
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			NotificationDataProvider.Tracer.TraceDebug<ObjectId>((long)this.GetHashCode(), "Reading notification with identity {0}", identity);
			IConfigurable result;
			using (NotificationDataProvider.NotificationDataStore notificationDataStore = this.CreateDataStore())
			{
				result = notificationDataStore.Read((NotificationIdentity)identity);
			}
			return result;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x00208C34 File Offset: 0x00206E34
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x00208C3C File Offset: 0x00206E3C
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			NotificationDataProvider.Tracer.TraceDebug<QueryFilter>((long)this.GetHashCode(), "Finding notifications matching filter {0}", filter);
			IEnumerable<T> result;
			using (NotificationDataProvider.NotificationDataStore notificationDataStore = this.CreateDataStore())
			{
				result = (IEnumerable<T>)notificationDataStore.Find(filter, null);
			}
			return result;
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x00208C94 File Offset: 0x00206E94
		public void Save(IConfigurable instance)
		{
			Notification notification = NotificationDataProvider.ValidateNotNullAndCastArgument(instance, "instance");
			NotificationDataProvider.Tracer.TraceDebug<Notification>((long)this.GetHashCode(), "Saving notification {0}", notification);
			if (!notification.IsValid)
			{
				throw new ArgumentException("instance is invalid.", "instance");
			}
			switch (notification.ObjectState)
			{
			case ObjectState.New:
				this.NewNotification(notification);
				return;
			case ObjectState.Unchanged:
				return;
			case ObjectState.Changed:
			case ObjectState.Deleted:
				throw new NotSupportedException();
			default:
				return;
			}
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x00208D08 File Offset: 0x00206F08
		public void Delete(IConfigurable instance)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x00208D10 File Offset: 0x00206F10
		internal RecentNotificationEmailTestResult RecentNotificationEmailExists(Notification notification)
		{
			if (notification == null)
			{
				throw new ArgumentNullException("notification");
			}
			RecentNotificationEmailTestResult result;
			using (NotificationDataProvider.NotificationDataStore notificationDataStore = this.CreateDataStore())
			{
				uint num = notificationDataStore.CountNotificationsSentPast24Hours(notification.CreationTimeUtc);
				if (num >= 50U)
				{
					result = RecentNotificationEmailTestResult.DailyCapReached;
				}
				else
				{
					QueryFilter criterion = QueryFilter.AndTogether(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, TenantNotificationMessageSchema.MonitoringNotificationEmailSent, true),
						new ComparisonFilter(ComparisonOperator.GreaterThan, TenantNotificationMessageSchema.MonitoringCreationTimeUtc, notification.CreationTimeUtc.Subtract(TimeSpan.FromHours(24.0))),
						new ComparisonFilter(ComparisonOperator.Equal, TenantNotificationMessageSchema.MonitoringHashCodeForDuplicateDetection, notification.ComputeHashCodeForDuplicateDetection())
					});
					result = (notificationDataStore.Exists(criterion, notification.EventSource) ? RecentNotificationEmailTestResult.PastDay : RecentNotificationEmailTestResult.None);
				}
			}
			return result;
		}

		// Token: 0x06007F85 RID: 32645 RVA: 0x00208DEC File Offset: 0x00206FEC
		private static Notification ValidateNotNullAndCastArgument(IConfigurable argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
			Notification result = argument as Notification;
			if (argument == null)
			{
				throw new ArgumentException(ServerStrings.ObjectMustBeOfType(typeof(Notification).ToString()));
			}
			return result;
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x00208E30 File Offset: 0x00207030
		private void NewNotification(Notification notification)
		{
			using (NotificationDataProvider.NotificationDataStore notificationDataStore = this.CreateDataStore())
			{
				notificationDataStore.Save(notification);
			}
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x00208E68 File Offset: 0x00207068
		private NotificationDataProvider.NotificationDataStore CreateDataStore()
		{
			return new NotificationDataProvider.NotificationDataStore(this.adUser, this.sessionSettings);
		}

		// Token: 0x04003E7C RID: 15996
		private const uint MaxNotificationsPerDay = 50U;

		// Token: 0x04003E7D RID: 15997
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.DataProviderTracer;

		// Token: 0x04003E7E RID: 15998
		private readonly ADUser adUser;

		// Token: 0x04003E7F RID: 15999
		private readonly ADSessionSettings sessionSettings;

		// Token: 0x04003E80 RID: 16000
		private readonly string source;

		// Token: 0x02000CF2 RID: 3314
		private sealed class NotificationDataStore : IDisposable
		{
			// Token: 0x06007F89 RID: 32649 RVA: 0x00208E87 File Offset: 0x00207087
			internal NotificationDataStore(ADUser user, ADSessionSettings sessionSettings)
			{
				this.mailboxSession = NotificationDataProvider.NotificationDataStore.OpenSession(user, sessionSettings);
			}

			// Token: 0x06007F8A RID: 32650 RVA: 0x00208E9C File Offset: 0x0020709C
			~NotificationDataStore()
			{
				this.Dispose(false);
			}

			// Token: 0x06007F8B RID: 32651 RVA: 0x00208ECC File Offset: 0x002070CC
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06007F8C RID: 32652 RVA: 0x00208EDC File Offset: 0x002070DC
			internal void Save(Notification notification)
			{
				this.CheckDisposed();
				using (Folder folder = Folder.Bind(this.mailboxSession, this.GetNotificationsFolderId(notification.EventSource, true)))
				{
					using (MessageItem messageItem = MessageItem.Create(this.mailboxSession, folder.Id))
					{
						messageItem[TenantNotificationMessageSchema.MonitoringUniqueId] = notification.Identity.GetBytes();
						messageItem[TenantNotificationMessageSchema.MonitoringEventInstanceId] = notification.EventInstanceId;
						messageItem[TenantNotificationMessageSchema.MonitoringEventSource] = notification.EventSource;
						messageItem[TenantNotificationMessageSchema.MonitoringEventCategoryId] = notification.EventCategoryId;
						messageItem[TenantNotificationMessageSchema.MonitoringEventTimeUtc] = notification.EventTimeUtc;
						messageItem[TenantNotificationMessageSchema.MonitoringEventEntryType] = notification.EntryType;
						messageItem[TenantNotificationMessageSchema.MonitoringInsertionStrings] = notification.InsertionStrings.ToArray<string>();
						messageItem[TenantNotificationMessageSchema.MonitoringNotificationEmailSent] = notification.EmailSent;
						messageItem[TenantNotificationMessageSchema.MonitoringCreationTimeUtc] = notification.CreationTimeUtc;
						messageItem[TenantNotificationMessageSchema.MonitoringNotificationRecipients] = notification.NotificationRecipients.ToArray();
						messageItem[TenantNotificationMessageSchema.MonitoringHashCodeForDuplicateDetection] = notification.ComputeHashCodeForDuplicateDetection();
						messageItem[TenantNotificationMessageSchema.MonitoringNotificationMessageIds] = notification.NotificationMessageIds.ToArray();
						messageItem[TenantNotificationMessageSchema.MonitoringEventPeriodicKey] = notification.PeriodicKey;
						messageItem[ItemSchema.Subject] = string.Format(CultureInfo.InvariantCulture, "creation-time:{0}; event-source:{1}; event-category-id:{2}; event-id:{3}; email-sent:{4}; periodic-key:{5};", new object[]
						{
							notification.CreationTimeUtc,
							notification.EventSource,
							notification.EventCategoryId,
							notification.EventInstanceId,
							notification.EmailSent,
							notification.PeriodicKey
						});
						PolicyTagHelper.SetRetentionProperties(messageItem, ExDateTime.UtcNow.AddDays(14.0), 14);
						messageItem.Save(SaveMode.NoConflictResolution);
					}
				}
				if (notification.EmailSent)
				{
					using (Folder folder2 = Folder.Bind(this.mailboxSession, this.GetTenantNotificationsFolderId(), NotificationDataProvider.NotificationDataStore.FolderColumns))
					{
						long valueOrDefault = folder2.GetValueOrDefault<long>(TenantNotificationMessageSchema.MonitoringCountOfNotificationsSentInPast24Hours, 0L);
						folder2[TenantNotificationMessageSchema.MonitoringCountOfNotificationsSentInPast24Hours] = NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.IncrementCount(valueOrDefault, notification.CreationTimeUtc);
						folder2.Save(SaveMode.NoConflictResolution);
					}
				}
			}

			// Token: 0x06007F8D RID: 32653 RVA: 0x0020918C File Offset: 0x0020738C
			internal Notification Read(NotificationIdentity identity)
			{
				this.CheckDisposed();
				if (identity == null)
				{
					throw new ArgumentNullException("identity");
				}
				byte[] bytes = identity.GetBytes();
				using (Folder folder = Folder.Bind(this.mailboxSession, this.GetNotificationsFolderId(identity.EventSource, true)))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, NotificationDataProvider.NotificationDataStore.SortByCreationTime, NotificationDataProvider.NotificationDataStore.NotificationColumns))
					{
						queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, TenantNotificationMessageSchema.MonitoringUniqueId, bytes));
						foreach (object[] array in queryResult.GetRows(10))
						{
							if (array != null && array.Length >= NotificationDataProvider.NotificationDataStore.NotificationColumns.Length && !(array[0] is PropertyError) && ArrayComparer<byte>.Comparer.Equals(bytes, (byte[])array[0]))
							{
								return NotificationDataProvider.NotificationDataStore.Deserialize(array[0], array[1], array[2], array[6], array[3], array[4], array[5], array[7], array[8], array[9], array[10], array[11]);
							}
						}
					}
				}
				return null;
			}

			// Token: 0x06007F8E RID: 32654 RVA: 0x002092B0 File Offset: 0x002074B0
			internal IEnumerable<Notification> Find(QueryFilter filter, string eventSource)
			{
				this.CheckDisposed();
				StoreObjectId notificationsFolderId;
				bool includeSubfolders;
				if (string.IsNullOrEmpty(eventSource))
				{
					notificationsFolderId = this.GetTenantNotificationsFolderId();
					includeSubfolders = true;
				}
				else
				{
					notificationsFolderId = this.GetNotificationsFolderId(eventSource, false);
					includeSubfolders = false;
				}
				if (notificationsFolderId == null)
				{
					return NotificationDataProvider.NotificationDataStore.EmptyNotificationArray;
				}
				return this.Find(notificationsFolderId, includeSubfolders, filter);
			}

			// Token: 0x06007F8F RID: 32655 RVA: 0x002092F8 File Offset: 0x002074F8
			internal uint CountNotificationsSentPast24Hours(ExDateTime now)
			{
				this.CheckDisposed();
				uint count;
				using (Folder folder = Folder.Bind(this.mailboxSession, this.GetTenantNotificationsFolderId(), NotificationDataProvider.NotificationDataStore.FolderColumns))
				{
					long valueOrDefault = folder.GetValueOrDefault<long>(TenantNotificationMessageSchema.MonitoringCountOfNotificationsSentInPast24Hours, 0L);
					count = NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.GetCount(valueOrDefault, now);
				}
				return count;
			}

			// Token: 0x06007F90 RID: 32656 RVA: 0x00209358 File Offset: 0x00207558
			internal bool Exists(QueryFilter criterion, string eventSource)
			{
				this.CheckDisposed();
				if (string.IsNullOrEmpty(eventSource))
				{
					throw new ArgumentNullException("eventSource");
				}
				StoreObjectId notificationsFolderId = this.GetNotificationsFolderId(eventSource, false);
				if (notificationsFolderId == null)
				{
					return false;
				}
				bool result;
				using (Folder folder = Folder.Bind(this.mailboxSession, notificationsFolderId))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, NotificationDataProvider.NotificationDataStore.NotificationColumns))
					{
						if (criterion != null)
						{
							queryResult.SeekToCondition(SeekReference.OriginBeginning, criterion, SeekToConditionFlags.AllowExtendedFilters);
						}
						result = (queryResult.GetRows(10).Length > 0);
					}
				}
				return result;
			}

			// Token: 0x06007F91 RID: 32657 RVA: 0x002093F8 File Offset: 0x002075F8
			private static MailboxSession OpenSession(ADUser user, ADSessionSettings sessionSettings)
			{
				return MailboxSession.OpenAsAdmin(ExchangePrincipal.FromADUser(sessionSettings, user, RemotingOptions.AllowCrossSite), CultureInfo.InvariantCulture, "Client=Management;Action=Manage-TenantMonitoringNotifications");
			}

			// Token: 0x06007F92 RID: 32658 RVA: 0x00209414 File Offset: 0x00207614
			private static Notification Deserialize(object uniqueIdOrPropertyError, object eventSourceOrPropertyError, object eventIdOrPropertyError, object eventEntryTypeOrPropertyError, object insertionStringsOrPropertyError, object creationTimeOrPropertyError, object emailSentOrPropertyError, object notificationRecipientsOrPropertyError, object eventCategoryIdOrPropertyError, object eventTimeOrPropertyError, object notificationMessageIdsOrPropertyError, object periodicKeyOrPropertyError)
			{
				object[] array = new object[]
				{
					uniqueIdOrPropertyError,
					eventSourceOrPropertyError,
					eventIdOrPropertyError,
					eventEntryTypeOrPropertyError,
					creationTimeOrPropertyError
				};
				foreach (object obj in array)
				{
					if (obj is PropertyError)
					{
						return null;
					}
				}
				return new Notification(new NotificationIdentity((byte[])uniqueIdOrPropertyError), (string)eventSourceOrPropertyError, (int)eventIdOrPropertyError, (eventCategoryIdOrPropertyError is PropertyError) ? 0 : ((int)eventCategoryIdOrPropertyError), (eventTimeOrPropertyError is PropertyError) ? ((ExDateTime)creationTimeOrPropertyError) : ((ExDateTime)eventTimeOrPropertyError), (EventLogEntryType)eventEntryTypeOrPropertyError, (insertionStringsOrPropertyError is PropertyError) ? null : ((string[])insertionStringsOrPropertyError), !(emailSentOrPropertyError is PropertyError) && (bool)emailSentOrPropertyError, (ExDateTime)creationTimeOrPropertyError, (notificationRecipientsOrPropertyError is PropertyError) ? null : ((string[])notificationRecipientsOrPropertyError), (notificationMessageIdsOrPropertyError is PropertyError) ? null : ((string[])notificationMessageIdsOrPropertyError), (periodicKeyOrPropertyError is PropertyError) ? string.Empty : ((string)periodicKeyOrPropertyError), ObjectState.Unchanged);
			}

			// Token: 0x06007F93 RID: 32659 RVA: 0x00209528 File Offset: 0x00207728
			private IEnumerable<Notification> Find(StoreObjectId searchRootId, bool includeSubfolders, QueryFilter filter)
			{
				if (searchRootId == null)
				{
					throw new ArgumentNullException("searchRootId");
				}
				LinkedList<Notification> linkedList = new LinkedList<Notification>();
				Stack<StoreObjectId> stack = new Stack<StoreObjectId>(100);
				stack.Push(searchRootId);
				while (stack.Count > 0)
				{
					using (Folder folder = Folder.Bind(this.mailboxSession, stack.Pop()))
					{
						if (includeSubfolders)
						{
							using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, NotificationDataProvider.NotificationDataStore.FolderColumns))
							{
								object[][] rows = queryResult.GetRows(100);
								while (rows.Length > 0)
								{
									foreach (object[] array2 in rows)
									{
										if (array2 != null && array2.Length >= NotificationDataProvider.NotificationDataStore.FolderColumns.Length && !(array2[0] is PropertyError))
										{
											stack.Push(((VersionedId)array2[0]).ObjectId);
										}
									}
									rows = queryResult.GetRows(100);
								}
							}
						}
						using (QueryResult queryResult2 = folder.ItemQuery(ItemQueryType.None, filter, NotificationDataProvider.NotificationDataStore.SortByCreationTime, NotificationDataProvider.NotificationDataStore.NotificationColumns))
						{
							object[][] rows2 = queryResult2.GetRows(100);
							while (rows2.Length > 0)
							{
								foreach (object[] array4 in rows2)
								{
									if (array4 != null && array4.Length >= NotificationDataProvider.NotificationDataStore.NotificationColumns.Length)
									{
										Notification notification = NotificationDataProvider.NotificationDataStore.Deserialize(array4[0], array4[1], array4[2], array4[6], array4[3], array4[4], array4[5], array4[7], array4[8], array4[9], array4[10], array4[11]);
										if (notification != null)
										{
											linkedList.AddLast(notification);
										}
									}
								}
								rows2 = queryResult2.GetRows(100);
							}
						}
					}
				}
				return linkedList;
			}

			// Token: 0x06007F94 RID: 32660 RVA: 0x00209720 File Offset: 0x00207920
			private StoreObjectId GetNotificationsFolderId(string eventSource, bool create)
			{
				if (string.IsNullOrEmpty(eventSource))
				{
					throw new ArgumentNullException("eventSource");
				}
				return this.GetOrCreateFolder(this.GetTenantNotificationsFolderId(), eventSource, create);
			}

			// Token: 0x06007F95 RID: 32661 RVA: 0x00209743 File Offset: 0x00207943
			private StoreObjectId GetTenantNotificationsFolderId()
			{
				if (this.tenantNotificationsFolderId == null)
				{
					this.tenantNotificationsFolderId = this.GetOrCreateFolder(this.mailboxSession.GetDefaultFolderId(DefaultFolderType.Root), "TenantNotifications", true);
				}
				return this.tenantNotificationsFolderId;
			}

			// Token: 0x06007F96 RID: 32662 RVA: 0x00209774 File Offset: 0x00207974
			private StoreObjectId GetOrCreateFolder(StoreObjectId parentFolderId, string folderName, bool create)
			{
				if (parentFolderId == null)
				{
					throw new ArgumentNullException("parentFolderId");
				}
				if (string.IsNullOrEmpty(folderName))
				{
					throw new ArgumentNullException("folderName");
				}
				using (Folder folder = Folder.Bind(this.mailboxSession, parentFolderId))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, folderName), null, NotificationDataProvider.NotificationDataStore.FolderColumns))
					{
						foreach (object[] array in queryResult.GetRows(10))
						{
							if (array != null && array.Length >= NotificationDataProvider.NotificationDataStore.FolderColumns.Length && !(array[0] is PropertyError) && !(array[1] is PropertyError) && folderName.Equals((string)array[1], StringComparison.OrdinalIgnoreCase))
							{
								return ((VersionedId)array[0]).ObjectId;
							}
						}
					}
				}
				if (!create)
				{
					return null;
				}
				StoreObjectId storeObjectId;
				using (Folder folder2 = Folder.Create(this.mailboxSession, parentFolderId, StoreObjectType.Folder))
				{
					folder2.DisplayName = folderName;
					folder2.Save();
					folder2.Load();
					storeObjectId = folder2.StoreObjectId;
				}
				return storeObjectId;
			}

			// Token: 0x06007F97 RID: 32663 RVA: 0x002098B0 File Offset: 0x00207AB0
			private void Dispose(bool disposing)
			{
				if (!this.disposed)
				{
					if (disposing && this.mailboxSession != null)
					{
						this.mailboxSession.Dispose();
					}
					this.disposed = true;
				}
			}

			// Token: 0x06007F98 RID: 32664 RVA: 0x002098D7 File Offset: 0x00207AD7
			private void CheckDisposed()
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException("NotificationDataStore");
				}
			}

			// Token: 0x04003E81 RID: 16001
			private const string TenantNotificationsFolderDisplayName = "TenantNotifications";

			// Token: 0x04003E82 RID: 16002
			private const int NotificationTimeToLiveInDays = 14;

			// Token: 0x04003E83 RID: 16003
			private static readonly PropertyDefinition[] NotificationColumns = new PropertyDefinition[]
			{
				TenantNotificationMessageSchema.MonitoringUniqueId,
				TenantNotificationMessageSchema.MonitoringEventSource,
				TenantNotificationMessageSchema.MonitoringEventInstanceId,
				TenantNotificationMessageSchema.MonitoringInsertionStrings,
				TenantNotificationMessageSchema.MonitoringCreationTimeUtc,
				TenantNotificationMessageSchema.MonitoringNotificationEmailSent,
				TenantNotificationMessageSchema.MonitoringEventEntryType,
				TenantNotificationMessageSchema.MonitoringNotificationRecipients,
				TenantNotificationMessageSchema.MonitoringEventCategoryId,
				TenantNotificationMessageSchema.MonitoringEventTimeUtc,
				TenantNotificationMessageSchema.MonitoringNotificationMessageIds,
				TenantNotificationMessageSchema.MonitoringEventPeriodicKey
			};

			// Token: 0x04003E84 RID: 16004
			private static readonly PropertyDefinition[] FolderColumns = new PropertyDefinition[]
			{
				MessageItemSchema.FolderId,
				FolderSchema.DisplayName,
				TenantNotificationMessageSchema.MonitoringCountOfNotificationsSentInPast24Hours
			};

			// Token: 0x04003E85 RID: 16005
			private static readonly SortBy[] SortByCreationTime = new SortBy[]
			{
				new SortBy(TenantNotificationMessageSchema.MonitoringCreationTimeUtc, SortOrder.Descending)
			};

			// Token: 0x04003E86 RID: 16006
			private static readonly Notification[] EmptyNotificationArray = new Notification[0];

			// Token: 0x04003E87 RID: 16007
			private readonly MailboxSession mailboxSession;

			// Token: 0x04003E88 RID: 16008
			private bool disposed;

			// Token: 0x04003E89 RID: 16009
			private StoreObjectId tenantNotificationsFolderId;

			// Token: 0x02000CF3 RID: 3315
			private static class ColumnIndices
			{
				// Token: 0x04003E8A RID: 16010
				internal const int UniqueId = 0;

				// Token: 0x04003E8B RID: 16011
				internal const int EventSource = 1;

				// Token: 0x04003E8C RID: 16012
				internal const int EventInstanceId = 2;

				// Token: 0x04003E8D RID: 16013
				internal const int InsertionStrings = 3;

				// Token: 0x04003E8E RID: 16014
				internal const int CreationTimeUtc = 4;

				// Token: 0x04003E8F RID: 16015
				internal const int NotificationEmailSent = 5;

				// Token: 0x04003E90 RID: 16016
				internal const int EntryType = 6;

				// Token: 0x04003E91 RID: 16017
				internal const int NotificationRecipients = 7;

				// Token: 0x04003E92 RID: 16018
				internal const int EventCategoryId = 8;

				// Token: 0x04003E93 RID: 16019
				internal const int EventTimeUtc = 9;

				// Token: 0x04003E94 RID: 16020
				internal const int NotificationMessageIds = 10;

				// Token: 0x04003E95 RID: 16021
				internal const int PeriodicKey = 11;
			}

			// Token: 0x02000CF4 RID: 3316
			private static class FolderColumnIndices
			{
				// Token: 0x04003E96 RID: 16022
				internal const int FolderId = 0;

				// Token: 0x04003E97 RID: 16023
				internal const int DisplayName = 1;

				// Token: 0x04003E98 RID: 16024
				internal const int CountOfNotificationsInPast24Hours = 2;
			}

			// Token: 0x02000CF5 RID: 3317
			private static class NotificationCountPropertyHelper
			{
				// Token: 0x06007F9A RID: 32666 RVA: 0x002099B8 File Offset: 0x00207BB8
				internal static uint GetCount(long propertyValue, ExDateTime now)
				{
					long num = now.UtcTicks / 600000000L;
					long num2 = (long)((ulong)NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ExtractTimeStamp(propertyValue));
					if (num - num2 >= 1440L)
					{
						return 0U;
					}
					return NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ExtractCounter(propertyValue);
				}

				// Token: 0x06007F9B RID: 32667 RVA: 0x002099F0 File Offset: 0x00207BF0
				internal static long IncrementCount(long propertyValue, ExDateTime now)
				{
					long num = now.UtcTicks / 600000000L;
					long num2 = (long)((ulong)NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ExtractTimeStamp(propertyValue));
					if (num - num2 > 1440L)
					{
						return NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ToBinaryForm(num, 1L);
					}
					uint num3 = NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ExtractCounter(propertyValue);
					return NotificationDataProvider.NotificationDataStore.NotificationCountPropertyHelper.ToBinaryForm(num2, (long)((ulong)(num3 + 1U)));
				}

				// Token: 0x06007F9C RID: 32668 RVA: 0x00209A39 File Offset: 0x00207C39
				private static uint ExtractTimeStamp(long propertyValue)
				{
					return (uint)((ulong)(propertyValue & -4294967296L) >> 32);
				}

				// Token: 0x06007F9D RID: 32669 RVA: 0x00209A4A File Offset: 0x00207C4A
				private static uint ExtractCounter(long propertyValue)
				{
					return (uint)(propertyValue & (long)((ulong)-1));
				}

				// Token: 0x06007F9E RID: 32670 RVA: 0x00209A51 File Offset: 0x00207C51
				private static long ToBinaryForm(long timestamp, long counter)
				{
					return timestamp << 32 | counter;
				}

				// Token: 0x04003E99 RID: 16025
				private const int MinutesInADay = 1440;
			}
		}
	}
}
