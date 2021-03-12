using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000219 RID: 537
	internal class ConnectorRoutingDestination : RoutingDestination
	{
		// Token: 0x060017B7 RID: 6071 RVA: 0x0006049F File Offset: 0x0005E69F
		public ConnectorRoutingDestination(MailGateway connector, IList<AddressSpace> addressSpaces, RouteInfo routeInfo) : base(routeInfo)
		{
			RoutingUtils.ThrowIfNull(connector, "connector");
			RoutingUtils.ThrowIfNullOrEmpty<AddressSpace>(addressSpaces, "addressSpaces");
			this.connector = connector;
			this.addressSpaces = addressSpaces;
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000604CC File Offset: 0x0005E6CC
		public IList<AddressSpace> AddressSpaces
		{
			get
			{
				return this.addressSpaces;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000604D4 File Offset: 0x0005E6D4
		public override MailRecipientType DestinationType
		{
			get
			{
				return MailRecipientType.External;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x000604D7 File Offset: 0x0005E6D7
		public override string StringIdentity
		{
			get
			{
				return this.connector.DistinguishedName;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x000604E4 File Offset: 0x0005E6E4
		public long MaxMessageSize
		{
			get
			{
				return (long)this.connector.AbsoluteMaxMessageSize;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x000604F1 File Offset: 0x0005E6F1
		public string ConnectorName
		{
			get
			{
				return this.connector.Name;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x000604FE File Offset: 0x0005E6FE
		public Guid ConnectorGuid
		{
			get
			{
				return this.connector.Guid;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0006050C File Offset: 0x0005E70C
		private bool DnsRoutingEnabled
		{
			get
			{
				SmtpSendConnectorConfig smtpSendConnectorConfig = this.connector as SmtpSendConnectorConfig;
				return smtpSendConnectorConfig != null && smtpSendConnectorConfig.DNSRoutingEnabled;
			}
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x00060530 File Offset: 0x0005E730
		public static RoutingDestination GetRoutingDestination(MailRecipient recipient, RoutingContext context)
		{
			string addressType;
			string address;
			string text;
			if (!ConnectorRoutingDestination.TryGetRecipientAddress(recipient, context, out addressType, out address, out text))
			{
				return ConnectorRoutingDestination.invalidAddressNdr;
			}
			ConnectorRoutingDestination connectorRoutingDestination = null;
			switch (context.RoutingTables.ConnectorMap.TryFindBestConnector(addressType, address, context.MessageSize, out connectorRoutingDestination))
			{
			case ConnectorMatchResult.InvalidSmtpAddress:
				return ConnectorRoutingDestination.invalidAddressNdr;
			case ConnectorMatchResult.InvalidX400Address:
				return ConnectorRoutingDestination.invalidX400AddressNdr;
			case ConnectorMatchResult.MaxMessageSizeExceeded:
				return ConnectorRoutingDestination.maxMessageSizeExceededNdr;
			case ConnectorMatchResult.NoAddressMatch:
				if (!RoutingUtils.IsSmtpAddressType(addressType))
				{
					return ConnectorRoutingDestination.noMatchingConnectorNdr;
				}
				return ConnectorRoutingDestination.noMatchingConnectorUnreachable;
			}
			if (connectorRoutingDestination.DnsRoutingEnabled && text.IndexOf(RoutingDomain.Separator) != -1)
			{
				return ConnectorRoutingDestination.dnsDomainUnreachable;
			}
			return connectorRoutingDestination;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000605D4 File Offset: 0x0005E7D4
		public static bool TryGetRecipientAddress(MailRecipient recipient, RoutingContext context, out string addressType, out string address, out string dnsDomain)
		{
			addressType = null;
			address = null;
			dnsDomain = null;
			if (recipient.RoutingOverride != null)
			{
				RoutingDomain routingDomain = recipient.RoutingOverride.RoutingDomain;
				address = routingDomain.Domain;
				addressType = routingDomain.Type;
				switch (recipient.RoutingOverride.DeliveryQueueDomain)
				{
				case DeliveryQueueDomain.UseOverrideDomain:
					if (routingDomain.IsSmtp())
					{
						dnsDomain = address;
					}
					else
					{
						dnsDomain = routingDomain.ToString();
					}
					break;
				case DeliveryQueueDomain.UseRecipientDomain:
					dnsDomain = recipient.Email.DomainPart;
					break;
				case DeliveryQueueDomain.UseAlternateDeliveryRoutingHosts:
					dnsDomain = recipient.RoutingOverride.AlternateDeliveryRoutingHostsString;
					break;
				default:
					throw new InvalidOperationException("Unexpected DeliveryQueueDomain value: " + recipient.RoutingOverride.DeliveryQueueDomain);
				}
			}
			else
			{
				if (!context.Core.IsEdgeMode)
				{
					ProxyAddress proxyAddress;
					if (context.Core.Dependencies.TryDeencapsulate(recipient.Email, out proxyAddress))
					{
						addressType = proxyAddress.Prefix.PrimaryPrefix;
						address = proxyAddress.AddressString;
						dnsDomain = recipient.Email.DomainPart;
						RoutingDiag.Tracer.TraceDebug(0L, "[{0}] De-encapsulated address '{1}' into '{2}:{3}'", new object[]
						{
							context.Timestamp,
							recipient.Email.ToString(),
							addressType,
							address
						});
					}
					else if (proxyAddress is InvalidProxyAddress)
					{
						RoutingDiag.Tracer.TraceError<DateTime, string, ProxyAddress>(0L, "[{0}] Cannot route to encapsulated recipient address {1} because inner address <{2}> is invalid", context.Timestamp, recipient.Email.ToString(), proxyAddress);
						return false;
					}
				}
				if (string.IsNullOrEmpty(address))
				{
					addressType = "smtp";
					address = recipient.Email.DomainPart;
					dnsDomain = address;
				}
			}
			if (string.IsNullOrEmpty(address) || string.IsNullOrEmpty(dnsDomain))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>(0L, "[{0}] Cannot route to recipient with null or empty domain {1}", context.Timestamp, recipient.Email.ToString());
				return false;
			}
			return true;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000607E8 File Offset: 0x0005E9E8
		public static bool TryGetNextHop(NextHopSolutionKey nextHopKey, RoutingTables routingTables, out RoutingNextHop nextHop)
		{
			if (!routingTables.ConnectorMap.TryGetConnectorNextHop(nextHopKey.NextHopConnector, out nextHop))
			{
				RoutingDiag.Tracer.TraceError<DateTime, NextHopSolutionKey>(0L, "[{0}] No connector next hop for next hop key <{1}>", routingTables.WhenCreated, nextHopKey);
				return false;
			}
			if (nextHopKey.NextHopType.DeliveryType != nextHop.DeliveryType)
			{
				RoutingDiag.Tracer.TraceError<DateTime, DeliveryType, NextHopSolutionKey>(0L, "[{0}] Delivery Type mismatch between connector {1} and next hop key <{2}>", routingTables.WhenCreated, nextHop.DeliveryType, nextHopKey);
				nextHop = null;
				return false;
			}
			return true;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00060861 File Offset: 0x0005EA61
		public bool Match(ConnectorRoutingDestination other)
		{
			return this.ConnectorGuid == other.ConnectorGuid && this.MaxMessageSize == other.MaxMessageSize && base.RouteInfo.Match(other.RouteInfo, NextHopMatch.Full);
		}

		// Token: 0x04000B96 RID: 2966
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination noMatchingConnectorUnreachable = new ConnectorRoutingDestination.NoMatchingConnectorDestination(UnreachableNextHop.NoMatchingConnector);

		// Token: 0x04000B97 RID: 2967
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination noMatchingConnectorNdr = new ConnectorRoutingDestination.NoMatchingConnectorDestination(NdrNextHop.NoConnectorForAddressType);

		// Token: 0x04000B98 RID: 2968
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination invalidAddressNdr = new ConnectorRoutingDestination.NoMatchingConnectorDestination(NdrNextHop.InvalidAddressForRouting);

		// Token: 0x04000B99 RID: 2969
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination invalidX400AddressNdr = new ConnectorRoutingDestination.NoMatchingConnectorDestination(NdrNextHop.InvalidX400AddressForRouting);

		// Token: 0x04000B9A RID: 2970
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination maxMessageSizeExceededNdr = new ConnectorRoutingDestination.NoMatchingConnectorDestination(NdrNextHop.MessageTooLargeForRoute);

		// Token: 0x04000B9B RID: 2971
		private static ConnectorRoutingDestination.NoMatchingConnectorDestination dnsDomainUnreachable = new ConnectorRoutingDestination.NoMatchingConnectorDestination(UnreachableNextHop.IncompatibleDeliveryDomain);

		// Token: 0x04000B9C RID: 2972
		private MailGateway connector;

		// Token: 0x04000B9D RID: 2973
		private IList<AddressSpace> addressSpaces;

		// Token: 0x0200021B RID: 539
		private class NoMatchingConnectorDestination : UnroutableDestination
		{
			// Token: 0x060017C7 RID: 6087 RVA: 0x0006092A File Offset: 0x0005EB2A
			public NoMatchingConnectorDestination(RoutingNextHop nextHop) : base(MailRecipientType.External, "<No Matching Connector>", nextHop)
			{
			}
		}
	}
}
