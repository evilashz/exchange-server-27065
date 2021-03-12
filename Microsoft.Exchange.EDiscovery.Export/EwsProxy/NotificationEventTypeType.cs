using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002D2 RID: 722
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum NotificationEventTypeType
	{
		// Token: 0x04001096 RID: 4246
		CopiedEvent,
		// Token: 0x04001097 RID: 4247
		CreatedEvent,
		// Token: 0x04001098 RID: 4248
		DeletedEvent,
		// Token: 0x04001099 RID: 4249
		ModifiedEvent,
		// Token: 0x0400109A RID: 4250
		MovedEvent,
		// Token: 0x0400109B RID: 4251
		NewMailEvent,
		// Token: 0x0400109C RID: 4252
		FreeBusyChangedEvent
	}
}
