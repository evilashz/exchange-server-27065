using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003AC RID: 940
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ReminderActionType
	{
		// Token: 0x040014C1 RID: 5313
		Dismiss,
		// Token: 0x040014C2 RID: 5314
		Snooze
	}
}
