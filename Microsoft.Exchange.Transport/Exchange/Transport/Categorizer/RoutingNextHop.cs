using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200020E RID: 526
	internal abstract class RoutingNextHop
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001754 RID: 5972
		public abstract DeliveryType DeliveryType { get; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001755 RID: 5973
		public abstract Guid NextHopGuid { get; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001756 RID: 5974 RVA: 0x0005EDF1 File Offset: 0x0005CFF1
		public virtual SmtpSendConnectorConfig NextHopConnector
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001757 RID: 5975 RVA: 0x0005EDF4 File Offset: 0x0005CFF4
		public virtual bool IsMandatoryTopologyHop
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001758 RID: 5976
		public abstract string GetNextHopDomain(RoutingContext context);

		// Token: 0x06001759 RID: 5977 RVA: 0x0005EDF7 File Offset: 0x0005CFF7
		public virtual IEnumerable<INextHopServer> GetLoadBalancedNextHopServers(string nextHopDomain)
		{
			throw new NotSupportedException("GetLoadBalancedNextHopServers is not supported in " + base.GetType());
		}

		// Token: 0x0600175A RID: 5978
		public abstract bool Match(RoutingNextHop other);

		// Token: 0x0600175B RID: 5979 RVA: 0x0005EE0E File Offset: 0x0005D00E
		public virtual void UpdateRecipient(MailRecipient recipient, RoutingContext context)
		{
			recipient.NextHop = this.GetNextHopSolutionKey(context);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0005EE20 File Offset: 0x0005D020
		public virtual void TraceRoutingResult(MailRecipient recipient, RoutingContext context)
		{
			RoutingDiag.SystemProbeTracer.TracePass(context.MailItem, (long)this.GetHashCode(), "[{0}] Recipient '{1}' of type {2} and final destination '{3}' routed to next hop type '{4}', next hop domain '{5}', connector {6}", new object[]
			{
				context.Timestamp,
				recipient.Email.ToString(),
				recipient.Type,
				recipient.FinalDestination,
				recipient.NextHop.NextHopType,
				recipient.NextHop.NextHopDomain,
				recipient.NextHop.NextHopConnector
			});
			RoutingDiag.Tracer.TracePfd((long)this.GetHashCode(), "PFD CAT {0} Routed recipient {1} (msgId={2}) to next hop type '{3}', next hop domain '{4}', connector {5}", new object[]
			{
				23458,
				recipient.Email.ToString(),
				context.MailItem.RecordId,
				recipient.NextHop.NextHopType,
				recipient.NextHop.NextHopDomain,
				recipient.NextHop.NextHopConnector
			});
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0005EF69 File Offset: 0x0005D169
		protected virtual NextHopSolutionKey GetNextHopSolutionKey(RoutingContext context)
		{
			return new NextHopSolutionKey(this.DeliveryType, this.GetNextHopDomain(context), this.NextHopGuid);
		}
	}
}
