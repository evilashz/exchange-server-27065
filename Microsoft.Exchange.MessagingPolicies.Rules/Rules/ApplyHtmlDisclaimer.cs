using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport.Internal;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200005D RID: 93
	internal class ApplyHtmlDisclaimer : TransportAction
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00011FC0 File Offset: 0x000101C0
		public ApplyHtmlDisclaimer(ShortList<Argument> arguments) : base(arguments)
		{
			ApplyHtmlDisclaimer.ReadConfigFile();
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00011FCE File Offset: 0x000101CE
		public override Version MinimumVersion
		{
			get
			{
				return ApplyHtmlDisclaimer.minimumVersion;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00011FD5 File Offset: 0x000101D5
		public override Type[] ArgumentsType
		{
			get
			{
				return ApplyHtmlDisclaimer.argumentTypes;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00011FDC File Offset: 0x000101DC
		public override string Name
		{
			get
			{
				return "ApplyHtmlDisclaimer";
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00011FE3 File Offset: 0x000101E3
		public override TransportActionType Type
		{
			get
			{
				return TransportActionType.BifurcationNeeded;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011FE8 File Offset: 0x000101E8
		protected override ExecutionControl OnExecute(RulesEvaluationContext baseContext)
		{
			TransportRulesEvaluationContext transportRulesEvaluationContext = (TransportRulesEvaluationContext)baseContext;
			string a = (string)base.Arguments[0].GetValue(transportRulesEvaluationContext);
			string text = (string)base.Arguments[1].GetValue(transportRulesEvaluationContext);
			string fallbackAction = (string)base.Arguments[2].GetValue(transportRulesEvaluationContext);
			Encoding messageBodyEncoding = ApplyHtmlDisclaimer.GetMessageBodyEncoding(transportRulesEvaluationContext);
			string key = text;
			HtmlDisclaimerEntry entry;
			bool flag2;
			lock (ApplyHtmlDisclaimer.disclaimerLockVar)
			{
				flag2 = ApplyHtmlDisclaimer.disclaimerLookupTable.TryGetValue(key, out entry);
			}
			if (!flag2)
			{
				entry = this.AddDisclaimerValuesToLookup(key, text, messageBodyEncoding, transportRulesEvaluationContext.MailItem);
			}
			Header[] array = transportRulesEvaluationContext.MailItem.Message.MimeDocument.RootPart.Headers.FindAll("X-MS-Exchange-Organization-Disclaimer-Hash");
			foreach (Header header in array)
			{
				if (header.Value == entry.TextHash)
				{
					return ExecutionControl.Execute;
				}
			}
			if (!transportRulesEvaluationContext.CanModify || messageBodyEncoding == null)
			{
				return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
			}
			Body body = transportRulesEvaluationContext.MailItem.Message.Body;
			if (body.BodyFormat == BodyFormat.None && transportRulesEvaluationContext.MailItem.Message.TnefPart != null && transportRulesEvaluationContext.MailItem.Message.MapiMessageClass.StartsWith("IPM.Schedule.Meeting.Resp."))
			{
				return ExecutionControl.Execute;
			}
			bool flag3 = false;
			switch (body.BodyFormat)
			{
			case BodyFormat.Text:
			{
				TextToText textToText = new TextToText();
				if (a == "Append" || a == "Inline")
				{
					textToText.Footer = this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem);
				}
				else
				{
					textToText.Header = this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem);
				}
				textToText.InputEncoding = messageBodyEncoding;
				textToText.HeaderFooterFormat = HeaderFooterFormat.Text;
				if (!ApplyHtmlDisclaimer.ConvertTextBody(transportRulesEvaluationContext, entry, textToText, messageBodyEncoding))
				{
					return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
				}
				flag3 = true;
				break;
			}
			case BodyFormat.Rtf:
			{
				RtfToRtf rtfToRtf = new RtfToRtf();
				if (a == "Append" || a == "Inline")
				{
					rtfToRtf.Footer = this.BuildDisclaimerTextFromSegments(entry.HtmlTextSegments, transportRulesEvaluationContext.MailItem);
				}
				else
				{
					rtfToRtf.Header = this.BuildDisclaimerTextFromSegments(entry.HtmlTextSegments, transportRulesEvaluationContext.MailItem);
				}
				rtfToRtf.HeaderFooterFormat = HeaderFooterFormat.Html;
				Stream stream = null;
				Stream stream2 = null;
				if (!body.TryGetContentReadStream(out stream))
				{
					return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
				}
				try
				{
					stream2 = body.GetContentWriteStream();
					try
					{
						rtfToRtf.Convert(stream, stream2);
					}
					catch (ExchangeDataException)
					{
						return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
					}
					flag3 = true;
				}
				finally
				{
					if (stream2 != null)
					{
						stream2.Close();
					}
					if (stream != null)
					{
						stream.Close();
					}
				}
				break;
			}
			case BodyFormat.Html:
			{
				HtmlToHtml htmlToHtml = new HtmlToHtml();
				if (a == "Append" || a == "Inline")
				{
					htmlToHtml.Footer = this.BuildDisclaimerTextFromSegments(entry.HtmlTextSegments, transportRulesEvaluationContext.MailItem);
				}
				else
				{
					htmlToHtml.Header = this.BuildDisclaimerTextFromSegments(entry.HtmlTextSegments, transportRulesEvaluationContext.MailItem);
				}
				htmlToHtml.HeaderFooterFormat = HeaderFooterFormat.Html;
				htmlToHtml.InputEncoding = messageBodyEncoding;
				htmlToHtml.NormalizeHtml = true;
				htmlToHtml.FilterHtml = false;
				if (!ApplyHtmlDisclaimer.ConvertHtmlBody(transportRulesEvaluationContext, entry, htmlToHtml, messageBodyEncoding))
				{
					return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
				}
				flag3 = true;
				break;
			}
			}
			if (flag3)
			{
				TransportUtils.AddHeaderToMail(transportRulesEvaluationContext.MailItem.Message, "X-MS-Exchange-Organization-Disclaimer-Hash", entry.TextHash);
			}
			else
			{
				MimePart calendarPart = transportRulesEvaluationContext.MailItem.Message.CalendarPart;
				if (body.BodyFormat == BodyFormat.None && calendarPart == null)
				{
					return this.HandleFallbackOption(transportRulesEvaluationContext, fallbackAction, this.BuildDisclaimerTextFromSegments(entry.PlainTextSegments, transportRulesEvaluationContext.MailItem));
				}
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001243C File Offset: 0x0001063C
		private static void ReadConfigFile()
		{
			ApplyHtmlDisclaimer.DisableDetectEncodingFromMetaTag = ApplyHtmlDisclaimer.TryReadBoolean("DisableDetectEncodingFromMetaTag", ApplyHtmlDisclaimer.DisableDetectEncodingFromMetaTag);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00012454 File Offset: 0x00010654
		private static bool TryReadBoolean(string key, bool defaultValue)
		{
			bool result = defaultValue;
			try
			{
				string text = ConfigurationManager.AppSettings[key];
				ExTraceGlobals.RulesEngineTracer.TraceDebug<string, string>(0L, "{0} key value configured in transport config is {1}", key, text);
				if (!bool.TryParse(text, out result))
				{
					result = defaultValue;
				}
			}
			catch (ConfigurationErrorsException arg)
			{
				ExTraceGlobals.RulesEngineTracer.TraceError<string, ConfigurationErrorsException>(0L, "Not able to load the {0} value from the transport config file. Exception raised {1}", key, arg);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x000124BC File Offset: 0x000106BC
		private static Encoding GetMessageBodyEncoding(TransportRulesEvaluationContext context)
		{
			Encoding result = null;
			bool flag;
			try
			{
				string charsetName = context.MailItem.Message.Body.CharsetName;
				if (string.IsNullOrEmpty(charsetName))
				{
					flag = true;
					result = Encoding.ASCII;
				}
				else
				{
					flag = Charset.TryGetEncoding(charsetName, out result);
				}
			}
			catch (ExchangeDataException)
			{
				flag = false;
			}
			if (flag)
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001251C File Offset: 0x0001071C
		private static bool ConvertTextBody(TransportRulesEvaluationContext context, HtmlDisclaimerEntry entry, TextToText t2tConverter, Encoding bodyEncoding)
		{
			Stream stream = null;
			Stream stream2 = null;
			Body body = context.MailItem.Message.Body;
			if (!body.TryGetContentReadStream(out stream))
			{
				return false;
			}
			Encoding utf = Encoding.UTF8;
			bool flag = ApplyHtmlDisclaimer.ComputeRequiredOutputEncoding(entry, bodyEncoding, ref utf);
			if (!flag)
			{
				flag = body.ConversionNeeded(entry.ValidCodePages);
			}
			try
			{
				if (flag)
				{
					stream2 = body.GetContentWriteStream(utf.WebName);
					t2tConverter.OutputEncoding = utf;
				}
				else
				{
					stream2 = body.GetContentWriteStream();
				}
				try
				{
					t2tConverter.Convert(stream, stream2);
				}
				catch (ExchangeDataException)
				{
					return false;
				}
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
			}
			return true;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000125D4 File Offset: 0x000107D4
		private static bool ConvertHtmlBody(TransportRulesEvaluationContext context, HtmlDisclaimerEntry entry, HtmlToHtml h2hConverter, Encoding bodyEncoding)
		{
			Stream stream = null;
			Stream stream2 = null;
			Body body = context.MailItem.Message.Body;
			if (!body.TryGetContentReadStream(out stream))
			{
				return false;
			}
			Encoding utf = Encoding.UTF8;
			bool flag = ApplyHtmlDisclaimer.ComputeRequiredOutputEncoding(entry, bodyEncoding, ref utf);
			try
			{
				if (flag)
				{
					stream2 = body.GetContentWriteStream(utf.WebName);
					h2hConverter.OutputEncoding = utf;
				}
				else
				{
					stream2 = body.GetContentWriteStream();
					if (ApplyHtmlDisclaimer.DisableDetectEncodingFromMetaTag)
					{
						h2hConverter.DetectEncodingFromMetaTag = false;
					}
				}
				try
				{
					h2hConverter.Convert(stream, stream2);
				}
				catch (ExchangeDataException)
				{
					return false;
				}
			}
			finally
			{
				if (stream2 != null)
				{
					stream2.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
			}
			return true;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00012688 File Offset: 0x00010888
		private static bool ComputeRequiredOutputEncoding(HtmlDisclaimerEntry entry, Encoding bodyEncoding, ref Encoding outputEncoding)
		{
			bool result;
			if (bodyEncoding == Encoding.Unicode || bodyEncoding == Encoding.UTF8 || bodyEncoding == Encoding.UTF7 || bodyEncoding.CodePage == 65001 || bodyEncoding.CodePage == 65000 || bodyEncoding.CodePage == 1200 || bodyEncoding.CodePage == 1201 || bodyEncoding.CodePage == 54936 || bodyEncoding.CodePage == 12000 || bodyEncoding.CodePage == 12001)
			{
				result = false;
			}
			else
			{
				result = true;
				foreach (int codePage in entry.ValidCodePages)
				{
					Encoding encoding;
					if (Charset.TryGetEncoding(codePage, out encoding) && encoding == bodyEncoding)
					{
						outputEncoding = encoding;
						result = false;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00012744 File Offset: 0x00010944
		private HtmlDisclaimerEntry AddDisclaimerValuesToLookup(string key, string text, Encoding bodyEncoding, MailItem mailItem)
		{
			HtmlDisclaimerEntry htmlDisclaimerEntry = default(HtmlDisclaimerEntry);
			if (bodyEncoding == null)
			{
				htmlDisclaimerEntry.PlainTextSegments = new string[]
				{
					text
				};
				htmlDisclaimerEntry.HtmlTextSegments = new string[]
				{
					text
				};
			}
			else
			{
				HtmlToText htmlToText = new HtmlToText();
				htmlToText.InputEncoding = bodyEncoding;
				StringBuilder stringBuilder = new StringBuilder();
				using (StringReader stringReader = new StringReader(text))
				{
					using (StringWriter stringWriter = new StringWriter(stringBuilder))
					{
						htmlToText.Convert(stringReader, stringWriter);
					}
				}
				htmlDisclaimerEntry.PlainTextSegments = this.ParseDynamicEntries(stringBuilder.ToString());
				HtmlToHtml htmlToHtml = new HtmlToHtml();
				htmlToHtml.InputEncoding = bodyEncoding;
				htmlToHtml.NormalizeHtml = true;
				htmlToHtml.FilterHtml = false;
				StringBuilder stringBuilder2 = new StringBuilder();
				using (StringReader stringReader2 = new StringReader(text))
				{
					using (StringWriter stringWriter2 = new StringWriter(stringBuilder2))
					{
						htmlToHtml.Convert(stringReader2, stringWriter2);
					}
				}
				htmlDisclaimerEntry.HtmlTextSegments = this.ParseDynamicEntries(stringBuilder2.ToString());
			}
			htmlDisclaimerEntry.TextHash = TransportUtils.GenerateHashString(key);
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			outboundCodePageDetector.AddText(this.BuildDisclaimerTextFromSegments(htmlDisclaimerEntry.HtmlTextSegments, mailItem));
			htmlDisclaimerEntry.ValidCodePages = outboundCodePageDetector.GetCodePages();
			lock (ApplyHtmlDisclaimer.disclaimerLockVar)
			{
				if (!ApplyHtmlDisclaimer.disclaimerLookupTable.ContainsKey(key))
				{
					ApplyHtmlDisclaimer.disclaimerLookupTable.Add(key, htmlDisclaimerEntry);
				}
			}
			return htmlDisclaimerEntry;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001290C File Offset: 0x00010B0C
		private void HandleDisclaimerWrap(TransportRulesEvaluationContext context, string disclaimerText)
		{
			MimePart rootPart = context.MailItem.Message.RootPart;
			MimePart mimePart = new MimePart("message/rfc822");
			using (Stream rawContentWriteStream = mimePart.GetRawContentWriteStream())
			{
				rootPart.WriteTo(rawContentWriteStream);
			}
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			outboundCodePageDetector.AddText(disclaimerText);
			Header header = rootPart.Headers.FindFirst(HeaderId.Subject);
			if (header != null && !string.IsNullOrEmpty(header.Value))
			{
				outboundCodePageDetector.AddText(header.Value);
			}
			Encoding utf;
			if (!Charset.TryGetEncoding(outboundCodePageDetector.GetCodePage(), out utf))
			{
				utf = Encoding.UTF8;
			}
			MimePart mimePart2 = new MimePart("text/plain", ContentTransferEncoding.QuotedPrintable, new MemoryStream(utf.GetBytes(disclaimerText)), CachingMode.SourceTakeOwnership);
			ContentTypeHeader contentTypeHeader = mimePart2.Headers.FindFirst(HeaderId.ContentType) as ContentTypeHeader;
			contentTypeHeader.AppendChild(new MimeParameter("charset", utf.WebName));
			rootPart.Headers.AppendChild(new ContentTypeHeader("multipart/mixed"));
			ContentTransferEncoding contentTransferEncoding = rootPart.ContentTransferEncoding;
			bool flag = contentTransferEncoding == ContentTransferEncoding.EightBit || contentTransferEncoding == ContentTransferEncoding.SevenBit || contentTransferEncoding == ContentTransferEncoding.Binary;
			if (flag)
			{
				if (contentTransferEncoding != ContentTransferEncoding.SevenBit)
				{
					mimePart.Headers.AppendChild(rootPart.Headers.FindFirst(HeaderId.ContentTransferEncoding).Clone());
				}
			}
			else
			{
				rootPart.Headers.RemoveAll(HeaderId.ContentTransferEncoding);
			}
			for (int i = 0; i < ApplyHtmlDisclaimer.HeadersToClear.Length; i++)
			{
				rootPart.Headers.RemoveAll(ApplyHtmlDisclaimer.HeadersToClear[i]);
			}
			rootPart.RemoveAll();
			rootPart.AppendChild(mimePart2);
			rootPart.AppendChild(mimePart);
			HtmlDisclaimerEntry htmlDisclaimerEntry;
			ApplyHtmlDisclaimer.disclaimerLookupTable.TryGetValue(context.CurrentRule.Name, out htmlDisclaimerEntry);
			Header header2 = Header.Create("X-MS-Exchange-Organization-Disclaimer-Hash");
			header2.Value = htmlDisclaimerEntry.TextHash;
			rootPart.Headers.AppendChild(header2);
			TransportConfigContainer transportConfigObject = Microsoft.Exchange.Transport.Internal.Configuration.TransportConfigObject;
			if (transportConfigObject.ConvertDisclaimerWrapperToEml)
			{
				header2 = Header.Create("X-MS-Exchange-Organization-Disclaimer-Wrapper");
				header2.Value = "True";
				rootPart.Headers.AppendChild(header2);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00012B18 File Offset: 0x00010D18
		private string[] ParseDynamicEntries(string text)
		{
			return text.Split(new string[]
			{
				"%%"
			}, StringSplitOptions.None);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012B3C File Offset: 0x00010D3C
		private string BuildDisclaimerTextFromSegments(string[] disclaimerSegments, MailItem mailItem)
		{
			if (disclaimerSegments.Length == 1)
			{
				return disclaimerSegments[0];
			}
			IADRecipientCache iadrecipientCache = (IADRecipientCache)TransportUtils.GetTransportMailItemFacade(mailItem).ADRecipientCacheAsObject;
			ADRawEntry adrawEntry = null;
			if (mailItem != null && mailItem.Message != null && mailItem.Message.Sender != null && SmtpAddress.IsValidSmtpAddress(mailItem.Message.Sender.SmtpAddress))
			{
				ProxyAddress proxyAddress = new SmtpProxyAddress(mailItem.Message.Sender.SmtpAddress, true);
				adrawEntry = iadrecipientCache.FindAndCacheRecipient(proxyAddress).Data;
			}
			StringBuilder stringBuilder = new StringBuilder(disclaimerSegments[0], 200);
			for (int i = 1; i < disclaimerSegments.Length; i++)
			{
				if (i % 2 == 1)
				{
					if (adrawEntry != null)
					{
						stringBuilder.Append(TransportUtils.GetMacroPropertyDefinition(new SmtpProxyAddress(mailItem.Message.Sender.SmtpAddress, true), disclaimerSegments[i], adrawEntry));
					}
				}
				else
				{
					stringBuilder.Append(disclaimerSegments[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012C24 File Offset: 0x00010E24
		private ExecutionControl HandleFallbackOption(TransportRulesEvaluationContext context, string fallbackAction, string disclaimerText)
		{
			if (fallbackAction != null && !(fallbackAction == "Ignore"))
			{
				if (!(fallbackAction == "Wrap"))
				{
					if (fallbackAction == "Reject")
					{
						return RejectMessage.Reject(context, "550", "5.7.1", "Delivery not authorized, message refused");
					}
				}
				else
				{
					this.HandleDisclaimerWrap(context, disclaimerText);
				}
			}
			return ExecutionControl.Execute;
		}

		// Token: 0x04000200 RID: 512
		public const string DisableDetectEncodingFromMetaTagLabel = "DisableDetectEncodingFromMetaTag";

		// Token: 0x04000201 RID: 513
		internal const string DisclaimerAppendValue = "Append";

		// Token: 0x04000202 RID: 514
		internal const string DisclaimerInlineValue = "Inline";

		// Token: 0x04000203 RID: 515
		private const string DisclaimerHashHeader = "X-MS-Exchange-Organization-Disclaimer-Hash";

		// Token: 0x04000204 RID: 516
		private const string MeetingResponseString = "IPM.Schedule.Meeting.Resp.";

		// Token: 0x04000205 RID: 517
		public static bool DisableDetectEncodingFromMetaTag = true;

		// Token: 0x04000206 RID: 518
		private static readonly HeaderId[] HeadersToClear = new HeaderId[]
		{
			HeaderId.ContentClass,
			HeaderId.ContentDisposition,
			HeaderId.ContentDescription,
			HeaderId.ContentMD5
		};

		// Token: 0x04000207 RID: 519
		private static Version minimumVersion = TransportRuleConstants.VersionedContainerBaseVersion;

		// Token: 0x04000208 RID: 520
		private static object disclaimerLockVar = new object();

		// Token: 0x04000209 RID: 521
		private static Dictionary<string, HtmlDisclaimerEntry> disclaimerLookupTable = new Dictionary<string, HtmlDisclaimerEntry>();

		// Token: 0x0400020A RID: 522
		private static Type[] argumentTypes = new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		};
	}
}
