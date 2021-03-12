using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000242 RID: 578
	internal class MailRouter : IMailRouter
	{
		// Token: 0x0600193D RID: 6461 RVA: 0x00065D62 File Offset: 0x00063F62
		public MailRouter()
		{
			this.routingTablesLoader = new RoutingTablesLoader(this);
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600193E RID: 6462 RVA: 0x00065D76 File Offset: 0x00063F76
		// (remove) Token: 0x0600193F RID: 6463 RVA: 0x00065D84 File Offset: 0x00063F84
		public event RoutingTablesChangedHandler RoutingTablesChanged
		{
			add
			{
				this.routingTablesLoader.RoutingTablesChanged += value;
			}
			remove
			{
				this.routingTablesLoader.RoutingTablesChanged -= value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00065D92 File Offset: 0x00063F92
		public IList<RoutingAddress> ExternalPostmasterAddresses
		{
			get
			{
				if (!this.contextCore.ServerRoutingSupported)
				{
					throw new NotSupportedException("ExternalPostmasterAddresses is not supported");
				}
				return this.RoutingTables.ServerMap.ExternalPostmasterAddresses;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00065DBC File Offset: 0x00063FBC
		private RoutingTables RoutingTables
		{
			get
			{
				return this.routingTablesLoader.RoutingTables;
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x00065DC9 File Offset: 0x00063FC9
		public void Load(RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(contextCore, "contextCore");
			this.contextCore = contextCore;
			this.routingTablesLoader.LoadAndSubscribe(this.contextCore);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00065DEF File Offset: 0x00063FEF
		public void Unload()
		{
			if (this.contextCore != null)
			{
				this.routingTablesLoader.Unsubscribe();
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00065E04 File Offset: 0x00064004
		public void RouteToMultipleDestinations(TransportMailItem mailItem, TaskContext taskContext)
		{
			if (!this.contextCore.MessageQueuesSupported)
			{
				throw new NotSupportedException("RouteToMultipleDestinations() is not supported in processes that do not queue messages");
			}
			RoutingContext routingContext = new RoutingContext(mailItem, this.RoutingTables, this.contextCore, taskContext);
			mailItem.RoutingTimeStamp = routingContext.RoutingTables.WhenCreated;
			foreach (MailRecipient mailRecipient in mailItem.Recipients.AllUnprocessed)
			{
				MailRouter.ClearRoutingProperties(mailRecipient);
				routingContext.CurrentRecipient = mailRecipient;
				RoutingDestination routingDestinationForPreResolvedRecipient = MailRouter.GetRoutingDestinationForPreResolvedRecipient(mailRecipient, routingContext);
				MailRouter.SetFinalDestination(mailRecipient, routingDestinationForPreResolvedRecipient);
				RoutingNextHop nextHop = routingDestinationForPreResolvedRecipient.GetNextHop(routingContext);
				nextHop.UpdateRecipient(mailRecipient, routingContext);
				nextHop.TraceRoutingResult(mailRecipient, routingContext);
			}
			routingContext.ExecuteActions();
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00065ECC File Offset: 0x000640CC
		public bool TryGetServersForNextHop(NextHopSolutionKey nextHopKey, out IEnumerable<INextHopServer> servers, out SmtpSendConnectorConfig connector)
		{
			servers = null;
			connector = null;
			if (!this.contextCore.MessageQueuesSupported)
			{
				throw new NotSupportedException("TryGetServersForNextHop() is not supported in processes that do not queue messages");
			}
			RoutingNextHop routingNextHop = null;
			switch (nextHopKey.NextHopType.DeliveryType)
			{
			case DeliveryType.DnsConnectorDelivery:
			case DeliveryType.SmartHostConnectorDelivery:
				ConnectorRoutingDestination.TryGetNextHop(nextHopKey, this.RoutingTables, out routingNextHop);
				goto IL_11D;
			case DeliveryType.SmtpRelayToRemoteAdSite:
				this.RoutingTables.ServerMap.SiteRelayMap.TryGetNextHop(nextHopKey, out routingNextHop);
				goto IL_11D;
			case DeliveryType.SmtpRelayToTiRg:
			case DeliveryType.SmtpRelayWithinAdSiteToEdge:
			case DeliveryType.SmtpRelayToConnectorSourceServers:
				ConnectorRoutingDestination.TryGetNextHop(nextHopKey, this.RoutingTables, out routingNextHop);
				goto IL_11D;
			case DeliveryType.Heartbeat:
				return this.TryGetServersForHeartbeat(nextHopKey, out servers, out connector);
			case DeliveryType.SmtpDeliveryToMailbox:
				return this.TryGetServersForSmtpDeliveryToMailbox(nextHopKey, out servers);
			case DeliveryType.SmtpRelayToDag:
			case DeliveryType.SmtpRelayToMailboxDeliveryGroup:
				MailboxDatabaseDestination.TryGetNextHop(nextHopKey, this.RoutingTables, out routingNextHop);
				goto IL_11D;
			case DeliveryType.SmtpRelayToServers:
				RedirectGroup.TryGetNextHop(nextHopKey, this.RoutingTables, this.contextCore, out routingNextHop);
				goto IL_11D;
			}
			throw new InvalidOperationException("Unexpected Delivery Type in TryGetServersForNextHop: " + nextHopKey.NextHopType.DeliveryType);
			IL_11D:
			if (routingNextHop == null)
			{
				return false;
			}
			servers = routingNextHop.GetLoadBalancedNextHopServers(nextHopKey.NextHopDomain);
			connector = routingNextHop.NextHopConnector;
			return true;
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00066014 File Offset: 0x00064214
		public bool TryGetOutboundFrontendServers(out IEnumerable<INextHopServer> servers, out bool externalOutboundFrontendProxyEnabled)
		{
			servers = null;
			externalOutboundFrontendProxyEnabled = false;
			if (!this.contextCore.ConnectorRoutingSupported)
			{
				throw new NotSupportedException("TryGetOutboundFrontendServers() is not supported in processes that do not do connector routing");
			}
			if (!RoutingUtils.IsNullOrEmpty<RoutingHost>(this.contextCore.Settings.OutboundFrontendServers))
			{
				servers = this.contextCore.Settings.OutboundFrontendServers;
				externalOutboundFrontendProxyEnabled = this.contextCore.Settings.ExternalOutboundFrontendProxyEnabled;
				return true;
			}
			IEnumerable<RoutingServerInfo> enumerable;
			if (this.RoutingTables.ServerMap.TryGetLoadBalancedFrontendServersInLocalSite(out enumerable))
			{
				servers = enumerable;
				return true;
			}
			return false;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00066098 File Offset: 0x00064298
		public void ApplyDelayedFanout(TransportMailItem mailItem)
		{
			if (!this.contextCore.MessageQueuesSupported)
			{
				throw new NotSupportedException("ApplyDelayedFanout() is not supported in processes that do not queue messages");
			}
			FanOutPlanner fanOutPlanner = null;
			MailRecipient mailRecipient = null;
			foreach (MailRecipient mailRecipient2 in mailItem.Recipients.AllUnprocessed)
			{
				DeliveryType deliveryType = mailRecipient2.NextHop.NextHopType.DeliveryType;
				if (deliveryType == DeliveryType.SmtpRelayToRemoteAdSite)
				{
					if (fanOutPlanner == null)
					{
						if (mailRecipient == null)
						{
							mailRecipient = mailRecipient2;
							goto IL_79;
						}
						fanOutPlanner = new FanOutPlanner(this.RoutingTables);
						fanOutPlanner.AddRecipient(mailRecipient);
					}
					fanOutPlanner.AddRecipient(mailRecipient2);
				}
				IL_79:
				if (fanOutPlanner != null)
				{
					fanOutPlanner.UpdateRecipientNextHops();
				}
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0006614C File Offset: 0x0006434C
		public bool TrySelectHubServersUsingDagSelector(Guid externalOrganizationId, out IEnumerable<INextHopServer> hubServers)
		{
			hubServers = null;
			if (!this.contextCore.ProxyRoutingSupported)
			{
				throw new NotSupportedException("TrySelectHubServersUsingDagSelector is not supported");
			}
			ProxyRoutingContext context = new ProxyRoutingContext(this.RoutingTables, this.contextCore);
			ProxyTargetHubCollection proxyTargetHubCollection;
			if (ProxyTargetHubCollection.TryCreateInstance(externalOrganizationId, context, out proxyTargetHubCollection))
			{
				hubServers = proxyTargetHubCollection;
				return true;
			}
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoHubServersSelectedForTenant, null, new object[]
			{
				this.RoutingTables.WhenCreated,
				this.contextCore.GetProcessRoleForDiagnostics(),
				externalOrganizationId
			});
			return false;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000661E0 File Offset: 0x000643E0
		public bool TrySelectHubServersForDatabases(IList<ADObjectId> databaseIds, Guid? externalOrganizationId, out IEnumerable<INextHopServer> hubServers)
		{
			hubServers = null;
			if (!this.contextCore.ProxyRoutingSupported)
			{
				throw new NotSupportedException("TrySelectHubServersForDatabases is not supported");
			}
			ProxyRoutingContext context = new ProxyRoutingContext(this.RoutingTables, this.contextCore);
			ProxyTargetHubCollection proxyTargetHubCollection;
			if (ProxyTargetHubCollection.TryCreateInstance(databaseIds, externalOrganizationId, context, out proxyTargetHubCollection))
			{
				hubServers = proxyTargetHubCollection;
				return true;
			}
			string text = string.Empty;
			if (!RoutingUtils.IsNullOrEmpty<ADObjectId>(databaseIds))
			{
				text = string.Join<ADObjectId>(", ", databaseIds);
			}
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingNoHubServersSelectedForDatabases, null, new object[]
			{
				this.RoutingTables.WhenCreated,
				this.contextCore.GetProcessRoleForDiagnostics(),
				text
			});
			return false;
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0006628C File Offset: 0x0006448C
		public bool TrySelectHubServersForShadow(ShadowRoutingConfiguration shadowRoutingConfig, out IEnumerable<INextHopServer> hubServers)
		{
			hubServers = null;
			if (!this.contextCore.ShadowRoutingSupported)
			{
				throw new NotSupportedException("TrySelectHubServersForShadow is not supported");
			}
			ShadowRoutingContext context = new ShadowRoutingContext(this.RoutingTables, this.contextCore, shadowRoutingConfig);
			ProxyTargetHubCollection proxyTargetHubCollection;
			if (ProxyTargetHubCollection.TryCreateInstanceForShadowing(context, out proxyTargetHubCollection))
			{
				hubServers = proxyTargetHubCollection;
				return true;
			}
			return false;
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000662D7 File Offset: 0x000644D7
		public bool TryGetLocalSendConnector<ConnectorType>(Guid connectorGuid, out ConnectorType connector) where ConnectorType : MailGateway
		{
			if (!this.contextCore.ConnectorRoutingSupported)
			{
				throw new NotSupportedException("TryGetLocalSendConnector is not supported");
			}
			return this.RoutingTables.ConnectorMap.TryGetLocalSendConnector<ConnectorType>(connectorGuid, out connector);
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00066303 File Offset: 0x00064503
		public IList<ConnectorType> GetLocalSendConnectors<ConnectorType>() where ConnectorType : MailGateway
		{
			if (!this.contextCore.ConnectorRoutingSupported)
			{
				throw new NotSupportedException("GetLocalSendConnectors is not supported");
			}
			return this.RoutingTables.ConnectorMap.GetLocalSendConnectors<ConnectorType>();
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00066330 File Offset: 0x00064530
		public bool IsHubTransportServer(string serverFqdn)
		{
			if (!this.contextCore.ServerRoutingSupported)
			{
				throw new NotSupportedException("IsHubServer is not supported");
			}
			RoutingServerInfo routingServerInfo;
			return this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(serverFqdn, out routingServerInfo) && routingServerInfo.IsHubTransportServer;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00066374 File Offset: 0x00064574
		public bool IsInLocalSite(string serverFqdn)
		{
			RoutingServerInfo serverInfo;
			return this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(serverFqdn, out serverInfo) && this.RoutingTables.ServerMap.IsInLocalSite(serverInfo);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000663AC File Offset: 0x000645AC
		public bool TryGetServerFqdnByLegacyDN(string serverLegacyDN, out string serverFqdn)
		{
			serverFqdn = null;
			if (!this.contextCore.ServerRoutingSupported)
			{
				throw new NotSupportedException("TryGetServerFqdnByLegacyDN is not supported");
			}
			RoutingServerInfo routingServerInfo;
			if (this.RoutingTables.ServerMap.TryGetServerInfoByLegacyDN(serverLegacyDN, out routingServerInfo))
			{
				serverFqdn = routingServerInfo.Fqdn;
				return true;
			}
			return false;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000663F4 File Offset: 0x000645F4
		public bool TryGetServerLegacyDNByFqdn(string serverFqdn, out string serverLegacyDN)
		{
			serverLegacyDN = null;
			if (!this.contextCore.ServerRoutingSupported)
			{
				throw new NotSupportedException("TryGetServerLegacyDNByFqdn is not supported");
			}
			RoutingServerInfo routingServerInfo;
			if (this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(serverFqdn, out routingServerInfo))
			{
				serverLegacyDN = routingServerInfo.ExchangeLegacyDN;
				return true;
			}
			return false;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x0006643C File Offset: 0x0006463C
		public bool TryGetRelatedServersForShadowQueue(NextHopSolutionKey shadowQueueKey, out IEnumerable<INextHopServer> servers)
		{
			servers = null;
			if (!this.contextCore.MessageQueuesSupported)
			{
				throw new NotSupportedException("TryGetRelatedServersForShadowQueue() is not supported in processes that do not queue messages");
			}
			RoutingNextHop routingNextHop;
			if (shadowQueueKey.NextHopConnector != Guid.Empty && this.RoutingTables.ConnectorMap.TryGetConnectorNextHop(shadowQueueKey.NextHopConnector, out routingNextHop))
			{
				DeliveryType deliveryType = routingNextHop.DeliveryType;
				if (deliveryType == DeliveryType.DnsConnectorDelivery)
				{
					return false;
				}
				if (deliveryType == DeliveryType.SmartHostConnectorDelivery || deliveryType == DeliveryType.SmtpRelayWithinAdSiteToEdge)
				{
					servers = routingNextHop.GetLoadBalancedNextHopServers(shadowQueueKey.NextHopDomain);
					return true;
				}
			}
			RoutingServerInfo serverInfo;
			RouteInfo routeInfo;
			if (this.contextCore.ServerRoutingSupported && this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(shadowQueueKey.NextHopDomain, out serverInfo) && this.RoutingTables.ServerMap.TryGetServerRoute(serverInfo, out routeInfo) && routeInfo.DestinationProximity == Proximity.RemoteADSite)
			{
				servers = routeInfo.NextHop.GetLoadBalancedNextHopServers(shadowQueueKey.NextHopDomain);
				return true;
			}
			return false;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00066515 File Offset: 0x00064715
		public bool IsJournalMessage(IReadOnlyMailItem mailItem)
		{
			return MessagingPoliciesUtils.CheckJournalReportVersion(mailItem.RootPart.Headers) != MessagingPoliciesUtils.JournalVersion.None;
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006667C File Offset: 0x0006487C
		public IEnumerable<XElement> GetDiagnosticInfo(bool verbose, DiagnosableParameters parameters)
		{
			XElement diagnosticInfo;
			if (this.RoutingTables.TryGetDiagnosticInfo(verbose, parameters, out diagnosticInfo))
			{
				yield return diagnosticInfo;
			}
			if (this.routingTablesLoader.TryGetDiagnosticInfo(verbose, parameters, out diagnosticInfo))
			{
				yield return diagnosticInfo;
			}
			yield break;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000666A8 File Offset: 0x000648A8
		private static void SetFinalDestination(MailRecipient recipient, RoutingDestination destination)
		{
			recipient.Type = destination.DestinationType;
			if (recipient.RoutingOverride == null)
			{
				recipient.FinalDestination = destination.StringIdentity;
				return;
			}
			recipient.FinalDestination = string.Format(CultureInfo.InvariantCulture, "{0}; RoutingOverride={1}", new object[]
			{
				destination.StringIdentity,
				recipient.RoutingOverride
			});
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00066705 File Offset: 0x00064905
		private static void ClearRoutingProperties(MailRecipient recipient)
		{
			recipient.NextHop = NextHopSolutionKey.Empty;
			recipient.UnreachableReason = UnreachableReason.None;
			recipient.Type = MailRecipientType.Unknown;
			recipient.FinalDestination = string.Empty;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0006672C File Offset: 0x0006492C
		private static RoutingDestination GetRoutingDestinationForPreResolvedRecipient(MailRecipient recipient, RoutingContext context)
		{
			List<string> moveToHosts = context.MailItem.MoveToHosts;
			if (moveToHosts != null && moveToHosts.Count > 0)
			{
				return RedirectDestination.GetRoutingDestination(moveToHosts, context);
			}
			if (recipient.RoutingOverride != null)
			{
				return ConnectorRoutingDestination.GetRoutingDestination(recipient, context);
			}
			RecipientItem recipientItem = RecipientItem.Create(recipient);
			DeliverableItem deliverableItem = recipientItem as DeliverableItem;
			if (deliverableItem != null)
			{
				return MailboxDatabaseDestination.GetRoutingDestination(deliverableItem.Database, context);
			}
			ReroutableItem reroutableItem = recipientItem as ReroutableItem;
			if (reroutableItem != null)
			{
				return ExpansionServerDestination.GetRoutingDestination(reroutableItem.HomeMtaServerId, context);
			}
			return ConnectorRoutingDestination.GetRoutingDestination(recipient, context);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000667A8 File Offset: 0x000649A8
		private bool TryGetServersForHeartbeat(NextHopSolutionKey nextHopKey, out IEnumerable<INextHopServer> servers, out SmtpSendConnectorConfig connector)
		{
			servers = null;
			connector = null;
			RoutingNextHop routingNextHop;
			if (nextHopKey.NextHopConnector != Guid.Empty && this.RoutingTables.ConnectorMap.TryGetConnectorNextHop(nextHopKey.NextHopConnector, out routingNextHop))
			{
				connector = routingNextHop.NextHopConnector;
				if (connector != null)
				{
					servers = ConnectorDeliveryHop.GetNextHopServersForDomain(nextHopKey.NextHopDomain);
					return true;
				}
			}
			RoutingServerInfo routingServerInfo;
			if (this.contextCore.ServerRoutingSupported && this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(nextHopKey.NextHopDomain, out routingServerInfo))
			{
				servers = new INextHopServer[]
				{
					routingServerInfo
				};
				return true;
			}
			RoutingDiag.Tracer.TraceError<NextHopSolutionKey>((long)this.GetHashCode(), "Cannot find connector or server route for Heartbeat NHSK <{1}>", nextHopKey);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_HeartbeatDestinationConfigChanged, null, new object[]
			{
				nextHopKey.NextHopConnector
			});
			return false;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00066880 File Offset: 0x00064A80
		private bool TryGetServersForSmtpDeliveryToMailbox(NextHopSolutionKey nextHopKey, out IEnumerable<INextHopServer> servers)
		{
			servers = null;
			if (!this.contextCore.MailboxDeliveryQueuesSupported)
			{
				throw new NotSupportedException("TryGetServersForSmtpDeliveryToMailbox() is not supported");
			}
			string text = null;
			if (!this.contextCore.Dependencies.TryGetServerForDatabase(nextHopKey.NextHopConnector, out text))
			{
				RoutingDiag.Tracer.TraceError<NextHopSolutionKey>((long)this.GetHashCode(), "TryGetServerForDatabase() failed for NHSK <{0}>", nextHopKey);
				return false;
			}
			RoutingServerInfo routingServerInfo;
			if (!this.RoutingTables.ServerMap.TryGetServerInfoByFqdn(text, out routingServerInfo))
			{
				RoutingDiag.Tracer.TraceError<string>((long)this.GetHashCode(), "Could not find server info corresponding to Active-Manager-returned FQDN {0}", text);
				return false;
			}
			servers = new INextHopServer[]
			{
				routingServerInfo
			};
			return true;
		}

		// Token: 0x04000C12 RID: 3090
		private RoutingContextCore contextCore;

		// Token: 0x04000C13 RID: 3091
		private RoutingTablesLoader routingTablesLoader;
	}
}
