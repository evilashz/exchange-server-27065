using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000248 RID: 584
	internal class ProxyTargetHubCollection : IEnumerable<INextHopServer>, IEnumerable
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x000673A8 File Offset: 0x000655A8
		private ProxyTargetHubCollection(DeliveryGroup primaryDeliveryGroup, DeliveryGroup fallbackDeliveryGroup, ProxyRoutingContext context)
		{
			if (primaryDeliveryGroup == null && fallbackDeliveryGroup == null)
			{
				throw new InvalidOperationException("primaryDeliveryGroup and fallbackDeliveryGroup cannot both be null");
			}
			this.primaryDeliveryGroup = primaryDeliveryGroup;
			this.fallbackDeliveryGroup = fallbackDeliveryGroup;
			this.context = context;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000673D8 File Offset: 0x000655D8
		public static bool TryCreateInstance(IList<ADObjectId> databaseIds, Guid? externalOrganizationId, ProxyRoutingContext context, out ProxyTargetHubCollection hubCollection)
		{
			RoutingUtils.ThrowIfNull(context, "context");
			DeliveryGroup deliveryGroup = null;
			if (!RoutingUtils.IsNullOrEmpty<ADObjectId>(databaseIds) && ProxyTargetHubCollection.TrySelectPrimaryDeliveryGroup(databaseIds, context, out deliveryGroup) && externalOrganizationId != null && context.Core.Settings.DagSelectorEnabled)
			{
				context.RoutingTables.DatabaseMap.UpdateDagSelectorWithMessagesToDagDeliveryGroup(externalOrganizationId.Value, deliveryGroup);
			}
			return ProxyTargetHubCollection.TryCreateInstance(context, deliveryGroup, out hubCollection);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00067440 File Offset: 0x00065640
		public static bool TryCreateInstance(Guid externalOrganizationId, ProxyRoutingContext context, out ProxyTargetHubCollection hubCollection)
		{
			RoutingUtils.ThrowIfNull(context, "context");
			DeliveryGroup deliveryGroup;
			if (context.RoutingTables.DatabaseMap.TryGetDagDeliveryGroupUsingDagSelector(externalOrganizationId, out deliveryGroup))
			{
				return ProxyTargetHubCollection.TryCreateInstance(context, deliveryGroup, out hubCollection);
			}
			hubCollection = null;
			return false;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006747C File Offset: 0x0006567C
		private static bool TryCreateInstance(ProxyRoutingContext context, DeliveryGroup primaryDeliveryGroup, out ProxyTargetHubCollection hubCollection)
		{
			hubCollection = null;
			DeliveryGroup deliveryGroup;
			ProxyTargetHubCollection.TrySelectFallbackDeliveryGroup(primaryDeliveryGroup, context, out deliveryGroup);
			if (!ProxyTargetHubCollection.EvaluateForSuitableServers(ref primaryDeliveryGroup, ref deliveryGroup, context))
			{
				return false;
			}
			hubCollection = new ProxyTargetHubCollection(primaryDeliveryGroup, deliveryGroup, context);
			return true;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000674B0 File Offset: 0x000656B0
		public static bool TryCreateInstanceForShadowing(ShadowRoutingContext context, out ProxyTargetHubCollection hubCollection)
		{
			hubCollection = null;
			RoutingUtils.ThrowIfNull(context, "context");
			DeliveryGroup localDeliveryGroup = context.RoutingTables.DatabaseMap.LocalDeliveryGroup;
			if (!ProxyTargetHubCollection.EvaluateForSuitableServers(ref localDeliveryGroup, context))
			{
				return false;
			}
			if (!context.EvaluateDeliveryGroup(localDeliveryGroup))
			{
				return false;
			}
			hubCollection = new ProxyTargetHubCollection(localDeliveryGroup, null, context);
			return true;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00067854 File Offset: 0x00065A54
		IEnumerator<INextHopServer> IEnumerable<INextHopServer>.GetEnumerator()
		{
			ProxyRoutingEnumeratorContext enumeratorContext = new ProxyRoutingEnumeratorContext(this.context);
			if (this.primaryDeliveryGroup != null)
			{
				foreach (RoutingServerInfo serverInfo in this.context.GetDeliveryGroupServers(this.primaryDeliveryGroup, enumeratorContext))
				{
					yield return serverInfo;
				}
			}
			if (this.fallbackDeliveryGroup != null && enumeratorContext.RemainingServerCount > 0)
			{
				foreach (RoutingServerInfo serverInfo2 in this.context.GetDeliveryGroupServers(this.fallbackDeliveryGroup, enumeratorContext))
				{
					yield return serverInfo2;
				}
			}
			if (enumeratorContext.AllServersUnhealthy)
			{
				RoutingDiag.Tracer.TraceError<DateTime>((long)this.GetHashCode(), "[{0}] All selected proxy targets are on the Unhealthy list; will have to use them anyway", this.context.Timestamp);
				foreach (RoutingServerInfo serverInfo3 in enumeratorContext.GetUnhealthyServers())
				{
					yield return serverInfo3;
				}
			}
			yield break;
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00067870 File Offset: 0x00065A70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<INextHopServer>)this).GetEnumerator();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00067878 File Offset: 0x00065A78
		private static bool TrySelectPrimaryDeliveryGroup(IList<ADObjectId> databaseIds, ProxyRoutingContext context, out DeliveryGroup primaryDeliveryGroup)
		{
			primaryDeliveryGroup = null;
			foreach (ADObjectId databaseId in RoutingUtils.RandomShiftEnumerate<ADObjectId>(databaseIds))
			{
				DeliveryGroup deliveryGroup;
				if (ProxyTargetHubCollection.TryGetDeliveryGroup(databaseId, context, out deliveryGroup) && ProxyTargetHubCollection.MayContainSuitableServers(deliveryGroup, context) && (primaryDeliveryGroup == null || (!primaryDeliveryGroup.IsActive && deliveryGroup.IsActive) || (primaryDeliveryGroup.IsActive == deliveryGroup.IsActive && deliveryGroup.PrimaryRoute.CompareTo(primaryDeliveryGroup.PrimaryRoute, RouteComparison.None) < 0)))
				{
					primaryDeliveryGroup = deliveryGroup;
					if (primaryDeliveryGroup.PrimaryRoute.InLocalADSite)
					{
						break;
					}
				}
			}
			if (primaryDeliveryGroup != null)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Selected Delivery Group {1} as a primary delivery group", context.Timestamp, primaryDeliveryGroup.Name);
				return true;
			}
			RoutingDiag.Tracer.TraceDebug<DateTime>(0L, "[{0}] Unable to select a primary delivery group", context.Timestamp);
			return false;
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0006795C File Offset: 0x00065B5C
		private static bool TrySelectFallbackDeliveryGroup(DeliveryGroup primaryDeliveryGroup, ProxyRoutingContext context, out DeliveryGroup fallbackDeliveryGroup)
		{
			fallbackDeliveryGroup = context.RoutingTables.DatabaseMap.LocalDeliveryGroup;
			if (fallbackDeliveryGroup == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] Failed to select a fallback delivery group", context.Timestamp);
			}
			else if (object.ReferenceEquals(fallbackDeliveryGroup, primaryDeliveryGroup))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Primary and fallback delivery groups are identical: {1}", context.Timestamp, primaryDeliveryGroup.Name);
				fallbackDeliveryGroup = null;
			}
			return fallbackDeliveryGroup != null;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000679CC File Offset: 0x00065BCC
		private static bool EvaluateForSuitableServers(ref DeliveryGroup primaryDeliveryGroup, ref DeliveryGroup fallbackDeliveryGroup, ProxyRoutingContext context)
		{
			if (primaryDeliveryGroup == null && fallbackDeliveryGroup == null)
			{
				RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] Failed to select primary and fallback delivery group", context.Timestamp);
				return false;
			}
			if (ProxyTargetHubCollection.EvaluateForSuitableServers(ref primaryDeliveryGroup, context) || ProxyTargetHubCollection.EvaluateForSuitableServers(ref fallbackDeliveryGroup, context))
			{
				return true;
			}
			RoutingDiag.Tracer.TraceError<DateTime>(0L, "[{0}] Neither primary nor fallback delivery group contains any suitable servers", context.Timestamp);
			return false;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00067A28 File Offset: 0x00065C28
		private static bool EvaluateForSuitableServers(ref DeliveryGroup deliveryGroup, ProxyRoutingContext context)
		{
			if (deliveryGroup == null)
			{
				return false;
			}
			foreach (RoutingServerInfo serverInfo in deliveryGroup.AllServersNoFallback)
			{
				if (context.VerifyRestrictions(serverInfo))
				{
					return true;
				}
			}
			RoutingDiag.Tracer.TraceDebug<string, DateTime>(0L, "[{0}] Delivery group {1} does not contain any suitable servers", deliveryGroup.Name, context.Timestamp);
			deliveryGroup = null;
			return false;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00067AA8 File Offset: 0x00065CA8
		private static bool TryGetDeliveryGroup(ADObjectId databaseId, ProxyRoutingContext context, out DeliveryGroup deliveryGroup)
		{
			deliveryGroup = null;
			MailboxDatabaseDestination mailboxDatabaseDestination;
			if (!context.RoutingTables.DatabaseMap.TryGetDatabaseDestination(databaseId, out mailboxDatabaseDestination))
			{
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoDestinationForDatabase, databaseId.ObjectGuid.ToString(), new object[]
				{
					databaseId,
					context.Timestamp
				});
				RoutingDiag.Tracer.TraceError<DateTime, ADObjectId>(0L, "[{0}] No database destination found for database id {1}.", context.Timestamp, databaseId);
				return false;
			}
			deliveryGroup = (mailboxDatabaseDestination.RouteInfo.NextHop as DeliveryGroup);
			if (deliveryGroup == null)
			{
				RoutingNextHop nextHop = mailboxDatabaseDestination.RouteInfo.NextHop;
				throw new InvalidOperationException(string.Format("Unexpected non-DeliveryGroup NextHop type: {0}", (nextHop == null) ? "<null>" : nextHop.GetType()));
			}
			return true;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00067B68 File Offset: 0x00065D68
		private static bool MayContainSuitableServers(DeliveryGroup deliveryGroup, ProxyRoutingContext context)
		{
			if (!context.XSiteRoutingEnabled && !deliveryGroup.PrimaryRoute.InLocalADSite)
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Delivery Group {1} will not be used because it is in a remote AD site", context.Timestamp, deliveryGroup.Name);
				return false;
			}
			if (!deliveryGroup.MayContainServersOfVersions(context.Core.ProxyRoutingAllowedTargetVersions) && !deliveryGroup.MayContainServersOfVersion(context.RoutingTables.ServerMap.LocalServerVersion))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>(0L, "[{0}] Delivery Group {1} will not be used because its servers are of different major version than the ones supported", context.Timestamp, deliveryGroup.Name);
				return false;
			}
			return true;
		}

		// Token: 0x04000C23 RID: 3107
		private DeliveryGroup primaryDeliveryGroup;

		// Token: 0x04000C24 RID: 3108
		private DeliveryGroup fallbackDeliveryGroup;

		// Token: 0x04000C25 RID: 3109
		private ProxyRoutingContext context;
	}
}
