using System;
using System.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E4 RID: 1252
	internal class DataSmtpInCommand : DataBdatCommandBase
	{
		// Token: 0x06003A0D RID: 14861 RVA: 0x000F09C9 File Offset: 0x000EEBC9
		public DataSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x06003A0E RID: 14862 RVA: 0x000F09D4 File Offset: 0x000EEBD4
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			ParseResult result = DataSmtpCommandParser.Parse(commandContext, this.sessionState);
			if (result.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
			}
			else
			{
				agentEventTopic = "OnDataCommand";
				agentEventArgs = new DataCommandEventArgs(this.sessionState)
				{
					MailItem = this.sessionState.TransportMailItemWrapper
				};
			}
			return result;
		}

		// Token: 0x06003A0F RID: 14863 RVA: 0x000F0A27 File Offset: 0x000EEC27
		protected override SmtpInStateMachineEvents GetCommandFailureEvent()
		{
			return SmtpInStateMachineEvents.DataFailed;
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000F0A2C File Offset: 0x000EEC2C
		protected override ParseResult PreProcess()
		{
			bool allowBinaryContent = this.sessionState.ReceiveConnector.EightBitMimeEnabled || !this.sessionState.Configuration.TransportConfiguration.InboundApplyMissingEncoding;
			this.streamBuilder = new SmtpInDataStreamBuilder();
			this.messageHandler = new MessageContentHandler(this.sessionState, this.streamBuilder);
			if (!base.SetupMessageStream(allowBinaryContent))
			{
				this.sessionState.StartDiscardingMessage();
				return ParseResult.DataTransactionFailed;
			}
			this.sessionState.TransportMailItem.MimeDocument.EndOfHeaders = new MimeDocument.EndOfHeadersCallback(base.HandleEoh);
			return ParseResult.MoreDataRequired;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000F0AC9 File Offset: 0x000EECC9
		protected override bool TryGetInitialResponse(out SmtpResponse initialResponse)
		{
			initialResponse = SmtpResponse.StartData;
			return true;
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000F0AD8 File Offset: 0x000EECD8
		protected override bool ValidateAccumulatedSize(out SmtpResponse failureResponse)
		{
			failureResponse = DataBdatHelpers.ValidateHeaderSize(this.sessionState, this.AccumulatedMessageSize, base.IsEohSeen);
			if (failureResponse.IsEmpty)
			{
				failureResponse = DataBdatHelpers.ValidateMessageSize(this.sessionState, this.messageSizeLimit, this.originalMessageSize, this.AccumulatedMessageSize, base.IsEohSeen);
			}
			return failureResponse.IsEmpty;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000F0B39 File Offset: 0x000EED39
		protected override bool ShouldProcessEoh()
		{
			return !base.IsEohSeen;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000F0B44 File Offset: 0x000EED44
		protected override bool ShouldProcessEod()
		{
			return !this.sessionState.DiscardingMessage;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000F0B54 File Offset: 0x000EED54
		protected override void PostEoh()
		{
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000F0B56 File Offset: 0x000EED56
		protected override void PostEod()
		{
			this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
			this.sessionState.ReleaseMailItem();
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000F0B6F File Offset: 0x000EED6F
		protected override bool ShouldHandleBareLineFeedInBody()
		{
			return true;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000F0B74 File Offset: 0x000EED74
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetFinalResult(ParseAndProcessResult<SmtpInStateMachineEvents> eodResult)
		{
			if (this.sessionState.DiscardingMessage)
			{
				return DataBdatHelpers.CreateResultFromResponse(SmtpResponse.DataTransactionFailed, SmtpInStateMachineEvents.DataFailed);
			}
			if (!eodResult.SmtpResponse.IsEmpty)
			{
				return eodResult;
			}
			string recordId = this.sessionState.TransportMailItem.RecordId.ToString(CultureInfo.InvariantCulture);
			SmtpResponse response = SmtpResponse.QueuedMailForDelivery(SmtpCommand.GetBracketedString(this.sessionState.TransportMailItem.InternetMessageId), recordId, this.sessionState.Configuration.TransportConfiguration.PhysicalMachineName, this.sessionState.Configuration.TransportConfiguration.SmtpDataResponse);
			return DataBdatHelpers.CreateResultFromResponse(response, SmtpInStateMachineEvents.DataProcessed);
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x000F0C18 File Offset: 0x000EEE18
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetFailureResult(ParsingStatus parsingStatus, SmtpResponse failureResponse, out bool shouldAbortTransaction)
		{
			shouldAbortTransaction = true;
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(parsingStatus, failureResponse, SmtpInStateMachineEvents.DataFailed, false);
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x000F0C26 File Offset: 0x000EEE26
		protected override SmtpInStateMachineEvents GetSuccessEvent()
		{
			return SmtpInStateMachineEvents.DataProcessed;
		}
	}
}
