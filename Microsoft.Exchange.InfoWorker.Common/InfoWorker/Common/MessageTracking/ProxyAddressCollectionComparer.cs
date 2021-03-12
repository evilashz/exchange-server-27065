using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002C8 RID: 712
	internal class ProxyAddressCollectionComparer : IEqualityComparer<ProxyAddressCollection>
	{
		// Token: 0x060013DA RID: 5082 RVA: 0x0005C85C File Offset: 0x0005AA5C
		public bool Equals(ProxyAddressCollection x, ProxyAddressCollection y)
		{
			if (x == y)
			{
				return true;
			}
			foreach (ProxyAddress item in x)
			{
				if (y.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0005C8BC File Offset: 0x0005AABC
		public int GetHashCode(ProxyAddressCollection x)
		{
			int num = 0;
			foreach (ProxyAddress proxyAddress in x)
			{
				num ^= proxyAddress.GetHashCode();
			}
			return num;
		}
	}
}
