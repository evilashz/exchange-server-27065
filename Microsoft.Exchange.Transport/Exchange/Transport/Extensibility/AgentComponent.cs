using System;
using System.IO;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x020002FD RID: 765
	internal class AgentComponent : IAgentRuntime, ITransportComponent, IDiagnosable
	{
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002195 RID: 8597 RVA: 0x0007F34C File Offset: 0x0007D54C
		public AcceptedDomainCollection FirstOrgAcceptedDomains
		{
			get
			{
				return Components.Configuration.FirstOrgAcceptedDomainTable;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002196 RID: 8598 RVA: 0x0007F358 File Offset: 0x0007D558
		public RemoteDomainCollection RemoteDomains
		{
			get
			{
				return Components.Configuration.RemoteDomainTable;
			}
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0007F364 File Offset: 0x0007D564
		public void Load()
		{
			string configFile = null;
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub || Components.Configuration.ProcessTransportRole == ProcessTransportRole.Edge)
			{
				configFile = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config");
			}
			else if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.FrontEnd)
			{
				configFile = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\fetagents.config");
			}
			try
			{
				this.mexRuntime.Initialize(configFile, "Microsoft.Exchange.Data.Transport.Smtp.SmtpReceiveAgent", Components.Configuration.ProcessTransportRole, ConfigurationContext.Setup.InstallPath, new FactoryInitializer(ProcessAccessManager.RegisterAgentFactory));
				AgentLatencyTracker.RegisterMExRuntime(LatencyAgentGroup.SmtpReceive, this.mexRuntime);
			}
			catch (ExchangeConfigurationException ex)
			{
				Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_CannotStartAgents, null, new object[]
				{
					ex.LocalizedString,
					ex
				});
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, "Microsoft Exchange couldn't start transport agents.", ResultSeverityLevel.Error, false);
				throw new TransportComponentLoadFailedException(Strings.AgentComponentFailed, ex);
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0007F460 File Offset: 0x0007D660
		public void Unload()
		{
			try
			{
				this.mexRuntime.Shutdown();
			}
			catch (InvalidOperationException)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug(0L, "MExEvents.Shutdown threw InvalidOperationException: ongoing sessions.");
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0007F4A0 File Offset: 0x0007D6A0
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0007F4A4 File Offset: 0x0007D6A4
		public ISmtpAgentSession NewSmtpAgentSession(ISmtpInSession smtpInSession, INetworkConnection networkConnection, bool isExternalConnection)
		{
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			ArgumentValidator.ThrowIfNull("networkConnection", networkConnection);
			SmtpReceiveServer smtpReceiveServer = SmtpReceiveServer.FromSmtpInSession(smtpInSession, Components.AgentComponent.FirstOrgAcceptedDomains, Components.AgentComponent.RemoteDomains, Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion);
			return new SmtpAgentSession(this.mexRuntime, smtpReceiveServer, smtpInSession, new SmtpSessionImpl(smtpInSession, networkConnection, isExternalConnection));
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0007F510 File Offset: 0x0007D710
		public ISmtpAgentSession NewSmtpAgentSession(SmtpInSessionState sessionState, IIsMemberOfResolver<RoutingAddress> isMemberOfResolver, AcceptedDomainCollection firstOrgAcceptedDomains, RemoteDomainCollection remoteDomains, ServerVersion adminDisplayVersion, out IMExSession mexSession)
		{
			ArgumentValidator.ThrowIfNull("sessionState", sessionState);
			ArgumentValidator.ThrowIfNull("isMemberOfResolver", isMemberOfResolver);
			ArgumentValidator.ThrowIfNull("firstOrgAcceptedDomains", firstOrgAcceptedDomains);
			ArgumentValidator.ThrowIfNull("remoteDomains", remoteDomains);
			ArgumentValidator.ThrowIfNull("adminDisplayVersion", adminDisplayVersion);
			SmtpReceiveServer smtpReceiveServer = SmtpReceiveServer.FromSmtpInSessionState(sessionState, firstOrgAcceptedDomains, remoteDomains, adminDisplayVersion, isMemberOfResolver);
			return new SmtpAgentSession(this.mexRuntime, smtpReceiveServer, sessionState, ref mexSession);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0007F577 File Offset: 0x0007D777
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "SmtpReceiveAgents";
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0007F580 File Offset: 0x0007D780
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			if (this.mexRuntime != null)
			{
				xelement.Add(this.mexRuntime.GetDiagnosticInfo(parameters, "SmtpReceiveAgent"));
			}
			return xelement;
		}

		// Token: 0x040011A0 RID: 4512
		private readonly MExRuntime mexRuntime = new MExRuntime();
	}
}
