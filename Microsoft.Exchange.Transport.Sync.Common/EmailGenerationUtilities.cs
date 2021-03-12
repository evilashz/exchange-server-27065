using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000067 RID: 103
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EmailGenerationUtilities
	{
		// Token: 0x06000280 RID: 640 RVA: 0x00006F58 File Offset: 0x00005158
		public static string SanitizeSubject(string inputSubject)
		{
			SyncUtilities.ThrowIfArgumentNull("inputSubject", inputSubject);
			string result = inputSubject;
			if (inputSubject.Length > 255)
			{
				result = inputSubject.Substring(0, 255 - "...".Length) + "...";
			}
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00006FE8 File Offset: 0x000051E8
		public static bool TryGetMicrosoftExchangeRecipientSmtpAddress(ADSessionSettings userADSessionSettings, SyncLogSession syncLogSession, out string microsoftExchangeRecipientSmtpAddress)
		{
			microsoftExchangeRecipientSmtpAddress = null;
			MicrosoftExchangeRecipient exchangeRecipient = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(null, true, ConsistencyMode.IgnoreInvalid, null, userADSessionSettings, 84, "TryGetMicrosoftExchangeRecipientSmtpAddress", "f:\\15.00.1497\\sources\\dev\\transportSync\\src\\Common\\EmailGenerationUtilities.cs");
				exchangeRecipient = tenantOrTopologyConfigurationSession.FindMicrosoftExchangeRecipient();
			});
			if (!adoperationResult.Succeeded)
			{
				syncLogSession.LogError((TSLID)176UL, "AD operation failed while trying to find the Microsoft Exchange Recipient; Exception: {0}", new object[]
				{
					adoperationResult.Exception ?? "<null>"
				});
				return false;
			}
			if (exchangeRecipient == null)
			{
				syncLogSession.LogError((TSLID)2UL, "Unable to find a Microsoft Exchange Recipient for user's tenant org.", new object[0]);
				return false;
			}
			microsoftExchangeRecipientSmtpAddress = exchangeRecipient.PrimarySmtpAddress.ToString();
			return true;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007098 File Offset: 0x00005298
		public static MemoryStream GenerateEmailMimeStream(string messageId, string fromDisplayName, string fromSmtpAddress, string toSmtpAddress, string emailSubject, string htmlBodyContent, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentNullOrEmpty("messageId", messageId);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("fromDisplayName", fromDisplayName);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("fromSmtpAddress", fromSmtpAddress);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("toSmtpAddress", toSmtpAddress);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("emailSubject", emailSubject);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("htmlBodyContent", htmlBodyContent);
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			outboundCodePageDetector.AddText(messageId);
			outboundCodePageDetector.AddText(emailSubject);
			outboundCodePageDetector.AddText(htmlBodyContent);
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Charset charset;
			Encoding encoding;
			EmailGenerationUtilities.DetectCharSetAndEncodingFrom(outboundCodePageDetector, currentCulture, syncLogSession, out charset, out encoding);
			EncodingOptions encodingOptions = new EncodingOptions(charset.Name, currentCulture.Name, EncodingFlags.None);
			MemoryStream memoryStream = new MemoryStream(2000);
			using (MemoryStream memoryStream2 = new MemoryStream(2000))
			{
				using (MimeWriter mimeWriter = new MimeWriter(memoryStream2, true, encodingOptions))
				{
					mimeWriter.StartPart();
					EmailGenerationUtilities.WriteRFC822HeadersTo(mimeWriter, messageId, fromDisplayName, fromSmtpAddress, toSmtpAddress, emailSubject);
					EmailGenerationUtilities.WriteTextBodyPartTo(mimeWriter, htmlBodyContent, charset, encoding);
					EmailGenerationUtilities.WriteHtmlBodyPartTo(mimeWriter, htmlBodyContent, charset, encoding);
					mimeWriter.EndPart();
					mimeWriter.Flush();
					memoryStream2.WriteTo(memoryStream);
				}
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000071DC File Offset: 0x000053DC
		private static void DetectCharSetAndEncodingFrom(OutboundCodePageDetector codePageDetector, CultureInfo cultureInfo, SyncLogSession syncLogSession, out Charset charset, out Encoding encoding)
		{
			Culture culture;
			int codePage;
			if (Culture.TryGetCulture(cultureInfo.LCID, out culture))
			{
				codePage = codePageDetector.GetCodePage(culture, false);
			}
			else
			{
				codePage = codePageDetector.GetCodePage();
			}
			if (!Charset.TryGetCharset(codePage, out charset) || !charset.IsAvailable)
			{
				syncLogSession.LogInformation((TSLID)3UL, "Unable to determine charset for codepage: {0}.  Falling back to the UTF-8 charset.", new object[]
				{
					codePage
				});
				charset = Charset.UTF8;
			}
			if (!charset.TryGetEncoding(out encoding))
			{
				syncLogSession.LogInformation((TSLID)4UL, "Unable to get encoding for charset: {0}.  Falling back to using the UTF-8 charset and encoding.", new object[]
				{
					charset.Name
				});
				charset = Charset.UTF8;
				encoding = Encoding.UTF8;
			}
			syncLogSession.LogInformation((TSLID)5UL, "Detected charset: {0} and encoding: {1}", new object[]
			{
				charset.Name,
				encoding.EncodingName
			});
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000072B8 File Offset: 0x000054B8
		private static void WriteRFC822HeadersTo(MimeWriter mimeWriter, string messageId, string fromDisplayName, string fromSmtpAddress, string toSmtpAddress, string emailSubject)
		{
			mimeWriter.WriteHeader(HeaderId.MimeVersion, "1.0");
			mimeWriter.StartHeader(HeaderId.ContentType);
			mimeWriter.WriteHeaderValue("multipart/alternative");
			mimeWriter.WriteParameter("differences", "Content-Type");
			mimeWriter.WriteParameter("boundary", Guid.NewGuid().ToString());
			mimeWriter.WriteHeader(HeaderId.MessageId, messageId);
			mimeWriter.StartHeader(HeaderId.From);
			mimeWriter.WriteRecipient(fromDisplayName, fromSmtpAddress);
			mimeWriter.WriteHeader(HeaderId.To, toSmtpAddress);
			mimeWriter.WriteHeader(HeaderId.ReturnPath, "<>");
			mimeWriter.WriteHeader(HeaderId.Subject, emailSubject);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000734C File Offset: 0x0000554C
		private static void WriteTextBodyPartTo(MimeWriter mimeWriter, string htmlContent, Charset charset, Encoding encoding)
		{
			mimeWriter.StartPart();
			mimeWriter.StartHeader(HeaderId.ContentType);
			mimeWriter.WriteHeaderValue("text/plain;");
			mimeWriter.WriteParameter("charset", charset.Name);
			mimeWriter.WriteHeader("Content-Transfer-Encoding", "quoted-printable");
			using (EncoderStream encoderStream = new EncoderStream(mimeWriter.GetRawContentWriteStream(), new QPEncoder(), EncoderStreamAccess.Write))
			{
				new HtmlToText
				{
					InputEncoding = encoding,
					DetectEncodingFromByteOrderMark = false,
					DetectEncodingFromMetaTag = false,
					OutputEncoding = encoding
				}.Convert(new StringReader(htmlContent), encoderStream);
				encoderStream.Flush();
			}
			mimeWriter.EndPart();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000073FC File Offset: 0x000055FC
		private static void WriteHtmlBodyPartTo(MimeWriter mimeWriter, string htmlContent, Charset charset, Encoding encoding)
		{
			mimeWriter.StartPart();
			mimeWriter.StartHeader(HeaderId.ContentType);
			mimeWriter.WriteHeaderValue("text/html;");
			mimeWriter.WriteParameter("charset", charset.Name);
			mimeWriter.WriteHeader("Content-Transfer-Encoding", "quoted-printable");
			using (EncoderStream encoderStream = new EncoderStream(mimeWriter.GetRawContentWriteStream(), new QPEncoder(), EncoderStreamAccess.Write))
			{
				byte[] bytes = encoding.GetBytes(htmlContent);
				encoderStream.Write(bytes, 0, bytes.Length);
				encoderStream.Flush();
			}
			mimeWriter.EndPart();
		}

		// Token: 0x0400011A RID: 282
		private const int EstimatedMessageDataSize = 2000;
	}
}
