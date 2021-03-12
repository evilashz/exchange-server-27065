using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200012C RID: 300
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarItemTypeType
	{
		// Token: 0x0400098B RID: 2443
		Single,
		// Token: 0x0400098C RID: 2444
		Occurrence,
		// Token: 0x0400098D RID: 2445
		Exception,
		// Token: 0x0400098E RID: 2446
		RecurringMaster
	}
}
