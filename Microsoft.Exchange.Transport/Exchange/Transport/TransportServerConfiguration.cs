using System;
using System.DirectoryServices;
using System.IO;
using System.Security.AccessControl;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002F3 RID: 755
	internal class TransportServerConfiguration
	{
		// Token: 0x06002165 RID: 8549 RVA: 0x0007E768 File Offset: 0x0007C968
		protected TransportServerConfiguration(Server server, ActiveDirectorySecurity sid)
		{
			this.server = server;
			this.sid = sid;
			this.processorCount = Environment.ProcessorCount;
			if (TransportServerConfiguration.isBridgehead == null)
			{
				TransportServerConfiguration.isBridgehead = new bool?(server.IsHubTransportServer || server.IsFrontendTransportServer);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0007E7BB File Offset: 0x0007C9BB
		public Server TransportServer
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x0007E7C3 File Offset: 0x0007C9C3
		public string ContentConversionTracingPath
		{
			get
			{
				if (this.TransportServer.ContentConversionTracingEnabled && this.TransportServer.PipelineTracingPath != null)
				{
					return this.TransportServer.PipelineTracingPath.PathName;
				}
				return null;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0007E7F7 File Offset: 0x0007C9F7
		public int MaxConcurrentMailboxSubmissions
		{
			get
			{
				return this.processorCount * this.server.MaxConcurrentMailboxSubmissions;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0007E80B File Offset: 0x0007CA0B
		public int MaxConcurrentMailboxDeliveries
		{
			get
			{
				return this.processorCount * this.server.MaxConcurrentMailboxDeliveries;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0007E81F File Offset: 0x0007CA1F
		public bool IsBridgehead
		{
			get
			{
				if (TransportServerConfiguration.isBridgehead == null)
				{
					throw new InvalidOperationException("TransportServerConfiguration.isBridgehead should not be null");
				}
				return TransportServerConfiguration.isBridgehead.Value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600216B RID: 8555 RVA: 0x0007E842 File Offset: 0x0007CA42
		public ActiveDirectorySecurity TransportServerSecurity
		{
			get
			{
				return this.sid;
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0007E84A File Offset: 0x0007CA4A
		private static LocalLongFullPath AddPathSuffix(LocalLongFullPath path, string suffix)
		{
			if (path != null && !string.IsNullOrEmpty(path.PathName))
			{
				return LocalLongFullPath.Parse(Path.Combine(path.PathName, suffix));
			}
			return null;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0007E878 File Offset: 0x0007CA78
		private static ActiveDirectorySecurity SetupActiveDirectorySecurity(RawSecurityDescriptor rawSecurityDescriptor)
		{
			ActiveDirectorySecurity result;
			try
			{
				result = TransportADUtils.SetupActiveDirectorySecurity(rawSecurityDescriptor);
			}
			catch (OverflowException ex)
			{
				throw new TransportComponentLoadFailedException(ex.Message, ex);
			}
			return result;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0007E8B0 File Offset: 0x0007CAB0
		private static ADObjectId GetNotificationRootId()
		{
			if (TransportServerConfiguration.notificationRootId == null)
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 201, "GetNotificationRootId", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\TransportServerConfiguration.cs");
				TransportServerConfiguration.notificationRootId = topologyConfigurationSession.FindLocalServer().Id;
			}
			return TransportServerConfiguration.notificationRootId;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0007E8FC File Offset: 0x0007CAFC
		internal void UpdateFrontEndConfiguration(FrontendTransportServer frontendServer, bool cloneAndUpdate)
		{
			lock (TransportServerConfiguration.serverUpdateLock)
			{
				ExTraceGlobals.ConfigurationTracer.TraceDebug(0L, "Frontend server object has been updated. We will reconcile the server object with this change.");
				Server server;
				if (cloneAndUpdate)
				{
					server = (Server)this.server.Clone();
				}
				else
				{
					server = this.server;
				}
				server.SetIsReadOnly(false);
				server.TransientFailureRetryInterval = frontendServer.TransientFailureRetryInterval;
				server.TransientFailureRetryCount = frontendServer.TransientFailureRetryCount;
				server.ReceiveProtocolLogPath = frontendServer.ReceiveProtocolLogPath;
				server.ReceiveProtocolLogMaxAge = frontendServer.ReceiveProtocolLogMaxAge;
				server.ReceiveProtocolLogMaxDirectorySize = frontendServer.ReceiveProtocolLogMaxDirectorySize;
				server.ReceiveProtocolLogMaxFileSize = frontendServer.ReceiveProtocolLogMaxFileSize;
				server.SendProtocolLogPath = frontendServer.SendProtocolLogPath;
				server.SendProtocolLogMaxAge = frontendServer.SendProtocolLogMaxAge;
				server.SendProtocolLogMaxDirectorySize = frontendServer.SendProtocolLogMaxDirectorySize;
				server.SendProtocolLogMaxFileSize = frontendServer.SendProtocolLogMaxFileSize;
				server.InternalDNSAdapterEnabled = frontendServer.InternalDNSAdapterEnabled;
				server.InternalDNSAdapterGuid = frontendServer.InternalDNSAdapterGuid;
				server.InternalDNSServers = frontendServer.InternalDNSServers;
				server.InternalDNSProtocolOption = frontendServer.InternalDNSProtocolOption;
				server.IntraOrgConnectorProtocolLoggingLevel = frontendServer.IntraOrgConnectorProtocolLoggingLevel;
				server.ExternalDNSAdapterEnabled = frontendServer.ExternalDNSAdapterEnabled;
				server.ExternalDNSAdapterGuid = frontendServer.ExternalDNSAdapterGuid;
				server.ExternalDNSServers = frontendServer.ExternalDNSServers;
				server.ExternalDNSProtocolOption = frontendServer.ExternalDNSProtocolOption;
				server.ExternalIPAddress = frontendServer.ExternalIPAddress;
				server.ConnectivityLogEnabled = frontendServer.ConnectivityLogEnabled;
				server.ConnectivityLogPath = frontendServer.ConnectivityLogPath;
				server.ConnectivityLogMaxAge = frontendServer.ConnectivityLogMaxAge;
				server.ConnectivityLogMaxDirectorySize = frontendServer.ConnectivityLogMaxDirectorySize;
				server.ConnectivityLogMaxFileSize = frontendServer.ConnectivityLogMaxFileSize;
				server.AntispamAgentsEnabled = frontendServer.AntispamAgentsEnabled;
				server.MaxConnectionRatePerMinute = frontendServer.MaxConnectionRatePerMinute;
				server.MaxOutboundConnections = frontendServer.MaxOutboundConnections;
				server.MaxPerDomainOutboundConnections = frontendServer.MaxPerDomainOutboundConnections;
				server.IntraOrgConnectorSmtpMaxMessagesPerConnection = frontendServer.IntraOrgConnectorSmtpMaxMessagesPerConnection;
				server.AgentLogEnabled = frontendServer.AgentLogEnabled;
				server.AgentLogMaxAge = frontendServer.AgentLogMaxAge;
				server.AgentLogMaxDirectorySize = frontendServer.AgentLogMaxDirectorySize;
				server.AgentLogMaxFileSize = frontendServer.AgentLogMaxFileSize;
				server.AgentLogPath = frontendServer.AgentLogPath;
				server.DnsLogEnabled = frontendServer.DnsLogEnabled;
				server.DnsLogMaxAge = frontendServer.DnsLogMaxAge;
				server.DnsLogMaxDirectorySize = frontendServer.DnsLogMaxDirectorySize;
				server.DnsLogMaxFileSize = frontendServer.DnsLogMaxFileSize;
				server.DnsLogPath = frontendServer.DnsLogPath;
				server.ResourceLogEnabled = frontendServer.ResourceLogEnabled;
				server.ResourceLogMaxAge = frontendServer.ResourceLogMaxAge;
				server.ResourceLogMaxDirectorySize = frontendServer.ResourceLogMaxDirectorySize;
				server.ResourceLogMaxFileSize = frontendServer.ResourceLogMaxFileSize;
				server.ResourceLogPath = frontendServer.ResourceLogPath;
				server.AttributionLogEnabled = frontendServer.AttributionLogEnabled;
				server.AttributionLogMaxAge = frontendServer.AttributionLogMaxAge;
				server.AttributionLogMaxDirectorySize = frontendServer.AttributionLogMaxDirectorySize;
				server.AttributionLogMaxFileSize = frontendServer.AttributionLogMaxFileSize;
				server.AttributionLogPath = frontendServer.AttributionLogPath;
				server.MaxReceiveTlsRatePerMinute = frontendServer.MaxReceiveTlsRatePerMinute;
				server.SetIsReadOnly(true);
				this.server = server;
			}
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0007EBE8 File Offset: 0x0007CDE8
		internal void UpdateMailboxConfiguration(MailboxTransportServer mailboxServer, string pathSuffix, bool cloneAndUpdate)
		{
			lock (TransportServerConfiguration.serverUpdateLock)
			{
				ExTraceGlobals.ConfigurationTracer.TraceDebug(0L, "Mailbox server object has been updated. We will reconcile the server object with this change.");
				Server server;
				if (cloneAndUpdate)
				{
					server = (Server)this.server.Clone();
				}
				else
				{
					server = this.server;
				}
				server.SetIsReadOnly(false);
				server.ReceiveProtocolLogPath = TransportServerConfiguration.AddPathSuffix(mailboxServer.ReceiveProtocolLogPath, pathSuffix);
				server.ReceiveProtocolLogMaxAge = mailboxServer.ReceiveProtocolLogMaxAge;
				server.ReceiveProtocolLogMaxDirectorySize = mailboxServer.ReceiveProtocolLogMaxDirectorySize;
				server.ReceiveProtocolLogMaxFileSize = mailboxServer.ReceiveProtocolLogMaxFileSize;
				server.SendProtocolLogPath = TransportServerConfiguration.AddPathSuffix(mailboxServer.SendProtocolLogPath, pathSuffix);
				server.SendProtocolLogMaxAge = mailboxServer.SendProtocolLogMaxAge;
				server.SendProtocolLogMaxDirectorySize = mailboxServer.SendProtocolLogMaxDirectorySize;
				server.SendProtocolLogMaxFileSize = mailboxServer.SendProtocolLogMaxFileSize;
				server.ConnectivityLogEnabled = mailboxServer.ConnectivityLogEnabled;
				server.ConnectivityLogPath = TransportServerConfiguration.AddPathSuffix(mailboxServer.ConnectivityLogPath, pathSuffix);
				server.ConnectivityLogMaxAge = mailboxServer.ConnectivityLogMaxAge;
				server.ConnectivityLogMaxDirectorySize = mailboxServer.ConnectivityLogMaxDirectorySize;
				server.ConnectivityLogMaxFileSize = mailboxServer.ConnectivityLogMaxFileSize;
				server.MaxConcurrentMailboxDeliveries = mailboxServer.MaxConcurrentMailboxDeliveries;
				server.MaxConcurrentMailboxSubmissions = mailboxServer.MaxConcurrentMailboxSubmissions;
				server.ContentConversionTracingEnabled = mailboxServer.ContentConversionTracingEnabled;
				server.PipelineTracingEnabled = mailboxServer.PipelineTracingEnabled;
				server.PipelineTracingPath = TransportServerConfiguration.AddPathSuffix(mailboxServer.PipelineTracingPath, pathSuffix);
				server.PipelineTracingSenderAddress = mailboxServer.PipelineTracingSenderAddress;
				server.InMemoryReceiveConnectorProtocolLoggingLevel = mailboxServer.InMemoryReceiveConnectorProtocolLoggingLevel;
				server.InMemoryReceiveConnectorSmtpUtf8Enabled = mailboxServer.InMemoryReceiveConnectorSmtpUtf8Enabled;
				server.MailboxDeliveryAgentLogEnabled = mailboxServer.MailboxDeliveryAgentLogEnabled;
				server.MailboxDeliveryAgentLogMaxAge = mailboxServer.MailboxDeliveryAgentLogMaxAge;
				server.MailboxDeliveryAgentLogMaxDirectorySize = mailboxServer.MailboxDeliveryAgentLogMaxDirectorySize;
				server.MailboxDeliveryAgentLogMaxFileSize = mailboxServer.MailboxDeliveryAgentLogMaxFileSize;
				server.MailboxDeliveryAgentLogPath = mailboxServer.MailboxDeliveryAgentLogPath;
				server.MailboxDeliveryThrottlingLogEnabled = mailboxServer.MailboxDeliveryThrottlingLogEnabled;
				server.MailboxDeliveryThrottlingLogMaxAge = mailboxServer.MailboxDeliveryThrottlingLogMaxAge;
				server.MailboxDeliveryThrottlingLogMaxDirectorySize = mailboxServer.MailboxDeliveryThrottlingLogMaxDirectorySize;
				server.MailboxDeliveryThrottlingLogMaxFileSize = mailboxServer.MailboxDeliveryThrottlingLogMaxFileSize;
				server.MailboxDeliveryThrottlingLogPath = mailboxServer.MailboxDeliveryThrottlingLogPath;
				server.MailboxSubmissionAgentLogEnabled = mailboxServer.MailboxSubmissionAgentLogEnabled;
				server.MailboxSubmissionAgentLogMaxAge = mailboxServer.MailboxSubmissionAgentLogMaxAge;
				server.MailboxSubmissionAgentLogMaxDirectorySize = mailboxServer.MailboxSubmissionAgentLogMaxDirectorySize;
				server.MailboxSubmissionAgentLogMaxFileSize = mailboxServer.MailboxSubmissionAgentLogMaxFileSize;
				server.MailboxSubmissionAgentLogPath = mailboxServer.MailboxSubmissionAgentLogPath;
				server.SetIsReadOnly(true);
				this.server = server;
			}
		}

		// Token: 0x04001190 RID: 4496
		private static bool? isBridgehead;

		// Token: 0x04001191 RID: 4497
		private static readonly object serverUpdateLock = new object();

		// Token: 0x04001192 RID: 4498
		private static ADObjectId notificationRootId;

		// Token: 0x04001193 RID: 4499
		private Server server;

		// Token: 0x04001194 RID: 4500
		private readonly ActiveDirectorySecurity sid;

		// Token: 0x04001195 RID: 4501
		private readonly int processorCount;

		// Token: 0x020002F4 RID: 756
		internal class Builder : ConfigurationLoader<TransportServerConfiguration, TransportServerConfiguration.Builder>.Builder
		{
			// Token: 0x06002172 RID: 8562 RVA: 0x0007EE44 File Offset: 0x0007D044
			public override void Register()
			{
				base.Register<Server>(new Func<ADObjectId>(TransportServerConfiguration.GetNotificationRootId));
			}

			// Token: 0x17000AB1 RID: 2737
			// (set) Token: 0x06002173 RID: 8563 RVA: 0x0007EE58 File Offset: 0x0007D058
			public bool RoleCheck
			{
				set
				{
					this.roleCheck = value;
				}
			}

			// Token: 0x06002174 RID: 8564 RVA: 0x0007EE64 File Offset: 0x0007D064
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				Server server = session.FindLocalServer();
				if (server != null)
				{
					RawSecurityDescriptor rawSecurityDescriptor = server.ReadSecurityDescriptor();
					if (rawSecurityDescriptor != null)
					{
						ActiveDirectorySecurity sid = TransportServerConfiguration.SetupActiveDirectorySecurity(rawSecurityDescriptor);
						this.config = new TransportServerConfiguration(server, sid);
					}
				}
			}

			// Token: 0x06002175 RID: 8565 RVA: 0x0007EE9C File Offset: 0x0007D09C
			public override TransportServerConfiguration BuildCache()
			{
				if (this.config != null)
				{
					if (!this.roleCheck)
					{
						return this.config;
					}
					if (this.config.TransportServer.IsHubTransportServer || this.config.TransportServer.IsEdgeServer)
					{
						if (TransportServerConfiguration.isBridgehead.Value == this.config.TransportServer.IsHubTransportServer)
						{
							return this.config;
						}
						base.FailureMessage = Strings.InvalidRoleChange;
					}
					else
					{
						if (this.config.TransportServer.IsFrontendTransportServer || this.config.TransportServer.IsMailboxServer)
						{
							return this.config;
						}
						base.FailureMessage = Strings.InvalidTransportServerRole;
					}
				}
				return null;
			}

			// Token: 0x04001196 RID: 4502
			private TransportServerConfiguration config;

			// Token: 0x04001197 RID: 4503
			private bool roleCheck = true;
		}

		// Token: 0x020002F5 RID: 757
		internal class FrontendBuilder : ConfigurationLoader<FrontendTransportServer, TransportServerConfiguration.FrontendBuilder>.Builder
		{
			// Token: 0x06002177 RID: 8567 RVA: 0x0007EF5D File Offset: 0x0007D15D
			public override void Register()
			{
				base.Register<FrontendTransportServer>(new Func<ADObjectId>(TransportServerConfiguration.GetNotificationRootId));
			}

			// Token: 0x06002178 RID: 8568 RVA: 0x0007EF74 File Offset: 0x0007D174
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				Server server = session.FindLocalServer();
				if (server != null)
				{
					ADObjectId childId = server.Id.GetChildId("Transport Configuration");
					ADObjectId childId2 = childId.GetChildId("Frontend");
					FrontendTransportServer frontendTransportServer = session.Read<FrontendTransportServer>(childId2);
					if (frontendTransportServer != null)
					{
						RawSecurityDescriptor rawSecurityDescriptor = frontendTransportServer.ReadSecurityDescriptor();
						if (rawSecurityDescriptor != null)
						{
							TransportServerConfiguration.SetupActiveDirectorySecurity(rawSecurityDescriptor);
							this.frontEndServer = frontendTransportServer;
						}
					}
				}
			}

			// Token: 0x06002179 RID: 8569 RVA: 0x0007EFCE File Offset: 0x0007D1CE
			public override FrontendTransportServer BuildCache()
			{
				if (this.frontEndServer == null)
				{
					return null;
				}
				if (this.frontEndServer.IsFrontendTransportServer)
				{
					return this.frontEndServer;
				}
				throw new InvalidOperationException(string.Format("The Frontend object's server role has been set to an invalid role: {0}", this.frontEndServer.CurrentServerRole));
			}

			// Token: 0x04001198 RID: 4504
			private FrontendTransportServer frontEndServer;
		}

		// Token: 0x020002F6 RID: 758
		internal class MailboxBuilder : ConfigurationLoader<MailboxTransportServer, TransportServerConfiguration.MailboxBuilder>.Builder
		{
			// Token: 0x0600217B RID: 8571 RVA: 0x0007F015 File Offset: 0x0007D215
			public override void Register()
			{
				base.Register<MailboxTransportServer>(new Func<ADObjectId>(TransportServerConfiguration.GetNotificationRootId));
			}

			// Token: 0x0600217C RID: 8572 RVA: 0x0007F02C File Offset: 0x0007D22C
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope)
			{
				Server server = session.FindLocalServer();
				if (server != null)
				{
					ADObjectId childId = server.Id.GetChildId("Transport Configuration");
					ADObjectId childId2 = childId.GetChildId("Mailbox");
					MailboxTransportServer mailboxTransportServer = session.Read<MailboxTransportServer>(childId2);
					if (mailboxTransportServer != null)
					{
						RawSecurityDescriptor rawSecurityDescriptor = mailboxTransportServer.ReadSecurityDescriptor();
						if (rawSecurityDescriptor != null)
						{
							TransportServerConfiguration.SetupActiveDirectorySecurity(rawSecurityDescriptor);
							this.mailboxServer = mailboxTransportServer;
						}
					}
				}
			}

			// Token: 0x0600217D RID: 8573 RVA: 0x0007F086 File Offset: 0x0007D286
			public override MailboxTransportServer BuildCache()
			{
				if (this.mailboxServer == null)
				{
					return null;
				}
				if (this.mailboxServer.IsMailboxServer)
				{
					return this.mailboxServer;
				}
				throw new InvalidOperationException(string.Format("The Mailbox object's server role has been set to an invalid role: {0}", this.mailboxServer.CurrentServerRole));
			}

			// Token: 0x04001199 RID: 4505
			private MailboxTransportServer mailboxServer;
		}
	}
}
