using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000211 RID: 529
	internal class ConnectorDeliveryHop : RoutingNextHop
	{
		// Token: 0x06001775 RID: 6005 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		public ConnectorDeliveryHop(SendConnector connector, RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(connector, "connector");
			this.connector = connector;
			SmtpSendConnectorConfig smtpSendConnectorConfig = connector as SmtpSendConnectorConfig;
			if (smtpSendConnectorConfig != null)
			{
				if (smtpSendConnectorConfig.DNSRoutingEnabled)
				{
					this.deliveryType = DeliveryType.DnsConnectorDelivery;
					return;
				}
				this.deliveryType = DeliveryType.SmartHostConnectorDelivery;
				this.nextHopDomain = smtpSendConnectorConfig.SmartHostsString;
				this.SetSmartHosts(smtpSendConnectorConfig.SmartHosts, contextCore);
				return;
			}
			else
			{
				if (connector is DeliveryAgentConnector)
				{
					this.deliveryType = DeliveryType.DeliveryAgent;
					return;
				}
				if (connector is ForeignConnector)
				{
					this.deliveryType = DeliveryType.NonSmtpGatewayDelivery;
					this.nextHopDomain = connector.Name;
					return;
				}
				if (connector is RoutingGroupConnector)
				{
					this.deliveryType = DeliveryType.SmtpRelayToTiRg;
					this.nextHopDomain = connector.Name;
					return;
				}
				throw new InvalidOperationException("Unexpected type of local connector: " + connector.GetType());
			}
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		public ConnectorDeliveryHop(SmtpSendConnectorConfig connector, IList<SmartHost> expandedSmartHosts, string expandedSmartHostsString, RoutingContextCore contextCore)
		{
			RoutingUtils.ThrowIfNull(connector, "connector");
			RoutingUtils.ThrowIfNullOrEmpty(expandedSmartHostsString, "expandedSmartHostsString");
			RoutingUtils.ThrowIfNullOrEmpty<SmartHost>(expandedSmartHosts, "expandedSmartHosts");
			if (connector.DNSRoutingEnabled)
			{
				throw new ArgumentOutOfRangeException("connector", "Connector must be a smart host connector");
			}
			this.connector = connector;
			this.deliveryType = DeliveryType.SmartHostConnectorDelivery;
			this.nextHopDomain = expandedSmartHostsString;
			this.SetSmartHosts(expandedSmartHosts, contextCore);
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001777 RID: 6007 RVA: 0x0005F20E File Offset: 0x0005D40E
		public SendConnector Connector
		{
			get
			{
				return this.connector;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001778 RID: 6008 RVA: 0x0005F216 File Offset: 0x0005D416
		public override DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x0005F21E File Offset: 0x0005D41E
		public override Guid NextHopGuid
		{
			get
			{
				return this.connector.Guid;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0005F22B File Offset: 0x0005D42B
		public override SmtpSendConnectorConfig NextHopConnector
		{
			get
			{
				return this.connector as SmtpSendConnectorConfig;
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0005F23C File Offset: 0x0005D43C
		public static IList<INextHopServer> GetNextHopServersForDomain(string nextHopDomain)
		{
			List<INextHopServer> routingHostsFromString = RoutingHost.GetRoutingHostsFromString<INextHopServer>(nextHopDomain, (RoutingHost routingHost) => routingHost);
			if (routingHostsFromString.Count == 0)
			{
				routingHostsFromString.Add(new ConnectorDeliveryHop.NextHopFqdn(nextHopDomain));
			}
			RoutingUtils.ShuffleList<INextHopServer>(routingHostsFromString);
			return routingHostsFromString;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0005F288 File Offset: 0x0005D488
		public override string GetNextHopDomain(RoutingContext context)
		{
			if (this.deliveryType != DeliveryType.DnsConnectorDelivery && this.deliveryType != DeliveryType.DeliveryAgent)
			{
				return this.nextHopDomain;
			}
			string text;
			string text2;
			string result;
			if (!ConnectorRoutingDestination.TryGetRecipientAddress(context.CurrentRecipient, context, out text, out text2, out result))
			{
				throw new InvalidOperationException("Invalid recipient address in GetNextHopDomainForRecipient(): " + context.CurrentRecipient.Email);
			}
			return result;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0005F2E4 File Offset: 0x0005D4E4
		public override IEnumerable<INextHopServer> GetLoadBalancedNextHopServers(string nextHopDomain)
		{
			if (this.deliveryType == DeliveryType.SmartHostConnectorDelivery || this.deliveryType == DeliveryType.SmtpRelayToTiRg)
			{
				if (this.hosts == null || this.hosts.Count == 0)
				{
					throw new InvalidOperationException("No hosts set for delivery type " + this.deliveryType);
				}
				return this.hosts.LoadBalancedCollection;
			}
			else
			{
				if (this.deliveryType == DeliveryType.DnsConnectorDelivery)
				{
					return ConnectorDeliveryHop.GetNextHopServersForDomain(nextHopDomain);
				}
				throw new NotSupportedException("GetLoadBalancedNextHopServers() is not supported for delivery type " + this.deliveryType);
			}
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0005F374 File Offset: 0x0005D574
		public override bool Match(RoutingNextHop other)
		{
			ConnectorDeliveryHop connectorDeliveryHop = other as ConnectorDeliveryHop;
			if (connectorDeliveryHop == null)
			{
				return false;
			}
			if (this.NextHopGuid != connectorDeliveryHop.NextHopGuid || this.deliveryType != connectorDeliveryHop.deliveryType || !RoutingUtils.MatchStrings(this.nextHopDomain, connectorDeliveryHop.nextHopDomain))
			{
				return false;
			}
			return RoutingUtils.MatchLists<INextHopServer>(this.hosts, connectorDeliveryHop.hosts, (INextHopServer host1, INextHopServer host2) => RoutingUtils.MatchNextHopServers(host1, host2));
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0005F3F1 File Offset: 0x0005D5F1
		public void SetTargetBridgeheads(ListLoadBalancer<INextHopServer> targetBridgeheads)
		{
			RoutingUtils.ThrowIfNullOrEmpty<INextHopServer>(targetBridgeheads, "targetBridgeheads");
			if (this.deliveryType != DeliveryType.SmtpRelayToTiRg)
			{
				throw new InvalidOperationException("Cannot set target bridgeheads for delivery type " + this.deliveryType);
			}
			this.hosts = targetBridgeheads;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0005F429 File Offset: 0x0005D629
		protected override NextHopSolutionKey GetNextHopSolutionKey(RoutingContext context)
		{
			return new NextHopSolutionKey(this.DeliveryType, this.GetNextHopDomain(context), this.NextHopGuid, context.CurrentRecipient.TlsAuthLevel, context.CurrentRecipient.GetTlsDomain(), context.CurrentRecipient.OverrideSource);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0005F464 File Offset: 0x0005D664
		private void SetSmartHosts(IList<SmartHost> smartHosts, RoutingContextCore contextCore)
		{
			this.hosts = new ListLoadBalancer<INextHopServer>(contextCore.Settings.RandomLoadBalancingOffsetEnabled);
			foreach (SmartHost smartHost in smartHosts)
			{
				this.hosts.AddItem(smartHost.InnerRoutingHost);
			}
		}

		// Token: 0x04000B7F RID: 2943
		private readonly SendConnector connector;

		// Token: 0x04000B80 RID: 2944
		private readonly DeliveryType deliveryType;

		// Token: 0x04000B81 RID: 2945
		private readonly string nextHopDomain;

		// Token: 0x04000B82 RID: 2946
		private ListLoadBalancer<INextHopServer> hosts;

		// Token: 0x02000212 RID: 530
		private class NextHopFqdn : INextHopServer
		{
			// Token: 0x06001784 RID: 6020 RVA: 0x0005F4CC File Offset: 0x0005D6CC
			public NextHopFqdn(string fqdn)
			{
				this.fqdn = fqdn;
			}

			// Token: 0x17000644 RID: 1604
			// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005F4DB File Offset: 0x0005D6DB
			bool INextHopServer.IsIPAddress
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000645 RID: 1605
			// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005F4DE File Offset: 0x0005D6DE
			IPAddress INextHopServer.Address
			{
				get
				{
					throw new InvalidOperationException("INextHopServer.Address must not be requested from NextHopFqdn objects");
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x06001787 RID: 6023 RVA: 0x0005F4EA File Offset: 0x0005D6EA
			string INextHopServer.Fqdn
			{
				get
				{
					return this.fqdn;
				}
			}

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x06001788 RID: 6024 RVA: 0x0005F4F2 File Offset: 0x0005D6F2
			bool INextHopServer.IsFrontendAndHubColocatedServer
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000B85 RID: 2949
			private readonly string fqdn;
		}
	}
}
