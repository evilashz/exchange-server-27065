using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042F RID: 1071
	[XmlType(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum GetRemindersTypeReminderType
	{
		// Token: 0x0400169D RID: 5789
		All,
		// Token: 0x0400169E RID: 5790
		Current,
		// Token: 0x0400169F RID: 5791
		Old
	}
}
