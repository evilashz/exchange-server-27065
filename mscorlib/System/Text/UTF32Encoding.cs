using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A56 RID: 2646
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class UTF32Encoding : Encoding
	{
		// Token: 0x06006818 RID: 26648 RVA: 0x001637AD File Offset: 0x001619AD
		[__DynamicallyInvokable]
		public UTF32Encoding() : this(false, true, false)
		{
		}

		// Token: 0x06006819 RID: 26649 RVA: 0x001637B8 File Offset: 0x001619B8
		[__DynamicallyInvokable]
		public UTF32Encoding(bool bigEndian, bool byteOrderMark) : this(bigEndian, byteOrderMark, false)
		{
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x001637C3 File Offset: 0x001619C3
		[__DynamicallyInvokable]
		public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters) : base(bigEndian ? 12001 : 12000)
		{
			this.bigEndian = bigEndian;
			this.emitUTF32ByteOrderMark = byteOrderMark;
			this.isThrowException = throwOnInvalidCharacters;
			if (this.isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x00163800 File Offset: 0x00161A00
		internal override void SetDefaultFallbacks()
		{
			if (this.isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("�");
			this.decoderFallback = new DecoderReplacementFallback("�");
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x0016384C File Offset: 0x00161A4C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			fixed (char* ptr = chars)
			{
				return this.GetByteCount(ptr + index, count, null);
			}
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x001638E4 File Offset: 0x00161AE4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, s.Length, null);
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x0016391D File Offset: 0x00161B1D
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetByteCount(chars, count, null);
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x0016395C File Offset: 0x00161B5C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			fixed (byte* ptr2 = bytes)
			{
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
			}
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x00163A50 File Offset: 0x00161C50
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (chars.Length == 0)
			{
				return 0;
			}
			int byteCount = bytes.Length - byteIndex;
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			fixed (char* ptr = chars)
			{
				fixed (byte* ptr2 = bytes)
				{
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
				}
			}
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x00163B4C File Offset: 0x00161D4C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x00163BBC File Offset: 0x00161DBC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			fixed (byte* ptr = bytes)
			{
				return this.GetCharCount(ptr + index, count, null);
			}
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x00163C4F File Offset: 0x00161E4F
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetCharCount(bytes, count, null);
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x00163C90 File Offset: 0x00161E90
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (bytes.Length == 0)
			{
				return 0;
			}
			int charCount = chars.Length - charIndex;
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			fixed (byte* ptr = bytes)
			{
				fixed (char* ptr2 = chars)
				{
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, null);
				}
			}
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x00163D8C File Offset: 0x00161F8C
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x00163DFC File Offset: 0x00161FFC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = bytes)
			{
				return string.CreateStringFromEncoding(ptr + index, count, this);
			}
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x00163E94 File Offset: 0x00162094
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			char* ptr = chars + count;
			char* charStart = chars;
			int num = 0;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoderFallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
					{
						this.EncodingName,
						encoder.Fallback.GetType()
					}));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
			for (;;)
			{
				char c2;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) == '\0' && chars >= ptr)
				{
					if ((encoder != null && !encoder.MustFlush) || c <= '\0')
					{
						break;
					}
					encoderFallbackBuffer.InternalFallback(c, ref chars);
					c = '\0';
				}
				else
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (char.IsLowSurrogate(c2))
						{
							c = '\0';
							num += 4;
						}
						else
						{
							chars--;
							encoderFallbackBuffer.InternalFallback(c, ref chars);
							c = '\0';
						}
					}
					else if (char.IsHighSurrogate(c2))
					{
						c = c2;
					}
					else if (char.IsLowSurrogate(c2))
					{
						encoderFallbackBuffer.InternalFallback(c2, ref chars);
					}
					else
					{
						num += 4;
					}
				}
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return num;
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x00163FBC File Offset: 0x001621BC
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoder.m_throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
					{
						this.EncodingName,
						encoder.Fallback.GetType()
					}));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, true);
			for (;;)
			{
				char c2;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (!char.IsLowSurrogate(c2))
						{
							chars--;
							encoderFallbackBuffer.InternalFallback(c, ref chars);
							c = '\0';
							continue;
						}
						uint surrogate = this.GetSurrogate(c, c2);
						c = '\0';
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars -= 2;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
							c = '\0';
						}
						else
						{
							if (this.bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = (byte)(surrogate >> 16);
								*(bytes++) = (byte)(surrogate >> 8);
								*(bytes++) = (byte)surrogate;
								continue;
							}
							*(bytes++) = (byte)surrogate;
							*(bytes++) = (byte)(surrogate >> 8);
							*(bytes++) = (byte)(surrogate >> 16);
							*(bytes++) = 0;
							continue;
						}
					}
					else
					{
						if (char.IsHighSurrogate(c2))
						{
							c = c2;
							continue;
						}
						if (char.IsLowSurrogate(c2))
						{
							encoderFallbackBuffer.InternalFallback(c2, ref chars);
							continue;
						}
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars--;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
						}
						else
						{
							if (this.bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = 0;
								*(bytes++) = (byte)(c2 >> 8);
								*(bytes++) = (byte)c2;
								continue;
							}
							*(bytes++) = (byte)c2;
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = 0;
							*(bytes++) = 0;
							continue;
						}
					}
				}
				if ((encoder != null && !encoder.MustFlush) || c <= '\0')
				{
					break;
				}
				encoderFallbackBuffer.InternalFallback(c, ref chars);
				c = '\0';
			}
			if (encoder != null)
			{
				encoder.charLeftOver = c;
				encoder.m_charsUsed = (int)((long)(chars - ptr));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x0016424C File Offset: 0x0016244C
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			int num = 0;
			byte* ptr = bytes + count;
			byte* byteStart = bytes;
			int i = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				i = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = utf32Decoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(byteStart, null);
			while (bytes < ptr && num >= 0)
			{
				if (this.bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				i++;
				if (i >= 4)
				{
					i = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] bytes2;
						if (this.bigEndian)
						{
							bytes2 = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							bytes2 = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						num += decoderFallbackBuffer.InternalFallback(bytes2, bytes);
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							num++;
						}
						num++;
						num2 = 0U;
					}
				}
			}
			if (i > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array = new byte[i];
				if (this.bigEndian)
				{
					while (i > 0)
					{
						array[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				num += decoderFallbackBuffer.InternalFallback(array, bytes);
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return num;
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00164414 File Offset: 0x00162614
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			int num = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				num = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = baseDecoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(bytes, chars + charCount);
			while (bytes < ptr4)
			{
				if (this.bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				num++;
				if (num >= 4)
				{
					num = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] bytes2;
						if (this.bigEndian)
						{
							bytes2 = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							bytes2 = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						if (!decoderFallbackBuffer.InternalFallback(bytes2, bytes, ref chars))
						{
							bytes -= 4;
							num2 = 0U;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 4;
								num2 = 0U;
								base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
								break;
							}
							*(chars++) = this.GetHighSurrogate(num2);
							num2 = (uint)this.GetLowSurrogate(num2);
						}
						else if (chars >= ptr2)
						{
							bytes -= 4;
							num2 = 0U;
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						*(chars++) = (char)num2;
						num2 = 0U;
					}
				}
			}
			if (num > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array = new byte[num];
				int i = num;
				if (this.bigEndian)
				{
					while (i > 0)
					{
						array[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				if (!decoderFallbackBuffer.InternalFallback(array, bytes, ref chars))
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
				}
				else
				{
					num = 0;
					num2 = 0U;
				}
			}
			if (utf32Decoder != null)
			{
				utf32Decoder.iChar = (int)num2;
				utf32Decoder.readByteCount = num;
				utf32Decoder.m_bytesUsed = (int)((long)(bytes - ptr3));
			}
			return (int)((long)(chars - ptr));
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x00164686 File Offset: 0x00162886
		private uint GetSurrogate(char cHigh, char cLow)
		{
			return (uint)((cHigh - '\ud800') * 'Ѐ' + (cLow - '\udc00')) + 65536U;
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x001646A3 File Offset: 0x001628A3
		private char GetHighSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) / 1024U + 55296U);
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x001646B9 File Offset: 0x001628B9
		private char GetLowSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) % 1024U + 56320U);
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x001646CF File Offset: 0x001628CF
		[__DynamicallyInvokable]
		public override Decoder GetDecoder()
		{
			return new UTF32Encoding.UTF32Decoder(this);
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x001646D7 File Offset: 0x001628D7
		[__DynamicallyInvokable]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x001646E0 File Offset: 0x001628E0
		[__DynamicallyInvokable]
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 4L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x00164750 File Offset: 0x00162950
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			int num = byteCount / 2 + 2;
			if (base.DecoderFallback.MaxCharCount > 2)
			{
				num *= base.DecoderFallback.MaxCharCount;
				num /= 2;
			}
			if (num > 2147483647)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
			}
			return num;
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x001647BC File Offset: 0x001629BC
		[__DynamicallyInvokable]
		public override byte[] GetPreamble()
		{
			if (!this.emitUTF32ByteOrderMark)
			{
				return EmptyArray<byte>.Value;
			}
			if (this.bigEndian)
			{
				return new byte[]
				{
					0,
					0,
					254,
					byte.MaxValue
				};
			}
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x0016480C File Offset: 0x00162A0C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			UTF32Encoding utf32Encoding = value as UTF32Encoding;
			return utf32Encoding != null && (this.emitUTF32ByteOrderMark == utf32Encoding.emitUTF32ByteOrderMark && this.bigEndian == utf32Encoding.bigEndian && base.EncoderFallback.Equals(utf32Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf32Encoding.DecoderFallback);
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x00164867 File Offset: 0x00162A67
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + this.CodePage + (this.emitUTF32ByteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
		}

		// Token: 0x04002E65 RID: 11877
		private bool emitUTF32ByteOrderMark;

		// Token: 0x04002E66 RID: 11878
		private bool isThrowException;

		// Token: 0x04002E67 RID: 11879
		private bool bigEndian;

		// Token: 0x02000C8C RID: 3212
		[Serializable]
		internal class UTF32Decoder : DecoderNLS
		{
			// Token: 0x06007091 RID: 28817 RVA: 0x001825F9 File Offset: 0x001807F9
			public UTF32Decoder(UTF32Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06007092 RID: 28818 RVA: 0x00182602 File Offset: 0x00180802
			public override void Reset()
			{
				this.iChar = 0;
				this.readByteCount = 0;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x17001364 RID: 4964
			// (get) Token: 0x06007093 RID: 28819 RVA: 0x00182625 File Offset: 0x00180825
			internal override bool HasState
			{
				get
				{
					return this.readByteCount != 0;
				}
			}

			// Token: 0x040037EF RID: 14319
			internal int iChar;

			// Token: 0x040037F0 RID: 14320
			internal int readByteCount;
		}
	}
}
