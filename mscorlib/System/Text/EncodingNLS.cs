using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A48 RID: 2632
	[ComVisible(true)]
	[Serializable]
	internal abstract class EncodingNLS : Encoding
	{
		// Token: 0x06006764 RID: 26468 RVA: 0x0015C043 File Offset: 0x0015A243
		protected EncodingNLS(int codePage) : base(codePage)
		{
		}

		// Token: 0x06006765 RID: 26469 RVA: 0x0015C04C File Offset: 0x0015A24C
		[SecuritySafeCritical]
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

		// Token: 0x06006766 RID: 26470 RVA: 0x0015C0E4 File Offset: 0x0015A2E4
		[SecuritySafeCritical]
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

		// Token: 0x06006767 RID: 26471 RVA: 0x0015C11D File Offset: 0x0015A31D
		[SecurityCritical]
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

		// Token: 0x06006768 RID: 26472 RVA: 0x0015C15C File Offset: 0x0015A35C
		[SecuritySafeCritical]
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

		// Token: 0x06006769 RID: 26473 RVA: 0x0015C250 File Offset: 0x0015A450
		[SecuritySafeCritical]
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

		// Token: 0x0600676A RID: 26474 RVA: 0x0015C34C File Offset: 0x0015A54C
		[SecurityCritical]
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

		// Token: 0x0600676B RID: 26475 RVA: 0x0015C3BC File Offset: 0x0015A5BC
		[SecuritySafeCritical]
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

		// Token: 0x0600676C RID: 26476 RVA: 0x0015C44F File Offset: 0x0015A64F
		[SecurityCritical]
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

		// Token: 0x0600676D RID: 26477 RVA: 0x0015C490 File Offset: 0x0015A690
		[SecuritySafeCritical]
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

		// Token: 0x0600676E RID: 26478 RVA: 0x0015C58C File Offset: 0x0015A78C
		[SecurityCritical]
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

		// Token: 0x0600676F RID: 26479 RVA: 0x0015C5FC File Offset: 0x0015A7FC
		[SecuritySafeCritical]
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

		// Token: 0x06006770 RID: 26480 RVA: 0x0015C692 File Offset: 0x0015A892
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x0015C69A File Offset: 0x0015A89A
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}
	}
}
