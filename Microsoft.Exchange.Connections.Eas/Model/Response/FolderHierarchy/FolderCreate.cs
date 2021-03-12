using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy
{
	// Token: 0x02000034 RID: 52
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderCreate")]
	[XmlRoot(Namespace = "FolderHierarchy", ElementName = "FolderCreate")]
	public class FolderCreate
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000046C2 File Offset: 0x000028C2
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000046CA File Offset: 0x000028CA
		[XmlElement(ElementName = "Status")]
		public byte Status { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000046D3 File Offset: 0x000028D3
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000046DB File Offset: 0x000028DB
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000046E4 File Offset: 0x000028E4
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000046EC File Offset: 0x000028EC
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }
	}
}
