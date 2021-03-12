using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000514 RID: 1300
	internal class SmtpOutConnectionHandler : ISmtpOutConnectionHandler, IStartableTransportComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000FDAB5 File Offset: 0x000FBCB5
		public static SmtpOutSessionCache SessionCache
		{
			get
			{
				return SmtpOutConnectionHandler.sessionCache;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x000FDABC File Offset: 0x000FBCBC
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(512);
				stringBuilder.AppendLine("Connections=" + SmtpOutConnectionHandler.connections.Count.ToString());
				lock (SmtpOutConnectionHandler.SyncRoot)
				{
					foreach (SmtpOutConnection smtpOutConnection in SmtpOutConnectionHandler.connections.Values)
					{
						stringBuilder.Append(smtpOutConnection.GetSessionAndConnectionInfo());
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000FDB78 File Offset: 0x000FBD78
		private static object SyncRoot
		{
			get
			{
				return SmtpOutConnectionHandler.connections;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000FDB7F File Offset: 0x000FBD7F
		public BufferCache BufferCache
		{
			get
			{
				return this.bufferCache;
			}
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000FDB88 File Offset: 0x000FBD88
		public static bool AddConnection(ulong id, SmtpOutConnection smtpOutConnection)
		{
			bool result;
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				if (Components.ShuttingDown)
				{
					result = false;
				}
				else
				{
					TransportHelpers.AttemptAddToDictionary<ulong, SmtpOutConnection>(SmtpOutConnectionHandler.connections, id, smtpOutConnection, null);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x000FDBE0 File Offset: 0x000FBDE0
		public static bool UpdateConnection(ulong id, NetworkConnection nc)
		{
			bool result;
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				if (Components.ShuttingDown)
				{
					result = false;
				}
				else
				{
					SmtpOutConnection smtpOutConnection = SmtpOutConnectionHandler.connections[id];
					smtpOutConnection.UpdateSession(nc);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x000FDC3C File Offset: 0x000FBE3C
		public static bool ReplaceConnectionID(ulong previousID, ulong newID)
		{
			bool result;
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				if (Components.ShuttingDown)
				{
					result = false;
				}
				else
				{
					SmtpOutConnection valueToAdd = SmtpOutConnectionHandler.connections[previousID];
					SmtpOutConnectionHandler.connections.Remove(previousID);
					TransportHelpers.AttemptAddToDictionary<ulong, SmtpOutConnection>(SmtpOutConnectionHandler.connections, newID, valueToAdd, null);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x000FDCAC File Offset: 0x000FBEAC
		public static void RemoveConnection(ulong id)
		{
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				if (!SmtpOutConnectionHandler.connections.Remove(id))
				{
					throw new ArgumentException(string.Format("Session {0} not found", id), "id");
				}
			}
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000FDD10 File Offset: 0x000FBF10
		public void SetRunTimeDependencies(EnhancedDns enhancedDns, UnhealthyTargetFilterComponent unhealthyTargetFilter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager)
		{
			this.enhancedDns = enhancedDns;
			this.unhealthyTargetFilter = unhealthyTargetFilter;
			this.certificateCache = certificateCache;
			this.certificateValidator = certificateValidator;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.runTimeDependenciesSet = true;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000FDD3E File Offset: 0x000FBF3E
		public void SetLoadTimeDependencies(IMailRouter mailRouter, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, ISmtpInComponent smtpInComponent, Components.LoggingComponent loggingComponent)
		{
			this.mailRouter = mailRouter;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
			this.smtpInComponent = smtpInComponent;
			this.loggingComponent = loggingComponent;
			this.loadTimeDependenciesSet = true;
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000FDD6C File Offset: 0x000FBF6C
		public void Load()
		{
			if (!this.loadTimeDependenciesSet)
			{
				throw new InvalidOperationException("load-time dependencies should be set before calling Load()");
			}
			this.Configure();
			Components.ConfigChanged += this.ConfigUpdate;
			this.mailRouter.RoutingTablesChanged += this.SendConnectorsUpdate;
			this.SendConnectorsUpdate(this.mailRouter, DateTime.MinValue, true);
			this.AddPerInMemoryConnectorPerfCounters();
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000FDDD4 File Offset: 0x000FBFD4
		public void Unload()
		{
			if (SmtpOutConnectionHandler.connections.Count > 0)
			{
				throw new InvalidOperationException(string.Format("We should not have any smtp out connections; we have {0}.", SmtpOutConnectionHandler.connections.Count));
			}
			Components.ConfigChanged -= this.ConfigUpdate;
			this.mailRouter.RoutingTablesChanged -= this.SendConnectorsUpdate;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000FDE35 File Offset: 0x000FC035
		public string OnUnhandledException(Exception e)
		{
			this.FlushProtocolLog();
			return null;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000FDE3E File Offset: 0x000FC03E
		public void FlushProtocolLog()
		{
			if (this.loggingComponent != null)
			{
				this.loggingComponent.SmtpSendLog.Flush();
			}
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000FDE58 File Offset: 0x000FC058
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			if (!this.runTimeDependenciesSet)
			{
				throw new InvalidOperationException("run-time dependencies should be set before calling Start()");
			}
			TlsCredentialCache.Initialize(1000, 3);
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x000FDE78 File Offset: 0x000FC078
		public void Stop()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug(0L, "Shutdown called");
			TlsCredentialCache.Shutdown();
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				foreach (SmtpOutConnection smtpOutConnection in SmtpOutConnectionHandler.connections.Values)
				{
					smtpOutConnection.Shutdown();
				}
			}
			SmtpOutConnectionHandler.SessionCache.RemoveAll(ConnectionCacheRemovalType.ShutDown);
			while (SmtpOutConnectionHandler.connections.Count > 0)
			{
				Thread.Sleep(1000);
			}
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000FDF30 File Offset: 0x000FC130
		public void Pause()
		{
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000FDF32 File Offset: 0x000FC132
		public void Continue()
		{
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000FDF34 File Offset: 0x000FC134
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "SmtpOut";
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000FDF3C File Offset: 0x000FC13C
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			string diagnosticComponentName = ((IDiagnosable)this).GetDiagnosticComponentName();
			XElement xelement = new XElement(diagnosticComponentName);
			if (flag2)
			{
				xelement.Add(new XElement("help", "Supported arguments: verbose, help."));
			}
			else
			{
				UnhealthyTargetFilterComponent unhealthyTargetFilterComponent = this.unhealthyTargetFilter;
				XElement xelement2 = new XElement("UnhealthyTargetFilter");
				xelement.Add(xelement2);
				XElement xelement3 = new XElement("Configuration");
				XElement xelement4 = new XElement("UnhealthyTargetFqdnFilter");
				XElement xelement5 = new XElement("UnhealthyTargetIpAddressFilter");
				XElement xelement6 = new XElement("InboundProxyCache");
				xelement2.Add(xelement3);
				xelement2.Add(xelement4);
				xelement2.Add(xelement5);
				xelement2.Add(xelement6);
				UnhealthyTargetFilterConfiguration unhealthyTargetFilterConfiguration = (unhealthyTargetFilterComponent != null) ? unhealthyTargetFilterComponent.UnhealthyTargetFilterConfiguration : null;
				UnhealthyTargetFilter<IPAddressPortPair> unhealthyTargetFilter = (unhealthyTargetFilterComponent != null) ? unhealthyTargetFilterComponent.UnhealthyTargetIPAddressFilter : null;
				UnhealthyTargetFilter<FqdnPortPair> unhealthyTargetFilter2 = (unhealthyTargetFilterComponent != null) ? unhealthyTargetFilterComponent.UnhealthyTargetFqdnFilter : null;
				if (unhealthyTargetFilterConfiguration != null)
				{
					unhealthyTargetFilterConfiguration.AddDiagnosticInfoTo(xelement3);
				}
				if (unhealthyTargetFilter2 != null)
				{
					unhealthyTargetFilter2.AddDiagnosticInfoTo(xelement4, flag);
				}
				if (unhealthyTargetFilter != null)
				{
					unhealthyTargetFilter.AddDiagnosticInfoTo(xelement5, flag);
				}
				this.bufferCache.AddDiagnosticInfoTo(xelement6, flag);
				if (SmtpOutConnectionHandler.sessionCache != null)
				{
					XElement xelement7 = new XElement("SmtpOutSessionCache");
					SmtpOutConnectionHandler.sessionCache.AddDiagnosticInfoTo(xelement7, flag);
					xelement2.Add(xelement7);
				}
			}
			return xelement;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000FE0D0 File Offset: 0x000FC2D0
		public void HandleConnection(NextHopConnection connection)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Initiating new outbound connection");
			SmtpOutConnection smtpOutConnection = new SmtpOutConnection(connection, this.loggingComponent.SmtpSendLog, this.mailRouter, this.enhancedDns, this.unhealthyTargetFilter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, this.smtpInComponent, RiskLevel.Normal, 0);
			smtpOutConnection.Connect();
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x000FE144 File Offset: 0x000FC344
		public void HandleProxyConnection(NextHopConnection connection, IEnumerable<INextHopServer> proxyDestinations, bool internalDestination, string connectionContextString)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Initiating new outbound connection for {0} proxy destination(s)", internalDestination ? "internal" : "external");
			SmtpOutConnection smtpOutConnection = new SmtpOutConnection(connection, this.loggingComponent.SmtpSendLog, this.mailRouter, this.enhancedDns, this.unhealthyTargetFilter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, this.smtpInComponent, RiskLevel.Normal, 0, this.transportAppConfig.SmtpInboundProxyConfiguration.PerHostConnectionAttempts, connectionContextString);
			smtpOutConnection.ConnectToPerMessageProxyDestinations(proxyDestinations, internalDestination);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x000FE1DC File Offset: 0x000FC3DC
		public SmtpOutConnection NewBlindProxyConnection(NextHopConnection connection, IEnumerable<INextHopServer> proxyDestinations, bool clientProxy, SmtpSendConnectorConfig connector, TlsSendConfiguration tlsSendConfiguration, RiskLevel riskLevel, int outboundIPPool, int connectionAttempts, ISmtpInSession inSession, string connectionContextString)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Initiating new outbound connection for blind {0} proxy destination(s)", clientProxy ? "client" : "outbound");
			return new SmtpOutConnection(connection, this.loggingComponent.SmtpSendLog, this.mailRouter, this.enhancedDns, this.unhealthyTargetFilter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, this.smtpInComponent, riskLevel, outboundIPPool, connectionAttempts, 1, clientProxy, tlsSendConfiguration, inSession, false, connectionContextString);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x000FE268 File Offset: 0x000FC468
		public void HandleShadowConnection(NextHopConnection connection, IEnumerable<INextHopServer> shadowHubs)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Initiating new outbound shadow connection");
			SmtpOutConnection smtpOutConnection = new SmtpOutConnection(connection, this.loggingComponent.SmtpSendLog, this.mailRouter, this.enhancedDns, this.unhealthyTargetFilter, this.certificateCache, this.certificateValidator, this.shadowRedundancyManager, this.transportAppConfig, this.transportConfiguration, this.smtpInComponent, RiskLevel.Normal, 0, 0, 1, false, null, null, true, null);
			smtpOutConnection.ConnectToShadowDestinations(shadowHubs);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x000FE2E3 File Offset: 0x000FC4E3
		internal static SmtpSendPerfCountersInstance GetSmtpSendPerfCounterInstance(string connectorName)
		{
			SmtpSendPerfCounters.SetCategoryName(SmtpOutConnectionHandler.perfCounterCategoryMap[Components.Configuration.ProcessTransportRole]);
			return SmtpSendPerfCounters.GetInstance(connectorName);
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x000FE304 File Offset: 0x000FC504
		private static void AddPerConnectorPerfCounters(IList<SmtpSendConnectorConfig> sendConnectorCollection)
		{
			foreach (SmtpSendConnectorConfig smtpSendConnectorConfig in sendConnectorCollection)
			{
				SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(smtpSendConnectorConfig.Name);
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x000FE354 File Offset: 0x000FC554
		private void AddPerInMemoryConnectorPerfCounters()
		{
			if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.Hub)
			{
				SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(Strings.IntraorgSendConnectorName);
				return;
			}
			if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.FrontEnd)
			{
				SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(Strings.ExternalDestinationInboundProxySendConnector);
				SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(Strings.InternalDestinationInboundProxySendConnector);
				return;
			}
			if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery || this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission)
			{
				SmtpOutConnectionHandler.GetSmtpSendPerfCounterInstance(Strings.MailboxProxySendConnector);
			}
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x000FE3DC File Offset: 0x000FC5DC
		private void SendConnectorsUpdate(IMailRouter routing, DateTime newRoutingTablesTimestamp, bool routesChanged)
		{
			if (routing == null)
			{
				return;
			}
			if (this.transportConfiguration.ProcessTransportRole != ProcessTransportRole.Hub && this.transportConfiguration.ProcessTransportRole != ProcessTransportRole.Edge)
			{
				return;
			}
			IList<SmtpSendConnectorConfig> localSendConnectors = routing.GetLocalSendConnectors<SmtpSendConnectorConfig>();
			SmtpOutConnectionHandler.AddPerConnectorPerfCounters(localSendConnectors);
			SmtpOutConnectionHandler.SessionCache.RemoveAll(ConnectionCacheRemovalType.ConfigChange);
			lock (SmtpOutConnectionHandler.SyncRoot)
			{
				if (newRoutingTablesTimestamp > SmtpOutConnectionHandler.currentRoutingTablesTimestamp)
				{
					if (SmtpOutConnectionHandler.currentRoutingTablesTimestamp != DateTime.MinValue)
					{
						ExTraceGlobals.SmtpSendTracer.TraceDebug(0L, "Routing table changed, notify connections for DNS update.");
						foreach (SmtpOutConnection smtpOutConnection in SmtpOutConnectionHandler.connections.Values)
						{
							smtpOutConnection.RoutingTableUpdate();
						}
					}
					if (newRoutingTablesTimestamp != DateTime.MaxValue)
					{
						SmtpOutConnectionHandler.currentRoutingTablesTimestamp = newRoutingTablesTimestamp;
					}
				}
			}
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000FE4D8 File Offset: 0x000FC6D8
		private void ConfigUpdate(object source, EventArgs args)
		{
			this.SendConnectorsUpdate(this.mailRouter, DateTime.MaxValue, false);
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x000FE4EC File Offset: 0x000FC6EC
		private void Configure()
		{
			int maxEntriesForOutboundProxy = 0;
			int maxEntriesForNonOutboundProxy = 0;
			TimeSpan connectionTimeoutForOutboundProxy = this.transportAppConfig.ConnectionCacheConfig.ConnectionTimeoutForOutboundProxy;
			TimeSpan connectionInactivityTimeout = this.transportAppConfig.ConnectionCacheConfig.ConnectionInactivityTimeout;
			if (this.transportAppConfig.ConnectionCacheConfig.EnableConnectionCache)
			{
				maxEntriesForOutboundProxy = this.transportAppConfig.ConnectionCacheConfig.ConnectionCacheMaxNumberOfEntriesForOutboundProxy;
				maxEntriesForNonOutboundProxy = this.transportAppConfig.ConnectionCacheConfig.ConnectionCacheMaxNumberOfEntriesForNonOutboundProxy;
			}
			SmtpOutConnectionHandler.sessionCache = new SmtpOutSessionCache(maxEntriesForOutboundProxy, maxEntriesForNonOutboundProxy, connectionTimeoutForOutboundProxy, connectionInactivityTimeout, new SmtpOutSessionCache.ConnectionCachePerfCounters(this.transportConfiguration.ProcessTransportRole, "OutboundProxyConnectionCache"), new SmtpOutSessionCache.ConnectionCachePerfCounters(this.transportConfiguration.ProcessTransportRole, "NonOutboundProxyConnectionCache"));
		}

		// Token: 0x04001ED0 RID: 7888
		internal static ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.SmtpSendTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001ED1 RID: 7889
		private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport SmtpSend"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport SmtpSend"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport SmtpSend"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery SmtpSend"
			},
			{
				ProcessTransportRole.MailboxSubmission,
				"MSExchange Submission SmtpSend"
			}
		};

		// Token: 0x04001ED2 RID: 7890
		private static Dictionary<ulong, SmtpOutConnection> connections = new Dictionary<ulong, SmtpOutConnection>();

		// Token: 0x04001ED3 RID: 7891
		private static SmtpOutSessionCache sessionCache;

		// Token: 0x04001ED4 RID: 7892
		private static DateTime currentRoutingTablesTimestamp = DateTime.MinValue;

		// Token: 0x04001ED5 RID: 7893
		private Components.LoggingComponent loggingComponent;

		// Token: 0x04001ED6 RID: 7894
		private IMailRouter mailRouter;

		// Token: 0x04001ED7 RID: 7895
		private EnhancedDns enhancedDns;

		// Token: 0x04001ED8 RID: 7896
		private UnhealthyTargetFilterComponent unhealthyTargetFilter;

		// Token: 0x04001ED9 RID: 7897
		private CertificateCache certificateCache;

		// Token: 0x04001EDA RID: 7898
		private CertificateValidator certificateValidator;

		// Token: 0x04001EDB RID: 7899
		private ShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001EDC RID: 7900
		private TransportAppConfig transportAppConfig;

		// Token: 0x04001EDD RID: 7901
		private ITransportConfiguration transportConfiguration;

		// Token: 0x04001EDE RID: 7902
		private ISmtpInComponent smtpInComponent;

		// Token: 0x04001EDF RID: 7903
		private bool loadTimeDependenciesSet;

		// Token: 0x04001EE0 RID: 7904
		private bool runTimeDependenciesSet;

		// Token: 0x04001EE1 RID: 7905
		private BufferCache bufferCache = new BufferCache(1000);
	}
}
