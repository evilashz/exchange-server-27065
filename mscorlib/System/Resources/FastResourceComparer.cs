using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200035D RID: 861
	internal sealed class FastResourceComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		// Token: 0x06002BB9 RID: 11193 RVA: 0x000A4724 File Offset: 0x000A2924
		public int GetHashCode(object key)
		{
			string key2 = (string)key;
			return FastResourceComparer.HashFunction(key2);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000A473E File Offset: 0x000A293E
		public int GetHashCode(string key)
		{
			return FastResourceComparer.HashFunction(key);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000A4748 File Offset: 0x000A2948
		internal static int HashFunction(string key)
		{
			uint num = 5381U;
			for (int i = 0; i < key.Length; i++)
			{
				num = ((num << 5) + num ^ (uint)key[i]);
			}
			return (int)num;
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000A477C File Offset: 0x000A297C
		public int Compare(object a, object b)
		{
			if (a == b)
			{
				return 0;
			}
			string strA = (string)a;
			string strB = (string)b;
			return string.CompareOrdinal(strA, strB);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000A47A4 File Offset: 0x000A29A4
		public int Compare(string a, string b)
		{
			return string.CompareOrdinal(a, b);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000A47AD File Offset: 0x000A29AD
		public bool Equals(string a, string b)
		{
			return string.Equals(a, b);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000A47B8 File Offset: 0x000A29B8
		public bool Equals(object a, object b)
		{
			if (a == b)
			{
				return true;
			}
			string a2 = (string)a;
			string b2 = (string)b;
			return string.Equals(a2, b2);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000A47E0 File Offset: 0x000A29E0
		[SecurityCritical]
		public unsafe static int CompareOrdinal(string a, byte[] bytes, int bCharLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = a.Length;
			if (num3 > bCharLength)
			{
				num3 = bCharLength;
			}
			if (bCharLength == 0)
			{
				if (a.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				fixed (byte* ptr = bytes)
				{
					byte* ptr2 = ptr;
					while (num < num3 && num2 == 0)
					{
						int num4 = (int)(*ptr2) | (int)ptr2[1] << 8;
						num2 = (int)a[num++] - num4;
						ptr2 += 2;
					}
				}
				if (num2 != 0)
				{
					return num2;
				}
				return a.Length - bCharLength;
			}
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000A4866 File Offset: 0x000A2A66
		[SecurityCritical]
		public static int CompareOrdinal(byte[] bytes, int aCharLength, string b)
		{
			return -FastResourceComparer.CompareOrdinal(b, bytes, aCharLength);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000A4874 File Offset: 0x000A2A74
		[SecurityCritical]
		internal unsafe static int CompareOrdinal(byte* a, int byteLen, string b)
		{
			int num = 0;
			int num2 = 0;
			int num3 = byteLen >> 1;
			if (num3 > b.Length)
			{
				num3 = b.Length;
			}
			while (num2 < num3 && num == 0)
			{
				char c = (char)((int)(*(a++)) | (int)(*(a++)) << 8);
				num = (int)(c - b[num2++]);
			}
			if (num != 0)
			{
				return num;
			}
			return byteLen - b.Length * 2;
		}

		// Token: 0x0400116E RID: 4462
		internal static readonly FastResourceComparer Default = new FastResourceComparer();
	}
}
