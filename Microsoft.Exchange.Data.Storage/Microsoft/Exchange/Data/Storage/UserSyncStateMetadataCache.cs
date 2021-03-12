using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E72 RID: 3698
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UserSyncStateMetadataCache
	{
		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x0600803B RID: 32827 RVA: 0x002311D0 File Offset: 0x0022F3D0
		public static UserSyncStateMetadataCache Singleton
		{
			get
			{
				return UserSyncStateMetadataCache.singleton.Value;
			}
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x002311DC File Offset: 0x0022F3DC
		private UserSyncStateMetadataCache()
		{
			this.cache = new ExactTimeoutCache<Guid, UserSyncStateMetadata>(new RemoveItemDelegate<Guid, UserSyncStateMetadata>(this.HandleRemoveItem), null, null, UserSyncStateMetadataCache.CacheSizeLimitEntry.Value, false, CacheFullBehavior.ExpireExisting);
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x00231214 File Offset: 0x0022F414
		public UserSyncStateMetadata Get(MailboxSession mailboxSession, ISyncLogger syncLogger = null)
		{
			if (syncLogger == null)
			{
				syncLogger = TracingLogger.Singleton;
			}
			UserSyncStateMetadata userSyncStateMetadata = null;
			if (!this.cache.TryGetValue(mailboxSession.MailboxGuid, out userSyncStateMetadata))
			{
				syncLogger.TraceDebug<Guid>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadataCache.Get] Cache miss for mailbox {0}", mailboxSession.MailboxGuid);
				userSyncStateMetadata = new UserSyncStateMetadata(mailboxSession);
				lock (this.instanceLock)
				{
					UserSyncStateMetadata result = null;
					if (this.cache.TryGetValue(mailboxSession.MailboxGuid, out result))
					{
						syncLogger.TraceDebug(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadataCache.Get] Second TryGet returned cached value.  Discarding new one.");
						return result;
					}
					this.cache.TryAddSliding(mailboxSession.MailboxGuid, userSyncStateMetadata, UserSyncStateMetadataCache.CacheEntrySlidingLiveTimeEntry.Value);
				}
				return userSyncStateMetadata;
			}
			syncLogger.TraceDebug<SmtpAddress>(ExTraceGlobals.SyncProcessTracer, (long)this.GetHashCode(), "[UserSyncStateMetadataCache.Get] Cache hit for user: {0}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress);
			if (userSyncStateMetadata.MailboxGuid != mailboxSession.MailboxGuid)
			{
				throw new InvalidOperationException(string.Format("[UserSyncStateMetadataCache.Get] cached userState for mailbox {0} was keyed off incorrect mailbox {1}", userSyncStateMetadata.MailboxGuid, mailboxSession.MailboxGuid));
			}
			return userSyncStateMetadata;
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x0023134C File Offset: 0x0022F54C
		public void Clear()
		{
			lock (this.instanceLock)
			{
				this.cache.Clear();
			}
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x00231394 File Offset: 0x0022F594
		private void HandleRemoveItem(Guid key, UserSyncStateMetadata value, RemoveReason removeReason)
		{
			ExTraceGlobals.SyncProcessTracer.TraceDebug<Guid, RemoveReason>((long)this.GetHashCode(), "[UserSyncStateMetadataCache.HandleRemoveItem] Mailbox {0} removed from cache due to reason: {1}", key, removeReason);
		}

		// Token: 0x0400568E RID: 22158
		private object instanceLock = new object();

		// Token: 0x0400568F RID: 22159
		private static readonly IntAppSettingsEntry CacheSizeLimitEntry = new IntAppSettingsEntry("UserSyncStateMetadataCache.CacheSizeLimit", 100000, ExTraceGlobals.SyncProcessTracer);

		// Token: 0x04005690 RID: 22160
		private static readonly TimeSpanAppSettingsEntry CacheEntrySlidingLiveTimeEntry = new TimeSpanAppSettingsEntry("UserSyncStateMetadataCache.CacheEntrySlidingLiveTime", TimeSpanUnit.Hours, TimeSpan.FromHours(3.0), ExTraceGlobals.SyncProcessTracer);

		// Token: 0x04005691 RID: 22161
		private static Lazy<UserSyncStateMetadataCache> singleton = new Lazy<UserSyncStateMetadataCache>(() => new UserSyncStateMetadataCache(), true);

		// Token: 0x04005692 RID: 22162
		private static object staticLock = new object();

		// Token: 0x04005693 RID: 22163
		private ExactTimeoutCache<Guid, UserSyncStateMetadata> cache;
	}
}
