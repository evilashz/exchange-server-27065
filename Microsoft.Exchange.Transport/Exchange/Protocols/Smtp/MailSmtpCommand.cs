using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200045E RID: 1118
	internal class MailSmtpCommand : SmtpCommand
	{
		// Token: 0x060033B2 RID: 13234 RVA: 0x000CFC42 File Offset: 0x000CDE42
		public MailSmtpCommand(ISmtpSession session, ITransportAppConfig transportAppConfig) : base(session, "MAIL", "OnMailCommand", LatencyComponent.None)
		{
			base.IsResponseBuffered = true;
			this.mailCommandEventArgs = new MailCommandEventArgs();
			this.CommandEventArgs = this.mailCommandEventArgs;
			this.transportAppConfig = transportAppConfig;
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000CFC7B File Offset: 0x000CDE7B
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x000CFC88 File Offset: 0x000CDE88
		internal RoutingAddress FromAddress
		{
			get
			{
				return this.mailCommandEventArgs.FromAddress;
			}
			set
			{
				this.mailCommandEventArgs.FromAddress = value;
			}
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000CFC96 File Offset: 0x000CDE96
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000CFCA3 File Offset: 0x000CDEA3
		internal long Size
		{
			get
			{
				return this.mailCommandEventArgs.Size;
			}
			set
			{
				this.mailCommandEventArgs.Size = value;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000CFCB1 File Offset: 0x000CDEB1
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x000CFCC3 File Offset: 0x000CDEC3
		internal Microsoft.Exchange.Transport.BodyType BodyType
		{
			get
			{
				return EnumConverter.PublicToInternal(this.mailCommandEventArgs.BodyType);
			}
			set
			{
				this.mailCommandEventArgs.BodyType = EnumConverter.InternalToPublic(value);
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000CFCD6 File Offset: 0x000CDED6
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000CFCE8 File Offset: 0x000CDEE8
		internal DsnFormat Ret
		{
			get
			{
				return EnumConverter.PublicToInternal(this.mailCommandEventArgs.DsnFormatRequested);
			}
			set
			{
				this.mailCommandEventArgs.DsnFormatRequested = EnumConverter.InternalToPublic(value);
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x060033BB RID: 13243 RVA: 0x000CFCFB File Offset: 0x000CDEFB
		// (set) Token: 0x060033BC RID: 13244 RVA: 0x000CFD08 File Offset: 0x000CDF08
		internal string EnvId
		{
			get
			{
				return this.mailCommandEventArgs.EnvelopeId;
			}
			set
			{
				this.mailCommandEventArgs.EnvelopeId = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000CFD16 File Offset: 0x000CDF16
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000CFD23 File Offset: 0x000CDF23
		internal string Auth
		{
			get
			{
				return this.mailCommandEventArgs.Auth;
			}
			set
			{
				this.mailCommandEventArgs.Auth = value;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000CFD31 File Offset: 0x000CDF31
		// (set) Token: 0x060033C0 RID: 13248 RVA: 0x000CFD39 File Offset: 0x000CDF39
		internal string XShadow
		{
			get
			{
				return this.xshadow;
			}
			set
			{
				this.xshadow = value;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000CFD42 File Offset: 0x000CDF42
		// (set) Token: 0x060033C2 RID: 13250 RVA: 0x000CFD4F File Offset: 0x000CDF4F
		internal string Oorg
		{
			get
			{
				return this.mailCommandEventArgs.Oorg;
			}
			set
			{
				this.mailCommandEventArgs.Oorg = value;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000CFD5D File Offset: 0x000CDF5D
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x000CFD6A File Offset: 0x000CDF6A
		internal Guid SystemProbeId
		{
			get
			{
				return this.mailCommandEventArgs.SystemProbeId;
			}
			set
			{
				this.mailCommandEventArgs.SystemProbeId = value;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000CFD78 File Offset: 0x000CDF78
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000CFD85 File Offset: 0x000CDF85
		internal RoutingAddress OriginalFromAddress
		{
			get
			{
				return this.mailCommandEventArgs.OriginalFromAddress;
			}
			set
			{
				this.mailCommandEventArgs.OriginalFromAddress = value;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000CFD93 File Offset: 0x000CDF93
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000CFDA0 File Offset: 0x000CDFA0
		internal bool SmtpUtf8
		{
			get
			{
				return this.mailCommandEventArgs.SmtpUtf8;
			}
			set
			{
				this.mailCommandEventArgs.SmtpUtf8 = value;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x060033C9 RID: 13257 RVA: 0x000CFDAE File Offset: 0x000CDFAE
		// (set) Token: 0x060033CA RID: 13258 RVA: 0x000CFDBB File Offset: 0x000CDFBB
		internal Dictionary<string, string> ConsumerMailOptionalArguments
		{
			get
			{
				return this.mailCommandEventArgs.ConsumerMailOptionalArguments;
			}
			set
			{
				this.mailCommandEventArgs.ConsumerMailOptionalArguments = value;
			}
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000CFDCC File Offset: 0x000CDFCC
		internal static void FormatCommand(StringBuilder mailFromLine, RoutingAddress fromAddress, IEhloOptions ehloOptions, TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, bool usingHelo, long size, string auth, string envId, DsnFormat ret, Microsoft.Exchange.Transport.BodyType bodyType, MailDirectionality directionality, Guid externalOrganizationId, OrganizationId internalOrganizationId, string exoAccountForest, string exoTenantContainer)
		{
			MailSmtpCommand.FormatCommand(mailFromLine, null, fromAddress, ehloOptions, mailCommandConfig, usingHelo, size, auth, null, envId, ret, bodyType, null, null, directionality, externalOrganizationId, internalOrganizationId, exoAccountForest, exoTenantContainer, Guid.Empty, string.Empty, RoutingAddress.Empty, false);
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000CFE0C File Offset: 0x000CE00C
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.MailInboundParseCommand);
			CommandContext commandContext = CommandContext.FromSmtpCommand(this);
			SmtpInSessionState smtpInSessionState = SmtpInSessionState.FromSmtpInSession(smtpInSession);
			TimeSpan tarpitInterval = base.TarpitInterval;
			bool flag = false;
			RoutingAddress routingAddress;
			MailParseOutput mailParseOutput;
			ParseResult parseResult = MailSmtpCommandParser.Parse(commandContext, smtpInSessionState, smtpInSession.SmtpInServer.CertificateValidator, smtpInSession.IsDataRedactionNecessary, new MailSmtpCommandParser.ValidateSequence(this.IsValidSequence), new MailSmtpCommandParser.UpdateState(this.ResetState), new MailSmtpCommandParser.IsPoisonMessage(PoisonMessage.IsMessagePoison), smtpInSessionState.EventLog, new MailSmtpCommandParser.PublishNotification(EventNotificationItem.Publish), ref flag, ref tarpitInterval, out routingAddress, out mailParseOutput);
			base.SmtpResponse = parseResult.SmtpResponse;
			base.ParsingStatus = parseResult.ParsingStatus;
			if (flag)
			{
				smtpInSession.Disconnect(DisconnectReason.Local);
			}
			if (routingAddress != RoutingAddress.Empty && routingAddress.IsValid)
			{
				this.FromAddress = routingAddress;
			}
			if (parseResult.IsFailed)
			{
				base.TarpitInterval = tarpitInterval;
				return;
			}
			base.CurrentOffset = commandContext.Offset;
			this.Size = mailParseOutput.Size;
			this.BodyType = mailParseOutput.MailBodyType;
			this.Ret = mailParseOutput.DsnFormat;
			this.EnvId = mailParseOutput.EnvelopeId;
			this.Auth = mailParseOutput.Auth;
			this.XShadow = mailParseOutput.XShadow;
			this.Oorg = mailParseOutput.Oorg;
			this.messageContext = mailParseOutput.MessageContextParameters;
			this.directionality = mailParseOutput.Directionality;
			this.SystemProbeId = mailParseOutput.SystemProbeId;
			this.internetMessageId = mailParseOutput.InternetMessageId;
			this.shadowMessageId = mailParseOutput.ShadowMessageId;
			this.OriginalFromAddress = mailParseOutput.OriginalFromAddress;
			this.SmtpUtf8 = mailParseOutput.SmtpUtf8;
			this.ConsumerMailOptionalArguments = mailParseOutput.ConsumerMailOptionalArguments;
			if (mailParseOutput.XAttrOrgId != null)
			{
				this.externalOrganizationId = mailParseOutput.XAttrOrgId.ExternalOrgId;
				this.internalOrganizationId = mailParseOutput.XAttrOrgId.InternalOrgId;
				this.exoAccountForest = mailParseOutput.XAttrOrgId.ExoAccountForest;
				this.exoTenantContainer = mailParseOutput.XAttrOrgId.ExoTenantContainer;
			}
			smtpInSession.RemoteIdentity = smtpInSessionState.RemoteIdentity;
			smtpInSession.RemoteIdentityName = smtpInSessionState.RemoteIdentityName;
			smtpInSession.SessionPermissions = smtpInSessionState.SessionPermissions;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000D0028 File Offset: 0x000CE228
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.MailInboundProcessCommand);
			if (Components.SmtpInComponent.TargetRunningState == ServiceState.Inactive && this.SystemProbeId == Guid.Empty)
			{
				base.SmtpResponse = SmtpResponse.ServiceInactive;
				smtpInSession.LogInformation(ProtocolLoggingLevel.Verbose, "Rejecting the non-probe message and disconnecting as transport service is Inactive.", null);
				smtpInSession.Disconnect(DisconnectReason.Local);
				return;
			}
			string arg;
			if (this.externalOrganizationId != Guid.Empty && smtpInSession.QueueQuotaComponent != null && !smtpInSession.IsPeerShadowSession && smtpInSession.QueueQuotaComponent.IsOrganizationOverQuota(this.exoAccountForest, this.externalOrganizationId, this.GetThrottledAddress(), out arg))
			{
				base.SmtpResponse = SmtpResponse.OrgQueueQuotaExceeded;
				smtpInSession.LogInformation(ProtocolLoggingLevel.Verbose, string.Format("Rejecting the message QQ limit was exceeded. ({0})", arg), null);
				return;
			}
			smtpInSession.SmtpUtf8Supported = this.SmtpUtf8;
			SmtpResponse smtpResponse;
			if (smtpInSession.CreateTransportMailItem(this.internalOrganizationId, this.externalOrganizationId, this.directionality, this.exoAccountForest, this.exoTenantContainer, out smtpResponse))
			{
				smtpInSession.LogInformation(ProtocolLoggingLevel.Verbose, "receiving message", Encoding.ASCII.GetBytes(smtpInSession.CurrentMessageTemporaryId));
				if (!RoutingAddress.IsEmpty(this.OriginalFromAddress))
				{
					smtpInSession.TransportMailItem.From = this.OriginalFromAddress;
				}
				smtpInSession.TransportMailItem.DateReceived = DateTime.UtcNow;
				smtpInSession.TransportMailItem.From = this.FromAddress;
				smtpInSession.TransportMailItem.Auth = this.Auth;
				smtpInSession.TransportMailItem.EnvId = this.EnvId;
				smtpInSession.TransportMailItem.DsnFormat = this.Ret;
				smtpInSession.TransportMailItem.BodyType = this.BodyType;
				smtpInSession.TransportMailItem.Oorg = this.Oorg;
				smtpInSession.TransportMailItem.InternetMessageId = this.internetMessageId;
				LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceive, smtpInSession.TransportMailItem.LatencyTracker);
				if (!ConfigurationComponent.IsFrontEndTransportProcess(Components.Configuration))
				{
					LatencyComponent component = (smtpInSession.SessionSource.IsInboundProxiedSession || smtpInSession.SessionSource.IsClientProxiedSession) ? LatencyComponent.SmtpReceiveDataExternal : LatencyComponent.SmtpReceiveDataInternal;
					LatencyTracker.BeginTrackLatency(component, smtpInSession.TransportMailItem.LatencyTracker);
				}
				smtpInSession.SetupPoisonContext();
				smtpInSession.SetupExpectedBlobs(this.messageContext);
				smtpInSession.MailCommandMessageContextInformation = this.messageContext;
				smtpInSession.TransportMailItem.SystemProbeId = this.SystemProbeId;
				if (!string.IsNullOrEmpty(this.XShadow))
				{
					smtpInSession.TransportMailItem.ShadowServerDiscardId = this.XShadow;
					if (!smtpInSession.IsPeerShadowSession)
					{
						smtpInSession.TransportMailItem.ShadowServerContext = smtpInSession.SenderShadowContext;
					}
					if (this.shadowMessageId != Guid.Empty)
					{
						smtpInSession.TransportMailItem.ShadowMessageId = this.shadowMessageId;
					}
				}
				if (SmtpInSessionUtils.IsPartner(smtpInSession.RemoteIdentity))
				{
					smtpInSession.TransportMailItem.AuthMethod = MultilevelAuthMechanism.MutualTLS;
				}
				else if (SmtpInSessionUtils.IsExternalAuthoritative(smtpInSession.RemoteIdentity))
				{
					smtpInSession.TransportMailItem.AuthMethod = MultilevelAuthMechanism.SecureExternalSubmit;
				}
				else
				{
					smtpInSession.TransportMailItem.AuthMethod = smtpInSession.AuthMethod;
				}
				smtpInSession.TransportMailItem.HeloDomain = smtpInSession.HelloSmtpDomain;
				foreach (KeyValuePair<string, object> keyValuePair in this.mailCommandEventArgs.MailItemProperties)
				{
					smtpInSession.TransportMailItem.ExtendedProperties.SetValue<object>(keyValuePair.Key, keyValuePair.Value);
				}
				smtpInSession.TransportMailItem.ExtendedProperties.SetValue<ulong>("Microsoft.Exchange.Transport.SmtpInSessionId", smtpInSession.SessionId);
				if (smtpInSession.XProxyFromSeqNum > 0U)
				{
					smtpInSession.TransportMailItem.ExtendedProperties.SetValue<uint>("Microsoft.Exchange.Transport.TransportMailItem.InboundProxySequenceNumber", smtpInSession.XProxyFromSeqNum);
				}
				smtpInSession.TransportMailItemWrapper = new TransportMailItemWrapper(smtpInSession.TransportMailItem, smtpInSession.MexSession, true);
				base.SmtpResponse = SmtpResponse.MailFromOk;
			}
			else
			{
				base.SmtpResponse = smtpResponse;
			}
			TimeSpan throttleDelay = smtpInSession.SmtpInServer.ThrottleDelay;
			if (throttleDelay > TimeSpan.Zero)
			{
				base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
				base.HighAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
				base.TarpitInterval = throttleDelay;
				base.TarpitReason = "Back Pressure";
				base.TarpitContext = smtpInSession.SmtpInServer.ThrottleDelayContext;
			}
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000D0438 File Offset: 0x000CE638
		internal override void OutboundCreateCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			this.FromAddress = smtpOutSession.RoutedMailItem.From;
			this.Size = smtpOutSession.RoutedMailItem.MimeSize;
			if (smtpOutSession.RoutedMailItem.BodyType != Microsoft.Exchange.Transport.BodyType.Default)
			{
				this.BodyType = smtpOutSession.RoutedMailItem.BodyType;
				ExTraceGlobals.SmtpSendTracer.TraceDebug<Microsoft.Exchange.Transport.BodyType>((long)this.GetHashCode(), "BodyType: {0}", this.BodyType);
			}
			if (smtpOutSession.AdvertisedEhloOptions.AreAnyAuthMechanismsSupported() && !string.IsNullOrEmpty(smtpOutSession.RoutedMailItem.Auth))
			{
				this.Auth = smtpOutSession.RoutedMailItem.Auth;
			}
			if (smtpOutSession.ShadowCurrentMailItem)
			{
				if (smtpOutSession.RoutedMailItem.ShadowMessageId == Guid.Empty)
				{
					throw new InvalidOperationException(string.Format("MailItem {0} doesn't have a valid ShadowMessageId", smtpOutSession.RoutedMailItem.RecordId));
				}
				this.XShadow = smtpOutSession.RoutedMailItem.ShadowMessageId.ToString();
			}
			if (smtpOutSession.AdvertisedEhloOptions.Dsn)
			{
				if (!string.IsNullOrEmpty(smtpOutSession.RoutedMailItem.EnvId))
				{
					this.EnvId = smtpOutSession.RoutedMailItem.EnvId;
				}
				if (smtpOutSession.RoutedMailItem.DsnFormat == DsnFormat.Full)
				{
					this.Ret = DsnFormat.Full;
				}
				else if (smtpOutSession.RoutedMailItem.DsnFormat == DsnFormat.Headers)
				{
					this.Ret = DsnFormat.Headers;
				}
				else
				{
					this.Ret = DsnFormat.Default;
				}
			}
			if (smtpOutSession.AdvertisedEhloOptions.XOorg)
			{
				this.Oorg = smtpOutSession.RoutedMailItem.Oorg;
			}
			if (smtpOutSession.AdvertisedEhloOptions.XAttr)
			{
				this.directionality = smtpOutSession.RoutedMailItem.Directionality;
				this.externalOrganizationId = smtpOutSession.RoutedMailItem.ExternalOrganizationId;
				this.exoAccountForest = smtpOutSession.RoutedMailItem.ExoAccountForest;
				this.exoTenantContainer = smtpOutSession.RoutedMailItem.ExoTenantContainer;
				if ((smtpOutSession.Permissions & Permission.SendForestHeaders) != Permission.None)
				{
					this.internalOrganizationId = smtpOutSession.RoutedMailItem.OrganizationId;
				}
			}
			if (smtpOutSession.AdvertisedEhloOptions.XSysProbe)
			{
				this.SystemProbeId = smtpOutSession.RoutedMailItem.SystemProbeId;
			}
			if (smtpOutSession.AdvertisedEhloOptions.XOrigFrom && !this.FromAddress.Equals(smtpOutSession.RoutedMailItem.OriginalFrom))
			{
				this.OriginalFromAddress = smtpOutSession.RoutedMailItem.OriginalFrom;
			}
			if (smtpOutSession.AdvertisedEhloOptions.SmtpUtf8)
			{
				this.SmtpUtf8 = this.ShouldSendOutboundSmtpUtf8(this.FromAddress, smtpOutSession.RoutedMailItem);
			}
			smtpOutSession.SetupBlobsToSend();
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000D06B0 File Offset: 0x000CE8B0
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (smtpOutSession.RoutedMailItem != null)
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3754306877U, smtpOutSession.RoutedMailItem.Subject);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2949000509U, smtpOutSession.RoutedMailItem.Subject);
			}
			if (smtpOutSession.TlsConfiguration.RequireTls && smtpOutSession.SecureState == SecureState.None)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Connector is configured to send mail only over TLS connections and we didn't achieve it");
				string nextHopDomain = smtpOutSession.NextHopDomain;
				SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpSendTLSRequiredFailed, smtpOutSession.Connector.Name, new object[]
				{
					smtpOutSession.Connector.Name,
					nextHopDomain,
					smtpOutSession.AdvertisedEhloOptions.AdvertisedFQDN
				});
				string notificationReason = string.Format("Send connector {0} couldn't connect to remote domain {1}. The send connector requires Transport Layer Security (TLS) authentication, but is unable to establish TLS with the receiving server for the remote domain. Check this connector's authentication setting and the EHLO response from the remote server {2}.", smtpOutSession.Connector.Name, nextHopDomain, smtpOutSession.AdvertisedEhloOptions.AdvertisedFQDN);
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "SmtpSendTLSRequiredFailed", null, notificationReason, ResultSeverityLevel.Error, false);
				smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Drop connection because TLS configuration requires TLS before MailFrom command and could not achieve it, check connection authentication setting or remote capabilities");
				smtpOutSession.FailoverConnection(SmtpResponse.RequireTLSToSendMail);
				smtpOutSession.SetNextStateToQuit();
				return;
			}
			if (smtpOutSession.RoutedMailItem == null)
			{
				throw new InvalidOperationException("Must have a message object obtained in PrepareForNextMessage");
			}
			RoutingAddress shortAddress = smtpOutSession.GetShortAddress(this.FromAddress);
			smtpOutSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "sending message with RecordId {0} and InternetMessageId {1}", new object[]
			{
				smtpOutSession.RoutedMailItem.RecordId.ToString(NumberFormatInfo.InvariantInfo),
				smtpOutSession.RoutedMailItem.InternetMessageId
			});
			StringBuilder stringBuilder = new StringBuilder("MAIL FROM:");
			StringBuilder stringBuilder2 = null;
			if (Util.IsDataRedactionNecessary())
			{
				stringBuilder2 = new StringBuilder("MAIL FROM:");
			}
			MailSmtpCommand.FormatCommand(stringBuilder, stringBuilder2, shortAddress, smtpOutSession.AdvertisedEhloOptions, this.transportAppConfig.SmtpMailCommandConfiguration, smtpOutSession.UsingHELO, this.Size, this.Auth, this.XShadow, this.EnvId, this.Ret, this.BodyType, this.Oorg, smtpOutSession.BlobsToSend, this.directionality, this.externalOrganizationId, this.internalOrganizationId, this.exoAccountForest, this.exoTenantContainer, this.SystemProbeId, smtpOutSession.RoutedMailItem.InternetMessageId, this.OriginalFromAddress, this.SmtpUtf8);
			base.ProtocolCommandString = stringBuilder.ToString();
			base.RedactedProtocolCommandString = ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted mail command: {0}", base.ProtocolCommandString);
			this.outboundStopwatch = new Stopwatch();
			this.outboundStopwatch.Start();
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000D094C File Offset: 0x000CEB4C
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			this.outboundStopwatch.Stop();
			if (smtpOutSession.NextHopConnection == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Connection already marked for Failover.  Not processing response for MAIL command: {0}", base.SmtpResponse);
				return;
			}
			if (smtpOutSession.PipeLineFailOverPending)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Fail over initiated by prior responses to pipelined commands. Not processing response for MAIL command : {0}", base.SmtpResponse);
				smtpOutSession.FailoverConnection(base.SmtpResponse, false);
				return;
			}
			if (smtpOutSession.RecipientsAckedPending)
			{
				smtpOutSession.RecipientsAckedPending = false;
			}
			if (smtpOutSession.NextHopDeliveryType == DeliveryType.SmtpDeliveryToMailbox && base.SmtpResponse.Equals(SmtpResponse.TooManyRelatedErrors))
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, SmtpResponse>((long)this.GetHashCode(), "MAIL FROM {0} failed with: {1}", smtpOutSession.RoutedMailItem.From.ToString(), base.SmtpResponse);
				((RoutedMailItem)smtpOutSession.RoutedMailItem).Poison();
				smtpOutSession.AckMessage(AckStatus.Fail, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			if ((base.SmtpResponse.SmtpResponseType == SmtpResponseType.PermanentError && !Util.DowngradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)) || (base.SmtpResponse.SmtpResponseType == SmtpResponseType.TransientError && Util.UpgradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)))
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, SmtpResponse>((long)this.GetHashCode(), "MAIL FROM {0} failed with: {1}", smtpOutSession.RoutedMailItem.From.ToString(), base.SmtpResponse);
				smtpOutSession.AckMessage(AckStatus.Fail, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			if (statusCode[0] != '2')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, SmtpResponse>((long)this.GetHashCode(), "MAIL FROM {0} failed with: {1}", smtpOutSession.RoutedMailItem.From.ToString(), base.SmtpResponse);
				smtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "MAIL command for {0} succeeded, will issue RCPT", smtpOutSession.RoutedMailItem.From.ToString());
			smtpOutSession.NextState = SmtpOutSession.SessionState.PerRecipient;
			smtpOutSession.UpdateServerLatency(this.outboundStopwatch.Elapsed);
			this.outboundStopwatch = null;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000D0BB0 File Offset: 0x000CEDB0
		private static void FormatCommand(StringBuilder mailFromLine, StringBuilder redactedMailFromLine, RoutingAddress fromAddress, IEhloOptions ehloOptions, TransportAppConfig.ISmtpMailCommandConfig mailCommandConfig, bool usingHelo, long size, string auth, string xshadow, string envId, DsnFormat ret, Microsoft.Exchange.Transport.BodyType bodyType, string oorg, IEnumerable<SmtpMessageContextBlob> messageContextBlobs, MailDirectionality directionality, Guid externalOrganizationId, OrganizationId internalOrganizationId, string exoAccountForest, string exoTenantContainer, Guid systemProbeId, string internetMessageId, RoutingAddress originalFromAddress, bool smtpUtf8)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = ehloOptions != null && ehloOptions.SmtpUtf8 && smtpUtf8;
			if (ehloOptions != null && ehloOptions.Size == SizeMode.Enabled)
			{
				stringBuilder.Append(" SIZE=" + size);
			}
			if ((ehloOptions == null || ehloOptions.AreAnyAuthMechanismsSupported()) && !string.IsNullOrEmpty(auth))
			{
				stringBuilder.Append(" AUTH=" + SmtpUtils.ToXtextString(auth, false));
			}
			if (!string.IsNullOrEmpty(xshadow))
			{
				stringBuilder.Append(" XSHADOW=" + SmtpUtils.ToXtextString(xshadow, false));
			}
			if (ehloOptions == null || ehloOptions.Dsn)
			{
				if (!string.IsNullOrEmpty(envId))
				{
					stringBuilder.Append(" ENVID=" + SmtpUtils.ToXtextString(envId, false));
				}
				if (ret == DsnFormat.Full)
				{
					stringBuilder.Append(" RET=FULL");
				}
				else if (ret == DsnFormat.Headers)
				{
					stringBuilder.Append(" RET=HDRS");
				}
			}
			switch (bodyType)
			{
			case Microsoft.Exchange.Transport.BodyType.SevenBit:
				if (ehloOptions == null || (!usingHelo && (ehloOptions.EightBitMime || ehloOptions.BinaryMime)))
				{
					stringBuilder.Append(" BODY=7BIT");
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Body Type: 7 BIT MIME");
				}
				else
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Using HELO. Body Type Not set");
				}
				break;
			case Microsoft.Exchange.Transport.BodyType.EightBitMIME:
				if (ehloOptions != null && !ehloOptions.EightBitMime)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Message body is 8 bit MIME, but 8 Bit MIME not advertised by remote host. Use 7 bit");
					if (!usingHelo && ehloOptions.BinaryMime)
					{
						stringBuilder.Append(" BODY=7BIT");
					}
				}
				else
				{
					stringBuilder.Append(" BODY=8BITMIME");
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Body Type: 8 BIT MIME");
				}
				break;
			case Microsoft.Exchange.Transport.BodyType.BinaryMIME:
				if (ehloOptions != null && !ehloOptions.BinaryMime)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Message body is Binary MIME, but Binary MIME not advertised by remote host. Use 7 bit");
					if (!usingHelo && ehloOptions.EightBitMime)
					{
						stringBuilder.Append(" BODY=7BIT");
					}
				}
				else if (ehloOptions != null && !ehloOptions.Chunking)
				{
					ExTraceGlobals.GeneralTracer.TraceError(0L, "Binary MIME is advertised by remote host, but CHUNKING is not. Cannot send Binary MIME with DATA. Downconverting message to 7 bit");
					if (!usingHelo)
					{
						stringBuilder.Append(" BODY=7BIT");
					}
				}
				else
				{
					stringBuilder.Append(" BODY=BINARYMIME");
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Body Type: BINARY MIME");
				}
				break;
			default:
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Body Type: Not set");
				break;
			}
			if (ehloOptions != null && ehloOptions.XOorg && !string.IsNullOrEmpty(oorg))
			{
				stringBuilder.Append(" XOORG=" + SmtpUtils.ToXtextString(oorg, false));
			}
			if (ehloOptions != null && (ehloOptions.XAdrc || ehloOptions.XExprops || ehloOptions.XFastIndex) && messageContextBlobs != null && ((ICollection)messageContextBlobs).Count != 0)
			{
				stringBuilder.Append(" XMESSAGECONTEXT=" + MailSmtpCommand.GetMessageContext(messageContextBlobs, ehloOptions));
			}
			if (ehloOptions != null && ehloOptions.XAttr && directionality != MailDirectionality.Undefined && externalOrganizationId != Guid.Empty)
			{
				stringBuilder.Append(" XATTRDIRECT=" + SmtpUtils.ToXtextString(MailSmtpCommandParser.XAttrHelper.GetDirectionalityString(directionality), false));
				stringBuilder.Append(" XATTRORGID=" + SmtpUtils.ToXtextString(MailSmtpCommandParser.XAttrHelper.GetOrgIdString(mailCommandConfig, externalOrganizationId, internalOrganizationId, exoAccountForest, exoTenantContainer), false));
			}
			if (ehloOptions != null && ehloOptions.XSysProbe && systemProbeId != Guid.Empty)
			{
				stringBuilder.Append(" XSYSPROBEID=" + SmtpUtils.ToXtextString(systemProbeId.ToString(), false));
			}
			if (ehloOptions != null && ehloOptions.XMsgId && !string.IsNullOrEmpty(internetMessageId) && internetMessageId.Length <= 500)
			{
				stringBuilder.Append(" XMSGID=" + SmtpUtils.ToXtextString(internetMessageId, false));
			}
			if ((ehloOptions != null & ehloOptions.XOrigFrom) && !RoutingAddress.IsEmpty(originalFromAddress))
			{
				stringBuilder.Append(" XORIGFROM=" + SmtpUtils.ToXtextString(originalFromAddress.ToString(), flag));
			}
			if (flag)
			{
				stringBuilder.Append(" SMTPUTF8");
			}
			string value = stringBuilder.ToString();
			string emailAddress = fromAddress.ToString();
			mailFromLine.Append(SmtpCommand.GetBracketedString(emailAddress));
			mailFromLine.Append(value);
			if (redactedMailFromLine != null)
			{
				redactedMailFromLine.Append(SmtpCommand.GetBracketedString(Util.Redact(fromAddress)));
				redactedMailFromLine.Append(value);
			}
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000D0FE0 File Offset: 0x000CF1E0
		private static string GetMessageContext(IEnumerable<SmtpMessageContextBlob> messageContext, IEhloOptions ehloOptions)
		{
			StringBuilder stringBuilder = null;
			foreach (SmtpMessageContextBlob smtpMessageContextBlob in messageContext)
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				else
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(smtpMessageContextBlob.GetVersionToSend(ehloOptions));
			}
			if (stringBuilder == null)
			{
				throw new InvalidOperationException("Expecting atleast one blob to be present, but did not find any.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000D105C File Offset: 0x000CF25C
		private void ResetState(SmtpInSessionState sessionState)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.ResetMailItemPermissions();
			smtpInSession.ResetExpectedBlobs();
			smtpInSession.SeenRcpt2 = false;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000D1088 File Offset: 0x000CF288
		private ParseResult IsValidSequence()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!base.VerifyHelloReceived() || !base.VerifyNoOngoingBdat())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return new ParseResult(base.ParsingStatus, base.SmtpResponse, false);
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000D10D4 File Offset: 0x000CF2D4
		private string GetThrottledAddress()
		{
			if (this.OriginalFromAddress != RoutingAddress.Empty)
			{
				return this.OriginalFromAddress.ToString();
			}
			return this.FromAddress.ToString();
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000D1138 File Offset: 0x000CF338
		private bool ShouldSendOutboundSmtpUtf8(RoutingAddress sender, IReadOnlyMailItem mailItem)
		{
			bool flag = sender.IsUTF8;
			if (!flag)
			{
				flag = mailItem.Recipients.Any((MailRecipient recipient) => recipient.Email.IsUTF8);
			}
			return flag;
		}

		// Token: 0x04001A2B RID: 6699
		private const string XShadowKeyword = "XSHADOW";

		// Token: 0x04001A2C RID: 6700
		private const string DirectionalityParam = "XATTRDIRECT";

		// Token: 0x04001A2D RID: 6701
		private const string OrganizationIdParam = "XATTRORGID";

		// Token: 0x04001A2E RID: 6702
		private const string SystemProbeIdParam = "XSYSPROBEID";

		// Token: 0x04001A2F RID: 6703
		private const string MessageIdParam = "XMSGID";

		// Token: 0x04001A30 RID: 6704
		private const string OriginalFromParam = "XORIGFROM";

		// Token: 0x04001A31 RID: 6705
		private readonly MailCommandEventArgs mailCommandEventArgs;

		// Token: 0x04001A32 RID: 6706
		private string xshadow;

		// Token: 0x04001A33 RID: 6707
		private Guid shadowMessageId;

		// Token: 0x04001A34 RID: 6708
		private readonly ITransportAppConfig transportAppConfig;

		// Token: 0x04001A35 RID: 6709
		private MailCommandMessageContextParameters messageContext;

		// Token: 0x04001A36 RID: 6710
		private MailDirectionality directionality;

		// Token: 0x04001A37 RID: 6711
		private Guid externalOrganizationId;

		// Token: 0x04001A38 RID: 6712
		private OrganizationId internalOrganizationId;

		// Token: 0x04001A39 RID: 6713
		private string exoAccountForest;

		// Token: 0x04001A3A RID: 6714
		private string exoTenantContainer;

		// Token: 0x04001A3B RID: 6715
		private string internetMessageId;

		// Token: 0x04001A3C RID: 6716
		private Stopwatch outboundStopwatch;
	}
}
