using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000348 RID: 840
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum MessageTrackingReportTemplateType
	{
		// Token: 0x04001228 RID: 4648
		Summary,
		// Token: 0x04001229 RID: 4649
		RecipientPath
	}
}
