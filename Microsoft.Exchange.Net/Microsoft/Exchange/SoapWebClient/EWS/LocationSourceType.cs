using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001DC RID: 476
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum LocationSourceType
	{
		// Token: 0x04000CAA RID: 3242
		None,
		// Token: 0x04000CAB RID: 3243
		LocationServices,
		// Token: 0x04000CAC RID: 3244
		PhonebookServices,
		// Token: 0x04000CAD RID: 3245
		Device,
		// Token: 0x04000CAE RID: 3246
		Contact,
		// Token: 0x04000CAF RID: 3247
		Resource
	}
}
