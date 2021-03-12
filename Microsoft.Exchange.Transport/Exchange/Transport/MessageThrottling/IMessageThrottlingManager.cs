using System;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Transport.MessageThrottling
{
	// Token: 0x0200012E RID: 302
	internal interface IMessageThrottlingManager
	{
		// Token: 0x06000D70 RID: 3440
		void CleanupIdleEntries();

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D71 RID: 3441
		bool Enabled { get; }

		// Token: 0x06000D72 RID: 3442
		MessageThrottlingReason ShouldThrottleMessage(IPAddress ipAddress, int receiveConnectorLimit, MessageRateSourceFlags messageRateSource);

		// Token: 0x06000D73 RID: 3443
		MessageThrottlingReason ShouldThrottleMessage(Guid mailboxGuid, int userMessageRateLimit, IPAddress ipAddress, int receiveConnectorLimit, MessageRateSourceFlags messageRateSource);

		// Token: 0x06000D74 RID: 3444
		MessageThrottlingReason ShouldThrottleMessage(Guid mailboxGuid, int messageRateLimit);
	}
}
