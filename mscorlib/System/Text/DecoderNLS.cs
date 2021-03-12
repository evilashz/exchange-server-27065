using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A31 RID: 2609
	[Serializable]
	internal class DecoderNLS : Decoder, ISerializable
	{
		// Token: 0x06006654 RID: 26196 RVA: 0x00158C57 File Offset: 0x00156E57
		internal DecoderNLS(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("NotSupported_TypeCannotDeserialized"), base.GetType()));
		}

		// Token: 0x06006655 RID: 26197 RVA: 0x00158C7E File Offset: 0x00156E7E
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeDecoder(info);
			info.AddValue("encoding", this.m_encoding);
			info.SetType(typeof(Encoding.DefaultDecoder));
		}

		// Token: 0x06006656 RID: 26198 RVA: 0x00158CA8 File Offset: 0x00156EA8
		internal DecoderNLS(Encoding encoding)
		{
			this.m_encoding = encoding;
			this.m_fallback = this.m_encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x06006657 RID: 26199 RVA: 0x00158CCE File Offset: 0x00156ECE
		internal DecoderNLS()
		{
			this.m_encoding = null;
			this.Reset();
		}

		// Token: 0x06006658 RID: 26200 RVA: 0x00158CE3 File Offset: 0x00156EE3
		public override void Reset()
		{
			if (this.m_fallbackBuffer != null)
			{
				this.m_fallbackBuffer.Reset();
			}
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x00158CF8 File Offset: 0x00156EF8
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x00158D04 File Offset: 0x00156F04
		[SecuritySafeCritical]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
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
				bytes = new byte[1];
			}
			fixed (byte* ptr = bytes)
			{
				return this.GetCharCount(ptr + index, count, flush);
			}
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x00158DA0 File Offset: 0x00156FA0
		[SecurityCritical]
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.m_mustFlush = flush;
			this.m_throwOnOverflow = true;
			return this.m_encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x00158DFC File Offset: 0x00156FFC
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x00158E0C File Offset: 0x0015700C
		[SecuritySafeCritical]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
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
				bytes = new byte[1];
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
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush);
				}
			}
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x00158F10 File Offset: 0x00157110
		[SecurityCritical]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
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
			return this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x00158F94 File Offset: 0x00157194
		[SecuritySafeCritical]
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			if (bytes.Length == 0)
			{
				bytes = new byte[1];
			}
			if (chars.Length == 0)
			{
				chars = new char[1];
			}
			fixed (byte* ptr = bytes)
			{
				fixed (char* ptr2 = chars)
				{
					this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x001590C4 File Offset: 0x001572C4
		[SecurityCritical]
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			this.m_throwOnOverflow = false;
			this.m_bytesUsed = 0;
			charsUsed = this.m_encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this.m_bytesUsed;
			completed = (bytesUsed == byteCount && (!flush || !this.HasState) && (this.m_fallbackBuffer == null || this.m_fallbackBuffer.Remaining == 0));
		}

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06006661 RID: 26209 RVA: 0x00159189 File Offset: 0x00157389
		public bool MustFlush
		{
			get
			{
				return this.m_mustFlush;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06006662 RID: 26210 RVA: 0x00159191 File Offset: 0x00157391
		internal virtual bool HasState
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x00159194 File Offset: 0x00157394
		internal void ClearMustFlush()
		{
			this.m_mustFlush = false;
		}

		// Token: 0x04002D81 RID: 11649
		protected Encoding m_encoding;

		// Token: 0x04002D82 RID: 11650
		[NonSerialized]
		protected bool m_mustFlush;

		// Token: 0x04002D83 RID: 11651
		[NonSerialized]
		internal bool m_throwOnOverflow;

		// Token: 0x04002D84 RID: 11652
		[NonSerialized]
		internal int m_bytesUsed;
	}
}
