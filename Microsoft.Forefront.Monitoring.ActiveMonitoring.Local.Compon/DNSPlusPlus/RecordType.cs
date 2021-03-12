using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x0200000B RID: 11
	public enum RecordType
	{
		// Token: 0x0400002F RID: 47
		A = 1,
		// Token: 0x04000030 RID: 48
		NS,
		// Token: 0x04000031 RID: 49
		CNAME = 5,
		// Token: 0x04000032 RID: 50
		SOA,
		// Token: 0x04000033 RID: 51
		WKS = 11,
		// Token: 0x04000034 RID: 52
		PTR,
		// Token: 0x04000035 RID: 53
		HINFO,
		// Token: 0x04000036 RID: 54
		MX = 15,
		// Token: 0x04000037 RID: 55
		TXT,
		// Token: 0x04000038 RID: 56
		AAAA = 28,
		// Token: 0x04000039 RID: 57
		SRV = 33,
		// Token: 0x0400003A RID: 58
		ANY = 255
	}
}
