using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200026F RID: 623
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ExternalAudience
	{
		// Token: 0x04000FC2 RID: 4034
		None,
		// Token: 0x04000FC3 RID: 4035
		Known,
		// Token: 0x04000FC4 RID: 4036
		All
	}
}
