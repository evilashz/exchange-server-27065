using System;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.MailboxTransport.Delivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000021 RID: 33
	internal interface IDeliveryThrottling : IDisposable
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001BC RID: 444
		XElement DeliveryRecipientDiagnostics { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001BD RID: 445
		XElement DeliveryDatabaseDiagnostics { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001BE RID: 446
		XElement DeliveryServerDiagnostics { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BF RID: 447
		IMailboxDatabaseCollectionManager MailboxDatabaseCollectionManager { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001C0 RID: 448
		IDeliveryThrottlingLog DeliveryThrottlingLog { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001C1 RID: 449
		IDeliveryThrottlingLogWorker DeliveryThrottlingLogWorker { get; }

		// Token: 0x060001C2 RID: 450
		bool CheckAndTrackThrottleServer(long smtpSessionId);

		// Token: 0x060001C3 RID: 451
		void UpdateMdbThreadCounters();

		// Token: 0x060001C4 RID: 452
		bool CheckAndTrackThrottleMDB(Guid databaseGuid, long smtpSessionId, out List<KeyValuePair<string, double>> mdbHealthMonitorValues);

		// Token: 0x060001C5 RID: 453
		bool CheckAndTrackDynamicThrottleMDBPendingConnections(Guid databaseGuid, IMailboxDatabaseConnectionManager mdbConnectionManager, long smtpSessionId, IPAddress sessionRemoteEndPointAddress, out List<KeyValuePair<string, double>> mdbHealthMonitorValues);

		// Token: 0x060001C6 RID: 454
		bool CheckAndTrackDynamicThrottleMDBTimeout(Guid databaseGuid, IMailboxDatabaseConnectionInfo mdbConnectionInfo, IMailboxDatabaseConnectionManager mdbConnectionManager, long smtpSessionId, IPAddress sessionRemoteEndPointAddress, TimeSpan connectionWaitTime, List<KeyValuePair<string, double>> mdbHealthMonitorValues);

		// Token: 0x060001C7 RID: 455
		bool CheckAndTrackThrottleRecipient(RoutingAddress recipient, long smtpSessionId, string mdbName, Guid tenantId);

		// Token: 0x060001C8 RID: 456
		bool CheckAndTrackThrottleConcurrentMessageSizeLimit(long smtpSessionId, int numOfRecipients);

		// Token: 0x060001C9 RID: 457
		void SetSessionMessageSize(long messageSize, long smtpSessionId);

		// Token: 0x060001CA RID: 458
		bool TryGetDatabaseHealth(Guid databaseGuid, out int health);

		// Token: 0x060001CB RID: 459
		bool TryGetDatabaseHealth(Guid databaseGuid, out int health, out List<KeyValuePair<string, double>> monitorHealthValues);

		// Token: 0x060001CC RID: 460
		void ResetSession(long smtpSessionId);

		// Token: 0x060001CD RID: 461
		void ClearSession(long smtpSessionId);

		// Token: 0x060001CE RID: 462
		void DecrementRecipient(long smtpSessionId, RoutingAddress recipient);

		// Token: 0x060001CF RID: 463
		void DecrementCurrentMessageSize(long smtpSessionId);

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D0 RID: 464
		GetMDBThreadLimitAndHealth GetMDBThreadLimitAndHealth { get; }
	}
}
