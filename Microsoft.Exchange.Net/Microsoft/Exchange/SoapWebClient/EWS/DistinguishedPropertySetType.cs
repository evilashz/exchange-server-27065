using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001C0 RID: 448
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DistinguishedPropertySetType
	{
		// Token: 0x04000A42 RID: 2626
		Meeting,
		// Token: 0x04000A43 RID: 2627
		Appointment,
		// Token: 0x04000A44 RID: 2628
		Common,
		// Token: 0x04000A45 RID: 2629
		PublicStrings,
		// Token: 0x04000A46 RID: 2630
		Address,
		// Token: 0x04000A47 RID: 2631
		InternetHeaders,
		// Token: 0x04000A48 RID: 2632
		CalendarAssistant,
		// Token: 0x04000A49 RID: 2633
		UnifiedMessaging,
		// Token: 0x04000A4A RID: 2634
		Task,
		// Token: 0x04000A4B RID: 2635
		Sharing
	}
}
