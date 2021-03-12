using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000124 RID: 292
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum SensitivityChoicesType
	{
		// Token: 0x0400091D RID: 2333
		Normal,
		// Token: 0x0400091E RID: 2334
		Personal,
		// Token: 0x0400091F RID: 2335
		Private,
		// Token: 0x04000920 RID: 2336
		Confidential
	}
}
