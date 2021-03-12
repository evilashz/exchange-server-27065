using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200009A RID: 154
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public enum ExchangeVersion
	{
		// Token: 0x0400034E RID: 846
		Exchange2010,
		// Token: 0x0400034F RID: 847
		Exchange2010_SP1,
		// Token: 0x04000350 RID: 848
		Exchange2010_SP2,
		// Token: 0x04000351 RID: 849
		Exchange2013,
		// Token: 0x04000352 RID: 850
		Exchange2013_SP1
	}
}
