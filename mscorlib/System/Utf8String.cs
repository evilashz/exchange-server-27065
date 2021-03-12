using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
	// Token: 0x0200012E RID: 302
	internal struct Utf8String
	{
		// Token: 0x060011B0 RID: 4528
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool EqualsCaseSensitive(void* szLhs, void* szRhs, int cSz);

		// Token: 0x060011B1 RID: 4529
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern bool EqualsCaseInsensitive(void* szLhs, void* szRhs, int cSz);

		// Token: 0x060011B2 RID: 4530
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern uint HashCaseInsensitive(void* sz, int cSz);

		// Token: 0x060011B3 RID: 4531 RVA: 0x00036998 File Offset: 0x00034B98
		[SecurityCritical]
		private unsafe static int GetUtf8StringByteLength(void* pUtf8String)
		{
			int num = 0;
			byte* ptr = (byte*)pUtf8String;
			while (*ptr != 0)
			{
				num++;
				ptr++;
			}
			return num;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000369B8 File Offset: 0x00034BB8
		[SecurityCritical]
		internal unsafe Utf8String(void* pStringHeap)
		{
			this.m_pStringHeap = pStringHeap;
			if (pStringHeap != null)
			{
				this.m_StringHeapByteLength = Utf8String.GetUtf8StringByteLength(pStringHeap);
				return;
			}
			this.m_StringHeapByteLength = 0;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000369DA File Offset: 0x00034BDA
		[SecurityCritical]
		internal unsafe Utf8String(void* pUtf8String, int cUtf8String)
		{
			this.m_pStringHeap = pUtf8String;
			this.m_StringHeapByteLength = cUtf8String;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000369EC File Offset: 0x00034BEC
		[SecuritySafeCritical]
		internal bool Equals(Utf8String s)
		{
			if (this.m_pStringHeap == null)
			{
				return s.m_StringHeapByteLength == 0;
			}
			return s.m_StringHeapByteLength == this.m_StringHeapByteLength && this.m_StringHeapByteLength != 0 && Utf8String.EqualsCaseSensitive(s.m_pStringHeap, this.m_pStringHeap, this.m_StringHeapByteLength);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00036A3C File Offset: 0x00034C3C
		[SecuritySafeCritical]
		internal bool EqualsCaseInsensitive(Utf8String s)
		{
			if (this.m_pStringHeap == null)
			{
				return s.m_StringHeapByteLength == 0;
			}
			return s.m_StringHeapByteLength == this.m_StringHeapByteLength && this.m_StringHeapByteLength != 0 && Utf8String.EqualsCaseInsensitive(s.m_pStringHeap, this.m_pStringHeap, this.m_StringHeapByteLength);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00036A8C File Offset: 0x00034C8C
		[SecuritySafeCritical]
		internal uint HashCaseInsensitive()
		{
			return Utf8String.HashCaseInsensitive(this.m_pStringHeap, this.m_StringHeapByteLength);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00036AA0 File Offset: 0x00034CA0
		[SecuritySafeCritical]
		public unsafe override string ToString()
		{
			byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)this.m_StringHeapByteLength) * 1)];
			byte* ptr2 = (byte*)this.m_pStringHeap;
			for (int i = 0; i < this.m_StringHeapByteLength; i++)
			{
				ptr[i] = *ptr2;
				ptr2++;
			}
			if (this.m_StringHeapByteLength == 0)
			{
				return "";
			}
			int charCount = Encoding.UTF8.GetCharCount(ptr, this.m_StringHeapByteLength);
			char* ptr3 = stackalloc char[checked(unchecked((UIntPtr)charCount) * 2)];
			Encoding.UTF8.GetChars(ptr, this.m_StringHeapByteLength, ptr3, charCount);
			return new string(ptr3, 0, charCount);
		}

		// Token: 0x0400065C RID: 1628
		[SecurityCritical]
		private unsafe void* m_pStringHeap;

		// Token: 0x0400065D RID: 1629
		private int m_StringHeapByteLength;
	}
}
