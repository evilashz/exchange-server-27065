using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.UnifiedContent.Exchange
{
	// Token: 0x02000006 RID: 6
	internal static class XSOMessageExtensions
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002438 File Offset: 0x00000638
		internal static void Serialize(this Item item, IMapiFilteringContext context, UnifiedContentSerializer serializer, bool bypassTextTruncation = true)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			if (item.Body.Size > 0L && context.NeedsClassificationScan())
			{
				serializer.WriteProperty(UnifiedContentSerializer.PropertyId.Subject, item.GetValueOrDefault<string>(InternalSchema.Subject, string.Empty));
				StorePropertyDefinition bodyFormat = XSOMessageExtensions.GetBodyFormat(item.Body.Format);
				if (bodyFormat != null)
				{
					using (Stream stream = item.OpenPropertyStream(bodyFormat, PropertyOpenMode.ReadOnly))
					{
						using (Stream bodyStream = XSOMessageExtensions.GetBodyStream(stream))
						{
							SharedContent sharedContent = XSOMessageExtensions.WriteBody(serializer, bodyStream);
							if (sharedContent != null)
							{
								sharedContent.Properties["Parsing::ParsingKeys::PreferredBody"] = true;
								sharedContent.Properties["Parsing::ParsingKeys::ContentType"] = XSOMessageExtensions.GetBodyContentType(item.Body);
								sharedContent.Properties["Parsing::ParsingKeys::CharSet"] = item.Body.RawCharset.Name;
								if (bypassTextTruncation)
								{
									sharedContent.Properties["Parsing::ConfigKeys::BypassTextTruncation"] = true;
								}
							}
						}
					}
				}
			}
			IList<AttachmentHandle> allHandles = item.AttachmentCollection.GetAllHandles();
			foreach (AttachmentHandle handle in allHandles)
			{
				using (Attachment attachment = item.AttachmentCollection.Open(handle))
				{
					XSOMessageExtensions.Serialize(attachment, context, serializer);
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000025EC File Offset: 0x000007EC
		private static void Serialize(Attachment attachment, IMapiFilteringContext context, UnifiedContentSerializer serializer)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			if (attachment.Size > 0L && context.NeedsClassificationScan(attachment))
			{
				using (Stream stream = attachment.PropertyBag.OpenPropertyStream(AttachmentSchema.AttachDataBin, PropertyOpenMode.ReadOnly))
				{
					serializer.AddStream(UnifiedContentSerializer.EntryId.Attachment, stream, string.Format("{0}:{1}", attachment.FileName, attachment.Id.ToBase64String()));
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002688 File Offset: 0x00000888
		private static string GetBodyContentType(Body body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			string result = string.Empty;
			switch (body.Format)
			{
			case BodyFormat.TextPlain:
				result = "text/plaintext";
				break;
			case BodyFormat.TextHtml:
				result = "text/html";
				break;
			case BodyFormat.ApplicationRtf:
				result = "text/rtf";
				break;
			}
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000026E0 File Offset: 0x000008E0
		private static StorePropertyDefinition GetBodyFormat(BodyFormat bodyFormat)
		{
			StorePropertyDefinition result = null;
			switch (bodyFormat)
			{
			case BodyFormat.TextPlain:
				result = ItemSchema.TextBody;
				break;
			case BodyFormat.TextHtml:
				result = ItemSchema.HtmlBody;
				break;
			case BodyFormat.ApplicationRtf:
				result = ItemSchema.RtfBody;
				break;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002720 File Offset: 0x00000920
		private static Stream GetBodyStream(Stream bodyStream)
		{
			if (bodyStream != null)
			{
				try
				{
					long length = bodyStream.Length;
				}
				catch (NotSupportedException)
				{
					Stream stream = new MemoryStream();
					bodyStream.CopyTo(stream);
					bodyStream.Dispose();
					bodyStream = stream;
				}
			}
			return bodyStream;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002764 File Offset: 0x00000964
		private static SharedContent WriteBody(UnifiedContentSerializer serializer, Stream bodyStream)
		{
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
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

		// Token: 0x04000012 RID: 18
		public const string BypassTextTruncationPropertyKey = "Parsing::ConfigKeys::BypassTextTruncation";

		// Token: 0x04000013 RID: 19
		public const string MessageBodyFileName = "Message Body";

		// Token: 0x02000007 RID: 7
		internal enum EntryId
		{
			// Token: 0x04000015 RID: 21
			PpeHeader,
			// Token: 0x04000016 RID: 22
			Property,
			// Token: 0x04000017 RID: 23
			Body,
			// Token: 0x04000018 RID: 24
			Attachment
		}

		// Token: 0x02000008 RID: 8
		internal enum PropertyId
		{
			// Token: 0x0400001A RID: 26
			Subject = 1
		}
	}
}
