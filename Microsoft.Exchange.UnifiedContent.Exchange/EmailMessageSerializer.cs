using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.UnifiedContent.Exchange
{
	// Token: 0x02000003 RID: 3
	internal static class EmailMessageSerializer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal static void Serialize(this EmailMessage message, UnifiedContentSerializer serializer, bool bypassTextTruncation = true)
		{
			HashSet<object> hashSet = new HashSet<object>();
			serializer.WriteProperty(UnifiedContentSerializer.PropertyId.Subject, message.Subject);
			Stream seekableStream = EmailMessageSerializer.GetSeekableStream(message.Body.GetContentReadStreamOrNull());
			if (seekableStream != null)
			{
				try
				{
					SharedContent sharedContent = EmailMessageSerializer.WriteBody(serializer, seekableStream);
					sharedContent.Properties["Parsing::ParsingKeys::PreferredBody"] = true;
					sharedContent.Properties["Parsing::ParsingKeys::ContentType"] = EmailMessageSerializer.GetBodyContentType(message.Body);
					sharedContent.Properties["Parsing::ParsingKeys::CharSet"] = message.Body.CharsetName;
					if (bypassTextTruncation)
					{
						sharedContent.Properties["Parsing::ConfigKeys::BypassTextTruncation"] = true;
					}
					if (message.Body.MimePart != null)
					{
						hashSet.Add(message.Body.MimePart);
					}
				}
				finally
				{
					seekableStream.Dispose();
				}
			}
			foreach (Attachment attachment in message.Attachments)
			{
				Stream seekableStream2 = EmailMessageSerializer.GetSeekableStream(attachment.GetContentReadStream());
				if (seekableStream2 != null)
				{
					try
					{
						SharedContent sharedContent2 = serializer.AddStream(UnifiedContentSerializer.EntryId.Attachment, seekableStream2, attachment.FileName);
						sharedContent2.Properties["Parsing::ParsingKeys::ContentType"] = attachment.ContentType;
						if (attachment.MimePart != null)
						{
							hashSet.Add(attachment.MimePart);
						}
					}
					finally
					{
						seekableStream2.Dispose();
					}
				}
			}
			if (message.TnefPart != null)
			{
				hashSet.Add(message.TnefPart);
			}
			EmailMessageSerializer.SerializeMimeDocument(serializer, message, hashSet);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000226C File Offset: 0x0000046C
		private static void SerializeMimeDocument(UnifiedContentSerializer serializer, EmailMessage email, HashSet<object> serializedMimeParts)
		{
			foreach (MimePart mimePart in email.MimeDocument.RootPart.Subtree)
			{
				if (!mimePart.HasChildren && !serializedMimeParts.Contains(mimePart))
				{
					string rawFileName = Utility.GetRawFileName(mimePart);
					Stream seekableStream = EmailMessageSerializer.GetSeekableStream(mimePart.GetContentReadStream());
					if (seekableStream != null)
					{
						try
						{
							SharedContent sharedContent;
							if (string.IsNullOrWhiteSpace(rawFileName) || string.Equals(rawFileName, "Mail message body", StringComparison.CurrentCultureIgnoreCase))
							{
								sharedContent = EmailMessageSerializer.WriteBody(serializer, seekableStream);
								sharedContent.Properties["Parsing::ParsingKeys::PreferredBody"] = false;
							}
							else
							{
								sharedContent = serializer.AddStream(UnifiedContentSerializer.EntryId.Attachment, seekableStream, rawFileName);
							}
							sharedContent.Properties["UnifiedContent::PropertyKeys::ExtendedContent"] = true;
							sharedContent.Properties["Parsing::ParsingKeys::ContentType"] = mimePart.ContentType;
							serializedMimeParts.Add(mimePart);
						}
						finally
						{
							seekableStream.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002384 File Offset: 0x00000584
		private static Stream GetSeekableStream(Stream ctsStream)
		{
			if (ctsStream != null && !ctsStream.CanSeek)
			{
				Stream stream = new MemoryStream();
				ctsStream.CopyTo(stream);
				ctsStream.Dispose();
				return stream;
			}
			return ctsStream;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023B4 File Offset: 0x000005B4
		private static string GetBodyContentType(Body body)
		{
			string result = string.Empty;
			switch (body.BodyFormat)
			{
			case BodyFormat.Text:
				result = "text/plaintext";
				break;
			case BodyFormat.Rtf:
				result = "text/rtf";
				break;
			case BodyFormat.Html:
				result = "text/html";
				break;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000023FC File Offset: 0x000005FC
		private static SharedContent WriteBody(UnifiedContentSerializer serializer, Stream bodyStream)
		{
			SharedContent sharedContent = null;
			if (bodyStream != null)
			{
				sharedContent = serializer.AddStream(UnifiedContentSerializer.EntryId.Body, bodyStream, "Message Body");
				if (sharedContent != null)
				{
					sharedContent.Properties["Parsing::ParsingKeys::MessageBody"] = true;
				}
			}
			return sharedContent;
		}

		// Token: 0x04000009 RID: 9
		public const string BypassTextTruncationPropertyKey = "Parsing::ConfigKeys::BypassTextTruncation";

		// Token: 0x0400000A RID: 10
		private const string MessageBodyFileName = "Message Body";

		// Token: 0x02000004 RID: 4
		internal enum EntryId
		{
			// Token: 0x0400000C RID: 12
			PpeHeader,
			// Token: 0x0400000D RID: 13
			Property,
			// Token: 0x0400000E RID: 14
			Body,
			// Token: 0x0400000F RID: 15
			Attachment
		}

		// Token: 0x02000005 RID: 5
		internal enum PropertyId
		{
			// Token: 0x04000011 RID: 17
			Subject = 1
		}
	}
}
