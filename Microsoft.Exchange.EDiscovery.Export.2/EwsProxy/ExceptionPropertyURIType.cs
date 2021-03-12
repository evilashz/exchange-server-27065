using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000E2 RID: 226
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ExceptionPropertyURIType
	{
		// Token: 0x04000618 RID: 1560
		[XmlEnum("attachment:Name")]
		attachmentName,
		// Token: 0x04000619 RID: 1561
		[XmlEnum("attachment:ContentType")]
		attachmentContentType,
		// Token: 0x0400061A RID: 1562
		[XmlEnum("attachment:Content")]
		attachmentContent,
		// Token: 0x0400061B RID: 1563
		[XmlEnum("recurrence:Month")]
		recurrenceMonth,
		// Token: 0x0400061C RID: 1564
		[XmlEnum("recurrence:DayOfWeekIndex")]
		recurrenceDayOfWeekIndex,
		// Token: 0x0400061D RID: 1565
		[XmlEnum("recurrence:DaysOfWeek")]
		recurrenceDaysOfWeek,
		// Token: 0x0400061E RID: 1566
		[XmlEnum("recurrence:DayOfMonth")]
		recurrenceDayOfMonth,
		// Token: 0x0400061F RID: 1567
		[XmlEnum("recurrence:Interval")]
		recurrenceInterval,
		// Token: 0x04000620 RID: 1568
		[XmlEnum("recurrence:NumberOfOccurrences")]
		recurrenceNumberOfOccurrences,
		// Token: 0x04000621 RID: 1569
		[XmlEnum("timezone:Offset")]
		timezoneOffset
	}
}
