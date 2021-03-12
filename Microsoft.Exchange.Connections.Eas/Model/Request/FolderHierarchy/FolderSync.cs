using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.FolderHierarchy
{
	// Token: 0x0200003E RID: 62
	[XmlType(Namespace = "FolderHierarchy", TypeName = "FolderSync")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class FolderSync
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004943 File Offset: 0x00002B43
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000494B File Offset: 0x00002B4B
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }
	}
}
