using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.AutoDiscover
{
	// Token: 0x0200012C RID: 300
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ExchangeVersion
	{
		// Token: 0x040005E5 RID: 1509
		Exchange2010,
		// Token: 0x040005E6 RID: 1510
		Exchange2010_SP1,
		// Token: 0x040005E7 RID: 1511
		Exchange2010_SP2,
		// Token: 0x040005E8 RID: 1512
		Exchange2013,
		// Token: 0x040005E9 RID: 1513
		Exchange2013_SP1
	}
}
