using System;
using System.Collections.Generic;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200025E RID: 606
	internal class RoutingPerformanceCounters
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x0006AF68 File Offset: 0x00069168
		public RoutingPerformanceCounters(ProcessTransportRole transportRole)
		{
			this.transportRole = transportRole;
			this.Initialize();
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006AF7D File Offset: 0x0006917D
		public virtual void IncrementRoutingNdrs()
		{
			if (this.instance != null)
			{
				this.instance.RoutingNdrsTotal.Increment();
			}
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006AF98 File Offset: 0x00069198
		public virtual void IncrementRoutingTablesCalculated()
		{
			if (this.instance != null)
			{
				this.instance.RoutingTablesCalculatedTotal.Increment();
			}
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006AFB3 File Offset: 0x000691B3
		public virtual void IncrementRoutingTablesChanged()
		{
			if (this.instance != null)
			{
				this.instance.RoutingTablesChangedTotal.Increment();
			}
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0006AFD0 File Offset: 0x000691D0
		protected virtual void Initialize()
		{
			try
			{
				RoutingPerfCounters.SetCategoryName(RoutingPerformanceCounters.perfCounterCategoryMap[this.transportRole]);
				this.instance = RoutingPerfCounters.GetInstance("_total");
			}
			catch (InvalidOperationException ex)
			{
				RoutingDiag.Tracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Failed to initialize performance counters: {0}", ex);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingPerfCountersLoadFailure, null, new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x04000C79 RID: 3193
		private const string InstanceName = "_total";

		// Token: 0x04000C7A RID: 3194
		private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport Routing"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport Routing"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport Routing"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery Routing"
			},
			{
				ProcessTransportRole.MailboxSubmission,
				"MSExchange Submission Routing"
			}
		};

		// Token: 0x04000C7B RID: 3195
		private RoutingPerfCountersInstance instance;

		// Token: 0x04000C7C RID: 3196
		private ProcessTransportRole transportRole;
	}
}
