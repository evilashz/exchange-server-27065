using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase
{
	// Token: 0x02000088 RID: 136
	public enum SyncFilterType
	{
		// Token: 0x04000437 RID: 1079
		[XmlEnum("0")]
		NoFilter,
		// Token: 0x04000438 RID: 1080
		[XmlEnum("1")]
		OneDayBack,
		// Token: 0x04000439 RID: 1081
		[XmlEnum("2")]
		ThreeDaysBack,
		// Token: 0x0400043A RID: 1082
		[XmlEnum("3")]
		OneWeekBack,
		// Token: 0x0400043B RID: 1083
		[XmlEnum("4")]
		TwoWeeksBack,
		// Token: 0x0400043C RID: 1084
		[XmlEnum("5")]
		OneMonthBack,
		// Token: 0x0400043D RID: 1085
		[XmlEnum("6")]
		ThreeMonthsBack,
		// Token: 0x0400043E RID: 1086
		[XmlEnum("7")]
		SixMonthsBack,
		// Token: 0x0400043F RID: 1087
		[XmlEnum("8")]
		IncompleteTasks
	}
}
