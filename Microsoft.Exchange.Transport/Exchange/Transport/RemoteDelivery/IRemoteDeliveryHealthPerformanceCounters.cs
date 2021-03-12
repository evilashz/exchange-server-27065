using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003AD RID: 941
	internal interface IRemoteDeliveryHealthPerformanceCounters
	{
		// Token: 0x06002A41 RID: 10817
		void UpdateOutboundIPPoolPerfCounter(string pool, RiskLevel riskLevel, int percentageQueueErrors);

		// Token: 0x06002A42 RID: 10818
		void UpdateSmtpResponseSubCodePerfCounter(int code, int messageCount);
	}
}
