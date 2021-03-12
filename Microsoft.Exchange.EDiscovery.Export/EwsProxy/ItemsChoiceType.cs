using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000233 RID: 563
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum ItemsChoiceType
	{
		// Token: 0x04000EC0 RID: 3776
		CopiedEvent,
		// Token: 0x04000EC1 RID: 3777
		CreatedEvent,
		// Token: 0x04000EC2 RID: 3778
		DeletedEvent,
		// Token: 0x04000EC3 RID: 3779
		FreeBusyChangedEvent,
		// Token: 0x04000EC4 RID: 3780
		ModifiedEvent,
		// Token: 0x04000EC5 RID: 3781
		MovedEvent,
		// Token: 0x04000EC6 RID: 3782
		NewMailEvent,
		// Token: 0x04000EC7 RID: 3783
		StatusEvent
	}
}
