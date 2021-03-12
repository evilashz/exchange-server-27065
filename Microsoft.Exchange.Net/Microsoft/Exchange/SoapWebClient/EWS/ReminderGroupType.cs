using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B7 RID: 695
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ReminderGroupType
	{
		// Token: 0x040011F8 RID: 4600
		Calendar,
		// Token: 0x040011F9 RID: 4601
		Task
	}
}
