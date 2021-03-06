using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200000E RID: 14
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SubscriptionCacheManager
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000038C0 File Offset: 0x00001AC0
		private SubscriptionCacheManager(Guid databaseGuid, Guid systemMailboxGuid, EventHandler<SubscriptionInformation> subscriptionAddedHandler, EventHandler<SubscriptionInformation> subscriptionRemovedHandler)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfGuidEmpty("systemMailboxGuid", systemMailboxGuid);
			SyncUtilities.ThrowIfArgumentNull("subscriptionAddedHandler", subscriptionAddedHandler);
			SyncUtilities.ThrowIfArgumentNull("subscriptionRemovedHandler", subscriptionRemovedHandler);
			this.databaseGuid = databaseGuid;
			this.tokenManager = new TokenManager(ContentAggregationConfig.MaxDispatcherThreads + ContentAggregationConfig.MaxCompletionThreads, ContentAggregationConfig.MaxManualResetEventsInResourcePool);
			this.systemMailboxSessionPool = new SystemMailboxSessionPool(ContentAggregationConfig.MaxDispatcherThreads + ContentAggregationConfig.MaxCompletionThreads, ContentAggregationConfig.MaxMailboxSessionsInResourcePool, databaseGuid, systemMailboxGuid);
			this.coreCacheManager = new SubscriptionCacheManager.SubscriptionCoreCacheManager(databaseGuid);
			this.OnSubscriptionAdded += subscriptionAddedHandler;
			this.OnSubscriptionRemoved += subscriptionRemovedHandler;
			this.subscriptionQueueBuilderTimer = new Timer(new TimerCallback(this.EnumerateSubscriptionsInCacheWorker), null, -1, -1);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000066 RID: 102 RVA: 0x0000398C File Offset: 0x00001B8C
		// (remove) Token: 0x06000067 RID: 103 RVA: 0x000039C4 File Offset: 0x00001BC4
		private event EventHandler<SubscriptionInformation> OnSubscriptionAdded;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000068 RID: 104 RVA: 0x000039FC File Offset: 0x00001BFC
		// (remove) Token: 0x06000069 RID: 105 RVA: 0x00003A34 File Offset: 0x00001C34
		private event EventHandler<SubscriptionInformation> OnSubscriptionRemoved;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003A69 File Offset: 0x00001C69
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003A71 File Offset: 0x00001C71
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003A79 File Offset: 0x00001C79
		internal bool SubscriptionQueueBuildupDone
		{
			get
			{
				return this.subscriptionQueueBuildupDone;
			}
			set
			{
				this.subscriptionQueueBuildupDone = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003A82 File Offset: 0x00001C82
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003A8A File Offset: 0x00001C8A
		internal ISubscriptionCoreCacheManager CoreCacheManager
		{
			get
			{
				return this.coreCacheManager;
			}
			set
			{
				this.coreCacheManager = value;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A94 File Offset: 0x00001C94
		internal static bool TryCreateCacheManager(DatabaseManager databaseManager, EventHandler<SubscriptionInformation> subscriptionAddedHandler, EventHandler<SubscriptionInformation> subscriptionRemovedHandler, bool postponeInitialize, out SubscriptionCacheManager cacheManager)
		{
			SyncUtilities.ThrowIfArgumentNull("databaseManager", databaseManager);
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)99UL, SubscriptionCacheManager.Tracer, "TryCreateCacheManager: Creating cache manager for database {0}.", new object[]
			{
				databaseManager.DatabaseGuid
			});
			cacheManager = new SubscriptionCacheManager(databaseManager.DatabaseGuid, databaseManager.SystemMailboxGuid, subscriptionAddedHandler, subscriptionRemovedHandler);
			return postponeInitialize || cacheManager.TryEnsureInitialized();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B07 File Offset: 0x00001D07
		internal bool TestInitialize()
		{
			return this.TryEnsureInitialized();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B10 File Offset: 0x00001D10
		internal void UpdateCacheMessage(SubscriptionCacheEntry cacheEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("cacheEntry", cacheEntry);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)101UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "UpdateCacheMessage: Updating cache entry in database {0}: {1}.", new object[]
			{
				this.databaseGuid,
				cacheEntry
			});
			Token? token = this.tokenManager.GetToken(cacheEntry.MailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(cacheEntry.MailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(cacheEntry.MailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(cacheEntry.MailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				try
				{
					SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, cacheEntry.MailboxGuid, true);
					if (subscriptionCacheMessage != null)
					{
						using (subscriptionCacheMessage)
						{
							subscriptionCacheMessage.RemoveSubscription(cacheEntry.SubscriptionMessageId);
							subscriptionCacheMessage.AddSubscription(cacheEntry);
							this.SaveCacheMessage(subscriptionCacheMessage);
							goto IL_173;
						}
						goto IL_114;
						IL_173:
						goto IL_1BA;
					}
					IL_114:
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)102UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "UpdateCacheMessage: Could not find cache message in database {0}.", new object[]
					{
						this.databaseGuid
					});
					throw new CacheNotFoundException(this.databaseGuid, cacheEntry.MailboxGuid);
				}
				catch (SerializationException exception)
				{
					Exception ex3 = this.ConvertToCacheException(cacheEntry.MailboxGuid, exception, out reuse);
					throw ex3;
				}
				catch (StorageTransientException exception2)
				{
					Exception ex4 = this.ConvertToCacheException(cacheEntry.MailboxGuid, exception2, out reuse);
					throw ex4;
				}
				catch (StoragePermanentException exception3)
				{
					Exception ex5 = this.ConvertToCacheException(cacheEntry.MailboxGuid, exception3, out reuse);
					throw ex5;
				}
				IL_1BA:;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(cacheEntry.MailboxGuid, token);
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D44 File Offset: 0x00001F44
		internal void AddToCacheMessage(Guid mailboxGuid, Guid tenantGuid, Guid externalDirectoryOrgId, AggregationSubscription subscription, ExDateTime subscriptionListTimestamp)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)103UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), subscription.SubscriptionGuid, mailboxGuid, "AddToCacheMessage: Adding subscription to mailbox in database {0}.", new object[]
			{
				this.databaseGuid
			});
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
				if (subscriptionCacheMessage == null)
				{
					subscriptionCacheMessage = this.CreateCacheMessage(item.Item, mailboxGuid, subscriptionListTimestamp);
				}
				else
				{
					subscriptionCacheMessage.SubscriptionListTimestamp = subscriptionListTimestamp;
				}
				SubscriptionCacheEntry subscriptionCacheEntry = null;
				using (subscriptionCacheMessage)
				{
					subscriptionCacheEntry = subscriptionCacheMessage.AddSubscription(tenantGuid, externalDirectoryOrgId, subscription);
					if (subscriptionCacheEntry != null)
					{
						subscriptionCacheMessage.SubscriptionListTimestamp = subscriptionListTimestamp;
						this.SaveCacheMessage(subscriptionCacheMessage);
						if (subscriptionCacheMessage.IsNew)
						{
							this.UpdateMailboxCountBy(1);
						}
					}
				}
				if (subscriptionCacheEntry != null)
				{
					this.OnSubscriptionAddedHelper(subscriptionCacheEntry, true);
				}
				this.tokenManager.ReleaseToken(mailboxGuid, new Token?(token.Value));
				token = null;
			}
			catch (SerializationException exception)
			{
				Exception ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
				throw ex3;
			}
			catch (StorageTransientException exception2)
			{
				Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
				throw ex4;
			}
			catch (StoragePermanentException exception3)
			{
				Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
				throw ex5;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, new Token?(token.Value));
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003F58 File Offset: 0x00002158
		internal SubscriptionCacheEntry ReadSubscriptionFromCache(Guid mailboxGuid, Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
				if (subscriptionCacheMessage != null)
				{
					using (subscriptionCacheMessage)
					{
						return subscriptionCacheMessage.FindSubscriptionBySubscriptionGuid(subscriptionGuid);
					}
				}
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)104UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), subscriptionGuid, mailboxGuid, "ReadSubscriptionFromCache: Could not find cache message in database {0}.", new object[]
				{
					this.databaseGuid
				});
				throw new CacheNotFoundException(this.databaseGuid, mailboxGuid);
			}
			catch (SerializationException exception)
			{
				Exception ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
				throw ex3;
			}
			catch (StorageTransientException exception2)
			{
				Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
				throw ex4;
			}
			catch (StoragePermanentException exception3)
			{
				Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
				throw ex5;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, token);
				}
			}
			SubscriptionCacheEntry result;
			return result;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000040F8 File Offset: 0x000022F8
		internal SubscriptionCacheEntry ReadSubscriptionFromCache(Guid mailboxGuid, StoreObjectId subscriptionMessageId)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNull("subscriptionMessageId", subscriptionMessageId);
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
				if (subscriptionCacheMessage != null)
				{
					using (subscriptionCacheMessage)
					{
						return subscriptionCacheMessage.FindSubscriptionByMessageId(subscriptionMessageId);
					}
				}
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)105UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "ReadSubscriptionFromCache: Could not find cache message in database {0}.", new object[]
				{
					this.databaseGuid
				});
				throw new CacheNotFoundException(this.databaseGuid, mailboxGuid);
			}
			catch (SerializationException exception)
			{
				Exception ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
				throw ex3;
			}
			catch (StorageTransientException exception2)
			{
				Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
				throw ex4;
			}
			catch (StoragePermanentException exception3)
			{
				Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
				throw ex5;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, token);
				}
			}
			SubscriptionCacheEntry result;
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000429C File Offset: 0x0000249C
		internal void DeleteCacheMessage(Guid mailboxGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)110UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "DeleteCacheMessage: In database {0}.", new object[]
			{
				this.databaseGuid
			});
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				try
				{
					SubscriptionCacheMessage subscriptionCacheMessage = null;
					Exception ex3 = null;
					try
					{
						subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
					}
					catch (SerializationException exception)
					{
						ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
					}
					catch (StorageTransientException exception2)
					{
						ex3 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
					}
					catch (StoragePermanentException exception3)
					{
						ex3 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
					}
					bool flag2 = false;
					if (ex3 != null)
					{
						if (!(ex3 is CacheCorruptException))
						{
							throw ex3;
						}
						flag2 = true;
					}
					if (flag2)
					{
						subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, false);
					}
					if (subscriptionCacheMessage != null)
					{
						using (subscriptionCacheMessage)
						{
							if (!flag2)
							{
								foreach (SubscriptionCacheEntry cacheEntry in subscriptionCacheMessage.Subscriptions)
								{
									this.OnSubscriptionRemovedHelper(cacheEntry);
								}
							}
							this.DeleteCacheMessage(item.Item, subscriptionCacheMessage.Id);
							goto IL_1C8;
						}
						goto IL_187;
						IL_1C8:
						goto IL_1EE;
					}
					IL_187:
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)111UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "DeleteCacheMessage: Could not find cache message.", new object[0]);
					throw new CacheNotFoundException(this.databaseGuid, mailboxGuid);
				}
				catch (StorageTransientException exception4)
				{
					Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception4, out reuse);
					throw ex4;
				}
				catch (StoragePermanentException exception5)
				{
					Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception5, out reuse);
					throw ex5;
				}
				IL_1EE:;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, new Token?(token.Value));
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004590 File Offset: 0x00002790
		internal IEnumerable<SubscriptionCacheEntry> ReadAllSubscriptionsFromCache(Guid mailboxGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)118UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "ReadAllSubscriptionsFromCache: In database {0}.", new object[]
			{
				this.databaseGuid
			});
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
				if (subscriptionCacheMessage != null)
				{
					using (subscriptionCacheMessage)
					{
						return subscriptionCacheMessage.Subscriptions;
					}
				}
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)119UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "ReadAllSubscriptionsFromCache: Could not find cache message in database {0}.", new object[]
				{
					this.databaseGuid
				});
				throw new CacheNotFoundException(this.databaseGuid, mailboxGuid);
			}
			catch (SerializationException exception)
			{
				Exception ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
				throw ex3;
			}
			catch (StorageTransientException exception2)
			{
				Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
				throw ex4;
			}
			catch (StoragePermanentException exception3)
			{
				Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
				throw ex5;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, token);
				}
			}
			IEnumerable<SubscriptionCacheEntry> result;
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004774 File Offset: 0x00002974
		internal bool TryWaitForSubscriptionQueueBuildupToFinish()
		{
			ManualResetEvent manualResetEvent;
			lock (this.subscriptionQueueBuilderLock)
			{
				if (this.subscriptionQueueBuilderTimer == null)
				{
					return false;
				}
				manualResetEvent = new ManualResetEvent(false);
				this.subscriptionQueueBuilderTimer.Dispose(manualResetEvent);
				this.subscriptionQueueBuilderTimer = null;
			}
			manualResetEvent.WaitOne();
			manualResetEvent.Close();
			return true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000047E8 File Offset: 0x000029E8
		internal void Shutdown()
		{
			this.shuttingDown = true;
			this.systemMailboxSessionPool.Shutdown();
			ManualResetEvent manualResetEvent = null;
			lock (this.subscriptionQueueBuilderLock)
			{
				if (this.subscriptionQueueBuilderTimer != null)
				{
					manualResetEvent = new ManualResetEvent(false);
					this.subscriptionQueueBuilderTimer.Dispose(manualResetEvent);
					this.subscriptionQueueBuilderTimer = null;
					ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)120UL, SubscriptionCacheManager.Tracer, "Shutdown: In database {0}, Stopped subscription queue builder timer, waiting on subscription queue builder thread to be done.", new object[]
					{
						this.databaseGuid
					});
				}
			}
			if (manualResetEvent != null)
			{
				manualResetEvent.WaitOne();
				manualResetEvent.Close();
				manualResetEvent = null;
			}
			this.tokenManager.Shutdown();
			this.UpdateMailboxCount(0);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000048B0 File Offset: 0x00002AB0
		internal void UpdateCacheMessageForCrawl(IList<AggregationSubscription> allSubscriptions, Guid mailboxGuid, ExDateTime subscriptionListTimestamp, Guid tenantGuid, Guid externalDirectoryOrgId, out bool hasCacheChanged)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)121UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "UpdateCacheMessageForCrawl called.", new object[0]);
			hasCacheChanged = false;
			IList<AggregationSubscription> list = new List<AggregationSubscription>(allSubscriptions.Count);
			IList<AggregationSubscription> list2 = new List<AggregationSubscription>(allSubscriptions.Count);
			foreach (AggregationSubscription aggregationSubscription in allSubscriptions)
			{
				if (this.ValidateSubscription(aggregationSubscription))
				{
					list.Add(aggregationSubscription);
				}
				else
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)122UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), aggregationSubscription.SubscriptionGuid, mailboxGuid, "UpdateCacheMessageForCrawl: In Database {0} Subscription is not valid. Status {1}, Detailed Status {2}.", new object[]
					{
						this.databaseGuid,
						aggregationSubscription.Status,
						aggregationSubscription.DetailedAggregationStatus
					});
					list2.Add(aggregationSubscription);
				}
			}
			LinkedList<SubscriptionCacheEntry> linkedList = new LinkedList<SubscriptionCacheEntry>();
			LinkedList<SubscriptionCacheEntry> linkedList2 = new LinkedList<SubscriptionCacheEntry>();
			LinkedList<SubscriptionCacheEntry> linkedList3 = new LinkedList<SubscriptionCacheEntry>();
			Token? token = this.tokenManager.GetToken(mailboxGuid);
			if (token == null)
			{
				CacheTransientException ex = this.CreateCacheTransientException(mailboxGuid, Strings.CacheTokenNotAvailable);
				throw ex;
			}
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				this.tokenManager.ReleaseToken(mailboxGuid, token);
				CacheTransientException ex2 = this.CreateCacheTransientException(mailboxGuid, Strings.SystemMailboxSessionNotAvailable);
				throw ex2;
			}
			bool reuse = true;
			try
			{
				SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, mailboxGuid, true);
				if (subscriptionCacheMessage == null)
				{
					if (list.Count == 0)
					{
						return;
					}
					subscriptionCacheMessage = this.CreateCacheMessage(item.Item, mailboxGuid, subscriptionListTimestamp);
				}
				using (subscriptionCacheMessage)
				{
					foreach (SubscriptionCacheEntry subscriptionCacheEntry in subscriptionCacheMessage.Subscriptions)
					{
						bool flag2 = false;
						int i;
						for (i = 0; i < list.Count; i++)
						{
							if (object.Equals(list[i].SubscriptionMessageId, subscriptionCacheEntry.SubscriptionMessageId))
							{
								flag2 = true;
								break;
							}
						}
						if (flag2)
						{
							string text;
							bool flag3 = subscriptionCacheEntry.Validate(list[i], mailboxGuid, true, out text);
							if (flag3)
							{
								ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)123UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), subscriptionCacheEntry.SubscriptionGuid, mailboxGuid, "UpdateCacheMessageForCrawl: In database {0}, Cache entry is now fixed: {1}.", new object[]
								{
									this.databaseGuid,
									text
								});
								linkedList2.AddLast(subscriptionCacheEntry);
								hasCacheChanged = true;
							}
						}
						else
						{
							linkedList.AddLast(subscriptionCacheEntry);
						}
					}
					foreach (SubscriptionCacheEntry subscriptionCacheEntry2 in linkedList)
					{
						ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)124UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), subscriptionCacheEntry2.SubscriptionGuid, subscriptionCacheEntry2.MailboxGuid, "UpdateCacheMessageForCrawl: Removing subscription of type {0} in database {1}.", new object[]
						{
							subscriptionCacheEntry2.SubscriptionType,
							this.databaseGuid
						});
						subscriptionCacheMessage.RemoveSubscription(subscriptionCacheEntry2.SubscriptionMessageId);
						bool wasSubscriptionDeleted = true;
						foreach (AggregationSubscription aggregationSubscription2 in list2)
						{
							if (object.Equals(aggregationSubscription2.SubscriptionMessageId, subscriptionCacheEntry2.SubscriptionMessageId))
							{
								wasSubscriptionDeleted = false;
								break;
							}
						}
						DataAccessLayer.TryLogSubscriptionDeleted(mailboxGuid, tenantGuid, subscriptionCacheEntry2, wasSubscriptionDeleted);
						hasCacheChanged = true;
					}
					foreach (AggregationSubscription aggregationSubscription3 in list)
					{
						if (subscriptionCacheMessage.FindSubscriptionByMessageId(aggregationSubscription3.SubscriptionMessageId) == null)
						{
							ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)125UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), aggregationSubscription3.SubscriptionGuid, mailboxGuid, "UpdateCacheMessageForCrawl: Adding subscription of type {0} in database {1}.", new object[]
							{
								aggregationSubscription3.SubscriptionType,
								this.databaseGuid
							});
							SubscriptionCacheEntry value = subscriptionCacheMessage.AddSubscription(tenantGuid, externalDirectoryOrgId, aggregationSubscription3);
							if (aggregationSubscription3.LastSyncTime == null)
							{
								DataAccessLayer.TryLogSubscriptionCreated(mailboxGuid, tenantGuid, aggregationSubscription3);
							}
							linkedList3.AddLast(value);
							hasCacheChanged = true;
						}
					}
					if (subscriptionCacheMessage.SubscriptionCount != 0)
					{
						subscriptionCacheMessage.SubscriptionListTimestamp = subscriptionListTimestamp;
						this.SaveCacheMessage(subscriptionCacheMessage);
						if (subscriptionCacheMessage.IsNew)
						{
							this.UpdateMailboxCountBy(1);
						}
					}
					else
					{
						ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)126UL, SubscriptionCacheManager.Tracer, Guid.Empty, mailboxGuid, "UpdateCacheMessageForCrawl: Deleting cache message.", new object[0]);
						this.DeleteCacheMessage(item.Item, subscriptionCacheMessage.Id);
						hasCacheChanged = true;
					}
					this.tokenManager.ReleaseToken(mailboxGuid, new Token?(token.Value));
					token = null;
					foreach (SubscriptionCacheEntry cacheEntry in linkedList3)
					{
						this.OnSubscriptionAddedHelper(cacheEntry, true);
					}
					foreach (SubscriptionCacheEntry cacheEntry2 in linkedList2)
					{
						this.OnSubscriptionAddedHelper(cacheEntry2, false);
					}
					foreach (SubscriptionCacheEntry cacheEntry3 in linkedList)
					{
						this.OnSubscriptionRemovedHelper(cacheEntry3);
					}
				}
			}
			catch (SerializationException exception)
			{
				Exception ex3 = this.ConvertToCacheException(mailboxGuid, exception, out reuse);
				throw ex3;
			}
			catch (StorageTransientException exception2)
			{
				Exception ex4 = this.ConvertToCacheException(mailboxGuid, exception2, out reuse);
				throw ex4;
			}
			catch (StoragePermanentException exception3)
			{
				Exception ex5 = this.ConvertToCacheException(mailboxGuid, exception3, out reuse);
				throw ex5;
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
				if (token != null)
				{
					this.tokenManager.ReleaseToken(mailboxGuid, new Token?(token.Value));
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000500C File Offset: 0x0000320C
		internal bool EnumerateSubscriptionsInCache(bool loadSubscriptions, SubscriptionCacheMessageProcessingCallback cacheMessageProcessor)
		{
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)127UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: In database {0}.", new object[]
			{
				this.databaseGuid
			});
			if (this.shuttingDown)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)128UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Aborting due to shutdown.", new object[0]);
				return false;
			}
			bool result = true;
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				result = false;
				return false;
			}
			bool reuse = true;
			try
			{
				int num = 0;
				using (Folder folder = Folder.Bind(item.Item, this.cacheFolderId))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, null, SubscriptionCacheManager.EnumerateCacheMessageProperties))
					{
						object[][] rows = queryResult.GetRows(10000);
						while (rows.Length != 0)
						{
							for (int i = 0; i < rows.Length; i++)
							{
								if (this.shuttingDown)
								{
									ContentAggregationConfig.SyncLogSession.LogError((TSLID)129UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Aborting due to shutdown.", new object[0]);
									result = false;
									return false;
								}
								VersionedId versionedId = (VersionedId)rows[i][0];
								StoreObjectId objectId = versionedId.ObjectId;
								object obj = rows[i][1];
								if (obj == null || obj is PropertyError)
								{
									ContentAggregationConfig.SyncLogSession.LogError((TSLID)130UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Corrupt Cache message detected.", new object[0]);
									this.DeleteCacheMessage(item.Item, objectId);
								}
								else
								{
									num++;
									Guid guid = (Guid)obj;
									ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)131UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, guid, "EnumerateSubscriptionsInCache.", new object[0]);
									try
									{
										Token? token = this.tokenManager.GetToken(guid);
										try
										{
											if (token == null)
											{
												CacheTransientException exception = this.CreateCacheTransientException(guid, Strings.CacheTokenNotAvailable);
												cacheMessageProcessor(guid, null, exception);
											}
											else
											{
												using (SubscriptionCacheMessage subscriptionCacheMessage = this.BindCacheMessage(item.Item, objectId, loadSubscriptions))
												{
													ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)132UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, guid, "EnumerateSubscriptionsInCache: found {0} subscriptions for this mailbox.", new object[]
													{
														subscriptionCacheMessage.SubscriptionCount
													});
													cacheMessageProcessor(guid, subscriptionCacheMessage, null);
												}
											}
										}
										finally
										{
											if (token != null)
											{
												this.tokenManager.ReleaseToken(guid, token);
											}
										}
									}
									catch (SerializationException exception2)
									{
										Exception ex = this.ConvertToCacheException(guid, exception2, out reuse);
										if (ex is CacheCorruptException)
										{
											cacheMessageProcessor(guid, null, ex);
										}
									}
									catch (StorageTransientException exception3)
									{
										Exception exception4 = this.ConvertToCacheException(guid, exception3, out reuse);
										cacheMessageProcessor(guid, null, exception4);
									}
									catch (StoragePermanentException exception5)
									{
										Exception exception6 = this.ConvertToCacheException(guid, exception5, out reuse);
										cacheMessageProcessor(guid, null, exception6);
									}
								}
							}
							rows = queryResult.GetRows(10000);
						}
					}
				}
				this.UpdateMailboxCount(num);
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)133UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Found {0} mailboxes in cache on database {1}.", new object[]
				{
					num,
					this.databaseGuid
				});
			}
			catch (StorageTransientException ex2)
			{
				result = false;
				reuse = SubscriptionCacheManager.IsSessionReusableAfterException(ex2);
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)134UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Encountered: {0}.", new object[]
				{
					ex2
				});
			}
			catch (StoragePermanentException ex3)
			{
				result = false;
				reuse = SubscriptionCacheManager.IsSessionReusableAfterException(ex3);
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)135UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCache: Encountered: {0}.", new object[]
				{
					ex3
				});
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
			}
			return result;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005530 File Offset: 0x00003730
		internal void AddDiagnosticInfoTo(XElement parentElement)
		{
			XElement xelement = new XElement("SystemMailboxSessionPool");
			this.systemMailboxSessionPool.AddDiagnosticInfoTo(xelement);
			parentElement.Add(xelement);
			parentElement.Add(new XElement("totalMailboxesInCache", this.mailboxCount));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005580 File Offset: 0x00003780
		private static bool IsSessionReusableAfterException(Exception e)
		{
			return CacheExceptionUtilities.Instance.IsSessionReusableAfterException(e);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000558D File Offset: 0x0000378D
		private CacheTransientException CreateCacheTransientException(Guid mailboxGuid, LocalizedString exceptionInfo)
		{
			return CacheExceptionUtilities.Instance.CreateCacheTransientException(SubscriptionCacheManager.Tracer, this.GetHashCode(), this.databaseGuid, mailboxGuid, exceptionInfo);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000055AC File Offset: 0x000037AC
		private Exception ConvertToCacheException(Guid mailboxGuid, Exception exception, out bool reuseSession)
		{
			reuseSession = false;
			return CacheExceptionUtilities.Instance.ConvertToCacheException(SubscriptionCacheManager.Tracer, this.GetHashCode(), this.databaseGuid, mailboxGuid, exception, out reuseSession);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000055D0 File Offset: 0x000037D0
		private bool ValidateSubscription(AggregationSubscription subscription)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)154UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: Validating subscription.", new object[0]);
			if (!subscription.IsValid)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)355UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: Subscription.IsValid is false.", new object[0]);
				return false;
			}
			if (object.Equals(subscription.SubscriptionGuid, Guid.Empty))
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)96UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: SubscriptionGuid is invalid.", new object[0]);
				return false;
			}
			if (string.IsNullOrEmpty(subscription.UserLegacyDN))
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)156UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: UserLegacyDN is invalid.", new object[0]);
				return false;
			}
			if (string.IsNullOrEmpty(subscription.PrimaryMailboxUserLegacyDN))
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)157UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: PrimaryMailboxUserLegacyDN is invalid: {0}.", new object[]
				{
					subscription.PrimaryMailboxUserLegacyDN
				});
				return false;
			}
			DateTime? lastSyncTime = subscription.LastSyncTime;
			if (subscription.LastSyncTime != null && (subscription.LastSyncTime.Value < DateTime.MinValue || subscription.LastSyncTime.Value > DateTime.MaxValue))
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)159UL, SubscriptionCacheManager.Tracer, subscription.SubscriptionGuid, subscription.UserLegacyDN, "ValidateSubscription: LastSyncTime is invalid, so we are resetting it before we add it to the cache.", new object[0]);
				subscription.LastSyncTime = null;
			}
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000057AD File Offset: 0x000039AD
		private void DeleteCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId)
		{
			this.coreCacheManager.DeleteCacheMessage(systemMailboxSession, cacheMessageId);
			this.UpdateMailboxCountBy(-1);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000057C3 File Offset: 0x000039C3
		private void SaveCacheMessage(SubscriptionCacheMessage cacheMessage)
		{
			this.coreCacheManager.SaveCacheMessage(cacheMessage);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000057D1 File Offset: 0x000039D1
		private SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, Guid mailboxGuid, bool loadSubscriptions)
		{
			return this.coreCacheManager.BindCacheMessage(systemMailboxSession, this.cacheFolderId, mailboxGuid, loadSubscriptions);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000057E7 File Offset: 0x000039E7
		private SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId, bool loadSubscriptions)
		{
			return this.coreCacheManager.BindCacheMessage(systemMailboxSession, cacheMessageId, loadSubscriptions);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000057F7 File Offset: 0x000039F7
		private SubscriptionCacheMessage CreateCacheMessage(MailboxSession systemMailboxSession, Guid mailboxGuid, ExDateTime subscriptionListTimestamp)
		{
			return this.coreCacheManager.CreateCacheMessage(systemMailboxSession, this.cacheFolderId, mailboxGuid, subscriptionListTimestamp);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005810 File Offset: 0x00003A10
		private bool TryFireOnSubscriptionAddedEvent(SubscriptionCacheEntry cacheEntry, bool fireEventForDisabled)
		{
			bool result;
			try
			{
				this.OnSubscriptionAddedHelper(cacheEntry, fireEventForDisabled);
				result = true;
			}
			catch (CacheTransientException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005840 File Offset: 0x00003A40
		private void OnSubscriptionAddedHelper(SubscriptionCacheEntry cacheEntry, bool fireEventForDisabled)
		{
			ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)136UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "OnSubscriptionAddedHelper: In database {0}.", new object[]
			{
				this.databaseGuid
			});
			if (this.OnSubscriptionAdded == null)
			{
				ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)202UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "OnSubscriptionAddedHelper: Skipping subscription because no add event handler is registered.", new object[0]);
				return;
			}
			if (cacheEntry.Disabled && !fireEventForDisabled)
			{
				ContentAggregationConfig.SyncLogSession.LogDebugging((TSLID)137UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "OnSubscriptionAddedHelper: Skipping disabled subscription.", new object[0]);
				return;
			}
			SubscriptionInformation e = new SubscriptionInformation(this, cacheEntry);
			this.OnSubscriptionAdded(null, e);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005940 File Offset: 0x00003B40
		private void OnSubscriptionRemovedHelper(SubscriptionCacheEntry cacheEntry)
		{
			ContentAggregationConfig.SyncLogSession.LogVerbose((TSLID)138UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), cacheEntry.SubscriptionGuid, cacheEntry.MailboxGuid, "OnSubscriptionRemovedHelper: In database {0}.", new object[]
			{
				this.databaseGuid
			});
			if (this.OnSubscriptionRemoved != null)
			{
				SubscriptionInformation e = new SubscriptionInformation(this, cacheEntry);
				this.OnSubscriptionRemoved(null, e);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000059B8 File Offset: 0x00003BB8
		private bool TryEnsureInitialized()
		{
			if (this.cacheFolderId != null)
			{
				return true;
			}
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)139UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialize: Initializing the cache manager for database {0}.", new object[]
			{
				this.databaseGuid
			});
			bool flag = false;
			PoolItem<MailboxSession> item = this.systemMailboxSessionPool.GetItem(out flag);
			if (item == null)
			{
				return false;
			}
			bool reuse = true;
			bool result;
			try
			{
				StoreObjectId defaultFolderId = item.Item.GetDefaultFolderId(DefaultFolderType.Root);
				try
				{
					using (Folder folder = Folder.Bind(item.Item, DefaultFolderType.Root))
					{
						using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, SubscriptionCacheManager.sortByDisplayName, SubscriptionCacheManager.folderProperties))
						{
							if (queryResult.SeekToCondition(SeekReference.OriginBeginning, SubscriptionCacheManager.cacheFolderNameFilter))
							{
								object[][] rows = queryResult.GetRows(1);
								if (rows.Length != 0)
								{
									VersionedId versionedId = (VersionedId)rows[0][1];
									this.cacheFolderId = versionedId.ObjectId;
								}
							}
						}
					}
				}
				catch (StorageTransientException ex)
				{
					reuse = SubscriptionCacheManager.IsSessionReusableAfterException(ex);
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)140UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialized: In database {0}, encountered: {1}.", new object[]
					{
						this.databaseGuid,
						ex
					});
					return false;
				}
				catch (StoragePermanentException ex2)
				{
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)141UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialized: In database {0}, encountered: {1}.", new object[]
					{
						this.databaseGuid,
						ex2
					});
				}
				Folder folder2 = null;
				PropertyDefinition[] array = new PropertyDefinition[]
				{
					ItemSchema.Id
				};
				try
				{
					if (this.cacheFolderId == null)
					{
						ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)95UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialize: Creating a new cache manager folder for database {0}.", new object[]
						{
							this.databaseGuid
						});
						folder2 = Folder.Create(item.Item, defaultFolderId, StoreObjectType.Folder, SubscriptionCacheManager.FolderName, CreateMode.CreateNew);
						folder2.Save();
						folder2.Load(array);
					}
					else
					{
						folder2 = Folder.Bind(item.Item, this.cacheFolderId, array);
					}
					this.cacheFolderId = folder2.Id.ObjectId;
					ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)142UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialize: Cache folder ID for database {0} is {1}.", new object[]
					{
						this.databaseGuid,
						this.cacheFolderId
					});
					if (!ContentAggregationConfig.TransportSyncDispatchEnabled)
					{
						ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)143UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialized: Exiting initialization because dispatching is disabled in database {0}.", new object[]
						{
							this.databaseGuid
						});
						result = true;
					}
					else
					{
						this.TryStartSubscriptionQueueBuildup(0);
						result = true;
					}
				}
				catch (StorageTransientException ex3)
				{
					reuse = SubscriptionCacheManager.IsSessionReusableAfterException(ex3);
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)144UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialized: In database {0}, encountered: {1}.", new object[]
					{
						this.databaseGuid,
						ex3
					});
					result = false;
				}
				catch (StoragePermanentException ex4)
				{
					reuse = SubscriptionCacheManager.IsSessionReusableAfterException(ex4);
					ContentAggregationConfig.SyncLogSession.LogError((TSLID)145UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "TryEnsureInitialized: In database {0}, encountered: {1}.", new object[]
					{
						this.databaseGuid,
						ex4
					});
					result = false;
				}
				finally
				{
					if (folder2 != null)
					{
						folder2.Dispose();
						folder2 = null;
					}
				}
			}
			finally
			{
				this.systemMailboxSessionPool.ReturnItem(item, reuse);
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005E30 File Offset: 0x00004030
		private void UpdateMailboxCount(int newMailboxCount)
		{
			ManagerPerfCounterHandler.Instance.IncrementMailboxesInSubscriptionCachesBy((long)(newMailboxCount - this.mailboxCount));
			this.mailboxCount = newMailboxCount;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005E4C File Offset: 0x0000404C
		private void UpdateMailboxCountBy(int increment)
		{
			ManagerPerfCounterHandler.Instance.IncrementMailboxesInSubscriptionCachesBy((long)increment);
			Interlocked.Add(ref this.mailboxCount, increment);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005EF0 File Offset: 0x000040F0
		private void EnumerateSubscriptionsInCacheWorker(object objectState)
		{
			List<SubscriptionCacheEntry> subscriptionList = new List<SubscriptionCacheEntry>();
			bool flag = true;
			if (this.shuttingDown)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)204UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCacheWorker: no queue buildup because we are shutting down.", new object[]
				{
					this.databaseGuid
				});
				flag = false;
			}
			else if (!DataAccessLayer.Initialized)
			{
				ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)205UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCacheWorker: postpone queue buildup. DAL not yet initialized.", new object[]
				{
					this.databaseGuid
				});
				flag = false;
			}
			else if (this.failedToLoad.Count > 0)
			{
				List<Guid> list = new List<Guid>();
				foreach (Guid guid in this.failedToLoad)
				{
					try
					{
						foreach (SubscriptionCacheEntry item in this.ReadAllSubscriptionsFromCache(guid))
						{
							subscriptionList.Add(item);
						}
					}
					catch (CacheTransientException)
					{
						list.Add(guid);
					}
					catch (CachePermanentException)
					{
						list.Add(guid);
					}
				}
				List<SubscriptionCacheEntry> subscriptionList2 = subscriptionList;
				this.failedToLoad.Clear();
				if (list.Count > 0)
				{
					this.failedToLoad = list;
				}
			}
			else
			{
				flag = this.EnumerateSubscriptionsInCache(true, delegate(Guid mailboxGuid, SubscriptionCacheMessage cacheMessage, Exception exception)
				{
					if (cacheMessage != null && cacheMessage.Subscriptions != null)
					{
						using (IEnumerator<SubscriptionCacheEntry> enumerator4 = cacheMessage.Subscriptions.GetEnumerator())
						{
							while (enumerator4.MoveNext())
							{
								SubscriptionCacheEntry item2 = enumerator4.Current;
								subscriptionList.Add(item2);
							}
							return;
						}
					}
					if (exception is CacheTransientException)
					{
						this.failedToLoad.Add(mailboxGuid);
						return;
					}
					CachePermanentException ex = exception as CachePermanentException;
				});
			}
			if (flag)
			{
				foreach (SubscriptionCacheEntry subscriptionCacheEntry in subscriptionList)
				{
					if (!this.TryFireOnSubscriptionAddedEvent(subscriptionCacheEntry, true))
					{
						this.failedToLoad.Add(subscriptionCacheEntry.MailboxGuid);
					}
				}
			}
			if (this.failedToLoad.Count > 0)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)146UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCacheWorker: {0} cache messages failed to load.", new object[]
				{
					this.failedToLoad.Count
				});
				flag = false;
			}
			if (!flag && !this.shuttingDown)
			{
				ContentAggregationConfig.SyncLogSession.LogError((TSLID)147UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCacheWorker: Enqueueing another attempt to build the subscription queue.", new object[0]);
				this.TryStartSubscriptionQueueBuildup((int)ContentAggregationConfig.DelayBetweenDispatchQueueBuilds.TotalMilliseconds);
				return;
			}
			ContentAggregationConfig.SyncLogSession.LogInformation((TSLID)148UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), "EnumerateSubscriptionsInCacheWorker: Building subscription queue has finished.", new object[0]);
			this.subscriptionQueueBuildupDone = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000061FC File Offset: 0x000043FC
		private bool TryStartSubscriptionQueueBuildup(int waitBeforeStartInMilliseconds)
		{
			lock (this.subscriptionQueueBuilderLock)
			{
				if (this.subscriptionQueueBuilderTimer != null)
				{
					this.subscriptionQueueBuilderTimer.Change(waitBeforeStartInMilliseconds, -1);
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000032 RID: 50
		internal static readonly string FolderName = "AggregationSubscriptionCacheFolder";

		// Token: 0x04000033 RID: 51
		private static readonly Trace Tracer = ExTraceGlobals.CacheManagerTracer;

		// Token: 0x04000034 RID: 52
		private static readonly PropertyDefinition[] EnumerateCacheMessageProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			MessageItemSchema.SharingInstanceGuid
		};

		// Token: 0x04000035 RID: 53
		private static readonly QueryFilter cacheFolderNameFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.DisplayName, SubscriptionCacheManager.FolderName);

		// Token: 0x04000036 RID: 54
		private static readonly SortBy[] sortByDisplayName = new SortBy[]
		{
			new SortBy(StoreObjectSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x04000037 RID: 55
		private static readonly PropertyDefinition[] folderProperties = new PropertyDefinition[]
		{
			FolderSchema.DisplayName,
			FolderSchema.Id
		};

		// Token: 0x04000038 RID: 56
		private Guid databaseGuid;

		// Token: 0x04000039 RID: 57
		private ISubscriptionCoreCacheManager coreCacheManager;

		// Token: 0x0400003A RID: 58
		private SystemMailboxSessionPool systemMailboxSessionPool;

		// Token: 0x0400003B RID: 59
		private TokenManager tokenManager;

		// Token: 0x0400003C RID: 60
		private StoreObjectId cacheFolderId;

		// Token: 0x0400003D RID: 61
		private int mailboxCount;

		// Token: 0x0400003E RID: 62
		private bool shuttingDown;

		// Token: 0x0400003F RID: 63
		private bool subscriptionQueueBuildupDone;

		// Token: 0x04000040 RID: 64
		private object subscriptionQueueBuilderLock = new object();

		// Token: 0x04000041 RID: 65
		private Timer subscriptionQueueBuilderTimer;

		// Token: 0x04000042 RID: 66
		private List<Guid> failedToLoad = new List<Guid>();

		// Token: 0x0200000F RID: 15
		private class SubscriptionCoreCacheManager : ISubscriptionCoreCacheManager
		{
			// Token: 0x0600008E RID: 142 RVA: 0x000062DF File Offset: 0x000044DF
			public SubscriptionCoreCacheManager(Guid databaseGuid)
			{
				SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
				this.databaseGuid = databaseGuid;
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00006304 File Offset: 0x00004504
			public void DeleteCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId)
			{
				this.syncLogSession.LogDebugging((TSLID)149UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, Guid.Empty, "DeleteCacheMessage: In database {0}: cache message Id {1}.", new object[]
				{
					this.databaseGuid,
					cacheMessageId
				});
				systemMailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					cacheMessageId
				});
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00006375 File Offset: 0x00004575
			public void SaveCacheMessage(SubscriptionCacheMessage cacheMessage)
			{
				cacheMessage.Save();
			}

			// Token: 0x06000091 RID: 145 RVA: 0x00006380 File Offset: 0x00004580
			public SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheFolderId, Guid mailboxGuid, bool loadSubscriptions)
			{
				this.syncLogSession.LogDebugging((TSLID)150UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "TryBindCacheMessage: In database {0}.", new object[]
				{
					this.databaseGuid
				});
				SubscriptionCacheMessage subscriptionCacheMessage = SubscriptionCacheMessage.Bind(this.syncLogSession, systemMailboxSession, mailboxGuid, cacheFolderId);
				if (subscriptionCacheMessage != null && loadSubscriptions)
				{
					bool flag = true;
					try
					{
						subscriptionCacheMessage.LoadSubscriptions();
						flag = false;
					}
					finally
					{
						if (flag)
						{
							subscriptionCacheMessage.Dispose();
							subscriptionCacheMessage = null;
						}
					}
				}
				return subscriptionCacheMessage;
			}

			// Token: 0x06000092 RID: 146 RVA: 0x00006414 File Offset: 0x00004614
			public SubscriptionCacheMessage BindCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheMessageId, bool loadSubscriptions)
			{
				this.syncLogSession.LogDebugging((TSLID)151UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, Guid.Empty, "TryBindCacheMessage: In database {0}, cache message id {1}.", new object[]
				{
					this.databaseGuid,
					cacheMessageId
				});
				SubscriptionCacheMessage subscriptionCacheMessage = SubscriptionCacheMessage.Bind(this.syncLogSession, systemMailboxSession, cacheMessageId);
				if (subscriptionCacheMessage != null)
				{
					bool flag = true;
					try
					{
						subscriptionCacheMessage.LoadSubscriptions();
						flag = false;
					}
					finally
					{
						if (flag)
						{
							subscriptionCacheMessage.Dispose();
							subscriptionCacheMessage = null;
						}
					}
				}
				return subscriptionCacheMessage;
			}

			// Token: 0x06000093 RID: 147 RVA: 0x000064AC File Offset: 0x000046AC
			public SubscriptionCacheMessage CreateCacheMessage(MailboxSession systemMailboxSession, StoreObjectId cacheFolderId, Guid mailboxGuid, ExDateTime subscriptionListTimestamp)
			{
				this.syncLogSession.LogVerbose((TSLID)152UL, SubscriptionCacheManager.Tracer, (long)this.GetHashCode(), Guid.Empty, mailboxGuid, "CreateCacheMessage: In database {0}.", new object[]
				{
					this.databaseGuid
				});
				MessageItem messageItem = MessageItem.CreateAssociated(systemMailboxSession, cacheFolderId);
				SubscriptionCacheMessage subscriptionCacheMessage = null;
				try
				{
					subscriptionCacheMessage = new SubscriptionCacheMessage(this.syncLogSession, messageItem, mailboxGuid, subscriptionListTimestamp);
				}
				finally
				{
					if (subscriptionCacheMessage == null)
					{
						messageItem.Dispose();
					}
				}
				return subscriptionCacheMessage;
			}

			// Token: 0x04000045 RID: 69
			private readonly GlobalSyncLogSession syncLogSession = ContentAggregationConfig.SyncLogSession;

			// Token: 0x04000046 RID: 70
			private Guid databaseGuid;
		}
	}
}
