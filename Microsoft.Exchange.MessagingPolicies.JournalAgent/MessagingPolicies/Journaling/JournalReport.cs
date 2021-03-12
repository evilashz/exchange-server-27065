using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000012 RID: 18
	internal sealed class JournalReport
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00008241 File Offset: 0x00006441
		private JournalReport(Configuration currentConfiguration, MailItem originalMailItem)
		{
			this.currentConfiguration = currentConfiguration;
			this.originalMailItem = originalMailItem;
			this.globalHistory = TransportFacades.ReadHistoryFrom(((ITransportMailItemWrapperFacade)this.originalMailItem).TransportMailItem);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00008274 File Offset: 0x00006474
		public static ITransportMailItemFacade CreateReport(Configuration configuration, MailItem originalMailItem, List<string> journalRecipsSorted, List<string> remoteAccounts, bool gccReport, bool fullReport, List<string> ruleNames, List<LegacyRecipientRecord> legacyRecords)
		{
			ITransportMailItemFacade transportMailItemFacade = null;
			transportMailItemFacade = TransportFacades.NewMailItem(((ITransportMailItemWrapperFacade)originalMailItem).TransportMailItem);
			transportMailItemFacade.FallbackToRawOverride = true;
			if (gccReport)
			{
				transportMailItemFacade.From = configuration.JournalReportNdrToForGcc;
				JournalReport.SetJournalItemTenantIdToSpecialTenant(originalMailItem, (TransportMailItem)transportMailItemFacade);
			}
			else
			{
				transportMailItemFacade.From = configuration.JournalReportNdrTo;
			}
			string[] remoteAccountGuids = null;
			JournalReport.AddRemoteAccountRecipients(remoteAccounts, ref journalRecipsSorted, out remoteAccountGuids);
			foreach (string smtpAddress in journalRecipsSorted)
			{
				transportMailItemFacade.Recipients.Add(smtpAddress);
			}
			JournalReport.BccToReconciliationMailboxes(configuration, transportMailItemFacade, remoteAccountGuids);
			JournalReport journalReport = new JournalReport(configuration, originalMailItem);
			journalReport.CreateMessage(transportMailItemFacade.Message, remoteAccounts, gccReport, fullReport, ruleNames, legacyRecords);
			transportMailItemFacade.ReceiveConnectorName = "Journaling";
			transportMailItemFacade.PrepareJournalReport();
			JournalReport.PromoteProperties((TransportMailItem)transportMailItemFacade, originalMailItem);
			if (!gccReport)
			{
				JournalReport.MarkOriginalMailItemForNdr(originalMailItem, journalRecipsSorted);
			}
			TransportFacades.EnsureSecurityAttributes(transportMailItemFacade);
			transportMailItemFacade.Priority = DeliveryPriority.Low;
			Header header = originalMailItem.Message.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Organization-OriginalSize");
			if (header != null)
			{
				long num = 0L;
				long.TryParse(header.Value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num);
				if (num > 0L)
				{
					transportMailItemFacade.Message.MimeDocument.RootPart.Headers.AppendChild(new AsciiTextHeader("X-MS-Exchange-Organization-OriginalSize", header.Value));
				}
			}
			return transportMailItemFacade;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000083E8 File Offset: 0x000065E8
		public void CreateMessage(EmailMessage journalMessage, List<string> remoteAccounts, bool gccReport, bool fullReport, List<string> ruleNames, List<LegacyRecipientRecord> legacyRecords)
		{
			EmailMessage message = this.originalMailItem.Message;
			journalMessage.Sender = new EmailRecipient(null, this.currentConfiguration.MSExchangeRecipient.ToString());
			if (message.RootPart != null && journalMessage.RootPart != null)
			{
				Header header;
				for (header = message.RootPart.Headers.FindFirst(HeaderId.From); header != null; header = message.RootPart.Headers.FindNext(header))
				{
					journalMessage.RootPart.Headers.InsertAfter(header.Clone(), null);
				}
				for (header = message.RootPart.Headers.FindFirst(HeaderId.To); header != null; header = message.RootPart.Headers.FindNext(header))
				{
					journalMessage.RootPart.Headers.InsertAfter(header.Clone(), null);
				}
				for (header = message.RootPart.Headers.FindFirst(HeaderId.Cc); header != null; header = message.RootPart.Headers.FindNext(header))
				{
					journalMessage.RootPart.Headers.InsertAfter(header.Clone(), null);
				}
				header = message.RootPart.Headers.FindFirst(HeaderId.Subject);
				if (header != null)
				{
					journalMessage.RootPart.Headers.RemoveAll(HeaderId.Subject);
					journalMessage.RootPart.Headers.InsertAfter(header.Clone(), null);
				}
				MimeInternalHelpers.CopyHeaderBetweenList(message.RootPart.Headers, journalMessage.RootPart.Headers, "X-MS-Exchange-Transport-Rules-Loop", true);
				MimeInternalHelpers.CopyHeaderBetweenList(message.RootPart.Headers, journalMessage.RootPart.Headers, "X-MS-Exchange-Moderation-Loop");
			}
			if (remoteAccounts != null && remoteAccounts.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder(remoteAccounts.Count * 53);
				foreach (string value in remoteAccounts)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(value);
				}
				TextHeader newChild = new TextHeader("X-MS-Exchange-Organization-Journaling-Remote-Accounts", stringBuilder.ToString());
				journalMessage.RootPart.Headers.AppendChild(newChild);
			}
			Header header2 = Header.Create(HeaderId.MessageId);
			header2.Value = string.Format("<{0}@{1}>", Guid.NewGuid(), "journal.report.generator");
			journalMessage.MimeDocument.RootPart.Headers.AppendChild(header2);
			journalMessage.Date = DateTime.UtcNow;
			if (gccReport)
			{
				Header newChild2 = new TextHeader("X-MS-Gcc-Journal-Report", null);
				journalMessage.MimeDocument.RootPart.Headers.AppendChild(newChild2);
			}
			int arg = 0;
			Charset charset = JournalReport.DetermineBestCharsetToEncodeSubject(message, ruleNames, out arg);
			Encoding encoding = charset.GetEncoding();
			this.SetCharsetName(journalMessage, charset.Name);
			ExTraceGlobals.JournalingTracer.TraceDebug<int, string>((long)this.GetHashCode(), "Encoding message using, Codepage: {0}, Charset: {1}", arg, charset.Name);
			this.WriteTo(journalMessage, encoding, gccReport, fullReport, ruleNames, legacyRecords);
			if (!gccReport || fullReport)
			{
				Attachment attachment = journalMessage.Attachments.Add(null, "message/rfc822");
				attachment.EmbeddedMessage = this.originalMailItem.Message;
				AddressHeader xbccHeader = JournalReport.GetXBccHeader(this.originalMailItem.Message);
				JournalReport.AddBccRecipients(attachment.EmbeddedMessage, xbccHeader);
				JournalReport.TryStampMissingCharset(message.Body.CharsetName, attachment.EmbeddedMessage);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008748 File Offset: 0x00006948
		private static bool IsEqualRecipient(EmailRecipient recip1, EmailRecipient recip2)
		{
			return (recip1 == null && recip2 == null) || (!(recip1 == null ^ recip2 == null) && recip1.SmtpAddress == recip2.SmtpAddress);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008770 File Offset: 0x00006970
		private static string GetUserAddress(EmailRecipient recipient)
		{
			if (recipient == null || recipient.SmtpAddress == string.Empty)
			{
				return "Unspecified Address";
			}
			return recipient.SmtpAddress.ToString();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00008798 File Offset: 0x00006998
		private static void AppendHistoryRecord(StringBuilder outBuffer, IHistoryRecordFacade record)
		{
			switch (record.Type)
			{
			case HistoryType.Expanded:
				outBuffer.Append(", Expanded: ");
				break;
			case HistoryType.Forwarded:
			case HistoryType.DeliveredAndForwarded:
				outBuffer.Append(", Forwarded: ");
				break;
			}
			outBuffer.Append(record.Address.ToString());
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000087F8 File Offset: 0x000069F8
		private static void PromoteProperties(TransportMailItem journalReportItem, MailItem originalMailItem)
		{
			object obj = null;
			originalMailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", out obj);
			string value = (string)obj;
			if (!string.IsNullOrEmpty(value))
			{
				journalReportItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.RightsManagement.TransportDecryptionUL", value);
			}
			obj = null;
			string value2;
			if (!originalMailItem.Properties.TryGetValue("Microsoft.Exchange.RightsManagement.DecryptionTokenRecipient", out obj))
			{
				value2 = MPCommonUtils.GetDecryptionTokenRecipient(originalMailItem);
			}
			else
			{
				value2 = (string)obj;
			}
			journalReportItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.RightsManagement.DecryptionTokenRecipient", value2);
			if (originalMailItem.Recipients.Count == 1)
			{
				journalReportItem.ExtendedProperties.SetValue<string>("Microsoft.Exchange.Encryption.DecryptionTokenRecipient", originalMailItem.Recipients[0].Address.ToString());
			}
			if (originalMailItem.Message != null && originalMailItem.Message.RootPart != null)
			{
				Header[] array = originalMailItem.Message.RootPart.Headers.FindAll("X-MS-Exchange-Organization-Classification");
				if (array != null && array.Length > 0)
				{
					foreach (Header header in array)
					{
						journalReportItem.Message.RootPart.Headers.InsertAfter(header.Clone(), null);
					}
				}
				ITransportMailItemWrapperFacade transportMailItemWrapperFacade = originalMailItem as ITransportMailItemWrapperFacade;
				if (transportMailItemWrapperFacade != null && transportMailItemWrapperFacade.TransportMailItem != null && transportMailItemWrapperFacade.TransportMailItem.IsProbe)
				{
					Header header2 = originalMailItem.Message.RootPart.Headers.FindFirst("X-Exchange-Probe-Drop-Message");
					if (header2 != null)
					{
						ExTraceGlobals.JournalingTracer.TraceDebug<string, string>(0L, "Email has dropped header '{0}' with value '{1}', clone to Journal Report", "X-Exchange-Probe-Drop-Message", header2.Value);
						journalReportItem.Message.RootPart.Headers.InsertAfter(header2.Clone(), null);
					}
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000089AC File Offset: 0x00006BAC
		private static void BccToReconciliationMailboxes(Configuration configuration, ITransportMailItemFacade journalReportItem, string[] remoteAccountGuids)
		{
			Dictionary<string, ReconciliationAccountConfig> reconciliationAccounts = configuration.ReconcileConfig.ReconciliationAccounts;
			if (remoteAccountGuids == null || remoteAccountGuids.Length == 0)
			{
				return;
			}
			foreach (string text in remoteAccountGuids)
			{
				if (text.Length > 0 && text[0] == '!')
				{
					ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "Remote-account {0} is disabled, not bcc'ing copy to reconcile mailbox for this", text);
				}
				else
				{
					ReconciliationAccountConfig reconciliationAccountConfig = null;
					if (!reconciliationAccounts.TryGetValue(text, out reconciliationAccountConfig))
					{
						ExTraceGlobals.JournalingTracer.TraceError<string>(0L, "Remote-account for GUID: {0} was not found. Not reconciling against this account", text);
					}
					else
					{
						string nextMailbox = reconciliationAccountConfig.GetNextMailbox();
						if (!string.IsNullOrEmpty(nextMailbox))
						{
							ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "Bcc'ed journal-report to reconciliation mailbox: {0}", nextMailbox);
							journalReportItem.Recipients.Add(nextMailbox);
						}
					}
				}
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00008A68 File Offset: 0x00006C68
		private static void AddRemoteAccountRecipients(List<string> remoteAccounts, ref List<string> journalRecipsSorted, out string[] remoteAccountGuids)
		{
			remoteAccountGuids = null;
			if (remoteAccounts == null)
			{
				return;
			}
			remoteAccountGuids = new string[remoteAccounts.Count];
			for (int i = 0; i < remoteAccounts.Count; i++)
			{
				string text = remoteAccounts[i];
				int num = text.IndexOf('+');
				bool flag = false;
				if (num != -1)
				{
					remoteAccountGuids[i] = text.Substring(0, num);
					string text2 = text.Substring(num + 1, text.Length - num - 1);
					if (ProxyAddressBase.IsAddressStringValid(text2))
					{
						Utils.AddRecipSortedToList(text2, ref journalRecipsSorted);
						ExTraceGlobals.JournalingTracer.TraceDebug<string, string>(0L, "Reconcile-Policy {0}, Added recip {1}", text, text2);
						flag = true;
					}
				}
				if (!flag)
				{
					throw new JournalReport.ReportGenerationException(string.Format("Remote account cannot be parsed {0}, Rule may be corrupt", text));
				}
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00008B10 File Offset: 0x00006D10
		private static void MarkOriginalMailItemForNdr(MailItem originalMailItem, List<string> journalRecipsSorted)
		{
			if (Utils.IsNdr(originalMailItem))
			{
				return;
			}
			string[] array = Utils.ParseJournaledToHeader(originalMailItem);
			if (array != null)
			{
				foreach (string recipientEmail in array)
				{
					Utils.AddRecipSortedToList(recipientEmail, ref journalRecipsSorted);
				}
			}
			Utils.WriteJournaledToHeader(originalMailItem, journalRecipsSorted);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00008B54 File Offset: 0x00006D54
		private static bool Is8BitEncoded(byte[] characters)
		{
			foreach (byte b in characters)
			{
				if (b >= 128)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00008B84 File Offset: 0x00006D84
		private static void TryStampMissingCharset(string inferredCharsetName, EmailMessage message)
		{
			if (string.IsNullOrEmpty(inferredCharsetName))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug(0L, "No inferred charset was present");
				return;
			}
			MimePart mimePart = message.Body.MimePart;
			if (mimePart == null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug(0L, "Unable to get best body-part of message");
				return;
			}
			ContentTypeHeader contentTypeHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
			if (contentTypeHeader == null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug(0L, "No Content-Type header on message, creating it");
				contentTypeHeader = new ContentTypeHeader("text/plain");
				mimePart.Headers.AppendChild(contentTypeHeader);
			}
			MimeParameter mimeParameter = contentTypeHeader["charset"];
			if (mimeParameter != null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "Charset already present on message: {0}", mimeParameter.Value);
				return;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug(0L, "No charset parameter to Content-Type, adding it");
			mimeParameter = new MimeParameter("charset", inferredCharsetName);
			ExTraceGlobals.JournalingTracer.TraceDebug<string>(0L, "Added inferred charset {0} to embedded message in journal-report", inferredCharsetName);
			contentTypeHeader.AppendChild(mimeParameter);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00008C6C File Offset: 0x00006E6C
		private static Charset DetermineBestCharsetToEncodeSubject(EmailMessage originalMessage, List<string> ruleNames, out int codePage)
		{
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			if (!string.IsNullOrEmpty(originalMessage.Subject))
			{
				outboundCodePageDetector.AddText(originalMessage.Subject);
			}
			foreach (EmailRecipient emailRecipient in originalMessage.To)
			{
				if (!string.IsNullOrEmpty(emailRecipient.DisplayName))
				{
					outboundCodePageDetector.AddText(emailRecipient.DisplayName);
				}
			}
			foreach (EmailRecipient emailRecipient2 in originalMessage.Cc)
			{
				if (!string.IsNullOrEmpty(emailRecipient2.DisplayName))
				{
					outboundCodePageDetector.AddText(emailRecipient2.DisplayName);
				}
			}
			if (ruleNames != null && ruleNames.Count > 0)
			{
				foreach (string value in ruleNames)
				{
					outboundCodePageDetector.AddText(value);
				}
			}
			Charset charset = null;
			if (originalMessage.Body.BodyFormat != BodyFormat.None && originalMessage.Body.CharsetName != null && !Charset.TryGetCharset(originalMessage.Body.CharsetName, out charset))
			{
				charset = null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<Charset>(0L, "Original message specified charset: {0}", charset ?? Charset.Unicode);
			codePage = 0;
			if (charset == null)
			{
				codePage = outboundCodePageDetector.GetCodePage();
			}
			else
			{
				codePage = outboundCodePageDetector.GetCodePage(charset, true);
			}
			Charset result = null;
			if (!Charset.TryGetCharset(codePage, out result))
			{
				ExTraceGlobals.JournalingTracer.TraceError<int>(0L, "Codepage: {0} not installed on this server, falling back to UTF8", codePage);
				result = Charset.GetCharset(Encoding.UTF8.CodePage);
			}
			return result;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00008E30 File Offset: 0x00007030
		private static AddressHeader GetXBccHeader(EmailMessage originalMessage)
		{
			if (originalMessage != null && originalMessage.RootPart != null && originalMessage.RootPart.Headers != null)
			{
				HeaderList headers = originalMessage.RootPart.Headers;
				return headers.FindFirst("X-MS-Exchange-Organization-BCC") as AddressHeader;
			}
			return null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00008E78 File Offset: 0x00007078
		private static void AddBccRecipients(EmailMessage originalMessage, AddressHeader bccHeader)
		{
			if (originalMessage == null || bccHeader == null)
			{
				return;
			}
			foreach (AddressItem addressItem in bccHeader)
			{
				MimeRecipient mimeRecipient = addressItem as MimeRecipient;
				if (mimeRecipient != null)
				{
					originalMessage.Bcc.Add(new EmailRecipient(mimeRecipient.DisplayName, mimeRecipient.Email.ToString()));
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00008EF4 File Offset: 0x000070F4
		private static void SetJournalItemTenantIdToSpecialTenant(MailItem mailItem, TransportMailItem journalItem)
		{
			string config = JournalConfigSchema.Configuration.GetConfig<string>("LegalInterceptTenantName");
			if (!string.IsNullOrEmpty(config))
			{
				Guid lawfulInterceptTenantGuid = ADJournalRuleStorageManager.GetLawfulInterceptTenantGuid(config);
				if (lawfulInterceptTenantGuid == Guid.Empty)
				{
					JournalingGlobals.Logger.LogEvent(MessagingPoliciesEventLogConstants.Tuple_JournalingLITenantNotFoundInResourceForestError, null, new object[]
					{
						mailItem.Message.MessageId,
						config,
						JournalingGlobals.RetryIntervalOnError
					});
					throw new JournalReport.ReportGenerationException(string.Format("The LI tenant '{0}' is not found in Resource Forest.", config));
				}
				journalItem.ExternalOrganizationId = lawfulInterceptTenantGuid;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00008F80 File Offset: 0x00007180
		private void WriteTo(EmailMessage journalMessage, Encoding encoding, bool gccReport, bool fullReport, List<string> ruleNames, List<LegacyRecipientRecord> legacyRecords)
		{
			EmailMessage message = this.originalMailItem.Message;
			ContentTransferEncoding transferEncoding = ContentTransferEncoding.SevenBit;
			if (!string.IsNullOrEmpty(message.Subject))
			{
				byte[] bytes = encoding.GetBytes(message.Subject);
				if (JournalReport.Is8BitEncoded(bytes))
				{
					ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "Subject contains 8bit characters, will be QP encoded");
					transferEncoding = ContentTransferEncoding.QuotedPrintable;
				}
			}
			Stream contentWriteStream = journalMessage.RootPart.GetContentWriteStream(transferEncoding);
			using (TextWriter textWriter = new StreamWriter(contentWriteStream, encoding))
			{
				textWriter.Write("Sender: ");
				string userAddress = JournalReport.GetUserAddress(message.Sender);
				textWriter.Write(userAddress);
				textWriter.Write("\r\n");
				if (!JournalReport.IsEqualRecipient(message.Sender, message.From))
				{
					textWriter.Write("On-Behalf-Of: ");
					string userAddress2 = JournalReport.GetUserAddress(message.From);
					textWriter.Write(userAddress2);
					textWriter.Write("\r\n");
				}
				if (!gccReport || fullReport)
				{
					textWriter.Write("Subject: ");
					textWriter.Write(message.Subject);
					textWriter.Write("\r\n");
				}
				if (gccReport)
				{
					this.WriteExtraGccHeaders(textWriter, ruleNames);
				}
				string value = this.originalMailItem.Message.MessageId;
				if (string.IsNullOrEmpty(value))
				{
					value = "Unknown Message-ID";
				}
				textWriter.Write("Message-Id: ");
				textWriter.Write(value);
				textWriter.Write("\r\n");
				if (legacyRecords != null && legacyRecords.Count > 0)
				{
					this.WriteRecipientsFromLegacyRecords(textWriter, legacyRecords);
				}
				else
				{
					this.WriteRecipients(textWriter);
				}
				textWriter.Flush();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000912C File Offset: 0x0000732C
		private void WriteExtraGccHeaders(TextWriter writer, List<string> ruleNames)
		{
			EmailMessage message = this.originalMailItem.Message;
			if (ruleNames != null && ruleNames.Count > 0)
			{
				bool flag = true;
				writer.Write("Rule-Names: ");
				foreach (string value in ruleNames)
				{
					if (!flag)
					{
						writer.Write(", ");
					}
					flag = false;
					writer.Write(value);
				}
				writer.Write("\r\n");
			}
			Header[] array = message.MimeDocument.RootPart.Headers.FindAll(HeaderId.Received);
			if (array != null && array.Length > 0)
			{
				foreach (Header header in array)
				{
					writer.Write("Received: ");
					writer.Write(header.Value);
					writer.Write("\r\n");
				}
			}
			writer.Write("Mapi-Message-Class: ");
			writer.Write(message.MapiMessageClass);
			writer.Write("\r\n");
			writer.Write("Original-Authenticator: ");
			writer.Write(this.originalMailItem.OriginalAuthenticator);
			writer.Write("\r\n");
			writer.Write("Original-Message-Size: ");
			writer.Write(this.originalMailItem.MimeStreamLength);
			writer.Write("\r\n");
			int count = this.originalMailItem.Recipients.Count;
			writer.Write("Recipient-Count: ");
			writer.Write(count);
			writer.Write("\r\n");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000092BC File Offset: 0x000074BC
		private void WriteRecipients(TextWriter writer)
		{
			List<EnvelopeRecipient>[] recipientBuckets = this.SortIntoBuckets();
			this.WriteRecipientsByBucket(writer, recipientBuckets);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000092D8 File Offset: 0x000074D8
		private void PutRecipientIntoBucketsByRecipientType<T>(RecipientP2Type type, T recipient, List<T>[] buckets)
		{
			switch (type)
			{
			case RecipientP2Type.Unknown:
				buckets[3].Add(recipient);
				return;
			case RecipientP2Type.To:
				buckets[0].Add(recipient);
				return;
			case RecipientP2Type.Cc:
				buckets[1].Add(recipient);
				return;
			case RecipientP2Type.Bcc:
				buckets[2].Add(recipient);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00009328 File Offset: 0x00007528
		private void WriteRecipientsFromLegacyRecords(TextWriter writer, List<LegacyRecipientRecord> legacyRecords)
		{
			List<LegacyRecipientRecord>[] array = new List<LegacyRecipientRecord>[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<LegacyRecipientRecord>();
			}
			foreach (LegacyRecipientRecord legacyRecipientRecord in legacyRecords)
			{
				RecipientP2Type p2Type = legacyRecipientRecord.P2Type;
				this.PutRecipientIntoBucketsByRecipientType<LegacyRecipientRecord>(p2Type, legacyRecipientRecord, array);
			}
			for (int j = 0; j < array.Length; j++)
			{
				foreach (LegacyRecipientRecord legacyRecipientRecord2 in array[j])
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(JournalReport.RecipientTags[j]);
					stringBuilder.Append(legacyRecipientRecord2.Address);
					if (legacyRecipientRecord2.History != null)
					{
						JournalReport.AppendHistoryRecord(stringBuilder, legacyRecipientRecord2.History);
					}
					writer.Write(stringBuilder.ToString());
					writer.Write("\r\n");
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00009448 File Offset: 0x00007648
		private List<EnvelopeRecipient>[] SortIntoBuckets()
		{
			List<EnvelopeRecipient>[] array = new List<EnvelopeRecipient>[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new List<EnvelopeRecipient>();
			}
			EnvelopeRecipientCollection recipients = this.originalMailItem.Recipients;
			foreach (EnvelopeRecipient envelopeRecipient in recipients)
			{
				IHistoryFacade perRecipientHistory = TransportFacades.ReadHistoryFrom(((IMailRecipientWrapperFacade)envelopeRecipient).MailRecipient);
				RecipientHistory recipientHistory = new RecipientHistory(this.globalHistory, perRecipientHistory);
				RecipientP2Type p2Type = recipientHistory.P2Type;
				this.PutRecipientIntoBucketsByRecipientType<EnvelopeRecipient>(p2Type, envelopeRecipient, array);
			}
			return array;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000094F0 File Offset: 0x000076F0
		private void WriteRecipientsByBucket(TextWriter writer, List<EnvelopeRecipient>[] recipientBuckets)
		{
			for (int i = 0; i < recipientBuckets.Length; i++)
			{
				foreach (EnvelopeRecipient envelopeRecipient in recipientBuckets[i])
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(JournalReport.RecipientTags[i]);
					stringBuilder.Append(envelopeRecipient.Address.ToString());
					IHistoryFacade perRecipientHistory = TransportFacades.ReadHistoryFrom(((IMailRecipientWrapperFacade)envelopeRecipient).MailRecipient);
					RecipientHistory recipientHistory = new RecipientHistory(this.globalHistory, perRecipientHistory);
					if (recipientHistory.FirstRecord != null)
					{
						JournalReport.AppendHistoryRecord(stringBuilder, recipientHistory.FirstRecord);
					}
					writer.Write(stringBuilder.ToString());
					writer.Write("\r\n");
				}
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000095D0 File Offset: 0x000077D0
		private void SetCharsetName(EmailMessage message, string charsetName)
		{
			MimePart rootPart = message.RootPart;
			ContentTypeHeader contentTypeHeader = rootPart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
			if (contentTypeHeader == null)
			{
				contentTypeHeader = new ContentTypeHeader("text/plain");
				rootPart.Headers.AppendChild(contentTypeHeader);
			}
			MimeParameter mimeParameter = contentTypeHeader["charset"];
			if (mimeParameter == null)
			{
				mimeParameter = new MimeParameter("charset");
				contentTypeHeader.AppendChild(mimeParameter);
			}
			mimeParameter.Value = charsetName;
		}

		// Token: 0x04000070 RID: 112
		private const string SenderLine = "Sender: ";

		// Token: 0x04000071 RID: 113
		private const string OnBehalfOf = "On-Behalf-Of: ";

		// Token: 0x04000072 RID: 114
		private const string SubjectLine = "Subject: ";

		// Token: 0x04000073 RID: 115
		private const string GccRuleNames = "Rule-Names: ";

		// Token: 0x04000074 RID: 116
		private const string ReceivedHeaderLine = "Received: ";

		// Token: 0x04000075 RID: 117
		private const string MapiMessageClass = "Mapi-Message-Class: ";

		// Token: 0x04000076 RID: 118
		private const string RecipientCount = "Recipient-Count: ";

		// Token: 0x04000077 RID: 119
		private const string OriginalMessageSize = "Original-Message-Size: ";

		// Token: 0x04000078 RID: 120
		private const string OriginalAuthenticator = "Original-Authenticator: ";

		// Token: 0x04000079 RID: 121
		private const string MessageIdLine = "Message-Id: ";

		// Token: 0x0400007A RID: 122
		private const string Crlf = "\r\n";

		// Token: 0x0400007B RID: 123
		private const string DefaultContentType = "text/plain";

		// Token: 0x0400007C RID: 124
		private const string CharsetParameterName = "charset";

		// Token: 0x0400007D RID: 125
		private static readonly string[] RecipientTags = new string[]
		{
			"To: ",
			"Cc: ",
			"Bcc: ",
			"Recipient: "
		};

		// Token: 0x0400007E RID: 126
		private MailItem originalMailItem;

		// Token: 0x0400007F RID: 127
		private Configuration currentConfiguration;

		// Token: 0x04000080 RID: 128
		private IHistoryFacade globalHistory;

		// Token: 0x02000013 RID: 19
		private enum RecipientBucketTag
		{
			// Token: 0x04000082 RID: 130
			To,
			// Token: 0x04000083 RID: 131
			Cc,
			// Token: 0x04000084 RID: 132
			Bcc,
			// Token: 0x04000085 RID: 133
			Recipient
		}

		// Token: 0x02000014 RID: 20
		internal class ReportGenerationException : Exception
		{
			// Token: 0x0600007A RID: 122 RVA: 0x00009676 File Offset: 0x00007876
			public ReportGenerationException(string message) : base(message)
			{
			}
		}
	}
}
