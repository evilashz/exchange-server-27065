using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200021B RID: 539
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DayOfWeekType
	{
		// Token: 0x04000E12 RID: 3602
		Sunday,
		// Token: 0x04000E13 RID: 3603
		Monday,
		// Token: 0x04000E14 RID: 3604
		Tuesday,
		// Token: 0x04000E15 RID: 3605
		Wednesday,
		// Token: 0x04000E16 RID: 3606
		Thursday,
		// Token: 0x04000E17 RID: 3607
		Friday,
		// Token: 0x04000E18 RID: 3608
		Saturday,
		// Token: 0x04000E19 RID: 3609
		Day,
		// Token: 0x04000E1A RID: 3610
		Weekday,
		// Token: 0x04000E1B RID: 3611
		WeekendDay
	}
}
