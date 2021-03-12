using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.AirSyncBase
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSyncBase", TypeName = "Attachments")]
	public class Attachments
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000AFDD File Offset: 0x000091DD
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000AFE5 File Offset: 0x000091E5
		[XmlElement(ElementName = "Attachment")]
		public Attachment Attachment { get; set; }
	}
}
