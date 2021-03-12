using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data.Mime.Internal
{
	// Token: 0x02000015 RID: 21
	internal sealed class MimeInternalHelpers
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00004364 File Offset: 0x00002564
		public static byte[] GetHeaderRawValue(Header header)
		{
			return header.RawValue;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000436C File Offset: 0x0000256C
		public static void SetHeaderRawValue(Header header, byte[] rawValue)
		{
			header.RawValue = rawValue;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004375 File Offset: 0x00002575
		public static void SetDocumentDecodingOptions(MimeDocument mimeDocument, DecodingOptions decodingOptions)
		{
			mimeDocument.HeaderDecodingOptions = decodingOptions;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000437E File Offset: 0x0000257E
		public static void SetDecodingOptionsDecodingFlags(ref DecodingOptions decodingOptions, DecodingFlags decodingFlags)
		{
			decodingOptions.DecodingFlags = decodingFlags;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004387 File Offset: 0x00002587
		public static long GetDocumentPosition(MimeDocument document)
		{
			return document.Position;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000438F File Offset: 0x0000258F
		public static Stream GetLoadStream(MimeDocument document, bool expectBinaryContent)
		{
			return document.GetLoadStream(expectBinaryContent);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004398 File Offset: 0x00002598
		public static int IndexOf(byte[] buffer, byte val, int offset, int count)
		{
			return ByteString.IndexOf(buffer, val, offset, count);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000043A3 File Offset: 0x000025A3
		public static bool IsValidSmtpAddress(string address, bool checkLength, out int domainStart, bool allowUTF8 = false)
		{
			return MimeAddressParser.IsValidSmtpAddress(address, checkLength, out domainStart, allowUTF8);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000043AE File Offset: 0x000025AE
		public static bool IsValidDomain(string address, int offset, bool checkLength, bool allowUTF8 = false)
		{
			return MimeAddressParser.IsValidDomain(address, offset, checkLength, allowUTF8);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000043BC File Offset: 0x000025BC
		public static bool IsEaiEnabled()
		{
			bool result;
			try
			{
				result = InternalConfiguration.IsEaiEnabled();
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000043E8 File Offset: 0x000025E8
		internal static bool IsEncodingRequired(string value, bool allowUTF8 = false)
		{
			return MimeCommon.IsEncodingRequired(value, allowUTF8);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000043F4 File Offset: 0x000025F4
		public static string DomainFromSmtpAddress(string address)
		{
			int startIndex;
			if (MimeAddressParser.IsValidSmtpAddress(address, false, out startIndex, false))
			{
				return address.Substring(startIndex);
			}
			return null;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004418 File Offset: 0x00002618
		public static string Rfc2047Encode(string value, Encoding encoding)
		{
			EncodingOptions encodingOptions = new EncodingOptions(Charset.GetCharset(encoding));
			return MimeCommon.EncodeValue(value, encodingOptions, ValueEncodingStyle.Normal).ToString();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004448 File Offset: 0x00002648
		public static string Rfc2047Decode(string encodedValue)
		{
			if (encodedValue == null)
			{
				return null;
			}
			if (!encodedValue.Contains("=?"))
			{
				return encodedValue;
			}
			MimeString str = new MimeString(encodedValue.Trim());
			MimeStringList lines = new MimeStringList(str);
			DecodingOptions decodingOptions = new DecodingOptions(DecodingFlags.Rfc2047);
			DecodingResults decodingResults;
			string result;
			if (!MimeCommon.TryDecodeValue(lines, 4026531840U, decodingOptions, out decodingResults, out result))
			{
				return encodedValue;
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000449C File Offset: 0x0000269C
		public static void SetDateHeaderValue(DateHeader header, DateTime value, TimeSpan timeZoneOffset)
		{
			header.SetValue(value, timeZoneOffset);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000044A8 File Offset: 0x000026A8
		public static void CopyHeaderBetweenList(HeaderList sourceHeaderList, HeaderList targetHeaderList, string headerName, bool onlyWriteFirstHeader)
		{
			Header header = sourceHeaderList.FindFirst(headerName);
			while (header != null)
			{
				if (onlyWriteFirstHeader)
				{
					Header header2 = targetHeaderList.FindFirst(headerName);
					if (header2 != null)
					{
						header2.Value = header.Value;
						return;
					}
					targetHeaderList.AppendChild(header.Clone());
					return;
				}
				else
				{
					targetHeaderList.AppendChild(header.Clone());
					header = sourceHeaderList.FindNext(header);
				}
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004500 File Offset: 0x00002700
		public static void CopyHeaderBetweenList(HeaderList sourceHeaderList, HeaderList targetHeaderList, string headerName)
		{
			MimeInternalHelpers.CopyHeaderBetweenList(sourceHeaderList, targetHeaderList, headerName, false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000450B File Offset: 0x0000270B
		public static string BytesToString(byte[] bytes, bool allowUTF8 = true)
		{
			return ByteString.BytesToString(bytes, allowUTF8);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004514 File Offset: 0x00002714
		public static string BytesToString(byte[] bytes, int offset, int count, bool allowUTF8 = true)
		{
			return ByteString.BytesToString(bytes, offset, count, allowUTF8);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000451F File Offset: 0x0000271F
		public static byte[] StringToBytes(string value, bool allowUTF8 = true)
		{
			return ByteString.StringToBytes(value, allowUTF8);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004528 File Offset: 0x00002728
		public static int StringToBytes(string value, int valueOffset, int valueCount, byte[] bytes, int bytesOffset, bool allowUTF8 = true)
		{
			return ByteString.StringToBytes(value, valueOffset, valueCount, bytes, bytesOffset, allowUTF8);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004537 File Offset: 0x00002737
		public static bool IsPureASCII(string value)
		{
			return MimeString.IsPureASCII(value);
		}

		// Token: 0x040000EE RID: 238
		public const int MaxEmailName = 315;

		// Token: 0x040000EF RID: 239
		public const int MaxX400EmailName = 1604;

		// Token: 0x040000F0 RID: 240
		public const int MaxDomainName = 255;

		// Token: 0x040000F1 RID: 241
		public const int MaxInternetName = 571;

		// Token: 0x040000F2 RID: 242
		public const int MaxX400InternetName = 1860;
	}
}
