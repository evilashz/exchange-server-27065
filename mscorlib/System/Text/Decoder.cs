using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A30 RID: 2608
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Decoder
	{
		// Token: 0x06006645 RID: 26181 RVA: 0x0015881D File Offset: 0x00156A1D
		internal void SerializeDecoder(SerializationInfo info)
		{
			info.AddValue("m_fallback", this.m_fallback);
		}

		// Token: 0x06006646 RID: 26182 RVA: 0x00158830 File Offset: 0x00156A30
		[__DynamicallyInvokable]
		protected Decoder()
		{
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06006647 RID: 26183 RVA: 0x00158838 File Offset: 0x00156A38
		// (set) Token: 0x06006648 RID: 26184 RVA: 0x00158840 File Offset: 0x00156A40
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallback Fallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_fallback;
			}
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.m_fallbackBuffer != null && this.m_fallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_FallbackBufferNotEmpty"), "value");
				}
				this.m_fallback = value;
				this.m_fallbackBuffer = null;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06006649 RID: 26185 RVA: 0x00158894 File Offset: 0x00156A94
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallbackBuffer FallbackBuffer
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_fallbackBuffer == null)
				{
					if (this.m_fallback != null)
					{
						this.m_fallbackBuffer = this.m_fallback.CreateFallbackBuffer();
					}
					else
					{
						this.m_fallbackBuffer = DecoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this.m_fallbackBuffer;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x0600664A RID: 26186 RVA: 0x001588CF File Offset: 0x00156ACF
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this.m_fallbackBuffer != null;
			}
		}

		// Token: 0x0600664B RID: 26187 RVA: 0x001588DC File Offset: 0x00156ADC
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			byte[] bytes = new byte[0];
			char[] chars = new char[this.GetCharCount(bytes, 0, 0, true)];
			this.GetChars(bytes, 0, 0, chars, 0, true);
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x0600664C RID: 26188
		[__DynamicallyInvokable]
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x0600664D RID: 26189 RVA: 0x00158920 File Offset: 0x00156B20
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			return this.GetCharCount(bytes, index, count);
		}

		// Token: 0x0600664E RID: 26190 RVA: 0x0015892C File Offset: 0x00156B2C
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x0600664F RID: 26191
		[__DynamicallyInvokable]
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06006650 RID: 26192 RVA: 0x0015898F File Offset: 0x00156B8F
		[__DynamicallyInvokable]
		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06006651 RID: 26193 RVA: 0x001589A0 File Offset: 0x00156BA0
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0, flush);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06006652 RID: 26194 RVA: 0x00158A54 File Offset: 0x00156C54
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
					completed = (bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x06006653 RID: 26195 RVA: 0x00158B88 File Offset: 0x00156D88
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
					completed = (bytesUsed == byteCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x04002D7F RID: 11647
		internal DecoderFallback m_fallback;

		// Token: 0x04002D80 RID: 11648
		[NonSerialized]
		internal DecoderFallbackBuffer m_fallbackBuffer;
	}
}
