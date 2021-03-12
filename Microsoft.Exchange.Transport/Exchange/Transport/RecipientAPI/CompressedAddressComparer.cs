using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Transport.RecipientAPI
{
	// Token: 0x02000530 RID: 1328
	internal class CompressedAddressComparer : IEqualityComparer<byte[]>
	{
		// Token: 0x06003DF2 RID: 15858 RVA: 0x00104304 File Offset: 0x00102504
		bool IEqualityComparer<byte[]>.Equals(byte[] aBytes, byte[] bBytes)
		{
			if (aBytes.Length != bBytes.Length)
			{
				return false;
			}
			for (int i = 0; i < aBytes.Length; i++)
			{
				if (aBytes[i] != bBytes[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00104334 File Offset: 0x00102534
		public int GetHashCode(byte[] bytes)
		{
			string @string = Encoding.ASCII.GetString(bytes);
			return @string.GetHashCode();
		}
	}
}
