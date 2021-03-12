using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x0200001D RID: 29
	public class BrokerDatabaseData : IBrokerDatabaseData
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00007694 File Offset: 0x00005894
		static BrokerDatabaseData()
		{
			int num = MailboxTableQuery.RequiredMailboxTableProperties.Length;
			BrokerDatabaseData.RequiredMailboxTableProperties = new PropTag[num + 1];
			Array.Copy(MailboxTableQuery.RequiredMailboxTableProperties, BrokerDatabaseData.RequiredMailboxTableProperties, num);
			BrokerDatabaseData.RequiredMailboxTableProperties[num] = PropTag.NotificationBrokerSubscriptions;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000076E7 File Offset: 0x000058E7
		internal BrokerDatabaseData(DatabaseInfo databaseInfo)
		{
			this.databaseInfo = databaseInfo;
			this.mailboxData = new ConcurrentDictionary<Guid, BrokerMailboxData>();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00007701 File Offset: 0x00005901
		public string Name
		{
			get
			{
				return this.databaseInfo.DisplayName;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000770E File Offset: 0x0000590E
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseInfo.Guid;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000771B File Offset: 0x0000591B
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00007723 File Offset: 0x00005923
		public int TotalMailboxes { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000772C File Offset: 0x0000592C
		internal ConcurrentDictionary<Guid, BrokerMailboxData> MailboxData
		{
			get
			{
				return this.mailboxData;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007734 File Offset: 0x00005934
		internal void AddMailbox(Guid mailboxGuid, bool wasMoved = false)
		{
			this.LoadThisMailbox(mailboxGuid, wasMoved);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00007740 File Offset: 0x00005940
		internal void RemoveMailbox(Guid mailboxGuid)
		{
			BrokerMailboxData brokerMailboxData = null;
			if (this.MailboxData.TryRemove(mailboxGuid, out brokerMailboxData) & TestHooks.OnMailboxRemoveEvent != null)
			{
				TestHooks.OnMailboxRemoveEvent(mailboxGuid, mailboxGuid);
			}
			if (brokerMailboxData != null && brokerMailboxData.Subscriptions != null && brokerMailboxData.Subscriptions.Count > 0)
			{
				brokerMailboxData.CallRemoveForAllHandlers();
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00007798 File Offset: 0x00005998
		internal void CallRemoveForAllHandlers()
		{
			foreach (KeyValuePair<Guid, BrokerMailboxData> keyValuePair in this.MailboxData)
			{
				keyValuePair.Value.CallRemoveForAllHandlers();
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000077EC File Offset: 0x000059EC
		internal DatabaseSnapshot GetDiagnostics(Func<BrokerSubscription, bool> subscriptionFilter)
		{
			DatabaseSnapshot databaseSnapshot = new DatabaseSnapshot();
			databaseSnapshot.DatabaseName = this.Name;
			databaseSnapshot.MdbGuid = this.DatabaseGuid;
			databaseSnapshot.TotalMailboxCount = this.TotalMailboxes;
			databaseSnapshot.Mailboxes = new List<MailboxSnapshot>(databaseSnapshot.TotalMailboxCount);
			foreach (KeyValuePair<Guid, BrokerMailboxData> keyValuePair in this.MailboxData)
			{
				if (keyValuePair.Value.Subscriptions != null && keyValuePair.Value.Subscriptions.Count > 0)
				{
					MailboxSnapshot diagnostics = keyValuePair.Value.GetDiagnostics(subscriptionFilter);
					if ((subscriptionFilter == null && diagnostics.SubscriptionCount > 0) || (subscriptionFilter != null && (diagnostics.Subscriptions != null & diagnostics.Subscriptions.Count > 0)))
					{
						databaseSnapshot.SubscribedMailboxCount++;
						databaseSnapshot.Mailboxes.Add(diagnostics);
					}
				}
			}
			return databaseSnapshot;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000078E8 File Offset: 0x00005AE8
		internal void LoadAllMailboxes()
		{
			List<MailboxInformation> mailboxTable = this.databaseInfo.GetMailboxTable(ClientType.EventBased, BrokerDatabaseData.AdditionalMailboxTableProperties);
			this.TotalMailboxes = mailboxTable.Count;
			foreach (MailboxInformation record in mailboxTable)
			{
				this.LoadThisMailbox(record, false);
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00007958 File Offset: 0x00005B58
		private void LoadThisMailbox(Guid mailboxGuid, bool wasMoved = false)
		{
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=NotificationBroker", null, null, null, null))
			{
				try
				{
					PropValue[][] mailboxTableInfo = exRpcAdmin.GetMailboxTableInfo(this.databaseInfo.Guid, mailboxGuid, BrokerDatabaseData.RequiredMailboxTableProperties);
					if (mailboxTableInfo.Length != 1 || mailboxTableInfo[0].Length < 1)
					{
						throw new MailboxNotFoundException(ServiceStrings.MailboxNotFoundInDatabaseExceptionError(mailboxGuid, this.databaseInfo.Guid));
					}
					MailboxInformation record = MailboxInformation.Create(mailboxGuid.ToByteArray(), this.databaseInfo.Guid, mailboxGuid.ToString(), ControlData.Empty, mailboxTableInfo[0], MailboxInformation.GetLastLogonTime(mailboxTableInfo[0]), null);
					this.LoadThisMailbox(record, wasMoved);
				}
				catch (MapiExceptionNotFound arg)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug<BrokerDatabaseData, MapiExceptionNotFound>((long)this.GetHashCode(), "{0}: Mailbox does not exist on the store: {1}", this, arg);
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007A34 File Offset: 0x00005C34
		private void LoadThisMailbox(MailboxInformation record, bool wasMoved = false)
		{
			Guid mailboxGuid = record.MailboxGuid;
			if (this.mailboxData.ContainsKey(mailboxGuid))
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid>((long)this.GetHashCode(), "[BrokerDatabasedata.LoadThisMailbox] Mailbox {0} is already in the cache.  Skipping.", mailboxGuid);
				return;
			}
			BrokerMailboxData brokerMailboxData = new BrokerMailboxData(this, record);
			if (this.mailboxData.TryAdd(mailboxGuid, brokerMailboxData))
			{
				ExTraceGlobals.SubscriptionsTracer.TraceDebug<Guid, DatabaseInfo>((long)this.GetHashCode(), "[BrokerDatabasedata.LoadThisMailbox] Mailbox {0} added to {1}'s cache successfully.", mailboxGuid, this.databaseInfo);
				brokerMailboxData.LoadSubscriptions(wasMoved);
				return;
			}
			ExTraceGlobals.SubscriptionsTracer.TraceWarning<Guid, DatabaseInfo>((long)this.GetHashCode(), "[BrokerDatabasedata.LoadThisMailbox] Mailbox {0} failed to add to {1}'s cache.", mailboxGuid, this.databaseInfo);
		}

		// Token: 0x04000080 RID: 128
		private static readonly PropertyTagPropertyDefinition[] AdditionalMailboxTableProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.NotificationBrokerSubscriptions
		};

		// Token: 0x04000081 RID: 129
		private static readonly PropTag[] RequiredMailboxTableProperties;

		// Token: 0x04000082 RID: 130
		private readonly DatabaseInfo databaseInfo;

		// Token: 0x04000083 RID: 131
		private readonly ConcurrentDictionary<Guid, BrokerMailboxData> mailboxData;
	}
}
