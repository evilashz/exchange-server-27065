using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.MessageTracking;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000218 RID: 536
	internal abstract class RoutingDestination
	{
		// Token: 0x060017AC RID: 6060 RVA: 0x0006016D File Offset: 0x0005E36D
		protected RoutingDestination(RouteInfo routeInfo)
		{
			RoutingUtils.ThrowIfNull(routeInfo, "routeInfo");
			this.routeInfo = routeInfo;
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060017AD RID: 6061
		public abstract MailRecipientType DestinationType { get; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060017AE RID: 6062
		public abstract string StringIdentity { get; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00060187 File Offset: 0x0005E387
		public RouteInfo RouteInfo
		{
			get
			{
				return this.routeInfo;
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00060190 File Offset: 0x0005E390
		public virtual RoutingNextHop GetNextHop(RoutingContext context)
		{
			if (context.CurrentRecipient.RoutingOverride == null && RoutingDestination.ShouldMakeMailItemShadowed(context))
			{
				context.RegisterCurrentRecipientForAction(RoutingDestination.TrackRedirectForShadowRedundancyActionId, new RoutingContext.ActionOnRecipients(RoutingDestination.TrackRedirectForShadowRedundancy));
				return context.RoutingTables.DatabaseMap.LocalDeliveryGroup;
			}
			RoutingNextHop result;
			if (this.ViolatesRouteRestrictions(context, out result))
			{
				return result;
			}
			return this.routeInfo.NextHop;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x000601F4 File Offset: 0x0005E3F4
		private static bool ShouldMakeMailItemShadowed(RoutingContext context)
		{
			bool? shouldMakeMailItemShadowed = context.ShouldMakeMailItemShadowed;
			if (shouldMakeMailItemShadowed != null)
			{
				return shouldMakeMailItemShadowed.Value;
			}
			if (RoutingDestination.RoutedForShadowRedundancyOnOtherHub(context))
			{
				shouldMakeMailItemShadowed = new bool?(false);
			}
			else
			{
				shouldMakeMailItemShadowed = new bool?(context.Core.MailboxDeliveryQueuesSupported && context.MailItem.RouteForHighAvailability && context.Core.Dependencies.ShadowRedundancyPromotionEnabled && !context.MailItem.IsShadowedByXShadow() && context.Core.Dependencies.ShouldShadowMailItem(context.MailItem) && context.RoutingTables.DatabaseMap.LocalDeliveryGroup != null);
			}
			context.ShouldMakeMailItemShadowed = shouldMakeMailItemShadowed;
			if (shouldMakeMailItemShadowed.Value)
			{
				RoutingDestination.StampRoutedForShadowRedundancyHeader(context);
			}
			return shouldMakeMailItemShadowed.Value;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000602BC File Offset: 0x0005E4BC
		private static bool RoutedForShadowRedundancyOnOtherHub(RoutingContext context)
		{
			Header header = context.MailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Forest-RoutedForHighAvailability");
			return header != null && !string.IsNullOrEmpty(header.Value) && !header.Value.Equals(context.Core.Dependencies.LocalComputerFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00060318 File Offset: 0x0005E518
		private static void StampRoutedForShadowRedundancyHeader(RoutingContext context)
		{
			Header header = context.MailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Forest-RoutedForHighAvailability");
			if (header != null)
			{
				if (string.IsNullOrEmpty(header.Value))
				{
					context.MailItem.RootPart.Headers.RemoveAll("X-MS-Exchange-Forest-RoutedForHighAvailability");
				}
				else if (!header.Value.Equals(context.Core.Dependencies.LocalComputerFqdn, StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException("Header already stamped on other server: " + header.Value);
				}
			}
			Header header2 = Header.Create("X-MS-Exchange-Forest-RoutedForHighAvailability");
			header2.Value = context.Core.Dependencies.LocalComputerFqdn;
			context.MailItem.RootPart.Headers.AppendChild(header2);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000603D8 File Offset: 0x0005E5D8
		private static void TrackRedirectForShadowRedundancy(Guid actionId, ICollection<MailRecipient> recipients, RoutingContext context)
		{
			MessageTrackingLog.TrackHighAvailabilityRedirect(MessageTrackingSource.ROUTING, context.MailItem, recipients, "NoShadowServer");
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x000603EC File Offset: 0x0005E5EC
		private bool ViolatesRouteRestrictions(RoutingContext context, out RoutingNextHop restrictionViolationHop)
		{
			restrictionViolationHop = null;
			if (context.MessageSize > this.routeInfo.MaxMessageSize)
			{
				RoutingDiag.Tracer.TraceError((long)this.GetHashCode(), "[{0}] Recipient '{1}' failed to be routed because the message size {2} is over the limit of {3} for route {4}", new object[]
				{
					context.Timestamp,
					context.CurrentRecipient.Email.ToString(),
					context.MessageSize,
					this.routeInfo.MaxMessageSize,
					this.routeInfo.DestinationName
				});
				restrictionViolationHop = NdrNextHop.MessageTooLargeForRoute;
				return true;
			}
			return false;
		}

		// Token: 0x04000B94 RID: 2964
		private static readonly Guid TrackRedirectForShadowRedundancyActionId = Guid.NewGuid();

		// Token: 0x04000B95 RID: 2965
		private RouteInfo routeInfo;
	}
}
