using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000478 RID: 1144
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarItemUpdateOperationType
	{
		// Token: 0x04001786 RID: 6022
		SendToNone,
		// Token: 0x04001787 RID: 6023
		SendOnlyToAll,
		// Token: 0x04001788 RID: 6024
		SendOnlyToChanged,
		// Token: 0x04001789 RID: 6025
		SendToAllAndSaveCopy,
		// Token: 0x0400178A RID: 6026
		SendToChangedAndSaveCopy
	}
}
