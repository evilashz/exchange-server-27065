using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.FolderHierarchy
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderUpdate")]
	public class FolderUpdate
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004A9C File Offset: 0x00002C9C
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004AA4 File Offset: 0x00002CA4
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004AAD File Offset: 0x00002CAD
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00004AB5 File Offset: 0x00002CB5
		[XmlElement(ElementName = "ServerId")]
		public string ServerId { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004ABE File Offset: 0x00002CBE
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004AC6 File Offset: 0x00002CC6
		[XmlElement(ElementName = "ParentId")]
		public string ParentId { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00004ACF File Offset: 0x00002CCF
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004AD7 File Offset: 0x00002CD7
		[XmlElement(ElementName = "DisplayName")]
		public string DisplayName { get; set; }
	}
}
