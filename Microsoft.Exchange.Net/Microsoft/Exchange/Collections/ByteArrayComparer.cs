using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x0200003A RID: 58
	internal sealed class ByteArrayComparer : IEqualityComparer<byte[]>
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00007D55 File Offset: 0x00005F55
		private ByteArrayComparer()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007D60 File Offset: 0x00005F60
		public bool Equals(byte[] left, byte[] right)
		{
			if (object.ReferenceEquals(left, right))
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007DA4 File Offset: 0x00005FA4
		public int GetHashCode(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			int num = 0;
			for (int i = 0; i < bytes.Length; i++)
			{
				num = ((num << 3 | num >> 29) ^ (int)bytes[i]);
			}
			return num;
		}

		// Token: 0x0400010C RID: 268
		public static readonly ByteArrayComparer Instance = new ByteArrayComparer();
	}
}
