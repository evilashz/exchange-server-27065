using System;

namespace Microsoft.Exchange.TextProcessing.Boomerang
{
	// Token: 0x02000005 RID: 5
	internal class Constants
	{
		// Token: 0x04000014 RID: 20
		internal const int BoomerangRandomDataLength = 5;

		// Token: 0x04000015 RID: 21
		internal const int BoomerangMacSize = 5;

		// Token: 0x04000016 RID: 22
		internal const int BoomerangRecipientHintSize = 1;

		// Token: 0x04000017 RID: 23
		internal const int BoomerangDateHintSize = 1;

		// Token: 0x04000018 RID: 24
		internal const int BoomerangVersionLength = 1;

		// Token: 0x04000019 RID: 25
		internal const string BoomerangVersion = "0";

		// Token: 0x0400001A RID: 26
		internal const int BoomerangDefaultValidIntervalsConfig = 30;

		// Token: 0x0400001B RID: 27
		internal const string XMSExchangeOrganizationValidBoomerang = "X-MS-Exchange-Organization-Valid-Boomerang";

		// Token: 0x0400001C RID: 28
		internal static readonly byte[] BoomerangCodeKey = new byte[]
		{
			93,
			34,
			225,
			50,
			124,
			245,
			72,
			28,
			170,
			204,
			9,
			245,
			217,
			28,
			0,
			45
		};
	}
}
