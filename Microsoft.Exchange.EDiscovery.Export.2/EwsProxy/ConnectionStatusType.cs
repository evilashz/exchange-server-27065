using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000235 RID: 565
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ConnectionStatusType
	{
		// Token: 0x04000ECD RID: 3789
		OK,
		// Token: 0x04000ECE RID: 3790
		Closed
	}
}
