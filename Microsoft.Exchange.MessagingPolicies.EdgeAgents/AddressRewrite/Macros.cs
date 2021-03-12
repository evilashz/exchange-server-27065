using System;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000020 RID: 32
	internal static class Macros
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000544C File Offset: 0x0000364C
		internal static int Offset(uint address)
		{
			return (int)(address & 1048575U);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005455 File Offset: 0x00003655
		internal static int BlockIndex(uint address)
		{
			return (int)((address & 4293918720U) >> 20);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005461 File Offset: 0x00003661
		internal static uint Address(int blockIndex, int offset)
		{
			return (uint)(blockIndex << 20 | offset);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005469 File Offset: 0x00003669
		internal static int Left(int i)
		{
			return 2 * i + 1;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005470 File Offset: 0x00003670
		internal static int Right(int i)
		{
			return 2 * (i + 1);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005477 File Offset: 0x00003677
		internal static int Parent(int i)
		{
			return (i - 1) / 2;
		}
	}
}
