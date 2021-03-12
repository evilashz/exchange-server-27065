using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023E RID: 574
	internal class MailboxDatabaseDestination : RoutingDestination
	{
		// Token: 0x06001927 RID: 6439 RVA: 0x00065A09 File Offset: 0x00063C09
		public MailboxDatabaseDestination(ADObjectId databaseId, RouteInfo routeInfo, DateTime? databaseCreationTime) : base(routeInfo)
		{
			RoutingUtils.ThrowIfNullOrEmpty(databaseId, "databaseId");
			this.databaseId = databaseId;
			this.databaseCreationTime = databaseCreationTime;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x00065A2B File Offset: 0x00063C2B
		public override MailRecipientType DestinationType
		{
			get
			{
				return MailRecipientType.Mailbox;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x00065A2E File Offset: 0x00063C2E
		public override string StringIdentity
		{
			get
			{
				return this.databaseId.DistinguishedName;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00065A3B File Offset: 0x00063C3B
		public DateTime? DatabaseCreationTime
		{
			get
			{
				return this.databaseCreationTime;
			}
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x00065A44 File Offset: 0x00063C44
		public static RoutingDestination GetRoutingDestination(ADObjectId databaseId, RoutingContext context)
		{
			if (databaseId == null)
			{
				context.RegisterCurrentRecipientForAction(MailboxDatabaseDestination.ForkAndDeferRecipientWithNoMdbActionId, new RoutingContext.ActionOnRecipients(MailboxDatabaseDestination.ForkAndDeferRecipientWithNoMdb));
				return MailboxDatabaseDestination.NoDatabaseDestination;
			}
			MailboxDatabaseDestination result;
			if (!context.RoutingTables.DatabaseMap.TryGetDatabaseDestination(databaseId, out result))
			{
				return new UnroutableDestination(MailRecipientType.Mailbox, databaseId.ToString(), UnreachableNextHop.NoRouteToDatabase);
			}
			return result;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x00065A9C File Offset: 0x00063C9C
		public static bool TryGetNextHop(NextHopSolutionKey nextHopKey, RoutingTables routingTables, out RoutingNextHop nextHop)
		{
			nextHop = null;
			if (nextHopKey.NextHopType.DeliveryType == DeliveryType.SmtpRelayToDag)
			{
				DagDeliveryGroup dagDeliveryGroup;
				if (!routingTables.DatabaseMap.TryGetDagDeliveryGroup(nextHopKey.NextHopConnector, out dagDeliveryGroup))
				{
					RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] No DAG delivery group for next hop key <{1}>", routingTables.WhenCreated, nextHopKey);
					return false;
				}
				nextHop = dagDeliveryGroup;
				return true;
			}
			else
			{
				if (nextHopKey.NextHopType.DeliveryType != DeliveryType.SmtpRelayToMailboxDeliveryGroup)
				{
					throw new ArgumentOutOfRangeException("nextHopKey", nextHopKey.NextHopType.DeliveryType, "Unexpected nextHopKey.NextHopType.DeliveryType: " + nextHopKey.NextHopType.DeliveryType);
				}
				MailboxDeliveryGroup mailboxDeliveryGroup;
				if (!routingTables.DatabaseMap.TryGetMailboxDeliveryGroup(new MailboxDeliveryGroupId(nextHopKey.NextHopDomain), out mailboxDeliveryGroup))
				{
					RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] No mailbox delivery group for next hop key <{1}>", routingTables.WhenCreated, nextHopKey);
					return false;
				}
				nextHop = mailboxDeliveryGroup;
				return true;
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x00065B7F File Offset: 0x00063D7F
		public bool Match(MailboxDatabaseDestination other)
		{
			return base.RouteInfo.Match(other.RouteInfo, NextHopMatch.GuidOnly);
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00065B93 File Offset: 0x00063D93
		private static void ForkAndDeferRecipientWithNoMdb(Guid actionId, ICollection<MailRecipient> recipients, RoutingContext context)
		{
			context.Core.Dependencies.BifurcateRecipientsAndDefer(context.MailItem, recipients, context.TaskContext, SmtpResponse.RecipientDeferredNoMdb, context.Core.Settings.DeferralTimeForNoMdb, DeferReason.RecipientHasNoMdb);
		}

		// Token: 0x04000C0A RID: 3082
		private static readonly UnroutableDestination NoDatabaseDestination = new UnroutableDestination(MailRecipientType.Mailbox, "<No Home Database>", UnreachableNextHop.NoDatabase);

		// Token: 0x04000C0B RID: 3083
		private static readonly Guid ForkAndDeferRecipientWithNoMdbActionId = Guid.NewGuid();

		// Token: 0x04000C0C RID: 3084
		private ADObjectId databaseId;

		// Token: 0x04000C0D RID: 3085
		private DateTime? databaseCreationTime;
	}
}
