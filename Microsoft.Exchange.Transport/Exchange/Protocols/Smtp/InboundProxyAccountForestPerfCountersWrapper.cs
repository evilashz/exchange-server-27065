using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200042C RID: 1068
	internal class InboundProxyAccountForestPerfCountersWrapper : IInboundProxyDestinationPerfCounters
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x000C4A28 File Offset: 0x000C2C28
		public InboundProxyAccountForestPerfCountersWrapper(string instanceName)
		{
			this.perfCountersInstance = InboundProxyAccountForestPerfCounters.GetInstance(instanceName);
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000C4A3C File Offset: 0x000C2C3C
		public IExPerformanceCounter ConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.ConnectionsCurrent;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000C4A49 File Offset: 0x000C2C49
		public IExPerformanceCounter ConnectionsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsTotal;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000C4A56 File Offset: 0x000C2C56
		public IExPerformanceCounter MessagesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessagesReceivedTotal;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000C4A63 File Offset: 0x000C2C63
		public IExPerformanceCounter MessageBytesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessageBytesReceivedTotal;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000C4A70 File Offset: 0x000C2C70
		public IExPerformanceCounter RecipientsAccepted
		{
			get
			{
				return this.perfCountersInstance.InboundRecipientsAccepted;
			}
		}

		// Token: 0x04001809 RID: 6153
		private readonly InboundProxyAccountForestPerfCountersInstance perfCountersInstance;
	}
}
