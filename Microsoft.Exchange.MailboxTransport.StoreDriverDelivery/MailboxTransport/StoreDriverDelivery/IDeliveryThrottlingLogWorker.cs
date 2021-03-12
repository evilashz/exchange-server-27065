using System;
using System.Collections.Generic;
using Microsoft.Exchange.MailboxTransport.Delivery;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000026 RID: 38
	internal interface IDeliveryThrottlingLogWorker
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001FF RID: 511
		IDeliveryThrottlingLog DeliveryThrottlingLog { get; }

		// Token: 0x06000200 RID: 512
		void TrackMDBServerThrottle(bool isThrottle, double mdbServerThreadThreshold);

		// Token: 0x06000201 RID: 513
		void TrackMDBThrottle(bool isThrottle, string mdbName, double mdbResourceThreshold, List<KeyValuePair<string, double>> healthMonitorList, ThrottlingResource throttleResource);

		// Token: 0x06000202 RID: 514
		void TrackRecipientThrottle(bool isThrottle, string recipient, Guid orgID, string mdbName, double recipientThreadThreshold);

		// Token: 0x06000203 RID: 515
		void TrackConcurrentMessageSizeThrottle(bool isThrottle, ulong concurrentMessageSizeThreshold, int numOfRecipients);
	}
}
