using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200022A RID: 554
	internal class EnhancedDns : Dns, ITransportComponent, IDiagnosable, IEnhancedDns
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00062EB4 File Offset: 0x000610B4
		public SmtpSendConnectorConfig EnterpriseRelayConnector
		{
			get
			{
				return this.enterpriseRelayConnector;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00062EBC File Offset: 0x000610BC
		public SmtpSendConnectorConfig ClientProxyConnector
		{
			get
			{
				return this.clientProxyConnector;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00062EC4 File Offset: 0x000610C4
		private static ExEventLog Logger
		{
			get
			{
				if (EnhancedDns.eventLogger == null)
				{
					ExEventLog value = new ExEventLog(ExTraceGlobals.RoutingTracer.Category, TransportEventLog.GetEventSource());
					Interlocked.CompareExchange<ExEventLog>(ref EnhancedDns.eventLogger, value, null);
				}
				return EnhancedDns.eventLogger;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00062F00 File Offset: 0x00061100
		public static DnsStatus EndResolveToNextHop(IAsyncResult asyncResult, out EnhancedDnsTargetHost[] hosts, out IEnumerable<INextHopServer> destinationServers, out SmtpSendConnectorConfig destinationConnector, out SmtpSendConnectorConfig proxyConnector, out IPAddress reportingServer, out string diagnosticInfo)
		{
			diagnosticInfo = string.Empty;
			LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
			if (lazyAsyncResult == null || (!(lazyAsyncResult.AsyncObject is EnhancedDnsRequest) && !(lazyAsyncResult.AsyncObject is int)))
			{
				throw new ArgumentException("Incorrect IAsyncResult value");
			}
			int arg = (lazyAsyncResult.AsyncObject is EnhancedDnsRequest) ? (lazyAsyncResult.AsyncObject as EnhancedDnsRequest).RequestId : ((int)lazyAsyncResult.AsyncObject);
			if (lazyAsyncResult.Result is EnhancedDnsStatusResult)
			{
				EnhancedDnsStatusResult enhancedDnsStatusResult = (EnhancedDnsStatusResult)lazyAsyncResult.Result;
				ExTraceGlobals.RoutingTracer.TraceError<int, DnsStatus>(0L, "Request ID={0}: EndResolve with status '{1}'", arg, enhancedDnsStatusResult.Status);
				hosts = new EnhancedDnsTargetHost[0];
				destinationServers = enhancedDnsStatusResult.RequestContext.DestinationServers;
				destinationConnector = enhancedDnsStatusResult.RequestContext.DestinationConnector;
				proxyConnector = enhancedDnsStatusResult.RequestContext.ProxyConnector;
				reportingServer = enhancedDnsStatusResult.Server;
				diagnosticInfo = enhancedDnsStatusResult.DiagnosticInfo;
				return enhancedDnsStatusResult.Status;
			}
			EnhancedDnsHostsResult enhancedDnsHostsResult = (EnhancedDnsHostsResult)lazyAsyncResult.Result;
			hosts = enhancedDnsHostsResult.Hosts;
			destinationServers = enhancedDnsHostsResult.RequestContext.DestinationServers;
			destinationConnector = enhancedDnsHostsResult.RequestContext.DestinationConnector;
			proxyConnector = enhancedDnsHostsResult.RequestContext.ProxyConnector;
			reportingServer = IPAddress.None;
			ExTraceGlobals.RoutingTracer.TraceDebug<int, int>(0L, "Request ID={0}: EndResolve with {1} hosts", arg, hosts.Length);
			return (DnsStatus)lazyAsyncResult.ErrorCode;
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00063054 File Offset: 0x00061254
		public void SetRunTimeDependencies(IMailRouter router)
		{
			this.router = router;
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00063060 File Offset: 0x00061260
		public void Load()
		{
			this.requestCounter = 0;
			this.nextPollTcpipSettings = DateTime.UtcNow + EnhancedDns.TcpipPollInterval;
			NetworkChange.NetworkAddressChanged += this.HandleAddressChange;
			Components.Configuration.LocalServerChanged += this.HandleTransportServerConfigChange;
			if (!this.LoadConfiguration())
			{
				throw new TransportComponentLoadFailedException("Failed to load local IP address information");
			}
			Components.ConfigChanged += this.HandleConfigChange;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x000630D4 File Offset: 0x000612D4
		public void Unload()
		{
			NetworkChange.NetworkAddressChanged -= this.HandleAddressChange;
			Components.Configuration.LocalServerChanged -= this.HandleTransportServerConfigChange;
			Components.ConfigChanged -= this.HandleConfigChange;
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0006310E File Offset: 0x0006130E
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x00063114 File Offset: 0x00061314
		public void FlushCache()
		{
			DnsServerList serverList = this.internalDnsServerList;
			if (serverList != null)
			{
				serverList.FlushCache();
			}
			serverList = base.ServerList;
			if (serverList != null)
			{
				serverList.FlushCache();
			}
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00063144 File Offset: 0x00061344
		public IAsyncResult BeginResolveToNextHop(NextHopSolutionKey key, RiskLevel riskLevel, int outboundIPPool, AsyncCallback requestCallback, object stateObject)
		{
			if (this.router == null)
			{
				throw new InvalidOperationException("Router not set for Enhanced DNS");
			}
			this.CheckForTcpInterfaceChanges();
			int num = Interlocked.Increment(ref this.requestCounter);
			ExTraceGlobals.RoutingTracer.TraceDebug((long)this.GetHashCode(), "New enhanced DNS request ID={0}: NextHopType='{1}'; NextHopDomain='{2}'; Connector='{3}'", new object[]
			{
				num,
				key.NextHopType,
				key.NextHopDomain,
				key.NextHopConnector
			});
			IEnumerable<INextHopServer> enumerable;
			SmtpSendConnectorConfig smtpSendConnectorConfig;
			if (!this.router.TryGetServersForNextHop(key, out enumerable, out smtpSendConnectorConfig))
			{
				return EnhancedDnsRequest.CompleteWithStatus(num, DnsStatus.ConfigChanged, default(EnhancedDnsRequestContext), requestCallback, stateObject);
			}
			IEnumerable<INextHopServer> enumerable2 = null;
			SmtpSendConnectorConfig smtpSendConnectorConfig2 = null;
			EnhancedDnsRequest.QueryType queryType;
			if (smtpSendConnectorConfig == null)
			{
				queryType = EnhancedDnsRequest.QueryType.AQuery;
				smtpSendConnectorConfig = this.enterpriseRelayConnector;
			}
			else if (!smtpSendConnectorConfig.FrontendProxyEnabled)
			{
				queryType = EnhancedDnsRequest.QueryType.MXQuery;
			}
			else
			{
				bool flag;
				if (!this.router.TryGetOutboundFrontendServers(out enumerable2, out flag))
				{
					return EnhancedDnsRequest.CompleteWithStatus(num, DnsStatus.NoOutboundFrontendServers, new EnhancedDnsRequestContext(smtpSendConnectorConfig), requestCallback, stateObject);
				}
				if (flag)
				{
					queryType = EnhancedDnsRequest.QueryType.MXQuery;
					smtpSendConnectorConfig2 = this.outboundProxyExternalConnector;
				}
				else
				{
					queryType = EnhancedDnsRequest.QueryType.AQuery;
					smtpSendConnectorConfig2 = this.outboundProxyInternalConnector;
				}
			}
			EnhancedDnsRequestContext requestContext = new EnhancedDnsRequestContext((enumerable2 == null) ? null : enumerable, smtpSendConnectorConfig, smtpSendConnectorConfig2);
			EnhancedDnsRequest enhancedDnsRequest = new EnhancedDnsRequest(num, enumerable2 ?? enumerable, queryType, key.NextHopType.DeliveryType, (smtpSendConnectorConfig2 != null) ? smtpSendConnectorConfig2 : smtpSendConnectorConfig, riskLevel, outboundIPPool, smtpSendConnectorConfig2 != null, requestContext);
			DnsServerList list = null;
			DnsQueryOptions options = DnsQueryOptions.None;
			this.GetDnsQuerySettings(num, smtpSendConnectorConfig2 ?? smtpSendConnectorConfig, out list, out options);
			return enhancedDnsRequest.Resolve(this, list, options, requestCallback, stateObject);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x000632C4 File Offset: 0x000614C4
		public IAsyncResult BeginResolveProxyNextHop(IEnumerable<INextHopServer> destinations, bool internalDestination, SmtpSendConnectorConfig sendConnector, SmtpOutProxyType proxyType, RiskLevel riskLevel, int outboundIPPool, AsyncCallback requestCallback, object stateObject)
		{
			this.CheckForTcpInterfaceChanges();
			int num = Interlocked.Increment(ref this.requestCounter);
			ExTraceGlobals.RoutingTracer.TraceDebug<int, bool, SmtpOutProxyType>((long)this.GetHashCode(), "New enhanced DNS request for proxy ID={0}: internalDestination='{1}', proxyType='{2}'", num, internalDestination, proxyType);
			SmtpSendConnectorConfig smtpSendConnectorConfig = sendConnector;
			EnhancedDnsRequest.QueryType queryType;
			switch (proxyType)
			{
			case SmtpOutProxyType.PerMessage:
				if (smtpSendConnectorConfig != null)
				{
					throw new ArgumentException("PerMessage proxy type cannot be used with a specified connector");
				}
				smtpSendConnectorConfig = (internalDestination ? this.perMessageProxyInternalConnector : this.perMessageProxyExternalConnector);
				queryType = EnhancedDnsRequest.QueryType.AQuery;
				break;
			case SmtpOutProxyType.Blind:
				if (smtpSendConnectorConfig == null)
				{
					throw new ArgumentException("Blind proxy type cannot be used without a specified connector");
				}
				queryType = (internalDestination ? EnhancedDnsRequest.QueryType.AQuery : EnhancedDnsRequest.QueryType.MXQuery);
				break;
			case SmtpOutProxyType.ShadowPeerToPeer:
				if (smtpSendConnectorConfig != null)
				{
					throw new ArgumentException("ShadowPeerToPeer proxy type cannot be used with a specified connector");
				}
				if (!internalDestination)
				{
					throw new ArgumentException("ShadowPeerToPeer must be internal destination");
				}
				smtpSendConnectorConfig = this.enterpriseRelayConnector;
				queryType = EnhancedDnsRequest.QueryType.AQuery;
				break;
			default:
				throw new InvalidOperationException("Illegal proxy type");
			}
			EnhancedDnsRequest enhancedDnsRequest = new EnhancedDnsRequest(num, destinations, queryType, DeliveryType.Undefined, smtpSendConnectorConfig, riskLevel, outboundIPPool, false, new EnhancedDnsRequestContext(smtpSendConnectorConfig));
			DnsServerList list = null;
			DnsQueryOptions options = DnsQueryOptions.None;
			this.GetDnsQuerySettings(num, smtpSendConnectorConfig, out list, out options);
			return enhancedDnsRequest.Resolve(this, list, options, requestCallback, stateObject);
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x000633BD File Offset: 0x000615BD
		public void HandleTransportServerConfigChange(TransportServerConfiguration args)
		{
			ExTraceGlobals.RoutingTracer.TraceDebug((long)this.GetHashCode(), "Transport server config change detected");
			this.LoadConfiguration();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x000633DC File Offset: 0x000615DC
		private static DnsQueryOptions GetDnsOptions(ProtocolOption protocolOption)
		{
			DnsQueryOptions dnsQueryOptions = DnsQueryOptions.None;
			switch (protocolOption)
			{
			case ProtocolOption.UseUdpOnly:
				dnsQueryOptions |= DnsQueryOptions.AcceptTruncatedResponse;
				break;
			case ProtocolOption.UseTcpOnly:
				dnsQueryOptions |= DnsQueryOptions.UseTcpOnly;
				break;
			}
			return dnsQueryOptions;
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x0006340C File Offset: 0x0006160C
		private static ADObjectId GetHomeRoutingGroup(Server transportServer)
		{
			if (transportServer.HomeRoutingGroup != null)
			{
				return transportServer.HomeRoutingGroup;
			}
			if (transportServer.Id.Parent != null && transportServer.Id.Parent.Parent != null)
			{
				return transportServer.Id.Parent.Parent.GetChildId(RoutingGroupsContainer.DefaultName).GetChildId(RoutingGroup.DefaultName);
			}
			return transportServer.Id.GetChildId(RoutingGroupsContainer.DefaultName).GetChildId(RoutingGroup.DefaultName);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x00063486 File Offset: 0x00061686
		private void HandleAddressChange(object sender, EventArgs e)
		{
			ExTraceGlobals.RoutingTracer.TraceDebug((long)this.GetHashCode(), "Network address change detected");
			this.LoadConfiguration();
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000634A5 File Offset: 0x000616A5
		private void HandleConfigChange(object sender, EventArgs e)
		{
			ExTraceGlobals.RoutingTracer.TraceDebug((long)this.GetHashCode(), "Config change detected");
			this.LoadConfiguration();
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x000634C4 File Offset: 0x000616C4
		private bool LoadConfiguration()
		{
			Server transportServer = Components.Configuration.LocalServer.TransportServer;
			List<IPAddress> list = null;
			NetworkInformationException ex;
			if (!LocalComputer.TryGetIPAddresses(out list, out ex))
			{
				ExTraceGlobals.RoutingTracer.TraceError<NetworkInformationException>((long)this.GetHashCode(), "Failed GetLocalIPAddresses, {0}", ex);
				EnhancedDns.Logger.LogEvent(TransportEventLogConstants.Tuple_NetworkAdapterIPQueryFailed, null, new object[]
				{
					ex
				});
				return false;
			}
			IPAddress externalIPAddress = transportServer.ExternalIPAddress;
			if (externalIPAddress != null)
			{
				list.Add(externalIPAddress);
			}
			base.LocalIPAddresses = list;
			ExTraceGlobals.RoutingTracer.TraceDebug<List<IPAddress>>((long)this.GetHashCode(), "New local IP addresses: {0}", list);
			base.Options = EnhancedDns.GetDnsOptions(transportServer.ExternalDNSProtocolOption);
			MultiValuedProperty<IPAddress> externalDNSServers = transportServer.ExternalDNSServers;
			if (transportServer.ExternalDNSAdapterEnabled || MultiValuedPropertyBase.IsNullOrEmpty(externalDNSServers))
			{
				base.AdapterServerList(transportServer.ExternalDNSAdapterGuid, Components.TransportAppConfig.RemoteDelivery.ExcludeDnsServersFromLoopbackAdapters, Components.TransportAppConfig.RemoteDelivery.ExcludeIPv6SiteLocalDnsAddresses);
			}
			else
			{
				IPAddress[] array = new IPAddress[externalDNSServers.Count];
				externalDNSServers.CopyTo(array, 0);
				base.InitializeServerList(array);
			}
			MultiValuedProperty<IPAddress> multiValuedProperty = transportServer.InternalDNSServers;
			if (transportServer.InternalDNSAdapterEnabled || MultiValuedPropertyBase.IsNullOrEmpty(multiValuedProperty))
			{
				IPAddress[] adapterDnsServerList = DnsServerList.GetAdapterDnsServerList(transportServer.InternalDNSAdapterGuid, Components.TransportAppConfig.RemoteDelivery.ExcludeDnsServersFromLoopbackAdapters, Components.TransportAppConfig.RemoteDelivery.ExcludeIPv6SiteLocalDnsAddresses);
				if (adapterDnsServerList != null && adapterDnsServerList.Length != 0)
				{
					multiValuedProperty = adapterDnsServerList;
				}
				else
				{
					EnhancedDns.Logger.LogEvent(TransportEventLogConstants.Tuple_InvalidAdapterGuid, null, new object[]
					{
						transportServer.InternalDNSAdapterGuid
					});
					string notificationReason = string.Format("No DNS servers could be retrieved from network adapter {0}", transportServer.InternalDNSAdapterGuid);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Warning, false);
				}
			}
			DnsServerList dnsServerList = this.internalDnsServerList;
			IPAddress[] array2 = null;
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				array2 = new IPAddress[multiValuedProperty.Count];
				multiValuedProperty.CopyTo(array2, 0);
			}
			if (dnsServerList == null || !dnsServerList.IsAddressListIdentical(array2))
			{
				DnsServerList dnsServerList2 = new DnsServerList();
				dnsServerList2.Initialize(array2);
				DnsServerList dnsServerList3 = Interlocked.CompareExchange<DnsServerList>(ref this.internalDnsServerList, dnsServerList2, dnsServerList);
				if (dnsServerList3 == dnsServerList)
				{
					if (dnsServerList3 != null)
					{
						dnsServerList3.Dispose();
					}
				}
				else
				{
					dnsServerList2.Dispose();
				}
			}
			this.internalDnsOptions = EnhancedDns.GetDnsOptions(transportServer.InternalDNSProtocolOption);
			base.Timeout = Components.Configuration.AppConfig.RemoteDelivery.DnsRequestTimeout;
			base.QueryRetryInterval = Components.Configuration.AppConfig.RemoteDelivery.DnsQueryRetryInterval;
			if (!Components.Configuration.AppConfig.RemoteDelivery.DnsIpv6Enabled)
			{
				base.DefaultAddressFamily = AddressFamily.InterNetwork;
			}
			ADObjectId homeRoutingGroup = EnhancedDns.GetHomeRoutingGroup(transportServer);
			this.CreateEnterpriseRelayConnector(homeRoutingGroup);
			this.CreateProxyConnectors(homeRoutingGroup);
			ExTraceGlobals.RoutingTracer.TraceDebug<DnsServerList>((long)this.GetHashCode(), "New internal DNS servers: {0}", this.internalDnsServerList);
			ExTraceGlobals.RoutingTracer.TraceDebug<DnsQueryOptions>((long)this.GetHashCode(), "New internal DNS query options: {0}", this.internalDnsOptions);
			ExTraceGlobals.RoutingTracer.TraceDebug<DnsServerList>((long)this.GetHashCode(), "New external DNS servers: {0}", base.ServerList);
			ExTraceGlobals.RoutingTracer.TraceDebug<DnsQueryOptions>((long)this.GetHashCode(), "New external DNS query options: {0}", base.Options);
			ExTraceGlobals.RoutingTracer.TraceDebug<string>((long)this.GetHashCode(), "New timeout: {0}", base.Timeout.ToString());
			ExTraceGlobals.RoutingTracer.TraceDebug<string>((long)this.GetHashCode(), "New query retry interval: {0}", base.QueryRetryInterval.ToString());
			ExTraceGlobals.RoutingTracer.TraceDebug<AddressFamily>((long)this.GetHashCode(), "New default address family: {0}", base.DefaultAddressFamily);
			return true;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x0006385C File Offset: 0x00061A5C
		private void GetDnsQuerySettings(int requestId, SmtpSendConnectorConfig connector, out DnsServerList dnsServerList, out DnsQueryOptions dnsQueryOptions)
		{
			if (connector.UseExternalDNSServersEnabled)
			{
				dnsServerList = base.ServerList;
				dnsQueryOptions = base.Options;
				if (Components.TransportAppConfig.RemoteDelivery.DnsFaultTolerance == DnsFaultTolerance.Lenient)
				{
					dnsQueryOptions |= DnsQueryOptions.FailureTolerant;
				}
				else
				{
					dnsQueryOptions &= ~DnsQueryOptions.FailureTolerant;
				}
				ExTraceGlobals.RoutingTracer.TraceDebug<int>((long)this.GetHashCode(), "Request ID={0}: using external DNS", requestId);
				return;
			}
			dnsServerList = this.internalDnsServerList;
			dnsQueryOptions = this.internalDnsOptions;
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x000638D0 File Offset: 0x00061AD0
		private void CheckForTcpInterfaceChanges()
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow > this.nextPollTcpipSettings)
			{
				bool flag = false;
				lock (this)
				{
					if (utcNow > this.nextPollTcpipSettings)
					{
						this.nextPollTcpipSettings = utcNow + EnhancedDns.TcpipPollInterval;
						flag = true;
					}
				}
				if (flag && this.tcpInterfaceWatcher.IsChanged())
				{
					this.LoadConfiguration();
				}
			}
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x00063954 File Offset: 0x00061B54
		private void CreateProxyConnectors(ADObjectId homeRoutingGroup)
		{
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery)
			{
				this.perMessageProxyInternalConnector = new ProxySendConnector(Strings.MailboxProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, true, true, new TlsAuthLevel?(TlsAuthLevel.EncryptionOnly), null, false, 0, null, null);
				if (Components.TransportAppConfig.Routing.DisableExchangeServerAuth)
				{
					this.perMessageProxyInternalConnector.SmartHostAuthMechanism = SmtpSendConnectorConfig.AuthMechanisms.None;
					return;
				}
			}
			else
			{
				if (ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					this.perMessageProxyExternalConnector = new ProxySendConnector(Strings.ExternalDestinationInboundProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, false, Components.TransportAppConfig.SmtpInboundProxyConfiguration.RequireTls, new TlsAuthLevel?(Components.TransportAppConfig.SmtpInboundProxyConfiguration.TlsAuthLevel), Components.TransportAppConfig.SmtpInboundProxyConfiguration.TlsDomain, Components.TransportAppConfig.SmtpInboundProxyConfiguration.UseExternalDnsServers, 0, null, Components.TransportAppConfig.SmtpInboundProxyConfiguration.ExternalCertificateSubject);
					if (Components.Configuration.AppConfig.SmtpInboundProxyConfiguration.TreatProxyDestinationAsExternal)
					{
						this.perMessageProxyInternalConnector = this.perMessageProxyExternalConnector;
					}
					else
					{
						this.perMessageProxyInternalConnector = new ProxySendConnector(Strings.InternalDestinationInboundProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, true, true, new TlsAuthLevel?(TlsAuthLevel.EncryptionOnly), null, false, 0, null, null);
					}
					this.clientProxyConnector = new ProxySendConnector(Strings.ClientProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, true, true, new TlsAuthLevel?(TlsAuthLevel.EncryptionOnly), null, false, Components.TransportAppConfig.SmtpProxyConfiguration.ProxyPort, null, null);
					return;
				}
				if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub)
				{
					this.outboundProxyExternalConnector = new ProxySendConnector(Strings.ExternalDestinationOutboundProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, false, Components.TransportAppConfig.SmtpOutboundProxyConfiguration.RequireTls, new TlsAuthLevel?(Components.TransportAppConfig.SmtpOutboundProxyConfiguration.TlsAuthLevel), Components.TransportAppConfig.SmtpOutboundProxyConfiguration.TlsDomain, Components.TransportAppConfig.SmtpOutboundProxyConfiguration.UseExternalDnsServers, 0, null, Components.TransportAppConfig.SmtpOutboundProxyConfiguration.ExternalCertificateSubject);
					this.outboundProxyExternalConnector.ErrorPolicies = ErrorPolicies.DowngradeDnsFailures;
					if (Components.Configuration.AppConfig.SmtpOutboundProxyConfiguration.TreatProxyHopAsExternal)
					{
						this.outboundProxyInternalConnector = this.outboundProxyExternalConnector;
						return;
					}
					this.outboundProxyInternalConnector = new ProxySendConnector(Strings.InternalDestinationOutboundProxySendConnector, Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, true, true, new TlsAuthLevel?(TlsAuthLevel.EncryptionOnly), null, false, 717, null, null);
					this.outboundProxyInternalConnector.ErrorPolicies = ErrorPolicies.DowngradeDnsFailures;
				}
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x00063BE4 File Offset: 0x00061DE4
		private void CreateEnterpriseRelayConnector(ADObjectId homeRoutingGroup)
		{
			if (Components.IsBridgehead)
			{
				this.enterpriseRelayConnector = new EnterpriseRelaySendConnector(Components.Configuration.LocalServer.TransportServer, homeRoutingGroup, Components.TransportAppConfig.Routing.DisableExchangeServerAuth);
			}
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00063C17 File Offset: 0x00061E17
		public string GetDiagnosticComponentName()
		{
			return "Dns";
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00063C20 File Offset: 0x00061E20
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			string diagnosticComponentName = ((IDiagnosable)this).GetDiagnosticComponentName();
			XElement xelement = new XElement(diagnosticComponentName);
			if (flag2)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, help."));
			}
			else if (flag)
			{
				XElement xelement2 = new XElement("DnsCache");
				xelement.Add(xelement2);
				xelement.SetAttributeValue("DefaultAddressFamily", base.DefaultAddressFamily);
				xelement.SetAttributeValue("DnsOptions", this.internalDnsOptions);
				xelement.SetAttributeValue("LocalIPAddresses", (base.LocalIPAddresses == null) ? string.Empty : string.Join<IPAddress>(",", base.LocalIPAddresses));
				xelement.SetAttributeValue("DnsServers", (this.internalDnsServerList.Addresses == null) ? string.Empty : string.Join<IPAddress>(",", this.internalDnsServerList.Addresses));
				this.internalDnsServerList.Cache.AddDiagnosticInfoTo(xelement2);
			}
			return xelement;
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x00063D92 File Offset: 0x00061F92
		int IEnhancedDns.get_MaxDataPerRequest()
		{
			return base.MaxDataPerRequest;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x00063D9A File Offset: 0x00061F9A
		void IEnhancedDns.set_MaxDataPerRequest(int A_1)
		{
			base.MaxDataPerRequest = A_1;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00063DA3 File Offset: 0x00061FA3
		DnsServerList IEnhancedDns.get_ServerList()
		{
			return base.ServerList;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00063DAB File Offset: 0x00061FAB
		void IEnhancedDns.set_ServerList(DnsServerList A_1)
		{
			base.ServerList = A_1;
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00063DB4 File Offset: 0x00061FB4
		IEnumerable<IPAddress> IEnhancedDns.get_LocalIPAddresses()
		{
			return base.LocalIPAddresses;
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00063DBC File Offset: 0x00061FBC
		void IEnhancedDns.set_LocalIPAddresses(IEnumerable<IPAddress> A_1)
		{
			base.LocalIPAddresses = A_1;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00063DC5 File Offset: 0x00061FC5
		TimeSpan IEnhancedDns.get_Timeout()
		{
			return base.Timeout;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00063DCD File Offset: 0x00061FCD
		void IEnhancedDns.set_Timeout(TimeSpan A_1)
		{
			base.Timeout = A_1;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00063DD6 File Offset: 0x00061FD6
		TimeSpan IEnhancedDns.get_QueryRetryInterval()
		{
			return base.QueryRetryInterval;
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00063DDE File Offset: 0x00061FDE
		void IEnhancedDns.set_QueryRetryInterval(TimeSpan A_1)
		{
			base.QueryRetryInterval = A_1;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x00063DE7 File Offset: 0x00061FE7
		AddressFamily IEnhancedDns.get_DefaultAddressFamily()
		{
			return base.DefaultAddressFamily;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00063DEF File Offset: 0x00061FEF
		void IEnhancedDns.set_DefaultAddressFamily(AddressFamily A_1)
		{
			base.DefaultAddressFamily = A_1;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00063DF8 File Offset: 0x00061FF8
		DnsQueryOptions IEnhancedDns.get_Options()
		{
			return base.Options;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00063E00 File Offset: 0x00062000
		void IEnhancedDns.set_Options(DnsQueryOptions A_1)
		{
			base.Options = A_1;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00063E09 File Offset: 0x00062009
		void IEnhancedDns.AdapterServerList(Guid A_1)
		{
			base.AdapterServerList(A_1);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00063E12 File Offset: 0x00062012
		void IEnhancedDns.AdapterServerList(Guid A_1, bool A_2, bool A_3)
		{
			base.AdapterServerList(A_1, A_2, A_3);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00063E1D File Offset: 0x0006201D
		void IEnhancedDns.InitializeFromMachineServerList()
		{
			base.InitializeFromMachineServerList();
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00063E25 File Offset: 0x00062025
		void IEnhancedDns.InitializeServerList(IPAddress[] A_1)
		{
			base.InitializeServerList(A_1);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00063E2E File Offset: 0x0006202E
		IAsyncResult IEnhancedDns.BeginResolveToAddresses(string A_1, AddressFamily A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginResolveToAddresses(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00063E3B File Offset: 0x0006203B
		IAsyncResult IEnhancedDns.BeginResolveToAddresses(string A_1, AddressFamily A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginResolveToAddresses(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00063E4A File Offset: 0x0006204A
		IAsyncResult IEnhancedDns.BeginResolveToAddresses(string A_1, AddressFamily A_2, DnsServerList A_3, DnsQueryOptions A_4, AsyncCallback A_5, object A_6)
		{
			return base.BeginResolveToAddresses(A_1, A_2, A_3, A_4, A_5, A_6);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00063E5B File Offset: 0x0006205B
		IAsyncResult IEnhancedDns.BeginResolveToAddresses(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginResolveToAddresses(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00063E6A File Offset: 0x0006206A
		IAsyncResult IEnhancedDns.BeginResolveToMailServers(string A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginResolveToMailServers(A_1, A_2, A_3);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00063E75 File Offset: 0x00062075
		IAsyncResult IEnhancedDns.BeginResolveToMailServers(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginResolveToMailServers(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00063E82 File Offset: 0x00062082
		IAsyncResult IEnhancedDns.BeginResolveToMailServers(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginResolveToMailServers(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00063E91 File Offset: 0x00062091
		IAsyncResult IEnhancedDns.BeginResolveToMailServers(string A_1, bool A_2, AddressFamily A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginResolveToMailServers(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00063EA0 File Offset: 0x000620A0
		IAsyncResult IEnhancedDns.BeginRetrieveTextRecords(string A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginRetrieveTextRecords(A_1, A_2, A_3);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00063EAB File Offset: 0x000620AB
		IAsyncResult IEnhancedDns.BeginRetrieveTextRecords(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginRetrieveTextRecords(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00063EB8 File Offset: 0x000620B8
		IAsyncResult IEnhancedDns.BeginRetrieveTextRecords(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginRetrieveTextRecords(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00063EC7 File Offset: 0x000620C7
		IAsyncResult IEnhancedDns.BeginRetrieveSoaRecords(string A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginRetrieveSoaRecords(A_1, A_2, A_3);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00063ED2 File Offset: 0x000620D2
		IAsyncResult IEnhancedDns.BeginRetrieveSoaRecords(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginRetrieveSoaRecords(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00063EDF File Offset: 0x000620DF
		IAsyncResult IEnhancedDns.BeginRetrieveSoaRecords(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginRetrieveSoaRecords(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00063EEE File Offset: 0x000620EE
		IAsyncResult IEnhancedDns.BeginRetrieveCNameRecords(string A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginRetrieveCNameRecords(A_1, A_2, A_3);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00063EF9 File Offset: 0x000620F9
		IAsyncResult IEnhancedDns.BeginRetrieveCNameRecords(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginRetrieveCNameRecords(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00063F06 File Offset: 0x00062106
		IAsyncResult IEnhancedDns.BeginRetrieveCNameRecords(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginRetrieveCNameRecords(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00063F15 File Offset: 0x00062115
		IAsyncResult IEnhancedDns.BeginResolveToNames(IPAddress A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginResolveToNames(A_1, A_2, A_3);
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00063F20 File Offset: 0x00062120
		IAsyncResult IEnhancedDns.BeginResolveToNames(IPAddress A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginResolveToNames(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00063F2D File Offset: 0x0006212D
		IAsyncResult IEnhancedDns.BeginResolveToNames(IPAddress A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginResolveToNames(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00063F3C File Offset: 0x0006213C
		IAsyncResult IEnhancedDns.BeginRetrieveSrvRecords(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginRetrieveSrvRecords(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00063F49 File Offset: 0x00062149
		IAsyncResult IEnhancedDns.BeginRetrieveNsRecords(string A_1, AsyncCallback A_2, object A_3)
		{
			return base.BeginRetrieveNsRecords(A_1, A_2, A_3);
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00063F54 File Offset: 0x00062154
		IAsyncResult IEnhancedDns.BeginRetrieveNsRecords(string A_1, DnsQueryOptions A_2, AsyncCallback A_3, object A_4)
		{
			return base.BeginRetrieveNsRecords(A_1, A_2, A_3, A_4);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00063F61 File Offset: 0x00062161
		IAsyncResult IEnhancedDns.BeginRetrieveNsRecords(string A_1, DnsServerList A_2, DnsQueryOptions A_3, AsyncCallback A_4, object A_5)
		{
			return base.BeginRetrieveNsRecords(A_1, A_2, A_3, A_4, A_5);
		}

		// Token: 0x04000BD0 RID: 3024
		public const int OutboundProxyFrontendPort = 717;

		// Token: 0x04000BD1 RID: 3025
		private static readonly TimeSpan TcpipPollInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000BD2 RID: 3026
		private static ExEventLog eventLogger;

		// Token: 0x04000BD3 RID: 3027
		private DnsQueryOptions internalDnsOptions;

		// Token: 0x04000BD4 RID: 3028
		private DnsServerList internalDnsServerList;

		// Token: 0x04000BD5 RID: 3029
		private IMailRouter router;

		// Token: 0x04000BD6 RID: 3030
		private int requestCounter;

		// Token: 0x04000BD7 RID: 3031
		private DateTime nextPollTcpipSettings;

		// Token: 0x04000BD8 RID: 3032
		private RegistryWatcher tcpInterfaceWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces", true);

		// Token: 0x04000BD9 RID: 3033
		private SmtpSendConnectorConfig enterpriseRelayConnector;

		// Token: 0x04000BDA RID: 3034
		private SmtpSendConnectorConfig perMessageProxyInternalConnector;

		// Token: 0x04000BDB RID: 3035
		private SmtpSendConnectorConfig perMessageProxyExternalConnector;

		// Token: 0x04000BDC RID: 3036
		private SmtpSendConnectorConfig outboundProxyInternalConnector;

		// Token: 0x04000BDD RID: 3037
		private SmtpSendConnectorConfig outboundProxyExternalConnector;

		// Token: 0x04000BDE RID: 3038
		private SmtpSendConnectorConfig clientProxyConnector;
	}
}
