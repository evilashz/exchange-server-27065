using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002EA RID: 746
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum AggregateType
	{
		// Token: 0x0400110D RID: 4365
		Minimum,
		// Token: 0x0400110E RID: 4366
		Maximum
	}
}
