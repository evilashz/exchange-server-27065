using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000396 RID: 918
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum MessageDispositionType
	{
		// Token: 0x04001330 RID: 4912
		SaveOnly,
		// Token: 0x04001331 RID: 4913
		SendOnly,
		// Token: 0x04001332 RID: 4914
		SendAndSaveCopy
	}
}
