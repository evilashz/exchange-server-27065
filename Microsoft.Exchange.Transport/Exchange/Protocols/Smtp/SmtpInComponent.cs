using System;
using System.Net.Sockets;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B9 RID: 1209
	internal class SmtpInComponent : ISmtpInComponent, IStartableTransportComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x06003690 RID: 13968 RVA: 0x000E01FC File Offset: 0x000DE3FC
		public SmtpInComponent(bool useModernSmtpStack = false)
		{
			this.log = new ProtocolLog("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version, "SMTP Receive Protocol Log", "RECV", "SmtpReceiveProtocolLogs");
			if (useModernSmtpStack)
			{
				this.server = new ModernSmtpInServer();
			}
			else
			{
				this.server = new SmtpInServer();
			}
			this.loadTimeDependenciesSet = false;
			this.runTimeDependenciesSet = false;
		}

		// Token: 0x17001046 RID: 4166
		// (set) Token: 0x06003691 RID: 13969 RVA: 0x000E0271 File Offset: 0x000DE471
		public bool SelfListening
		{
			set
			{
				this.selfListening = value;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003692 RID: 13970 RVA: 0x000E027A File Offset: 0x000DE47A
		public string CurrentState
		{
			get
			{
				return this.server.CurrentState;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x000E0287 File Offset: 0x000DE487
		public ServiceState TargetRunningState
		{
			get
			{
				return this.server.TargetRunningState;
			}
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000E0294 File Offset: 0x000DE494
		public void UpdateTime(DateTime time)
		{
			this.server.CurrentTime = time;
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000E02A4 File Offset: 0x000DE4A4
		public void SetRunTimeDependencies(IAgentRuntime agentRuntime, IMailRouter mailRouter, IProxyHubSelector proxyHubSelector, IEnhancedDns enhancedDns, ICategorizer categorizer, ICertificateCache certificateCache, ICertificateValidator certificateValidator, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessagingDatabase messagingDatabase, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, SmtpOutConnectionHandler smtpOutConnectionHandler, IQueueQuotaComponent queueQuotaComponent)
		{
			if (messagingDatabase == null)
			{
				throw new ArgumentNullException("messagingDatabase");
			}
			this.messagingDatabase = messagingDatabase;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.server.SetRunTimeDependencies(agentRuntime, mailRouter, proxyHubSelector, enhancedDns, categorizer, certificateCache, certificateValidator, memberOfResolver, messageThrottlingManager, shadowRedundancyManager, new SmtpInMailItemStorage(), smtpOutConnectionHandler, queueQuotaComponent);
			this.runTimeDependenciesSet = true;
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000E02FC File Offset: 0x000DE4FC
		public void SetLoadTimeDependencies(TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration)
		{
			this.networkConnectionReceiveBufferSize = transportAppConfig.SmtpReceiveConfiguration.NetworkConnectionReceiveBufferSize;
			this.server.SetLoadTimeDependencies(this.log, transportAppConfig, transportConfiguration);
			this.loadTimeDependenciesSet = true;
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000E0329 File Offset: 0x000DE529
		public void Load()
		{
			if (!this.loadTimeDependenciesSet)
			{
				throw new InvalidOperationException("load-time dependencies should be set before calling Load()");
			}
			this.server.Load();
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000E0349 File Offset: 0x000DE549
		public void Unload()
		{
			this.server.Unload();
			this.log.Close();
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000E0361 File Offset: 0x000DE561
		public string OnUnhandledException(Exception e)
		{
			this.NonGracefullyCloseTcpListener();
			this.Pause();
			this.FlushProtocolLog();
			return null;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000E0378 File Offset: 0x000DE578
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			if (!this.runTimeDependenciesSet)
			{
				throw new InvalidOperationException("run-time dependencies should be set before calling Start()");
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "Components Start");
			this.server.TargetRunningState = targetRunningState;
			if (initiallyPaused || !this.ShouldExecute())
			{
				this.Pause(true, SmtpResponse.ServiceUnavailable);
			}
			if (this.selfListening)
			{
				this.server.Initialize(new TcpListener.HandleFailure(SmtpInComponent.OnTcpListenerFailure), new TcpListener.HandleConnection(this.HandleConnection));
			}
			else
			{
				this.server.Initialize(null, null);
			}
			this.server.CurrentTime = DateTime.UtcNow;
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000E041B File Offset: 0x000DE61B
		public void FlushProtocolLog()
		{
			if (this.log != null)
			{
				this.log.Flush();
			}
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000E0430 File Offset: 0x000DE630
		public virtual void SetThrottleDelay(TimeSpan throttleDelay, string throttleDelayContext)
		{
			if (this.server.ThrottleDelay != throttleDelay || !string.Equals(this.server.ThrottleDelayContext, throttleDelayContext, StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<TimeSpan, string>((long)this.GetHashCode(), "Changing throttling delay to '{0}' with context '{1}'.", throttleDelay, throttleDelayContext ?? string.Empty);
				this.server.SetThrottleState(throttleDelay, throttleDelayContext);
			}
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000E0494 File Offset: 0x000DE694
		public void Stop()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "SmtpIn Component Stop");
			if (this.messagingDatabase.DataSource != null)
			{
				this.messagingDatabase.DataSource.TryForceFlush();
			}
			if (this.server != null)
			{
				this.server.Shutdown();
			}
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000E04E8 File Offset: 0x000DE6E8
		public void Pause()
		{
			this.Pause(true, SmtpResponse.ServiceUnavailable);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000E04F6 File Offset: 0x000DE6F6
		public virtual void Pause(bool rejectSubmits, SmtpResponse reasonForPause)
		{
			this.server.SetRejectState(rejectSubmits && (this.shadowRedundancyManager == null || !this.shadowRedundancyManager.Configuration.Enabled), rejectSubmits, true, reasonForPause);
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000E052A File Offset: 0x000DE72A
		public virtual void Continue()
		{
			if (this.ShouldExecute())
			{
				this.server.SetRejectState(false, false, false, SmtpResponse.Empty);
			}
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000E0547 File Offset: 0x000DE747
		public void RejectCommands()
		{
			this.server.SetRejectState(true, this.server.RejectSubmits, this.server.RejectMailFromInternet, SmtpResponse.ConnectionDropped);
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000E0570 File Offset: 0x000DE770
		public void RejectSubmits()
		{
			this.server.SetRejectState(this.server.RejectCommands, true, this.server.RejectMailFromInternet, SmtpResponse.ConnectionDropped);
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000E059C File Offset: 0x000DE79C
		public bool HandleConnection(Socket socket)
		{
			if (Components.ShuttingDown || this.server == null)
			{
				socket.Close();
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "Drop new connection since SmtpInServer isn't created yet or is shutting down");
				return false;
			}
			bool flag = false;
			INetworkConnection networkConnection = null;
			bool result;
			try
			{
				networkConnection = this.server.CreateNetworkConnection(socket, this.networkConnectionReceiveBufferSize);
				flag = this.server.HandleConnection(networkConnection);
				result = flag;
			}
			finally
			{
				if (!flag && networkConnection != null)
				{
					networkConnection.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000E061C File Offset: 0x000DE81C
		private static void OnTcpListenerFailure(bool addressAlreadyInUseFailure)
		{
			string reason = Strings.TcpListenerError;
			bool retryAlways = true;
			Components.StopService(reason, false, retryAlways, addressAlreadyInUseFailure);
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000E063D File Offset: 0x000DE83D
		private bool ShouldExecute()
		{
			return this.server.TargetRunningState == ServiceState.Active || this.server.TargetRunningState == ServiceState.Inactive;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000E065D File Offset: 0x000DE85D
		private void NonGracefullyCloseTcpListener()
		{
			this.server.NonGracefullyCloseTcpListener();
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000E066A File Offset: 0x000DE86A
		public string GetDiagnosticComponentName()
		{
			return "SmtpIn";
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000E0674 File Offset: 0x000DE874
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			this.server.AddDiagnosticInfo(parameters, xelement);
			return xelement;
		}

		// Token: 0x04001BDB RID: 7131
		private readonly ISmtpInServer server;

		// Token: 0x04001BDC RID: 7132
		private bool selfListening;

		// Token: 0x04001BDD RID: 7133
		private readonly ProtocolLog log;

		// Token: 0x04001BDE RID: 7134
		private IMessagingDatabase messagingDatabase;

		// Token: 0x04001BDF RID: 7135
		private IShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001BE0 RID: 7136
		private bool loadTimeDependenciesSet;

		// Token: 0x04001BE1 RID: 7137
		private bool runTimeDependenciesSet;

		// Token: 0x04001BE2 RID: 7138
		private int networkConnectionReceiveBufferSize = 4096;
	}
}
