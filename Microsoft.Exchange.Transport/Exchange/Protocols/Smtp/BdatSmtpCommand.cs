using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Common.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000454 RID: 1108
	internal class BdatSmtpCommand : BaseDataSmtpCommand
	{
		// Token: 0x0600335F RID: 13151 RVA: 0x000CC93C File Offset: 0x000CAB3C
		public BdatSmtpCommand(ISmtpSession session, ITransportAppConfig transportAppConfig, SmtpMessageContextBlob smtpMessageContextBlob) : base(session, "BDAT", "OnDataCommand", LatencyComponent.SmtpReceiveOnDataCommand, transportAppConfig)
		{
			this.smtpMessageContextBlob = smtpMessageContextBlob;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000CC958 File Offset: 0x000CAB58
		internal BdatSmtpCommand(int chunkSize, bool isLastChunk) : base("BDAT")
		{
			this.chunkSize = (long)chunkSize;
			this.isLastChunk = isLastChunk;
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x000CC974 File Offset: 0x000CAB74
		internal bool IsBdat0Last
		{
			get
			{
				return this.isLastChunk && this.chunkSize == 0L;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003362 RID: 13154 RVA: 0x000CC98A File Offset: 0x000CAB8A
		protected override SmtpInParser SmtpInParser
		{
			get
			{
				return this.smtpInBdatParser;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000CC994 File Offset: 0x000CAB94
		protected override long AccumulatedMessageSize
		{
			get
			{
				ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
				long num = this.SmtpInParser.TotalBytesWritten;
				if (smtpInSession.TransportMailItem != null)
				{
					num += smtpInSession.TransportMailItem.MimeSize;
				}
				return num;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003364 RID: 13156 RVA: 0x000CC9D0 File Offset: 0x000CABD0
		public override bool IsBlob
		{
			get
			{
				return this.SmtpMessageContextBlob != null;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000CC9DE File Offset: 0x000CABDE
		public SmtpMessageContextBlob SmtpMessageContextBlob
		{
			get
			{
				return this.smtpMessageContextBlob;
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003366 RID: 13158 RVA: 0x000CC9E6 File Offset: 0x000CABE6
		public long ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000CC9EE File Offset: 0x000CABEE
		public bool IsLastChunk
		{
			get
			{
				return this.isLastChunk;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003368 RID: 13160 RVA: 0x000CC9F6 File Offset: 0x000CABF6
		public bool IsLastChunkOutbound
		{
			get
			{
				return this.isLastChunkOutbound;
			}
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x000CC9FE File Offset: 0x000CABFE
		public static bool RunBdatSequenceChecks(SmtpCommand command, ISmtpInSession session, out bool isBdatOngoing, out bool isFirstChunk)
		{
			isBdatOngoing = false;
			if (session.IsBdatOngoing)
			{
				isBdatOngoing = true;
				isFirstChunk = false;
			}
			else
			{
				isFirstChunk = true;
				if (!command.VerifyHelloReceived() || !command.VerifyMailFromReceived() || !command.VerifyRcptToReceived())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x000CCA31 File Offset: 0x000CAC31
		public static void SetMessageTooLargeResponse(ISmtpInSession session, SmtpCommand command, bool updateCounter = true)
		{
			command.SmtpResponse = SmtpResponse.MessageTooLarge;
			command.ParsingStatus = ParsingStatus.MoreDataRequired;
			command.IsResponseReady = false;
			if (session.SmtpReceivePerformanceCounters != null && updateCounter)
			{
				session.SmtpReceivePerformanceCounters.MessagesRefusedForSize.Increment();
			}
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x000CCA68 File Offset: 0x000CAC68
		protected override void InboundParseCommandInternal()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "BDAT.InboundParseCommand");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			bool flag;
			if (!BdatSmtpCommand.RunBdatSequenceChecks(this, smtpInSession, out flag, out this.isFirstChunk))
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
				base.StartDiscardingMessage();
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
				return;
			}
			this.isLastChunk = BdatSmtpCommandParser.IsLastChunk(parseResult.ParsingStatus);
			if (this.IsBlob)
			{
				if (this.chunkSize > (long)this.smtpMessageContextBlob.MaxBlobSize.ToBytes())
				{
					BdatSmtpCommand.SetMessageTooLargeResponse(smtpInSession, this, true);
					base.StartDiscardingMessage();
					return;
				}
				if (!this.SmtpMessageContextBlob.VerifyPermission(smtpInSession.Permissions))
				{
					base.SmtpResponse = SmtpResponse.NotAuthorized;
					base.ParsingStatus = ParsingStatus.MoreDataRequired;
					base.IsResponseReady = false;
					base.StartDiscardingMessage();
					return;
				}
			}
			else if ((this.seenEoh || !base.OnlyCheckMessageSizeAfterEoh) && BdatSmtpCommandParser.IsMessageSizeExceeded(SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(smtpInSession.Permissions), this.totalChunkSize, this.chunkSize, this.originalMessageSize, this.messageSizeLimit, smtpInSession.LogSession, ExTraceGlobals.SmtpReceiveTracer))
			{
				BdatSmtpCommand.SetMessageTooLargeResponse(smtpInSession, this, true);
				base.StartDiscardingMessage();
				return;
			}
			if (this.discardingMessage)
			{
				base.SmtpResponse = SmtpResponse.BadCommandSequence;
				base.IsResponseReady = false;
			}
			base.ParsingStatus = ParsingStatus.MoreDataRequired;
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x000CCC0C File Offset: 0x000CAE0C
		internal override void InboundProcessCommand()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "BDAT.InboundProcessCommand");
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2848337213U);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			base.IsResponseReady = false;
			if (this.IsBlob)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.BdatBlobInboundProcessCommand);
				this.bodyStream = new MultiByteArrayMemoryStream();
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Receiving blob {0}", new object[]
				{
					this.smtpMessageContextBlob.Name
				});
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.BdatInboundProcessCommand);
				if (!this.discardingMessage)
				{
					base.SetupMessageStream(true);
				}
				else if (smtpInSession.TransportMailItem != null)
				{
					smtpInSession.DeleteTransportMailItem();
				}
				this.smtpInBdatParser = new SmtpInBdatParser();
				this.smtpInBdatParser.ChunkSize = this.chunkSize;
				this.smtpInBdatParser.IsDiscardingData = this.discardingMessage;
				this.smtpInBdatParser.BodyStream = this.bodyStream;
				this.smtpInBdatParser.ExceptionFilter = new ExceptionFilter(base.ParserException);
				if (smtpInSession.MimeDocument != null && !this.discardingMessage)
				{
					smtpInSession.MimeDocument.EndOfHeaders = new MimeDocument.EndOfHeadersCallback(base.ParserEndOfHeadersCallback);
				}
			}
			smtpInSession.SetRawModeAfterCommandCompleted(new RawDataHandler(this.RawDataReceived));
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x000CCD56 File Offset: 0x000CAF56
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x000CCD58 File Offset: 0x000CAF58
		internal override void OutboundFormatCommand()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			bool flag = false;
			try
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Fetching the message stream");
				this.bodyStream = this.GetFirewalledStream();
				if (this.bodyStream != null)
				{
					long length = this.bodyStream.Length;
					if (!smtpOutSession.RemoteIsAuthenticated && !smtpOutSession.IsAuthenticated && smtpOutSession.AdvertisedEhloOptions.Size == SizeMode.Enabled && smtpOutSession.AdvertisedEhloOptions.MaxSize > 0L && length > smtpOutSession.AdvertisedEhloOptions.MaxSize)
					{
						flag = true;
						ExTraceGlobals.SmtpSendTracer.TraceError((long)this.GetHashCode(), "Message from {0} was NDR'ed because the size was {1} whereas the maximum allowed size by the receiving server (at {2}) was {3}", new object[]
						{
							smtpOutSession.RoutedMailItem.From.ToString(),
							length,
							smtpOutSession.RemoteEndPoint,
							smtpOutSession.AdvertisedEhloOptions.MaxSize
						});
						smtpOutSession.RoutedMailItem.AddDsnParameters("MaxMessageSizeInKB", smtpOutSession.AdvertisedEhloOptions.MaxSize >> 10);
						smtpOutSession.RoutedMailItem.AddDsnParameters("CurrentMessageSizeInKB", length >> 10);
						smtpOutSession.AckMessage(AckStatus.Fail, AckReason.BdatOverAdvertisedSizeLimit);
					}
					else
					{
						this.bodyStream.Seek(0L, SeekOrigin.Begin);
					}
				}
				else
				{
					flag = true;
					ExTraceGlobals.SmtpSendTracer.TraceError<string>((long)this.GetHashCode(), "Message from {0} was NDR'ed because the body stream was null", smtpOutSession.RoutedMailItem.From.ToString());
					smtpOutSession.AckMessage(AckStatus.Fail, AckReason.UnexpectedException);
				}
			}
			catch (IOException arg)
			{
				flag = true;
				ExTraceGlobals.SmtpSendTracer.TraceError<string, IOException>((long)this.GetHashCode(), "Unable to open message-stream, for message from: {0}, acking it retry and moving on to the next one. The exception was: {1}", smtpOutSession.RoutedMailItem.From.ToString(), arg);
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
			this.chunkSize = (this.totalChunkSize = this.bodyStream.Length);
			if (this.IsBlob)
			{
				base.ProtocolCommandString = string.Format(CultureInfo.InvariantCulture, "BDAT {0}", new object[]
				{
					this.chunkSize
				});
				return;
			}
			base.ProtocolCommandString = string.Format(CultureInfo.InvariantCulture, "BDAT {0} LAST", new object[]
			{
				this.chunkSize
			});
			this.isLastChunkOutbound = true;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x000CD004 File Offset: 0x000CB204
		internal override void OutboundProcessResponse()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			string statusCode = base.SmtpResponse.StatusCode;
			bool issueBetweenMsgRset = false;
			if ((base.SmtpResponse.SmtpResponseType == SmtpResponseType.PermanentError && !Util.DowngradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)) || (base.SmtpResponse.SmtpResponseType == SmtpResponseType.TransientError && Util.UpgradeCustomPermanentFailure(smtpOutSession.Connector.ErrorPolicies, base.SmtpResponse, this.transportAppConfig)))
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Message body response was: ", base.SmtpResponse);
				smtpOutSession.AckMessage(AckStatus.Fail, base.SmtpResponse);
				issueBetweenMsgRset = true;
			}
			else if (statusCode[0] != '2')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<string, IPEndPoint, SmtpResponse>((long)this.GetHashCode(), "Failed to deliver message from {0} to {1}, Status: {2}", smtpOutSession.RoutedMailItem.From.ToString(), smtpOutSession.RemoteEndPoint, base.SmtpResponse);
				smtpOutSession.AckMessage(AckStatus.Retry, base.SmtpResponse);
				issueBetweenMsgRset = true;
			}
			else
			{
				if (this.IsBlob)
				{
					if (!smtpOutSession.HasMoreBlobsPending())
					{
						ExTraceGlobals.SmtpSendTracer.TraceDebug<SmtpOutSession.SessionState, string>((long)this.GetHashCode(), "Setting the NextState from {0} {1}", smtpOutSession.NextState, "bdat");
						smtpOutSession.NextState = SmtpOutSession.SessionState.Bdat;
					}
					return;
				}
				ExTraceGlobals.SmtpSendTracer.TraceDebug<string, IPEndPoint>((long)this.GetHashCode(), "Delivered message from {0} to {1}", smtpOutSession.RoutedMailItem.From.ToString(), smtpOutSession.RemoteEndPoint);
				smtpOutSession.AckMessage(AckStatus.Success, base.SmtpResponse, this.totalChunkSize);
			}
			smtpOutSession.PrepareForNextMessage(issueBetweenMsgRset);
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x000CD1A4 File Offset: 0x000CB3A4
		protected override void StoreCurrentDataState()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (smtpInSession.BdatState == null)
			{
				smtpInSession.BdatState = new SmtpInBdatState();
			}
			smtpInSession.BdatState.TotalChunkSize = this.totalChunkSize;
			smtpInSession.BdatState.DiscardingMessage = this.discardingMessage;
			smtpInSession.BdatState.SeenEoh = this.seenEoh;
			smtpInSession.BdatState.MessageId = this.messageId;
			smtpInSession.BdatState.OriginalMessageSize = this.originalMessageSize;
			smtpInSession.BdatState.MessageSizeLimit = this.messageSizeLimit;
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x000CD238 File Offset: 0x000CB438
		protected override bool SetSuccessResponse()
		{
			if (this.IsBlob && !this.discardingMessage)
			{
				base.SmtpResponse = SmtpResponse.OctetsReceived(this.chunkSize);
				return true;
			}
			if (this.isLastChunk)
			{
				return base.SetSuccessResponse();
			}
			if (!this.discardingMessage)
			{
				base.SmtpResponse = SmtpResponse.OctetsReceived(this.chunkSize);
			}
			return !this.discardingMessage;
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x000CD29C File Offset: 0x000CB49C
		protected override AsyncReturnType SubmitMessageIfReady()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!this.discardingMessage)
			{
				this.totalChunkSize += this.chunkSize;
			}
			if (this.isLastChunk)
			{
				if (!this.discardingMessage)
				{
					if (base.SubmitMessage() == AsyncReturnType.Async)
					{
						return AsyncReturnType.Async;
					}
				}
				else
				{
					smtpInSession.AbortMailTransaction();
				}
				smtpInSession.BdatState = null;
			}
			else if (!this.discardingMessage)
			{
				this.StoreCurrentDataState();
			}
			return AsyncReturnType.Sync;
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x000CD30C File Offset: 0x000CB50C
		protected override void ContinueSubmitMessageIfReady()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.BdatState = null;
			base.EodDone(true);
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x000CD334 File Offset: 0x000CB534
		protected override Stream GetFirewalledStream()
		{
			if (this.IsBlob)
			{
				return this.smtpMessageContextBlob.SerializeBlob((SmtpOutSession)base.SmtpSession);
			}
			return base.GetFirewalledStream();
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x000CD35C File Offset: 0x000CB55C
		protected override AsyncReturnType RawDataReceived(byte[] data, int offset, int numBytes)
		{
			if (!this.IsBlob)
			{
				return base.RawDataReceived(data, offset, numBytes);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int>((long)this.GetHashCode(), "BDAT.RawDataReceived received {0} bytes", numBytes);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			int num = (int)Math.Min(this.chunkSize - this.totalBytesRead, (long)numBytes);
			this.totalBytesRead += (long)num;
			if (!this.discardingMessage)
			{
				this.bodyStream.Write(data, offset, num);
			}
			else
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "Discarding message");
			}
			if (this.totalBytesRead == this.chunkSize)
			{
				bool flag = true;
				if (!this.discardingMessage)
				{
					this.bodyStream.Seek(0L, SeekOrigin.Begin);
					try
					{
						this.SmtpMessageContextBlob.DeserializeBlob(this.bodyStream, smtpInSession, this.chunkSize);
					}
					catch (FormatException ex)
					{
						smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, ex.Message);
						smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DeserializeBlobFailed);
						ExTraceGlobals.SmtpReceiveTracer.TraceError<string, FormatException>((long)this.GetHashCode(), "Encountered exception while processing the blob {0}. Details: {1}", this.SmtpMessageContextBlob.Name, ex);
						SystemProbeHelper.SmtpReceiveTracer.TraceFail<string, FormatException>(smtpInSession.TransportMailItem, (long)this.GetHashCode(), "Encountered exception while processing the blob {0}. Details: {1}", this.SmtpMessageContextBlob.Name, ex);
						SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProcessingBlobFailed, this.smtpMessageContextBlob.Name, new object[]
						{
							this.smtpMessageContextBlob.Name,
							ex
						});
						if (this.SmtpMessageContextBlob.IsMandatory)
						{
							base.SmtpResponse = SmtpResponse.XMessageContextError;
							flag = false;
						}
						if (this.transportAppConfig.MessageContextBlobConfiguration.WatsonReportOnFailureEnabled)
						{
							this.SendWatsonReportWithoutCrashingTheProcess(ex, smtpInSession);
						}
					}
					if (flag)
					{
						this.SetSuccessResponse();
					}
				}
				base.ParsingStatus = ParsingStatus.Complete;
				base.IsResponseReady = true;
				smtpInSession.BdatState = null;
				if (this.bodyStream != null)
				{
					this.bodyStream.Close();
					this.bodyStream = null;
				}
			}
			if (numBytes != num)
			{
				smtpInSession.PutBackReceivedBytes(numBytes - num);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int>((long)this.GetHashCode(), "BDAT.RawDataReceived consumed {0} bytes", num);
			return AsyncReturnType.Sync;
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x000CD584 File Offset: 0x000CB784
		private void SendWatsonReportWithoutCrashingTheProcess(FormatException exception, ISmtpInSession smtpInSession)
		{
			if (DateTime.UtcNow - BdatSmtpCommand.watsonReportSentTimeForMessageContextParseFailure < TimeSpan.FromHours(1.0))
			{
				return;
			}
			if (exception.InnerException is TransientException)
			{
				return;
			}
			BdatSmtpCommand.watsonReportSentTimeForMessageContextParseFailure = DateTime.UtcNow;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("Exception={0}", exception));
			stringBuilder.AppendLine(string.Format("Time={0}", DateTime.UtcNow));
			stringBuilder.AppendLine(string.Format("ComputerName={0}", Environment.MachineName));
			stringBuilder.AppendLine(string.Format("AdvertisedEhloOptions={0}", smtpInSession.AdvertisedEhloOptions.CreateSmtpResponse(SmtpMessageContextBlob.AdrcSmtpMessageContextBlobInstance, SmtpMessageContextBlob.ExtendedPropertiesSmtpMessageContextBlobInstance, SmtpMessageContextBlob.FastIndexSmtpMessageContextBlobInstance)));
			stringBuilder.AppendLine(string.Format("Breadcrumbs={0}", smtpInSession.Breadcrumbs));
			stringBuilder.AppendLine(string.Format("ChunkSize={0}", this.totalChunkSize));
			stringBuilder.AppendLine(string.Format("DiscardingMessage={0}", this.discardingMessage));
			stringBuilder.AppendLine(string.Format("TotalBytesRead={0}", this.totalBytesRead));
			stringBuilder.AppendLine(string.Format("BodyStream Length={0}", (this.bodyStream == null) ? "null" : this.bodyStream.Length.ToString(CultureInfo.InvariantCulture)));
			stringBuilder.AppendLine(string.Format("BodyStream Position={0}", (this.bodyStream == null) ? "null" : this.bodyStream.Position.ToString(CultureInfo.InvariantCulture)));
			ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, exception.GetType().Name, exception.StackTrace, exception.GetHashCode().ToString(CultureInfo.InvariantCulture), exception.TargetSite.Name, stringBuilder.ToString());
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x000CD784 File Offset: 0x000CB984
		private void RestoreBdatState()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			this.totalChunkSize = smtpInSession.BdatState.TotalChunkSize;
			this.discardingMessage = smtpInSession.BdatState.DiscardingMessage;
			this.seenEoh = smtpInSession.BdatState.SeenEoh;
			this.messageId = smtpInSession.BdatState.MessageId;
			this.originalMessageSize = smtpInSession.BdatState.OriginalMessageSize;
			this.messageSizeLimit = smtpInSession.BdatState.MessageSizeLimit;
		}

		// Token: 0x04001A17 RID: 6679
		private static DateTime watsonReportSentTimeForMessageContextParseFailure = DateTime.MinValue;

		// Token: 0x04001A18 RID: 6680
		private SmtpInBdatParser smtpInBdatParser;

		// Token: 0x04001A19 RID: 6681
		private long chunkSize;

		// Token: 0x04001A1A RID: 6682
		private long totalChunkSize;

		// Token: 0x04001A1B RID: 6683
		private long totalBytesRead;

		// Token: 0x04001A1C RID: 6684
		private SmtpMessageContextBlob smtpMessageContextBlob;

		// Token: 0x04001A1D RID: 6685
		private bool isLastChunkOutbound;
	}
}
