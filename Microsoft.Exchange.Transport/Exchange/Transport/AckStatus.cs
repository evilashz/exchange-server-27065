using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200014E RID: 334
	internal enum AckStatus
	{
		// Token: 0x0400072D RID: 1837
		Pending,
		// Token: 0x0400072E RID: 1838
		Success,
		// Token: 0x0400072F RID: 1839
		Retry,
		// Token: 0x04000730 RID: 1840
		Fail,
		// Token: 0x04000731 RID: 1841
		Expand,
		// Token: 0x04000732 RID: 1842
		Relay,
		// Token: 0x04000733 RID: 1843
		SuccessNoDsn,
		// Token: 0x04000734 RID: 1844
		Resubmit,
		// Token: 0x04000735 RID: 1845
		Quarantine,
		// Token: 0x04000736 RID: 1846
		Skip
	}
}
