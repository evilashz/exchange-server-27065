using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000762 RID: 1890
	[Flags]
	[XmlType(TypeName = "NotificationEventTypeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum EventType
	{
		// Token: 0x04001F4D RID: 8013
		[XmlEnum("CopiedEvent")]
		Copied = 32,
		// Token: 0x04001F4E RID: 8014
		[XmlEnum("CreatedEvent")]
		Created = 2,
		// Token: 0x04001F4F RID: 8015
		[XmlEnum("DeletedEvent")]
		Deleted = 4,
		// Token: 0x04001F50 RID: 8016
		[XmlEnum("ModifiedEvent")]
		Modified = 8,
		// Token: 0x04001F51 RID: 8017
		[XmlEnum("MovedEvent")]
		Moved = 16,
		// Token: 0x04001F52 RID: 8018
		[XmlEnum("NewMailEvent")]
		NewMail = 1,
		// Token: 0x04001F53 RID: 8019
		[XmlEnum("FreeBusyChangedEvent")]
		FreeBusyChanged = 64
	}
}
