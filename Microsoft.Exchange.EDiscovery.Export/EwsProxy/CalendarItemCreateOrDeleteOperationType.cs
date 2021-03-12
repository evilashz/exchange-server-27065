using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000391 RID: 913
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum CalendarItemCreateOrDeleteOperationType
	{
		// Token: 0x04001317 RID: 4887
		SendToNone,
		// Token: 0x04001318 RID: 4888
		SendOnlyToAll,
		// Token: 0x04001319 RID: 4889
		SendToAllAndSaveCopy
	}
}
