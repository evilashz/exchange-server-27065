using System;

namespace Microsoft.Exchange.Data.ContentTypes.iCalendar
{
	// Token: 0x020000AF RID: 175
	[Flags]
	public enum CalendarComplianceStatus
	{
		// Token: 0x040005C6 RID: 1478
		Compliant = 0,
		// Token: 0x040005C7 RID: 1479
		StreamTruncated = 1,
		// Token: 0x040005C8 RID: 1480
		PropertyTruncated = 2,
		// Token: 0x040005C9 RID: 1481
		InvalidCharacterInPropertyName = 4,
		// Token: 0x040005CA RID: 1482
		InvalidCharacterInParameterName = 8,
		// Token: 0x040005CB RID: 1483
		InvalidCharacterInParameterText = 16,
		// Token: 0x040005CC RID: 1484
		InvalidCharacterInQuotedString = 32,
		// Token: 0x040005CD RID: 1485
		InvalidCharacterInPropertyValue = 64,
		// Token: 0x040005CE RID: 1486
		NotAllComponentsClosed = 128,
		// Token: 0x040005CF RID: 1487
		ParametersOnComponentTag = 256,
		// Token: 0x040005D0 RID: 1488
		EndTagWithoutBegin = 512,
		// Token: 0x040005D1 RID: 1489
		ComponentNameMismatch = 1024,
		// Token: 0x040005D2 RID: 1490
		InvalidValueFormat = 2048,
		// Token: 0x040005D3 RID: 1491
		EmptyParameterName = 4096,
		// Token: 0x040005D4 RID: 1492
		EmptyPropertyName = 8192,
		// Token: 0x040005D5 RID: 1493
		EmptyComponentName = 16384,
		// Token: 0x040005D6 RID: 1494
		PropertyOutsideOfComponent = 32768,
		// Token: 0x040005D7 RID: 1495
		InvalidParameterValue = 65536,
		// Token: 0x040005D8 RID: 1496
		ParameterNameMissing = 131072
	}
}
