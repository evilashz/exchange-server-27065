using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000463 RID: 1123
	internal class RcptSmtpCommand : SmtpCommand
	{
		// Token: 0x060033F8 RID: 13304 RVA: 0x000D18A8 File Offset: 0x000CFAA8
		public RcptSmtpCommand(ISmtpSession session, RecipientCorrelator recipientCorrelator, ITransportAppConfig transportAppConfig) : base(session, "RCPT", "OnRcptCommand", LatencyComponent.SmtpReceiveOnRcptCommand)
		{
			base.IsResponseBuffered = true;
			this.rcptCommandEventArgs = new RcptCommandEventArgs();
			this.CommandEventArgs = this.rcptCommandEventArgs;
			this.transportAppConfig = transportAppConfig;
			ISmtpInSession smtpInSession = session as ISmtpInSession;
			if (smtpInSession != null)
			{
				this.rcptCommandEventArgs.MailItem = smtpInSession.TransportMailItemWrapper;
			}
			this.recipientCorrelator = recipientCorrelator;
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x060033F9 RID: 13305 RVA: 0x000D190F File Offset: 0x000CFB0F
		// (set) Token: 0x060033FA RID: 13306 RVA: 0x000D191C File Offset: 0x000CFB1C
		internal RoutingAddress RecipientAddress
		{
			get
			{
				return this.rcptCommandEventArgs.RecipientAddress;
			}
			set
			{
				this.rcptCommandEventArgs.RecipientAddress = value;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x060033FB RID: 13307 RVA: 0x000D192A File Offset: 0x000CFB2A
		// (set) Token: 0x060033FC RID: 13308 RVA: 0x000D193C File Offset: 0x000CFB3C
		internal DsnRequestedFlags Notify
		{
			get
			{
				return EnumConverter.PublicToInternal(this.rcptCommandEventArgs.Notify);
			}
			set
			{
				this.rcptCommandEventArgs.Notify = EnumConverter.InternalToPublic(value);
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000D194F File Offset: 0x000CFB4F
		// (set) Token: 0x060033FE RID: 13310 RVA: 0x000D195C File Offset: 0x000CFB5C
		internal string ORcpt
		{
			get
			{
				return this.rcptCommandEventArgs.OriginalRecipient;
			}
			set
			{
				this.rcptCommandEventArgs.OriginalRecipient = value;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x000D196A File Offset: 0x000CFB6A
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x000D1972 File Offset: 0x000CFB72
		internal RoutingAddress Orar
		{
			get
			{
				return this.orar;
			}
			set
			{
				this.orar = value;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000D197B File Offset: 0x000CFB7B
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x000D1983 File Offset: 0x000CFB83
		internal string RDst
		{
			get
			{
				return this.rdst;
			}
			set
			{
				this.rdst = value;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06003403 RID: 13315 RVA: 0x000D198C File Offset: 0x000CFB8C
		// (set) Token: 0x06003404 RID: 13316 RVA: 0x000D1999 File Offset: 0x000CFB99
		internal Dictionary<string, string> ConsumerMailOptionalArguments
		{
			get
			{
				return this.rcptCommandEventArgs.ConsumerMailOptionalArguments;
			}
			set
			{
				this.rcptCommandEventArgs.ConsumerMailOptionalArguments = value;
			}
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000D19A8 File Offset: 0x000CFBA8
		internal static void FormatCommand(StringBuilder rcptCommand, StringBuilder redactedRcptCommand, RoutingAddress recipientAddress, IEhloOptions ehloOptions, string orcpt, DsnRequestedFlags dsnNotifyFlags, RoutingAddress orar, string rdst, bool smtpUtf8)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			if (ehloOptions == null || ehloOptions.Dsn)
			{
				if (!string.IsNullOrEmpty(orcpt))
				{
					bool flag = ehloOptions == null || ehloOptions.XLongAddr;
					int num;
					if (RcptSmtpCommandParser.IsValidOrcpt(orcpt, flag, 500, 1860, out num))
					{
						string text = SmtpUtils.ToXtextString(orcpt, smtpUtf8);
						if (flag || text.Length <= 500)
						{
							stringBuilder.Append(" ORCPT=");
							stringBuilder.Append(text);
							stringBuilder2.Append(" ORCPT=");
							stringBuilder2.Append(orcpt.Substring(0, num + 1));
							stringBuilder2.Append(SmtpUtils.ToXtextString(Util.Redact(orcpt.Substring(num + 1)), smtpUtf8));
						}
						else
						{
							ExTraceGlobals.SmtpSendTracer.TraceError<string>(0L, "ORCPT value is too long: '{0}'", text);
						}
					}
					else
					{
						ExTraceGlobals.SmtpSendTracer.TraceError<string>(0L, "Invalid ORCPT value: '{0}'", orcpt);
					}
				}
				StringBuilder stringBuilder3 = new StringBuilder();
				RcptSmtpCommand.AppendDsnArgument(stringBuilder3, dsnNotifyFlags);
				string value = stringBuilder3.ToString();
				stringBuilder.Append(value);
				stringBuilder2.Append(value);
			}
			if ((ehloOptions == null || ehloOptions.XOrar) && orar != RoutingAddress.Empty && (ehloOptions == null || ehloOptions.XLongAddr || !Util.IsLongAddress(orar)))
			{
				stringBuilder.Append(" XORAR=");
				stringBuilder.Append(SmtpUtils.ToXtextString(orar.ToString(), false));
				stringBuilder2.Append(" XORAR=");
				stringBuilder2.Append(SmtpUtils.ToXtextString(Util.Redact(orar), false));
			}
			if (ehloOptions != null && ehloOptions.XRDst && !string.IsNullOrEmpty(rdst))
			{
				stringBuilder.Append(" XRDST=");
				stringBuilder.Append(SmtpUtils.ToXtextString(rdst, false));
				stringBuilder2.Append(" XRDST=");
				stringBuilder2.Append(SmtpUtils.ToXtextString(rdst, false));
			}
			rcptCommand.Append(SmtpCommand.GetBracketedString(recipientAddress.ToString()));
			rcptCommand.Append(stringBuilder);
			if (redactedRcptCommand != null)
			{
				redactedRcptCommand.Append(SmtpCommand.GetBracketedString(Util.Redact(recipientAddress)));
				redactedRcptCommand.Append(stringBuilder2);
			}
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000D1BC0 File Offset: 0x000CFDC0
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RcptInboundParseCommand);
			if (!base.VerifyHelloReceived() || !base.VerifyMailFromReceived() || !base.VerifyNoOngoingBdat() || !base.VerifyXexch50NotReceived())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			CommandContext commandContext = CommandContext.FromSmtpCommand(this);
			RcptParseOutput rcptParseOutput;
			ParseResult parseResult = RcptSmtpCommandParser.Parse(commandContext, SmtpInSessionState.FromSmtpInSession(smtpInSession), smtpInSession.IsDataRedactionNecessary, smtpInSession.SmtpInServer.ReceiveConfiguration, Components.TransportAppConfig.SmtpReceiveConfiguration.SMTPAcceptAnyRecipient, out rcptParseOutput);
			smtpInSession.TooManyRecipientsResponseCount += rcptParseOutput.TooManyRecipientsResponseCount;
			base.LowAuthenticationLevelTarpitOverride = rcptParseOutput.LowAuthenticationLevelTarpitOverride;
			if (parseResult.ParsingStatus == ParsingStatus.Complete)
			{
				this.RecipientAddress = rcptParseOutput.RecipientAddress;
				this.Orar = rcptParseOutput.Orar;
				this.Notify = rcptParseOutput.Notify;
				this.ORcpt = rcptParseOutput.ORcpt;
				this.RDst = rcptParseOutput.RDst;
				this.ConsumerMailOptionalArguments = rcptParseOutput.ConsumerMailOptionalArguments;
				base.CurrentOffset = commandContext.Offset;
			}
			else
			{
				base.SmtpResponse = parseResult.SmtpResponse;
			}
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000D1D00 File Offset: 0x000CFF00
		internal override void InboundAgentEventCompleted()
		{
			ISmtpInSession session = base.SmtpSession as ISmtpInSession;
			if (session != null && session.TransportMailItemWrapper != null)
			{
				if (!session.SessionSource.SmtpResponse.IsEmpty)
				{
					return;
				}
				string destination;
				if (Util.TryGetNextHopFqdnProperty(session.TransportMailItem.ExtendedPropertyDictionary, out destination))
				{
					this.TrackInboundProxyDestination(session, destination, session.SmtpInServer.InboundProxyDestinationTracker, session.DestinationTrackerLastNextHopFqdn, delegate(string dest)
					{
						session.DestinationTrackerLastNextHopFqdn = dest;
					});
				}
				if (session.TransportMailItem.ExoAccountForest != null)
				{
					this.TrackInboundProxyDestination(session, session.TransportMailItem.ExoAccountForest, session.SmtpInServer.InboundProxyAccountForestTracker, session.DestinationTrackerLastExoAccountForest, delegate(string dest)
					{
						session.DestinationTrackerLastExoAccountForest = dest;
					});
				}
			}
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000D1E0C File Offset: 0x000D000C
		private void TrackInboundProxyDestination(ISmtpInSession session, string destination, IInboundProxyDestinationTracker proxyTracker, string lastDestination, Action<string> setLastDestination)
		{
			SmtpResponse smtpResponse;
			if (proxyTracker.ShouldRejectMessage(destination, out smtpResponse))
			{
				session.Disconnect(DisconnectReason.Local);
				base.SmtpResponse = smtpResponse;
				base.ParsingStatus = ParsingStatus.Error;
				return;
			}
			if (this.transportAppConfig.SmtpInboundProxyConfiguration.TrackInboundProxyDestinationsInRcpt)
			{
				if (destination.Equals(lastDestination, StringComparison.InvariantCultureIgnoreCase))
				{
					return;
				}
				if (lastDestination != null)
				{
					proxyTracker.DecrementProxyCount(lastDestination);
				}
				proxyTracker.IncrementProxyCount(destination);
				setLastDestination(destination);
			}
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000D1E74 File Offset: 0x000D0074
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RcptInboundProcessCommand);
			if (!((InboundRecipientCorrelator)this.recipientCorrelator).Contains((string)this.RecipientAddress))
			{
				this.mailRecipient = smtpInSession.TransportMailItem.Recipients.Add((string)this.RecipientAddress);
				this.mailRecipient.ORcpt = this.ORcpt;
				this.mailRecipient.DsnRequested = this.Notify;
				if (this.Orar != RoutingAddress.Empty)
				{
					OrarGenerator.SetOrarAddress(this.mailRecipient, this.Orar);
				}
				if (this.RDst != null)
				{
					this.mailRecipient.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Transport.RoutingOverride", this.RDst);
				}
				if (smtpInSession.IsPeerShadowSession)
				{
					ShadowRedundancyManager.PrepareRecipientForShadowing(this.mailRecipient, smtpInSession.PeerSessionPrimaryServer);
				}
			}
			else
			{
				base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
				if (this.Orar != RoutingAddress.Empty)
				{
					MailRecipient recipient = this.recipientCorrelator.Find(this.RecipientAddress);
					if (!OrarGenerator.ContainsOrar(recipient))
					{
						OrarGenerator.SetOrarAddress(recipient, this.Orar);
					}
				}
			}
			base.SmtpResponse = SmtpResponse.RcptToOk;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000D1FB0 File Offset: 0x000D01B0
		internal override void InboundCompleteCommand()
		{
			if (!base.SmtpResponse.Equals(SmtpResponse.Empty) && base.SmtpResponse.StatusCode[0] == '2')
			{
				if (this.mailRecipient == null)
				{
					((InboundRecipientCorrelator)this.recipientCorrelator).AddEmpty();
					return;
				}
				this.recipientCorrelator.Add(this.mailRecipient);
			}
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000D2014 File Offset: 0x000D0214
		internal override void OutboundCreateCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			smtpOutSession.CurrentRecipient = (smtpOutSession.NextRecipient ?? smtpOutSession.NextHopConnection.GetNextRecipient());
			smtpOutSession.NextRecipient = smtpOutSession.NextHopConnection.GetNextRecipient();
			if (smtpOutSession.CurrentRecipient != null)
			{
				this.RecipientAddress = smtpOutSession.CurrentRecipient.Email;
				if (!string.IsNullOrEmpty(smtpOutSession.CurrentRecipient.ORcpt))
				{
					this.ORcpt = smtpOutSession.CurrentRecipient.ORcpt;
				}
				this.Notify = smtpOutSession.CurrentRecipient.DsnRequested;
				RoutingAddress routingAddress;
				if (OrarGenerator.TryGetOrarAddress(smtpOutSession.CurrentRecipient, out routingAddress))
				{
					this.Orar = routingAddress;
				}
				string text;
				if (smtpOutSession.CurrentRecipient.ExtendedProperties.TryGetValue<string>("Microsoft.Exchange.Transport.RoutingOverride", out text))
				{
					this.RDst = text;
				}
			}
			else
			{
				this.RecipientAddress = RoutingAddress.Empty;
			}
			this.mailRecipient = smtpOutSession.CurrentRecipient;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000D20F8 File Offset: 0x000D02F8
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			RoutingAddress shortAddress = smtpOutSession.GetShortAddress(this.RecipientAddress);
			StringBuilder stringBuilder = new StringBuilder("RCPT TO:");
			StringBuilder stringBuilder2 = null;
			if (Util.IsDataRedactionNecessary())
			{
				stringBuilder2 = new StringBuilder("RCPT TO:");
			}
			RcptSmtpCommand.FormatCommand(stringBuilder, stringBuilder2, shortAddress, smtpOutSession.AdvertisedEhloOptions, this.ORcpt, this.Notify, this.Orar, this.RDst, smtpOutSession.SupportSmtpUtf8);
			smtpOutSession.NumberOfRecipientsAttempted++;
			base.ProtocolCommandString = stringBuilder.ToString();
			base.RedactedProtocolCommandString = ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "Formatted rcpt command: {0} ", base.ProtocolCommandString);
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000D21B4 File Offset: 0x000D03B4
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			smtpOutSession.NumberOfRecipientsAcked++;
			ExTraceGlobals.SmtpSendTracer.TraceDebug<int, int>((long)this.GetHashCode(), "Number of Recipients acked is {0}, attempted is {1}", smtpOutSession.NumberOfRecipientsAcked, smtpOutSession.NumberOfRecipientsAttempted);
			if (smtpOutSession.NextHopConnection == null || smtpOutSession.RoutedMailItem == null)
			{
				this.SetNextState();
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Connection already marked for Failover or the message has already been acked for a non-success status.  Not processing response for the RCPT TO command: {0}", base.SmtpResponse);
				return;
			}
			if (statusCode.Equals("552", StringComparison.OrdinalIgnoreCase) || statusCode.Equals("452", StringComparison.OrdinalIgnoreCase))
			{
				if (smtpOutSession.NumberOfRecipientsSucceeded > 0)
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<int, RoutingAddress, SmtpResponse>((long)this.GetHashCode(), "Recipient #{0} : {1} failed with {2} (\"too many recipients on message\" error).  Remaining recipients on this message will be retried in a separate MAIL FROM group of commands", smtpOutSession.NumberOfRecipientsAttempted, this.RecipientAddress, base.SmtpResponse);
					smtpOutSession.AckRecipient(AckStatus.Pending, base.SmtpResponse);
					if (smtpOutSession.NextRecipient != null)
					{
						smtpOutSession.AckRecipient(AckStatus.Pending, base.SmtpResponse);
						smtpOutSession.NextRecipient = null;
					}
					if (!smtpOutSession.RecipientsAckedPending)
					{
						smtpOutSession.NextHopConnection.MaxMessageRecipients = smtpOutSession.NumberOfRecipientsSucceeded;
						smtpOutSession.ResetPipelineState();
						smtpOutSession.RecipientsAckedPending = true;
						this.SetNextStateForSuccessfulRecipients();
					}
					return;
				}
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "Got response {0} (\"too many recipients on message\" error) for 1st recipient of message. This is an RFC standard violation by the remote SMTP.", base.SmtpResponse);
				AckStatus ackStatus;
				if (statusCode[0] == '5')
				{
					ackStatus = AckStatus.Fail;
					ExTraceGlobals.SmtpSendTracer.TraceError<RoutingAddress, SmtpResponse>((long)this.GetHashCode(), "Recipient {0} failed with {1}, will be NDR'ed", this.RecipientAddress, base.SmtpResponse);
				}
				else
				{
					ackStatus = AckStatus.Retry;
					ExTraceGlobals.SmtpSendTracer.TraceError<RoutingAddress, SmtpResponse>((long)this.GetHashCode(), "Recipient {0} failed with {1}, will be retried later.", this.RecipientAddress, base.SmtpResponse);
				}
				smtpOutSession.AckRecipient(ackStatus, base.SmtpResponse);
				this.SetNextState();
				return;
			}
			else
			{
				if (statusCode.Equals("421", StringComparison.OrdinalIgnoreCase) && !Util.UpgradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig))
				{
					ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Attempting failover. 421 Status code for RCPT command. NextState: QUIT");
					smtpOutSession.FailoverConnection(base.SmtpResponse, false);
					smtpOutSession.SetNextStateToQuit();
					return;
				}
				if ((base.SmtpResponse.SmtpResponseType == SmtpResponseType.PermanentError && !Util.DowngradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)) || (base.SmtpResponse.SmtpResponseType == SmtpResponseType.TransientError && Util.UpgradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)))
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<RoutingAddress, SmtpResponse>((long)this.GetHashCode(), "Recipient {0} failed with {1}, will be NDR'ed", this.RecipientAddress, base.SmtpResponse);
					smtpOutSession.AckRecipient(AckStatus.Fail, base.SmtpResponse);
					this.SetNextState();
					return;
				}
				if (statusCode[0] != '2')
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<RoutingAddress, SmtpResponse>((long)this.GetHashCode(), "Recipient {0} failed with {1}, will be retried", this.RecipientAddress, base.SmtpResponse);
					smtpOutSession.AckRecipient(AckStatus.Retry, base.SmtpResponse);
					this.SetNextState();
					return;
				}
				ExTraceGlobals.SmtpSendTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Recipient: {0} submitted", this.RecipientAddress);
				smtpOutSession.AckRecipient(AckStatus.Success, base.SmtpResponse);
				this.recipientCorrelator.Add(this.mailRecipient);
				this.SetNextState();
				return;
			}
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000D24D8 File Offset: 0x000D06D8
		protected void SetNextState()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (smtpOutSession.NumberOfRecipientsAcked != smtpOutSession.NumberOfRecipientsAttempted)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Waiting for other recipient responses");
				return;
			}
			if (smtpOutSession.PipeLineFailOverPending)
			{
				ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Fail over after completing the pipeline.");
				smtpOutSession.FailoverConnection(base.SmtpResponse, true);
				smtpOutSession.SetNextStateToQuit();
				return;
			}
			if (smtpOutSession.PipeLineNextMessagePending)
			{
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			if (smtpOutSession.NextRecipient != null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: PerRecipient");
				smtpOutSession.NextState = SmtpOutSession.SessionState.PerRecipient;
				return;
			}
			if (smtpOutSession.NumberOfRecipientsSucceeded == 0)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "No RCPT TO command succeeded, moving on to next message");
				AckStatus ackStatus;
				if (smtpOutSession.NumberOfRecipientsAckedForRetry == 0)
				{
					ackStatus = AckStatus.Fail;
				}
				else
				{
					ackStatus = AckStatus.Retry;
				}
				smtpOutSession.AckMessage(ackStatus, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			if (Components.IsMailboxProcess && this.transportAppConfig.SmtpSendConfiguration.RetryMessageOnRcptTransientError && smtpOutSession.NumberOfRecipientsAckedForRetry > 0)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<int>((long)this.GetHashCode(), "{0} RCPT TO commands resulted in deferred failures, moving on to next message", smtpOutSession.NumberOfRecipientsAckedForRetry);
				smtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			this.SetNextStateForSuccessfulRecipients();
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000D2618 File Offset: 0x000D0818
		protected virtual void SetNextStateForSuccessfulRecipients()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (smtpOutSession.ShouldSendExch50blob)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: XEXCH50");
				smtpOutSession.NextState = SmtpOutSession.SessionState.Xexch50;
				return;
			}
			if (!smtpOutSession.AdvertisedEhloOptions.Chunking)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: DATA");
				smtpOutSession.NextState = SmtpOutSession.SessionState.Data;
				return;
			}
			if (smtpOutSession.HasMoreBlobsPending())
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: XBdatBlob");
				smtpOutSession.NextState = SmtpOutSession.SessionState.XBdatBlob;
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: BDAT");
			smtpOutSession.NextState = SmtpOutSession.SessionState.Bdat;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000D26CC File Offset: 0x000D08CC
		private static void AppendDsnArgument(StringBuilder rcptCommand, DsnRequestedFlags dsnFlags)
		{
			bool flag = false;
			if (dsnFlags == DsnRequestedFlags.Default)
			{
				return;
			}
			rcptCommand.Append(" NOTIFY=");
			if ((dsnFlags & DsnRequestedFlags.Never) == DsnRequestedFlags.Default)
			{
				if ((dsnFlags & DsnRequestedFlags.Success) != DsnRequestedFlags.Default)
				{
					flag = true;
					rcptCommand.Append("SUCCESS");
				}
				if ((dsnFlags & DsnRequestedFlags.Failure) != DsnRequestedFlags.Default)
				{
					if (flag)
					{
						rcptCommand.Append(",");
					}
					flag = true;
					rcptCommand.Append("FAILURE");
				}
				if ((dsnFlags & DsnRequestedFlags.Delay) != DsnRequestedFlags.Default)
				{
					if (flag)
					{
						rcptCommand.Append(",");
					}
					rcptCommand.Append("DELAY");
				}
				return;
			}
			if ((dsnFlags & DsnRequestedFlags.Success & DsnRequestedFlags.Failure & DsnRequestedFlags.Delay) != DsnRequestedFlags.Default)
			{
				throw new InvalidOperationException("DSN requested flags on recipient were incorrectly set to both NEVER and one of SUCCESS, FAILURE or DELAY");
			}
			rcptCommand.Append("NEVER");
		}

		// Token: 0x04001A7B RID: 6779
		protected ITransportAppConfig transportAppConfig;

		// Token: 0x04001A7C RID: 6780
		private readonly RcptCommandEventArgs rcptCommandEventArgs;

		// Token: 0x04001A7D RID: 6781
		private readonly RecipientCorrelator recipientCorrelator;

		// Token: 0x04001A7E RID: 6782
		private MailRecipient mailRecipient;

		// Token: 0x04001A7F RID: 6783
		private RoutingAddress orar;

		// Token: 0x04001A80 RID: 6784
		private string rdst;
	}
}
