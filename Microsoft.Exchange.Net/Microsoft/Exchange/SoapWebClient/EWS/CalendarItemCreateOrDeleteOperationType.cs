using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000472 RID: 1138
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum CalendarItemCreateOrDeleteOperationType
	{
		// Token: 0x04001769 RID: 5993
		SendToNone,
		// Token: 0x0400176A RID: 5994
		SendOnlyToAll,
		// Token: 0x0400176B RID: 5995
		SendToAllAndSaveCopy
	}
}
