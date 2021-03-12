using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000002 RID: 2
	internal class NonRenderableItemConverter : IImapItemConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public NonRenderableItemConverter(string userName, string userSmtpAddress, Item itemIn, OutboundConversionOptions options)
		{
			string id = ProtocolBaseStrings.NotAvailable;
			string subject = ProtocolBaseStrings.NotAvailable;
			string displayName = ProtocolBaseStrings.NotAvailable;
			string mailAddress = ProtocolBaseStrings.NotAvailable;
			string sentDate = ProtocolBaseStrings.NotAvailable;
			object obj = itemIn.TryGetProperty(ItemSchema.ImapId);
			if (!(obj is PropertyError))
			{
				id = obj.ToString();
			}
			obj = itemIn.TryGetProperty(ItemSchema.Subject);
			if (!(obj is PropertyError))
			{
				subject = obj.ToString();
			}
			obj = itemIn.TryGetProperty(ItemSchema.SentRepresentingDisplayName);
			if (!(obj is PropertyError))
			{
				displayName = obj.ToString();
			}
			obj = itemIn.TryGetProperty(ItemSchema.SentRepresentingEmailAddress);
			if (!(obj is PropertyError))
			{
				mailAddress = obj.ToString();
			}
			obj = itemIn.TryGetProperty(ItemSchema.SentTime);
			if (!(obj is PropertyError))
			{
				sentDate = obj.ToString();
			}
			string text = ProtocolBaseStrings.NonRenderableSubject(id);
			string text2 = ProtocolBaseStrings.NonRenderableMessage(subject, displayName, mailAddress, sentDate);
			OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
			outboundCodePageDetector.AddText(text);
			outboundCodePageDetector.AddText(ProtocolBaseStrings.SystemFromDisplayName);
			outboundCodePageDetector.AddText(userName);
			outboundCodePageDetector.AddText(text2);
			int codePage = outboundCodePageDetector.GetCodePage();
			Charset utf;
			if (!Charset.TryGetCharset(codePage, out utf))
			{
				utf = Charset.UTF8;
			}
			Encoding utf2;
			if (!utf.TryGetEncoding(out utf2))
			{
				utf = Charset.UTF8;
				utf2 = Encoding.UTF8;
			}
			this.message = EmailMessage.Create(Microsoft.Exchange.Data.Transport.Email.BodyFormat.Text, false, utf.Name);
			this.message.From = new EmailRecipient(ProtocolBaseStrings.SystemFromDisplayName, string.Empty);
			this.message.To.Add(new EmailRecipient(userName, userSmtpAddress));
			this.message.Subject = text;
			byte[] bytes = utf2.GetBytes(text2);
			using (Stream contentWriteStream = this.message.Body.GetContentWriteStream())
			{
				contentWriteStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000022AC File Offset: 0x000004AC
		public override bool ItemNeedsSave
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000022AF File Offset: 0x000004AF
		public override MimePartInfo GetMimeStructure()
		{
			return this.GetMimeStructure(this.message.MimeDocument.RootPart, 1);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022C8 File Offset: 0x000004C8
		public override void GetBody(Stream outStream)
		{
			this.message.MimeDocument.WriteTo(outStream);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000022DC File Offset: 0x000004DC
		public override bool GetBody(Stream outStream, uint[] indices)
		{
			MimePart mimePart = this.GetMimePart(indices);
			if (mimePart == null)
			{
				return false;
			}
			if (indices == null || indices.Length == 0)
			{
				mimePart.WriteTo(outStream);
			}
			else if (mimePart.IsEmbeddedMessage)
			{
				mimePart = (mimePart.FirstChild as MimePart);
				if (mimePart == null)
				{
					return false;
				}
				mimePart.WriteTo(outStream);
			}
			else
			{
				mimePart.WriteTo(outStream, null, NonRenderableItemConverter.headerSuppressFilter);
			}
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000233A File Offset: 0x0000053A
		public override void GetText(Stream outStream)
		{
			this.message.MimeDocument.WriteTo(outStream, null, NonRenderableItemConverter.headerSuppressFilter);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002354 File Offset: 0x00000554
		public override bool GetText(Stream outStream, uint[] indices)
		{
			MimePart mimePart = this.GetMimePart(indices);
			if (mimePart == null)
			{
				return false;
			}
			mimePart.WriteTo(outStream, null, NonRenderableItemConverter.headerSuppressFilter);
			return true;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000237D File Offset: 0x0000057D
		public override MimePartHeaders GetHeaders()
		{
			return NonRenderableItemConverter.GetHeaders(this.message.MimeDocument.RootPart);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002394 File Offset: 0x00000594
		public override MimePartHeaders GetHeaders(uint[] indices)
		{
			MimePart mimePart = this.GetMimePart(indices);
			if (mimePart == this.message.MimeDocument.RootPart)
			{
				return NonRenderableItemConverter.GetHeaders(mimePart);
			}
			if (!mimePart.IsEmbeddedMessage || mimePart.FirstChild == null)
			{
				return null;
			}
			return NonRenderableItemConverter.GetHeaders(mimePart.FirstChild as MimePart);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023E5 File Offset: 0x000005E5
		public override MimePartHeaders GetMime(uint[] indices)
		{
			return NonRenderableItemConverter.GetHeaders(this.GetMimePart(indices));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023F3 File Offset: 0x000005F3
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NonRenderableItemConverter>(this);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023FB File Offset: 0x000005FB
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
			if (disposing && this.message != null)
			{
				this.message.Dispose();
				this.message = null;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002424 File Offset: 0x00000624
		private static MimePartHeaders GetHeaders(MimePart mimePart)
		{
			if (mimePart == null)
			{
				return null;
			}
			MimePartHeaders mimePartHeaders = new MimePartHeaders(NonRenderableItemConverter.GetCharset(mimePart));
			foreach (Header header in mimePart.Headers)
			{
				mimePartHeaders.AddHeader(header);
			}
			return mimePartHeaders;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000248C File Offset: 0x0000068C
		private static Charset GetCharset(MimePart mimePart)
		{
			ComplexHeader complexHeader = mimePart.Headers.FindFirst(HeaderId.ContentType) as ComplexHeader;
			if (complexHeader == null)
			{
				return null;
			}
			MimeParameter mimeParameter = complexHeader["charset"];
			if (mimeParameter == null)
			{
				return null;
			}
			string text;
			if (!mimeParameter.TryGetValue(out text) || string.IsNullOrEmpty(text))
			{
				return null;
			}
			Charset result;
			if (!Charset.TryGetCharset(text, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024E4 File Offset: 0x000006E4
		private MimePartInfo GetMimeStructure(MimePart mimePart, int partIndex)
		{
			MimePartContentType contentType = MimePartInfo.GetContentType(mimePart.ContentType);
			MimePartInfo mimePartInfo = new MimePartInfo(NonRenderableItemConverter.GetCharset(mimePart), null, contentType, ref partIndex);
			mimePartInfo.Headers = NonRenderableItemConverter.GetHeaders(mimePart);
			mimePartInfo.SetBodySize(1, 1);
			MimePart mimePart2 = mimePart.FirstChild as MimePart;
			int num = 0;
			if (mimePart.IsEmbeddedMessage)
			{
				mimePartInfo.AttachedItemStructure = this.GetMimeStructure(mimePart2, num + 1);
			}
			else
			{
				while (mimePart2 != null)
				{
					mimePartInfo.AddChild(this.GetMimeStructure(mimePart2, ++num));
					mimePart2 = (mimePart2.NextSibling as MimePart);
				}
			}
			return mimePartInfo;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002570 File Offset: 0x00000770
		private MimePart GetMimePart(uint[] indices)
		{
			if (indices == null || indices.Length == 0)
			{
				return this.message.MimeDocument.RootPart;
			}
			MimePart mimePart = this.message.MimeDocument.RootPart;
			if (!mimePart.HasChildren && indices.Length == 1 && indices[0] == 1U)
			{
				return mimePart;
			}
			for (int i = 0; i < indices.Length; i++)
			{
				int num = (int)(indices[i] - 1U);
				if (num < 0)
				{
					throw new ArgumentException("indices");
				}
				if (!mimePart.HasChildren)
				{
					return null;
				}
				mimePart = (mimePart.FirstChild as MimePart);
				for (int j = 0; j < num; j++)
				{
					if (mimePart == null)
					{
						return null;
					}
					mimePart = (mimePart.NextSibling as MimePart);
				}
			}
			return mimePart;
		}

		// Token: 0x04000001 RID: 1
		private static MimeOutputFilter headerSuppressFilter = new NonRenderableItemConverter.HeaderSuppressFilter();

		// Token: 0x04000002 RID: 2
		private EmailMessage message;

		// Token: 0x02000003 RID: 3
		private class HeaderSuppressFilter : MimeOutputFilter
		{
			// Token: 0x06000012 RID: 18 RVA: 0x00002620 File Offset: 0x00000820
			public override bool FilterHeaderList(HeaderList headerList, Stream stream)
			{
				return true;
			}
		}
	}
}
