using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A53 RID: 2643
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UnicodeEncoding : Encoding
	{
		// Token: 0x060067C2 RID: 26562 RVA: 0x0015F55B File Offset: 0x0015D75B
		[__DynamicallyInvokable]
		public UnicodeEncoding() : this(false, true)
		{
		}

		// Token: 0x060067C3 RID: 26563 RVA: 0x0015F565 File Offset: 0x0015D765
		[__DynamicallyInvokable]
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark) : this(bigEndian, byteOrderMark, false)
		{
		}

		// Token: 0x060067C4 RID: 26564 RVA: 0x0015F570 File Offset: 0x0015D770
		[__DynamicallyInvokable]
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes) : base(bigEndian ? 1201 : 1200)
		{
			this.isThrowException = throwOnInvalidBytes;
			this.bigEndian = bigEndian;
			this.byteOrderMark = byteOrderMark;
			if (this.isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x060067C5 RID: 26565 RVA: 0x0015F5BC File Offset: 0x0015D7BC
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.isThrowException = false;
		}

		// Token: 0x060067C6 RID: 26566 RVA: 0x0015F5C8 File Offset: 0x0015D7C8
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

		// Token: 0x060067C7 RID: 26567 RVA: 0x0015F614 File Offset: 0x0015D814
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

		// Token: 0x060067C8 RID: 26568 RVA: 0x0015F6AC File Offset: 0x0015D8AC
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

		// Token: 0x060067C9 RID: 26569 RVA: 0x0015F6E5 File Offset: 0x0015D8E5
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

		// Token: 0x060067CA RID: 26570 RVA: 0x0015F724 File Offset: 0x0015D924
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

		// Token: 0x060067CB RID: 26571 RVA: 0x0015F818 File Offset: 0x0015DA18
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

		// Token: 0x060067CC RID: 26572 RVA: 0x0015F914 File Offset: 0x0015DB14
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

		// Token: 0x060067CD RID: 26573 RVA: 0x0015F984 File Offset: 0x0015DB84
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

		// Token: 0x060067CE RID: 26574 RVA: 0x0015FA17 File Offset: 0x0015DC17
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

		// Token: 0x060067CF RID: 26575 RVA: 0x0015FA58 File Offset: 0x0015DC58
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

		// Token: 0x060067D0 RID: 26576 RVA: 0x0015FB54 File Offset: 0x0015DD54
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

		// Token: 0x060067D1 RID: 26577 RVA: 0x0015FBC4 File Offset: 0x0015DDC4
		[SecuritySafeCritical]
		[ComVisible(false)]
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

		// Token: 0x060067D2 RID: 26578 RVA: 0x0015FC5C File Offset: 0x0015DE5C
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			int num = count << 1;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			char* charStart = chars;
			char* ptr = chars + count;
			char c = '\0';
			bool flag = false;
			ulong* ptr2 = (ulong*)(ptr - 3);
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
				if (c > '\0')
				{
					num += 2;
				}
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.EncodingName,
							encoder.Fallback.GetType()
						}));
					}
					encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
				{
					if (c2 == '\0')
					{
						if (!this.bigEndian && c == '\0' && (chars & 7L) == null)
						{
							ulong* ptr3;
							for (ptr3 = (ulong*)chars; ptr3 < ptr2; ptr3++)
							{
								if ((9223512776490647552UL & *ptr3) != 0UL)
								{
									ulong num2 = (17870556004450629632UL & *ptr3) ^ 15564677810327967744UL;
									if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)-65536) == 0UL || (num2 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr3) ^ 15852908186546788352UL) != 0UL)
									{
										break;
									}
								}
							}
							chars = (char*)ptr3;
							if (chars >= ptr)
							{
								goto IL_278;
							}
						}
						c2 = *chars;
						chars++;
					}
					else
					{
						num += 2;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
								num -= 2;
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
									encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
								}
								encoderFallbackBuffer.InternalFallback(c, ref chars);
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
							{
								num -= 2;
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
									encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
								}
								encoderFallbackBuffer.InternalFallback(c2, ref chars);
								continue;
							}
							c = '\0';
							continue;
						}
					}
					else
					{
						if (c > '\0')
						{
							chars--;
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
								encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
							}
							encoderFallbackBuffer.InternalFallback(c, ref chars);
							num -= 2;
							c = '\0';
							continue;
						}
						continue;
					}
				}
				IL_278:
				if (c <= '\0')
				{
					return num;
				}
				num -= 2;
				if (encoder != null && !encoder.MustFlush)
				{
					return num;
				}
				if (flag)
				{
					break;
				}
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
					encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
				}
				encoderFallbackBuffer.InternalFallback(c, ref chars);
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[]
			{
				c
			}), "chars");
		}

		// Token: 0x060067D3 RID: 26579 RVA: 0x0015FF5C File Offset: 0x0015E15C
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char c = '\0';
			bool flag = false;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder.charLeftOver;
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
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						if (!this.bigEndian && (chars & 7L) == null && (bytes & 7L) == null && c == '\0')
						{
							ulong* ptr5 = (ulong*)(chars - 3 + (((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes) >> 1) : ((long)(ptr2 - chars))) * 2L / 8L);
							ulong* ptr6 = (ulong*)chars;
							ulong* ptr7 = (ulong*)bytes;
							while (ptr6 < ptr5)
							{
								if ((9223512776490647552UL & *ptr6) != 0UL)
								{
									ulong num = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
									if (((num & 18446462598732840960UL) == 0UL || (num & 281470681743360UL) == 0UL || (num & (ulong)-65536) == 0UL || (num & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr6) ^ 15852908186546788352UL) != 0UL)
									{
										break;
									}
								}
								*ptr7 = *ptr6;
								ptr6++;
								ptr7++;
							}
							chars = (char*)ptr6;
							bytes = (byte*)ptr7;
							if (chars >= ptr2)
							{
								goto IL_477;
							}
						}
						else if (c == '\0' && !this.bigEndian && ((byte*)chars & 7L) != (bytes & 7L) && (bytes & 1) == 0)
						{
							long num2 = ((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes) >> 1) : ((long)(ptr2 - chars));
							char* ptr8 = (char*)bytes;
							char* ptr9 = chars + num2 * 2L / 2L - 1;
							while (chars < ptr9)
							{
								if (*chars >= '\ud800' && *chars <= '\udfff')
								{
									if (*chars >= '\udc00' || chars[1] < '\udc00')
									{
										break;
									}
									if (chars[1] > '\udfff')
									{
										break;
									}
								}
								else if (chars[1] >= '\ud800' && chars[1] <= '\udfff')
								{
									*ptr8 = *chars;
									ptr8++;
									chars++;
									continue;
								}
								*ptr8 = *chars;
								ptr8[1] = chars[1];
								ptr8 += 2;
								chars += 2;
							}
							bytes = (byte*)ptr8;
							if (chars >= ptr2)
							{
								goto IL_477;
							}
						}
						c2 = *chars;
						chars++;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
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
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								encoderFallbackBuffer.InternalFallback(c, ref chars);
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
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
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								encoderFallbackBuffer.InternalFallback(c2, ref chars);
								continue;
							}
							if (bytes + 3 >= ptr)
							{
								if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
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
								goto IL_477;
							}
							if (this.bigEndian)
							{
								*(bytes++) = (byte)(c >> 8);
								*(bytes++) = (byte)c;
							}
							else
							{
								*(bytes++) = (byte)c;
								*(bytes++) = (byte)(c >> 8);
							}
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						chars--;
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
							encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
						}
						encoderFallbackBuffer.InternalFallback(c, ref chars);
						c = '\0';
						continue;
					}
					if (bytes + 1 >= ptr)
					{
						if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
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
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = (byte)c2;
							continue;
						}
						*(bytes++) = (byte)c2;
						*(bytes++) = (byte)(c2 >> 8);
						continue;
					}
				}
				IL_477:
				if (c <= '\0' || (encoder != null && !encoder.MustFlush))
				{
					goto IL_4F1;
				}
				if (flag)
				{
					break;
				}
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
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
				}
				encoderFallbackBuffer.InternalFallback(c, ref chars);
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_RecursiveFallback", new object[]
			{
				c
			}), "chars");
			IL_4F1:
			if (encoder != null)
			{
				encoder.charLeftOver = c;
				encoder.m_charsUsed = (int)((long)(chars - ptr4));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x060067D4 RID: 26580 RVA: 0x00160480 File Offset: 0x0015E680
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			byte* ptr = bytes + count;
			byte* byteStart = bytes;
			int num = -1;
			char c = '\0';
			int num2 = count >> 1;
			ulong* ptr2 = (ulong*)(ptr - 7);
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
				if (c > '\0')
				{
					num2++;
				}
				if (num >= 0 && (count & 1) == 1)
				{
					num2++;
				}
			}
			while (bytes < ptr)
			{
				if (!this.bigEndian && (bytes & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr3;
					for (ptr3 = (ulong*)bytes; ptr3 < ptr2; ptr3++)
					{
						if ((9223512776490647552UL & *ptr3) != 0UL)
						{
							ulong num3 = (17870556004450629632UL & *ptr3) ^ 15564677810327967744UL;
							if (((num3 & 18446462598732840960UL) == 0UL || (num3 & 281470681743360UL) == 0UL || (num3 & (ulong)-65536) == 0UL || (num3 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr3) ^ 15852908186546788352UL) != 0UL)
							{
								break;
							}
						}
					}
					bytes = (byte*)ptr3;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
					if (bytes >= ptr)
					{
						break;
					}
				}
				char c2;
				if (this.bigEndian)
				{
					c2 = (char)(num << 8 | (int)(*(bytes++)));
				}
				else
				{
					c2 = (char)((int)(*(bytes++)) << 8 | num);
				}
				num = -1;
				if (c2 >= '\ud800' && c2 <= '\udfff')
				{
					if (c2 <= '\udbff')
					{
						if (c > '\0')
						{
							num2--;
							byte[] bytes2;
							if (this.bigEndian)
							{
								bytes2 = new byte[]
								{
									(byte)(c >> 8),
									(byte)c
								};
							}
							else
							{
								bytes2 = new byte[]
								{
									(byte)c,
									(byte)(c >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(byteStart, null);
							}
							num2 += decoderFallbackBuffer.InternalFallback(bytes2, bytes);
						}
						c = c2;
					}
					else if (c == '\0')
					{
						num2--;
						byte[] bytes3;
						if (this.bigEndian)
						{
							bytes3 = new byte[]
							{
								(byte)(c2 >> 8),
								(byte)c2
							};
						}
						else
						{
							bytes3 = new byte[]
							{
								(byte)c2,
								(byte)(c2 >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(byteStart, null);
						}
						num2 += decoderFallbackBuffer.InternalFallback(bytes3, bytes);
					}
					else
					{
						c = '\0';
					}
				}
				else if (c > '\0')
				{
					num2--;
					byte[] bytes4;
					if (this.bigEndian)
					{
						bytes4 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes4 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(bytes4, bytes);
					c = '\0';
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					num2--;
					byte[] bytes5;
					if (this.bigEndian)
					{
						bytes5 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes5 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(bytes5, bytes);
					c = '\0';
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(new byte[]
					{
						(byte)num
					}, bytes);
				}
			}
			if (c > '\0')
			{
				num2--;
			}
			return num2;
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00160864 File Offset: 0x0015EA64
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			int num = -1;
			char c = '\0';
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
			}
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			while (bytes < ptr)
			{
				if (!this.bigEndian && (chars & 7L) == null && (bytes & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr5 = (ulong*)(bytes - 7 + (IntPtr)(((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes)) : ((long)(ptr2 - chars) << 1)) / 8);
					ulong* ptr6 = (ulong*)bytes;
					ulong* ptr7 = (ulong*)chars;
					while (ptr6 < ptr5)
					{
						if ((9223512776490647552UL & *ptr6) != 0UL)
						{
							ulong num2 = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
							if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)-65536) == 0UL || (num2 & 65535UL) == 0UL) && ((18158790778715962368UL & *ptr6) ^ 15852908186546788352UL) != 0UL)
							{
								break;
							}
						}
						*ptr7 = *ptr6;
						ptr6++;
						ptr7++;
					}
					chars = (char*)ptr7;
					bytes = (byte*)ptr6;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
				}
				else
				{
					char c2;
					if (this.bigEndian)
					{
						c2 = (char)(num << 8 | (int)(*(bytes++)));
					}
					else
					{
						c2 = (char)((int)(*(bytes++)) << 8 | num);
					}
					num = -1;
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								byte[] bytes2;
								if (this.bigEndian)
								{
									bytes2 = new byte[]
									{
										(byte)(c >> 8),
										(byte)c
									};
								}
								else
								{
									bytes2 = new byte[]
									{
										(byte)c,
										(byte)(c >> 8)
									};
								}
								if (decoderFallbackBuffer == null)
								{
									if (decoder == null)
									{
										decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
									}
									else
									{
										decoderFallbackBuffer = decoder.FallbackBuffer;
									}
									decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
								}
								if (!decoderFallbackBuffer.InternalFallback(bytes2, bytes, ref chars))
								{
									bytes -= 2;
									decoderFallbackBuffer.InternalReset();
									base.ThrowCharsOverflow(decoder, chars == ptr4);
									break;
								}
							}
							c = c2;
							continue;
						}
						if (c == '\0')
						{
							byte[] bytes3;
							if (this.bigEndian)
							{
								bytes3 = new byte[]
								{
									(byte)(c2 >> 8),
									(byte)c2
								};
							}
							else
							{
								bytes3 = new byte[]
								{
									(byte)c2,
									(byte)(c2 >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
							}
							if (!decoderFallbackBuffer.InternalFallback(bytes3, bytes, ref chars))
							{
								bytes -= 2;
								decoderFallbackBuffer.InternalReset();
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							continue;
						}
						else
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 2;
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							*(chars++) = c;
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						byte[] bytes4;
						if (this.bigEndian)
						{
							bytes4 = new byte[]
							{
								(byte)(c >> 8),
								(byte)c
							};
						}
						else
						{
							bytes4 = new byte[]
							{
								(byte)c,
								(byte)(c >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
						}
						if (!decoderFallbackBuffer.InternalFallback(bytes4, bytes, ref chars))
						{
							bytes -= 2;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(decoder, chars == ptr4);
							break;
						}
						c = '\0';
					}
					if (chars >= ptr2)
					{
						bytes -= 2;
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						break;
					}
					*(chars++) = c2;
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					byte[] bytes5;
					if (this.bigEndian)
					{
						bytes5 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes5 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					if (!decoderFallbackBuffer.InternalFallback(bytes5, bytes, ref chars))
					{
						bytes -= 2;
						if (num >= 0)
						{
							bytes--;
						}
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes += 2;
						if (num >= 0)
						{
							bytes++;
							goto IL_4A2;
						}
						goto IL_4A2;
					}
					else
					{
						c = '\0';
					}
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					if (!decoderFallbackBuffer.InternalFallback(new byte[]
					{
						(byte)num
					}, bytes, ref chars))
					{
						bytes--;
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes++;
					}
					else
					{
						num = -1;
					}
				}
			}
			IL_4A2:
			if (decoder != null)
			{
				decoder.m_bytesUsed = (int)((long)(bytes - ptr3));
				decoder.lastChar = c;
				decoder.lastByte = num;
			}
			return (int)((long)(chars - ptr4));
		}

		// Token: 0x060067D6 RID: 26582 RVA: 0x00160D3A File Offset: 0x0015EF3A
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x060067D7 RID: 26583 RVA: 0x00160D42 File Offset: 0x0015EF42
		[__DynamicallyInvokable]
		public override System.Text.Decoder GetDecoder()
		{
			return new UnicodeEncoding.Decoder(this);
		}

		// Token: 0x060067D8 RID: 26584 RVA: 0x00160D4C File Offset: 0x0015EF4C
		[__DynamicallyInvokable]
		public override byte[] GetPreamble()
		{
			if (!this.byteOrderMark)
			{
				return EmptyArray<byte>.Value;
			}
			if (this.bigEndian)
			{
				return new byte[]
				{
					254,
					byte.MaxValue
				};
			}
			return new byte[]
			{
				byte.MaxValue,
				254
			};
		}

		// Token: 0x060067D9 RID: 26585 RVA: 0x00160D9C File Offset: 0x0015EF9C
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
			num <<= 1;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x060067DA RID: 26586 RVA: 0x00160E0C File Offset: 0x0015F00C
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)(byteCount >> 1) + (long)(byteCount & 1) + 1L;
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

		// Token: 0x060067DB RID: 26587 RVA: 0x00160E7C File Offset: 0x0015F07C
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
			return unicodeEncoding != null && (this.CodePage == unicodeEncoding.CodePage && this.byteOrderMark == unicodeEncoding.byteOrderMark && this.bigEndian == unicodeEncoding.bigEndian && base.EncoderFallback.Equals(unicodeEncoding.EncoderFallback)) && base.DecoderFallback.Equals(unicodeEncoding.DecoderFallback);
		}

		// Token: 0x060067DC RID: 26588 RVA: 0x00160EE5 File Offset: 0x0015F0E5
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + (this.byteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
		}

		// Token: 0x04002E53 RID: 11859
		[OptionalField(VersionAdded = 2)]
		internal bool isThrowException;

		// Token: 0x04002E54 RID: 11860
		internal bool bigEndian;

		// Token: 0x04002E55 RID: 11861
		internal bool byteOrderMark = true;

		// Token: 0x04002E56 RID: 11862
		public const int CharSize = 2;

		// Token: 0x02000C84 RID: 3204
		[Serializable]
		private class Decoder : DecoderNLS, ISerializable
		{
			// Token: 0x0600706B RID: 28779 RVA: 0x00181E3C File Offset: 0x0018003C
			public Decoder(UnicodeEncoding encoding) : base(encoding)
			{
			}

			// Token: 0x0600706C RID: 28780 RVA: 0x00181E4C File Offset: 0x0018004C
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.lastByte = (int)info.GetValue("lastByte", typeof(int));
				try
				{
					this.m_encoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
					this.lastChar = (char)info.GetValue("lastChar", typeof(char));
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					bool bigEndian = (bool)info.GetValue("bigEndian", typeof(bool));
					this.m_encoding = new UnicodeEncoding(bigEndian, false);
				}
			}

			// Token: 0x0600706D RID: 28781 RVA: 0x00181F34 File Offset: 0x00180134
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("m_encoding", this.m_encoding);
				info.AddValue("m_fallback", this.m_fallback);
				info.AddValue("lastChar", this.lastChar);
				info.AddValue("lastByte", this.lastByte);
				info.AddValue("bigEndian", ((UnicodeEncoding)this.m_encoding).bigEndian);
			}

			// Token: 0x0600706E RID: 28782 RVA: 0x00181FAE File Offset: 0x001801AE
			public override void Reset()
			{
				this.lastByte = -1;
				this.lastChar = '\0';
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700135D RID: 4957
			// (get) Token: 0x0600706F RID: 28783 RVA: 0x00181FD1 File Offset: 0x001801D1
			internal override bool HasState
			{
				get
				{
					return this.lastByte != -1 || this.lastChar > '\0';
				}
			}

			// Token: 0x040037E3 RID: 14307
			internal int lastByte = -1;

			// Token: 0x040037E4 RID: 14308
			internal char lastChar;
		}
	}
}
