using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.PushNotifications;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x0200020C RID: 524
	internal class PushNotificationDataHandler
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x000741BC File Offset: 0x000723BC
		internal void UpdateSubscriptionData(Guid mailboxGuid, PushNotificationSubscription subscriptionContract)
		{
			PushNotificationSubscriptionOption subscriptionOption = subscriptionContract.GetSubscriptionOption();
			this.subscribedMailboxes.AddOrUpdate(mailboxGuid, (byte)subscriptionOption, (Guid key, byte oldValue) => (byte)subscriptionOption);
			if (subscriptionContract.InboxUnreadCount != null && subscriptionContract.InboxUnreadCount > 0L)
			{
				CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
				using (new PushNotificationDataHandler.CachedStateReadLock(cachedState))
				{
					MailboxData mailboxData = (MailboxData)cachedState.State[9];
					if (mailboxData != null)
					{
						using (new PushNotificationDataHandler.CachedStateUpgradeWriteLock(cachedState))
						{
							mailboxData.InboxUnreadCount = subscriptionContract.InboxUnreadCount.Value;
						}
					}
				}
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x000742A4 File Offset: 0x000724A4
		internal void RemoveSubscriptions(Guid mailboxGuid)
		{
			byte b;
			if (!this.subscribedMailboxes.TryRemove(mailboxGuid, out b))
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceError<Guid>((long)this.GetHashCode(), "PushNotificationAssistantAdapter.CleanupSubscription: Failed to clean-up subscription type entry on in-memory dictionary for='{0}'.", mailboxGuid);
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
			using (new PushNotificationDataHandler.CachedStateWriteLock(cachedState))
			{
				cachedState.State[9] = null;
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00074314 File Offset: 0x00072514
		private PushNotificationSubscriptionOption GetMailboxHeaderTableSubscriptionType(Guid mailboxGuid)
		{
			byte result;
			if (!this.subscribedMailboxes.TryGetValue(mailboxGuid, out result))
			{
				ExTraceGlobals.PushNotificationAssistantTracer.TraceError<Guid>((long)this.GetHashCode(), "PushNotificationDataHandler.GetMailboxHeaderTableSubscriptionType: Failed to retrieve subscription type for mailbox Guid:{0}.", mailboxGuid);
				return PushNotificationSubscriptionOption.NoSubscription;
			}
			return (PushNotificationSubscriptionOption)result;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0007434B File Offset: 0x0007254B
		internal bool IsEmailSubscriptionEnabled(Guid mailboxGuid)
		{
			return PushNotificationSubscriptionTableEntry.IsEmailSubscriptionEnabled(this.GetMailboxHeaderTableSubscriptionType(mailboxGuid));
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00074359 File Offset: 0x00072559
		internal bool IsSuppressNotificationsWhenOofEnabled(Guid mailboxGuid)
		{
			return PushNotificationSubscriptionTableEntry.IsSuppressNotificationsWhenOofEnabled(this.GetMailboxHeaderTableSubscriptionType(mailboxGuid));
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00074367 File Offset: 0x00072567
		internal bool IsBackgroundSyncEnabled(Guid mailboxGuid)
		{
			return PushNotificationSubscriptionTableEntry.IsBackgroundSyncEnabled(this.GetMailboxHeaderTableSubscriptionType(mailboxGuid));
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00074378 File Offset: 0x00072578
		internal void OnStart(IDatabaseInfo databaseInfo)
		{
			IEnumerable<IMailboxInformation> mailboxTable = databaseInfo.GetMailboxTable(ClientType.EventBased, new PropertyTagPropertyDefinition[]
			{
				MailboxSchema.PushNotificationSubscriptionType
			});
			foreach (IMailboxInformation mailboxInformation in mailboxTable)
			{
				object mailboxProperty = mailboxInformation.GetMailboxProperty(MailboxSchema.PushNotificationSubscriptionType);
				byte[] array = mailboxProperty as byte[];
				if (!(mailboxProperty is PropertyError) && !PushNotificationSubscriptionTableEntry.IsSubscriptionDisabled(array))
				{
					this.subscribedMailboxes[mailboxInformation.MailboxGuid] = array[0];
				}
			}
			PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions.IncrementBy((long)this.subscribedMailboxes.Count);
			PushNotificationHelper.LogAssistantStartup(databaseInfo.DatabaseName, this.subscribedMailboxes.Count);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00074440 File Offset: 0x00072640
		internal void OnShutdown()
		{
			PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions.IncrementBy((long)(-(long)this.subscribedMailboxes.Count));
			this.subscribedMailboxes.Clear();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00074468 File Offset: 0x00072668
		internal bool IsCacheUpdateRequiredForEmailSubscription(IMapiEvent mapiEvent)
		{
			byte[] array;
			return this.IsEmailSubscriptionEnabled(mapiEvent.MailboxGuid) && !this.TryGetInboxFolderIdFromCache(mapiEvent.MailboxGuid, out array);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00074498 File Offset: 0x00072698
		internal bool IsValidFolderMessageForEmailSubscription(IMapiEvent mapiEvent)
		{
			if (!this.IsEmailSubscriptionEnabled(mapiEvent.MailboxGuid))
			{
				return false;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			byte[] y;
			if (!this.TryGetInboxFolderIdFromCache(mapiEvent.MailboxGuid, out y))
			{
				return false;
			}
			if (!ArrayComparer<byte>.Comparer.Equals(mapiEvent.ItemEntryId, y))
			{
				return false;
			}
			long inboxItemCount;
			using (new PushNotificationDataHandler.CachedStateReadLock(cachedState))
			{
				MailboxData mailboxData = cachedState.State[9] as MailboxData;
				inboxItemCount = mailboxData.InboxItemCount;
				if (inboxItemCount != mapiEvent.ItemCount)
				{
					using (new PushNotificationDataHandler.CachedStateUpgradeWriteLock(cachedState))
					{
						mailboxData.InboxItemCount = mapiEvent.ItemCount;
					}
				}
			}
			return inboxItemCount < mapiEvent.ItemCount && mapiEvent.UnreadItemCount > 0L;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00074578 File Offset: 0x00072778
		internal bool ShouldProcessUnseenEmailEvent(IMailboxSession session, IXSOFactory xsoFactory, IMapiEvent mapiEvent)
		{
			if (!this.IsEmailSubscriptionEnabled(mapiEvent.MailboxGuid))
			{
				return false;
			}
			if (PushNotificationMapiEventAnalyzer.IsIpmFolderContentChangeEvent(mapiEvent))
			{
				return true;
			}
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(session.MailboxGuid);
			using (new PushNotificationDataHandler.CachedStateReadLock(cachedState))
			{
				if (!(cachedState.State[9] is MailboxData))
				{
					using (new PushNotificationDataHandler.CachedStateUpgradeWriteLock(cachedState))
					{
						if (!(cachedState.State[9] is MailboxData))
						{
							MailboxData mailboxData = new MailboxData(session.MailboxGuid);
							if (!mailboxData.LoadData(session, xsoFactory))
							{
								return false;
							}
							cachedState.State[9] = mailboxData;
							return ArrayComparer<byte>.Comparer.Equals(mapiEvent.ParentEntryId, mailboxData.InboxFolderId);
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0007465C File Offset: 0x0007285C
		internal long ReadEmailWatermarkFromCache(Guid mailboxGuid)
		{
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
			long inboxUnreadCount;
			using (new PushNotificationDataHandler.CachedStateReadLock(cachedState))
			{
				MailboxData mailboxData = cachedState.State[9] as MailboxData;
				if (mailboxData == null)
				{
					throw new FailedToResolveInMemoryCacheException(mailboxGuid);
				}
				inboxUnreadCount = mailboxData.InboxUnreadCount;
			}
			return inboxUnreadCount;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x000746BC File Offset: 0x000728BC
		internal void UpdateEmailWatermarkToCache(Guid mailboxGuid, long updatedValue)
		{
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
			using (new PushNotificationDataHandler.CachedStateWriteLock(cachedState))
			{
				MailboxData mailboxData = cachedState.State[9] as MailboxData;
				if (mailboxData == null)
				{
					throw new FailedToResolveInMemoryCacheException(mailboxGuid);
				}
				mailboxData.InboxUnreadCount = updatedValue;
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00074718 File Offset: 0x00072918
		private bool TryGetInboxFolderIdFromCache(Guid mailboxGuid, out byte[] inboxId)
		{
			inboxId = null;
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mailboxGuid);
			bool result;
			using (new PushNotificationDataHandler.CachedStateReadLock(cachedState))
			{
				MailboxData mailboxData = cachedState.State[9] as MailboxData;
				if (mailboxData == null)
				{
					result = false;
				}
				else
				{
					inboxId = mailboxData.InboxFolderId;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000C2A RID: 3114
		private ConcurrentDictionary<Guid, byte> subscribedMailboxes = new ConcurrentDictionary<Guid, byte>();

		// Token: 0x0200020D RID: 525
		private class CachedStateReadLock : IDisposable
		{
			// Token: 0x0600142C RID: 5164 RVA: 0x0007478B File Offset: 0x0007298B
			public CachedStateReadLock(CachedState state)
			{
				if (state == null)
				{
					throw new ArgumentNullException("state");
				}
				this.state = state;
				this.state.LockForRead();
			}

			// Token: 0x0600142D RID: 5165 RVA: 0x000747B3 File Offset: 0x000729B3
			public void Dispose()
			{
				if (this.state != null)
				{
					this.state.ReleaseReaderLock();
					this.state = null;
				}
			}

			// Token: 0x04000C2B RID: 3115
			private CachedState state;
		}

		// Token: 0x0200020E RID: 526
		private class CachedStateWriteLock : IDisposable
		{
			// Token: 0x0600142E RID: 5166 RVA: 0x000747CF File Offset: 0x000729CF
			public CachedStateWriteLock(CachedState state)
			{
				if (state == null)
				{
					throw new ArgumentNullException("state");
				}
				this.state = state;
				this.state.LockForWrite();
			}

			// Token: 0x0600142F RID: 5167 RVA: 0x000747F7 File Offset: 0x000729F7
			public void Dispose()
			{
				if (this.state != null)
				{
					this.state.ReleaseWriterLock();
					this.state = null;
				}
			}

			// Token: 0x04000C2C RID: 3116
			private CachedState state;
		}

		// Token: 0x0200020F RID: 527
		private class CachedStateUpgradeWriteLock : IDisposable
		{
			// Token: 0x06001430 RID: 5168 RVA: 0x00074813 File Offset: 0x00072A13
			public CachedStateUpgradeWriteLock(CachedState state)
			{
				if (state == null)
				{
					throw new ArgumentNullException("state");
				}
				this.state = state;
				this.cookie = this.state.UpgradeToWriterLock();
			}

			// Token: 0x06001431 RID: 5169 RVA: 0x00074841 File Offset: 0x00072A41
			public void Dispose()
			{
				if (this.state != null)
				{
					this.state.DowngradeFromWriterLock(ref this.cookie);
					this.state = null;
				}
			}

			// Token: 0x04000C2D RID: 3117
			private CachedState state;

			// Token: 0x04000C2E RID: 3118
			private LockCookie cookie;
		}
	}
}
