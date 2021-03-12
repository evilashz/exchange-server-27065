using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x020000B6 RID: 182
	internal enum ProbeState
	{
		// Token: 0x040003DD RID: 989
		Capability1,
		// Token: 0x040003DE RID: 990
		Capability2,
		// Token: 0x040003DF RID: 991
		Capability3,
		// Token: 0x040003E0 RID: 992
		Login1,
		// Token: 0x040003E1 RID: 993
		Login2,
		// Token: 0x040003E2 RID: 994
		User1,
		// Token: 0x040003E3 RID: 995
		User2,
		// Token: 0x040003E4 RID: 996
		Pass1,
		// Token: 0x040003E5 RID: 997
		Pass2,
		// Token: 0x040003E6 RID: 998
		StartTls1,
		// Token: 0x040003E7 RID: 999
		Failure,
		// Token: 0x040003E8 RID: 1000
		Finish
	}
}
