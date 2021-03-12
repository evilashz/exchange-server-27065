using System;

namespace Microsoft.Forefront.Reporting.OnDemandQuery
{
	// Token: 0x02000004 RID: 4
	internal enum OnDemandQueryLogEvent
	{
		// Token: 0x0400002A RID: 42
		New = 1,
		// Token: 0x0400002B RID: 43
		Submit,
		// Token: 0x0400002C RID: 44
		Step,
		// Token: 0x0400002D RID: 45
		Complete,
		// Token: 0x0400002E RID: 46
		View,
		// Token: 0x0400002F RID: 47
		Fail,
		// Token: 0x04000030 RID: 48
		FailToSendEmail
	}
}
