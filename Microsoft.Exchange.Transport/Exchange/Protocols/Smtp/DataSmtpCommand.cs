using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000456 RID: 1110
	internal class DataSmtpCommand : BaseDataSmtpCommand
	{
		// Token: 0x0600337C RID: 13180 RVA: 0x000CD95C File Offset: 0x000CBB5C
		public DataSmtpCommand(ISmtpSession session, ITransportAppConfig transportAppConfig) : base(session, "DATA", "OnDataCommand", LatencyComponent.SmtpReceiveOnDataCommand, transportAppConfig)
		{
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000CD971 File Offset: 0x000CBB71
		internal DataSmtpCommand() : base("DATA")
		{
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x0600337E RID: 13182 RVA: 0x000CD97E File Offset: 0x000CBB7E
		protected override SmtpInParser SmtpInParser
		{
			get
			{
				return this.smtpInDataParser;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x0600337F RID: 13183 RVA: 0x000CD986 File Offset: 0x000CBB86
		protected override long AccumulatedMessageSize
		{
			get
			{
				return this.SmtpInParser.TotalBytesWritten;
			}
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000CD994 File Offset: 0x000CBB94
		internal static bool RunInboundParseChecks(ISmtpInSession session, SmtpCommand command)
		{
			ArgumentValidator.ThrowIfNull("session", session);
			ArgumentValidator.ThrowIfNull("command", command);
			if (!command.VerifyHelloReceived() || !command.VerifyMailFromReceived() || !command.VerifyRcptToReceived() || !command.VerifyNoOngoingBdat())
			{
				return false;
			}
			ArgumentValidator.ThrowIfNull("session.TransportMailItem", session.TransportMailItem);
			ParseResult parseResult = DataSmtpCommandParser.Parse(CommandContext.FromSmtpCommand(command), SmtpInSessionState.FromSmtpInSession(session));
			command.SmtpResponse = parseResult.SmtpResponse;
			command.ParsingStatus = parseResult.ParsingStatus;
			return command.ParsingStatus == ParsingStatus.MoreDataRequired;
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000CDA20 File Offset: 0x000CBC20
		protected override void InboundParseCommandInternal()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "DATA.InboundParseCommand");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DATAInboundParseCommand);
			if (!DataSmtpCommand.RunInboundParseChecks(smtpInSession, this))
			{
				return;
			}
			this.isLastChunk = true;
			this.isFirstChunk = true;
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000CDA70 File Offset: 0x000CBC70
		internal override void InboundProcessCommand()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "DATA.InboundProcessCommand");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DATAInboundProcessCommand);
			if (base.ParsingStatus != ParsingStatus.MoreDataRequired)
			{
				return;
			}
			bool expectBinaryContent = smtpInSession.Connector.EightBitMimeEnabled || !this.transportAppConfig.SmtpDataConfiguration.InboundApplyMissingEncoding;
			if (!base.SetupMessageStream(expectBinaryContent))
			{
				base.ParsingStatus = ParsingStatus.Error;
				return;
			}
			this.smtpInDataParser = new SmtpInDataParser
			{
				BodyStream = this.bodyStream,
				ExceptionFilter = new ExceptionFilter(base.ParserException)
			};
			smtpInSession.MimeDocument.EndOfHeaders = new MimeDocument.EndOfHeadersCallback(base.ParserEndOfHeadersCallback);
			base.SmtpResponse = SmtpResponse.StartData;
			smtpInSession.SetRawModeAfterCommandCompleted(new RawDataHandler(this.RawDataReceived));
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000CDB46 File Offset: 0x000CBD46
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000CDB48 File Offset: 0x000CBD48
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (this.sentDataVerb)
			{
				return;
			}
			bool flag = false;
			try
			{
				Stream firewalledStream = this.GetFirewalledStream();
				if (firewalledStream != null)
				{
					this.bodyStream = new DotStuffingStream(firewalledStream, this.transportAppConfig.SmtpDataConfiguration.OutboundRejectBareLinefeeds);
					long length = firewalledStream.Length;
					if (!smtpOutSession.RemoteIsAuthenticated && !smtpOutSession.IsAuthenticated && smtpOutSession.AdvertisedEhloOptions.Size == SizeMode.Enabled && smtpOutSession.AdvertisedEhloOptions.MaxSize > 0L && length > smtpOutSession.AdvertisedEhloOptions.MaxSize)
					{
						flag = true;
						ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Message from {0} was NDR'ed because the size was {1} whereas the maximum allowed size by the receiving server (at {2}) was {3}", new object[]
						{
							smtpOutSession.RoutedMailItem.From.ToString(),
							length,
							smtpOutSession.SessionProps.RemoteEndPoint,
							smtpOutSession.AdvertisedEhloOptions.MaxSize
						});
						smtpOutSession.RoutedMailItem.AddDsnParameters("MaxMessageSizeInKB", smtpOutSession.AdvertisedEhloOptions.MaxSize >> 10);
						smtpOutSession.RoutedMailItem.AddDsnParameters("CurrentMessageSizeInKB", length >> 10);
						smtpOutSession.AckMessage(AckStatus.Fail, AckReason.DataOverAdvertisedSizeLimit);
					}
				}
				else
				{
					flag = true;
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from {0} was NDR'ed because the body stream was null", smtpOutSession.RoutedMailItem.From.ToString());
					smtpOutSession.AckMessage(AckStatus.Fail, AckReason.UnexpectedException);
				}
			}
			catch (IOException ex)
			{
				flag = true;
				ExTraceGlobals.SmtpSendTracer.TraceError<string, string>((long)this.GetHashCode(), "Unable to open message-stream, for message from: {0}, acking it retry and moving on to the next one. The IOException message was: {1}", smtpOutSession.RoutedMailItem.From.ToString(), ex.Message);
				smtpOutSession.AckMessage(AckStatus.Retry, SmtpResponse.CTSParseError);
			}
			if (flag)
			{
				if (this.bodyStream != null)
				{
					this.bodyStream.Close();
					this.bodyStream = null;
				}
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			base.ProtocolCommandString = "DATA";
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000CDD74 File Offset: 0x000CBF74
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			if (smtpOutSession.NextHopConnection == null || smtpOutSession.RoutedMailItem == null)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Connection already marked for Failover or the message has already been acked for a non-success status.  Not processing response for the DATA command: {0}", base.SmtpResponse);
				return;
			}
			if ((base.SmtpResponse.SmtpResponseType == SmtpResponseType.PermanentError && !Util.DowngradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)) || (base.SmtpResponse.SmtpResponseType == SmtpResponseType.TransientError && Util.UpgradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string, SmtpResponse>((long)this.GetHashCode(), "DATA command for message from {0} failed with: {1}", smtpOutSession.RoutedMailItem.From.ToString(), base.SmtpResponse);
				smtpOutSession.AckMessage(AckStatus.Fail, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			if (this.sentDataVerb)
			{
				bool issueBetweenMsgRset = false;
				if (statusCode[0] != '2')
				{
					ExTraceGlobals.SmtpSendTracer.TraceError<string, IPEndPoint, SmtpResponse>((long)this.GetHashCode(), "Failed to deliver message from {0} to {1}, Status: {2}", smtpOutSession.RoutedMailItem.From.ToString(), smtpOutSession.RemoteEndPoint, base.SmtpResponse);
					smtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
					issueBetweenMsgRset = true;
				}
				else
				{
					ExTraceGlobals.SmtpSendTracer.TraceDebug<string, IPEndPoint>((long)this.GetHashCode(), "Delivered message from {0} to {1}", smtpOutSession.RoutedMailItem.From.ToString(), smtpOutSession.RemoteEndPoint);
					smtpOutSession.AckMessage(AckStatus.Success, base.SmtpResponse, this.bodyStream.Position);
				}
				smtpOutSession.PrepareForNextMessage(issueBetweenMsgRset);
				return;
			}
			if (!string.Equals(statusCode, "354", StringComparison.Ordinal))
			{
				smtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
				smtpOutSession.PrepareForNextMessage(true);
				return;
			}
			this.sentDataVerb = true;
			base.ParsingStatus = ParsingStatus.MoreDataRequired;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000CDF60 File Offset: 0x000CC160
		protected override AsyncReturnType SubmitMessageIfReady()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!this.discardingMessage && base.SmtpResponse.StatusCode[0] == '2')
			{
				return base.SubmitMessage();
			}
			smtpInSession.AbortMailTransaction();
			return AsyncReturnType.Sync;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000CDFA7 File Offset: 0x000CC1A7
		protected override void ContinueSubmitMessageIfReady()
		{
			base.EodDone(true);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000CDFB1 File Offset: 0x000CC1B1
		protected override void StoreCurrentDataState()
		{
		}

		// Token: 0x04001A1F RID: 6687
		private SmtpInDataParser smtpInDataParser;

		// Token: 0x04001A20 RID: 6688
		private bool sentDataVerb;
	}
}
