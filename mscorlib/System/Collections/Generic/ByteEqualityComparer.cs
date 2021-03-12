using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	internal class ByteEqualityComparer : EqualityComparer<byte>
	{
		// Token: 0x0600396F RID: 14703 RVA: 0x000DAA6E File Offset: 0x000D8C6E
		public override bool Equals(byte x, byte y)
		{
			return x == y;
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x000DAA74 File Offset: 0x000D8C74
		public override int GetHashCode(byte b)
		{
			return b.GetHashCode();
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x000DAA80 File Offset: 0x000D8C80
		[SecuritySafeCritical]
		internal unsafe override int IndexOf(byte[] array, byte value, int startIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_Count"));
			}
			if (count > array.Length - startIndex)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOffLen"));
			}
			if (count == 0)
			{
				return -1;
			}
			fixed (byte* ptr = array)
			{
				return Buffer.IndexOfByte(ptr, value, startIndex, count);
			}
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x000DAB10 File Offset: 0x000D8D10
		internal override int LastIndexOf(byte[] array, byte value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			for (int i = startIndex; i >= num; i--)
			{
				if (array[i] == value)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000DAB3C File Offset: 0x000D8D3C
		public override bool Equals(object obj)
		{
			ByteEqualityComparer byteEqualityComparer = obj as ByteEqualityComparer;
			return byteEqualityComparer != null;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000DAB54 File Offset: 0x000D8D54
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
