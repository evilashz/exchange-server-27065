using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000068 RID: 104
	internal class GenerateNotification : TransportAction
	{
		// Token: 0x0600036C RID: 876 RVA: 0x000134AF File Offset: 0x000116AF
		public GenerateNotification(ShortList<Argument> arguments) : base(arguments)
		{
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000134B8 File Offset: 0x000116B8
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.RecipientRelated;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000134BB File Offset: 0x000116BB
		public override string Name
		{
			get
			{
				return "GenerateNotification";
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000134C2 File Offset: 0x000116C2
		public override Version MinimumVersion
		{
			get
			{
				return GenerateNotification.GenerateNotificationActionVersion;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000134CC File Offset: 0x000116CC
		public override void ValidateArguments(ShortList<Argument> inputArguments)
		{
			if (inputArguments.Count != 1)
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
			if (inputArguments[0].Type != typeof(string))
			{
				throw new RulesValidationException(RulesStrings.ActionArgumentMismatch(this.Name));
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00013524 File Offset: 0x00011724
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			if (GenerateNotification.IsEtrNotification(transportRulesEvaluationContext.MailItem.Message))
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "GenerateNotification: Skipping notification on a notification message");
				return ExecutionControl.Execute;
			}
			if (TransportRulesLoopChecker.IsIncidentReportLoopCountExceeded(transportRulesEvaluationContext.MailItem))
			{
				TransportRulesEvaluator.Trace(transportRulesEvaluationContext.TransportRulesTracer, transportRulesEvaluationContext.MailItem, "GenerateNotification: Message loop count limit exceeded. Skipping notification generation");
				return ExecutionControl.Execute;
			}
			string contentTemplate = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string body = GenerateNotification.GenerateContent(contentTemplate, transportRulesEvaluationContext.MailItem.Message);
			GenerateNotification.GenerateMessage(transportRulesEvaluationContext.MailItem, transportRulesEvaluationContext.Server.Name, new EmailRecipient("Microsoft Outlook", "<>"), transportRulesEvaluationContext.Message.EnvelopeRecipients, transportRulesEvaluationContext.MailItem.Message.Subject, body);
			return ExecutionControl.Execute;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000135F4 File Offset: 0x000117F4
		internal static void GenerateMessage(MailItem sourceMailItem, string serverName, EmailRecipient sender, IEnumerable<string> recipients, string subject, string body)
		{
			TransportMailItem transportMailItem = TransportMailItem.NewAgentMailItem(TransportUtils.GetTransportMailItemFacade(sourceMailItem));
			transportMailItem.From = RoutingAddress.NullReversePath;
			foreach (string smtpAddress in recipients)
			{
				transportMailItem.Recipients.Add(smtpAddress);
				transportMailItem.Message.To.Add(new EmailRecipient(string.Empty, smtpAddress));
			}
			transportMailItem.Message.Sender = sender;
			Header header = Header.Create(HeaderId.MessageId);
			header.Value = string.Format("<{0}@{1}>", Guid.NewGuid(), "EtrNotification");
			transportMailItem.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			transportMailItem.Message.Date = DateTime.UtcNow;
			transportMailItem.Message.Subject = subject;
			GenerateNotification.SetEtrHeaders(transportMailItem, serverName, sourceMailItem);
			GenerateNotification.SetNotificationContent(transportMailItem, body);
			transportMailItem.CommitLazy();
			TransportFacades.TrackReceiveByAgent(transportMailItem, "Transport Rule", null, new long?(transportMailItem.RecordId));
			Components.CategorizerComponent.EnqueueSideEffectMessage(sourceMailItem, transportMailItem, "Transport Rule Agent");
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00013720 File Offset: 0x00011920
		internal static void SetEtrHeaders(TransportMailItem notificationMessage, string serverName, MailItem mailItem)
		{
			Header header = Header.Create("X-MS-Exchange-Transport-Rules-Notification");
			header.Value = "1";
			notificationMessage.Message.MimeDocument.RootPart.Headers.AppendChild(header);
			Header header2 = Header.Create("X-MS-Exchange-Forest-RulesExecuted");
			header2.Value = serverName;
			notificationMessage.Message.MimeDocument.RootPart.Headers.AppendChild(header2);
			TransportRulesLoopChecker.StampLoopCountHeader(TransportRulesLoopChecker.GetMessageLoopCount(mailItem) + 1, notificationMessage);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001379C File Offset: 0x0001199C
		private static void SetNotificationContent(TransportMailItem notificationMessage, string content)
		{
			int num = 0;
			Charset charset = IncidentReport.DetectCharset(new List<string>
			{
				notificationMessage.Message.Subject,
				content
			}, out num);
			Encoding encoding = charset.GetEncoding();
			IncidentReport.SetMultipartAlternativeAndCharsetName(notificationMessage.Message, charset.Name);
			ContentTransferEncoding transferEncoding = ContentTransferEncoding.SevenBit;
			if (IncidentReport.Is8BitEncoded(notificationMessage.Message.Subject, encoding))
			{
				transferEncoding = ContentTransferEncoding.QuotedPrintable;
			}
			if (IncidentReport.Is8BitEncoded(content, encoding))
			{
				transferEncoding = ContentTransferEncoding.QuotedPrintable;
			}
			MimePart mimePart = (MimePart)notificationMessage.Message.RootPart.FirstChild;
			using (Stream contentWriteStream = mimePart.GetContentWriteStream(transferEncoding))
			{
				using (TextWriter textWriter = new StreamWriter(contentWriteStream, encoding))
				{
					string textPresentation = GenerateNotification.GetTextPresentation(content, encoding);
					textWriter.Write(textPresentation);
				}
			}
			MimePart mimePart2 = (MimePart)notificationMessage.Message.RootPart.LastChild;
			using (Stream contentWriteStream2 = mimePart2.GetContentWriteStream(transferEncoding))
			{
				using (TextWriter textWriter2 = new StreamWriter(contentWriteStream2, encoding))
				{
					string htmlPresentation = GenerateNotification.GetHtmlPresentation(content, encoding);
					textWriter2.Write(htmlPresentation);
				}
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000138F8 File Offset: 0x00011AF8
		internal static bool IsEtrNotification(EmailMessage message)
		{
			string text;
			return TransportUtils.TryGetHeaderValue(message, "X-MS-Exchange-Transport-Rules-Notification", out text);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013930 File Offset: 0x00011B30
		internal static string GenerateContent(string contentTemplate, EmailMessage message)
		{
			return GenerateNotification.dynamicParameters.Aggregate(contentTemplate, (string current, string parameter) => Regex.Replace(current, parameter, GenerateNotification.GetParameterValue(parameter, message), RegexOptions.IgnoreCase));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013964 File Offset: 0x00011B64
		internal static string GetParameterValue(string parameter, EmailMessage message)
		{
			switch (parameter)
			{
			case "%%From%%":
				return GenerateNotification.GetRecipientString(message.From);
			case "%%To%%":
				return GenerateNotification.GetRecipients(message.To);
			case "%%Cc%%":
				return GenerateNotification.GetRecipients(message.Cc);
			case "%%Subject%%":
				return message.Subject;
			case "%%MessageDate%%":
				return message.Date.ToString("G");
			case "%%Headers%%":
				return GenerateNotification.GetHeaders(message.MimeDocument);
			}
			return string.Empty;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00013A58 File Offset: 0x00011C58
		internal static string GetTextPresentation(string content, Encoding encoding)
		{
			HtmlToText htmlToText = new HtmlToText
			{
				InputEncoding = encoding
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(content))
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					htmlToText.Convert(stringReader, stringWriter);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013AD0 File Offset: 0x00011CD0
		internal static string GetHtmlPresentation(string content, Encoding encoding)
		{
			HtmlToHtml htmlToHtml = new HtmlToHtml
			{
				InputEncoding = encoding,
				NormalizeHtml = true,
				FilterHtml = false
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(content))
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					htmlToHtml.Convert(stringReader, stringWriter);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00013B58 File Offset: 0x00011D58
		internal static string PlainTextToHtml(string content, Encoding encoding)
		{
			TextToHtml textToHtml = new TextToHtml
			{
				InputEncoding = encoding,
				FilterHtml = false
			};
			StringBuilder stringBuilder = new StringBuilder();
			using (StringReader stringReader = new StringReader(content))
			{
				using (StringWriter stringWriter = new StringWriter(stringBuilder))
				{
					textToHtml.Convert(stringReader, stringWriter);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00013BD8 File Offset: 0x00011DD8
		internal static string GetRecipients(IEnumerable<EmailRecipient> recipients)
		{
			if (recipients == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EmailRecipient recipient in recipients)
			{
				stringBuilder.Append(GenerateNotification.GetRecipientString(recipient));
				stringBuilder.Append(", ");
			}
			string text = stringBuilder.ToString();
			if (!string.IsNullOrEmpty(text))
			{
				return text.Substring(0, text.Length - ", ".Length);
			}
			return text;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00013C6C File Offset: 0x00011E6C
		internal static string GetRecipientString(EmailRecipient recipient)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(recipient.DisplayName) && !recipient.DisplayName.Equals(recipient.SmtpAddress))
			{
				stringBuilder.Append(recipient.DisplayName);
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(recipient.SmtpAddress);
			return stringBuilder.ToString();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013CCC File Offset: 0x00011ECC
		internal static string GetHeaders(MimeDocument mimeDocument)
		{
			if (mimeDocument != null && mimeDocument.RootPart != null && mimeDocument.RootPart.Headers != null)
			{
				using (MimeNode.Enumerator<Header> enumerator = mimeDocument.RootPart.Headers.GetEnumerator())
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (enumerator.MoveNext())
					{
						Header header = enumerator.Current;
						stringBuilder.Append(header.Name);
						stringBuilder.Append(": ");
						stringBuilder.Append(enumerator.Current.Value);
						stringBuilder.Append("<br />");
					}
					return stringBuilder.ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x04000229 RID: 553
		private const string MessageIdPrefix = "EtrNotification";

		// Token: 0x0400022A RID: 554
		private const string NotificationHeaderValue = "1";

		// Token: 0x0400022B RID: 555
		private const string ItemSeparator = ", ";

		// Token: 0x0400022C RID: 556
		private const string LineSeparator = "<br />";

		// Token: 0x0400022D RID: 557
		public static readonly Version GenerateNotificationActionVersion = new Version("15.00.0013.00");

		// Token: 0x0400022E RID: 558
		private static readonly List<string> dynamicParameters = new List<string>
		{
			"%%From%%",
			"%%To%%",
			"%%Cc%%",
			"%%Subject%%",
			"%%Headers%%",
			"%%MessageDate%%"
		};

		// Token: 0x02000069 RID: 105
		internal static class ContentParameters
		{
			// Token: 0x0400022F RID: 559
			internal const string From = "%%From%%";

			// Token: 0x04000230 RID: 560
			internal const string To = "%%To%%";

			// Token: 0x04000231 RID: 561
			internal const string Cc = "%%Cc%%";

			// Token: 0x04000232 RID: 562
			internal const string Subject = "%%Subject%%";

			// Token: 0x04000233 RID: 563
			internal const string Headers = "%%Headers%%";

			// Token: 0x04000234 RID: 564
			internal const string MessageDate = "%%MessageDate%%";
		}
	}
}
