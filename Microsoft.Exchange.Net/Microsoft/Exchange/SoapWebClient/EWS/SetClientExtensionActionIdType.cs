using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003A1 RID: 929
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SetClientExtensionActionIdType
	{
		// Token: 0x040014AA RID: 5290
		Install,
		// Token: 0x040014AB RID: 5291
		Uninstall,
		// Token: 0x040014AC RID: 5292
		Configure
	}
}
