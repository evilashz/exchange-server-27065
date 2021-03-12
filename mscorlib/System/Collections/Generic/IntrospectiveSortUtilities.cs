using System;
using System.Runtime.Versioning;

namespace System.Collections.Generic
{
	// Token: 0x020004B1 RID: 1201
	internal static class IntrospectiveSortUtilities
	{
		// Token: 0x06003A17 RID: 14871 RVA: 0x000DC0E0 File Offset: 0x000DA2E0
		internal static int FloorLog2(int n)
		{
			int num = 0;
			while (n >= 1)
			{
				num++;
				n /= 2;
			}
			return num;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000DC0FF File Offset: 0x000DA2FF
		internal static void ThrowOrIgnoreBadComparer(object comparer)
		{
			if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_BogusIComparer", new object[]
				{
					comparer
				}));
			}
		}

		// Token: 0x040018A5 RID: 6309
		internal const int IntrosortSizeThreshold = 16;

		// Token: 0x040018A6 RID: 6310
		internal const int QuickSortDepthThreshold = 32;
	}
}
