using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000002 RID: 2
	public class CafeArraysCacheEntry : ISharedCacheEntry
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CafeArraysCacheEntry()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public CafeArraysCacheEntry(string cafeArrayPreferenceList)
		{
			if (string.IsNullOrEmpty(cafeArrayPreferenceList))
			{
				throw new ArgumentNullException("cafeArrayPreferenceList", "Argument in Null or Empty");
			}
			this.cafeArrayPreferenceList = cafeArrayPreferenceList;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020FF File Offset: 0x000002FF
		public string CafeArrayPreferenceList
		{
			get
			{
				return this.cafeArrayPreferenceList;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002107 File Offset: 0x00000307
		public override string ToString()
		{
			return this.CafeArrayPreferenceList;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000210F File Offset: 0x0000030F
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000211C File Offset: 0x0000031C
		public override bool Equals(object obj)
		{
			CafeArraysCacheEntry cafeArraysCacheEntry = obj as CafeArraysCacheEntry;
			string b = null;
			if (cafeArraysCacheEntry != null)
			{
				b = cafeArraysCacheEntry.CafeArrayPreferenceList;
			}
			return string.Equals(this.CafeArrayPreferenceList, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public string GetPreferredRoutingUnit()
		{
			string[] array = this.CafeArrayPreferenceList.Split(new char[]
			{
				','
			});
			if (array.Length > 4 || array.Length < 1)
			{
				throw new ArgumentOutOfRangeException("CafeArrayPreferenceList", "Length was {0}" + array.Length);
			}
			return array[0];
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A0 File Offset: 0x000003A0
		public void FromByteArray(byte[] bytes)
		{
			if (bytes == null || bytes.Length < 5)
			{
				throw new ArgumentException(string.Format("It's not a valid byte array for CafeArrayCacheEntry, which has at least {0} bytes", 5));
			}
			byte b = bytes[0];
			int num = 1;
			int num2 = BitConverter.ToInt32(bytes, num);
			num += 4;
			if (num2 > 0)
			{
				this.cafeArrayPreferenceList = Encoding.ASCII.GetString(bytes, num, num2);
				return;
			}
			this.cafeArrayPreferenceList = CafeArraysCacheEntry.DefaultCafeArrayPreferenceList;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002204 File Offset: 0x00000404
		public byte[] ToByteArray()
		{
			IEnumerable<byte> enumerable = CafeArraysCacheEntry.Version;
			int value = 0;
			bool flag = false;
			if (!string.IsNullOrEmpty(this.CafeArrayPreferenceList))
			{
				flag = true;
				value = this.CafeArrayPreferenceList.Length;
			}
			enumerable = enumerable.Concat(BitConverter.GetBytes(value));
			if (flag)
			{
				enumerable = enumerable.Concat(Encoding.ASCII.GetBytes(this.CafeArrayPreferenceList));
			}
			return enumerable.ToArray<byte>();
		}

		// Token: 0x04000001 RID: 1
		internal const int MaxPreferenceListElements = 4;

		// Token: 0x04000002 RID: 2
		internal const char Delimiter = ',';

		// Token: 0x04000003 RID: 3
		private const int MinimumLength = 5;

		// Token: 0x04000004 RID: 4
		private const byte CurrentSerializationVersion = 1;

		// Token: 0x04000005 RID: 5
		private static readonly byte[] Version = new byte[]
		{
			1
		};

		// Token: 0x04000006 RID: 6
		private static readonly string DefaultCafeArrayPreferenceList = string.Format("{0}{1}{0}{1}{0}{1}{0}", -1, ',');

		// Token: 0x04000007 RID: 7
		private string cafeArrayPreferenceList;
	}
}
