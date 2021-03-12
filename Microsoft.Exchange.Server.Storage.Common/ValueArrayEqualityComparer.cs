using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000C4 RID: 196
	internal class ValueArrayEqualityComparer : IEqualityComparer<object[]>
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x0001A7E4 File Offset: 0x000189E4
		bool IEqualityComparer<object[]>.Equals(object[] x, object[] y)
		{
			return ValueHelper.ArraysEqual(x, y, CultureInfo.InvariantCulture.CompareInfo, CompareOptions.None);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001A7F8 File Offset: 0x000189F8
		int IEqualityComparer<object[]>.GetHashCode(object[] x)
		{
			uint num = (uint)x.Length;
			for (int i = 0; i < x.Length; i++)
			{
				num ^= (uint)ValueHelper.GetHashCode(x[i]);
				num = (num << 1 | num >> 31);
			}
			return (int)num;
		}
	}
}
