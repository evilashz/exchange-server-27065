using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageThrottling;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000509 RID: 1289
	internal class SmtpInSessionState : SmtpSession, IDisposeTrackable, IDisposable
	{
		// Token: 0x06003B91 RID: 15249 RVA: 0x000F8C7C File Offset: 0x000F6E7C
		public SmtpInSessionState(SmtpInServerState serverState, INetworkConnection networkConnection, SmtpReceiveConnectorStub receiveConnectorStub)
		{
			ArgumentValidator.ThrowIfNull("serverState", serverState);
			ArgumentValidator.ThrowIfNull("networkConnection", networkConnection);
			ArgumentValidator.ThrowIfNull("receiveConnectorStub", receiveConnectorStub);
			this.ServerState = serverState;
			this.AuthzAuthorization = serverState.AuthzAuthorization;
			this.CertificateValidator = serverState.CertificateValidator;
			this.EventLog = serverState.EventLog;
			this.EventNotificationItem = serverState.EventNotificationItem;
			this.ExpectedMessageContextBlobs = SmtpInSessionState.EmptyInboundMessageContextBlobs;
			this.MessageContextBlob = serverState.MessageContextBlob;
			this.Tracer = serverState.Tracer;
			this.NetworkConnection = networkConnection;
			this.RemoteEndPoint = this.NetworkConnection.RemoteEndPoint;
			this.Configuration = this.ServerState.SmtpConfiguration;
			this.MessageThrottlingManager = serverState.MessageThrottlingManager;
			this.ReceiveConnector = receiveConnectorStub.Connector;
			this.ReceiveConnectorStub = receiveConnectorStub;
			this.SmtpResponse = SmtpResponse.Empty;
			this.DisconnectReason = DisconnectReason.None;
			this.LastExternalIPAddress = (this.IsExternalConnection ? this.RemoteEndPoint.Address : null);
			this.SessionStartTime = DateTime.UtcNow;
			this.sessionId = Microsoft.Exchange.Transport.SessionId.GetNextSessionId();
			this.AdvertisedEhloOptions = SmtpInSessionState.CreateEhloOptions(networkConnection, this.ReceiveConnector, this.AdvertisedDomain);
			this.ExtendedProtectionConfig = SmtpInSessionState.CreateExtendedProtectionConfig(this.ReceiveConnector.ExtendedProtectionPolicy);
			this.disposeTracker = this.GetDisposeTracker();
			IMExSession mexRuntimeSession;
			this.SmtpAgentSession = serverState.AgentRuntime.NewSmtpAgentSession(this, this.ServerState.IsMemberOfResolver, this.Configuration.TransportConfiguration.FirstOrgAcceptedDomainTable, this.Configuration.TransportConfiguration.RemoteDomainTable, this.Configuration.TransportConfiguration.Version, out mexRuntimeSession);
			this.MexRuntimeSession = mexRuntimeSession;
			this.ProtocolLogSession = this.ServerState.ProtocolLog.OpenSession(this.ReceiveConnector.Id.ToString(), this.sessionId, networkConnection.RemoteEndPoint, networkConnection.LocalEndPoint, this.ReceiveConnector.ProtocolLoggingLevel);
			this.ProtocolLogSession.LogConnect();
			bool isFrontEndTransportProcess = this.Configuration.TransportConfiguration.ProcessTransportRole == ProcessTransportRole.FrontEnd;
			SmtpInSessionUtils.ApplyRoleBasedEhloOptionsOverrides(this.AdvertisedEhloOptions, isFrontEndTransportProcess);
			this.supportIntegratedAuth = SmtpInSessionUtils.ShouldSupportIntegratedAuthentication(true, isFrontEndTransportProcess);
			this.LoadCertificates();
			this.SetInitialIdentity();
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x000F8EC8 File Offset: 0x000F70C8
		public static SmtpInSessionState FromSmtpInSession(ISmtpInSession session)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			return new SmtpInSessionState(session);
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000F8EDB File Offset: 0x000F70DB
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x000F8EE3 File Offset: 0x000F70E3
		public IEhloOptions AdvertisedEhloOptions { get; set; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000F8EEC File Offset: 0x000F70EC
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x000F8EF4 File Offset: 0x000F70F4
		public IX509Certificate2 AdvertisedTlsCertificate { get; private set; }

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000F8EFD File Offset: 0x000F70FD
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000F8F05 File Offset: 0x000F7105
		public TransportMiniRecipient AuthenticatedUser { get; private set; }

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000F8F0E File Offset: 0x000F710E
		public override AuthenticationSource AuthenticationSource
		{
			get
			{
				return this.authenticationSourceForAgents;
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000F8F16 File Offset: 0x000F7116
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000F8F1E File Offset: 0x000F711E
		public MultilevelAuthMechanism AuthMethod { get; private set; }

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000F8F27 File Offset: 0x000F7127
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000F8F2F File Offset: 0x000F712F
		public IAuthzAuthorization AuthzAuthorization { get; private set; }

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000F8F38 File Offset: 0x000F7138
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000F8F40 File Offset: 0x000F7140
		public BdatState BdatState { get; private set; }

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000F8F49 File Offset: 0x000F7149
		// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x000F8F51 File Offset: 0x000F7151
		public ICertificateValidator CertificateValidator { get; private set; }

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000F8F5A File Offset: 0x000F715A
		// (set) Token: 0x06003BA3 RID: 15267 RVA: 0x000F8F62 File Offset: 0x000F7162
		public ISmtpReceiveConfiguration Configuration { get; private set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003BA4 RID: 15268 RVA: 0x000F8F6B File Offset: 0x000F716B
		// (set) Token: 0x06003BA5 RID: 15269 RVA: 0x000F8F73 File Offset: 0x000F7173
		public DelayedAckStatus DelayedAckStatus { get; set; }

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000F8F7C File Offset: 0x000F717C
		// (set) Token: 0x06003BA7 RID: 15271 RVA: 0x000F8F84 File Offset: 0x000F7184
		internal override DisconnectReason DisconnectReason { get; set; }

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003BA8 RID: 15272 RVA: 0x000F8F8D File Offset: 0x000F718D
		// (set) Token: 0x06003BA9 RID: 15273 RVA: 0x000F8F95 File Offset: 0x000F7195
		public IExEventLog EventLog { get; private set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000F8F9E File Offset: 0x000F719E
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000F8FA6 File Offset: 0x000F71A6
		public IEventNotificationItem EventNotificationItem { get; private set; }

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000F8FAF File Offset: 0x000F71AF
		// (set) Token: 0x06003BAD RID: 15277 RVA: 0x000F8FB7 File Offset: 0x000F71B7
		public Queue<IInboundMessageContextBlob> ExpectedMessageContextBlobs { get; private set; }

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000F8FC0 File Offset: 0x000F71C0
		// (set) Token: 0x06003BAF RID: 15279 RVA: 0x000F8FC8 File Offset: 0x000F71C8
		public ExtendedProtectionConfig ExtendedProtectionConfig { get; private set; }

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000F8FD1 File Offset: 0x000F71D1
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000F8FD9 File Offset: 0x000F71D9
		public bool FirstBdatCall { get; set; }

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x000F8FE2 File Offset: 0x000F71E2
		// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000F8FEA File Offset: 0x000F71EA
		internal override bool RequestClientTlsCertificate { get; set; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000F8FF3 File Offset: 0x000F71F3
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x000F8FFB File Offset: 0x000F71FB
		public IX509Certificate2 InternalTransportCertificate { get; private set; }

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000F9004 File Offset: 0x000F7204
		internal override bool DiscardingMessage
		{
			get
			{
				return this.isDiscardingMessage;
			}
		}

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000F900C File Offset: 0x000F720C
		// (set) Token: 0x06003BB8 RID: 15288 RVA: 0x000F9014 File Offset: 0x000F7214
		public override bool IsExternalConnection { get; internal set; }

		// Token: 0x17001247 RID: 4679
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000F9032 File Offset: 0x000F7232
		public object LastNetworkError
		{
			set
			{
				ArgumentValidator.ThrowIfInvalidValue<object>("value", value, (object v) => v is SocketError || v is SecurityStatus);
				this.lastNetworkError = value;
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000F9063 File Offset: 0x000F7263
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000F907F File Offset: 0x000F727F
		public SocketError LastSocketError
		{
			get
			{
				if (this.lastNetworkError is SocketError)
				{
					return (SocketError)this.lastNetworkError;
				}
				return SocketError.Success;
			}
			set
			{
				this.lastNetworkError = value;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000F908D File Offset: 0x000F728D
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000F90A9 File Offset: 0x000F72A9
		public SecurityStatus LastTlsError
		{
			get
			{
				if (this.lastNetworkError is SecurityStatus)
				{
					return (SecurityStatus)this.lastNetworkError;
				}
				return SecurityStatus.OK;
			}
			set
			{
				this.lastNetworkError = value;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000F90B7 File Offset: 0x000F72B7
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000F90BF File Offset: 0x000F72BF
		public MailCommandMessageContextParameters MailCommandMessageContextInformation { get; set; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000F90C8 File Offset: 0x000F72C8
		// (set) Token: 0x06003BC1 RID: 15297 RVA: 0x000F90D0 File Offset: 0x000F72D0
		public ISmtpMessageContextBlob MessageContextBlob { get; private set; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000F90D9 File Offset: 0x000F72D9
		// (set) Token: 0x06003BC3 RID: 15299 RVA: 0x000F90E1 File Offset: 0x000F72E1
		public int NumberOfMessagesReceived { get; private set; }

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000F90EA File Offset: 0x000F72EA
		// (set) Token: 0x06003BC5 RID: 15301 RVA: 0x000F90F2 File Offset: 0x000F72F2
		public int NumberOfMessagesSubmitted { get; private set; }

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003BC6 RID: 15302 RVA: 0x000F90FB File Offset: 0x000F72FB
		// (set) Token: 0x06003BC7 RID: 15303 RVA: 0x000F9103 File Offset: 0x000F7303
		public IMessageThrottlingManager MessageThrottlingManager { get; private set; }

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x000F910C File Offset: 0x000F730C
		// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x000F9114 File Offset: 0x000F7314
		public MsgTrackReceiveInfo MessageTrackReceiveInfo { get; set; }

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x000F911D File Offset: 0x000F731D
		// (set) Token: 0x06003BCB RID: 15307 RVA: 0x000F9125 File Offset: 0x000F7325
		public Stream MessageWriteStream { get; private set; }

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x000F912E File Offset: 0x000F732E
		// (set) Token: 0x06003BCD RID: 15309 RVA: 0x000F9136 File Offset: 0x000F7336
		public IMExSession MexRuntimeSession { get; set; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x000F913F File Offset: 0x000F733F
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x000F9147 File Offset: 0x000F7347
		public INetworkConnection NetworkConnection { get; private set; }

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x000F9150 File Offset: 0x000F7350
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x000F9158 File Offset: 0x000F7358
		public int NumLogonFailures { get; private set; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x000F9161 File Offset: 0x000F7361
		// (set) Token: 0x06003BD3 RID: 15315 RVA: 0x000F9169 File Offset: 0x000F7369
		public string PeerSessionPrimaryServer { get; set; }

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003BD4 RID: 15316 RVA: 0x000F9172 File Offset: 0x000F7372
		// (set) Token: 0x06003BD5 RID: 15317 RVA: 0x000F917A File Offset: 0x000F737A
		public IProtocolLogSession ProtocolLogSession { get; private set; }

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003BD6 RID: 15318 RVA: 0x000F9183 File Offset: 0x000F7383
		// (set) Token: 0x06003BD7 RID: 15319 RVA: 0x000F918B File Offset: 0x000F738B
		public ReceiveConnector ReceiveConnector { get; private set; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x000F9194 File Offset: 0x000F7394
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x000F919C File Offset: 0x000F739C
		public SmtpReceiveConnectorStub ReceiveConnectorStub { get; private set; }

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x000F91A5 File Offset: 0x000F73A5
		// (set) Token: 0x06003BDB RID: 15323 RVA: 0x000F91AD File Offset: 0x000F73AD
		public InboundRecipientCorrelator RecipientCorrelator { get; private set; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x000F91B6 File Offset: 0x000F73B6
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x000F91BE File Offset: 0x000F73BE
		public WindowsIdentity RemoteWindowsIdentity { get; private set; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x000F91C7 File Offset: 0x000F73C7
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x000F91CF File Offset: 0x000F73CF
		public SecurityIdentifier RemoteIdentity { get; private set; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x000F91D8 File Offset: 0x000F73D8
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x000F91E0 File Offset: 0x000F73E0
		public string RemoteIdentityName { get; private set; }

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x000F91E9 File Offset: 0x000F73E9
		// (set) Token: 0x06003BE3 RID: 15331 RVA: 0x000F91F1 File Offset: 0x000F73F1
		public SecureState SecureState { get; set; }

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x000F91FA File Offset: 0x000F73FA
		// (set) Token: 0x06003BE5 RID: 15333 RVA: 0x000F9202 File Offset: 0x000F7402
		public bool SendAsRequiredADLookup { get; set; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000F920B File Offset: 0x000F740B
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000F9213 File Offset: 0x000F7413
		public string SenderShadowContext { get; set; }

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003BE8 RID: 15336 RVA: 0x000F921C File Offset: 0x000F741C
		// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x000F9224 File Offset: 0x000F7424
		public SmtpInServerState ServerState { get; private set; }

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003BEA RID: 15338 RVA: 0x000F922D File Offset: 0x000F742D
		// (set) Token: 0x06003BEB RID: 15339 RVA: 0x000F9235 File Offset: 0x000F7435
		public Permission SessionPermissions
		{
			get
			{
				return this.sessionPermissions;
			}
			private set
			{
				this.sessionPermissions = value;
				this.TraceAndLogSessionPermissions();
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003BEC RID: 15340 RVA: 0x000F9244 File Offset: 0x000F7444
		// (set) Token: 0x06003BED RID: 15341 RVA: 0x000F924C File Offset: 0x000F744C
		public DateTime SessionStartTime { get; private set; }

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003BEE RID: 15342 RVA: 0x000F9255 File Offset: 0x000F7455
		// (set) Token: 0x06003BEF RID: 15343 RVA: 0x000F925D File Offset: 0x000F745D
		public ISmtpAgentSession SmtpAgentSession { get; private set; }

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000F9266 File Offset: 0x000F7466
		// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x000F926E File Offset: 0x000F746E
		public bool SmtpUtf8Supported { get; set; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000F9277 File Offset: 0x000F7477
		// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x000F927F File Offset: 0x000F747F
		public SmtpReceiveCapabilities? TlsDomainCapabilities { get; set; }

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003BF4 RID: 15348 RVA: 0x000F9288 File Offset: 0x000F7488
		// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x000F9290 File Offset: 0x000F7490
		public IX509Certificate2 TlsRemoteCertificateInternal { get; set; }

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000F9299 File Offset: 0x000F7499
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x000F92A1 File Offset: 0x000F74A1
		public ITracer Tracer { get; private set; }

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x000F92AA File Offset: 0x000F74AA
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x000F92B2 File Offset: 0x000F74B2
		public TransportMailItem TransportMailItem { get; private set; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003BFA RID: 15354 RVA: 0x000F92BB File Offset: 0x000F74BB
		// (set) Token: 0x06003BFB RID: 15355 RVA: 0x000F92C3 File Offset: 0x000F74C3
		public TransportMailItemWrapper TransportMailItemWrapper { get; private set; }

		// Token: 0x06003BFC RID: 15356 RVA: 0x000F92CC File Offset: 0x000F74CC
		public virtual void OnDisconnect()
		{
			this.ProtocolLogSession.LogDisconnect(this.DisconnectReason);
			this.SmtpAgentSession.Close();
			this.AbortMailTransaction();
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x000F9305 File Offset: 0x000F7505
		public void HandleNetworkError(object errorCode)
		{
			ArgumentValidator.ThrowIfInvalidValue<object>("errorCode", errorCode, (object v) => v is SocketError || v is SecurityStatus);
			this.LastNetworkError = errorCode;
			this.DisconnectReason = Util.DisconnectReasonFromError(errorCode);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000F9344 File Offset: 0x000F7544
		public void AddSessionPermissions(SmtpReceiveCapabilities capabilities)
		{
			this.SessionPermissions = Util.AddSessionPermissions(capabilities, this.SessionPermissions, this.AuthzAuthorization, this.ReceiveConnectorStub.SecurityDescriptor, this.ProtocolLogSession, this.Tracer, this.GetHashCode());
			if (SmtpInSessionUtils.HasAcceptCrossForestMailCapability(capabilities))
			{
				this.RemoteIdentity = WellKnownSids.ExternallySecuredServers;
				this.RemoteIdentityName = "accepted_domain";
			}
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000F93A4 File Offset: 0x000F75A4
		public virtual bool TryCalculateTlsDomainCapabilitiesFromRemoteTlsCertificate(out SmtpReceiveCapabilities capabilities)
		{
			if (this.TlsDomainCapabilities != null)
			{
				capabilities = this.TlsDomainCapabilities.Value;
				return true;
			}
			return Util.TryDetermineTlsDomainCapabilities(this.CertificateValidator, this.TlsRemoteCertificateInternal, this.TlsRemoteCertificateChainValidationStatus, this.ReceiveConnectorStub, this.ProtocolLogSession, this.EventLog, this.Tracer, out capabilities);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000F9403 File Offset: 0x000F7603
		public virtual IThrottlingPolicy GetThrottlingPolicy()
		{
			if (this.AuthenticatedUser == null)
			{
				return null;
			}
			return ThrottlingPolicyCache.Singleton.Get(this.AuthenticatedUser.OrganizationId, this.AuthenticatedUser.ThrottlingPolicy);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000F942F File Offset: 0x000F762F
		public virtual bool IsValidMessagePriority(out SmtpResponse failureResponse)
		{
			if (this.TransportMailItem == null)
			{
				failureResponse = SmtpResponse.Empty;
				return false;
			}
			if (this.TransportMailItem.ValidateDeliveryPriority(out failureResponse))
			{
				return true;
			}
			this.StartDiscardingMessage();
			return false;
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x000F945D File Offset: 0x000F765D
		internal override void GrantMailItemPermissions(Permission permissions)
		{
			this.MailItemPermissionsGranted |= permissions;
			this.MailItemPermissionsDenied &= ~permissions;
			this.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permissions)), "Granted Mail Item Permissions");
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x000F9498 File Offset: 0x000F7698
		public void IncrementNumLogonFailures()
		{
			this.NumLogonFailures++;
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x000F94A8 File Offset: 0x000F76A8
		public void DenyMailItemPermissions(Permission permissions)
		{
			this.MailItemPermissionsGranted &= ~permissions;
			this.MailItemPermissionsDenied |= permissions;
			this.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(permissions)), "Denied Mail Item Permissions");
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x000F94E3 File Offset: 0x000F76E3
		public virtual bool IsMessagePoison(string messageId)
		{
			return PoisonMessage.IsMessagePoison(messageId);
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x000F94EB File Offset: 0x000F76EB
		public void ResetMailItemPermissions()
		{
			this.MailItemPermissionsGranted = Permission.None;
			this.MailItemPermissionsDenied = Permission.None;
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x000F94FB File Offset: 0x000F76FB
		public void ResetExpectedBlobs()
		{
			this.ExpectedMessageContextBlobs.Clear();
			this.MailCommandMessageContextInformation = null;
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x000F950F File Offset: 0x000F770F
		public void CloseMessageWriteStream()
		{
			Util.CloseMessageWriteStream(this.MessageWriteStream, this.TransportMailItem, this.Tracer, this.GetHashCode());
			this.MessageWriteStream = null;
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x000F9538 File Offset: 0x000F7738
		public virtual void AbortMailTransaction()
		{
			if (this.TransportMailItem != null)
			{
				if (this.TransportMailItem.IsActive && !this.TransportMailItem.IsNew)
				{
					this.TransportMailItem.ReleaseFromActiveMaterializedLazy();
				}
				this.ReleaseMailItem();
			}
			this.isDiscardingMessage = false;
			this.SmtpResponse = SmtpResponse.Empty;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x000F958C File Offset: 0x000F778C
		public virtual void ReleaseMailItem()
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
				this.TransportMailItem.ReleaseFromActive();
				this.ResetMailItemPermissions();
				this.MessageWriteStream = null;
				this.TransportMailItem = null;
				this.RecipientCorrelator = null;
				this.BdatState = null;
				this.isDiscardingMessage = false;
				this.SmtpResponse = SmtpResponse.Empty;
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x000F961C File Offset: 0x000F781C
		public void SetupExpectedBlobs(MailCommandMessageContextParameters messageContextParameters)
		{
			if (messageContextParameters == null)
			{
				this.Tracer.TraceDebug((long)this.GetHashCode(), "SmtpInSession(id={0}) Not Messagecontext is specified");
				return;
			}
			this.ExpectedMessageContextBlobs = new Queue<IInboundMessageContextBlob>(messageContextParameters.OrderedListOfBlobs.Count);
			foreach (IInboundMessageContextBlob item in messageContextParameters.OrderedListOfBlobs)
			{
				this.ExpectedMessageContextBlobs.Enqueue(item);
			}
			this.MailCommandMessageContextInformation = messageContextParameters;
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x000F96AC File Offset: 0x000F78AC
		public SmtpResponse CreateTransportMailItem(MailParseOutput parseOutput, MailCommandEventArgs agentEventArgs)
		{
			ArgumentValidator.ThrowIfNull("parseOutput", parseOutput);
			SmtpResponse result;
			try
			{
				ADRecipientCache<TransportMiniRecipient> recipientCache = null;
				Guid empty = Guid.Empty;
				MailDirectionality directionality = MailDirectionality.Undefined;
				if (this.AuthenticatedUser != null && this.AuthenticatedUser.PrimarySmtpAddress.IsValidAddress)
				{
					ADOperationResult adOperationResult = this.CreateRecipientCache(out recipientCache);
					result = this.HandleCacheCreationResponse(recipientCache, adOperationResult, this.Configuration.TransportConfiguration.RejectUnscopedMessages, out directionality, out empty);
					if (!result.IsEmpty)
					{
						return result;
					}
				}
				TransportMailItem transportMailItem = this.CreateAndInitializeTransportMailItem(parseOutput, recipientCache, directionality, empty);
				this.HandleShadowMessageChecks(transportMailItem);
				if (!this.IsExternalConnection)
				{
					this.LastExternalIPAddress = null;
				}
				result = this.TryUpdateRecipientCacheForAttributionData(transportMailItem, parseOutput);
				if (!result.IsEmpty)
				{
					return result;
				}
				this.InitializeMessageTrackingInfo();
				this.RecipientCorrelator = new InboundRecipientCorrelator();
				this.TransferMailCommandProperties(transportMailItem, parseOutput, agentEventArgs);
				this.TransportMailItem = transportMailItem;
				LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceive, this.TransportMailItem.LatencyTracker);
				if (!Util.IsFrontEndRole(this.Configuration.TransportConfiguration.ProcessTransportRole))
				{
					LatencyComponent component = (this.IsInboundProxiedSession || this.IsClientProxiedSession) ? LatencyComponent.SmtpReceiveDataExternal : LatencyComponent.SmtpReceiveDataInternal;
					LatencyTracker.BeginTrackLatency(component, this.TransportMailItem.LatencyTracker);
				}
				this.TransportMailItemWrapper = new TransportMailItemWrapper(this.TransportMailItem, this.MexRuntimeSession, true);
			}
			catch (IOException)
			{
				this.UpdateAvailabilityPerfCounters(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.TransportMailItem = null;
				this.MessageTrackReceiveInfo = null;
				this.RecipientCorrelator = null;
				result = SmtpResponse.DataTransactionFailed;
			}
			this.IncrementMessageCount();
			return result;
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x000F9840 File Offset: 0x000F7A40
		public virtual Stream OpenMessageWriteStream(bool expectBinaryContent)
		{
			if (this.TransportMailItem == null)
			{
				throw new InvalidOperationException("No transport message");
			}
			MimeLimits mimeLimits = SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(this.CombinedPermissions) ? MimeLimits.Unlimited : MimeLimits.Default;
			this.MessageWriteStream = this.TransportMailItem.OpenMimeWriteStream(mimeLimits, expectBinaryContent);
			return this.MessageWriteStream;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x000F9893 File Offset: 0x000F7A93
		public void UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory category)
		{
			if (this.ReceiveConnectorStub.SmtpAvailabilityPerfCounters != null)
			{
				this.ReceiveConnectorStub.SmtpAvailabilityPerfCounters.UpdatePerformanceCounters(category);
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x000F98B3 File Offset: 0x000F7AB3
		public void StartDiscardingMessage()
		{
			if (this.isDiscardingMessage)
			{
				return;
			}
			this.isDiscardingMessage = true;
			if (this.TransportMailItem != null && this.TransportMailItem.MimeDocument != null)
			{
				this.TransportMailItem.MimeDocument.EndOfHeaders = null;
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x000F98EC File Offset: 0x000F7AEC
		public void PutBackReceivedBytes(int bytesUnconsumed)
		{
			ISmtpReceivePerfCounters smtpReceivePerfCounterInstance = this.ReceiveConnectorStub.SmtpReceivePerfCounterInstance;
			if (smtpReceivePerfCounterInstance != null)
			{
				smtpReceivePerfCounterInstance.TotalBytesReceived.IncrementBy((long)(-(long)bytesUnconsumed));
			}
			this.NetworkConnection.PutBackReceivedBytes(bytesUnconsumed);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x000F9924 File Offset: 0x000F7B24
		public SmtpResponse TrackAndEnqueueMailItem()
		{
			this.UpdateSmtpReceivePerfCountersForMessageReceived(this.TransportMailItem.Recipients.Count, this.TransportMailItem.MimeSize);
			if (this.TransportMailItem.AuthMethod == MultilevelAuthMechanism.MutualTLS)
			{
				Utils.SecureMailPerfCounters.DomainSecureMessagesReceivedTotal.Increment();
			}
			if (!string.IsNullOrEmpty(this.TransportMailItem.MessageTrackingSecurityInfo))
			{
				this.MessageTrackReceiveInfo = new MsgTrackReceiveInfo(this.MessageTrackReceiveInfo.ClientIPAddress, this.MessageTrackReceiveInfo.ClientHostname, this.MessageTrackReceiveInfo.ServerIPAddress, this.MessageTrackReceiveInfo.SourceContext, this.MessageTrackReceiveInfo.ConnectorId, this.MessageTrackReceiveInfo.RelatedMailItemId, this.TransportMailItem.MessageTrackingSecurityInfo, string.Empty, string.Empty, this.MessageTrackReceiveInfo.ProxiedClientIPAddress, this.MessageTrackReceiveInfo.ProxiedClientHostname, this.TransportMailItem.RootPart.Headers.FindAll(HeaderId.Received), (this.AuthenticatedUser != null) ? this.AuthenticatedUser.ExchangeGuid : Guid.Empty);
			}
			if (!Util.IsMailboxTransportRole(this.Configuration.TransportConfiguration.ProcessTransportRole))
			{
				MessageTrackingLog.TrackReceive(MessageTrackingSource.SMTP, this.TransportMailItem, this.MessageTrackReceiveInfo);
			}
			if (this.Configuration.TransportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery)
			{
				this.TransportMailItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.MailboxTransport.SmtpInClientHostname", this.MessageTrackReceiveInfo.ClientHostname);
			}
			LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceive, this.TransportMailItem.LatencyTracker);
			this.NumberOfMessagesSubmitted++;
			this.TransportMailItem.PerfCounterAttribution = "InQueue";
			return this.ServerState.Categorizer.EnqueueSubmittedMessage(this.TransportMailItem);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x000F9AD0 File Offset: 0x000F7CD0
		public bool SetupMessageStream(bool allowBinaryContent, out Stream bodyStream)
		{
			bool result;
			try
			{
				bodyStream = this.OpenMessageWriteStream(allowBinaryContent);
				result = true;
			}
			catch (IOException arg)
			{
				this.Tracer.TraceError<IOException>((long)this.GetHashCode(), "OpenMessageWriteStream failed: {0}", arg);
				this.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				bodyStream = null;
				this.StartDiscardingMessage();
				this.AbortMailTransaction();
				result = false;
			}
			return result;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x000F9B30 File Offset: 0x000F7D30
		public bool InitializeBdatState(ISmtpInStreamBuilder streamBuilder, long chunkSize, long messageSizeLimit)
		{
			ArgumentValidator.ThrowIfNull("streamBuilder", streamBuilder);
			if (this.BdatState != null)
			{
				streamBuilder.BodyStream = this.BdatState.BdatStream;
				this.BdatState.IncrementAccumulatedChunkSize(chunkSize);
				return true;
			}
			Stream stream;
			if (!this.SetupMessageStream(true, out stream))
			{
				this.StartDiscardingMessage();
				streamBuilder.IsDiscardingData = true;
				return false;
			}
			streamBuilder.BodyStream = stream;
			this.BdatState = new BdatState(this.TransportMailItem.InternetMessageId, stream, chunkSize, 0L, messageSizeLimit, false);
			return true;
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003C14 RID: 15380 RVA: 0x000F9BC0 File Offset: 0x000F7DC0
		public string AdvertisedDomain
		{
			get
			{
				if (string.IsNullOrEmpty(this.advertisedDomain))
				{
					this.advertisedDomain = Util.AdvertisedDomainFromReceiveConnector(this.ReceiveConnector, () => this.Configuration.TransportConfiguration.PhysicalMachineName);
				}
				return this.advertisedDomain;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000F9C04 File Offset: 0x000F7E04
		public Permission AnonymousPermissions
		{
			get
			{
				if (this.anonymousPermissions == null)
				{
					this.anonymousPermissions = new Permission?(Util.GetPermissionsForSid(SmtpConstants.AnonymousSecurityIdentifier, this.ReceiveConnector.GetSecurityDescriptor(), this.AuthzAuthorization, "anonymous", this.ReceiveConnector.Name, this.Tracer));
				}
				return this.anonymousPermissions.Value;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003C16 RID: 15382 RVA: 0x000F9C65 File Offset: 0x000F7E65
		// (set) Token: 0x06003C17 RID: 15383 RVA: 0x000F9C6D File Offset: 0x000F7E6D
		internal override SmtpResponse Banner { get; set; }

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003C18 RID: 15384 RVA: 0x000F9C76 File Offset: 0x000F7E76
		public SmtpReceiveCapabilities Capabilities
		{
			get
			{
				return Util.SessionCapabilitiesFromTlsAndNonTlsCapabilities(this.SecureState, this.ReceiveConnectorStub.NoTlsCapabilities, this.TlsDomainCapabilities);
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000F9C94 File Offset: 0x000F7E94
		public Permission CombinedPermissions
		{
			get
			{
				Permission permission = this.SessionPermissions;
				if (this.Configuration.TransportConfiguration.GrantExchangeServerPermissions)
				{
					permission |= (Permission.SMTPSubmit | Permission.SMTPAcceptAnyRecipient | Permission.SMTPAcceptAuthenticationFlag | Permission.SMTPAcceptAnySender | Permission.SMTPAcceptAuthoritativeDomainSender | Permission.BypassAntiSpam | Permission.BypassMessageSizeLimit | Permission.SMTPAcceptEXCH50 | Permission.AcceptRoutingHeaders | Permission.AcceptForestHeaders | Permission.AcceptOrganizationHeaders | Permission.SMTPAcceptXShadow | Permission.SMTPAcceptXProxyFrom | Permission.SMTPAcceptXSessionParams | Permission.SMTPAcceptXMessageContextADRecipientCache | Permission.SMTPAcceptXMessageContextExtendedProperties | Permission.SMTPAcceptXMessageContextFastIndex | Permission.SMTPAcceptXAttr | Permission.SMTPAcceptXSysProbe);
				}
				return (permission | this.MailItemPermissionsGranted) & ~this.MailItemPermissionsDenied;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000F9CD2 File Offset: 0x000F7ED2
		internal override string CurrentMessageTemporaryId
		{
			get
			{
				return SmtpInSessionUtils.GetFormattedTemporaryMessageId(this.sessionId, this.SessionStartTime, this.NumberOfMessagesReceived);
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000F9CEB File Offset: 0x000F7EEB
		// (set) Token: 0x06003C1C RID: 15388 RVA: 0x000F9CF3 File Offset: 0x000F7EF3
		public override string HelloDomain { get; internal set; }

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000F9CFC File Offset: 0x000F7EFC
		public bool IsAnonymousTlsSupported
		{
			get
			{
				return this.InternalTransportCertificate != null && !this.ReceiveConnector.SuppressXAnonymousTls;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000F9D16 File Offset: 0x000F7F16
		public bool IsIntegratedAuthSupported
		{
			get
			{
				return this.supportIntegratedAuth && (this.ReceiveConnector.AuthMechanism & AuthMechanisms.Integrated) != AuthMechanisms.None;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000F9D35 File Offset: 0x000F7F35
		public bool IsMaxLogonFailuresExceeded
		{
			get
			{
				return this.ReceiveConnector.MaxLogonFailures > 0 && this.NumLogonFailures > this.ReceiveConnector.MaxLogonFailures;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000F9D5A File Offset: 0x000F7F5A
		public bool IsSecureSession
		{
			get
			{
				return this.SecureState == SecureState.AnonymousTls || this.SecureState == SecureState.StartTls;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000F9D70 File Offset: 0x000F7F70
		public bool IsStartTlsSupported
		{
			get
			{
				return this.AdvertisedTlsCertificate != null && !this.startTlsDisabled;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000F9D88 File Offset: 0x000F7F88
		public Permission PartnerPermissions
		{
			get
			{
				if (this.partnerPermissions == null)
				{
					this.partnerPermissions = new Permission?(Util.GetPermissionsForSid(WellKnownSids.PartnerServers, this.ReceiveConnector.GetSecurityDescriptor(), this.AuthzAuthorization, "partner", this.ReceiveConnector.Name, this.Tracer));
				}
				return this.partnerPermissions.Value;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003C23 RID: 15395 RVA: 0x000F9DE9 File Offset: 0x000F7FE9
		public ISmtpReceivePerfCounters ReceivePerfCounters
		{
			get
			{
				return this.ReceiveConnectorStub.SmtpReceivePerfCounterInstance;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003C24 RID: 15396 RVA: 0x000F9DF6 File Offset: 0x000F7FF6
		public override long SessionId
		{
			get
			{
				return (long)this.sessionId;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x000F9DFE File Offset: 0x000F7FFE
		// (set) Token: 0x06003C26 RID: 15398 RVA: 0x000F9E06 File Offset: 0x000F8006
		internal override bool DisableStartTls
		{
			get
			{
				return this.startTlsDisabled;
			}
			set
			{
				if (value && this.SecureState == SecureState.StartTls)
				{
					throw new InvalidOperationException("Cannnot disable STARTTLS after the command has already been received");
				}
				this.startTlsDisabled = value;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003C27 RID: 15399 RVA: 0x000F9E28 File Offset: 0x000F8028
		public ChainValidityStatus TlsRemoteCertificateChainValidationStatus
		{
			get
			{
				if (this.SecureState == SecureState.None)
				{
					return ChainValidityStatus.Valid;
				}
				if (this.tlsRemoteCertificateChainValidationStatus == null)
				{
					this.tlsRemoteCertificateChainValidationStatus = new ChainValidityStatus?(Util.CalculateTlsRemoteCertificateChainValidationStatus(this.Configuration.TransportConfiguration.ClientCertificateChainValidationEnabled, this.CertificateValidator, this.TlsRemoteCertificateInternal, this.ProtocolLogSession, this.EventLog));
				}
				return this.tlsRemoteCertificateChainValidationStatus.Value;
			}
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x000F9E8F File Offset: 0x000F808F
		public void UpdateAvailabilityPerfCounters(LegitimateSmtpAvailabilityCategory category)
		{
			if (this.ReceiveConnectorStub.SmtpAvailabilityPerfCounters != null)
			{
				this.ReceiveConnectorStub.SmtpAvailabilityPerfCounters.UpdatePerformanceCounters(category);
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003C29 RID: 15401 RVA: 0x000F9EAF File Offset: 0x000F80AF
		public virtual DateTime UtcNow
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003C2A RID: 15402 RVA: 0x000F9EB6 File Offset: 0x000F80B6
		// (set) Token: 0x06003C2B RID: 15403 RVA: 0x000F9EBE File Offset: 0x000F80BE
		private Permission MailItemPermissionsGranted { get; set; }

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003C2C RID: 15404 RVA: 0x000F9EC7 File Offset: 0x000F80C7
		// (set) Token: 0x06003C2D RID: 15405 RVA: 0x000F9ECF File Offset: 0x000F80CF
		private Permission MailItemPermissionsDenied { get; set; }

		// Token: 0x06003C2E RID: 15406 RVA: 0x000F9ED8 File Offset: 0x000F80D8
		protected DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpInSessionState>(this);
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000F9EE0 File Offset: 0x000F80E0
		protected void InternalDispose(bool disposingNotFinalizing)
		{
			if (this.isInvokedFromLegacyStack)
			{
				return;
			}
			if (disposingNotFinalizing)
			{
				if (this.RemoteWindowsIdentity != null)
				{
					this.RemoteWindowsIdentity.Dispose();
					this.RemoteWindowsIdentity = null;
				}
				if (this.NetworkConnection != null)
				{
					this.NetworkConnection.Dispose();
					this.NetworkConnection = null;
				}
			}
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x000F9F2D File Offset: 0x000F812D
		protected void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.InternalDispose(disposing);
				this.disposed = true;
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000F9F62 File Offset: 0x000F8162
		protected void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x000F9FA8 File Offset: 0x000F81A8
		protected virtual ADOperationResult CreateRecipientCache(out ADRecipientCache<TransportMiniRecipient> recipientCache)
		{
			ADRecipientCache<TransportMiniRecipient> adRecipientCache = null;
			ADOperationResult result = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				adRecipientCache = new ADRecipientCache<TransportMiniRecipient>(TransportMiniRecipientSchema.Properties, 1, this.AuthenticatedUser.OrganizationId);
			}, 0);
			recipientCache = adRecipientCache;
			return result;
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x000F9FE8 File Offset: 0x000F81E8
		protected virtual SmtpResponse HandleCacheCreationResponse(ADRecipientCache<TransportMiniRecipient> recipientCache, ADOperationResult adOperationResult, bool rejectUnscopedMessages, out MailDirectionality directionality, out Guid externalOrgId)
		{
			directionality = MailDirectionality.Undefined;
			externalOrgId = Guid.Empty;
			if (adOperationResult.Succeeded)
			{
				this.AddRecipientCacheEntry(recipientCache);
				directionality = MailDirectionality.Originating;
				adOperationResult = MultiTenantTransport.TryGetExternalOrgId(this.AuthenticatedUser.OrganizationId, out externalOrgId);
			}
			switch (adOperationResult.ErrorCode)
			{
			case ADOperationErrorCode.RetryableError:
				MultiTenantTransport.TraceAttributionError("Retriable Error {0} attributing authUserRecipient {1}", new object[]
				{
					adOperationResult.Exception,
					this.AuthenticatedUser.PrimarySmtpAddress
				});
				return SmtpResponse.HubAttributionTransientFailureInCreateTmi;
			case ADOperationErrorCode.PermanentError:
				if (rejectUnscopedMessages)
				{
					MultiTenantTransport.TraceAttributionError("Permanent Error {0} attributing authUserRecipient {1}", new object[]
					{
						adOperationResult.Exception,
						this.AuthenticatedUser.PrimarySmtpAddress
					});
					return SmtpResponse.HubAttributionFailureInCreateTmi;
				}
				MultiTenantTransport.TraceAttributionError("Permanent Error {0} attributing authUserRecipient {1}. Falling back to safe tenant", new object[]
				{
					adOperationResult.Exception,
					this.AuthenticatedUser.PrimarySmtpAddress
				});
				externalOrgId = MultiTenantTransport.SafeTenantId;
				break;
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000FA0F2 File Offset: 0x000F82F2
		protected virtual TransportMailItem NewTransportMailItem(ADRecipientCache<TransportMiniRecipient> recipientCache, MailDirectionality directionality, Guid externalOrgId)
		{
			return TransportMailItem.NewMailItem(recipientCache, LatencyComponent.SmtpReceive, directionality, externalOrgId);
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000FA100 File Offset: 0x000F8300
		protected virtual void InitializeMessageTrackingInfo()
		{
			if (this.MessageTrackReceiveInfo == null)
			{
				this.MessageTrackReceiveInfo = new MsgTrackReceiveInfo(this.NetworkConnection.RemoteEndPoint.Address, this.HelloDomain, this.NetworkConnection.LocalEndPoint.Address, this.CurrentMessageTemporaryId, this.ReceiveConnector.Id.ToString(), null, null, string.Empty, (this.AuthenticatedUser != null) ? this.AuthenticatedUser.ExchangeGuid : Guid.Empty);
			}
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x000FA188 File Offset: 0x000F8388
		protected virtual SmtpResponse TryUpdateRecipientCacheForAttributionData(TransportMailItem transportMailItem, MailParseOutput parseOutput)
		{
			SmtpResponse empty = SmtpResponse.Empty;
			if (!this.IsAttributionDataSpecified(parseOutput))
			{
				return empty;
			}
			OrganizationId mailCommandInternalOrganizationId = (parseOutput.XAttrOrgId != null) ? parseOutput.XAttrOrgId.InternalOrgId : null;
			ADOperationResult adoperationResult = SmtpInSessionUtils.TryCreateOrUpdateADRecipientCache(transportMailItem, mailCommandInternalOrganizationId, this.ProtocolLogSession);
			if (adoperationResult.ErrorCode != ADOperationErrorCode.RetryableError)
			{
				return empty;
			}
			this.RecipientCorrelator = null;
			return SmtpResponse.HubAttributionTransientFailureInMailFrom;
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000FA1E4 File Offset: 0x000F83E4
		private static EhloOptions CreateEhloOptions(INetworkConnection networkConnection, ReceiveConnector receiveConnector, string advertisedDomain)
		{
			return new EhloOptions
			{
				AdvertisedFQDN = advertisedDomain,
				AdvertisedIPAddress = networkConnection.RemoteEndPoint.Address,
				BinaryMime = receiveConnector.BinaryMimeEnabled,
				Chunking = receiveConnector.ChunkingEnabled,
				Dsn = receiveConnector.DeliveryStatusNotificationEnabled,
				EightBitMime = receiveConnector.EightBitMimeEnabled,
				EnhancedStatusCodes = receiveConnector.EnhancedStatusCodesEnabled,
				MaxSize = SmtpInSessionState.MaxSizeEhloOptionFromReceiveConnector(receiveConnector.MaxMessageSize.ToBytes()),
				Pipelining = receiveConnector.PipeliningEnabled,
				Size = receiveConnector.SizeEnabled,
				SmtpUtf8 = receiveConnector.SmtpUtf8Enabled,
				Xexch50 = false,
				XLongAddr = receiveConnector.LongAddressesEnabled,
				XOrar = receiveConnector.OrarEnabled,
				XRDst = false
			};
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000FA2AF File Offset: 0x000F84AF
		private static long MaxSizeEhloOptionFromReceiveConnector(ulong maxMessageSize)
		{
			if (maxMessageSize <= 9223372036854775807UL)
			{
				return (long)maxMessageSize;
			}
			return long.MaxValue;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000FA2C8 File Offset: 0x000F84C8
		private static ExtendedProtectionConfig CreateExtendedProtectionConfig(Microsoft.Exchange.Data.Directory.SystemConfiguration.ExtendedProtectionPolicySetting policy)
		{
			if (policy == Microsoft.Exchange.Data.Directory.SystemConfiguration.ExtendedProtectionPolicySetting.None)
			{
				return ExtendedProtectionConfig.NoExtendedProtection;
			}
			return new ExtendedProtectionConfig((int)policy, null, false);
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000FA2DC File Offset: 0x000F84DC
		private void LoadCertificates()
		{
			IX509Certificate2 internalTransportCertificate;
			Util.LoadDirectTrustCertificate(this.ReceiveConnector, this.NetworkConnection.ConnectionId, this.Configuration.TransportConfiguration.InternalTransportCertificateThumbprint, this.UtcNow, this.ServerState.CertificateCache, this.EventLog, this.Tracer, out internalTransportCertificate);
			this.InternalTransportCertificate = internalTransportCertificate;
			IX509Certificate2 advertisedTlsCertificate;
			Util.LoadStartTlsCertificate(this.ReceiveConnector, this.AdvertisedEhloOptions.AdvertisedFQDN, this.NetworkConnection.ConnectionId, this.Configuration.TransportConfiguration.OneLevelWildcardMatchForCertSelection, this.UtcNow, this.ServerState.CertificateCache, this.EventLog, this.Tracer, out advertisedTlsCertificate);
			this.AdvertisedTlsCertificate = advertisedTlsCertificate;
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000FA390 File Offset: 0x000F8590
		private void SetInitialIdentity()
		{
			this.ResetToAnonymousIdentity();
			if (this.ReceiveConnector.HasExternalAuthoritativeAuthMechanism)
			{
				this.RemoteIdentity = WellKnownSids.ExternallySecuredServers;
				this.RemoteIdentityName = "accepted_domain";
				this.SessionPermissions = SmtpInSessionUtils.GetPermissions(this.AuthzAuthorization, this.RemoteIdentity, this.ReceiveConnectorStub.SecurityDescriptor);
			}
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000FA3E8 File Offset: 0x000F85E8
		public void ResetToAnonymousIdentity()
		{
			this.AuthenticatedUser = null;
			this.AuthMethod = MultilevelAuthMechanism.None;
			this.RemoteIdentity = SmtpConstants.AnonymousSecurityIdentifier;
			this.RemoteIdentityName = "anonymous";
			this.SessionPermissions = this.AnonymousPermissions;
			if (this.RemoteWindowsIdentity != null)
			{
				this.RemoteWindowsIdentity.Dispose();
				this.RemoteWindowsIdentity = null;
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000FA440 File Offset: 0x000F8640
		public void ResetToPartnerServersIdentity(string domain)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("domain", domain);
			this.AuthenticatedUser = null;
			this.AuthMethod = MultilevelAuthMechanism.None;
			this.RemoteIdentity = WellKnownSids.PartnerServers;
			this.RemoteIdentityName = domain;
			this.SessionPermissions = this.PartnerPermissions;
			if (this.RemoteWindowsIdentity != null)
			{
				this.RemoteWindowsIdentity.Dispose();
				this.RemoteWindowsIdentity = null;
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000FA4A0 File Offset: 0x000F86A0
		public void SetAuthenticatedIdentity(TransportMiniRecipient authenticatedUser, SecurityIdentifier remoteIdentity, string remoteIdentityName, WindowsIdentity remoteWindowsIdentity, MultilevelAuthMechanism multilevelAuthMechanism, Permission permissions)
		{
			ArgumentValidator.ThrowIfNull("remoteIdentity", remoteIdentity);
			ArgumentValidator.ThrowIfNullOrEmpty("remoteIdentityName", remoteIdentityName);
			ArgumentValidator.ThrowIfNull("remoteWindowsIdentity", remoteWindowsIdentity);
			this.AuthenticatedUser = authenticatedUser;
			this.RemoteIdentity = remoteIdentity;
			this.RemoteIdentityName = remoteIdentityName;
			this.RemoteWindowsIdentity = remoteWindowsIdentity;
			this.AuthMethod = multilevelAuthMechanism;
			this.SessionPermissions = permissions;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000FA4FC File Offset: 0x000F86FC
		public void UpdateIdentityBasedOnClientTlsCertificate(IX509Certificate2 clientTlsCertificate)
		{
			ArgumentValidator.ThrowIfNull("clientTlsCertificate", clientTlsCertificate);
			this.RemoteIdentity = this.ServerState.DirectTrust.MapCertToSecurityIdentifier(clientTlsCertificate);
			if (this.RemoteIdentity == SmtpConstants.AnonymousSecurityIdentifier)
			{
				this.RemoteIdentityName = "anonymous";
				this.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "DirectTrust certificate failed to authenticate for '{0}'", new object[]
				{
					clientTlsCertificate.Subject
				});
				this.EventLog.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveDirectTrustFailed, this.NetworkConnection.RemoteEndPoint.Address.ToString(), new object[]
				{
					clientTlsCertificate.Subject,
					this.NetworkConnection.RemoteEndPoint.Address
				});
			}
			else
			{
				this.RemoteIdentityName = clientTlsCertificate.Subject;
				this.AuthMethod = MultilevelAuthMechanism.DirectTrustTLS;
				this.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "DirectTrust certificate authenticated as '{0}'", new object[]
				{
					this.RemoteIdentityName
				});
				CertificateExpiryCheck.CheckCertificateExpiry(clientTlsCertificate, this.EventLog, SmtpSessionCertificateUse.RemoteDirectTrust, clientTlsCertificate.Subject, this.UtcNow);
				this.ProtocolLogSession.LogCertificate("Received DirectTrust certificate", clientTlsCertificate);
			}
			this.SessionPermissions = SmtpInSessionUtils.GetPermissions(this.AuthzAuthorization, this.RemoteIdentity, this.ReceiveConnectorStub.SecurityDescriptor);
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000FA63B File Offset: 0x000F883B
		private void IncrementMessageCount()
		{
			this.NumberOfMessagesReceived++;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000FA64B File Offset: 0x000F884B
		private bool IsAttributionDataSpecified(MailParseOutput parseOutput)
		{
			return parseOutput.XAttrOrgId != null && parseOutput.Directionality != MailDirectionality.Undefined;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000FA664 File Offset: 0x000F8864
		private TransportMailItem CreateAndInitializeTransportMailItem(MailParseOutput parseOutput, ADRecipientCache<TransportMiniRecipient> recipientCache, MailDirectionality directionality, Guid externalOrgId)
		{
			TransportMailItem transportMailItem = this.NewTransportMailItem(recipientCache, directionality, externalOrgId);
			transportMailItem.DateReceived = DateTime.UtcNow;
			transportMailItem.From = (RoutingAddress.IsEmpty(parseOutput.OriginalFromAddress) ? parseOutput.FromAddress : parseOutput.OriginalFromAddress);
			transportMailItem.ExposeMessage = false;
			transportMailItem.ExposeMessageHeaders = false;
			transportMailItem.PerfCounterAttribution = "SMTPIn";
			transportMailItem.ReceiveConnectorName = "SMTP:" + (this.ReceiveConnector.Name ?? "Unknown");
			transportMailItem.SourceIPAddress = this.NetworkConnection.RemoteEndPoint.Address;
			transportMailItem.AuthMethod = this.AuthMethod;
			if (this.IsAttributionDataSpecified(parseOutput))
			{
				transportMailItem.ExternalOrganizationId = parseOutput.XAttrOrgId.ExternalOrgId;
				transportMailItem.Directionality = parseOutput.Directionality;
				transportMailItem.ExoAccountForest = parseOutput.XAttrOrgId.ExoAccountForest;
				transportMailItem.ExoTenantContainer = parseOutput.XAttrOrgId.ExoTenantContainer;
			}
			return transportMailItem;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000FA750 File Offset: 0x000F8950
		private void TransferMailCommandProperties(TransportMailItem transportMailItem, MailParseOutput parseOutput, MailCommandEventArgs agentEventArgs)
		{
			transportMailItem.Auth = parseOutput.Auth;
			transportMailItem.EnvId = parseOutput.EnvelopeId;
			transportMailItem.DsnFormat = parseOutput.DsnFormat;
			transportMailItem.BodyType = parseOutput.MailBodyType;
			transportMailItem.Oorg = parseOutput.Oorg;
			transportMailItem.InternetMessageId = parseOutput.InternetMessageId;
			this.SetupExpectedBlobs(parseOutput.MessageContextParameters);
			transportMailItem.SystemProbeId = parseOutput.SystemProbeId;
			this.TransferShadowProperties(transportMailItem, parseOutput);
			this.TransferAuthenticationMechanism(transportMailItem);
			transportMailItem.HeloDomain = this.HelloDomain;
			if (agentEventArgs != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in agentEventArgs.MailItemProperties)
				{
					transportMailItem.ExtendedProperties.SetValue<object>(keyValuePair.Key, keyValuePair.Value);
				}
			}
			transportMailItem.ExtendedProperties.SetValue<ulong>("Microsoft.Exchange.Transport.SmtpInSessionId", this.sessionId);
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x000FA844 File Offset: 0x000F8A44
		private void TransferShadowProperties(TransportMailItem transportMailItem, MailParseOutput parseOutput)
		{
			if (string.IsNullOrEmpty(parseOutput.XShadow))
			{
				return;
			}
			transportMailItem.ShadowServerDiscardId = parseOutput.XShadow;
			if (!SmtpInSessionUtils.IsPeerShadowSession(this.PeerSessionPrimaryServer))
			{
				transportMailItem.ShadowServerContext = this.SenderShadowContext;
			}
			if (parseOutput.ShadowMessageId != Guid.Empty)
			{
				transportMailItem.ShadowMessageId = parseOutput.ShadowMessageId;
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x000FA8A2 File Offset: 0x000F8AA2
		private void TransferAuthenticationMechanism(TransportMailItem transportMailItem)
		{
			if (SmtpInSessionUtils.IsPartner(this.RemoteIdentity))
			{
				transportMailItem.AuthMethod = MultilevelAuthMechanism.MutualTLS;
				return;
			}
			if (SmtpInSessionUtils.IsExternalAuthoritative(this.RemoteIdentity))
			{
				transportMailItem.AuthMethod = MultilevelAuthMechanism.SecureExternalSubmit;
				return;
			}
			transportMailItem.AuthMethod = this.AuthMethod;
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x000FA8DC File Offset: 0x000F8ADC
		private void AddRecipientCacheEntry(ADRecipientCache<TransportMiniRecipient> recipientCache)
		{
			ProxyAddress proxyAddress = new SmtpProxyAddress(this.AuthenticatedUser.PrimarySmtpAddress.ToString(), true);
			Result<TransportMiniRecipient> result = new Result<TransportMiniRecipient>(this.AuthenticatedUser, null);
			recipientCache.AddCacheEntry(proxyAddress, result);
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000FA920 File Offset: 0x000F8B20
		private void HandleShadowMessageChecks(TransportMailItem transportMailItem)
		{
			if (!SmtpInSessionUtils.IsShadowedBySender(this.SenderShadowContext) && this.ReceiveConnector.MaxAcknowledgementDelay > TimeSpan.Zero && this.ServerState.ShadowRedundancyManager != null && this.ServerState.ShadowRedundancyManager.ShouldDelayAck())
			{
				this.Tracer.TraceDebug<long>(0L, "SmtpInSession(id={0}).HandleShadowMessageChecks: Message stamped as a delayed ack message.", this.NetworkConnection.ConnectionId);
				transportMailItem.ShadowServerContext = "$localhost$";
				transportMailItem.ShadowServerDiscardId = transportMailItem.ShadowMessageId.ToString();
				this.DelayedAckStatus = DelayedAckStatus.Stamped;
				return;
			}
			if (SmtpInSessionUtils.IsPeerShadowSession(this.PeerSessionPrimaryServer) && this.ServerState.ShadowRedundancyManager != null)
			{
				transportMailItem.ShadowServerContext = this.ServerState.ShadowRedundancyManager.GetShadowContextForInboundSession();
				transportMailItem.ShadowServerDiscardId = transportMailItem.ShadowMessageId.ToString();
				return;
			}
			this.DelayedAckStatus = DelayedAckStatus.None;
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x000FAA10 File Offset: 0x000F8C10
		private void TraceAndLogSessionPermissions()
		{
			if (this.Tracer != null)
			{
				this.Tracer.TraceDebug<string, Permission>((long)this.GetHashCode(), "Client '{0}' is granted the following permissions: {1}", this.RemoteIdentityName ?? "anonymous", this.SessionPermissions);
			}
			this.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes(Util.GetPermissionString(this.SessionPermissions)), "Set Session Permissions");
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x000FAA74 File Offset: 0x000F8C74
		private void UpdateSmtpReceivePerfCountersForMessageReceived(int recipients, long messageBytes)
		{
			ISmtpReceivePerfCounters receivePerfCounters = this.ReceivePerfCounters;
			if (receivePerfCounters != null)
			{
				receivePerfCounters.MessagesReceivedTotal.Increment();
				receivePerfCounters.RecipientsAccepted.IncrementBy((long)recipients);
				receivePerfCounters.MessageBytesReceivedTotal.IncrementBy(messageBytes);
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x000FAAB2 File Offset: 0x000F8CB2
		private void ThrowIfNotTls()
		{
			if (!this.NetworkConnection.IsTls)
			{
				throw new InvalidOperationException("Method can only be invoked for TLS session.");
			}
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x000FAACC File Offset: 0x000F8CCC
		private SmtpInSessionState(ISmtpInSession session)
		{
			this.isInvokedFromLegacyStack = true;
			this.ServerState = new SmtpInServerState(session.SmtpInServer);
			this.AdvertisedEhloOptions = session.AdvertisedEhloOptions;
			this.AdvertisedTlsCertificate = ((session.AdvertisedTlsCertificate == null) ? null : new X509Certificate2Wrapper(session.AdvertisedTlsCertificate));
			this.AuthenticatedUser = session.AuthUserRecipient;
			this.authenticationSourceForAgents = session.AuthenticationSourceForAgents;
			this.AuthMethod = session.AuthMethod;
			this.AuthzAuthorization = session.AuthzAuthorization;
			this.CertificateValidator = session.SmtpInServer.CertificateValidator;
			this.Configuration = session.SmtpInServer.ReceiveConfiguration;
			this.EventLog = new ExEventLogWrapper(session.EventLogger);
			this.EventNotificationItem = session.SmtpInServer.EventNotificationItem;
			this.FirstBdatCall = !session.IsBdatOngoing;
			this.RequestClientTlsCertificate = session.ForceRequestClientTlsCertificate;
			this.InternalTransportCertificate = ((session.InternalTransportCertificate == null) ? null : new X509Certificate2Wrapper(session.InternalTransportCertificate));
			this.isDiscardingMessage = session.DiscardingMessage;
			this.IsExternalConnection = !session.IsTrustedIP(session.ClientEndPoint.Address);
			this.MailItemPermissionsGranted = session.MailItemPermissionsGranted;
			this.MailItemPermissionsDenied = session.MailItemPermissionsDenied;
			this.MessageContextBlob = session.MessageContextBlob;
			this.MessageThrottlingManager = session.MessageThrottlingManager;
			this.MessageWriteStream = session.MessageWriteStream;
			this.MexRuntimeSession = session.MexSession;
			this.NetworkConnection = session.NetworkConnection;
			this.NumberOfMessagesReceived = session.NumberOfMessagesReceived;
			this.PeerSessionPrimaryServer = session.PeerSessionPrimaryServer;
			this.ProtocolLogSession = session.LogSession;
			this.ReceiveConnector = session.Connector;
			this.ReceiveConnectorStub = session.ConnectorStub;
			this.RecipientCorrelator = session.RecipientCorrelator;
			this.RemoteIdentity = session.RemoteIdentity;
			this.RemoteIdentityName = session.RemoteIdentityName;
			this.SecureState = session.SecureState;
			this.SenderShadowContext = session.SenderShadowContext;
			this.sessionPermissions = session.SessionPermissions;
			this.SessionStartTime = session.SessionStartTime;
			this.SmtpAgentSession = session.AgentSession;
			this.startTlsDisabled = session.DisableStartTls;
			this.SmtpUtf8Supported = session.SmtpUtf8Supported;
			this.supportIntegratedAuth = session.SupportIntegratedAuth;
			this.TlsDomainCapabilities = session.TlsDomainCapabilities;
			this.TlsRemoteCertificateInternal = ((session.TlsRemoteCertificate == null) ? null : new X509Certificate2Wrapper(session.TlsRemoteCertificate));
			this.Tracer = session.Tracer;
			this.TransportMailItem = session.TransportMailItem;
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x000FAD5A File Offset: 0x000F8F5A
		public DisposeTracker GetDisposeTracker()
		{
			return this.InternalGetDisposeTracker();
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x000FAD62 File Offset: 0x000F8F62
		public virtual void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x000FAD77 File Offset: 0x000F8F77
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x000FAD86 File Offset: 0x000F8F86
		internal override void DiscardMessage(SmtpResponse response, string sourceContext)
		{
			if (response.SmtpResponseType != SmtpResponseType.Success)
			{
				throw new InvalidOperationException("Response provided must be a success (2xx) one. If you want to reject, call RejectMessage instead");
			}
			this.SmtpResponse = response;
			SmtpSessionHelper.DiscardMessage(sourceContext, this.ExecutionControl, this.TransportMailItem, this.ProtocolLogSession, this.messageTrackingLogWrapper);
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x000FADC4 File Offset: 0x000F8FC4
		internal override void Disconnect()
		{
			this.ShouldDisconnect = true;
			ISmtpReceivePerfCounters receivePerfCounters = this.ReceivePerfCounters;
			if (receivePerfCounters != null)
			{
				receivePerfCounters.ConnectionsDroppedByAgentsTotal.Increment();
			}
			this.ExecutionControl.HaltExecution();
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x000FADF9 File Offset: 0x000F8FF9
		internal override void RejectMessage(SmtpResponse response)
		{
			this.RejectMessage(response, null);
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x000FAE04 File Offset: 0x000F9004
		internal override void RejectMessage(SmtpResponse response, string sourceContext)
		{
			this.SmtpResponse = response;
			SmtpSessionHelper.RejectMessage(response, sourceContext, this.ExecutionControl, this.TransportMailItem, this.LocalEndPoint, this.RemoteEndPoint, this.sessionId, this.ReceiveConnector, this.ProtocolLogSession, this.messageTrackingLogWrapper);
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x000FAE4F File Offset: 0x000F904F
		internal override CertificateValidationStatus ValidateCertificate()
		{
			this.ThrowIfNotTls();
			return SmtpSessionHelper.ConvertChainValidityStatusToCertValidationStatus(this.TlsRemoteCertificateChainValidationStatus);
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x000FAE62 File Offset: 0x000F9062
		internal override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			this.ThrowIfNotTls();
			return SmtpSessionHelper.ValidateCertificate(domain, this.TlsRemoteCertificate, this.SecureState, this.TlsRemoteCertificateChainValidationStatus, this.ServerState.CertificateValidator, this.ProtocolLogSession, out matchedCertDomain);
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003C55 RID: 15445 RVA: 0x000FAE94 File Offset: 0x000F9094
		public override bool AntispamBypass
		{
			get
			{
				return SmtpInSessionUtils.HasSMTPAntiSpamBypassPermission(this.CombinedPermissions);
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x000FAEA1 File Offset: 0x000F90A1
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x000FAEA9 File Offset: 0x000F90A9
		internal override IExecutionControl ExecutionControl { get; set; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003C58 RID: 15448 RVA: 0x000FAEB2 File Offset: 0x000F90B2
		internal override X509Certificate2 TlsRemoteCertificate
		{
			get
			{
				if (this.TlsRemoteCertificateInternal != null)
				{
					return this.TlsRemoteCertificateInternal.Certificate;
				}
				return null;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000FAEC9 File Offset: 0x000F90C9
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x000FAED1 File Offset: 0x000F90D1
		internal override bool IsClientProxiedSession { get; set; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000FAEDA File Offset: 0x000F90DA
		public override bool IsConnected
		{
			get
			{
				return !this.ShouldDisconnect;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x000FAEE5 File Offset: 0x000F90E5
		// (set) Token: 0x06003C5D RID: 15453 RVA: 0x000FAEED File Offset: 0x000F90ED
		internal override bool IsInboundProxiedSession { get; set; }

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000FAEF6 File Offset: 0x000F90F6
		public override bool IsTls
		{
			get
			{
				return this.NetworkConnection.IsTls;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000FAF03 File Offset: 0x000F9103
		// (set) Token: 0x06003C60 RID: 15456 RVA: 0x000FAF0B File Offset: 0x000F910B
		public override IPAddress LastExternalIPAddress { get; internal set; }

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003C61 RID: 15457 RVA: 0x000FAF14 File Offset: 0x000F9114
		public override IPEndPoint LocalEndPoint
		{
			get
			{
				return this.NetworkConnection.LocalEndPoint;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x000FAF21 File Offset: 0x000F9121
		public override IDictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000FAF29 File Offset: 0x000F9129
		internal override string ReceiveConnectorName
		{
			get
			{
				return this.ReceiveConnector.Name;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000FAF36 File Offset: 0x000F9136
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x000FAF3E File Offset: 0x000F913E
		public override IPEndPoint RemoteEndPoint { get; internal set; }

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000FAF47 File Offset: 0x000F9147
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x000FAF4F File Offset: 0x000F914F
		internal override bool ShouldDisconnect { get; set; }

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000FAF58 File Offset: 0x000F9158
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x000FAF60 File Offset: 0x000F9160
		internal override SmtpResponse SmtpResponse { get; set; }

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000FAF69 File Offset: 0x000F9169
		internal override bool XAttrAdvertised
		{
			get
			{
				return this.AdvertisedEhloOptions.XAttr;
			}
		}

		// Token: 0x04001DF5 RID: 7669
		private static readonly Queue<IInboundMessageContextBlob> EmptyInboundMessageContextBlobs = new Queue<IInboundMessageContextBlob>(0);

		// Token: 0x04001DF6 RID: 7670
		private readonly bool isInvokedFromLegacyStack;

		// Token: 0x04001DF7 RID: 7671
		private readonly AuthenticationSource authenticationSourceForAgents;

		// Token: 0x04001DF8 RID: 7672
		private readonly MessageTrackingLogWrapper messageTrackingLogWrapper = new MessageTrackingLogWrapper();

		// Token: 0x04001DF9 RID: 7673
		private readonly IDictionary<string, object> properties = new Dictionary<string, object>();

		// Token: 0x04001DFA RID: 7674
		private readonly ulong sessionId;

		// Token: 0x04001DFB RID: 7675
		private bool isDiscardingMessage;

		// Token: 0x04001DFC RID: 7676
		private bool disposed;

		// Token: 0x04001DFD RID: 7677
		private DisposeTracker disposeTracker;

		// Token: 0x04001DFE RID: 7678
		private string advertisedDomain;

		// Token: 0x04001DFF RID: 7679
		private Permission? anonymousPermissions;

		// Token: 0x04001E00 RID: 7680
		private Permission? partnerPermissions;

		// Token: 0x04001E01 RID: 7681
		public Permission sessionPermissions;

		// Token: 0x04001E02 RID: 7682
		private ChainValidityStatus? tlsRemoteCertificateChainValidationStatus;

		// Token: 0x04001E03 RID: 7683
		private object lastNetworkError;

		// Token: 0x04001E04 RID: 7684
		protected bool startTlsDisabled;

		// Token: 0x04001E05 RID: 7685
		protected bool supportIntegratedAuth;
	}
}
