using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004FD RID: 1277
	internal class SmtpOutProxySession : ISmtpSession, ISmtpOutSession
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x000F4C80 File Offset: 0x000F2E80
		public SmtpOutProxySession(ISmtpInSession inSession, SmtpOutConnection smtpOutConnection, ulong sessionId, IPEndPoint target, ProtocolLog protocolLog, ProtocolLoggingLevel loggingLevel, CertificateCache certificateCache, CertificateValidator certificateValidator, ITransportConfiguration transportConfiguration, TransportAppConfig transportAppConfig, string connectionContextString)
		{
			this.InSession = inSession;
			this.smtpOutConnection = smtpOutConnection;
			this.Connector = smtpOutConnection.Connector;
			this.response = new List<string>(50);
			this.sessionProps = new SmtpSessionProps(sessionId);
			this.certificateValidator = certificateValidator;
			this.TlsConfiguration = smtpOutConnection.TlsConfig;
			this.transportConfiguration = transportConfiguration;
			this.transportAppConfig = transportAppConfig;
			this.currentState = SmtpOutSession.SessionState.ConnectResponse;
			this.sessionProps.AdvertisedEhloOptions = new EhloOptions();
			SmtpDomain fqdn = this.Connector.Fqdn;
			this.sessionProps.HelloDomain = ((fqdn != null && !string.IsNullOrEmpty(fqdn.Domain)) ? fqdn.Domain : ComputerInformation.DnsPhysicalFullyQualifiedDomainName);
			this.sessionProps.RemoteEndPoint = target;
			this.advertisedTlsCertificate = SmtpOutSession.LoadTlsCertificate(this.TlsConfiguration, certificateCache, this.transportAppConfig.SmtpSendConfiguration.OneLevelWildcardMatchForCertSelection, this.Connector.Name, this.GetHashCode());
			this.logSession = protocolLog.OpenSession(this.Connector.Name, this.SessionId, target, null, loggingLevel);
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "attempting to connect. " + connectionContextString);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<IPEndPoint>((long)this.GetHashCode(), "Attempting to connect to {0}", target);
			SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_SmtpSendNewProxySession, null, new object[]
			{
				this.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo),
				target
			});
			this.TlsConfiguration.LogTlsOverride(this.logSession);
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x000F4E37 File Offset: 0x000F3037
		public ulong SessionId
		{
			get
			{
				return this.sessionProps.SessionId;
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x000F4E44 File Offset: 0x000F3044
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.sessionProps.RemoteEndPoint;
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x000F4E51 File Offset: 0x000F3051
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.sessionProps.LocalEndPoint;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000F4E5E File Offset: 0x000F305E
		public string ProxyTargetHostName
		{
			get
			{
				return this.smtpOutConnection.SmtpHostName;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000F4E6B File Offset: 0x000F306B
		// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x000F4E78 File Offset: 0x000F3078
		public string HelloDomain
		{
			get
			{
				return this.sessionProps.HelloDomain;
			}
			set
			{
				this.sessionProps.HelloDomain = value;
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x000F4E86 File Offset: 0x000F3086
		public IEhloOptions AdvertisedEhloOptions
		{
			get
			{
				return this.sessionProps.AdvertisedEhloOptions;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x000F4E93 File Offset: 0x000F3093
		// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x000F4E9B File Offset: 0x000F309B
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

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x000F4EA4 File Offset: 0x000F30A4
		public IProtocolLogSession LogSession
		{
			get
			{
				return this.logSession;
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x000F4EAC File Offset: 0x000F30AC
		public SecureState SecureState
		{
			get
			{
				return this.secureState;
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x000F4EB4 File Offset: 0x000F30B4
		// (set) Token: 0x06003ADC RID: 15068 RVA: 0x000F4EBC File Offset: 0x000F30BC
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

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000F4EC5 File Offset: 0x000F30C5
		public virtual bool Disconnected
		{
			get
			{
				return this.disconnected;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x000F4ECD File Offset: 0x000F30CD
		public long ConnectionId
		{
			get
			{
				return this.connectionId;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000F4ED5 File Offset: 0x000F30D5
		public NetworkConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000F4EDD File Offset: 0x000F30DD
		// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x000F4EE5 File Offset: 0x000F30E5
		public bool IsProxying
		{
			get
			{
				return this.isProxying;
			}
			set
			{
				this.isProxying = value;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000F4EEE File Offset: 0x000F30EE
		public bool IsClientProxy
		{
			get
			{
				return this.smtpOutConnection.ClientProxy;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x000F4EFB File Offset: 0x000F30FB
		// (set) Token: 0x06003AE4 RID: 15076 RVA: 0x000F4F03 File Offset: 0x000F3103
		public SmtpResponse BlindProxySuccessfulInboundResponse
		{
			get
			{
				return this.blindProxySuccessfulInboundResponse;
			}
			set
			{
				this.blindProxySuccessfulInboundResponse = value;
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x000F4F0C File Offset: 0x000F310C
		public AckDetails AckDetails
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000F4F0F File Offset: 0x000F310F
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000F4F17 File Offset: 0x000F3117
		internal TlsSendConfiguration TlsConfiguration { get; private set; }

		// Token: 0x06003AE8 RID: 15080 RVA: 0x000F4F20 File Offset: 0x000F3120
		public byte[] GetTlsEapKey()
		{
			return this.connection.TlsEapKey;
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000F4F2D File Offset: 0x000F312D
		public byte[] GetCertificatePublicKey()
		{
			return this.connection.RemoteCertificate.GetPublicKey();
		}

		// Token: 0x06003AEA RID: 15082 RVA: 0x000F4F3F File Offset: 0x000F313F
		public void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse, SessionSetupFailureReason failureReason)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "AckConnection called with status: {0}, Response: {1}", ackStatus, smtpResponse);
			this.smtpOutConnection.BytesSent = (ulong)this.connection.BytesSent;
			this.smtpOutConnection.AckConnection(ackStatus, smtpResponse, null, null, failureReason);
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000F4F7F File Offset: 0x000F317F
		public void Disconnect()
		{
			this.Disconnect(false, false, SmtpResponse.Empty, SessionSetupFailureReason.None, false);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000F4F90 File Offset: 0x000F3190
		public void ShutdownConnection()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.ShutdownConnection);
			NetworkConnection networkConnection = this.connection;
			if (networkConnection != null)
			{
				networkConnection.Shutdown();
			}
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x000F4FB8 File Offset: 0x000F31B8
		public void StartUsingConnection()
		{
			this.logSession.LogConnect();
			SmtpProxyPerfCountersWrapper smtpProxyPerfCounters = this.InSession.SmtpProxyPerfCounters;
			if (smtpProxyPerfCounters != null)
			{
				this.InSession.SmtpProxyPerfCounters.IncrementOutboundConnectionsTotal();
				this.InSession.SmtpProxyPerfCounters.IncrementOutboundConnectionsCurrent();
			}
			this.StartReadLine();
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x000F5005 File Offset: 0x000F3205
		public void StartTls(SecureState secureState)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Initiating TLS on the outboundConnection");
			this.secureState = (secureState | SecureState.NegotiationRequested);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x000F502B File Offset: 0x000F322B
		public void DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x000F503C File Offset: 0x000F323C
		public NetworkConnection DissociateFromSetupHandler()
		{
			NetworkConnection result = this.connection;
			this.connection = null;
			return result;
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x000F5058 File Offset: 0x000F3258
		public void SetNextStateToAuthLogin()
		{
			if (!string.IsNullOrEmpty(this.InSession.ProxyUserName) && this.InSession.ProxyPassword != null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "XPROXY command succeeded or was not required, will try to authenticate under AUTH LOGIN");
				this.NextState = SmtpOutSession.SessionState.Auth;
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Unable to AUTH LOGIN because username or password is null");
			throw new InvalidOperationException("Username and/or password cannot be null or empty while setting up proxy session");
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x000F50C2 File Offset: 0x000F32C2
		public void FailoverConnection(SmtpResponse smtpResponse, SessionSetupFailureReason failoverReason)
		{
			this.FailoverConnection(smtpResponse, failoverReason, false);
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x000F50D0 File Offset: 0x000F32D0
		public void ConnectionCompleted(NetworkConnection networkConnection)
		{
			this.connection = networkConnection;
			this.connectionId = networkConnection.ConnectionId;
			this.connection.MaxLineLength = 2000;
			this.logSession.LocalEndPoint = this.connection.LocalEndPoint;
			this.connection.Timeout = (int)this.smtpOutConnection.Connector.ConnectionInactivityTimeOut.TotalSeconds;
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x000F513C File Offset: 0x000F333C
		public string GetConnectionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("LastIndex : ");
			stringBuilder.Append(this.breadcrumbs.LastFilledIndex);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Session BreadCrumb : ");
			for (int i = 0; i < 64; i++)
			{
				stringBuilder.Append(Enum.Format(typeof(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs), this.breadcrumbs.BreadCrumb[i], "x"));
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

		// Token: 0x06003AF5 RID: 15093 RVA: 0x000F51FB File Offset: 0x000F33FB
		public void PrepareForNextMessageOnCachedSession()
		{
			throw new InvalidOperationException("A blind proxy session should never be cached");
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x000F5208 File Offset: 0x000F3408
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

		// Token: 0x06003AF7 RID: 15095 RVA: 0x000F532F File Offset: 0x000F352F
		public bool TryGetRemainingSmtpTargets(out IEnumerable<INextHopServer> remainingTargets)
		{
			return this.smtpOutConnection.TryGetRemainingSmtpTargets(out remainingTargets);
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x000F5340 File Offset: 0x000F3540
		private static void WriteCompleteReadLine(IAsyncResult asyncResult)
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)asyncResult.AsyncState;
			object obj;
			smtpOutProxySession.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutProxySession.HandleErrorSettingUpProxySession(obj, false);
				return;
			}
			smtpOutProxySession.sendBuffer.Reset();
			smtpOutProxySession.StartReadLine();
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x000F5384 File Offset: 0x000F3584
		private static void ReadLineComplete(IAsyncResult asyncResult)
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)asyncResult.AsyncState;
			bool overflow = false;
			byte[] buffer;
			int offset;
			int size;
			object obj;
			smtpOutProxySession.connection.EndReadLine(asyncResult, out buffer, out offset, out size, out obj);
			if (obj != null)
			{
				if (!(obj is SocketError) || (SocketError)obj != SocketError.MessageSize)
				{
					smtpOutProxySession.HandleErrorSettingUpProxySession(obj, false);
					return;
				}
				overflow = true;
			}
			if (smtpOutProxySession.disconnected)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)smtpOutProxySession.GetHashCode(), "Command Received from NetworkConnection, but we are already disconnected");
				return;
			}
			smtpOutProxySession.StartProcessingResponse(buffer, offset, size, overflow);
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000F5408 File Offset: 0x000F3608
		private static void TlsNegotiationComplete(IAsyncResult asyncResult)
		{
			SmtpOutProxySession smtpOutProxySession = (SmtpOutProxySession)asyncResult.AsyncState;
			object obj;
			smtpOutProxySession.connection.EndNegotiateTlsAsClient(asyncResult, out obj);
			if (obj != null)
			{
				smtpOutProxySession.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "TLS negotiation failed with error {0}", new object[]
				{
					obj
				});
				bool retryWithoutStartTls = smtpOutProxySession.currentState == SmtpOutSession.SessionState.StartTLS && !smtpOutProxySession.TlsConfiguration.RequireTls;
				smtpOutProxySession.HandleErrorSettingUpProxySession(obj, retryWithoutStartTls);
				return;
			}
			ConnectionInfo tlsConnectionInfo = smtpOutProxySession.connection.TlsConnectionInfo;
			Util.LogTlsSuccessResult(smtpOutProxySession.logSession, tlsConnectionInfo, smtpOutProxySession.connection.RemoteCertificate);
			smtpOutProxySession.TlsNegotiationComplete();
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x000F54A0 File Offset: 0x000F36A0
		private static string ConvertStringListToString(List<string> stringList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in stringList)
			{
				stringBuilder.AppendLine(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x000F54FC File Offset: 0x000F36FC
		private void FailoverConnection(SmtpResponse smtpResponse, SessionSetupFailureReason failoverReason, bool retryWithoutStartTls)
		{
			this.failoverInProgress = true;
			this.smtpOutConnection.FailoverConnection(smtpResponse, retryWithoutStartTls, failoverReason);
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x000F5514 File Offset: 0x000F3714
		private void Disconnect(bool disposeCurrentCommand, bool failover, SmtpResponse failoverResponse, SessionSetupFailureReason failoverReason, bool retryWithoutStartTls)
		{
			if (failover && (failoverResponse.IsEmpty || failoverReason == SessionSetupFailureReason.None))
			{
				throw new ArgumentException("failoverResponse and failoverReason need to be specified when failing over");
			}
			ExTraceGlobals.SmtpSendTracer.TraceError<long, string>((long)this.GetHashCode(), "Disconnect initiated for connection {0} due to {1}", this.connectionId, failoverResponse.ToString());
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.Disconnect);
			if (disposeCurrentCommand)
			{
				this.DisposeCurrentCommand();
			}
			if (!this.disconnected)
			{
				this.disconnected = true;
				if (this.connection != null)
				{
					SmtpProxyPerfCountersWrapper smtpProxyPerfCounters = this.InSession.SmtpProxyPerfCounters;
					if (smtpProxyPerfCounters != null)
					{
						this.InSession.SmtpProxyPerfCounters.DecrementOutboundConnectionsCurrent();
					}
					this.connection.Dispose();
					this.LogSession.LogDisconnect(DisconnectReason.Local);
				}
				if (failover && !this.failoverInProgress)
				{
					this.FailoverConnection(failoverResponse, failoverReason, retryWithoutStartTls);
				}
				if (!this.failoverInProgress)
				{
					this.smtpOutConnection.RemoveConnection();
				}
				this.logSession = null;
			}
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x000F55F2 File Offset: 0x000F37F2
		private void DisposeCurrentCommand()
		{
			if (this.currentCommand != null)
			{
				this.currentCommand.Dispose();
				this.currentCommand = null;
			}
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x000F5610 File Offset: 0x000F3810
		private void HandleErrorSettingUpProxySession(object error, bool retryWithoutStartTls)
		{
			SessionSetupFailureReason failoverReason;
			string text;
			if (error is SocketError)
			{
				failoverReason = SessionSetupFailureReason.SocketError;
				text = "Socket error " + error.ToString();
			}
			else if (error is SecurityStatus)
			{
				failoverReason = SessionSetupFailureReason.ProtocolError;
				text = "Security status " + error.ToString();
			}
			else
			{
				failoverReason = SessionSetupFailureReason.ProtocolError;
				text = "Error " + ((error != null) ? error.ToString() : "<null>");
			}
			ExTraceGlobals.SmtpSendTracer.TraceError<long, string>((long)this.GetHashCode(), "SmtpOutProxySession(id={0}).HandleErrorSettingUpProxySession ({1})", this.connectionId, text);
			SmtpResponse failoverResponse = new SmtpResponse("451", "4.4.0", new string[]
			{
				text
			});
			bool failover = this.currentState != SmtpOutSession.SessionState.Quit;
			this.Disconnect(true, failover, failoverResponse, failoverReason, retryWithoutStartTls);
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x000F56CC File Offset: 0x000F38CC
		private void AppendToResponseBuffer(string responseline)
		{
			if (this.response.Count >= 50)
			{
				throw new FormatException("Excessive data, unable to parse : " + SmtpOutProxySession.ConvertStringListToString(this.response));
			}
			int length = responseline.Length;
			if (length > 0 && responseline[length - 1] == '\r')
			{
				responseline = responseline.Substring(0, length - 1);
			}
			this.response.Add(responseline);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000F5734 File Offset: 0x000F3934
		private void MoveToNextState()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.MoveToNextState);
			this.DisposeCurrentCommand();
			if (!this.Disconnected)
			{
				this.currentState = this.NextState;
				this.currentCommand = this.CreateSmtpCommand(this.currentState);
				while (!this.SendCurrentCommand())
				{
					this.DisposeCurrentCommand();
					this.currentState = this.NextState;
					this.currentCommand = this.CreateSmtpCommand(this.currentState);
				}
			}
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000F57A4 File Offset: 0x000F39A4
		private SmtpCommand CreateSmtpCommand(SmtpOutSession.SessionState state)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpOutSession.SessionState>((long)this.GetHashCode(), "Creating Smtp Command: {0}", state);
			SmtpCommand smtpCommand;
			switch (state)
			{
			case SmtpOutSession.SessionState.Ehlo:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdEhlo);
				smtpCommand = new EHLOSmtpProxyCommand(this, this.transportConfiguration);
				break;
			case SmtpOutSession.SessionState.Helo:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdHelo);
				smtpCommand = new HELOSmtpProxyCommand(this);
				break;
			case SmtpOutSession.SessionState.Auth:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdAuth);
				smtpCommand = new AuthSmtpProxyCommand(this, this.transportConfiguration, false);
				break;
			case SmtpOutSession.SessionState.Exps:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdAuth);
				smtpCommand = new AuthSmtpProxyCommand(this, this.transportConfiguration, true);
				break;
			case SmtpOutSession.SessionState.StartTLS:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdStarttls);
				smtpCommand = new StarttlsSmtpProxyCommand(this, this.transportConfiguration, false);
				break;
			case SmtpOutSession.SessionState.AnonymousTLS:
				this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdStarttls);
				smtpCommand = new StarttlsSmtpProxyCommand(this, this.transportConfiguration, true);
				break;
			default:
				if (state != SmtpOutSession.SessionState.XProxy)
				{
					if (state != SmtpOutSession.SessionState.Quit)
					{
						throw new ArgumentException("Unknown command encountered in SmtpOutProxy: " + state, "state");
					}
					this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdQuit);
					smtpCommand = new QuitSmtpProxyCommand(this);
				}
				else
				{
					this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.CreateCmdXProxy);
					smtpCommand = new XProxySmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
				}
				break;
			}
			smtpCommand.ParsingStatus = ParsingStatus.Complete;
			smtpCommand.OutboundCreateCommand();
			return smtpCommand;
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000F58E0 File Offset: 0x000F3AE0
		private bool SendCurrentCommand()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.SendCurrentCommand);
			if (this.currentState == SmtpOutSession.SessionState.StartTLS && (byte)(this.secureState & SecureState.NegotiationRequested) == 128)
			{
				throw new InvalidOperationException("Should not attempt to send command when TLS negotiation is on");
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpOutSession.SessionState>((long)this.GetHashCode(), "Invoking Command Handler for {0}", this.currentState);
			if (!this.InvokeCommandHandler())
			{
				return false;
			}
			this.SendBufferThenReadLine();
			return true;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x000F594C File Offset: 0x000F3B4C
		private void SendBufferThenReadLine()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.SendBufferThenReadLine);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Flushing SendBuffer");
			this.connection.BeginWrite(this.sendBuffer.GetBuffer(), 0, this.sendBuffer.Length, SmtpOutProxySession.writeCompleteReadLine, this);
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x000F59A0 File Offset: 0x000F3BA0
		private bool InvokeCommandHandler()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.InvokeCommandHandler);
			this.currentCommand.OutboundFormatCommand();
			if (this.currentCommand.ProtocolCommandString != null)
			{
				this.currentCommand.ProtocolCommand = ByteString.StringToBytesAndAppendCRLF(this.currentCommand.ProtocolCommandString, true);
				if (string.IsNullOrEmpty(this.currentCommand.RedactedProtocolCommandString))
				{
					this.logSession.LogSend(this.currentCommand.ProtocolCommand);
				}
				else
				{
					this.logSession.LogSend(ByteString.StringToBytes(this.currentCommand.RedactedProtocolCommandString, true));
				}
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Enqueuing Command: {0} on the connection", this.currentCommand.ProtocolCommandString);
				this.sendBuffer.Append(this.currentCommand.ProtocolCommand);
				return true;
			}
			if (this.currentCommand.ProtocolCommand != null)
			{
				this.logSession.LogSend(SmtpOutSession.BinaryData);
				this.sendBuffer.Append(this.currentCommand.ProtocolCommand);
				return true;
			}
			return false;
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x000F5AA1 File Offset: 0x000F3CA1
		private void ProcessResponse()
		{
			this.InvokeResponseHandler();
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x000F5AAC File Offset: 0x000F3CAC
		private void InvokeResponseHandler()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.InvokeResponseHandler);
			SmtpResponse smtpResponse;
			if (!SmtpResponse.TryParse(this.response, out smtpResponse))
			{
				throw new FormatException("Response text was incorrectly formed : " + SmtpOutProxySession.ConvertStringListToString(this.response));
			}
			this.response.Clear();
			if (this.currentCommand == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Invoked Response Handler for ConnectResponse");
				this.ConnectResponseEvent(smtpResponse);
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Invoked Response Handler for {0}", this.currentCommand.ProtocolCommandKeyword);
			this.currentCommand.SmtpResponse = smtpResponse;
			this.HandlePostParseResponse();
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x000F5B50 File Offset: 0x000F3D50
		private void HandlePostParseResponse()
		{
			if (string.Equals(this.currentCommand.SmtpResponse.StatusCode, "421", StringComparison.Ordinal) && !(this.currentCommand is QuitSmtpCommand))
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Attempting failover. 421 Status code on {0}. NextState: Quit", this.currentCommand.ProtocolCommandKeyword);
				this.FailoverConnection(this.currentCommand.SmtpResponse, SessionSetupFailureReason.Shutdown);
				this.NextState = SmtpOutSession.SessionState.Quit;
			}
			else
			{
				this.currentCommand.OutboundProcessResponse();
				if ((byte)(this.secureState & SecureState.NegotiationRequested) == 128)
				{
					if (!(this.currentCommand is StarttlsSmtpCommand))
					{
						throw new InvalidOperationException("Command being processed is not StartTls");
					}
					X509Certificate2 x509Certificate = null;
					if ((byte)(this.secureState & ~SecureState.NegotiationRequested) == 1)
					{
						x509Certificate = this.advertisedTlsCertificate;
					}
					this.logSession.LogCertificate("Sending certificate", x509Certificate);
					this.connection.BeginNegotiateTlsAsClient(x509Certificate, this.connection.RemoteEndPoint.Address.ToString(), SmtpOutProxySession.tlsNegotiationComplete, this);
					return;
				}
				else if (this.currentCommand.ParsingStatus == ParsingStatus.MoreDataRequired)
				{
					this.currentCommand.ProtocolCommand = null;
					this.currentCommand.ProtocolCommandString = null;
					this.currentCommand.ParsingStatus = ParsingStatus.Complete;
					this.InvokeCommandHandler();
					this.SendBufferThenReadLine();
					return;
				}
			}
			if (!this.IsProxying)
			{
				this.MoveToNextState();
				return;
			}
			if (this.IsClientProxy && !this.IsAuthenticated)
			{
				throw new InvalidOperationException("Cannot proxy client session without authenticating");
			}
			this.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy session successfully set up for {0}. Inbound session will now be blindly proxied", new object[]
			{
				this.IsClientProxy ? Util.Redact(this.InSession.ProxyUserName) : "outbound proxy"
			});
			SmtpResponse successfulResponse = this.BlindProxySuccessfulInboundResponse;
			this.DisposeCurrentCommand();
			this.smtpOutConnection.RemoveConnection();
			this.InSession.SmtpProxyPerfCounters.UpdateOnProxySuccess();
			this.InSession.HandleBlindProxySetupSuccess(successfulResponse, this.connection, this.SessionId, this.logSession, this.IsClientProxy);
			this.logSession = null;
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x000F5D4C File Offset: 0x000F3F4C
		private void ConnectResponseEvent(SmtpResponse smtpResponse)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<IPEndPoint>((long)this.GetHashCode(), "Connected to remote server: {0}", this.sessionProps.RemoteEndPoint);
			if (!this.disconnected)
			{
				if (smtpResponse.StatusCode[0] != '2')
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Server is not accepting mail, connect response: {0}", smtpResponse);
					this.FailoverConnection(smtpResponse, SessionSetupFailureReason.ProtocolError);
					this.NextState = SmtpOutSession.SessionState.Quit;
					this.response.Clear();
					this.MoveToNextState();
					return;
				}
				if (this.Connector.ForceHELO)
				{
					if (this.IsClientProxy)
					{
						throw new InvalidOperationException("Client proxy connector should not have ForceHelo set");
					}
					this.NextState = SmtpOutSession.SessionState.Helo;
				}
				else
				{
					this.NextState = SmtpOutSession.SessionState.Ehlo;
				}
				this.response.Clear();
				this.MoveToNextState();
			}
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x000F5E10 File Offset: 0x000F4010
		private void StartProcessingResponse(byte[] buffer, int offset, int size, bool overflow)
		{
			BufferBuilder bufferBuilder = this.responseBuffer ?? new BufferBuilder(size);
			try
			{
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
					if (!(this.currentCommand is AuthSmtpCommand))
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
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "The connection was dropped because a response was illegally formatted. The error is: {0}", new object[]
				{
					ex.Message
				});
				this.HandleErrorSettingUpProxySession("Response was not in the expected format", false);
			}
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x000F5F64 File Offset: 0x000F4164
		private void StartReadLine()
		{
			this.connection.BeginReadLine(SmtpOutProxySession.readLineComplete, this);
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000F5F78 File Offset: 0x000F4178
		private void TlsNegotiationComplete()
		{
			this.DropBreadcrumb(SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs.TlsNegotiationComplete);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<long>((long)this.GetHashCode(), "TLS negotiation completed for connection {0}. Reissue Ehlo", this.connectionId);
			if (this.TlsConfiguration.RequireTls && this.connection.RemoteCertificate == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "No remote certificate present");
				this.HandleErrorSettingUpProxySession(SecurityStatus.IncompleteCredentials, false);
				return;
			}
			this.logSession.LogCertificateThumbprint("Received certificate", this.connection.RemoteCertificate);
			this.secureState &= ~SecureState.NegotiationRequested;
			if (this.TlsConfiguration.RequireTls && this.connection.TlsCipherKeySize < 128)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<int>((long)this.GetHashCode(), "Quit proxy session because Tls cipher strength is too weak at {0}", this.connection.TlsCipherKeySize);
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Tls cipher strength is too weak");
				this.FailoverConnection(SmtpResponse.AuthTempFailureTLSCipherTooWeak, SessionSetupFailureReason.ProtocolError);
				this.NextState = SmtpOutSession.SessionState.Quit;
				this.MoveToNextState();
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Remote has supplied certificate {0}", this.connection.RemoteCertificate.Subject);
			ChainValidityStatus chainValidityStatus = ChainValidityStatus.Valid;
			if (this.TlsConfiguration.RequireTls && (this.TlsConfiguration.TlsAuthLevel == RequiredTlsAuthLevel.CertificateValidation || this.TlsConfiguration.TlsAuthLevel == RequiredTlsAuthLevel.DomainValidation))
			{
				RequiredTlsAuthLevel valueOrDefault = this.TlsConfiguration.TlsAuthLevel.GetValueOrDefault();
				RequiredTlsAuthLevel? requiredTlsAuthLevel;
				if (requiredTlsAuthLevel != null)
				{
					switch (valueOrDefault)
					{
					case RequiredTlsAuthLevel.CertificateValidation:
						chainValidityStatus = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate, this.transportAppConfig.SmtpSendConfiguration.CacheOnlyUrlRetrievalForRemoteCertChain);
						break;
					case RequiredTlsAuthLevel.DomainValidation:
						if (!SmtpOutSession.MatchCertificateWithTlsDomain(this.TlsConfiguration.TlsDomains, this.connection.RemoteCertificate, this.logSession, this.certificateValidator))
						{
							chainValidityStatus = ChainValidityStatus.SubjectMismatch;
						}
						else
						{
							chainValidityStatus = this.certificateValidator.ChainValidateAsAnonymous(this.connection.RemoteCertificate, this.transportAppConfig.SmtpSendConfiguration.CacheOnlyUrlRetrievalForRemoteCertChain);
						}
						break;
					}
				}
			}
			if (chainValidityStatus != ChainValidityStatus.Valid)
			{
				string arg = chainValidityStatus.ToString();
				ExTraceGlobals.SmtpSendTracer.TraceError<string, string>((long)this.GetHashCode(), "Outbound TLS authentication failed with error {0} for proxy session due to certificate validation error.Target is {1}.", arg, this.connection.RemoteEndPoint.Address.ToString());
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, string.Format("outbound TLS authentication failed for the proxy session due to certificate validation error. ChainValidityStatus = {0}.", arg));
				if (!this.certificateValidator.ShouldTreatValidationResultAsSuccess(chainValidityStatus))
				{
					this.FailoverConnection(SmtpResponse.CertificateValidationFailure, SessionSetupFailureReason.ProtocolError);
					this.NextState = SmtpOutSession.SessionState.Quit;
					this.MoveToNextState();
					return;
				}
				SmtpOutConnection.Events.LogEvent(TransportEventLogConstants.Tuple_CertificateRevocationListCheckTrasientFailureTreatedAsSuccess, this.connection.RemoteCertificate.SerialNumber, new object[]
				{
					chainValidityStatus.ToString(),
					this.connection.RemoteCertificate.SerialNumber,
					this.connection.RemoteCertificate.Subject,
					this.connection.RemoteCertificate.Issuer,
					this.connection.RemoteCertificate.Thumbprint,
					"SmtpOutProxy"
				});
				this.logSession.LogCertificate(string.Format(CultureInfo.InvariantCulture, "Treating CRL chain validation failure {0} as success.", new object[]
				{
					chainValidityStatus
				}), this.connection.RemoteCertificate);
			}
			this.logSession.LogCertificate("Proxy target certificate", this.connection.RemoteCertificate);
			this.NextState = SmtpOutSession.SessionState.Ehlo;
			this.MoveToNextState();
		}

		// Token: 0x04001DA2 RID: 7586
		private const int MaxResponseLength = 32768;

		// Token: 0x04001DA3 RID: 7587
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x04001DA4 RID: 7588
		public readonly ISmtpInSession InSession;

		// Token: 0x04001DA5 RID: 7589
		public readonly SmtpSendConnectorConfig Connector;

		// Token: 0x04001DA6 RID: 7590
		private static readonly SmtpResponse GenericProxyFailedResponse = new SmtpResponse("401", "4.5.4", new string[]
		{
			"Proxy failed"
		});

		// Token: 0x04001DA7 RID: 7591
		private static readonly AsyncCallback writeCompleteReadLine = new AsyncCallback(SmtpOutProxySession.WriteCompleteReadLine);

		// Token: 0x04001DA8 RID: 7592
		private static readonly AsyncCallback tlsNegotiationComplete = new AsyncCallback(SmtpOutProxySession.TlsNegotiationComplete);

		// Token: 0x04001DA9 RID: 7593
		private static readonly AsyncCallback readLineComplete = new AsyncCallback(SmtpOutProxySession.ReadLineComplete);

		// Token: 0x04001DAA RID: 7594
		private readonly SmtpOutConnection smtpOutConnection;

		// Token: 0x04001DAB RID: 7595
		private NetworkConnection connection;

		// Token: 0x04001DAC RID: 7596
		private SmtpCommand currentCommand;

		// Token: 0x04001DAD RID: 7597
		private List<string> response;

		// Token: 0x04001DAE RID: 7598
		private SecureState secureState;

		// Token: 0x04001DAF RID: 7599
		private X509Certificate2 advertisedTlsCertificate;

		// Token: 0x04001DB0 RID: 7600
		private bool isAuthenticated;

		// Token: 0x04001DB1 RID: 7601
		private IProtocolLogSession logSession;

		// Token: 0x04001DB2 RID: 7602
		private bool disconnected;

		// Token: 0x04001DB3 RID: 7603
		private SmtpOutSession.SessionState currentState;

		// Token: 0x04001DB4 RID: 7604
		private SmtpOutSession.SessionState nextState;

		// Token: 0x04001DB5 RID: 7605
		private SmtpSessionProps sessionProps;

		// Token: 0x04001DB6 RID: 7606
		private long connectionId;

		// Token: 0x04001DB7 RID: 7607
		private BufferBuilder responseBuffer;

		// Token: 0x04001DB8 RID: 7608
		private BufferBuilder sendBuffer = new BufferBuilder();

		// Token: 0x04001DB9 RID: 7609
		private Breadcrumbs<SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs> breadcrumbs = new Breadcrumbs<SmtpOutProxySession.SmtpOutProxySessionBreadcrumbs>(64);

		// Token: 0x04001DBA RID: 7610
		private ITransportConfiguration transportConfiguration;

		// Token: 0x04001DBB RID: 7611
		private TransportAppConfig transportAppConfig;

		// Token: 0x04001DBC RID: 7612
		private CertificateValidator certificateValidator;

		// Token: 0x04001DBD RID: 7613
		private bool isProxying;

		// Token: 0x04001DBE RID: 7614
		private bool failoverInProgress;

		// Token: 0x04001DBF RID: 7615
		private SmtpResponse blindProxySuccessfulInboundResponse = SmtpOutProxySession.GenericProxyFailedResponse;

		// Token: 0x020004FE RID: 1278
		public enum SmtpOutProxySessionBreadcrumbs
		{
			// Token: 0x04001DC2 RID: 7618
			EMPTY,
			// Token: 0x04001DC3 RID: 7619
			CreateCmdConnectResponse,
			// Token: 0x04001DC4 RID: 7620
			CreateCmdEhlo,
			// Token: 0x04001DC5 RID: 7621
			CreateCmdAuth,
			// Token: 0x04001DC6 RID: 7622
			CreateCmdStarttls,
			// Token: 0x04001DC7 RID: 7623
			CreateCmdXProxy,
			// Token: 0x04001DC8 RID: 7624
			Disconnect,
			// Token: 0x04001DC9 RID: 7625
			CreateCmdQuit,
			// Token: 0x04001DCA RID: 7626
			EnqueueResponseHandler,
			// Token: 0x04001DCB RID: 7627
			MoveToNextState,
			// Token: 0x04001DCC RID: 7628
			SendCurrentCommand,
			// Token: 0x04001DCD RID: 7629
			SendBufferThenReadLine,
			// Token: 0x04001DCE RID: 7630
			InvokeCommandHandler,
			// Token: 0x04001DCF RID: 7631
			InvokeResponseHandler,
			// Token: 0x04001DD0 RID: 7632
			TlsNegotiationComplete,
			// Token: 0x04001DD1 RID: 7633
			ShutdownConnection,
			// Token: 0x04001DD2 RID: 7634
			CreateCmdHelo
		}
	}
}
