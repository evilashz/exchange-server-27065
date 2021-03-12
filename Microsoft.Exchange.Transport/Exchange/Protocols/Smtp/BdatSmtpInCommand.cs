using System;
using System.Linq;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004E0 RID: 1248
	internal class BdatSmtpInCommand : DataBdatCommandBase
	{
		// Token: 0x060039B1 RID: 14769 RVA: 0x000EE883 File Offset: 0x000ECA83
		public BdatSmtpInCommand(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x000EE890 File Offset: 0x000ECA90
		protected override ParseResult Parse(CommandContext commandContext, out string agentEventTopic, out ReceiveCommandEventArgs agentEventArgs)
		{
			if (this.sessionState.TransportMailItem == null)
			{
				agentEventTopic = null;
				agentEventArgs = null;
				return new ParseResult(ParsingStatus.ProtocolError, SmtpResponse.BadCommandSequence, true);
			}
			this.parseResult = BdatSmtpCommandParser.Parse(commandContext, this.sessionState, (this.sessionState.BdatState != null) ? this.sessionState.BdatState.AccumulatedChunkSize : 0L, out this.chunkSize);
			if (this.parseResult.IsFailed)
			{
				agentEventTopic = null;
				agentEventArgs = null;
				this.sessionState.StartDiscardingMessage();
				return new ParseResult(this.parseResult.ParsingStatus, this.parseResult.SmtpResponse, true);
			}
			agentEventTopic = "OnDataCommand";
			agentEventArgs = new DataCommandEventArgs(this.sessionState)
			{
				MailItem = this.sessionState.TransportMailItemWrapper
			};
			this.isLastChunk = BdatSmtpCommandParser.IsLastChunk(this.parseResult.ParsingStatus);
			if (this.sessionState.DiscardingMessage)
			{
				this.parseResult = ParseResult.BadCommandSequence;
			}
			return ParseResult.ParsingComplete;
		}

		// Token: 0x060039B3 RID: 14771 RVA: 0x000EE98B File Offset: 0x000ECB8B
		protected override SmtpInStateMachineEvents GetCommandFailureEvent()
		{
			return SmtpInStateMachineEvents.BdatFailed;
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x000EE990 File Offset: 0x000ECB90
		protected override long AccumulatedMessageSize
		{
			get
			{
				long num = 0L;
				if (this.streamBuilder != null)
				{
					num = this.streamBuilder.TotalBytesWritten;
				}
				if (this.sessionState.TransportMailItem != null)
				{
					num += this.sessionState.TransportMailItem.MimeSize;
				}
				return num;
			}
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x000EE9D8 File Offset: 0x000ECBD8
		protected override ParseResult PreProcess()
		{
			bool flag = this.IsProcessingMessageContextBlob();
			this.streamBuilder = new SmtpInBdatStreamBuilder
			{
				ChunkSize = this.chunkSize
			};
			if (flag)
			{
				this.messageHandler = new MessageContextBlobHandler(this.sessionState, this.sessionState.ExpectedMessageContextBlobs.Dequeue(), this.chunkSize);
			}
			else
			{
				this.messageHandler = new MessageContentHandler(this.sessionState, this.streamBuilder);
			}
			if (!this.sessionState.InitializeBdatState(this.streamBuilder, this.chunkSize, this.messageSizeLimit))
			{
				return ParseResult.DataTransactionFailed;
			}
			if (this.isLastChunk && this.AccumulatedMessageSize + this.chunkSize <= 0L)
			{
				return ParseResult.InvalidLastChunk;
			}
			if (this.isLastChunk && this.chunkSize == 0L)
			{
				return ParseResult.ParsingComplete;
			}
			if (!base.IsEohSeen && !flag)
			{
				this.sessionState.TransportMailItem.MimeDocument.EndOfHeaders = new MimeDocument.EndOfHeadersCallback(base.HandleEoh);
			}
			return ParseResult.MoreDataRequired;
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000EEAD5 File Offset: 0x000ECCD5
		protected override bool TryGetInitialResponse(out SmtpResponse initialResponse)
		{
			initialResponse = SmtpResponse.Empty;
			return false;
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x000EEAE4 File Offset: 0x000ECCE4
		protected override bool ValidateAccumulatedSize(out SmtpResponse failureResponse)
		{
			failureResponse = DataBdatHelpers.ValidateHeaderSize(this.sessionState, this.sessionState.BdatState.AccumulatedChunkSize, this.sessionState.BdatState.IsEohSeen);
			if (failureResponse.IsEmpty)
			{
				failureResponse = DataBdatHelpers.ValidateMessageSize(this.sessionState, this.sessionState.BdatState.MessageSizeLimit, this.sessionState.BdatState.OriginalMessageSize, this.sessionState.BdatState.AccumulatedChunkSize, this.sessionState.BdatState.IsEohSeen);
			}
			return failureResponse.IsEmpty;
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x000EEB81 File Offset: 0x000ECD81
		protected override bool ShouldProcessEoh()
		{
			return !this.sessionState.BdatState.IsEohSeen && this.sessionState.TransportMailItem.MimeDocument.EndOfHeaders != null;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x000EEBB2 File Offset: 0x000ECDB2
		protected override bool ShouldProcessEod()
		{
			return this.isLastChunk && !this.sessionState.DiscardingMessage;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000EEBCC File Offset: 0x000ECDCC
		protected override void PostEoh()
		{
			this.sessionState.BdatState.UpdateState(this.sessionState.TransportMailItem.InternetMessageId, this.originalMessageSize, this.messageSizeLimit, true);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000EEBFB File Offset: 0x000ECDFB
		protected override void PostEod()
		{
			if (this.isLastChunk && !this.IsProcessingMessageContextBlob())
			{
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
				this.sessionState.ReleaseMailItem();
			}
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000EEC24 File Offset: 0x000ECE24
		protected override bool ShouldHandleBareLineFeedInBody()
		{
			return false;
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000EEC28 File Offset: 0x000ECE28
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetFinalResult(ParseAndProcessResult<SmtpInStateMachineEvents> eodResult)
		{
			if (this.sessionState.DiscardingMessage)
			{
				if (this.parseResult.IsFailed)
				{
					return new ParseAndProcessResult<SmtpInStateMachineEvents>(this.parseResult, SmtpInStateMachineEvents.BdatFailed);
				}
				return DataBdatHelpers.CreateResultFromResponse(SmtpResponse.DataTransactionFailed, SmtpInStateMachineEvents.BdatFailed);
			}
			else
			{
				if (!eodResult.SmtpResponse.IsEmpty)
				{
					return eodResult;
				}
				if (!this.isLastChunk)
				{
					return DataBdatHelpers.CreateResultFromResponse(SmtpResponse.OctetsReceived(this.chunkSize), SmtpInStateMachineEvents.BdatProcessed);
				}
				return DataBdatHelpers.CreateResultFromResponse(SmtpResponse.QueuedMailForDelivery(SmtpCommand.GetBracketedString(this.sessionState.TransportMailItem.InternetMessageId)), SmtpInStateMachineEvents.BdatLastProcessed);
			}
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000EECB5 File Offset: 0x000ECEB5
		protected override ParseAndProcessResult<SmtpInStateMachineEvents> GetFailureResult(ParsingStatus parsingStatus, SmtpResponse failureResponse, out bool shouldAbortTransaction)
		{
			if (this.isLastChunk)
			{
				shouldAbortTransaction = true;
			}
			else
			{
				shouldAbortTransaction = false;
			}
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(parsingStatus, failureResponse, this.GetCommandFailureEvent(), false);
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000EECD5 File Offset: 0x000ECED5
		protected override SmtpInStateMachineEvents GetSuccessEvent()
		{
			if (!this.isLastChunk)
			{
				return SmtpInStateMachineEvents.BdatProcessed;
			}
			return SmtpInStateMachineEvents.BdatLastProcessed;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x000EECE2 File Offset: 0x000ECEE2
		private bool IsProcessingMessageContextBlob()
		{
			return this.sessionState.ExpectedMessageContextBlobs.Any<IInboundMessageContextBlob>();
		}

		// Token: 0x04001D45 RID: 7493
		private long chunkSize;

		// Token: 0x04001D46 RID: 7494
		private bool isLastChunk;

		// Token: 0x04001D47 RID: 7495
		private ParseResult parseResult;
	}
}
