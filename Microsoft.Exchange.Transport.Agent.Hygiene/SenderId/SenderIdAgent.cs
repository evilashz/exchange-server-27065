using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.SenderId;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;

namespace Microsoft.Exchange.Transport.Agent.SenderId
{
	// Token: 0x0200002D RID: 45
	internal sealed class SenderIdAgent : SmtpReceiveAgent
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00009024 File Offset: 0x00007224
		public SenderIdAgent(SenderIdConfig senderIdConfig, BypassedRecipients bypassedRecipients, HashSet<string> bypassedSenderDomains, SmtpServer server)
		{
			base.OnEndOfHeaders += this.EndOfHeadersHandler;
			this.senderIdConfig = senderIdConfig;
			this.bypassedRecipients = bypassedRecipients;
			this.bypassedSenderDomains = bypassedSenderDomains;
			this.server = server;
			this.senderIdValidator = new SenderIdValidator(server);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009074 File Offset: 0x00007274
		private static SmtpResponse CreateFailResponse(SenderIdResult result)
		{
			SmtpResponse result2;
			if (string.IsNullOrEmpty(result.Explanation))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Sender ID (PRA) {0}", new object[]
				{
					SenderIdAgent.Responses.FailReasons[result.FailReason]
				});
				result2 = new SmtpResponse("550", "5.7.1", new string[]
				{
					text
				});
			}
			else
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "Sender ID (PRA) {0} - {1}", new object[]
				{
					SenderIdAgent.Responses.FailReasons[result.FailReason],
					result.Explanation
				});
				result2 = new SmtpResponse("550", "5.7.1", new string[]
				{
					text2
				});
			}
			return result2;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009134 File Offset: 0x00007334
		private static void AddHeader(HeaderList headers, string headerName, string value)
		{
			Header header = Header.Create(headerName);
			header.Value = value;
			headers.AppendChild(header);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009158 File Offset: 0x00007358
		private static void StampReceivedSpf(HeaderList headers, SenderIdStatus status, string serverFqdn, RoutingAddress pra, IPAddress clientIP, string helloDomain)
		{
			string value = string.Empty;
			switch (status)
			{
			case SenderIdStatus.Pass:
			{
				string format = "Pass ({0}: domain of {1} designates {2} as permitted sender) receiver={0}; client-ip={2}; helo={3};";
				value = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					serverFqdn,
					pra,
					clientIP,
					helloDomain
				});
				break;
			}
			case SenderIdStatus.Neutral:
				value = string.Format(CultureInfo.InvariantCulture, "Neutral ({0}: {1} is neither permitted nor denied by domain of {2})", new object[]
				{
					serverFqdn,
					clientIP,
					pra
				});
				break;
			case SenderIdStatus.SoftFail:
				value = string.Format(CultureInfo.InvariantCulture, "SoftFail ({0}: domain of transitioning {1} discourages use of {2} as permitted sender)", new object[]
				{
					serverFqdn,
					pra,
					clientIP
				});
				break;
			case SenderIdStatus.Fail:
			{
				string format = "Fail ({0}: domain of {1} does not designate {2} as permitted sender) receiver={0}; client-ip={2}; helo={3};";
				value = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					serverFqdn,
					pra,
					clientIP,
					helloDomain
				});
				break;
			}
			case SenderIdStatus.None:
				value = string.Format(CultureInfo.InvariantCulture, "None ({0}: {1} does not designate permitted sender hosts)", new object[]
				{
					serverFqdn,
					pra
				});
				break;
			case SenderIdStatus.TempError:
				value = string.Format(CultureInfo.InvariantCulture, "TempError ({0}: error in processing during lookup of {1}: DNS timeout)", new object[]
				{
					serverFqdn,
					pra
				});
				break;
			case SenderIdStatus.PermError:
				value = string.Format(CultureInfo.InvariantCulture, "PermError ({0}: domain of {1} used an invalid SPF mechanism)", new object[]
				{
					serverFqdn,
					pra
				});
				break;
			default:
				throw new ArgumentOutOfRangeException("status", status, "StampReceivedSpf: unknown SenderIdStatus.");
			}
			Header header = Header.Create("Received-SPF");
			Header refChild = headers.FindFirst("Received-SPF");
			header.Value = value;
			headers.InsertBefore(header, refChild);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00009336 File Offset: 0x00007536
		private void EndOfHeadersHandler(ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			if (this.IsPolicyDisabled(endOfHeadersEventArgs.SmtpSession))
			{
				ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Not validating message because policy is disabled or session has AntispamBypass permission.");
				return;
			}
			if (!this.IsValidationNeeded(endOfHeadersEventArgs))
			{
				Util.PerformanceCounters.MessageBypassedValidation();
				return;
			}
			this.ValidateSender(receiveMessageEventSource, endOfHeadersEventArgs);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009374 File Offset: 0x00007574
		private bool IsPolicyDisabled(SmtpSession smtpSession)
		{
			return !CommonUtils.IsEnabled(this.senderIdConfig, smtpSession);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009385 File Offset: 0x00007585
		private bool IsValidationNeeded(EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			return !this.IsOnAllowList(endOfHeadersEventArgs) && !this.AreAllRecipientsBypassed(endOfHeadersEventArgs) && !CommonUtils.HasAntispamBypassPermission(endOfHeadersEventArgs.SmtpSession, ExTraceGlobals.AgentTracer, this);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000093B0 File Offset: 0x000075B0
		private bool IsOnAllowList(EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			object obj;
			if (endOfHeadersEventArgs.SmtpSession.Properties.TryGetValue("Microsoft.Exchange.IsOnAllowList", out obj) && obj is bool)
			{
				bool flag = (bool)obj;
				if (flag)
				{
					ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Not validating message because it is on allow list");
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009404 File Offset: 0x00007604
		private bool AreAllRecipientsBypassed(EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			this.numBypassedRecipients = this.bypassedRecipients.NumBypassedRecipients(endOfHeadersEventArgs.MailItem.Recipients);
			bool flag = this.numBypassedRecipients == endOfHeadersEventArgs.MailItem.Recipients.Count;
			if (flag)
			{
				ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Not validating message because all recipients are bypassed");
			}
			return flag;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009460 File Offset: 0x00007660
		private void ValidateSender(ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "SenderIdAgent.EndOfHeadersHandler");
			IPAddress lastExternalIPAddress = endOfHeadersEventArgs.SmtpSession.LastExternalIPAddress;
			if (lastExternalIPAddress == null)
			{
				Util.PerformanceCounters.MissingOriginatingIP();
				return;
			}
			RoutingAddress purportedResponsibleAddress = SenderIdValidator.GetPurportedResponsibleAddress(endOfHeadersEventArgs.Headers);
			ExTraceGlobals.AgentTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "PRA for message is: {0}", purportedResponsibleAddress);
			if (string.IsNullOrEmpty(purportedResponsibleAddress.DomainPart))
			{
				Util.PerformanceCounters.NoPRA();
				this.ActOnNoPRA(receiveMessageEventSource, endOfHeadersEventArgs);
				SenderIdAgent.StampReceivedSpf(endOfHeadersEventArgs.Headers, SenderIdStatus.Fail, this.server.Name, purportedResponsibleAddress, lastExternalIPAddress, endOfHeadersEventArgs.SmtpSession.HelloDomain);
				return;
			}
			endOfHeadersEventArgs.MailItem.Properties["Microsoft.Exchange.PRA"] = purportedResponsibleAddress.ToString();
			endOfHeadersEventArgs.MailItem.Properties["Microsoft.Exchange.PRD"] = purportedResponsibleAddress.DomainPart;
			SenderIdAgent.AddHeader(endOfHeadersEventArgs.Headers, "X-MS-Exchange-Organization-PRD", purportedResponsibleAddress.DomainPart);
			if (this.bypassedSenderDomains.Contains(purportedResponsibleAddress.DomainPart))
			{
				ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "PRD is on bypassed sender domains list, skipping Sender Id");
				Util.PerformanceCounters.MessageBypassedValidation();
				return;
			}
			this.asyncContext = base.GetAgentAsyncContext();
			SenderIdAgent.AsyncState asyncState = new SenderIdAgent.AsyncState(purportedResponsibleAddress, receiveMessageEventSource, endOfHeadersEventArgs);
			bool processExpModifier = this.senderIdConfig.SpoofedDomainAction == SenderIdAction.Reject;
			this.senderIdValidator.BeginCheckHost(lastExternalIPAddress, purportedResponsibleAddress, endOfHeadersEventArgs.SmtpSession.HelloDomain, processExpModifier, new AsyncCallback(this.CheckHostCallback), asyncState);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000095CC File Offset: 0x000077CC
		private void CheckHostCallback(IAsyncResult ar)
		{
			this.Resume();
			SenderIdAgent.AsyncState asyncState = (SenderIdAgent.AsyncState)ar.AsyncState;
			SenderIdResult senderIdResult = this.senderIdValidator.EndCheckHost(ar);
			ExTraceGlobals.AgentTracer.TraceDebug<SenderIdStatus, SenderIdFailReason, string>((long)this.GetHashCode(), "Result of CheckHost - Status: {0}; FailReason: {1}; Explanation: \"{2}\"", senderIdResult.Status, senderIdResult.FailReason, senderIdResult.Explanation);
			asyncState.EndOfHeadersEventArgs.MailItem.Properties["Microsoft.Exchange.SenderIdStatus"] = senderIdResult.Status.ToString();
			SenderIdAgent.AddHeader(asyncState.EndOfHeadersEventArgs.Headers, "X-MS-Exchange-Organization-SenderIdResult", senderIdResult.Status.ToString());
			Util.PerformanceCounters.MessageValidatedWithResult(senderIdResult);
			switch (senderIdResult.Status)
			{
			case SenderIdStatus.Pass:
			case SenderIdStatus.Neutral:
			case SenderIdStatus.SoftFail:
			case SenderIdStatus.None:
			case SenderIdStatus.PermError:
				break;
			case SenderIdStatus.Fail:
				this.ActOnFail(asyncState, senderIdResult);
				break;
			case SenderIdStatus.TempError:
				if (Util.AsyncDns.IsDnsServerListEmpty())
				{
					SenderIdAgent.eventLogger.LogEvent(AgentsEventLogConstants.Tuple_SenderIdDnsNotConfigured, null, null);
					ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "DNS server list is empty");
				}
				this.ActOnTempError(asyncState);
				break;
			default:
				ExTraceGlobals.AgentTracer.TraceError<SenderIdStatus>((long)this.GetHashCode(), "Invalid result status: {0}", senderIdResult.Status);
				throw new InvalidOperationException("Invalid result status");
			}
			SenderIdAgent.StampReceivedSpf(asyncState.EndOfHeadersEventArgs.Headers, senderIdResult.Status, this.server.Name, asyncState.Pra, asyncState.EndOfHeadersEventArgs.SmtpSession.LastExternalIPAddress, asyncState.EndOfHeadersEventArgs.SmtpSession.HelloDomain);
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "SenderIdAgent calling agent async callback");
			this.AsyncCompleted();
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009770 File Offset: 0x00007970
		private void ActOnNoPRA(ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs)
		{
			switch (this.senderIdConfig.SpoofedDomainAction)
			{
			case SenderIdAction.StampStatus:
				endOfHeadersEventArgs.MailItem.Properties["Microsoft.Exchange.SenderIdStatus"] = SenderIdStatus.Fail.ToString();
				return;
			case SenderIdAction.Reject:
				this.RejectMessage(receiveMessageEventSource, endOfHeadersEventArgs, SenderIdAgent.Responses.MissingPRAResponse, "MissingPRA");
				return;
			case SenderIdAction.Delete:
				this.DeleteMessage(receiveMessageEventSource, endOfHeadersEventArgs, "MissingPRA");
				return;
			default:
				ExTraceGlobals.AgentTracer.TraceError<SenderIdAction>((long)this.GetHashCode(), "Invalid SpoofedDomainAction: {0}", this.senderIdConfig.SpoofedDomainAction);
				return;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00009800 File Offset: 0x00007A00
		private void ActOnFail(SenderIdAgent.AsyncState asyncState, SenderIdResult result)
		{
			string rejectId = SenderIdAgent.RejectContext.Id.Fail[(int)result.FailReason];
			switch (this.senderIdConfig.SpoofedDomainAction)
			{
			case SenderIdAction.StampStatus:
				break;
			case SenderIdAction.Reject:
				this.RejectMessage(asyncState.ReceiveMessageEventSource, asyncState.EndOfHeadersEventArgs, SenderIdAgent.CreateFailResponse(result), rejectId);
				return;
			case SenderIdAction.Delete:
				this.DeleteMessage(asyncState.ReceiveMessageEventSource, asyncState.EndOfHeadersEventArgs, rejectId);
				return;
			default:
				ExTraceGlobals.AgentTracer.TraceError<SenderIdAction>((long)this.GetHashCode(), "Invalid SpoofedDomainAction: {0}", this.senderIdConfig.SpoofedDomainAction);
				break;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000988C File Offset: 0x00007A8C
		private void ActOnTempError(SenderIdAgent.AsyncState asyncState)
		{
			switch (this.senderIdConfig.TempErrorAction)
			{
			case SenderIdAction.StampStatus:
				break;
			case SenderIdAction.Reject:
				this.RejectMessage(asyncState.ReceiveMessageEventSource, asyncState.EndOfHeadersEventArgs, SenderIdAgent.Responses.TempErrorResponse, "TempError");
				return;
			default:
				ExTraceGlobals.AgentTracer.TraceError<SenderIdAction>((long)this.GetHashCode(), "Invalid TempErrorAction: {0}", this.senderIdConfig.TempErrorAction);
				break;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000098F4 File Offset: 0x00007AF4
		private void RejectMessage(ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs, SmtpResponse smtpResponse, string rejectId)
		{
			LogEntry logEntry = SenderIdAgent.RejectContext.CreateLogEntry(rejectId, endOfHeadersEventArgs.MailItem);
			if (this.numBypassedRecipients == 0)
			{
				ExTraceGlobals.AgentTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Rejecting message with response: \n{0}", smtpResponse);
				AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, endOfHeadersEventArgs, endOfHeadersEventArgs.SmtpSession, endOfHeadersEventArgs.MailItem, smtpResponse, logEntry);
				receiveMessageEventSource.RejectMessage(smtpResponse);
				return;
			}
			ExTraceGlobals.AgentTracer.TraceDebug<SmtpResponse>((long)this.GetHashCode(), "Rejecting non-bypassed recipients with response: \n{0}", smtpResponse);
			this.bypassedRecipients.RemoveNonBypassedRecipients(endOfHeadersEventArgs.MailItem, true, smtpResponse, base.Name, base.EventTopic, endOfHeadersEventArgs, endOfHeadersEventArgs.SmtpSession, logEntry);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000999C File Offset: 0x00007B9C
		private void DeleteMessage(ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs, string rejectId)
		{
			LogEntry logEntry = SenderIdAgent.RejectContext.CreateLogEntry(rejectId, endOfHeadersEventArgs.MailItem);
			SmtpResponse response = SmtpResponse.QueuedMailForDelivery(endOfHeadersEventArgs.Headers.FindFirst("Message-Id").Value);
			if (this.numBypassedRecipients == 0)
			{
				ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Silently deleting message");
				AgentLog.Instance.LogDeleteMessage(base.Name, base.EventTopic, endOfHeadersEventArgs, endOfHeadersEventArgs.SmtpSession, endOfHeadersEventArgs.MailItem, logEntry);
				receiveMessageEventSource.RejectMessage(response);
				return;
			}
			ExTraceGlobals.AgentTracer.TraceDebug((long)this.GetHashCode(), "Silently removing non-bypassed recipients");
			this.bypassedRecipients.RemoveNonBypassedRecipients(endOfHeadersEventArgs.MailItem, false, response, base.Name, base.EventTopic, endOfHeadersEventArgs, endOfHeadersEventArgs.SmtpSession, logEntry);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00009A58 File Offset: 0x00007C58
		private void AsyncCompleted()
		{
			if (this.asyncContext != null)
			{
				AgentAsyncContext agentAsyncContext = this.asyncContext;
				this.asyncContext = null;
				agentAsyncContext.Complete();
				return;
			}
			ExTraceGlobals.AgentTracer.TraceError((long)this.GetHashCode(), "AsyncCompleted() was called but MEx context is null. This should never happen.");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009A98 File Offset: 0x00007C98
		private void Resume()
		{
			if (this.asyncContext != null)
			{
				this.asyncContext.Resume();
			}
		}

		// Token: 0x04000104 RID: 260
		private const string ReceivedSpfHeader = "Received-SPF";

		// Token: 0x04000105 RID: 261
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.OtherTracer.Category, "MSExchange Antispam");

		// Token: 0x04000106 RID: 262
		private SenderIdConfig senderIdConfig;

		// Token: 0x04000107 RID: 263
		private BypassedRecipients bypassedRecipients;

		// Token: 0x04000108 RID: 264
		private SenderIdValidator senderIdValidator;

		// Token: 0x04000109 RID: 265
		private int numBypassedRecipients;

		// Token: 0x0400010A RID: 266
		private HashSet<string> bypassedSenderDomains;

		// Token: 0x0400010B RID: 267
		private SmtpServer server;

		// Token: 0x0400010C RID: 268
		private AgentAsyncContext asyncContext;

		// Token: 0x0200002E RID: 46
		private static class RejectContext
		{
			// Token: 0x06000112 RID: 274 RVA: 0x00009AC8 File Offset: 0x00007CC8
			public static LogEntry CreateLogEntry(string contextId, MailItem mailItem)
			{
				object reasonData;
				if (!mailItem.Properties.TryGetValue("Microsoft.Exchange.PRA", out reasonData))
				{
					reasonData = "No valid PRA";
				}
				return new LogEntry(contextId, reasonData);
			}

			// Token: 0x0200002F RID: 47
			internal static class Id
			{
				// Token: 0x0400010D RID: 269
				public const string MissingPRA = "MissingPRA";

				// Token: 0x0400010E RID: 270
				public const string TempError = "TempError";

				// Token: 0x0400010F RID: 271
				public const string PermError = "PermError";

				// Token: 0x04000110 RID: 272
				public static readonly string[] Fail = new string[]
				{
					"Error",
					"None",
					"Fail_NotPermitted",
					"Fail_MalformedDomain",
					"Fail_DomainDoesNotExist"
				};
			}
		}

		// Token: 0x02000030 RID: 48
		private static class Responses
		{
			// Token: 0x06000114 RID: 276 RVA: 0x00009B3C File Offset: 0x00007D3C
			private static Dictionary<SenderIdFailReason, string> GetFailReasons()
			{
				Dictionary<SenderIdFailReason, string> dictionary = new Dictionary<SenderIdFailReason, string>(3);
				dictionary[SenderIdFailReason.NotPermitted] = "Not Permitted";
				dictionary[SenderIdFailReason.MalformedDomain] = "Malformed Domain";
				dictionary[SenderIdFailReason.DomainDoesNotExist] = "Domain Does Not Exist";
				return dictionary;
			}

			// Token: 0x04000111 RID: 273
			public const string FailStatusCode = "550";

			// Token: 0x04000112 RID: 274
			public const string FailEnhancedStatusCode = "5.7.1";

			// Token: 0x04000113 RID: 275
			public const string FailText = "Sender ID (PRA) {0}";

			// Token: 0x04000114 RID: 276
			public const string FailTextWithExplanation = "Sender ID (PRA) {0} - {1}";

			// Token: 0x04000115 RID: 277
			public const string NotPermittedReason = "Not Permitted";

			// Token: 0x04000116 RID: 278
			public const string MalformedDomainReason = "Malformed Domain";

			// Token: 0x04000117 RID: 279
			public const string DomainDoesNotExistReason = "Domain Does Not Exist";

			// Token: 0x04000118 RID: 280
			public static readonly SmtpResponse MissingPRAResponse = new SmtpResponse("550", "5.7.1", new string[]
			{
				"Missing purported responsible address"
			});

			// Token: 0x04000119 RID: 281
			public static readonly SmtpResponse TempErrorResponse = new SmtpResponse("450", "4.4.3", new string[]
			{
				"Sender ID check is temporarily unavailable"
			});

			// Token: 0x0400011A RID: 282
			public static readonly Dictionary<SenderIdFailReason, string> FailReasons = SenderIdAgent.Responses.GetFailReasons();
		}

		// Token: 0x02000031 RID: 49
		private class AsyncState
		{
			// Token: 0x06000116 RID: 278 RVA: 0x00009BD7 File Offset: 0x00007DD7
			public AsyncState(RoutingAddress pra, ReceiveMessageEventSource receiveMessageEventSource, EndOfHeadersEventArgs endOfHeadersEventArgs)
			{
				this.Pra = pra;
				this.ReceiveMessageEventSource = receiveMessageEventSource;
				this.EndOfHeadersEventArgs = endOfHeadersEventArgs;
			}

			// Token: 0x0400011B RID: 283
			public readonly RoutingAddress Pra;

			// Token: 0x0400011C RID: 284
			public readonly ReceiveMessageEventSource ReceiveMessageEventSource;

			// Token: 0x0400011D RID: 285
			public readonly EndOfHeadersEventArgs EndOfHeadersEventArgs;
		}
	}
}
