using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C03 RID: 3075
	internal enum DnsRecordType : ushort
	{
		// Token: 0x04003947 RID: 14663
		Unknown,
		// Token: 0x04003948 RID: 14664
		A,
		// Token: 0x04003949 RID: 14665
		NS,
		// Token: 0x0400394A RID: 14666
		CNAME = 5,
		// Token: 0x0400394B RID: 14667
		SOA,
		// Token: 0x0400394C RID: 14668
		PTR = 12,
		// Token: 0x0400394D RID: 14669
		MX = 15,
		// Token: 0x0400394E RID: 14670
		TXT,
		// Token: 0x0400394F RID: 14671
		AAAA = 28,
		// Token: 0x04003950 RID: 14672
		SRV = 33
	}
}
