using System;

namespace Microsoft.Exchange.HostedServices.Archive.MetaReplication
{
	// Token: 0x0200004D RID: 77
	public static class ByteArrayHelper
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000BAA4 File Offset: 0x00009CA4
		public static int GetHash(byte[] bytes)
		{
			int num = 0;
			if (bytes != null)
			{
				num ^= 1;
				foreach (byte b in bytes)
				{
					num ^= b.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000BADC File Offset: 0x00009CDC
		public static bool Equal(byte[] left, byte[] right)
		{
			if (left == null && right == null)
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
	}
}
