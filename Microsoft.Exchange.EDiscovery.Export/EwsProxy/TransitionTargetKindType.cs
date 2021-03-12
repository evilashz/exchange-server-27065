using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FB RID: 763
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum TransitionTargetKindType
	{
		// Token: 0x04001134 RID: 4404
		Period,
		// Token: 0x04001135 RID: 4405
		Group
	}
}
