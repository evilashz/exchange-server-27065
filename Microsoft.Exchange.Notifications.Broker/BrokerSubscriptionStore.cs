using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000022 RID: 34
	internal sealed class BrokerSubscriptionStore : IMailboxChangeHandler
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000093F6 File Offset: 0x000075F6
		internal BrokerSubscriptionStore()
		{
			this.onlineDatabases = new ConcurrentDictionary<Guid, BrokerDatabaseData>();
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00009409 File Offset: 0x00007609
		public static BrokerSubscriptionStore Instance
		{
			get
			{
				return BrokerSubscriptionStore.instance;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00009410 File Offset: 0x00007610
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00009418 File Offset: 0x00007618
		public ExDateTime LastUpdateTime { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00009421 File Offset: 0x00007621
		public ConcurrentDictionary<Guid, BrokerDatabaseData> Databases
		{
			get
			{
				return this.onlineDatabases;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000942C File Offset: 0x0000762C
		public void HandleDatabaseStart(DatabaseInfo databaseInfo)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseStart begin: {0}", databaseInfo);
			BrokerDatabaseData brokerDatabaseData = new BrokerDatabaseData(databaseInfo);
			if (this.onlineDatabases.TryAdd(databaseInfo.Guid, brokerDatabaseData))
			{
				brokerDatabaseData.LoadAllMailboxes();
			}
			else
			{
				ExTraceGlobals.MailboxChangeTracer.TraceWarning<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseStart fail to add {0}", databaseInfo);
			}
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseStart end: {0}", databaseInfo);
			this.LastUpdateTime = ExDateTime.UtcNow;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000094AC File Offset: 0x000076AC
		public void HandleDatabaseShutdown(DatabaseInfo databaseInfo)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseShutdown begin: {0}", databaseInfo);
			BrokerDatabaseData brokerDatabaseData;
			if (this.onlineDatabases.TryRemove(databaseInfo.Guid, out brokerDatabaseData))
			{
				brokerDatabaseData.CallRemoveForAllHandlers();
			}
			else
			{
				ExTraceGlobals.MailboxChangeTracer.TraceWarning<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseShutdown fail to remove {0}", databaseInfo);
			}
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo>((long)this.GetHashCode(), "HandleDatabaseShutdown end: {0}", databaseInfo);
			this.LastUpdateTime = ExDateTime.UtcNow;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00009528 File Offset: 0x00007728
		public void HandleMailboxCreatedOrConnected(DatabaseInfo databaseInfo, Guid mailboxGuid)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxCreatedOrConnected begin: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			BrokerDatabaseData brokerDatabaseData;
			if (this.onlineDatabases.TryGetValue(databaseInfo.Guid, out brokerDatabaseData))
			{
				brokerDatabaseData.AddMailbox(mailboxGuid, false);
			}
			else
			{
				ExTraceGlobals.MailboxChangeTracer.TraceWarning<DatabaseInfo>((long)this.GetHashCode(), "HandleMailboxCreatedOrConnected did not find the given database {0}", databaseInfo);
			}
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxCreatedOrConnected end: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			this.LastUpdateTime = ExDateTime.UtcNow;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000095A8 File Offset: 0x000077A8
		public void HandleMailboxMoveSucceeded(DatabaseInfo databaseInfo, Guid mailboxGuid)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxMoveSucceeded begin: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			BrokerDatabaseData brokerDatabaseData;
			if (this.onlineDatabases.TryGetValue(databaseInfo.Guid, out brokerDatabaseData))
			{
				brokerDatabaseData.AddMailbox(mailboxGuid, true);
			}
			else
			{
				ExTraceGlobals.MailboxChangeTracer.TraceWarning<DatabaseInfo>((long)this.GetHashCode(), "HandleMailboxMoveSucceeded did not find the given database {0}", databaseInfo);
			}
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxMoveSucceeded end: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			this.LastUpdateTime = ExDateTime.UtcNow;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009628 File Offset: 0x00007828
		public void HandleMailboxDeletedOrDisconnected(DatabaseInfo databaseInfo, Guid mailboxGuid)
		{
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxDeletedOrDisconnected begin: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			BrokerDatabaseData brokerDatabaseData;
			if (this.onlineDatabases.TryGetValue(databaseInfo.Guid, out brokerDatabaseData))
			{
				brokerDatabaseData.RemoveMailbox(mailboxGuid);
			}
			else
			{
				ExTraceGlobals.MailboxChangeTracer.TraceWarning<DatabaseInfo>((long)this.GetHashCode(), "HandleMailboxDeletedOrDisconnected did not find the given database {0}", databaseInfo);
			}
			ExTraceGlobals.MailboxChangeTracer.TraceFunction<DatabaseInfo, Guid>((long)this.GetHashCode(), "HandleMailboxDeletedOrDisconnected end: {0}, mbxguid: {1}", databaseInfo, mailboxGuid);
			this.LastUpdateTime = ExDateTime.UtcNow;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000096A8 File Offset: 0x000078A8
		internal DatabasesSnapshot GetDiagnostics(Func<BrokerSubscription, bool> subscriptionFilter)
		{
			DatabasesSnapshot databasesSnapshot = new DatabasesSnapshot();
			databasesSnapshot.LastSnapshotUtc = this.LastUpdateTime.ToISOString();
			databasesSnapshot.Databases = new List<DatabaseSnapshot>(this.onlineDatabases.Count);
			foreach (KeyValuePair<Guid, BrokerDatabaseData> keyValuePair in this.onlineDatabases)
			{
				databasesSnapshot.Databases.Add(keyValuePair.Value.GetDiagnostics(subscriptionFilter));
			}
			return databasesSnapshot;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009738 File Offset: 0x00007938
		internal bool MailboxIsHostedLocally(Guid mailboxGuid)
		{
			if (Guid.Empty == mailboxGuid)
			{
				return false;
			}
			foreach (BrokerDatabaseData brokerDatabaseData in this.onlineDatabases.Values)
			{
				if (brokerDatabaseData.MailboxData.ContainsKey(mailboxGuid))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000097A8 File Offset: 0x000079A8
		internal bool MailboxIsHostedLocally(Guid databaseGuid, Guid mailboxGuid)
		{
			BrokerDatabaseData brokerDatabaseData;
			return !(Guid.Empty == mailboxGuid) && !(Guid.Empty == databaseGuid) && this.onlineDatabases.TryGetValue(databaseGuid, out brokerDatabaseData) && brokerDatabaseData.MailboxData.ContainsKey(mailboxGuid);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000097F0 File Offset: 0x000079F0
		internal void Subscribe(BrokerSubscription subscription)
		{
			BrokerMailboxData mailboxData = this.GetMailboxData(subscription);
			if (!mailboxData.SubscriptionsLoaded)
			{
				mailboxData.LoadSubscriptions(false);
			}
			mailboxData.Save(subscription);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000981C File Offset: 0x00007A1C
		internal void Unsubscribe(BrokerSubscription subscription)
		{
			BrokerMailboxData mailboxData = this.GetMailboxData(subscription);
			if (!mailboxData.SubscriptionsLoaded)
			{
				mailboxData.LoadSubscriptions(false);
			}
			mailboxData.RemoveSubscription(subscription);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009848 File Offset: 0x00007A48
		internal void ClearDatabaseSnapshotForTest()
		{
			this.onlineDatabases = new ConcurrentDictionary<Guid, BrokerDatabaseData>();
			this.LastUpdateTime = ExDateTime.MinValue;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00009860 File Offset: 0x00007A60
		internal BrokerMailboxData GetMailboxData(BrokerSubscription subscription)
		{
			return this.GetMailboxData(subscription.Sender.DatabaseGuid, subscription.Sender.MailboxGuid);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009880 File Offset: 0x00007A80
		internal BrokerMailboxData GetMailboxData(Guid mdbGuid, Guid mbxGuid)
		{
			BrokerMailboxData result = null;
			BrokerDatabaseData brokerDatabaseData = null;
			if (!this.onlineDatabases.TryGetValue(mdbGuid, out brokerDatabaseData))
			{
				throw new DatabaseNotFoundException(mdbGuid.ToString());
			}
			if (!brokerDatabaseData.MailboxData.TryGetValue(mbxGuid, out result))
			{
				throw new BrokerMailboxNotFoundException(mbxGuid);
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000098CC File Offset: 0x00007ACC
		internal void CleanupExpiredSubscriptions()
		{
			foreach (BrokerDatabaseData brokerDatabaseData in this.onlineDatabases.Values)
			{
				foreach (BrokerMailboxData brokerMailboxData in brokerDatabaseData.MailboxData.Values)
				{
					brokerMailboxData.CheckExpiration();
				}
			}
		}

		// Token: 0x040000A4 RID: 164
		private static readonly BrokerSubscriptionStore instance = new BrokerSubscriptionStore();

		// Token: 0x040000A5 RID: 165
		private ConcurrentDictionary<Guid, BrokerDatabaseData> onlineDatabases;
	}
}
