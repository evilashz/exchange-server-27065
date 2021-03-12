using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C4 RID: 1732
	[XmlType(TypeName = "AttachmentFieldURIType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum AttachmentUriEnum
	{
		// Token: 0x04001DEB RID: 7659
		[XmlEnum("item:Attachment")]
		Attachment,
		// Token: 0x04001DEC RID: 7660
		[XmlEnum("item:AttachmentContent")]
		AttachmentContent
	}
}
