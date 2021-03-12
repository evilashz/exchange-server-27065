﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200001E RID: 30
	internal sealed class IncidentReport
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x000053C0 File Offset: 0x000035C0
		public static void CreateMessage(TransportRulesEvaluationContext context, IEnumerable<IncidentReportContent> reportContentItems, MailItem originalMessage, EmailMessage message)
		{
			message.Sender = new EmailRecipient("Microsoft Outlook", "<>");
			Header header = Header.Create(HeaderId.MessageId);
			header.Value = string.Format("<{0}@{1}>", Guid.NewGuid(), "IncidentReport");
			message.MimeDocument.RootPart.Headers.AppendChild(header);
			message.Date = DateTime.UtcNow;
			message.Subject = TransportRulesStrings.IncidentReportMessageSubject(context.CurrentRule.Name);
			Header header2 = Header.Create("X-MS-Exchange-Transport-Rules-IncidentReport");
			header2.Value = "1";
			message.MimeDocument.RootPart.Headers.AppendChild(header2);
			Header header3 = Header.Create("X-MS-Exchange-Forest-RulesExecuted");
			header3.Value = context.Server.Name;
			message.MimeDocument.RootPart.Headers.AppendChild(header3);
			if (reportContentItems.Contains(IncidentReportContent.Subject) && !string.IsNullOrEmpty(originalMessage.Message.Subject))
			{
				message.Subject = originalMessage.Message.Subject;
			}
			string text = IncidentReportGenerator.GenerateIncidentReport(originalMessage.Message, context, reportContentItems);
			int num = 0;
			Charset charset = IncidentReport.DetectCharset(new List<string>
			{
				message.Subject,
				text
			}, out num);
			Encoding encoding = charset.GetEncoding();
			IncidentReport.SetMultipartAlternativeAndCharsetName(message, charset.Name);
			ContentTransferEncoding transferEncoding = ContentTransferEncoding.SevenBit;
			if (IncidentReport.Is8BitEncoded(message.Subject, encoding))
			{
				transferEncoding = ContentTransferEncoding.QuotedPrintable;
			}
			if (IncidentReport.Is8BitEncoded(text, encoding))
			{
				transferEncoding = ContentTransferEncoding.QuotedPrintable;
			}
			MimePart mimePart = (MimePart)message.RootPart.FirstChild;
			using (Stream contentWriteStream = mimePart.GetContentWriteStream(transferEncoding))
			{
				using (TextWriter textWriter = new StreamWriter(contentWriteStream, encoding))
				{
					textWriter.Write(text);
				}
			}
			MimePart mimePart2 = (MimePart)message.RootPart.LastChild;
			using (Stream contentWriteStream2 = mimePart2.GetContentWriteStream(transferEncoding))
			{
				using (TextWriter textWriter2 = new StreamWriter(contentWriteStream2, encoding))
				{
					using (TextReader textReader = new StringReader(text))
					{
						TextToHtml textToHtml = new TextToHtml();
						textToHtml.Convert(textReader, textWriter2);
					}
				}
			}
			if (reportContentItems.Contains(IncidentReportContent.AttachOriginalMail))
			{
				Attachment attachment = message.Attachments.Add(null, "message/rfc822");
				attachment.EmbeddedMessage = originalMessage.Message;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005664 File Offset: 0x00003864
		public static ITransportMailItemFacade CreateReport(string recipientAddress, IEnumerable<IncidentReportContent> reportContentItems, TransportRulesEvaluationContext context)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("recipientAddress", recipientAddress);
			ArgumentValidator.ThrowIfNull("reportContentItems", reportContentItems);
			ArgumentValidator.ThrowIfNull("context", context);
			TransportMailItem transportMailItem = TransportMailItem.NewAgentMailItem(TransportUtils.GetTransportMailItemFacade(context.MailItem));
			transportMailItem.From = RoutingAddress.NullReversePath;
			transportMailItem.Recipients.Add(recipientAddress);
			transportMailItem.Message.To.Add(new EmailRecipient(string.Empty, recipientAddress));
			IncidentReport.CreateMessage(context, reportContentItems, context.MailItem, transportMailItem.Message);
			transportMailItem.ReceiveConnectorName = "Transport Rule";
			TransportFacades.EnsureSecurityAttributes(transportMailItem);
			TransportRulesLoopChecker.StampLoopCountHeader(1, transportMailItem);
			return transportMailItem;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005704 File Offset: 0x00003904
		internal static bool IsIncidentReport(EmailMessage message)
		{
			string text;
			return TransportUtils.TryGetHeaderValue(message, "X-MS-Exchange-Transport-Rules-IncidentReport", out text);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000572B File Offset: 0x0000392B
		internal static bool Is8BitEncoded(IEnumerable<byte> characters)
		{
			return characters.Any((byte ch) => ch >= 128);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00005750 File Offset: 0x00003950
		internal static bool Is8BitEncoded(string text, Encoding messageCharsetEncoding)
		{
			byte[] bytes = messageCharsetEncoding.GetBytes(text);
			return IncidentReport.Is8BitEncoded(bytes);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005770 File Offset: 0x00003970
		internal static void SetMultipartAlternativeAndCharsetName(EmailMessage message, string charsetName)
		{
			MimePart rootPart = message.RootPart;
			ContentTypeHeader contentTypeHeader = rootPart.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
			if (contentTypeHeader != null)
			{
				rootPart.Headers.RemoveChild(contentTypeHeader);
			}
			contentTypeHeader = new ContentTypeHeader("multipart/alternative");
			rootPart.Headers.AppendChild(contentTypeHeader);
			MimePart newChild = Utility.CreateBodyPart("text/plain", charsetName);
			rootPart.AppendChild(newChild);
			MimePart newChild2 = Utility.CreateBodyPart("text/html", charsetName);
			rootPart.AppendChild(newChild2);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000057E8 File Offset: 0x000039E8
		internal static Charset DetectCharset(IEnumerable<string> textLines, out int codePage)
		{
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			foreach (string value in textLines)
			{
				outboundCodePageDetector.AddText(value);
			}
			codePage = outboundCodePageDetector.GetCodePage();
			Charset result = null;
			if (!Charset.TryGetCharset(codePage, out result))
			{
				ExTraceGlobals.TransportRulesEngineTracer.TraceError<int>(0L, "Codepage: {0} not installed on this server, falling back to UTF8", codePage);
				result = Charset.GetCharset(Encoding.UTF8.CodePage);
			}
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005870 File Offset: 0x00003A70
		private static void CopyHeaders(EmailMessage copyFrom, TransportMailItem copyTo, HeaderId headerId)
		{
			if (copyFrom.RootPart != null && copyTo.RootPart != null)
			{
				for (Header header = copyFrom.RootPart.Headers.FindFirst(headerId); header != null; header = copyFrom.RootPart.Headers.FindNext(header))
				{
					copyTo.RootPart.Headers.InsertAfter(header.Clone(), null);
				}
			}
		}

		// Token: 0x040000E0 RID: 224
		internal const string ReportSenderName = "Microsoft Outlook";

		// Token: 0x040000E1 RID: 225
		internal const string ReportSenderAddress = "<>";

		// Token: 0x040000E2 RID: 226
		private const string MessageIdPrefix = "IncidentReport";

		// Token: 0x040000E3 RID: 227
		private const string IncidentReportHeaderValue = "1";
	}
}
