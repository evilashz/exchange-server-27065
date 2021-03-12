using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000243 RID: 579
	internal class NdrNextHop : RoutingNextHop
	{
		// Token: 0x06001959 RID: 6489 RVA: 0x0006691B File Offset: 0x00064B1B
		private NdrNextHop(SmtpResponse ackReason)
		{
			this.ackReason = ackReason;
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0006692A File Offset: 0x00064B2A
		public override DeliveryType DeliveryType
		{
			get
			{
				return DeliveryType.Undefined;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0006692D File Offset: 0x00064B2D
		public override Guid NextHopGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00066934 File Offset: 0x00064B34
		public override string GetNextHopDomain(RoutingContext context)
		{
			return string.Empty;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x0006693B File Offset: 0x00064B3B
		public override bool Match(RoutingNextHop other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00066944 File Offset: 0x00064B44
		public override void UpdateRecipient(MailRecipient recipient, RoutingContext context)
		{
			base.UpdateRecipient(recipient, context);
			recipient.Ack(AckStatus.Fail, this.ackReason);
			context.Core.PerfCounters.IncrementRoutingNdrs();
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x0006696C File Offset: 0x00064B6C
		public override void TraceRoutingResult(MailRecipient recipient, RoutingContext context)
		{
			RoutingDiag.SystemProbeTracer.TraceFail(context.MailItem, (long)this.GetHashCode(), "[{0}] Failed to route recipient '{1}' of type {2} and final destination '{3}'; NDR response = {4}", new object[]
			{
				context.Timestamp,
				recipient.Email.ToString(),
				recipient.Type,
				recipient.FinalDestination,
				this.ackReason
			});
			RoutingDiag.Tracer.TracePfd((long)this.GetHashCode(), "PFD CAT {0} Routing failed for recipient {1} (msgId={2}); recipient type {3}; final destination '{4}'; NDR response {5}", new object[]
			{
				31650,
				recipient.Email.ToString(),
				context.MailItem.RecordId,
				recipient.Type,
				recipient.FinalDestination,
				this.ackReason
			});
		}

		// Token: 0x04000C14 RID: 3092
		public static readonly NdrNextHop InvalidAddressForRouting = new NdrNextHop(AckReason.InvalidAddressForRouting);

		// Token: 0x04000C15 RID: 3093
		public static readonly NdrNextHop InvalidX400AddressForRouting = new NdrNextHop(AckReason.InvalidX400AddressForRouting);

		// Token: 0x04000C16 RID: 3094
		public static readonly NdrNextHop MessageTooLargeForRoute = new NdrNextHop(AckReason.MessageTooLargeForRoute);

		// Token: 0x04000C17 RID: 3095
		public static readonly NdrNextHop NoConnectorForAddressType = new NdrNextHop(AckReason.NoConnectorForAddressType);

		// Token: 0x04000C18 RID: 3096
		private SmtpResponse ackReason;
	}
}
