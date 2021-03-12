using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000766 RID: 1894
	[XmlType(TypeName = "ExceptionPropertyURIType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ExceptionPropertyUriEnum
	{
		// Token: 0x04001F58 RID: 8024
		[XmlEnum("attachment:Name")]
		AttachmentName,
		// Token: 0x04001F59 RID: 8025
		[XmlEnum("attachment:ContentType")]
		ContentType,
		// Token: 0x04001F5A RID: 8026
		[XmlEnum("attachment:Content")]
		Content,
		// Token: 0x04001F5B RID: 8027
		[XmlEnum("recurrence:Month")]
		Month,
		// Token: 0x04001F5C RID: 8028
		[XmlEnum("recurrence:DayOfWeekIndex")]
		DayOfWeekIndex,
		// Token: 0x04001F5D RID: 8029
		[XmlEnum("recurrence:DaysOfWeek")]
		DaysOfWeek,
		// Token: 0x04001F5E RID: 8030
		[XmlEnum("recurrence:DayOfMonth")]
		DayOfMonth,
		// Token: 0x04001F5F RID: 8031
		[XmlEnum("recurrence:Interval")]
		Interval,
		// Token: 0x04001F60 RID: 8032
		[XmlEnum("recurrence:NumberOfOccurrences")]
		NumberOfOccurrences,
		// Token: 0x04001F61 RID: 8033
		[XmlEnum("timezone:Offset")]
		Offset
	}
}
