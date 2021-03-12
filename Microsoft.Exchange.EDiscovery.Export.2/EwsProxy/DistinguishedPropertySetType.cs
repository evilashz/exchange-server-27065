using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000DF RID: 223
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum DistinguishedPropertySetType
	{
		// Token: 0x040005F0 RID: 1520
		Meeting,
		// Token: 0x040005F1 RID: 1521
		Appointment,
		// Token: 0x040005F2 RID: 1522
		Common,
		// Token: 0x040005F3 RID: 1523
		PublicStrings,
		// Token: 0x040005F4 RID: 1524
		Address,
		// Token: 0x040005F5 RID: 1525
		InternetHeaders,
		// Token: 0x040005F6 RID: 1526
		CalendarAssistant,
		// Token: 0x040005F7 RID: 1527
		UnifiedMessaging,
		// Token: 0x040005F8 RID: 1528
		Task,
		// Token: 0x040005F9 RID: 1529
		Sharing
	}
}
