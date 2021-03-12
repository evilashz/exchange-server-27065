using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SubscriptionCacheMessage : DisposeTrackableBase
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00006538 File Offset: 0x00004738
		internal SubscriptionCacheMessage(GlobalSyncLogSession syncLogSession, MessageItem message, Guid mailboxGuid, ExDateTime subscriptionListTimestamp) : this(syncLogSession, message, mailboxGuid, true)
		{
			message.ClassName = "IPM.Aggregation.Cache.Subscriptions";
			message[MessageItemSchema.SharingInstanceGuid] = mailboxGuid;
			this.subscriptionListTimestamp = subscriptionListTimestamp;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006568 File Offset: 0x00004768
		private SubscriptionCacheMessage(GlobalSyncLogSession syncLogSession, MessageItem message, Guid mailboxGuid, bool newlyCreated)
		{
			if (message.Id != null)
			{
				this.id = message.Id.ObjectId;
			}
			else
			{
				this.id = null;
			}
			this.syncLogSession = syncLogSession;
			this.message = message;
			this.mailboxGuid = mailboxGuid;
			this.subscriptionCacheEntries = new LinkedList<SubscriptionCacheEntry>();
			this.newlyCreated = newlyCreated;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000065C5 File Offset: 0x000047C5
		internal bool IsNew
		{
			get
			{
				base.CheckDisposed();
				return this.newlyCreated;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000065D3 File Offset: 0x000047D3
		internal StoreObjectId Id
		{
			get
			{
				base.CheckDisposed();
				return this.id;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000065E1 File Offset: 0x000047E1
		internal int SubscriptionCount
		{
			get
			{
				base.CheckDisposed();
				this.CheckSubscriptionsLoaded();
				return this.subscriptionCacheEntries.Count;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000065FA File Offset: 0x000047FA
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000660E File Offset: 0x0000480E
		internal ExDateTime SubscriptionListTimestamp
		{
			get
			{
				base.CheckDisposed();
				this.CheckSubscriptionsLoaded();
				return this.subscriptionListTimestamp;
			}
			set
			{
				base.CheckDisposed();
				this.CheckSubscriptionsLoaded();
				this.subscriptionListTimestamp = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00006623 File Offset: 0x00004823
		internal IEnumerable<SubscriptionCacheEntry> Subscriptions
		{
			get
			{
				base.CheckDisposed();
				this.CheckSubscriptionsLoaded();
				return this.subscriptionCacheEntries;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006638 File Offset: 0x00004838
		internal static SubscriptionCacheMessage Bind(GlobalSyncLogSession syncLogSession, MailboxSession systemMailboxSession, StoreObjectId cacheMessageId)
		{
			SyncUtilities.ThrowIfArgumentNull("systemMailboxSession", systemMailboxSession);
			SyncUtilities.ThrowIfArgumentNull("cacheMessageId", cacheMessageId);
			MessageItem messageItem = MessageItem.Bind(systemMailboxSession, cacheMessageId, SubscriptionCacheMessage.BindProperties);
			Guid guid = (Guid)messageItem[MessageItemSchema.SharingInstanceGuid];
			return new SubscriptionCacheMessage(syncLogSession, messageItem, guid, false);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006684 File Offset: 0x00004884
		internal static SubscriptionCacheMessage Bind(GlobalSyncLogSession syncLogSession, MailboxSession systemMailboxSession, Guid mailboxGuid, StoreObjectId cacheFolderId)
		{
			SyncUtilities.ThrowIfArgumentNull("systemMailboxSession", systemMailboxSession);
			SyncUtilities.ThrowIfGuidEmpty("mailboxGuid", mailboxGuid);
			SyncUtilities.ThrowIfArgumentNull("cacheFolderId", cacheFolderId);
			List<StoreId> list = new List<StoreId>();
			using (Folder folder = Folder.Bind(systemMailboxSession, cacheFolderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.Associated, null, SubscriptionCacheMessage.FindCacheMessageSortBySharingInstanceGuid, SubscriptionCacheMessage.FindCacheMessageProperties))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, MessageItemSchema.SharingInstanceGuid, mailboxGuid);
					if (queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter))
					{
						object[][] rows = queryResult.GetRows(5);
						while (rows.Length != 0 && mailboxGuid.Equals((Guid)rows[0][0]))
						{
							int num = 0;
							while (num < rows.Length && mailboxGuid.Equals((Guid)rows[num][0]))
							{
								list.Add((StoreId)rows[num][1]);
								num++;
							}
							rows = queryResult.GetRows(5);
						}
					}
				}
			}
			if (list.Count > 1)
			{
				syncLogSession.LogError((TSLID)206UL, SubscriptionCacheMessage.Tracer, Guid.Empty, mailboxGuid, "{0} duplicate cache messages found for mailbox {1}.", new object[]
				{
					list.Count,
					mailboxGuid
				});
				foreach (StoreId storeId in list)
				{
					syncLogSession.LogVerbose((TSLID)207UL, SubscriptionCacheMessage.Tracer, Guid.Empty, mailboxGuid, "Deleting duplicate cache message with store ID {0} for mailbox {1}.", new object[]
					{
						storeId.ToString(),
						mailboxGuid
					});
					systemMailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						storeId
					});
				}
				DataAccessLayer.ReportWatson("Duplicate cache messages found.", new CacheTransientException(systemMailboxSession.MdbGuid, mailboxGuid, new InvalidOperationException("Duplicate cache messages found.")));
				return null;
			}
			if (list.Count == 1)
			{
				MessageItem messageItem = MessageItem.Bind(systemMailboxSession, list[0], SubscriptionCacheMessage.BindProperties);
				return new SubscriptionCacheMessage(syncLogSession, messageItem, mailboxGuid, false);
			}
			return null;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000068C0 File Offset: 0x00004AC0
		internal SubscriptionCacheEntry FindSubscriptionByMessageId(StoreObjectId subscriptionMessageId)
		{
			base.CheckDisposed();
			this.CheckSubscriptionsLoaded();
			foreach (SubscriptionCacheEntry subscriptionCacheEntry in this.subscriptionCacheEntries)
			{
				if (object.Equals(subscriptionCacheEntry.SubscriptionMessageId, subscriptionMessageId))
				{
					return subscriptionCacheEntry;
				}
			}
			return null;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00006930 File Offset: 0x00004B30
		internal SubscriptionCacheEntry FindSubscriptionBySubscriptionGuid(Guid subscriptionGuid)
		{
			base.CheckDisposed();
			this.CheckSubscriptionsLoaded();
			foreach (SubscriptionCacheEntry subscriptionCacheEntry in this.subscriptionCacheEntries)
			{
				if (object.Equals(subscriptionCacheEntry.SubscriptionGuid, subscriptionGuid))
				{
					return subscriptionCacheEntry;
				}
			}
			return null;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000069A8 File Offset: 0x00004BA8
		internal void Save()
		{
			base.CheckDisposed();
			this.OpenMessageAsReadWrite();
			if (this.loadedSubscriptions || this.newlyCreated)
			{
				using (Stream stream = this.OpenSubscriptionsSerializationStream())
				{
					BinaryWriter writer = new BinaryWriter(stream);
					this.Serialize(writer);
				}
			}
			this.SaveMessage();
			this.syncLogSession.LogDebugging((TSLID)153UL, SubscriptionCacheMessage.Tracer, Guid.Empty, this.mailboxGuid, "Saved Cache Message with Id:{0}, IsNew:{1}, Last Repair Time:{2}.", new object[]
			{
				this.id,
				this.IsNew,
				this.subscriptionListTimestamp
			});
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006A68 File Offset: 0x00004C68
		internal void Load()
		{
			base.CheckDisposed();
			this.message.Load();
			this.id = this.message.Id.ObjectId;
			this.newlyCreated = false;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00006A98 File Offset: 0x00004C98
		internal SubscriptionCacheEntry AddSubscription(Guid tenantGuid, Guid externalDirectoryOrgId, AggregationSubscription subscription)
		{
			base.CheckDisposed();
			this.CheckSubscriptionsLoaded();
			SerializedSubscription serializedSubscription = SerializedSubscription.FromSubscription(subscription);
			SubscriptionCacheEntry cacheEntry = new SubscriptionCacheEntry(this.syncLogSession, subscription.SubscriptionGuid, subscription.SubscriptionMessageId, subscription.UserLegacyDN, this.mailboxGuid, tenantGuid, externalDirectoryOrgId, subscription.SubscriptionType, subscription.AggregationType, subscription.LastSyncTime, false, subscription.IncomingServerName, subscription.SyncPhase, serializedSubscription);
			return this.AddSubscription(cacheEntry);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006B05 File Offset: 0x00004D05
		internal SubscriptionCacheEntry AddSubscription(SubscriptionCacheEntry cacheEntry)
		{
			base.CheckDisposed();
			this.CheckSubscriptionsLoaded();
			if (this.FindSubscriptionBySubscriptionGuid(cacheEntry.SubscriptionGuid) != null)
			{
				return cacheEntry;
			}
			this.subscriptionCacheEntries.AddLast(cacheEntry);
			return cacheEntry;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006B34 File Offset: 0x00004D34
		internal SubscriptionCacheEntry RemoveSubscription(StoreObjectId subscriptionMessageId)
		{
			base.CheckDisposed();
			this.CheckSubscriptionsLoaded();
			foreach (SubscriptionCacheEntry subscriptionCacheEntry in this.subscriptionCacheEntries)
			{
				if (object.Equals(subscriptionMessageId, subscriptionCacheEntry.SubscriptionMessageId))
				{
					this.subscriptionCacheEntries.Remove(subscriptionCacheEntry);
					return subscriptionCacheEntry;
				}
			}
			return null;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006BB0 File Offset: 0x00004DB0
		internal void LoadSubscriptions()
		{
			base.CheckDisposed();
			if (this.loadedSubscriptions)
			{
				return;
			}
			this.loadedSubscriptions = true;
			using (Stream stream = this.OpenSubscriptionsDeserializationStream())
			{
				BinaryReader reader = new BinaryReader(stream);
				this.Deserialize(reader);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006C04 File Offset: 0x00004E04
		protected virtual void SaveMessage()
		{
			this.message.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00006C14 File Offset: 0x00004E14
		protected virtual void Serialize(BinaryWriter writer)
		{
			try
			{
				writer.Write(7);
				writer.Write(this.subscriptionListTimestamp.UtcTicks);
				if (this.subscriptionCacheEntries == null || this.subscriptionCacheEntries.Count == 0)
				{
					writer.Write(0);
				}
				else
				{
					writer.Write((byte)this.subscriptionCacheEntries.Count);
					foreach (SubscriptionCacheEntry subscriptionCacheEntry in this.subscriptionCacheEntries)
					{
						subscriptionCacheEntry.Serialize(writer);
					}
				}
			}
			catch (IOException innerException)
			{
				throw new SerializationException("Subscription Cache Entry is not valid.", innerException);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006CCC File Offset: 0x00004ECC
		protected virtual Stream OpenSubscriptionsSerializationStream()
		{
			return this.message.OpenPropertyStream(AggregationSubscriptionMessageSchema.SharingSubscriptionsCache, PropertyOpenMode.Create);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006CDF File Offset: 0x00004EDF
		protected virtual Stream OpenSubscriptionsDeserializationStream()
		{
			return this.message.OpenPropertyStream(AggregationSubscriptionMessageSchema.SharingSubscriptionsCache, PropertyOpenMode.ReadOnly);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00006CF2 File Offset: 0x00004EF2
		protected virtual void OpenMessageAsReadWrite()
		{
			this.message.OpenAsReadWrite();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006CFF File Offset: 0x00004EFF
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.message != null)
			{
				this.message.Dispose();
				this.message = null;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006D1E File Offset: 0x00004F1E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SubscriptionCacheMessage>(this);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006D26 File Offset: 0x00004F26
		private void CheckSubscriptionsLoaded()
		{
			if (!this.loadedSubscriptions && !this.newlyCreated)
			{
				throw new InvalidOperationException("Run LoadSubscriptions first to load all subscriptions.");
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006D44 File Offset: 0x00004F44
		private void Deserialize(BinaryReader reader)
		{
			try
			{
				this.version = reader.ReadByte();
				if (this.version != 7)
				{
					throw new VersionMismatchException(string.Format(CultureInfo.InvariantCulture, "Invalid Subscription Cache version found in stream: {0}.", new object[]
					{
						this.version
					}));
				}
				long num = reader.ReadInt64();
				if (num < DateTime.MinValue.Ticks || num > DateTime.MaxValue.Ticks)
				{
					throw new SerializationException("Invalid subscriptionListTimestamp in ticks.");
				}
				this.subscriptionListTimestamp = new ExDateTime(ExTimeZone.UtcTimeZone, num);
				byte b = reader.ReadByte();
				for (byte b2 = 0; b2 < b; b2 += 1)
				{
					SubscriptionCacheEntry value = SubscriptionCacheEntry.FromSerialization(this.syncLogSession, reader, this.version);
					this.subscriptionCacheEntries.AddLast(value);
				}
			}
			catch (CorruptDataException innerException)
			{
				throw new SerializationException("Subscription Cache Entry is not valid.", innerException);
			}
			catch (IOException innerException2)
			{
				throw new SerializationException("Subscription Cache Entry is not valid.", innerException2);
			}
			catch (FormatException innerException3)
			{
				throw new SerializationException("Subscription Cache Entry is not valid.", innerException3);
			}
		}

		// Token: 0x04000047 RID: 71
		private const short InvalidVersion = -32768;

		// Token: 0x04000048 RID: 72
		private const byte CurrentVersion = 7;

		// Token: 0x04000049 RID: 73
		private static readonly Trace Tracer = ExTraceGlobals.SubscriptionCacheMessageTracer;

		// Token: 0x0400004A RID: 74
		private static readonly SortBy[] FindCacheMessageSortBySharingInstanceGuid = new SortBy[]
		{
			new SortBy(MessageItemSchema.SharingInstanceGuid, SortOrder.Ascending)
		};

		// Token: 0x0400004B RID: 75
		private static readonly PropertyDefinition[] FindCacheMessageProperties = new PropertyDefinition[]
		{
			MessageItemSchema.SharingInstanceGuid,
			ItemSchema.Id
		};

		// Token: 0x0400004C RID: 76
		private static readonly PropertyDefinition[] BindProperties = new PropertyDefinition[]
		{
			MessageItemSchema.SharingInstanceGuid,
			AggregationSubscriptionMessageSchema.SharingSubscriptionConfiguration,
			AggregationSubscriptionMessageSchema.SharingSubscriptionsCache
		};

		// Token: 0x0400004D RID: 77
		private readonly GlobalSyncLogSession syncLogSession;

		// Token: 0x0400004E RID: 78
		private StoreObjectId id;

		// Token: 0x0400004F RID: 79
		private bool newlyCreated;

		// Token: 0x04000050 RID: 80
		private bool loadedSubscriptions;

		// Token: 0x04000051 RID: 81
		private MessageItem message;

		// Token: 0x04000052 RID: 82
		private Guid mailboxGuid;

		// Token: 0x04000053 RID: 83
		private ExDateTime subscriptionListTimestamp;

		// Token: 0x04000054 RID: 84
		private byte version;

		// Token: 0x04000055 RID: 85
		private LinkedList<SubscriptionCacheEntry> subscriptionCacheEntries;
	}
}
