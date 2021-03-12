using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200041F RID: 1055
	internal interface IInboundProxyDestinationPerfCounters
	{
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060030DA RID: 12506
		IExPerformanceCounter ConnectionsCurrent { get; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060030DB RID: 12507
		IExPerformanceCounter ConnectionsTotal { get; }

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060030DC RID: 12508
		IExPerformanceCounter MessagesReceivedTotal { get; }

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060030DD RID: 12509
		IExPerformanceCounter MessageBytesReceivedTotal { get; }

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060030DE RID: 12510
		IExPerformanceCounter RecipientsAccepted { get; }
	}
}
