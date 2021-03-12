using System;
using System.Collections.Generic;
using System.Net;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000030 RID: 48
	internal interface IMailboxDatabaseConnectionManager : IDisposable
	{
		// Token: 0x06000241 RID: 577
		bool AddConnection(long smtpSessionId, IPAddress remoteIPAddress);

		// Token: 0x06000242 RID: 578
		bool GetMdbHealthAndAddConnection(long smtpSessionId, IPAddress remoteIPAddress, out int mdbHealthMeasure, out List<KeyValuePair<string, double>> healthMonitorList, out int currentConnectionLimit);

		// Token: 0x06000243 RID: 579
		bool RemoveConnection(long smtpSessionId, IPAddress remoteIPAddress);

		// Token: 0x06000244 RID: 580
		IMailboxDatabaseConnectionInfo Acquire(long smtpSessionId, IPAddress remoteIPAddress, TimeSpan timeout);

		// Token: 0x06000245 RID: 581
		bool TryAcquire(long smtpSessionId, IPAddress remoteIPAddress, TimeSpan timeout, out IMailboxDatabaseConnectionInfo mdbConnectionInfo);

		// Token: 0x06000246 RID: 582
		bool Release(ref IMailboxDatabaseConnectionInfo mailboxDatabaseConnectionInfo);

		// Token: 0x06000247 RID: 583
		void UpdateLastActivityTime(long smtpSessionId);
	}
}
