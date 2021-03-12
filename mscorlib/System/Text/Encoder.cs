using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A3B RID: 2619
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Encoder
	{
		// Token: 0x060066A4 RID: 26276 RVA: 0x00159AC2 File Offset: 0x00157CC2
		internal void SerializeEncoder(SerializationInfo info)
		{
			info.AddValue("m_fallback", this.m_fallback);
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x00159AD5 File Offset: 0x00157CD5
		[__DynamicallyInvokable]
		protected Encoder()
		{
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x060066A6 RID: 26278 RVA: 0x00159ADD File Offset: 0x00157CDD
		// (set) Token: 0x060066A7 RID: 26279 RVA: 0x00159AE8 File Offset: 0x00157CE8
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallback Fallback
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

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x060066A8 RID: 26280 RVA: 0x00159B3C File Offset: 0x00157D3C
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallbackBuffer FallbackBuffer
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
						this.m_fallbackBuffer = EncoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this.m_fallbackBuffer;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x060066A9 RID: 26281 RVA: 0x00159B77 File Offset: 0x00157D77
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this.m_fallbackBuffer != null;
			}
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x00159B84 File Offset: 0x00157D84
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Reset()
		{
			char[] chars = new char[0];
			byte[] bytes = new byte[this.GetByteCount(chars, 0, 0, true)];
			this.GetBytes(chars, 0, 0, bytes, 0, true);
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x060066AB RID: 26283
		[__DynamicallyInvokable]
		public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

		// Token: 0x060066AC RID: 26284 RVA: 0x00159BC8 File Offset: 0x00157DC8
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count, flush);
		}

		// Token: 0x060066AD RID: 26285
		[__DynamicallyInvokable]
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

		// Token: 0x060066AE RID: 26286 RVA: 0x00159C30 File Offset: 0x00157E30
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0, flush);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x00159CE4 File Offset: 0x00157EE4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
					completed = (charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x060066B0 RID: 26288 RVA: 0x00159E18 File Offset: 0x00158018
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
					completed = (charsUsed == charCount && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_ConversionOverflow"));
		}

		// Token: 0x04002D99 RID: 11673
		internal EncoderFallback m_fallback;

		// Token: 0x04002D9A RID: 11674
		[NonSerialized]
		internal EncoderFallbackBuffer m_fallbackBuffer;
	}
}
