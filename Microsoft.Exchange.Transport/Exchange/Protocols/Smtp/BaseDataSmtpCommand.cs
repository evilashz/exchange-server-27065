using System;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000452 RID: 1106
	internal abstract class BaseDataSmtpCommand : SmtpCommand
	{
		// Token: 0x06003328 RID: 13096 RVA: 0x000CAA1C File Offset: 0x000C8C1C
		public BaseDataSmtpCommand(ISmtpSession session, string protocolCommandKeyword, string commandEventString, LatencyComponent commandEventComponent, ITransportAppConfig transportAppConfig) : base(session, protocolCommandKeyword, commandEventString, commandEventComponent)
		{
			DataCommandEventArgs dataCommandEventArgs = new DataCommandEventArgs();
			this.CommandEventArgs = dataCommandEventArgs;
			this.transportAppConfig = transportAppConfig;
			this.smtpCustomDataResponse = this.transportAppConfig.SmtpDataConfiguration.SmtpDataResponse;
			ISmtpInSession smtpInSession = session as ISmtpInSession;
			if (smtpInSession != null)
			{
				dataCommandEventArgs.MailItem = smtpInSession.TransportMailItemWrapper;
				this.messageSizeLimit = (long)smtpInSession.Connector.MaxMessageSize.ToBytes();
			}
			this.agentLoopChecker = new AgentGeneratedMessageLoopChecker(new AgentGeneratedMessageLoopCheckerTransportConfig(Components.Configuration));
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000CAAB2 File Offset: 0x000C8CB2
		internal BaseDataSmtpCommand(string protocolCommandKeyword) : base(protocolCommandKeyword)
		{
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x000CAACA File Offset: 0x000C8CCA
		public virtual bool IsBlob
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000CAACD File Offset: 0x000C8CCD
		internal Stream BodyStream
		{
			get
			{
				return this.bodyStream;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000CAAD5 File Offset: 0x000C8CD5
		protected internal bool DiscardingMessage
		{
			get
			{
				return this.discardingMessage;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x0600332D RID: 13101
		protected abstract SmtpInParser SmtpInParser { get; }

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x0600332E RID: 13102
		protected abstract long AccumulatedMessageSize { get; }

		// Token: 0x0600332F RID: 13103 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			this.InboundParseCommandInternal();
			if (base.ParsingStatus != ParsingStatus.Error && base.ParsingStatus != ParsingStatus.ProtocolError)
			{
				if (smtpInSession.ShadowSession != null)
				{
					if (this.IsBlob)
					{
						this.shadowSession = new NullShadowSession();
					}
					else
					{
						this.shadowSession = smtpInSession.ShadowSession;
					}
				}
				else
				{
					if (smtpInSession.ShadowRedundancyManagerObject != null && !this.DiscardingMessage && !this.IsBlob)
					{
						this.shadowSession = smtpInSession.ShadowRedundancyManagerObject.GetShadowSession(smtpInSession, this is BdatSmtpCommand);
					}
					else
					{
						this.shadowSession = new NullShadowSession();
					}
					this.shadowSession.BeginOpen(smtpInSession.TransportMailItem, BaseDataSmtpCommand.shadowOpenCallback, this);
					if (!this.IsBlob)
					{
						smtpInSession.ShadowSession = this.shadowSession;
					}
				}
				this.shadowSession.PrepareForNewCommand(this);
			}
		}

		// Token: 0x06003330 RID: 13104
		protected abstract AsyncReturnType SubmitMessageIfReady();

		// Token: 0x06003331 RID: 13105
		protected abstract void ContinueSubmitMessageIfReady();

		// Token: 0x06003332 RID: 13106 RVA: 0x000CABBC File Offset: 0x000C8DBC
		protected virtual bool SetSuccessResponse()
		{
			if (!this.discardingMessage)
			{
				ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
				if (smtpInSession.IsPeerShadowSession)
				{
					base.SmtpResponse = SmtpResponse.QueuedMailForRedundancy(SmtpCommand.GetBracketedString(this.messageId));
				}
				else
				{
					base.SmtpResponse = SmtpResponse.QueuedMailForDelivery(SmtpCommand.GetBracketedString(this.messageId));
				}
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.SuccessfulSubmission);
			}
			return !this.discardingMessage;
		}

		// Token: 0x06003333 RID: 13107
		protected abstract void StoreCurrentDataState();

		// Token: 0x06003334 RID: 13108
		protected abstract void InboundParseCommandInternal();

		// Token: 0x06003335 RID: 13109 RVA: 0x000CAC24 File Offset: 0x000C8E24
		protected AsyncReturnType EodDone(bool isAsync)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "EodDone");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (isAsync)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.EodDoneAsync);
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.EodDoneSync);
			}
			if (base.SmtpResponse.SmtpResponseType != SmtpResponseType.Success)
			{
				SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveMessageRejected, null, new object[]
				{
					smtpInSession.Connector.Name,
					base.SmtpResponse
				});
			}
			base.ParsingStatus = ParsingStatus.Complete;
			if (this.shouldDisconnect && !smtpInSession.SessionSource.ShouldDisconnect)
			{
				smtpInSession.Disconnect(DisconnectReason.DroppedSession);
			}
			if (isAsync)
			{
				smtpInSession.RawDataReceivedCompleted();
			}
			if (!isAsync)
			{
				return AsyncReturnType.Sync;
			}
			return AsyncReturnType.Async;
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000CACE0 File Offset: 0x000C8EE0
		protected bool FoundEndOfMessage()
		{
			return this.seenEod && this.isLastChunk;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000CACF4 File Offset: 0x000C8EF4
		protected AsyncReturnType SubmitMessage()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.SubmitMessage);
			SmtpResponse failureSmtpResponse;
			if (smtpInSession.ShouldRejectMailItem(smtpInSession.TransportMailItem.From, true, out failureSmtpResponse))
			{
				this.HandleSubmitFailure(failureSmtpResponse);
				return AsyncReturnType.Sync;
			}
			smtpInSession.CloseMessageWriteStream();
			if (smtpInSession.IsPeerShadowSession && smtpInSession.TransportMailItem.Priority == DeliveryPriority.Normal)
			{
				smtpInSession.TransportMailItem.PrioritizationReason = "ShadowRedundancy";
				smtpInSession.TransportMailItem.Priority = DeliveryPriority.None;
			}
			LatencyComponent component = (smtpInSession.SessionSource.IsInboundProxiedSession || smtpInSession.SessionSource.IsClientProxiedSession) ? LatencyComponent.SmtpReceiveDataExternal : LatencyComponent.SmtpReceiveDataInternal;
			LatencyTracker.EndTrackLatency(component, smtpInSession.TransportMailItem.LatencyTracker);
			LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveCommit, smtpInSession.TransportMailItem.LatencyTracker);
			this.commitCoordinator = new CommitCoordinator(smtpInSession.SmtpInServer.MailItemStorage, this.shadowSession, Components.Configuration.ProcessTransportRole);
			this.commitCoordinator.BeginCommitMailItem(smtpInSession.TransportMailItem, BaseDataSmtpCommand.commitCallback, this);
			return AsyncReturnType.Async;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000CADF0 File Offset: 0x000C8FF0
		protected bool SetupMessageStream(bool expectBinaryContent)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			bool result = false;
			if (!this.isFirstChunk)
			{
				this.bodyStream = smtpInSession.MessageWriteStream;
				return true;
			}
			try
			{
				this.bodyStream = smtpInSession.OpenMessageWriteStream(expectBinaryContent);
				result = true;
			}
			catch (IOException arg)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<IOException>((long)this.GetHashCode(), "OpenMessageWriteStream failed: {0}", arg);
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.bodyStream = null;
				this.StartDiscardingMessage();
				base.SmtpResponse = SmtpResponse.DataTransactionFailed;
				smtpInSession.DeleteTransportMailItem();
			}
			return result;
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000CAE84 File Offset: 0x000C9084
		protected void StartDiscardingMessage()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DiscardingMessage);
			this.discardingMessage = true;
			if (this.SmtpInParser != null)
			{
				this.SmtpInParser.IsDiscardingData = true;
			}
			if (smtpInSession.MimeDocument != null)
			{
				smtpInSession.MimeDocument.EndOfHeaders = null;
			}
			if (this.shadowSession != null)
			{
				this.shadowSession.Close(AckStatus.Fail, SmtpResponse.Empty);
			}
			this.StoreCurrentDataState();
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000CAEF4 File Offset: 0x000C90F4
		protected void ParserEndOfHeadersCallback(MimePart part, out bool stopLoading)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ParserEndOfHeadersCallback);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "ParserEndOfHeadersCallback");
			if (this.IsBlob)
			{
				throw new InvalidOperationException("ParserEndOfHeadersCallback should not be called for BDAT blob.");
			}
			stopLoading = false;
			smtpInSession.MimeDocument.EndOfHeaders = null;
			if (this.seenEoh)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "ParserEndOfHeadersCallback got called again");
				return;
			}
			this.seenEoh = true;
			if (this.discardingMessage)
			{
				return;
			}
			if (part == null)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "MimePart is null");
				this.StartDiscardingMessage();
				return;
			}
			if (this.CheckPoisonMessage(part.Headers, smtpInSession))
			{
				this.StartDiscardingMessage();
				return;
			}
			int num;
			if (!DataBdatHelpers.CheckHeaders(part.Headers, smtpInSession, this.SmtpInParser.EohPos, this) || !DataBdatHelpers.CheckMaxHopCounts(part.Headers, smtpInSession, this, this.transportAppConfig.Routing.LocalLoopDetectionEnabled, this.transportAppConfig.Routing.LocalLoopSubdomainDepth, this.transportAppConfig.Routing.LocalLoopMessageDeferralIntervals, out num))
			{
				this.StartDiscardingMessage();
				return;
			}
			DataBdatHelpers.UpdateDagSelectorPerformanceCounters(part.Headers, this.transportAppConfig.Routing.CheckDagSelectorHeader, smtpInSession.SmtpReceivePerformanceCounters);
			RestrictedHeaderSet blocked = SmtpInSessionUtils.RestrictedHeaderSetFromPermissions(smtpInSession.Permissions);
			HeaderFirewall.Filter(smtpInSession.TransportMailItem.RootPart.Headers, blocked);
			DataBdatHelpers.PatchHeaders(part.Headers, smtpInSession, this.transportAppConfig.SmtpDataConfiguration.AcceptAndFixSmtpAddressWithInvalidLocalPart, out this.messageId);
			smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "receiving message with InternetMessageId {0}", new object[]
			{
				this.messageId
			});
			if (smtpInSession.TransportMailItem != null)
			{
				smtpInSession.TransportMailItem.ExposeMessageHeaders = true;
				smtpInSession.TransportMailItem.InternetMessageId = this.messageId;
				smtpInSession.SetupPoisonContext();
				smtpInSession.TransportMailItem.SetMimeDefaultEncoding();
				if (!this.CheckAttributionAndCreateADRecipientCache(smtpInSession))
				{
					this.StartDiscardingMessage();
					return;
				}
				if (num > 0)
				{
					smtpInSession.TransportMailItem.DeferUntil = DateTime.UtcNow.AddSeconds((double)num);
					smtpInSession.TransportMailItem.DeferReason = DeferReason.LoopDetected;
					LatencyTracker.BeginTrackLatency(LatencyComponent.Deferral, smtpInSession.TransportMailItem.LatencyTracker);
					MessageTrackingLog.TrackDefer(MessageTrackingSource.SMTP, smtpInSession.TransportMailItem, null);
				}
				if (!DataBdatHelpers.CheckMessageSubmitPermissions(smtpInSession, this))
				{
					this.StartDiscardingMessage();
					return;
				}
				if (!SmtpInSessionUtils.HasSMTPAcceptAnySenderPermission(smtpInSession.Permissions) || !SmtpInSessionUtils.HasSMTPAcceptAuthoritativeDomainSenderPermission(smtpInSession.Permissions))
				{
					if (!this.P1ChecksPass())
					{
						return;
					}
					if (!this.P2ChecksPass())
					{
						return;
					}
				}
				if (!SmtpInSessionUtils.HasSMTPBypassMessageSizeLimitPermission(smtpInSession.Permissions))
				{
					long num2;
					if (SmtpInSessionUtils.HasSMTPAcceptOrgHeadersPermission(smtpInSession.Permissions) && DataBdatHelpers.TryGetOriginalSize(part.Headers, out num2))
					{
						this.originalMessageSize = num2;
					}
					if (smtpInSession.SmtpInServer.IsBridgehead && !SmtpInSessionUtils.IsAnonymous(smtpInSession.RemoteIdentity))
					{
						try
						{
							long num3;
							if (BaseDataSmtpCommand.GetSenderSizeLimit(smtpInSession, out num3))
							{
								this.messageSizeLimit = num3;
							}
						}
						catch (ADTransientException)
						{
							ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)smtpInSession.GetHashCode(), "SMTP rejected a mail due to a transient AD error");
							smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToADDown);
							base.SmtpResponse = SmtpResponse.DataTransactionFailed;
							base.IsResponseReady = false;
							this.StartDiscardingMessage();
							return;
						}
					}
					if (!base.OnlyCheckMessageSizeAfterEoh || !DataBdatHelpers.MessageSizeExceeded(this.AccumulatedMessageSize, this.originalMessageSize, this.messageSizeLimit, smtpInSession.Permissions))
					{
						goto IL_360;
					}
					base.SmtpResponse = SmtpResponse.MessageTooLarge;
					base.IsResponseReady = false;
					this.StartDiscardingMessage();
					if (smtpInSession.SmtpReceivePerformanceCounters != null)
					{
						smtpInSession.SmtpReceivePerformanceCounters.MessagesRefusedForSize.Increment();
					}
					return;
				}
				IL_360:
				if (!this.CheckSubmissionQuota())
				{
					return;
				}
				this.SetOorg();
			}
			DataBdatHelpers.EnableEOHEvent(smtpInSession, smtpInSession.TransportMailItem.RootPart.Headers, out this.eohEventArgs);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000CB29C File Offset: 0x000C949C
		protected void EnableEODEvent()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (smtpInSession.TransportMailItem.IsActive)
			{
				this.eodEventArgs = new EndOfDataEventArgs(smtpInSession.SessionSource);
				this.eodEventArgs.MailItem = smtpInSession.TransportMailItemWrapper;
			}
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000CB2E4 File Offset: 0x000C94E4
		protected AsyncReturnType OnEod(bool isAsync)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (isAsync)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.OnEodAsync);
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.OnEodSync);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "OnEod");
			if (smtpInSession.TransportMailItem != null)
			{
				smtpInSession.TransportMailItem.MimeSize += this.SmtpInParser.TotalBytesWritten;
			}
			if (this.discardingMessage || !this.isLastChunk)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary1);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			try
			{
				if (this.bodyStream != null)
				{
					this.bodyStream.Close();
					this.bodyStream = null;
				}
			}
			catch (IOException e)
			{
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.bodyStream = null;
				this.DiscardOnException(e);
			}
			catch (ExchangeDataException e2)
			{
				this.bodyStream = null;
				this.DiscardOnException(e2);
			}
			smtpInSession.TransportMailItem.ExposeMessage = true;
			if (!this.discardingMessage && smtpInSession.TransportMailItem != null && smtpInSession.TransportMailItem.RootPart != null && !this.SendOnBehalfOfChecksPass())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary2);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			if (this.discardingMessage || !this.isLastChunk)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary3);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			if (this is DataSmtpCommand && (smtpInSession.TransportMailItem.MimeDocument.ComplianceStatus & MimeComplianceStatus.BareLinefeedInBody) != MimeComplianceStatus.Compliant)
			{
				if (smtpInSession.SmtpReceivePerformanceCounters != null)
				{
					smtpInSession.SmtpReceivePerformanceCounters.MessagesReceivedWithBareLinefeeds.Increment();
				}
				if (smtpInSession.Connector.BareLinefeedRejectionEnabled)
				{
					using (BareLinefeedDetector bareLinefeedDetector = new BareLinefeedDetector())
					{
						try
						{
							smtpInSession.TransportMailItem.MimeDocument.WriteTo(bareLinefeedDetector);
						}
						catch (BareLinefeedException)
						{
							if (smtpInSession.SmtpReceivePerformanceCounters != null)
							{
								smtpInSession.SmtpReceivePerformanceCounters.MessagesRefusedForBareLinefeeds.Increment();
							}
							smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary7);
							this.HandleSubmitFailure(SmtpResponse.InvalidContentBareLinefeeds);
							return this.RaiseOnRejectIfNecessary(isAsync);
						}
					}
				}
			}
			try
			{
				smtpInSession.TransportMailItem.Message.Normalize(NormalizeOptions.NormalizeMessageId | NormalizeOptions.MergeAddressHeaders | NormalizeOptions.RemoveDuplicateHeaders, false);
			}
			catch (IOException e3)
			{
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.DiscardOnException(e3);
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary6);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			catch (ExchangeDataException e4)
			{
				this.DiscardOnException(e4);
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary4);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			smtpInSession.TransportMailItem.UpdateCachedHeaders();
			if (!this.PriorityMessageChecksPass())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaisingOnRejectIfNecessary5);
				return this.RaiseOnRejectIfNecessary(isAsync);
			}
			this.messageId = smtpInSession.TransportMailItem.InternetMessageId;
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub)
			{
				bool flag = this.agentLoopChecker.IsEnabledInSmtp();
				bool flag2 = this.agentLoopChecker.CheckInSmtp(smtpInSession.TransportMailItem.RootPart.Headers);
				if (flag2)
				{
					MessageTrackingLog.TrackAgentGeneratedMessageRejected(MessageTrackingSource.SMTP, flag, smtpInSession.TransportMailItem);
					if (flag)
					{
						base.SmtpResponse = SmtpResponse.AgentGeneratedMessageDepthExceeded;
						this.StartDiscardingMessage();
					}
				}
			}
			this.EnableEODEvent();
			IAsyncResult asyncResult = this.RaiseEODEvent(null);
			if (!asyncResult.CompletedSynchronously)
			{
				return AsyncReturnType.Async;
			}
			return this.ContinueEndOfData(asyncResult, isAsync);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000CB61C File Offset: 0x000C981C
		protected virtual AsyncReturnType RawDataReceived(byte[] data, int offset, int numBytes)
		{
			if (this.IsBlob)
			{
				throw new InvalidOperationException("RawDataReceived should not be called for BDAT blob.");
			}
			AsyncReturnType asyncReturnType = AsyncReturnType.Sync;
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int>((long)this.GetHashCode(), "RawDataReceived received {0} bytes", numBytes);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			bool isDiscardingData = this.SmtpInParser.IsDiscardingData;
			int num;
			this.seenEod = this.SmtpInParser.Write(data, offset, numBytes, out num);
			if (numBytes != num)
			{
				smtpInSession.PutBackReceivedBytes(numBytes - num);
			}
			if (!isDiscardingData && this.SmtpInParser.IsDiscardingData)
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
			if (!this.discardingMessage && (this.seenEoh || !base.OnlyCheckMessageSizeAfterEoh) && DataBdatHelpers.MessageSizeExceeded(this.AccumulatedMessageSize, this.originalMessageSize, this.messageSizeLimit, smtpInSession.Permissions))
			{
				base.SmtpResponse = SmtpResponse.MessageTooLarge;
				smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "AccumulatedMessageSize: {0} > MessageSizeLimit: {1}", new object[]
				{
					(ulong)this.AccumulatedMessageSize,
					this.messageSizeLimit
				});
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				if (smtpInSession.SmtpReceivePerformanceCounters != null)
				{
					smtpInSession.SmtpReceivePerformanceCounters.MessagesRefusedForSize.Increment();
				}
			}
			this.shadowSession.BeginWrite(data, offset, num, this.seenEod, BaseDataSmtpCommand.shadowWriteCallback, this);
			if (this.FoundEndOfMessage())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RawDataFoundEndOfMessage);
				if (!this.discardingMessage)
				{
					try
					{
						if (!this.seenEoh)
						{
							this.bodyStream.Write(data, offset, 0);
							this.bodyStream.Flush();
						}
					}
					catch (IOException ex)
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceDebug<IOException>((long)this.GetHashCode(), "Handled IO exception: {0}", ex);
						smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
						this.DiscardOnException(ex);
					}
					catch (ExchangeDataException ex2)
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceDebug<ExchangeDataException>((long)this.GetHashCode(), "Handled parser exception: {0}", ex2);
						this.DiscardOnException(ex2);
					}
				}
			}
			if (this.eohEventArgs != null)
			{
				IAsyncResult asyncResult = DataBdatHelpers.RaiseEOHEvent(null, smtpInSession, new AsyncCallback(this.EndOfHeadersCallback), this.eohEventArgs);
				if (!asyncResult.CompletedSynchronously)
				{
					asyncReturnType = AsyncReturnType.Async;
				}
				else
				{
					asyncReturnType = this.ContinueEndOfHeaders(asyncResult);
				}
			}
			else if (this.seenEod)
			{
				asyncReturnType = this.OnEod(false);
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int, AsyncReturnType>((long)this.GetHashCode(), "RawDataReceived consumed {0} bytes, return returnType={1}", num, asyncReturnType);
			return asyncReturnType;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000CB90C File Offset: 0x000C9B0C
		protected void EndOfHeadersCallback(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				this.ContinueEndOfHeaders(ar);
			}
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000CB91E File Offset: 0x000C9B1E
		protected void EndOfDataCallback(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				this.ContinueEndOfData(ar, true);
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000CB934 File Offset: 0x000C9B34
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

		// Token: 0x06003341 RID: 13121 RVA: 0x000CB9D4 File Offset: 0x000C9BD4
		protected virtual Stream GetFirewalledStream()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			Stream stream = null;
			Header header = null;
			RestrictedHeaderSet restrictedHeaderSet = smtpOutSession.RestrictedHeaderSet;
			if ((restrictedHeaderSet != RestrictedHeaderSet.None && HeaderFirewall.ContainsBlockedHeaders(smtpOutSession.RoutedMailItem.RootPart.Headers, restrictedHeaderSet)) || (!smtpOutSession.Connector.CloudServicesMailEnabled && smtpOutSession.NextHopType.IsSmtpConnectorDeliveryType))
			{
				if (!smtpOutSession.Connector.CloudServicesMailEnabled && smtpOutSession.NextHopType.IsSmtpConnectorDeliveryType && HeaderFirewall.CrossPremisesHeadersPresent(smtpOutSession.RoutedMailItem.RootPart.Headers))
				{
					header = Header.Create("X-CrossPremisesHeadersFilteredBySendConnector");
					header.Value = HeaderFirewall.ComputerName;
				}
				stream = Streams.CreateTemporaryStorageStream();
				smtpOutSession.RoutedMailItem.RootPart.WriteTo(stream, null, new HeaderFirewall.OutputFilter(restrictedHeaderSet, smtpOutSession.NeedToDownConvertMIME, header));
				if (stream != null)
				{
					stream.Seek(0L, SeekOrigin.Begin);
				}
			}
			if (stream == null)
			{
				stream = smtpOutSession.RoutedMailItem.OpenMimeReadStream(smtpOutSession.NeedToDownConvertMIME);
			}
			return stream;
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000CBAD0 File Offset: 0x000C9CD0
		private static void CommitCallback(IAsyncResult asyncResult)
		{
			BaseDataSmtpCommand baseDataSmtpCommand = (BaseDataSmtpCommand)asyncResult.AsyncState;
			ISmtpInSession smtpInSession = (ISmtpInSession)baseDataSmtpCommand.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.CommitCallback);
			smtpInSession.SetupPoisonContext();
			if (baseDataSmtpCommand.SmtpResponse.StatusCode == "250" && baseDataSmtpCommand.SmtpResponse.EnhancedStatusCode == "2.6.0" && smtpInSession.TransportMailItem != null)
			{
				string recordId = smtpInSession.TransportMailItem.RecordId.ToString();
				if (smtpInSession.IsPeerShadowSession)
				{
					baseDataSmtpCommand.SmtpResponse = SmtpResponse.QueuedMailForRedundancy(SmtpCommand.GetBracketedString(baseDataSmtpCommand.messageId), recordId, smtpInSession.SmtpInServer.Name);
				}
				else
				{
					baseDataSmtpCommand.SmtpResponse = SmtpResponse.QueuedMailForDelivery(SmtpCommand.GetBracketedString(baseDataSmtpCommand.messageId), recordId, smtpInSession.SmtpInServer.Name, baseDataSmtpCommand.smtpCustomDataResponse);
				}
			}
			baseDataSmtpCommand.CommitMailItemCompleted(asyncResult);
			baseDataSmtpCommand.ContinueSubmitMessage();
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000CBBBC File Offset: 0x000C9DBC
		private static void ShadowOpenCallback(IAsyncResult asyncResult)
		{
			BaseDataSmtpCommand baseDataSmtpCommand = (BaseDataSmtpCommand)asyncResult.AsyncState;
			if (!baseDataSmtpCommand.shadowSession.EndOpen(asyncResult))
			{
				baseDataSmtpCommand.shadowSession.Close(AckStatus.Fail, SmtpResponse.Empty);
			}
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000CBBF4 File Offset: 0x000C9DF4
		private static void ShadowWriteCallback(IAsyncResult asyncResult)
		{
			BaseDataSmtpCommand baseDataSmtpCommand = (BaseDataSmtpCommand)asyncResult.AsyncState;
			if (!baseDataSmtpCommand.shadowSession.EndWrite(asyncResult))
			{
				baseDataSmtpCommand.shadowSession.Close(AckStatus.Fail, SmtpResponse.Empty);
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000CBC2C File Offset: 0x000C9E2C
		private bool CheckPoisonMessage(HeaderList headers, ISmtpInSession session)
		{
			Header header = headers.FindFirst("Message-ID");
			if (header == null)
			{
				return false;
			}
			string value = header.Value;
			if (!string.IsNullOrEmpty(value) && PoisonMessage.IsMessagePoison(value))
			{
				base.SmtpResponse = SmtpResponse.TooManyRelatedErrors;
				session.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Rejecting Message-ID: {0} because it was detected as poison", new object[]
				{
					value
				});
				return true;
			}
			return false;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000CBC90 File Offset: 0x000C9E90
		private AsyncReturnType RaiseOnRejectIfNecessary(bool isAsync)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "RaiseOnRejectIfNecessary");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (isAsync)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaiseOnRejectIfNecessaryAsync);
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaiseOnRejectIfNecessarySync);
			}
			if (!this.discardingMessage || this.shouldDisconnect || base.SmtpResponse.SmtpResponseType == SmtpResponseType.Success)
			{
				return this.FinishEodSequence(isAsync);
			}
			IAsyncResult asyncResult = smtpInSession.RaiseOnRejectEvent(null, this.originalEventArgs, base.SmtpResponse, new AsyncCallback(this.OnRejectCallback));
			if (!asyncResult.CompletedSynchronously)
			{
				return AsyncReturnType.Async;
			}
			return this.ContinueOnReject(asyncResult, isAsync);
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000CBD2F File Offset: 0x000C9F2F
		private void OnRejectCallback(IAsyncResult ar)
		{
			if (!ar.CompletedSynchronously)
			{
				this.ContinueOnReject(ar, true);
			}
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000CBD44 File Offset: 0x000C9F44
		private AsyncReturnType ContinueOnReject(IAsyncResult ar, bool isAsync)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "OnRejectCallback");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueOnReject);
			this.ProcessAgentResponse(ar, null);
			return this.FinishEodSequence(isAsync);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000CBD8C File Offset: 0x000C9F8C
		private AsyncReturnType FinishEodSequence(bool isAsync)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "FinishEodSequence");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (isAsync)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.FinishEodSequenceAsync);
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.FinishEodSequenceSync);
			}
			if (base.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				this.SetSuccessResponse();
				if (base.SmtpResponse.Equals(SmtpResponse.Empty))
				{
					base.SmtpResponse = SmtpResponse.DataTransactionFailed;
				}
			}
			base.IsResponseReady = true;
			if (this.SubmitMessageIfReady() == AsyncReturnType.Sync)
			{
				return this.EodDone(isAsync);
			}
			return AsyncReturnType.Async;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000CBE24 File Offset: 0x000CA024
		private void CommitMailItemCompleted(IAsyncResult asyncResult)
		{
			SmtpResponse failureSmtpResponse;
			SmtpResponse smtpResponse;
			if (!this.GetCommitResults(asyncResult, out failureSmtpResponse, out smtpResponse))
			{
				this.HandleSubmitFailure(failureSmtpResponse);
				return;
			}
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery && !smtpResponse.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = smtpResponse;
			}
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000CBE68 File Offset: 0x000CA068
		private void HandleSubmitFailure(SmtpResponse failureSmtpResponse)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.SubmitMessageFailed);
			if (failureSmtpResponse.Equals(SmtpResponse.Empty))
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.SubmitMessageFailedNoResponse);
			}
			base.SmtpResponse = failureSmtpResponse;
			this.StartDiscardingMessage();
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000CBEAC File Offset: 0x000CA0AC
		private bool GetCommitResults(IAsyncResult asyncResult, out SmtpResponse failureSmtpResponse, out SmtpResponse enqueueMailItemResponse)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.GetCommitResults);
			bool flag = false;
			failureSmtpResponse = SmtpResponse.Empty;
			enqueueMailItemResponse = SmtpResponse.Empty;
			try
			{
				Exception ex = null;
				if (!this.commitCoordinator.EndCommitMailItem(asyncResult, out failureSmtpResponse, out ex))
				{
					if (ex is ExchangeDataException)
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "GetCommitResults: MIME exception on commit: {0}", ex.Message);
						failureSmtpResponse = SmtpResponse.InvalidContent;
						smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "MIME exception on commit: {0}", new object[]
						{
							ex.Message
						});
						return false;
					}
					if (ex is IOException && ExceptionHelper.IsHandleableTransientCtsException(ex))
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "GetCommitResults: IO exception on commit: {0}", ex.Message);
						smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
						failureSmtpResponse = SmtpResponse.CTSParseError;
						byte[] data = Util.AsciiStringToBytes(string.Format(CultureInfo.InvariantCulture, "IO Exception on commit: {0}", new object[]
						{
							ex.Message
						}));
						smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, data, null);
						return false;
					}
					if (ex == null)
					{
						ExTraceGlobals.SmtpReceiveTracer.TraceError<string>((long)this.GetHashCode(), "GetCommitResults: Unknown error, returning {0}", failureSmtpResponse.ToString());
						smtpInSession.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, Util.AsciiStringToBytes("Unknown error on commit"), null);
						return false;
					}
					throw new LocalizedException(Strings.CommitMailFailed, ex);
				}
				else
				{
					LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceiveCommit, smtpInSession.TransportMailItem.LatencyTracker);
					if (smtpInSession.IsPeerShadowSession)
					{
						smtpInSession.TrackAndEnqueuePeerShadowMailItem();
					}
					else
					{
						enqueueMailItemResponse = smtpInSession.TrackAndEnqueueMailItem();
					}
					flag = true;
				}
			}
			catch (IOException ex2)
			{
				SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveRejectDueToStorageError, null, new object[]
				{
					smtpInSession.Connector.Name,
					ex2.Message
				});
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				failureSmtpResponse = SmtpResponse.DataTransactionFailed;
			}
			finally
			{
				if (!flag)
				{
					smtpInSession.DeleteTransportMailItem();
				}
				smtpInSession.ReleaseMailItem();
			}
			return flag;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000CC0F0 File Offset: 0x000CA2F0
		private void ContinueSubmitMessage()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueSubmitMessage);
			this.ContinueSubmitMessageIfReady();
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000CC118 File Offset: 0x000CA318
		private static bool GetSenderSizeLimit(ISmtpInSession session, out long limit)
		{
			limit = 0L;
			RoutingAddress outer;
			if (!Util.TryGetP2Sender(session.TransportMailItem.RootPart.Headers, out outer))
			{
				return false;
			}
			ProxyAddress innermostAddress = Sender.GetInnermostAddress(outer);
			Result<TransportMiniRecipient> result = session.TransportMailItem.ADRecipientCache.FindAndCacheRecipient(innermostAddress);
			if (result.Data == null)
			{
				return false;
			}
			Unlimited<ByteQuantifiedSize> maxSendSize = result.Data.MaxSendSize;
			if (maxSendSize.IsUnlimited)
			{
				maxSendSize = session.SmtpInServer.TransportSettings.MaxSendSize;
			}
			if (maxSendSize.IsUnlimited)
			{
				limit = long.MaxValue;
				return true;
			}
			limit = (long)maxSendSize.Value.ToBytes();
			return true;
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000CC1B8 File Offset: 0x000CA3B8
		private bool PriorityMessageChecksPass()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			SmtpResponse smtpResponse;
			if (!smtpInSession.TransportMailItem.ValidateDeliveryPriority(out smtpResponse))
			{
				base.SmtpResponse = smtpResponse;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			return true;
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000CC1F8 File Offset: 0x000CA3F8
		private IAsyncResult RaiseEODEvent(object state)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.RaiseEODEvent);
			if (smtpInSession.TransportMailItem != null)
			{
				smtpInSession.AgentLatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveOnEndOfData, smtpInSession.TransportMailItem.LatencyTracker);
			}
			return smtpInSession.AgentSession.BeginRaiseEvent("OnEndOfData", ReceiveMessageEventSourceImpl.Create(smtpInSession.SessionSource, this.eodEventArgs.MailItem), this.eodEventArgs, new AsyncCallback(this.EndOfDataCallback), state);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000CC274 File Offset: 0x000CA474
		private AsyncReturnType ContinueEndOfHeaders(IAsyncResult ar)
		{
			bool flag = ar != null && !ar.CompletedSynchronously;
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<bool>((long)this.GetHashCode(), "EndOfHeadersCallback isAsync is {0}", flag);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (flag)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueEndOfHeadersAsync);
			}
			else
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueEndOfHeadersSync);
			}
			if (smtpInSession.TransportMailItem != null)
			{
				smtpInSession.AgentLatencyTracker.EndTrackLatency();
			}
			this.ProcessAgentResponse(ar, this.eohEventArgs);
			this.eohEventArgs = null;
			if (this.seenEod)
			{
				return this.OnEod(flag);
			}
			if (flag)
			{
				smtpInSession.RawDataReceivedCompleted();
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<bool>((long)this.GetHashCode(), "ContinueEndOfHeaders isAsync is {0}", flag);
			if (!flag)
			{
				return AsyncReturnType.Sync;
			}
			return AsyncReturnType.Async;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000CC328 File Offset: 0x000CA528
		private AsyncReturnType ContinueEndOfData(IAsyncResult ar, bool isAsync)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "EndOfDataCallback");
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			ArgumentValidator.ThrowIfNull("session.TransportMailItem", smtpInSession.TransportMailItem);
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.ContinueEndOfData);
			smtpInSession.AgentLatencyTracker.EndTrackLatency();
			try
			{
				if (smtpInSession.TransportMailItem.Message.TnefPart != null)
				{
					Util.ConvertMessageClassificationsFromTnefToHeaders(smtpInSession.TransportMailItem);
				}
				ClassificationUtils.DropStoreLabels(smtpInSession.TransportMailItem.RootPart.Headers);
				this.StampOriginalMessageSize();
			}
			catch (IOException e)
			{
				smtpInSession.UpdateSmtpAvailabilityPerfCounter(LegitimateSmtpAvailabilityCategory.RejectDueToIOException);
				this.DiscardOnException(e);
			}
			catch (ExchangeDataException e2)
			{
				this.DiscardOnException(e2);
			}
			this.ProcessAgentResponse(ar, this.eodEventArgs);
			this.eodEventArgs = null;
			return this.RaiseOnRejectIfNecessary(isAsync);
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000CC408 File Offset: 0x000CA608
		private void StampOriginalMessageSize()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			HeaderList headers = smtpInSession.TransportMailItem.RootPart.Headers;
			if (headers.FindFirst("X-MS-Exchange-Organization-OriginalSize") == null)
			{
				headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", smtpInSession.TransportMailItem.MimeSize.ToString(NumberFormatInfo.InvariantInfo)));
			}
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000CC46C File Offset: 0x000CA66C
		private void DiscardOnException(Exception e)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceError<Exception>((long)this.GetHashCode(), "MessageWriteStream.Flush OR Close threw exception: {0}", e);
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.DiscardOnException);
			this.ParserException(e);
			this.StartDiscardingMessage();
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000CC4B4 File Offset: 0x000CA6B4
		private bool P1ChecksPass()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (smtpInSession.TransportMailItem.From == RoutingAddress.NullReversePath)
			{
				return true;
			}
			SmtpResponse smtpResponse;
			if (!BaseDataSmtpCommand.IsSenderOkForClient(smtpInSession, smtpInSession.TransportMailItem.From, true, out smtpResponse))
			{
				base.SmtpResponse = smtpResponse;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			if (SmtpInAccessChecker.HasZeroProhibitSendQuota(smtpInSession, smtpInSession.TransportMailItem.ADRecipientCache, smtpInSession.TransportMailItem.From, out smtpResponse))
			{
				base.SmtpResponse = smtpResponse;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			return true;
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000CC54C File Offset: 0x000CA74C
		private bool P2ChecksPass()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			RoutingAddress routingAddress;
			Util.TryGetP2Sender(smtpInSession.TransportMailItem.RootPart.Headers, out routingAddress);
			if (!routingAddress.Equals(RoutingAddress.NullReversePath) && !routingAddress.Equals(RoutingAddress.Empty) && !routingAddress.Equals(smtpInSession.TransportMailItem.From))
			{
				SmtpResponse smtpResponse;
				if (!BaseDataSmtpCommand.IsSenderOkForClient(smtpInSession, routingAddress, false, out smtpResponse))
				{
					base.SmtpResponse = smtpResponse;
					base.IsResponseReady = false;
					this.StartDiscardingMessage();
					return false;
				}
				if (SmtpInAccessChecker.HasZeroProhibitSendQuota(smtpInSession, smtpInSession.TransportMailItem.ADRecipientCache, routingAddress, out smtpResponse))
				{
					base.SmtpResponse = smtpResponse;
					base.IsResponseReady = false;
					this.StartDiscardingMessage();
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000CC5FC File Offset: 0x000CA7FC
		private bool SendOnBehalfOfChecksPass()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			HeaderList headers = smtpInSession.TransportMailItem.RootPart.Headers;
			RoutingAddress senderAddress = Util.RetrieveRoutingAddress(headers, HeaderId.Sender);
			RoutingAddress routingAddress = Util.RetrieveRoutingAddress(headers, HeaderId.From);
			SmtpResponse smtpResponse;
			if (!SmtpInAccessChecker.VerifySendOnBehalfOfPermissionsInAD(smtpInSession, smtpInSession.TransportMailItem.ADRecipientCache, senderAddress, routingAddress, out smtpResponse))
			{
				base.SmtpResponse = smtpResponse;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			if (routingAddress.IsValid && routingAddress != RoutingAddress.NullReversePath && SmtpInAccessChecker.HasZeroProhibitSendQuota(smtpInSession, smtpInSession.TransportMailItem.ADRecipientCache, routingAddress, out smtpResponse))
			{
				base.SmtpResponse = smtpResponse;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			return true;
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000CC6A8 File Offset: 0x000CA8A8
		private bool CheckSubmissionQuota()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!smtpInSession.SmtpInServer.IsBridgehead)
			{
				return true;
			}
			if (!SubmissionQuotaChecker.CheckSubmissionQuota(SmtpInSessionState.FromSmtpInSession(smtpInSession)))
			{
				base.SmtpResponse = SmtpResponse.SubmissionQuotaExceeded;
				base.IsResponseReady = false;
				this.StartDiscardingMessage();
				return false;
			}
			return true;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000CC6F8 File Offset: 0x000CA8F8
		private bool CheckAttributionAndCreateADRecipientCache(ISmtpInSession session)
		{
			SmtpResponse smtpResponse = DataBdatHelpers.CheckAttributionAndCreateAdRecipientCache(session.TransportMailItem, this.transportAppConfig.SmtpReceiveConfiguration.RejectUnscopedMessages, session.InboundClientProxyState != InboundClientProxyStates.None, false);
			if (!smtpResponse.Equals(SmtpResponse.Empty))
			{
				base.SmtpResponse = smtpResponse;
			}
			return smtpResponse.Equals(SmtpResponse.Empty);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000CC750 File Offset: 0x000CA950
		private void ProcessAgentResponse(IAsyncResult ar, EventArgs args)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			SmtpResponse smtpResponse = smtpInSession.AgentSession.EndRaiseEvent(ar);
			this.shouldDisconnect = (!smtpResponse.IsEmpty || smtpInSession.SessionSource.ShouldDisconnect);
			if (this.shouldDisconnect)
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.SessionDisconnectByAgent);
				if (!smtpResponse.IsEmpty)
				{
					base.SmtpResponse = smtpResponse;
				}
				this.StartDiscardingMessage();
				return;
			}
			if (args != null && !smtpInSession.SessionSource.SmtpResponse.Equals(SmtpResponse.Empty))
			{
				if (smtpInSession.SessionSource.SmtpResponse.SmtpResponseType == SmtpResponseType.Success)
				{
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.MessageDiscardedByAgent);
				}
				else
				{
					smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.MessageRejectedByAgent);
				}
				base.SmtpResponse = smtpInSession.SessionSource.SmtpResponse;
				this.originalEventArgs = args;
				this.StartDiscardingMessage();
				smtpInSession.SessionSource.SmtpResponse = SmtpResponse.Empty;
			}
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000CC830 File Offset: 0x000CAA30
		private void SetOorg()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			if (!smtpInSession.TransportMailItem.ExposeMessageHeaders)
			{
				throw new InvalidOperationException("SetOorg() invoked on mail item that does not expose headers");
			}
			string oorg = DataBdatHelpers.GetOorg(smtpInSession.TransportMailItem, smtpInSession.Capabilities, smtpInSession.LogSession, smtpInSession.TransportMailItem.RootPart.Headers);
			smtpInSession.TransportMailItem.Oorg = oorg;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000CC898 File Offset: 0x000CAA98
		private static bool IsSenderOkForClient(ISmtpInSession session, RoutingAddress senderAddress, bool isMailFromSender, out SmtpResponse failureResponse)
		{
			return SmtpInAccessChecker.VerifySenderOkForClient(session, session.SmtpInServer.Configuration.GetAcceptedDomainTable(session.TransportMailItem.ADRecipientCache.OrganizationId), session.TransportMailItem.ADRecipientCache, session.SmtpInServer.IsBridgehead, senderAddress, session.RemoteWindowsIdentity, session.SmtpInServer.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName, isMailFromSender, out failureResponse);
		}

		// Token: 0x040019F9 RID: 6649
		internal const NormalizeOptions EndOfDataNormalizeOptions = NormalizeOptions.NormalizeMessageId | NormalizeOptions.MergeAddressHeaders | NormalizeOptions.RemoveDuplicateHeaders;

		// Token: 0x040019FA RID: 6650
		protected long originalMessageSize = long.MaxValue;

		// Token: 0x040019FB RID: 6651
		protected long messageSizeLimit;

		// Token: 0x040019FC RID: 6652
		protected Stream bodyStream;

		// Token: 0x040019FD RID: 6653
		protected bool seenEoh;

		// Token: 0x040019FE RID: 6654
		protected bool discardingMessage;

		// Token: 0x040019FF RID: 6655
		protected string messageId;

		// Token: 0x04001A00 RID: 6656
		protected bool isFirstChunk;

		// Token: 0x04001A01 RID: 6657
		protected bool isLastChunk;

		// Token: 0x04001A02 RID: 6658
		protected ITransportAppConfig transportAppConfig;

		// Token: 0x04001A03 RID: 6659
		private static readonly AsyncCallback commitCallback = new AsyncCallback(BaseDataSmtpCommand.CommitCallback);

		// Token: 0x04001A04 RID: 6660
		private static readonly AsyncCallback shadowWriteCallback = new AsyncCallback(BaseDataSmtpCommand.ShadowWriteCallback);

		// Token: 0x04001A05 RID: 6661
		private static readonly AsyncCallback shadowOpenCallback = new AsyncCallback(BaseDataSmtpCommand.ShadowOpenCallback);

		// Token: 0x04001A06 RID: 6662
		private readonly string smtpCustomDataResponse;

		// Token: 0x04001A07 RID: 6663
		private IShadowSession shadowSession;

		// Token: 0x04001A08 RID: 6664
		private CommitCoordinator commitCoordinator;

		// Token: 0x04001A09 RID: 6665
		private EndOfHeadersEventArgs eohEventArgs;

		// Token: 0x04001A0A RID: 6666
		private EndOfDataEventArgs eodEventArgs;

		// Token: 0x04001A0B RID: 6667
		private EventArgs originalEventArgs;

		// Token: 0x04001A0C RID: 6668
		private bool shouldDisconnect;

		// Token: 0x04001A0D RID: 6669
		private bool seenEod;

		// Token: 0x04001A0E RID: 6670
		private AgentGeneratedMessageLoopChecker agentLoopChecker;
	}
}
