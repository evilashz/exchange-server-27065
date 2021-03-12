using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B6 RID: 1206
	internal interface ISmtpReceivePerfCounters
	{
		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06003664 RID: 13924
		IExPerformanceCounter ConnectionsCurrent { get; }

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06003665 RID: 13925
		IExPerformanceCounter ConnectionsTotal { get; }

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06003666 RID: 13926
		IExPerformanceCounter TlsConnectionsCurrent { get; }

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06003667 RID: 13927
		IExPerformanceCounter TlsConnectionsRejectedDueToRateExceeded { get; }

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06003668 RID: 13928
		IExPerformanceCounter ConnectionsDroppedByAgentsTotal { get; }

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06003669 RID: 13929
		IExPerformanceCounter InboundMessageConnectionsCurrent { get; }

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x0600366A RID: 13930
		IExPerformanceCounter InboundMessageConnectionsTotal { get; }

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x0600366B RID: 13931
		IExPerformanceCounter MessagesReceivedTotal { get; }

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x0600366C RID: 13932
		IExPerformanceCounter MessageBytesReceivedTotal { get; }

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x0600366D RID: 13933
		IExPerformanceCounter MessagesRefusedForSize { get; }

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x0600366E RID: 13934
		IExPerformanceCounter MessagesReceivedWithBareLinefeeds { get; }

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600366F RID: 13935
		IExPerformanceCounter MessagesRefusedForBareLinefeeds { get; }

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06003670 RID: 13936
		IExPerformanceCounter MessagesReceivedForNonProvisionedUsers { get; }

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06003671 RID: 13937
		IExPerformanceCounter RecipientsAccepted { get; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003672 RID: 13938
		IExPerformanceCounter TotalBytesReceived { get; }

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003673 RID: 13939
		IExPerformanceCounter TarpittingDelaysAuthenticated { get; }

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003674 RID: 13940
		IExPerformanceCounter TarpittingDelaysAnonymous { get; }

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003675 RID: 13941
		IExPerformanceCounter TarpittingDelaysBackpressure { get; }

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003676 RID: 13942
		IExPerformanceCounter TlsNegotiationsFailed { get; }
	}
}
