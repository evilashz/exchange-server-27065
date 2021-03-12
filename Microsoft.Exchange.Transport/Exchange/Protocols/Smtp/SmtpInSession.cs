using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004C2 RID: 1218
	internal class SmtpInSession : ISmtpInSession, ISmtpSession
	{
		// Token: 0x06003739 RID: 14137 RVA: 0x000E2418 File Offset: 0x000E0618
		public SmtpInSession(INetworkConnection connection, ISmtpInServer server, SmtpReceiveConnectorStub connectorStub, IProtocolLog protocolLog, ExEventLog eventLogger, IAgentRuntime agentRuntime, IMailRouter mailRouter, IEnhancedDns enhancedDns, IIsMemberOfResolver<RoutingAddress> memberOfResolver, IMessageThrottlingManager messageThrottlingManager, IShadowRedundancyManager shadowRedundancyManager, ITransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, IQueueQuotaComponent queueQuotaComponent, IAuthzAuthorization authzAuthorization, ISmtpMessageContextBlob smtpMessageContextBlob)
		{
			ArgumentValidator.ThrowIfNull("connection", connection);
			ArgumentValidator.ThrowIfNull("server", server);
			ArgumentValidator.ThrowIfNull("protocolLog", protocolLog);
			ArgumentValidator.ThrowIfNull("eventLogger", eventLogger);
			ArgumentValidator.ThrowIfNull("agentRuntime", agentRuntime);
			ArgumentValidator.ThrowIfNull("mailRouter", mailRouter);
			ArgumentValidator.ThrowIfNull("enhancedDns", enhancedDns);
			ArgumentValidator.ThrowIfNull("memberOfResolver", memberOfResolver);
			ArgumentValidator.ThrowIfNull("messageThrottlingManager", messageThrottlingManager);
			ArgumentValidator.ThrowIfNull("transportAppConfig", transportAppConfig);
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			ArgumentValidator.ThrowIfNull("authzAuthorization", authzAuthorization);
			ArgumentValidator.ThrowIfNull("smtpMessageContextBlob", smtpMessageContextBlob);
			this.eventLogger = eventLogger;
			this.mailRouter = mailRouter;
			this.enhancedDns = enhancedDns;
			this.memberOfResolver = memberOfResolver;
			this.messageThrottlingManager = messageThrottlingManager;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
			this.queueQuotaComponent = queueQuotaComponent;
			this.authzAuthorization = authzAuthorization;
			this.smtpMessageContextBlob = smtpMessageContextBlob;
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.Init);
			this.sessionStartTime = DateTime.UtcNow;
			this.connection = connection;
			this.connectionId = connection.ConnectionId;
			this.server = server;
			this.relatedMessageInfo = string.Empty;
			this.agentSession = agentRuntime.NewSmtpAgentSession(this, connection, !this.IsTrustedIP(connection.RemoteEndPoint.Address));
			if (connectorStub != null)
			{
				this.connectorStub = connectorStub;
				this.connector = connectorStub.Connector;
				this.smtpReceivePerfCountersInstance = connectorStub.SmtpReceivePerfCounterInstance;
				this.smtpAvailabilityPerfCounters = connectorStub.SmtpAvailabilityPerfCounters;
			}
			string connectorId = string.Empty;
			ProtocolLoggingLevel loggingLevel;
			if (this.connector != null)
			{
				loggingLevel = this.connector.ProtocolLoggingLevel;
				connectorId = this.connector.Id.ToString();
				if (this.connector.ExtendedProtectionPolicy != Microsoft.Exchange.Data.Directory.SystemConfiguration.ExtendedProtectionPolicySetting.None)
				{
					this.extendedProtectionConfig = new ExtendedProtectionConfig((int)this.connector.ExtendedProtectionPolicy, null, false);
				}
				else
				{
					this.extendedProtectionConfig = ExtendedProtectionConfig.NoExtendedProtection;
				}
			}
			else
			{
				loggingLevel = ProtocolLoggingLevel.Verbose;
			}
			this.logSession = protocolLog.OpenSession(connectorId, this.SessionId, this.connection.RemoteEndPoint, this.connection.LocalEndPoint, loggingLevel);
			this.logSession.LogConnect();
			if (this.connector == null)
			{
				return;
			}
			this.clientIpAddress = connection.RemoteEndPoint.Address;
			this.significantAddressBytes = SmtpInSessionUtils.GetSignificantBytesOfIPAddress(this.clientIpAddress);
			if (this.SmtpInServer.Ipv6ReceiveConnectionThrottlingEnabled)
			{
				this.clientIpData = this.connectorStub.AddConnection(this.clientIpAddress, this.significantAddressBytes, out this.maxConnectionsExceeded, out this.maxConnectionsPerSourceExceeded);
			}
			else
			{
				this.clientIpData = this.connectorStub.AddConnection(this.clientIpAddress, out this.maxConnectionsExceeded, out this.maxConnectionsPerSourceExceeded);
			}
			this.IncrementConnectionLevelPerfCounters();
			this.connection.Timeout = (int)this.Connector.ConnectionInactivityTimeout.TotalSeconds;
			this.sessionExpireTime = this.sessionStartTime.Add(this.Connector.ConnectionTimeout);
			this.ehloOptions = new EhloOptions();
			this.ehloOptions.AdvertisedIPAddress = this.RemoteEndPoint.Address;
			this.ehloOptions.AdvertisedFQDN = this.AdvertisedDomain;
			this.ehloOptions.Size = this.Connector.SizeEnabled;
			if (this.Connector.MaxMessageSize.ToBytes() > 9223372036854775807UL)
			{
				this.ehloOptions.MaxSize = long.MaxValue;
			}
			else
			{
				this.ehloOptions.MaxSize = (long)this.Connector.MaxMessageSize.ToBytes();
			}
			this.ehloOptions.Pipelining = this.Connector.PipeliningEnabled;
			this.ehloOptions.Dsn = this.Connector.DeliveryStatusNotificationEnabled;
			this.ehloOptions.EnhancedStatusCodes = this.Connector.EnhancedStatusCodesEnabled;
			this.ehloOptions.EightBitMime = this.Connector.EightBitMimeEnabled;
			this.ehloOptions.BinaryMime = this.Connector.BinaryMimeEnabled;
			this.ehloOptions.Chunking = this.Connector.ChunkingEnabled;
			this.ehloOptions.Xexch50 = (this.SmtpInServer.TransportSettings.Xexch50Enabled && (this.Connector.AuthMechanism & (AuthMechanisms.ExchangeServer | AuthMechanisms.ExternalAuthoritative)) != AuthMechanisms.None);
			this.ehloOptions.XLongAddr = this.Connector.LongAddressesEnabled;
			this.ehloOptions.XOrar = this.Connector.OrarEnabled;
			this.ehloOptions.SmtpUtf8 = (this.Connector.SmtpUtf8Enabled && (this.Connector.EightBitMimeEnabled || this.Connector.BinaryMimeEnabled));
			this.ehloOptions.XRDst = (this.server.IsBridgehead && (this.Connector.AuthMechanism & AuthMechanisms.ExchangeServer) != AuthMechanisms.None);
			if (this.shadowRedundancyManager != null)
			{
				this.shadowRedundancyManager.SetSmtpInEhloOptions(this.ehloOptions, this.Connector);
			}
			bool isFrontEndTransportProcess = ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration);
			SmtpInSessionUtils.ApplyRoleBasedEhloOptionsOverrides(this.ehloOptions, isFrontEndTransportProcess);
			this.ApplySupportIntegratedAuthOverride(isFrontEndTransportProcess);
			this.certificatesLoadedSuccessfully = this.LoadCertificates();
			this.sessionPermissions = this.AnonymousPermissions;
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Set Session Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(this.sessionPermissions)));
			if ((this.Connector.AuthMechanism & AuthMechanisms.ExternalAuthoritative) != AuthMechanisms.None)
			{
				this.RemoteIdentity = WellKnownSids.ExternallySecuredServers;
				this.RemoteIdentityName = "accepted_domain";
				this.SetSessionPermissions(this.RemoteIdentity);
			}
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000E29F0 File Offset: 0x000E0BF0
		protected SmtpInSession()
		{
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x0600373B RID: 14139 RVA: 0x000E2A63 File Offset: 0x000E0C63
		// (set) Token: 0x0600373C RID: 14140 RVA: 0x000E2A6B File Offset: 0x000E0C6B
		public X509Certificate2 AdvertisedTlsCertificate { get; private set; }

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600373D RID: 14141 RVA: 0x000E2A74 File Offset: 0x000E0C74
		// (set) Token: 0x0600373E RID: 14142 RVA: 0x000E2A7C File Offset: 0x000E0C7C
		public X509Certificate2 InternalTransportCertificate { get; private set; }

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x0600373F RID: 14143 RVA: 0x000E2A85 File Offset: 0x000E0C85
		// (set) Token: 0x06003740 RID: 14144 RVA: 0x000E2A8D File Offset: 0x000E0C8D
		public X509Certificate2 TlsRemoteCertificate { get; private set; }

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06003741 RID: 14145 RVA: 0x000E2A96 File Offset: 0x000E0C96
		public SmtpProxyPerfCountersWrapper SmtpProxyPerfCounters
		{
			get
			{
				return this.smtpProxyPerfCounters;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003742 RID: 14146 RVA: 0x000E2A9E File Offset: 0x000E0C9E
		public INetworkConnection NetworkConnection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003743 RID: 14147 RVA: 0x000E2AA6 File Offset: 0x000E0CA6
		public long ConnectionId
		{
			get
			{
				return this.connectionId;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003744 RID: 14148 RVA: 0x000E2AAE File Offset: 0x000E0CAE
		public IPAddress ProxiedClientAddress
		{
			get
			{
				return this.proxiedClientAddress;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06003745 RID: 14149 RVA: 0x000E2AB6 File Offset: 0x000E0CB6
		public string ProxyHopHelloDomain
		{
			get
			{
				return this.proxyHopHelloDomain;
			}
		}

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000E2ABE File Offset: 0x000E0CBE
		public IPAddress ProxyHopAddress
		{
			get
			{
				return this.clientIpAddress;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06003747 RID: 14151 RVA: 0x000E2AC6 File Offset: 0x000E0CC6
		// (set) Token: 0x06003748 RID: 14152 RVA: 0x000E2ACE File Offset: 0x000E0CCE
		public InboundClientProxyStates InboundClientProxyState
		{
			get
			{
				return this.inboundClientProxyState;
			}
			set
			{
				this.inboundClientProxyState = value;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003749 RID: 14153 RVA: 0x000E2AD7 File Offset: 0x000E0CD7
		public bool IsAnonymousClientProxiedSession
		{
			get
			{
				return this.proxiedClientAddress != null && this.inboundClientProxyState == InboundClientProxyStates.None;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000E2AEC File Offset: 0x000E0CEC
		// (set) Token: 0x0600374B RID: 14155 RVA: 0x000E2AF4 File Offset: 0x000E0CF4
		public bool StartClientProxySession
		{
			get
			{
				return this.startClientProxySession;
			}
			set
			{
				if (this.startOutboundProxySession && value)
				{
					throw new InvalidOperationException("StartOutboundProxySession and StartClientProxySession cannot both be true");
				}
				this.startClientProxySession = value;
			}
		}

		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x0600374C RID: 14156 RVA: 0x000E2B13 File Offset: 0x000E0D13
		// (set) Token: 0x0600374D RID: 14157 RVA: 0x000E2B1B File Offset: 0x000E0D1B
		public string ProxyUserName
		{
			get
			{
				return this.proxyUserName;
			}
			set
			{
				this.proxyUserName = value;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600374E RID: 14158 RVA: 0x000E2B24 File Offset: 0x000E0D24
		// (set) Token: 0x0600374F RID: 14159 RVA: 0x000E2B2C File Offset: 0x000E0D2C
		public SecureString ProxyPassword
		{
			get
			{
				return this.proxyPassword;
			}
			set
			{
				this.proxyPassword = value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003750 RID: 14160 RVA: 0x000E2B35 File Offset: 0x000E0D35
		// (set) Token: 0x06003751 RID: 14161 RVA: 0x000E2B3D File Offset: 0x000E0D3D
		public bool ClientProxyFailedDueToIncompatibleBackend
		{
			get
			{
				return this.clientProxyFailedDueToIncompatibleBackend;
			}
			set
			{
				this.clientProxyFailedDueToIncompatibleBackend = value;
			}
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003752 RID: 14162 RVA: 0x000E2B46 File Offset: 0x000E0D46
		public IPEndPoint ClientEndPoint
		{
			get
			{
				return this.connection.RemoteEndPoint;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06003753 RID: 14163 RVA: 0x000E2B53 File Offset: 0x000E0D53
		public bool ShutdownConnectionCalled
		{
			get
			{
				return this.shutdownConnectionCalled;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06003754 RID: 14164 RVA: 0x000E2B5B File Offset: 0x000E0D5B
		public bool IsTls
		{
			get
			{
				return this.connection.IsTls;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003755 RID: 14165 RVA: 0x000E2B68 File Offset: 0x000E0D68
		public IProtocolLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06003756 RID: 14166 RVA: 0x000E2B70 File Offset: 0x000E0D70
		public ulong SessionId
		{
			get
			{
				return (ulong)this.SessionSource.SessionId;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06003757 RID: 14167 RVA: 0x000E2B7D File Offset: 0x000E0D7D
		public ClientData ClientIPData
		{
			get
			{
				return this.clientIpData;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06003758 RID: 14168 RVA: 0x000E2B85 File Offset: 0x000E0D85
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.SessionSource.RemoteEndPoint;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06003759 RID: 14169 RVA: 0x000E2B92 File Offset: 0x000E0D92
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.SessionSource.LocalEndPoint;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600375A RID: 14170 RVA: 0x000E2B9F File Offset: 0x000E0D9F
		public string HelloDomain
		{
			get
			{
				return this.SessionSource.HelloDomain;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600375B RID: 14171 RVA: 0x000E2BAC File Offset: 0x000E0DAC
		// (set) Token: 0x0600375C RID: 14172 RVA: 0x000E2BB9 File Offset: 0x000E0DB9
		public SmtpResponse Banner
		{
			get
			{
				return this.SessionSource.Banner;
			}
			set
			{
				this.SessionSource.Banner = value;
			}
		}

		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x0600375D RID: 14173 RVA: 0x000E2BC7 File Offset: 0x000E0DC7
		public IEhloOptions AdvertisedEhloOptions
		{
			get
			{
				return this.ehloOptions;
			}
		}

		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000E2BCF File Offset: 0x000E0DCF
		// (set) Token: 0x0600375F RID: 14175 RVA: 0x000E2BD7 File Offset: 0x000E0DD7
		public string SenderShadowContext
		{
			get
			{
				return this.senderShadowContext;
			}
			set
			{
				this.senderShadowContext = value;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06003760 RID: 14176 RVA: 0x000E2BE0 File Offset: 0x000E0DE0
		public bool IsShadowedBySender
		{
			get
			{
				return !string.IsNullOrEmpty(this.senderShadowContext);
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06003761 RID: 14177 RVA: 0x000E2BF0 File Offset: 0x000E0DF0
		// (set) Token: 0x06003762 RID: 14178 RVA: 0x000E2BF8 File Offset: 0x000E0DF8
		public string PeerSessionPrimaryServer
		{
			get
			{
				return this.peerSessionPrimaryServer;
			}
			set
			{
				this.peerSessionPrimaryServer = value;
				this.SessionSource.Properties["Microsoft.Exchange.IsShadow"] = true;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06003763 RID: 14179 RVA: 0x000E2C1C File Offset: 0x000E0E1C
		public bool IsPeerShadowSession
		{
			get
			{
				return !string.IsNullOrEmpty(this.PeerSessionPrimaryServer);
			}
		}

		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06003764 RID: 14180 RVA: 0x000E2C2C File Offset: 0x000E0E2C
		public bool ShouldProxyClientSession
		{
			get
			{
				return ConfigurationComponent.IsFrontEndTransportProcess(this.transportConfiguration);
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06003765 RID: 14181 RVA: 0x000E2C39 File Offset: 0x000E0E39
		public bool AcceptLongAddresses
		{
			get
			{
				return this.ehloOptions.XLongAddr;
			}
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000E2C46 File Offset: 0x000E0E46
		public SmtpReceiveCapabilities Capabilities
		{
			get
			{
				return Util.SessionCapabilitiesFromTlsAndNonTlsCapabilities(this.SecureState, this.connectorStub.NoTlsCapabilities, this.tlsDomainCapabilities);
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06003767 RID: 14183 RVA: 0x000E2C64 File Offset: 0x000E0E64
		public SmtpReceiveCapabilities? TlsDomainCapabilities
		{
			get
			{
				return this.tlsDomainCapabilities;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06003768 RID: 14184 RVA: 0x000E2C6C File Offset: 0x000E0E6C
		// (set) Token: 0x06003769 RID: 14185 RVA: 0x000E2C74 File Offset: 0x000E0E74
		public XProxyToSmtpCommandParser XProxyToParser
		{
			get
			{
				return this.xProxyToParser;
			}
			set
			{
				this.xProxyToParser = value;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x0600376A RID: 14186 RVA: 0x000E2C7D File Offset: 0x000E0E7D
		public ITransportAppConfig TransportAppConfig
		{
			get
			{
				return this.transportAppConfig;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000E2C85 File Offset: 0x000E0E85
		public InboundRecipientCorrelator RecipientCorrelator
		{
			get
			{
				return this.recipientCorrelator;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000E2C8D File Offset: 0x000E0E8D
		public ChannelBindingToken ChannelBindingToken
		{
			get
			{
				return this.connection.ChannelBindingToken;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000E2C9A File Offset: 0x000E0E9A
		public ExtendedProtectionConfig ExtendedProtectionConfig
		{
			get
			{
				return this.extendedProtectionConfig;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000E2CA4 File Offset: 0x000E0EA4
		public bool DiscardingMessage
		{
			get
			{
				BaseDataSmtpCommand baseDataSmtpCommand = this.commandHandler as BaseDataSmtpCommand;
				return baseDataSmtpCommand != null && baseDataSmtpCommand.DiscardingMessage;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600376F RID: 14191 RVA: 0x000E2CC8 File Offset: 0x000E0EC8
		public ChainValidityStatus TlsRemoteCertificateChainValidationStatus
		{
			get
			{
				if (this.secureState == SecureState.None)
				{
					return ChainValidityStatus.Valid;
				}
				if (this.tlsRemoteCertificateChainValidationStatus == null)
				{
					this.tlsRemoteCertificateChainValidationStatus = new ChainValidityStatus?(Util.CalculateTlsRemoteCertificateChainValidationStatus(this.SmtpInServer.Configuration.AppConfig.SecureMail.ClientCertificateChainValidationEnabled, this.server.CertificateValidator, this.TlsRemoteCertificate, this.logSession, this.eventLogger));
				}
				return this.tlsRemoteCertificateChainValidationStatus.Value;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06003770 RID: 14192 RVA: 0x000E2D3E File Offset: 0x000E0F3E
		public AgentLatencyTracker AgentLatencyTracker
		{
			get
			{
				return this.AgentSession.LatencyTracker;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x000E2D4B File Offset: 0x000E0F4B
		public SmtpSession SessionSource
		{
			get
			{
				return this.AgentSession.SessionSource;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x000E2D58 File Offset: 0x000E0F58
		// (set) Token: 0x06003773 RID: 14195 RVA: 0x000E2D6A File Offset: 0x000E0F6A
		public TransportMailItemWrapper TransportMailItemWrapper
		{
			get
			{
				if (this.transportMailItem != null)
				{
					return this.mailItemWrapper;
				}
				return null;
			}
			set
			{
				this.mailItemWrapper = value;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000E2D73 File Offset: 0x000E0F73
		// (set) Token: 0x06003775 RID: 14197 RVA: 0x000E2D7B File Offset: 0x000E0F7B
		public bool SendAsRequiredADLookup
		{
			get
			{
				return this.sendAsRequiredADLookup;
			}
			set
			{
				this.sendAsRequiredADLookup = value;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x000E2D84 File Offset: 0x000E0F84
		public ISmtpInServer SmtpInServer
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000E2D8C File Offset: 0x000E0F8C
		public IMessageThrottlingManager MessageThrottlingManager
		{
			get
			{
				return this.messageThrottlingManager;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000E2D94 File Offset: 0x000E0F94
		public IQueueQuotaComponent QueueQuotaComponent
		{
			get
			{
				return this.queueQuotaComponent;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000E2D9C File Offset: 0x000E0F9C
		public IIsMemberOfResolver<RoutingAddress> IsMemberOfResolver
		{
			get
			{
				return this.memberOfResolver;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x000E2DA4 File Offset: 0x000E0FA4
		public ISmtpAgentSession AgentSession
		{
			get
			{
				return this.agentSession;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x000E2DAC File Offset: 0x000E0FAC
		public ReceiveConnector Connector
		{
			get
			{
				return this.connector;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x0600377C RID: 14204 RVA: 0x000E2DB4 File Offset: 0x000E0FB4
		public SmtpReceiveConnectorStub ConnectorStub
		{
			get
			{
				return this.connectorStub;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x000E2DC3 File Offset: 0x000E0FC3
		public string AdvertisedDomain
		{
			get
			{
				return Util.AdvertisedDomainFromReceiveConnector(this.Connector, () => ComputerInformation.DnsPhysicalFullyQualifiedDomainName);
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x0600377E RID: 14206 RVA: 0x000E2DED File Offset: 0x000E0FED
		public bool IsExternalAuthoritative
		{
			get
			{
				return this.RemoteIdentity == WellKnownSids.ExternallySecuredServers;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600377F RID: 14207 RVA: 0x000E2DFF File Offset: 0x000E0FFF
		// (set) Token: 0x06003780 RID: 14208 RVA: 0x000E2E07 File Offset: 0x000E1007
		public Permission SessionPermissions
		{
			get
			{
				return this.sessionPermissions;
			}
			set
			{
				this.sessionPermissions = value;
				this.LogInformation(ProtocolLoggingLevel.Verbose, "Set Session Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(this.sessionPermissions)));
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06003781 RID: 14209 RVA: 0x000E2E2C File Offset: 0x000E102C
		public Permission Permissions
		{
			get
			{
				Permission permission = this.sessionPermissions;
				if (this.TransportAppConfig != null && this.TransportAppConfig.SmtpReceiveConfiguration.GrantExchangeServerPermissions)
				{
					permission |= (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
				}
				return (permission | this.MailItemPermissionsGranted) & ~this.MailItemPermissionsDenied;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x000E2E72 File Offset: 0x000E1072
		// (set) Token: 0x06003783 RID: 14211 RVA: 0x000E2E7A File Offset: 0x000E107A
		public TransportMiniRecipient AuthUserRecipient
		{
			get
			{
				return this.authUserRecipient;
			}
			set
			{
				this.authUserRecipient = value;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000E2E83 File Offset: 0x000E1083
		// (set) Token: 0x06003785 RID: 14213 RVA: 0x000E2E8B File Offset: 0x000E108B
		public SecurityIdentifier RemoteIdentity
		{
			get
			{
				return this.sessionRemoteIdentity;
			}
			set
			{
				this.sessionRemoteIdentity = value;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06003786 RID: 14214 RVA: 0x000E2E94 File Offset: 0x000E1094
		// (set) Token: 0x06003787 RID: 14215 RVA: 0x000E2E9C File Offset: 0x000E109C
		public WindowsIdentity RemoteWindowsIdentity
		{
			get
			{
				return this.remoteWindowsIdentity;
			}
			set
			{
				this.remoteWindowsIdentity = value;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06003788 RID: 14216 RVA: 0x000E2EA5 File Offset: 0x000E10A5
		// (set) Token: 0x06003789 RID: 14217 RVA: 0x000E2EAD File Offset: 0x000E10AD
		public string RemoteIdentityName
		{
			get
			{
				return this.sessionRemoteIdentityName;
			}
			set
			{
				this.sessionRemoteIdentityName = value;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600378A RID: 14218 RVA: 0x000E2EB6 File Offset: 0x000E10B6
		// (set) Token: 0x0600378B RID: 14219 RVA: 0x000E2EBE File Offset: 0x000E10BE
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.sessionAuthMethod;
			}
			set
			{
				this.sessionAuthMethod = value;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600378C RID: 14220 RVA: 0x000E2EC7 File Offset: 0x000E10C7
		// (set) Token: 0x0600378D RID: 14221 RVA: 0x000E2ECF File Offset: 0x000E10CF
		public bool SeenHelo
		{
			get
			{
				return this.seenHelo;
			}
			set
			{
				this.seenHelo = value;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600378E RID: 14222 RVA: 0x000E2ED8 File Offset: 0x000E10D8
		// (set) Token: 0x0600378F RID: 14223 RVA: 0x000E2EE0 File Offset: 0x000E10E0
		public bool SeenEhlo
		{
			get
			{
				return this.seenEhlo;
			}
			set
			{
				this.seenEhlo = value;
				if (value)
				{
					this.isLastCommandEhloBeforeQuit = true;
				}
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x000E2EF3 File Offset: 0x000E10F3
		// (set) Token: 0x06003791 RID: 14225 RVA: 0x000E2EFB File Offset: 0x000E10FB
		public bool SeenRcpt2 { get; set; }

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000E2F04 File Offset: 0x000E1104
		// (set) Token: 0x06003793 RID: 14227 RVA: 0x000E2F11 File Offset: 0x000E1111
		public string HelloSmtpDomain
		{
			get
			{
				return this.SessionSource.HelloDomain;
			}
			set
			{
				this.SessionSource.HelloDomain = value;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000E2F1F File Offset: 0x000E111F
		internal int MessagesSubmitted
		{
			get
			{
				return this.messagesSubmitted;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000E2F27 File Offset: 0x000E1127
		// (set) Token: 0x06003796 RID: 14230 RVA: 0x000E2F2F File Offset: 0x000E112F
		public int LogonFailures
		{
			get
			{
				return this.logonFailures;
			}
			set
			{
				this.logonFailures = value;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x000E2F38 File Offset: 0x000E1138
		public int MaxLogonFailures
		{
			get
			{
				return this.Connector.MaxLogonFailures;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06003798 RID: 14232 RVA: 0x000E2F45 File Offset: 0x000E1145
		// (set) Token: 0x06003799 RID: 14233 RVA: 0x000E2F4D File Offset: 0x000E114D
		public bool TarpitRset
		{
			get
			{
				return this.tarpitRset;
			}
			set
			{
				this.tarpitRset = value;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x0600379A RID: 14234 RVA: 0x000E2F56 File Offset: 0x000E1156
		public SecureState SecureState
		{
			get
			{
				return this.secureState;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x0600379B RID: 14235 RVA: 0x000E2F5E File Offset: 0x000E115E
		// (set) Token: 0x0600379C RID: 14236 RVA: 0x000E2F66 File Offset: 0x000E1166
		public SmtpInBdatState BdatState
		{
			get
			{
				return this.bdatState;
			}
			set
			{
				this.bdatState = value;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000E2F6F File Offset: 0x000E116F
		public bool IsBdatOngoing
		{
			get
			{
				return this.bdatState != null;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000E2F7D File Offset: 0x000E117D
		public TransportMailItem TransportMailItem
		{
			get
			{
				return this.transportMailItem;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000E2F85 File Offset: 0x000E1185
		public MimeDocument MimeDocument
		{
			get
			{
				if (this.transportMailItem == null)
				{
					return null;
				}
				return this.transportMailItem.MimeDocument;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000E2F9C File Offset: 0x000E119C
		public Stream MessageWriteStream
		{
			get
			{
				return this.messageWriteStream;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x060037A1 RID: 14241 RVA: 0x000E2FA4 File Offset: 0x000E11A4
		public bool IsXexch50Received
		{
			get
			{
				return this.inboundExch50 != null;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000E2FB2 File Offset: 0x000E11B2
		public bool StartTlsSupported
		{
			get
			{
				return this.AdvertisedTlsCertificate != null && !this.DisableStartTls;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060037A3 RID: 14243 RVA: 0x000E2FC7 File Offset: 0x000E11C7
		public bool AnonymousTlsSupported
		{
			get
			{
				return this.InternalTransportCertificate != null && !this.Connector.SuppressXAnonymousTls;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000E2FE1 File Offset: 0x000E11E1
		public ISmtpReceivePerfCounters SmtpReceivePerformanceCounters
		{
			get
			{
				return this.smtpReceivePerfCountersInstance;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060037A5 RID: 14245 RVA: 0x000E2FEC File Offset: 0x000E11EC
		public IInboundProxyDestinationPerfCounters InboundProxyDestinationPerfCounters
		{
			get
			{
				string instanceName;
				if (this.transportAppConfig.SmtpInboundProxyConfiguration.InboundProxyDestinationTrackingEnabled && Util.TryGetNextHopFqdnProperty(this.TransportMailItem.ExtendedPropertyDictionary, out instanceName))
				{
					return new InboundProxyDestinationPerfCountersWrapper(instanceName);
				}
				return new NullInboundProxyDestinationPerfCounters();
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060037A6 RID: 14246 RVA: 0x000E302B File Offset: 0x000E122B
		public IInboundProxyDestinationPerfCounters InboundProxyAccountForestPerfCounters
		{
			get
			{
				if (this.transportAppConfig.SmtpInboundProxyConfiguration.InboundProxyAccountForestTrackingEnabled && this.TransportMailItem.ExoAccountForest != null)
				{
					return new InboundProxyAccountForestPerfCountersWrapper(this.TransportMailItem.ExoAccountForest);
				}
				return new NullInboundProxyDestinationPerfCounters();
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000E3062 File Offset: 0x000E1262
		internal SmtpProxyPerfCountersWrapper SmtpProxyPerformanceCounters
		{
			get
			{
				return this.smtpProxyPerfCounters;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060037A8 RID: 14248 RVA: 0x000E306A File Offset: 0x000E126A
		// (set) Token: 0x060037A9 RID: 14249 RVA: 0x000E3072 File Offset: 0x000E1272
		public InboundExch50 InboundExch50
		{
			get
			{
				return this.inboundExch50;
			}
			set
			{
				this.inboundExch50 = value;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060037AA RID: 14250 RVA: 0x000E307B File Offset: 0x000E127B
		// (set) Token: 0x060037AB RID: 14251 RVA: 0x000E3083 File Offset: 0x000E1283
		public int TooManyRecipientsResponseCount
		{
			get
			{
				return this.tooManyRecipientsResponseCount;
			}
			set
			{
				this.tooManyRecipientsResponseCount = value;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000E308C File Offset: 0x000E128C
		public byte[] TlsEapKey
		{
			get
			{
				return this.connection.TlsEapKey;
			}
		}

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000E3099 File Offset: 0x000E1299
		public int TlsCipherKeySize
		{
			get
			{
				return this.connection.TlsCipherKeySize;
			}
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000E30A6 File Offset: 0x000E12A6
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x000E30AE File Offset: 0x000E12AE
		public IMExSession MexSession
		{
			get
			{
				return this.mexSession;
			}
			set
			{
				this.mexSession = value;
			}
		}

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x000E30B8 File Offset: 0x000E12B8
		public string CurrentMessageTemporaryId
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0:X16};{1:yyyy-MM-ddTHH\\:mm\\:ss.fffZ};{2}", new object[]
				{
					this.SessionId,
					this.sessionStartTime,
					this.numberOfMessagesReceived
				});
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000E3106 File Offset: 0x000E1306
		public DateTime SessionStartTime
		{
			get
			{
				return this.sessionStartTime;
			}
		}

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000E310E File Offset: 0x000E130E
		public int NumberOfMessagesReceived
		{
			get
			{
				return this.numberOfMessagesReceived;
			}
		}

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000E3116 File Offset: 0x000E1316
		// (set) Token: 0x060037B4 RID: 14260 RVA: 0x000E311E File Offset: 0x000E131E
		public string DestinationTrackerLastNextHopFqdn { get; set; }

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000E3127 File Offset: 0x000E1327
		// (set) Token: 0x060037B6 RID: 14262 RVA: 0x000E312F File Offset: 0x000E132F
		public string DestinationTrackerLastExoAccountForest { get; set; }

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000E3138 File Offset: 0x000E1338
		internal MailDirectionality Directionality
		{
			get
			{
				return this.TransportMailItem.Directionality;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000E3145 File Offset: 0x000E1345
		public ExEventLog EventLogger
		{
			get
			{
				return this.eventLogger;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000E314D File Offset: 0x000E134D
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x000E3158 File Offset: 0x000E1358
		public bool DisableStartTls
		{
			get
			{
				return this.startTlsDisabled;
			}
			set
			{
				if (value && this.SecureState == SecureState.StartTls)
				{
					throw new InvalidOperationException("Cannnot disable STARTTLS since session is already secure");
				}
				this.startTlsDisabled = value;
				if (this.startTlsDisabled && this.transportAppConfig.SmtpReceiveConfiguration.BlockedSessionLoggingEnabled)
				{
					this.LogSession.ProtocolLoggingLevel = ProtocolLoggingLevel.None;
					this.logSession.LogDisconnect(DisconnectReason.SuppressLogging);
				}
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000E31B5 File Offset: 0x000E13B5
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000E31BD File Offset: 0x000E13BD
		public bool ForceRequestClientTlsCertificate
		{
			get
			{
				return this.forceRequestClientTlsCertificate;
			}
			set
			{
				this.forceRequestClientTlsCertificate = value;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060037BD RID: 14269 RVA: 0x000E31C6 File Offset: 0x000E13C6
		public uint XProxyFromSeqNum
		{
			get
			{
				return this.xProxyFromSeqNum;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000E31CE File Offset: 0x000E13CE
		public IShadowRedundancyManager ShadowRedundancyManagerObject
		{
			get
			{
				return this.shadowRedundancyManager;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000E31D6 File Offset: 0x000E13D6
		// (set) Token: 0x060037C0 RID: 14272 RVA: 0x000E31DE File Offset: 0x000E13DE
		public IShadowSession ShadowSession
		{
			get
			{
				return this.shadowSession;
			}
			set
			{
				this.shadowSession = value;
			}
		}

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000E31E7 File Offset: 0x000E13E7
		public bool SupportIntegratedAuth
		{
			get
			{
				return this.supportIntegratedAuth && (this.Connector.AuthMechanism & AuthMechanisms.Integrated) != AuthMechanisms.None;
			}
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060037C2 RID: 14274 RVA: 0x000E3206 File Offset: 0x000E1406
		public AuthenticationSource AuthenticationSourceForAgents
		{
			get
			{
				if (this.proxiedClientAuthSource != null)
				{
					return this.proxiedClientAuthSource.Value;
				}
				if (SmtpInSessionUtils.IsAnonymous(this.RemoteIdentity))
				{
					return AuthenticationSource.Anonymous;
				}
				if (!SmtpInSessionUtils.IsPartner(this.RemoteIdentity))
				{
					return AuthenticationSource.Organization;
				}
				return AuthenticationSource.Partner;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000E3240 File Offset: 0x000E1440
		public Permission ProxiedClientPermissions
		{
			get
			{
				if (this.proxiedClientPermissions == null)
				{
					return this.Permissions;
				}
				return (Permission)this.proxiedClientPermissions.Value;
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000E3261 File Offset: 0x000E1461
		private TimeSpan MaxAcknowledgementDelay
		{
			get
			{
				return this.Connector.MaxAcknowledgementDelay;
			}
		}

		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000E3273 File Offset: 0x000E1473
		public Breadcrumbs<SmtpInSessionBreadcrumbs> Breadcrumbs
		{
			get
			{
				return this.breadcrumbs;
			}
		}

		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000E327B File Offset: 0x000E147B
		// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000E3283 File Offset: 0x000E1483
		public MailCommandMessageContextParameters MailCommandMessageContextInformation
		{
			get
			{
				return this.mailCommandMessageContextInformation;
			}
			set
			{
				this.mailCommandMessageContextInformation = value;
			}
		}

		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060037C8 RID: 14280 RVA: 0x000E328C File Offset: 0x000E148C
		public IAuthzAuthorization AuthzAuthorization
		{
			get
			{
				return this.authzAuthorization;
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000E3294 File Offset: 0x000E1494
		public ISmtpMessageContextBlob MessageContextBlob
		{
			get
			{
				return this.smtpMessageContextBlob;
			}
		}

		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000E329C File Offset: 0x000E149C
		public bool IsDataRedactionNecessary
		{
			get
			{
				return Util.IsDataRedactionNecessary();
			}
		}

		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060037CB RID: 14283 RVA: 0x000E32A3 File Offset: 0x000E14A3
		public Permission AnonymousPermissions
		{
			get
			{
				return this.DetermineAnonymousPermissions();
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060037CC RID: 14284 RVA: 0x000E32AB File Offset: 0x000E14AB
		public Permission PartnerPermissions
		{
			get
			{
				return this.DeterminePartnerPermissions();
			}
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x000E32B3 File Offset: 0x000E14B3
		public ITracer Tracer
		{
			get
			{
				return ExTraceGlobals.SmtpReceiveTracer;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060037CE RID: 14286 RVA: 0x000E32BA File Offset: 0x000E14BA
		// (set) Token: 0x060037CF RID: 14287 RVA: 0x000E32C2 File Offset: 0x000E14C2
		public bool SmtpUtf8Supported
		{
			get
			{
				return this.smtpUtf8Supported;
			}
			set
			{
				this.smtpUtf8Supported = value;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060037D0 RID: 14288 RVA: 0x000E32CB File Offset: 0x000E14CB
		private Guid AuthUserMailboxGuid
		{
			get
			{
				if (this.authUserRecipient != null)
				{
					return this.authUserRecipient.ExchangeGuid;
				}
				return Guid.Empty;
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000E32E8 File Offset: 0x000E14E8
		public static bool DelayedAckCompletedCallback(object state, DelayedAckCompletionStatus status, TimeSpan delay, string context)
		{
			if (state == null)
			{
				throw new ArgumentNullException("state");
			}
			SmtpInSession smtpInSession = (SmtpInSession)state;
			bool result;
			if (smtpInSession.delayedAckStatus == SmtpInSession.DelayedAckStatus.WaitingForShadowRedundancyManager)
			{
				string text;
				switch (status)
				{
				case DelayedAckCompletionStatus.Delivered:
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayedAckCompletedByDelivery);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}).DelayedAckCompletedCallback: SRM relay notification received.", smtpInSession.connectionId);
					text = "Delivered";
					break;
				case DelayedAckCompletionStatus.Expired:
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayedAckCompletedByExpiry);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}).DelayedAckCompletedCallback: SRM expiry notification received.", smtpInSession.connectionId);
					text = "Expired";
					break;
				case DelayedAckCompletionStatus.Skipped:
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayedAckCompletedBySkipping);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}).DelayedAckCompletedCallback: SRM skipping notification received.", smtpInSession.connectionId);
					text = "Skipped";
					break;
				default:
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported completion status '{0}'.", new object[]
					{
						status
					}));
				}
				if (!string.IsNullOrEmpty(context))
				{
					text = text + ";" + context;
				}
				smtpInSession.LogTarpitEvent(delay, "DelayedAck", text);
				smtpInSession.DelayResponseCompleted(null);
				result = true;
			}
			else
			{
				if (smtpInSession.delayedAckStatus != SmtpInSession.DelayedAckStatus.ShadowRedundancyManagerNotified)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Session '{0}': Got a DelayedAckCompletedCallback() {1} notification for a session with status '{2}'.", new object[]
					{
						smtpInSession.SessionId,
						status,
						smtpInSession.delayedAckStatus
					}));
				}
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, DelayedAckCompletionStatus>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}).DelayedAckCompletedCallback: SRM {1} notification received too early, retry later.", smtpInSession.connectionId, status);
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayedAckCompletedTooEarly);
				result = false;
			}
			return result;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000E3494 File Offset: 0x000E1694
		public void Start()
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.Start);
			if (this.server.RejectCommands)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected: reject commands", this.connectionId);
				if (this.server.RejectionSmtpResponse.Equals(SmtpResponse.InsufficientResource))
				{
					this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToBackPressure);
				}
				this.WriteLineWithLogThenShutdown(this.server.RejectionSmtpResponse);
				return;
			}
			if (this.connector == null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected: no connector mapping found", this.connectionId);
				this.WriteLineWithLogThenShutdown(SmtpResponse.ServiceUnavailable);
				return;
			}
			MultiValuedProperty<IPRange> internalSMTPServers = this.SmtpInServer.TransportSettings.InternalSMTPServers;
			if (this.SmtpInServer.ServerConfiguration.AntispamAgentsEnabled && MultiValuedPropertyBase.IsNullOrEmpty(internalSMTPServers))
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected: list of internal SMTP servers is empty", this.connectionId);
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_InternalSMTPServerListEmpty, this.Connector.Name, new object[]
				{
					this.Connector.Name,
					this.Connector.MaxInboundConnection
				});
			}
			if (this.maxConnectionsExceeded)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, Unlimited<int>>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected, maximum number of connections ({1}) exceeded", this.connectionId, this.Connector.MaxInboundConnection);
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveMaxConnectionReached, this.Connector.Name, new object[]
				{
					this.Connector.Name,
					this.Connector.MaxInboundConnection
				});
				this.WriteLineWithLogThenShutdown(SmtpResponse.TooManyConnections);
				return;
			}
			if (this.maxConnectionsPerSourceExceeded)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected, maximum number of connections per source exceeded ", this.connectionId);
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveMaxConnectionPerSourceReached, this.clientIpAddress.ToString(), new object[]
				{
					this.Connector.Name,
					this.Connector.MaxInboundConnectionPerSource,
					this.clientIpAddress
				});
				this.WriteLineWithLogThenShutdown(SmtpResponse.TooManyConnectionsPerSource);
				return;
			}
			SmtpResponse banner;
			if (SmtpResponse.TryParse(this.Connector.Banner, out banner))
			{
				this.Banner = banner;
			}
			else
			{
				this.Banner = Util.SmtpBanner(this.Connector, () => this.server.Name, this.server.Version, this.server.CurrentTime, false);
			}
			this.firedOnConnectEvent = true;
			this.AgentSession.BeginRaiseEvent("OnConnectEvent", ConnectEventSourceImpl.Create(this.SessionSource), new ConnectEventArgs(this.SessionSource), new AsyncCallback(this.OnConnectCompleted), null);
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000E3760 File Offset: 0x000E1960
		public void SetupSessionToProxyTarget(SmtpSendConnectorConfig outboundProxyConnector, IEnumerable<INextHopServer> outboundProxyDestinations, TlsSendConfiguration outboundProxyTlsSendConfiguration, RiskLevel outboundProxyRiskLevel, int outboundProxyOutboundIPPool, string outboundProxyNextHopDomain, string outboundProxySessionId)
		{
			if (outboundProxyConnector == null)
			{
				throw new ArgumentNullException("outboundProxySendConnector");
			}
			if (outboundProxyDestinations == null)
			{
				throw new ArgumentNullException("outboundProxyDestinations");
			}
			if (!outboundProxyDestinations.GetEnumerator().MoveNext())
			{
				throw new ArgumentException("outboundProxyDestinations cannot be empty");
			}
			if (outboundProxyTlsSendConfiguration == null)
			{
				throw new ArgumentNullException("outboundProxyTlsSendConfiguratio");
			}
			if (this.StartClientProxySession)
			{
				throw new InvalidOperationException("StartOutboundProxySession and StartClientProxySession cannot both be true");
			}
			this.XProxyToParser = null;
			this.startOutboundProxySession = true;
			this.outboundProxyDestinations = outboundProxyDestinations;
			this.outboundProxySendConnector = outboundProxyConnector;
			this.outboundProxyTlsSendConfiguration = outboundProxyTlsSendConfiguration;
			this.outboundProxyRiskLevel = outboundProxyRiskLevel;
			this.outboundProxyOutboundIPPool = outboundProxyOutboundIPPool;
			this.outboundProxyNextHopDomain = outboundProxyNextHopDomain;
			this.outboundProxySessionId = outboundProxySessionId;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000E3808 File Offset: 0x000E1A08
		public void SetupExpectedBlobs(MailCommandMessageContextParameters messageContextParameters)
		{
			if (messageContextParameters == null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "SmtpInSession(id={0}) Not Messagecontext is specified");
				return;
			}
			if (this.expectedBlobs == null)
			{
				this.expectedBlobs = new Queue<SmtpMessageContextBlob>(messageContextParameters.OrderedListOfBlobs.Count);
			}
			foreach (IInboundMessageContextBlob inboundMessageContextBlob in messageContextParameters.OrderedListOfBlobs)
			{
				SmtpMessageContextBlob item = (SmtpMessageContextBlob)inboundMessageContextBlob;
				this.expectedBlobs.Enqueue(item);
			}
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000E38A0 File Offset: 0x000E1AA0
		public void ResetExpectedBlobs()
		{
			if (this.expectedBlobs != null)
			{
				this.expectedBlobs.Clear();
			}
			this.mailCommandMessageContextInformation = null;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000E38BC File Offset: 0x000E1ABC
		public bool ShouldRejectMailItem(RoutingAddress fromAddress, bool checkRecipientCount, out SmtpResponse failureSmtpResponse)
		{
			bool result = CommandParsingHelper.ShouldRejectMailItem(fromAddress, SmtpInSessionState.FromSmtpInSession(this), checkRecipientCount, out failureSmtpResponse);
			if (failureSmtpResponse.Equals(SmtpResponse.InsufficientResource))
			{
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToBackPressure);
			}
			return result;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000E38F0 File Offset: 0x000E1AF0
		public SmtpResponse TrackAndEnqueueMailItem()
		{
			this.UpdateSmtpReceivePerfCountersForMessageReceived(this.transportMailItem.Recipients.Count, this.transportMailItem.MimeSize);
			if (this.transportMailItem.AuthMethod == MultilevelAuthMechanism.MutualTLS)
			{
				Utils.SecureMailPerfCounters.DomainSecureMessagesReceivedTotal.Increment();
			}
			if (!string.IsNullOrEmpty(this.transportMailItem.MessageTrackingSecurityInfo))
			{
				this.msgTrackInfo = new MsgTrackReceiveInfo(this.msgTrackInfo.ClientIPAddress, this.msgTrackInfo.ClientHostname, this.msgTrackInfo.ServerIPAddress, this.msgTrackInfo.SourceContext, this.msgTrackInfo.ConnectorId, this.msgTrackInfo.RelatedMailItemId, this.transportMailItem.MessageTrackingSecurityInfo, this.relatedMessageInfo, string.Empty, this.msgTrackInfo.ProxiedClientIPAddress, this.msgTrackInfo.ProxiedClientHostname, this.transportMailItem.RootPart.Headers.FindAll(HeaderId.Received), this.AuthUserMailboxGuid);
			}
			if (this.transportConfiguration.ProcessTransportRole != ProcessTransportRole.MailboxSubmission && this.transportConfiguration.ProcessTransportRole != ProcessTransportRole.MailboxDelivery)
			{
				MessageTrackingLog.TrackReceive(MessageTrackingSource.SMTP, this.transportMailItem, this.msgTrackInfo);
			}
			if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery)
			{
				this.transportMailItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.MailboxTransport.SmtpInClientHostname", this.msgTrackInfo.ClientHostname);
			}
			LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceive, this.transportMailItem.LatencyTracker);
			this.messagesSubmitted++;
			if (this.delayedAckStatus == SmtpInSession.DelayedAckStatus.Stamped)
			{
				double num = 0.0;
				if (!PoisonMessage.DidMessageCrashTransport(this.transportMailItem.InternetMessageId, out num))
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, Guid>((long)this.GetHashCode(), "SmtpInSession(id={0}).TrackAndEnqueMailItem(): ShadowRedundancyManager notified about delayed ack message '{1}'.", this.connectionId, this.transportMailItem.ShadowMessageId);
					this.server.ShadowRedundancyManager.EnqueueDelayedAckMessage(this.transportMailItem.ShadowMessageId, this, this.server.CurrentTime, this.MaxAcknowledgementDelay);
					this.delayedAckStatus = SmtpInSession.DelayedAckStatus.ShadowRedundancyManagerNotified;
				}
				else
				{
					this.delayedAckStatus = SmtpInSession.DelayedAckStatus.None;
					this.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, string.Format("Disabling Delayed Ack for potential poison message. Internet Message Id:" + this.transportMailItem.InternetMessageId, new object[0]));
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).TrackAndEnqueMailItem: Disabling Delayed Ack for potential poison message.", this.connectionId);
				}
			}
			this.transportMailItem.PerfCounterAttribution = "InQueue";
			return this.server.Categorizer.EnqueueSubmittedMessage(this.transportMailItem);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000E3B60 File Offset: 0x000E1D60
		public void TrackAndEnqueuePeerShadowMailItem()
		{
			this.messagesSubmitted++;
			this.transportMailItem.PerfCounterAttribution = "InQueue";
			this.shadowRedundancyManager.EnqueuePeerShadowMailItem(this.transportMailItem, this.PeerSessionPrimaryServer);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000E3B98 File Offset: 0x000E1D98
		public void ReleaseMailItem()
		{
			try
			{
				if (this.TransportMailItemWrapper != null)
				{
					this.TransportMailItemWrapper.CloseWrapper();
					this.TransportMailItemWrapper = null;
				}
				this.CloseMessageWriteStream();
			}
			catch (IOException)
			{
			}
			finally
			{
				this.messageWriteStream = null;
				if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery && this.transportMailItem != null)
				{
					this.transportMailItem.ReleaseFromActive();
				}
				this.transportMailItem = null;
				this.recipientCorrelator = null;
				this.ResetMailItemPermissions();
				this.tooManyRecipientsResponseCount = 0;
				this.tarpitRset = false;
				this.shadowSession = null;
			}
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000E3C3C File Offset: 0x000E1E3C
		public void UpdateSessionWithProxyInformation(IPAddress clientIp, int clientPort, string clientHelloDomain, bool isAuthenticatedProxy, SecurityIdentifier securityId, string clientIdentityName, WindowsIdentity identity, TransportMiniRecipient recipient, int? capabilitiesInt)
		{
			this.proxiedClientAddress = clientIp;
			if (this.proxyHopHelloDomain == string.Empty)
			{
				this.proxyHopHelloDomain = this.HelloSmtpDomain;
			}
			this.HelloSmtpDomain = clientHelloDomain;
			this.SessionSource.RemoteEndPoint = new IPEndPoint(clientIp, clientPort);
			this.SessionSource.IsExternalConnection = !this.IsTrustedIP(this.proxiedClientAddress);
			this.SessionSource.LastExternalIPAddress = (this.SessionSource.IsExternalConnection ? this.proxiedClientAddress : null);
			if (isAuthenticatedProxy)
			{
				this.secureState = SecureState.StartTls;
				this.tlsDomainCapabilities = new SmtpReceiveCapabilities?(SmtpReceiveCapabilities.None);
				if (capabilitiesInt != null && (capabilitiesInt.Value & 128) == 128)
				{
					this.tlsDomainCapabilities = new SmtpReceiveCapabilities?(SmtpReceiveCapabilities.AcceptXSysProbeProtocol);
				}
				this.inboundClientProxyState = InboundClientProxyStates.XProxyReceived;
				this.SessionSource.IsClientProxiedSession = true;
				this.ResetSessionAuthentication();
				if (securityId != null)
				{
					this.RemoteIdentity = securityId;
					this.inboundClientProxyState = InboundClientProxyStates.XProxyReceivedAndAuthenticated;
					if (!string.IsNullOrEmpty(clientIdentityName))
					{
						this.RemoteIdentityName = clientIdentityName;
					}
					else
					{
						this.RemoteIdentityName = "unknown";
					}
					if (identity != null)
					{
						this.remoteWindowsIdentity = identity;
						this.SetSessionPermissions(identity.Token);
					}
					this.authUserRecipient = recipient;
				}
			}
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000E3D7C File Offset: 0x000E1F7C
		public void UpdateSessionWithProxyFromInformation(IPAddress clientIp, int clientPort, string clientHelloDomain, uint xProxyFromSeqNum, uint? permissionsInt, AuthenticationSource? authSource)
		{
			this.UpdateSessionWithProxyInformation(clientIp, clientPort, clientHelloDomain, false, null, null, null, null, null);
			this.xProxyFromSeqNum = xProxyFromSeqNum;
			this.proxiedClientAuthSource = authSource;
			this.proxiedClientPermissions = permissionsInt;
			if (permissionsInt != null)
			{
				if ((permissionsInt.Value & 64U) == 64U)
				{
					this.sessionPermissions |= Permission.BypassAntiSpam;
				}
				else
				{
					this.sessionPermissions &= ~Permission.BypassAntiSpam;
				}
				this.enforceMimeLimitsForProxiedSession = ((permissionsInt.Value & 128U) == 0U);
			}
			this.sessionPermissions |= Permission.BypassMessageSizeLimit;
			this.SessionSource.IsInboundProxiedSession = true;
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000E3E24 File Offset: 0x000E2024
		public void Shutdown()
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.Shutdown);
			if (this.transportAppConfig.SmtpInboundProxyConfiguration.TrackInboundProxyDestinationsInRcpt)
			{
				if (this.DestinationTrackerLastNextHopFqdn != null)
				{
					this.SmtpInServer.InboundProxyDestinationTracker.DecrementProxyCount(this.DestinationTrackerLastNextHopFqdn);
				}
				if (this.DestinationTrackerLastExoAccountForest != null)
				{
					this.SmtpInServer.InboundProxyAccountForestTracker.DecrementProxyCount(this.DestinationTrackerLastExoAccountForest);
				}
			}
			bool flag = this.SessionSource.ShouldDisconnect && !this.disconnectByServer;
			if (!flag && this.firedOnConnectEvent)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaiseOnDisconnectEvent);
				this.AgentSession.BeginRaiseEvent("OnDisconnectEvent", DisconnectEventSourceImpl.Create(this.SessionSource), new DisconnectEventArgs(this.SessionSource), new AsyncCallback(this.ShutdownCompletedFromMEx), null);
			}
			else if (flag)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ShutdownAgentDisconnect);
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).Shutdown: An agent decided to drop the connection", this.connectionId);
				ISmtpReceivePerfCounters smtpReceivePerformanceCounters = this.SmtpReceivePerformanceCounters;
				if (smtpReceivePerformanceCounters != null)
				{
					smtpReceivePerformanceCounters.ConnectionsDroppedByAgentsTotal.Increment();
				}
				SmtpResponse response = this.SessionSource.SmtpResponse;
				if (response.Equals(SmtpResponse.Empty))
				{
					response = SmtpResponse.ConnectionDroppedByAgentError;
				}
				this.WriteLineWithLog(response, new AsyncCallback(this.ShutdownCompleted), null, true);
			}
			else
			{
				this.ShutdownCompleted(null);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).Shutdown completed", this.connectionId);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000E3F88 File Offset: 0x000E2188
		public void ShutdownConnection()
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ShutdownConnection);
			this.connection.Shutdown();
			SmtpInSession.BlindProxyContext blindProxyContext = this.blindProxyContext;
			if (blindProxyContext != null)
			{
				blindProxyContext.ProxyConnection.Shutdown();
			}
			this.shutdownConnectionCalled = true;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000E3FC3 File Offset: 0x000E21C3
		public void Disconnect(DisconnectReason disconnectReason)
		{
			this.disconnectByServer = true;
			this.SessionSource.DisconnectReason = disconnectReason;
			this.SessionSource.ShouldDisconnect = true;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000E3FE4 File Offset: 0x000E21E4
		public void HandleBlindProxySetupFailure(SmtpResponse response, bool clientProxy)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.HandleBlindProxySetupFailure);
			if (clientProxy)
			{
				this.ResetSessionAuthentication();
			}
			this.proxySetupHandler.ReleaseReferences();
			this.proxySetupHandler = null;
			this.commandHandler.SmtpResponse = response;
			this.ContinueProcessCommand(true);
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000E4020 File Offset: 0x000E2220
		public void ResetSessionAuthentication()
		{
			this.sessionPermissions = this.AnonymousPermissions;
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Set Session Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(this.sessionPermissions)));
			this.sessionRemoteIdentity = SmtpConstants.AnonymousSecurityIdentifier;
			this.sessionRemoteIdentityName = "anonymous";
			if (this.remoteWindowsIdentity != null)
			{
				this.remoteWindowsIdentity.Dispose();
				this.remoteWindowsIdentity = null;
			}
			this.authUserRecipient = null;
			this.AuthMethod = MultilevelAuthMechanism.None;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000E4094 File Offset: 0x000E2294
		public void HandleBlindProxySetupSuccess(SmtpResponse successfulResponse, NetworkConnection targetConnection, ulong sendSessionId, IProtocolLogSession sendLogSession, bool isClientProxy)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.HandleBlindProxySetupSuccess);
			this.blindProxyContext = new SmtpInSession.BlindProxyContext(this, targetConnection, sendLogSession, sendSessionId);
			this.commandHandler.ParsingStatus = ParsingStatus.Complete;
			this.commandHandler.SmtpResponse = successfulResponse;
			this.proxySetupHandler.ReleaseReferences();
			this.proxySetupHandler = null;
			if (this.ProxyPassword != null)
			{
				this.ProxyPassword.Dispose();
				this.ProxyPassword = null;
			}
			this.blindProxyingAuthenticatedUser = isClientProxy;
			this.ContinueProcessCommand(true);
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000E4110 File Offset: 0x000E2310
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[SmtpInSession: SessionId={0} ConnectionId={1}]", new object[]
			{
				this.SessionId,
				this.connectionId
			});
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000E4150 File Offset: 0x000E2350
		public byte[] GetCertificatePublicKey()
		{
			IX509Certificate2 localCertificate = this.connection.LocalCertificate;
			if (localCertificate != null)
			{
				return localCertificate.GetPublicKey();
			}
			return null;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000E4174 File Offset: 0x000E2374
		public bool IsTrustedIP(IPAddress address)
		{
			MultiValuedProperty<IPRange> internalSMTPServers = this.SmtpInServer.TransportSettings.InternalSMTPServers;
			return Util.IsTrustedIP(address, internalSMTPServers);
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000E419C File Offset: 0x000E239C
		public bool DetermineTlsDomainCapabilities()
		{
			if (this.SecureState != SecureState.StartTls)
			{
				throw new InvalidOperationException("DetermineTlsDomainCapabilities() invoked without STARTTLS");
			}
			if (this.tlsDomainCapabilities != null)
			{
				return true;
			}
			SmtpReceiveCapabilities value;
			if (Util.TryDetermineTlsDomainCapabilities(this.SmtpInServer.CertificateValidator, this.TlsRemoteCertificate, this.TlsRemoteCertificateChainValidationStatus, this.connectorStub, this.LogSession, this.eventLogger, ExTraceGlobals.SmtpReceiveTracer, out value))
			{
				this.tlsDomainCapabilities = new SmtpReceiveCapabilities?(value);
				return true;
			}
			return false;
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000E4212 File Offset: 0x000E2412
		// (set) Token: 0x060037E7 RID: 14311 RVA: 0x000E421A File Offset: 0x000E241A
		public Permission MailItemPermissionsGranted { get; private set; }

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000E4223 File Offset: 0x000E2423
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000E422B File Offset: 0x000E242B
		public Permission MailItemPermissionsDenied { get; private set; }

		// Token: 0x060037EA RID: 14314 RVA: 0x000E4234 File Offset: 0x000E2434
		public void GrantMailItemPermissions(Permission permissions)
		{
			this.MailItemPermissionsGranted |= permissions;
			this.MailItemPermissionsDenied &= ~permissions;
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Granted Mail Item Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(permissions)));
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000E426A File Offset: 0x000E246A
		public void DenyMailItemPermissions(Permission permissions)
		{
			this.MailItemPermissionsGranted &= ~permissions;
			this.MailItemPermissionsDenied |= permissions;
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Denied Mail Item Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(permissions)));
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000E42A0 File Offset: 0x000E24A0
		public void ResetMailItemPermissions()
		{
			this.MailItemPermissionsGranted = Permission.None;
			this.MailItemPermissionsDenied = Permission.None;
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000E42B0 File Offset: 0x000E24B0
		public void RemoveClientIpConnection()
		{
			if (this.connectorStub != null && !this.clientIpConnectionAlreadyRemoved)
			{
				if (this.SmtpInServer.Ipv6ReceiveConnectionThrottlingEnabled)
				{
					this.connectorStub.RemoveConnection(this.significantAddressBytes);
				}
				else
				{
					this.connectorStub.RemoveConnection(this.clientIpAddress);
				}
				this.clientIpConnectionAlreadyRemoved = true;
			}
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000E4330 File Offset: 0x000E2530
		public bool CreateTransportMailItem(OrganizationId mailCommandInternalOrganizationId, Guid mailCommandExternalOrganizationId, MailDirectionality mailCommandDirectionality, string mailCommandExoAccountForest, string mailCommandExoTenantContainer, out SmtpResponse smtpResponse)
		{
			smtpResponse = SmtpResponse.Empty;
			if (this.transportMailItem != null)
			{
				throw new InvalidOperationException("Previous use of transportMailItem was not cleaned up properly.");
			}
			bool result = false;
			try
			{
				ADRecipientCache<TransportMiniRecipient> recipientCache = null;
				Guid externalOrgId = Guid.Empty;
				MailDirectionality directionality = MailDirectionality.Undefined;
				if (this.authUserRecipient != null)
				{
					SmtpAddress primarySmtpAddress = this.authUserRecipient.PrimarySmtpAddress;
					if (primarySmtpAddress.IsValidAddress)
					{
						ProxyAddress proxyAddress = new SmtpProxyAddress(primarySmtpAddress.ToString(), true);
						Result<TransportMiniRecipient> result2 = new Result<TransportMiniRecipient>(this.authUserRecipient, null);
						ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
						{
							recipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 1, this.authUserRecipient.OrganizationId);
						}, 0);
						if (adoperationResult.Succeeded)
						{
							recipientCache.AddCacheEntry(proxyAddress, result2);
							directionality = MailDirectionality.Originating;
							adoperationResult = MultiTenantTransport.TryGetExternalOrgId(this.authUserRecipient.OrganizationId, out externalOrgId);
						}
						switch (adoperationResult.ErrorCode)
						{
						case ADOperationErrorCode.RetryableError:
							MultiTenantTransport.TraceAttributionError("Retriable Error {0} attributing authUserRecipient {1}", new object[]
							{
								adoperationResult.Exception,
								this.authUserRecipient.PrimarySmtpAddress
							});
							smtpResponse = SmtpResponse.HubAttributionTransientFailureInCreateTmi;
							return false;
						case ADOperationErrorCode.PermanentError:
							if (this.transportAppConfig.SmtpReceiveConfiguration.RejectUnscopedMessages)
							{
								MultiTenantTransport.TraceAttributionError("Permanent Error {0} attributing authUserRecipient {1}", new object[]
								{
									adoperationResult.Exception,
									this.authUserRecipient.PrimarySmtpAddress
								});
								smtpResponse = SmtpResponse.HubAttributionFailureInCreateTmi;
								return false;
							}
							MultiTenantTransport.TraceAttributionError("Permanent Error {0} attributing authUserRecipient {1}. Falling back to safe tenant", new object[]
							{
								adoperationResult.Exception,
								this.authUserRecipient.PrimarySmtpAddress
							});
							externalOrgId = MultiTenantTransport.SafeTenantId;
							break;
						}
					}
				}
				this.transportMailItem = TransportMailItem.NewMailItem(recipientCache, LatencyComponent.SmtpReceive, directionality, externalOrgId);
				this.transportMailItem.ExposeMessage = false;
				this.transportMailItem.ExposeMessageHeaders = false;
				this.transportMailItem.PerfCounterAttribution = "SMTPIn";
				if (!this.IsShadowedBySender && this.MaxAcknowledgementDelay > TimeSpan.Zero && this.server.ShadowRedundancyManager != null && this.server.ShadowRedundancyManager.ShouldDelayAck())
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).CreateTransportMailItem: Message stamped as a delayed ack message.", this.connectionId);
					this.transportMailItem.ShadowServerContext = "$localhost$";
					this.transportMailItem.ShadowServerDiscardId = this.transportMailItem.ShadowMessageId.ToString();
					this.delayedAckStatus = SmtpInSession.DelayedAckStatus.Stamped;
				}
				else if (this.IsPeerShadowSession)
				{
					this.transportMailItem.ShadowServerContext = this.shadowRedundancyManager.GetShadowContextForInboundSession();
					this.transportMailItem.ShadowServerDiscardId = this.transportMailItem.ShadowMessageId.ToString();
				}
				else
				{
					this.delayedAckStatus = SmtpInSession.DelayedAckStatus.None;
				}
				string str = "SMTP:";
				this.transportMailItem.ReceiveConnectorName = str + (this.Connector.Name ?? "Unknown");
				this.transportMailItem.SourceIPAddress = ((this.proxiedClientAddress != null) ? this.proxiedClientAddress : this.clientIpAddress);
				this.transportMailItem.AuthMethod = this.sessionAuthMethod;
				this.inboundExch50 = null;
				if (this.ShouldInitializeMessageTrackingInfo())
				{
					IPAddress proxiedClientIPAddress = null;
					string proxiedClientHostname = string.Empty;
					string helloDomain;
					if (this.InboundClientProxyState != InboundClientProxyStates.None || this.IsAnonymousClientProxiedSession)
					{
						helloDomain = this.proxyHopHelloDomain;
						proxiedClientIPAddress = this.proxiedClientAddress;
						proxiedClientHostname = this.HelloDomain;
					}
					else
					{
						helloDomain = this.HelloDomain;
					}
					this.msgTrackInfo = new MsgTrackReceiveInfo(this.connection.RemoteEndPoint.Address, helloDomain, this.connection.LocalEndPoint.Address, this.CurrentMessageTemporaryId, this.connector.Id.ToString(), null, proxiedClientIPAddress, proxiedClientHostname, this.AuthUserMailboxGuid);
				}
				this.recipientCorrelator = new InboundRecipientCorrelator();
				if (!this.SessionSource.IsExternalConnection)
				{
					this.SessionSource.LastExternalIPAddress = null;
				}
				if (mailCommandExternalOrganizationId != Guid.Empty && mailCommandDirectionality != MailDirectionality.Undefined)
				{
					this.TransportMailItem.ExternalOrganizationId = mailCommandExternalOrganizationId;
					this.TransportMailItem.Directionality = mailCommandDirectionality;
					this.TransportMailItem.ExoAccountForest = mailCommandExoAccountForest;
					this.TransportMailItem.ExoTenantContainer = mailCommandExoTenantContainer;
					ADOperationResult adoperationResult2 = SmtpInSessionUtils.TryCreateOrUpdateADRecipientCache(this.transportMailItem, mailCommandInternalOrganizationId, this.LogSession);
					if (adoperationResult2.ErrorCode == ADOperationErrorCode.RetryableError)
					{
						smtpResponse = SmtpResponse.HubAttributionTransientFailureInMailFrom;
						this.transportMailItem = null;
						this.recipientCorrelator = null;
						return false;
					}
				}
				result = true;
			}
			catch (IOException)
			{
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.transportMailItem = null;
				this.recipientCorrelator = null;
				smtpResponse = SmtpResponse.DataTransactionFailed;
			}
			this.numberOfMessagesReceived++;
			return result;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000E4828 File Offset: 0x000E2A28
		public void UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory category)
		{
			if (this.smtpAvailabilityPerfCounters != null)
			{
				this.smtpAvailabilityPerfCounters.UpdatePerformanceCounters(category);
			}
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000E483E File Offset: 0x000E2A3E
		public void IncrementSmtpAvailabilityPerfCounterForMessageLoopsInLastHour(long incrementValue)
		{
			if (this.smtpAvailabilityPerfCounters != null)
			{
				this.smtpAvailabilityPerfCounters.IncrementMessageLoopsInLastHourCounter(incrementValue);
			}
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000E4854 File Offset: 0x000E2A54
		public void UpdateSmtpReceivePerfCountersForMessageReceived(int recipients, long messageBytes)
		{
			ISmtpReceivePerfCounters smtpReceivePerformanceCounters = this.SmtpReceivePerformanceCounters;
			if (smtpReceivePerformanceCounters != null)
			{
				smtpReceivePerformanceCounters.MessagesReceivedTotal.Increment();
				smtpReceivePerformanceCounters.RecipientsAccepted.IncrementBy((long)recipients);
				smtpReceivePerformanceCounters.MessageBytesReceivedTotal.IncrementBy(messageBytes);
			}
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000E4894 File Offset: 0x000E2A94
		public void UpdateInboundProxyDestinationPerfCountersForMessageReceived(int recipients, long messageBytes)
		{
			IInboundProxyDestinationPerfCounters inboundProxyDestinationPerfCounters = this.InboundProxyDestinationPerfCounters;
			inboundProxyDestinationPerfCounters.MessagesReceivedTotal.Increment();
			inboundProxyDestinationPerfCounters.RecipientsAccepted.IncrementBy((long)recipients);
			inboundProxyDestinationPerfCounters.MessageBytesReceivedTotal.IncrementBy(messageBytes);
			IInboundProxyDestinationPerfCounters inboundProxyAccountForestPerfCounters = this.InboundProxyAccountForestPerfCounters;
			inboundProxyAccountForestPerfCounters.MessagesReceivedTotal.Increment();
			inboundProxyAccountForestPerfCounters.RecipientsAccepted.IncrementBy((long)recipients);
			inboundProxyAccountForestPerfCounters.MessageBytesReceivedTotal.IncrementBy(messageBytes);
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000E48FD File Offset: 0x000E2AFD
		public void DeleteTransportMailItem()
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DeleteTransportMailItem);
			if (this.transportMailItem != null && !this.transportMailItem.IsNew && this.transportMailItem.IsActive)
			{
				this.TransportMailItem.ReleaseFromActiveMaterializedLazy();
			}
			this.ReleaseMailItem();
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000E493C File Offset: 0x000E2B3C
		public void AbortMailTransaction()
		{
			if (this.shadowSession != null)
			{
				this.shadowSession.Close(AckStatus.Fail, SmtpResponse.Empty);
			}
			if (this.transportMailItem != null)
			{
				this.DeleteTransportMailItem();
			}
			SmtpInBdatState smtpInBdatState = this.BdatState;
			if (smtpInBdatState != null && smtpInBdatState.ProxyLayer != null)
			{
				smtpInBdatState.ProxyLayer.NotifySmtpInStopProxy();
			}
			this.BdatState = null;
			this.shadowSession = null;
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000E499C File Offset: 0x000E2B9C
		public Stream OpenMessageWriteStream(bool expectBinaryContent)
		{
			if (this.transportMailItem == null)
			{
				throw new InvalidOperationException("No transport message");
			}
			MimeLimits mimeLimits;
			if (SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(this.Permissions) && !this.enforceMimeLimitsForProxiedSession)
			{
				mimeLimits = MimeLimits.Unlimited;
			}
			else
			{
				mimeLimits = MimeLimits.Default;
			}
			this.messageWriteStream = this.transportMailItem.OpenMimeWriteStream(mimeLimits, expectBinaryContent);
			return this.messageWriteStream;
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000E49F8 File Offset: 0x000E2BF8
		public void CloseMessageWriteStream()
		{
			Util.CloseMessageWriteStream(this.messageWriteStream, this.transportMailItem, ExTraceGlobals.SmtpReceiveTracer, this.GetHashCode());
			this.messageWriteStream = null;
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000E4A20 File Offset: 0x000E2C20
		public void PutBackReceivedBytes(int bytesUnconsumed)
		{
			ISmtpReceivePerfCounters smtpReceivePerformanceCounters = this.SmtpReceivePerformanceCounters;
			if (smtpReceivePerformanceCounters != null)
			{
				smtpReceivePerformanceCounters.TotalBytesReceived.IncrementBy((long)(-(long)bytesUnconsumed));
			}
			this.connection.PutBackReceivedBytes(bytesUnconsumed);
		}

		// Token: 0x060037F8 RID: 14328 RVA: 0x000E4A54 File Offset: 0x000E2C54
		public void RawDataReceivedCompleted()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, string>((long)this.GetHashCode(), "SmtpInSession(id={0}).RawDataReceivedCompleted. ParsingStatus = {1}", this.connectionId, this.commandHandler.ParsingStatus.ToString());
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.RawDataReceivedCompleted);
			if (this.SessionSource.ShouldDisconnect)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DisconnectFromRawDataReceivedCompleted);
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).RawDataReceivedCompleted calling Shutdown", this.connectionId);
				SmtpResponse response = SmtpResponse.Empty;
				if (this.commandHandler != null)
				{
					response = this.commandHandler.SmtpResponse;
				}
				this.DisposeSmtpCommand();
				this.rawDataHandler = null;
				if (this.disconnectByServer && !response.Equals(SmtpResponse.Empty))
				{
					this.WriteLineWithLogThenShutdown(response);
					return;
				}
				this.Shutdown();
				return;
			}
			else
			{
				if (this.commandHandler.ParsingStatus == ParsingStatus.MoreDataRequired)
				{
					this.StartRead();
					return;
				}
				this.rawDataHandler = null;
				this.DelayResponseIfNecessary(true);
				return;
			}
		}

		// Token: 0x060037F9 RID: 14329 RVA: 0x000E4B3A File Offset: 0x000E2D3A
		public void SetRawModeAfterCommandCompleted(RawDataHandler rawDataHandler)
		{
			this.rawDataHandler = rawDataHandler;
		}

		// Token: 0x060037FA RID: 14330 RVA: 0x000E4B43 File Offset: 0x000E2D43
		public void LogInformation(ProtocolLoggingLevel loggingLevel, string information, byte[] data)
		{
			this.logSession.LogInformation(loggingLevel, data, information);
		}

		// Token: 0x060037FB RID: 14331 RVA: 0x000E4B53 File Offset: 0x000E2D53
		public void StartTls(SecureState secureState)
		{
			this.secureState = (secureState | SecureState.NegotiationRequested);
		}

		// Token: 0x060037FC RID: 14332 RVA: 0x000E4B64 File Offset: 0x000E2D64
		public IAsyncResult RaiseOnRejectEvent(byte[] command, EventArgs originalEventArgs, SmtpResponse smtpResponse, AsyncCallback callback)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaiseOnRejectEvent);
			RejectEventArgs rejectEventArgs = new RejectEventArgs(this.SessionSource);
			rejectEventArgs.RawCommand = command;
			rejectEventArgs.ParsingStatus = EnumConverter.InternalToPublic(this.commandHandler.ParsingStatus);
			rejectEventArgs.OriginalArguments = originalEventArgs;
			rejectEventArgs.SmtpResponse = smtpResponse;
			return this.AgentSession.BeginRaiseEvent("OnReject", RejectEventSourceImpl.Create(this.SessionSource), rejectEventArgs, callback, null);
		}

		// Token: 0x060037FD RID: 14333 RVA: 0x000E4BCF File Offset: 0x000E2DCF
		public byte[] GetTlsEapKey()
		{
			return this.connection.TlsEapKey;
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000E4BDC File Offset: 0x000E2DDC
		internal void SetSessionPermissions(SecurityIdentifier client)
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2347117885U);
			this.sessionPermissions = SmtpInSessionUtils.GetPermissions(this.authzAuthorization, client, this.connectorStub.SecurityDescriptor);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client '{0}' is granted the following permissions: {1}", this.RemoteIdentityName, this.sessionPermissions);
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Set Session Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(this.sessionPermissions)));
		}

		// Token: 0x060037FF RID: 14335 RVA: 0x000E4C54 File Offset: 0x000E2E54
		public void SetSessionPermissions(IntPtr userToken)
		{
			this.sessionPermissions = SmtpInSessionUtils.GetPermissions(this.authzAuthorization, userToken, this.connectorStub.SecurityDescriptor);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client '{0}' is granted the following permissions: {1}", this.RemoteIdentityName, this.sessionPermissions);
			this.LogInformation(ProtocolLoggingLevel.Verbose, "Set Session Permissions", Util.AsciiStringToBytes(Util.GetPermissionString(this.sessionPermissions)));
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000E4CBC File Offset: 0x000E2EBC
		public void AddSessionPermissions(SmtpReceiveCapabilities capabilities)
		{
			this.SessionPermissions = Util.AddSessionPermissions(capabilities, this.SessionPermissions, this.authzAuthorization, this.connectorStub.SecurityDescriptor, this.logSession, ExTraceGlobals.SmtpReceiveTracer, this.GetHashCode());
			if (SmtpInSessionUtils.HasAcceptCrossForestMailCapability(capabilities))
			{
				this.RemoteIdentity = WellKnownSids.ExternallySecuredServers;
				this.RemoteIdentityName = "accepted_domain";
			}
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x000E4D1B File Offset: 0x000E2F1B
		public void DropBreadcrumb(SmtpInSessionBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x000E4D29 File Offset: 0x000E2F29
		public void SetupPoisonContext()
		{
			if (this.transportMailItem != null && !string.IsNullOrEmpty(this.transportMailItem.InternetMessageId))
			{
				PoisonMessage.Context = new MessageContext(this.transportMailItem.RecordId, this.transportMailItem.InternetMessageId, MessageProcessingSource.SmtpReceive);
			}
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x000E4D68 File Offset: 0x000E2F68
		private static void ToShutdown(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			SmtpInSession session = writeCompleteLogCallbackParameters.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.ToShutdown);
			session.SetupPoisonContext();
			session.Shutdown();
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x000E4D9C File Offset: 0x000E2F9C
		private static void StartTlsNegotiation(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			SmtpInSession session = writeCompleteLogCallbackParameters.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartTlsNegotiation);
			session.SetupPoisonContext();
			SmtpInSession smtpInSession = session;
			smtpInSession.secureState &= ~SecureState.NegotiationRequested;
			X509Certificate2 cert = (session.secureState == SecureState.StartTls) ? session.AdvertisedTlsCertificate : session.InternalTransportCertificate;
			session.logSession.LogCertificate("Sending certificate", cert);
			bool requestClientCertificate = false;
			if (session.secureState == SecureState.AnonymousTls)
			{
				requestClientCertificate = true;
			}
			else if (session.secureState == SecureState.StartTls)
			{
				if (session.ForceRequestClientTlsCertificate)
				{
					requestClientCertificate = true;
				}
				else if (session.Connector.DomainSecureEnabled && session.SmtpInServer.TransportSettings.TLSReceiveDomainSecureList.Count > 0)
				{
					requestClientCertificate = true;
				}
				else if (session.ConnectorStub.ContainsTlsDomainCapabilities)
				{
					requestClientCertificate = true;
				}
			}
			session.connection.BeginNegotiateTlsAsServer(cert, requestClientCertificate, SmtpInSession.tlsNegotiationComplete, session);
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x000E4E74 File Offset: 0x000E3074
		private static void BeginReadLine(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			SmtpInSession session = writeCompleteLogCallbackParameters.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.BeginReadLine);
			session.SetupPoisonContext();
			session.StartReadLine();
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x000E4EA8 File Offset: 0x000E30A8
		private static void BeginRead(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			SmtpInSession session = writeCompleteLogCallbackParameters.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.BeginRead);
			session.SetupPoisonContext();
			session.StartRead();
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x000E4EDC File Offset: 0x000E30DC
		private static void StartProxying(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			SmtpInSession session = writeCompleteLogCallbackParameters.Session;
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)writeCompleteLogCallbackParameters.CallbackContextParam;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartProxying);
			if (blindProxyContext == null)
			{
				throw new InvalidOperationException("blindProxyContext is null in StartProxying()");
			}
			session.StartReadFromProxyClient(blindProxyContext);
			session.StartReadFromProxyTarget(blindProxyContext);
		}

		// Token: 0x06003808 RID: 14344 RVA: 0x000E4F2C File Offset: 0x000E312C
		private static void WriteCompleteLogCallback(IAsyncResult asyncResult)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters writeCompleteLogCallbackParameters = (SmtpInSession.WriteCompleteLogCallbackParameters)asyncResult.AsyncState;
			writeCompleteLogCallbackParameters.Session.SetupPoisonContext();
			object obj;
			writeCompleteLogCallbackParameters.Session.connection.EndWrite(asyncResult, out obj);
			if (obj == null)
			{
				if (writeCompleteLogCallbackParameters.ResponseList != null)
				{
					foreach (SmtpResponse response in writeCompleteLogCallbackParameters.ResponseList)
					{
						writeCompleteLogCallbackParameters.Session.WriteLog(response);
					}
				}
				writeCompleteLogCallbackParameters.Callback(asyncResult);
				return;
			}
			if (writeCompleteLogCallbackParameters.AlwaysCall)
			{
				writeCompleteLogCallbackParameters.Callback(asyncResult);
				return;
			}
			if (writeCompleteLogCallbackParameters.CallbackContextParam is SmtpInSession.BlindProxyContext)
			{
				SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = writeCompleteLogCallbackParameters.Session.SmtpProxyPerfCounters;
				if (smtpProxyPerfCountersWrapper != null)
				{
					smtpProxyPerfCountersWrapper.DecrementOutboundConnectionsCurrent();
				}
			}
			writeCompleteLogCallbackParameters.Session.HandleError(obj, false);
		}

		// Token: 0x06003809 RID: 14345 RVA: 0x000E500C File Offset: 0x000E320C
		private static void ReadComplete(IAsyncResult asyncResult)
		{
			SmtpInSession smtpInSession = (SmtpInSession)asyncResult.AsyncState;
			smtpInSession.SetupPoisonContext();
			try
			{
				byte[] data;
				int offset;
				int num;
				object obj;
				smtpInSession.connection.EndRead(asyncResult, out data, out offset, out num, out obj);
				if (obj != null)
				{
					smtpInSession.HandleError(obj, true);
				}
				else if (smtpInSession.sessionExpireTime < smtpInSession.server.CurrentTime)
				{
					smtpInSession.WriteLineWithLogThenShutdown(SmtpResponse.ConnectionTimedOut);
				}
				else
				{
					ISmtpReceivePerfCounters smtpReceivePerformanceCounters = smtpInSession.SmtpReceivePerformanceCounters;
					if (smtpReceivePerformanceCounters != null)
					{
						smtpReceivePerformanceCounters.TotalBytesReceived.IncrementBy((long)num);
					}
					if (smtpInSession.rawDataHandler(data, offset, num) == AsyncReturnType.Sync)
					{
						smtpInSession.RawDataReceivedCompleted();
					}
				}
			}
			catch (Exception ex)
			{
				smtpInSession.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveCatchAll, null, new object[]
				{
					smtpInSession.clientIpAddress,
					ex
				});
				throw;
			}
		}

		// Token: 0x0600380A RID: 14346 RVA: 0x000E50F0 File Offset: 0x000E32F0
		private static void ReadCompleteFromProxyClient(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.ReadCompleteFromProxyClient);
			session.SetupPoisonContext();
			try
			{
				byte[] buffer;
				int offset;
				int num;
				object obj;
				session.connection.EndRead(asyncResult, out buffer, out offset, out num, out obj);
				if (obj != null)
				{
					session.HandleErrorDuringBlindProxying(obj, blindProxyContext, true, true);
				}
				else
				{
					ISmtpReceivePerfCounters smtpReceivePerformanceCounters = session.SmtpReceivePerformanceCounters;
					if (smtpReceivePerformanceCounters != null)
					{
						smtpReceivePerformanceCounters.TotalBytesReceived.IncrementBy((long)num);
					}
					if (session.blindProxyingAuthenticatedUser || !session.ReceivedAndProcessedXRsetProxyToCommand(buffer, offset, num, blindProxyContext))
					{
						session.StartWriteToProxyTarget(buffer, offset, num, blindProxyContext);
						session.bytesToBeProxied = num;
					}
				}
			}
			catch (Exception ex)
			{
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					session.clientIpAddress,
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000E51FC File Offset: 0x000E33FC
		private static void WriteToProxyTargetCompleted(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteToProxyTargetCompleted);
			session.SetupPoisonContext();
			try
			{
				object obj;
				blindProxyContext.ProxyConnection.EndWrite(asyncResult, out obj);
				if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyTargetWritePending, 0, 1) != 1)
				{
					throw new InvalidOperationException("A write operation was not pending for proxy target");
				}
				if (obj != null)
				{
					session.HandleErrorDuringBlindProxying(obj, blindProxyContext, false, false);
				}
				else
				{
					SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = session.smtpProxyPerfCounters;
					if (smtpProxyPerfCountersWrapper != null)
					{
						smtpProxyPerfCountersWrapper.UpdateBytesProxied(session.bytesToBeProxied);
					}
					session.bytesToBeProxied = 0;
					if (!session.StartWriteQuitToProxyTargetIfNecessary(blindProxyContext))
					{
						session.StartReadFromProxyClient(blindProxyContext);
					}
				}
			}
			catch (Exception ex)
			{
				NetworkConnection proxyConnection = blindProxyContext.ProxyConnection;
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					(proxyConnection == null) ? "disposed" : proxyConnection.RemoteEndPoint.Address.ToString(),
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x000E5320 File Offset: 0x000E3520
		private static void WriteQuitToProxyTargetCompleted(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteQuitToProxyTargetCompleted);
			session.SetupPoisonContext();
			try
			{
				object obj;
				blindProxyContext.ProxyConnection.EndWrite(asyncResult, out obj);
				if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyTargetWritePending, 0, 1) != 1)
				{
					throw new InvalidOperationException("WriteQuitToProxyTargetCompleted: A write operation was not pending for proxy target");
				}
				blindProxyContext.BlindProxySendLogSession.LogSend(SmtpInSession.QuitCommand);
				blindProxyContext.BlindProxySendLogSession.LogDisconnect(DisconnectReason.QuitVerb);
				ConnectionLog.SmtpConnectionStop(blindProxyContext.BlindProxySendSessionId, string.Empty, string.Empty, 0UL, 0UL, 0UL);
				blindProxyContext.ProxyConnection.Shutdown();
				blindProxyContext.ProxyConnection.Dispose();
				SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = session.SmtpProxyPerfCounters;
				if (smtpProxyPerfCountersWrapper != null)
				{
					smtpProxyPerfCountersWrapper.DecrementOutboundConnectionsCurrent();
				}
			}
			catch (Exception ex)
			{
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					blindProxyContext.ProxyConnection.RemoteEndPoint.Address.ToString(),
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x000E5454 File Offset: 0x000E3654
		private static void ReadCompleteFromProxyTarget(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.ReadCompleteFromProxyTarget);
			if (Util.InterlockedEquals(ref blindProxyContext.BlindProxyWorkDone, 1))
			{
				return;
			}
			session.SetupPoisonContext();
			try
			{
				byte[] buffer;
				int offset;
				int size;
				object obj;
				blindProxyContext.ProxyConnection.EndRead(asyncResult, out buffer, out offset, out size, out obj);
				if (obj != null)
				{
					session.HandleErrorDuringBlindProxying(obj, blindProxyContext, false, true);
				}
				else
				{
					session.StartWriteToProxyClient(buffer, offset, size, blindProxyContext);
				}
			}
			catch (Exception ex)
			{
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					blindProxyContext.ProxyConnection.RemoteEndPoint.Address.ToString(),
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000E5540 File Offset: 0x000E3740
		private static void WriteToProxyClientCompleted(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteToProxyClientCompleted);
			session.SetupPoisonContext();
			try
			{
				object obj;
				session.connection.EndWrite(asyncResult, out obj);
				if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyClientWritePending, 0, 1) != 1)
				{
					throw new InvalidOperationException("A write operation was not pending for proxy client");
				}
				if (obj != null)
				{
					session.HandleErrorDuringBlindProxying(obj, blindProxyContext, true, false);
				}
				else if (!session.StartWriteXRsetResponseToProxyClientIfNecessary(blindProxyContext) && Util.InterlockedEquals(ref blindProxyContext.BlindProxyWorkDone, 0))
				{
					session.StartReadFromProxyTarget(blindProxyContext);
				}
			}
			catch (Exception ex)
			{
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					session.clientIpAddress,
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000E5630 File Offset: 0x000E3830
		private static void WriteXRsetResponseToProxyClientCompleted(IAsyncResult asyncResult)
		{
			SmtpInSession.BlindProxyContext blindProxyContext = (SmtpInSession.BlindProxyContext)asyncResult.AsyncState;
			SmtpInSession session = blindProxyContext.Session;
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteXRsetResponseToProxyClientCompleted);
			session.SetupPoisonContext();
			try
			{
				object obj;
				session.connection.EndWrite(asyncResult, out obj);
				if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyClientWritePending, 0, 1) != 1)
				{
					throw new InvalidOperationException("WriteXRsetResponseToProxyClientCompleted: A write operation was not pending for proxy client");
				}
				if (obj != null)
				{
					session.HandleError(obj, false);
				}
				else
				{
					byte[] data = session.CreateXRsetProxyToAcceptedResponse().ToByteArray();
					session.LogSession.LogSend(data);
					session.StartReadLine();
				}
			}
			catch (Exception ex)
			{
				session.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCatchAll, null, new object[]
				{
					session.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					session.clientIpAddress,
					ex,
					SmtpInSessionUtils.GetBreadcrumbsAsString(session.Breadcrumbs)
				});
				throw;
			}
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x000E572C File Offset: 0x000E392C
		private static void ReadLineComplete(IAsyncResult asyncResult)
		{
			SmtpInSession smtpInSession = (SmtpInSession)asyncResult.AsyncState;
			bool flag = false;
			smtpInSession.SetupPoisonContext();
			try
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ReadLineComplete);
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}).ReadLinecomplete", smtpInSession.connectionId);
				byte[] inputBuffer;
				int offset;
				int num;
				object obj;
				smtpInSession.connection.EndReadLine(asyncResult, out inputBuffer, out offset, out num, out obj);
				if (obj != null)
				{
					if (!(obj is SocketError) || (SocketError)obj != SocketError.MessageSize)
					{
						smtpInSession.HandleError(obj, true);
						return;
					}
					flag = true;
				}
				ISmtpReceivePerfCounters smtpReceivePerformanceCounters = smtpInSession.SmtpReceivePerformanceCounters;
				if (smtpReceivePerformanceCounters != null)
				{
					smtpReceivePerformanceCounters.TotalBytesReceived.IncrementBy((long)(num + (flag ? 0 : 2)));
				}
				if (smtpInSession.sessionExpireTime < smtpInSession.server.CurrentTime)
				{
					smtpInSession.WriteLineWithLogThenShutdown(SmtpResponse.ConnectionTimedOut);
				}
				else if (smtpInSession.server.RejectCommands)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0}) disconnected: reject commands", smtpInSession.connectionId);
					smtpInSession.WriteLineWithLogThenShutdown(smtpInSession.server.RejectionSmtpResponse);
				}
				else if (smtpInSession.StartProcessingCommand(inputBuffer, offset, num, flag) == AsyncReturnType.Sync)
				{
					if (smtpInSession.rawDataHandler == null)
					{
						smtpInSession.StartReadLine();
					}
					else
					{
						smtpInSession.StartRead();
					}
				}
			}
			catch (Exception ex)
			{
				smtpInSession.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveCatchAll, null, new object[]
				{
					smtpInSession.clientIpAddress,
					ex
				});
				throw;
			}
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x000E58B4 File Offset: 0x000E3AB4
		private static void TlsNegotiationComplete(IAsyncResult asyncResult)
		{
			SmtpInSession smtpInSession = (SmtpInSession)asyncResult.AsyncState;
			smtpInSession.SetupPoisonContext();
			try
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.TlsNegotiationComplete);
				object obj;
				smtpInSession.connection.EndNegotiateTlsAsServer(asyncResult, out obj);
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, object>((long)smtpInSession.GetHashCode(), "SmtpInSession(id={0})TlsNegotiationComplete, Status: {1}", smtpInSession.connectionId, obj ?? "OK");
				if (obj != null)
				{
					smtpInSession.smtpReceivePerfCountersInstance.TlsNegotiationsFailed.Increment();
					smtpInSession.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TLS negotiation failed with error {0}", new object[]
					{
						obj
					});
					smtpInSession.SessionSource.DisconnectReason = DisconnectReason.DroppedSession;
					smtpInSession.HandleError(obj, false);
				}
				else
				{
					ConnectionInfo tlsConnectionInfo = smtpInSession.connection.TlsConnectionInfo;
					Util.LogTlsSuccessResult(smtpInSession.logSession, tlsConnectionInfo, smtpInSession.connection.RemoteCertificate);
					smtpInSession.TlsNegotiationComplete();
				}
			}
			catch (Exception ex)
			{
				smtpInSession.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveCatchAll, null, new object[]
				{
					smtpInSession.clientIpAddress,
					ex
				});
				if (smtpInSession.logSession != null)
				{
					smtpInSession.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TLS negotiation failed, fatal exception");
				}
				throw;
			}
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000E59E0 File Offset: 0x000E3BE0
		private void HandleErrorDuringBlindProxying(object error, SmtpInSession.BlindProxyContext blindProxyContext, bool clientError, bool readError)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.HandleErrorDuringBlindProxying);
			if (Interlocked.CompareExchange(ref blindProxyContext.BlindProxyWorkDone, 1, 0) == 1)
			{
				return;
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, object, string>((long)this.GetHashCode(), "SmtpInSession(id={0}).HandleErrorDuringBlindProxying (Error={1}, ClientError={2}).", this.connectionId, error, clientError.ToString());
			if (clientError)
			{
				if (SmtpInSessionUtils.IsRemoteConnectionError(error))
				{
					this.disconnectReason = DisconnectReason.Remote;
					this.remoteConnectionError = error.ToString();
				}
			}
			else if (SmtpInSessionUtils.IsRemoteConnectionError(error))
			{
				this.proxyTargetDisconnectReason = DisconnectReason.Remote;
				this.proxyTargetRemoteConnectionError = error.ToString();
			}
			if (!clientError && readError)
			{
				this.connection.Shutdown(5);
			}
			else
			{
				this.connection.Shutdown();
			}
			blindProxyContext.ProxyConnection.Shutdown();
			SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = this.SmtpProxyPerfCounters;
			if (smtpProxyPerfCountersWrapper != null)
			{
				smtpProxyPerfCountersWrapper.DecrementOutboundConnectionsCurrent();
			}
			this.Shutdown();
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000E5AA8 File Offset: 0x000E3CA8
		private void HandleError(object error, bool receiveError)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.HandleError);
			if (error is SocketError)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpInSession(id={0}).HandleError (socketError={1}) ", this.connectionId, error);
				switch ((SocketError)error)
				{
				case SocketError.Shutdown:
					this.Shutdown(DisconnectReason.Local);
					return;
				case SocketError.TimedOut:
					this.SessionSource.DisconnectReason = DisconnectReason.Timeout;
					if (receiveError)
					{
						this.connection.SendTimeout = 15;
						this.WriteLineWithLogThenShutdown(SmtpResponse.TimeoutOccurred);
						return;
					}
					this.Shutdown(DisconnectReason.Local);
					return;
				}
				this.remoteConnectionError = error.ToString();
				this.Shutdown(DisconnectReason.Remote);
				return;
			}
			if (error is SecurityStatus)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpInSession(id={0}).HandleError (SecurityStatus={1})", this.connectionId, error);
				this.SessionSource.DisconnectReason = DisconnectReason.DroppedSession;
				this.Shutdown(DisconnectReason.Local);
				return;
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpInSession(id={0}).HandleError (error={1})", this.connectionId, error);
			this.SessionSource.DisconnectReason = DisconnectReason.Local;
			this.Shutdown(DisconnectReason.Local);
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x000E5BBC File Offset: 0x000E3DBC
		private void OnConnectCompleted(IAsyncResult ar)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.OnConnectCompleted);
			this.SetupPoisonContext();
			SmtpResponse response = this.AgentSession.EndRaiseEvent(ar);
			if (!response.IsEmpty)
			{
				this.WriteLineWithLogThenShutdown(response);
				return;
			}
			if (this.SessionSource.ShouldDisconnect)
			{
				this.Shutdown();
				return;
			}
			if (!this.clientIpData.Discredited || this.Connector.TarpitInterval.CompareTo(EnhancedTimeSpan.Zero) <= 0)
			{
				this.WriteBanner(null);
				return;
			}
			this.LogTarpitEvent(this.Connector.TarpitInterval, "IP discredited", null);
			this.delayResponseTimer = new GuardedTimer(new TimerCallback(this.WriteBanner), null, (int)this.Connector.TarpitInterval.TotalMilliseconds, -1);
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000E5C81 File Offset: 0x000E3E81
		private void WriteBanner(object obj)
		{
			this.clientIpData.MarkGood();
			this.WriteLineWithLog(this.Banner);
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000E5C9C File Offset: 0x000E3E9C
		private void StartRead()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}). StartRead.", this.connectionId);
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartRead);
			if (this.rawDataHandler == null)
			{
				throw new InvalidOperationException("StartRead called without handler");
			}
			this.connection.BeginRead(SmtpInSession.readComplete, this);
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000E5CF2 File Offset: 0x000E3EF2
		private void StartReadFromProxyClient(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartReadFromProxyClient);
			this.connection.BeginRead(SmtpInSession.readCompleteFromProxyClient, blindProxyContext);
		}

		// Token: 0x06003818 RID: 14360 RVA: 0x000E5D0E File Offset: 0x000E3F0E
		private void StartReadFromProxyTarget(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartReadFromProxyTarget);
			blindProxyContext.ProxyConnection.BeginRead(SmtpInSession.readCompleteFromProxyTarget, blindProxyContext);
		}

		// Token: 0x06003819 RID: 14361 RVA: 0x000E5D2A File Offset: 0x000E3F2A
		private void StartWriteToProxyTarget(byte[] buffer, int offset, int size, SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartWriteToProxyTarget);
			if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyTargetWritePending, 1, 0) != 0)
			{
				throw new InvalidOperationException("A wite operation to the proxy target is already pending");
			}
			blindProxyContext.ProxyConnection.BeginWrite(buffer, offset, size, SmtpInSession.writeToProxyTargetCompleted, blindProxyContext);
		}

		// Token: 0x0600381A RID: 14362 RVA: 0x000E5D66 File Offset: 0x000E3F66
		private void StartWriteToProxyClient(byte[] buffer, int offset, int size, SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartWriteToProxyClient);
			if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyClientWritePending, 1, 0) != 0)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteToProxyClientSkipped);
				return;
			}
			this.connection.BeginWrite(buffer, offset, size, SmtpInSession.writeToProxyClientCompleted, blindProxyContext);
		}

		// Token: 0x0600381B RID: 14363 RVA: 0x000E5DA4 File Offset: 0x000E3FA4
		private bool StartWriteQuitToProxyTargetIfNecessary(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			if (Interlocked.CompareExchange(ref blindProxyContext.QuitCommandToTargetWriteOwner, 1, 0) != 0)
			{
				return false;
			}
			try
			{
				if (Util.InterlockedEquals(ref blindProxyContext.QuitCommandToTargetNeeded, 0))
				{
					return false;
				}
				this.StartWriteQuitToProxyTarget(blindProxyContext);
				Interlocked.Exchange(ref blindProxyContext.QuitCommandToTargetNeeded, 0);
			}
			finally
			{
				if (Interlocked.CompareExchange(ref blindProxyContext.QuitCommandToTargetWriteOwner, 0, 1) != 1)
				{
					throw new InvalidOperationException("Unexpected quitCommandToTargetWriteOwner value");
				}
			}
			return true;
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x000E5E1C File Offset: 0x000E401C
		private void StartWriteQuitToProxyTarget(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartWriteQuitToProxyTarget);
			if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyTargetWritePending, 1, 0) != 0)
			{
				throw new InvalidOperationException("StartWriteQuitToProxyTarget: A wite operation to the proxy target is already pending");
			}
			blindProxyContext.ProxyConnection.BeginWrite(SmtpInSession.QuitCommand, 0, SmtpInSession.QuitCommand.Length, SmtpInSession.writeQuitToProxyTargetCompleted, blindProxyContext);
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x000E5E70 File Offset: 0x000E4070
		private bool ShouldInitializeMessageTrackingInfo()
		{
			return this.msgTrackInfo == null || (this.IsAnonymousClientProxiedSession && (!string.Equals(this.msgTrackInfo.ProxiedClientHostname, this.HelloDomain, StringComparison.OrdinalIgnoreCase) || !object.Equals(this.msgTrackInfo.ProxiedClientIPAddress, this.ProxiedClientAddress)));
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x000E5EC8 File Offset: 0x000E40C8
		private bool StartWriteXRsetResponseToProxyClientIfNecessary(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			if (Interlocked.CompareExchange(ref blindProxyContext.XRsetProxyToResponseWriteOwner, 1, 0) != 0)
			{
				return false;
			}
			try
			{
				if (Util.InterlockedEquals(ref blindProxyContext.XRsetProxyToResponseNeeded, 0))
				{
					return false;
				}
				this.StartWriteXRsetResponseToProxyClient(blindProxyContext);
				Interlocked.Exchange(ref blindProxyContext.XRsetProxyToResponseNeeded, 0);
			}
			finally
			{
				if (Interlocked.CompareExchange(ref blindProxyContext.XRsetProxyToResponseWriteOwner, 0, 1) != 1)
				{
					throw new InvalidOperationException("Unexpected XRsetProxyToResponseWriteOwner value");
				}
			}
			return true;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x000E5F40 File Offset: 0x000E4140
		private void StartWriteXRsetResponseToProxyClient(SmtpInSession.BlindProxyContext blindProxyContext)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartWriteXRsetResponseToProxyClient);
			if (Interlocked.CompareExchange(ref blindProxyContext.IsProxyClientWritePending, 1, 0) != 0)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.WriteXRsetResponseToProxyClientSkipped);
				return;
			}
			byte[] array = this.CreateXRsetProxyToAcceptedResponse().ToByteArray();
			this.connection.BeginWrite(array, 0, array.Length, SmtpInSession.writeXRsetResponseToProxyClientCompleted, blindProxyContext);
		}

		// Token: 0x06003820 RID: 14368 RVA: 0x000E5F99 File Offset: 0x000E4199
		private void StartReadLine()
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartReadLine);
			if (this.rawDataHandler != null)
			{
				throw new InvalidOperationException("StartReadLine called with an outstanding handler");
			}
			this.connection.BeginReadLine(SmtpInSession.readLineComplete, this);
		}

		// Token: 0x06003821 RID: 14369 RVA: 0x000E5FC8 File Offset: 0x000E41C8
		private void WriteLineWithLog(SmtpResponse response)
		{
			this.WriteLineWithLog(response, SmtpInSession.beginReadLine);
		}

		// Token: 0x06003822 RID: 14370 RVA: 0x000E5FD6 File Offset: 0x000E41D6
		private void WriteLineWithLogThenShutdown(SmtpResponse response)
		{
			this.WriteLineWithLog(response, SmtpInSession.toShutdown, null, true);
		}

		// Token: 0x06003823 RID: 14371 RVA: 0x000E5FE8 File Offset: 0x000E41E8
		private void WriteLineWithLog(SmtpResponse response, AsyncCallback callback, object callbackContextParam, bool alwaysCall)
		{
			SmtpInSession.WriteCompleteLogCallbackParameters state = new SmtpInSession.WriteCompleteLogCallbackParameters(this, new List<SmtpResponse>(1)
			{
				response
			}, callback, callbackContextParam, alwaysCall);
			byte[] array = response.ToByteArray();
			this.connection.BeginWrite(array, 0, array.Length, SmtpInSession.writeCompleteLogCallback, state);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x000E602E File Offset: 0x000E422E
		private void WriteLineWithLog(SmtpResponse response, AsyncCallback callback)
		{
			this.WriteLineWithLog(response, callback, null, false);
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x000E603C File Offset: 0x000E423C
		private void WriteSendBuffer(AsyncCallback callback, object callbackContextParam, bool alwaysCall)
		{
			BufferBuilder bufferBuilder = this.sendBuffer;
			this.sendBuffer = new BufferBuilder();
			SmtpInSession.WriteCompleteLogCallbackParameters state = new SmtpInSession.WriteCompleteLogCallbackParameters(this, this.responseList, callback, callbackContextParam, alwaysCall);
			this.responseList = null;
			this.connection.BeginWrite(bufferBuilder.GetBuffer(), 0, bufferBuilder.Length, SmtpInSession.writeCompleteLogCallback, state);
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x000E6091 File Offset: 0x000E4291
		private void BufferResponse(SmtpResponse response)
		{
			if (this.responseList == null)
			{
				this.responseList = new List<SmtpResponse>();
			}
			this.responseList.Add(response);
			this.sendBuffer.Append(response.ToByteArray());
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x000E60C4 File Offset: 0x000E42C4
		private bool ReceivedAndProcessedXRsetProxyToCommand(byte[] buffer, int offset, int size, SmtpInSession.BlindProxyContext blindProxyContext)
		{
			string text = "XRSETPROXYTO " + this.outboundProxySessionId;
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ReceivedAndProcessedXRsetProxyToCommand);
			if (size == text.Length + " YYYXXX".Length + "\r\n".Length)
			{
				string @string = Encoding.ASCII.GetString(buffer, offset, size);
				int count;
				if (@string.StartsWith(text, StringComparison.OrdinalIgnoreCase) && @string.EndsWith("\r\n", StringComparison.OrdinalIgnoreCase) && int.TryParse(@string.Substring(text.Length + 1, 3), out count))
				{
					SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = this.SmtpProxyPerfCounters;
					if (smtpProxyPerfCountersWrapper != null)
					{
						smtpProxyPerfCountersWrapper.IncrementMessagesProxiedTotalBy(count);
					}
					if (Interlocked.CompareExchange(ref blindProxyContext.BlindProxyWorkDone, 1, 0) != 1)
					{
						this.blindProxyingAuthenticatedUser = false;
						this.logSession.LogReceive(ByteString.StringToBytes(@string, true));
						if (Interlocked.CompareExchange(ref blindProxyContext.QuitCommandToTargetNeeded, 1, 0) != 0)
						{
							throw new InvalidOperationException("QuitCommandToTargetNeeded is already set");
						}
						if (Interlocked.CompareExchange(ref blindProxyContext.XRsetProxyToResponseNeeded, 1, 0) != 0)
						{
							throw new InvalidOperationException("XRsetProxyToResponseNeeded is already set");
						}
						if (Util.InterlockedEquals(ref blindProxyContext.IsProxyTargetWritePending, 0))
						{
							this.StartWriteQuitToProxyTargetIfNecessary(blindProxyContext);
						}
						if (Util.InterlockedEquals(ref blindProxyContext.IsProxyClientWritePending, 0))
						{
							this.StartWriteXRsetResponseToProxyClientIfNecessary(blindProxyContext);
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x000E6200 File Offset: 0x000E4400
		private SmtpResponse CreateXRsetProxyToAcceptedResponse()
		{
			return new SmtpResponse("250", null, new string[]
			{
				"XRSETPROXYTO accepted; " + this.outboundProxySessionId
			});
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x000E6234 File Offset: 0x000E4434
		private void WriteLog(SmtpResponse response)
		{
			if (response.StatusCode == "334")
			{
				this.logSession.LogSend(SmtpInSession.AuthLogLine);
				return;
			}
			if (response.StatusCode == "235" && this.sessionAuthMethod == MultilevelAuthMechanism.MUTUALGSSAPI)
			{
				this.logSession.LogSend(SmtpInSession.ExchangeAuthSuccessLine);
				return;
			}
			this.logSession.LogSend(response.ToByteArray());
			if (response.SmtpResponseType == SmtpResponseType.TransientError || response.SmtpResponseType == SmtpResponseType.PermanentError)
			{
				string text = null;
				if (this.transportMailItem != null)
				{
					text = this.transportMailItem.InternetMessageId;
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "InternetMessageId: {0}", new object[]
					{
						text
					});
				}
			}
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x000E62F8 File Offset: 0x000E44F8
		private void CreateSmtpCommand()
		{
			int currentOffset;
			switch (SmtpInSessionUtils.IdentifySmtpCommand(this.originalCommand, out currentOffset))
			{
			case SmtpInCommand.AUTH:
				this.commandHandler = new AuthSmtpCommand(this, false, this.transportConfiguration);
				break;
			case SmtpInCommand.BDAT:
				if (ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					this.commandHandler = new BdatInboundProxySmtpCommand(this, this.transportAppConfig);
					this.isFrontEndProxyingInbound = true;
				}
				else if (this.expectedBlobs != null && this.expectedBlobs.Count != 0)
				{
					this.commandHandler = new BdatSmtpCommand(this, this.transportAppConfig, this.expectedBlobs.Dequeue());
				}
				else
				{
					this.commandHandler = new BdatSmtpCommand(this, this.transportAppConfig, null);
				}
				break;
			case SmtpInCommand.DATA:
				if (ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					this.commandHandler = new DataInboundProxySmtpCommand(this, this.transportAppConfig);
					this.isFrontEndProxyingInbound = true;
				}
				else
				{
					this.commandHandler = new DataSmtpCommand(this, this.transportAppConfig);
				}
				break;
			case SmtpInCommand.EHLO:
				this.commandHandler = new EHLOSmtpCommand(this, this.transportConfiguration);
				break;
			case SmtpInCommand.EXPN:
				this.commandHandler = new UnknownSmtpCommand(this, "expn", true);
				break;
			case SmtpInCommand.HELO:
				this.commandHandler = new HELOSmtpCommand(this);
				break;
			case SmtpInCommand.HELP:
				this.commandHandler = new HelpSmtpCommand(this);
				break;
			case SmtpInCommand.MAIL:
				this.commandHandler = new MailSmtpCommand(this, this.transportAppConfig);
				break;
			case SmtpInCommand.NOOP:
				this.commandHandler = new NoopSmtpCommand(this);
				break;
			case SmtpInCommand.QUIT:
				this.commandHandler = new QuitSmtpCommand(this);
				break;
			case SmtpInCommand.RCPT:
				this.commandHandler = new RcptSmtpCommand(this, this.recipientCorrelator, this.transportAppConfig);
				break;
			case SmtpInCommand.RSET:
				this.commandHandler = new RsetSmtpCommand(this);
				break;
			case SmtpInCommand.STARTTLS:
				this.commandHandler = new StarttlsSmtpCommand(this, false);
				break;
			case SmtpInCommand.VRFY:
				this.commandHandler = new UnknownSmtpCommand(this, "vrfy", true);
				break;
			case SmtpInCommand.XANONYMOUSTLS:
				this.commandHandler = new StarttlsSmtpCommand(this, true);
				break;
			case SmtpInCommand.XEXCH50:
				this.commandHandler = new Xexch50SmtpCommand(this, this.recipientCorrelator, this.mailRouter, this.transportAppConfig, this.transportConfiguration);
				break;
			case SmtpInCommand.XEXPS:
				this.commandHandler = new AuthSmtpCommand(this, true, this.transportConfiguration);
				break;
			case SmtpInCommand.XPROXY:
				this.commandHandler = new XProxySmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
				break;
			case SmtpInCommand.XPROXYFROM:
				this.commandHandler = new XProxyFromSmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
				break;
			case SmtpInCommand.XPROXYTO:
				this.commandHandler = new XProxyToSmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
				break;
			case SmtpInCommand.XQDISCARD:
				this.commandHandler = new XQDiscardSmtpCommand(this, this.shadowRedundancyManager);
				break;
			case SmtpInCommand.XSESSIONPARAMS:
				this.commandHandler = new XSessionParamsSmtpCommand(this);
				break;
			case SmtpInCommand.XSHADOW:
				this.commandHandler = new XShadowSmtpCommand(this, this.shadowRedundancyManager);
				break;
			case SmtpInCommand.XSHADOWREQUEST:
				this.commandHandler = new XShadowRequestSmtpCommand(this, this.shadowRedundancyManager);
				break;
			case SmtpInCommand.RCPT2:
				this.commandHandler = new Rcpt2SmtpCommand(this);
				break;
			default:
				ExTraceGlobals.SmtpReceiveTracer.TraceError<byte[]>((long)this.GetHashCode(), "Received an unexpected command : {0}", this.originalCommand);
				this.commandHandler = new UnknownSmtpCommand(this, "unknown", false);
				break;
			}
			this.commandHandler.ProtocolCommand = this.originalCommand;
			this.commandHandler.CurrentOffset = currentOffset;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x000E6688 File Offset: 0x000E4888
		private void DisposeSmtpCommand()
		{
			if (this.commandHandler != null)
			{
				this.commandHandler.Dispose();
				this.commandHandler = null;
			}
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x000E66A4 File Offset: 0x000E48A4
		private void LogTarpitEvent(TimeSpan tarpitInterval, string tarpitReason, string tarpitContext)
		{
			if (string.IsNullOrEmpty(tarpitReason))
			{
				byte[] data = Util.AsciiStringToBytes(string.Format(CultureInfo.InvariantCulture, "Tarpit for '{0}'", new object[]
				{
					SmtpInSessionUtils.FormatTimeSpan(tarpitInterval)
				}));
				this.LogInformation(ProtocolLoggingLevel.Verbose, tarpitContext, data);
				return;
			}
			byte[] data2 = Util.AsciiStringToBytes(string.Format(CultureInfo.InvariantCulture, "Tarpit for '{0}' due to '{1}'", new object[]
			{
				SmtpInSessionUtils.FormatTimeSpan(tarpitInterval),
				tarpitReason
			}));
			this.LogInformation(ProtocolLoggingLevel.Verbose, tarpitContext, data2);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x000E671C File Offset: 0x000E491C
		private bool LoadCertificates()
		{
			DateTime utcNow = DateTime.UtcNow;
			ExEventLogWrapper eventLog = new ExEventLogWrapper(this.eventLogger);
			IX509Certificate2 ix509Certificate;
			bool flag = Util.LoadDirectTrustCertificate(this.connector, this.connectionId, this.SmtpInServer.ServerConfiguration.InternalTransportCertificateThumbprint, utcNow, this.server.CertificateCache, eventLog, ExTraceGlobals.SmtpReceiveTracer, out ix509Certificate);
			this.InternalTransportCertificate = ((ix509Certificate == null) ? null : ix509Certificate.Certificate);
			IX509Certificate2 ix509Certificate2;
			flag &= Util.LoadStartTlsCertificate(this.connector, this.ehloOptions.AdvertisedFQDN, this.connectionId, this.transportAppConfig.SmtpReceiveConfiguration.OneLevelWildcardMatchForCertSelection, utcNow, this.server.CertificateCache, eventLog, ExTraceGlobals.SmtpReceiveTracer, out ix509Certificate2);
			this.AdvertisedTlsCertificate = ((ix509Certificate2 == null) ? null : ix509Certificate2.Certificate);
			return flag;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x000E67DC File Offset: 0x000E49DC
		private void Shutdown(DisconnectReason disconnectReason)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ShutdownWithArg);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).Shutdown", this.connectionId);
			this.disconnectReason = disconnectReason;
			if (!this.certificatesLoadedSuccessfully && this.isLastCommandEhloBeforeQuit && (disconnectReason == DisconnectReason.Remote || disconnectReason == DisconnectReason.QuitVerb))
			{
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToTLSError);
			}
			this.Shutdown();
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x000E6837 File Offset: 0x000E4A37
		private void ShutdownCompletedFromMEx(IAsyncResult ar)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ShutdownCompletedFromMEx);
			this.SetupPoisonContext();
			this.AgentSession.EndRaiseEvent(ar);
			this.ShutdownCompleted(null);
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000E6860 File Offset: 0x000E4A60
		private void ShutdownCompleted(IAsyncResult ar)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ShutdownCompleted);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).ShutdownCompleted (disconnect)", this.connectionId);
			if (this.disconnectReason == DisconnectReason.Remote)
			{
				this.logSession.LogDisconnect(this.disconnectReason, this.remoteConnectionError);
			}
			else
			{
				this.logSession.LogDisconnect(this.disconnectReason);
			}
			this.AgentSession.Close();
			if (this.shadowSession != null)
			{
				this.shadowSession.Close(AckStatus.Fail, SmtpResponse.Empty);
			}
			this.DeleteTransportMailItem();
			this.server.RemoveConnection(this.connectionId);
			this.connection.Dispose();
			SmtpInSession.BlindProxyContext blindProxyContext = this.blindProxyContext;
			if (blindProxyContext != null)
			{
				blindProxyContext.ProxyConnection.Dispose();
				IProtocolLogSession blindProxySendLogSession = blindProxyContext.BlindProxySendLogSession;
				if (blindProxySendLogSession != null)
				{
					blindProxySendLogSession.LogDisconnect(this.proxyTargetDisconnectReason);
					string description = string.Empty;
					if (!string.IsNullOrEmpty(this.proxyTargetRemoteConnectionError))
					{
						description = string.Format("Remote error from proxy target - {0}", this.proxyTargetRemoteConnectionError);
					}
					else if (!string.IsNullOrEmpty(this.remoteConnectionError))
					{
						description = string.Format("Remote error from proxy client", new object[0]);
					}
					ConnectionLog.SmtpConnectionStop(blindProxyContext.BlindProxySendSessionId, string.Empty, description, 0UL, 0UL, 0UL);
				}
				this.blindProxyContext = null;
			}
			if (this.ProxyPassword != null)
			{
				this.ProxyPassword.Dispose();
			}
			this.RemoveClientIpConnection();
			ISmtpReceivePerfCounters smtpReceivePerformanceCounters = this.SmtpReceivePerformanceCounters;
			if (smtpReceivePerformanceCounters != null)
			{
				smtpReceivePerformanceCounters.ConnectionsCurrent.Decrement();
				if (this.IsTls)
				{
					smtpReceivePerformanceCounters.TlsConnectionsCurrent.Decrement();
				}
				if (this.isFrontEndProxyingInbound)
				{
					smtpReceivePerformanceCounters.InboundMessageConnectionsCurrent.Decrement();
				}
			}
			SmtpProxyPerfCountersWrapper smtpProxyPerfCountersWrapper = this.smtpProxyPerfCounters;
			if (smtpProxyPerfCountersWrapper != null)
			{
				smtpProxyPerfCountersWrapper.DecrementInboundConnectionsCurrent();
			}
			if (this.SmtpInServer.OutboundProxyBySourceTracker != null && !string.IsNullOrWhiteSpace(this.HelloSmtpDomain))
			{
				this.SmtpInServer.OutboundProxyBySourceTracker.DecrementProxyCount(this.HelloSmtpDomain);
			}
			this.server = null;
			this.connector = null;
			this.originalCommand = null;
			this.logSession = null;
			this.ehloOptions = null;
			this.msgTrackInfo = null;
			this.DisposeSmtpCommand();
			if (this.bdatState != null && this.bdatState.ProxyLayer != null)
			{
				this.bdatState.ProxyLayer.NotifySmtpInStopProxy();
				this.bdatState.ProxyLayer = null;
			}
			this.bdatState = null;
			if (this.delayResponseTimer != null)
			{
				this.delayResponseTimer.Dispose(false);
				this.delayResponseTimer = null;
			}
			this.smtpReceivePerfCountersInstance = null;
			this.smtpAvailabilityPerfCounters = null;
			this.smtpProxyPerfCounters = null;
			if (this.remoteWindowsIdentity != null)
			{
				this.remoteWindowsIdentity.Dispose();
				this.remoteWindowsIdentity = null;
			}
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x000E6AE8 File Offset: 0x000E4CE8
		private AsyncReturnType StartProcessingCommand(byte[] inputBuffer, int offset, int size, bool overflow)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.StartProcessingCommand);
			BufferBuilder bufferBuilder = this.commandBuffer ?? new BufferBuilder(size);
			if (bufferBuilder.Length + size > 32768)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}) disconnected: Command too long", this.connectionId);
				this.WriteLineWithLogThenShutdown(SmtpResponse.CommandTooLong);
				return AsyncReturnType.Async;
			}
			bufferBuilder.Append(inputBuffer, offset, size);
			if (overflow)
			{
				this.commandBuffer = bufferBuilder;
				return AsyncReturnType.Sync;
			}
			this.commandBuffer = null;
			bufferBuilder.RemoveUnusedBufferSpace();
			if (this.commandHandler == null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, BufferBuilder>((long)this.GetHashCode(), "SmtpInSession(id={0}) cmd: {1}", this.connectionId, bufferBuilder);
				this.originalCommand = bufferBuilder.GetBuffer();
				this.CreateSmtpCommand();
				if (!(this.commandHandler is AuthSmtpCommand) && !(this.commandHandler is MailSmtpCommand) && !(this.commandHandler is RcptSmtpCommand) && !(this.commandHandler is XProxySmtpCommand))
				{
					this.logSession.LogReceive(bufferBuilder.GetBuffer());
				}
			}
			else
			{
				this.commandHandler.ProtocolCommand = bufferBuilder.GetBuffer();
				this.commandHandler.CurrentOffset = 0;
			}
			this.commandHandler.InboundParseCommand();
			if (this.commandHandler.IsResponseReady && (this.commandHandler.ParsingStatus == ParsingStatus.ProtocolError || this.commandHandler.ParsingStatus == ParsingStatus.Error || this.commandHandler.ParsingStatus == ParsingStatus.IgnorableProtocolError))
			{
				IAsyncResult asyncResult = this.RaiseOnRejectEvent(this.commandHandler.ProtocolCommand, null, this.commandHandler.SmtpResponse, new AsyncCallback(this.OnRejectCallback));
				if (!asyncResult.CompletedSynchronously)
				{
					return AsyncReturnType.Async;
				}
				return this.ContinueOnReject(asyncResult, false);
			}
			else
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.PostParseCommand);
				if (this.commandHandler.CommandEventComponent != LatencyComponent.None && this.transportMailItem != null)
				{
					this.AgentLatencyTracker.BeginTrackLatency(this.commandHandler.CommandEventComponent, this.transportMailItem.LatencyTracker);
				}
				IAsyncResult asyncResult2 = this.commandHandler.BeginRaiseEvent(new AsyncCallback(this.PostParseCommandCompleted), null);
				if (!asyncResult2.CompletedSynchronously)
				{
					return AsyncReturnType.Async;
				}
				return this.ContinuePostParseCommand(asyncResult2, false);
			}
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x000E6CEB File Offset: 0x000E4EEB
		private void PostParseCommandCompleted(IAsyncResult ar)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.PostParseCommandCompleted);
			this.SetupPoisonContext();
			if (!ar.CompletedSynchronously)
			{
				this.ContinuePostParseCommand(ar, true);
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x000E6D0C File Offset: 0x000E4F0C
		private AsyncReturnType ContinuePostParseCommand(IAsyncResult ar, bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinuePostParseCommand);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).ContinuePostParseCommand", this.connectionId);
			SmtpResponse smtpResponse = this.AgentSession.EndRaiseEvent(ar);
			if (this.commandHandler.CommandEventComponent != LatencyComponent.None && this.transportMailItem != null)
			{
				this.AgentLatencyTracker.EndTrackLatency();
			}
			this.commandHandler.InboundAgentEventCompleted();
			if (!smtpResponse.IsEmpty || this.SessionSource.ShouldDisconnect)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DisconnectFromContinuePostParseCommand);
				SmtpResponse response = smtpResponse.IsEmpty ? this.commandHandler.SmtpResponse : smtpResponse;
				if (!response.IsEmpty)
				{
					this.WriteLineWithLogThenShutdown(response);
				}
				else
				{
					this.Shutdown();
				}
				return AsyncReturnType.Async;
			}
			if (this.SessionSource.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				return this.ProcessCommand(isAsync);
			}
			this.commandHandler.SmtpResponse = this.SessionSource.SmtpResponse;
			this.SessionSource.SmtpResponse = SmtpResponse.Empty;
			byte[] command = null;
			if (this.commandHandler.OriginalEventArgsWrapper is ReceiveCommandEventArgs)
			{
				command = this.commandHandler.ProtocolCommand;
			}
			IAsyncResult asyncResult = this.RaiseOnRejectEvent(command, this.commandHandler.OriginalEventArgsWrapper, this.commandHandler.SmtpResponse, new AsyncCallback(this.OnRejectCallback));
			if (!asyncResult.CompletedSynchronously)
			{
				return AsyncReturnType.Async;
			}
			return this.ContinueOnReject(asyncResult, isAsync);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x000E6E6A File Offset: 0x000E506A
		private void OnRejectCallback(IAsyncResult ar)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.OnRejectCallback);
			this.SetupPoisonContext();
			if (!ar.CompletedSynchronously)
			{
				this.ContinueOnReject(ar, true);
			}
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000E6E8C File Offset: 0x000E508C
		private AsyncReturnType ContinueOnReject(IAsyncResult ar, bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueOnReject);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).OnRejectcompleted", this.connectionId);
			if (this.commandHandler.ParsingStatus == ParsingStatus.MoreDataRequired)
			{
				this.commandHandler.ParsingStatus = ParsingStatus.Complete;
			}
			SmtpResponse response = this.AgentSession.EndRaiseEvent(ar);
			if (!response.IsEmpty)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DisconnectFromContinueOnReject);
				this.WriteLineWithLogThenShutdown(response);
				return AsyncReturnType.Async;
			}
			return this.DelayResponseIfNecessary(isAsync);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000E6F08 File Offset: 0x000E5108
		private AsyncReturnType ProcessCommand(bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ProcessCommand);
			if (!(this.commandHandler is QuitSmtpCommand))
			{
				this.isLastCommandEhloBeforeQuit = false;
				this.isLastCommandAuthBeforeQuit = false;
			}
			else if (this.isLastCommandAuthBeforeQuit && this.AuthMethod == MultilevelAuthMechanism.MUTUALGSSAPI)
			{
				AuthCommandHelpers.TryFlushKerberosTicketCache(this.transportConfiguration.AppConfig.SmtpAvailabilityConfiguration.KerberosTicketCacheFlushMinInterval, this.LogSession);
			}
			if (this.commandHandler is AuthSmtpCommand)
			{
				this.isLastCommandAuthBeforeQuit = true;
			}
			this.commandHandler.InboundProcessCommand();
			if (this.StartClientProxySession)
			{
				this.StartClientProxySession = false;
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
				this.smtpProxyPerfCounters = this.SmtpInServer.ClientProxyPerfCounters;
				if (!this.usedForBlindProxy)
				{
					this.usedForBlindProxy = true;
					this.smtpProxyPerfCounters.IncrementInboundConnectionsCurrent();
				}
				this.proxySetupHandler = new ProxySessionSetupHandler(this, this.enhancedDns, this.transportConfiguration);
				this.proxySetupHandler.BeginSettingUpProxySession();
				return AsyncReturnType.Async;
			}
			if (this.startOutboundProxySession)
			{
				this.startOutboundProxySession = false;
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
				this.smtpProxyPerfCounters = this.SmtpInServer.OutboundProxyPerfCounters;
				if (!this.usedForBlindProxy)
				{
					this.usedForBlindProxy = true;
					this.smtpProxyPerfCounters.IncrementInboundConnectionsCurrent();
					if (!string.IsNullOrWhiteSpace(this.HelloSmtpDomain))
					{
						this.SmtpInServer.OutboundProxyBySourceTracker.IncrementProxyCount(this.HelloSmtpDomain);
					}
				}
				this.proxySetupHandler = new ProxySessionSetupHandler(this, this.enhancedDns, this.transportConfiguration, this.outboundProxyDestinations, this.outboundProxySendConnector, this.outboundProxyTlsSendConfiguration, this.outboundProxyRiskLevel, this.outboundProxyOutboundIPPool, this.outboundProxyNextHopDomain, this.outboundProxySessionId);
				this.proxySetupHandler.BeginSettingUpProxySession();
				return AsyncReturnType.Async;
			}
			return this.ContinueProcessCommand(isAsync);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000E70B0 File Offset: 0x000E52B0
		private AsyncReturnType ContinueProcessCommand(bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueProcessCommand);
			if (this.commandHandler.IsResponseReady && !this.commandHandler.SmtpResponse.Equals(SmtpResponse.Empty) && (this.commandHandler.SmtpResponse.StatusCode[0] == '5' || this.commandHandler.SmtpResponse.StatusCode[0] == '4'))
			{
				IAsyncResult asyncResult = this.RaiseOnRejectEvent(this.commandHandler.ProtocolCommand, null, this.commandHandler.SmtpResponse, new AsyncCallback(this.OnRejectCallback));
				if (!asyncResult.CompletedSynchronously)
				{
					return AsyncReturnType.Async;
				}
				return this.ContinueOnReject(asyncResult, isAsync);
			}
			else
			{
				BdatSmtpCommand bdatSmtpCommand = this.commandHandler as BdatSmtpCommand;
				BdatInboundProxySmtpCommand bdatInboundProxySmtpCommand = this.commandHandler as BdatInboundProxySmtpCommand;
				if ((bdatSmtpCommand != null && bdatSmtpCommand.IsBdat0Last) || (bdatInboundProxySmtpCommand != null && bdatInboundProxySmtpCommand.IsBdat0Last))
				{
					if (this.rawDataHandler(SmtpInSession.EmptyBuffer, 0, 0) == AsyncReturnType.Sync)
					{
						this.RawDataReceivedCompleted();
					}
					return AsyncReturnType.Async;
				}
				return this.DelayResponseIfNecessary(isAsync);
			}
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000E71BC File Offset: 0x000E53BC
		private AsyncReturnType DelayResponseIfNecessary(bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayResponseIfNecessary);
			ParsingStatus parsingStatus = this.commandHandler.ParsingStatus;
			if (parsingStatus == ParsingStatus.ProtocolError)
			{
				this.protocolErrors++;
				if (SmtpInSessionUtils.IsMaxProtocolErrorsExceeded(this.protocolErrors, this.Connector))
				{
					this.commandHandler.SmtpResponse = SmtpResponse.TooManyProtocolErrors;
					this.Disconnect(DisconnectReason.TooManyErrors);
				}
			}
			if (parsingStatus != ParsingStatus.MoreDataRequired)
			{
				this.commandHandler.InboundCompleteCommand();
			}
			TarpitAction tarpitAction;
			TimeSpan timeSpan;
			string text;
			string tarpitContext;
			if (this.commandHandler.IsResponseReady)
			{
				tarpitAction = (SmtpInSessionUtils.IsTarpitAuthenticationLevelHigh(this.Permissions, this.RemoteIdentity, this.SmtpInServer.Configuration.AppConfig.SmtpReceiveConfiguration.TarpitMuaSubmission) ? this.commandHandler.HighAuthenticationLevelTarpitOverride : this.commandHandler.LowAuthenticationLevelTarpitOverride);
				timeSpan = this.commandHandler.TarpitInterval;
				text = this.commandHandler.TarpitReason;
				tarpitContext = this.commandHandler.TarpitContext;
				this.protocolResponse = this.commandHandler.SmtpResponse;
				this.isResponseBuffered = this.commandHandler.IsResponseBuffered;
				this.commandHandler.LowAuthenticationLevelTarpitOverride = TarpitAction.None;
				this.commandHandler.HighAuthenticationLevelTarpitOverride = TarpitAction.None;
				this.commandHandler.SmtpResponse = SmtpResponse.Empty;
			}
			else
			{
				tarpitAction = TarpitAction.None;
				timeSpan = TimeSpan.Zero;
				text = string.Empty;
				tarpitContext = null;
			}
			if (parsingStatus != ParsingStatus.MoreDataRequired)
			{
				this.DisposeSmtpCommand();
			}
			if (!this.protocolResponse.Equals(SmtpResponse.Empty) && parsingStatus != ParsingStatus.MoreDataRequired)
			{
				if (this.delayedAckStatus == SmtpInSession.DelayedAckStatus.ShadowRedundancyManagerNotified)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).DelayResponseIfNecessary: Waiting for notification from ShadowRedundancyManager relating to delayed ack message", this.connectionId);
					this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayedAckStarted);
					this.delayedAckStatus = SmtpInSession.DelayedAckStatus.WaitingForShadowRedundancyManager;
					return AsyncReturnType.Async;
				}
				bool flag = false;
				if (!SmtpInSessionUtils.IsTarpitAuthenticationLevelHigh(this.Permissions, this.RemoteIdentity, this.SmtpInServer.Configuration.AppConfig.SmtpReceiveConfiguration.TarpitMuaSubmission) && (this.protocolResponse.SmtpResponseType == SmtpResponseType.PermanentError || parsingStatus == ParsingStatus.Error || parsingStatus == ParsingStatus.ProtocolError))
				{
					if (!this.IsAnonymousClientProxiedSession && this.InboundClientProxyState == InboundClientProxyStates.None)
					{
						this.clientIpData.MarkBad();
					}
					flag = true;
					if (string.IsNullOrEmpty(text))
					{
						text = this.protocolResponse.ToString();
					}
				}
				if (tarpitAction == TarpitAction.DoTarpit)
				{
					flag = true;
				}
				else if (tarpitAction == TarpitAction.DoNotTarpit)
				{
					flag = false;
				}
				if (flag && timeSpan > TimeSpan.Zero)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, double, string>((long)this.GetHashCode(), "SmtpInSession(id={0}).DelayResponseIfNecessary: Delay ({1} msec.) the response due to '{0}'.", this.connectionId, timeSpan.TotalMilliseconds, text);
					ISmtpReceivePerfCounters smtpReceivePerformanceCounters = this.SmtpReceivePerformanceCounters;
					if (smtpReceivePerformanceCounters != null)
					{
						if (SmtpInSessionUtils.IsAnonymous(this.RemoteIdentity))
						{
							smtpReceivePerformanceCounters.TarpittingDelaysAnonymous.Increment();
						}
						else
						{
							smtpReceivePerformanceCounters.TarpittingDelaysAuthenticated.Increment();
						}
						if ("Back Pressure".Equals(text))
						{
							smtpReceivePerformanceCounters.TarpittingDelaysBackpressure.Increment();
						}
					}
					this.LogTarpitEvent(timeSpan, text, tarpitContext);
					this.delayResponseTimer = new GuardedTimer(new TimerCallback(this.DelayResponseCompleted), null, (int)timeSpan.TotalMilliseconds, -1);
					return AsyncReturnType.Async;
				}
			}
			return this.EndProcessingCommand(isAsync);
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000E74A8 File Offset: 0x000E56A8
		private void DelayResponseCompleted(object state)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DelayResponseCompleted);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).DelayResponseCompleted", this.connectionId);
			if (this.delayedAckStatus == SmtpInSession.DelayedAckStatus.WaitingForShadowRedundancyManager)
			{
				this.delayedAckStatus = SmtpInSession.DelayedAckStatus.None;
			}
			else
			{
				if (this.delayResponseTimer != null)
				{
					this.delayResponseTimer.Dispose(false);
					this.delayResponseTimer = null;
				}
				this.clientIpData.MarkGood();
			}
			this.EndProcessingCommand(true);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000E751C File Offset: 0x000E571C
		private AsyncReturnType EndProcessingCommand(bool isAsync)
		{
			this.DropBreadcrumb(SmtpInSessionBreadcrumbs.EndProcessingCommand);
			if (this.SessionSource.ShouldDisconnect)
			{
				this.DropBreadcrumb(SmtpInSessionBreadcrumbs.DisconnectFromEndProcessingCommand);
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpInSession(id={0}).EndProcessingCommand calling Shutdown", this.connectionId);
				if (this.disconnectByServer && !this.protocolResponse.Equals(SmtpResponse.Empty))
				{
					if (this.sendBuffer.Length > 0)
					{
						this.BufferResponse(this.protocolResponse);
						this.WriteSendBuffer(SmtpInSession.toShutdown, null, true);
					}
					else
					{
						this.WriteLineWithLogThenShutdown(this.protocolResponse);
					}
				}
				else
				{
					this.Shutdown();
				}
				return AsyncReturnType.Async;
			}
			bool flag = (byte)(this.secureState & SecureState.NegotiationRequested) == 128;
			object callbackContextParam = null;
			AsyncCallback callback;
			if (flag)
			{
				callback = SmtpInSession.startTlsNegotiation;
			}
			else if (this.rawDataHandler != null)
			{
				callback = SmtpInSession.beginRead;
			}
			else
			{
				callback = SmtpInSession.beginReadLine;
			}
			SmtpInSession.BlindProxyContext blindProxyContext = this.blindProxyContext;
			if (blindProxyContext != null && Util.InterlockedEquals(ref blindProxyContext.BlindProxyWorkDone, 0))
			{
				this.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy session was successfully set up. {0} will now be proxied", new object[]
				{
					(this.ProxyUserName != null) ? ("Session for" + Util.Redact(this.ProxyUserName)) : "Outbound session"
				});
				callback = new AsyncCallback(SmtpInSession.StartProxying);
				callbackContextParam = blindProxyContext;
			}
			if (!this.protocolResponse.Equals(SmtpResponse.Empty))
			{
				SmtpResponse response = this.protocolResponse;
				this.protocolResponse = SmtpResponse.Empty;
				if (isAsync || !this.SeenEhlo || !this.isResponseBuffered || response.StatusCode[0] != '2' || !this.connection.IsLineAvailable || flag)
				{
					this.DropBreadcrumb(SmtpInSessionBreadcrumbs.EndProcessingCommandWriteResponse);
					isAsync = true;
					if (this.sendBuffer.Length > 0)
					{
						this.BufferResponse(response);
						this.WriteSendBuffer(callback, callbackContextParam, false);
					}
					else
					{
						this.WriteLineWithLog(response, callback, callbackContextParam, false);
					}
				}
				else
				{
					this.DropBreadcrumb(SmtpInSessionBreadcrumbs.EndProcessingCommandWriteResponseToBuffer);
					this.BufferResponse(response);
				}
			}
			else if (isAsync)
			{
				if (this.rawDataHandler == null)
				{
					this.StartReadLine();
				}
				else
				{
					this.StartRead();
				}
			}
			if (!isAsync)
			{
				return AsyncReturnType.Sync;
			}
			return AsyncReturnType.Async;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000E7724 File Offset: 0x000E5924
		private void TlsNegotiationComplete()
		{
			if (this.connection.RemoteCertificate != null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string>((long)this.GetHashCode(), "Remote has supplied client certificate {0}", this.connection.RemoteCertificate.Subject);
				this.TlsRemoteCertificate = this.connection.RemoteCertificate.Certificate;
				if (this.secureState == SecureState.AnonymousTls)
				{
					this.RemoteIdentity = DirectTrust.MapCertToSecurityIdentifier(this.connection.RemoteCertificate.Certificate);
					if (this.RemoteIdentity != SmtpConstants.AnonymousSecurityIdentifier)
					{
						this.RemoteIdentityName = this.connection.RemoteCertificate.Subject;
						this.AuthMethod = MultilevelAuthMechanism.DirectTrustTLS;
						ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string>((long)this.GetHashCode(), "DirectTrust certificate authenticated as {0}", this.RemoteIdentityName);
						CertificateExpiryCheck.CheckCertificateExpiry(this.connection.RemoteCertificate.Certificate, this.eventLogger, (this.secureState == SecureState.StartTls) ? SmtpSessionCertificateUse.RemoteSTARTTLS : SmtpSessionCertificateUse.RemoteDirectTrust, this.connection.RemoteCertificate.Subject);
					}
					else
					{
						this.RemoteIdentityName = "anonymous";
						ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "DirectTrust certificate failed to authenticate for {0}", this.TlsRemoteCertificate.Subject);
						this.LogInformation(ProtocolLoggingLevel.Verbose, "DirectTrust certificate failed to authenticate for " + this.connection.RemoteCertificate.Subject, null);
						this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveDirectTrustFailed, this.RemoteEndPoint.Address.ToString(), new object[]
						{
							this.TlsRemoteCertificate.Subject,
							this.RemoteEndPoint.Address
						});
					}
				}
				this.SetSessionPermissions(this.RemoteIdentity);
			}
			if (this.IsTls)
			{
				this.SmtpReceivePerformanceCounters.TlsConnectionsCurrent.Increment();
			}
			this.ehloOptions.StartTLS = false;
			this.ehloOptions.AnonymousTLS = false;
			this.StartReadLine();
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000E7904 File Offset: 0x000E5B04
		private void ApplySupportIntegratedAuthOverride(bool isFrontEndTransportProcess)
		{
			this.supportIntegratedAuth = SmtpInSessionUtils.ShouldSupportIntegratedAuthentication(this.supportIntegratedAuth, isFrontEndTransportProcess);
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000E7918 File Offset: 0x000E5B18
		private Permission DetermineAnonymousPermissions()
		{
			if (this.anonymousPermissions == null)
			{
				this.anonymousPermissions = new Permission?(Util.GetPermissionsForSid(SmtpConstants.AnonymousSecurityIdentifier, this.connector.GetSecurityDescriptor(), this.authzAuthorization, "anonymous", this.connector.Name, ExTraceGlobals.SmtpReceiveTracer));
			}
			return this.anonymousPermissions.Value;
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000E7978 File Offset: 0x000E5B78
		private Permission DeterminePartnerPermissions()
		{
			if (this.partnerPermissions == null)
			{
				this.partnerPermissions = new Permission?(Util.GetPermissionsForSid(WellKnownSids.PartnerServers, this.connector.GetSecurityDescriptor(), this.authzAuthorization, "partner", this.Connector.Name, ExTraceGlobals.SmtpReceiveTracer));
			}
			return this.partnerPermissions.Value;
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000E79D8 File Offset: 0x000E5BD8
		private void IncrementConnectionLevelPerfCounters()
		{
			if (this.smtpAvailabilityPerfCounters != null)
			{
				int arg = (int)this.SmtpReceivePerformanceCounters.ConnectionsCurrent.Increment();
				this.SmtpReceivePerformanceCounters.ConnectionsTotal.Increment();
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug<long, int>((long)this.GetHashCode(), "SmtpInSession(id={0}) created, connectionPerfCount={1}", this.connectionId, arg);
			}
		}

		// Token: 0x04001C3D RID: 7229
		private const int BlindProxyMaxClientConnectionShutdownWaitSeconds = 5;

		// Token: 0x04001C3E RID: 7230
		internal static readonly byte[] QuitCommand = new byte[]
		{
			81,
			85,
			73,
			84,
			13,
			10
		};

		// Token: 0x04001C3F RID: 7231
		private static readonly byte[] AuthLogLine = Encoding.ASCII.GetBytes("334 <authentication response>");

		// Token: 0x04001C40 RID: 7232
		private static readonly byte[] ExchangeAuthSuccessLine = Encoding.ASCII.GetBytes("235 <authentication response>");

		// Token: 0x04001C41 RID: 7233
		private static readonly AsyncCallback writeCompleteLogCallback = new AsyncCallback(SmtpInSession.WriteCompleteLogCallback);

		// Token: 0x04001C42 RID: 7234
		private static readonly AsyncCallback beginReadLine = new AsyncCallback(SmtpInSession.BeginReadLine);

		// Token: 0x04001C43 RID: 7235
		private static readonly AsyncCallback beginRead = new AsyncCallback(SmtpInSession.BeginRead);

		// Token: 0x04001C44 RID: 7236
		private static readonly AsyncCallback readCompleteFromProxyClient = new AsyncCallback(SmtpInSession.ReadCompleteFromProxyClient);

		// Token: 0x04001C45 RID: 7237
		private static readonly AsyncCallback writeToProxyTargetCompleted = new AsyncCallback(SmtpInSession.WriteToProxyTargetCompleted);

		// Token: 0x04001C46 RID: 7238
		private static readonly AsyncCallback readCompleteFromProxyTarget = new AsyncCallback(SmtpInSession.ReadCompleteFromProxyTarget);

		// Token: 0x04001C47 RID: 7239
		private static readonly AsyncCallback writeToProxyClientCompleted = new AsyncCallback(SmtpInSession.WriteToProxyClientCompleted);

		// Token: 0x04001C48 RID: 7240
		private static readonly AsyncCallback writeQuitToProxyTargetCompleted = new AsyncCallback(SmtpInSession.WriteQuitToProxyTargetCompleted);

		// Token: 0x04001C49 RID: 7241
		private static readonly AsyncCallback writeXRsetResponseToProxyClientCompleted = new AsyncCallback(SmtpInSession.WriteXRsetResponseToProxyClientCompleted);

		// Token: 0x04001C4A RID: 7242
		private static readonly AsyncCallback toShutdown = new AsyncCallback(SmtpInSession.ToShutdown);

		// Token: 0x04001C4B RID: 7243
		private static readonly AsyncCallback startTlsNegotiation = new AsyncCallback(SmtpInSession.StartTlsNegotiation);

		// Token: 0x04001C4C RID: 7244
		private static readonly AsyncCallback tlsNegotiationComplete = new AsyncCallback(SmtpInSession.TlsNegotiationComplete);

		// Token: 0x04001C4D RID: 7245
		private static readonly AsyncCallback readComplete = new AsyncCallback(SmtpInSession.ReadComplete);

		// Token: 0x04001C4E RID: 7246
		private static readonly AsyncCallback readLineComplete = new AsyncCallback(SmtpInSession.ReadLineComplete);

		// Token: 0x04001C4F RID: 7247
		private static readonly byte[] EmptyBuffer = new byte[0];

		// Token: 0x04001C50 RID: 7248
		protected ISmtpInServer server;

		// Token: 0x04001C51 RID: 7249
		protected ReceiveConnector connector;

		// Token: 0x04001C52 RID: 7250
		protected Permission sessionPermissions;

		// Token: 0x04001C53 RID: 7251
		protected IAuthzAuthorization authzAuthorization;

		// Token: 0x04001C54 RID: 7252
		protected IPAddress proxiedClientAddress;

		// Token: 0x04001C55 RID: 7253
		private readonly DateTime sessionStartTime;

		// Token: 0x04001C56 RID: 7254
		private readonly DateTime sessionExpireTime;

		// Token: 0x04001C57 RID: 7255
		private readonly ExtendedProtectionConfig extendedProtectionConfig;

		// Token: 0x04001C58 RID: 7256
		private readonly IMessageThrottlingManager messageThrottlingManager;

		// Token: 0x04001C59 RID: 7257
		private readonly IQueueQuotaComponent queueQuotaComponent;

		// Token: 0x04001C5A RID: 7258
		private readonly IIsMemberOfResolver<RoutingAddress> memberOfResolver;

		// Token: 0x04001C5B RID: 7259
		private readonly IMailRouter mailRouter;

		// Token: 0x04001C5C RID: 7260
		private readonly IEnhancedDns enhancedDns;

		// Token: 0x04001C5D RID: 7261
		private readonly IShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x04001C5E RID: 7262
		private IShadowSession shadowSession;

		// Token: 0x04001C5F RID: 7263
		private readonly ITransportAppConfig transportAppConfig;

		// Token: 0x04001C60 RID: 7264
		private readonly ITransportConfiguration transportConfiguration;

		// Token: 0x04001C61 RID: 7265
		private readonly ExEventLog eventLogger;

		// Token: 0x04001C62 RID: 7266
		private IProtocolLogSession logSession;

		// Token: 0x04001C63 RID: 7267
		private readonly ISmtpAgentSession agentSession;

		// Token: 0x04001C64 RID: 7268
		private readonly ulong significantAddressBytes;

		// Token: 0x04001C65 RID: 7269
		private string remoteConnectionError;

		// Token: 0x04001C66 RID: 7270
		private bool sendAsRequiredADLookup;

		// Token: 0x04001C67 RID: 7271
		private int numberOfMessagesReceived;

		// Token: 0x04001C68 RID: 7272
		private readonly INetworkConnection connection;

		// Token: 0x04001C69 RID: 7273
		private readonly SmtpReceiveConnectorStub connectorStub;

		// Token: 0x04001C6A RID: 7274
		private readonly IPAddress clientIpAddress;

		// Token: 0x04001C6B RID: 7275
		private byte[] originalCommand;

		// Token: 0x04001C6C RID: 7276
		private SmtpCommand commandHandler;

		// Token: 0x04001C6D RID: 7277
		private SmtpResponse protocolResponse = SmtpResponse.Empty;

		// Token: 0x04001C6E RID: 7278
		private TransportMailItem transportMailItem;

		// Token: 0x04001C6F RID: 7279
		private TransportMailItemWrapper mailItemWrapper;

		// Token: 0x04001C70 RID: 7280
		private Stream messageWriteStream;

		// Token: 0x04001C71 RID: 7281
		private bool seenHelo;

		// Token: 0x04001C72 RID: 7282
		private bool seenEhlo;

		// Token: 0x04001C73 RID: 7283
		private int protocolErrors;

		// Token: 0x04001C74 RID: 7284
		private int logonFailures;

		// Token: 0x04001C75 RID: 7285
		private int messagesSubmitted;

		// Token: 0x04001C76 RID: 7286
		private bool tarpitRset = true;

		// Token: 0x04001C77 RID: 7287
		private readonly long connectionId;

		// Token: 0x04001C78 RID: 7288
		private bool isResponseBuffered;

		// Token: 0x04001C79 RID: 7289
		private GuardedTimer delayResponseTimer;

		// Token: 0x04001C7A RID: 7290
		private SmtpInBdatState bdatState;

		// Token: 0x04001C7B RID: 7291
		private EhloOptions ehloOptions;

		// Token: 0x04001C7C RID: 7292
		private bool firedOnConnectEvent;

		// Token: 0x04001C7D RID: 7293
		private MsgTrackReceiveInfo msgTrackInfo;

		// Token: 0x04001C7E RID: 7294
		private bool disconnectByServer;

		// Token: 0x04001C7F RID: 7295
		private AuthenticationSource? proxiedClientAuthSource;

		// Token: 0x04001C80 RID: 7296
		private uint? proxiedClientPermissions;

		// Token: 0x04001C81 RID: 7297
		private bool enforceMimeLimitsForProxiedSession;

		// Token: 0x04001C82 RID: 7298
		private SmtpSendConnectorConfig outboundProxySendConnector;

		// Token: 0x04001C83 RID: 7299
		private IEnumerable<INextHopServer> outboundProxyDestinations;

		// Token: 0x04001C84 RID: 7300
		private TlsSendConfiguration outboundProxyTlsSendConfiguration;

		// Token: 0x04001C85 RID: 7301
		private RiskLevel outboundProxyRiskLevel;

		// Token: 0x04001C86 RID: 7302
		private int outboundProxyOutboundIPPool;

		// Token: 0x04001C87 RID: 7303
		private string outboundProxySessionId;

		// Token: 0x04001C88 RID: 7304
		private string outboundProxyNextHopDomain;

		// Token: 0x04001C89 RID: 7305
		private string proxyUserName;

		// Token: 0x04001C8A RID: 7306
		private SecureString proxyPassword;

		// Token: 0x04001C8B RID: 7307
		private DisconnectReason proxyTargetDisconnectReason = DisconnectReason.Local;

		// Token: 0x04001C8C RID: 7308
		private string proxyTargetRemoteConnectionError;

		// Token: 0x04001C8D RID: 7309
		private bool blindProxyingAuthenticatedUser;

		// Token: 0x04001C8E RID: 7310
		private SmtpInSession.BlindProxyContext blindProxyContext;

		// Token: 0x04001C8F RID: 7311
		private int tooManyRecipientsResponseCount;

		// Token: 0x04001C90 RID: 7312
		private BufferBuilder commandBuffer;

		// Token: 0x04001C91 RID: 7313
		private SecureState secureState;

		// Token: 0x04001C92 RID: 7314
		private MultilevelAuthMechanism sessionAuthMethod;

		// Token: 0x04001C93 RID: 7315
		private readonly ClientData clientIpData;

		// Token: 0x04001C94 RID: 7316
		private SecurityIdentifier sessionRemoteIdentity = SmtpConstants.AnonymousSecurityIdentifier;

		// Token: 0x04001C95 RID: 7317
		private WindowsIdentity remoteWindowsIdentity;

		// Token: 0x04001C96 RID: 7318
		private string sessionRemoteIdentityName = "anonymous";

		// Token: 0x04001C97 RID: 7319
		private InboundRecipientCorrelator recipientCorrelator;

		// Token: 0x04001C98 RID: 7320
		private InboundExch50 inboundExch50;

		// Token: 0x04001C99 RID: 7321
		private XProxyToSmtpCommandParser xProxyToParser;

		// Token: 0x04001C9A RID: 7322
		private ChainValidityStatus? tlsRemoteCertificateChainValidationStatus;

		// Token: 0x04001C9B RID: 7323
		private RawDataHandler rawDataHandler;

		// Token: 0x04001C9C RID: 7324
		private BufferBuilder sendBuffer = new BufferBuilder();

		// Token: 0x04001C9D RID: 7325
		private List<SmtpResponse> responseList;

		// Token: 0x04001C9E RID: 7326
		private DisconnectReason disconnectReason = DisconnectReason.Local;

		// Token: 0x04001C9F RID: 7327
		private readonly Breadcrumbs<SmtpInSessionBreadcrumbs> breadcrumbs = new Breadcrumbs<SmtpInSessionBreadcrumbs>(64);

		// Token: 0x04001CA0 RID: 7328
		private readonly bool certificatesLoadedSuccessfully;

		// Token: 0x04001CA1 RID: 7329
		private bool isLastCommandEhloBeforeQuit;

		// Token: 0x04001CA2 RID: 7330
		private ISmtpAvailabilityPerfCounters smtpAvailabilityPerfCounters;

		// Token: 0x04001CA3 RID: 7331
		private ISmtpReceivePerfCounters smtpReceivePerfCountersInstance;

		// Token: 0x04001CA4 RID: 7332
		private SmtpProxyPerfCountersWrapper smtpProxyPerfCounters;

		// Token: 0x04001CA5 RID: 7333
		private readonly bool maxConnectionsExceeded;

		// Token: 0x04001CA6 RID: 7334
		private readonly bool maxConnectionsPerSourceExceeded;

		// Token: 0x04001CA7 RID: 7335
		private readonly string relatedMessageInfo;

		// Token: 0x04001CA8 RID: 7336
		private SmtpInSession.DelayedAckStatus delayedAckStatus;

		// Token: 0x04001CA9 RID: 7337
		private TransportMiniRecipient authUserRecipient;

		// Token: 0x04001CAA RID: 7338
		private string senderShadowContext;

		// Token: 0x04001CAB RID: 7339
		private SmtpReceiveCapabilities? tlsDomainCapabilities;

		// Token: 0x04001CAC RID: 7340
		private bool startTlsDisabled;

		// Token: 0x04001CAD RID: 7341
		private bool forceRequestClientTlsCertificate;

		// Token: 0x04001CAE RID: 7342
		private bool startClientProxySession;

		// Token: 0x04001CAF RID: 7343
		private bool startOutboundProxySession;

		// Token: 0x04001CB0 RID: 7344
		private bool shutdownConnectionCalled;

		// Token: 0x04001CB1 RID: 7345
		private string proxyHopHelloDomain = string.Empty;

		// Token: 0x04001CB2 RID: 7346
		private InboundClientProxyStates inboundClientProxyState;

		// Token: 0x04001CB3 RID: 7347
		private int bytesToBeProxied;

		// Token: 0x04001CB4 RID: 7348
		private ProxySessionSetupHandler proxySetupHandler;

		// Token: 0x04001CB5 RID: 7349
		private uint xProxyFromSeqNum;

		// Token: 0x04001CB6 RID: 7350
		private Queue<SmtpMessageContextBlob> expectedBlobs;

		// Token: 0x04001CB7 RID: 7351
		private bool supportIntegratedAuth = true;

		// Token: 0x04001CB8 RID: 7352
		private bool isLastCommandAuthBeforeQuit;

		// Token: 0x04001CB9 RID: 7353
		private bool isFrontEndProxyingInbound;

		// Token: 0x04001CBA RID: 7354
		private bool clientIpConnectionAlreadyRemoved;

		// Token: 0x04001CBB RID: 7355
		private MailCommandMessageContextParameters mailCommandMessageContextInformation;

		// Token: 0x04001CBC RID: 7356
		private bool clientProxyFailedDueToIncompatibleBackend;

		// Token: 0x04001CBD RID: 7357
		private IMExSession mexSession;

		// Token: 0x04001CBE RID: 7358
		private string peerSessionPrimaryServer;

		// Token: 0x04001CBF RID: 7359
		private Permission? anonymousPermissions;

		// Token: 0x04001CC0 RID: 7360
		private Permission? partnerPermissions;

		// Token: 0x04001CC1 RID: 7361
		private bool usedForBlindProxy;

		// Token: 0x04001CC2 RID: 7362
		private readonly ISmtpMessageContextBlob smtpMessageContextBlob;

		// Token: 0x04001CC3 RID: 7363
		private bool smtpUtf8Supported;

		// Token: 0x020004C3 RID: 1219
		private enum DelayedAckStatus
		{
			// Token: 0x04001CCE RID: 7374
			None,
			// Token: 0x04001CCF RID: 7375
			Stamped,
			// Token: 0x04001CD0 RID: 7376
			ShadowRedundancyManagerNotified,
			// Token: 0x04001CD1 RID: 7377
			WaitingForShadowRedundancyManager
		}

		// Token: 0x020004C4 RID: 1220
		private sealed class WriteCompleteLogCallbackParameters
		{
			// Token: 0x06003843 RID: 14403 RVA: 0x000E7B7C File Offset: 0x000E5D7C
			public WriteCompleteLogCallbackParameters(SmtpInSession session, List<SmtpResponse> responseList, AsyncCallback callback, object callbackContextParam, bool alwaysCall)
			{
				this.session = session;
				this.responseList = responseList;
				this.callback = callback;
				this.callbackContextParam = callbackContextParam;
				this.alwaysCall = alwaysCall;
			}

			// Token: 0x170010F8 RID: 4344
			// (get) Token: 0x06003844 RID: 14404 RVA: 0x000E7BA9 File Offset: 0x000E5DA9
			public SmtpInSession Session
			{
				get
				{
					return this.session;
				}
			}

			// Token: 0x170010F9 RID: 4345
			// (get) Token: 0x06003845 RID: 14405 RVA: 0x000E7BB1 File Offset: 0x000E5DB1
			public List<SmtpResponse> ResponseList
			{
				get
				{
					return this.responseList;
				}
			}

			// Token: 0x170010FA RID: 4346
			// (get) Token: 0x06003846 RID: 14406 RVA: 0x000E7BB9 File Offset: 0x000E5DB9
			public AsyncCallback Callback
			{
				get
				{
					return this.callback;
				}
			}

			// Token: 0x170010FB RID: 4347
			// (get) Token: 0x06003847 RID: 14407 RVA: 0x000E7BC1 File Offset: 0x000E5DC1
			public object CallbackContextParam
			{
				get
				{
					return this.callbackContextParam;
				}
			}

			// Token: 0x170010FC RID: 4348
			// (get) Token: 0x06003848 RID: 14408 RVA: 0x000E7BC9 File Offset: 0x000E5DC9
			public bool AlwaysCall
			{
				get
				{
					return this.alwaysCall;
				}
			}

			// Token: 0x04001CD2 RID: 7378
			private readonly SmtpInSession session;

			// Token: 0x04001CD3 RID: 7379
			private readonly List<SmtpResponse> responseList;

			// Token: 0x04001CD4 RID: 7380
			private readonly AsyncCallback callback;

			// Token: 0x04001CD5 RID: 7381
			private readonly object callbackContextParam;

			// Token: 0x04001CD6 RID: 7382
			private readonly bool alwaysCall;
		}

		// Token: 0x020004C5 RID: 1221
		private sealed class BlindProxyContext
		{
			// Token: 0x06003849 RID: 14409 RVA: 0x000E7BD1 File Offset: 0x000E5DD1
			public BlindProxyContext(SmtpInSession session, NetworkConnection proxyConnection, IProtocolLogSession blindProxySendLogSession, ulong blindProxySendSessionId)
			{
				if (proxyConnection == null)
				{
					throw new ArgumentNullException("proxyConnection");
				}
				this.Session = session;
				this.ProxyConnection = proxyConnection;
				this.BlindProxySendLogSession = blindProxySendLogSession;
				this.BlindProxySendSessionId = blindProxySendSessionId;
			}

			// Token: 0x04001CD7 RID: 7383
			public readonly SmtpInSession Session;

			// Token: 0x04001CD8 RID: 7384
			public readonly NetworkConnection ProxyConnection;

			// Token: 0x04001CD9 RID: 7385
			public readonly IProtocolLogSession BlindProxySendLogSession;

			// Token: 0x04001CDA RID: 7386
			public readonly ulong BlindProxySendSessionId;

			// Token: 0x04001CDB RID: 7387
			public int IsProxyClientWritePending;

			// Token: 0x04001CDC RID: 7388
			public int XRsetProxyToResponseNeeded;

			// Token: 0x04001CDD RID: 7389
			public int XRsetProxyToResponseWriteOwner;

			// Token: 0x04001CDE RID: 7390
			public int IsProxyTargetWritePending;

			// Token: 0x04001CDF RID: 7391
			public int QuitCommandToTargetNeeded;

			// Token: 0x04001CE0 RID: 7392
			public int QuitCommandToTargetWriteOwner;

			// Token: 0x04001CE1 RID: 7393
			public int BlindProxyWorkDone;
		}
	}
}
