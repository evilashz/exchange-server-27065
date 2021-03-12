using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.StatelessResponse
{
	// Token: 0x020001A2 RID: 418
	[XmlRoot("ResponseContentType", Namespace = "HMMAIL:", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.3038")]
	[XmlType(Namespace = "HMMAIL:")]
	[Serializable]
	public enum ResponseContentTypeType
	{
		// Token: 0x040006AA RID: 1706
		mtom
	}
}
