using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000314 RID: 788
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x04001312 RID: 4882
		CopiedEvent,
		// Token: 0x04001313 RID: 4883
		CreatedEvent,
		// Token: 0x04001314 RID: 4884
		DeletedEvent,
		// Token: 0x04001315 RID: 4885
		FreeBusyChangedEvent,
		// Token: 0x04001316 RID: 4886
		ModifiedEvent,
		// Token: 0x04001317 RID: 4887
		MovedEvent,
		// Token: 0x04001318 RID: 4888
		NewMailEvent,
		// Token: 0x04001319 RID: 4889
		StatusEvent
	}
}
