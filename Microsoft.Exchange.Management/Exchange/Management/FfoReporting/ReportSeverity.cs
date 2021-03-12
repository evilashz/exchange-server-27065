using System;
using Microsoft.Exchange.Core;

namespace Microsoft.Exchange.Management.FfoReporting
{
	// Token: 0x020003FB RID: 1019
	public enum ReportSeverity
	{
		// Token: 0x04001CB3 RID: 7347
		[LocDescription(CoreStrings.IDs.ReportSeverityLow)]
		Low,
		// Token: 0x04001CB4 RID: 7348
		[LocDescription(CoreStrings.IDs.ReportSeverityMedium)]
		Medium,
		// Token: 0x04001CB5 RID: 7349
		[LocDescription(CoreStrings.IDs.ReportSeverityHigh)]
		High
	}
}
