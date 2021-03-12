using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.FolderHierarchy
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderCreate")]
	public class FolderCreate
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000466E File Offset: 0x0000286E
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00004676 File Offset: 0x00002876
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000467F File Offset: 0x0000287F
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00004687 File Offset: 0x00002887
		[XmlElement(ElementName = "ParentId")]
		public string ParentId { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004690 File Offset: 0x00002890
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00004698 File Offset: 0x00002898
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000046A1 File Offset: 0x000028A1
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000046A9 File Offset: 0x000028A9
		[XmlElement(ElementName = "Type")]
		public int Type { get; set; }
	}
}
