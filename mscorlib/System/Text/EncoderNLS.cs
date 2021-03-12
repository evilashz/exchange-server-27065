using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A3C RID: 2620
	[Serializable]
	internal class EncoderNLS : Encoder, ISerializable
	{
		// Token: 0x060066B1 RID: 26289 RVA: 0x00159EE7 File Offset: 0x001580E7
		internal EncoderNLS(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), base.GetType()));
		}

		// Token: 0x060066B2 RID: 26290 RVA: 0x00159F0E File Offset: 0x0015810E
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoder(info);
			info.AddValue("encoding", this.m_encoding);
			info.AddValue("charLeftOver", this.charLeftOver);
			info.SetType(typeof(Encoding.DefaultEncoder));
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x00159F49 File Offset: 0x00158149
		internal EncoderNLS(Encoding encoding)
		{
			this.m_encoding = encoding;
			this.m_fallback = this.m_encoding.EncoderFallback;
			this.Reset();
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x00159F6F File Offset: 0x0015816F
		internal EncoderNLS()
		{
			this.m_encoding = null;
			this.Reset();
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00159F84 File Offset: 0x00158184
		public override void Reset()
		{
			this.charLeftOver = '\0';
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x060066B6 RID: 26294 RVA: 0x00159FA0 File Offset: 0x001581A0
		[SecuritySafeCritical]
		public unsafe override int GetByteCount(char[] chars, int index, int count, bool flush)
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
				chars = new char[1];
			}
			int byteCount;
			fixed (char* ptr = chars)
			{
				byteCount = this.GetByteCount(ptr + index, count, flush);
			}
			return byteCount;
		}

		// Token: 0x060066B7 RID: 26295 RVA: 0x0015A044 File Offset: 0x00158244
		[SecurityCritical]
		public unsafe override int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetByteCount(chars, count, this);
		}

		// Token: 0x060066B8 RID: 26296 RVA: 0x0015A0A0 File Offset: 0x001582A0
		[SecuritySafeCritical]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
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
				chars = new char[1];
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
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush);
				}
			}
		}

		// Token: 0x060066B9 RID: 26297 RVA: 0x0015A1A4 File Offset: 0x001583A4
		[SecurityCritical]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
		}

		// Token: 0x060066BA RID: 26298 RVA: 0x0015A228 File Offset: 0x00158428
		[SecuritySafeCritical]
		public unsafe override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
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
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			fixed (char* ptr = chars)
			{
				fixed (byte* ptr2 = bytes)
				{
					this.Convert(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x060066BB RID: 26299 RVA: 0x0015A358 File Offset: 0x00158558
		[SecurityCritical]
		public unsafe override void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = false;
			this.m_charsUsed = 0;
			bytesUsed = this.m_encoding.GetBytes(chars, charCount, bytes, byteCount, this);
			charsUsed = this.m_charsUsed;
			completed = (charsUsed == charCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060066BC RID: 26300 RVA: 0x0015A41D File Offset: 0x0015861D
		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060066BD RID: 26301 RVA: 0x0015A425 File Offset: 0x00158625
		public bool MustFlush
		{
			get
			{
				return this.m_mustFlush;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060066BE RID: 26302 RVA: 0x0015A42D File Offset: 0x0015862D
		internal virtual bool HasState
		{
			get
			{
				return this.charLeftOver > '\0';
			}
		}

		// Token: 0x060066BF RID: 26303 RVA: 0x0015A438 File Offset: 0x00158638
		internal void ClearMustFlush()
		{
			this.m_mustFlush = false;
		}

		// Token: 0x04002D9B RID: 11675
		internal char charLeftOver;

		// Token: 0x04002D9C RID: 11676
		protected Encoding m_encoding;

		// Token: 0x04002D9D RID: 11677
		[NonSerialized]
		protected bool m_mustFlush;

		// Token: 0x04002D9E RID: 11678
		[NonSerialized]
		internal bool m_throwOnOverflow;

		// Token: 0x04002D9F RID: 11679
		[NonSerialized]
		internal int m_charsUsed;
	}
}
