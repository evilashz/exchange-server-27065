using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x02000108 RID: 264
	internal class Utility
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x0001D2CC File Offset: 0x0001B4CC
		internal static bool IsBodyContentType(string contentType)
		{
			BodyTypes bodyType = Utility.GetBodyType(contentType);
			return bodyType != BodyTypes.None;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001D2E7 File Offset: 0x0001B4E7
		internal static BodyTypes GetBodyType(string contentType)
		{
			if (contentType == "text/plain")
			{
				return BodyTypes.Text;
			}
			if (contentType == "text/html")
			{
				return BodyTypes.Html;
			}
			if (contentType == "text/enriched")
			{
				return BodyTypes.Enriched;
			}
			if (contentType == "text/calendar")
			{
				return BodyTypes.Calendar;
			}
			return BodyTypes.None;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001D328 File Offset: 0x0001B528
		internal static Charset TranslateWriteStreamCharset(Charset charset)
		{
			if (charset.Kind == CodePageKind.Unicode)
			{
				charset = charset.Culture.MimeCharset;
			}
			else if (charset.CodePage == 20127)
			{
				charset = Charset.GetCharset(28591);
				if (!charset.IsAvailable)
				{
					charset = Charset.GetCharset(1252);
				}
				if (!charset.IsAvailable)
				{
					charset = Charset.ASCII;
				}
			}
			return charset;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001D38C File Offset: 0x0001B58C
		internal static InternalAttachmentType CheckContentDisposition(MimePart part)
		{
			string headerValue = Utility.GetHeaderValue(part, HeaderId.ContentDisposition);
			if (string.IsNullOrEmpty(headerValue) || !headerValue.Equals("inline", StringComparison.OrdinalIgnoreCase))
			{
				return InternalAttachmentType.Regular;
			}
			string headerValue2 = Utility.GetHeaderValue(part, HeaderId.ContentId);
			string headerValue3 = Utility.GetHeaderValue(part, HeaderId.ContentLocation);
			if (string.IsNullOrEmpty(headerValue2) && string.IsNullOrEmpty(headerValue3))
			{
				return InternalAttachmentType.Regular;
			}
			return InternalAttachmentType.Inline;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001D3E0 File Offset: 0x0001B5E0
		internal static string GetHeaderValue(MimePart part, HeaderId headerId)
		{
			Header header = part.Headers.FindFirst(headerId);
			if (header == null)
			{
				return null;
			}
			return Utility.GetHeaderValue(header);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001D408 File Offset: 0x0001B608
		internal static string GetHeaderValue(Header header)
		{
			TextHeader textHeader = header as TextHeader;
			if (textHeader != null)
			{
				DecodingResults decodingResults;
				string result;
				textHeader.TryGetValue(Utility.DecodeOrFallBack, out decodingResults, out result);
				return result;
			}
			if (header != null)
			{
				return header.Value;
			}
			return null;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001D43C File Offset: 0x0001B63C
		internal static string GetParameterValue(ComplexHeader header, string parameterName)
		{
			string result = null;
			MimeParameter mimeParameter = header[parameterName];
			if (mimeParameter != null)
			{
				DecodingResults decodingResults;
				mimeParameter.TryGetValue(Utility.DecodeOrFallBack, out decodingResults, out result);
				return result;
			}
			return result;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001D46C File Offset: 0x0001B66C
		internal static string GetParameterValue(MimePart part, HeaderId headerId, string parameterName)
		{
			string result = null;
			ComplexHeader complexHeader = part.Headers.FindFirst(headerId) as ComplexHeader;
			if (complexHeader != null)
			{
				result = Utility.GetParameterValue(complexHeader, parameterName);
			}
			return result;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001D49C File Offset: 0x0001B69C
		internal static void SetParameterValue(MimePart part, HeaderId headerId, string parameterName, string value)
		{
			ComplexHeader complexHeader = part.Headers.FindFirst(headerId) as ComplexHeader;
			if (complexHeader == null)
			{
				complexHeader = (Header.Create(headerId) as ComplexHeader);
				complexHeader.AppendChild(new MimeParameter(parameterName, value));
				part.Headers.AppendChild(complexHeader);
				return;
			}
			MimeParameter mimeParameter = complexHeader[parameterName];
			if (mimeParameter != null)
			{
				mimeParameter.Value = value;
				return;
			}
			complexHeader.AppendChild(new MimeParameter(parameterName, value));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001D508 File Offset: 0x0001B708
		internal static MimePart CreateBodyPart(string type, string charsetName)
		{
			MimePart mimePart = new MimePart();
			ContentTypeHeader contentTypeHeader = new ContentTypeHeader(type);
			MimeParameter newChild = new MimeParameter("charset", charsetName);
			contentTypeHeader.AppendChild(newChild);
			mimePart.Headers.AppendChild(contentTypeHeader);
			return mimePart;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001D544 File Offset: 0x0001B744
		internal static MimePart GetStartChild(MimePart part)
		{
			string parameterValue = Utility.GetParameterValue(part, HeaderId.ContentType, "start");
			foreach (MimePart mimePart in part)
			{
				if (string.IsNullOrEmpty(parameterValue))
				{
					return mimePart;
				}
				Header header = mimePart.Headers.FindFirst(HeaderId.ContentId);
				if (header != null && Utility.CompareIdentifiers(header.Value, parameterValue))
				{
					return mimePart;
				}
			}
			return part.FirstChild as MimePart;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		internal static bool HasExactlyOneChild(MimePart part)
		{
			return part.IsMultipart && part.FirstChild != null && null == part.FirstChild.NextSibling;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001D5FA File Offset: 0x0001B7FA
		internal static bool HasExactlyTwoChildren(MimePart part)
		{
			return part.IsMultipart && part.FirstChild != null && part.FirstChild.NextSibling != null && null == part.FirstChild.NextSibling.NextSibling;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001D62E File Offset: 0x0001B82E
		internal static bool HasAtLeastTwoChildren(MimePart part)
		{
			return part.IsMultipart && part.FirstChild != null && part.FirstChild.NextSibling != null;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001D650 File Offset: 0x0001B850
		internal static bool Get2047CharsetName(AddressItem addressItem, out string charsetName)
		{
			DecodingOptions decodingOptions = new DecodingOptions(DecodingFlags.Rfc2047, null);
			DecodingResults decodingResults;
			string text;
			if (addressItem.TryGetDisplayName(decodingOptions, out decodingResults, out text) && EncodingScheme.Rfc2047 == decodingResults.EncodingScheme)
			{
				charsetName = decodingResults.CharsetName;
				return true;
			}
			charsetName = null;
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001D68C File Offset: 0x0001B88C
		internal static bool Get2047CharsetName(TextHeader textHeader, out string charsetName)
		{
			DecodingOptions decodingOptions = new DecodingOptions(DecodingFlags.Rfc2047, null);
			DecodingResults decodingResults;
			string text;
			if (textHeader.TryGetValue(decodingOptions, out decodingResults, out text) && EncodingScheme.Rfc2047 == decodingResults.EncodingScheme)
			{
				charsetName = decodingResults.CharsetName;
				return true;
			}
			charsetName = null;
			return false;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001D6C8 File Offset: 0x0001B8C8
		internal static Header FindLastHeader(MimePart part, HeaderId headerId)
		{
			Header result = null;
			for (Header header = part.Headers.FindFirst(headerId); header != null; header = part.Headers.FindNext(header))
			{
				result = header;
			}
			return result;
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		internal static Header FindLastHeader(MimePart part, string name)
		{
			Header result = null;
			for (Header header = part.Headers.FindFirst(name); header != null; header = part.Headers.FindNext(header))
			{
				result = header;
			}
			return result;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001D730 File Offset: 0x0001B930
		internal static bool TryGet822Subject(MimePart mimePart, ref string subject)
		{
			if (mimePart.FirstChild == null)
			{
				return false;
			}
			MimePart mimePart2 = mimePart.FirstChild as MimePart;
			if (mimePart2 == null)
			{
				return false;
			}
			Header header = Utility.FindLastHeader(mimePart2, HeaderId.Subject);
			if (header == null)
			{
				return false;
			}
			string headerValue = Utility.GetHeaderValue(header);
			if (string.IsNullOrEmpty(headerValue))
			{
				return false;
			}
			subject = headerValue;
			return true;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001D77C File Offset: 0x0001B97C
		internal static bool CompareIdentifiers(string a, string b)
		{
			if (Utility.IsBracketed(a) != Utility.IsBracketed(b))
			{
				if (Utility.IsBracketed(a))
				{
					a = a.Substring(1, a.Length - 2);
				}
				if (Utility.IsBracketed(b))
				{
					b = b.Substring(1, b.Length - 2);
				}
			}
			return a.Equals(b, StringComparison.Ordinal);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001D7D1 File Offset: 0x0001B9D1
		private static bool IsBracketed(string str)
		{
			return 2 <= str.Length && str[0] == '<' && str[str.Length - 1] == '>';
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001D7FC File Offset: 0x0001B9FC
		internal static void SynchronizeEncoding(BodyData body, MimePart part)
		{
			Encoding encoding = body.Encoding;
			Encoding encoding2 = Utility.GetEncoding(part);
			if (!encoding.Equals(encoding2))
			{
				Utility.SetEncoding(part, encoding);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001D828 File Offset: 0x0001BA28
		internal static void UpdateTransferEncoding(MimePart part, ContentTransferEncoding embeddedCte)
		{
			if (ContentTransferEncoding.EightBit == embeddedCte || ContentTransferEncoding.Binary == embeddedCte)
			{
				string value = (ContentTransferEncoding.EightBit == embeddedCte) ? "8bit" : "binary";
				do
				{
					ContentTransferEncoding contentTransferEncoding = part.ContentTransferEncoding;
					if (ContentTransferEncoding.Binary != contentTransferEncoding && embeddedCte != contentTransferEncoding)
					{
						Header header = part.Headers.FindFirst(HeaderId.ContentTransferEncoding);
						if (header == null)
						{
							header = Header.Create(HeaderId.ContentTransferEncoding);
							part.Headers.AppendChild(header);
						}
						header.Value = value;
					}
					part = (part.Parent as MimePart);
				}
				while (part != null);
				return;
			}
			Header header2 = part.Headers.FindFirst(HeaderId.ContentTransferEncoding);
			if (header2 != null)
			{
				header2.RemoveFromParent();
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001D8B4 File Offset: 0x0001BAB4
		private static bool IsRestrictedHeader(HeaderId headerId)
		{
			switch (headerId)
			{
			case HeaderId.Unknown:
			case HeaderId.Received:
			case HeaderId.To:
			case HeaderId.Cc:
			case HeaderId.Bcc:
			case HeaderId.Comments:
			case HeaderId.Keywords:
			case HeaderId.ResentDate:
			case HeaderId.ResentSender:
			case HeaderId.ResentFrom:
			case HeaderId.ResentBcc:
			case HeaderId.ResentCc:
			case HeaderId.ResentTo:
			case HeaderId.ResentReplyTo:
			case HeaderId.ResentMessageId:
				return false;
			}
			return true;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001D936 File Offset: 0x0001BB36
		internal static void MoveChildToNewParent(MimeNode newParent, MimeNode child)
		{
			if (newParent == child.Parent)
			{
				return;
			}
			child.RemoveFromParent();
			newParent.AppendChild(child);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001D950 File Offset: 0x0001BB50
		internal static void NormalizeHeaders(MimePart part, Utility.HeaderNormalization normalizationFlags)
		{
			Dictionary<HeaderId, Header> dictionary = new Dictionary<HeaderId, Header>(Utility.HeaderIdComparerInstance);
			foreach (Header header in part.Headers)
			{
				if (!dictionary.ContainsKey(header.HeaderId))
				{
					dictionary.Add(header.HeaderId, header);
				}
				else
				{
					Header header2 = dictionary[header.HeaderId];
					if ((normalizationFlags & Utility.HeaderNormalization.MergeAddressHeaders) != Utility.HeaderNormalization.None && header is AddressHeader)
					{
						foreach (MimeNode child in header)
						{
							Utility.MoveChildToNewParent(header2, child);
						}
						header.RemoveFromParent();
					}
					else if ((normalizationFlags & Utility.HeaderNormalization.PruneRestrictedHeaders) != Utility.HeaderNormalization.None && Utility.IsRestrictedHeader(header.HeaderId))
					{
						header2.RemoveFromParent();
						dictionary[header2.HeaderId] = header;
					}
				}
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001DA50 File Offset: 0x0001BC50
		public static void CopyStream(Stream src, Stream dst, ref byte[] scratchBuffer)
		{
			if (scratchBuffer == null)
			{
				scratchBuffer = new byte[4096];
			}
			int count;
			while (0 < (count = src.Read(scratchBuffer, 0, scratchBuffer.Length)))
			{
				dst.Write(scratchBuffer, 0, count);
			}
			dst.Flush();
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001DA93 File Offset: 0x0001BC93
		internal static Exception InternalError()
		{
			return new Exception("EmailMessage internal error.");
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0001DA9F File Offset: 0x0001BC9F
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string message)
		{
			if (!condition)
			{
				throw new Exception(message);
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0001DAAB File Offset: 0x0001BCAB
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		internal static bool TryGetAppleDoubleParts(MimePart doublePart, out MimePart dataPart, out MimePart resourcePart)
		{
			dataPart = null;
			resourcePart = null;
			if (!Utility.HasExactlyTwoChildren(doublePart))
			{
				return false;
			}
			if (doublePart.ContentType != "multipart/appledouble")
			{
				return false;
			}
			MimePart mimePart = doublePart.FirstChild as MimePart;
			MimePart mimePart2 = doublePart.FirstChild.NextSibling as MimePart;
			if (mimePart.ContentType == "application/applefile" && mimePart2.ContentType != "application/applefile" && !mimePart2.IsMultipart)
			{
				resourcePart = mimePart;
				dataPart = mimePart2;
				return true;
			}
			if (mimePart.ContentType != "application/applefile" && !mimePart.IsMultipart && mimePart2.ContentType == "application/applefile")
			{
				dataPart = mimePart;
				resourcePart = mimePart2;
				return true;
			}
			return false;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001DB68 File Offset: 0x0001BD68
		internal static string GetRawFileName(MimePart part)
		{
			string result = null;
			if (Utility.TryGetFileNameFromHeader(part, HeaderId.ContentDisposition, "filename", ref result))
			{
				return result;
			}
			if (Utility.TryGetFileNameFromHeader(part, HeaderId.ContentType, "name", ref result))
			{
				return result;
			}
			string contentType = part.ContentType;
			if (contentType.Equals("message/rfc822", StringComparison.OrdinalIgnoreCase))
			{
				string result2 = "No Subject";
				Utility.TryGet822Subject(part, ref result2);
				return result2;
			}
			if (contentType.Equals("multipart/appledouble", StringComparison.OrdinalIgnoreCase))
			{
				Utility.TryGetFileNameFromAppleDouble(part, ref result);
				return result;
			}
			if (contentType.Equals("application/applefile", StringComparison.OrdinalIgnoreCase))
			{
				Utility.TryGetFileNameFromAppleFile(part, ref result);
				return result;
			}
			ContentTransferEncoding contentTransferEncoding = part.ContentTransferEncoding;
			if (contentType.Equals("application/mac-binhex40", StringComparison.OrdinalIgnoreCase) || ContentTransferEncoding.BinHex == contentTransferEncoding)
			{
				Utility.TryGetFileNameFromBinHex(part, ref result);
				return result;
			}
			if (ContentTransferEncoding.UUEncode == contentTransferEncoding)
			{
				Utility.TryGetFileNameFromUuencode(part, ref result);
				return result;
			}
			if (Utility.TryGetFileNameFromHeader(part, HeaderId.ContentDescription, ref result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001DC38 File Offset: 0x0001BE38
		internal static Stream GetContentReadStream(MimePart part)
		{
			Stream rawContentReadStream;
			if (!part.TryGetContentReadStream(out rawContentReadStream))
			{
				rawContentReadStream = part.GetRawContentReadStream();
			}
			return rawContentReadStream;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001DC58 File Offset: 0x0001BE58
		private static bool TryGetFileNameFromAppleDouble(MimePart part, ref string fileName)
		{
			MimePart mimePart;
			MimePart part2;
			if (Utility.TryGetAppleDoubleParts(part, out mimePart, out part2))
			{
				if (Utility.TryGetFileNameFromHeader(mimePart, HeaderId.ContentDisposition, "filename", ref fileName) || Utility.TryGetFileNameFromHeader(mimePart, HeaderId.ContentType, "name", ref fileName))
				{
					return true;
				}
				if (Utility.TryGetFileNameFromAppleFile(part2, ref fileName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
		private static bool TryGetFileNameFromAppleFile(MimePart part, ref string fileName)
		{
			string fileNameFromResourceFork;
			using (Stream contentReadStream = Utility.GetContentReadStream(part))
			{
				fileNameFromResourceFork = MimeAppleTranscoder.GetFileNameFromResourceFork(contentReadStream);
			}
			if (string.IsNullOrEmpty(fileNameFromResourceFork))
			{
				return false;
			}
			fileName = fileNameFromResourceFork;
			return true;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
		private static bool TryGetFileNameFromUuencode(MimePart part, ref string fileName)
		{
			byte[] array = new byte[1050];
			int inputSize;
			using (Stream rawContentReadStream = part.GetRawContentReadStream())
			{
				inputSize = rawContentReadStream.Read(array, 0, array.Length);
			}
			bool result;
			using (UUDecoder uudecoder = new UUDecoder())
			{
				byte[] array2 = new byte[1050];
				int num;
				int num2;
				bool flag;
				uudecoder.Convert(array, 0, inputSize, array2, 0, array2.Length, false, out num, out num2, out flag);
				string fileName2 = uudecoder.FileName;
				if (string.IsNullOrEmpty(fileName2))
				{
					result = false;
				}
				else
				{
					fileName = fileName2;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001DD90 File Offset: 0x0001BF90
		internal static bool TryGetFileNameFromHeader(MimePart mimePart, HeaderId headerId, ref string fileName)
		{
			Header header = mimePart.Headers.FindFirst(headerId);
			string headerValue = Utility.GetHeaderValue(header);
			if (string.IsNullOrEmpty(headerValue))
			{
				return false;
			}
			fileName = headerValue;
			return true;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		internal static bool TryGetFileNameFromHeader(MimePart mimePart, HeaderId headerId, string parameterName, ref string fileName)
		{
			string parameterValue = Utility.GetParameterValue(mimePart, headerId, parameterName);
			if (string.IsNullOrEmpty(parameterValue))
			{
				return false;
			}
			fileName = parameterValue;
			return true;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001DDE4 File Offset: 0x0001BFE4
		internal static bool TryGetFileNameFromBinHex(MimePart mimePart, ref string fileName)
		{
			string text;
			using (Stream rawContentReadStream = mimePart.GetRawContentReadStream())
			{
				try
				{
					BinHexDecoder binHexDecoder = new BinHexDecoder();
					using (EncoderStream encoderStream = new EncoderStream(rawContentReadStream, binHexDecoder, EncoderStreamAccess.Read))
					{
						byte[] array = new byte[256];
						encoderStream.Read(array, 0, array.Length);
						text = binHexDecoder.MacBinaryHeader.FileName;
					}
				}
				catch (ByteEncoderException)
				{
					text = null;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			fileName = text;
			return true;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001DE84 File Offset: 0x0001C084
		internal static AttachmentMethod GetAttachmentMethod(TnefPropertyBag properties)
		{
			AttachmentMethod result = AttachmentMethod.AttachByValue;
			object obj = properties[TnefPropertyId.AttachMethod];
			if (obj is int)
			{
				result = (AttachmentMethod)((int)obj);
			}
			return result;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		internal static string GetRawFileName(TnefPropertyBag properties, bool stnef)
		{
			AttachmentMethod attachmentMethod = Utility.GetAttachmentMethod(properties);
			if (attachmentMethod == AttachmentMethod.EmbeddedMessage)
			{
				return properties.GetProperty(TnefPropertyTag.DisplayNameA, stnef) as string;
			}
			string text = properties.GetProperty(TnefPropertyTag.AttachLongFilenameA, stnef) as string;
			if (string.IsNullOrEmpty(text))
			{
				text = (properties.GetProperty(TnefPropertyTag.AttachFilenameA, stnef) as string);
			}
			return text;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001DF08 File Offset: 0x0001C108
		internal static string SanitizeOrRegenerateFileName(string rawName, ref int sequenceNumber)
		{
			string result;
			if (Utility.TrySanitizeAttachmentFileName(rawName, out result))
			{
				return result;
			}
			return Attachment.FileNameGenerator.GenerateFileName(ref sequenceNumber);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001DF54 File Offset: 0x0001C154
		internal static void SetFileName(MimePart attachmentPart, AttachmentType attachmentType, string value)
		{
			TextHeader textHeader = attachmentPart.Headers.FindFirst(HeaderId.ContentDescription) as TextHeader;
			if (textHeader == null)
			{
				textHeader = (Header.Create(HeaderId.ContentDescription) as TextHeader);
				attachmentPart.Headers.AppendChild(textHeader);
			}
			textHeader.Value = value;
			Utility.StoreFileNameInHeader(attachmentPart, HeaderId.ContentType, () => attachmentPart.ContentType, "name", value);
			Utility.StoreFileNameInHeader(attachmentPart, HeaderId.ContentDisposition, delegate
			{
				if (attachmentType != AttachmentType.Inline)
				{
					return "attachment";
				}
				return "inline";
			}, "filename", value);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001DFF4 File Offset: 0x0001C1F4
		internal static void StoreFileNameInHeader(MimePart attachmentPart, HeaderId headerId, GetDefaultValue getDefaultValue, string parameterName, string value)
		{
			ComplexHeader complexHeader = attachmentPart.Headers.FindFirst(headerId) as ComplexHeader;
			if (complexHeader == null)
			{
				complexHeader = (Header.Create(headerId) as ComplexHeader);
				complexHeader.Value = getDefaultValue();
				attachmentPart.Headers.AppendChild(complexHeader);
			}
			MimeParameter mimeParameter = complexHeader[parameterName];
			if (mimeParameter == null)
			{
				mimeParameter = new MimeParameter(parameterName);
				complexHeader.AppendChild(mimeParameter);
			}
			mimeParameter.Value = value;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001E060 File Offset: 0x0001C260
		internal static bool TrySanitizeAttachmentFileName(string rawFileName, out string fileName)
		{
			fileName = rawFileName;
			if (string.IsNullOrEmpty(rawFileName))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(fileName.Length);
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			int num = 0;
			for (int i = 0; i < fileName.Length; i++)
			{
				bool flag = false;
				char c = fileName[i];
				for (int j = 0; j < invalidFileNameChars.Length; j++)
				{
					if (c == invalidFileNameChars[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					stringBuilder.Append(c);
					num++;
				}
			}
			fileName = ((num == fileName.Length) ? fileName : stringBuilder.ToString());
			return !string.IsNullOrEmpty(fileName);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001E100 File Offset: 0x0001C300
		internal static string GetTnefCorrelator(MimePart part)
		{
			Header header = part.Headers.FindFirst("X-MS-TNEF-Correlator");
			return Utility.GetHeaderValue(header);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001E124 File Offset: 0x0001C324
		internal static string RemoveMimeHeaderComments(string headerValue)
		{
			if (headerValue == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(headerValue.Length);
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < headerValue.Length; i++)
			{
				bool flag3 = true;
				if (!flag2)
				{
					char c = headerValue[i];
					if (c != '"')
					{
						switch (c)
						{
						case '(':
							if (!flag)
							{
								num++;
							}
							break;
						case ')':
							if (!flag && num > 0)
							{
								num--;
								flag3 = false;
							}
							break;
						default:
							if (c == '\\')
							{
								flag2 = true;
							}
							break;
						}
					}
					else if (num == 0)
					{
						flag = !flag;
					}
					flag3 &= (num == 0);
				}
				else
				{
					flag2 = false;
				}
				if (flag3)
				{
					stringBuilder.Append(headerValue[i]);
				}
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001E1E4 File Offset: 0x0001C3E4
		private static Encoding GetEncoding(MimePart part)
		{
			string parameterValue = Utility.GetParameterValue(part, HeaderId.ContentType, "charset");
			Charset charset;
			Encoding result;
			if (Charset.TryGetCharset(parameterValue, out charset) && charset.TryGetEncoding(out result))
			{
				return result;
			}
			return Charset.DefaultMimeCharset.GetEncoding();
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001E220 File Offset: 0x0001C420
		private static void SetEncoding(MimePart part, Encoding encoding)
		{
			Charset defaultMimeCharset;
			if (!Charset.TryGetCharset(encoding, out defaultMimeCharset))
			{
				defaultMimeCharset = Charset.DefaultMimeCharset;
			}
			Utility.SetParameterValue(part, HeaderId.ContentType, "charset", defaultMimeCharset.Name);
		}

		// Token: 0x04000466 RID: 1126
		internal static DecodingOptions DecodeOrFallBack = new DecodingOptions((DecodingFlags)131071);

		// Token: 0x04000467 RID: 1127
		private static readonly Utility.HeaderIdComparer HeaderIdComparerInstance = new Utility.HeaderIdComparer();

		// Token: 0x02000109 RID: 265
		[Flags]
		internal enum HeaderNormalization
		{
			// Token: 0x04000469 RID: 1129
			None = 0,
			// Token: 0x0400046A RID: 1130
			PruneRestrictedHeaders = 1,
			// Token: 0x0400046B RID: 1131
			MergeAddressHeaders = 2,
			// Token: 0x0400046C RID: 1132
			All = 255
		}

		// Token: 0x0200010A RID: 266
		private class HeaderIdComparer : IEqualityComparer<HeaderId>
		{
			// Token: 0x06000842 RID: 2114 RVA: 0x0001E273 File Offset: 0x0001C473
			public bool Equals(HeaderId x, HeaderId y)
			{
				return x == y;
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x0001E279 File Offset: 0x0001C479
			public int GetHashCode(HeaderId obj)
			{
				return (int)obj;
			}
		}
	}
}
