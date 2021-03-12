using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase
{
	// Token: 0x02000099 RID: 153
	[XmlType(Namespace = "AirSyncBase", TypeName = "Attachments")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class Attachments
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00009EC7 File Offset: 0x000080C7
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00009ECF File Offset: 0x000080CF
		[XmlElement(ElementName = "Attachment")]
		public Attachment Attachment { get; set; }
	}
}
