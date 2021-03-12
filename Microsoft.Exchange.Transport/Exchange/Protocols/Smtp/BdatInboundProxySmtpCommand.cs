using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A0 RID: 1184
	internal class BdatInboundProxySmtpCommand : BaseDataInboundProxySmtpCommand
	{
		// Token: 0x06003590 RID: 13712 RVA: 0x000DB59B File Offset: 0x000D979B
		public BdatInboundProxySmtpCommand(ISmtpSession session, ITransportAppConfig transportAppConfig) : base(session, transportAppConfig, "BDAT")
		{
		}

		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003591 RID: 13713 RVA: 0x000DB5AA File Offset: 0x000D97AA
		protected override long AccumulatedMessageSize
		{
			get
			{
				return this.smtpInBdatParser.TotalBytesWritten;
			}
		}

		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003592 RID: 13714 RVA: 0x000DB5B7 File Offset: 0x000D97B7
		protected override bool IsProxying
		{
			get
			{
				return this.bdatProxyLayer != null;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06003593 RID: 13715 RVA: 0x000DB5C5 File Offset: 0x000D97C5
		protected override long EohPosition
		{
			get
			{
				return this.smtpInBdatParser.EohPos;
			}
		}

		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x06003594 RID: 13716 RVA: 0x000DB5D2 File Offset: 0x000D97D2
		public bool IsBdat0Last
		{
			get
			{
				return this.isLastChunk && this.chunkSize == 0L;
			}
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000DB5E8 File Offset: 0x000D97E8
		internal override void InboundParseCommand()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "BDATInboundProxy.InboundParseCommand");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBdatInboundParseCommand);
			bool flag;
			bool flag2;
			if (!BdatSmtpCommand.RunBdatSequenceChecks(this, smtpInSession, out flag, out flag2))
			{
				return;
			}
			if (flag)
			{
				this.RestoreBdatState();
			}
			ParseResult parseResult = BdatSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(this), SmtpInSessionState.FromSmtpInSession(smtpInSession), this.totalChunkSize, out this.chunkSize);
			if (parseResult.IsFailed)
			{
				if (parseResult.SmtpResponse == SmtpResponse.MessageTooLarge)
				{
					BdatSmtpCommand.SetMessageTooLargeResponse(smtpInSession, this, false);
				}
				else
				{
					base.SmtpResponse = parseResult.SmtpResponse;
					base.ParsingStatus = parseResult.ParsingStatus;
				}
				this.StartDiscardingMessage();
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
				return;
			}
			this.isLastChunk = BdatSmtpCommandParser.IsLastChunk(parseResult.ParsingStatus);
			if ((this.seenEoh || !base.OnlyCheckMessageSizeAfterEoh) && BdatSmtpCommandParser.IsMessageSizeExceeded(SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(smtpInSession.Permissions), this.totalChunkSize, this.chunkSize, this.originalMessageSize, this.messageSizeLimit, smtpInSession.LogSession, ExTraceGlobals.SmtpReceiveTracer))
			{
				BdatSmtpCommand.SetMessageTooLargeResponse(smtpInSession, this, true);
				this.StartDiscardingMessage();
				return;
			}
			if (this.discardingMessage)
			{
				base.SmtpResponse = SmtpResponse.BadCommandSequence;
				base.IsResponseReady = false;
			}
			base.ParsingStatus = ParsingStatus.MoreDataRequired;
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000DB72C File Offset: 0x000D992C
		internal override void InboundProcessCommand()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "BDATInboundProxy.InboundProcessCommand");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyDataInboundProcessCommand);
			base.IsResponseReady = false;
			if (this.smtpInBdatParser == null)
			{
				this.smtpInBdatParser = new SmtpInBdatProxyParser(smtpInSession.MimeDocument);
			}
			this.smtpInBdatParser.ResetForNewBdatCommand(this.chunkSize, this.discardingMessage, this.isLastChunk, new SmtpInDataProxyParser.EndOfHeadersCallback(base.ParserEndOfHeadersCallback), new ExceptionFilter(base.ParserException));
			if (!this.isLastChunk)
			{
				base.LowAuthenticationLevelTarpitOverride = TarpitAction.DoTarpit;
			}
			smtpInSession.SetRawModeAfterCommandCompleted(new RawDataHandler(this.RawDataReceived));
			if (this.IsProxying)
			{
				this.bdatProxyLayer.CreateNewCommand(this.chunkSize, this.chunkSize, this.isLastChunk);
			}
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000DB801 File Offset: 0x000D9A01
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000DB804 File Offset: 0x000D9A04
		internal override void OutboundFormatCommand()
		{
			InboundProxySmtpOutSession inboundProxySmtpOutSession = (InboundProxySmtpOutSession)base.SmtpSession;
			this.chunkSize = inboundProxySmtpOutSession.ProxyLayer.OutboundChunkSize;
			this.totalChunkSize = inboundProxySmtpOutSession.ProxyLayer.BytesRead + inboundProxySmtpOutSession.ProxyLayer.OutboundChunkSize;
			if (inboundProxySmtpOutSession.ProxyLayer.IsLastChunk)
			{
				base.ProtocolCommandString = string.Format(CultureInfo.InvariantCulture, "BDAT {0} LAST", new object[]
				{
					this.chunkSize
				});
				return;
			}
			base.ProtocolCommandString = string.Format(CultureInfo.InvariantCulture, "BDAT {0}", new object[]
			{
				this.chunkSize
			});
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000DB8B0 File Offset: 0x000D9AB0
		internal override void OutboundProcessResponse()
		{
			InboundProxySmtpOutSession inboundProxySmtpOutSession = (InboundProxySmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			bool issueBetweenMsgRset = true;
			bool flag = true;
			if (statusCode[0] == '5')
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Message body response was: ", base.SmtpResponse);
				inboundProxySmtpOutSession.AckMessage(AckStatus.Fail, base.SmtpResponse);
			}
			else if (statusCode[0] != '2')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, IPEndPoint, SmtpResponse>((long)this.GetHashCode(), "Failed to deliver message from {0} to {1}, Status: {2}", inboundProxySmtpOutSession.RoutedMailItem.From.ToString(), inboundProxySmtpOutSession.RemoteEndPoint, base.SmtpResponse);
				inboundProxySmtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
			}
			else if (inboundProxySmtpOutSession.ProxyLayer.IsLastChunk)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string, IPEndPoint>((long)this.GetHashCode(), "Delivered message from {0} to {1}", inboundProxySmtpOutSession.RoutedMailItem.From.ToString(), inboundProxySmtpOutSession.RemoteEndPoint);
				issueBetweenMsgRset = false;
				inboundProxySmtpOutSession.AckMessage(AckStatus.Success, base.SmtpResponse, this.totalChunkSize);
			}
			else
			{
				inboundProxySmtpOutSession.ProxyLayer.AckCommandSuccessful();
				flag = false;
			}
			if (flag)
			{
				inboundProxySmtpOutSession.PrepareForNextMessage(issueBetweenMsgRset);
				return;
			}
			inboundProxySmtpOutSession.NextState = SmtpOutSession.SessionState.Bdat;
			inboundProxySmtpOutSession.WaitingForNextProxiedBdat = true;
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x000DB9F0 File Offset: 0x000D9BF0
		protected override void StartDiscardingMessage()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBdatDiscardingMessage);
			this.discardingMessage = true;
			if (this.smtpInBdatParser != null)
			{
				this.smtpInBdatParser.StartDiscardingMessage();
			}
			if (this.bdatProxyLayer != null)
			{
				this.bdatProxyLayer.NotifySmtpInStopProxy();
				this.bdatProxyLayer = null;
			}
			this.StoreCurrentDataState();
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x000DBA50 File Offset: 0x000D9C50
		private static IAsyncResult RaiseProxyInboundMessageEvent(ISmtpInSession session, AsyncCallback callback)
		{
			session.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBdatRaiseOnProxyInboundMessageEvent);
			session.AgentLatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveOnProxyInboundMessage, session.TransportMailItem.LatencyTracker);
			ProxyInboundMessageEventSource proxyInboundMessageEventSource = ProxyInboundMessageEventSourceImpl.Create(session.SessionSource);
			bool clientIsPreE15InternalServer = SmtpInSessionUtils.IsAuthenticated(session.RemoteIdentity) && session.AuthMethod == MultilevelAuthMechanism.MUTUALGSSAPI;
			bool localFrontendIsColocatedWithHub = Components.Configuration.LocalServer.TransportServer.IsHubTransportServer && Components.Configuration.LocalServer.TransportServer.IsFrontendTransportServer;
			return session.AgentSession.BeginRaiseEvent("OnProxyInboundMessage", proxyInboundMessageEventSource, new ProxyInboundMessageEventArgs(session.SessionSource, session.TransportMailItemWrapper, clientIsPreE15InternalServer, localFrontendIsColocatedWithHub, session.SmtpInServer.Name), callback, proxyInboundMessageEventSource);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x000DBB08 File Offset: 0x000D9D08
		private AsyncReturnType RawDataReceived(byte[] data, int offset, int numBytes)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int>((long)this.GetHashCode(), "RawDataReceived received {0} bytes", numBytes);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			bool isDiscardingData = this.smtpInBdatParser.IsDiscardingData;
			int num;
			this.seenEod = this.smtpInBdatParser.Write(data, offset, numBytes, out num);
			if (numBytes != num)
			{
				smtpInSession.PutBackReceivedBytes(numBytes - num);
			}
			this.bytesReceived += (long)num;
			if (!isDiscardingData && this.smtpInBdatParser.IsDiscardingData)
			{
				this.StartDiscardingMessage();
			}
			if (!this.discardingMessage && !this.seenEoh && !SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(smtpInSession.Permissions) && smtpInSession.Connector.MaxHeaderSize.ToBytes() < (ulong)this.AccumulatedMessageSize)
			{
				base.SmtpResponse = SmtpResponse.HeadersTooLarge;
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "AccumulatedMessageSize: {0} > MaxHeaderSize: {1}", new object[]
				{
					this.AccumulatedMessageSize,
					smtpInSession.Connector.MaxHeaderSize
				});
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
			}
			if (this.eohEventArgs != null)
			{
				DataBdatHelpers.RaiseEOHEvent(null, smtpInSession, new AsyncCallback(this.ContinueEndOfHeaders), this.eohEventArgs);
			}
			else if (this.IsProxying)
			{
				this.bdatProxyLayer.BeginWriteData(data, offset, num, this.seenEod, base.WriteCompleteCallback);
			}
			else if (this.seenEod)
			{
				base.RaiseOnRejectIfNecessary();
			}
			else
			{
				smtpInSession.RawDataReceivedCompleted();
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int, AsyncReturnType>((long)this.GetHashCode(), "RawDataReceived consumed {0} bytes, return returnType={1}", num, AsyncReturnType.Async);
			return AsyncReturnType.Async;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000DBC98 File Offset: 0x000D9E98
		private void ContinueEndOfHeaders(IAsyncResult ar)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "ContinueEndOfHeaders");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBdatContinueEndOfHeaders);
			smtpInSession.AgentLatencyTracker.EndTrackLatency();
			base.ProcessAgentResponse(ar, this.eohEventArgs);
			this.eohEventArgs = null;
			if (!this.discardingMessage)
			{
				BdatInboundProxySmtpCommand.RaiseProxyInboundMessageEvent((ISmtpInSession)base.SmtpSession, new AsyncCallback(this.ContinueProxyInboundMessage));
				return;
			}
			if (this.seenEod)
			{
				base.RaiseOnRejectIfNecessary();
				return;
			}
			smtpInSession.RawDataReceivedCompleted();
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000DBD2C File Offset: 0x000D9F2C
		protected override void FinishEodSequence()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "FinishEodSequence");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBdatFinishEodSequence);
			if (!this.discardingMessage)
			{
				this.totalChunkSize += this.chunkSize;
				if (base.SmtpResponse.Equals(SmtpResponse.Empty))
				{
					if (this.seenEoh)
					{
						base.SmtpResponse = SmtpResponse.DataTransactionFailed;
					}
					else if (!this.isLastChunk)
					{
						base.SmtpResponse = SmtpResponse.OctetsReceived(this.chunkSize);
					}
				}
			}
			else if (base.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = SmtpResponse.GenericProxyFailure;
			}
			base.IsResponseReady = true;
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveMessageRejected, null, new object[]
				{
					smtpInSession.Connector.Name,
					base.SmtpResponse
				});
			}
			else if (this.isLastChunk)
			{
				if (this.bdatProxyLayer != null)
				{
					smtpInSession.UpdateSmtpReceivePerfCountersForMessageReceived(smtpInSession.TransportMailItem.Recipients.Count, this.bdatProxyLayer.BytesWritten);
					smtpInSession.UpdateInboundProxyDestinationPerfCountersForMessageReceived(smtpInSession.TransportMailItem.Recipients.Count, this.bdatProxyLayer.BytesWritten);
				}
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
			}
			base.ParsingStatus = ParsingStatus.Complete;
			if (base.ShouldDisconnect && !smtpInSession.SessionSource.ShouldDisconnect)
			{
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
			}
			if (this.isLastChunk)
			{
				smtpInSession.ReleaseMailItem();
				smtpInSession.BdatState = null;
			}
			else if (!this.discardingMessage)
			{
				this.StoreCurrentDataState();
			}
			this.bdatProxyLayer = null;
			smtpInSession.RawDataReceivedCompleted();
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000DBEE8 File Offset: 0x000DA0E8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this.bdatProxyLayer != null)
					{
						this.bdatProxyLayer.NotifySmtpInStopProxy();
						this.bdatProxyLayer = null;
					}
					if (this.smtpInBdatParser != null && this.isLastChunk)
					{
						this.smtpInBdatParser.Dispose();
						this.smtpInBdatParser = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000DBF50 File Offset: 0x000DA150
		private void StoreCurrentDataState()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (smtpInSession.BdatState == null)
			{
				smtpInSession.BdatState = new SmtpInBdatState();
			}
			smtpInSession.BdatState.TotalChunkSize = this.totalChunkSize;
			smtpInSession.BdatState.DiscardingMessage = this.discardingMessage;
			smtpInSession.BdatState.SeenEoh = this.seenEoh;
			smtpInSession.BdatState.OriginalMessageSize = this.originalMessageSize;
			smtpInSession.BdatState.MessageSizeLimit = this.messageSizeLimit;
			smtpInSession.BdatState.ProxyParser = this.smtpInBdatParser;
			smtpInSession.BdatState.ProxyLayer = this.bdatProxyLayer;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000DBFF4 File Offset: 0x000DA1F4
		private void RestoreBdatState()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			this.totalChunkSize = smtpInSession.BdatState.TotalChunkSize;
			this.discardingMessage = smtpInSession.BdatState.DiscardingMessage;
			this.seenEoh = smtpInSession.BdatState.SeenEoh;
			this.originalMessageSize = smtpInSession.BdatState.OriginalMessageSize;
			this.messageSizeLimit = smtpInSession.BdatState.MessageSizeLimit;
			this.smtpInBdatParser = smtpInSession.BdatState.ProxyParser;
			this.bdatProxyLayer = smtpInSession.BdatState.ProxyLayer;
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000DC084 File Offset: 0x000DA284
		private void ContinueProxyInboundMessage(IAsyncResult ar)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.AgentLatencyTracker.EndTrackLatency();
			ProxyInboundMessageEventSourceImpl proxyInboundMessageEventSourceImpl = (ProxyInboundMessageEventSourceImpl)ar.AsyncState;
			IEnumerable<INextHopServer> enumerable = null;
			bool internalDestination = false;
			if (this.TransportAppConfig.SmtpInboundProxyConfiguration.ProxyDestinations != null && this.TransportAppConfig.SmtpInboundProxyConfiguration.ProxyDestinations.Count != 0)
			{
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy destination(s) obtained from app config");
				List<RoutingHost> list = new List<RoutingHost>(this.TransportAppConfig.SmtpInboundProxyConfiguration.ProxyDestinations.Count);
				foreach (IPAddress ipaddress in this.TransportAppConfig.SmtpInboundProxyConfiguration.ProxyDestinations)
				{
					list.Add(new RoutingHost(ipaddress.ToString()));
				}
				enumerable = list;
				internalDestination = this.TransportAppConfig.SmtpInboundProxyConfiguration.IsInternalDestination;
			}
			else if (proxyInboundMessageEventSourceImpl.DestinationServers != null)
			{
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Proxy destination(s) obtained from OnProxyInboundMessage event");
				enumerable = proxyInboundMessageEventSourceImpl.DestinationServers;
				internalDestination = proxyInboundMessageEventSourceImpl.InternalDestination;
			}
			if (enumerable != null)
			{
				BaseDataInboundProxySmtpCommand.FinalizeLatencyTracking(smtpInSession);
				this.bdatProxyLayer = new InboundBdatProxyLayer(smtpInSession.SessionId, smtpInSession.RemoteEndPoint, smtpInSession.HelloDomain, smtpInSession.AdvertisedEhloOptions, smtpInSession.XProxyFromSeqNum, smtpInSession.TransportMailItem, internalDestination, enumerable, this.TransportAppConfig.SmtpInboundProxyConfiguration.AccumulatedMessageSizeThreshold.ToBytes(), smtpInSession.LogSession, smtpInSession.SmtpInServer.SmtpOutConnectionHandler, this.TransportAppConfig.SmtpInboundProxyConfiguration.PreserveTargetResponse, smtpInSession.ProxiedClientPermissions, smtpInSession.AuthenticationSourceForAgents, smtpInSession.SmtpInServer.InboundProxyDestinationTracker);
				byte[] accumulatedBytesForProxying = this.smtpInBdatParser.GetAccumulatedBytesForProxying();
				this.bdatProxyLayer.CreateNewCommand(this.chunkSize, (long)accumulatedBytesForProxying.Length + this.chunkSize - this.bytesReceived, this.isLastChunk);
				this.bdatProxyLayer.SetupProxySession();
				this.bdatProxyLayer.BeginWriteData(accumulatedBytesForProxying, 0, accumulatedBytesForProxying.Length, this.seenEod, base.WriteCompleteCallback, false);
				return;
			}
			smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "No destinations could be obtained to proxy to");
			SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveNoDestinationToProxyTo, smtpInSession.Connector.Name, new object[]
			{
				smtpInSession.ToString()
			});
			ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "No destinations could be obtained to proxy to. Details {0}", smtpInSession.ToString());
			if (this.TransportAppConfig.SmtpInboundProxyConfiguration.PreserveTargetResponse)
			{
				base.SmtpResponse = SmtpResponse.NoProxyDestinationsResponse;
			}
			else
			{
				base.SmtpResponse = SmtpResponse.EncodedProxyFailureResponseNoDestinations;
			}
			this.StartDiscardingMessage();
			if (this.seenEod)
			{
				base.RaiseOnRejectIfNecessary();
				return;
			}
			smtpInSession.RawDataReceivedCompleted();
		}

		// Token: 0x04001B95 RID: 7061
		private SmtpInBdatProxyParser smtpInBdatParser;

		// Token: 0x04001B96 RID: 7062
		private InboundBdatProxyLayer bdatProxyLayer;

		// Token: 0x04001B97 RID: 7063
		private long chunkSize;

		// Token: 0x04001B98 RID: 7064
		private long bytesReceived;

		// Token: 0x04001B99 RID: 7065
		private long totalChunkSize;

		// Token: 0x04001B9A RID: 7066
		private bool isLastChunk;
	}
}
