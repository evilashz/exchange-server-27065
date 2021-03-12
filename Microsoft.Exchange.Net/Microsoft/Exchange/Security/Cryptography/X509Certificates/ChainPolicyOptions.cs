using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A93 RID: 2707
	[Flags]
	internal enum ChainPolicyOptions : uint
	{
		// Token: 0x040032A7 RID: 12967
		None = 0U,
		// Token: 0x040032A8 RID: 12968
		IgnoreNotTimeValid = 1U,
		// Token: 0x040032A9 RID: 12969
		IgnoreCTLNotTimeValid = 2U,
		// Token: 0x040032AA RID: 12970
		IgnoreNotTimeNested = 4U,
		// Token: 0x040032AB RID: 12971
		IgnoreInvalidBasicConstraints = 8U,
		// Token: 0x040032AC RID: 12972
		AllowUnknownCA = 16U,
		// Token: 0x040032AD RID: 12973
		IgnoreWrongUsage = 32U,
		// Token: 0x040032AE RID: 12974
		IgnoreInvalidName = 64U,
		// Token: 0x040032AF RID: 12975
		IgnoreInvalidPolicy = 128U,
		// Token: 0x040032B0 RID: 12976
		IgnoreEndRevUnknown = 256U,
		// Token: 0x040032B1 RID: 12977
		IgnoreCTLSignerRevUnknown = 512U,
		// Token: 0x040032B2 RID: 12978
		IgnoreCARevUnknown = 1024U,
		// Token: 0x040032B3 RID: 12979
		IgnoreRootRevUnknown = 2048U,
		// Token: 0x040032B4 RID: 12980
		AllowTestRoot = 32768U,
		// Token: 0x040032B5 RID: 12981
		TrustTestRoot = 16384U
	}
}
