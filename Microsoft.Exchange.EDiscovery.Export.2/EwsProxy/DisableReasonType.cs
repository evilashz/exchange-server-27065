using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200032D RID: 813
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum DisableReasonType
	{
		// Token: 0x040011A6 RID: 4518
		NoReason,
		// Token: 0x040011A7 RID: 4519
		OutlookClientPerformance,
		// Token: 0x040011A8 RID: 4520
		OWAClientPerformance,
		// Token: 0x040011A9 RID: 4521
		MobileClientPerformance
	}
}
