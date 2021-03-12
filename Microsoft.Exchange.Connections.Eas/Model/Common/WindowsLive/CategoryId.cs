using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Common.WindowsLive
{
	// Token: 0x0200008B RID: 139
	[XmlType(Namespace = "WindowsLive", TypeName = "CategoryId")]
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class CategoryId
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000095E8 File Offset: 0x000077E8
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000095F0 File Offset: 0x000077F0
		[XmlText]
		public int Id { get; set; }
	}
}
