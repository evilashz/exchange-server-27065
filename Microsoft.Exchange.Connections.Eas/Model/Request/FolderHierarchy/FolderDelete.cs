using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.FolderHierarchy
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderDelete")]
	public class FolderDelete
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000047CF File Offset: 0x000029CF
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000047D7 File Offset: 0x000029D7
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000047E0 File Offset: 0x000029E0
		// (set) Token: 0x06000136 RID: 310 RVA: 0x000047E8 File Offset: 0x000029E8
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
