using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000431 RID: 1073
	internal class NullInboundProxyDestinationPerfCounters : IInboundProxyDestinationPerfCounters
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000C4F8C File Offset: 0x000C318C
		public IExPerformanceCounter ConnectionsCurrent
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000C4F93 File Offset: 0x000C3193
		public IExPerformanceCounter ConnectionsTotal
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000C4F9A File Offset: 0x000C319A
		public IExPerformanceCounter MessagesReceivedTotal
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000C4FA1 File Offset: 0x000C31A1
		public IExPerformanceCounter MessageBytesReceivedTotal
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000C4FA8 File Offset: 0x000C31A8
		public IExPerformanceCounter RecipientsAccepted
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}
	}
}
