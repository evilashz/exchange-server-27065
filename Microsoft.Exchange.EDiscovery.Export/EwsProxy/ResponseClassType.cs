using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000AD RID: 173
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum ResponseClassType
	{
		// Token: 0x0400053C RID: 1340
		Success,
		// Token: 0x0400053D RID: 1341
		Warning,
		// Token: 0x0400053E RID: 1342
		Error
	}
}
