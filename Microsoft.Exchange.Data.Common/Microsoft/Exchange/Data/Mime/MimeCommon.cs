using System;
using System.Text;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Mime.Encoders;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x0200003B RID: 59
	internal static class MimeCommon
	{
		// Token: 0x0600024B RID: 587 RVA: 0x0000AD1C File Offset: 0x00008F1C
		internal static void CheckBufferArguments(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentException(Strings.LengthExceeded(offset + count, buffer.Length), "buffer");
			}
			if (0 > offset || 0 > count)
			{
				throw new ArgumentOutOfRangeException((offset < 0) ? "offset" : "count");
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AD74 File Offset: 0x00008F74
		internal static bool TryDecodeValue(MimeStringList lines, uint linesMask, DecodingOptions decodingOptions, out DecodingResults decodingResults, out string value)
		{
			decodingResults = default(DecodingResults);
			if (lines.GetLength(linesMask) == 0)
			{
				value = string.Empty;
				return true;
			}
			DecodingFlags decodingFlags = decodingOptions.DecodingFlags;
			bool flag = DecodingFlags.None != (DecodingFlags.FallbackToRaw & decodingFlags);
			bool allowControlCharacters = DecodingFlags.None != (DecodingFlags.AllowControlCharacters & decodingFlags);
			bool enable = false;
			bool enableJisDetection = false;
			bool enableUtf8Detection = false;
			bool enableDbcsDetection = false;
			Charset defaultCharset = null;
			ValueDecoder valueDecoder = new ValueDecoder(lines, linesMask);
			if ((decodingFlags & DecodingFlags.AllEncodings) == DecodingFlags.None)
			{
				if (!flag)
				{
					defaultCharset = Charset.ASCII;
				}
			}
			else
			{
				enable = (DecodingFlags.None != (DecodingFlags.Rfc2047 & decodingFlags));
				enableJisDetection = (DecodingFlags.None != (DecodingFlags.Jis & decodingFlags));
				enableUtf8Detection = (DecodingFlags.None != (DecodingFlags.Utf8 & decodingFlags));
				enableDbcsDetection = (DecodingFlags.None != (DecodingFlags.Dbcs & decodingFlags));
				defaultCharset = decodingOptions.Charset;
			}
			string charsetName;
			string cultureName;
			EncodingScheme encodingScheme;
			bool flag2 = valueDecoder.TryDecodeValue(defaultCharset, flag, allowControlCharacters, enable, enableJisDetection, enableUtf8Detection, enableDbcsDetection, out charsetName, out cultureName, out encodingScheme, out value);
			decodingResults.EncodingScheme = encodingScheme;
			decodingResults.CharsetName = charsetName;
			decodingResults.CultureName = cultureName;
			decodingResults.DecodingFailed = !flag2;
			return flag2;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AE62 File Offset: 0x00009062
		internal static void ThrowDecodingFailedException(ref DecodingResults decodingResults)
		{
			Charset.GetEncoding(decodingResults.CharsetName);
			throw new ExchangeDataException("internal value decoding error");
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AE7A File Offset: 0x0000907A
		public static bool IsAnySurrogate(char ch)
		{
			return '\ud800' <= ch && ch < '';
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AE8E File Offset: 0x0000908E
		public static bool IsHighSurrogate(char ch)
		{
			return '\ud800' <= ch && ch < '\udc00';
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AEA2 File Offset: 0x000090A2
		public static bool IsLowSurrogate(char ch)
		{
			return '\udc00' <= ch && ch < '';
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AEB8 File Offset: 0x000090B8
		internal static bool IsEncodingRequired(string value, bool allowUTF8)
		{
			if (string.IsNullOrEmpty(value))
			{
				return false;
			}
			char[] array = new char[1];
			int num = 0;
			foreach (char c in value)
			{
				if (c < '\u0080')
				{
					if (MimeScan.IsEncodingRequired((byte)c))
					{
						return true;
					}
					if (MimeScan.IsLWSP((byte)c))
					{
						num = 0;
					}
					else
					{
						num++;
					}
				}
				else
				{
					if (!allowUTF8)
					{
						return true;
					}
					array[0] = c;
					num += ByteString.StringToBytesCount(new string(array), allowUTF8);
				}
				if (998 < num + 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000AF40 File Offset: 0x00009140
		internal static MimeStringList EncodeValue(string value, EncodingOptions encodingOptions, ValueEncodingStyle style)
		{
			if (string.IsNullOrEmpty(value))
			{
				return MimeStringList.Empty;
			}
			if (!MimeCommon.IsEncodingRequired(value, encodingOptions.AllowUTF8))
			{
				return new MimeStringList(new MimeString(value));
			}
			MimeStringList result = default(MimeStringList);
			Charset encodingCharset;
			if (encodingOptions.CharsetName != null)
			{
				encodingCharset = encodingOptions.GetEncodingCharset();
			}
			else
			{
				OutboundCodePageDetector outboundCodePageDetector = new OutboundCodePageDetector();
				outboundCodePageDetector.AddText(value);
				int codePage = outboundCodePageDetector.GetCodePage();
				if (!Charset.TryGetCharset(codePage, out encodingCharset))
				{
					encodingCharset = MimeCommon.DefaultEncodingOptions.GetEncodingCharset();
				}
			}
			ByteEncoder.Tables.CharClasses charClasses = ByteEncoder.Tables.CharClasses.QEncode;
			if (style == ValueEncodingStyle.Phrase)
			{
				charClasses |= ByteEncoder.Tables.CharClasses.QPhraseUnsafe;
			}
			else if (style == ValueEncodingStyle.Comment)
			{
				charClasses |= ByteEncoder.Tables.CharClasses.QCommentUnsafe;
			}
			bool allowQEncoding = false;
			MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSize;
			if (encodingCharset.Kind == CodePageKind.Sbcs)
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeSbcs;
				if (encodingCharset.AsciiSupport >= CodePageAsciiSupport.Fine)
				{
					allowQEncoding = true;
				}
			}
			else if (encodingCharset.Kind == CodePageKind.Dbcs)
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeDbcs;
				if (encodingCharset.AsciiSupport >= CodePageAsciiSupport.Fine)
				{
					allowQEncoding = true;
				}
			}
			else if (encodingCharset.CodePage == 65001)
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeUtf8;
				allowQEncoding = true;
			}
			else if (encodingCharset.CodePage == 1200 || encodingCharset.CodePage == 1201)
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeUnicode16;
			}
			else if (encodingCharset.CodePage == 12000 || encodingCharset.CodePage == 12001)
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeUnicode32;
			}
			else
			{
				calculateMethodAndChunkSize = MimeCommon.calculateMethodAndChunkSizeMbcs;
			}
			int num = 75;
			int num2 = 7 + encodingCharset.Name.Length;
			int num3 = num - num2;
			if (num3 < 32)
			{
				num = num2 + 32;
				num3 = 32;
			}
			byte[] byteBuffer = ScratchPad.GetByteBuffer(num3);
			Encoding encoding = encodingCharset.GetEncoding();
			byte[] array = new byte[5 + encodingCharset.Name.Length];
			int num4 = 0;
			array[num4++] = 61;
			array[num4++] = 63;
			num4 += ByteString.StringToBytes(encodingCharset.Name, array, num4, false);
			array[num4++] = 63;
			array[num4++] = 88;
			array[num4++] = 63;
			MimeString mimeString = new MimeString(array);
			int num5 = 0;
			byte[] array2 = null;
			int num6 = 0;
			int num7 = num3 / 4;
			while (num5 != value.Length)
			{
				byte b;
				int num8;
				calculateMethodAndChunkSize(allowQEncoding, charClasses, encoding, value, num5, num3, out b, out num8);
				int bytes;
				int num10;
				for (;;)
				{
					for (;;)
					{
						try
						{
							bytes = encoding.GetBytes(value, num5, num8, byteBuffer, 0);
						}
						catch (ArgumentException)
						{
							if (num8 < 2)
							{
								throw;
							}
							num8 -= ((num8 > 10) ? 3 : 1);
							if (MimeCommon.IsLowSurrogate(value[num5 + num8]) && MimeCommon.IsHighSurrogate(value[num5 + num8 - 1]))
							{
								num8--;
							}
							break;
						}
						if (bytes == 0)
						{
							goto IL_424;
						}
						if (array2 == null || array2.Length - num6 < num + 1)
						{
							if (array2 != null)
							{
								result.Append(new MimeString(array2, 0, num6));
								num6 = 0;
							}
							int val = ((value.Length - num5) / num8 + 1) * (num + 1);
							int num9 = Math.Min(val, 4096 / (num + 1) * (num + 1));
							array2 = new byte[num9];
						}
						num10 = num6;
						if (result.Count > 0 || num10 > 0)
						{
							array2[num10++] = 32;
						}
						num10 += mimeString.CopyTo(array2, num10);
						array2[num10 - 2] = b;
						if (b != 81)
						{
							goto IL_3F5;
						}
						int num11 = num10;
						int num12 = 0;
						while (num12 < bytes && num10 - num11 + 1 <= num3)
						{
							byte b2 = byteBuffer[num12];
							if (MimeCommon.QEncodingRequired((char)b2, charClasses))
							{
								if (num10 - num11 + 3 > num3)
								{
									break;
								}
								array2[num10++] = 61;
								array2[num10++] = ByteEncoder.NibbleToHex[b2 >> 4];
								array2[num10++] = ByteEncoder.NibbleToHex[(int)(b2 & 15)];
							}
							else
							{
								if (b2 == 32)
								{
									b2 = 95;
								}
								array2[num10++] = b2;
							}
							num12++;
						}
						if (num12 == bytes)
						{
							goto IL_408;
						}
						if (num8 < 2)
						{
							goto Block_26;
						}
						num8 -= ((num8 > 10) ? 3 : 1);
						if (MimeCommon.IsLowSurrogate(value[num5 + num8]) && MimeCommon.IsHighSurrogate(value[num5 + num8 - 1]))
						{
							num8--;
						}
					}
				}
				IL_424:
				num5 += num8;
				continue;
				Block_26:
				throw new InvalidOperationException("unexpected thing just happened");
				IL_408:
				array2[num10++] = 63;
				array2[num10++] = 61;
				num6 = num10;
				goto IL_424;
				IL_3F5:
				num10 += MimeCommon.Base64EncodeChunk(byteBuffer, 0, bytes, array2, num10);
				goto IL_408;
			}
			if (array2 != null)
			{
				result.Append(new MimeString(array2, 0, num6));
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B3AC File Offset: 0x000095AC
		internal static int Base64EncodeChunk(byte[] input, int offset, int length, byte[] encodedOutput, int outputOffset)
		{
			int num = 0;
			while (length >= 3)
			{
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[input[offset] >> 2 & 63];
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[((int)input[offset] << 4 | input[offset + 1] >> 4) & 63];
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[((int)input[offset + 1] << 2 | input[offset + 2] >> 6) & 63];
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[(int)(input[offset + 2] & 63)];
				length -= 3;
				offset += 3;
				num += 4;
			}
			if (length > 0)
			{
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[input[offset] >> 2 & 63];
				encodedOutput[outputOffset++] = ByteEncoder.Tables.ByteToBase64[((int)input[offset] << 4 | ((length < 2) ? 0 : (input[offset + 1] >> 4))) & 63];
				encodedOutput[outputOffset++] = ((length < 2) ? 61 : ByteEncoder.Tables.ByteToBase64[(int)input[offset + 1] << 2 & 63]);
				encodedOutput[outputOffset++] = 61;
				num += 4;
			}
			return num;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B4B0 File Offset: 0x000096B0
		private static void CalculateMethodAndChunkSize_Sbcs(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			int num2 = Math.Min(num, value.Length - valueOffset);
			if (num2 != value.Length - valueOffset && MimeCommon.IsHighSurrogate(value[valueOffset + num2 - 1]) && MimeCommon.IsLowSurrogate(value[valueOffset + num2]))
			{
				num2--;
			}
			int num3 = (num2 + 2) / 3 * 4;
			if (!allowQEncoding)
			{
				chunkSize = MimeCommon.AdjustChunkSize(encoding, num2, value, valueOffset, num);
				method = 66;
				return;
			}
			int num4 = 0;
			int num5 = 0;
			int num6 = valueOffset;
			while (num6 != value.Length && num5 < encodedWordSpace)
			{
				int num7 = 1;
				int num8 = 1;
				char ch = value[num6++];
				if (MimeCommon.QEncodingRequired(ch, unsafeCharClassesForQEncoding))
				{
					num8 = 3;
					if (MimeCommon.IsHighSurrogate(ch) && num6 != value.Length && MimeCommon.IsLowSurrogate(value[num6]))
					{
						num6++;
						num7++;
						num8 = 6;
					}
				}
				if (num5 + num8 > encodedWordSpace)
				{
					break;
				}
				num5 += num8;
				num4 += num7;
				if (num5 > num3 && num4 <= num2)
				{
					break;
				}
			}
			if (num4 >= num2 && num5 < num3 + 3)
			{
				chunkSize = num4;
				method = 81;
				return;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, num2, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B5E4 File Offset: 0x000097E4
		private static void CalculateMethodAndChunkSize_Dbcs(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = valueOffset;
			bool flag = false;
			bool flag2 = !allowQEncoding;
			while (num6 != value.Length && (!flag || !flag2))
			{
				int num7 = 1;
				int num8 = 1;
				int num9 = 1;
				char c = value[num6++];
				if (MimeCommon.QEncodingRequired(c, unsafeCharClassesForQEncoding))
				{
					num8 = ((c > '\u007f') ? 6 : 3);
					num9 = ((c > '\u007f') ? 2 : 1);
					if (MimeCommon.IsAnySurrogate(c))
					{
						if (MimeCommon.IsHighSurrogate(c) && num6 != value.Length && MimeCommon.IsLowSurrogate(value[num6]))
						{
							num6++;
							num7++;
						}
						else
						{
							num8 = 3;
							num9 = 1;
						}
					}
				}
				flag = (flag || num5 + num9 > num);
				flag2 = (flag2 || num3 + num8 > encodedWordSpace);
				if (!flag)
				{
					num5 += num9;
					num4 += num7;
				}
				if (!flag2)
				{
					num3 += num8;
					num2 += num7;
				}
			}
			if (allowQEncoding && num2 >= num4 && num3 < num5 + 3)
			{
				chunkSize = num2;
				method = 81;
				return;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, num4, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B710 File Offset: 0x00009910
		private static void CalculateMethodAndChunkSize_Utf8(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = valueOffset;
			bool flag = false;
			bool flag2 = false;
			while (num6 != value.Length && (!flag || !flag2))
			{
				int num7 = 1;
				int num8 = 1;
				int num9 = 1;
				char c = value[num6++];
				if (MimeCommon.QEncodingRequired(c, unsafeCharClassesForQEncoding))
				{
					if (c > '\u007f')
					{
						num9++;
						if (c > '߿')
						{
							num9++;
							if (MimeCommon.IsAnySurrogate(c) && MimeCommon.IsHighSurrogate(c) && num6 != value.Length && MimeCommon.IsLowSurrogate(value[num6]))
							{
								num6++;
								num7++;
								num9++;
							}
						}
					}
					num8 = num9 * 3;
				}
				flag = (flag || num5 + num9 > num);
				flag2 = (flag2 || num3 + num8 > encodedWordSpace);
				if (!flag)
				{
					num5 += num9;
					num4 += num7;
				}
				if (!flag2)
				{
					num3 += num8;
					num2 += num7;
				}
			}
			if (num2 >= num4 && num3 < num5 + 3)
			{
				chunkSize = num2;
				method = 81;
				return;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, num4, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B83C File Offset: 0x00009A3C
		private static void CalculateMethodAndChunkSize_Unicode16(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			chunkSize = Math.Min(num / 2, value.Length - valueOffset);
			if (chunkSize < value.Length - valueOffset && MimeCommon.IsLowSurrogate(value[valueOffset + chunkSize]) && MimeCommon.IsHighSurrogate(value[valueOffset + chunkSize - 1]))
			{
				chunkSize--;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, chunkSize, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		private static void CalculateMethodAndChunkSize_Unicode32(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			chunkSize = Math.Min(num / 4, value.Length - valueOffset);
			if (chunkSize < value.Length - valueOffset && MimeCommon.IsLowSurrogate(value[valueOffset + chunkSize]) && MimeCommon.IsHighSurrogate(value[valueOffset + chunkSize - 1]))
			{
				chunkSize--;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, chunkSize, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B934 File Offset: 0x00009B34
		private static void CalculateMethodAndChunkSize_Mbcs(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize)
		{
			int num = encodedWordSpace / 4 * 3;
			chunkSize = Math.Min(num, value.Length - valueOffset);
			if (chunkSize < value.Length - valueOffset && MimeCommon.IsLowSurrogate(value[valueOffset + chunkSize]) && MimeCommon.IsHighSurrogate(value[valueOffset + chunkSize - 1]))
			{
				chunkSize--;
			}
			chunkSize = MimeCommon.AdjustChunkSize(encoding, chunkSize, value, valueOffset, num);
			method = 66;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B9AC File Offset: 0x00009BAC
		private static int AdjustChunkSize(Encoding encoding, int chunkSize, string value, int valueOffset, int targetBytes)
		{
			int i = MimeCommon.GetByteCount(encoding, value, valueOffset, chunkSize);
			if (i > targetBytes)
			{
				int num = chunkSize * targetBytes / i;
				if (num < chunkSize)
				{
					if (MimeCommon.IsLowSurrogate(value[valueOffset + num]) && MimeCommon.IsHighSurrogate(value[valueOffset + num - 1]))
					{
						num--;
					}
					chunkSize = num;
					i = MimeCommon.GetByteCount(encoding, value, valueOffset, chunkSize);
				}
				while (i > targetBytes)
				{
					num = chunkSize * targetBytes / i;
					if (num < chunkSize)
					{
						chunkSize = num;
					}
					else
					{
						chunkSize--;
					}
					if (MimeCommon.IsLowSurrogate(value[valueOffset + chunkSize]) && MimeCommon.IsHighSurrogate(value[valueOffset + chunkSize - 1]))
					{
						chunkSize--;
					}
					i = MimeCommon.GetByteCount(encoding, value, valueOffset, chunkSize);
				}
			}
			if (i < targetBytes - 2 && i != 0)
			{
				int num2 = 0;
				while (valueOffset + chunkSize < value.Length && num2++ < 3)
				{
					int num3 = 1;
					if (MimeCommon.IsHighSurrogate(value[valueOffset + chunkSize]) && valueOffset + chunkSize + 1 < value.Length && MimeCommon.IsLowSurrogate(value[valueOffset + chunkSize + 1]))
					{
						num3++;
					}
					i = MimeCommon.GetByteCount(encoding, value, valueOffset, chunkSize + num3);
					if (i > targetBytes)
					{
						break;
					}
					chunkSize += num3;
				}
			}
			return chunkSize;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000BAC3 File Offset: 0x00009CC3
		private static bool QEncodingRequired(char ch, ByteEncoder.Tables.CharClasses unsafeCharClasses)
		{
			return ch >= '\u0080' || 0 != (byte)(ByteEncoder.Tables.CharacterTraits[(int)ch] & unsafeCharClasses);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		internal static LineTerminationState AdvanceLineTerminationState(LineTerminationState previousLineTerminationState, byte[] data, int offset, int count)
		{
			if (count == 0)
			{
				return previousLineTerminationState;
			}
			byte b = data[offset + count - 1];
			if (b == 13)
			{
				return LineTerminationState.CR;
			}
			if (b != 10)
			{
				return LineTerminationState.Other;
			}
			if (count >= 2)
			{
				if (data[offset + count - 2] == 13)
				{
					return LineTerminationState.CRLF;
				}
				return LineTerminationState.Other;
			}
			else
			{
				if (previousLineTerminationState == LineTerminationState.CR)
				{
					return LineTerminationState.CRLF;
				}
				return LineTerminationState.Other;
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000BB24 File Offset: 0x00009D24
		private unsafe static int GetByteCount(Encoding encoding, string value, int offset, int size)
		{
			int byteCount;
			fixed (char* ptr = value)
			{
				byteCount = encoding.GetByteCount(ptr + offset, size);
			}
			return byteCount;
		}

		// Token: 0x040001B0 RID: 432
		public const int FoldLineLength = 78;

		// Token: 0x040001B1 RID: 433
		public const int MaxMimeLineLength = 998;

		// Token: 0x040001B2 RID: 434
		public const int MaxBoundaryLength = 70;

		// Token: 0x040001B3 RID: 435
		public const int BoundaryPrefixSuffixLength = 2;

		// Token: 0x040001B4 RID: 436
		public const int MaxRfc2231SegmentsAllowed = 10000;

		// Token: 0x040001B5 RID: 437
		internal static readonly EncodingOptions DefaultEncodingOptions = new EncodingOptions(Encoding.UTF8.WebName, null, EncodingFlags.None);

		// Token: 0x040001B6 RID: 438
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeSbcs = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Sbcs);

		// Token: 0x040001B7 RID: 439
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeDbcs = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Dbcs);

		// Token: 0x040001B8 RID: 440
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeUtf8 = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Utf8);

		// Token: 0x040001B9 RID: 441
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeUnicode16 = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Unicode16);

		// Token: 0x040001BA RID: 442
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeUnicode32 = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Unicode32);

		// Token: 0x040001BB RID: 443
		private static MimeCommon.CalculateMethodAndChunkSize calculateMethodAndChunkSizeMbcs = new MimeCommon.CalculateMethodAndChunkSize(MimeCommon.CalculateMethodAndChunkSize_Mbcs);

		// Token: 0x0200003C RID: 60
		// (Invoke) Token: 0x06000260 RID: 608
		private delegate void CalculateMethodAndChunkSize(bool allowQEncoding, ByteEncoder.Tables.CharClasses unsafeCharClassesForQEncoding, Encoding encoding, string value, int valueOffset, int encodedWordSpace, out byte method, out int chunkSize);
	}
}
