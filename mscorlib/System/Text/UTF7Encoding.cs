using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A54 RID: 2644
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class UTF7Encoding : Encoding
	{
		// Token: 0x060067DD RID: 26589 RVA: 0x00160F1F File Offset: 0x0015F11F
		[__DynamicallyInvokable]
		public UTF7Encoding() : this(false)
		{
		}

		// Token: 0x060067DE RID: 26590 RVA: 0x00160F28 File Offset: 0x0015F128
		[__DynamicallyInvokable]
		public UTF7Encoding(bool allowOptionals) : base(65000)
		{
			this.m_allowOptionals = allowOptionals;
			this.MakeTables();
		}

		// Token: 0x060067DF RID: 26591 RVA: 0x00160F44 File Offset: 0x0015F144
		private void MakeTables()
		{
			this.base64Bytes = new byte[64];
			for (int i = 0; i < 64; i++)
			{
				this.base64Bytes[i] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[i];
			}
			this.base64Values = new sbyte[128];
			for (int j = 0; j < 128; j++)
			{
				this.base64Values[j] = -1;
			}
			for (int k = 0; k < 64; k++)
			{
				this.base64Values[(int)this.base64Bytes[k]] = (sbyte)k;
			}
			this.directEncode = new bool[128];
			int length = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length;
			for (int l = 0; l < length; l++)
			{
				this.directEncode[(int)"\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[l]] = true;
			}
			if (this.m_allowOptionals)
			{
				length = "!\"#$%&*;<=>@[]^_`{|}".Length;
				for (int m = 0; m < length; m++)
				{
					this.directEncode[(int)"!\"#$%&*;<=>@[]^_`{|}"[m]] = true;
				}
			}
		}

		// Token: 0x060067E0 RID: 26592 RVA: 0x0016103C File Offset: 0x0015F23C
		internal override void SetDefaultFallbacks()
		{
			this.encoderFallback = new EncoderReplacementFallback(string.Empty);
			this.decoderFallback = new UTF7Encoding.DecoderUTF7Fallback();
		}

		// Token: 0x060067E1 RID: 26593 RVA: 0x00161059 File Offset: 0x0015F259
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			base.OnDeserializing();
		}

		// Token: 0x060067E2 RID: 26594 RVA: 0x00161061 File Offset: 0x0015F261
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			base.OnDeserialized();
			if (this.m_deserializedFromEverett)
			{
				this.m_allowOptionals = this.directEncode[(int)"!\"#$%&*;<=>@[]^_`{|}"[0]];
			}
			this.MakeTables();
		}

		// Token: 0x060067E3 RID: 26595 RVA: 0x00161090 File Offset: 0x0015F290
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			UTF7Encoding utf7Encoding = value as UTF7Encoding;
			return utf7Encoding != null && (this.m_allowOptionals == utf7Encoding.m_allowOptionals && base.EncoderFallback.Equals(utf7Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf7Encoding.DecoderFallback);
		}

		// Token: 0x060067E4 RID: 26596 RVA: 0x001610DD File Offset: 0x0015F2DD
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode();
		}

		// Token: 0x060067E5 RID: 26597 RVA: 0x00161100 File Offset: 0x0015F300
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

		// Token: 0x060067E6 RID: 26598 RVA: 0x00161198 File Offset: 0x0015F398
		[SecuritySafeCritical]
		[ComVisible(false)]
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

		// Token: 0x060067E7 RID: 26599 RVA: 0x001611D1 File Offset: 0x0015F3D1
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

		// Token: 0x060067E8 RID: 26600 RVA: 0x00161210 File Offset: 0x0015F410
		[SecuritySafeCritical]
		[ComVisible(false)]
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

		// Token: 0x060067E9 RID: 26601 RVA: 0x00161304 File Offset: 0x0015F504
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

		// Token: 0x060067EA RID: 26602 RVA: 0x00161400 File Offset: 0x0015F600
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

		// Token: 0x060067EB RID: 26603 RVA: 0x00161470 File Offset: 0x0015F670
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

		// Token: 0x060067EC RID: 26604 RVA: 0x00161503 File Offset: 0x0015F703
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

		// Token: 0x060067ED RID: 26605 RVA: 0x00161544 File Offset: 0x0015F744
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

		// Token: 0x060067EE RID: 26606 RVA: 0x00161640 File Offset: 0x0015F840
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

		// Token: 0x060067EF RID: 26607 RVA: 0x001616B0 File Offset: 0x0015F8B0
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

		// Token: 0x060067F0 RID: 26608 RVA: 0x00161746 File Offset: 0x0015F946
		[SecurityCritical]
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			return this.GetBytes(chars, count, null, 0, baseEncoder);
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x00161754 File Offset: 0x0015F954
		[SecurityCritical]
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			UTF7Encoding.Encoder encoder = (UTF7Encoding.Encoder)baseEncoder;
			int num = 0;
			int i = -1;
			Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer(this, encoder, bytes, byteCount, chars, charCount);
			if (encoder != null)
			{
				num = encoder.bits;
				i = encoder.bitCount;
				while (i >= 6)
				{
					i -= 6;
					if (!encodingByteBuffer.AddByte(this.base64Bytes[num >> i & 63]))
					{
						base.ThrowBytesOverflow(encoder, encodingByteBuffer.Count == 0);
					}
				}
			}
			while (encodingByteBuffer.MoreData)
			{
				char nextChar = encodingByteBuffer.GetNextChar();
				if (nextChar < '\u0080' && this.directEncode[(int)nextChar])
				{
					if (i >= 0)
					{
						if (i > 0)
						{
							if (!encodingByteBuffer.AddByte(this.base64Bytes[num << 6 - i & 63]))
							{
								break;
							}
							i = 0;
						}
						if (!encodingByteBuffer.AddByte(45))
						{
							break;
						}
						i = -1;
					}
					if (!encodingByteBuffer.AddByte((byte)nextChar))
					{
						break;
					}
				}
				else if (i < 0 && nextChar == '+')
				{
					if (!encodingByteBuffer.AddByte(43, 45))
					{
						break;
					}
				}
				else
				{
					if (i < 0)
					{
						if (!encodingByteBuffer.AddByte(43))
						{
							break;
						}
						i = 0;
					}
					num = (num << 16 | (int)nextChar);
					i += 16;
					while (i >= 6)
					{
						i -= 6;
						if (!encodingByteBuffer.AddByte(this.base64Bytes[num >> i & 63]))
						{
							i += 6;
							nextChar = encodingByteBuffer.GetNextChar();
							break;
						}
					}
					if (i >= 6)
					{
						break;
					}
				}
			}
			if (i >= 0 && (encoder == null || encoder.MustFlush))
			{
				if (i > 0 && encodingByteBuffer.AddByte(this.base64Bytes[num << 6 - i & 63]))
				{
					i = 0;
				}
				if (encodingByteBuffer.AddByte(45))
				{
					num = 0;
					i = -1;
				}
				else
				{
					encodingByteBuffer.GetNextChar();
				}
			}
			if (bytes != null && encoder != null)
			{
				encoder.bits = num;
				encoder.bitCount = i;
				encoder.m_charsUsed = encodingByteBuffer.CharsUsed;
			}
			return encodingByteBuffer.Count;
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x00161906 File Offset: 0x0015FB06
		[SecurityCritical]
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			return this.GetChars(bytes, count, null, 0, baseDecoder);
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x00161914 File Offset: 0x0015FB14
		[SecurityCritical]
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF7Encoding.Decoder decoder = (UTF7Encoding.Decoder)baseDecoder;
			Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer(this, decoder, chars, charCount, bytes, byteCount);
			int num = 0;
			int num2 = -1;
			bool flag = false;
			if (decoder != null)
			{
				num = decoder.bits;
				num2 = decoder.bitCount;
				flag = decoder.firstByte;
			}
			if (num2 >= 16)
			{
				if (!encodingCharBuffer.AddChar((char)(num >> num2 - 16 & 65535)))
				{
					base.ThrowCharsOverflow(decoder, true);
				}
				num2 -= 16;
			}
			while (encodingCharBuffer.MoreData)
			{
				byte nextByte = encodingCharBuffer.GetNextByte();
				int num3;
				if (num2 >= 0)
				{
					sbyte b;
					if (nextByte < 128 && (b = this.base64Values[(int)nextByte]) >= 0)
					{
						flag = false;
						num = (num << 6 | (int)((byte)b));
						num2 += 6;
						if (num2 < 16)
						{
							continue;
						}
						num3 = (num >> num2 - 16 & 65535);
						num2 -= 16;
					}
					else
					{
						num2 = -1;
						if (nextByte != 45)
						{
							if (!encodingCharBuffer.Fallback(nextByte))
							{
								break;
							}
							continue;
						}
						else
						{
							if (!flag)
							{
								continue;
							}
							num3 = 43;
						}
					}
				}
				else
				{
					if (nextByte == 43)
					{
						num2 = 0;
						flag = true;
						continue;
					}
					if (nextByte >= 128)
					{
						if (!encodingCharBuffer.Fallback(nextByte))
						{
							break;
						}
						continue;
					}
					else
					{
						num3 = (int)nextByte;
					}
				}
				if (num3 >= 0 && !encodingCharBuffer.AddChar((char)num3))
				{
					if (num2 >= 0)
					{
						encodingCharBuffer.AdjustBytes(1);
						num2 += 16;
						break;
					}
					break;
				}
			}
			if (chars != null && decoder != null)
			{
				if (decoder.MustFlush)
				{
					decoder.bits = 0;
					decoder.bitCount = -1;
					decoder.firstByte = false;
				}
				else
				{
					decoder.bits = num;
					decoder.bitCount = num2;
					decoder.firstByte = flag;
				}
				decoder.m_bytesUsed = encodingCharBuffer.BytesUsed;
			}
			return encodingCharBuffer.Count;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x00161A98 File Offset: 0x0015FC98
		[__DynamicallyInvokable]
		public override System.Text.Decoder GetDecoder()
		{
			return new UTF7Encoding.Decoder(this);
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x00161AA0 File Offset: 0x0015FCA0
		[__DynamicallyInvokable]
		public override System.Text.Encoder GetEncoder()
		{
			return new UTF7Encoding.Encoder(this);
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x00161AA8 File Offset: 0x0015FCA8
		[__DynamicallyInvokable]
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			long num = (long)charCount * 3L + 2L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", Environment.GetResourceString("ArgumentOutOfRange_GetByteCountOverflow"));
			}
			return (int)num;
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x00161AF8 File Offset: 0x0015FCF8
		[__DynamicallyInvokable]
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			int num = byteCount;
			if (num == 0)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x04002E57 RID: 11863
		private const string base64Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x04002E58 RID: 11864
		private const string directChars = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		// Token: 0x04002E59 RID: 11865
		private const string optionalChars = "!\"#$%&*;<=>@[]^_`{|}";

		// Token: 0x04002E5A RID: 11866
		private byte[] base64Bytes;

		// Token: 0x04002E5B RID: 11867
		private sbyte[] base64Values;

		// Token: 0x04002E5C RID: 11868
		private bool[] directEncode;

		// Token: 0x04002E5D RID: 11869
		[OptionalField(VersionAdded = 2)]
		private bool m_allowOptionals;

		// Token: 0x04002E5E RID: 11870
		private const int UTF7_CODEPAGE = 65000;

		// Token: 0x02000C85 RID: 3205
		[Serializable]
		private class Decoder : DecoderNLS, ISerializable
		{
			// Token: 0x06007070 RID: 28784 RVA: 0x00181FE7 File Offset: 0x001801E7
			public Decoder(UTF7Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06007071 RID: 28785 RVA: 0x00181FF0 File Offset: 0x001801F0
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.bits = (int)info.GetValue("bits", typeof(int));
				this.bitCount = (int)info.GetValue("bitCount", typeof(int));
				this.firstByte = (bool)info.GetValue("firstByte", typeof(bool));
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x06007072 RID: 28786 RVA: 0x00182094 File Offset: 0x00180294
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
				info.AddValue("bits", this.bits);
				info.AddValue("bitCount", this.bitCount);
				info.AddValue("firstByte", this.firstByte);
			}

			// Token: 0x06007073 RID: 28787 RVA: 0x001820F3 File Offset: 0x001802F3
			public override void Reset()
			{
				this.bits = 0;
				this.bitCount = -1;
				this.firstByte = false;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700135E RID: 4958
			// (get) Token: 0x06007074 RID: 28788 RVA: 0x0018211D File Offset: 0x0018031D
			internal override bool HasState
			{
				get
				{
					return this.bitCount != -1;
				}
			}

			// Token: 0x040037E5 RID: 14309
			internal int bits;

			// Token: 0x040037E6 RID: 14310
			internal int bitCount;

			// Token: 0x040037E7 RID: 14311
			internal bool firstByte;
		}

		// Token: 0x02000C86 RID: 3206
		[Serializable]
		private class Encoder : EncoderNLS, ISerializable
		{
			// Token: 0x06007075 RID: 28789 RVA: 0x0018212B File Offset: 0x0018032B
			public Encoder(UTF7Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06007076 RID: 28790 RVA: 0x00182134 File Offset: 0x00180334
			internal Encoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.bits = (int)info.GetValue("bits", typeof(int));
				this.bitCount = (int)info.GetValue("bitCount", typeof(int));
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x06007077 RID: 28791 RVA: 0x001821B8 File Offset: 0x001803B8
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
				info.AddValue("bits", this.bits);
				info.AddValue("bitCount", this.bitCount);
			}

			// Token: 0x06007078 RID: 28792 RVA: 0x00182206 File Offset: 0x00180406
			public override void Reset()
			{
				this.bitCount = -1;
				this.bits = 0;
				if (this.m_fallbackBuffer != null)
				{
					this.m_fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700135F RID: 4959
			// (get) Token: 0x06007079 RID: 28793 RVA: 0x00182229 File Offset: 0x00180429
			internal override bool HasState
			{
				get
				{
					return this.bits != 0 || this.bitCount != -1;
				}
			}

			// Token: 0x040037E8 RID: 14312
			internal int bits;

			// Token: 0x040037E9 RID: 14313
			internal int bitCount;
		}

		// Token: 0x02000C87 RID: 3207
		[Serializable]
		internal sealed class DecoderUTF7Fallback : DecoderFallback
		{
			// Token: 0x0600707B RID: 28795 RVA: 0x00182249 File Offset: 0x00180449
			public override DecoderFallbackBuffer CreateFallbackBuffer()
			{
				return new UTF7Encoding.DecoderUTF7FallbackBuffer(this);
			}

			// Token: 0x17001360 RID: 4960
			// (get) Token: 0x0600707C RID: 28796 RVA: 0x00182251 File Offset: 0x00180451
			public override int MaxCharCount
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x0600707D RID: 28797 RVA: 0x00182254 File Offset: 0x00180454
			public override bool Equals(object value)
			{
				return value is UTF7Encoding.DecoderUTF7Fallback;
			}

			// Token: 0x0600707E RID: 28798 RVA: 0x0018226E File Offset: 0x0018046E
			public override int GetHashCode()
			{
				return 984;
			}
		}

		// Token: 0x02000C88 RID: 3208
		internal sealed class DecoderUTF7FallbackBuffer : DecoderFallbackBuffer
		{
			// Token: 0x0600707F RID: 28799 RVA: 0x00182275 File Offset: 0x00180475
			public DecoderUTF7FallbackBuffer(UTF7Encoding.DecoderUTF7Fallback fallback)
			{
			}

			// Token: 0x06007080 RID: 28800 RVA: 0x00182284 File Offset: 0x00180484
			public override bool Fallback(byte[] bytesUnknown, int index)
			{
				this.cFallback = (char)bytesUnknown[0];
				if (this.cFallback == '\0')
				{
					return false;
				}
				this.iCount = (this.iSize = 1);
				return true;
			}

			// Token: 0x06007081 RID: 28801 RVA: 0x001822B8 File Offset: 0x001804B8
			public override char GetNextChar()
			{
				int num = this.iCount;
				this.iCount = num - 1;
				if (num > 0)
				{
					return this.cFallback;
				}
				return '\0';
			}

			// Token: 0x06007082 RID: 28802 RVA: 0x001822E1 File Offset: 0x001804E1
			public override bool MovePrevious()
			{
				if (this.iCount >= 0)
				{
					this.iCount++;
				}
				return this.iCount >= 0 && this.iCount <= this.iSize;
			}

			// Token: 0x17001361 RID: 4961
			// (get) Token: 0x06007083 RID: 28803 RVA: 0x00182316 File Offset: 0x00180516
			public override int Remaining
			{
				get
				{
					if (this.iCount <= 0)
					{
						return 0;
					}
					return this.iCount;
				}
			}

			// Token: 0x06007084 RID: 28804 RVA: 0x00182329 File Offset: 0x00180529
			[SecuritySafeCritical]
			public override void Reset()
			{
				this.iCount = -1;
				this.byteStart = null;
			}

			// Token: 0x06007085 RID: 28805 RVA: 0x0018233A File Offset: 0x0018053A
			[SecurityCritical]
			internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
			{
				if (bytes.Length != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"));
				}
				if (bytes[0] != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x040037EA RID: 14314
			private char cFallback;

			// Token: 0x040037EB RID: 14315
			private int iCount = -1;

			// Token: 0x040037EC RID: 14316
			private int iSize;
		}
	}
}
