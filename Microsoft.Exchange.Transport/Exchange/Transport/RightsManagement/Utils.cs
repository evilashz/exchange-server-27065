using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Transport.RightsManagement
{
	// Token: 0x020003ED RID: 1005
	internal static class Utils
	{
		// Token: 0x06002DB9 RID: 11705 RVA: 0x000B7D88 File Offset: 0x000B5F88
		public static OutboundConversionOptions GetOutboundConversionOptions(MailItem mailItem)
		{
			OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomain.DomainName.Domain);
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			outboundConversionOptions.RecipientCache = (ADRecipientCache<TransportMiniRecipient>)transportMailItemWrapperFacade.TransportMailItem.ADRecipientCacheAsObject;
			if (transportMailItemWrapperFacade.TransportMailItem.TransportSettings != null)
			{
				outboundConversionOptions.ClearCategories = transportMailItemWrapperFacade.TransportMailItem.TransportSettings.ClearCategories;
				outboundConversionOptions.UseRFC2231Encoding = transportMailItemWrapperFacade.TransportMailItem.TransportSettings.Rfc2231EncodingEnabled;
			}
			return outboundConversionOptions;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000B7E0C File Offset: 0x000B600C
		public static OutboundConversionOptions GetOutboundConversionOptions(IReadOnlyMailItem mailItem)
		{
			OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomain.DomainName.Domain);
			outboundConversionOptions.RecipientCache = mailItem.ADRecipientCache;
			if (mailItem.TransportSettings != null)
			{
				outboundConversionOptions.ClearCategories = mailItem.TransportSettings.ClearCategories;
				outboundConversionOptions.UseRFC2231Encoding = mailItem.TransportSettings.Rfc2231EncodingEnabled;
			}
			return outboundConversionOptions;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000B7E70 File Offset: 0x000B6070
		public static void CopyHeadersDuringDecryption(EmailMessage srcMessage, EmailMessage targetMessage)
		{
			targetMessage.RootPart.Headers.RemoveAll(HeaderId.Subject);
			targetMessage.RootPart.Headers.RemoveAll("X-MS-Has-Attach");
			if (srcMessage.RootPart != null && targetMessage.RootPart != null)
			{
				for (MimeNode mimeNode = srcMessage.RootPart.Headers.LastChild; mimeNode != null; mimeNode = mimeNode.PreviousSibling)
				{
					Header header = mimeNode as Header;
					if (header != null && HeaderId.ContentClass != header.HeaderId && HeaderId.ContentType != header.HeaderId && HeaderId.ContentTransferEncoding != header.HeaderId && !string.Equals(header.Name, "X-MS-TNEF-Correlator", StringComparison.OrdinalIgnoreCase))
					{
						MimeNode newChild = header.Clone();
						targetMessage.RootPart.Headers.InsertAfter(newChild, null);
					}
				}
				if (targetMessage.AttachmentCollection_Count() > 0)
				{
					Utils.StampXHeader(targetMessage, "X-MS-Has-Attach", "yes");
				}
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000B7F44 File Offset: 0x000B6144
		public static EmailMessage ConvertMessageItemToEmailMessage(MessageItem item, OutboundConversionOptions contentConversionOption, bool useTNEF = true)
		{
			Stream stream = Streams.CreateTemporaryStorageStream();
			if (useTNEF)
			{
				ItemConversion.ConvertItemToSummaryTnef(item, stream, contentConversionOption);
			}
			else
			{
				ItemConversion.ConvertItemToMime(item, stream, contentConversionOption);
			}
			MimeDocument mimeDocument = new MimeDocument();
			mimeDocument.Load(stream, CachingMode.SourceTakeOwnership);
			return EmailMessage.Create(mimeDocument);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000B7F88 File Offset: 0x000B6188
		public static OrganizationId OrgIdFromMailItem(MailItem mailItem)
		{
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = mailItem as ITransportMailItemWrapperFacade;
			if (transportMailItemWrapperFacade != null && transportMailItemWrapperFacade.TransportMailItem != null)
			{
				return transportMailItemWrapperFacade.TransportMailItem.OrganizationIdAsObject as OrganizationId;
			}
			return null;
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000B7FC8 File Offset: 0x000B61C8
		public static bool TryGetCultureInfoAndEncoding(EmailMessage message, out string charsetName, out CultureInfo cultureInfo, out Encoding encoding)
		{
			HeaderList headers = (message.RootPart == null) ? null : message.RootPart.Headers;
			cultureInfo = CultureProcessor.Instance.GetCultureInfo(headers, false);
			Charset charset = Utils.GetCharset(message);
			if (charset != null && charset.TryGetEncoding(out encoding))
			{
				charsetName = charset.Name;
				if (cultureInfo == null)
				{
					cultureInfo = charset.Culture.GetCultureInfo();
					if (string.IsNullOrEmpty(cultureInfo.Name))
					{
						cultureInfo = CultureProcessor.Instance.DefaultCulture;
					}
				}
				return true;
			}
			charsetName = null;
			cultureInfo = null;
			encoding = null;
			return false;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000B804C File Offset: 0x000B624C
		public static void StampXHeader(EmailMessage message, string xheader, string value)
		{
			Header header = Header.Create(xheader);
			header.Value = value;
			message.MimeDocument.RootPart.Headers.AppendChild(header);
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B8080 File Offset: 0x000B6280
		public static Header GetXHeader(EmailMessage message, string xheader)
		{
			MimePart rootPart = message.RootPart;
			if (rootPart == null)
			{
				return null;
			}
			return rootPart.Headers.FindFirst(xheader);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B80A8 File Offset: 0x000B62A8
		public static ReadOnlyCollection<string> GetRecipientEmailAddresses(IReadOnlyMailItem mailItem)
		{
			ReadOnlyCollection<string> result = null;
			if (mailItem.ExtendedProperties.TryGetListValue<string>("Microsoft.Exchange.RMSEncryptionAgent.RecipientListForPL", out result))
			{
				return result;
			}
			List<string> list = new List<string>();
			EmailRecipientCollection emailRecipientCollection = mailItem.Message.To;
			foreach (EmailRecipient emailRecipient in emailRecipientCollection)
			{
				if (!string.IsNullOrEmpty(emailRecipient.SmtpAddress))
				{
					list.Add(emailRecipient.SmtpAddress);
				}
			}
			emailRecipientCollection = mailItem.Message.Cc;
			foreach (EmailRecipient emailRecipient2 in emailRecipientCollection)
			{
				if (!string.IsNullOrEmpty(emailRecipient2.SmtpAddress))
				{
					list.Add(emailRecipient2.SmtpAddress);
				}
			}
			emailRecipientCollection = mailItem.Message.BccFromOrgHeader;
			foreach (EmailRecipient emailRecipient3 in emailRecipientCollection)
			{
				if (!string.IsNullOrEmpty(emailRecipient3.SmtpAddress))
				{
					list.Add(emailRecipient3.SmtpAddress);
				}
			}
			if (list.Count == 0)
			{
				if (mailItem.Recipients == null || mailItem.Recipients.Count == 0)
				{
					return null;
				}
				foreach (MailRecipient mailRecipient in mailItem.Recipients)
				{
					list.Add(mailRecipient.Email.ToString());
				}
			}
			return new ReadOnlyCollection<string>(list);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B8270 File Offset: 0x000B6470
		public static void SetProtectedContentClass(EmailMessage protectedMessage)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Setting Protected ContentClass - rpmsg.message or the um classes");
			MimePart rootPart = protectedMessage.RootPart;
			HeaderList headers = rootPart.Headers;
			Header header = headers.FindFirst(HeaderId.ContentClass);
			if (header == null)
			{
				header = Header.Create(HeaderId.ContentClass);
				headers.AppendChild(header);
			}
			header.Value = "rpmsg.message";
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B82C4 File Offset: 0x000B64C4
		public static void SetBodyContent(CultureInfo culture, Body body)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Setting BodyContent for the down level clients");
			string s = string.Format("{0} {1}", SystemMessages.BodyReceiveRMEmail.ToString(culture), SystemMessages.BodyDownload.ToString(culture));
			Encoding encoding = Encoding.GetEncoding(body.CharsetName);
			byte[] bytes = encoding.GetBytes(s);
			using (Stream contentWriteStream = body.GetContentWriteStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(contentWriteStream))
				{
					binaryWriter.Write(bytes);
				}
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B836C File Offset: 0x000B656C
		public static void CopyHeadersDuringEncryption(EmailMessage srcMessage, EmailMessage targetMessage)
		{
			ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Copying Email Properties to the protected message");
			if (srcMessage.RootPart != null && targetMessage.RootPart != null)
			{
				for (MimeNode mimeNode = srcMessage.RootPart.Headers.LastChild; mimeNode != null; mimeNode = mimeNode.PreviousSibling)
				{
					Header header = mimeNode as Header;
					if (header != null && HeaderId.ContentClass != header.HeaderId && HeaderId.ContentType != header.HeaderId && HeaderId.ContentTransferEncoding != header.HeaderId && !string.Equals("X-MS-Exchange-Organization-RightsProtectMessage", header.Name, StringComparison.OrdinalIgnoreCase) && !string.Equals("X-MS-Has-Attach", header.Name, StringComparison.OrdinalIgnoreCase))
					{
						MimeNode newChild = header.Clone();
						targetMessage.RootPart.Headers.InsertAfter(newChild, null);
					}
				}
				Utils.StampXHeader(targetMessage, "X-MS-Has-Attach", "yes");
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B8438 File Offset: 0x000B6638
		public static bool IsProtectedEmail(EmailMessage message)
		{
			bool result = false;
			if (Utils.IsProtectedContentClass(message) && message.Attachments != null && 1 == message.Attachments.Count && Utils.IsProtectedAttachment(message.Attachments[0]))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B847C File Offset: 0x000B667C
		private static bool IsProtectedContentClass(EmailMessage message)
		{
			if (message.RootPart == null)
			{
				return false;
			}
			HeaderList headers = message.RootPart.Headers;
			Header header = headers.FindFirst(HeaderId.ContentClass);
			return header != null && (string.Equals(header.Value, "rpmsg.message", StringComparison.OrdinalIgnoreCase) || string.Equals(header.Value, "IPM.Note.rpmsg.Microsoft.Voicemail.UM.CA", StringComparison.OrdinalIgnoreCase) || string.Equals(header.Value, "IPM.Note.rpmsg.Microsoft.Voicemail.UM", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B84E6 File Offset: 0x000B66E6
		private static bool IsProtectedAttachment(Attachment attachment)
		{
			return string.Equals(attachment.FileName, "message.rpmsg", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B84FC File Offset: 0x000B66FC
		private static Charset GetCharset(EmailMessage message)
		{
			Charset result;
			if (!EmailMessageHelpers.TryGetTnefBinaryCharset(message, out result))
			{
				string charsetName = message.Body.CharsetName;
				if (string.IsNullOrEmpty(charsetName))
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Use Default ANSI charset; Body.CharsetName is null.");
					Charset.TryGetCharset(1252, out result);
				}
				else
				{
					ExTraceGlobals.RightsManagementTracer.TraceDebug(0L, "Use Body.CharsetName as charset; PureTnefMessage or PureTnefMessage.BinaryCharset is null.");
					Charset.TryGetCharset(charsetName, out result);
				}
			}
			return result;
		}

		// Token: 0x040016CE RID: 5838
		public const int DefaultANSICodePage = 1252;

		// Token: 0x040016CF RID: 5839
		public const string RecipientListToGeneratePL = "Microsoft.Exchange.RMSEncryptionAgent.RecipientListForPL";
	}
}
