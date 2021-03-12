using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000434 RID: 1076
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum TeamMailboxLifecycleStateType
	{
		// Token: 0x040016A6 RID: 5798
		Active,
		// Token: 0x040016A7 RID: 5799
		Closed,
		// Token: 0x040016A8 RID: 5800
		Unlinked,
		// Token: 0x040016A9 RID: 5801
		PendingDelete
	}
}
