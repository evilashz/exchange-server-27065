using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004C9 RID: 1225
	internal class SmtpReceivePerfCountersFrontendWrapper : ISmtpReceivePerfCounters
	{
		// Token: 0x06003870 RID: 14448 RVA: 0x000E8186 File Offset: 0x000E6386
		public SmtpReceivePerfCountersFrontendWrapper(string instanceName)
		{
			this.perfCountersInstance = SmtpReceiveFrontendPerfCounters.GetInstance(instanceName);
		}

		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000E819A File Offset: 0x000E639A
		public IExPerformanceCounter ConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.ConnectionsCurrent;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000E81A7 File Offset: 0x000E63A7
		public IExPerformanceCounter ConnectionsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsTotal;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000E81B4 File Offset: 0x000E63B4
		public IExPerformanceCounter TlsConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.TlsConnectionsCurrent;
			}
		}

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000E81C1 File Offset: 0x000E63C1
		public IExPerformanceCounter TlsNegotiationsFailed
		{
			get
			{
				return this.perfCountersInstance.TlsNegotiationsFailed;
			}
		}

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000E81CE File Offset: 0x000E63CE
		public IExPerformanceCounter TlsConnectionsRejectedDueToRateExceeded
		{
			get
			{
				return this.perfCountersInstance.TlsConnectionsRejectedDueToRateExceeded;
			}
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000E81DB File Offset: 0x000E63DB
		public IExPerformanceCounter ConnectionsDroppedByAgentsTotal
		{
			get
			{
				return this.perfCountersInstance.ConnectionsDroppedByAgentsTotal;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000E81E8 File Offset: 0x000E63E8
		public IExPerformanceCounter InboundMessageConnectionsCurrent
		{
			get
			{
				return this.perfCountersInstance.InboundMessageConnectionsCurrent;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x000E81F5 File Offset: 0x000E63F5
		public IExPerformanceCounter InboundMessageConnectionsTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessageConnectionsTotal;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06003879 RID: 14457 RVA: 0x000E8202 File Offset: 0x000E6402
		public IExPerformanceCounter MessagesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessagesReceivedTotal;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x0600387A RID: 14458 RVA: 0x000E820F File Offset: 0x000E640F
		public IExPerformanceCounter MessageBytesReceivedTotal
		{
			get
			{
				return this.perfCountersInstance.InboundMessageBytesReceivedTotal;
			}
		}

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600387B RID: 14459 RVA: 0x000E821C File Offset: 0x000E641C
		public IExPerformanceCounter MessagesRefusedForSize
		{
			get
			{
				return this.perfCountersInstance.InboundMessagesRefusedForSize;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600387C RID: 14460 RVA: 0x000E8229 File Offset: 0x000E6429
		public IExPerformanceCounter MessagesReceivedWithBareLinefeeds
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600387D RID: 14461 RVA: 0x000E8230 File Offset: 0x000E6430
		public IExPerformanceCounter MessagesReceivedForNonProvisionedUsers
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600387E RID: 14462 RVA: 0x000E8237 File Offset: 0x000E6437
		public IExPerformanceCounter MessagesRefusedForBareLinefeeds
		{
			get
			{
				return TransportNoopExPerformanceCounter.Instance;
			}
		}

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000E823E File Offset: 0x000E643E
		public IExPerformanceCounter RecipientsAccepted
		{
			get
			{
				return this.perfCountersInstance.InboundRecipientsAccepted;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06003880 RID: 14464 RVA: 0x000E824B File Offset: 0x000E644B
		public IExPerformanceCounter TotalBytesReceived
		{
			get
			{
				return this.perfCountersInstance.TotalBytesReceived;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x000E8258 File Offset: 0x000E6458
		public IExPerformanceCounter TarpittingDelaysAuthenticated
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysAuthenticated;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x000E8265 File Offset: 0x000E6465
		public IExPerformanceCounter TarpittingDelaysAnonymous
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysAnonymous;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000E8272 File Offset: 0x000E6472
		public IExPerformanceCounter TarpittingDelaysBackpressure
		{
			get
			{
				return this.perfCountersInstance.TarpittingDelaysBackpressure;
			}
		}

		// Token: 0x04001CF5 RID: 7413
		private readonly SmtpReceiveFrontendPerfCountersInstance perfCountersInstance;
	}
}
