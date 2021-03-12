using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200042D RID: 1069
	internal class InboundProxyDestinationPerfCountersWrapper : IInboundProxyDestinationPerfCounters
	{
		// Token: 0x06003145 RID: 12613 RVA: 0x000C4A7D File Offset: 0x000C2C7D
		public InboundProxyDestinationPerfCountersWrapper(string instanceName)
		{
			this.perfCountersInstance = InboundProxyDestinationPerfCounters.GetInstance(instanceName);
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000C4A91 File Offset: 0x000C2C91
		public IExPerformanceCounter ConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.ConnectionsCurrent;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000C4A9E File Offset: 0x000C2C9E
		public IExPerformanceCounter ConnectionsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsTotal;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000C4AAB File Offset: 0x000C2CAB
		public IExPerformanceCounter MessagesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessagesReceivedTotal;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000C4AB8 File Offset: 0x000C2CB8
		public IExPerformanceCounter MessageBytesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessageBytesReceivedTotal;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000C4AC5 File Offset: 0x000C2CC5
		public IExPerformanceCounter RecipientsAccepted
		{
			get
			{
				return this.perfCountersInstance.InboundRecipientsAccepted;
			}
		}

		// Token: 0x0400180A RID: 6154
		private readonly InboundProxyDestinationPerfCountersInstance perfCountersInstance;
	}
}
