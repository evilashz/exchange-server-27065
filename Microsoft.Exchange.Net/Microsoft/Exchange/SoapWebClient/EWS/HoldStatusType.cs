using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000278 RID: 632
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum HoldStatusType
	{
		// Token: 0x04001048 RID: 4168
		NotOnHold,
		// Token: 0x04001049 RID: 4169
		Pending,
		// Token: 0x0400104A RID: 4170
		OnHold,
		// Token: 0x0400104B RID: 4171
		PartialHold,
		// Token: 0x0400104C RID: 4172
		Failed
	}
}
