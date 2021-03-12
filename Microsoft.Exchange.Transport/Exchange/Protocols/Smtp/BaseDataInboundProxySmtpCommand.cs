using System;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200049F RID: 1183
	internal abstract class BaseDataInboundProxySmtpCommand : SmtpCommand
	{
		// Token: 0x0600357F RID: 13695 RVA: 0x000DAF0C File Offset: 0x000D910C
		public BaseDataInboundProxySmtpCommand(ISmtpSession session, ITransportAppConfig transportAppConfig, string protocolCommandKeyword) : base(session, protocolCommandKeyword, "OnDataCommand", LatencyComponent.SmtpReceiveOnDataCommand)
		{
			DataCommandEventArgs dataCommandEventArgs = new DataCommandEventArgs();
			this.CommandEventArgs = dataCommandEventArgs;
			ISmtpInSession smtpInSession = session as ISmtpInSession;
			if (smtpInSession != null)
			{
				dataCommandEventArgs.MailItem = smtpInSession.TransportMailItemWrapper;
				this.messageSizeLimit = (long)smtpInSession.Connector.MaxMessageSize.ToBytes();
				if (smtpInSession.SmtpReceivePerformanceCounters != null)
				{
					smtpInSession.SmtpReceivePerformanceCounters.InboundMessageConnectionsCurrent.Increment();
					smtpInSession.SmtpReceivePerformanceCounters.InboundMessageConnectionsTotal.Increment();
				}
			}
			this.TransportAppConfig = transportAppConfig;
			this.writeCompleteCallback = new InboundProxyLayer.CompletionCallback(this.WriteProxyDataComplete);
		}

		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003580 RID: 13696
		protected abstract long AccumulatedMessageSize { get; }

		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003581 RID: 13697
		protected abstract bool IsProxying { get; }

		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003582 RID: 13698
		protected abstract long EohPosition { get; }

		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000DAFB5 File Offset: 0x000D91B5
		protected bool ShouldDisconnect
		{
			get
			{
				return this.shouldDisconnect;
			}
		}

		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000DAFBD File Offset: 0x000D91BD
		protected InboundProxyLayer.CompletionCallback WriteCompleteCallback
		{
			get
			{
				return this.writeCompleteCallback;
			}
		}

		// Token: 0x06003585 RID: 13701
		protected abstract void StartDiscardingMessage();

		// Token: 0x06003586 RID: 13702 RVA: 0x000DAFC8 File Offset: 0x000D91C8
		public static void FinalizeLatencyTracking(ISmtpInSession session)
		{
			LatencyComponent previousHopLatencyComponent = BaseDataInboundProxySmtpCommand.GetPreviousHopLatencyComponent(session);
			LatencyHeaderManager.HandleLatencyHeaders(null, session.TransportMailItem.RootPart.Headers, session.TransportMailItem.DateReceived, previousHopLatencyComponent);
			LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceive, session.TransportMailItem.LatencyTracker);
			LatencyHeaderManager.FinalizeLatencyHeadersOnFrontend(session.TransportMailItem, session.SmtpInServer.ServerConfiguration.Fqdn);
			LatencyTracker.BeginTrackLatency(LatencyComponent.Delivery, session.TransportMailItem.LatencyTracker);
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000DB040 File Offset: 0x000D9240
		public void ParserEndOfHeadersCallback(HeaderList headerList)
		{
			if (headerList == null)
			{
				throw new ArgumentNullException("headerList");
			}
			if (this.seenEoh)
			{
				throw new InvalidOperationException("EndOfHeadersCallback got called again");
			}
			if (this.EohPosition == -1L)
			{
				throw new InvalidOperationException("The parsers Eoh position cannot be -1");
			}
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBaseDataParserEndOfHeadersCallback);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "ParserEndOfHeadersCallback");
			this.seenEoh = true;
			if (this.discardingMessage)
			{
				return;
			}
			if (!DataBdatHelpers.CheckHeaders(headerList, smtpInSession, this.EohPosition, this) || !DataBdatHelpers.CheckMaxHopCounts(headerList, smtpInSession, this, this.TransportAppConfig.Routing.LocalLoopDetectionEnabled, this.TransportAppConfig.Routing.LocalLoopSubdomainDepth))
			{
				this.StartDiscardingMessage();
				return;
			}
			RestrictedHeaderSet blocked = SmtpInSessionUtils.RestrictedHeaderSetFromPermissions(smtpInSession.Permissions);
			HeaderFirewall.Filter(headerList, blocked);
			string text;
			DataBdatHelpers.PatchHeaders(headerList, smtpInSession, this.TransportAppConfig.SmtpDataConfiguration.AcceptAndFixSmtpAddressWithInvalidLocalPart, out text);
			DataBdatHelpers.UpdateLoopDetectionCounter(headerList, smtpInSession, this.TransportAppConfig.Routing.LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter, this.TransportAppConfig.Routing.LoopDetectionNumberOfTransits, text);
			if (smtpInSession.TransportMailItem != null)
			{
				smtpInSession.TransportMailItem.ExposeMessageHeaders = true;
				smtpInSession.TransportMailItem.InternetMessageId = text;
				if (!this.CheckAttributionAndLoadADRecipientCache(smtpInSession))
				{
					this.StartDiscardingMessage();
					return;
				}
				if (!DataBdatHelpers.CheckMessageSubmitPermissions(smtpInSession, this))
				{
					this.StartDiscardingMessage();
					return;
				}
				if (!SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(smtpInSession.Permissions))
				{
					long num;
					if (SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(smtpInSession.Permissions) && DataBdatHelpers.TryGetOriginalSize(headerList, out num))
					{
						this.originalMessageSize = num;
					}
					if (base.OnlyCheckMessageSizeAfterEoh && DataBdatHelpers.MessageSizeExceeded(this.AccumulatedMessageSize, this.originalMessageSize, this.messageSizeLimit, smtpInSession.Permissions))
					{
						base.SmtpResponse = SmtpResponse.MessageTooLarge;
						base.IsResponseReady = false;
						this.StartDiscardingMessage();
						if (smtpInSession.SmtpReceivePerformanceCounters != null)
						{
							smtpInSession.SmtpReceivePerformanceCounters.MessagesRefusedForSize.Increment();
						}
						return;
					}
				}
				DataInboundProxySmtpCommand.SetOorg(headerList, smtpInSession);
			}
			DataBdatHelpers.EnableEOHEvent(smtpInSession, headerList, out this.eohEventArgs);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000DB230 File Offset: 0x000D9430
		public void WriteProxyDataComplete(SmtpResponse response)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!response.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = response;
				if (response.SmtpResponseType != SmtpResponseType.Success)
				{
					this.StartDiscardingMessage();
				}
			}
			if (this.seenEod)
			{
				this.RaiseOnRejectIfNecessary();
				return;
			}
			smtpInSession.RawDataReceivedCompleted();
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x000DB284 File Offset: 0x000D9484
		protected void ProcessAgentResponse(IAsyncResult ar, EventArgs args)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			SmtpResponse smtpResponse = smtpInSession.AgentSession.EndRaiseEvent(ar);
			this.shouldDisconnect = (!smtpResponse.IsEmpty || smtpInSession.SessionSource.ShouldDisconnect);
			if (this.shouldDisconnect)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxySessionDisconnectByAgent);
				if (!smtpResponse.IsEmpty)
				{
					base.SmtpResponse = smtpResponse;
				}
				this.StartDiscardingMessage();
				return;
			}
			if (args != null && !smtpInSession.SessionSource.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyMessageRejectedByAgent);
				base.SmtpResponse = smtpInSession.SessionSource.SmtpResponse;
				this.originalEventArgs = args;
				this.StartDiscardingMessage();
				smtpInSession.SessionSource.SmtpResponse = SmtpResponse.Empty;
			}
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x000DB348 File Offset: 0x000D9548
		protected void ParserException(Exception e)
		{
			if (e is ExchangeDataException)
			{
				base.SmtpResponse = SmtpResponse.InvalidContent;
				ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "A parsing error has occurred: {0}", new object[]
				{
					e.Message
				});
			}
			else if (e is IOException)
			{
				base.SmtpResponse = SmtpResponse.CTSParseError;
			}
			else if (base.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = SmtpResponse.DataTransactionFailed;
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<Exception>((long)this.GetHashCode(), "Handled parser exception: {0}", e);
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000DB3E8 File Offset: 0x000D95E8
		protected void RaiseOnRejectIfNecessary()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "RaiseOnRejectIfNecessary");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBaseDataRaiseOnRejectIfNecessary);
			if (this.discardingMessage && !this.shouldDisconnect)
			{
				smtpInSession.RaiseOnRejectEvent(null, this.originalEventArgs, base.SmtpResponse, new AsyncCallback(this.ContinueOnReject));
				return;
			}
			this.FinishEodSequence();
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x000DB458 File Offset: 0x000D9658
		protected void ContinueOnReject(IAsyncResult ar)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "OnRejectCallback");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.InboundProxyBaseDataContinueOnReject);
			this.ProcessAgentResponse(ar, null);
			this.FinishEodSequence();
		}

		// Token: 0x0600358D RID: 13709
		protected abstract void FinishEodSequence();

		// Token: 0x0600358E RID: 13710 RVA: 0x000DB4A0 File Offset: 0x000D96A0
		private static LatencyComponent GetPreviousHopLatencyComponent(ISmtpInSession session)
		{
			LatencyComponent result;
			if (session.TransportMailItem.ExtendedProperties.GetValue<uint>("Microsoft.Exchange.Transport.TransportMailItem.InboundProxySequenceNumber", 0U) != 0U)
			{
				result = LatencyComponent.SmtpSend;
			}
			else if (SmtpInSessionUtils.IsAuthenticated(session.RemoteIdentity) && session.AuthMethod == MultilevelAuthMechanism.MUTUALGSSAPI && session.SmtpInServer.Configuration.LocalServer.TransportServer.IsHubTransportServer && session.SmtpInServer.Configuration.LocalServer.TransportServer.IsFrontendTransportServer)
			{
				result = LatencyComponent.DeliveryQueueInternal;
			}
			else if (session.TransportMailItem.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-Hygiene-ReleasedFromQuarantine") != null)
			{
				result = LatencyComponent.QuarantineReleaseOrReport;
			}
			else
			{
				result = LatencyComponent.ExternalServers;
			}
			return result;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x000DB544 File Offset: 0x000D9744
		private bool CheckAttributionAndLoadADRecipientCache(ISmtpInSession session)
		{
			SmtpResponse smtpResponse = DataBdatHelpers.CheckAttributionAndCreateAdRecipientCache(session.TransportMailItem, this.TransportAppConfig.SmtpReceiveConfiguration.RejectUnscopedMessages, session.InboundClientProxyState != InboundClientProxyStates.None, true);
			if (!smtpResponse.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = smtpResponse;
			}
			return smtpResponse.Equals(SmtpResponse.Empty);
		}

		// Token: 0x04001B8B RID: 7051
		protected readonly ITransportAppConfig TransportAppConfig;

		// Token: 0x04001B8C RID: 7052
		protected long messageSizeLimit;

		// Token: 0x04001B8D RID: 7053
		protected bool seenEoh;

		// Token: 0x04001B8E RID: 7054
		protected bool seenEod;

		// Token: 0x04001B8F RID: 7055
		protected bool discardingMessage;

		// Token: 0x04001B90 RID: 7056
		protected EndOfHeadersEventArgs eohEventArgs;

		// Token: 0x04001B91 RID: 7057
		private EventArgs originalEventArgs;

		// Token: 0x04001B92 RID: 7058
		protected long originalMessageSize = long.MaxValue;

		// Token: 0x04001B93 RID: 7059
		private bool shouldDisconnect;

		// Token: 0x04001B94 RID: 7060
		private readonly InboundProxyLayer.CompletionCallback writeCompleteCallback;
	}
}
