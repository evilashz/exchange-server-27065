using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004EE RID: 1262
	internal class MessageContentHandler : IMessageHandler
	{
		// Token: 0x06003A5E RID: 14942 RVA: 0x000F28B9 File Offset: 0x000F0AB9
		public MessageContentHandler(SmtpInSessionState sessionStateToUse, ISmtpInStreamBuilder streamBuilderToUse)
		{
			ArgumentValidator.ThrowIfNull("sessionStateToUse", sessionStateToUse);
			ArgumentValidator.ThrowIfNull("streamBuilderToUse", streamBuilderToUse);
			this.sessionState = sessionStateToUse;
			this.streamBuilder = streamBuilderToUse;
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000F28F0 File Offset: 0x000F0AF0
		public MessageHandlerResult Process(CommandContext commandContext, out SmtpResponse smtpResponse)
		{
			ArgumentValidator.ThrowIfNull("commandContext", commandContext);
			int num;
			bool flag;
			SmtpResponse smtpResponse2;
			if (!this.WriteContentsToStream(commandContext, out num, out flag, out smtpResponse2))
			{
				this.finalResponse = smtpResponse2;
				this.sessionState.StartDiscardingMessage();
			}
			if (commandContext.OriginalLength != num)
			{
				this.sessionState.PutBackReceivedBytes(commandContext.OriginalLength - num);
			}
			if (!flag)
			{
				smtpResponse = SmtpResponse.Empty;
				return MessageHandlerResult.MoreDataRequired;
			}
			if (!this.finalResponse.IsEmpty)
			{
				smtpResponse = this.finalResponse;
				return MessageHandlerResult.Failure;
			}
			smtpResponse = SmtpResponse.Empty;
			return MessageHandlerResult.Complete;
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000F297C File Offset: 0x000F0B7C
		private bool WriteContentsToStream(CommandContext commandContext, out int numberOfBytesWritten, out bool eodFound, out SmtpResponse failureResponse)
		{
			numberOfBytesWritten = 0;
			failureResponse = SmtpResponse.Empty;
			try
			{
				eodFound = this.streamBuilder.Write(commandContext, out numberOfBytesWritten);
			}
			catch (Exception exception)
			{
				eodFound = this.streamBuilder.IsEodSeen;
				if (DataBdatHelpers.IsFilterableException(exception))
				{
					failureResponse = DataBdatHelpers.HandleFilterableException(exception, this.sessionState);
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x04001D64 RID: 7524
		private readonly SmtpInSessionState sessionState;

		// Token: 0x04001D65 RID: 7525
		private readonly ISmtpInStreamBuilder streamBuilder;

		// Token: 0x04001D66 RID: 7526
		private SmtpResponse finalResponse = SmtpResponse.Empty;
	}
}
