using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004DF RID: 1247
	internal abstract class DataBdatCommandBase : SmtpInCommandBase
	{
		// Token: 0x06003984 RID: 14724 RVA: 0x000EC0B4 File Offset: 0x000EA2B4
		protected DataBdatCommandBase(SmtpInSessionState sessionState, AwaitCompletedDelegate awaitCompletedDelegate) : base(sessionState, awaitCompletedDelegate)
		{
			this.messageSizeLimit = (long)sessionState.ReceiveConnector.MaxMessageSize.ToBytes();
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06003985 RID: 14725 RVA: 0x000EC107 File Offset: 0x000EA307
		protected virtual long AccumulatedMessageSize
		{
			get
			{
				if (this.streamBuilder != null)
				{
					return this.streamBuilder.TotalBytesWritten;
				}
				return 0L;
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x06003986 RID: 14726 RVA: 0x000EC11F File Offset: 0x000EA31F
		// (set) Token: 0x06003987 RID: 14727 RVA: 0x000EC127 File Offset: 0x000EA327
		private protected bool IsEohSeen { protected get; private set; }

		// Token: 0x06003988 RID: 14728
		protected abstract ParseResult PreProcess();

		// Token: 0x06003989 RID: 14729
		protected abstract bool TryGetInitialResponse(out SmtpResponse initialResponse);

		// Token: 0x0600398A RID: 14730
		protected abstract bool ValidateAccumulatedSize(out SmtpResponse failureResponse);

		// Token: 0x0600398B RID: 14731
		protected abstract bool ShouldProcessEoh();

		// Token: 0x0600398C RID: 14732
		protected abstract bool ShouldProcessEod();

		// Token: 0x0600398D RID: 14733
		protected abstract void PostEoh();

		// Token: 0x0600398E RID: 14734
		protected abstract void PostEod();

		// Token: 0x0600398F RID: 14735
		protected abstract bool ShouldHandleBareLineFeedInBody();

		// Token: 0x06003990 RID: 14736
		protected abstract ParseAndProcessResult<SmtpInStateMachineEvents> GetFinalResult(ParseAndProcessResult<SmtpInStateMachineEvents> eodResult);

		// Token: 0x06003991 RID: 14737
		protected abstract ParseAndProcessResult<SmtpInStateMachineEvents> GetFailureResult(ParsingStatus parsingStatus, SmtpResponse failureResponse, out bool shouldAbortTransaction);

		// Token: 0x06003992 RID: 14738
		protected abstract SmtpInStateMachineEvents GetSuccessEvent();

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000EC130 File Offset: 0x000EA330
		protected override LatencyComponent LatencyComponent
		{
			get
			{
				return LatencyComponent.SmtpReceiveOnDataCommand;
			}
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000EC9FC File Offset: 0x000EABFC
		protected override async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> ProcessAsync(CancellationToken cancellationToken)
		{
			this.sessionState.Tracer.TraceDebug((long)this.sessionState.GetHashCode(), "DataBdatCommandBase.ProcessAsync");
			SmtpResponse finalResponse = SmtpResponse.Empty;
			ParseResult preProcessResult = this.PreProcess();
			if (preProcessResult.IsFailed)
			{
				this.sessionState.StartDiscardingMessage();
				finalResponse = preProcessResult.SmtpResponse;
				if (preProcessResult.DisconnectClient)
				{
					return new ParseAndProcessResult<SmtpInStateMachineEvents>(preProcessResult.ParsingStatus, preProcessResult.SmtpResponse, SmtpInStateMachineEvents.SendResponseAndDisconnectClient, preProcessResult.DisconnectClient);
				}
			}
			SmtpResponse initialResponse;
			if (this.TryGetInitialResponse(out initialResponse))
			{
				this.sessionState.ProtocolLogSession.LogSend(initialResponse.ToByteArray());
				object error = await base.WriteToClientAsync(initialResponse);
				base.OnAwaitCompleted(cancellationToken);
				if (error != null)
				{
					this.sessionState.HandleNetworkError(error);
					return DataBdatCommandBase.NetworkError;
				}
			}
			CommandContext commandContext = null;
			MessageHandlerResult handlerResult = MessageHandlerResult.MoreDataRequired;
			ParseAndProcessResult<SmtpInStateMachineEvents> agentResult = SmtpInCommandBase.SmtpResponseEmptyResult;
			SmtpResponse failureResponse;
			while (handlerResult == MessageHandlerResult.MoreDataRequired)
			{
				NetworkConnection.LazyAsyncResultWithTimeout readResult = await Util.ReadAsync(this.sessionState);
				base.OnAwaitCompleted(cancellationToken);
				if (Util.IsReadFailure(readResult))
				{
					this.sessionState.HandleNetworkError(readResult.Result);
					return DataBdatCommandBase.NetworkError;
				}
				commandContext = CommandContext.FromAsyncResult(readResult);
				handlerResult = this.messageHandler.Process(commandContext, out failureResponse);
				if (handlerResult == MessageHandlerResult.Failure)
				{
					this.sessionState.StartDiscardingMessage();
					finalResponse = failureResponse;
				}
				if (!this.sessionState.DiscardingMessage)
				{
					if (!this.ValidateAccumulatedSize(out failureResponse))
					{
						this.sessionState.StartDiscardingMessage();
						finalResponse = failureResponse;
					}
					else
					{
						ParseAndProcessResult<SmtpInStateMachineEvents> eohResult = await this.HandleEohAsync(commandContext, cancellationToken);
						base.OnAwaitCompleted(cancellationToken);
						if (agentResult.SmtpResponse.IsEmpty && !eohResult.SmtpResponse.IsEmpty)
						{
							agentResult = eohResult;
							this.sessionState.StartDiscardingMessage();
						}
					}
				}
			}
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (!finalResponse.IsEmpty || !this.EohResponse.IsEmpty)
			{
				finalResponse = (finalResponse.IsEmpty ? this.EohResponse : finalResponse);
				result = await this.HandleCommandFailureAsync(commandContext, ParsingStatus.Complete, finalResponse, cancellationToken);
			}
			else if (!agentResult.SmtpResponse.IsEmpty)
			{
				this.HandleMessageRejection(agentResult.SmtpResponse);
				result = agentResult;
			}
			else if (!this.FlushContents(out failureResponse))
			{
				this.HandleMessageRejection(failureResponse);
				result = await this.HandleCommandFailureAsync(commandContext, ParsingStatus.Complete, failureResponse, cancellationToken);
			}
			else
			{
				agentResult = await this.HandleEohAsync(commandContext, cancellationToken);
				base.OnAwaitCompleted(cancellationToken);
				if (!agentResult.SmtpResponse.IsEmpty)
				{
					this.HandleMessageRejection(agentResult.SmtpResponse);
					result = agentResult;
				}
				else
				{
					ParseAndProcessResult<SmtpInStateMachineEvents> eodResult = await this.HandleEodAsync(commandContext, cancellationToken);
					base.OnAwaitCompleted(cancellationToken);
					if (DataBdatCommandBase.IsFailureResponseType(eodResult.SmtpResponse))
					{
						this.HandleMessageRejection(eodResult.SmtpResponse);
						result = eodResult;
					}
					else
					{
						ParseAndProcessResult<SmtpInStateMachineEvents> finalResult = this.GetFinalResult(eodResult);
						if (finalResult.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
						{
							this.HandleMessageRejection(finalResult.SmtpResponse);
							result = await this.HandleCommandFailureAsync(commandContext, finalResult.ParsingStatus, finalResult.SmtpResponse, cancellationToken);
						}
						else
						{
							this.PostEod();
							result = finalResult;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000ECA4C File Offset: 0x000EAC4C
		protected void HandleEoh(MimePart part, out bool stopLoading)
		{
			stopLoading = false;
			if (!this.ShouldProcessEoh())
			{
				return;
			}
			this.sessionState.TransportMailItem.MimeDocument.EndOfHeaders = null;
			if (this.IsEohSeen)
			{
				this.sessionState.Tracer.TraceError((long)this.sessionState.GetHashCode(), "HandleEoh callback got called more than once");
				return;
			}
			this.IsEohSeen = true;
			SmtpResponse eohResponse;
			if (!this.ShouldContinueEndOfHeaders(part, out eohResponse))
			{
				if (!eohResponse.IsEmpty)
				{
					this.EohResponse = eohResponse;
				}
				return;
			}
			eohResponse = this.PerformHeaderValidation(part);
			if (!eohResponse.IsEmpty)
			{
				this.EohResponse = eohResponse;
				return;
			}
			DataBdatHelpers.UpdateDagSelectorPerformanceCounters(part.Headers, this.sessionState.Configuration.RoutingConfiguration.CheckDagSelectorHeader, this.sessionState.ReceivePerfCounters);
			this.sessionState.TransportMailItem.Oorg = DataBdatHelpers.GetOorg(this.sessionState.TransportMailItem, this.sessionState.Capabilities, this.sessionState.ProtocolLogSession, this.sessionState.TransportMailItem.RootPart.Headers);
			this.eohEventArgs = new EndOfHeadersEventArgs(this.sessionState)
			{
				MailItem = this.sessionState.TransportMailItemWrapper,
				Headers = this.sessionState.TransportMailItem.RootPart.Headers
			};
			this.PostEoh();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000ECB9C File Offset: 0x000EAD9C
		protected bool SetupMessageStream(bool allowBinaryContent)
		{
			Stream bodyStream;
			if (this.sessionState.SetupMessageStream(allowBinaryContent, out bodyStream))
			{
				this.streamBuilder.BodyStream = bodyStream;
				return true;
			}
			this.streamBuilder.IsDiscardingData = true;
			return false;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000ED1E4 File Offset: 0x000EB3E4
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> HandleEodAsync(CommandContext commandContext, CancellationToken cancellationToken)
		{
			ParseAndProcessResult<SmtpInStateMachineEvents> result2;
			if (!this.ShouldProcessEod())
			{
				result2 = SmtpInCommandBase.SmtpResponseEmptyResult;
			}
			else
			{
				if (this.sessionState.TransportMailItem != null)
				{
					this.sessionState.TransportMailItem.MimeSize += this.streamBuilder.TotalBytesWritten;
				}
				SmtpResponse result;
				if (!this.CloseMessageStream(out result))
				{
					result2 = await this.RaiseOnRejectEventAsync(commandContext, ParsingStatus.Complete, result, null, cancellationToken);
				}
				else
				{
					this.sessionState.TransportMailItem.ExposeMessage = true;
					if (!this.sessionState.DiscardingMessage && this.sessionState.TransportMailItem != null && this.sessionState.TransportMailItem.RootPart != null && !DataBdatHelpers.SendOnBehalfOfChecksPass(this.sessionState, out result))
					{
						result2 = await this.RaiseOnRejectEventAsync(commandContext, ParsingStatus.Complete, result, null, cancellationToken);
					}
					else if ((this.ShouldHandleBareLineFeedInBody() && !DataBdatCommandBase.HandleBareLineFeedInBody(this.sessionState, out result)) || !this.NormalizeMime(out result))
					{
						result2 = await this.RaiseOnRejectEventAsync(commandContext, ParsingStatus.Complete, result, null, cancellationToken);
					}
					else
					{
						this.sessionState.TransportMailItem.UpdateCachedHeaders();
						if (!this.sessionState.IsValidMessagePriority(out result))
						{
							result2 = await this.RaiseOnRejectEventAsync(commandContext, ParsingStatus.Complete, result, null, cancellationToken);
						}
						else
						{
							this.eodEventArgs = new EndOfDataEventArgs(this.sessionState)
							{
								MailItem = this.sessionState.TransportMailItemWrapper
							};
							ParseAndProcessResult<SmtpInStateMachineEvents> eodResult = await this.RaiseEodEventAsync(commandContext, cancellationToken);
							if (eodResult.SmtpResponse.SmtpResponseType != SmtpResponseType.Success && !eodResult.SmtpResponse.IsEmpty)
							{
								result2 = eodResult;
							}
							else if (!this.ModifyTransportMailItemProperties(out result))
							{
								result2 = await this.RaiseOnRejectEventAsync(commandContext, ParsingStatus.Complete, result, this.eohEventArgs, cancellationToken);
							}
							else
							{
								result2 = await this.CommitMessageAsync(cancellationToken);
							}
						}
					}
				}
			}
			return result2;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000ED23C File Offset: 0x000EB43C
		private bool CheckPoisonMessage(HeaderList headers)
		{
			Header header = headers.FindFirst("Message-ID");
			if (header == null)
			{
				return false;
			}
			string value = header.Value;
			if (!string.IsNullOrEmpty(value) && this.sessionState.IsMessagePoison(value))
			{
				this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Rejecting Message-ID: {0} because it was detected as poison", new object[]
				{
					value
				});
				return true;
			}
			return false;
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000ED2A0 File Offset: 0x000EB4A0
		private bool FlushContents(out SmtpResponse failureResponse)
		{
			if (!this.ShouldProcessEod())
			{
				failureResponse = SmtpResponse.Empty;
				return true;
			}
			bool result;
			try
			{
				if (this.streamBuilder != null)
				{
					this.streamBuilder.BodyStream.Flush();
				}
				failureResponse = SmtpResponse.Empty;
				result = true;
			}
			catch (IOException ex)
			{
				this.sessionState.Tracer.TraceDebug<IOException>((long)this.sessionState.GetHashCode(), "Handled IO exception: {0}", ex);
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				failureResponse = DataBdatHelpers.HandleFilterableException(ex, this.sessionState);
				result = false;
			}
			catch (ExchangeDataException ex2)
			{
				this.sessionState.Tracer.TraceDebug<ExchangeDataException>((long)this.sessionState.GetHashCode(), "Handled parser exception: {0}", ex2);
				failureResponse = DataBdatHelpers.HandleFilterableException(ex2, this.sessionState);
				result = false;
			}
			return result;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000ED384 File Offset: 0x000EB584
		private bool ShouldContinueEndOfHeaders(MimePart part, out SmtpResponse failureResponse)
		{
			if (this.sessionState.DiscardingMessage)
			{
				failureResponse = SmtpResponse.Empty;
				return false;
			}
			if (part == null)
			{
				failureResponse = SmtpResponse.DataTransactionFailed;
				this.sessionState.Tracer.TraceError((long)this.sessionState.GetHashCode(), "MimePart is null");
				this.sessionState.StartDiscardingMessage();
				return false;
			}
			if (this.CheckPoisonMessage(part.Headers))
			{
				failureResponse = SmtpResponse.TooManyRelatedErrors;
				this.sessionState.StartDiscardingMessage();
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000ED41C File Offset: 0x000EB61C
		private SmtpResponse PerformSizeValidations(MimePart part)
		{
			if (SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(this.sessionState.CombinedPermissions))
			{
				return SmtpResponse.Empty;
			}
			long num;
			if (SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(this.sessionState.CombinedPermissions) && DataBdatHelpers.TryGetOriginalSize(part.Headers, out num))
			{
				this.originalMessageSize = num;
			}
			SmtpResponse result;
			if (!this.TryUpdateSizeLimits(out result))
			{
				return result;
			}
			if (DataBdatHelpers.ShouldOnlyCheckMessageSizeAfterEoh(this.sessionState) && DataBdatHelpers.MessageSizeExceeded(this.AccumulatedMessageSize, this.originalMessageSize, this.messageSizeLimit, this.sessionState.CombinedPermissions))
			{
				this.sessionState.StartDiscardingMessage();
				if (this.sessionState.ReceivePerfCounters != null)
				{
					this.sessionState.ReceivePerfCounters.MessagesRefusedForSize.Increment();
				}
				return SmtpResponse.MessageTooLarge;
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000ED4E0 File Offset: 0x000EB6E0
		private bool TryUpdateSizeLimits(out SmtpResponse failureResponse)
		{
			if (!this.ShouldUpdateSizeLimits())
			{
				failureResponse = SmtpResponse.Empty;
				return true;
			}
			try
			{
				long num;
				if (DataBdatHelpers.GetSenderSizeLimit(this.sessionState.TransportMailItem, this.sessionState.Configuration.TransportConfiguration.MaxSendSize, out num))
				{
					this.messageSizeLimit = num;
				}
			}
			catch (ADTransientException)
			{
				this.sessionState.Tracer.TraceDebug((long)this.sessionState.GetHashCode(), "SMTP rejected a mail due to a transient AD error");
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToADDown);
				this.sessionState.StartDiscardingMessage();
				failureResponse = SmtpResponse.DataTransactionFailed;
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000ED59C File Offset: 0x000EB79C
		private bool ShouldUpdateSizeLimits()
		{
			return Util.IsHubOrFrontEndRole(this.sessionState.Configuration.TransportConfiguration.ProcessTransportRole) && !SmtpInSessionUtils.IsAnonymous(this.sessionState.RemoteIdentity);
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000ED5D0 File Offset: 0x000EB7D0
		private SmtpResponse PerformP1AndP2Checks()
		{
			if (this.CanP1AndP2ChecksBeBypassed(this.sessionState.CombinedPermissions))
			{
				return SmtpResponse.Empty;
			}
			SmtpResponse result = DataBdatHelpers.ValidateMailFromAddress(this.sessionState);
			if (!result.IsEmpty)
			{
				return result;
			}
			result = DataBdatHelpers.ValidateFromAddressInHeader(this.sessionState);
			if (!result.IsEmpty)
			{
				return result;
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x0600399F RID: 14751 RVA: 0x000ED629 File Offset: 0x000EB829
		private bool CanP1AndP2ChecksBeBypassed(Permission permissions)
		{
			return SmtpInSessionUtils.HasSMTPAcceptAnySenderPermission(permissions) && SmtpInSessionUtils.HasSMTPAcceptAuthoritativeDomainSenderPermission(permissions);
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000ED63C File Offset: 0x000EB83C
		private SmtpResponse UpdateTransportMailItem(SmtpInSessionState state, string internetMessageId, int localLoopDeferralSeconds)
		{
			state.TransportMailItem.ExposeMessageHeaders = true;
			state.TransportMailItem.InternetMessageId = internetMessageId;
			state.TransportMailItem.SetMimeDefaultEncoding();
			SmtpResponse result = DataBdatHelpers.CheckAttributionAndCreateAdRecipientCache(state.TransportMailItem, state.Configuration.TransportConfiguration.RejectUnscopedMessages, false, false);
			if (!result.IsEmpty)
			{
				state.StartDiscardingMessage();
				return result;
			}
			if (localLoopDeferralSeconds > 0)
			{
				state.TransportMailItem.DeferUntil = DateTime.UtcNow.AddSeconds((double)localLoopDeferralSeconds);
				state.TransportMailItem.DeferReason = DeferReason.LoopDetected;
				LatencyTracker.BeginTrackLatency(LatencyComponent.Deferral, state.TransportMailItem.LatencyTracker);
				MessageTrackingLog.TrackDefer(MessageTrackingSource.SMTP, state.TransportMailItem, null);
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000ED6EC File Offset: 0x000EB8EC
		private void FilterAndPatchHeaders(MimePart part, out string messageId)
		{
			RestrictedHeaderSet blocked = SmtpInSessionUtils.RestrictedHeaderSetFromPermissions(this.sessionState.CombinedPermissions);
			HeaderFirewall.Filter(this.sessionState.TransportMailItem.RootPart.Headers, blocked);
			DataBdatHelpers.PatchHeaders(part.Headers, this.sessionState, out messageId);
			this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "receiving message with InternetMessageId {0}", new object[]
			{
				messageId
			});
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000ED75C File Offset: 0x000EB95C
		private SmtpResponse PerformHeaderValidation(MimePart part)
		{
			int localLoopDeferralSeconds = 0;
			SmtpResponse result = DataBdatHelpers.PerformHeaderSizeAndPartialMessageChecks(part.Headers, this.sessionState, this.streamBuilder.EohPos);
			if (result.IsEmpty)
			{
				result = DataBdatHelpers.CheckMaxHopCounts(part.Headers, this.sessionState, out localLoopDeferralSeconds);
			}
			if (!result.IsEmpty)
			{
				this.sessionState.StartDiscardingMessage();
				return result;
			}
			string internetMessageId;
			this.FilterAndPatchHeaders(part, out internetMessageId);
			result = this.UpdateTransportMailItem(this.sessionState, internetMessageId, localLoopDeferralSeconds);
			if (!result.IsEmpty)
			{
				this.sessionState.StartDiscardingMessage();
				return result;
			}
			result = DataBdatHelpers.CheckMessageSubmitPermissions(this.sessionState);
			if (!result.IsEmpty)
			{
				this.sessionState.StartDiscardingMessage();
				return result;
			}
			result = this.PerformP1AndP2Checks();
			if (!result.IsEmpty)
			{
				this.sessionState.StartDiscardingMessage();
				return result;
			}
			result = this.PerformSizeValidations(part);
			if (!result.IsEmpty)
			{
				this.sessionState.StartDiscardingMessage();
				return result;
			}
			if (Util.IsHubOrFrontEndRole(this.sessionState.Configuration.TransportConfiguration.ProcessTransportRole) && !SubmissionQuotaChecker.CheckSubmissionQuota(this.sessionState))
			{
				this.sessionState.StartDiscardingMessage();
				return SmtpResponse.SubmissionQuotaExceeded;
			}
			return SmtpResponse.Empty;
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000EDA50 File Offset: 0x000EBC50
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> HandleEohAsync(CommandContext commandContext, CancellationToken cancellationToken)
		{
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (this.eohEventArgs == null || !this.EohResponse.IsEmpty)
			{
				result = SmtpInCommandBase.SmtpResponseEmptyResult;
			}
			else
			{
				this.sessionState.SmtpAgentSession.LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveOnEndOfHeaders, this.sessionState.TransportMailItem.LatencyTracker);
				ParseAndProcessResult<SmtpInStateMachineEvents> endOfHeaderResult = await base.RaiseAgentEventAsync("OnEndOfHeaders", this.eohEventArgs, commandContext, ParseResult.ParsingComplete, cancellationToken, ReceiveMessageEventSourceImpl.Create(this.sessionState, this.eohEventArgs.MailItem));
				base.OnAwaitCompleted(cancellationToken);
				this.sessionState.SmtpAgentSession.LatencyTracker.EndTrackLatency();
				this.eohEventArgs = null;
				result = endOfHeaderResult;
			}
			return result;
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000EDAA8 File Offset: 0x000EBCA8
		private bool CloseMessageStream(out SmtpResponse failureResponse)
		{
			try
			{
				if (this.streamBuilder.BodyStream != null)
				{
					this.streamBuilder.BodyStream.Close();
					this.streamBuilder.BodyStream = null;
				}
			}
			catch (IOException exception)
			{
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.streamBuilder.BodyStream = null;
				failureResponse = DataBdatHelpers.HandleFilterableException(exception, this.sessionState);
				return false;
			}
			catch (ExchangeDataException exception2)
			{
				this.streamBuilder.BodyStream = null;
				failureResponse = DataBdatHelpers.HandleFilterableException(exception2, this.sessionState);
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000EDB5C File Offset: 0x000EBD5C
		private static bool HandleBareLineFeedInBody(SmtpInSessionState sessionState, out SmtpResponse failureResponse)
		{
			if ((sessionState.TransportMailItem.MimeDocument.ComplianceStatus & MimeComplianceStatus.BareLinefeedInBody) != MimeComplianceStatus.Compliant)
			{
				if (sessionState.ReceivePerfCounters != null)
				{
					sessionState.ReceivePerfCounters.MessagesReceivedWithBareLinefeeds.Increment();
				}
				if (!sessionState.ReceiveConnector.BareLinefeedRejectionEnabled)
				{
					failureResponse = SmtpResponse.Empty;
					return true;
				}
				using (BareLinefeedDetector bareLinefeedDetector = new BareLinefeedDetector())
				{
					try
					{
						sessionState.TransportMailItem.MimeDocument.WriteTo(bareLinefeedDetector);
					}
					catch (BareLinefeedException)
					{
						if (sessionState.ReceivePerfCounters != null)
						{
							sessionState.ReceivePerfCounters.MessagesRefusedForBareLinefeeds.Increment();
						}
						failureResponse = SmtpResponse.InvalidContentBareLinefeeds;
						return false;
					}
				}
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000EDC2C File Offset: 0x000EBE2C
		private bool NormalizeMime(out SmtpResponse failureResponse)
		{
			try
			{
				this.sessionState.TransportMailItem.Message.Normalize(NormalizeOptions.NormalizeMessageId | NormalizeOptions.MergeAddressHeaders | NormalizeOptions.RemoveDuplicateHeaders, false);
			}
			catch (IOException exception)
			{
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				failureResponse = DataBdatHelpers.HandleFilterableException(exception, this.sessionState);
				return false;
			}
			catch (ExchangeDataException exception2)
			{
				failureResponse = DataBdatHelpers.HandleFilterableException(exception2, this.sessionState);
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000EDDE4 File Offset: 0x000EBFE4
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> RaiseOnRejectEventAsync(CommandContext commandContext, ParsingStatus parsingStatus, SmtpResponse smtpResponse, ReceiveEventArgs eventArgs, CancellationToken cancellationToken)
		{
			this.sessionState.AbortMailTransaction();
			SmtpResponse agentResponse = await base.RaiseRejectEventAsync(commandContext, parsingStatus, smtpResponse, eventArgs, cancellationToken);
			return new ParseAndProcessResult<SmtpInStateMachineEvents>(parsingStatus, agentResponse, this.GetCommandFailureEvent(), false);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000EDFE4 File Offset: 0x000EC1E4
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> RaiseEodEventAsync(CommandContext commandContext, CancellationToken cancellationToken)
		{
			this.sessionState.SmtpAgentSession.LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveOnEndOfData, this.sessionState.TransportMailItem.LatencyTracker);
			ParseAndProcessResult<SmtpInStateMachineEvents> endOfDataResult = await base.RaiseAgentEventAsync("OnEndOfData", this.eodEventArgs, commandContext, ParseResult.ParsingComplete, cancellationToken, ReceiveMessageEventSourceImpl.Create(this.sessionState, this.eodEventArgs.MailItem));
			base.OnAwaitCompleted(cancellationToken);
			this.sessionState.SmtpAgentSession.LatencyTracker.EndTrackLatency();
			return endOfDataResult;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000EE03C File Offset: 0x000EC23C
		private void StampOriginalMessageSize()
		{
			HeaderList headers = this.sessionState.TransportMailItem.RootPart.Headers;
			if (headers.FindFirst("X-MS-Exchange-Organization-OriginalSize") == null)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", this.sessionState.TransportMailItem.MimeSize.ToString(NumberFormatInfo.InvariantInfo)));
			}
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000EE09C File Offset: 0x000EC29C
		private bool ModifyTransportMailItemProperties(out SmtpResponse failureResponse)
		{
			try
			{
				if (this.sessionState.TransportMailItem.Message.TnefPart != null)
				{
					Util.ConvertMessageClassificationsFromTnefToHeaders(this.sessionState.TransportMailItem);
				}
				ClassificationUtils.DropStoreLabels(this.sessionState.TransportMailItem.RootPart.Headers);
				this.StampOriginalMessageSize();
			}
			catch (IOException exception)
			{
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				failureResponse = DataBdatHelpers.HandleFilterableException(exception, this.sessionState);
				return false;
			}
			catch (ExchangeDataException exception2)
			{
				failureResponse = DataBdatHelpers.HandleFilterableException(exception2, this.sessionState);
				return false;
			}
			failureResponse = SmtpResponse.Empty;
			return true;
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000EE4F8 File Offset: 0x000EC6F8
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> CommitMessageAsync(CancellationToken cancellationToken)
		{
			SmtpResponse failureResponse;
			bool shouldRejectMailItem = CommandParsingHelper.ShouldRejectMailItem(this.sessionState.TransportMailItem.From, this.sessionState, true, out failureResponse);
			if (failureResponse.Equals(SmtpResponse.InsufficientResource))
			{
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToBackPressure);
			}
			ParseAndProcessResult<SmtpInStateMachineEvents> result2;
			if (shouldRejectMailItem)
			{
				result2 = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, failureResponse, this.GetCommandFailureEvent(), false);
			}
			else
			{
				this.sessionState.CloseMessageWriteStream();
				if (!string.IsNullOrEmpty(this.sessionState.PeerSessionPrimaryServer) && this.sessionState.TransportMailItem.Priority == DeliveryPriority.Normal)
				{
					this.sessionState.TransportMailItem.PrioritizationReason = "ShadowRedundancy";
					this.sessionState.TransportMailItem.Priority = DeliveryPriority.None;
				}
				LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceiveDataInternal, this.sessionState.TransportMailItem.LatencyTracker);
				LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveCommit, this.sessionState.TransportMailItem.LatencyTracker);
				CommitCoordinator commitCoordinator = new CommitCoordinator(this.sessionState.ServerState.MailItemStorage, this.shadowSession, this.sessionState.Configuration.TransportConfiguration.ProcessTransportRole);
				try
				{
					SmtpResponse commitResponse = await commitCoordinator.CommitMailItemAsync(this.sessionState.TransportMailItem);
					base.OnAwaitCompleted(cancellationToken);
					if (!commitResponse.IsEmpty)
					{
						this.sessionState.AbortMailTransaction();
						return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, commitResponse, this.GetCommandFailureEvent(), false);
					}
				}
				catch (Exception commitException)
				{
					this.sessionState.AbortMailTransaction();
					return this.HandleCommitException(commitException);
				}
				LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceiveCommit, this.sessionState.TransportMailItem.LatencyTracker);
				SmtpResponse result = this.sessionState.TrackAndEnqueueMailItem();
				if (this.sessionState.Configuration.TransportConfiguration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery && DataBdatCommandBase.IsFailureResponseType(result))
				{
					this.sessionState.AbortMailTransaction();
					result2 = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, result, this.GetCommandFailureEvent(), false);
				}
				else
				{
					result2 = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, result, this.GetSuccessEvent(), false);
				}
			}
			return result2;
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000EE548 File Offset: 0x000EC748
		private ParseAndProcessResult<SmtpInStateMachineEvents> HandleCommitException(Exception commitException)
		{
			if (commitException is ExchangeDataException)
			{
				this.sessionState.Tracer.TraceError<string>((long)this.sessionState.GetHashCode(), "GetCommitResults: MIME exception on commit: {0}", commitException.Message);
				this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "MIME exception on commit: {0}", new object[]
				{
					commitException.Message
				});
				return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.InvalidContent, this.GetCommandFailureEvent(), false);
			}
			if (commitException is IOException && ExceptionHelper.IsHandleableTransientCtsException(commitException))
			{
				this.sessionState.Tracer.TraceError<string>((long)this.sessionState.GetHashCode(), "GetCommitResults: IO exception on commit: {0}", commitException.Message);
				this.sessionState.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				byte[] data = Util.AsciiStringToBytes(string.Format(CultureInfo.InvariantCulture, "IO Exception on commit: {0}", new object[]
				{
					commitException.Message
				}));
				this.sessionState.ProtocolLogSession.LogInformation(ProtocolLoggingLevel.Verbose, data, null);
				return new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.CTSParseError, this.GetCommandFailureEvent(), false);
			}
			throw new LocalizedException(Strings.CommitMailFailed, commitException);
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000EE65C File Offset: 0x000EC85C
		private void HandleMessageRejection(SmtpResponse failureResponse)
		{
			bool flag;
			this.GetFailureResult(ParsingStatus.Complete, failureResponse, out flag);
			if (flag)
			{
				this.sessionState.AbortMailTransaction();
			}
			this.sessionState.EventLog.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveMessageRejected, null, new object[]
			{
				this.sessionState.ReceiveConnector.Name,
				failureResponse
			});
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000EE7F0 File Offset: 0x000EC9F0
		private async Task<ParseAndProcessResult<SmtpInStateMachineEvents>> HandleCommandFailureAsync(CommandContext commandContext, ParsingStatus parsingStatus, SmtpResponse failureResponse, CancellationToken cancellationToken)
		{
			bool shouldAbortTransaction;
			ParseAndProcessResult<SmtpInStateMachineEvents> failureResult = this.GetFailureResult(parsingStatus, failureResponse, out shouldAbortTransaction);
			ParseAndProcessResult<SmtpInStateMachineEvents> result;
			if (shouldAbortTransaction)
			{
				result = await this.RaiseOnRejectEventAsync(commandContext, failureResult.ParsingStatus, failureResult.SmtpResponse, null, cancellationToken);
			}
			else
			{
				result = failureResult;
			}
			return result;
		}

		// Token: 0x060039AF RID: 14767 RVA: 0x000EE857 File Offset: 0x000ECA57
		private static bool IsFailureResponseType(SmtpResponse smtpResponseToCheck)
		{
			return smtpResponseToCheck.SmtpResponseType == SmtpResponseType.PermanentError || smtpResponseToCheck.SmtpResponseType == SmtpResponseType.TransientError;
		}

		// Token: 0x04001D3B RID: 7483
		public static readonly ParseAndProcessResult<SmtpInStateMachineEvents> NetworkError = new ParseAndProcessResult<SmtpInStateMachineEvents>(ParsingStatus.Complete, SmtpResponse.Empty, SmtpInStateMachineEvents.NetworkError, false);

		// Token: 0x04001D3C RID: 7484
		protected ISmtpInStreamBuilder streamBuilder;

		// Token: 0x04001D3D RID: 7485
		protected IMessageHandler messageHandler;

		// Token: 0x04001D3E RID: 7486
		protected long messageSizeLimit;

		// Token: 0x04001D3F RID: 7487
		protected long originalMessageSize = long.MaxValue;

		// Token: 0x04001D40 RID: 7488
		private EndOfHeadersEventArgs eohEventArgs;

		// Token: 0x04001D41 RID: 7489
		private EndOfDataEventArgs eodEventArgs;

		// Token: 0x04001D42 RID: 7490
		private readonly IShadowSession shadowSession = new NullShadowSession();

		// Token: 0x04001D43 RID: 7491
		private SmtpResponse EohResponse = SmtpResponse.Empty;
	}
}
