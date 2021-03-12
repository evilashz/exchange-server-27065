using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000276 RID: 630
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum SuggestionQuality
	{
		// Token: 0x04000FCF RID: 4047
		Excellent,
		// Token: 0x04000FD0 RID: 4048
		Good,
		// Token: 0x04000FD1 RID: 4049
		Fair,
		// Token: 0x04000FD2 RID: 4050
		Poor
	}
}
