using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200027E RID: 638
	internal class UnreachableNextHop : RoutingNextHop
	{
		// Token: 0x06001B89 RID: 7049 RVA: 0x00070D37 File Offset: 0x0006EF37
		private UnreachableNextHop(UnreachableReason reason)
		{
			this.reason = reason;
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00070D46 File Offset: 0x0006EF46
		public override DeliveryType DeliveryType
		{
			get
			{
				return DeliveryType.Unreachable;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x00070D4A File Offset: 0x0006EF4A
		public override Guid NextHopGuid
		{
			get
			{
				return Guid.Empty;
			}
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x00070D54 File Offset: 0x0006EF54
		public override string GetNextHopDomain(RoutingContext context)
		{
			return NextHopSolutionKey.Unreachable.NextHopDomain;
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00070D6E File Offset: 0x0006EF6E
		public override bool Match(RoutingNextHop other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00070D77 File Offset: 0x0006EF77
		public override void UpdateRecipient(MailRecipient recipient, RoutingContext context)
		{
			base.UpdateRecipient(recipient, context);
			recipient.UnreachableReason = this.reason;
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00070D90 File Offset: 0x0006EF90
		public override void TraceRoutingResult(MailRecipient recipient, RoutingContext context)
		{
			RoutingDiag.SystemProbeTracer.TraceFail(context.MailItem, (long)this.GetHashCode(), "[{0}] Failed to route recipient '{1}' of type {2} and final destination '{3}'; Unreachable reason = {4}", new object[]
			{
				context.Timestamp,
				recipient.Email.ToString(),
				recipient.Type,
				recipient.FinalDestination,
				this.reason
			});
			RoutingDiag.Tracer.TracePfd((long)this.GetHashCode(), "PFD CAT {0} Routing failed for recipient {1} (msgId={2}); recipient type {3}; final destination '{4}'; Unreachable reason {5}", new object[]
			{
				31650,
				recipient.Email.ToString(),
				context.MailItem.RecordId,
				recipient.Type,
				recipient.FinalDestination,
				this.reason
			});
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00070E85 File Offset: 0x0006F085
		protected override NextHopSolutionKey GetNextHopSolutionKey(RoutingContext context)
		{
			return NextHopSolutionKey.Unreachable;
		}

		// Token: 0x04000CFD RID: 3325
		public static readonly UnreachableNextHop IncompatibleDeliveryDomain = new UnreachableNextHop(UnreachableReason.IncompatibleDeliveryDomain);

		// Token: 0x04000CFE RID: 3326
		public static readonly UnreachableNextHop NoDatabase = new UnreachableNextHop(UnreachableReason.NoMdb);

		// Token: 0x04000CFF RID: 3327
		public static readonly UnreachableNextHop NoRouteToDatabase = new UnreachableNextHop(UnreachableReason.NoRouteToMdb);

		// Token: 0x04000D00 RID: 3328
		public static readonly UnreachableNextHop NoRouteToServer = new UnreachableNextHop(UnreachableReason.NoRouteToMta);

		// Token: 0x04000D01 RID: 3329
		public static readonly UnreachableNextHop NoMatchingConnector = new UnreachableNextHop(UnreachableReason.NoMatchingConnector);

		// Token: 0x04000D02 RID: 3330
		public static readonly UnreachableNextHop NonHubExpansionServer = new UnreachableNextHop(UnreachableReason.NonBHExpansionServer);

		// Token: 0x04000D03 RID: 3331
		private UnreachableReason reason;
	}
}
