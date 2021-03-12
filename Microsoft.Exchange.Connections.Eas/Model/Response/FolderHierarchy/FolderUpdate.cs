using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x02000046 RID: 70
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderUpdate")]
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderUpdate")]
	public class FolderUpdate
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004AF0 File Offset: 0x00002CF0
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00004AF8 File Offset: 0x00002CF8
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004B01 File Offset: 0x00002D01
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00004B09 File Offset: 0x00002D09
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }
	}
}
