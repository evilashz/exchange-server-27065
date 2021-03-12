using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x0200003A RID: 58
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderDelete")]
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderDelete")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FolderDelete
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00004801 File Offset: 0x00002A01
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00004809 File Offset: 0x00002A09
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00004812 File Offset: 0x00002A12
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000481A File Offset: 0x00002A1A
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }
	}
}
