using System;

namespace System.Diagnostics
{
	// Token: 0x020003C8 RID: 968
	[Serializable]
	internal enum LoggingLevels
	{
		// Token: 0x04001601 RID: 5633
		TraceLevel0,
		// Token: 0x04001602 RID: 5634
		TraceLevel1,
		// Token: 0x04001603 RID: 5635
		TraceLevel2,
		// Token: 0x04001604 RID: 5636
		TraceLevel3,
		// Token: 0x04001605 RID: 5637
		TraceLevel4,
		// Token: 0x04001606 RID: 5638
		StatusLevel0 = 20,
		// Token: 0x04001607 RID: 5639
		StatusLevel1,
		// Token: 0x04001608 RID: 5640
		StatusLevel2,
		// Token: 0x04001609 RID: 5641
		StatusLevel3,
		// Token: 0x0400160A RID: 5642
		StatusLevel4,
		// Token: 0x0400160B RID: 5643
		WarningLevel = 40,
		// Token: 0x0400160C RID: 5644
		ErrorLevel = 50,
		// Token: 0x0400160D RID: 5645
		PanicLevel = 100
	}
}
