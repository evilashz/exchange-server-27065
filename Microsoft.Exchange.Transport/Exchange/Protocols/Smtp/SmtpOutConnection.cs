using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200050E RID: 1294
	internal class SmtpOutConnection
	{
		// Token: 0x06003C77 RID: 15479 RVA: 0x000FAF84 File Offset: 0x000F9184
		public SmtpOutConnection(NextHopConnection connection, ProtocolLog protocolLog, IMailRouter mailRouter, EnhancedDns enhancedDns, UnhealthyTargetFilterComponent unhealthyTargetFilter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, ISmtpInComponent smtpInComponent, RiskLevel riskLevel, int outboundIPPool, int perHostConnectionAttemptCount, string connectionContextString) : this(connection, protocolLog, mailRouter, enhancedDns, unhealthyTargetFilter, certificateCache, certificateValidator, shadowRedundancyManager, transportAppConfig, transportConfiguration, smtpInComponent, riskLevel, outboundIPPool, 0, perHostConnectionAttemptCount, false, null, null, false, connectionContextString)
		{
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x000FAFB8 File Offset: 0x000F91B8
		public SmtpOutConnection(NextHopConnection connection, ProtocolLog protocolLog, IMailRouter mailRouter, EnhancedDns enhancedDns, UnhealthyTargetFilterComponent unhealthyTargetFilter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, ISmtpInComponent smtpInComponent, RiskLevel riskLevel, int outboundIPPool) : this(connection, protocolLog, mailRouter, enhancedDns, unhealthyTargetFilter, certificateCache, certificateValidator, shadowRedundancyManager, transportAppConfig, transportConfiguration, smtpInComponent, riskLevel, outboundIPPool, 0, 1, false, null, null, false, null)
		{
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x000FAFEC File Offset: 0x000F91EC
		public SmtpOutConnection(NextHopConnection connection, ProtocolLog protocolLog, IMailRouter mailRouter, EnhancedDns enhancedDns, UnhealthyTargetFilterComponent unhealthyTargetFilter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, ISmtpInComponent smtpInComponent, RiskLevel riskLevel, int outboundIPPool, int fixedTotalConnectionAttemptCount, int perHostConnectionAttemptCount, bool clientProxy, TlsSendConfiguration tlsSendConfiguration, ISmtpInSession inSession, bool isShadowOut, string connectionContextString)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.nextHopConnection = connection;
			this.protocolLog = protocolLog;
			this.mailRouter = mailRouter;
			this.certificateCache = certificateCache;
			this.certificateValidator = certificateValidator;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
			this.smtpInComponent = smtpInComponent;
			this.fixedTotalConnectionAttemptCount = fixedTotalConnectionAttemptCount;
			this.perHostConnectionAttemptCount = perHostConnectionAttemptCount;
			this.ClientProxy = clientProxy;
			this.inSession = inSession;
			this.isShadowOut = isShadowOut;
			this.TlsConfig = tlsSendConfiguration;
			this.RiskLevel = riskLevel;
			this.OutboundIPPool = outboundIPPool;
			this.connectionContextString = connectionContextString;
			this.smtpOutTargetHostPicker = new SmtpOutTargetHostPicker(this.sessionId, this, this.nextHopConnection, enhancedDns, unhealthyTargetFilter.UnhealthyTargetIPAddressFilter, unhealthyTargetFilter.UnhealthyTargetFqdnFilter);
			this.unhealthyTargetFilter = unhealthyTargetFilter;
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003C7A RID: 15482 RVA: 0x000FB0E9 File Offset: 0x000F92E9
		public bool IsBlindProxy
		{
			get
			{
				return this.inSession != null;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x000FB0F7 File Offset: 0x000F92F7
		public bool NextHopIsOutboundProxy
		{
			get
			{
				return this.outboundProxyContext != null;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003C7C RID: 15484 RVA: 0x000FB105 File Offset: 0x000F9305
		public string SmtpHostName
		{
			get
			{
				return this.smtpOutTargetHostPicker.SmtpHostName;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x000FB112 File Offset: 0x000F9312
		public string SmtpHost
		{
			get
			{
				return this.smtpOutTargetHostPicker.SmtpHost;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x000FB11F File Offset: 0x000F931F
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x000FB127 File Offset: 0x000F9327
		public ulong BytesSent
		{
			get
			{
				return this.bytesSent;
			}
			set
			{
				this.bytesSent = value;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x000FB130 File Offset: 0x000F9330
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x000FB138 File Offset: 0x000F9338
		public ulong MessagesSent
		{
			get
			{
				return this.messagesSent;
			}
			set
			{
				this.messagesSent = value;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003C82 RID: 15490 RVA: 0x000FB141 File Offset: 0x000F9341
		// (set) Token: 0x06003C83 RID: 15491 RVA: 0x000FB149 File Offset: 0x000F9349
		public ulong DiscardIdsReceived
		{
			get
			{
				return this.discardIdsReceived;
			}
			set
			{
				this.discardIdsReceived = value;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06003C84 RID: 15492 RVA: 0x000FB152 File Offset: 0x000F9352
		public SmtpSendConnectorConfig Connector
		{
			get
			{
				return this.sendConnector;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x000FB15A File Offset: 0x000F935A
		public SmtpSendPerfCountersInstance SmtpSendPerformanceCounters
		{
			get
			{
				return this.smtpSendPerformanceCounters;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06003C86 RID: 15494 RVA: 0x000FB162 File Offset: 0x000F9362
		public int TotalTargets
		{
			get
			{
				return this.smtpOutTargetHostPicker.TotalTargets;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x000FB16F File Offset: 0x000F936F
		public ProtocolLog ProtocolLog
		{
			get
			{
				return this.protocolLog;
			}
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000FB177 File Offset: 0x000F9377
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000FB17F File Offset: 0x000F937F
		internal TlsSendConfiguration TlsConfig { get; private set; }

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x000FB188 File Offset: 0x000F9388
		// (set) Token: 0x06003C8B RID: 15499 RVA: 0x000FB190 File Offset: 0x000F9390
		internal RiskLevel RiskLevel { get; private set; }

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06003C8C RID: 15500 RVA: 0x000FB199 File Offset: 0x000F9399
		// (set) Token: 0x06003C8D RID: 15501 RVA: 0x000FB1A1 File Offset: 0x000F93A1
		internal int OutboundIPPool { get; private set; }

		// Token: 0x06003C8E RID: 15502 RVA: 0x000FB1AA File Offset: 0x000F93AA
		public void DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x06003C8F RID: 15503 RVA: 0x000FB1B8 File Offset: 0x000F93B8
		public void SetSendConnector(SmtpSendConnectorConfig connector, SmtpSendConnectorConfig outboundProxyDestinationConnector, IEnumerable<INextHopServer> outboundProxyDestinations)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SetSendConnector);
			this.sendConnector = connector;
			if (outboundProxyDestinationConnector != null)
			{
				this.SetOutboundProxyContext(outboundProxyDestinations, outboundProxyDestinationConnector);
			}
			bool flag = outboundProxyDestinationConnector == null;
			if (this.createSmtpSendPerfCounters)
			{
				string connectorName = (outboundProxyDestinationConnector != null) ? outboundProxyDestinationConnector.Name : this.sendConnector.Name;
				this.smtpSendPerformanceCounters = SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(connectorName);
			}
			if (this.TlsConfig == null)
			{
				if (flag)
				{
					this.TlsConfig = new TlsSendConfiguration(this.sendConnector, this.nextHopConnection.Key.TlsAuthLevel, this.nextHopConnection.Key.NextHopDomain, this.nextHopConnection.Key.NextHopTlsDomain);
					return;
				}
				this.TlsConfig = new TlsSendConfiguration(this.sendConnector, null, null, null);
			}
		}

		// Token: 0x06003C90 RID: 15504 RVA: 0x000FB283 File Offset: 0x000F9483
		public void UpdateOnSuccessfulOutboundProxySetup(IEnumerable<INextHopServer> remainingDestinations, bool shouldSkipTls)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.UpdateOnSuccessfulOutboundProxySetup);
			if (!this.NextHopIsOutboundProxy)
			{
				throw new InvalidOperationException("Should only be called if the next hop is outbound proxy");
			}
			this.outboundProxyContext.ProxyDestinations = remainingDestinations;
			this.outboundProxyContext.ProxyTlsConfiguration.ShouldSkipTls = shouldSkipTls;
		}

		// Token: 0x06003C91 RID: 15505 RVA: 0x000FB2C0 File Offset: 0x000F94C0
		public void GetOutboundProxyDestinationSettings(out IEnumerable<INextHopServer> destinations, out SmtpSendConnectorConfig sendConnector, out TlsSendConfiguration tlsSendConfiguration, out RiskLevel riskLevel, out int outboundIPPool)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.GetOutboundProxyDestinationSettings);
			if (!this.NextHopIsOutboundProxy)
			{
				throw new InvalidOperationException("Should only be called if the next hop is outbound proxy");
			}
			destinations = this.outboundProxyContext.ProxyDestinations;
			sendConnector = this.outboundProxyContext.ProxySendConnector;
			tlsSendConfiguration = this.outboundProxyContext.ProxyTlsConfiguration;
			riskLevel = this.outboundProxyContext.ProxyRiskLevel;
			outboundIPPool = this.outboundProxyContext.OutboundIPPool;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x000FB32B File Offset: 0x000F952B
		public bool TryGetRemainingSmtpTargets(out IEnumerable<INextHopServer> remainingTargets)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.TryGetRemainingSmtpTargets);
			return this.smtpOutTargetHostPicker.TryGetRemainingSmtpTargets(out remainingTargets);
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x000FB341 File Offset: 0x000F9541
		public void UpdateSession(NetworkConnection nc)
		{
			this.nextHopConnection.ConnectionAttemptSucceeded();
			this.smtpOutSession.ConnectionCompleted(nc);
		}

		// Token: 0x06003C94 RID: 15508 RVA: 0x000FB35A File Offset: 0x000F955A
		public void Shutdown()
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.Shutdown);
			if (this.smtpOutSession != null)
			{
				this.smtpOutSession.ShutdownConnection();
			}
		}

		// Token: 0x06003C95 RID: 15509 RVA: 0x000FB378 File Offset: 0x000F9578
		public void Connect()
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.Connect);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<NextHopType, string, Guid>((long)this.GetHashCode(), "Resolving next hop '{0}':'{1}':'{2}' in Enhanced DNS.", this.nextHopConnection.Key.NextHopType, this.nextHopConnection.Key.NextHopDomain, this.nextHopConnection.Key.NextHopConnector);
			int[] activeCountsPerPriority;
			int[] retryCountsPerPriority;
			this.nextHopConnection.GetQueueCountsOnlyForIndividualPriorities(out activeCountsPerPriority, out retryCountsPerPriority);
			ConnectionLog.SmtpConnectionStart(this.sessionId, this.nextHopConnection.Key, this.nextHopConnection.ActiveQueueLength, activeCountsPerPriority, retryCountsPerPriority, null);
			this.smtpOutTargetHostPicker.ResolveToNextHopAndConnect();
		}

		// Token: 0x06003C96 RID: 15510 RVA: 0x000FB41C File Offset: 0x000F961C
		public void ConnectToPerMessageProxyDestinations(IEnumerable<INextHopServer> destinations, bool internalDestination)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.ConnectToPerMessageProxyDestinations);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<NextHopType, string, Guid>((long)this.GetHashCode(), "Resolving proxy next hop '{0}':'{1}':'{2}' in Enhanced DNS.", this.nextHopConnection.Key.NextHopType, this.nextHopConnection.Key.NextHopDomain, this.nextHopConnection.Key.NextHopConnector);
			ConnectionLog.SmtpConnectionStart(this.sessionId, this.nextHopConnection.Key, this.nextHopConnection.ActiveQueueLength, null, null, this.connectionContextString);
			this.smtpOutTargetHostPicker.ResolveProxyNextHopAndConnect(destinations, internalDestination, SmtpOutProxyType.PerMessage);
		}

		// Token: 0x06003C97 RID: 15511 RVA: 0x000FB4B8 File Offset: 0x000F96B8
		public void ConnectToBlindProxyDestinations(IEnumerable<INextHopServer> destinations, bool internalDestinations, SmtpSendConnectorConfig connector)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.ConnectToBlindProxyDestinations);
			if (this.inSession == null)
			{
				throw new InvalidOperationException("InSession needs to be set for blind proxy");
			}
			this.createSmtpSendPerfCounters = false;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<NextHopType, string, Guid>((long)this.GetHashCode(), "Resolving proxy next hop '{0}':'{1}':'{2}' in Enhanced DNS.", this.nextHopConnection.Key.NextHopType, this.nextHopConnection.Key.NextHopDomain, this.nextHopConnection.Key.NextHopConnector);
			ConnectionLog.SmtpConnectionStart(this.sessionId, this.nextHopConnection.Key, this.connectionContextString);
			this.smtpOutTargetHostPicker.ResolveProxyNextHopAndConnect(destinations, internalDestinations, connector);
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000FB560 File Offset: 0x000F9760
		public void ConnectToShadowDestinations(IEnumerable<INextHopServer> destinations)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.ConnectToShadowDestinations);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Initiating resolution and connection to shadow next hop");
			ConnectionLog.SmtpConnectionStart(this.sessionId, this.nextHopConnection.Key, this.nextHopConnection.ActiveQueueLength, null, null, null);
			this.smtpOutTargetHostPicker.ResolveProxyNextHopAndConnect(destinations, true, SmtpOutProxyType.ShadowPeerToPeer);
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x000FB5C0 File Offset: 0x000F97C0
		public void FailoverConnection(SmtpResponse response, bool retryWithoutStartTls, SessionSetupFailureReason failoverReason, bool nextHopWasProxyingBlindly)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.FailoverConnection);
			ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Attempting to failover connection to an alternate IP address for domain '{0}':'{1}':'{2}'.retryWithoutStartTls={3}, failoverReason={4}, nexthopWasProxyingBlindly={5}", new object[]
			{
				this.nextHopConnection.Key.NextHopType,
				this.nextHopConnection.Key.NextHopDomain,
				this.nextHopConnection.Key.NextHopConnector,
				retryWithoutStartTls,
				failoverReason,
				nextHopWasProxyingBlindly
			});
			if (this.failoverResponse.Equals(SmtpResponse.Empty))
			{
				this.failoverResponse = response;
				this.failoverReason = failoverReason;
			}
			this.lastFailoverReason = failoverReason;
			this.UpdateSmtpSendFailurePerfCounter(failoverReason);
			this.nextHopConnection.NotifyConnectionFailedOver(this.smtpOutTargetHostPicker.SmtpHostName, response, failoverReason);
			this.ConnectToNextHost(retryWithoutStartTls, nextHopWasProxyingBlindly);
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x000FB6AA File Offset: 0x000F98AA
		public void FailoverConnection(SmtpResponse response, bool retryWithoutStartTls, SessionSetupFailureReason failoverReason)
		{
			this.FailoverConnection(response, retryWithoutStartTls, failoverReason, false);
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x000FB6B6 File Offset: 0x000F98B6
		public void ConnectToNextHost()
		{
			this.ConnectToNextHost(false, false);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x000FB6C0 File Offset: 0x000F98C0
		public void UpdateSmtpSendFailurePerfCounter(SessionSetupFailureReason failureReason)
		{
			if (this.SmtpSendPerformanceCounters != null)
			{
				switch (failureReason)
				{
				case SessionSetupFailureReason.None:
				case SessionSetupFailureReason.UserLookupFailure:
				case SessionSetupFailureReason.Shutdown:
					break;
				case SessionSetupFailureReason.DnsLookupFailure:
					this.smtpSendPerformanceCounters.DnsErrors.Increment();
					return;
				case SessionSetupFailureReason.ConnectionFailure:
					this.SmtpSendPerformanceCounters.ConnectionFailures.Increment();
					return;
				case SessionSetupFailureReason.ProtocolError:
					this.SmtpSendPerformanceCounters.ProtocolErrors.Increment();
					return;
				case SessionSetupFailureReason.SocketError:
					this.SmtpSendPerformanceCounters.SocketErrors.Increment();
					return;
				default:
					throw new InvalidOperationException("Invalid session failure reason");
				}
			}
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x000FB750 File Offset: 0x000F9950
		internal void AckConnectionForResubmitWithoutHighAvailability(SmtpResponse smtpResponse, string reason)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.AckConnectionForResubmitWithoutHighAvailability);
			if (this.nextHopConnection == null)
			{
				throw new InvalidOperationException("Connection has already been acked!");
			}
			AckStatus ackStatus = AckStatus.Resubmit;
			string text = ackStatus.ToString();
			if (reason != null)
			{
				text = text + " : " + reason;
			}
			ConnectionLog.SmtpConnectionStop(this.sessionId, this.nextHopConnection.Key.NextHopDomain, text, this.messagesSent, this.bytesSent, this.discardIdsReceived);
			this.nextHopConnection.AckConnection(MessageTrackingSource.QUEUE, null, ackStatus, smtpResponse, null, null, true);
			this.nextHopConnection = null;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x000FB7E8 File Offset: 0x000F99E8
		internal void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, string reason, SessionSetupFailureReason failureReason)
		{
			this.AckConnection(ackStatus, smtpResponse, details, reason, failureReason, true);
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x000FB7F8 File Offset: 0x000F99F8
		internal void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, string reason, SessionSetupFailureReason failureReason, bool updateFailureCounters)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.AckConnection);
			if (this.nextHopConnection == null)
			{
				throw new InvalidOperationException("Connection has already been acked!");
			}
			string text = string.Empty;
			if (ackStatus != AckStatus.Success)
			{
				text = ackStatus.ToString();
				if (reason != null)
				{
					text = text + " : " + reason;
				}
			}
			ConnectionLog.SmtpConnectionStop(this.sessionId, this.nextHopConnection.Key.NextHopDomain, text, this.messagesSent, this.bytesSent, this.discardIdsReceived);
			if (updateFailureCounters)
			{
				this.UpdateSmtpSendFailurePerfCounter(failureReason);
			}
			this.nextHopConnection.AckConnection(ackStatus, smtpResponse, details, failureReason);
			this.nextHopConnection = null;
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x000FB89B File Offset: 0x000F9A9B
		internal void RemoveConnection()
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.RemoveConnection);
			if (this.connectionSucceededToNextHop)
			{
				this.smtpOutTargetHostPicker.ConnectionDisconnected();
				this.connectionSucceededToNextHop = false;
			}
			SmtpOutConnectionHandler.RemoveConnection(this.sessionId);
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x000FB8CA File Offset: 0x000F9ACA
		internal bool AddConnection()
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.AddConnection);
			if (!this.connectionAdded)
			{
				if (!SmtpOutConnectionHandler.AddConnection(this.sessionId, this))
				{
					return false;
				}
				this.connectionAdded = true;
			}
			return true;
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x000FB8F3 File Offset: 0x000F9AF3
		internal string GetSessionAndConnectionInfo()
		{
			if (this.smtpOutSession != null)
			{
				return this.smtpOutSession.GetConnectionInfo();
			}
			return string.Empty;
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000FB90E File Offset: 0x000F9B0E
		internal void RoutingTableUpdate()
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.RoutingTableUpdate);
			this.smtpOutTargetHostPicker.RoutingTableUpdate();
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000FB924 File Offset: 0x000F9B24
		internal void NextHopResolutionFailed(DnsStatus status, IPAddress reportingServer, string diagnosticInfo)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.NextHopResolutionFailed);
			bool flag = false;
			bool flag2 = false;
			SmtpSendConnectorConfig connector = this.Connector;
			if (connector == null && this.nextHopConnection.Key.NextHopType.IsSmtpConnectorDeliveryType)
			{
				this.mailRouter.TryGetLocalSendConnector<SmtpSendConnectorConfig>(this.nextHopConnection.Key.NextHopConnector, out connector);
			}
			SmtpResponse actualError = SmtpResponse.Empty;
			switch (status)
			{
			case DnsStatus.Success:
				throw new InvalidOperationException("NextHopResolutionFailed status=Success");
			case DnsStatus.InfoNoRecords:
				break;
			case DnsStatus.InfoDomainNonexistent:
			{
				SmtpResponse smtpResponse;
				if (this.NextHopIsOutboundProxy)
				{
					smtpResponse = AckReason.DnsNonExistentDomainForOutboundFrontend;
					ConnectionLog.SmtpHostResolutionFailedForOutboundProxyFrontend(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Non-existent domain", diagnosticInfo);
				}
				else
				{
					smtpResponse = AckReason.DnsNonExistentDomain;
					ConnectionLog.SmtpHostResolutionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Non-existent domain", diagnosticInfo);
				}
				if (!SmtpOutConnection.IsDnsConnectorDelivery(this.nextHopConnection.Key.NextHopType.DeliveryType, connector, this.transportConfiguration))
				{
					flag = true;
					actualError = smtpResponse;
					goto IL_4E2;
				}
				if (connector != null && (connector.ErrorPolicies & ErrorPolicies.DowngradeDnsFailures) != ErrorPolicies.Default)
				{
					if (this.NextHopIsOutboundProxy)
					{
						flag = true;
					}
					else
					{
						flag2 = true;
					}
					actualError = smtpResponse;
					goto IL_4E2;
				}
				this.AckConnectionWithDNSError(AckStatus.Fail, smtpResponse, "The domain name does not exist. Please correct the address and try again.");
				goto IL_4E2;
			}
			case DnsStatus.InfoMxLoopback:
			{
				SmtpResponse smtpResponse;
				if (this.NextHopIsOutboundProxy)
				{
					smtpResponse = AckReason.DnsMxLoopbackForOutboundFrontend;
					ConnectionLog.SmtpHostResolutionFailedForOutboundProxyFrontend(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Mail would loop back to itself", diagnosticInfo);
				}
				else
				{
					smtpResponse = AckReason.DnsMxLoopback;
					ConnectionLog.SmtpHostResolutionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Mail would loop back to itself", diagnosticInfo);
				}
				if (!SmtpOutConnection.IsDnsConnectorDelivery(this.nextHopConnection.Key.NextHopType.DeliveryType, connector, this.transportConfiguration))
				{
					flag = true;
					actualError = smtpResponse;
					goto IL_4E2;
				}
				if (connector != null && (connector.ErrorPolicies & ErrorPolicies.DowngradeDnsFailures) != ErrorPolicies.Default)
				{
					if (this.NextHopIsOutboundProxy)
					{
						flag = true;
					}
					else
					{
						flag2 = true;
					}
					actualError = smtpResponse;
					goto IL_4E2;
				}
				this.AckConnectionWithDNSError(AckStatus.Fail, smtpResponse, "The domain name has misconfigured records registered in DNS. The records are configured in a loop.");
				goto IL_4E2;
			}
			case DnsStatus.ErrorInvalidData:
			{
				SmtpResponse smtpResponse;
				if (this.NextHopIsOutboundProxy)
				{
					smtpResponse = AckReason.DnsInvalidDataOutboundFrontend;
					ConnectionLog.SmtpHostResolutionFailedForOutboundProxyFrontend(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Invalid DNS data returned", diagnosticInfo);
				}
				else
				{
					smtpResponse = AckReason.DnsInvalidData;
					ConnectionLog.SmtpHostResolutionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "Invalid DNS data returned", diagnosticInfo);
				}
				if (!SmtpOutConnection.IsDnsConnectorDelivery(this.nextHopConnection.Key.NextHopType.DeliveryType, connector, this.transportConfiguration))
				{
					flag = true;
					actualError = smtpResponse;
					goto IL_4E2;
				}
				if (connector != null && (connector.ErrorPolicies & ErrorPolicies.DowngradeDnsFailures) != ErrorPolicies.Default)
				{
					if (this.NextHopIsOutboundProxy)
					{
						flag = true;
					}
					else
					{
						flag2 = true;
					}
					actualError = smtpResponse;
					goto IL_4E2;
				}
				this.AckConnectionWithDNSError(AckStatus.Fail, smtpResponse, "A DNS server returned corrupt information when resolving this domain");
				goto IL_4E2;
			}
			default:
				if (status == DnsStatus.ConfigChanged)
				{
					string eventText = string.Format("The DNS query for '{0}':'{1}':'{2}' failed with error: {3}. {4}", new object[]
					{
						this.nextHopConnection.Key.NextHopType,
						this.nextHopConnection.Key.NextHopDomain,
						this.nextHopConnection.Key.NextHopConnector,
						status,
						(this.nextHopConnection.Key.NextHopType == NextHopType.Heartbeat) ? string.Empty : "The messages in this queue are being resubmitted to the categorizer."
					});
					this.AckConnectionWithDNSError(AckStatus.Resubmit, SmtpResponse.DnsConfigChangedSmtpResponse, eventText);
					goto IL_4E2;
				}
				if (status == DnsStatus.NoOutboundFrontendServers)
				{
					string eventText = string.Format("Connector {0} has the FrontendProxyEnabled option set to true but there are no suitable Frontend Transport servers in the local AD site or app.config.", (connector == null) ? this.nextHopConnection.Key.NextHopConnector.ToString() : connector.Name);
					this.AckConnectionWithDNSError(AckStatus.Retry, AckReason.NoOutboundFrontendServers, eventText);
					goto IL_4E2;
				}
				break;
			}
			if (this.NextHopIsOutboundProxy)
			{
				actualError = new SmtpResponse("451", "4.4.0", new string[]
				{
					"DNS query for the outbound proxy frontend server failed with error " + status
				});
				ConnectionLog.SmtpHostResolutionFailedForOutboundProxyFrontend(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "DNS server returned " + status.ToString(), diagnosticInfo);
			}
			else
			{
				actualError = new SmtpResponse("451", "4.4.0", new string[]
				{
					"DNS query failed with error " + status
				});
				ConnectionLog.SmtpHostResolutionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, reportingServer, "DNS server returned " + status.ToString(), diagnosticInfo);
			}
			flag = true;
			IL_4E2:
			if (flag || flag2)
			{
				string eventText2 = string.Format("The DNS query for {0} '{1}':'{2}':'{3}' failed with error {4}: {5}", new object[]
				{
					this.NextHopIsOutboundProxy ? "the outbound frontend servers to proxy" : string.Empty,
					this.nextHopConnection.Key.NextHopType,
					this.nextHopConnection.Key.NextHopDomain,
					this.nextHopConnection.Key.NextHopConnector,
					flag2 ? "but was ignored as per connector configuration" : string.Empty,
					status
				});
				this.AckConnectionWithDNSError(AckStatus.Retry, SmtpOutConnection.DnsQueryFailedResponse(actualError), eventText2);
			}
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000FBEC8 File Offset: 0x000FA0C8
		public void UpdateServerLatency(TimeSpan latency)
		{
			this.unhealthyTargetFilter.UpdateServerLatency(this.smtpOutTargetHostPicker.CurrentSmtpTarget, latency);
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000FBEE1 File Offset: 0x000FA0E1
		public TimeSpan GetDelayForCurrentTarget(IPEndPoint remoteEndPoint)
		{
			return this.unhealthyTargetFilter.GetServerLatency(new IPAddressPortPair(remoteEndPoint.Address, (ushort)remoteEndPoint.Port));
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x000FBF00 File Offset: 0x000FA100
		private static SmtpResponse DnsQueryFailedResponse(SmtpResponse actualError)
		{
			if (actualError.Equals(SmtpResponse.Empty) || actualError.StatusText == null)
			{
				return SmtpResponse.DnsQueryFailedResponseDefault;
			}
			return new SmtpResponse("451", "4.4.0", new string[]
			{
				"DNS query failed. The error was: " + actualError.StatusText[0]
			});
		}

		// Token: 0x06003CA8 RID: 15528 RVA: 0x000FBF58 File Offset: 0x000FA158
		private static void OnConnectComplete(IAsyncResult asyncResult)
		{
			SmtpOutConnection smtpOutConnection = (SmtpOutConnection)asyncResult.AsyncState;
			smtpOutConnection.ConnectComplete(asyncResult);
		}

		// Token: 0x06003CA9 RID: 15529 RVA: 0x000FBF78 File Offset: 0x000FA178
		private static bool IsProxySessionSetupProtocolFailure(SessionSetupFailureReason failoverReason, SmtpResponse failoverResponse)
		{
			return failoverReason == SessionSetupFailureReason.ProtocolError && (failoverResponse.Equals(SmtpResponse.ProxySessionProtocolSetupPermanentFailure) || failoverResponse.Equals(SmtpResponse.ProxySessionProtocolSetupTransientFailure)) && failoverResponse.StatusText.Length > 0 && failoverResponse.StatusText[0].StartsWith("Proxy session setup failed on Frontend with ", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003CAA RID: 15530 RVA: 0x000FBFCB File Offset: 0x000FA1CB
		private static bool IsDnsConnectorDeliveryOnHub(DeliveryType deliveryType, ProcessTransportRole processTransportRole)
		{
			return processTransportRole == ProcessTransportRole.Hub && deliveryType == DeliveryType.DnsConnectorDelivery;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x000FBFD6 File Offset: 0x000FA1D6
		private static bool IsDnsConnectorDeliveryOnEdge(DeliveryType deliveryType, ProcessTransportRole processTransportRole)
		{
			return processTransportRole == ProcessTransportRole.Edge && deliveryType == DeliveryType.DnsConnectorDelivery;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x000FBFE2 File Offset: 0x000FA1E2
		private static bool IsDnsConnectorDeliveryOnFrontend(DeliveryType deliveryType, SmtpSendConnectorConfig connector, ITransportConfiguration transportConfiguration)
		{
			return ConfigurationComponent.IsFrontEndTransportProcess(transportConfiguration) && connector.DNSRoutingEnabled && deliveryType == DeliveryType.Undefined;
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x000FBFFA File Offset: 0x000FA1FA
		private static bool IsDnsConnectorDelivery(DeliveryType deliveryType, SmtpSendConnectorConfig connector, ITransportConfiguration transportConfiguration)
		{
			return SmtpOutConnection.IsDnsConnectorDeliveryOnHub(deliveryType, transportConfiguration.ProcessTransportRole) || SmtpOutConnection.IsDnsConnectorDeliveryOnEdge(deliveryType, transportConfiguration.ProcessTransportRole) || SmtpOutConnection.IsDnsConnectorDeliveryOnFrontend(deliveryType, connector, transportConfiguration);
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000FC024 File Offset: 0x000FA224
		private void SetOutboundProxyContext(IEnumerable<INextHopServer> destinations, SmtpSendConnectorConfig sendConnector)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SetOutboundProxyContext);
			if (destinations == null)
			{
				throw new ArgumentNullException("destinations");
			}
			if (sendConnector == null)
			{
				throw new ArgumentNullException("sendConnector");
			}
			TlsSendConfiguration proxyTlsConfiguration = new TlsSendConfiguration(sendConnector, this.nextHopConnection.Key.TlsAuthLevel, this.nextHopConnection.Key.NextHopDomain, this.nextHopConnection.Key.NextHopTlsDomain);
			this.outboundProxyContext = new SmtpOutConnection.OutboundProxyContext(destinations, proxyTlsConfiguration, sendConnector, this.nextHopConnection.Key.RiskLevel, this.nextHopConnection.Key.OutboundIPPool);
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000FC0CC File Offset: 0x000FA2CC
		private bool TryUsingCachedSmtpOutSession(IPEndPoint endPoint)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.TryUsingCachedSmtpOutSession);
			if (this.nextHopConnection == null || this.nextHopConnection.Key.NextHopType == NextHopType.Heartbeat)
			{
				return false;
			}
			NextHopSolutionKey nextHopKey;
			IPEndPoint remoteEndPoint;
			if (this.NextHopIsOutboundProxy)
			{
				nextHopKey = SmtpOutSessionCache.OutboundFrontendCacheKey;
				remoteEndPoint = SmtpOutSessionCache.OutboundFrontendIPEndpointCacheKey;
			}
			else
			{
				nextHopKey = this.nextHopConnection.Key;
				remoteEndPoint = endPoint;
			}
			SmtpOutSession smtpOutSession;
			string logMessage;
			if (!SmtpOutConnectionHandler.SessionCache.TryGetValue(nextHopKey, remoteEndPoint, out smtpOutSession, out logMessage))
			{
				return false;
			}
			smtpOutSession.ResetSession(this, this.nextHopConnection);
			ConnectionLog.SmtpConnectionStopDueToCacheHit(this.sessionId, smtpOutSession.SessionId, this.nextHopConnection.Key.NextHopDomain);
			int[] activeCountsPerPriority;
			int[] retryCountsPerPriority;
			this.nextHopConnection.GetQueueCountsOnlyForIndividualPriorities(out activeCountsPerPriority, out retryCountsPerPriority);
			ConnectionLog.SmtpConnectionStartCacheHit(smtpOutSession.SessionId, this.sessionId, this.nextHopConnection.Key, this.nextHopConnection.ActiveQueueLength, activeCountsPerPriority, retryCountsPerPriority, logMessage);
			if (!SmtpOutConnectionHandler.ReplaceConnectionID(this.sessionId, smtpOutSession.SessionId))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Already shutting down");
				smtpOutSession.ShutdownConnection();
				this.RemoveConnection();
				return false;
			}
			this.sessionId = smtpOutSession.SessionId;
			this.smtpOutSession = smtpOutSession;
			this.smtpOutTargetHostPicker.UpdateSessionId(this.sessionId);
			this.nextHopConnection.ConnectionAttemptSucceeded();
			this.smtpOutSession.PrepareForNextMessageOnCachedSession();
			return true;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000FC228 File Offset: 0x000FA428
		private void ConnectToNextHost(bool retryWithoutStartTls, bool nextHopWasProxyingBlindly)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.ConnectToNextHost);
			if (this.connectionSucceededToNextHop)
			{
				this.smtpOutTargetHostPicker.ConnectionDisconnected();
				this.connectionSucceededToNextHop = false;
			}
			for (;;)
			{
				SmtpOutTargetHostPicker.SmtpTarget smtpTarget = null;
				if (nextHopWasProxyingBlindly)
				{
					if (this.outboundProxyContext.ProxyDestinations != null && this.outboundProxyContext.ProxyDestinations.GetEnumerator().MoveNext())
					{
						smtpTarget = this.smtpOutTargetHostPicker.CurrentSmtpTarget;
					}
				}
				else if (this.smtpOutTargetHostPicker.CurrentSmtpTarget != null && this.connectionsAttemptedToCurrentTarget < this.perHostConnectionAttemptCount)
				{
					smtpTarget = this.smtpOutTargetHostPicker.CurrentSmtpTarget;
				}
				else
				{
					this.connectionsAttemptedToCurrentTarget = 0;
					smtpTarget = this.smtpOutTargetHostPicker.GetNextTargetToConnect();
				}
				if (smtpTarget == null || (this.fixedTotalConnectionAttemptCount > 0 && this.totalConnectionsAttempted >= this.fixedTotalConnectionAttemptCount))
				{
					if (this.totalConnectionsAttempted < this.fixedTotalConnectionAttemptCount)
					{
						this.smtpOutTargetHostPicker.StartOverForRetry();
					}
					else
					{
						this.HandleConnectionToAllTargetsFailed(retryWithoutStartTls);
						this.connectionsAttemptedToCurrentTarget = 0;
						if (!retryWithoutStartTls)
						{
							break;
						}
					}
				}
				else
				{
					this.totalConnectionsAttempted++;
					this.connectionsAttemptedToCurrentTarget++;
					IPEndPoint ipendPoint = new IPEndPoint(smtpTarget.Address, (int)smtpTarget.Port);
					ExTraceGlobals.SmtpSendTracer.TraceDebug<string, IPEndPoint>((long)this.GetHashCode(), "Initiating connection to remote domain {0} to {1}", smtpTarget.TargetHostName, ipendPoint);
					this.currentTargetEndpoint = ipendPoint;
					if (Components.ShuttingDown)
					{
						goto Block_11;
					}
					if (this.connectionAdded && this.smtpOutSession != null)
					{
						ulong num = (this.inSession == null) ? SessionId.GetNextSessionId() : this.inSession.SessionId;
						ConnectionLog.SmtpConnectionStop(this.sessionId, this.nextHopConnection.Key.NextHopDomain, "Attempting next target", this.messagesSent, this.bytesSent, this.discardIdsReceived);
						if (!SmtpOutConnectionHandler.ReplaceConnectionID(this.sessionId, num))
						{
							goto Block_15;
						}
						ConnectionLog.SmtpConnectionFailover(num, this.sessionId, this.nextHopConnection.Key.NextHopDomain, this.lastFailoverReason);
						this.sessionId = num;
						int[] activeCountsPerPriority;
						int[] retryCountsPerPriority;
						this.nextHopConnection.GetQueueCountsOnlyForIndividualPriorities(out activeCountsPerPriority, out retryCountsPerPriority);
						ConnectionLog.SmtpConnectionStart(this.sessionId, this.nextHopConnection.Key, this.nextHopConnection.TotalQueueLength, activeCountsPerPriority, retryCountsPerPriority, null);
						this.smtpOutTargetHostPicker.UpdateSessionId(num);
					}
					if (!this.IsBlindProxy && this.TryUsingCachedSmtpOutSession(ipendPoint))
					{
						return;
					}
					if (!this.TryInitializeSmtpOutSession(ipendPoint))
					{
						return;
					}
					if (this.smtpOutTargetHostPicker.TryMarkCurrentSmtpTargetInConnectingState() && this.TryBeginConnectToNextHop(ipendPoint))
					{
						return;
					}
					if (this.failoverResponse.Equals(SmtpResponse.Empty))
					{
						this.failoverResponse = SmtpResponse.UnableToConnect;
						this.failoverReason = SessionSetupFailureReason.ConnectionFailure;
					}
					this.lastFailoverReason = SessionSetupFailureReason.ConnectionFailure;
					this.UpdateSmtpSendFailurePerfCounter(SessionSetupFailureReason.ConnectionFailure);
					this.nextHopConnection.NotifyConnectionFailedOver(this.smtpOutTargetHostPicker.SmtpHostName, SmtpResponse.Empty, SessionSetupFailureReason.ConnectionFailure);
				}
			}
			return;
			Block_11:
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Already shutting down");
			this.AckConnection(AckStatus.Retry, SmtpResponse.ServiceUnavailable, null, "Transport Service Shutting down", SessionSetupFailureReason.Shutdown);
			this.RemoveConnection();
			return;
			Block_15:
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Already shutting down");
			this.RemoveConnection();
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000FC524 File Offset: 0x000FA724
		private void HandleConnectionToAllTargetsFailed(bool retryWithoutStartTls)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.HandleConnectionToAllTargetsFailed);
			if (retryWithoutStartTls)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "STARTTLS negotiation failed. No more hosts to connect to. Retrying without STARTTLS");
				this.smtpOutTargetHostPicker.StartOverForRetry();
				this.totalConnectionsAttempted = 0;
				this.TlsConfig.ShouldSkipTls = true;
				return;
			}
			string reason = null;
			if (this.failoverResponse.StatusText != null)
			{
				reason = string.Concat(this.failoverResponse.StatusText);
			}
			if (this.nextHopConnection.Key.IsLocalDeliveryGroupRelay)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "No more hosts connect to for high availability routing, acking connection as resubmit");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("Primary target IP address responded with: \"{0}.\" Attempted failover to alternate host, but that did not succeed. Either there are no alternate hosts, or delivery failed to all alternate hosts. Queue will be resubmitted for routing for MAPI delivery.", this.failoverResponse);
				if (this.currentTargetEndpoint != null)
				{
					stringBuilder.AppendFormat(" The last endpoint attempted was {0}:{1}", this.currentTargetEndpoint.Address, this.currentTargetEndpoint.Port.ToString());
				}
				this.AckConnectionForResubmitWithoutHighAvailability(new SmtpResponse("451", "4.4.0", new string[]
				{
					stringBuilder.ToString()
				}), reason);
			}
			else
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "No more hosts connect to, acking connection as retry");
				StringBuilder stringBuilder2 = new StringBuilder();
				string format;
				if (this.failoverReason == SessionSetupFailureReason.ProtocolError)
				{
					if (this.NextHopIsOutboundProxy)
					{
						format = "Primary outbound frontend IP address responded with:   \"{0}.\"   Attempted failover to alternate frontend address, but that did not succeed. Either there are no alternate frontend addresses, or delivery failed to all alternate frontend addresses.";
					}
					else
					{
						format = "Primary target IP address responded with: \"{0}.\" Attempted failover to alternate host, but that did not succeed. Either there are no alternate hosts, or delivery failed to all alternate hosts.";
					}
				}
				else if (this.NextHopIsOutboundProxy)
				{
					format = "Error encountered while communicating with primary outbound frontend IP address:   \"{0}.\"   Attempted failover to alternate frontend, but that did not succeed. Either there are no alternate frontend addresses, or delivery failed to all alternate frontend addresses.";
				}
				else
				{
					format = "Error encountered while communicating with primary target IP address: \"{0}.\" Attempted failover to alternate host, but that did not succeed. Either there are no alternate hosts, or delivery failed to all alternate hosts.";
				}
				string statusCode;
				string enhancedStatusCode;
				if (this.failoverReason == SessionSetupFailureReason.SocketError)
				{
					statusCode = SmtpResponse.SocketError.StatusCode;
					enhancedStatusCode = SmtpResponse.SocketError.EnhancedStatusCode;
					stringBuilder2.AppendFormat(format, (this.socketErrorDetails != null) ? this.socketErrorDetails : this.failoverResponse.ToString());
				}
				else
				{
					statusCode = SmtpResponse.ConnectionFailover.StatusCode;
					enhancedStatusCode = SmtpResponse.ConnectionFailover.EnhancedStatusCode;
					stringBuilder2.AppendFormat(format, this.failoverResponse.ToString());
				}
				if (this.currentTargetEndpoint != null)
				{
					stringBuilder2.AppendFormat(" The last endpoint attempted was {0}:{1}", this.currentTargetEndpoint.Address, this.currentTargetEndpoint.Port.ToString());
				}
				SmtpResponse smtpResponse = new SmtpResponse(statusCode, enhancedStatusCode, new string[]
				{
					stringBuilder2.ToString()
				});
				SessionSetupFailureReason failureReason = (this.lastFailoverReason == SessionSetupFailureReason.None) ? SessionSetupFailureReason.ConnectionFailure : this.lastFailoverReason;
				AckDetails details;
				if (!this.NextHopIsOutboundProxy)
				{
					details = ((this.smtpOutSession.AckDetails != null) ? this.smtpOutSession.AckDetails : new AckDetails((this.currentTargetEndpoint != null) ? this.currentTargetEndpoint : null, this.nextHopConnection.Key.NextHopDomain, this.sessionId.ToString(), string.Empty, (this.smtpOutSession.LocalEndPoint != null) ? this.smtpOutSession.LocalEndPoint.Address : null));
				}
				else
				{
					bool flag = SmtpOutConnection.IsProxySessionSetupProtocolFailure(this.failoverReason, this.failoverResponse);
					details = new AckDetails(flag ? null : this.currentTargetEndpoint, flag ? this.nextHopConnection.Key.NextHopDomain : ((this.smtpOutSession.AckDetails != null) ? this.smtpOutSession.AckDetails.RemoteHostName : null), this.sessionId.ToString(), string.Empty, null);
				}
				this.AckConnection(AckStatus.Retry, smtpResponse, details, reason, failureReason, false);
			}
			this.RemoveConnection();
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000FC890 File Offset: 0x000FAA90
		private bool TryInitializeSmtpOutSession(IPEndPoint endPoint)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.TryInitializeSmtpOutSession);
			bool result;
			try
			{
				bool flag = false;
				SmtpResponse smtpResponse = SmtpResponse.Empty;
				if (this.TlsConfig.TlsDomains == null || this.TlsConfig.TlsDomains.Count == 0)
				{
					if (this.TlsConfig.TlsAuthLevel != null && this.TlsConfig.TlsAuthLevel.Value.Equals(RequiredTlsAuthLevel.DomainValidation))
					{
						flag = true;
						smtpResponse = SmtpResponse.TlsDomainRequired;
					}
				}
				else if (this.TlsConfig.TlsAuthLevel == null || !this.TlsConfig.TlsAuthLevel.Value.Equals(RequiredTlsAuthLevel.DomainValidation))
				{
					flag = true;
					smtpResponse = SmtpResponse.IncorrectTlsAuthLevel;
				}
				if (flag)
				{
					this.UpdateSmtpSendFailurePerfCounter(SessionSetupFailureReason.ProtocolError);
					this.nextHopConnection.AckConnection(AckStatus.Retry, smtpResponse, null, SessionSetupFailureReason.ProtocolError);
					this.RemoveConnection();
					result = false;
				}
				else
				{
					if (ConfigurationComponent.IsFrontEndTransportProcess(this.transportConfiguration))
					{
						if (this.IsBlindProxy)
						{
							this.smtpOutSession = new SmtpOutProxySession(this.inSession, this, this.sessionId, endPoint, this.protocolLog, this.Connector.ProtocolLoggingLevel, this.certificateCache, this.certificateValidator, this.transportConfiguration, this.transportAppConfig, this.connectionContextString);
						}
						else
						{
							this.smtpOutSession = new InboundProxySmtpOutSession(this.sessionId, this, this.nextHopConnection, endPoint, this.protocolLog, this.Connector.ProtocolLoggingLevel, this.mailRouter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, ((InboundProxyNextHopConnection)this.nextHopConnection).ProxyLayer);
						}
					}
					else if (this.isShadowOut)
					{
						this.smtpOutSession = new ShadowSmtpOutSession(this.sessionId, this, this.nextHopConnection, endPoint, this.protocolLog, this.Connector.ProtocolLoggingLevel, this.mailRouter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, ((ShadowPeerNextHopConnection)this.nextHopConnection).ProxyLayer);
					}
					else if (this.IsBlindProxy)
					{
						this.smtpOutSession = new SmtpOutProxySession(this.inSession, this, this.sessionId, endPoint, this.protocolLog, this.Connector.ProtocolLoggingLevel, this.certificateCache, this.certificateValidator, this.transportConfiguration, this.transportAppConfig, this.connectionContextString);
					}
					else
					{
						ProtocolLoggingLevel loggingLevel = (this.outboundProxyContext == null) ? this.Connector.ProtocolLoggingLevel : this.outboundProxyContext.ProxySendConnector.ProtocolLoggingLevel;
						this.smtpOutSession = new SmtpOutSession(this.sessionId, this, this.nextHopConnection, endPoint, this.protocolLog, loggingLevel, this.mailRouter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, this.smtpInComponent.TargetRunningState == ServiceState.Inactive);
					}
					result = true;
				}
			}
			catch (TlsCertificateNameNotFoundException arg)
			{
				ConnectionLog.SmtpConnectionAborted(this.sessionId, this.nextHopConnection.Key.NextHopDomain, endPoint.Address);
				SmtpResponse smtpResponse2 = new SmtpResponse("454", "4.7.5", new string[]
				{
					"The certificate specified in TlsCertificateName of the SendConnector could not be found."
				});
				this.nextHopConnection.AckConnection(AckStatus.Retry, smtpResponse2, null);
				this.RemoveConnection();
				ExTraceGlobals.SmtpSendTracer.TraceError<TlsCertificateNameNotFoundException>((long)this.GetHashCode(), "TlsCertificateNameException occurred while trying to handle a new SMTP outbound connection. Exception test: {0}", arg);
				result = false;
			}
			catch (LocalizedException arg2)
			{
				ConnectionLog.SmtpConnectionAborted(this.sessionId, this.nextHopConnection.Key.NextHopDomain, endPoint.Address);
				this.nextHopConnection.AckConnection(AckStatus.Retry, SmtpResponse.UnexpectedExceptionHandlingNewOutboundConnection, null);
				this.RemoveConnection();
				ExTraceGlobals.SmtpSendTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "Unexpected exception occurred when trying to handle a new SMTP outbound connection. Exception text: {0}", arg2);
				result = false;
			}
			return result;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000FCC90 File Offset: 0x000FAE90
		private bool TryBeginConnectToNextHop(IPEndPoint endPoint)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.TryBeginConnectToNextHop);
			bool result;
			try
			{
				this.socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				if (!Components.IsBridgehead && endPoint.AddressFamily == this.sendConnector.SourceIPAddress.AddressFamily)
				{
					try
					{
						this.socket.Bind(new IPEndPoint(this.sendConnector.SourceIPAddress, 0));
					}
					catch (SocketException ex)
					{
						if (ex.SocketErrorCode != SocketError.AddressNotAvailable)
						{
							throw;
						}
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SendConnectorInvalidSourceIPAddress, null, new object[]
						{
							this.sendConnector.SourceIPAddress,
							this.sendConnector.Name
						});
						string notificationReason = string.Format("Failed to bind to SourceIPAddress '{0}' configured on send connector '{1}'.", this.sendConnector.SourceIPAddress, this.sendConnector.Name);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Warning, false);
					}
				}
				this.socket.BeginConnect(endPoint, SmtpOutConnection.onConnectComplete, this);
				result = true;
			}
			catch (SocketException socketException)
			{
				this.smtpOutTargetHostPicker.HandleSocketError(socketException);
				this.socket.Close();
				this.socket = null;
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendConnectionError, null, new object[]
				{
					this.Connector.Name,
					endPoint
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000FCE08 File Offset: 0x000FB008
		private void ConnectComplete(IAsyncResult asyncResult)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.ConnectComplete);
			try
			{
				this.socket.EndConnect(asyncResult);
				NetworkConnection networkConnection = new NetworkConnection(this.socket, 4096)
				{
					ClientTlsProtocols = SchannelProtocols.Zero
				};
				this.socket = null;
				ExTraceGlobals.SmtpSendTracer.TraceDebug<long, IPEndPoint>((long)this.GetHashCode(), "Connection Completed. Connection ID : {0}, Remote End Point {1}", networkConnection.ConnectionId, networkConnection.RemoteEndPoint);
				if (SmtpOutConnectionHandler.UpdateConnection(this.sessionId, networkConnection))
				{
					ConnectionLog.SmtpConnected(this.sessionId, this.nextHopConnection.Key.NextHopDomain, this.smtpOutTargetHostPicker.CurrentIpAddress);
					this.smtpOutTargetHostPicker.ConnectionSucceeded();
					this.connectionSucceededToNextHop = true;
					this.smtpOutSession.StartUsingConnection();
				}
				else
				{
					networkConnection.Dispose();
					this.smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Server shutdown");
					ConnectionLog.SmtpConnectionAborted(this.sessionId, this.nextHopConnection.Key.NextHopDomain, this.smtpOutTargetHostPicker.CurrentIpAddress);
					this.RemoveConnection();
				}
			}
			catch (SocketException ex)
			{
				this.smtpOutTargetHostPicker.HandleSocketError(ex);
				ExTraceGlobals.SmtpSendTracer.TraceError<SocketException>((long)this.GetHashCode(), "Failed to connect, SocketException:{0}", ex);
				this.socket.Close();
				this.socket = null;
				this.socketErrorDetails = string.Format(CultureInfo.InvariantCulture, "Failed to connect. Winsock error code: {0}, Win32 error code: {1}", new object[]
				{
					ex.ErrorCode,
					ex.NativeErrorCode
				});
				string context = string.Format(CultureInfo.InvariantCulture, "{0}, Error Message: {1}", new object[]
				{
					this.socketErrorDetails,
					ex.Message
				});
				this.smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, context);
				this.smtpOutSession.FailoverConnection(SmtpResponse.UnableToConnect, SessionSetupFailureReason.SocketError);
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x000FCFEC File Offset: 0x000FB1EC
		private void AckConnectionWithDNSError(AckStatus ackStatus, SmtpResponse response, string eventText)
		{
			this.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.AckConnectionWithDnsError);
			switch (ackStatus)
			{
			case AckStatus.Retry:
			case AckStatus.Fail:
				break;
			default:
				if (ackStatus != AckStatus.Resubmit)
				{
					throw new ArgumentException("AckConnectionWithDNSError should only be used for retry or failure.", "ackStatus");
				}
				break;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus, string>((long)this.GetHashCode(), "Acking Connection due to DNS error. Status -> {0} : {1}", ackStatus, eventText);
			SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendDnsConnectionFailure, null, new object[]
			{
				this.nextHopConnection.Key.NextHopConnector.ToString(),
				eventText
			});
			ConnectionLog.SmtpConnectionStop(this.sessionId, this.nextHopConnection.Key.NextHopDomain, eventText, 0UL, 0UL, 0UL);
			this.UpdateSmtpSendFailurePerfCounter(SessionSetupFailureReason.DnsLookupFailure);
			this.nextHopConnection.AckConnection(ackStatus, response, null, SessionSetupFailureReason.DnsLookupFailure);
			this.RemoveConnection();
			this.nextHopConnection = null;
		}

		// Token: 0x04001E5F RID: 7775
		public const ushort MailboxTransportDeliveryTargetPort = 475;

		// Token: 0x04001E60 RID: 7776
		public const ushort ColocatedFrontEndAndHubTargetPort = 2525;

		// Token: 0x04001E61 RID: 7777
		private const string ProtocolFailureResponseFormat = "Primary target IP address responded with: \"{0}.\" Attempted failover to alternate host, but that did not succeed. Either there are no alternate hosts, or delivery failed to all alternate hosts.";

		// Token: 0x04001E62 RID: 7778
		private const string FrontendProtocolFailureResponseFormat = "Primary outbound frontend IP address responded with:   \"{0}.\"   Attempted failover to alternate frontend address, but that did not succeed. Either there are no alternate frontend addresses, or delivery failed to all alternate frontend addresses.";

		// Token: 0x04001E63 RID: 7779
		private const string NonProtocolFailureResponseFormat = "Error encountered while communicating with primary target IP address: \"{0}.\" Attempted failover to alternate host, but that did not succeed. Either there are no alternate hosts, or delivery failed to all alternate hosts.";

		// Token: 0x04001E64 RID: 7780
		private const string FrontendNonProtocolFailureResponseFormat = "Error encountered while communicating with primary outbound frontend IP address:   \"{0}.\"   Attempted failover to alternate frontend, but that did not succeed. Either there are no alternate frontend addresses, or delivery failed to all alternate frontend addresses.";

		// Token: 0x04001E65 RID: 7781
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x04001E66 RID: 7782
		public static ExEventLog Events = new ExEventLog(ExTraceGlobals.SmtpSendTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001E67 RID: 7783
		public readonly bool ClientProxy;

		// Token: 0x04001E68 RID: 7784
		private static readonly AsyncCallback onConnectComplete = new AsyncCallback(SmtpOutConnection.OnConnectComplete);

		// Token: 0x04001E69 RID: 7785
		private readonly int fixedTotalConnectionAttemptCount;

		// Token: 0x04001E6A RID: 7786
		private readonly int perHostConnectionAttemptCount;

		// Token: 0x04001E6B RID: 7787
		private readonly ISmtpInSession inSession;

		// Token: 0x04001E6C RID: 7788
		private readonly bool isShadowOut;

		// Token: 0x04001E6D RID: 7789
		private readonly string connectionContextString;

		// Token: 0x04001E6E RID: 7790
		private Breadcrumbs<SmtpOutConnection.SmtpOutConnectionBreadcrumbs> breadcrumbs = new Breadcrumbs<SmtpOutConnection.SmtpOutConnectionBreadcrumbs>(64);

		// Token: 0x04001E6F RID: 7791
		private SmtpOutTargetHostPicker smtpOutTargetHostPicker;

		// Token: 0x04001E70 RID: 7792
		private bool connectionSucceededToNextHop;

		// Token: 0x04001E71 RID: 7793
		private NextHopConnection nextHopConnection;

		// Token: 0x04001E72 RID: 7794
		private SmtpResponse failoverResponse;

		// Token: 0x04001E73 RID: 7795
		private SessionSetupFailureReason failoverReason;

		// Token: 0x04001E74 RID: 7796
		private ProtocolLog protocolLog;

		// Token: 0x04001E75 RID: 7797
		private SmtpSendConnectorConfig sendConnector;

		// Token: 0x04001E76 RID: 7798
		private ISmtpOutSession smtpOutSession;

		// Token: 0x04001E77 RID: 7799
		private Socket socket;

		// Token: 0x04001E78 RID: 7800
		private ulong bytesSent;

		// Token: 0x04001E79 RID: 7801
		private ulong messagesSent;

		// Token: 0x04001E7A RID: 7802
		private ulong discardIdsReceived;

		// Token: 0x04001E7B RID: 7803
		private ulong sessionId = SessionId.GetNextSessionId();

		// Token: 0x04001E7C RID: 7804
		private SmtpSendPerfCountersInstance smtpSendPerformanceCounters;

		// Token: 0x04001E7D RID: 7805
		private int totalConnectionsAttempted;

		// Token: 0x04001E7E RID: 7806
		private int connectionsAttemptedToCurrentTarget;

		// Token: 0x04001E7F RID: 7807
		private bool connectionAdded;

		// Token: 0x04001E80 RID: 7808
		private IMailRouter mailRouter;

		// Token: 0x04001E81 RID: 7809
		private CertificateCache certificateCache;

		// Token: 0x04001E82 RID: 7810
		private CertificateValidator certificateValidator;

		// Token: 0x04001E83 RID: 7811
		private ShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001E84 RID: 7812
		private TransportAppConfig transportAppConfig;

		// Token: 0x04001E85 RID: 7813
		private ITransportConfiguration transportConfiguration;

		// Token: 0x04001E86 RID: 7814
		private ISmtpInComponent smtpInComponent;

		// Token: 0x04001E87 RID: 7815
		private SmtpOutConnection.OutboundProxyContext outboundProxyContext;

		// Token: 0x04001E88 RID: 7816
		private IPEndPoint currentTargetEndpoint;

		// Token: 0x04001E89 RID: 7817
		private bool createSmtpSendPerfCounters = true;

		// Token: 0x04001E8A RID: 7818
		private SessionSetupFailureReason lastFailoverReason;

		// Token: 0x04001E8B RID: 7819
		private readonly UnhealthyTargetFilterComponent unhealthyTargetFilter;

		// Token: 0x04001E8C RID: 7820
		private string socketErrorDetails;

		// Token: 0x0200050F RID: 1295
		public enum SmtpOutConnectionBreadcrumbs
		{
			// Token: 0x04001E91 RID: 7825
			EMPTY,
			// Token: 0x04001E92 RID: 7826
			AckConnection,
			// Token: 0x04001E93 RID: 7827
			AckConnectionForResubmitWithoutHighAvailability,
			// Token: 0x04001E94 RID: 7828
			AckConnectionWithDnsError,
			// Token: 0x04001E95 RID: 7829
			AddConnection,
			// Token: 0x04001E96 RID: 7830
			Connect,
			// Token: 0x04001E97 RID: 7831
			ConnectComplete,
			// Token: 0x04001E98 RID: 7832
			ConnectToBlindProxyDestinations,
			// Token: 0x04001E99 RID: 7833
			ConnectToNextHost,
			// Token: 0x04001E9A RID: 7834
			ConnectToPerMessageProxyDestinations,
			// Token: 0x04001E9B RID: 7835
			ConnectToShadowDestinations,
			// Token: 0x04001E9C RID: 7836
			FailoverConnection,
			// Token: 0x04001E9D RID: 7837
			GetOutboundProxyDestinationSettings,
			// Token: 0x04001E9E RID: 7838
			HandleConnectionToAllTargetsFailed,
			// Token: 0x04001E9F RID: 7839
			NextHopResolutionFailed,
			// Token: 0x04001EA0 RID: 7840
			RemoveConnection,
			// Token: 0x04001EA1 RID: 7841
			RoutingTableUpdate,
			// Token: 0x04001EA2 RID: 7842
			SetOutboundProxyContext,
			// Token: 0x04001EA3 RID: 7843
			SetSendConnector,
			// Token: 0x04001EA4 RID: 7844
			Shutdown,
			// Token: 0x04001EA5 RID: 7845
			TryBeginConnectToNextHop,
			// Token: 0x04001EA6 RID: 7846
			TryGetRemainingSmtpTargets,
			// Token: 0x04001EA7 RID: 7847
			TryInitializeSmtpOutSession,
			// Token: 0x04001EA8 RID: 7848
			TryUsingCachedSmtpOutSession,
			// Token: 0x04001EA9 RID: 7849
			UpdateOnSuccessfulOutboundProxySetup,
			// Token: 0x04001EAA RID: 7850
			SthpConnectAfterDns,
			// Token: 0x04001EAB RID: 7851
			SthpConnectionDisconnected,
			// Token: 0x04001EAC RID: 7852
			SthpConnectionSucceeded,
			// Token: 0x04001EAD RID: 7853
			SthpGetNextTargetToConnect,
			// Token: 0x04001EAE RID: 7854
			SthpHandleSocketError,
			// Token: 0x04001EAF RID: 7855
			SthpResolveProxyNextHopAndConnect,
			// Token: 0x04001EB0 RID: 7856
			SthpResolveToNextHopAndConnect,
			// Token: 0x04001EB1 RID: 7857
			SthpStartOverForRetry,
			// Token: 0x04001EB2 RID: 7858
			SthpTryMarkCurrentSmtpTargetInConnectingState,
			// Token: 0x04001EB3 RID: 7859
			SthpUpdateSessionId
		}

		// Token: 0x02000510 RID: 1296
		private class OutboundProxyContext
		{
			// Token: 0x06003CB7 RID: 15543 RVA: 0x000FD0F4 File Offset: 0x000FB2F4
			public OutboundProxyContext(IEnumerable<INextHopServer> proxyDestinations, TlsSendConfiguration proxyTlsConfiguration, SmtpSendConnectorConfig proxySendConnector, RiskLevel proxyRiskLevel, int proxyOutboundIPPool)
			{
				if (proxyDestinations == null)
				{
					throw new ArgumentNullException("proxyDestinations");
				}
				if (proxyTlsConfiguration == null)
				{
					throw new ArgumentNullException("proxyTlsConfiguration");
				}
				if (proxySendConnector == null)
				{
					throw new ArgumentNullException("proxySendConnector");
				}
				this.ProxyDestinations = proxyDestinations;
				this.ProxyTlsConfiguration = proxyTlsConfiguration;
				this.proxySendConnector = proxySendConnector;
				this.proxyRiskLevel = proxyRiskLevel;
				this.proxyOutboundIPPool = proxyOutboundIPPool;
			}

			// Token: 0x1700129A RID: 4762
			// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000FD156 File Offset: 0x000FB356
			public SmtpSendConnectorConfig ProxySendConnector
			{
				get
				{
					return this.proxySendConnector;
				}
			}

			// Token: 0x1700129B RID: 4763
			// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000FD15E File Offset: 0x000FB35E
			public RiskLevel ProxyRiskLevel
			{
				get
				{
					return this.proxyRiskLevel;
				}
			}

			// Token: 0x1700129C RID: 4764
			// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000FD166 File Offset: 0x000FB366
			public int OutboundIPPool
			{
				get
				{
					return this.proxyOutboundIPPool;
				}
			}

			// Token: 0x04001EB4 RID: 7860
			public IEnumerable<INextHopServer> ProxyDestinations;

			// Token: 0x04001EB5 RID: 7861
			public TlsSendConfiguration ProxyTlsConfiguration;

			// Token: 0x04001EB6 RID: 7862
			private readonly SmtpSendConnectorConfig proxySendConnector;

			// Token: 0x04001EB7 RID: 7863
			private readonly RiskLevel proxyRiskLevel;

			// Token: 0x04001EB8 RID: 7864
			private readonly int proxyOutboundIPPool;
		}
	}
}
