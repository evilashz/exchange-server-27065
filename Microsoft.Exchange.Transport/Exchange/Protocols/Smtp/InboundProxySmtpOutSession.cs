using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200039D RID: 925
	internal class InboundProxySmtpOutSession : SmtpOutSession
	{
		// Token: 0x0600294B RID: 10571 RVA: 0x000A3298 File Offset: 0x000A1498
		public InboundProxySmtpOutSession(ulong sessionId, SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection, IPEndPoint target, ProtocolLog protocolLog, ProtocolLoggingLevel loggingLevel, IMailRouter mailRouter, CertificateCache certificateCache, CertificateValidator certificateValidator, ShadowRedundancyManager shadowRedundancyManager, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration, IInboundProxyLayer proxyLayer) : base(sessionId, smtpOutConnection, nextHopConnection, target, protocolLog, loggingLevel, mailRouter, certificateCache, certificateValidator, shadowRedundancyManager, transportAppConfig, transportConfiguration, false)
		{
			this.proxyLayer = proxyLayer;
			this.ReadFromProxyLayerCompleteCallback = new InboundProxyLayer.ReadCompletionCallback(this.ReadFromProxyLayerComplete);
			this.WaitForNextBdatCompleteCallback = new InboundBdatProxyLayer.CommandReceivedCallback(this.WaitForBdatCommandComplete);
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000A32ED File Offset: 0x000A14ED
		public IInboundProxyLayer ProxyLayer
		{
			get
			{
				return this.proxyLayer;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000A32F5 File Offset: 0x000A14F5
		// (set) Token: 0x0600294E RID: 10574 RVA: 0x000A32FD File Offset: 0x000A14FD
		public bool WaitingForNextProxiedBdat
		{
			get
			{
				return this.waitingForNextProxiedBdat;
			}
			set
			{
				this.waitingForNextProxiedBdat = value;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000A3306 File Offset: 0x000A1506
		public override bool SendShadow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000A3309 File Offset: 0x000A1509
		public override bool SupportExch50
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x000A330C File Offset: 0x000A150C
		protected override bool SendFewerMessagesToSlowerServerEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x000A330F File Offset: 0x000A150F
		protected override bool FailoverPermittedForRemoteShutdown
		{
			get
			{
				return !this.proxyingMessageBody;
			}
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x000A331A File Offset: 0x000A151A
		protected override void IncrementConnectionCounters()
		{
			base.IncrementConnectionCounters();
			if (!this.transportAppConfig.SmtpInboundProxyConfiguration.TrackInboundProxyDestinationsInRcpt)
			{
				this.proxyLayer.InboundProxyDestinationTracker.IncrementProxyCount(this.proxyLayer.NextHopFqdn);
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000A334F File Offset: 0x000A154F
		protected override void DecrementConnectionCounters()
		{
			base.DecrementConnectionCounters();
			if (!this.transportAppConfig.SmtpInboundProxyConfiguration.TrackInboundProxyDestinationsInRcpt)
			{
				this.proxyLayer.InboundProxyDestinationTracker.DecrementProxyCount(this.proxyLayer.NextHopFqdn);
			}
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000A3384 File Offset: 0x000A1584
		public override void ShutdownConnection()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxySmtpOutSession.ShutdownConnection");
			base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyShutdownConnection);
			this.dontCacheThisConnection = true;
			base.ShutdownConnection();
			this.proxyLayer.Shutdown();
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000A33BC File Offset: 0x000A15BC
		public override void ResetSession(SmtpOutConnection smtpOutConnection, NextHopConnection nextHopConnection)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxySmtpOutSession.ResetSession");
			this.proxyLayer = this.GetProxyLayer(nextHopConnection);
			this.waitingForNextProxiedBdat = false;
			base.ResetSession(smtpOutConnection, nextHopConnection);
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxying inbound session with session id {0}", new object[]
			{
				this.proxyLayer.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo)
			});
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000A3438 File Offset: 0x000A1638
		public override void AckMessage(AckStatus ackStatus, SmtpResponse smtpResponse, long messageSize, SessionSetupFailureReason failureReason, bool updateSmtpSendFailureCounters)
		{
			this.proxyingMessageBody = false;
			bool replaceFailureResponse = failureReason != SessionSetupFailureReason.ProtocolError;
			this.ProxyLayer.AckMessage(ackStatus, smtpResponse, replaceFailureResponse, "InboundProxySmtpOutSession.AckMessage with session id" + base.SessionId, failureReason);
			base.AckMessage(ackStatus, smtpResponse, messageSize, failureReason, updateSmtpSendFailureCounters);
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000A3487 File Offset: 0x000A1687
		public override void PrepareForNextMessageOnCachedSession()
		{
			base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyPrepareForNextMessageOnCachedSession);
			if (base.AdvertisedEhloOptions.XProxyFrom)
			{
				base.NextState = SmtpOutSession.SessionState.Rset;
				base.SendNextCommands();
				return;
			}
			base.PrepareForNextMessageOnCachedSession();
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000A34B4 File Offset: 0x000A16B4
		protected override void HandleError(object error)
		{
			bool failoverConnection = !this.proxyingMessageBody;
			base.HandleError(error, false, failoverConnection);
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000A34D4 File Offset: 0x000A16D4
		protected override bool PreProcessMessage()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxySmtpOutSession.PreProcessMessage");
			if (base.NextHopDeliveryType == DeliveryType.Heartbeat)
			{
				throw new InvalidOperationException("Inbound proxy should never send a hearbeat");
			}
			bool supportLongAddresses = base.SupportLongAddresses;
			bool supportOrar = base.SupportOrar;
			bool supportRDst = base.SupportRDst;
			bool supportSmtpUtf = base.SupportSmtpUtf8;
			if (!base.CheckLongSenderSupport(supportLongAddresses))
			{
				return false;
			}
			if (!base.CheckSmtpUtf8SenderSupport(supportSmtpUtf))
			{
				return false;
			}
			foreach (MailRecipient recipient in base.NextHopConnection.ReadyRecipients)
			{
				if (!base.PreProcessRecipient(recipient, supportLongAddresses, supportOrar, supportRDst, supportSmtpUtf))
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from '{0}' was Ack'ed because a recipient failed in PreProcess()", base.RoutedMailItem.From.ToString());
					base.AckMessage(AckStatus.Fail, SmtpResponse.NoRecipientSucceeded);
					this.ProxyLayer.AckMessage(AckStatus.Fail, SmtpResponse.NoRecipientSucceeded, true, "InboundProxySmtpOutSession.PreProcessMessage for session id " + base.SessionId, SessionSetupFailureReason.ProtocolError);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000A3604 File Offset: 0x000A1804
		protected override SmtpCommand CreateSmtpCommand(string cmd)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "InboundProxySmtpOutSession.CreateSmtpCommand: {0}", cmd);
			SmtpCommand smtpCommand = null;
			if (cmd != null)
			{
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027e2-1 == null)
				{
					<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027e2-1 = new Dictionary<string, int>(12)
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
							"X-EXPS",
							2
						},
						{
							"STARTTLS",
							3
						},
						{
							"X-ANONYMOUSTLS",
							4
						},
						{
							"MAIL",
							5
						},
						{
							"RCPT",
							6
						},
						{
							"DATA",
							7
						},
						{
							"BDAT",
							8
						},
						{
							"RSET",
							9
						},
						{
							"XPROXYFROM",
							10
						},
						{
							"QUIT",
							11
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{35B61D4F-52EA-4BAD-B9BF-6C1C27B6D99A}.$$method0x60027e2-1.TryGetValue(cmd, out num))
				{
					switch (num)
					{
					case 0:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdConnectResponse);
						break;
					case 1:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdEhlo);
						smtpCommand = new EHLOInboundProxySmtpCommand(this);
						break;
					case 2:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdAuth);
						smtpCommand = new AuthSmtpCommand(this, true, this.transportConfiguration);
						break;
					case 3:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, false);
						break;
					case 4:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdStarttls);
						smtpCommand = new StarttlsSmtpCommand(this, true);
						break;
					case 5:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdMail);
						smtpCommand = new MailSmtpCommand(this, this.transportAppConfig);
						break;
					case 6:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdRcpt);
						smtpCommand = new RcptInboundProxySmtpCommand(this, this.recipientCorrelator, this.transportAppConfig);
						break;
					case 7:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdData);
						smtpCommand = new DataInboundProxySmtpCommand(this, this.transportAppConfig);
						break;
					case 8:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdBdat);
						smtpCommand = new BdatInboundProxySmtpCommand(this, this.transportAppConfig);
						break;
					case 9:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdRset);
						smtpCommand = new RsetInboundProxySmtpCommand(this);
						break;
					case 10:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdXProxyFrom);
						smtpCommand = new XProxyFromSmtpCommand(this, this.transportConfiguration, this.transportAppConfig);
						break;
					case 11:
						base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyCreateCmdQuit);
						smtpCommand = new QuitSmtpCommand(this);
						break;
					default:
						goto IL_22A;
					}
					if (smtpCommand != null)
					{
						smtpCommand.ParsingStatus = ParsingStatus.Complete;
						smtpCommand.OutboundCreateCommand();
					}
					return smtpCommand;
				}
			}
			IL_22A:
			throw new ArgumentException("Unknown command encountered in SmtpOut: " + cmd, "cmd");
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000A3864 File Offset: 0x000A1A64
		protected override bool InvokeCommandHandler(SmtpCommand command)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxySmtpOutSession.InvokeCommandHandler");
			base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyInvokeCommandHandler);
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
				base.EnqueueResponseHandler(command);
				BdatInboundProxySmtpCommand bdatInboundProxySmtpCommand = command as BdatInboundProxySmtpCommand;
				if (bdatInboundProxySmtpCommand != null)
				{
					if (this.sendBuffer.Length != 0)
					{
						throw new InvalidOperationException("BDAT cannot be pipelined");
					}
					this.connection.BeginWrite(command.ProtocolCommand, 0, command.ProtocolCommand.Length, InboundProxySmtpOutSession.WriteBdatCompleteSendBuffersCallback, this);
					return true;
				}
				else
				{
					this.sendBuffer.Append(command.ProtocolCommand);
				}
			}
			else if (command.ProtocolCommand != null)
			{
				base.EnqueueResponseHandler(command);
				this.logSession.LogSend(SmtpOutSession.BinaryData);
				this.sendBuffer.Append(command.ProtocolCommand);
			}
			else
			{
				DataInboundProxySmtpCommand dataInboundProxySmtpCommand = command as DataInboundProxySmtpCommand;
				if (dataInboundProxySmtpCommand != null)
				{
					if (this.sendBuffer.Length != 0)
					{
						throw new InvalidOperationException("DATA cannot send stream unless send buffer is empty");
					}
					base.EnqueueResponseHandler(command);
					this.proxyingMessageBody = true;
					this.SendDataBuffers();
					return true;
				}
				else
				{
					command.Dispose();
				}
			}
			return false;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x000A39D8 File Offset: 0x000A1BD8
		protected override void ConnectResponseEvent(SmtpResponse smtpResponse)
		{
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxying inbound session with session id {0}", new object[]
			{
				this.proxyLayer.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo)
			});
			base.ConnectResponseEvent(smtpResponse);
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000A3A26 File Offset: 0x000A1C26
		protected override void FinalizeNextStateAndSendCommands()
		{
			if (this.WaitingForNextProxiedBdat)
			{
				this.ProxyLayer.WaitForNewCommand(this.WaitForNextBdatCompleteCallback);
				return;
			}
			base.FinalizeNextStateAndSendCommands();
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000A3A48 File Offset: 0x000A1C48
		protected virtual IInboundProxyLayer GetProxyLayer(NextHopConnection newConnection)
		{
			if (!(newConnection is InboundProxyNextHopConnection))
			{
				throw new InvalidOperationException("GetProxyLayer called with incorrect NextHopConnection type");
			}
			return ((InboundProxyNextHopConnection)newConnection).ProxyLayer;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000A3A68 File Offset: 0x000A1C68
		private static void WriteBdatCompleteSendBuffers(IAsyncResult asyncResult)
		{
			InboundProxySmtpOutSession inboundProxySmtpOutSession = (InboundProxySmtpOutSession)asyncResult.AsyncState;
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)inboundProxySmtpOutSession.GetHashCode(), "InboundProxySmtpOutSession.WriteBdatCompleteSendBuffers");
			inboundProxySmtpOutSession.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyWriteBdatCompleteSendBuffers);
			object obj;
			inboundProxySmtpOutSession.connection.EndWrite(asyncResult, out obj);
			if (obj != null)
			{
				inboundProxySmtpOutSession.HandleError(obj);
				return;
			}
			inboundProxySmtpOutSession.proxyingMessageBody = true;
			if (inboundProxySmtpOutSession.ProxyLayer.OutboundChunkSize == 0L)
			{
				inboundProxySmtpOutSession.sendBuffer.Reset();
				inboundProxySmtpOutSession.StartReadLine();
				return;
			}
			inboundProxySmtpOutSession.ProxyLayer.BeginReadData(inboundProxySmtpOutSession.ReadFromProxyLayerCompleteCallback);
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x000A3AF4 File Offset: 0x000A1CF4
		private static void WriteProxiedBytesToTargetComplete(IAsyncResult asyncResult)
		{
			KeyValuePair<InboundProxySmtpOutSession, BufferCacheEntry> keyValuePair = (KeyValuePair<InboundProxySmtpOutSession, BufferCacheEntry>)asyncResult.AsyncState;
			InboundProxySmtpOutSession key = keyValuePair.Key;
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)key.GetHashCode(), "InboundProxySmtpOutSession.WriteProxiedBytesToTargetComplete");
			key.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyWriteProxiedBytesToTargetComplete);
			object obj;
			key.connection.EndWrite(asyncResult, out obj);
			key.ProxyLayer.ReturnBuffer(keyValuePair.Value);
			if (obj != null)
			{
				key.HandleError(obj);
				return;
			}
			if (key.eodSeen)
			{
				key.sendBuffer.Reset();
				key.StartReadLine();
				return;
			}
			key.ProxyLayer.BeginReadData(key.ReadFromProxyLayerCompleteCallback);
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x000A3B89 File Offset: 0x000A1D89
		private void SendDataBuffers()
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxySmtpOutSession.SendDataBuffers");
			base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxySendDataBuffers);
			this.ProxyLayer.BeginReadData(this.ReadFromProxyLayerCompleteCallback);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000A3BBC File Offset: 0x000A1DBC
		private void ReadFromProxyLayerComplete(BufferCacheEntry buffer, bool lastBuffer)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "InboundProxySmtpOutSession.ReadFromProxyLayerComplete. buffer = {0}. lastBuffer = {1}", (buffer == null) ? "null" : buffer.Buffer.Length.ToString(CultureInfo.InvariantCulture), lastBuffer);
			base.DropBreadcrumb(SmtpOutSession.SmtpOutSessionBreadcrumbs.InboundProxyReadFromProxyLayerComplete);
			if (buffer != null)
			{
				this.eodSeen = lastBuffer;
				this.connection.BeginWrite(buffer.Buffer, 0, buffer.Buffer.Length, InboundProxySmtpOutSession.WriteProxiedBytesToTargetCompleteCallback, new KeyValuePair<InboundProxySmtpOutSession, BufferCacheEntry>(this, buffer));
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "The proxy layer returned null bytes. Acking the message from {0}.", base.RoutedMailItem.From.ToString());
			this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy layer started discarding data. Acking message as failed.");
			base.AckMessage(AckStatus.Fail, SmtpResponse.ProxyDiscardingMessage);
			base.Disconnect();
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000A3C94 File Offset: 0x000A1E94
		private void WaitForBdatCommandComplete(bool commandReceived)
		{
			if (commandReceived)
			{
				base.NextState = SmtpOutSession.SessionState.Bdat;
				this.WaitingForNextProxiedBdat = false;
			}
			else
			{
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy layer started discarding data. Acking message as failed.");
				base.AckMessage(AckStatus.Fail, SmtpResponse.ProxyDiscardingMessage);
				base.NextState = SmtpOutSession.SessionState.Quit;
			}
			base.FinalizeNextStateAndSendCommands();
		}

		// Token: 0x0400153A RID: 5434
		private static readonly AsyncCallback WriteProxiedBytesToTargetCompleteCallback = new AsyncCallback(InboundProxySmtpOutSession.WriteProxiedBytesToTargetComplete);

		// Token: 0x0400153B RID: 5435
		private static readonly AsyncCallback WriteBdatCompleteSendBuffersCallback = new AsyncCallback(InboundProxySmtpOutSession.WriteBdatCompleteSendBuffers);

		// Token: 0x0400153C RID: 5436
		private readonly InboundProxyLayer.ReadCompletionCallback ReadFromProxyLayerCompleteCallback;

		// Token: 0x0400153D RID: 5437
		private readonly InboundBdatProxyLayer.CommandReceivedCallback WaitForNextBdatCompleteCallback;

		// Token: 0x0400153E RID: 5438
		private IInboundProxyLayer proxyLayer;

		// Token: 0x0400153F RID: 5439
		private bool eodSeen;

		// Token: 0x04001540 RID: 5440
		private bool waitingForNextProxiedBdat;

		// Token: 0x04001541 RID: 5441
		private bool proxyingMessageBody;
	}
}
