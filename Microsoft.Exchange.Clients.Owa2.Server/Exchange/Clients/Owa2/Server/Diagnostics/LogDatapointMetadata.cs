using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000449 RID: 1097
	internal enum LogDatapointMetadata
	{
		// Token: 0x040014E6 RID: 5350
		[DisplayName("LD", "CDE")]
		CreateDatapointEventsElapsed,
		// Token: 0x040014E7 RID: 5351
		[DisplayName("LD", "DE")]
		DatapointsToLoggerElapsed,
		// Token: 0x040014E8 RID: 5352
		[DisplayName("LD", "TDSz")]
		TotalDatapointSize,
		// Token: 0x040014E9 RID: 5353
		[DisplayName("LD", "ADC")]
		AnalyticsDatapointCount,
		// Token: 0x040014EA RID: 5354
		[DisplayName("LD", "ADSz")]
		AnalyticsDatapointSize,
		// Token: 0x040014EB RID: 5355
		[DisplayName("LD", "IDC")]
		InferenceDatapointCount,
		// Token: 0x040014EC RID: 5356
		[DisplayName("LD", "IDSz")]
		InferenceDatapointSize,
		// Token: 0x040014ED RID: 5357
		[DisplayName("LD", "DDC")]
		DiagnosticsDatapointCount,
		// Token: 0x040014EE RID: 5358
		[DisplayName("LD", "DDSz")]
		DiagnosticsDatapointSize,
		// Token: 0x040014EF RID: 5359
		[DisplayName("LD", "CIAE")]
		CreateInferenceActivitiesElapsed,
		// Token: 0x040014F0 RID: 5360
		[DisplayName("LD", "IAE")]
		InferenceActivitiesToMailboxElapsed,
		// Token: 0x040014F1 RID: 5361
		[DisplayName("LD", "IAC")]
		InferenceActivitiesToMailboxCount,
		// Token: 0x040014F2 RID: 5362
		[DisplayName("LD", "WRE")]
		WatsonReportingElapsed,
		// Token: 0x040014F3 RID: 5363
		[DisplayName("LD", "WDC")]
		WatsonDatapointCount,
		// Token: 0x040014F4 RID: 5364
		[DisplayName("LD", "WDSz")]
		WatsonDatapointSize,
		// Token: 0x040014F5 RID: 5365
		[DisplayName("LD", "WDS")]
		WatsonDatapointSkipped,
		// Token: 0x040014F6 RID: 5366
		[DisplayName("LD", "WDF")]
		WatsonDatapointFailed
	}
}
