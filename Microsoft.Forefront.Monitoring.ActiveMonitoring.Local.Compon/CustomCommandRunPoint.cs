using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200024E RID: 590
	public enum CustomCommandRunPoint
	{
		// Token: 0x0400094E RID: 2382
		None,
		// Token: 0x0400094F RID: 2383
		AfterConnect,
		// Token: 0x04000950 RID: 2384
		AfterHelo,
		// Token: 0x04000951 RID: 2385
		AfterAuthenticate,
		// Token: 0x04000952 RID: 2386
		AfterStartTls,
		// Token: 0x04000953 RID: 2387
		AfterHeloAfterStartTls,
		// Token: 0x04000954 RID: 2388
		AfterMailFrom,
		// Token: 0x04000955 RID: 2389
		BeforeRcptTo,
		// Token: 0x04000956 RID: 2390
		AfterRcptTo,
		// Token: 0x04000957 RID: 2391
		BeforeData,
		// Token: 0x04000958 RID: 2392
		AfterData
	}
}
