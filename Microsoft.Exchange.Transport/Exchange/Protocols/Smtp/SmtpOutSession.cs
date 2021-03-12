using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;
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
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000397 RID: 919
	internal class SmtpOutSession : ISmtpSession, ISmtpOutSession
	{
		// Token: 0x06002877 RID: 10359 RVA: 0x0009DD80 File Offset: 0x0009BF80
		public SmtpOutSession(ulong sessionId, SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection, IPEndPoint target, ProtocolLog protocolLog, ProtocolLoggingLevel loggingLevel, IMailRouter mailRouter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, bool isProbeSession = false)
		{
			if (smtpOutConnection == null)
			{
				throw new ArgumentNullException("smtpOutConnection");
			}
			if (nextHopConnection == null)
			{
				throw new ArgumentNullException("nextHopConnection");
			}
			if (smtpOutConnection.Connector == null)
			{
				throw new ArgumentException("Outbound Smtp Connection has a null connector.", "nextHopConnection");
			}
			this.smtpOutConnection = smtpOutConnection;
			this.nextHopConnection = nextHopConnection;
			this.sendConnector = smtpOutConnection.Connector;
			this.sessionPermissions = this.sendConnector.GetAnonymousPermissions();
			this.smtpSendPerformanceCounters = smtpOutConnection.SmtpSendPerformanceCounters;
			this.mailRouter = mailRouter;
			this.certificateCache = certificateCache;
			this.certificateValidator = certificateValidator;
			this.TlsConfiguration = smtpOutConnection.TlsConfig;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
			this.connectorPassword = this.sendConnector.GetSmartHostPassword();
			this.response = new List<string>(50);
			this.pipelinedCommandQueue = new Queue();
			this.pipelinedResponseQueue = new Queue();
			this.sessionProps = new SmtpSessionProps(sessionId);
			this.commandLists = new SmtpOutSession.CommandList[24];
			for (int i = 0; i < 24; i++)
			{
				this.commandLists[i] = new SmtpOutSession.CommandList((SmtpOutSession.SessionState)i);
			}
			this.CurrentState = SmtpOutSession.SessionState.ConnectResponse;
			this.EnqueueResponseHandler(null);
			this.sessionProps.AdvertisedEhloOptions = new EhloOptions();
			SmtpDomain fqdn = this.Connector.Fqdn;
			this.sessionProps.HelloDomain = ((fqdn != null && !string.IsNullOrEmpty(fqdn.Domain)) ? fqdn.Domain : ComputerInformation.DnsPhysicalFullyQualifiedDomainName);
			this.sessionProps.RemoteEndPoint = target;
			this.shadowRedundancyEnabled = (this.shadowRedundancyManager != null && this.shadowRedundancyManager.Configuration.Enabled);
			this.useDowngradedExchangeServerAuth = this.transportConfiguration.LocalServer.TransportServer.UseDowngradedExchangeServerAuth;
			this.LoadCertificate();
			string connectorId;
			if (smtpOutConnection.NextHopIsOutboundProxy)
			{
				IEnumerable<INextHopServer> enumerable;
				TlsSendConfiguration tlsSendConfiguration;
				RiskLevel riskLevel;
				int num;
				smtpOutConnection.GetOutboundProxyDestinationSettings(out enumerable, out this.outboundProxySendConnector, out tlsSendConfiguration, out riskLevel, out num);
				connectorId = this.outboundProxySendConnector.Id.ToString();
			}
			else
			{
				connectorId = this.sendConnector.Id.ToString();
			}
			this.logSession = protocolLog.OpenSession(connectorId, this.SessionId, target, null, loggingLevel);
			this.SetDefaultIdentity();
			if (this.NextHopConnection.Key.NextHopType == NextHopType.Heartbeat)
			{
				this.dontCacheThisConnection = true;
			}
			else if (this.nextHopConnection.Key.NextHopType == NextHopType.ShadowRedundancy && !this.transportAppConfig.ConnectionCacheConfig.EnableShadowConnectionCache)
			{
				this.dontCacheThisConnection = true;
			}
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "attempting to connect");
			ExTraceGlobals.SmtpSendTracer.TraceDebug<IPEndPoint>((long)this.GetHashCode(), "Attempting to connect to {0}", target);
			SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendNewSession, null, new object[]
			{
				this.sendConnector.Name,
				target
			});
			this.TlsConfiguration.LogTlsOverride(this.logSession);
			this.isProbeSession = isProbeSession;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0009E0A5 File Offset: 0x0009C2A5
		protected SmtpOutSession()
		{
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x0009E0DB File Offset: 0x0009C2DB
		public ulong SessionId
		{
			get
			{
				return this.sessionProps.SessionId;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.sessionProps.RemoteEndPoint;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x0009E0F5 File Offset: 0x0009C2F5
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.sessionProps.LocalEndPoint;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x0600287C RID: 10364 RVA: 0x0009E102 File Offset: 0x0009C302
		public virtual DateTime SessionStartTime
		{
			get
			{
				return this.sessionStartTime;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x0009E10A File Offset: 0x0009C30A
		public string HelloDomain
		{
			get
			{
				return this.sessionProps.HelloDomain;
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x0600287E RID: 10366 RVA: 0x0009E117 File Offset: 0x0009C317
		// (set) Token: 0x0600287F RID: 10367 RVA: 0x0009E124 File Offset: 0x0009C324
		public SmtpResponse Banner
		{
			get
			{
				return this.sessionProps.Banner;
			}
			set
			{
				this.sessionProps.Banner = value;
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0009E132 File Offset: 0x0009C332
		public IEhloOptions AdvertisedEhloOptions
		{
			get
			{
				return this.sessionProps.AdvertisedEhloOptions;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0009E13F File Offset: 0x0009C33F
		public bool SupportLongAddresses
		{
			get
			{
				return this.AdvertisedEhloOptions.XLongAddr || this.SupportExch50;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x0009E156 File Offset: 0x0009C356
		public bool SupportOrar
		{
			get
			{
				return this.AdvertisedEhloOptions.XOrar || this.SupportExch50;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002883 RID: 10371 RVA: 0x0009E16D File Offset: 0x0009C36D
		public bool SupportRDst
		{
			get
			{
				return this.AdvertisedEhloOptions.XRDst || this.SupportExch50;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0009E184 File Offset: 0x0009C384
		public bool SupportSmtpUtf8
		{
			get
			{
				return this.AdvertisedEhloOptions.SmtpUtf8 && (this.AdvertisedEhloOptions.EightBitMime || this.AdvertisedEhloOptions.BinaryMime);
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x0009E1AF File Offset: 0x0009C3AF
		public virtual bool SupportExch50
		{
			get
			{
				return this.transportConfiguration.TransportSettings.TransportSettings.Xexch50Enabled && this.AdvertisedEhloOptions.Xexch50 && (this.Permissions & Permission.SMTPSendEXCH50) > Permission.None;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06002886 RID: 10374 RVA: 0x0009E1E8 File Offset: 0x0009C3E8
		public bool ShouldSendExch50blob
		{
			get
			{
				return this.SupportExch50 && (this.exch50DataPresent || this.NextHopDeliveryType == DeliveryType.SmtpRelayToTiRg || this.NextHopType.IsSmtpConnectorDeliveryType);
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x0009E222 File Offset: 0x0009C422
		public virtual bool SendShadow
		{
			get
			{
				return this.shadowRedundancyEnabled && this.shadowRedundancyManager.ShouldSmtpOutSendXShadow(this.Permissions, this.NextHopDeliveryType, this.AdvertisedEhloOptions, this.Connector);
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x0009E251 File Offset: 0x0009C451
		public virtual bool SendXShadowRequest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x0009E254 File Offset: 0x0009C454
		public virtual bool SendXQDiscard
		{
			get
			{
				return this.Shadowed && this.shadowRedundancyManager != null && this.shadowRedundancyManager.ShouldSmtpOutSendXQDiscard(this.SmtpHost);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x0600288A RID: 10378 RVA: 0x0009E279 File Offset: 0x0009C479
		public virtual bool ShouldReduceRecipientCacheForTransmission
		{
			get
			{
				return this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.Hub;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x0009E289 File Offset: 0x0009C489
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x0009E291 File Offset: 0x0009C491
		public bool Shadowed
		{
			get
			{
				return this.shadowed;
			}
			set
			{
				this.shadowed = value;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0009E29A File Offset: 0x0009C49A
		public string SmtpHost
		{
			get
			{
				return this.smtpOutConnection.SmtpHost;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x0009E2A7 File Offset: 0x0009C4A7
		public Permission Permissions
		{
			get
			{
				return this.sessionPermissions;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x0009E2AF File Offset: 0x0009C4AF
		public bool HasTlsClientCertificate
		{
			get
			{
				return this.advertisedTlsCertificate != null;
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x0009E2BD File Offset: 0x0009C4BD
		public IProtocolLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0009E2C8 File Offset: 0x0009C4C8
		public string NextHopDomain
		{
			get
			{
				return this.nextHopConnection.Key.NextHopDomain;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x0009E2E8 File Offset: 0x0009C4E8
		public DeliveryType NextHopDeliveryType
		{
			get
			{
				return this.NextHopType.DeliveryType;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x0009E304 File Offset: 0x0009C504
		public NextHopType NextHopType
		{
			get
			{
				return this.NextHopConnection.Key.NextHopType;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x0009E324 File Offset: 0x0009C524
		// (set) Token: 0x06002895 RID: 10389 RVA: 0x0009E32C File Offset: 0x0009C52C
		public MultilevelAuthMechanism AuthMethod
		{
			get
			{
				return this.authMethod;
			}
			set
			{
				this.authMethod = value;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002896 RID: 10390 RVA: 0x0009E335 File Offset: 0x0009C535
		// (set) Token: 0x06002897 RID: 10391 RVA: 0x0009E33D File Offset: 0x0009C53D
		public bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
			set
			{
				this.isAuthenticated = value;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0009E346 File Offset: 0x0009C546
		public bool RemoteIsAuthenticated
		{
			get
			{
				return !this.RemoteIdentity.IsWellKnown(WellKnownSidType.AnonymousSid);
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x0009E358 File Offset: 0x0009C558
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x0009E360 File Offset: 0x0009C560
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

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x0009E369 File Offset: 0x0009C569
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x0009E371 File Offset: 0x0009C571
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

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x0009E37C File Offset: 0x0009C57C
		public RestrictedHeaderSet RestrictedHeaderSet
		{
			get
			{
				RestrictedHeaderSet restrictedHeaderSet = RestrictedHeaderSet.None;
				if ((this.Permissions & Permission.SendRoutingHeaders) == Permission.None)
				{
					restrictedHeaderSet |= RestrictedHeaderSet.MTA;
				}
				if ((this.Permissions & Permission.SendForestHeaders) == Permission.None)
				{
					restrictedHeaderSet |= RestrictedHeaderSet.Forest;
				}
				if ((this.Permissions & Permission.SendOrganizationHeaders) == Permission.None)
				{
					restrictedHeaderSet |= RestrictedHeaderSet.Organization;
				}
				return restrictedHeaderSet;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x0600289E RID: 10398 RVA: 0x0009E3C2 File Offset: 0x0009C5C2
		public SmtpSessionProps SessionProps
		{
			get
			{
				return this.sessionProps;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x0009E3CA File Offset: 0x0009C5CA
		public NextHopConnection NextHopConnection
		{
			get
			{
				return this.nextHopConnection;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x0009E3D2 File Offset: 0x0009C5D2
		public AckDetails AckDetails
		{
			get
			{
				return this.ackDetails;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x0009E3DA File Offset: 0x0009C5DA
		public IReadOnlyMailItem RoutedMailItem
		{
			get
			{
				return this.routedMailItem;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060028A2 RID: 10402 RVA: 0x0009E3E2 File Offset: 0x0009C5E2
		public virtual bool ShadowCurrentMailItem
		{
			get
			{
				return this.shadowCurrentMailItem;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x0009E3EA File Offset: 0x0009C5EA
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x0009E3F2 File Offset: 0x0009C5F2
		public MailRecipient CurrentRecipient
		{
			get
			{
				return this.currentRecipient;
			}
			set
			{
				this.currentRecipient = value;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0009E3FB File Offset: 0x0009C5FB
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0009E403 File Offset: 0x0009C603
		public MailRecipient NextRecipient
		{
			get
			{
				return this.nextRecipient;
			}
			set
			{
				this.nextRecipient = value;
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0009E40C File Offset: 0x0009C60C
		// (set) Token: 0x060028A8 RID: 10408 RVA: 0x0009E414 File Offset: 0x0009C614
		public int NumberOfRecipientsAttempted
		{
			get
			{
				return this.numberOfRecipientsAttempted;
			}
			set
			{
				this.numberOfRecipientsAttempted = value;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060028A9 RID: 10409 RVA: 0x0009E41D File Offset: 0x0009C61D
		public int NumberOfRecipientsSucceeded
		{
			get
			{
				return this.numberOfRecipientsSucceeded;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x0009E425 File Offset: 0x0009C625
		public int NumberOfRecipientsAckedForRetry
		{
			get
			{
				return this.numberOfRecipientsAckedForRetry;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060028AB RID: 10411 RVA: 0x0009E42D File Offset: 0x0009C62D
		// (set) Token: 0x060028AC RID: 10412 RVA: 0x0009E435 File Offset: 0x0009C635
		public int NumberOfRecipientsAcked
		{
			get
			{
				return this.numberOfRecipientsAcked;
			}
			set
			{
				this.numberOfRecipientsAcked = value;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x0009E43E File Offset: 0x0009C63E
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x0009E446 File Offset: 0x0009C646
		public bool BetweenMessagesRset
		{
			get
			{
				return this.betweenMessagesRset;
			}
			set
			{
				this.betweenMessagesRset = value;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x0009E44F File Offset: 0x0009C64F
		public SecureState SecureState
		{
			get
			{
				return this.secureState;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x0009E457 File Offset: 0x0009C657
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x0009E45F File Offset: 0x0009C65F
		public SmtpOutSession.SessionState NextState
		{
			get
			{
				return this.nextState;
			}
			set
			{
				this.nextState = value;
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x0009E468 File Offset: 0x0009C668
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x0009E470 File Offset: 0x0009C670
		public bool RecipientsAckedPending
		{
			get
			{
				return this.recipsAckedPending;
			}
			set
			{
				this.recipsAckedPending = value;
			}
		}

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0009E479 File Offset: 0x0009C679
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x0009E481 File Offset: 0x0009C681
		public SmtpSendConnectorConfig Connector
		{
			get
			{
				return this.sendConnector;
			}
			set
			{
				this.sendConnector = value;
			}
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x0009E48A File Offset: 0x0009C68A
		public SmtpSendConnectorConfig.AuthMechanisms AuthMechanism
		{
			get
			{
				return this.Connector.SmartHostAuthMechanism;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060028B7 RID: 10423 RVA: 0x0009E498 File Offset: 0x0009C698
		public string AuthenticationUsername
		{
			get
			{
				string authenticationUserName = this.Connector.GetAuthenticationUserName();
				if (!string.IsNullOrEmpty(authenticationUserName))
				{
					return authenticationUserName;
				}
				return null;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x0009E4BC File Offset: 0x0009C6BC
		public SecureString AuthenticationPassword
		{
			get
			{
				return this.connectorPassword;
			}
		}

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x060028B9 RID: 10425 RVA: 0x0009E4C4 File Offset: 0x0009C6C4
		// (set) Token: 0x060028BA RID: 10426 RVA: 0x0009E4CC File Offset: 0x0009C6CC
		public bool UsingHELO
		{
			get
			{
				return this.usingHELO;
			}
			set
			{
				this.usingHELO = value;
			}
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0009E4D5 File Offset: 0x0009C6D5
		public SmtpSendPerfCountersInstance SmtpSendPerformanceCounters
		{
			get
			{
				return this.smtpSendPerformanceCounters;
			}
		}

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x0009E4DD File Offset: 0x0009C6DD
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x0009E4E5 File Offset: 0x0009C6E5
		public bool NeedToDownConvertMIME
		{
			get
			{
				return this.needToDownconvertMIME;
			}
			set
			{
				this.needToDownconvertMIME = value;
			}
		}

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x0009E4EE File Offset: 0x0009C6EE
		public bool PipeLineNextMessagePending
		{
			get
			{
				return this.pipeLineNextMessagePending;
			}
		}

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x0009E4F6 File Offset: 0x0009C6F6
		public bool PipeLineFailOverPending
		{
			get
			{
				return this.pipeLineFailOverPending;
			}
		}

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x0009E4FE File Offset: 0x0009C6FE
		public virtual bool Disconnected
		{
			get
			{
				return this.disconnected;
			}
		}

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x0009E506 File Offset: 0x0009C706
		public long ConnectionId
		{
			get
			{
				return this.connectionId;
			}
		}

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x0009E510 File Offset: 0x0009C710
		public string CurrentMessageTemporaryId
		{
			get
			{
				string[] value = new string[]
				{
					this.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
					this.sessionStartTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo),
					this.messageCount.ToString()
				};
				return string.Join(";", value);
			}
		}

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x0009E574 File Offset: 0x0009C774
		public bool IsNextHopDomainSecured
		{
			get
			{
				return this.Connector.DomainSecureEnabled && this.transportConfiguration.TransportSettings.TransportSettings.IsTLSSendSecureDomain(this.NextHopConnection.Key.NextHopDomain);
			}
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x0009E5B8 File Offset: 0x0009C7B8
		public bool RequiresDirectTrust
		{
			get
			{
				return this.AuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer && (this.sendConnector.IsInitialSendConnector() || this.NextHopDeliveryType == DeliveryType.SmtpRelayWithinAdSiteToEdge || (this.NextHopDeliveryType == DeliveryType.Heartbeat && (!Components.IsBridgehead || !this.IsHubServer(this.NextHopDomain))));
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x0009E60C File Offset: 0x0009C80C
		public bool IsInternalTransportCertificateAvailable
		{
			get
			{
				return this.internalTransportCertificate != null;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x0009E61A File Offset: 0x0009C81A
		public bool IsOpportunisticTls
		{
			get
			{
				return (this.SecureState == SecureState.AnonymousTls || this.SecureState == SecureState.StartTls) && (!this.TlsConfiguration.RequireTls && this.AuthMechanism != SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS && this.AuthMechanism != SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer) && !this.IsNextHopDomainSecured;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x0009E65A File Offset: 0x0009C85A
		// (set) Token: 0x060028C8 RID: 10440 RVA: 0x0009E667 File Offset: 0x0009C867
		public ulong DiscardIdsReceived
		{
			get
			{
				return this.smtpOutConnection.DiscardIdsReceived;
			}
			set
			{
				this.smtpOutConnection.DiscardIdsReceived = value;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0009E678 File Offset: 0x0009C878
		public bool CanDowngradeExchangeServerAuth
		{
			get
			{
				return this.useDowngradedExchangeServerAuth && (this.NextHopType.IsHubRelayDeliveryType || this.SendingIntraOrgHeartbeatFromHubToHub);
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x0009E6A8 File Offset: 0x0009C8A8
		public bool SendingIntraOrgHeartbeatFromHubToHub
		{
			get
			{
				bool flag = string.Equals(this.Connector.Name, Strings.IntraorgSendConnectorName);
				return this.NextHopDeliveryType == DeliveryType.Heartbeat && flag && Components.IsBridgehead && this.IsHubServer(this.NextHopDomain);
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x0009E6F2 File Offset: 0x0009C8F2
		public Queue<SmtpMessageContextBlob> BlobsToSend
		{
			get
			{
				return this.blobsToSend;
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x0009E6FA File Offset: 0x0009C8FA
		public RecipientCorrelator RecipientCorrelator
		{
			get
			{
				return this.recipientCorrelator;
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0009E702 File Offset: 0x0009C902
		public int MessagesSentOverSession
		{
			get
			{
				return this.messagesSentOverSession;
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x060028CE RID: 10446 RVA: 0x0009E70A File Offset: 0x0009C90A
		// (set) Token: 0x060028CF RID: 10447 RVA: 0x0009E712 File Offset: 0x0009C912
		public Queue<string> RemainingXProxyToCommands
		{
			get
			{
				return this.remainingXProxyToCommands;
			}
			set
			{
				this.remainingXProxyToCommands = value;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0009E71B File Offset: 0x0009C91B
		public bool XRsetProxyToAccepted
		{
			set
			{
				this.xRsetProxyToAccepted = value;
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0009E724 File Offset: 0x0009C924
		public bool IsProbeSession
		{
			get
			{
				return this.isProbeSession;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x0009E72C File Offset: 0x0009C92C
		// (set) Token: 0x060028D3 RID: 10451 RVA: 0x0009E734 File Offset: 0x0009C934
		internal TlsSendConfiguration TlsConfiguration { get; private set; }

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x0009E73D File Offset: 0x0009C93D
		protected virtual bool MessageContextBlobTransferSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x0009E740 File Offset: 0x0009C940
		protected virtual bool SendFewerMessagesToSlowerServerEnabled
		{
			get
			{
				return this.transportAppConfig.SmtpSendConfiguration.SendFewerMessagesToSlowerServerEnabled;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x0009E752 File Offset: 0x0009C952
		protected virtual bool FailoverPermittedForRemoteShutdown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x0009E755 File Offset: 0x0009C955
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x0009E75D File Offset: 0x0009C95D
		private SmtpOutSession.SessionState CurrentState
		{
			get
			{
				return this.currentState;
			}
			set
			{
				this.currentState = value;
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0009E768 File Offset: 0x0009C968
		public static bool MatchCertificateWithTlsDomain(IList<SmtpDomainWithSubdomains> tlsDomains, IX509Certificate2 remoteCertificate, IProtocolLogSession protocolLogSession, CertificateValidator certificateValidatorInstance)
		{
			foreach (SmtpDomainWithSubdomains domain in tlsDomains)
			{
				if (certificateValidatorInstance.MatchCertificateFqdns(domain, remoteCertificate, MatchOptions.None, protocolLogSession))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0009E7BC File Offset: 0x0009C9BC
		public static X509Certificate2 LoadTlsCertificate(TlsSendConfiguration tlsConfiguration, CertificateCache cache, bool oneLevelWildcardMatch, string connectorName, int hashCode)
		{
			if (tlsConfiguration.ShouldSkipTls)
			{
				return null;
			}
			string text;
			X509Certificate2 x509Certificate;
			if (tlsConfiguration.TlsCertificateName != null)
			{
				text = tlsConfiguration.TlsCertificateName.ToString();
				x509Certificate = cache.Find(tlsConfiguration.TlsCertificateName);
			}
			else
			{
				text = (string.IsNullOrEmpty(tlsConfiguration.TlsCertificateFqdn) ? ComputerInformation.DnsPhysicalFullyQualifiedDomainName : tlsConfiguration.TlsCertificateFqdn);
				List<string> names = new List<string>
				{
					text
				};
				x509Certificate = cache.Find(names, false, WildcardMatchType.OneLevel);
				if (x509Certificate == null)
				{
					x509Certificate = cache.Find(names, true, oneLevelWildcardMatch ? WildcardMatchType.OneLevel : WildcardMatchType.MultiLevel);
				}
			}
			if (x509Certificate == null)
			{
				string text2 = string.Format("Can't load STARTTLS certificate for {0}", text);
				ExTraceGlobals.SmtpSendTracer.TraceError((long)hashCode, text2);
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_CannotLoadSTARTTLSCertificateFromStore, connectorName, new object[]
				{
					text,
					connectorName
				});
				EventNotificationItem.PublishPeriodic(ExchangeComponent.Transport.Name, "CannotLoadSTARTTLSCertificateFromStore", null, string.Format("Connector: '{0}' Error: '{1}'", connectorName, text2), "CannotLoadSTARTTLSCertificateFromStore", TimeSpan.FromMinutes(5.0), ResultSeverityLevel.Error, false);
				if (tlsConfiguration.TlsCertificateName != null)
				{
					throw new TlsCertificateNameNotFoundException(tlsConfiguration.TlsCertificateName.ToString(), connectorName);
				}
			}
			else if ((tlsConfiguration.TlsAuthLevel == null || tlsConfiguration.TlsAuthLevel.Value != RequiredTlsAuthLevel.EncryptionOnly) && CertificateExpiryCheck.CheckCertificateExpiry(x509Certificate, SmtpOutConnection.Events, SmtpSessionCertificateUse.STARTTLS, text))
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)hashCode, "STARTTLS certificate for {0} for outbound session is expired", text);
			}
			return x509Certificate;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0009E91B File Offset: 0x0009CB1B
		public void ResetAdvertisedEhloOptions()
		{
			this.sessionProps.AdvertisedEhloOptions = new EhloOptions();
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0009E930 File Offset: 0x0009CB30
		public virtual void ResetSession(SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection)
		{
			if (smtpOutConnection == null)
			{
				throw new ArgumentNullException("smtpOutConnection");
			}
			if (nextHopConnection == null)
			{
				throw new ArgumentNullException("nextHopConnection");
			}
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ResetSession);
			this.presentInSmtpOutSessionCache = false;
			this.smtpOutConnection.RemoveConnection();
			this.messagesSentOverSession = 0;
			this.smtpOutConnection = smtpOutConnection;
			this.nextHopConnection = nextHopConnection;
			if (this.smtpOutConnection.NextHopIsOutboundProxy)
			{
				IEnumerable<INextHopServer> enumerable;
				TlsSendConfiguration tlsSendConfiguration;
				RiskLevel riskLevel;
				int num;
				smtpOutConnection.GetOutboundProxyDestinationSettings(out enumerable, out this.outboundProxySendConnector, out tlsSendConfiguration, out riskLevel, out num);
				string connectorId = this.outboundProxySendConnector.Id.ToString();
				this.logSession = smtpOutConnection.ProtocolLog.OpenSession(connectorId, this.SessionId, this.RemoteEndPoint, null, this.outboundProxySendConnector.ProtocolLoggingLevel);
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0009E9E8 File Offset: 0x0009CBE8
		public bool CheckDomainSecure<T>(bool condition, string trace, ExEventLog.EventTuple log, T additionalInformation)
		{
			string nextHopDomain = this.nextHopConnection.Key.NextHopDomain;
			if (this.transportConfiguration.TransportSettings.TransportSettings.IsTLSSendSecureDomain(nextHopDomain))
			{
				SmtpResponse smtpResponse = SmtpResponse.RequireTLSToSendMail;
				if (!this.Connector.DomainSecureEnabled)
				{
					trace = "DomainSecureEnabled was set to false";
					log = TransportEventLogConstants.Tuple_TlsDomainSecureDisabled;
					smtpResponse = SmtpResponse.DomainSecureDisabled;
				}
				else if (condition)
				{
					return true;
				}
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Message to secure domain '{0}' on send connector '{1}' failed because {2} {3}.", new object[]
				{
					nextHopDomain,
					this.Connector.Name,
					trace,
					additionalInformation
				});
				SmtpCommand.EventLogger.LogEvent(log, nextHopDomain + "-" + this.Connector.Name, new object[]
				{
					nextHopDomain,
					this.Connector.Name,
					additionalInformation
				});
				string context = string.Format(CultureInfo.InvariantCulture, "Message to secure domain '{0}' on send connector '{1}' failed because {2} {3}.", new object[]
				{
					nextHopDomain,
					this.Connector.Name,
					trace,
					additionalInformation
				});
				this.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, context);
				this.FailoverConnection(smtpResponse, SessionSetupFailureReason.ProtocolError);
				Utils.SecureMailPerfCounters.DomainSecureOutboundSessionFailuresTotal.Increment();
				this.NextState = SmtpOutSession.SessionState.Quit;
				return false;
			}
			return true;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0009EB50 File Offset: 0x0009CD50
		public bool CheckRequireOorg()
		{
			if (!this.Connector.RequireOorg || this.AdvertisedEhloOptions.XOorg)
			{
				return true;
			}
			ExTraceGlobals.SmtpSendTracer.TraceError<IPEndPoint, string, string>((long)this.GetHashCode(), "Connection to remote endpoint '{0} ({1})' for send connector '{2}' will be dropped because the server did not advertise XOORG.", this.connection.RemoteEndPoint, this.smtpOutConnection.SmtpHostName, this.Connector.Name);
			SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SessionFailedBecauseXOorgNotOffered, this.Connector.Name + "-" + this.connection.RemoteEndPoint, new object[]
			{
				this.connection.RemoteEndPoint,
				this.smtpOutConnection.SmtpHostName,
				this.Connector.Name
			});
			string context = string.Format(CultureInfo.InvariantCulture, "Connection to remote endpoint '{0} ({1})' for send connector '{2}' will be dropped because the server did not advertise XOORG.", new object[]
			{
				this.connection.RemoteEndPoint,
				this.smtpOutConnection.SmtpHostName,
				this.Connector.Name
			});
			this.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, context);
			this.FailoverConnection(SmtpResponse.RequireXOorgToSendMail, SessionSetupFailureReason.ProtocolError);
			this.NextState = SmtpOutSession.SessionState.Quit;
			return false;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0009EC77 File Offset: 0x0009CE77
		public void Disconnect()
		{
			this.Disconnect(DisconnectReason.Local, false, false, null, SessionSetupFailureReason.None);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0009EC84 File Offset: 0x0009CE84
		public byte[] GetTlsEapKey()
		{
			return this.connection.TlsEapKey;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0009EC91 File Offset: 0x0009CE91
		public byte[] GetCertificatePublicKey()
		{
			return this.connection.RemoteCertificate.GetPublicKey();
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0009ECA3 File Offset: 0x0009CEA3
		public virtual void ShutdownConnection()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.ShutdownConnection);
			if (this.connection != null)
			{
				this.connection.Shutdown();
			}
			this.shutdownConnectionCalled = true;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0009ECC8 File Offset: 0x0009CEC8
		public RoutingAddress GetShortAddress(RoutingAddress p1Address)
		{
			if (!Util.IsLongAddressForE2k3(p1Address))
			{
				return p1Address;
			}
			if (this.AdvertisedEhloOptions.XLongAddr || !this.SupportExch50)
			{
				return p1Address;
			}
			RoutingAddress result;
			if (Util.TryGetShortAddress(p1Address, out result))
			{
				this.exch50DataPresent = true;
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Replaced long address {0} with short address {1}", p1Address.ToString(), result.ToString());
				return result;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Address {0} was not converted to a short form", p1Address.ToString());
			return p1Address;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0009ED60 File Offset: 0x0009CF60
		public void ConnectionCompleted(NetworkConnection networkConnection)
		{
			this.connection = networkConnection;
			this.connectionId = networkConnection.ConnectionId;
			this.connection.MaxLineLength = 2000;
			this.logSession.LocalEndPoint = this.connection.LocalEndPoint;
			this.connection.Timeout = (int)this.Connector.ConnectionInactivityTimeOut.TotalSeconds;
			this.ackDetails = new AckDetails(this.connection.RemoteEndPoint, this.smtpOutConnection.SmtpHostName, this.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo), this.Connector.Id.ToString(), this.connection.LocalEndPoint.Address);
			this.AckDetails.AddEventData("Microsoft.Exchange.Transport.MailRecipient.RequiredTlsAuthLevel", SmtpOutSession.AuthLevelToString(this.TlsConfiguration.TlsAuthLevel));
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0009EE3E File Offset: 0x0009D03E
		public void StartUsingConnection()
		{
			this.sessionStartTime = DateTime.UtcNow;
			this.IncrementConnectionCounters();
			this.logSession.LogConnect();
			this.StartReadLine();
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0009EE64 File Offset: 0x0009D064
		public string GetConnectionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LastIndex : ");
			stringBuilder.Append(this.breadcrumbs.LastFilledIndex);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Session BreadCrumb : ");
			for (int i = 0; i < 64; i++)
			{
				stringBuilder.Append(Enum.Format(typeof(SmtpOutSession.SmtpOutSessionBreadcrumbs), this.breadcrumbs.BreadCrumb[i], "x"));
				stringBuilder.Append(" ");
			}
			stringBuilder.AppendLine();
			if (this.connection != null)
			{
				stringBuilder.Append(this.connection.GetBreadCrumbsInfo());
			}
			else
			{
				stringBuilder.AppendLine("connection = null");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0009EF23 File Offset: 0x0009D123
		public virtual void PrepareForNextMessageOnCachedSession()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.PrepareForNextMessageOnCachedSession);
			if (this.smtpOutConnection.NextHopIsOutboundProxy)
			{
				this.nextState = SmtpOutSession.SessionState.XProxyTo;
				this.MoveToNextState();
				return;
			}
			this.PrepareForNextMessage(true);
			this.EnqueueNextPipeLinedCommands();
			this.SendNextCommands();
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0009EF5B File Offset: 0x0009D15B
		public void FailoverConnection(SmtpResponse smtpResponse)
		{
			this.FailoverConnection(smtpResponse, SessionSetupFailureReason.ProtocolError);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0009EF65 File Offset: 0x0009D165
		public void FailoverConnection(SmtpResponse smtpResponse, SessionSetupFailureReason failoverReason)
		{
			this.FailoverConnection(smtpResponse, true, false, failoverReason);
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0009EF71 File Offset: 0x0009D171
		public void GetOutboundProxyDestinationSettings(out IEnumerable<INextHopServer> destinations, out SmtpSendConnectorConfig sendConnector, out TlsSendConfiguration tlsSendConfiguration, out RiskLevel riskLevel, out int outboundIPPool)
		{
			this.smtpOutConnection.GetOutboundProxyDestinationSettings(out destinations, out sendConnector, out tlsSendConfiguration, out riskLevel, out outboundIPPool);
			this.outboundProxySendConnector = sendConnector;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0009EF90 File Offset: 0x0009D190
		public void OutboundProxyConnectionEstablished(SmtpResponse xProxyToResponse, IPAddress proxyTargetIPAddress, string proxyTargetHostName, IEnumerable<INextHopServer> remainingDestinations, bool shouldSkipTls, int precedingXProxyToSpecificLines)
		{
			if (this.outboundProxySendConnector == null)
			{
				throw new InvalidOperationException("OutboundProxySendConnector should not be null");
			}
			this.nextHopIsProxyingBlindly = true;
			this.smtpOutConnection.UpdateOnSuccessfulOutboundProxySetup(remainingDestinations, shouldSkipTls);
			this.outboundProxyOriginalSessionState = new SmtpOutSession.OutboundProxyOriginalSessionState(this.sessionProps.AdvertisedEhloOptions, this.sendConnector, this.RemoteIdentity, this.RemoteIdentityName, this.AckDetails, this.sessionPermissions);
			this.sessionProps.AdvertisedEhloOptions = new EhloOptions();
			this.sessionProps.AdvertisedEhloOptions.ParseResponse(xProxyToResponse, this.RemoteEndPoint.Address, precedingXProxyToSpecificLines);
			if (!this.AdvertisedEhloOptions.Dsn)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Remote server does not support DSN. Relay DSNs will be generated for successful recipients as needed.");
				this.NextHopConnection.GenerateSuccessDSNs = DsnFlags.Relay;
			}
			this.sendConnector = this.outboundProxySendConnector;
			this.RemoteIdentity = SmtpOutSession.anonymousSecurityIdentifier;
			this.RemoteIdentityName = "anonymous";
			this.SetSessionPermissions(this.RemoteIdentity);
			this.SetDefaultIdentity();
			this.ackDetails = new AckDetails(this.connection.RemoteEndPoint, this.smtpOutConnection.SmtpHostName, this.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo), this.Connector.Id.ToString(), this.connection.LocalEndPoint.Address);
			this.ackDetails.AddEventData("OutboundProxyTargetIPAddress", proxyTargetIPAddress.ToString());
			if (!string.IsNullOrEmpty(proxyTargetHostName))
			{
				this.ackDetails.AddEventData("OutboundProxyTargetHostName", proxyTargetHostName);
			}
			if (!string.IsNullOrEmpty(this.helloDomainOfOutboundProxyFrontEnd))
			{
				this.ackDetails.AddEventData("OutboundProxyFrontendName", this.helloDomainOfOutboundProxyFrontEnd);
			}
			string context = "This session is now being proxied through the next hop. The actual connector being used is: " + this.sendConnector.Name;
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, context);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0009F15C File Offset: 0x0009D35C
		public void SetSessionPermissions(Permission permissions)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client {0} is granted the following permission {1}", this.RemoteIdentityName, permissions);
			this.sessionPermissions = permissions;
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permissions)), "Set Session Permissions");
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0009F1AC File Offset: 0x0009D3AC
		public void SetSessionPermissions(SecurityIdentifier client)
		{
			Permission permission = Permission.None;
			try
			{
				RawSecurityDescriptor securityDescriptor = this.sendConnector.GetSecurityDescriptor();
				if (securityDescriptor != null)
				{
					permission = AuthzAuthorization.CheckPermissions(client, securityDescriptor, null);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client {0} is granted the following permission {1}", this.RemoteIdentityName, permission);
				}
				else
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "Client {0}'s SD is null", this.RemoteIdentityName);
				}
			}
			catch (Win32Exception ex)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<int>((long)this.GetHashCode(), "AuthzAuthorization.CheckPermissions failed with {0}.", ex.NativeErrorCode);
			}
			this.sessionPermissions = permission;
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permission)), "Set Session Permissions");
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0009F264 File Offset: 0x0009D464
		public void SetSessionPermissions(IntPtr userToken)
		{
			Permission permission = Permission.None;
			try
			{
				RawSecurityDescriptor securityDescriptor = this.sendConnector.GetSecurityDescriptor();
				if (securityDescriptor != null)
				{
					permission = AuthzAuthorization.CheckPermissions(userToken, securityDescriptor, null);
					ExTraceGlobals.SmtpSendTracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client {0} is granted the following permission {1}", this.RemoteIdentityName, permission);
				}
				else
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "Client {0}'s SD is null", this.RemoteIdentityName);
				}
			}
			catch (Win32Exception ex)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<int>((long)this.GetHashCode(), "AuthzAuthorization.CheckPermissions failed with {0}.", ex.NativeErrorCode);
			}
			this.sessionPermissions = permission;
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permission)), "Set Session Permissions");
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0009F31C File Offset: 0x0009D51C
		public void EnqueueCommandList(SmtpOutSession.SessionState state)
		{
			if (state >= SmtpOutSession.SessionState.NumStates)
			{
				throw new InvalidOperationException("Cannot enqueue an unknown state");
			}
			if (state == SmtpOutSession.SessionState.XProxy)
			{
				throw new InvalidOperationException("Cannot enqueue XProxy in SmtpOut");
			}
			SmtpOutSession.CommandList obj = this.commandLists[(int)state];
			this.pipelinedCommandQueue.Enqueue(obj);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpOutSession.SessionState>((long)this.GetHashCode(), "Enqueued command List: {0}", state);
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0009F378 File Offset: 0x0009D578
		public void LoadCertificate()
		{
			if (this.RequiresDirectTrust)
			{
				string internalTransportCertificateThumbprint = this.transportConfiguration.LocalServer.TransportServer.InternalTransportCertificateThumbprint;
				if (internalTransportCertificateThumbprint != null)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<long>((long)this.GetHashCode(), "SmtpOutSession(id={0}). Loading Internal Transport Certificate for use with Direct Trust.", this.connectionId);
					this.internalTransportCertificate = this.certificateCache.GetInternalTransportCertificate(Components.Configuration.LocalServer.TransportServer.InternalTransportCertificateThumbprint, SmtpOutConnection.Events);
					if (this.internalTransportCertificate != null)
					{
						CertificateExpiryCheck.CheckCertificateExpiry(this.internalTransportCertificate, SmtpOutConnection.Events, SmtpSessionCertificateUse.DirectTrust, null);
					}
					else
					{
						ExTraceGlobals.SmtpSendTracer.TraceError<long>((long)this.GetHashCode(), "SmtpOutSession(id={0}). Internal Transport Certificate could not be loaded.", this.connectionId);
					}
				}
				else
				{
					ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "this.transportConfiguration.LocalServer.TransportServer.InternalTransportCertificateThumbprint is null. Examine AD/ADAM server object");
				}
			}
			this.advertisedTlsCertificate = SmtpOutSession.LoadTlsCertificate(this.TlsConfiguration, this.certificateCache, this.transportAppConfig.SmtpSendConfiguration.OneLevelWildcardMatchForCertSelection, this.Connector.Name, this.GetHashCode());
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0009F47B File Offset: 0x0009D67B
		public void StartTls(SecureState secureState)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Initiating TLS on the outboundConnection");
			this.secureState = (secureState | SecureState.NegotiationRequested);
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0009F4A4 File Offset: 0x0009D6A4
		public virtual void SetNextStateToQuit()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SetNextStateToQuit);
			if (this.presentInSmtpOutSessionCache)
			{
				this.NextState = SmtpOutSession.SessionState.Quit;
				this.MoveToNextState();
				return;
			}
			if (this.nextHopIsProxyingBlindly && this.outboundProxyOriginalSessionState.EhloOptions.XRsetProxyTo)
			{
				this.NextState = SmtpOutSession.SessionState.XRsetProxyTo;
				return;
			}
			this.NextState = SmtpOutSession.SessionState.Quit;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0009F4FA File Offset: 0x0009D6FA
		public void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.AckConnection(ackStatus, smtpResponse, SessionSetupFailureReason.ProtocolError);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0009F505 File Offset: 0x0009D705
		public void AckMessage(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.AckMessage(ackStatus, smtpResponse, 0L);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0009F511 File Offset: 0x0009D711
		public void AckMessage(AckStatus ackStatus, SmtpResponse smtpResponse, long messageSize)
		{
			this.AckMessage(ackStatus, smtpResponse, messageSize, SessionSetupFailureReason.ProtocolError, true);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x0009F520 File Offset: 0x0009D720
		public virtual void AckMessage(AckStatus ackStatus, SmtpResponse smtpResponse, long messageSize, SessionSetupFailureReason failureReason, bool updateSmtpSendFailureCounters)
		{
			this.AckMessage(ackStatus, smtpResponse, messageSize, failureReason, null, updateSmtpSendFailureCounters, null);
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0009F544 File Offset: 0x0009D744
		public void AckMessage(AckStatus ackStatus, SmtpResponse smtpResponse, long messageSize, SessionSetupFailureReason failureReason, TimeSpan? retryInterval, bool updateSmtpSendFailureCounters, string messageTrackingSourceContext = null)
		{
			if (this.RoutedMailItem == null)
			{
				throw new InvalidOperationException("Message has already been acked!");
			}
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.AckMessage);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus>((long)this.GetHashCode(), "AckMessage called with status: {0}", ackStatus);
			if (ackStatus == AckStatus.Success)
			{
				if (this.numberOfRecipientsSucceeded > 0)
				{
					this.smtpOutConnection.MessagesSent += 1UL;
					this.messagesSentOverSession++;
				}
				this.currentRecipient = null;
				if (this.NextHopDeliveryType != DeliveryType.Heartbeat)
				{
					if (this.messageStream != null || messageSize == 0L)
					{
						throw new InvalidOperationException("Cleanup should be completed by this point.");
					}
					if (this.SmtpSendPerformanceCounters != null)
					{
						this.SmtpSendPerformanceCounters.MessageBytesSentTotal.IncrementBy(messageSize);
						this.SmtpSendPerformanceCounters.MessagesSentTotal.Increment();
						this.SmtpSendPerformanceCounters.TotalRecipientsSent.IncrementBy((long)this.NumberOfRecipientsSucceeded);
					}
					if (this.IsNextHopDomainSecured)
					{
						Utils.SecureMailPerfCounters.DomainSecureMessagesSentTotal.Increment();
					}
				}
			}
			else
			{
				if (this.messageStream != null)
				{
					this.messageStream.Close();
					this.messageStream = null;
				}
				this.currentRecipient = null;
				if (messageSize != 0L)
				{
					throw new InvalidOperationException("There is still part of a message left to send.");
				}
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendAckMessage, null, new object[]
				{
					this.Connector.Name,
					this.RoutedMailItem.InternetMessageId,
					smtpResponse
				});
				if (updateSmtpSendFailureCounters)
				{
					this.smtpOutConnection.UpdateSmtpSendFailurePerfCounter(failureReason);
				}
			}
			if (this.SmtpSendPerformanceCounters != null && DeliveryType.Heartbeat != this.NextHopDeliveryType)
			{
				this.SmtpSendPerformanceCounters.TotalBytesSent.IncrementBy(this.connection.BytesSent - this.bytesSentAtLastCount);
				this.bytesSentAtLastCount = this.connection.BytesSent;
			}
			this.nextHopConnection.AckMailItem(ackStatus, smtpResponse, this.ackDetails, retryInterval, MessageTrackingSource.SMTP, messageTrackingSourceContext, LatencyComponent.SmtpSend, this.AdvertisedEhloOptions.AdvertisedFQDN, this.ShadowCurrentMailItem, this.SmtpHost, (this.Permissions & Permission.SendOrganizationHeaders) == Permission.None);
			this.routedMailItem = null;
			this.shadowCurrentMailItem = false;
			this.recipientCorrelator = null;
			if (this.NextHopDeliveryType != DeliveryType.Heartbeat)
			{
				this.messageCount++;
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0009F778 File Offset: 0x0009D978
		public void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus>((long)this.GetHashCode(), "AckRecipient called with status: {0}", ackStatus);
			switch (ackStatus)
			{
			case AckStatus.Success:
				this.numberOfRecipientsSucceeded++;
				break;
			case AckStatus.Retry:
				this.numberOfRecipientsAckedForRetry++;
				break;
			}
			this.nextHopConnection.AckRecipient(ackStatus, smtpResponse);
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0009F7DB File Offset: 0x0009D9DB
		public void FailoverConnection(SmtpResponse smtpResponse, bool ignorePipeLine)
		{
			this.FailoverConnection(smtpResponse, ignorePipeLine, false, SessionSetupFailureReason.ProtocolError);
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0009F7E7 File Offset: 0x0009D9E7
		public void RemoveConnection()
		{
			if (this.smtpOutConnection != null)
			{
				this.smtpOutConnection.RemoveConnection();
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0009F7FC File Offset: 0x0009D9FC
		public void PrepareNextStateForEstablishedSession()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.PrepareNextStateForEstablishedSession);
			if (this.nextHopConnection.ReadOnlyMailItem != null)
			{
				this.nextHopConnection.ReadOnlyMailItem.TrackSuccessfulConnectLatency(LatencyComponent.SmtpSendConnect);
			}
			if (!this.smtpOutConnection.NextHopIsOutboundProxy)
			{
				this.PrepareToSendXshadowOrMessage();
				return;
			}
			this.helloDomainOfOutboundProxyFrontEnd = this.AdvertisedEhloOptions.AdvertisedFQDN;
			if (this.AdvertisedEhloOptions.XProxyTo)
			{
				this.NextState = SmtpOutSession.SessionState.XProxyTo;
				return;
			}
			this.FailoverConnection(SmtpResponse.XProxyToRequired, SessionSetupFailureReason.ProtocolError);
			this.nextState = SmtpOutSession.SessionState.Quit;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0009F880 File Offset: 0x0009DA80
		public void PrepareToSendXshadowOrMessage()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.PrepareSendXshadowOrMessage);
			if (this.SendShadow)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Issue XSHADOW before sending messages.");
				this.NextState = SmtpOutSession.SessionState.XShadow;
				return;
			}
			if (this.SendXShadowRequest)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Issue XSHADOWREQUEST before sending shadow messages.");
				this.NextState = SmtpOutSession.SessionState.XShadowRequest;
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Starting sending messages.");
			this.PrepareForNextMessage(false);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0009F900 File Offset: 0x0009DB00
		public void EnqueueNextPipeLinedCommands()
		{
			if (this.doPrepareForNextMessage)
			{
				this.doPrepareForNextMessage = false;
				this.betweenMessagesRset = this.issueBetweenMsgRset;
				if (this.betweenMessagesRset)
				{
					this.NextState = SmtpOutSession.SessionState.Rset;
				}
				else
				{
					this.NextState = SmtpOutSession.SessionState.MessageStart;
				}
				if (this.AdvertisedEhloOptions.Pipelining)
				{
					if (this.issueBetweenMsgRset)
					{
						this.EnqueueCommandList(SmtpOutSession.SessionState.Rset);
					}
					this.EnqueueCommandList(SmtpOutSession.SessionState.MessageStart);
					this.EnqueueCommandList(SmtpOutSession.SessionState.PerRecipient);
					this.numRcptCommandsInPipelineQueue = this.nextHopConnection.RecipientCount;
					ExTraceGlobals.SmtpSendTracer.TraceDebug<int>((long)this.GetHashCode(), "Total Number of RCPT TO commands enqueued : {0}", this.numRcptCommandsInPipelineQueue);
				}
			}
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0009F99C File Offset: 0x0009DB9C
		public void PrepareForNextMessage(bool issueBetweenMsgRset)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.PrepareForNextMessage);
			if (this.pipelinedResponseQueue.Count > 0 || this.doPrepareForNextMessage)
			{
				this.pipeLineNextMessagePending = true;
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "PrepareForNextMessage will be honored when we finish the pipeline");
				return;
			}
			for (;;)
			{
				this.numberOfRecipientsAttempted = 0;
				this.numberOfRecipientsSucceeded = 0;
				this.numberOfRecipientsAckedForRetry = 0;
				this.numberOfRecipientsAcked = 0;
				this.currentRecipient = null;
				this.nextRecipient = null;
				this.shadowCurrentMailItem = false;
				this.ResetPipelineState();
				bool flag;
				if (this.ShouldAttemptSendingMessageOnSameConnection(out flag))
				{
					this.routedMailItem = this.nextHopConnection.GetNextMailItem();
					if (this.routedMailItem == null)
					{
						ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "No more messages in queue");
					}
				}
				else
				{
					this.routedMailItem = null;
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "The message count have reached SMTP Max Messages per connection.");
					this.messageSendAttemptCount = 0;
				}
				if (!flag)
				{
					this.dontCacheThisConnection = true;
				}
				if (this.routedMailItem == null)
				{
					break;
				}
				if (!this.Shadowed)
				{
					this.shadowCurrentMailItem = false;
				}
				else
				{
					this.shadowCurrentMailItem = (this.shadowRedundancyManager != null && this.shadowRedundancyManager.ShouldShadowMailItem(this.RoutedMailItem));
				}
				this.SetupPoisonContext();
				if (this.PreProcessMessage())
				{
					goto Block_9;
				}
			}
			this.pipelinedCommandQueue.Clear();
			if (this.SendXQDiscard)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "XQDISCARD will be issued");
				this.NextState = SmtpOutSession.SessionState.XQDiscard;
				return;
			}
			this.SetNextStateToQuit();
			return;
			Block_9:
			this.messageSendAttemptCount++;
			this.recipientCorrelator = new RecipientCorrelator();
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Got a message");
			if (!this.doPrepareForNextMessage)
			{
				this.issueBetweenMsgRset = issueBetweenMsgRset;
				this.doPrepareForNextMessage = true;
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0009FB4F File Offset: 0x0009DD4F
		public void ResetPipelineState()
		{
			this.pipelinedCommandQueue.Clear();
			this.numRcptCommandsInPipelineQueue = 0;
			this.pipeLineNextMessagePending = false;
			this.pipeLineFailOverPending = false;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0009FB71 File Offset: 0x0009DD71
		public virtual void SetNextStateForCachedSessionAndLogInfo(int cacheSize)
		{
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, string.Format("successfully added connection to cache. Current cache size is {0}", cacheSize));
			this.SetNextStateForCachedSession();
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0009FB98 File Offset: 0x0009DD98
		public virtual void SetNextStateForCachedSession()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SetNextStateForCachedSession);
			this.pipelinedCommandQueue.Clear();
			this.presentInSmtpOutSessionCache = true;
			this.responseBuffer = null;
			this.sendBuffer = new BufferBuilder();
			this.NextState = SmtpOutSession.SessionState.Inactive;
			this.AckConnection(AckStatus.Success, SmtpResponse.SuccessNoNewConnectionResponse, SessionSetupFailureReason.None);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0009FBE5 File Offset: 0x0009DDE5
		public void DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0009FBF3 File Offset: 0x0009DDF3
		public bool HasMoreBlobsPending()
		{
			return this.BlobsToSend != null && this.BlobsToSend.Count > 0;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0009FC10 File Offset: 0x0009DE10
		public void SetupBlobsToSend()
		{
			if (this.AuthMechanism != SmtpSendConnectorConfig.AuthMechanisms.ExchangeServer || !this.MessageContextBlobTransferSupported)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Sending MessageContext blob is not supported");
				return;
			}
			List<SmtpMessageContextBlob> list = SmtpMessageContextBlob.GetBlobsToSend(this.AdvertisedEhloOptions, this);
			if (this.blobsToSend != null)
			{
				this.blobsToSend.Clear();
			}
			if (list != null)
			{
				if (this.blobsToSend == null)
				{
					this.blobsToSend = new Queue<SmtpMessageContextBlob>(list.Count);
				}
				foreach (SmtpMessageContextBlob item in list)
				{
					this.blobsToSend.Enqueue(item);
				}
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0009FCC8 File Offset: 0x0009DEC8
		internal static string AuthLevelToString(RequiredTlsAuthLevel? authLevel)
		{
			if (authLevel != null)
			{
				return authLevel.Value.ToString();
			}
			return "Opportunistic";
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0009FCEA File Offset: 0x0009DEEA
		public void UpdateServerLatency(TimeSpan latency)
		{
			if (this.SendFewerMessagesToSlowerServerEnabled)
			{
				this.smtpOutConnection.UpdateServerLatency(latency);
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0009FD00 File Offset: 0x0009DF00
		protected virtual void IncrementConnectionCounters()
		{
			if (this.SmtpSendPerformanceCounters != null)
			{
				this.SmtpSendPerformanceCounters.ConnectionsTotal.Increment();
				this.SmtpSendPerformanceCounters.ConnectionsCurrent.Increment();
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0009FD2C File Offset: 0x0009DF2C
		protected virtual void DecrementConnectionCounters()
		{
			if (this.SmtpSendPerformanceCounters != null)
			{
				this.SmtpSendPerformanceCounters.ConnectionsCurrent.Decrement();
			}
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0009FD48 File Offset: 0x0009DF48
		protected virtual bool InvokeCommandHandler(SmtpCommand command)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InvokeCommandHandler);
			command.OutboundFormatCommand();
			if (command.ProtocolCommandString != null)
			{
				command.ProtocolCommand = ByteString.StringToBytesAndAppendCRLF(command.ProtocolCommandString, true);
				if (string.IsNullOrEmpty(command.RedactedProtocolCommandString))
				{
					this.logSession.LogSend(command.ProtocolCommand);
				}
				else
				{
					this.logSession.LogSend(ByteString.StringToBytes(command.RedactedProtocolCommandString, true));
				}
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Enqueuing Command: {0} on the connection", command.ProtocolCommandString);
				this.EnqueueResponseHandler(command);
				BdatSmtpCommand bdatSmtpCommand = command as BdatSmtpCommand;
				if (bdatSmtpCommand != null)
				{
					if (this.sendBuffer.Length != 0)
					{
						throw new InvalidOperationException("BDAT cannot be pipelined");
					}
					if (bdatSmtpCommand.SmtpMessageContextBlob != null)
					{
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Sending blob {0}", new object[]
						{
							bdatSmtpCommand.SmtpMessageContextBlob.Name
						});
					}
					this.SendBdatStream(command.ProtocolCommand, bdatSmtpCommand.BodyStream);
					return true;
				}
				else
				{
					this.sendBuffer.Append(command.ProtocolCommand);
				}
			}
			else if (command.ProtocolCommand != null)
			{
				this.EnqueueResponseHandler(command);
				this.logSession.LogSend(SmtpOutSession.BinaryData);
				this.sendBuffer.Append(command.ProtocolCommand);
			}
			else
			{
				DataSmtpCommand dataSmtpCommand = command as DataSmtpCommand;
				if (dataSmtpCommand != null && dataSmtpCommand.BodyStream != null)
				{
					if (this.sendBuffer.Length != 0)
					{
						throw new InvalidOperationException("DATA cannot send stream unless send buffer is empty");
					}
					this.EnqueueResponseHandler(command);
					this.SendDataStream(dataSmtpCommand.BodyStream);
					return true;
				}
				else
				{
					command.Dispose();
				}
			}
			return false;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0009FECC File Offset: 0x0009E0CC
		protected virtual SmtpCommand CreateSmtpCommand(string cmd)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Creating Smtp Command: {0}", cmd);
			SmtpCommand smtpCommand = null;
			if (cmd != null)
			{
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x6002793-1 == null)
				{
					<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x6002793-1 = new Dictionary<string, int>(20)
					{
						{
							"ConnectResponse",
							0
						},
						{
							"EHLO",
							1
						},
						{
							"HELO",
							2
						},
						{
							"AUTH",
							3
						},
						{
							"X-EXPS",
							4
						},
						{
							"STARTTLS",
							5
						},
						{
							"X-ANONYMOUSTLS",
							6
						},
						{
							"MAIL",
							7
						},
						{
							"RCPT",
							8
						},
						{
							"XEXCH50",
							9
						},
						{
							"DATA",
							10
						},
						{
							"BDAT",
							11
						},
						{
							"XBDATBLOB",
							12
						},
						{
							"RSET",
							13
						},
						{
							"XSHADOW",
							14
						},
						{
							"XQDISCARD",
							15
						},
						{
							"XPROXYTO",
							16
						},
						{
							"XRSETPROXYTO",
							17
						},
						{
							"XSESSIONPARAMS",
							18
						},
						{
							"QUIT",
							19
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x6002793-1.TryGetValue(cmd, out num))
				{
					switch (num)
					{
					case 0:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdConnectResponse);
						break;
					case 1:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdEhlo);
						smtpCommand = new EHLOSmtpCommand(this, this.transportConfiguration);
						break;
					case 2:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdHelo);
						smtpCommand = new HELOSmtpCommand(this);
						break;
					case 3:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdAuth);
						smtpCommand = new AuthSmtpCommand(this, false, this.transportConfiguration);
						break;
					case 4:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdAuth);
						smtpCommand = new AuthSmtpCommand(this, true, this.transportConfiguration);
						break;
					case 5:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, false);
						break;
					case 6:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, true);
						break;
					case 7:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdMail);
						smtpCommand = new MailSmtpCommand(this, this.transportAppConfig);
						break;
					case 8:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdRcpt);
						smtpCommand = new RcptSmtpCommand(this, this.recipientCorrelator, this.transportAppConfig);
						break;
					case 9:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXexch50);
						smtpCommand = new Xexch50SmtpCommand(this, this.recipientCorrelator, this.mailRouter, this.transportAppConfig, this.transportConfiguration);
						break;
					case 10:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdData);
						smtpCommand = new DataSmtpCommand(this, this.transportAppConfig);
						break;
					case 11:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdBdat);
						smtpCommand = new BdatSmtpCommand(this, this.transportAppConfig, null);
						break;
					case 12:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdBdat);
						smtpCommand = new BdatSmtpCommand(this, this.transportAppConfig, this.BlobsToSend.Dequeue());
						break;
					case 13:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdRset);
						smtpCommand = new RsetSmtpCommand(this);
						break;
					case 14:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXShadow);
						smtpCommand = new XShadowSmtpCommand(this, this.shadowRedundancyManager);
						break;
					case 15:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXQDiscard);
						smtpCommand = new XQDiscardSmtpCommand(this, this.shadowRedundancyManager);
						break;
					case 16:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXProxyTo);
						smtpCommand = new XProxyToSmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
						break;
					case 17:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXRsetProxyTo);
						smtpCommand = new XRsetProxyToSmtpCommand(this);
						break;
					case 18:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdXSessionParams);
						smtpCommand = new XSessionParamsSmtpCommand(this);
						break;
					case 19:
						this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.CreateCmdQuit);
						smtpCommand = new QuitSmtpCommand(this);
						break;
					default:
						goto IL_391;
					}
					if (smtpCommand != null)
					{
						smtpCommand.ParsingStatus = ParsingStatus.Complete;
						smtpCommand.OutboundCreateCommand();
					}
					return smtpCommand;
				}
			}
			IL_391:
			throw new ArgumentException("Unknown command encountered in SmtpOut: " + cmd, "cmd");
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x000A0294 File Offset: 0x0009E494
		protected virtual bool PreProcessMessage()
		{
			if (this.NextHopDeliveryType == DeliveryType.Heartbeat)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Message is a heartbeat message and is not sent to the remote server");
				this.AckMessage(AckStatus.Success, SmtpResponse.Empty);
				return false;
			}
			this.exch50DataPresent = (this.RoutedMailItem.LegacyXexch50Blob != null);
			if (!this.PreCheckMessageSize())
			{
				return false;
			}
			bool supportLongAddresses = this.SupportLongAddresses;
			bool supportOrar = this.SupportOrar;
			bool supportRDst = this.SupportRDst;
			bool supportSmtpUtf = this.SupportSmtpUtf8;
			if (!this.CheckLongSenderSupport(supportLongAddresses))
			{
				return false;
			}
			if (!this.CheckSmtpUtf8SenderSupport(supportSmtpUtf))
			{
				return false;
			}
			bool flag = false;
			foreach (MailRecipient recipient in this.nextHopConnection.ReadyRecipients)
			{
				if (this.PreProcessRecipient(recipient, supportLongAddresses, supportOrar, supportRDst, supportSmtpUtf))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from '{0}' was NDR'ed because all recipients failed in PreProcess()", this.RoutedMailItem.From.ToString());
				this.AckMessage(AckStatus.Fail, SmtpResponse.NoRecipientSucceeded);
				return false;
			}
			return true;
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x000A03C0 File Offset: 0x0009E5C0
		protected virtual void ConnectResponseEvent(SmtpResponse smtpResponse)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<IPEndPoint>((long)this.GetHashCode(), "Connected to remote server: {0}", this.sessionProps.RemoteEndPoint);
			this.Banner = smtpResponse;
			if (!this.disconnected)
			{
				if (smtpResponse.StatusCode[0] != '2')
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Server is not accepting mail, connect response: {0}", smtpResponse);
					this.FailoverConnection(smtpResponse, SessionSetupFailureReason.ProtocolError);
					this.NextState = SmtpOutSession.SessionState.Quit;
					this.response.Clear();
					this.SendNextCommands();
					return;
				}
				if (this.Connector.ForceHELO)
				{
					this.NextState = SmtpOutSession.SessionState.Helo;
				}
				else
				{
					this.NextState = SmtpOutSession.SessionState.Ehlo;
				}
				this.response.Clear();
				this.SendNextCommands();
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x000A0475 File Offset: 0x0009E675
		protected void EnqueueResponseHandler(SmtpCommand command)
		{
			if (command != null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Enqueueing ResponseHandler for {0}", command.ProtocolCommandKeyword);
			}
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.EnqueueResponseHandler);
			this.pipelinedResponseQueue.Enqueue(command);
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000A04AC File Offset: 0x0009E6AC
		protected virtual void FinalizeNextStateAndSendCommands()
		{
			if (this.NextHopConnection != null && this.NextHopConnection.RetryQueueRequested)
			{
				this.AckConnection(AckStatus.Retry, this.NextHopConnection.RetryQueueSmtpResponse, SessionSetupFailureReason.None);
				return;
			}
			bool flag = true;
			if (this.NextState == SmtpOutSession.SessionState.Quit && !this.pipeLineFailOverPending)
			{
				NextHopSolutionKey nextHopKey = NextHopSolutionKey.Empty;
				IPEndPoint remoteEndPoint = this.RemoteEndPoint;
				if (this.smtpOutConnection.NextHopIsOutboundProxy)
				{
					nextHopKey = SmtpOutSessionCache.OutboundFrontendCacheKey;
					remoteEndPoint = SmtpOutSessionCache.OutboundFrontendIPEndpointCacheKey;
					this.outboundProxySendConnector = null;
					if (this.nextHopIsProxyingBlindly)
					{
						if (!this.xRsetProxyToAccepted)
						{
							this.dontCacheThisConnection = true;
						}
						else
						{
							this.xRsetProxyToAccepted = false;
							this.nextHopIsProxyingBlindly = false;
							if (this.outboundProxyOriginalSessionState == null)
							{
								throw new InvalidOperationException("original session state wasn't saved before blind proxy");
							}
							this.sessionProps.AdvertisedEhloOptions = this.outboundProxyOriginalSessionState.EhloOptions;
							this.sendConnector = this.outboundProxyOriginalSessionState.SendConnector;
							this.RemoteIdentity = this.outboundProxyOriginalSessionState.RemoteIdentity;
							this.RemoteIdentityName = this.outboundProxyOriginalSessionState.RemoteIdentityName;
							this.ackDetails = this.outboundProxyOriginalSessionState.AckDetails;
							this.sessionPermissions = this.outboundProxyOriginalSessionState.SessionPermissions;
						}
					}
					else if (!this.SessionProps.AdvertisedEhloOptions.XProxyTo)
					{
						this.dontCacheThisConnection = true;
					}
				}
				else if (this.NextHopConnection != null)
				{
					nextHopKey = this.NextHopConnection.Key;
				}
				else
				{
					this.dontCacheThisConnection = true;
				}
				NextHopConnection nextHopConnection = this.NextHopConnection;
				if (!this.dontCacheThisConnection && !this.failoverInProgress && SmtpOutConnectionHandler.SessionCache.TryAdd(nextHopKey, remoteEndPoint, this))
				{
					flag = false;
					if (nextHopConnection != null)
					{
						nextHopConnection.CreateConnectionIfNecessary();
					}
				}
				else if (this.NextHopConnection != null)
				{
					this.AckConnection(AckStatus.Success, SmtpResponse.SuccessfulConnection, SessionSetupFailureReason.None);
				}
			}
			if (flag)
			{
				this.EnqueueNextPipeLinedCommands();
				this.SendNextCommands();
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x000A0668 File Offset: 0x0009E868
		protected void StartReadLine()
		{
			this.connection.BeginReadLine(SmtpOutSession.readLineComplete, this);
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x000A067C File Offset: 0x0009E87C
		protected virtual void HandleError(object error)
		{
			this.HandleError(error, false, true);
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x000A0688 File Offset: 0x0009E888
		protected void HandleError(object error, bool retryWithoutStartTls, bool failoverConnection)
		{
			bool flag = false;
			this.SetupPoisonContext();
			string error2 = null;
			if (this.messageStream != null)
			{
				this.messageStream.Close();
				this.messageStream = null;
			}
			bool flag2;
			SessionSetupFailureReason failureReason;
			if (error is SocketError)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpOutSession(id={0}).HandleError (SocketError={1})", this.connectionId, error);
				error2 = ((SocketError)error).ToString();
				flag2 = false;
				failureReason = SessionSetupFailureReason.SocketError;
			}
			else if (error is SecurityStatus)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpOutSession(id={0}).HandleError (SecurityStatus={1})", this.connectionId, error);
				flag2 = true;
				failureReason = SessionSetupFailureReason.ProtocolError;
			}
			else if (error is BareLinefeedException)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpOutSession(id={0}).HandleError (Bare linefeed in content going out over SMTP DATA)", this.connectionId, error);
				this.routedMailItem.SuppressBodyInDsn = true;
				if (this.SmtpSendPerformanceCounters != null)
				{
					this.SmtpSendPerformanceCounters.MessagesSuppressedDueToBareLinefeeds.Increment();
				}
				this.AckMessage(AckStatus.Fail, AckReason.BareLinefeedsAreIllegal);
				flag2 = true;
				failureReason = SessionSetupFailureReason.ProtocolError;
			}
			else
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<long, object>((long)this.GetHashCode(), "SmtpOutSession(id={0}).HandleError (error={1})", this.connectionId, error);
				flag2 = true;
				failureReason = SessionSetupFailureReason.ProtocolError;
			}
			if (error is SocketError && (SocketError)error == SocketError.ConnectionReset && this.routedMailItem != null && failoverConnection)
			{
				if ((this.IsHubDeliveringToNonMailbox(this.transportConfiguration.ProcessTransportRole) || this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.Edge) && this.transportAppConfig.SmtpSendConfiguration.SuspiciousDisconnectRetryInterval > TimeSpan.Zero)
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug<long>((long)this.GetHashCode(), "SmtpOutSession(id={0}).HandleError has encountered a suspicious connection reset from a remote server.", this.connectionId);
					this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "HandleError has encountered a suspicious connection reset from a remote, non-mailbox transport server (will retry in {0}).", new object[]
					{
						this.transportAppConfig.SmtpSendConfiguration.SuspiciousDisconnectRetryInterval
					});
					TimeSpan suspiciousDisconnectRetryInterval = this.transportAppConfig.SmtpSendConfiguration.SuspiciousDisconnectRetryInterval;
					string messageTrackingSourceContext = string.Format("Retrying due to suspicious connection reset with retry time of {0}", suspiciousDisconnectRetryInterval);
					this.AckMessage(AckStatus.Retry, AckReason.SuspiciousRemoteServerError, 0L, failureReason, new TimeSpan?(suspiciousDisconnectRetryInterval), false, messageTrackingSourceContext);
					this.AckConnection(AckStatus.Retry, AckReason.SuspiciousRemoteServerError, failureReason);
				}
				else if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission || this.IsHubDeliveringToMailbox(this.transportConfiguration.ProcessTransportRole))
				{
					this.routedMailItem.IncrementPoisonForRemoteCount();
					if (this.routedMailItem.PoisonForRemoteCount > this.transportAppConfig.SmtpSendConfiguration.PoisonForRemoteThreshold)
					{
						ExTraceGlobals.SmtpSendTracer.TraceDebug<long, int>((long)this.GetHashCode(), "SmtpOutSession(id={0}).PoisonForRemote has exceeded the configurable threshold of {1}, acking the message as fail", this.connectionId, this.transportAppConfig.SmtpSendConfiguration.PoisonForRemoteThreshold);
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "PoisonForRemote has exceeded the configurable threshold of {0}, acking the message as fail", new object[]
						{
							this.transportAppConfig.SmtpSendConfiguration.PoisonForRemoteThreshold
						});
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendPoisonForRemoteThresholdExceeded, null, new object[]
						{
							this.routedMailItem.InternetMessageId,
							this.sessionProps.RemoteEndPoint,
							this.transportAppConfig.SmtpSendConfiguration.PoisonForRemoteThreshold
						});
						if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.Hub)
						{
							((RoutedMailItem)this.routedMailItem).Poison();
						}
						this.AckMessage(AckStatus.Fail, AckReason.MessageIsPoisonForRemoteServer, 0L, failureReason, false);
						this.AckConnection(AckStatus.Retry, AckReason.MessageIsPoisonForRemoteServer, failureReason);
						flag = true;
					}
				}
			}
			if (!failoverConnection)
			{
				if (this.RoutedMailItem != null)
				{
					this.AckMessage(AckStatus.Fail, AckReason.SendingError, 0L, failureReason, false);
				}
				this.AckConnection(AckStatus.Fail, AckReason.SendingError, failureReason);
			}
			if (this.nextHopConnection != null)
			{
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendRemoteDisconnected, null, new object[]
				{
					this.Connector.Name,
					this.sessionProps.RemoteEndPoint
				});
			}
			this.Disconnect(flag2 ? DisconnectReason.Local : DisconnectReason.Remote, failoverConnection && !flag && this.currentState != SmtpOutSession.SessionState.Quit, retryWithoutStartTls, error2, failureReason);
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000A0A84 File Offset: 0x0009EC84
		protected bool CheckSmtpUtf8SenderSupport(bool supportSmtpUtf8)
		{
			if (this.RoutedMailItem.From.IsUTF8 && !supportSmtpUtf8)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from '{0}' was NDR'ed because the session does not support UTF-8 addresses", this.RoutedMailItem.From.ToString());
				this.AckMessage(AckStatus.Fail, AckReason.SmtpSendUtf8SenderAddress);
				return false;
			}
			return true;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x000A0AE8 File Offset: 0x0009ECE8
		private bool CheckSmtpUtf8RecipientSupport(MailRecipient recipient, bool supportSmtpUtf8)
		{
			if (recipient.Email.IsUTF8 && !supportSmtpUtf8)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because the session does not support UTF-8 addresses", recipient.Email.ToString());
				recipient.Ack(AckStatus.Fail, AckReason.SmtpSendUtf8RecipientAddress);
				return false;
			}
			return true;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x000A0B44 File Offset: 0x0009ED44
		protected bool CheckLongSenderSupport(bool supportLongAddresses)
		{
			if (Util.IsLongAddress(this.RoutedMailItem.From))
			{
				if (!Util.IsValidInnerAddress(this.RoutedMailItem.From))
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from '{0}' was NDR'ed because the long address is invalid", this.RoutedMailItem.From.ToString());
					this.AckMessage(AckStatus.Fail, AckReason.SmtpSendInvalidLongSenderAddress);
					return false;
				}
				if (!supportLongAddresses)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from '{0}' was NDR'ed because the session does not support long addresses", this.RoutedMailItem.From.ToString());
					this.AckMessage(AckStatus.Fail, AckReason.SmtpSendLongSenderAddress);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x000A0BF8 File Offset: 0x0009EDF8
		protected bool PreProcessRecipient(MailRecipient recipient, bool supportLongAddresses, bool supportOrar, bool supportRDst, bool supportSmtpUtf8)
		{
			if (recipient.ExtendedProperties.Contains("Microsoft.Exchange.Legacy.PassThru"))
			{
				this.exch50DataPresent = true;
			}
			return this.CheckLongRecipientSupport(recipient, supportLongAddresses) && this.CheckOrarSupport(recipient, supportOrar, supportLongAddresses) && this.CheckRDstSupport(recipient, supportRDst) && this.CheckSmtpUtf8RecipientSupport(recipient, supportSmtpUtf8);
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000A0C52 File Offset: 0x0009EE52
		protected void SendNextCommands()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SendNextCommands);
			if (this.SendPipelinedCommands())
			{
				return;
			}
			if (this.pipelinedResponseQueue.Count != 0)
			{
				this.StartReadLine();
				return;
			}
			this.MoveToNextState();
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000A0C80 File Offset: 0x0009EE80
		private static void WriteCompleteReadLine(IAsyncResult asyncResult)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)asyncResult.AsyncState;
			smtpOutSession.SetupPoisonContext();
			object obj;
			smtpOutSession.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutSession.HandleError(obj);
				return;
			}
			smtpOutSession.sendBuffer.Reset();
			smtpOutSession.StartReadLine();
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000A0CCC File Offset: 0x0009EECC
		private static void WriteBdatCompleteSendStream(IAsyncResult asyncResult)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)asyncResult.AsyncState;
			smtpOutSession.SetupPoisonContext();
			object obj;
			smtpOutSession.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutSession.HandleError(obj);
				return;
			}
			smtpOutSession.connection.BeginWrite(smtpOutSession.messageStream, SmtpOutSession.writeStreamCompleteReadLine, smtpOutSession);
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000A0D1C File Offset: 0x0009EF1C
		private static void WriteStreamCompleteReadLine(IAsyncResult asyncResult)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)asyncResult.AsyncState;
			smtpOutSession.SetupPoisonContext();
			smtpOutSession.messageStream.Close();
			smtpOutSession.messageStream = null;
			object obj;
			smtpOutSession.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutSession.HandleError(obj);
				return;
			}
			smtpOutSession.sendBuffer.Reset();
			if (smtpOutSession.ShouldTrackMailboxDeliveryLatency())
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpSendMailboxDelivery, smtpOutSession.routedMailItem.LatencyTracker);
			}
			smtpOutSession.StartReadLine();
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x000A0D94 File Offset: 0x0009EF94
		private static void CanCommandBePipelined(string command, out bool isLastCmdInPipeline, out bool isCmdPipelinable)
		{
			isLastCmdInPipeline = false;
			if ("MAIL".Equals(command, StringComparison.OrdinalIgnoreCase) || "RCPT".Equals(command, StringComparison.OrdinalIgnoreCase) || "RSET".Equals(command, StringComparison.OrdinalIgnoreCase))
			{
				isCmdPipelinable = true;
				return;
			}
			if ("DATA".Equals(command, StringComparison.OrdinalIgnoreCase))
			{
				isLastCmdInPipeline = true;
				isCmdPipelinable = true;
				return;
			}
			isCmdPipelinable = false;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x000A0DEC File Offset: 0x0009EFEC
		private static void ReadLineComplete(IAsyncResult asyncResult)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)asyncResult.AsyncState;
			smtpOutSession.SetupPoisonContext();
			bool overflow = false;
			byte[] buffer;
			int offset;
			int size;
			object obj;
			smtpOutSession.connection.EndReadLine(asyncResult, out buffer, out offset, out size, out obj);
			if (obj != null)
			{
				if (!(obj is SocketError) || (SocketError)obj != SocketError.MessageSize)
				{
					smtpOutSession.HandleError(obj);
					return;
				}
				overflow = true;
			}
			if (smtpOutSession.disconnected)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)smtpOutSession.GetHashCode(), "Command Received from NetworkConnection, but we are already disconnected");
				return;
			}
			if (smtpOutSession.shutdownConnectionCalled)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)smtpOutSession.GetHashCode(), "Command Received from NetworkConnection, but we have already begun to shut down");
				return;
			}
			if (smtpOutSession.ShouldTrackMailboxDeliveryLatency())
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.SmtpSendMailboxDelivery, smtpOutSession.routedMailItem.LatencyTracker);
			}
			smtpOutSession.StartProcessingResponse(buffer, offset, size, overflow);
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x000A0EB0 File Offset: 0x0009F0B0
		private static void TlsNegotiationComplete(IAsyncResult asyncResult)
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)asyncResult.AsyncState;
			smtpOutSession.SetupPoisonContext();
			object obj;
			smtpOutSession.connection.EndNegotiateTlsAsClient(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutSession.smtpSendPerformanceCounters.TlsNegotiationsFailed.Increment();
				smtpOutSession.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TLS negotiation failed with error {0}", new object[]
				{
					obj
				});
				bool retryWithoutStartTls;
				if (smtpOutSession.IsNextHopDomainSecured)
				{
					retryWithoutStartTls = false;
					string nextHopDomain = smtpOutSession.NextHopConnection.Key.NextHopDomain;
					Utils.SecureMailPerfCounters.DomainSecureOutboundSessionFailuresTotal.Increment();
					SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_MessageToSecureDomainFailedDueToTlsNegotiationFailure, nextHopDomain, new object[]
					{
						nextHopDomain,
						smtpOutSession.Connector.Name,
						obj
					});
				}
				else
				{
					retryWithoutStartTls = (smtpOutSession.currentState == SmtpOutSession.SessionState.StartTLS && !smtpOutSession.TlsConfiguration.RequireTls && smtpOutSession.AuthMechanism != SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS);
				}
				smtpOutSession.HandleError(obj, retryWithoutStartTls, true);
				return;
			}
			ConnectionInfo tlsConnectionInfo = smtpOutSession.connection.TlsConnectionInfo;
			Util.LogTlsSuccessResult(smtpOutSession.logSession, tlsConnectionInfo, smtpOutSession.connection.RemoteCertificate);
			smtpOutSession.TlsNegotiationComplete();
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x000A0FD8 File Offset: 0x0009F1D8
		private void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse, SessionSetupFailureReason failureReason)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.AckConnection);
			if (this.nextHopConnection != null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "AckConnection called with status: {0}, Response: {1}", ackStatus, smtpResponse);
				if (ackStatus == AckStatus.Success)
				{
					if (this.messageStream != null || this.currentRecipient != null || this.RoutedMailItem != null)
					{
						throw new InvalidOperationException("Cleanup should be completed by this point");
					}
				}
				else
				{
					if (this.messageStream != null)
					{
						this.messageStream.Close();
						this.messageStream = null;
					}
					this.routedMailItem = null;
					this.currentRecipient = null;
					this.recipientCorrelator = null;
					SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendAckConnection, null, new object[]
					{
						this.Connector.Name,
						this.RemoteEndPoint,
						smtpResponse
					});
				}
				this.smtpOutConnection.BytesSent = (ulong)this.connection.BytesSent;
				this.smtpOutConnection.AckConnection(ackStatus, smtpResponse, this.ackDetails, null, failureReason);
				this.nextHopConnection = null;
				return;
			}
			if (!this.smtpOutConnection.NextHopIsOutboundProxy)
			{
				throw new InvalidOperationException("Connection has already been acked!");
			}
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x000A10E8 File Offset: 0x0009F2E8
		private void Disconnect(DisconnectReason disconnectReason, bool failOverConnection, bool retryWithoutStartTls, string error, SessionSetupFailureReason failureReason)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<long, DisconnectReason, bool>((long)this.GetHashCode(), "Disconnect Initiated for connection {0}.  DisconnectReason : {1}, FailoverConnection : {2}", this.connectionId, disconnectReason, failOverConnection);
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.Disconnect);
			while (this.pipelinedResponseQueue.Count > 0)
			{
				SmtpCommand smtpCommand = (SmtpCommand)this.pipelinedResponseQueue.Dequeue();
				if (smtpCommand != null)
				{
					smtpCommand.Dispose();
				}
			}
			if (!this.disconnected)
			{
				if (this.connection != null)
				{
					this.connection.Dispose();
					this.logSession.LogDisconnect(disconnectReason);
					if (this.SmtpSendPerformanceCounters != null)
					{
						this.DecrementConnectionCounters();
					}
				}
				this.disconnected = true;
				if (failOverConnection && this.nextHopConnection != null)
				{
					SmtpResponse smtpResponse = SmtpResponse.Empty;
					if (disconnectReason == DisconnectReason.Local)
					{
						smtpResponse = SmtpResponse.ConnectionTimedOut;
					}
					else
					{
						smtpResponse = SmtpResponse.ConnectionDroppedDueTo(error);
					}
					ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Initiating FailoverConnection {0}", smtpResponse);
					this.FailoverConnection(smtpResponse, true, retryWithoutStartTls, failureReason);
				}
				if (!this.failoverInProgress)
				{
					this.smtpOutConnection.RemoveConnection();
				}
			}
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000A11E0 File Offset: 0x0009F3E0
		private void FailoverConnection(SmtpResponse smtpResponse, bool ignorePipeLine, bool retryWithoutStartTls, SessionSetupFailureReason failoverReason)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.FailoverConnection);
			if (this.RoutedMailItem != null)
			{
				this.AckMessage(AckStatus.Pending, smtpResponse);
			}
			else if (this.SmtpSendPerformanceCounters != null && this.connection != null)
			{
				this.SmtpSendPerformanceCounters.TotalBytesSent.IncrementBy(this.connection.BytesSent - this.bytesSentAtLastCount);
				this.bytesSentAtLastCount = this.connection.BytesSent;
			}
			if (!ignorePipeLine && this.pipelinedResponseQueue.Count > 0)
			{
				if (!this.pipeLineFailOverPending)
				{
					this.pipeLineFailOverResponse = smtpResponse;
				}
				this.pipeLineFailOverPending = true;
				ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "FailoverConnection pending with response {0}", smtpResponse);
				return;
			}
			SmtpResponse arg = this.pipeLineFailOverPending ? this.pipeLineFailOverResponse : smtpResponse;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "FailoverConnection initiated with response {0}", arg);
			this.nextHopConnection = null;
			this.ResetPipelineState();
			this.failoverInProgress = true;
			this.smtpOutConnection.FailoverConnection(arg, retryWithoutStartTls, failoverReason, this.nextHopIsProxyingBlindly);
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000A12DC File Offset: 0x0009F4DC
		private bool ShouldTrackMailboxDeliveryLatency()
		{
			bool result = false;
			if (this.transportConfiguration.ProcessTransportRole == ProcessTransportRole.Hub && this.NextHopConnection != null && this.NextHopDeliveryType == DeliveryType.SmtpDeliveryToMailbox && this.pipelinedResponseQueue.Count > 0)
			{
				BdatSmtpCommand bdatSmtpCommand = this.pipelinedResponseQueue.Peek() as BdatSmtpCommand;
				if (bdatSmtpCommand != null && bdatSmtpCommand.IsLastChunkOutbound)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x000A1337 File Offset: 0x0009F537
		private bool IsHubServer(string fqdn)
		{
			if (!Components.IsBridgehead)
			{
				throw new InvalidOperationException("IsHubServer: should only be called on Hub server");
			}
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			return this.mailRouter.IsHubTransportServer(fqdn);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x000A136C File Offset: 0x0009F56C
		private void AppendToResponseBuffer(string responseline)
		{
			if ((this.response.Count >= 50 && this.CurrentState != SmtpOutSession.SessionState.XQDiscard) || (this.response.Count > this.transportAppConfig.ShadowRedundancy.MaxDiscardIdsPerSmtpCommand && this.CurrentState == SmtpOutSession.SessionState.XQDiscard))
			{
				throw new FormatException("Excessive data, unable to parse");
			}
			int length = responseline.Length;
			if (length > 0 && responseline[length - 1] == '\r')
			{
				responseline = responseline.Substring(0, length - 1);
			}
			this.response.Add(responseline);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x000A13F4 File Offset: 0x0009F5F4
		private void SetDefaultIdentity()
		{
			if ((this.Connector.SmartHostAuthMechanism & SmtpSendConnectorConfig.AuthMechanisms.ExternalAuthoritative) != SmtpSendConnectorConfig.AuthMechanisms.None)
			{
				this.RemoteIdentity = WellKnownSids.ExternallySecuredServers;
				this.RemoteIdentityName = "accepted_domain";
				this.SetSessionPermissions(this.RemoteIdentity);
				return;
			}
			if (this.NextHopDeliveryType == DeliveryType.SmtpRelayToTiRg)
			{
				this.RemoteIdentity = WellKnownSids.LegacyExchangeServers;
				this.RemoteIdentityName = "ti_rg_servers";
				this.SetSessionPermissions(this.RemoteIdentity);
				return;
			}
			if (this.CanDowngradeExchangeServerAuth)
			{
				this.RemoteIdentity = WellKnownSids.HubTransportServers;
				this.RemoteIdentityName = "e14andhigher_hub_servers";
				this.SetSessionPermissions(this.RemoteIdentity);
			}
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x000A148C File Offset: 0x0009F68C
		private void MoveToNextState()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpOutSession.SessionState, SmtpOutSession.SessionState>(0L, "MoveToNextState {0} -> {1}", this.currentState, this.nextState);
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.MoveToNextState);
			while (this.pipelinedResponseQueue.Count == 0 && !this.disconnected)
			{
				if ((byte)(this.secureState & SecureState.NegotiationRequested) == 128)
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting for TLS Handshake to complete");
					return;
				}
				if (this.doPrepareForNextMessage)
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting for doPrepareForNextMessage lookup to complete");
					return;
				}
				if (this.NextState == SmtpOutSession.SessionState.Inactive)
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting in the Smtp out Connection cache");
					return;
				}
				if (this.pipelinedCommandQueue.Count != 0 || (this.currentEnumeratorInPipeline != null && this.currentEnumeratorInPipeline.HasNext))
				{
					throw new InvalidOperationException("Should not move to the next state if there are any commands ready to be sent out");
				}
				this.CurrentState = this.NextState;
				this.EnqueueCommandList(this.CurrentState);
				if (this.SendPipelinedCommands())
				{
					return;
				}
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x000A1594 File Offset: 0x0009F794
		private bool SendPipelinedCommands()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SendPipelinedCommands);
			bool flag = true;
			bool flag2 = false;
			int num = this.pipelinedResponseQueue.Count;
			if ((this.CurrentState == SmtpOutSession.SessionState.StartTLS || this.CurrentState == SmtpOutSession.SessionState.AnonymousTLS) && (byte)(this.secureState & SecureState.NegotiationRequested) == 128)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting for TLS handshake completion to be called");
				if (this.pipelinedCommandQueue.Count != 0)
				{
					throw new InvalidOperationException("Pipelined command queue is not empty");
				}
				return false;
			}
			else
			{
				if ((this.lastCommandPipelined || this.pipelinedResponseQueue.Count <= 0) && !this.pipeLineNextMessagePending && !this.pipeLineFailOverPending && this.pipelinedResponseQueue.Count < 100)
				{
					try
					{
						while (flag && (this.pipelinedCommandQueue.Count > 0 || (this.currentEnumeratorInPipeline != null && this.currentEnumeratorInPipeline.HasNext)))
						{
							if (this.pipelinedCommandQueue.Count > 4)
							{
								throw new InvalidOperationException("The command queue can never grow bigger than 4, because the only states that can be enqueued at the same time are RSET, MAIL, RCPT, and DATA");
							}
							if (num >= 200)
							{
								ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Total commands pipelined greater than MaxPipelinedCommands");
								break;
							}
							if (this.currentCommandListInPipeline == null || this.currentEnumeratorInPipeline == null || !this.currentEnumeratorInPipeline.HasNext)
							{
								this.currentCommandListInPipeline = (SmtpOutSession.CommandList)this.pipelinedCommandQueue.Peek();
								if (this.AdvertisedEhloOptions.Pipelining && this.currentCommandListInPipeline.Equals(this.commandLists[8]))
								{
									this.numRcptCommandsInPipelineQueue--;
									if (this.numRcptCommandsInPipelineQueue < 0)
									{
										throw new InvalidOperationException("Number of recipients in pipeline should never be negative");
									}
									ExTraceGlobals.SmtpSendTracer.TraceDebug<int>((long)this.GetHashCode(), "Number of RCPT remaining to be sent out: {0}", this.numRcptCommandsInPipelineQueue);
								}
								if (!this.AdvertisedEhloOptions.Pipelining || !this.currentCommandListInPipeline.Equals(this.commandLists[8]) || this.numRcptCommandsInPipelineQueue == 0)
								{
									this.pipelinedCommandQueue.Dequeue();
								}
								this.currentEnumeratorInPipeline = (SmtpOutSession.CommandList.CommandListEnumerator)this.currentCommandListInPipeline.GetEnumerator();
							}
							if (!this.AdvertisedEhloOptions.Pipelining)
							{
								flag = false;
							}
							if (this.currentCommandListInPipeline.HasAddedCommands())
							{
								if (this.pipelinedResponseQueue.Count > 0)
								{
									ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "There are new commands added for the current commandList and the response Q is not empty. Waiting for response before sending out more commands");
									break;
								}
								flag = false;
							}
							string text;
							if (this.lastCmdNotSent != null)
							{
								text = this.lastCmdNotSent;
								this.lastCmdNotSent = null;
								flag = false;
								ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "LastCmdNotSent {0}", text);
							}
							else
							{
								if (!this.currentEnumeratorInPipeline.MoveNext())
								{
									throw new InvalidOperationException("Cannot MoveNext in the command list even though HasNext is true");
								}
								text = (string)this.currentEnumeratorInPipeline.Current;
								ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Command string : {0}", text);
								if (flag)
								{
									bool flag3 = true;
									bool flag4 = false;
									SmtpOutSession.CanCommandBePipelined(text, out flag3, out flag4);
									if (!flag4 && this.pipelinedResponseQueue.Count > 0)
									{
										this.lastCmdNotSent = text;
										ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Command {0} cannot be pipelined as we are waiting on responses of other commands previously sent", text);
										break;
									}
									flag = (flag4 && !flag3);
								}
							}
							SmtpCommand smtpCommand = this.CreateSmtpCommand(text);
							if (smtpCommand != null)
							{
								ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Invoking Command Handler for {0}", text);
								this.lastCommandPipelined = flag;
								flag2 = this.InvokeCommandHandler(smtpCommand);
								if (flag2 && flag)
								{
									throw new InvalidOperationException("ICH sent commands in the middle of a pipeline.");
								}
								num++;
							}
						}
					}
					finally
					{
						if (!flag2 && this.sendBuffer.Length > 0)
						{
							this.SendBufferThenReadLine();
							flag2 = true;
						}
					}
					return flag2;
				}
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting for response from remote server before sending any more commands");
				if (this.sendBuffer.Length != 0)
				{
					throw new InvalidOperationException("Smtp send buffer is not empty");
				}
				return false;
			}
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x000A195C File Offset: 0x0009FB5C
		private void SendBufferThenReadLine()
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SendBufferThenReadLine);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Flushing SendBuffer");
			this.connection.BeginWrite(this.sendBuffer.GetBuffer(), 0, this.sendBuffer.Length, SmtpOutSession.writeCompleteReadLine, this);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x000A19B0 File Offset: 0x0009FBB0
		private void SendBdatStream(byte[] command, Stream stream)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SendBdatStream);
			this.messageStream = stream;
			this.connection.BeginWrite(command, 0, command.Length, SmtpOutSession.writeBdatCompleteSendStream, this);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x000A19D8 File Offset: 0x0009FBD8
		private void SendDataStream(Stream stream)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.SendDataStream);
			this.messageStream = stream;
			this.connection.BeginWrite(this.messageStream, SmtpOutSession.writeStreamCompleteReadLine, this);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000A1A04 File Offset: 0x0009FC04
		private void ProcessResponse()
		{
			SmtpCommand command = (SmtpCommand)this.pipelinedResponseQueue.Dequeue();
			this.InvokeResponseHandler(command);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000A1A2C File Offset: 0x0009FC2C
		private void InvokeResponseHandler(SmtpCommand command)
		{
			this.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InvokeResponseHandler);
			SmtpResponse smtpResponse;
			if (!SmtpResponse.TryParse(this.response, out smtpResponse))
			{
				if (command != null)
				{
					command.Dispose();
				}
				throw new FormatException("Response text was incorrectly formed.");
			}
			this.response.Clear();
			if (command == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Invoked Response Handler for ConnectResponse");
				this.ConnectResponseEvent(smtpResponse);
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Invoked Response Handler for {0}", command.ProtocolCommandKeyword);
			command.SmtpResponse = smtpResponse;
			this.HandlePostParseResponse(command);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x000A1ABC File Offset: 0x0009FCBC
		private void HandlePostParseResponse(SmtpCommand command)
		{
			if ((string.Equals(command.SmtpResponse.StatusCode, "421", StringComparison.Ordinal) && !Util.UpgradeCustomPermanentFailure(this.Connector.ErrorPolicies, command.SmtpResponse, this.transportAppConfig) && this.FailoverPermittedForRemoteShutdown && !(command is QuitSmtpCommand)) || this.pipeLineFailOverPending)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Attempting failover. 421 Status code on {0}. NextState: Quit", command.ProtocolCommandKeyword);
				this.FailoverConnection(command.SmtpResponse, false);
				this.NextState = SmtpOutSession.SessionState.Quit;
			}
			else
			{
				command.OutboundProcessResponse();
				if ((byte)(this.secureState & SecureState.NegotiationRequested) == 128)
				{
					if (!(command is StarttlsSmtpCommand))
					{
						throw new InvalidOperationException("Command being processed is not StartTls");
					}
					command.Dispose();
					X509Certificate2 x509Certificate;
					if ((byte)(this.secureState & ~SecureState.NegotiationRequested) == 1)
					{
						x509Certificate = this.advertisedTlsCertificate;
					}
					else
					{
						x509Certificate = this.internalTransportCertificate;
					}
					this.logSession.LogCertificate("Sending certificate", x509Certificate);
					this.connection.BeginNegotiateTlsAsClient(x509Certificate, this.connection.RemoteEndPoint.Address.ToString(), SmtpOutSession.tlsNegotiationComplete, this);
					return;
				}
				else if (command.ParsingStatus == ParsingStatus.MoreDataRequired)
				{
					command.ProtocolCommand = null;
					command.ProtocolCommandString = null;
					command.ParsingStatus = ParsingStatus.Complete;
					if (!this.InvokeCommandHandler(command) && this.sendBuffer.Length != 0)
					{
						this.SendBufferThenReadLine();
					}
					return;
				}
			}
			command.Dispose();
			this.FinalizeNextStateAndSendCommands();
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x000A1C25 File Offset: 0x0009FE25
		private void SetupPoisonContext()
		{
			if (this.RoutedMailItem != null)
			{
				PoisonMessage.Context = new MessageContext(this.RoutedMailItem.RecordId, this.RoutedMailItem.InternetMessageId, MessageProcessingSource.SmtpSend);
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000A1C50 File Offset: 0x0009FE50
		private bool RequiresDownConversion()
		{
			bool result = false;
			if (this.RoutedMailItem != null)
			{
				switch (this.RoutedMailItem.BodyType)
				{
				case Microsoft.Exchange.Transport.BodyType.EightBitMIME:
					if (!this.AdvertisedEhloOptions.EightBitMime)
					{
						result = true;
					}
					break;
				case Microsoft.Exchange.Transport.BodyType.BinaryMIME:
					if (!this.AdvertisedEhloOptions.BinaryMime)
					{
						result = true;
					}
					else if (!this.AdvertisedEhloOptions.Chunking)
					{
						result = true;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000A1CC0 File Offset: 0x0009FEC0
		private bool ShouldAttemptSendingMessageOnSameConnection(out bool canCacheConnection)
		{
			canCacheConnection = false;
			if (this.smtpOutConnection.NextHopIsOutboundProxy)
			{
				canCacheConnection = true;
			}
			if (this.sendConnector.SmtpMaxMessagesPerConnection == 0 || this.messageSendAttemptCount < this.sendConnector.SmtpMaxMessagesPerConnection)
			{
				canCacheConnection = true;
				if (!this.smtpOutConnection.NextHopIsOutboundProxy && this.smtpOutConnection.TotalTargets > 1 && this.SendFewerMessagesToSlowerServerEnabled)
				{
					int num = (int)this.smtpOutConnection.GetDelayForCurrentTarget(this.RemoteEndPoint).TotalSeconds;
					if (num < 1)
					{
						return true;
					}
					if (this.messageCount > 0 && this.messageCount > this.sendConnector.SmtpMaxMessagesPerConnection / 2 - num)
					{
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Detected a possibly slower server with delay {0}. Completing after {1} messages", new object[]
						{
							num,
							this.messageCount
						});
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x000A1DA4 File Offset: 0x0009FFA4
		private void StartProcessingResponse(byte[] buffer, int offset, int size, bool overflow)
		{
			BufferBuilder bufferBuilder = this.responseBuffer ?? new BufferBuilder(size);
			try
			{
				this.SetupPoisonContext();
				if (bufferBuilder.Length + size > 32768)
				{
					this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "line too long");
					string message = string.Format("Illegal response, length exceeds the maximum that can be handled by SmtpOut. Max = {0} chars", 32768);
					throw new FormatException(message);
				}
				bufferBuilder.Append(buffer, offset, size);
				if (overflow)
				{
					this.responseBuffer = bufferBuilder;
					this.StartReadLine();
				}
				else
				{
					this.responseBuffer = null;
					bufferBuilder.RemoveUnusedBufferSpace();
					if (!(this.pipelinedResponseQueue.Peek() is AuthSmtpCommand))
					{
						this.logSession.LogReceive(bufferBuilder.GetBuffer());
					}
					string text = bufferBuilder.ToString();
					this.AppendToResponseBuffer(text);
					if (text.Length < 3)
					{
						throw new FormatException("Illegal response: " + text);
					}
					if (text.Length > 3 && text[3] == '-')
					{
						this.StartReadLine();
					}
					else
					{
						this.ProcessResponse();
					}
				}
			}
			catch (FormatException ex)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "The connection was dropped because a response was illegally formatted. The error is: {0}", ex.Message);
				string text2;
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.VerboseLogging.Enabled)
				{
					text2 = ex.ToString();
				}
				else
				{
					text2 = ex.Message;
				}
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "The connection was dropped because a response was illegally formatted. The error is: {0}", new object[]
				{
					text2
				});
				if (this.RoutedMailItem != null)
				{
					this.AckMessage(AckStatus.Retry, SmtpResponse.InvalidResponse);
				}
				if (this.nextHopConnection != null)
				{
					this.AckConnection(AckStatus.Retry, SmtpResponse.InvalidResponse, SessionSetupFailureReason.ProtocolError);
				}
				this.Disconnect();
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000A1F54 File Offset: 0x000A0154
		private void TlsNegotiationComplete()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<long>((long)this.GetHashCode(), "TLS negotiation completed for connection {0}. Reissue Ehlo", this.connectionId);
			if (this.connection.RemoteCertificate == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "No remote certificate present");
				this.HandleError(SecurityStatus.IncompleteCredentials);
				return;
			}
			this.logSession.LogCertificateThumbprint("Received certificate", this.connection.RemoteCertificate.Certificate);
			this.secureState &= ~SecureState.NegotiationRequested;
			if (!this.IsOpportunisticTls && this.connection.TlsCipherKeySize < 128)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<int>((long)this.GetHashCode(), "Quit session because Tls cipher strength is too weak at {0}", this.connection.TlsCipherKeySize);
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Tls cipher strength is too weak");
				this.FailoverConnection(SmtpResponse.AuthTempFailureTLSCipherTooWeak, SessionSetupFailureReason.ProtocolError);
				this.NextState = SmtpOutSession.SessionState.Quit;
				this.MoveToNextState();
				return;
			}
			if (this.IsOpportunisticTls)
			{
				this.AckDetails.ExtraEventData.Add(new KeyValuePair<string, string>("Microsoft.Exchange.Transport.MailRecipient.EffectiveTlsAuthLevel", SmtpOutSession.AuthLevelToString(new RequiredTlsAuthLevel?(RequiredTlsAuthLevel.EncryptionOnly))));
			}
			if (this.RemoteIdentity == SmtpOutSession.anonymousSecurityIdentifier)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Remote has supplied certificate {0}", this.connection.RemoteCertificate.Subject);
				if (this.secureState == SecureState.AnonymousTls && this.RequiresDirectTrust)
				{
					this.RemoteIdentity = DirectTrust.MapCertToSecurityIdentifier(this.connection.RemoteCertificate.Certificate);
					if (!(this.RemoteIdentity != SmtpOutSession.anonymousSecurityIdentifier))
					{
						string text = "DirectTrust certificate failed to authenticate";
						ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), text);
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendDirectTrustFailed, this.RemoteEndPoint.Address.ToString(), new object[]
						{
							this.connection.RemoteCertificate.Subject,
							this.RemoteEndPoint.Address
						});
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "SmtpSendDirectTrustFailed", null, text, ResultSeverityLevel.Error, false);
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, text);
						this.FailoverConnection(SmtpResponse.CertificateValidationFailure, SessionSetupFailureReason.ProtocolError);
						this.NextState = SmtpOutSession.SessionState.Quit;
						this.MoveToNextState();
						return;
					}
					this.RemoteIdentityName = this.connection.RemoteCertificate.Subject;
					this.SetSessionPermissions(this.RemoteIdentity);
					SmtpSessionCertificateUse use = (this.secureState == SecureState.StartTls) ? SmtpSessionCertificateUse.RemoteSTARTTLS : SmtpSessionCertificateUse.RemoteDirectTrust;
					CertificateExpiryCheck.CheckCertificateExpiry(this.connection.RemoteCertificate.Certificate, SmtpOutConnection.Events, use, this.connection.RemoteCertificate.Subject);
					this.logSession.LogCertificate("DirectTrust certificate", this.connection.RemoteCertificate.Certificate);
				}
				else if (this.Connector.SmartHostAuthMechanism == SmtpSendConnectorConfig.AuthMechanisms.BasicAuthRequireTLS)
				{
					ChainValidityStatus chainValidityStatus = ChainValidityStatus.SubjectMismatch;
					foreach (SmartHost smartHost in this.smtpOutConnection.Connector.SmartHosts)
					{
						string s = smartHost.ToString();
						SmtpDomainWithSubdomains domain;
						if (SmtpDomainWithSubdomains.TryParse(s, out domain) && this.certificateValidator.MatchCertificateFqdns(domain, this.connection.RemoteCertificate, MatchOptions.None, this.logSession))
						{
							chainValidityStatus = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate.Certificate, true);
							break;
						}
					}
					this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, Encoding.UTF8.GetBytes(chainValidityStatus.ToString()), "Chain validation status");
					if (chainValidityStatus != ChainValidityStatus.Valid)
					{
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_MessageSecurityTLSCertificateValidationFailure, this.smtpOutConnection.Connector.Name, new object[]
						{
							this.smtpOutConnection.Connector.Name,
							chainValidityStatus
						});
						string notificationReason = string.Format("Unable to validate the TLS certificate of the smart host for the connector {0}. The certificate validation error for the certificate is {1}.", this.smtpOutConnection.Connector.Name, chainValidityStatus);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "MessageSecurityTLSCertificateValidationFailure", null, notificationReason, ResultSeverityLevel.Error, false);
						this.FailoverConnection(SmtpResponse.CertificateValidationFailure, SessionSetupFailureReason.ProtocolError);
						this.NextState = SmtpOutSession.SessionState.Quit;
						this.MoveToNextState();
						return;
					}
					this.logSession.LogCertificate("SmartHost certificate", this.connection.RemoteCertificate.Certificate);
				}
				else if (this.transportConfiguration.TransportSettings.TransportSettings.IsTLSSendSecureDomain(this.NextHopConnection.Key.NextHopDomain))
				{
					string nextHopDomain = this.NextHopConnection.Key.NextHopDomain;
					if (!this.Connector.DomainSecureEnabled)
					{
						ExTraceGlobals.SmtpSendTracer.TraceError<string, string>((long)this.GetHashCode(), "Message to secure domain '{0}' failed because DomainSecureEnabled on send connector '{1}' was set to false", nextHopDomain, this.Connector.Name);
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_TlsDomainSecureDisabled, this.Connector.Name + " - " + nextHopDomain, new object[]
						{
							nextHopDomain,
							this.Connector.Name
						});
						this.FailoverConnection(SmtpResponse.DomainSecureDisabled, SessionSetupFailureReason.ProtocolError);
						this.NextState = SmtpOutSession.SessionState.Quit;
						this.MoveToNextState();
						return;
					}
					SmtpDomainWithSubdomains domain2 = new SmtpDomainWithSubdomains(new SmtpDomain(nextHopDomain), true);
					ChainValidityStatus chainValidityStatus2;
					if (!this.certificateValidator.MatchCertificateFqdns(domain2, this.connection.RemoteCertificate, MatchOptions.MultiLevelCertWildcards, this.logSession))
					{
						chainValidityStatus2 = ChainValidityStatus.SubjectMismatch;
					}
					else
					{
						chainValidityStatus2 = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate.Certificate, true);
					}
					if (chainValidityStatus2 != ChainValidityStatus.Valid && chainValidityStatus2 != (ChainValidityStatus)2148081683U)
					{
						Utils.SecureMailPerfCounters.DomainSecureOutboundSessionFailuresTotal.Increment();
						if (chainValidityStatus2 == ChainValidityStatus.SubjectMismatch)
						{
							string text2 = string.Format("Message to secure domain '{0}' on connector '{1}' failed because the TLS server certificate subject did not match.", nextHopDomain, this.Connector.Name);
							ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), text2);
							SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_TlsDomainServerCertificateSubjectMismatch, nextHopDomain, new object[]
							{
								nextHopDomain,
								this.Connector.Name,
								this.connection.RemoteCertificate.Subject
							});
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TlsDomainServerCertificateSubjectMismatch", null, text2, ResultSeverityLevel.Warning, false);
						}
						else
						{
							string text3 = string.Format("Message to secure domain '{0}' on connector '{1}' failed because the TLS server certificate chain failed to validate and returned status '{2}'.", nextHopDomain, this.Connector.Name, chainValidityStatus2);
							ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), text3);
							SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_TlsDomainServerCertificateValidationFailure, nextHopDomain, new object[]
							{
								nextHopDomain,
								this.Connector.Name,
								chainValidityStatus2
							});
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TlsDomainServerCertificateValidationFailure", null, text3, ResultSeverityLevel.Error, false);
						}
						if (!this.certificateValidator.ShouldTreatValidationResultAsSuccess(chainValidityStatus2))
						{
							this.FailoverConnection(SmtpResponse.CertificateValidationFailure, SessionSetupFailureReason.ProtocolError);
							this.NextState = SmtpOutSession.SessionState.Quit;
							this.MoveToNextState();
							return;
						}
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess, this.connection.RemoteCertificate.SerialNumber, new object[]
						{
							chainValidityStatus2.ToString(),
							this.connection.RemoteCertificate.SerialNumber,
							this.connection.RemoteCertificate.Subject,
							this.connection.RemoteCertificate.Issuer,
							this.connection.RemoteCertificate.Thumbprint,
							"SmtpOut"
						});
						this.logSession.LogCertificate(string.Format(CultureInfo.InvariantCulture, "CRL validation failed with status {0}. Treating the failure as success.", new object[]
						{
							chainValidityStatus2
						}), this.connection.RemoteCertificate.Certificate);
					}
					ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Upgrade remote identity to Partner.");
					this.RemoteIdentity = WellKnownSids.PartnerServers;
					this.RemoteIdentityName = nextHopDomain;
					this.SetSessionPermissions(this.Connector.GetPartnerPermissions());
					this.logSession.LogCertificate("Secure domain certificate", this.connection.RemoteCertificate.Certificate);
				}
				else if (this.TlsConfiguration.RequireTls && (this.TlsConfiguration.TlsAuthLevel == RequiredTlsAuthLevel.CertificateValidation || this.TlsConfiguration.TlsAuthLevel == RequiredTlsAuthLevel.DomainValidation))
				{
					ChainValidityStatus chainValidityStatus3 = ChainValidityStatus.Valid;
					RequiredTlsAuthLevel valueOrDefault = this.TlsConfiguration.TlsAuthLevel.GetValueOrDefault();
					RequiredTlsAuthLevel? requiredTlsAuthLevel;
					if (requiredTlsAuthLevel != null)
					{
						switch (valueOrDefault)
						{
						case RequiredTlsAuthLevel.CertificateValidation:
							chainValidityStatus3 = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate.Certificate, this.transportAppConfig.SmtpSendConfiguration.CacheOnlyUrlRetrievalForRemoteCertChain);
							break;
						case RequiredTlsAuthLevel.DomainValidation:
							if (!SmtpOutSession.MatchCertificateWithTlsDomain(this.TlsConfiguration.TlsDomains, this.connection.RemoteCertificate, this.logSession, this.certificateValidator))
							{
								chainValidityStatus3 = ChainValidityStatus.SubjectMismatch;
							}
							else
							{
								chainValidityStatus3 = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate.Certificate, this.transportAppConfig.SmtpSendConfiguration.CacheOnlyUrlRetrievalForRemoteCertChain);
							}
							break;
						}
					}
					if (chainValidityStatus3 != ChainValidityStatus.Valid)
					{
						ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Outbound TLS authentication failed with error {0} for Send connector {1}. The TLS authentication mechanism is {3}.Target is {4}.", new object[]
						{
							chainValidityStatus3.ToString(),
							this.Connector.Name,
							this.TlsConfiguration.TlsAuthLevel.ToString(),
							this.NextHopConnection.Key.NextHopDomain
						});
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendOutboundAtTLSAuthLevelFailed, this.NextHopConnection.Key.NextHopDomain, new object[]
						{
							chainValidityStatus3.ToString(),
							this.Connector.Name,
							this.TlsConfiguration.TlsAuthLevel.ToString(),
							this.NextHopConnection.Key.NextHopDomain
						});
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, string.Format("Outbound TLS authentication failed for auth level {0} with error {1}", this.TlsConfiguration.TlsAuthLevel.ToString(), chainValidityStatus3.ToString()));
						if (!Components.CertificateComponent.Validator.ShouldTreatValidationResultAsSuccess(chainValidityStatus3))
						{
							this.FailoverConnection(SmtpResponse.CertificateValidationFailure, SessionSetupFailureReason.ProtocolError);
							this.NextState = SmtpOutSession.SessionState.Quit;
							this.MoveToNextState();
							return;
						}
						ExTraceGlobals.SmtpSendTracer.TraceDebug<ChainValidityStatus>((long)this.GetHashCode(), "Treating certification validation failure {0} as succcess", chainValidityStatus3);
						SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess, this.connection.RemoteCertificate.SerialNumber, new object[]
						{
							chainValidityStatus3.ToString(),
							this.connection.RemoteCertificate.SerialNumber,
							this.connection.RemoteCertificate.Subject,
							this.connection.RemoteCertificate.Issuer,
							this.connection.RemoteCertificate.Thumbprint,
							"SmtpOut"
						});
						this.logSession.LogCertificate(string.Format(CultureInfo.InvariantCulture, "CRL validation failed with status {0}. Treating the failure as success.", new object[]
						{
							chainValidityStatus3
						}), this.connection.RemoteCertificate.Certificate);
					}
					this.AckDetails.ExtraEventData.Add(new KeyValuePair<string, string>("Microsoft.Exchange.Transport.MailRecipient.EffectiveTlsAuthLevel", SmtpOutSession.AuthLevelToString(this.TlsConfiguration.TlsAuthLevel)));
				}
			}
			this.NextState = SmtpOutSession.SessionState.Ehlo;
			this.MoveToNextState();
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000A2B30 File Offset: 0x000A0D30
		private bool PreCheckMessageSize()
		{
			this.needToDownconvertMIME = this.RequiresDownConversion();
			if (!this.needToDownconvertMIME)
			{
				long mimeSize = this.RoutedMailItem.MimeSize;
				if (!this.RemoteIsAuthenticated && !this.IsAuthenticated && this.AdvertisedEhloOptions.Size == SizeMode.Enabled && this.AdvertisedEhloOptions.MaxSize > 0L && mimeSize > this.AdvertisedEhloOptions.MaxSize)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Message from {0} was NDR'ed because the size was {1} whereas the maximum allowed size by the receiving server (at {2}) was {3}", new object[]
					{
						this.RoutedMailItem.From.ToString(),
						mimeSize,
						this.sessionProps.RemoteEndPoint,
						this.AdvertisedEhloOptions.MaxSize
					});
					this.RoutedMailItem.AddDsnParameters("MaxMessageSizeInKB", this.AdvertisedEhloOptions.MaxSize >> 10);
					this.RoutedMailItem.AddDsnParameters("CurrentMessageSizeInKB", mimeSize >> 10);
					this.AckMessage(AckStatus.Fail, AckReason.OverAdvertisedSizeLimit);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000A2C5C File Offset: 0x000A0E5C
		private bool CheckLongRecipientSupport(MailRecipient recipient, bool supportLongAddresses)
		{
			if (Util.IsLongAddress(recipient.Email))
			{
				if (!Util.IsValidInnerAddress(recipient.Email))
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because the long address is invalid", recipient.Email.ToString());
					recipient.Ack(AckStatus.Fail, AckReason.SmtpSendInvalidLongRecipientAddress);
					return false;
				}
				if (!supportLongAddresses)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because the session does not support long addresses", recipient.Email.ToString());
					recipient.Ack(AckStatus.Fail, AckReason.SmtpSendLongRecipientAddress);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000A2CFC File Offset: 0x000A0EFC
		private bool CheckOrarSupport(MailRecipient recipient, bool supportOrar, bool supportLongAddresses)
		{
			RoutingAddress address;
			if (!OrarGenerator.TryGetOrarAddress(recipient, out address))
			{
				return true;
			}
			if (!supportOrar)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because the ORAR address could not be transmitted", recipient.Email.ToString());
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendUnableToTransmitOrar, null, new object[]
				{
					this.AdvertisedEhloOptions.AdvertisedFQDN,
					this.Connector.Name,
					this.RoutedMailItem.InternetMessageId,
					recipient.Email.ToString()
				});
				recipient.Ack(AckStatus.Fail, AckReason.SmtpSendOrarNotTransmittable);
				return false;
			}
			if (!supportLongAddresses && Util.IsLongAddress(address))
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because the long ORAR address could not be transmitted", recipient.Email.ToString());
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendUnableToTransmitLongOrar, null, new object[]
				{
					address.ToString(),
					this.AdvertisedEhloOptions.AdvertisedFQDN,
					this.Connector.Name,
					this.RoutedMailItem.InternetMessageId,
					recipient.Email.ToString()
				});
				recipient.Ack(AckStatus.Fail, AckReason.SmtpSendLongOrarNotTransmittable);
				return false;
			}
			if (!this.AdvertisedEhloOptions.XOrar)
			{
				this.exch50DataPresent = true;
			}
			return true;
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000A2E80 File Offset: 0x000A1080
		private bool CheckRDstSupport(MailRecipient recipient, bool supportRDst)
		{
			if (recipient.ExtendedProperties.Contains("Microsoft.Exchange.Transport.RoutingOverride"))
			{
				if (!supportRDst)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Recipient '{0}' was failed because Routing Destination could not be transmitted", recipient.Email.ToString());
					SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendUnableToTransmitRDst, null, new object[]
					{
						this.AdvertisedEhloOptions.AdvertisedFQDN,
						this.Connector.Name,
						this.RoutedMailItem.InternetMessageId,
						recipient.Email.ToString()
					});
					recipient.Ack(AckStatus.Fail, AckReason.SmtpSendRDstNotTransmittable);
					return false;
				}
				if (!this.AdvertisedEhloOptions.XRDst)
				{
					this.exch50DataPresent = true;
				}
			}
			return true;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000A2F4F File Offset: 0x000A114F
		private bool IsHubDeliveringToMailbox(ProcessTransportRole processTransportRole)
		{
			return processTransportRole == ProcessTransportRole.Hub && this.NextHopDeliveryType == DeliveryType.SmtpDeliveryToMailbox;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000A2F60 File Offset: 0x000A1160
		private bool IsHubDeliveringToNonMailbox(ProcessTransportRole processTransportRole)
		{
			return processTransportRole == ProcessTransportRole.Hub && this.NextHopDeliveryType != DeliveryType.SmtpDeliveryToMailbox;
		}

		// Token: 0x0400146D RID: 5229
		private const string MailCommand = "MAIL";

		// Token: 0x0400146E RID: 5230
		private const string RcptCommand = "RCPT";

		// Token: 0x0400146F RID: 5231
		private const string RsetCommand = "RSET";

		// Token: 0x04001470 RID: 5232
		private const string DataCommand = "DATA";

		// Token: 0x04001471 RID: 5233
		private const int MaxOutstandingResponses = 100;

		// Token: 0x04001472 RID: 5234
		private const int MaxPipelinedCommands = 200;

		// Token: 0x04001473 RID: 5235
		private const int MaxResponseLength = 32768;

		// Token: 0x04001474 RID: 5236
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x04001475 RID: 5237
		public static readonly byte[] BinaryData = Util.AsciiStringToBytesAndAppendCRLF("<Binary Data>");

		// Token: 0x04001476 RID: 5238
		protected TransportAppConfig transportAppConfig;

		// Token: 0x04001477 RID: 5239
		protected ITransportConfiguration transportConfiguration;

		// Token: 0x04001478 RID: 5240
		protected NetworkConnection connection;

		// Token: 0x04001479 RID: 5241
		protected bool dontCacheThisConnection;

		// Token: 0x0400147A RID: 5242
		protected BufferBuilder sendBuffer = new BufferBuilder();

		// Token: 0x0400147B RID: 5243
		protected ShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x0400147C RID: 5244
		protected RecipientCorrelator recipientCorrelator;

		// Token: 0x0400147D RID: 5245
		protected IProtocolLogSession logSession;

		// Token: 0x0400147E RID: 5246
		private static readonly AsyncCallback writeCompleteReadLine = new AsyncCallback(SmtpOutSession.WriteCompleteReadLine);

		// Token: 0x0400147F RID: 5247
		private static readonly AsyncCallback writeBdatCompleteSendStream = new AsyncCallback(SmtpOutSession.WriteBdatCompleteSendStream);

		// Token: 0x04001480 RID: 5248
		private static readonly AsyncCallback writeStreamCompleteReadLine = new AsyncCallback(SmtpOutSession.WriteStreamCompleteReadLine);

		// Token: 0x04001481 RID: 5249
		private static readonly AsyncCallback tlsNegotiationComplete = new AsyncCallback(SmtpOutSession.TlsNegotiationComplete);

		// Token: 0x04001482 RID: 5250
		private static readonly AsyncCallback readLineComplete = new AsyncCallback(SmtpOutSession.ReadLineComplete);

		// Token: 0x04001483 RID: 5251
		private static readonly SecurityIdentifier anonymousSecurityIdentifier = new SecurityIdentifier(WellKnownSidType.AnonymousSid, null);

		// Token: 0x04001484 RID: 5252
		private readonly SmtpOutSession.CommandList[] commandLists;

		// Token: 0x04001485 RID: 5253
		private readonly bool isProbeSession;

		// Token: 0x04001486 RID: 5254
		private SmtpOutConnection smtpOutConnection;

		// Token: 0x04001487 RID: 5255
		private DateTime sessionStartTime;

		// Token: 0x04001488 RID: 5256
		private int messageCount;

		// Token: 0x04001489 RID: 5257
		private int messageSendAttemptCount;

		// Token: 0x0400148A RID: 5258
		private long bytesSentAtLastCount;

		// Token: 0x0400148B RID: 5259
		private NextHopConnection nextHopConnection;

		// Token: 0x0400148C RID: 5260
		private IReadOnlyMailItem routedMailItem;

		// Token: 0x0400148D RID: 5261
		private bool shadowCurrentMailItem;

		// Token: 0x0400148E RID: 5262
		private bool shutdownConnectionCalled;

		// Token: 0x0400148F RID: 5263
		private MailRecipient currentRecipient;

		// Token: 0x04001490 RID: 5264
		private int numberOfRecipientsAttempted;

		// Token: 0x04001491 RID: 5265
		private int numberOfRecipientsSucceeded;

		// Token: 0x04001492 RID: 5266
		private int numberOfRecipientsAckedForRetry;

		// Token: 0x04001493 RID: 5267
		private int numberOfRecipientsAcked;

		// Token: 0x04001494 RID: 5268
		private Stream messageStream;

		// Token: 0x04001495 RID: 5269
		private MailRecipient nextRecipient;

		// Token: 0x04001496 RID: 5270
		private Queue pipelinedCommandQueue;

		// Token: 0x04001497 RID: 5271
		private Queue pipelinedResponseQueue;

		// Token: 0x04001498 RID: 5272
		private List<string> response;

		// Token: 0x04001499 RID: 5273
		private bool betweenMessagesRset;

		// Token: 0x0400149A RID: 5274
		private bool issueBetweenMsgRset;

		// Token: 0x0400149B RID: 5275
		private bool doPrepareForNextMessage;

		// Token: 0x0400149C RID: 5276
		private SecureState secureState;

		// Token: 0x0400149D RID: 5277
		private SecurityIdentifier sessionRemoteIdentity = SmtpOutSession.anonymousSecurityIdentifier;

		// Token: 0x0400149E RID: 5278
		private string sessionRemoteIdentityName = "anonymous";

		// Token: 0x0400149F RID: 5279
		private X509Certificate2 advertisedTlsCertificate;

		// Token: 0x040014A0 RID: 5280
		private X509Certificate2 internalTransportCertificate;

		// Token: 0x040014A1 RID: 5281
		private SecureString connectorPassword;

		// Token: 0x040014A2 RID: 5282
		private MultilevelAuthMechanism authMethod;

		// Token: 0x040014A3 RID: 5283
		private bool isAuthenticated;

		// Token: 0x040014A4 RID: 5284
		private Permission sessionPermissions;

		// Token: 0x040014A5 RID: 5285
		private bool shadowRedundancyEnabled;

		// Token: 0x040014A6 RID: 5286
		private bool shadowed;

		// Token: 0x040014A7 RID: 5287
		private bool disconnected;

		// Token: 0x040014A8 RID: 5288
		private AckDetails ackDetails;

		// Token: 0x040014A9 RID: 5289
		private bool presentInSmtpOutSessionCache;

		// Token: 0x040014AA RID: 5290
		private SmtpOutSession.SessionState currentState;

		// Token: 0x040014AB RID: 5291
		private SmtpOutSession.SessionState nextState;

		// Token: 0x040014AC RID: 5292
		private SmtpOutSession.CommandList.CommandListEnumerator currentEnumeratorInPipeline;

		// Token: 0x040014AD RID: 5293
		private SmtpOutSession.CommandList currentCommandListInPipeline;

		// Token: 0x040014AE RID: 5294
		private string lastCmdNotSent;

		// Token: 0x040014AF RID: 5295
		private bool lastCommandPipelined;

		// Token: 0x040014B0 RID: 5296
		private bool recipsAckedPending;

		// Token: 0x040014B1 RID: 5297
		private int numRcptCommandsInPipelineQueue;

		// Token: 0x040014B2 RID: 5298
		private SmtpSessionProps sessionProps;

		// Token: 0x040014B3 RID: 5299
		private long connectionId;

		// Token: 0x040014B4 RID: 5300
		private SmtpSendConnectorConfig sendConnector;

		// Token: 0x040014B5 RID: 5301
		private bool usingHELO;

		// Token: 0x040014B6 RID: 5302
		private readonly SmtpSendPerfCountersInstance smtpSendPerformanceCounters;

		// Token: 0x040014B7 RID: 5303
		private bool needToDownconvertMIME;

		// Token: 0x040014B8 RID: 5304
		private bool pipeLineNextMessagePending;

		// Token: 0x040014B9 RID: 5305
		private bool pipeLineFailOverPending;

		// Token: 0x040014BA RID: 5306
		private SmtpResponse pipeLineFailOverResponse;

		// Token: 0x040014BB RID: 5307
		private BufferBuilder responseBuffer;

		// Token: 0x040014BC RID: 5308
		private Breadcrumbs<SmtpOutSession.SmtpOutSessionBreadcrumbs> breadcrumbs = new Breadcrumbs<SmtpOutSession.SmtpOutSessionBreadcrumbs>(64);

		// Token: 0x040014BD RID: 5309
		private bool failoverInProgress;

		// Token: 0x040014BE RID: 5310
		private bool exch50DataPresent;

		// Token: 0x040014BF RID: 5311
		private bool useDowngradedExchangeServerAuth;

		// Token: 0x040014C0 RID: 5312
		private IMailRouter mailRouter;

		// Token: 0x040014C1 RID: 5313
		private CertificateCache certificateCache;

		// Token: 0x040014C2 RID: 5314
		private CertificateValidator certificateValidator;

		// Token: 0x040014C3 RID: 5315
		private Queue<SmtpMessageContextBlob> blobsToSend;

		// Token: 0x040014C4 RID: 5316
		private Queue<string> remainingXProxyToCommands;

		// Token: 0x040014C5 RID: 5317
		private SmtpSendConnectorConfig outboundProxySendConnector;

		// Token: 0x040014C6 RID: 5318
		private bool nextHopIsProxyingBlindly;

		// Token: 0x040014C7 RID: 5319
		private string helloDomainOfOutboundProxyFrontEnd;

		// Token: 0x040014C8 RID: 5320
		private bool xRsetProxyToAccepted;

		// Token: 0x040014C9 RID: 5321
		private SmtpOutSession.OutboundProxyOriginalSessionState outboundProxyOriginalSessionState;

		// Token: 0x040014CA RID: 5322
		private int messagesSentOverSession;

		// Token: 0x02000398 RID: 920
		public enum SessionState
		{
			// Token: 0x040014CD RID: 5325
			ConnectResponse,
			// Token: 0x040014CE RID: 5326
			Ehlo,
			// Token: 0x040014CF RID: 5327
			Helo,
			// Token: 0x040014D0 RID: 5328
			Auth,
			// Token: 0x040014D1 RID: 5329
			Exps,
			// Token: 0x040014D2 RID: 5330
			StartTLS,
			// Token: 0x040014D3 RID: 5331
			AnonymousTLS,
			// Token: 0x040014D4 RID: 5332
			MessageStart,
			// Token: 0x040014D5 RID: 5333
			PerRecipient,
			// Token: 0x040014D6 RID: 5334
			Data,
			// Token: 0x040014D7 RID: 5335
			Xexch50,
			// Token: 0x040014D8 RID: 5336
			Bdat,
			// Token: 0x040014D9 RID: 5337
			XBdatBlob,
			// Token: 0x040014DA RID: 5338
			XShadow,
			// Token: 0x040014DB RID: 5339
			XQDiscard,
			// Token: 0x040014DC RID: 5340
			XProxy,
			// Token: 0x040014DD RID: 5341
			XProxyFrom,
			// Token: 0x040014DE RID: 5342
			XProxyTo,
			// Token: 0x040014DF RID: 5343
			XSessionParams,
			// Token: 0x040014E0 RID: 5344
			Quit,
			// Token: 0x040014E1 RID: 5345
			Rset,
			// Token: 0x040014E2 RID: 5346
			Inactive,
			// Token: 0x040014E3 RID: 5347
			XShadowRequest,
			// Token: 0x040014E4 RID: 5348
			XRsetProxyTo,
			// Token: 0x040014E5 RID: 5349
			NumStates
		}

		// Token: 0x02000399 RID: 921
		public enum SmtpOutSessionBreadcrumbs
		{
			// Token: 0x040014E7 RID: 5351
			EMPTY,
			// Token: 0x040014E8 RID: 5352
			FailoverConnection,
			// Token: 0x040014E9 RID: 5353
			SetNextStateToQuit,
			// Token: 0x040014EA RID: 5354
			AckConnection,
			// Token: 0x040014EB RID: 5355
			ResetSession,
			// Token: 0x040014EC RID: 5356
			PrepareForNextMessageOnCachedSession,
			// Token: 0x040014ED RID: 5357
			AckMessage,
			// Token: 0x040014EE RID: 5358
			SetNextStateForCachedSession,
			// Token: 0x040014EF RID: 5359
			CreateCmdConnectResponse,
			// Token: 0x040014F0 RID: 5360
			CreateCmdEhlo,
			// Token: 0x040014F1 RID: 5361
			CreateCmdHelo,
			// Token: 0x040014F2 RID: 5362
			CreateCmdAuth,
			// Token: 0x040014F3 RID: 5363
			CreateCmdStarttls,
			// Token: 0x040014F4 RID: 5364
			CreateCmdMail,
			// Token: 0x040014F5 RID: 5365
			CreateCmdRcpt,
			// Token: 0x040014F6 RID: 5366
			CreateCmdXexch50,
			// Token: 0x040014F7 RID: 5367
			CreateCmdData,
			// Token: 0x040014F8 RID: 5368
			Disconnect,
			// Token: 0x040014F9 RID: 5369
			CreateCmdBdat,
			// Token: 0x040014FA RID: 5370
			CreateCmdRset,
			// Token: 0x040014FB RID: 5371
			CreateCmdQuit,
			// Token: 0x040014FC RID: 5372
			EnqueueResponseHandler,
			// Token: 0x040014FD RID: 5373
			MoveToNextState,
			// Token: 0x040014FE RID: 5374
			SendPipelinedCommands,
			// Token: 0x040014FF RID: 5375
			SendBufferThenReadLine,
			// Token: 0x04001500 RID: 5376
			SendBdatStream,
			// Token: 0x04001501 RID: 5377
			SendDataStream,
			// Token: 0x04001502 RID: 5378
			InvokeCommandHandler,
			// Token: 0x04001503 RID: 5379
			InvokeResponseHandler,
			// Token: 0x04001504 RID: 5380
			PrepareForNextMessage,
			// Token: 0x04001505 RID: 5381
			SendNextCommands,
			// Token: 0x04001506 RID: 5382
			ShutdownConnection,
			// Token: 0x04001507 RID: 5383
			CreateCmdXShadow,
			// Token: 0x04001508 RID: 5384
			CreateCmdXQDiscard,
			// Token: 0x04001509 RID: 5385
			PrepareNextStateForEstablishedSession,
			// Token: 0x0400150A RID: 5386
			InboundProxyCreateCmdConnectResponse,
			// Token: 0x0400150B RID: 5387
			InboundProxyCreateCmdEhlo,
			// Token: 0x0400150C RID: 5388
			InboundProxyCreateCmdAuth,
			// Token: 0x0400150D RID: 5389
			InboundProxyCreateCmdStarttls,
			// Token: 0x0400150E RID: 5390
			InboundProxyCreateCmdMail,
			// Token: 0x0400150F RID: 5391
			InboundProxyCreateCmdRcpt,
			// Token: 0x04001510 RID: 5392
			InboundProxyCreateCmdData,
			// Token: 0x04001511 RID: 5393
			InboundProxyCreateCmdBdat,
			// Token: 0x04001512 RID: 5394
			InboundProxyCreateCmdRset,
			// Token: 0x04001513 RID: 5395
			InboundProxyCreateCmdQuit,
			// Token: 0x04001514 RID: 5396
			InboundProxyShutdownConnection,
			// Token: 0x04001515 RID: 5397
			InboundProxyInvokeCommandHandler,
			// Token: 0x04001516 RID: 5398
			InboundProxySendDataBuffers,
			// Token: 0x04001517 RID: 5399
			InboundProxyWriteBdatCompleteSendBuffers,
			// Token: 0x04001518 RID: 5400
			InboundProxyReadFromProxyLayerComplete,
			// Token: 0x04001519 RID: 5401
			InboundProxyWriteProxiedBytesToTargetComplete,
			// Token: 0x0400151A RID: 5402
			InboundProxyCreateCmdXProxyFrom,
			// Token: 0x0400151B RID: 5403
			InboundProxyPrepareForNextMessageOnCachedSession,
			// Token: 0x0400151C RID: 5404
			CreateCmdXProxyTo,
			// Token: 0x0400151D RID: 5405
			PrepareSendXshadowOrMessage,
			// Token: 0x0400151E RID: 5406
			ShadowCreateCmdConnectResponse,
			// Token: 0x0400151F RID: 5407
			ShadowCreateCmdEhlo,
			// Token: 0x04001520 RID: 5408
			ShadowCreateCmdAuth,
			// Token: 0x04001521 RID: 5409
			ShadowCreateCmdStarttls,
			// Token: 0x04001522 RID: 5410
			ShadowCreateCmdXShadowRequest,
			// Token: 0x04001523 RID: 5411
			ShadowCreateCmdRset,
			// Token: 0x04001524 RID: 5412
			ShadowCreateCmdQuit,
			// Token: 0x04001525 RID: 5413
			ShadowCreateCmdMail,
			// Token: 0x04001526 RID: 5414
			ShadowCreateCmdRcpt,
			// Token: 0x04001527 RID: 5415
			ShadowCreateCmdData,
			// Token: 0x04001528 RID: 5416
			ShadowCreateCmdBdat,
			// Token: 0x04001529 RID: 5417
			CreateCmdXSessionParams,
			// Token: 0x0400152A RID: 5418
			SerializeExtendedPropertiesBlob,
			// Token: 0x0400152B RID: 5419
			SerializeAdrcPropertiesBlob,
			// Token: 0x0400152C RID: 5420
			SerializeFastIndexBlob,
			// Token: 0x0400152D RID: 5421
			CreateCmdXRsetProxyTo
		}

		// Token: 0x0200039A RID: 922
		[Serializable]
		public class CommandList : IEnumerable
		{
			// Token: 0x06002938 RID: 10552 RVA: 0x000A2FF2 File Offset: 0x000A11F2
			public CommandList(SmtpOutSession.SessionState state)
			{
				this.commands = new HybridDictionary();
				this.commands.Add(this.highestIndex, SmtpOutSession.CommandList.protocolCommands[(int)state]);
			}

			// Token: 0x17000C84 RID: 3204
			// (get) Token: 0x06002939 RID: 10553 RVA: 0x000A3022 File Offset: 0x000A1222
			private int LowestIndex
			{
				get
				{
					return this.lowestIndex;
				}
			}

			// Token: 0x17000C85 RID: 3205
			// (get) Token: 0x0600293A RID: 10554 RVA: 0x000A302A File Offset: 0x000A122A
			private int HighestIndex
			{
				get
				{
					return this.highestIndex;
				}
			}

			// Token: 0x17000C86 RID: 3206
			private string this[int index]
			{
				get
				{
					if (this.commands.Contains(index))
					{
						return (string)this.commands[index];
					}
					return null;
				}
			}

			// Token: 0x0600293C RID: 10556 RVA: 0x000A305F File Offset: 0x000A125F
			public void AddCommandToBeginningOfList(string command)
			{
				this.lowestIndex--;
				this.commands.Add(this.lowestIndex, command);
			}

			// Token: 0x0600293D RID: 10557 RVA: 0x000A3086 File Offset: 0x000A1286
			public void AddCommandToEndOfList(string command)
			{
				this.highestIndex++;
				this.commands.Add(this.highestIndex, command);
			}

			// Token: 0x0600293E RID: 10558 RVA: 0x000A30AD File Offset: 0x000A12AD
			public bool HasCommandsBeforePredefined()
			{
				return this.lowestIndex < 0;
			}

			// Token: 0x0600293F RID: 10559 RVA: 0x000A30B8 File Offset: 0x000A12B8
			public bool HasCommandsAfterPredefined()
			{
				return this.highestIndex > 0;
			}

			// Token: 0x06002940 RID: 10560 RVA: 0x000A30C3 File Offset: 0x000A12C3
			public bool HasAddedCommands()
			{
				return this.HasCommandsBeforePredefined() || this.HasCommandsAfterPredefined();
			}

			// Token: 0x06002941 RID: 10561 RVA: 0x000A30D5 File Offset: 0x000A12D5
			public void Commit(SmtpOutSession.CommandList commandList)
			{
				this.commands = commandList.commands;
				this.lowestIndex = commandList.lowestIndex;
				this.highestIndex = commandList.highestIndex;
			}

			// Token: 0x06002942 RID: 10562 RVA: 0x000A30FB File Offset: 0x000A12FB
			public IEnumerator GetEnumerator()
			{
				return new SmtpOutSession.CommandList.CommandListEnumerator(this);
			}

			// Token: 0x06002943 RID: 10563 RVA: 0x000A3104 File Offset: 0x000A1304
			private static string[] InitializeProtocolCommands()
			{
				string[] array = new string[24];
				array[0] = "ConnectResponse";
				array[1] = "EHLO";
				array[3] = "AUTH";
				array[4] = "X-EXPS";
				array[5] = "STARTTLS";
				array[6] = "X-ANONYMOUSTLS";
				array[2] = "HELO";
				array[7] = "MAIL";
				array[8] = "RCPT";
				array[10] = "XEXCH50";
				array[9] = "DATA";
				array[11] = "BDAT";
				array[12] = "XBDATBLOB";
				array[13] = "XSHADOW";
				array[22] = "XSHADOWREQUEST";
				array[14] = "XQDISCARD";
				array[15] = "XPROXY";
				array[16] = "XPROXYFROM";
				array[17] = "XPROXYTO";
				array[18] = "XSESSIONPARAMS";
				array[20] = "RSET";
				array[19] = "QUIT";
				array[23] = "XRSETPROXYTO";
				return array;
			}

			// Token: 0x0400152E RID: 5422
			private static string[] protocolCommands = SmtpOutSession.CommandList.InitializeProtocolCommands();

			// Token: 0x0400152F RID: 5423
			private HybridDictionary commands;

			// Token: 0x04001530 RID: 5424
			private int lowestIndex;

			// Token: 0x04001531 RID: 5425
			private int highestIndex;

			// Token: 0x0200039B RID: 923
			public class CommandListEnumerator : IEnumerator
			{
				// Token: 0x06002945 RID: 10565 RVA: 0x000A31EC File Offset: 0x000A13EC
				public CommandListEnumerator(SmtpOutSession.CommandList commandList)
				{
					this.commandList = commandList;
					this.Reset();
				}

				// Token: 0x17000C87 RID: 3207
				// (get) Token: 0x06002946 RID: 10566 RVA: 0x000A3208 File Offset: 0x000A1408
				public object Current
				{
					get
					{
						return this.commandList[this.index];
					}
				}

				// Token: 0x17000C88 RID: 3208
				// (get) Token: 0x06002947 RID: 10567 RVA: 0x000A321B File Offset: 0x000A141B
				public bool HasNext
				{
					get
					{
						return this.index < this.commandList.HighestIndex;
					}
				}

				// Token: 0x06002948 RID: 10568 RVA: 0x000A3230 File Offset: 0x000A1430
				public void Reset()
				{
					this.index = this.commandList.LowestIndex - 1;
				}

				// Token: 0x06002949 RID: 10569 RVA: 0x000A3245 File Offset: 0x000A1445
				public bool MoveNext()
				{
					if (this.HasNext)
					{
						this.index++;
						return true;
					}
					return false;
				}

				// Token: 0x04001532 RID: 5426
				private SmtpOutSession.CommandList commandList;

				// Token: 0x04001533 RID: 5427
				private int index = -1;
			}
		}

		// Token: 0x0200039C RID: 924
		private class OutboundProxyOriginalSessionState
		{
			// Token: 0x0600294A RID: 10570 RVA: 0x000A3260 File Offset: 0x000A1460
			public OutboundProxyOriginalSessionState(IEhloOptions ehloOptions, SmtpSendConnectorConfig sendConnector, SecurityIdentifier remoteIdentity, string remoteIdentityName, AckDetails ackDetails, Permission sessionPermissions)
			{
				this.EhloOptions = ehloOptions;
				this.SendConnector = sendConnector;
				this.RemoteIdentity = remoteIdentity;
				this.RemoteIdentityName = remoteIdentityName;
				this.AckDetails = ackDetails;
				this.SessionPermissions = sessionPermissions;
			}

			// Token: 0x04001534 RID: 5428
			public readonly IEhloOptions EhloOptions;

			// Token: 0x04001535 RID: 5429
			public readonly SmtpSendConnectorConfig SendConnector;

			// Token: 0x04001536 RID: 5430
			public readonly SecurityIdentifier RemoteIdentity;

			// Token: 0x04001537 RID: 5431
			public readonly string RemoteIdentityName;

			// Token: 0x04001538 RID: 5432
			public readonly AckDetails AckDetails;

			// Token: 0x04001539 RID: 5433
			public readonly Permission SessionPermissions;
		}
	}
}
