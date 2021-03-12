using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A2D RID: 2605
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class ASCIIEncoding : Encoding
	{
		// Token: 0x0600661D RID: 26141 RVA: 0x001576FF File Offset: 0x001558FF
		[__DynamicallyInvokable]
		public ASCIIEncoding() : base(20127)
		{
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x0015770C File Offset: 0x0015590C
		internal override void SetDefaultFallbacks()
		{
			this.encoderFallback = EncoderFallback.ReplacementFallback;
			this.decoderFallback = DecoderFallback.ReplacementFallback;
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x00157724 File Offset: 0x00155924
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

		// Token: 0x06006620 RID: 26144 RVA: 0x001577BC File Offset: 0x001559BC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetByteCount(string chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars");
			}
			char* ptr = chars;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return this.GetByteCount(ptr, chars.Length, null);
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x001577F5 File Offset: 0x001559F5
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
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

		// Token: 0x06006622 RID: 26146 RVA: 0x00157834 File Offset: 0x00155A34
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
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
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCount"));
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
			char* ptr = chars;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			fixed (byte* ptr2 = bytes)
			{
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
			}
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x00157928 File Offset: 0x00155B28
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

		// Token: 0x06006624 RID: 26148 RVA: 0x00157A24 File Offset: 0x00155C24
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
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

		// Token: 0x06006625 RID: 26149 RVA: 0x00157A94 File Offset: 0x00155C94
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

		// Token: 0x06006626 RID: 26150 RVA: 0x00157B27 File Offset: 0x00155D27
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
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

		// Token: 0x06006627 RID: 26151 RVA: 0x00157B68 File Offset: 0x00155D68
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

		// Token: 0x06006628 RID: 26152 RVA: 0x00157C64 File Offset: 0x00155E64
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
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

		// Token: 0x06006629 RID: 26153 RVA: 0x00157CD4 File Offset: 0x00155ED4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe override string GetString(byte[] bytes, int byteIndex, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			fixed (byte* ptr = bytes)
			{
				return string.CreateStringFromEncoding(ptr + byteIndex, byteCount, this);
			}
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x00157D6C File Offset: 0x00155F6C
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int charCount, EncoderNLS encoder)
		{
			char c = '\0';
			char* ptr = chars + charCount;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			EncoderReplacementFallback encoderReplacementFallback;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				encoderReplacementFallback = (encoder.Fallback as EncoderReplacementFallback);
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, false);
				}
			}
			else
			{
				encoderReplacementFallback = (base.EncoderFallback as EncoderReplacementFallback);
			}
			if (encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1)
			{
				if (c > '\0')
				{
					charCount++;
				}
				return charCount;
			}
			int num = 0;
			if (c > '\0')
			{
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, false);
				encoderFallbackBuffer.InternalFallback(c, ref chars);
			}
			char c2;
			while ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c2 == '\0')
				{
					c2 = *chars;
					chars++;
				}
				if (c2 > '\u007f')
				{
					if (encoderFallbackBuffer == null)
					{
						if (encoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = encoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(ptr - charCount, ptr, encoder, false);
					}
					encoderFallbackBuffer.InternalFallback(c2, ref chars);
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600662B RID: 26155 RVA: 0x00157EA4 File Offset: 0x001560A4
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			char* ptr = chars + charCount;
			byte* ptr2 = bytes;
			char* ptr3 = chars;
			EncoderReplacementFallback encoderReplacementFallback;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				encoderReplacementFallback = (encoder.Fallback as EncoderReplacementFallback);
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && encoder.m_throwOnOverflow)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(ptr3, ptr, encoder, true);
				}
			}
			else
			{
				encoderReplacementFallback = (base.EncoderFallback as EncoderReplacementFallback);
			}
			if (encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1)
			{
				char c2 = encoderReplacementFallback.DefaultString[0];
				if (c2 <= '\u007f')
				{
					if (c > '\0')
					{
						if (byteCount == 0)
						{
							base.ThrowBytesOverflow(encoder, true);
						}
						*(bytes++) = (byte)c2;
						byteCount--;
					}
					if (byteCount < charCount)
					{
						base.ThrowBytesOverflow(encoder, byteCount < 1);
						ptr = chars + byteCount;
					}
					while (chars < ptr)
					{
						char c3 = *(chars++);
						if (c3 >= '\u0080')
						{
							*(bytes++) = (byte)c2;
						}
						else
						{
							*(bytes++) = (byte)c3;
						}
					}
					if (encoder != null)
					{
						encoder.charLeftOver = '\0';
						encoder.m_charsUsed = (int)((long)(chars - ptr3));
					}
					return (int)((long)(bytes - ptr2));
				}
			}
			byte* ptr4 = bytes + byteCount;
			if (c > '\0')
			{
				encoderFallbackBuffer = encoder.FallbackBuffer;
				encoderFallbackBuffer.InternalInitialize(chars, ptr, encoder, true);
				encoderFallbackBuffer.InternalFallback(c, ref chars);
			}
			char c4;
			while ((c4 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
			{
				if (c4 == '\0')
				{
					c4 = *chars;
					chars++;
				}
				if (c4 > '\u007f')
				{
					if (encoderFallbackBuffer == null)
					{
						if (encoder == null)
						{
							encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
						}
						else
						{
							encoderFallbackBuffer = encoder.FallbackBuffer;
						}
						encoderFallbackBuffer.InternalInitialize(ptr - charCount, ptr, encoder, true);
					}
					encoderFallbackBuffer.InternalFallback(c4, ref chars);
				}
				else
				{
					if (bytes >= ptr4)
					{
						if (encoderFallbackBuffer == null || !encoderFallbackBuffer.bFallingBack)
						{
							chars--;
						}
						else
						{
							encoderFallbackBuffer.MovePrevious();
						}
						base.ThrowBytesOverflow(encoder, bytes == ptr2);
						break;
					}
					*bytes = (byte)c4;
					bytes++;
				}
			}
			if (encoder != null)
			{
				if (encoderFallbackBuffer != null && !encoderFallbackBuffer.bUsedEncoder)
				{
					encoder.charLeftOver = '\0';
				}
				encoder.m_charsUsed = (int)((long)(chars - ptr3));
			}
			return (int)((long)(bytes - ptr2));
		}

		// Token: 0x0600662C RID: 26156 RVA: 0x001580FC File Offset: 0x001562FC
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			DecoderReplacementFallback decoderReplacementFallback;
			if (decoder == null)
			{
				decoderReplacementFallback = (base.DecoderFallback as DecoderReplacementFallback);
			}
			else
			{
				decoderReplacementFallback = (decoder.Fallback as DecoderReplacementFallback);
			}
			if (decoderReplacementFallback != null && decoderReplacementFallback.MaxCharCount == 1)
			{
				return count;
			}
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			int num = count;
			byte[] array = new byte[1];
			byte* ptr = bytes + count;
			while (bytes < ptr)
			{
				byte b = *bytes;
				bytes++;
				if (b >= 128)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = base.DecoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr - count, null);
					}
					array[0] = b;
					num--;
					num += decoderFallbackBuffer.InternalFallback(array, bytes);
				}
			}
			return num;
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x0015819C File Offset: 0x0015639C
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			byte* ptr = bytes + byteCount;
			byte* ptr2 = bytes;
			char* ptr3 = chars;
			DecoderReplacementFallback decoderReplacementFallback;
			if (decoder == null)
			{
				decoderReplacementFallback = (base.DecoderFallback as DecoderReplacementFallback);
			}
			else
			{
				decoderReplacementFallback = (decoder.Fallback as DecoderReplacementFallback);
			}
			if (decoderReplacementFallback != null && decoderReplacementFallback.MaxCharCount == 1)
			{
				char c = decoderReplacementFallback.DefaultString[0];
				if (charCount < byteCount)
				{
					base.ThrowCharsOverflow(decoder, charCount < 1);
					ptr = bytes + charCount;
				}
				while (bytes < ptr)
				{
					byte b = *(bytes++);
					if (b >= 128)
					{
						*(chars++) = c;
					}
					else
					{
						*(chars++) = (char)b;
					}
				}
				if (decoder != null)
				{
					decoder.m_bytesUsed = (int)((long)(bytes - ptr2));
				}
				return (int)((long)(chars - ptr3));
			}
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			byte[] array = new byte[1];
			char* ptr4 = chars + charCount;
			while (bytes < ptr)
			{
				byte b2 = *bytes;
				bytes++;
				if (b2 >= 128)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = base.DecoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr - byteCount, ptr4);
					}
					array[0] = b2;
					if (!decoderFallbackBuffer.InternalFallback(array, bytes, ref chars))
					{
						bytes--;
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr3);
						break;
					}
				}
				else
				{
					if (chars >= ptr4)
					{
						bytes--;
						base.ThrowCharsOverflow(decoder, chars == ptr3);
						break;
					}
					*chars = (char)b2;
					chars++;
				}
			}
			if (decoder != null)
			{
				decoder.m_bytesUsed = (int)((long)(bytes - ptr2));
			}
			return (int)((long)(chars - ptr3));
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x00158310 File Offset: 0x00156510
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
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x0015837C File Offset: 0x0015657C
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)byteCount;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_GetCharCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06006630 RID: 26160 RVA: 0x001583E2 File Offset: 0x001565E2
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override bool IsSingleByte
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x06006631 RID: 26161 RVA: 0x001583E5 File Offset: 0x001565E5
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06006632 RID: 26162 RVA: 0x001583ED File Offset: 0x001565ED
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}
	}
}
