using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005D3 RID: 1491
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IncludeInSchema = false)]
	[Serializable]
	public enum NotificationTypeEnum
	{
		// Token: 0x04001AFF RID: 6911
		CopiedEvent,
		// Token: 0x04001B00 RID: 6912
		CreatedEvent,
		// Token: 0x04001B01 RID: 6913
		DeletedEvent,
		// Token: 0x04001B02 RID: 6914
		FreeBusyChangedEvent,
		// Token: 0x04001B03 RID: 6915
		ModifiedEvent,
		// Token: 0x04001B04 RID: 6916
		MovedEvent,
		// Token: 0x04001B05 RID: 6917
		NewMailEvent,
		// Token: 0x04001B06 RID: 6918
		StatusEvent
	}
}
