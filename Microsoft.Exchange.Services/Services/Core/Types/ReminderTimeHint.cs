using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F9 RID: 1529
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ReminderTimeHint
	{
		// Token: 0x04001B7F RID: 7039
		LaterToday,
		// Token: 0x04001B80 RID: 7040
		Tomorrow,
		// Token: 0x04001B81 RID: 7041
		TomorrowMorning,
		// Token: 0x04001B82 RID: 7042
		TomorrowAfternoon,
		// Token: 0x04001B83 RID: 7043
		TomorrowEvening,
		// Token: 0x04001B84 RID: 7044
		ThisWeekend,
		// Token: 0x04001B85 RID: 7045
		NextWeek,
		// Token: 0x04001B86 RID: 7046
		NextMonth,
		// Token: 0x04001B87 RID: 7047
		Someday,
		// Token: 0x04001B88 RID: 7048
		Custom,
		// Token: 0x04001B89 RID: 7049
		Now,
		// Token: 0x04001B8A RID: 7050
		InTwoDays
	}
}
