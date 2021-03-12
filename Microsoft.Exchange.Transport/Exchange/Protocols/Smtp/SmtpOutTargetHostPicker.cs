using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000511 RID: 1297
	internal sealed class SmtpOutTargetHostPicker
	{
		// Token: 0x06003CBB RID: 15547 RVA: 0x000FD170 File Offset: 0x000FB370
		internal SmtpOutTargetHostPicker(ulong sessionId, SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection, EnhancedDns enhancedDns, UnhealthyTargetFilter<IPAddressPortPair> unhealthyTargetIpAddressFilter, UnhealthyTargetFilter<FqdnPortPair> unhealthyTargetFqdnFilter)
		{
			this.sessionId = sessionId;
			this.smtpOutConnection = smtpOutConnection;
			this.nextHopConnection = nextHopConnection;
			this.enhancedDns = enhancedDns;
			this.unhealthyTargetIpAddressFilter = unhealthyTargetIpAddressFilter;
			this.unhealthyTargetFqdnFilter = unhealthyTargetFqdnFilter;
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000FD1C2 File Offset: 0x000FB3C2
		internal string SmtpHostName
		{
			get
			{
				return this.currentSmtpTarget.TargetHostName ?? string.Empty;
			}
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x000FD1D8 File Offset: 0x000FB3D8
		internal string SmtpHost
		{
			get
			{
				if (string.IsNullOrEmpty(this.SmtpHostName))
				{
					return this.currentSmtpTarget.Address.ToString();
				}
				return this.SmtpHostName;
			}
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000FD1FE File Offset: 0x000FB3FE
		internal IPAddress CurrentIpAddress
		{
			get
			{
				return this.currentSmtpTarget.Address;
			}
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000FD20B File Offset: 0x000FB40B
		internal SmtpOutTargetHostPicker.SmtpTarget CurrentSmtpTarget
		{
			get
			{
				return this.currentSmtpTarget;
			}
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000FD213 File Offset: 0x000FB413
		internal int TotalTargets
		{
			get
			{
				return this.totalTargets;
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000FD21B File Offset: 0x000FB41B
		private bool ShouldConnect
		{
			get
			{
				return this.smtpOutConnection.IsBlindProxy || this.nextHopConnection.GetNextMailItem() != null;
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000FD23D File Offset: 0x000FB43D
		internal void RoutingTableUpdate()
		{
			this.routingTableChange = true;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x000FD246 File Offset: 0x000FB446
		internal void UpdateSessionId(ulong sessionId)
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpUpdateSessionId);
			this.sessionId = sessionId;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000FD25C File Offset: 0x000FB45C
		internal void ResolveToNextHopAndConnect()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpResolveToNextHopAndConnect);
			this.routingTableChange = false;
			this.asyncResult = this.enhancedDns.BeginResolveToNextHop(this.nextHopConnection.Key, this.smtpOutConnection.RiskLevel, this.smtpOutConnection.OutboundIPPool, new AsyncCallback(this.ConnectAfterDNS), null);
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000FD2BC File Offset: 0x000FB4BC
		internal void ResolveProxyNextHopAndConnect(IEnumerable<INextHopServer> destinations, bool internalDestination, SmtpOutProxyType proxyType)
		{
			this.ResolveProxyNextHopAndConnect(destinations, internalDestination, null, proxyType);
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x000FD2C8 File Offset: 0x000FB4C8
		internal void ResolveProxyNextHopAndConnect(IEnumerable<INextHopServer> destinations, bool internalDestinations, SmtpSendConnectorConfig sendConnector)
		{
			this.ResolveProxyNextHopAndConnect(destinations, internalDestinations, sendConnector, SmtpOutProxyType.Blind);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000FD2D4 File Offset: 0x000FB4D4
		internal void StartOverForRetry()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpStartOverForRetry);
			if (Components.ShuttingDown)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Already shutting down");
			}
			lock (this.dnsUpdateLock)
			{
				this.currentTargetIndex = 0;
				this.currentIpAddressIndex = -1;
			}
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x000FD348 File Offset: 0x000FB548
		internal bool TryGetRemainingSmtpTargets(out IEnumerable<INextHopServer> remainingTargets)
		{
			List<INextHopServer> list = new List<INextHopServer>();
			remainingTargets = list;
			bool result;
			lock (this.dnsUpdateLock)
			{
				int num = this.currentIpAddressIndex;
				int num2 = this.currentTargetIndex;
				for (;;)
				{
					num++;
					if (num >= this.smtpTargetHosts[num2].IPAddresses.Count)
					{
						num2++;
						num = -1;
						if (num2 >= this.smtpTargetHosts.Length)
						{
							break;
						}
					}
					else
					{
						IPAddress ipaddress = this.smtpTargetHosts[num2].IPAddresses[num];
						if (Socket.OSSupportsIPv6 || ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
						{
							list.Add(new SmtpOutTargetHostPicker.SmtpOutNextHopServer(ipaddress));
						}
					}
				}
				result = (list.Count != 0);
			}
			return result;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x000FD40C File Offset: 0x000FB60C
		internal SmtpOutTargetHostPicker.SmtpTarget GetNextTargetToConnect()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpGetNextTargetToConnect);
			IPAddress ipaddress;
			string name;
			ushort port;
			lock (this.dnsUpdateLock)
			{
				for (;;)
				{
					this.currentIpAddressIndex++;
					if (this.currentIpAddressIndex >= this.smtpTargetHosts[this.currentTargetIndex].IPAddresses.Count)
					{
						this.currentTargetIndex++;
						this.currentIpAddressIndex = -1;
						if (this.currentTargetIndex >= this.smtpTargetHosts.Length)
						{
							break;
						}
					}
					else
					{
						ipaddress = this.smtpTargetHosts[this.currentTargetIndex].IPAddresses[this.currentIpAddressIndex];
						if (Socket.OSSupportsIPv6 || ipaddress.AddressFamily != AddressFamily.InterNetworkV6)
						{
							goto IL_A8;
						}
					}
				}
				return null;
				IL_A8:
				name = this.smtpTargetHosts[this.currentTargetIndex].Name;
				port = this.smtpTargetHosts[this.currentTargetIndex].Port;
			}
			this.currentSmtpTarget = new SmtpOutTargetHostPicker.SmtpTarget(ipaddress, name, port);
			return this.currentSmtpTarget;
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x000FD51C File Offset: 0x000FB71C
		internal bool TryMarkCurrentSmtpTargetInConnectingState()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpTryMarkCurrentSmtpTargetInConnectingState);
			string smtpHost = this.SmtpHost;
			IPAddress currentIpAddress = this.CurrentIpAddress;
			IPAddressPortPair key = new IPAddressPortPair(currentIpAddress, this.CurrentSmtpTarget.Port);
			bool flag = this.unhealthyTargetIpAddressFilter.TryMarkTargetInConnectingState(key);
			bool flag2 = flag && this.unhealthyTargetFqdnFilter.TryMarkTargetInConnectingState(new FqdnPortPair(smtpHost, this.CurrentSmtpTarget.Port));
			if (flag && !flag2)
			{
				this.unhealthyTargetIpAddressFilter.UnMarkTargetInConnectingState(key);
			}
			return flag && flag2;
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x000FD5A4 File Offset: 0x000FB7A4
		internal void ConnectionSucceeded()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpConnectionSucceeded);
			string smtpHost = this.SmtpHost;
			IPAddress currentIpAddress = this.CurrentIpAddress;
			this.unhealthyTargetFqdnFilter.IncrementConnectionCountToTarget(new FqdnPortPair(smtpHost, this.CurrentSmtpTarget.Port));
			this.unhealthyTargetIpAddressFilter.IncrementConnectionCountToTarget(new IPAddressPortPair(currentIpAddress, this.CurrentSmtpTarget.Port));
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x000FD604 File Offset: 0x000FB804
		internal void ConnectionDisconnected()
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpConnectionDisconnected);
			string smtpHost = this.SmtpHost;
			IPAddress currentIpAddress = this.CurrentIpAddress;
			this.unhealthyTargetFqdnFilter.DecrementConnectionCountToTarget(new FqdnPortPair(smtpHost, this.CurrentSmtpTarget.Port));
			this.unhealthyTargetIpAddressFilter.DecrementConnectionCountToTarget(new IPAddressPortPair(currentIpAddress, this.CurrentSmtpTarget.Port));
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x000FD664 File Offset: 0x000FB864
		internal void HandleSocketError(SocketException socketException)
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpHandleSocketError);
			string smtpHost = this.SmtpHost;
			IPAddress currentIpAddress = this.CurrentIpAddress;
			int errorCode = socketException.ErrorCode;
			if (this.unhealthyTargetFqdnFilter.Enabled || this.unhealthyTargetIpAddressFilter.Enabled)
			{
				bool targetHostMarkedUnhealthy = false;
				int minValue = int.MinValue;
				int minValue2 = int.MinValue;
				ExDateTime minValue3 = ExDateTime.MinValue;
				ExDateTime ipAddressNextRetryTime;
				int currentIpAddressConnectionCount;
				int currentIpAddressFailureCount;
				bool flag = this.unhealthyTargetIpAddressFilter.TryMarkTargetUnhealthyIfNoConnectionOpen(new IPAddressPortPair(currentIpAddress, this.CurrentSmtpTarget.Port), out ipAddressNextRetryTime, out currentIpAddressConnectionCount, out currentIpAddressFailureCount);
				if (flag && this.currentIpAddressIndex == this.smtpTargetHosts[this.currentTargetIndex].IPAddresses.Count - 1)
				{
					targetHostMarkedUnhealthy = this.unhealthyTargetFqdnFilter.TryMarkTargetUnhealthyIfNoConnectionOpen(new FqdnPortPair(smtpHost, this.CurrentSmtpTarget.Port), out minValue3, out minValue, out minValue2);
				}
				else
				{
					this.unhealthyTargetFqdnFilter.UnMarkTargetInConnectingState(new FqdnPortPair(smtpHost, this.CurrentSmtpTarget.Port));
				}
				ConnectionLog.SmtpConnectionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, currentIpAddress, smtpHost, this.CurrentSmtpTarget.Port, flag, ipAddressNextRetryTime, currentIpAddressConnectionCount, currentIpAddressFailureCount, targetHostMarkedUnhealthy, minValue3, minValue, minValue2, socketException);
				return;
			}
			ConnectionLog.SmtpConnectionFailed(this.sessionId, this.nextHopConnection.Key.NextHopDomain, currentIpAddress, this.CurrentSmtpTarget.Port, socketException);
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000FD7BC File Offset: 0x000FB9BC
		private void ResolveProxyNextHopAndConnect(IEnumerable<INextHopServer> destinations, bool internalDestination, SmtpSendConnectorConfig connector, SmtpOutProxyType proxyType)
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpResolveProxyNextHopAndConnect);
			this.routingTableChange = false;
			this.internalDestination = internalDestination;
			this.proxyDestinations = destinations;
			this.blindProxySendConnector = connector;
			this.proxyType = proxyType;
			this.asyncResult = this.enhancedDns.BeginResolveProxyNextHop(destinations, internalDestination, connector, proxyType, this.smtpOutConnection.RiskLevel, this.smtpOutConnection.OutboundIPPool, new AsyncCallback(this.ConnectAfterDNS), null);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000FD834 File Offset: 0x000FBA34
		private void ConnectAfterDNS(IAsyncResult ar)
		{
			this.smtpOutConnection.DropBreadcrumb(SmtpOutConnection.SmtpOutConnectionBreadcrumbs.SthpConnectAfterDns);
			EnhancedDnsTargetHost[] array;
			IEnumerable<INextHopServer> outboundProxyDestinations;
			SmtpSendConnectorConfig smtpSendConnectorConfig;
			SmtpSendConnectorConfig smtpSendConnectorConfig2;
			IPAddress reportingServer;
			string diagnosticInfo;
			DnsStatus dnsStatus = EnhancedDns.EndResolveToNextHop(ar, out array, out outboundProxyDestinations, out smtpSendConnectorConfig, out smtpSendConnectorConfig2, out reportingServer, out diagnosticInfo);
			this.asyncResult = null;
			if (!this.smtpOutConnection.AddConnection())
			{
				this.smtpOutConnection.AckConnection(AckStatus.Retry, SmtpResponse.Empty, null, "Connection not attempted because service is shutting down", SessionSetupFailureReason.Shutdown);
				return;
			}
			SmtpSendConnectorConfig smtpSendConnectorConfig3 = smtpSendConnectorConfig2 ?? smtpSendConnectorConfig;
			SmtpSendConnectorConfig smtpSendConnectorConfig4 = (smtpSendConnectorConfig2 != null) ? smtpSendConnectorConfig : null;
			if (smtpSendConnectorConfig3 != null)
			{
				this.smtpOutConnection.SetSendConnector(smtpSendConnectorConfig3, smtpSendConnectorConfig4, outboundProxyDestinations);
			}
			if (dnsStatus != DnsStatus.Success)
			{
				this.smtpOutConnection.NextHopResolutionFailed(dnsStatus, reportingServer, diagnosticInfo);
				return;
			}
			if (smtpSendConnectorConfig3 == null)
			{
				throw new InvalidOperationException("Successful resolution should return non-null connector");
			}
			if (array.Length == 0)
			{
				throw new InvalidOperationException("Successful resolution should return at least 1 target host");
			}
			lock (this.dnsUpdateLock)
			{
				this.smtpTargetHosts = array;
				this.currentTargetIndex = 0;
				this.currentIpAddressIndex = -1;
			}
			ConnectionLog.SmtpHostResolved(this.sessionId, this.nextHopConnection.Key.NextHopDomain, array, smtpSendConnectorConfig4 != null);
			if (!this.ShouldConnect)
			{
				this.smtpOutConnection.AckConnection(AckStatus.Pending, SmtpResponse.Empty, null, null, SessionSetupFailureReason.None);
				this.smtpOutConnection.RemoveConnection();
				return;
			}
			if (Components.ShuttingDown)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Already shutting down");
				this.smtpOutConnection.RemoveConnection();
				return;
			}
			if (this.routingTableChange)
			{
				if (Interlocked.Exchange(ref this.numberOfDnsQuery, 1) == 0)
				{
					if (this.proxyDestinations == null)
					{
						this.ResolveToNextHopAndConnect();
						return;
					}
					if (this.blindProxySendConnector == null)
					{
						this.ResolveProxyNextHopAndConnect(this.proxyDestinations, this.internalDestination, this.proxyType);
						return;
					}
					this.ResolveProxyNextHopAndConnect(this.proxyDestinations, this.internalDestination, this.blindProxySendConnector);
				}
				return;
			}
			Interlocked.Exchange(ref this.numberOfDnsQuery, 0);
			foreach (EnhancedDnsTargetHost enhancedDnsTargetHost in this.smtpTargetHosts)
			{
				this.totalTargets += enhancedDnsTargetHost.IPAddresses.Count;
			}
			this.smtpOutConnection.ConnectToNextHost();
		}

		// Token: 0x04001EB9 RID: 7865
		private EnhancedDns enhancedDns;

		// Token: 0x04001EBA RID: 7866
		private UnhealthyTargetFilter<IPAddressPortPair> unhealthyTargetIpAddressFilter;

		// Token: 0x04001EBB RID: 7867
		private UnhealthyTargetFilter<FqdnPortPair> unhealthyTargetFqdnFilter;

		// Token: 0x04001EBC RID: 7868
		private SmtpOutConnection smtpOutConnection;

		// Token: 0x04001EBD RID: 7869
		private EnhancedDnsTargetHost[] smtpTargetHosts;

		// Token: 0x04001EBE RID: 7870
		private IEnumerable<INextHopServer> proxyDestinations;

		// Token: 0x04001EBF RID: 7871
		private SmtpSendConnectorConfig blindProxySendConnector;

		// Token: 0x04001EC0 RID: 7872
		private SmtpOutProxyType proxyType;

		// Token: 0x04001EC1 RID: 7873
		private bool internalDestination;

		// Token: 0x04001EC2 RID: 7874
		private int currentTargetIndex;

		// Token: 0x04001EC3 RID: 7875
		private int currentIpAddressIndex = -1;

		// Token: 0x04001EC4 RID: 7876
		private ulong sessionId;

		// Token: 0x04001EC5 RID: 7877
		private object dnsUpdateLock = new object();

		// Token: 0x04001EC6 RID: 7878
		private int numberOfDnsQuery;

		// Token: 0x04001EC7 RID: 7879
		private NextHopConnection nextHopConnection;

		// Token: 0x04001EC8 RID: 7880
		private bool routingTableChange;

		// Token: 0x04001EC9 RID: 7881
		private SmtpOutTargetHostPicker.SmtpTarget currentSmtpTarget;

		// Token: 0x04001ECA RID: 7882
		private IAsyncResult asyncResult;

		// Token: 0x04001ECB RID: 7883
		private int totalTargets;

		// Token: 0x02000512 RID: 1298
		internal class SmtpTarget
		{
			// Token: 0x06003CD0 RID: 15568 RVA: 0x000FDA60 File Offset: 0x000FBC60
			public SmtpTarget(IPAddress address, string targetHostName, ushort port)
			{
				this.address = address;
				this.targetHostName = targetHostName;
				this.port = port;
			}

			// Token: 0x170012A3 RID: 4771
			// (get) Token: 0x06003CD1 RID: 15569 RVA: 0x000FDA7D File Offset: 0x000FBC7D
			internal IPAddress Address
			{
				get
				{
					return this.address;
				}
			}

			// Token: 0x170012A4 RID: 4772
			// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000FDA85 File Offset: 0x000FBC85
			internal string TargetHostName
			{
				get
				{
					return this.targetHostName;
				}
			}

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x06003CD3 RID: 15571 RVA: 0x000FDA8D File Offset: 0x000FBC8D
			internal ushort Port
			{
				get
				{
					return this.port;
				}
			}

			// Token: 0x04001ECC RID: 7884
			private readonly IPAddress address;

			// Token: 0x04001ECD RID: 7885
			private readonly string targetHostName;

			// Token: 0x04001ECE RID: 7886
			private readonly ushort port;
		}

		// Token: 0x02000513 RID: 1299
		private class SmtpOutNextHopServer : INextHopServer
		{
			// Token: 0x06003CD4 RID: 15572 RVA: 0x000FDA95 File Offset: 0x000FBC95
			public SmtpOutNextHopServer(IPAddress address)
			{
				this.address = address;
			}

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x000FDAA4 File Offset: 0x000FBCA4
			public bool IsIPAddress
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000FDAA7 File Offset: 0x000FBCA7
			public IPAddress Address
			{
				get
				{
					return this.address;
				}
			}

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000FDAAF File Offset: 0x000FBCAF
			public string Fqdn
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x000FDAB2 File Offset: 0x000FBCB2
			public bool IsFrontendAndHubColocatedServer
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04001ECF RID: 7887
			private readonly IPAddress address;
		}
	}
}
