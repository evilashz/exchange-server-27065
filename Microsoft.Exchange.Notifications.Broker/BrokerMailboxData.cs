using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001E RID: 30
	internal class BrokerMailboxData : IBrokerMailboxData
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00007AC8 File Offset: 0x00005CC8
		internal BrokerMailboxData(BrokerDatabaseData databaseData, MailboxInformation record)
		{
			this.MailboxGuid = record.MailboxGuid;
			this.DisplayName = record.DisplayName;
			object mailboxProperty = record.GetMailboxProperty(MailboxSchema.NotificationBrokerSubscriptions);
			if (mailboxProperty is int)
			{
				this.subscriptionFlags = new BrokerSubscriptionFlags?((BrokerSubscriptionFlags)mailboxProperty);
			}
			byte[] buffer = record.GetMailboxProperty(MailboxSchema.PersistableTenantPartitionHint) as byte[];
			this.TenantPartitionHint = TenantPartitionHint.FromPersistablePartitionHint(buffer);
			this.NextExpirationTime = DateTime.MaxValue;
			this.DatabaseData = databaseData;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00007B52 File Offset: 0x00005D52
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00007B5A File Offset: 0x00005D5A
		public bool SubscriptionsLoaded { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00007B63 File Offset: 0x00005D63
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00007B6B File Offset: 0x00005D6B
		public string DisplayName { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00007B74 File Offset: 0x00005D74
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00007B7C File Offset: 0x00005D7C
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007B85 File Offset: 0x00005D85
		public IDictionary<Guid, BrokerSubscription> Subscriptions
		{
			get
			{
				return this.subscriptions;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00007B8D File Offset: 0x00005D8D
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00007B95 File Offset: 0x00005D95
		public DateTime NextExpirationTime { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007B9E File Offset: 0x00005D9E
		public Guid DatabaseGuid
		{
			get
			{
				return this.DatabaseData.DatabaseGuid;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007BAB File Offset: 0x00005DAB
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007BB3 File Offset: 0x00005DB3
		public TenantPartitionHint TenantPartitionHint { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00007BBC File Offset: 0x00005DBC
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00007BC4 File Offset: 0x00005DC4
		public IBrokerDatabaseData DatabaseData { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00007BCD File Offset: 0x00005DCD
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00007BD5 File Offset: 0x00005DD5
		internal StoreObjectId SubscriptionFolderId { get; private set; }

		// Token: 0x0600013C RID: 316 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public void LoadSubscriptions(bool wasMoved = false)
		{
			if (this.SubscriptionsLoaded)
			{
				return;
			}
			if (this.subscriptionFlags != null && this.subscriptionFlags.Value == BrokerSubscriptionFlags.NoSubscription)
			{
				this.SubscriptionsLoaded = true;
				return;
			}
			if (this.subscriptionFlags == null && !wasMoved)
			{
				this.SubscriptionsLoaded = true;
				return;
			}
			bool flag = false;
			lock (this.instanceLock)
			{
				if (this.SubscriptionsLoaded)
				{
					return;
				}
				using (MailboxSession session = this.GetSession())
				{
					this.LoadSubscriptions(session);
				}
				flag = (this.subscriptions != null && this.subscriptions.Count > 0);
			}
			if (flag)
			{
				foreach (KeyValuePair<Guid, BrokerSubscription> keyValuePair in this.subscriptions)
				{
					Generator.Singleton.CreateMapiNotificationHandler(keyValuePair.Value);
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007CFC File Offset: 0x00005EFC
		internal static StoreObjectId CreateSubscriptionFolder(Folder rootFolder)
		{
			ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>(0L, "[BrokerMailboxData.CreateSubscriptionFolder] Creating root subscription folder for mailbox '{0}'", rootFolder.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			StoreObjectId objectId;
			using (Folder folder = Folder.Create(rootFolder.Session, rootFolder.Id, StoreObjectType.Folder))
			{
				folder.DisplayName = "EventSubscriptions";
				folder.Save();
				folder.Load();
				objectId = folder.Id.ObjectId;
			}
			return objectId;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007D94 File Offset: 0x00005F94
		internal static StoreObjectId GetSubscriptionFolderId(MailboxSession session, bool createIfMissing)
		{
			StoreObjectId result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Root))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, BrokerMailboxData.folderNameSort, new PropertyDefinition[]
				{
					FolderSchema.Id,
					FolderSchema.DisplayName
				}))
				{
					if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, BrokerMailboxData.FolderNameFilter))
					{
						ExTraceGlobals.SubscriptionsTracer.TraceDebug<string>(0L, "[BrokerMailboxData.GetSubscriptionFolderId] Subscription folder was missing for mailbox '{0}'.", session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
						result = (createIfMissing ? BrokerMailboxData.CreateSubscriptionFolder(folder) : null);
					}
					else
					{
						IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
						StoreObjectId objectId = (propertyBags[0].TryGetProperty(FolderSchema.Id) as VersionedId).ObjectId;
						ExTraceGlobals.SubscriptionsTracer.TraceDebug<StoreObjectId>(0L, "[BrokerMailboxData.GetSubscriptionFolderId] Found subscription folder Id: '{0}'", objectId);
						result = objectId;
					}
				}
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007E8C File Offset: 0x0000608C
		internal static void SetSubscriptionFlagOnMailbox(MailboxSession session, BrokerSubscriptionFlags flag)
		{
			session.Mailbox.Load(BrokerMailboxData.notificationSubscriptionFlag);
			object obj = session.Mailbox.TryGetProperty(MailboxSchema.NotificationBrokerSubscriptions);
			if (!(obj is int) || flag != (BrokerSubscriptionFlags)obj)
			{
				session.Mailbox[MailboxSchema.NotificationBrokerSubscriptions] = (int)flag;
				session.Mailbox.Save();
				session.Mailbox.Load();
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007F00 File Offset: 0x00006100
		internal void SetSubscriptionFlagOnMailbox(BrokerSubscriptionFlags flag)
		{
			using (MailboxSession session = this.GetSession())
			{
				BrokerMailboxData.SetSubscriptionFlagOnMailbox(session, flag);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007F38 File Offset: 0x00006138
		internal void CallRemoveForAllHandlers()
		{
			List<KeyValuePair<INotificationHandler, BrokerSubscription>> list = null;
			lock (this.instanceLock)
			{
				if (this.subscriptions != null && this.subscriptions.Count > 0)
				{
					list = new List<KeyValuePair<INotificationHandler, BrokerSubscription>>(this.subscriptions.Count);
					foreach (KeyValuePair<Guid, BrokerSubscription> keyValuePair in this.subscriptions)
					{
						if (keyValuePair.Value.Handler != null)
						{
							list.Add(new KeyValuePair<INotificationHandler, BrokerSubscription>(keyValuePair.Value.Handler, keyValuePair.Value));
							keyValuePair.Value.Handler = null;
						}
					}
				}
			}
			if (list != null)
			{
				foreach (KeyValuePair<INotificationHandler, BrokerSubscription> keyValuePair2 in list)
				{
					keyValuePair2.Key.SubscriptionRemoved(keyValuePair2.Value);
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00008060 File Offset: 0x00006260
		internal void Save(BrokerSubscription subscription)
		{
			lock (this.instanceLock)
			{
				this.ValidateCorrectMailbox(subscription);
				this.FixUpSubscription(subscription);
				using (MailboxSession session = this.GetSession())
				{
					this.Save(session, subscription);
				}
				if (this.subscriptions == null)
				{
					this.subscriptions = new ConcurrentDictionary<Guid, BrokerSubscription>();
				}
				this.subscriptions[subscription.SubscriptionId] = subscription;
				if (this.NextExpirationTime > subscription.Expiration)
				{
					this.NextExpirationTime = subscription.Expiration;
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008114 File Offset: 0x00006314
		internal MailboxSnapshot GetDiagnostics(Func<BrokerSubscription, bool> subscriptionFilter)
		{
			MailboxSnapshot mailboxSnapshot = new MailboxSnapshot();
			lock (this.instanceLock)
			{
				mailboxSnapshot.DisplayName = this.DisplayName;
				mailboxSnapshot.MailboxGuid = this.MailboxGuid;
				mailboxSnapshot.SubscriptionsLoaded = this.SubscriptionsLoaded;
				mailboxSnapshot.SubscriptionCount = ((this.subscriptions == null) ? 0 : this.subscriptions.Count);
				if (subscriptionFilter != null && this.subscriptions != null)
				{
					mailboxSnapshot.Subscriptions = new List<BrokerSubscription>();
					foreach (KeyValuePair<Guid, BrokerSubscription> keyValuePair in this.subscriptions)
					{
						if (subscriptionFilter(keyValuePair.Value))
						{
							mailboxSnapshot.Subscriptions.Add(keyValuePair.Value);
						}
					}
				}
			}
			return mailboxSnapshot;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00008208 File Offset: 0x00006408
		internal bool RemoveSubscription(BrokerSubscription subscription)
		{
			bool result = false;
			this.ValidateCorrectMailbox(subscription);
			if (this.subscriptions == null)
			{
				return false;
			}
			this.FixUpSubscription(subscription);
			BrokerSubscription brokerSubscription = null;
			lock (this.instanceLock)
			{
				if (this.subscriptions.TryRemove(subscription.SubscriptionId, out brokerSubscription))
				{
					using (MailboxSession session = this.GetSession())
					{
						AggregateOperationResult aggregateOperationResult = session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							subscription.StoreId
						});
						if (aggregateOperationResult.OperationResult != OperationResult.Succeeded)
						{
							ExTraceGlobals.SubscriptionsTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "[BrokerMailboxData.RemoveSubscription] Failed to remove subscription due to delete error - {0}", aggregateOperationResult.GroupOperationResults[0].Exception);
						}
						if (this.subscriptions.Count == 0)
						{
							BrokerMailboxData.SetSubscriptionFlagOnMailbox(session, BrokerSubscriptionFlags.NoSubscription);
						}
					}
					result = true;
				}
			}
			if (brokerSubscription != null && brokerSubscription.Handler != null)
			{
				brokerSubscription.Handler.SubscriptionRemoved(brokerSubscription);
				brokerSubscription.Handler = null;
			}
			return result;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008314 File Offset: 0x00006514
		internal void RemoveAllSubscriptions()
		{
			lock (this.instanceLock)
			{
				if (this.subscriptions != null)
				{
					this.subscriptions.Clear();
				}
			}
			using (MailboxSession session = this.GetSession())
			{
				session.DeleteAllObjects(DeleteItemFlags.HardDelete, this.SubscriptionFolderId);
				BrokerMailboxData.SetSubscriptionFlagOnMailbox(session, BrokerSubscriptionFlags.NoSubscription);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00008398 File Offset: 0x00006598
		internal List<BrokerSubscription> CheckExpiration()
		{
			List<BrokerSubscription> list = null;
			lock (this.instanceLock)
			{
				if (this.subscriptions == null)
				{
					return null;
				}
				DateTime dateTime = DateTime.MaxValue;
				if (this.NextExpirationTime > TimeProvider.UtcNow)
				{
					return null;
				}
				foreach (KeyValuePair<Guid, BrokerSubscription> keyValuePair in this.subscriptions)
				{
					if (keyValuePair.Value.Expiration < TimeProvider.UtcNow)
					{
						if (list == null)
						{
							list = new List<BrokerSubscription>();
						}
						list.Add(keyValuePair.Value);
					}
					else if (keyValuePair.Value.Expiration < dateTime)
					{
						dateTime = keyValuePair.Value.Expiration;
					}
				}
				this.NextExpirationTime = dateTime;
				if (list != null)
				{
					this.RemoveSubscriptions(list);
					if (this.subscriptions.Count == 0)
					{
						this.SetSubscriptionFlagOnMailbox(BrokerSubscriptionFlags.NoSubscription);
					}
				}
			}
			return list;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000084BC File Offset: 0x000066BC
		internal void CreateSubscription(MailboxSession session, BrokerSubscription subscriptionToSave)
		{
			if (this.SubscriptionFolderId == null)
			{
				this.SubscriptionFolderId = BrokerMailboxData.GetSubscriptionFolderId(session, true);
			}
			using (Item item = Item.Create(session, "IPM.NotificationBroker.Subscription", this.SubscriptionFolderId))
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid, ConsumerId>((long)this.GetHashCode(), "[BrokerMailboxData.CreateAndSaveSubscription] Created subscription id '{0}' for consumer '{1}'", subscriptionToSave.SubscriptionId, subscriptionToSave.ConsumerId);
				this.CopyTo(subscriptionToSave, item);
				item.Save(SaveMode.NoConflictResolutionForceSave);
				item.Load();
				subscriptionToSave.StoreId = item.Id.ObjectId;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008558 File Offset: 0x00006758
		internal void UpdateSubscription(MailboxSession session, BrokerSubscription subscriptionToUpdate)
		{
			if (this.SubscriptionFolderId == null)
			{
				this.SubscriptionFolderId = BrokerMailboxData.GetSubscriptionFolderId(session, true);
			}
			using (Item item = Item.Bind(session, subscriptionToUpdate.StoreId, BrokerMailboxData.propertiesToFetch))
			{
				item.OpenAsReadWrite();
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid, ConsumerId>((long)this.GetHashCode(), "[BrokerMailboxData.UpdateSubscription] Updated subscription id '{0}' for consumer '{1}'", subscriptionToUpdate.SubscriptionId, subscriptionToUpdate.ConsumerId);
				this.CopyTo(subscriptionToUpdate, item);
				item.Save(SaveMode.NoConflictResolutionForceSave);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000085E0 File Offset: 0x000067E0
		internal void CopyTo(BrokerSubscription subscription, Item storeItem)
		{
			storeItem[BrokerSubscriptionSchema.SubscriptionId] = subscription.SubscriptionId;
			storeItem[BrokerSubscriptionSchema.ConsumerId] = subscription.ConsumerId.ToString();
			if (!string.IsNullOrEmpty(subscription.ChannelId))
			{
				storeItem[BrokerSubscriptionSchema.ChannelId] = subscription.ChannelId;
			}
			storeItem[BrokerSubscriptionSchema.Expiration] = new ExDateTime(ExTimeZone.UtcTimeZone, subscription.Expiration.ToUniversalTime());
			if (subscription.Receiver.MailboxGuid != Guid.Empty)
			{
				storeItem[BrokerSubscriptionSchema.ReceiverMailboxGuid] = subscription.Receiver.MailboxGuid;
			}
			if (!string.IsNullOrEmpty(subscription.Receiver.MailboxSmtp))
			{
				storeItem[BrokerSubscriptionSchema.ReceiverMailboxSmtp] = subscription.Receiver.MailboxSmtp;
			}
			if (!string.IsNullOrEmpty(subscription.Receiver.FrontEndUrl))
			{
				storeItem[BrokerSubscriptionSchema.ReceiverUrl] = subscription.Receiver.FrontEndUrl;
			}
			if (subscription.Parameters != null)
			{
				string text = JsonConverter.Serialize<BaseSubscription>(subscription.Parameters, null);
				if (text.Length < 255)
				{
					storeItem[BrokerSubscriptionSchema.Parameters] = text;
					return;
				}
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid, string, int>((long)this.GetHashCode(), "[BrokerSubscription.CopyTo] Mailbox {0}, Subscription {1}. Parameters were long enough to require streaming.  Length {2}", subscription.Sender.MailboxGuid, storeItem.IsNew ? "<NEW>" : storeItem.Id.ToBase64String(), text.Length);
				using (Stream stream = storeItem.OpenPropertyStream(BrokerSubscriptionSchema.Parameters, PropertyOpenMode.Create))
				{
					using (StreamWriter streamWriter = new StreamWriter(stream, Encoding.Unicode))
					{
						streamWriter.Write(text);
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000087B4 File Offset: 0x000069B4
		internal void ClearForTest(BrokerSubscriptionFlags flagsToUse)
		{
			lock (this.instanceLock)
			{
				this.subscriptions = null;
				this.subscriptionFlags = new BrokerSubscriptionFlags?(flagsToUse);
				this.SubscriptionsLoaded = false;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00008808 File Offset: 0x00006A08
		private void Save(MailboxSession session, BrokerSubscription subscription)
		{
			if (this.SubscriptionFolderId == null)
			{
				this.SubscriptionFolderId = BrokerMailboxData.GetSubscriptionFolderId(session, true);
			}
			this.VerifySubscriptionValid(subscription, false);
			if (subscription.StoreId == null)
			{
				this.CreateSubscription(session, subscription);
			}
			else
			{
				this.UpdateSubscription(session, subscription);
			}
			subscription.MailboxData = this;
			subscription.Sender.MailboxSmtp = session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			BrokerMailboxData.SetSubscriptionFlagOnMailbox(session, BrokerSubscriptionFlags.HasSubscriptions);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00008884 File Offset: 0x00006A84
		private void VerifySubscriptionValid(BrokerSubscription subscription, bool onLoad)
		{
			if (subscription.IsValid)
			{
				return;
			}
			if (onLoad)
			{
				throw new InvalidBrokerSubscriptionOnLoadException((subscription.StoreId == null) ? "<NULL>" : subscription.StoreId.ToBase64String(), subscription.Sender.MailboxSmtp);
			}
			throw new InvalidBrokerSubscriptionException(subscription.ToJson());
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000088D3 File Offset: 0x00006AD3
		private void ValidateCorrectMailbox(BrokerSubscription subscription)
		{
			if (this.MailboxGuid != subscription.Sender.MailboxGuid)
			{
				throw new WrongMailboxForSubscriptionException(subscription.Sender.MailboxGuid, this.MailboxGuid);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008904 File Offset: 0x00006B04
		private bool FixUpSubscription(BrokerSubscription subscription)
		{
			lock (this.instanceLock)
			{
				BrokerSubscription brokerSubscription;
				if (subscription.StoreId == null && this.subscriptions != null && this.subscriptions.TryGetValue(subscription.SubscriptionId, out brokerSubscription))
				{
					subscription.StoreId = brokerSubscription.StoreId;
				}
			}
			return subscription.StoreId != null;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000897C File Offset: 0x00006B7C
		private void RemoveSubscriptions(List<BrokerSubscription> subscriptions)
		{
			StoreId[] array = new StoreId[subscriptions.Count];
			lock (this.instanceLock)
			{
				for (int i = 0; i < array.Length; i++)
				{
					BrokerSubscription brokerSubscription = subscriptions[i];
					array[i] = brokerSubscription.StoreId;
					BrokerSubscription brokerSubscription2;
					if (this.subscriptions.TryRemove(brokerSubscription.SubscriptionId, out brokerSubscription2) && brokerSubscription.Handler != null)
					{
						brokerSubscription.Handler.SubscriptionRemoved(brokerSubscription);
					}
				}
			}
			using (MailboxSession session = this.GetSession())
			{
				session.Delete(DeleteItemFlags.HardDelete, array);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00008A3C File Offset: 0x00006C3C
		private void DeleteExpiredSubscriptions(MailboxSession session, List<StoreObjectId> subscriptionsToDelete)
		{
			if (subscriptionsToDelete.Count > 0)
			{
				session.Delete(DeleteItemFlags.HardDelete, subscriptionsToDelete.ToArray());
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008A58 File Offset: 0x00006C58
		private BrokerSubscription HydrateAndCheckExpiration(IStorePropertyBag propertyBag, MailboxSession session, List<StoreObjectId> subscriptionsToDelete)
		{
			BrokerSubscription brokerSubscription = null;
			try
			{
				brokerSubscription = this.HydrateSubscription(propertyBag, session, this);
				this.VerifySubscriptionValid(brokerSubscription, true);
			}
			catch
			{
				subscriptionsToDelete.Add(brokerSubscription.StoreId);
				throw;
			}
			if (brokerSubscription.Expiration < TimeProvider.UtcNow)
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "[BrokerMailboxData.HydrateAndCheckExpiration] Subscription '{0}', EventType '{1}', User '{2}' has expired and will be removed from the mailbox.", brokerSubscription.SubscriptionId, (brokerSubscription.Parameters == null) ? "<NULL>" : brokerSubscription.Parameters.NotificationType.ToString(), brokerSubscription.Receiver.MailboxSmtp);
				subscriptionsToDelete.Add(brokerSubscription.StoreId);
				return null;
			}
			ExTraceGlobals.SubscriptionsTracer.TraceDebug((long)this.GetHashCode(), "[BrokerSubscriptionStore.HydrateAndCheckExpiration] Subscription '{0}' for event '{1}' hydrated for user '{2}'.  Expires '{3}'", new object[]
			{
				brokerSubscription.SubscriptionId,
				(brokerSubscription.Parameters == null) ? "<NULL>" : brokerSubscription.Parameters.NotificationType.ToString(),
				brokerSubscription.Receiver.MailboxSmtp,
				brokerSubscription.Expiration
			});
			return brokerSubscription;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00008B78 File Offset: 0x00006D78
		private BrokerSubscription HydrateSubscription(IStorePropertyBag propertyBag, MailboxSession session, IBrokerMailboxData brokerMailboxData)
		{
			BrokerSubscription brokerSubscription = new BrokerSubscription();
			brokerSubscription.StoreId = propertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null).ObjectId;
			brokerSubscription.SubscriptionId = propertyBag.GetValueOrDefault<Guid>(BrokerSubscriptionSchema.SubscriptionId, Guid.Empty);
			brokerSubscription.ConsumerId = (ConsumerId)Enum.Parse(typeof(ConsumerId), propertyBag.GetValueOrDefault<string>(BrokerSubscriptionSchema.ConsumerId, string.Empty));
			brokerSubscription.ChannelId = propertyBag.GetValueOrDefault<string>(BrokerSubscriptionSchema.ChannelId, string.Empty);
			brokerSubscription.Expiration = propertyBag.GetValueOrDefault<ExDateTime>(BrokerSubscriptionSchema.Expiration, ExDateTime.UtcNow).UniversalTime;
			brokerSubscription.Sender = new NotificationParticipant
			{
				OrganizationId = session.OrganizationId,
				DatabaseGuid = brokerMailboxData.DatabaseGuid,
				MailboxGuid = brokerMailboxData.MailboxGuid,
				MailboxSmtp = session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()
			};
			brokerSubscription.Receiver = new NotificationParticipant
			{
				MailboxGuid = propertyBag.GetValueOrDefault<Guid>(BrokerSubscriptionSchema.ReceiverMailboxGuid, Guid.Empty),
				MailboxSmtp = propertyBag.GetValueOrDefault<string>(BrokerSubscriptionSchema.ReceiverMailboxSmtp, string.Empty),
				FrontEndUrl = propertyBag.GetValueOrDefault<string>(BrokerSubscriptionSchema.ReceiverUrl, string.Empty)
			};
			brokerSubscription.Parameters = this.GetParameters(brokerSubscription.StoreId, propertyBag.GetValueOrDefault<string>(BrokerSubscriptionSchema.Parameters, string.Empty), session);
			brokerSubscription.MailboxData = brokerMailboxData;
			return brokerSubscription;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private BaseSubscription GetParameters(StoreObjectId storeId, string parameterString, MailboxSession session)
		{
			BaseSubscription result = null;
			if (parameterString != string.Empty)
			{
				if (parameterString.Length >= 255)
				{
					using (Item item = Item.Bind(session, storeId, new PropertyDefinition[]
					{
						BrokerSubscriptionSchema.Parameters
					}))
					{
						using (Stream stream = item.OpenPropertyStream(BrokerSubscriptionSchema.Parameters, PropertyOpenMode.ReadOnly))
						{
							using (StreamReader streamReader = new StreamReader(stream, Encoding.Unicode))
							{
								parameterString = streamReader.ReadToEnd();
								ExTraceGlobals.SubscriptionsTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BrokerSubscription.GetParameters] Parameter length was {0} for item {1}.  Required full bind.", parameterString.Length, storeId.ToBase64String());
								streamReader.Close();
							}
						}
					}
				}
				try
				{
					result = JsonConverter.Deserialize<BaseSubscription>(parameterString, null);
				}
				catch (SerializationException ex)
				{
					ExTraceGlobals.SubscriptionsTracer.TraceError((long)this.GetHashCode(), "[BrokerSubscription.GetParameters] Mailbox '{0}', Subscription '{1}' failed parameter deserialization with exception: '{2}'.  Data: '{3}'", new object[]
					{
						session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
						storeId.ToBase64String(),
						ex,
						parameterString
					});
					throw new InvalidBrokerSubscriptionOnLoadException(storeId.ToBase64String(), session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), ex);
				}
			}
			return result;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008E68 File Offset: 0x00007068
		private void LoadSubscriptions(MailboxSession session)
		{
			lock (this.instanceLock)
			{
				if (!this.SubscriptionsLoaded)
				{
					DateTime dateTime = DateTime.MaxValue;
					this.SubscriptionFolderId = BrokerMailboxData.GetSubscriptionFolderId(session, false);
					if (this.SubscriptionFolderId == null)
					{
						this.SubscriptionsLoaded = true;
					}
					else
					{
						int num = 0;
						ConcurrentDictionary<Guid, BrokerSubscription> concurrentDictionary = new ConcurrentDictionary<Guid, BrokerSubscription>();
						using (Folder folder = Folder.Bind(session, this.SubscriptionFolderId))
						{
							List<StoreObjectId> list = new List<StoreObjectId>();
							List<InvalidBrokerSubscriptionOnLoadException> list2 = null;
							int num2 = 0;
							using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, BrokerMailboxData.propertiesToFetch))
							{
								int num3 = 0;
								do
								{
									IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(100);
									num3 = propertyBags.Length;
									num += num3;
									foreach (IStorePropertyBag propertyBag in propertyBags)
									{
										try
										{
											BrokerSubscription brokerSubscription = this.HydrateAndCheckExpiration(propertyBag, session, list);
											if (brokerSubscription != null)
											{
												if (brokerSubscription.Expiration < dateTime)
												{
													dateTime = brokerSubscription.Expiration;
												}
												concurrentDictionary[brokerSubscription.SubscriptionId] = brokerSubscription;
											}
										}
										catch (InvalidBrokerSubscriptionOnLoadException ex)
										{
											num2++;
											ExTraceGlobals.SubscriptionsTracer.TraceError<string, InvalidBrokerSubscriptionOnLoadException>((long)this.GetHashCode(), "Failed to load subscription from mailbox {0} due to invalid data.   Exception: {1}", this.DisplayName, ex);
											if (list2 == null)
											{
												list2 = new List<InvalidBrokerSubscriptionOnLoadException>();
											}
											if (list2.Count < BrokerMailboxData.maxBadItemsToLog.Value)
											{
												list2.Add(ex);
											}
										}
									}
								}
								while (num3 > 0);
								if (list2 != null)
								{
									this.LogFailedSubscriptionLoad(num2, list2);
								}
								this.DeleteExpiredSubscriptions(session, list);
								ExTraceGlobals.SubscriptionsTracer.TraceDebug<int, int>((long)this.GetHashCode(), "[BrokerMailboxData.LoadSubscriptions]  Loaded {0} and Deleted {1} subscriptions", concurrentDictionary.Count, list.Count);
							}
							BrokerSubscriptionFlags brokerSubscriptionFlags = (concurrentDictionary.Count > 0) ? BrokerSubscriptionFlags.HasSubscriptions : BrokerSubscriptionFlags.NoSubscription;
							if (this.subscriptionFlags == null || (this.subscriptionFlags != null && this.subscriptionFlags.Value != brokerSubscriptionFlags))
							{
								BrokerMailboxData.SetSubscriptionFlagOnMailbox(session, brokerSubscriptionFlags);
							}
						}
						this.subscriptions = concurrentDictionary;
						this.SubscriptionsLoaded = true;
						this.NextExpirationTime = dateTime;
					}
				}
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000090D8 File Offset: 0x000072D8
		private void LogFailedSubscriptionLoad(int totalBadItems, List<InvalidBrokerSubscriptionOnLoadException> failedLoads)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (InvalidBrokerSubscriptionOnLoadException ex in failedLoads)
			{
				stringBuilder.AppendLine(ex.Message);
			}
			string text = stringBuilder.ToString();
			if (text.Length > 32000)
			{
				text = text.Substring(0, 32000) + "...";
			}
			NotificationsBrokerService.LogEvent(NotificationsBrokerEventLogConstants.Tuple_InvalidSubscriptionsOnLoad, this.MailboxGuid.ToString(), new object[]
			{
				this.DisplayName,
				totalBadItems,
				text
			});
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000091A0 File Offset: 0x000073A0
		private MailboxSession GetSession()
		{
			ADSessionSettings adSettings = (this.TenantPartitionHint == null) ? ADSessionSettings.FromRootOrgScopeSet() : ADSessionSettings.FromTenantPartitionHint(this.TenantPartitionHint);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromLocalServerMailboxGuid(adSettings, this.DatabaseGuid, this.MailboxGuid);
			return MailboxSession.OpenAsSystemService(mailboxOwner, CultureInfo.InvariantCulture, "Client=NotificationBroker");
		}

		// Token: 0x04000085 RID: 133
		public const string SubscriptionFolderName = "EventSubscriptions";

		// Token: 0x04000086 RID: 134
		public const string SubscriptionClass = "IPM.NotificationBroker.Subscription";

		// Token: 0x04000087 RID: 135
		public const int RowsToFetch = 100;

		// Token: 0x04000088 RID: 136
		public const int MaxViewStringLength = 255;

		// Token: 0x04000089 RID: 137
		private const int DisplayNameIndex = 1;

		// Token: 0x0400008A RID: 138
		private const int MailboxTypeIndex = 2;

		// Token: 0x0400008B RID: 139
		private const int MailboxTypeDetailIndex = 3;

		// Token: 0x0400008C RID: 140
		private const int NotificationBrokerSubscriptionsIndex = 4;

		// Token: 0x0400008D RID: 141
		private const int PersistableTenantPartitionHintIndex = 5;

		// Token: 0x0400008E RID: 142
		private static readonly ComparisonFilter FolderNameFilter = new ComparisonFilter(ComparisonOperator.Equal, FolderSchema.DisplayName, "EventSubscriptions");

		// Token: 0x0400008F RID: 143
		private static readonly SortBy[] folderNameSort = new SortBy[]
		{
			new SortBy(FolderSchema.DisplayName, SortOrder.Ascending)
		};

		// Token: 0x04000090 RID: 144
		private static readonly PropertyDefinition[] propertiesToFetch = new PropertyDefinition[]
		{
			ItemSchema.Id,
			BrokerSubscriptionSchema.SubscriptionId,
			BrokerSubscriptionSchema.ConsumerId,
			BrokerSubscriptionSchema.ChannelId,
			BrokerSubscriptionSchema.Expiration,
			BrokerSubscriptionSchema.ReceiverMailboxGuid,
			BrokerSubscriptionSchema.ReceiverMailboxSmtp,
			BrokerSubscriptionSchema.ReceiverUrl,
			BrokerSubscriptionSchema.Parameters
		};

		// Token: 0x04000091 RID: 145
		private static readonly PropertyDefinition[] notificationSubscriptionFlag = new PropertyDefinition[]
		{
			MailboxSchema.NotificationBrokerSubscriptions
		};

		// Token: 0x04000092 RID: 146
		private static readonly IntAppSettingsEntry maxBadItemsToLog = new IntAppSettingsEntry("NotificationsBroker.MaxBadItemsToLog", 100, ExTraceGlobals.SubscriptionsTracer);

		// Token: 0x04000093 RID: 147
		private object instanceLock = new object();

		// Token: 0x04000094 RID: 148
		private ConcurrentDictionary<Guid, BrokerSubscription> subscriptions;

		// Token: 0x04000095 RID: 149
		private BrokerSubscriptionFlags? subscriptionFlags;
	}
}
