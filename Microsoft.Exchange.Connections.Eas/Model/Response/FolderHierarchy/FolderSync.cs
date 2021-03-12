using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x02000040 RID: 64
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderSync")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderSync")]
	public class FolderSync
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00004997 File Offset: 0x00002B97
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000499F File Offset: 0x00002B9F
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000049A8 File Offset: 0x00002BA8
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000049B0 File Offset: 0x00002BB0
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000151 RID: 337 RVA: 0x000049B9 File Offset: 0x00002BB9
		// (set) Token: 0x06000152 RID: 338 RVA: 0x000049C1 File Offset: 0x00002BC1
		[XmlElement(ElementName = "Changes")]
		public Changes Changes { get; set; }
	}
}
