using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Data.Transport.Interop;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.ContentFilter;
using Microsoft.Exchange.SenderId;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Transport.Agent.ContentFilter
{
	// Token: 0x02000012 RID: 18
	internal sealed class ContentFilterAgent : SmtpReceiveAgent
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00003E60 File Offset: 0x00002060
		internal ContentFilterAgent(ContentFilterAgentFactory agentFactory, ContentFilterConfig contentFilterConfig, BypassedRecipients bypassedRecipients, BypassedSenders bypassedSenders)
		{
			this.agentFactory = agentFactory;
			this.contentFilterConfig = contentFilterConfig;
			this.bypassedRecipients = bypassedRecipients;
			this.quarantineMailboxIsValid = (contentFilterConfig.QuarantineMailbox != null && contentFilterConfig.QuarantineMailbox.Value.IsValidAddress);
			this.bypassedSenders = bypassedSenders;
			base.OnEndOfData += this.EndOfDataHandler;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003ED1 File Offset: 0x000020D1
		private static bool ContainsPuzzle(MailItem mailItem)
		{
			return mailItem.Message.MimeDocument.RootPart.Headers.FindFirst("x-cr-hashedpuzzle") != null;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003EF8 File Offset: 0x000020F8
		private static void StampAntiSpamReport(MailItem mailItem, string value)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			if (!string.IsNullOrEmpty(value) && value.Length > 1024)
			{
				value = value.Substring(0, 1021);
				value += "...";
			}
			Header header = Header.Create("X-MS-Exchange-Organization-Antispam-Report");
			mailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			header.Value = value;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003F74 File Offset: 0x00002174
		private static void SetComArgumentsProperty(ComArguments comArguments, int comArgumentsIndex, MailItem mailItem, string mailItemPropertyName, string defaultValue)
		{
			object obj = null;
			if (!mailItem.Properties.TryGetValue(mailItemPropertyName, out obj))
			{
				obj = defaultValue;
			}
			if (obj != null)
			{
				comArguments[comArgumentsIndex] = Encoding.Unicode.GetBytes(obj.ToString());
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003FB0 File Offset: 0x000021B0
		private static RoutingAddress GetSenderForSafeSenderCheck(MailItem mailItem)
		{
			EmailRecipient from = mailItem.Message.From;
			string text = (from != null) ? from.SmtpAddress : string.Empty;
			if (!string.IsNullOrEmpty(text) && RoutingAddress.IsValidAddress(text))
			{
				return new RoutingAddress(text);
			}
			return RoutingAddress.Empty;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000042CC File Offset: 0x000024CC
		private static IEnumerable<RoutingAddress> GetP2RecipientsForSafeRecipientsCheck(MailItem mailItem)
		{
			if (mailItem != null && mailItem.Message != null)
			{
				if (mailItem.Message.To != null)
				{
					foreach (EmailRecipient recipient in mailItem.Message.To)
					{
						if (!string.IsNullOrEmpty(recipient.SmtpAddress))
						{
							yield return (RoutingAddress)recipient.SmtpAddress;
						}
					}
				}
				if (mailItem.Message.Cc != null)
				{
					foreach (EmailRecipient recipient2 in mailItem.Message.Cc)
					{
						if (!string.IsNullOrEmpty(recipient2.SmtpAddress))
						{
							yield return (RoutingAddress)recipient2.SmtpAddress;
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000042EC File Offset: 0x000024EC
		private void InitializeSenderIdResultIsHardFail(MailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.senderIdResultIsHardFail = false;
			object obj;
			if (mailItem.Properties.TryGetValue("Microsoft.Exchange.SenderIdStatus", out obj))
			{
				this.senderIdResultIsHardFail = string.Equals(obj as string, ContentFilterAgent.SenderIdFailResult, StringComparison.OrdinalIgnoreCase);
				if (this.senderIdResultIsHardFail)
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Per-recipient safe sender checks on message {0} will be skipped because Sender ID returned a Fail result.", this.traceMessageId);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004360 File Offset: 0x00002560
		private void EndOfDataHandler(ReceiveMessageEventSource receiveMessageEventSource, EndOfDataEventArgs endOfDataEventArgs)
		{
			if (receiveMessageEventSource == null)
			{
				throw new ArgumentNullException("receiveMessageEventSource");
			}
			if (endOfDataEventArgs == null)
			{
				throw new ArgumentNullException("endOfDataEventArgs");
			}
			this.CacheMessageIdForTracing(endOfDataEventArgs.MailItem);
			this.InitializeSenderIdResultIsHardFail(endOfDataEventArgs.MailItem);
			if (this.IsPolicyDisabled(endOfDataEventArgs))
			{
				AgentLog.Instance.LogAcceptMessage(base.Name, base.EventTopic, endOfDataEventArgs, endOfDataEventArgs.SmtpSession, endOfDataEventArgs.MailItem, new LogEntry("SCL", "not available: policy is disabled."));
				return;
			}
			if (!this.IsScanNeeded(endOfDataEventArgs))
			{
				Util.PerformanceCounters.MessageNotScanned();
				AgentLog.Instance.LogAcceptMessage(base.Name, base.EventTopic, endOfDataEventArgs, endOfDataEventArgs.SmtpSession, endOfDataEventArgs.MailItem, new LogEntry("SCL", "not available: content filtering was bypassed."));
				return;
			}
			int scl;
			if (this.TryGetValidSCL(endOfDataEventArgs.MailItem, out scl))
			{
				Util.PerformanceCounters.PreExistingSCL();
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Message {0} has a pre-existing SCL and will NOT be rescanned.", this.traceMessageId);
				this.ActOnMessage(receiveMessageEventSource, endOfDataEventArgs, scl, "pre-existing SCL found on the message.");
				return;
			}
			if (this.agentFactory.IsDataCenterEnvironment)
			{
				ExTraceGlobals.ScanMessageTracer.TraceError<string, int>((long)this.GetHashCode(), "Message {0} has not been assigned an SCL in data center.  Using fall-back SCL of {1}", this.traceMessageId, 0);
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "SCLNotProvided");
				this.ActOnMessage(receiveMessageEventSource, endOfDataEventArgs, 0, "no SCL has been assigned by upstream server in data center. A fall-back SCL has been assigned to the message.");
				return;
			}
			this.ScanMessage(receiveMessageEventSource, endOfDataEventArgs);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000044AF File Offset: 0x000026AF
		private bool IsScanNeeded(EndOfDataEventArgs endOfDataEventArgs)
		{
			if (this.IsOnAllowList(endOfDataEventArgs) || this.IsSenderBypassed(endOfDataEventArgs) || this.HasAntispamBypassPermission(endOfDataEventArgs) || this.AreAllRecipientsBypassed(endOfDataEventArgs))
			{
				this.StampSCLHeader(endOfDataEventArgs.MailItem, -1);
				return false;
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000044E5 File Offset: 0x000026E5
		private bool IsPolicyDisabled(EndOfDataEventArgs endOfDataEventArgs)
		{
			if (!CommonUtils.IsEnabled(this.contentFilterConfig, endOfDataEventArgs.SmtpSession))
			{
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because policy is disabled on this connector", this.traceMessageId);
				return true;
			}
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000451C File Offset: 0x0000271C
		private bool IsOnAllowList(EndOfDataEventArgs endOfDataEventArgs)
		{
			object obj;
			if (endOfDataEventArgs.SmtpSession.Properties.TryGetValue("Microsoft.Exchange.IsOnAllowList", out obj) && obj is bool)
			{
				bool flag = (bool)obj;
				if (flag)
				{
					ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "IPOnAllowList");
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because the sender's IP address is on the IP Allow list", this.traceMessageId);
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004583 File Offset: 0x00002783
		private bool HasAntispamBypassPermission(EndOfDataEventArgs endOfDataEventArgs)
		{
			if (CommonUtils.HasAntispamBypassPermission(endOfDataEventArgs.SmtpSession, ExTraceGlobals.ScanMessageTracer, this) && endOfDataEventArgs.MailItem.FromAddress != RoutingAddress.NullReversePath)
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "MessageSecurityAntispamBypass");
				return true;
			}
			return false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000045C4 File Offset: 0x000027C4
		private bool IsSenderBypassed(EndOfDataEventArgs endOfDataEventArgs)
		{
			RoutingAddress senderForSafeSenderCheck = ContentFilterAgent.GetSenderForSafeSenderCheck(endOfDataEventArgs.MailItem);
			if (senderForSafeSenderCheck != RoutingAddress.Empty && this.bypassedSenders.IsBypassed(senderForSafeSenderCheck))
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "ContentFilterConfigBypassedSender");
				Util.PerformanceCounters.MessageNotScannedDueToOrgSafeSender();
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because the sender (or the sender domain) is bypassed", this.traceMessageId);
				return true;
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000462C File Offset: 0x0000282C
		private bool AreAllRecipientsBypassed(EndOfDataEventArgs endOfDataEventArgs)
		{
			MailItem mailItem = endOfDataEventArgs.MailItem;
			RoutingAddress senderForSafeSenderCheck = ContentFilterAgent.GetSenderForSafeSenderCheck(mailItem);
			IEnumerable<RoutingAddress> p2RecipientsForSafeRecipientsCheck = ContentFilterAgent.GetP2RecipientsForSafeRecipientsCheck(mailItem);
			ReadOnlyCollection<AddressBookEntry> readOnlyCollection = null;
			int count = mailItem.Recipients.Count;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			if (this.bypassedRecipients.AddressBook != null)
			{
				CommonUtils.TryAddressBookFind(this.bypassedRecipients.AddressBook, mailItem.Recipients, out readOnlyCollection);
			}
			for (int i = 0; i < mailItem.Recipients.Count; i++)
			{
				EnvelopeRecipient envelopeRecipient = mailItem.Recipients[i];
				AddressBookEntry addressBookEntry = (readOnlyCollection != null) ? readOnlyCollection[i] : null;
				bool flag = false;
				if (this.IsSafeSender(envelopeRecipient, addressBookEntry, senderForSafeSenderCheck))
				{
					num2++;
					Util.PerformanceCounters.BypassedRecipientDueToPerRecipientSafeSender();
				}
				else if (this.IsSafeRecipient(envelopeRecipient, addressBookEntry, p2RecipientsForSafeRecipientsCheck))
				{
					num3++;
					Util.PerformanceCounters.BypassedRecipientDueToPerRecipientSafeRecipient();
				}
				else if (!flag && this.bypassedRecipients.IsBypassed(envelopeRecipient.Address, addressBookEntry))
				{
					num++;
				}
			}
			bool flag2 = num + num2 + num3 == count;
			if (num2 == count)
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "SenderOnRecipientSafeSendersList");
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because the sender is on the envelope recipient's safe senders list", this.traceMessageId);
			}
			else if (num3 == count)
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "P2RecipientOnSafeRecipientsList");
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because one of the P2 recipients is on the envelope recipient's safe recipients list", this.traceMessageId);
			}
			else if (num == count)
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "ContentFilterConfigBypassedRecipient");
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because all recipients are bypassed", this.traceMessageId);
			}
			else if (flag2)
			{
				ContentFilterAgent.StampAntiSpamReport(endOfDataEventArgs.MailItem, "SenderOnRecipientSafeSendersList Or P2RecipientOnSafeRecipientsList Or ContentFilterConfigBypassedRecipient");
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Not scanning message {0} because some recipients are bypassed, or have the sender on the safe senders list, or one of the P2 recipients is in the safe recipients list.", this.traceMessageId);
			}
			return flag2;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004810 File Offset: 0x00002A10
		private void ScanMessage(ReceiveMessageEventSource receiveMessageEventSource, EndOfDataEventArgs endOfDataEventArgs)
		{
			ComArguments comArguments = new ComArguments();
			ContentFilterAgent.SetComArgumentsProperty(comArguments, 8, endOfDataEventArgs.MailItem, "Microsoft.Exchange.PRD", string.Empty);
			ContentFilterAgent.SetComArgumentsProperty(comArguments, 9, endOfDataEventArgs.MailItem, "Microsoft.Exchange.SenderIdStatus", SenderIdStatus.None.ToString());
			this.SerializeRecipients(endOfDataEventArgs.MailItem, comArguments);
			ContentFilterAgent.AsyncState state = new ContentFilterAgent.AsyncState(receiveMessageEventSource, endOfDataEventArgs, comArguments, base.GetAgentAsyncContext());
			ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Scan request for message {0} is going async. at BeginScanMessage()...", this.traceMessageId);
			this.agentFactory.BeginScanMessage(new AsyncCallback(this.ScanMessageCallback), state);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000048AC File Offset: 0x00002AAC
		private void ScanMessageCallback(IAsyncResult asyncResult)
		{
			ContentFilterAgent.AsyncState asyncState = (ContentFilterAgent.AsyncState)asyncResult.AsyncState;
			asyncState.MexContext.Resume();
			EndOfDataEventArgs endOfDataEventArgs = asyncState.EndOfDataEventArgs;
			ReceiveMessageEventSource receiveMessageEventSource = asyncState.ReceiveMessageEventSource;
			ComArguments comArguments = asyncState.ComArguments;
			ScanMessageResult scanMessageResult = ScanMessageResult.Error;
			ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Entered callback of scan request for message {0}", this.traceMessageId);
			try
			{
				try
				{
					scanMessageResult = this.agentFactory.EndScanMessage(asyncResult);
				}
				catch (COMException arg)
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string, COMException>((long)this.GetHashCode(), "Failed to submit message {0} to the wrapper. Details: {1}", this.traceMessageId, arg);
				}
				bool flag = false;
				LogEntry logEntry = ContentFilterAgent.RejectContext.TemporaryFailure(string.Empty);
				ScanMessageResult scanMessageResult2 = scanMessageResult;
				switch (scanMessageResult2)
				{
				case ScanMessageResult.Error:
					ExTraceGlobals.ScanMessageTracer.TraceError<string>((long)this.GetHashCode(), "Error when reading result from ExLapi property bag, returning 400-level error for message {0}.", this.traceMessageId);
					flag = true;
					logEntry = ContentFilterAgent.RejectContext.TemporaryFailure("error reading ExLapi property bag");
					goto IL_2AF;
				case ScanMessageResult.OK:
					this.ProcessScannedMessage(asyncState);
					goto IL_2AF;
				case ScanMessageResult.Pending:
					break;
				case ScanMessageResult.FilterNotInitialized:
					ExTraceGlobals.ScanMessageTracer.TraceError<string>((long)this.GetHashCode(), "Filter was created but not initialized, returning 400-level error for message {0}", this.traceMessageId);
					flag = true;
					logEntry = ContentFilterAgent.RejectContext.TemporaryFailure("filter not initialized");
					goto IL_2AF;
				case ScanMessageResult.UnableToProcessMessage:
				{
					string text = "<unknown>";
					string text2 = "<unknown>";
					int num = 0;
					Util.PerformanceCounters.FilterFailure();
					if (comArguments[19] != null)
					{
						text = ((Constants.FailureFunctionIDs)comArguments.GetInt32(19)).ToString();
					}
					if (comArguments[18] != null)
					{
						num = comArguments.GetInt32(18);
						Exception exceptionForHR = Marshal.GetExceptionForHR(num);
						text2 = exceptionForHR.ToString();
					}
					if (Util.IsUnexpectedMessageFailure((uint)num))
					{
						ExTraceGlobals.ScanMessageTracer.TraceError<string, string, string>((long)this.GetHashCode(), "SmartScreen returned an unexpected failure for message {0} at the call to {1}: {2}", this.traceMessageId, text, text2);
						Util.LogUnexpectedFailureScanningMessage(this.traceMessageId, num, text2);
					}
					AgentLog.Instance.LogAcceptMessage(base.Name, base.EventTopic, endOfDataEventArgs, endOfDataEventArgs.SmtpSession, endOfDataEventArgs.MailItem, new LogEntry("SCL", "not available: filter unable to process message. Failure at function ID " + text + " : " + text2));
					ExTraceGlobals.ScanMessageTracer.TraceError<string, string, string>((long)this.GetHashCode(), "Message {0} has been accepted WITHOUT being assigned an SCL because the filter failed to process the message. Failure at function ID {1}: {2}", this.traceMessageId, text, text2);
					flag = false;
					goto IL_2AF;
				}
				default:
					switch (scanMessageResult2)
					{
					case (ScanMessageResult)4294967294U:
						ExTraceGlobals.ScanMessageTracer.TraceError<string>((long)this.GetHashCode(), "Message {0} could not be submitted to the filter and timed out, returning 400-level error.", this.traceMessageId);
						flag = true;
						logEntry = ContentFilterAgent.RejectContext.TemporaryFailure("timed out");
						goto IL_2AF;
					case (ScanMessageResult)4294967295U:
						ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Message {0} has been aborted due to shutdown, returning 400-level error.", this.traceMessageId);
						flag = true;
						logEntry = ContentFilterAgent.RejectContext.TemporaryFailure("service is shutting down");
						goto IL_2AF;
					}
					break;
				}
				ExTraceGlobals.ScanMessageTracer.TraceError<ScanMessageResult, string>((long)this.GetHashCode(), "Got an invalid result from ContentFilterWrapper: {0} for message {1}", scanMessageResult, this.traceMessageId);
				flag = true;
				logEntry = ContentFilterAgent.RejectContext.TemporaryFailure("ContentFilterWrapper returned an unknown value for ScanMessageResult");
				IL_2AF:
				if (flag)
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Rejecting message {0} with 400-level error", this.traceMessageId);
					AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, endOfDataEventArgs, endOfDataEventArgs.SmtpSession, endOfDataEventArgs.MailItem, SmtpResponse.DataTransactionFailed, logEntry);
					receiveMessageEventSource.RejectMessage(SmtpResponse.DataTransactionFailed);
				}
			}
			finally
			{
				asyncState.MexContext.Complete();
			}
			ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Exiting callback of scan request for message {0}", this.traceMessageId);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004C1C File Offset: 0x00002E1C
		private void ProcessScannedMessage(object state)
		{
			ContentFilterAgent.AsyncState asyncState = (ContentFilterAgent.AsyncState)state;
			ComArguments comArguments = asyncState.ComArguments;
			ReceiveMessageEventSource receiveMessageEventSource = asyncState.ReceiveMessageEventSource;
			EndOfDataEventArgs endOfDataEventArgs = asyncState.EndOfDataEventArgs;
			if (receiveMessageEventSource == null)
			{
				throw new ArgumentNullException("Event source is null in callback");
			}
			if (endOfDataEventArgs == null)
			{
				throw new ArgumentNullException("Event args is null in callback");
			}
			Util.PerformanceCounters.MessageScanned();
			ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "SmartScreen has successfully scanned message {0}", this.traceMessageId);
			int @int = comArguments.GetInt32(4);
			int int2 = comArguments.GetInt32(5);
			int int3 = comArguments.GetInt32(15);
			string @string = comArguments.GetString(14);
			if (this.contentFilterConfig.OutlookEmailPostmarkValidationEnabled && comArguments[16] != null)
			{
				this.IncrementPostmarkPerfCounter((ContentFilterAgent.PostmarkResult)comArguments.GetInt32(16));
			}
			this.StampHeaders(endOfDataEventArgs, @int, int2, int3, @string);
			this.ActOnMessage(receiveMessageEventSource, endOfDataEventArgs, @int, @string);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004CE8 File Offset: 0x00002EE8
		private void StampHeaders(EndOfDataEventArgs endOfDataEventArgs, int scl, int unmodifiedScl, int pcl, string diagnostics)
		{
			MailItem mailItem = endOfDataEventArgs.MailItem;
			this.StampSCLHeader(mailItem, scl);
			mailItem.Properties["Microsoft.Exchange.UnmodifiedSCL"] = unmodifiedScl;
			Header header = Header.Create("X-MS-Exchange-Organization-PCL");
			header.Value = pcl.ToString(CultureInfo.InvariantCulture);
			mailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			IPAddress lastExternalIPAddress = endOfDataEventArgs.SmtpSession.LastExternalIPAddress;
			diagnostics = diagnostics + ";OrigIP:" + ((lastExternalIPAddress != null) ? lastExternalIPAddress.ToString() : "unavailable");
			ContentFilterAgent.StampAntiSpamReport(mailItem, diagnostics);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004D88 File Offset: 0x00002F88
		private void StampSCLHeader(MailItem mailItem, int scl)
		{
			int num;
			if (!this.TryGetValidSCL(mailItem, out num))
			{
				Header header = Header.Create("X-MS-Exchange-Organization-SCL");
				header.Value = scl.ToString(CultureInfo.InvariantCulture);
				mailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004DDC File Offset: 0x00002FDC
		private void ActOnMessage(ReceiveMessageEventSource source, EndOfDataEventArgs e, int scl, string diagnostics)
		{
			if (e.MailItem == null || e.MailItem.Recipients == null)
			{
				ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Either MailItem or MailItem.Recipients is null in ActOnMessage().");
				return;
			}
			bool flag = true;
			if (scl != -1)
			{
				int count = e.MailItem.Recipients.Count;
				int num = 0;
				List<EnvelopeRecipient> list = new List<EnvelopeRecipient>(count);
				List<EnvelopeRecipient> list2 = new List<EnvelopeRecipient>(count);
				List<EnvelopeRecipient> list3 = new List<EnvelopeRecipient>(count);
				RoutingAddress senderForSafeSenderCheck = ContentFilterAgent.GetSenderForSafeSenderCheck(e.MailItem);
				IEnumerable<RoutingAddress> p2RecipientsForSafeRecipientsCheck = ContentFilterAgent.GetP2RecipientsForSafeRecipientsCheck(e.MailItem);
				ReadOnlyCollection<AddressBookEntry> readOnlyCollection = null;
				Util.PerformanceCounters.MessageIsAtSCL(scl);
				AddressBookFindStatus addressBookFindStatus;
				if (this.bypassedRecipients.AddressBook != null && !CommonUtils.TryAddressBookFind(this.bypassedRecipients.AddressBook, e.MailItem.Recipients, out readOnlyCollection, out addressBookFindStatus) && addressBookFindStatus == AddressBookFindStatus.TransientFailure)
				{
					string formatString = "Could not look up address book entries for message {0} due to a transient error.";
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), formatString, this.traceMessageId);
					AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, SmtpResponse.DataTransactionFailed, ContentFilterAgent.RejectContext.TransientFailure);
					source.RejectMessage(SmtpResponse.DataTransactionFailed);
					return;
				}
				for (int i = 0; i < count; i++)
				{
					EnvelopeRecipient envelopeRecipient = e.MailItem.Recipients[i];
					AddressBookEntry addressBookEntry = (readOnlyCollection != null) ? readOnlyCollection[i] : null;
					ContentFilterAgent.RecipientActionType recipientActionType = ContentFilterAgent.RecipientActionType.Bypass;
					if (!this.IsSafeSender(envelopeRecipient, addressBookEntry, senderForSafeSenderCheck) && !this.bypassedRecipients.IsBypassed(envelopeRecipient.Address, addressBookEntry) && !this.IsSafeRecipient(envelopeRecipient, addressBookEntry, p2RecipientsForSafeRecipientsCheck))
					{
						recipientActionType = this.GetAction(addressBookEntry, scl);
					}
					switch (recipientActionType)
					{
					case ContentFilterAgent.RecipientActionType.Bypass:
						num++;
						break;
					case ContentFilterAgent.RecipientActionType.Delete:
						list.Add(envelopeRecipient);
						break;
					case ContentFilterAgent.RecipientActionType.Reject:
						list2.Add(envelopeRecipient);
						break;
					case ContentFilterAgent.RecipientActionType.Quarantine:
						list3.Add(envelopeRecipient);
						break;
					default:
						ExTraceGlobals.ScanMessageTracer.TraceError<ContentFilterAgent.RecipientActionType>((long)this.GetHashCode(), "GetAction() returned an unknown action: {0}.", recipientActionType);
						throw new InvalidOperationException("GetAction() returned an unknown action: " + recipientActionType);
					}
				}
				if (num + list.Count + list2.Count + list3.Count != count)
				{
					throw new InvalidOperationException("Missed one or more recipients.");
				}
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Message {0} has {1} recipient(s); Bypassed: {2}; Deleted: {3}; Rejected: {4}: Quarantined: {5}", new object[]
				{
					this.traceMessageId,
					count,
					num,
					list.Count,
					list2.Count,
					list3.Count
				});
				if (num < count)
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Some recipients in message {0} were not bypassed.", this.traceMessageId);
					if (list.Count > 0)
					{
						LogEntry logEntry = ContentFilterAgent.RejectContext.SclAtOrAboveDeleteThreshold(scl, diagnostics);
						if (list.Count == count)
						{
							ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Silently deleting entire message {0}", this.traceMessageId);
							SmtpResponse response = SmtpResponse.QueuedMailForDelivery(e.MailItem.Message.MessageId);
							AgentLog.Instance.LogDeleteMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, logEntry);
							source.RejectMessage(response);
							Util.PerformanceCounters.MessageDeleted();
							return;
						}
						ExTraceGlobals.ScanMessageTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Silently deleting {0} recipients from message {1}", list.Count, this.traceMessageId);
						AgentLog.Instance.LogDeleteRecipients(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, list, logEntry);
						foreach (EnvelopeRecipient recipient in list)
						{
							e.MailItem.Recipients.Remove(recipient);
						}
					}
					if (list2.Count > 0)
					{
						LogEntry logEntry2 = ContentFilterAgent.RejectContext.SclAtOrAboveRejectThreshold(scl, diagnostics);
						SmtpResponse rejectionResponse = this.GetRejectionResponse();
						if (list2.Count == count)
						{
							ExTraceGlobals.ScanMessageTracer.TraceDebug<string, SmtpResponse>((long)this.GetHashCode(), "Rejecting message {0} with response '{1}'", this.traceMessageId, rejectionResponse);
							AgentLog.Instance.LogRejectMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, rejectionResponse, logEntry2);
							source.RejectMessage(rejectionResponse);
							flag = false;
						}
						else
						{
							ExTraceGlobals.ScanMessageTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Removing and NDR'ing {0} recipients from message {1}", list2.Count, this.traceMessageId);
							AgentLog.Instance.LogRejectRecipients(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, list2, rejectionResponse, logEntry2);
							foreach (EnvelopeRecipient recipient2 in list2)
							{
								e.MailItem.Recipients.Remove(recipient2, DsnType.Failure, rejectionResponse);
							}
						}
						Util.PerformanceCounters.MessageRejected();
					}
					if (list3.Count > 0)
					{
						AgentAction action = (list3.Count == count) ? AgentAction.QuarantineMessage : AgentAction.QuarantineRecipients;
						AgentLog.Instance.LogQuarantineAction(base.Name, base.EventTopic, e, action, list3, ContentFilterAgent.QuarantineSmtpResponse, ContentFilterAgent.RejectContext.SclAtOrAboveQuarantineThreshold(scl, diagnostics));
						if (list3.Count == count)
						{
							ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Quarantining message {0} for all recipients.", this.traceMessageId);
							source.Quarantine(null, "Content Filter agent quarantined this message");
							flag = false;
						}
						else
						{
							ExTraceGlobals.ScanMessageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Quarantining message {0} for {1} recipient(s)", this.traceMessageId, list3.Count);
							source.Quarantine(list3, "Content Filter agent quarantined this message");
						}
						Util.PerformanceCounters.MessageQuarantined();
					}
				}
				else
				{
					ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "All recipients on message {0} were bypassed", this.traceMessageId);
				}
			}
			if (flag)
			{
				AgentLog.Instance.LogAcceptMessage(base.Name, base.EventTopic, e, e.SmtpSession, e.MailItem, new LogEntry("SCL", scl));
				ExTraceGlobals.ScanMessageTracer.TraceDebug<string>((long)this.GetHashCode(), "Message {0} has been accepted", this.traceMessageId);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000053F8 File Offset: 0x000035F8
		private ContentFilterAgent.RecipientActionType GetAction(AddressBookEntry addressBookEntry, int messageSCL)
		{
			ContentFilterAgent.RecipientActionType result = ContentFilterAgent.RecipientActionType.Bypass;
			int num = this.contentFilterConfig.SCLDeleteEnabled ? this.contentFilterConfig.SCLDeleteThreshold : int.MaxValue;
			int num2 = this.contentFilterConfig.SCLRejectEnabled ? this.contentFilterConfig.SCLRejectThreshold : int.MaxValue;
			int num3 = this.contentFilterConfig.SCLQuarantineEnabled ? this.contentFilterConfig.SCLQuarantineThreshold : int.MaxValue;
			if (addressBookEntry != null)
			{
				num = addressBookEntry.GetSpamConfidenceLevelThreshold(SpamAction.Delete, num);
				num2 = addressBookEntry.GetSpamConfidenceLevelThreshold(SpamAction.Reject, num2);
				num3 = addressBookEntry.GetSpamConfidenceLevelThreshold(SpamAction.Quarantine, num3);
			}
			if (messageSCL >= num)
			{
				result = ContentFilterAgent.RecipientActionType.Delete;
			}
			else if (messageSCL >= num2)
			{
				result = ContentFilterAgent.RecipientActionType.Reject;
			}
			else if (messageSCL >= num3)
			{
				if (this.quarantineMailboxIsValid)
				{
					result = ContentFilterAgent.RecipientActionType.Quarantine;
				}
				else
				{
					result = ContentFilterAgent.RecipientActionType.Bypass;
					ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Action is Quarantine, but Quarantine Mailbox is invalid.");
					Util.LogQuarantineMailboxIsInvalid();
				}
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000054C3 File Offset: 0x000036C3
		private bool TryGetValidSCL(MailItem mailItem, out int scl)
		{
			return CommonUtils.TryGetValidScl(mailItem.Message.MimeDocument.RootPart.Headers, out scl);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000054E0 File Offset: 0x000036E0
		private bool IsSafeSender(EnvelopeRecipient recipient, AddressBookEntry recipientAddressBookEntry, RoutingAddress sender)
		{
			if (this.senderIdResultIsHardFail)
			{
				return false;
			}
			bool flag = recipientAddressBookEntry != null && recipientAddressBookEntry.IsSafeSender(sender);
			if (flag)
			{
				ExTraceGlobals.ScanMessageTracer.TraceDebug<RoutingAddress, EnvelopeRecipient>((long)this.GetHashCode(), "Sender {0} is listed in {1}'s safe senders list.", sender, recipient);
			}
			return flag;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00005524 File Offset: 0x00003724
		private bool IsSafeRecipient(EnvelopeRecipient recipient, AddressBookEntry recipientAddressBookEntry, IEnumerable<RoutingAddress> p2Recipients)
		{
			if (this.senderIdResultIsHardFail)
			{
				return false;
			}
			bool flag = recipientAddressBookEntry != null && CommonUtils.IsSafeRecipient(recipientAddressBookEntry, p2Recipients);
			if (flag)
			{
				ExTraceGlobals.ScanMessageTracer.TraceDebug<EnvelopeRecipient>((long)this.GetHashCode(), "One of the P2 recipients is a safe recipient of recipient {0}.", recipient);
			}
			return flag;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00005564 File Offset: 0x00003764
		private void SerializeRecipients(MailItem mailItem, ComArguments comArguments)
		{
			byte[] array3;
			if (this.contentFilterConfig.OutlookEmailPostmarkValidationEnabled && ContentFilterAgent.ContainsPuzzle(mailItem))
			{
				byte[][] array = new byte[mailItem.Recipients.Count][];
				byte[][] array2 = new byte[mailItem.Recipients.Count][];
				int num = 0;
				int num2 = 0;
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					string s = envelopeRecipient.Address.ToString();
					array[num] = Encoding.Unicode.GetBytes(s);
					array2[num] = BitConverter.GetBytes(array[num].Length);
					num2 += array[num].Length + array2[num].Length;
					num++;
				}
				array3 = Util.SerializeByteArrays(num2, new byte[][][]
				{
					array2,
					array
				});
			}
			else
			{
				array3 = new byte[0];
			}
			comArguments[12] = BitConverter.GetBytes(array3.Length);
			comArguments[13] = array3;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000567C File Offset: 0x0000387C
		private void IncrementPostmarkPerfCounter(ContentFilterAgent.PostmarkResult postmarkResult)
		{
			switch (postmarkResult)
			{
			case ContentFilterAgent.PostmarkResult.PostmarkNotFound:
				ExTraceGlobals.ScanMessageTracer.TraceDebug((long)this.GetHashCode(), "Not incrementing perf. counter because the mail item doesn't not contain a postmark.");
				return;
			case ContentFilterAgent.PostmarkResult.Valid:
				Util.PerformanceCounters.MessagesWithValidPostmarks();
				return;
			case ContentFilterAgent.PostmarkResult.Invalid:
				Util.PerformanceCounters.MessagesWithInvalidPostmarks();
				return;
			default:
				ExTraceGlobals.ScanMessageTracer.TraceError((long)this.GetHashCode(), "Not incrementing perf. counter because the postmark result is invalid.");
				return;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000056D8 File Offset: 0x000038D8
		private SmtpResponse GetRejectionResponse()
		{
			return new SmtpResponse("550", "5.7.1", new string[]
			{
				this.contentFilterConfig.RejectionResponse
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000570F File Offset: 0x0000390F
		private void CacheMessageIdForTracing(MailItem mailItem)
		{
			this.traceMessageId = ((mailItem != null && mailItem.Message != null && !string.IsNullOrEmpty(mailItem.Message.MessageId)) ? mailItem.Message.MessageId : "<unavailable>");
		}

		// Token: 0x04000079 RID: 121
		private const int AntiSpamReportMaxLength = 1024;

		// Token: 0x0400007A RID: 122
		private const string PuzzleValidationHeader = "x-cr-hashedpuzzle";

		// Token: 0x0400007B RID: 123
		private const string QuarantineReason = "Content Filter agent quarantined this message";

		// Token: 0x0400007C RID: 124
		private const int BypassingSCL = -1;

		// Token: 0x0400007D RID: 125
		private const string MessageHasPreexistingSCL = "pre-existing SCL found on the message.";

		// Token: 0x0400007E RID: 126
		private const int FallbackSclForDataCenterEnvironment = 0;

		// Token: 0x0400007F RID: 127
		private const string MessageHasNoSclInDataCenter = "no SCL has been assigned by upstream server in data center. A fall-back SCL has been assigned to the message.";

		// Token: 0x04000080 RID: 128
		private static readonly SmtpResponse QuarantineSmtpResponse = new SmtpResponse("550", "5.2.1", new string[]
		{
			"Content Filter agent quarantined this message"
		});

		// Token: 0x04000081 RID: 129
		private static readonly string SenderIdFailResult = SenderIdStatus.Fail.ToString();

		// Token: 0x04000082 RID: 130
		private ContentFilterAgentFactory agentFactory;

		// Token: 0x04000083 RID: 131
		private ContentFilterConfig contentFilterConfig;

		// Token: 0x04000084 RID: 132
		private BypassedRecipients bypassedRecipients;

		// Token: 0x04000085 RID: 133
		private bool quarantineMailboxIsValid;

		// Token: 0x04000086 RID: 134
		private BypassedSenders bypassedSenders;

		// Token: 0x04000087 RID: 135
		private string traceMessageId;

		// Token: 0x04000088 RID: 136
		private bool senderIdResultIsHardFail;

		// Token: 0x02000013 RID: 19
		private enum RecipientActionType
		{
			// Token: 0x0400008A RID: 138
			Bypass = 1,
			// Token: 0x0400008B RID: 139
			Delete,
			// Token: 0x0400008C RID: 140
			Reject,
			// Token: 0x0400008D RID: 141
			Quarantine
		}

		// Token: 0x02000014 RID: 20
		private enum PostmarkResult
		{
			// Token: 0x0400008F RID: 143
			PostmarkNotFound,
			// Token: 0x04000090 RID: 144
			Valid,
			// Token: 0x04000091 RID: 145
			Invalid
		}

		// Token: 0x02000015 RID: 21
		internal class AsyncState
		{
			// Token: 0x06000050 RID: 80 RVA: 0x00005789 File Offset: 0x00003989
			internal AsyncState(ReceiveMessageEventSource receiveMessageEventSource, EndOfDataEventArgs endOfDataEventArgs, ComArguments comArguments, AgentAsyncContext mexContext)
			{
				this.ReceiveMessageEventSource = receiveMessageEventSource;
				this.EndOfDataEventArgs = endOfDataEventArgs;
				this.ComArguments = comArguments;
				this.MexContext = mexContext;
			}

			// Token: 0x04000092 RID: 146
			internal readonly ReceiveMessageEventSource ReceiveMessageEventSource;

			// Token: 0x04000093 RID: 147
			internal readonly EndOfDataEventArgs EndOfDataEventArgs;

			// Token: 0x04000094 RID: 148
			internal readonly AgentAsyncContext MexContext;

			// Token: 0x04000095 RID: 149
			internal readonly ComArguments ComArguments;
		}

		// Token: 0x02000016 RID: 22
		private static class RejectContext
		{
			// Token: 0x06000051 RID: 81 RVA: 0x000057AE File Offset: 0x000039AE
			public static LogEntry TemporaryFailure(string reasonData)
			{
				return new LogEntry("UnableToScanMessage", reasonData);
			}

			// Token: 0x06000052 RID: 82 RVA: 0x000057BB File Offset: 0x000039BB
			public static LogEntry SclAtOrAboveRejectThreshold(int scl, string diagnostics)
			{
				return new LogEntry("SclAtOrAboveRejectThreshold", scl.ToString(CultureInfo.InvariantCulture), diagnostics);
			}

			// Token: 0x06000053 RID: 83 RVA: 0x000057D4 File Offset: 0x000039D4
			public static LogEntry SclAtOrAboveDeleteThreshold(int scl, string diagnostics)
			{
				return new LogEntry("SclAtOrAboveDeleteThreshold", scl.ToString(CultureInfo.InvariantCulture), diagnostics);
			}

			// Token: 0x06000054 RID: 84 RVA: 0x000057ED File Offset: 0x000039ED
			public static LogEntry SclAtOrAboveQuarantineThreshold(int scl, string diagnostics)
			{
				return new LogEntry("SclAtOrAboveQuarantineThreshold", scl.ToString(CultureInfo.InvariantCulture), diagnostics);
			}

			// Token: 0x04000096 RID: 150
			public static readonly LogEntry TransientFailure = new LogEntry("TransientFailure", string.Empty);
		}
	}
}
