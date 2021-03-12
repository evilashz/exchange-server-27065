using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C3 RID: 451
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ExceptionPropertyURIType
	{
		// Token: 0x04000A6A RID: 2666
		[XmlEnum("attachment:Name")]
		attachmentName,
		// Token: 0x04000A6B RID: 2667
		[XmlEnum("attachment:ContentType")]
		attachmentContentType,
		// Token: 0x04000A6C RID: 2668
		[XmlEnum("attachment:Content")]
		attachmentContent,
		// Token: 0x04000A6D RID: 2669
		[XmlEnum("recurrence:Month")]
		recurrenceMonth,
		// Token: 0x04000A6E RID: 2670
		[XmlEnum("recurrence:DayOfWeekIndex")]
		recurrenceDayOfWeekIndex,
		// Token: 0x04000A6F RID: 2671
		[XmlEnum("recurrence:DaysOfWeek")]
		recurrenceDaysOfWeek,
		// Token: 0x04000A70 RID: 2672
		[XmlEnum("recurrence:DayOfMonth")]
		recurrenceDayOfMonth,
		// Token: 0x04000A71 RID: 2673
		[XmlEnum("recurrence:Interval")]
		recurrenceInterval,
		// Token: 0x04000A72 RID: 2674
		[XmlEnum("recurrence:NumberOfOccurrences")]
		recurrenceNumberOfOccurrences,
		// Token: 0x04000A73 RID: 2675
		[XmlEnum("timezone:Offset")]
		timezoneOffset
	}
}
