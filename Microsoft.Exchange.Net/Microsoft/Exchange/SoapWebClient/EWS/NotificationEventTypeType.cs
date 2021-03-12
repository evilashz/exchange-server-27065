using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003B3 RID: 947
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum NotificationEventTypeType
	{
		// Token: 0x040014E8 RID: 5352
		CopiedEvent,
		// Token: 0x040014E9 RID: 5353
		CreatedEvent,
		// Token: 0x040014EA RID: 5354
		DeletedEvent,
		// Token: 0x040014EB RID: 5355
		ModifiedEvent,
		// Token: 0x040014EC RID: 5356
		MovedEvent,
		// Token: 0x040014ED RID: 5357
		NewMailEvent,
		// Token: 0x040014EE RID: 5358
		FreeBusyChangedEvent
	}
}
