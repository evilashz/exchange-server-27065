using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000061 RID: 97
	internal enum WeatherMetadata
	{
		// Token: 0x04000542 RID: 1346
		[DisplayName("WEA", "QRY")]
		QueryString,
		// Token: 0x04000543 RID: 1347
		[DisplayName("WEA", "CNT")]
		LocationsCount,
		// Token: 0x04000544 RID: 1348
		[DisplayName("WEA", "LOC")]
		Culture,
		// Token: 0x04000545 RID: 1349
		[DisplayName("WEA", "SLA")]
		SearchLocationsLatency,
		// Token: 0x04000546 RID: 1350
		[DisplayName("WEA", "FLA")]
		ForecastLatency,
		// Token: 0x04000547 RID: 1351
		[DisplayName("WEA", "WESC")]
		WebExceptionStatusCode,
		// Token: 0x04000548 RID: 1352
		[DisplayName("WEA", "FAIL")]
		Failed
	}
}
