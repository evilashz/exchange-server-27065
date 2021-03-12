using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004C8 RID: 1224
	internal class SmtpReceivePerfCountersWrapper : ISmtpReceivePerfCounters
	{
		// Token: 0x0600385C RID: 14428 RVA: 0x000E8087 File Offset: 0x000E6287
		public SmtpReceivePerfCountersWrapper(string instanceName)
		{
			this.perfCountersInstance = SmtpReceivePerfCounters.GetInstance(instanceName);
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000E809B File Offset: 0x000E629B
		public IExPerformanceCounter ConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.ConnectionsCurrent;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000E80A8 File Offset: 0x000E62A8
		public IExPerformanceCounter ConnectionsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsTotal;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000E80B5 File Offset: 0x000E62B5
		public IExPerformanceCounter TlsConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.TlsConnectionsCurrent;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06003860 RID: 14432 RVA: 0x000E80C2 File Offset: 0x000E62C2
		public IExPerformanceCounter TlsNegotiationsFailed
		{
			get
			{
				return this.perfCountersInstance.TlsNegotiationsFailed;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000E80CF File Offset: 0x000E62CF
		public IExPerformanceCounter TlsConnectionsRejectedDueToRateExceeded
		{
			get
			{
				return this.perfCountersInstance.TlsConnectionsRejectedDueToRateExceeded;
			}
		}

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000E80DC File Offset: 0x000E62DC
		public IExPerformanceCounter ConnectionsDroppedByAgentsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsDroppedByAgentsTotal;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000E80E9 File Offset: 0x000E62E9
		public IExPerformanceCounter InboundMessageConnectionsCurrent
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06003864 RID: 14436 RVA: 0x000E80F0 File Offset: 0x000E62F0
		public IExPerformanceCounter InboundMessageConnectionsTotal
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x000E80F7 File Offset: 0x000E62F7
		public IExPerformanceCounter MessagesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.MessagesReceivedTotal;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06003866 RID: 14438 RVA: 0x000E8104 File Offset: 0x000E6304
		public IExPerformanceCounter MessageBytesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.MessageBytesReceivedTotal;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000E8111 File Offset: 0x000E6311
		public IExPerformanceCounter MessagesRefusedForSize
		{
			get
			{
				return this.perfCountersInstance.MessagesRefusedForSize;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000E811E File Offset: 0x000E631E
		public IExPerformanceCounter MessagesReceivedWithBareLinefeeds
		{
			get
			{
				return this.perfCountersInstance.MessagesReceivedWithBareLinefeeds;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x000E812B File Offset: 0x000E632B
		public IExPerformanceCounter MessagesReceivedForNonProvisionedUsers
		{
			get
			{
				return this.perfCountersInstance.MessagesReceivedForNonProvisionedUsers;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000E8138 File Offset: 0x000E6338
		public IExPerformanceCounter MessagesRefusedForBareLinefeeds
		{
			get
			{
				return this.perfCountersInstance.MessagesRefusedForBareLinefeeds;
			}
		}

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000E8145 File Offset: 0x000E6345
		public IExPerformanceCounter RecipientsAccepted
		{
			get
			{
				return this.perfCountersInstance.RecipientsAccepted;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000E8152 File Offset: 0x000E6352
		public IExPerformanceCounter TotalBytesReceived
		{
			get
			{
				return this.perfCountersInstance.TotalBytesReceived;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x000E815F File Offset: 0x000E635F
		public IExPerformanceCounter TarpittingDelaysAuthenticated
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysAuthenticated;
			}
		}

		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000E816C File Offset: 0x000E636C
		public IExPerformanceCounter TarpittingDelaysAnonymous
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysAnonymous;
			}
		}

		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000E8179 File Offset: 0x000E6379
		public IExPerformanceCounter TarpittingDelaysBackpressure
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysBackpressure;
			}
		}

		// Token: 0x04001CF4 RID: 7412
		private readonly SmtpReceivePerfCountersInstance perfCountersInstance;
	}
}
